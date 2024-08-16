﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorLog;

namespace ImageViewer2
{
    interface IViewImageControl
    {
        int setControl(Control ViewControl);
        int setParentControl(Control ParentControl);
        int setErrorLog(Object errorLog);
        int setControlState(IViewControlState viewControlState);
        int Initialize();
        void setImageWithDispose(Image image);
        void setImageNotDispose(Image image);
        Size getSize();
        Point getLocation();
        void changeSize(Size size);
        void setVisible(bool flag);
        void changeLocation(Point point);
    }
}
