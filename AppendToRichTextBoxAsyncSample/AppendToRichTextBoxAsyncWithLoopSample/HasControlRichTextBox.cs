using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppendToRichTextBoxAsyncWithLoopSample
{
    public class HasControlRichTextBox: IHasControl
    {
        ErrorManager.ErrorManager _err;
        RichTextBox _richTextBox;
        public HasControlRichTextBox(ErrorManager.ErrorManager err,RichTextBox richTextBox)
        {
            _err = err;
            _richTextBox = richTextBox;
        }

        public void AppendText(string value)
        {
            try
            {
                _richTextBox.AppendText(value);
                _richTextBox.ScrollToCaret();
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "AppendText");
            }
        }
    }
}
