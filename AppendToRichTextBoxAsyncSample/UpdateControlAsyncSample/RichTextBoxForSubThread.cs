using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateControlAsyncSample
{
    public class RichTextBoxForSubThread
    {
        protected ErrorManager.ErrorManager _err;
        protected RichTextBox _richTextBox;
        public string AppendValue = "";
        public Thread SubThread;
        public Action ActionForSubThread;

        public RichTextBoxForSubThread(ErrorManager.ErrorManager err,RichTextBox richTextBox)
        {
            _err = err;
            _richTextBox = richTextBox;
        }

        public void DisposeSubThread()
        {
            if(SubThread != null)
            {
                SubThread.Abort();
                SubThread.Join();
                SubThread = null;
            }
        }
        public void ExcuteActionFromSubThread()
        {
            try
            {
                _err.AddLog(this, "ExcuteActionFromSubThread");
                DisposeSubThread();
                SubThread = new Thread(new ThreadStart(ActionForSubThread));
                SubThread.Start();
            }
            catch(Exception ex)
            {
                _err.AddException(ex, this, "AppendTextFromSubThread");
            }
        }

        public void AppendText()
        {
            try
            {
                _err.AddLog(this, "AppendText");
                _richTextBox.AppendText(this.AppendValue);
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "AppendText");
            }
        }
    }
}
