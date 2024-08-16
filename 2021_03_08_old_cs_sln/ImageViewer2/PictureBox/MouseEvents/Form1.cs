using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorLog;

namespace MouseEvents
{
    public partial class Form1 : Form
    {
        ErrorLog.IErrorLog _errorLog;
        TestMouseEventsMain _testMouseEventsMain;
        public Form1()
        {
            InitializeComponent();
            GlobalErrloLog.ErrorLog = new ErrorLog.ErrorLog();
            _errorLog = GlobalErrloLog.ErrorLog;
            _testMouseEventsMain = new TestMouseEventsMain(this,this.panel1,_errorLog);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
