using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using ViewImageAction.Function;

namespace ViewImageAction.Events
{
    public class FrameControlEvents
    {
        ErrorLog.IErrorLog _errorLog;
        readonly Control _frameControl;
        Control _recieveEventControl;
        ViewControl.IViewFrameControl _viewFrameControl;
        public ImageViewer2.IViewControlState State;

        //public ViewInnerControl ViewInnerControl;
        public ViewImageObjects ViewImageObjects;
        public CommonFunctions Functions;

        public void SetErrorLog(ErrorLog.IErrorLog erorLog) { _errorLog = erorLog; }
        public FrameControlEvents(Control control)
        {
            _frameControl = control;
        }

        //public void setParentControl(Control control)
        //{
        //    _parentControl = control;
        //}

        public void SetViewFrameControl(ViewControl.IViewFrameControl viewFrameControl)
        {
            try { _viewFrameControl = viewFrameControl; }
            catch(Exception ex)
            { _errorLog.addException(ex, this.ToString(), "setViewFrameControl Failed"); }
        }
        public int SetRecieveEventControl(Control control)
        {
            try
            {
                _recieveEventControl = control;
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setRecieveEventControl Failed");
                return 0;
            }
        }

        public int Initialize()
        {
            try
            {
                _frameControl.MouseHover += FrameControl_MouseHover;
                _recieveEventControl.MouseHover += FrameControl_MouseHover;

                _frameControl.MouseLeave += FrameControl_MouseLeave;
                _recieveEventControl.MouseLeave += FrameControl_MouseLeave;

                //_frameControl.ClientSizeChanged
                _frameControl.SizeChanged += FrameControl_SizeChanged;
                _frameControl.LocationChanged += FrameControl_LocationChanged;

                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize Failed");
                return 0;
            }
        }

        private void FrameControl_LocationChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("FrameControl_LocationChanged");
            _viewFrameControl.saveRatioFromContentscControl();

        }
        private void FrameControl_SizeChanged(object sender, EventArgs e)
        {
            _viewFrameControl.State.IsFrameSizeChanging = true;
            Debug.WriteLine("FrameControl_SizeChanged");
            _viewFrameControl.saveRatioFromContentscControl();
            // Inner のサイズと位置を外側の拡大縮小の際に、以前のものと相対的に同じにする
            Functions.ControlFunction.MaintainSizeAndPositionRatioFrameAndInner(sender, (EventArgs)e);
            //Functions.ControlFunction.ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();

            //Debug.WriteLine("FrameSize = " + _frameControl.Size.Width + " , " + _frameControl.Size.Height);
            //Debug.WriteLine("FramePos  = " + _frameControl.Location.X + " , " + _frameControl.Location.Y);
            // PresentationFramework.dll、PresentationCore.dll、WindowsBase.dll
            //RoutedEventArgs er = (RoutedEventArgs)e;
            _viewFrameControl.State.IsFrameSizeChanging = false;
        }

        public void FrameControl_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Debug.WriteLine("FrameControl_MouseHover");
                State.IsMouseHover = true;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "FrameControl_MouseHover Failed");
                return;
            }
        }
        public void FrameControl_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                //Debug.WriteLine("FrameControl_MouseLeave");
                State.IsMouseHover = false;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "FrameControl_MouseLeave Failed");
                return;
            }
        }
    }
}
