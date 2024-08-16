using System;
using System.IO;
using System.Windows.Forms;
using ErrorLog;

namespace Log
{
    public partial class Form1 : Form
    {
        ErrorLog.IErrorLog _errorLog;
        public Log _log;
        TestLog _testLog;
        string path;
        string filename;

        FormAddEvents _formAddEvents;
        public Form1()
        {
            InitializeComponent();
            GlobalErrloLog.ErrorLog = new ErrorLog.ErrorLog();
            _errorLog = GlobalErrloLog.ErrorLog;
            _log = new Log();
            _testLog = new TestLog();
            _formAddEvents = new FormAddEvents(this);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            path = Directory.GetCurrentDirectory();
            filename = "testlog.log";
            textBox_path.Text = path;
            textBox_filename.Text = filename;
            _testLog.TestInitialize(this,_log,path,filename);
            
            _errorLog.ShowErrorMessage();

            // 値を更新
            if (_log.IsOutPutLogToForm)
            {
                textBox_OutPutForm.Text = "1";
            } else
            {
                textBox_OutPutForm.Text = "0";
            }
            if (_log.IsSuppressLogWhenAddValueSameLastOfList)
            {
                textBox_Suppress.Text = "1";
            } else
            {
                textBox_Suppress.Text = "0";
            }
            //
            if (_log.TractabilityWithMainWindow)
            {
                this.textBox_tractability.Text = "1";
            } else
            {
                this.textBox_tractability.Text = "0";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _testLog.TestLog1(this,_log,path,filename);
            _errorLog.ShowErrorMessage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _testLog.TestApplySettings(
                    textBox_path.Text,
                    textBox_filename.Text,
                    textBox_OutPutForm.Text,
                    textBox_Suppress.Text,
                    textBox_tractability.Text);
                if (!_errorLog.ShowErrorMessage()) { MessageBox.Show("Success"); }
                
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "showWindow");
            }                
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            // ログ記録
            try
            {
                // priority
                int tmpint;
                int priority;
                if (int.TryParse(textBox_priority.Text, out tmpint))
                {
                    priority = tmpint;
                } else {
                    priority = 0;
                }

                // level
                int level;
                if (int.TryParse(textBox_loglevel.Text, out tmpint))
                {
                    level = tmpint;
                }
                else
                {
                    level = 0;
                }

                // addlog
                _log.addLog(priority,textBox_function.Text,textBox_parameter.Text);

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "ログ記録");
            }

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            _log.FollowParentWindow();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            _log.FollowParentWindow();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
