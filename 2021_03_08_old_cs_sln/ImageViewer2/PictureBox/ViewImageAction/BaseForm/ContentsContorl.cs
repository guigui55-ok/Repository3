using System;
using System.Drawing;
using System.Windows.Forms;

namespace ViewImageAction.BaseForm
{
    public class ContentsContorl
    {
        ErrorLog.IErrorLog _errorLog;
        Panel _contentsPanel;
        Form _parentControl;
        //public List<ViewControl.IViewFrameControl> ViewFrameCotntrolList;
        public ContentsContorl(Panel panel) { _contentsPanel = panel; }
        public void setErrorLog(ErrorLog.IErrorLog erorLog) { _errorLog = erorLog; }

        public int setParentControl(Form form)
        {
            try
            {
                _parentControl = form;
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setPrentControl Failed");
                return 0;
            }
        }

        public Size getSize() { return _contentsPanel.Size; }
        public void changeSize(Size size)
        {
            try
            {
                _contentsPanel.Size = size;
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
                _contentsPanel.Location = point;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setPrentControl Failed");
                return;
            }
        }
    }
}
