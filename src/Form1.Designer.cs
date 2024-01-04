namespace FindLongFilenamesLinux
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.targetDir = new System.Windows.Forms.TextBox();
            this.find = new System.Windows.Forms.Button();
            this.outputList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rename = new System.Windows.Forms.Button();
            this.showFile = new System.Windows.Forms.Button();
            this.autoRename = new System.Windows.Forms.Button();
            this.setDir = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.current = new System.Windows.Forms.Label();
            this.about = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // targetDir
            // 
            this.targetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetDir.Location = new System.Drawing.Point(14, 38);
            this.targetDir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.targetDir.Name = "targetDir";
            this.targetDir.Size = new System.Drawing.Size(985, 23);
            this.targetDir.TabIndex = 0;
            // 
            // find
            // 
            this.find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.find.Location = new System.Drawing.Point(1102, 36);
            this.find.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(89, 28);
            this.find.TabIndex = 1;
            this.find.Text = "Начать";
            this.find.UseVisualStyleBackColor = true;
            this.find.Click += new System.EventHandler(this.find_Click);
            // 
            // outputList
            // 
            this.outputList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputList.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outputList.FormattingEnabled = true;
            this.outputList.HorizontalScrollbar = true;
            this.outputList.ItemHeight = 16;
            this.outputList.Location = new System.Drawing.Point(14, 96);
            this.outputList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.outputList.Name = "outputList";
            this.outputList.Size = new System.Drawing.Size(1176, 356);
            this.outputList.TabIndex = 2;
            this.outputList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.outputList_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Проверяемая папка:";
            // 
            // rename
            // 
            this.rename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rename.Location = new System.Drawing.Point(14, 478);
            this.rename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(154, 28);
            this.rename.TabIndex = 4;
            this.rename.Text = "Переименовать";
            this.rename.UseVisualStyleBackColor = true;
            this.rename.Click += new System.EventHandler(this.rename_Click);
            // 
            // showFile
            // 
            this.showFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showFile.Location = new System.Drawing.Point(175, 478);
            this.showFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.showFile.Name = "showFile";
            this.showFile.Size = new System.Drawing.Size(154, 28);
            this.showFile.TabIndex = 4;
            this.showFile.Text = "Показать в папке";
            this.showFile.UseVisualStyleBackColor = true;
            this.showFile.Click += new System.EventHandler(this.showFile_Click);
            // 
            // autoRename
            // 
            this.autoRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoRename.Location = new System.Drawing.Point(362, 478);
            this.autoRename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.autoRename.Name = "autoRename";
            this.autoRename.Size = new System.Drawing.Size(239, 28);
            this.autoRename.TabIndex = 5;
            this.autoRename.Text = "Переименовать автоматически";
            this.autoRename.UseVisualStyleBackColor = true;
            this.autoRename.Click += new System.EventHandler(this.autoRename_Click);
            // 
            // setDir
            // 
            this.setDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setDir.Location = new System.Drawing.Point(1007, 36);
            this.setDir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.setDir.Name = "setDir";
            this.setDir.Size = new System.Drawing.Size(89, 28);
            this.setDir.TabIndex = 6;
            this.setDir.Text = "Открыть";
            this.setDir.UseVisualStyleBackColor = true;
            this.setDir.Click += new System.EventHandler(this.setDir_Click);
            // 
            // current
            // 
            this.current.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.current.ForeColor = System.Drawing.Color.DarkCyan;
            this.current.Location = new System.Drawing.Point(14, 67);
            this.current.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.current.Name = "current";
            this.current.Size = new System.Drawing.Size(1177, 18);
            this.current.TabIndex = 7;
            // 
            // about
            // 
            this.about.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.about.Location = new System.Drawing.Point(1036, 479);
            this.about.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(154, 28);
            this.about.TabIndex = 8;
            this.about.Text = "О программе...";
            this.about.UseVisualStyleBackColor = true;
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 519);
            this.Controls.Add(this.about);
            this.Controls.Add(this.current);
            this.Controls.Add(this.setDir);
            this.Controls.Add(this.autoRename);
            this.Controls.Add(this.showFile);
            this.Controls.Add(this.rename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputList);
            this.Controls.Add(this.find);
            this.Controls.Add(this.targetDir);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1021, 458);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск файлов с длинными именами для Linux";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox targetDir;
        private System.Windows.Forms.Button find;
        private System.Windows.Forms.ListBox outputList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button rename;
        private System.Windows.Forms.Button showFile;
        private System.Windows.Forms.Button autoRename;
        private System.Windows.Forms.Button setDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label current;
        private System.Windows.Forms.Button about;
    }
}

