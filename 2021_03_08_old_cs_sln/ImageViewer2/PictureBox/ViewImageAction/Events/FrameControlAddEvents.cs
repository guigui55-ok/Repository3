using System;
using System.Windows.Forms;
using ViewImageAction.Function;

namespace ViewImageAction.Events
{
    //  Pos Size 記録用
    public class FrameControlAddEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Control _recieveControl;
        public CommonFunctions Functions;
        private bool IsDown = false;
        private bool IsMouseMove = false;

        public FrameControlAddEvents()
        {
        }

        public FrameControlAddEvents(Control control)
        {
            _recieveControl = control;
        }
        public void SetErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }
        public void SetRecieveControl(Control control) { _recieveControl = control; }

        public void Initialize()
        {
            try {
                // CS1656 メソッドグループであるため、これに割り当てることはできません
                //MoveControlEvents._mouseEventHandler.MouseMove += InnerControlAdd_MouseMove;
                //Events.ViewImageMouseEventHandler _mouseEventHandler = MoveControlEvents._mouseEventHandler;
                // CS1656 メソッドグループであるため、これに割り当てることはできません
                //_mouseEventHandler.MouseMove += InnerControlAdd_MouseMove;

                _recieveControl.MouseMove += InnerControlAdd_MouseMove;
                _recieveControl.MouseDown += InnerControlAdd_MouseDown;
                _recieveControl.MouseUp += InnerControlAdd_MouseUp;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize");
            }
        }

        private void InnerControlAdd_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                IsMouseMove = true;
                if (IsDown)
                {
                    // Inner 
                    Functions.ControlFunction.SaveDifferenceSizeAndPositionInnerControlFromFramecControl();
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "InnerControlAdd_MouseMove");
            }
            finally
            {
                IsMouseMove = false;
            }
            
        }
        private void InnerControlAdd_MouseDown(object sender, MouseEventArgs e)
        {
            IsDown = true;
        }
        private void InnerControlAdd_MouseUp(object sender, MouseEventArgs e)
        {
            IsDown = false;
        }
    }
}
