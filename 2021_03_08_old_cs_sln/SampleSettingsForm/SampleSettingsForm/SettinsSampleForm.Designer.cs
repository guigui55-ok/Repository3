
namespace SampleSettingsForm
{
    partial class SettinsSampleForm
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
            this.TabControl_Settings = new System.Windows.Forms.TabControl();
            this.TabPage_SettingsGeneral = new System.Windows.Forms.TabPage();
            this.TabPage_SettingsOptions = new System.Windows.Forms.TabPage();
            this.Button_SettingsClose = new System.Windows.Forms.Button();
            this.Button_SettingsApply = new System.Windows.Forms.Button();
            this.Panel_SettingsGeneral = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TabControl_Settings.SuspendLayout();
            this.TabPage_SettingsGeneral.SuspendLayout();
            this.Panel_SettingsGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl_Settings
            // 
            this.TabControl_Settings.Controls.Add(this.TabPage_SettingsGeneral);
            this.TabControl_Settings.Controls.Add(this.TabPage_SettingsOptions);
            this.TabControl_Settings.Location = new System.Drawing.Point(12, 12);
            this.TabControl_Settings.Name = "TabControl_Settings";
            this.TabControl_Settings.SelectedIndex = 0;
            this.TabControl_Settings.Size = new System.Drawing.Size(637, 332);
            this.TabControl_Settings.TabIndex = 0;
            // 
            // TabPage_SettingsGeneral
            // 
            this.TabPage_SettingsGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_SettingsGeneral.Controls.Add(this.Panel_SettingsGeneral);
            this.TabPage_SettingsGeneral.Location = new System.Drawing.Point(4, 22);
            this.TabPage_SettingsGeneral.Name = "TabPage_SettingsGeneral";
            this.TabPage_SettingsGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_SettingsGeneral.Size = new System.Drawing.Size(629, 306);
            this.TabPage_SettingsGeneral.TabIndex = 0;
            this.TabPage_SettingsGeneral.Text = "General";
            // 
            // TabPage_SettingsOptions
            // 
            this.TabPage_SettingsOptions.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_SettingsOptions.Location = new System.Drawing.Point(4, 22);
            this.TabPage_SettingsOptions.Name = "TabPage_SettingsOptions";
            this.TabPage_SettingsOptions.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_SettingsOptions.Size = new System.Drawing.Size(629, 306);
            this.TabPage_SettingsOptions.TabIndex = 1;
            this.TabPage_SettingsOptions.Text = "Options";
            // 
            // Button_SettingsClose
            // 
            this.Button_SettingsClose.Location = new System.Drawing.Point(570, 358);
            this.Button_SettingsClose.Name = "Button_SettingsClose";
            this.Button_SettingsClose.Size = new System.Drawing.Size(75, 23);
            this.Button_SettingsClose.TabIndex = 0;
            this.Button_SettingsClose.Text = "Close";
            this.Button_SettingsClose.UseVisualStyleBackColor = true;
            this.Button_SettingsClose.Click += new System.EventHandler(this.Button_SettingsClose_Click);
            // 
            // Button_SettingsApply
            // 
            this.Button_SettingsApply.Location = new System.Drawing.Point(489, 358);
            this.Button_SettingsApply.Name = "Button_SettingsApply";
            this.Button_SettingsApply.Size = new System.Drawing.Size(75, 23);
            this.Button_SettingsApply.TabIndex = 1;
            this.Button_SettingsApply.Text = "Apply";
            this.Button_SettingsApply.UseVisualStyleBackColor = true;
            // 
            // Panel_SettingsGeneral
            // 
            this.Panel_SettingsGeneral.Controls.Add(this.checkBox1);
            this.Panel_SettingsGeneral.Location = new System.Drawing.Point(3, 0);
            this.Panel_SettingsGeneral.Name = "Panel_SettingsGeneral";
            this.Panel_SettingsGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.Panel_SettingsGeneral.Size = new System.Drawing.Size(623, 303);
            this.Panel_SettingsGeneral.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 393);
            this.Controls.Add(this.Button_SettingsApply);
            this.Controls.Add(this.Button_SettingsClose);
            this.Controls.Add(this.TabControl_Settings);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TabControl_Settings.ResumeLayout(false);
            this.TabPage_SettingsGeneral.ResumeLayout(false);
            this.Panel_SettingsGeneral.ResumeLayout(false);
            this.Panel_SettingsGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl_Settings;
        private System.Windows.Forms.TabPage TabPage_SettingsGeneral;
        private System.Windows.Forms.Panel Panel_SettingsGeneral;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage TabPage_SettingsOptions;
        private System.Windows.Forms.Button Button_SettingsClose;
        private System.Windows.Forms.Button Button_SettingsApply;
    }
}

