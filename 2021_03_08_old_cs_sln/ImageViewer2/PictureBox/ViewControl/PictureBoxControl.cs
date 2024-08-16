using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorLog;
using System.Drawing;

namespace ImageViewer2
{
    class PictureBoxControl : IViewImageControl
    {
        private PictureBox _pictureBox;
        private Form _parentControl;
        ErrorLog.IErrorLog _errorLog;
        private IViewControlState _viewControlState;

        public PictureBoxControl()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
        }
        public int setControl(Control pictureBox)
        {
            try
            {
                if (Object.ReferenceEquals(pictureBox.GetType(), new PictureBox().GetType()))
                {
                    _pictureBox = (PictureBox)pictureBox;
                } else
                {
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "setControl");
                return 0;
            }
        }
        public int setParentControl(Control form)
        {
            try {
                //if (Object.ReferenceEquals(form.GetType(), new Form().GetType()))
                if (!( form is Form))
                {
                    return -1;
                }
                else
                {
                    _parentControl = (Form)form;
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "setParentControl");
                return 0;
            }
        }
        public int setErrorLog(Object errorLog)
        {
            try
            {
                if (Object.ReferenceEquals(errorLog.GetType(), new ErrorLog.ErrorLog().GetType()))
                {
                    _errorLog = (IErrorLog)errorLog;
                }
                else
                {
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "setErrorLog");
                return 0;
            }
        }

        public int setControlState(IViewControlState viewControlState)
        {
            try
            {
                _viewControlState = viewControlState;
                return 1;
            }
            catch (Exception ex) { _errorLog.addException(ex, "setControlState"); return 0; }
        }
        public int Initialize()
        {
            try
            {

                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "Initialize");
                return 0;
            }
        }

        public bool ContorlIsNull()
        {
            try { 
                if (_pictureBox == null)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "ContorlIsNull");
                return false;
            }
        }

        public void setImageWithDispose(Image image)
        {
            try
            {
                if (_pictureBox == null)
                {
                    _pictureBox.Image.Dispose();
                }
                _pictureBox.Image = image;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "setImageWithDispose");
            }
        }
        
        public void setImageNotDispose(Image image)
        {
            try
            {
                _pictureBox.Image = image;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " setImageNotDispose");
            }
        }

        public Image getImage()
        {
            try
            {
                if (_pictureBox != null)
                {
                    return _pictureBox.Image;
                }
                else
                {
                    _errorLog.addErrorNotException(this.ToString() + "setImageNotDispose");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " getImage");
                return null;
            }
        }

        public void setBitmapWithDispose(Bitmap bitmap)
        {
            try
            {
                if (_pictureBox != null)
                {
                    _pictureBox.Image.Dispose();
                    _pictureBox.Image = bitmap;
                }
                else
                {
                    _pictureBox.Image = bitmap;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " setBitmapWithDispose");
            }
        }

        public void setBitmapNotDispose(Bitmap bitmap)
        {
            if (_pictureBox != null)
            {
                _pictureBox.Image = bitmap;
            }
            else
            {
                _errorLog.addErrorNotException("setBitmapNotDispose");
            }
        }

        public Size getSize()
        {
            try
            {
                if (_pictureBox != null)
                {
                    return _pictureBox.Size;
                }
                else
                {
                    return new Size(0, 0);
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "getSize");
                return new Size(0, 0);
            }
        }

        public Point getLocation()
        {
            if (ContorlIsNull())
            {
                _errorLog.addErrorNotException("getLocation");
                return new Point(0, 0);
            }
            else
            {
                return _pictureBox.Location;
            }
        }
        public void changeSize(Size size)
        {
            try
            {
                if (ContorlIsNull()) { _errorLog.addErrorNotException("changeSize");  }
                if (_pictureBox.Size.Equals(new Size(0,0))) {
                    _errorLog.addErrorNotException("changeSize:Size is zero.");
                }
                _viewControlState.NowSizeUpdate = true;
                _pictureBox.Size = size;
                _viewControlState.NowSizeUpdate = false;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex,"changeSize");
            }
        }
        public void setVisible(bool flag)
        {
            if (ContorlIsNull()) { _errorLog.addErrorNotException("setVisible"); }
            _pictureBox.Visible = flag;
        }
        public void changeLocation(Point point)
        {
            try
            {
                if (ContorlIsNull()) { _errorLog.addErrorNotException("changeLocation"); }
                _pictureBox.Location = point;

            }
            catch (Exception ex) { _errorLog.addException(ex,"changeLocation"); }

        }
        public void setImageLocation(String path)
        {
            try
            {
                _pictureBox.ImageLocation = path;
            }
            catch (Exception ex) { _errorLog.addException(ex, "setImageLocation"); }
        }
        public PictureBox getPictureBox()
        {
            return _pictureBox;
        }
    }
}
