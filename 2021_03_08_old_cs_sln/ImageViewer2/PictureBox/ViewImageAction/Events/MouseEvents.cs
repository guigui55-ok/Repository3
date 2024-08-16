using System;
using System.Windows.Forms;
using ErrorLog;

namespace MouseEvents
{
    public class MouseEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Control _control;
        public MouseEvents()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
        }

        public MouseEvents(Control control)
        {
            _control = control;
        }

        public int ClickPointIsRightSideOnControl(MouseEventArgs e)
        {
            return ClickPointIsRightSideOnControl(_control, e);
        }

        public int ClickPointIsRightSideOnControl(Control control, MouseEventArgs e)
        {
            try
            {
                if (e.X > (control.Width / 2))
                {
                    return 1;
                } else
                {
                    return 2;
                }
                // update flag
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "ClickPointIsRightSideOnControl");
                return 0;
            }
        }

        public string[] GetFilesByDragAndDrop(DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // ドラッグ中のファイルやディレクトリの取得
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    return files;
                } else
                {
                    _errorLog.addErrorNotException(this.ToString(), "GetFilesByDragAndDrop GetDataPresent Else");
                    return null;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "ClickPointIsRightSideOnControl");
                return null;
            }
        }
    }
}
