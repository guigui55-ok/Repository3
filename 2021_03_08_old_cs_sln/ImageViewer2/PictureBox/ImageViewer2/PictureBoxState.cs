using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer2
{
    class PictureBoxState : IViewControlState
    {
        bool NowSizeUpdate;
        bool IViewControlState.NowSizeUpdate { get => NowSizeUpdate; set => NowSizeUpdate = value; }
    }
}
