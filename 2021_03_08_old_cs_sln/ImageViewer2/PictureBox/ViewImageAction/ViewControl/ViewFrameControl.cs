using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLog;
using System.Windows.Forms;
using System.Drawing;
using ImageViewer2;

namespace ViewImageAction.ViewControl
{
    public class ViewFrameControl : IViewFrameControl
    {
        ErrorLog.IErrorLog _errorLog;
        Panel _framePanel;
        Panel _parentControl;
        PointF bufPointF = new PointF();
        IViewControlState _state;
        private IViewInnerControl _viewInnerControl;

        public IViewInnerControl ViewInnerControl { get => _viewInnerControl; set => _viewInnerControl = value; }
        public IViewControlState State { get => _state; set => _state = value; }
        public ViewFrameControl(Panel panel) { _framePanel = panel; }
        public void setErrorLog(ErrorLog.IErrorLog erorLog) { _errorLog = erorLog; }

        public Control getControl() { return _framePanel; }
        public int setParentControl(Control control)
        {
            try
            {
                if (control is Panel) { 
                    _parentControl = (Panel)control;
                    return 1;
                } else 
                { _errorLog.addErrorNotException(this.ToString(), "setPrentControl Failed"); return -1; }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setPrentControl Failed");
                return 0;
            }
        }

        private int initialize()
        {
            try
            {
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize Failed");
                return 0;
            }
        }

        public Size getSize() { return _framePanel.Size; }
        public void changeSize(Size size)
        {
            try
            {
                _framePanel.Size = size;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setPrentControl Failed");
                return;
            }
        }

        public void changeLocation(Point point)
        {
            try
            {
                _framePanel.Location = point;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setPrentControl Failed");
                return;
            }
        }
        public void saveRatioFromContentscControl()
        {
            saveRatioFromContentscControl(_parentControl.Size);
        }
        public void saveRatioFromContentscControl(Size contents)
        {
            try
            {
                // Size Ratio
                bufPointF.X = contents.Width -_framePanel.Width;
                bufPointF.Y = contents.Height - _framePanel.Height;
                State.DifferenceSizeFromContents = bufPointF;

                // Position Ratio
                bufPointF.X = _framePanel.Location.X;
                bufPointF.Y =  _framePanel.Location.Y;
                State.DifferencePositionFromContents = bufPointF;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "saveRatioFromContentscControl Failed");
                return;
            }
        }



        //public int getHeightMenuBar()
        //{
        //    try
        //    {
        //        if (control is Form)
        //        {
        //            _parentControl = (Form)control;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _errorLog.addException(ex, this.ToString(), "setPrentControl Failed");
        //        return;
        //    }
        //}
    }
}
