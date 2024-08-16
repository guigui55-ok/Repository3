using OpenFileDialogManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFileSample
{
    public partial class Form1 : Form
    {
        protected OpenFileDialogManager.OpenFileDialogManager openFileDialogManager;
        protected ErrorManager.ErrorManager error = new ErrorManager.ErrorManager(1);
        public Form1()
        {
            openFileDialogManager = new OpenFileDialogManager.OpenFileDialogManager(error);
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            //openFileDialogManager.OpenFileSample();
            // 初期化
            int ret = openFileDialogManager.Initialize();
            if (ret < 1) {MessageBox.Show(openFileDialogManager.Error.GetMesseges()); return; }
            // ダイアログを表示
            ret = Convert.ToInt32( openFileDialogManager.ShowDialog());
            // ファイル名を表示
            if (ret == 1)
            {
                // result.OK
                //richTextBox1.Text = openFileDialogManager.GetOpenFileDialog().FileName;
                ListToRichTextBox();
            } else if(ret < 0){
                // result.Cancel
                richTextBox1.Text = "Result.Cancel";
            } else if (ret == 0)
            {
                // exception
                richTextBox1.Text = "Rise Exception";
            }
        }

        private void ListToRichTextBox()
        {
            try
            {
                string[] ary = openFileDialogManager.GetOpenFileDialog().FileNames;
                if (ary is null) { throw new Exception("ary is null"); }
                if (ary.Length < 1) { throw new Exception("ary.Length < 1"); }
                foreach(string val in ary)
                {
                    richTextBox1.Text += val + "\n";
                }

            } catch (Exception ex)
            {
                MessageBox.Show("ListToRichTextBox Failed \n" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
