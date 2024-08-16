using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolBarSample
{
    public partial class ToolBarSampleForm : Form
    {
        private System.Windows.Forms.ToolStrip toolStrip1;  // ツールバーの土台となるオブジェクト
        private System.Windows.Forms.ToolStripButton toolStripButton1;  // ツールバーのボタン1
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;    // ツールバーのセパレータ
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;  // ツールバーのラベル1
        private ToolStripContainer toolStripContainer1;
        public ToolBarSampleForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TestToolBarMethod();
        }

        private void TestToolBarMethod()
        {
            try
            {
                // フォームをロードした時の処理
                // レイアウトを一時停止
                this.SuspendLayout();



                // ToolStripクラスを生成します
                this.toolStrip1 = new ToolStrip();

                // ツールバーのレイアウトを一時停止
                this.toolStrip1.SuspendLayout();

                //
                // ツールバーの中に追加するコントロールを生成する
                // ここから --->

                // ToolStripButtonを作成
                this.toolStripButton1 = new ToolStripButton();
                // テキストを設定
                this.toolStripButton1.Text = "開く(&O)";
                // 画像を設定
                this.toolStripButton1.Image = Image.FromFile(GetProjectFolder() + @"\Icon1_openfile.ico");
                // 画像だけを表示するボタンにします
                this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;

                // ToolStripにボタンを追加します
                this.toolStrip1.Items.Add(this.toolStripButton1);

                // ToolStripSeparatorを作成
                this.toolStripSeparator1 = new ToolStripSeparator();
                // ToolStripにセパレータを追加します
                this.toolStrip1.Items.Add(this.toolStripSeparator1);

                // ToolStripLabelを作成
                this.toolStripLabel1 = new ToolStripLabel("らべるです");

                Debug.WriteLine("current directry = " + GetProjectFolder()) ;

                // イメージを設定
                this.toolStripLabel1.Image = Image.FromFile(GetProjectFolder() + @"\Icon1_openfile.ico");
                // ToolStripにラベルを追加します
                this.toolStrip1.Items.Add(this.toolStripLabel1);


                //this.toolStripContainer1 = new ToolStripContainer();
                //toolStripContainer1.TopToolStripPanel.BackColor = Color.Aqua;
                //this.Controls.Add(toolStripContainer1);
                //toolStripContainer1.Dock = DockStyle.Fill;
                //toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);

                // フォームにToolStrip(ツールバー)を追加
                this.Controls.Add(this.toolStrip1);

                // ---> ここまで
                // ツールバーの中に追加するコントロールを生成する
                //

                // ツールバーのレイアウトを再開
                this.toolStrip1.ResumeLayout(false);
                this.toolStrip1.PerformLayout();

                //toolStripContainer1.ResumeLayout(false);
                //toolStripContainer1.PerformLayout();
                // レイアウトを再開
                this.ResumeLayout(false);
                this.PerformLayout();
            } catch (Exception ex)
            {
                MessageBox.Show("ToolBarSampleForm.TestToolBarMethod");
                MessageBox.Show(ex.Message);
            }
        }

        private string GetProjectFolder()
        {
            return Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
        }
    }
}
