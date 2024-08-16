using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewImageAction
{
    public class ViewImageObjects
    {
        ErrorLog.IErrorLog _errorLog;
        public FileList.FileListForUse FileList;
        public FileList.FileList FileRegister;
        //public List<ViewControl.IViewFrameControl> ViewFrameControlList;
        public ViewControl.IViewFrameControl ViewFrameControl;
        public ImageViewer2.IViewImage ViewImage;

        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }
        public ViewImageObjects()
        {
            ViewImage = new ImageViewer2.ViewImage();
        }

    }
}
