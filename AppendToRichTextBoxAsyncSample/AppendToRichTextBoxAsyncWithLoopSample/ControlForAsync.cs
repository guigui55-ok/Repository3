using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppendToRichTextBoxAsyncWithLoopSample
{
    public class ControlForAsync
    {
        ErrorManager.ErrorManager _err;
        IHasControl _hasControl;
        Form _form;
        public string AppendValue = "";
        public ControlForAsync(ErrorManager.ErrorManager err,Form form, IHasControl hasControl)
        {
            _err = err;
            _form = form;
            _hasControl = hasControl;
        }

        // 非同期でコントロールにアクセスするための delegate を実装する
        public delegate void DelegateAppendTextToRichTextBoxBySubThread();

        // 非同期でコントロールにアクセスするための Method を実装する
        private void AppendTextToRichTextBoxBySubThread()
        {
            try
            {
                if (_form.InvokeRequired)
                {
                    _err.AddLog("InvokeRequired=true");
                    _form.Invoke(new DelegateAppendTextToRichTextBoxBySubThread(this.AppendTextToRichTextBoxBySubThread));
                    return;
                }
                AppendText(AppendValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void AppendTextToControlBySubThread(string value)
        {
            this.AppendValue = value;
            AppendTextToRichTextBoxBySubThread();
        }

        private void AppendText(string value)
        {
            try
            {
                this._hasControl.AppendText(value);
               
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "AppendText");
            }
        }
    }
}
