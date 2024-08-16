using System;
using System.Drawing;
using ErrorLog;

namespace ImageViewer2
{
    class ViewImage : IViewImage
    {
        private Image _image;
        String path;
        private ErrorLog.IErrorLog _errorLog;

        public ViewImage()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
        }
        public int setPath(String path)
        {
            try
            {
                this.path = path;
                if (System.IO.File.Exists(path))
                {
                    _image = new Bitmap(path, true);
                }
                else
                {
                    _errorLog.addErrorNotException(this.ToString() + "FileNotExists:" + path);
                    return -1;
                }
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " setPath");
                return 0;
            }
        }

        public bool isExistsImage()
        {
            try
            {
                if (this._image == null)
                {
                    return false;
                } else {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " isExistsImage");
                return false;
            }
        }

        public Image getImage()
        {
            try
            {
                if (!isExistsImage()) { _errorLog.addErrorNotException(this.ToString() + " getImage : isExistsImage : false"); }
                return _image;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " getImage");
                return null;
            }
        }

        public Size getSize()
        {
            try
            {
                if (!isExistsImage()) { _errorLog.addErrorNotException(this.ToString() + " getSize : isExistsImage : false"); }
                return _image.Size;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " isExistsImage");
                return new Size(0,0);
            }
        }
    }
}
