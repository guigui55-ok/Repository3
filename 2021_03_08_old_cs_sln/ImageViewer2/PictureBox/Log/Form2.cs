using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log
{
    public partial class Form2 : Form
    {
        public LogFormManager _logFormManager;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(LogFormManager logFormManager)
        {
            _logFormManager = logFormManager;
            InitializeComponent();
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            _logFormManager.FollowParentForm();
        }
    }
}
