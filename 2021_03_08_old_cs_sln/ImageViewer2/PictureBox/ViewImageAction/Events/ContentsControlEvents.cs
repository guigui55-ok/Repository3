using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewImageAction.Events
{
    public class ContentsControlEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Panel _contentsControl;
        //Control _recieveEventControl;
        //ViewControl.IViewFrameControl _viewFrameControl;
        public ImageViewer2.IViewControlState State;

        public void setErrorLog(ErrorLog.IErrorLog erorLog) { _errorLog = erorLog; }
        public ContentsControlEvents(Control control)
        {
            _contentsControl = (Panel)control;
        }
        public int initialize()
        {
            try
            {
                _contentsControl.LocationChanged += ContentsControl_LocationChanged;
                _contentsControl.SizeChanged += ContentsControl_SizeChanged;

                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize Failed");
                return 0;
            }
        }
        private void ContentsControl_LocationChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("ContentsControl_LocationChanged");
            //_viewFrameControl.saveRatioFromContentscControl();
        }
        private void ContentsControl_SizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("ContentsControl_SizeChanged");
            //_viewFrameControl.saveRatioFromContentscControl();
        }

    }
}
