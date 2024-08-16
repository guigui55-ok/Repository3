using System;
using ImageViewer2;
using System.Windows.Forms;
using System.Drawing;
using ErrorLog;

namespace ViewImageAction
{
    public interface IViewInnerControl 
    {
        IViewImageControl ViewImageControl { get; set; }
        IViewImageSettings Settings { get; set; }

        IViewControlState State { get; set; }

        void setErrorLog(IErrorLog errorLog);
        int setControl(Control ViewControl);
        int setParentControl(Control ParentControl);
        int setErrorLog(Object errorLog);
        int setControlState(IViewControlState viewControlState);

        int setViewImageControl(IViewImageControl viewImageControl);
        int Initialize();
        void setImageWithDispose(Image image);
        void setImageNotDispose(Image image);
        Size getSize();
        Point getLocation();
        void changeSize(Size size);
        void setVisible(bool flag);
        void changeLocation(Point point);

        void RefreshPaint();
        Control getParentControl();

        void saveDifferenceSizeAndPositionFromFramecControl();

        //void MaintainSizeAndPositionRatioFrameAndInner();
    }
}
