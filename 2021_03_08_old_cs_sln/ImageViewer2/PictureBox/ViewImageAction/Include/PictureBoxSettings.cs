using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer2
{
    public class PictureBoxSettings : IViewImageSettings
    {
        public bool _isViewAlwaysCenter = true;

        bool IViewImageSettings.IsViewAlwaysCenter { get => _isViewAlwaysCenter; set => _isViewAlwaysCenter = value;}
    }
}
