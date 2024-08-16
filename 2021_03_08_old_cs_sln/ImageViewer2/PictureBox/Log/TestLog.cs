using ErrorLog;
using System;
using System.Windows.Forms;

namespace Log
{
    class TestLog
    {
        ErrorLog.IErrorLog _errorLog;
        Log _log;

        public TestLog()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
        }
        public void TestInitialize(Form form,Log log,string path,string filename)
        {
            int ret;
            try
            {
                if (log == null)
                {
                    _errorLog.addErrorNotException(this.ToString(), "log is null");
                    return;
                }
                _log = log;
                //　初期化
                ret = _log.initialize(3, 3, true, path,filename);
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "initialize");
                }
                // set form
                ret = _log.setParintForm(form);
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "setParintForm");
                }

            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "TestInitialize");
            }
        }

        // initialize + show
        public void TestLog1(Form form,Log log,string path,string filename)
        {
            int ret;
            try
            {
                if (log == null)
                {
                    _errorLog.addErrorNotException(this.ToString(), "log is null");
                    return;
                }
                _log = log;
                //　初期化
                ret = _log.initialize(3, 3, true, path, filename);
                if (ret < 1)
                {
                    _errorLog.addErrorNotException( this.ToString(), "initialize");
                }
                // set form
                ret = _log.setParintForm(form);
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "setParintForm");
                }

                // show
                _log.showWindow();

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "TestLog1");
            }
        }

        // Setting apply
        public void TestApplySettings(string textBox_path,string textBox_filename,string textBox_OutPutForm,string textBox_Suppress, string textBox_tractability)
        {
            try
            {
                // path
                _log.setLogFullPath(textBox_path, textBox_filename);
                if (_errorLog.haveError())
                {
                    _errorLog.ShowErrorMessage();
                    return;
                }
                // OutputForm
                int tmpint;
                if (int.TryParse(textBox_OutPutForm, out tmpint))
                {
                    if (tmpint == 0)
                    {
                        _log.IsOutPutLogToForm = false;
                    }
                    else
                    {
                        _log.IsOutPutLogToForm = true;
                    }
                }
                else
                {
                    _log.IsOutPutLogToForm = false;
                }

                // suppress
                if (int.TryParse(textBox_Suppress, out tmpint))
                {
                    if (tmpint == 0)
                    {
                        _log.IsSuppressLogWhenAddValueSameLastOfList = false;
                    }
                    else
                    {
                        _log.IsSuppressLogWhenAddValueSameLastOfList = true;
                    }
                }
                else
                {
                    _log.IsSuppressLogWhenAddValueSameLastOfList = false;
                }

                // tractabilityWithMainWindow
                if (int.TryParse(textBox_tractability, out tmpint))
                {
                    if (tmpint == 0)
                    {
                        _log.TractabilityWithMainWindow = false;
                    }
                    else
                    {
                        _log.TractabilityWithMainWindow = true;
                    }
                }
                else
                {
                    _log.TractabilityWithMainWindow = false;
                }


            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "TestApplySettings");
            }
        }

        public void SaveLog()
        {
            try
            {
                int ret = _log.writeLogAllToFile();
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "setParintForm");
                    return;
                }
                MessageBox.Show("Success");

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "SaveLog");
            }
        }
    }
}
