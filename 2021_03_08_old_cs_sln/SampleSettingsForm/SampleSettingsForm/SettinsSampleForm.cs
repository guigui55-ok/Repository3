using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleSettingsForm
{
    public partial class SettinsSampleForm : Form
    {
        public SettinsSampleForm()
        {
            InitializeComponent();
            Console.WriteLine("checkBox1.Height = " + checkBox1.Height);
        }

        private void Button_SettingsClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
