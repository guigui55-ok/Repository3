namespace Log
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_priority = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_loglevel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_classname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_function = new System.Windows.Forms.TextBox();
            this.textBox_parameter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_filename = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_Suppress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_OutPutForm = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label_tractability = new System.Windows.Forms.Label();
            this.textBox_tractability = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "LogForm表示";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "priority";
            // 
            // textBox_priority
            // 
            this.textBox_priority.Location = new System.Drawing.Point(99, 6);
            this.textBox_priority.Name = "textBox_priority";
            this.textBox_priority.Size = new System.Drawing.Size(66, 19);
            this.textBox_priority.TabIndex = 2;
            this.textBox_priority.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "LogLevel";
            // 
            // textBox_loglevel
            // 
            this.textBox_loglevel.Location = new System.Drawing.Point(99, 38);
            this.textBox_loglevel.Name = "textBox_loglevel";
            this.textBox_loglevel.Size = new System.Drawing.Size(66, 19);
            this.textBox_loglevel.TabIndex = 4;
            this.textBox_loglevel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "ClassName";
            // 
            // textBox_classname
            // 
            this.textBox_classname.Location = new System.Drawing.Point(99, 69);
            this.textBox_classname.Name = "textBox_classname";
            this.textBox_classname.Size = new System.Drawing.Size(125, 19);
            this.textBox_classname.TabIndex = 6;
            this.textBox_classname.Text = "TestClassName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "FunctionName";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Parameter";
            // 
            // textBox_function
            // 
            this.textBox_function.Location = new System.Drawing.Point(99, 97);
            this.textBox_function.Name = "textBox_function";
            this.textBox_function.Size = new System.Drawing.Size(125, 19);
            this.textBox_function.TabIndex = 9;
            this.textBox_function.Text = "TestFunctionName";
            // 
            // textBox_parameter
            // 
            this.textBox_parameter.Location = new System.Drawing.Point(99, 125);
            this.textBox_parameter.Name = "textBox_parameter";
            this.textBox_parameter.Size = new System.Drawing.Size(125, 19);
            this.textBox_parameter.TabIndex = 10;
            this.textBox_parameter.Text = "TestParameter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "LogPath";
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(99, 181);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(290, 19);
            this.textBox_path.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "LogFileName";
            // 
            // textBox_filename
            // 
            this.textBox_filename.Location = new System.Drawing.Point(99, 212);
            this.textBox_filename.Name = "textBox_filename";
            this.textBox_filename.Size = new System.Drawing.Size(290, 19);
            this.textBox_filename.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "同じ内容を抑制";
            // 
            // textBox_Suppress
            // 
            this.textBox_Suppress.Location = new System.Drawing.Point(128, 251);
            this.textBox_Suppress.Name = "textBox_Suppress";
            this.textBox_Suppress.Size = new System.Drawing.Size(51, 19);
            this.textBox_Suppress.TabIndex = 16;
            this.textBox_Suppress.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 284);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "IsOutPutLogToForm";
            // 
            // textBox_OutPutForm
            // 
            this.textBox_OutPutForm.Location = new System.Drawing.Point(128, 281);
            this.textBox_OutPutForm.Name = "textBox_OutPutForm";
            this.textBox_OutPutForm.Size = new System.Drawing.Size(51, 19);
            this.textBox_OutPutForm.TabIndex = 18;
            this.textBox_OutPutForm.Text = "1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(156, 380);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "設定値反映";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(258, 380);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "ログ記録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label_tractability
            // 
            this.label_tractability.AutoSize = true;
            this.label_tractability.Location = new System.Drawing.Point(18, 311);
            this.label_tractability.Name = "label_tractability";
            this.label_tractability.Size = new System.Drawing.Size(104, 12);
            this.label_tractability.TabIndex = 21;
            this.label_tractability.Text = "LogWindow追従する";
            // 
            // textBox_tractability
            // 
            this.textBox_tractability.Location = new System.Drawing.Point(129, 311);
            this.textBox_tractability.Name = "textBox_tractability";
            this.textBox_tractability.Size = new System.Drawing.Size(50, 19);
            this.textBox_tractability.TabIndex = 22;
            this.textBox_tractability.Text = "1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(369, 380);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "ログをファイルへ出力";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(492, 415);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox_tractability);
            this.Controls.Add(this.label_tractability);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_OutPutForm);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_Suppress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_filename);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_parameter);
            this.Controls.Add(this.textBox_function);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_classname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_loglevel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_priority);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "LogTest";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_priority;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_loglevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_classname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_function;
        private System.Windows.Forms.TextBox textBox_parameter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_filename;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_Suppress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_OutPutForm;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label_tractability;
        private System.Windows.Forms.TextBox textBox_tractability;
        private System.Windows.Forms.Button button4;
    }
}

