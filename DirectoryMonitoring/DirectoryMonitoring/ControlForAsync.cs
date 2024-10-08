﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderMonitoring
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

        // ③ 非同期でコントロールにアクセスするための delegate を実装する
        public delegate void DelegateAppendTextToControlBoxBySubThread();

        // ④ 非同期でコントロールにアクセスするための Method を実装する
        private void AppendTextToControlBySubThread()
        {
            try
            {
                if (_form.InvokeRequired)
                {
                    _err.AddLog("InvokeRequired=true");
                    _form.Invoke(new DelegateAppendTextToControlBoxBySubThread(this.AppendTextToControlBySubThread));
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
            AppendTextToControlBySubThread();
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
