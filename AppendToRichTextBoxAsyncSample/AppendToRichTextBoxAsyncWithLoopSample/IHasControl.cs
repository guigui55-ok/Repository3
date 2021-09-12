using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppendToRichTextBoxAsyncWithLoopSample
{
    public interface IHasControl
    {
        void AppendText(string value);
    }
}
