using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ErrorLog;

namespace ImageViewer2
{
    public interface IViewImage
    {
        int setPath(String path);
        Image getImage();

        string getPath();
    }
}
