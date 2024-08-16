using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewImageAction.Function;

namespace ViewImageAction.Events
{
    public class InnerControlSizeLocationEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Control _recieveControl;
        public CommonFunctions Functions;
        //protected bool IsDown = false;
        //protected bool IsMouseMove = false;
        public void SetErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }
        public void SetRecieveControl(Control control) { _recieveControl = control; }
        public InnerControlSizeLocationEvents(Control control)
        {
            _recieveControl = control;
            Debug.WriteLine(control.Name);
        }
        public void Initialize()
        {
            try
            {
                _recieveControl.SizeChanged += Control_SizeChanged;
                _recieveControl.LocationChanged += Control_LocationChanged;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize");
            }
        }

        private void Control_SizeChanged(object sender,EventArgs e)
        {
            Functions.ControlFunction.ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();
        }
        private void Control_LocationChanged(object sender, EventArgs e)
        {
            Functions.ControlFunction.ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();
        }
    }
}
