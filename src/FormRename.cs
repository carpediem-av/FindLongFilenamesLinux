/**********************************************************************/
/* Copyright (c) 2024 Carpe Diem Software Developing by Alex Versetty */
/* http://carpediem.0fees.us                                          */
/**********************************************************************/

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FindLongFilenamesLinux
{
    public partial class FormRename : Form
    {
        string filename;
        string ext;
        int extLen;
        public bool RenamedOK { get; set; }

        public FormRename(string filename)
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.filename = filename;
            ext = Path.GetExtension(filename);
            extLen = Encoding.UTF8.GetByteCount(ext);
            textBox1.Text = Path.GetFileNameWithoutExtension(filename);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int len = Encoding.UTF8.GetByteCount(textBox1.Text) + extLen;
            label1.Text = $"Длина: {len}";

            if (len <= 255)
            {
                label1.ForeColor = Color.Green;
                button1.Enabled = true;
            }
            else
            {
                label1.ForeColor = Color.Red;
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string newFilename = Path.Combine(Path.GetDirectoryName(filename), textBox1.Text + ext);

                if (File.Exists(filename)) File.Move(filename, newFilename);
                else Directory.Move(filename, newFilename);

                RenamedOK = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка переименования", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
