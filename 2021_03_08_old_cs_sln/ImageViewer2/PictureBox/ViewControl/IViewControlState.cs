using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer2
{
    interface IViewControlState
    {
        bool NowSizeUpdate { get; set; }
    }
}
