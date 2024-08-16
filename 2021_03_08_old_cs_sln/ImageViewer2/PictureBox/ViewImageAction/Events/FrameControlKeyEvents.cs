using System;
using System.Diagnostics;
using System.Windows.Forms;
using ErrorLog;

namespace ViewImageAction.ViewControl
{
    public class FrameControlKeyEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Control _frameControl;
        Control _recieveEventControl;
        public Function.CommonFunctions Functions;
        public Settings.ImageViewerSettings Settings;

        public void setErrorLog(ErrorLog.IErrorLog erorLog) { _errorLog = erorLog; }
        public FrameControlKeyEvents(Control panel) {             
            _frameControl = panel;         
        }

        public int setRecieveEventControl(Control control)
        {
            try
            {
                if (control is null)
                { _errorLog.addErrorNotException(this.ToString(), "initialize control is null"); return -1; }
                _recieveEventControl = control;
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize Failed");
                return 0;
            }
        }

        public int initialize()
        {
            try
            {
                if (_frameControl is null)
                { _errorLog.addErrorNotException(this.ToString(), "initialize control is null"); return -1; }

                _frameControl.KeyDown += FrameControl_KeyDown;
                _recieveEventControl.KeyDown += FrameControl_KeyDown;

                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize Failed");
                return 0;
            }
        }

        private void FrameControl_KeyDown(object sender,KeyEventArgs e)
        {
            try
            {
                //Debug.WriteLine("FrameControl_KeyDown");


                if (e.Alt)
                {
                    if (!(Settings.IsMenuBarVisibleAlways))
                    {
                        Functions.MainFormFunction.chcangeVisibleMenuStrip();

                    }
                }


            } catch ( Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "FrameControl_KeyDown Failed");
                return;
            }
        }

    }
}
