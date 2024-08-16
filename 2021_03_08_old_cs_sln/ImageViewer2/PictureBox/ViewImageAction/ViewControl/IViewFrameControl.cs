using ImageViewer2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewImageAction.ViewControl
{
    public interface IViewFrameControl
    {
        IViewInnerControl ViewInnerControl { get; set; }
        IViewControlState State { get; set; }
        void setErrorLog(ErrorLog.IErrorLog errorLog);

        Control getControl();
        int setParentControl(Control ParentControl);
        void changeSize(Size size);
        void changeLocation(Point point);

        void saveRatioFromContentscControl();

        Size getSize();
    }
}
