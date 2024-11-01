using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormInfo
{
    public partial class SubInfoForm : Form
    {
        public SubInfoForm()
        {
            InitializeComponent();
        }

        private void SubInfoForm_Closing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        public void outputInfoToTextBox(FormInfoData info)
        {
            string buf = info.getInfoStr();
            richTextBox1.Text = buf;
        }

        private void SubInfoForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(100, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }
    }
}
