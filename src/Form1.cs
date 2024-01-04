/**********************************************************************/
/* Copyright (c) 2024 Carpe Diem Software Developing by Alex Versetty */
/* http://carpediem.0fees.us                                          */
/**********************************************************************/

using CDSD.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindLongFilenamesLinux
{
    public partial class Form1 : Form
    {
        const int LB_SETITEMHEIGHT = 0x1A0;
        
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

        const int ITEM_NUM_WIDTH = 24;
        const int ITEM_PADDING = 3;
        bool scanningActive = false;
        bool cancelScan = false;

        public Form1()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            outputList.DrawMode = DrawMode.OwnerDrawVariable;
            outputList.MeasureItem += OutputList_MeasureItem;
            outputList.DrawItem += OutputList_DrawItem;
            outputList.Resize += (sender, args) => ForceMeasureItems(outputList, OutputList_MeasureItem);
        }

        void ForceMeasureItems(ListBox listBox, Action<object, MeasureItemEventArgs> onMeasureEvent)
        {
            for (int i = 0; i < outputList.Items.Count; i++)
            {
                MeasureItemEventArgs eArgs = new MeasureItemEventArgs(listBox.CreateGraphics(), i);
                onMeasureEvent(listBox, eArgs);
                SendMessage(listBox.Handle, LB_SETITEMHEIGHT, (IntPtr)i, (IntPtr)eArgs.ItemHeight);
            }
            listBox.Refresh();
        }

        private void OutputList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            
            Rectangle numBounds = new Rectangle(e.Bounds.X, e.Bounds.Y + ITEM_PADDING, ITEM_NUM_WIDTH, e.Bounds.Height - ITEM_PADDING * 2);
            string num = (e.Index + 1).ToString();
            e.Graphics.DrawString(num, e.Font, new SolidBrush(Color.DarkOrange), numBounds);
            
            Rectangle textBounds = new Rectangle(e.Bounds.X + ITEM_NUM_WIDTH, e.Bounds.Y + ITEM_PADDING, e.Bounds.Width - ITEM_NUM_WIDTH, e.Bounds.Height - ITEM_PADDING * 2);
            string text = outputList.Items[e.Index].ToString();
            e.Graphics.DrawString(text, e.Font, new SolidBrush(e.ForeColor), textBounds);
            
            e.DrawFocusRectangle();
        }

        private void OutputList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;
            string text = outputList.Items[e.Index].ToString();
            SizeF sz = e.Graphics.MeasureString(text, outputList.Font, outputList.Width - ITEM_NUM_WIDTH - 4);
            e.ItemHeight = (int) Math.Ceiling(sz.Height) + ITEM_PADDING * 2;
        }

        private void find_Click(object sender, EventArgs e)
        {
            if (scanningActive)
            {
                cancelScan = true;
            }
            else
            {
                ConvertToUNC();
                outputList.Items.Clear();
                scanningActive = true;
                UpdateFormByScanningStatus();

                var t = Task.Factory.StartNew(() =>
                {
                    FindProblemFiles(targetDir.Text);
                    cancelScan = false;
                    scanningActive = false;
                    UpdateFormByScanningStatus();
                });
            }
        }

        void ConvertToUNC()
        {
            Regex rg = new Regex(@"^.:\\");
            if (rg.Match(targetDir.Text).Success) targetDir.Text = @"\\?\" + targetDir.Text;
        }

        void UpdateFormByScanningStatus()
        {
            if (InvokeRequired) Invoke((Action)UpdateFormByScanningStatus); 
            else
            {
                if (scanningActive)
                {
                    Text = "Поиск файлов с длинными именами для Linux... Идет сканирование";
                    find.Text = "Стоп";
                }
                else
                {
                    Text = "Поиск файлов с длинными именами для Linux";
                    find.Text = "Начать";
                    current.Text = "";
                }
            };
        }

        void FindProblemFiles(string path)
        {
            UpdateCurrent(path);

            try
            {
                if (cancelScan) return;
                var files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

                foreach (var item in files)
                {
                    string filename = Path.GetFileName(item);
                    if (Encoding.UTF8.GetByteCount(filename) > 255) AddToOutput(item);
                }

                string dirname = Path.GetFileName(path);
                if (Encoding.UTF8.GetByteCount(dirname) > 255) AddToOutput(path);

                if (cancelScan) return;
                var dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
                foreach (var d in dirs) FindProblemFiles(d);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void AddToOutput(string item)
        {
            if (InvokeRequired) Invoke((Action<string>)AddToOutput, item);
            else outputList.Items.Add(item);
        }
        
        void UpdateCurrent(string item)
        {
            if (InvokeRequired) Invoke((Action<string>)UpdateCurrent, item);
            else current.Text = "Сканирую: " + item;
        }

        private void rename_Click(object sender, EventArgs e)
        {
            var filename = outputList.SelectedItem as string;
            if (filename == null) return;
            var f = new FormRename(filename);
            f.ShowDialog();
            if (f.RenamedOK) outputList.Items.RemoveAt(outputList.SelectedIndex);
        }

        private void showFile_Click(object sender, EventArgs e)
        {
            var filename = outputList.SelectedItem as string;
            if (filename == null) return;
            ShowInExplorer(Path.GetDirectoryName(filename).Substring(4), Path.GetFileName(filename));
        }

        void ShowInExplorer(string dir, string file)
        {
            IntPtr nativeFolder;
            IntPtr nativeFile;
            IntPtr[] filesToDisplay;
            uint psfgaoOut;
            
            SHParseDisplayName(dir, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);
            if (nativeFolder == IntPtr.Zero) return; //dir not found

            SHParseDisplayName(Path.Combine(dir, file), IntPtr.Zero, out nativeFile, 0, out psfgaoOut);
            if (nativeFile == IntPtr.Zero) filesToDisplay = new IntPtr[0]; //file not found
            else filesToDisplay = new IntPtr[] { nativeFile };
            
            SHOpenFolderAndSelectItems(nativeFolder, (uint)filesToDisplay.Length, filesToDisplay, 0);

            Marshal.FreeCoTaskMem(nativeFolder);
            if (nativeFile != IntPtr.Zero) Marshal.FreeCoTaskMem(nativeFile);
        }

        private void autoRename_Click(object sender, EventArgs e)
        {
            var filePath = outputList.SelectedItem as string;
            if (filePath == null) return;

            var dir = Path.GetDirectoryName(filePath);
            var filenameNoExt = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath);
            int extLen = Encoding.UTF8.GetByteCount(ext);

            while ((Encoding.UTF8.GetByteCount(filenameNoExt) + extLen) > 255)
            {
                filenameNoExt = filenameNoExt.Substring(0, filenameNoExt.Length - 1);
            }

            if (Directory.Exists(Path.Combine(dir, filenameNoExt + ext)) || File.Exists(Path.Combine(dir, filenameNoExt + ext)))
            {
                const int suffixMaxLen = 5;
                var filenameNoExt_base = filenameNoExt.Substring(0, filenameNoExt.Length - suffixMaxLen);
                int i = 2;

                while (i < 99 && (Directory.Exists(Path.Combine(dir, filenameNoExt + ext)) || File.Exists(Path.Combine(dir, filenameNoExt + ext))))
                {
                    filenameNoExt = $"{filenameNoExt_base} ({i++})";
                }
            }

            if (Directory.Exists(Path.Combine(dir, filenameNoExt + ext)) || File.Exists(Path.Combine(dir, filenameNoExt + ext)))
            {
                //ну его нафиг тогда
                MessageBox.Show("Не удалось автоматически переименовать", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string text = $"{Path.GetFileName(filePath)}\n\r\n\r➔\n\r\n\r{filenameNoExt}{ext}";
            var r = MessageBox.Show(text, "Подтвердите переименование", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            
            if (r == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(filePath)) File.Move(filePath, Path.Combine(dir, filenameNoExt + ext));
                    else Directory.Move(filePath, Path.Combine(dir, filenameNoExt + ext));
                    
                    outputList.Items.RemoveAt(outputList.SelectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка переименования", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void setDir_Click(object sender, EventArgs e)
        {
            var r = folderBrowserDialog1.ShowDialog();
            if (r == DialogResult.OK) targetDir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void outputList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showFile.PerformClick();
        }

        private void about_Click(object sender, EventArgs e)
        {
            var f = new FAbout();
            f.ShowDialog();
        }
    }
}
