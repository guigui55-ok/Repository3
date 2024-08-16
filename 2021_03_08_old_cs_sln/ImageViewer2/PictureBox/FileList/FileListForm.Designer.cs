namespace FileList
{
    partial class FileListForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label_includeType = new System.Windows.Forms.Label();
            this.textBox_includeType = new System.Windows.Forms.TextBox();
            this.label_notIncludeType = new System.Windows.Forms.Label();
            this.textBox_notIncludeType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label_include_Filename = new System.Windows.Forms.Label();
            this.textBox_includeFilename = new System.Windows.Forms.TextBox();
            this.label_notIncludeFilename = new System.Windows.Forms.Label();
            this.textBox_notIncludeFilename = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(110, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(664, 73);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // label_includeType
            // 
            this.label_includeType.AutoSize = true;
            this.label_includeType.Location = new System.Drawing.Point(15, 96);
            this.label_includeType.Name = "label_includeType";
            this.label_includeType.Size = new System.Drawing.Size(67, 12);
            this.label_includeType.TabIndex = 2;
            this.label_includeType.Text = "include type";
            // 
            // textBox_includeType
            // 
            this.textBox_includeType.Location = new System.Drawing.Point(110, 93);
            this.textBox_includeType.Name = "textBox_includeType";
            this.textBox_includeType.Size = new System.Drawing.Size(664, 19);
            this.textBox_includeType.TabIndex = 3;
            // 
            // label_notIncludeType
            // 
            this.label_notIncludeType.AutoSize = true;
            this.label_notIncludeType.Location = new System.Drawing.Point(15, 118);
            this.label_notIncludeType.Name = "label_notIncludeType";
            this.label_notIncludeType.Size = new System.Drawing.Size(87, 12);
            this.label_notIncludeType.TabIndex = 4;
            this.label_notIncludeType.Text = "not include type";
            // 
            // textBox_notIncludeType
            // 
            this.textBox_notIncludeType.Location = new System.Drawing.Point(110, 115);
            this.textBox_notIncludeType.Name = "textBox_notIncludeType";
            this.textBox_notIncludeType.Size = new System.Drawing.Size(664, 19);
            this.textBox_notIncludeType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "file list";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(110, 185);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(664, 136);
            this.listBox1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(110, 335);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 8;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(this.panel1_DragOver);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "ここにドラッグ アンド ドロップ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(388, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Path から取得";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_include_Filename
            // 
            this.label_include_Filename.AutoSize = true;
            this.label_include_Filename.Location = new System.Drawing.Point(15, 140);
            this.label_include_Filename.Name = "label_include_Filename";
            this.label_include_Filename.Size = new System.Drawing.Size(88, 12);
            this.label_include_Filename.TabIndex = 10;
            this.label_include_Filename.Text = "include filename";
            // 
            // textBox_includeFilename
            // 
            this.textBox_includeFilename.Location = new System.Drawing.Point(110, 137);
            this.textBox_includeFilename.Name = "textBox_includeFilename";
            this.textBox_includeFilename.Size = new System.Drawing.Size(664, 19);
            this.textBox_includeFilename.TabIndex = 11;
            // 
            // label_notIncludeFilename
            // 
            this.label_notIncludeFilename.AutoSize = true;
            this.label_notIncludeFilename.Location = new System.Drawing.Point(15, 163);
            this.label_notIncludeFilename.Name = "label_notIncludeFilename";
            this.label_notIncludeFilename.Size = new System.Drawing.Size(102, 12);
            this.label_notIncludeFilename.TabIndex = 12;
            this.label_notIncludeFilename.Text = "not includ filename";
            // 
            // textBox_notIncludeFilename
            // 
            this.textBox_notIncludeFilename.Location = new System.Drawing.Point(110, 160);
            this.textBox_notIncludeFilename.Name = "textBox_notIncludeFilename";
            this.textBox_notIncludeFilename.Size = new System.Drawing.Size(664, 19);
            this.textBox_notIncludeFilename.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(543, 412);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "ランダム切り替え";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FileListForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 447);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_notIncludeFilename);
            this.Controls.Add(this.label_notIncludeFilename);
            this.Controls.Add(this.textBox_includeFilename);
            this.Controls.Add(this.label_include_Filename);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_notIncludeType);
            this.Controls.Add(this.label_notIncludeType);
            this.Controls.Add(this.textBox_includeType);
            this.Controls.Add(this.label_includeType);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Name = "FileListForm";
            this.Text = "FileList";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label_includeType;
        private System.Windows.Forms.TextBox textBox_includeType;
        private System.Windows.Forms.Label label_notIncludeType;
        private System.Windows.Forms.TextBox textBox_notIncludeType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_include_Filename;
        private System.Windows.Forms.TextBox textBox_includeFilename;
        private System.Windows.Forms.Label label_notIncludeFilename;
        private System.Windows.Forms.TextBox textBox_notIncludeFilename;
        private System.Windows.Forms.Button button2;
    }
}

