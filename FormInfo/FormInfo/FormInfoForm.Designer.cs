
namespace FormInfo
{
    partial class FormInfoForm
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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(295, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show SubForm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 275);
            this.Controls.Add(this.button1);
            this.Name = "FormInfoForm";
            this.Text = "FormInfo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormInfoForm_FormClosed);
            this.Load += new System.EventHandler(this.FormInfoForm_Load);
            this.DoubleClick += new System.EventHandler(this.FormInfoForm_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInfoForm_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormInfoForm_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormInfoForm_MouseMove);
            this.Move += new System.EventHandler(this.FormInfoForm_Move);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
    }
}

