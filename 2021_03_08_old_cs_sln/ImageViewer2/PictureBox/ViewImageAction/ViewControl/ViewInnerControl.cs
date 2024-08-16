using ImageViewer2;
using System;
using System.Drawing;
using System.Windows.Forms;
using ErrorLog;
using System.Diagnostics;

namespace ViewImageAction
{
    public class ViewInnerControl : IViewInnerControl
    {
        //private PictureBox _pictureBox;
        private Panel _parentControl;
        private Panel _innerControl;
        ErrorLog.IErrorLog _errorLog;
        private IViewControlState _viewControlState;
        private IViewImageSettings _viewImageSettings;
        private IViewImageControl _viewImageControl;

        private Point bufPoint;
        private PointF bufPointF;
        public IViewImageControl ViewImageControl { get => _viewImageControl; set => _viewImageControl = value; }
        public IViewImageSettings Settings { get => _viewImageSettings; set => _viewImageSettings=value; }
        public IViewControlState State { get => _viewControlState; set => _viewControlState=value; }
        

        public ViewInnerControl()
        {
            
        }
        public void setErrorLog(IErrorLog errorLog) { _errorLog = errorLog; }

        public int setErrorLog(object errorLog)
        {
            //if (object is ErrorLog.IErrorLog) { } // error
            try
            {
                _errorLog = (IErrorLog)errorLog;
                return 1;
            } catch (Exception ex) { _errorLog.addException(ex, this.ToString(), "setErrorLog"); return 0; }
        }
        public int setControl(Control ViewControl)
        {
            try
            {
                if (ViewControl is Panel)
                {
                    _innerControl = (Panel)ViewControl;
                    return 1;
                } else
                {
                    _errorLog.addErrorNotException(this.ToString(), "setControl is not Panel");
                    return -1;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setControl");
                return 0;
            }
        }
        public int setParentControl(Control ParentControl)
        {
            try
            {
                if (ParentControl is Panel)
                {
                    _parentControl = (Panel)ParentControl;
                    return 1;
                }
                else
                {
                    _errorLog.addErrorNotException(this.ToString(), "setParentControl");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setParentControl");
                return 0;
            }
        }

        public int setViewImageControl(IViewImageControl viewImageControl)
        {
            try
            {
                if (!(viewImageControl is null))
                {
                    _viewImageControl = viewImageControl;
                    _viewControlState = _viewImageControl.State;
                    _viewImageSettings = _viewImageControl.Settings;
                    return 1;
                }
                else
                {
                    _errorLog.addErrorNotException(this.ToString(), "setViewImageControl");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setParentControl");
                return 0;
            }
        }


        public void changeLocation(Point point)
        {
            try
            {
                _innerControl.Location = point; 
            } catch (Exception ex)
            { _errorLog.addException(ex, this.ToString(), "changeLocation"); }
        }

        public void changeSize(Size size)
        {
            try 
            {
                _innerControl.Size = size;
            }
            catch (Exception ex)
            { _errorLog.addException(ex, this.ToString(), "changeSize"); }
        }

        public Point getLocation()
        {
            try
            {
                if (_innerControl == null) { 
                    _errorLog.addErrorNotException(this.ToString(), "setControl is not Panel"); return new Point(); }
                return _innerControl.Location;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getLocation");
                return new Point();
            }
        }

        public Control getParentControl()
        {
            try
            {
                if (_parentControl == null)
                {
                    _errorLog.addErrorNotException(this.ToString(), "getParentControl is null"); return null;
                }
                return _parentControl;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getParentControl");
                return null;
            }
        }

        public Size getSize()
        {
            try
            {
                if (_innerControl == null)
                {
                    _errorLog.addErrorNotException(this.ToString(), "getSize innerControl is null"); return new Size();
                }
                return _innerControl.Size;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getSize");
                return new Size();
            }
        
        }

        public int Initialize()
        {
            try {
                return 1;
            } catch (Exception ex) {
                _errorLog.addException(ex, this.ToString(), "Initialize");
                return 0;
            }
        }


        public int setControlState(IViewControlState viewControlState)
        {
            _viewControlState = viewControlState;
            return 1;
        }


        public void setImageNotDispose(Image image)
        {
            _viewImageControl.setImageNotDispose(image);
        }

        public void setImageWithDispose(Image image)
        {
            _viewImageControl.setImageWithDispose(image);
        }


        public void setVisible(bool flag)
        {
            _viewImageControl.setVisible(flag);
        }

        public void RefreshPaint()
        {
            _viewImageControl.RefreshPaint();
        }

        public void PausePaint(bool flag)
        {
            if (flag)
            {
                _innerControl.SuspendLayout();
            } else
            {
                _innerControl.ResumeLayout();
            }
            _viewImageControl.PausePaint(flag);
        }

        // 画像表示時、マウスドラッグでコントロール移動時、拡大縮小時に記録する
        public void saveDifferenceSizeAndPositionFromFramecControl()
        {
            try
            {
                if (State.IsFrameSizeChanging) { return; }
                Size frameSize = _parentControl.Size;
                // Size Ratio
                bufPointF.X = (float)_innerControl.Size.Width / (float)_parentControl.Size.Width;
                bufPointF.Y = (float)_innerControl.Size.Height / (float)_parentControl.Size.Height;
                State.RatioSizeInnerFromFrame = bufPointF;
                // Location Raito
                State.RatioLocationInnerFromFrameX = (double)_innerControl.Location.X / (double)_parentControl.Size.Width;
                State.RatioLocationInnerFromFrameY = (double)_innerControl.Location.Y / (double)_parentControl.Size.Height;

                //Debug.WriteLine("save size ratio = " + State.RatioSizeInnerFromFrame.X  + " , " + State.RatioSizeInnerFromFrame.Y);
                Debug.WriteLine("save pos  ratio = " + State.RatioLocationInnerFromFrameX + " , " + State.RatioLocationInnerFromFrameY);

                // Size Difference
                bufPoint.X = frameSize.Width - _innerControl.Width;
                bufPoint.Y = frameSize.Height - _innerControl.Height;
                State.DifferenceSizeFromContents = bufPoint;

                // Position
                State.DifferencePositionFromContents = _innerControl.Location;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "saveDifferenceSizeAndPositionFromFramecControl Failed");
                return;
            }
        }
    }
}
