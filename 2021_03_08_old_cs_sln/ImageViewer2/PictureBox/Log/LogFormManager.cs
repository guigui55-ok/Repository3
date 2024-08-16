using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ErrorLog;

namespace Log
{
    public class LogFormManager
    {
        private ErrorLog.IErrorLog _errorLog;
        public Point AbsoluteLocationFromMainForm;
        public Size WindowSize;
        public Form2 LogForm;
        private RichTextBox _richtextbox;
        private Form _parentForm;
        // tractability 追従
        // メインウィンドウに追従する
        public bool tractabilityWithMainWindow = true;

        public LogFormManager()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
            AbsoluteLocationFromMainForm = new Point(10,0);
            WindowSize = new Size(620,720);
            LogForm = new Form2();
            _richtextbox = getRichTextBox();
        }
        public int show()
        {
            try
            {
                LogForm.Show();
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex,this.ToString(), "show");
                return 0;
            }
        }

        public int setParentForm(Object form)
        {
            try
            {
                if (form is Form)
                {
                    _parentForm = (Form)form;
                }
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setParentForm");
                return 0;
            }
        }

        public void updateWindow(string value)
        {
            _richtextbox.AppendText(value);
            _richtextbox.ScrollToCaret();
        }

        public RichTextBox getRichTextBox()
        {
            try
            {
                foreach (Control ctrl in LogForm.Controls){
                    if (ctrl is RichTextBox)
                    {
                        return (RichTextBox)ctrl;
                    }
                }
                return null;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getRichTextBox");
                return null;
            }
        }

        // メインフォームに追従する
        public int FollowParentForm()
        {
            try
            {
                if (_parentForm == null)
                {
                    // FormLoad_initialize時にエラーになる
                    //_errorLog.addErrorNotException(this.ToString(), "FollowMainForm : ParentForm is null");
                    return -1; 
                }
                // フラグOFF時は実行しない
                if (!this.tractabilityWithMainWindow) { return 2; }

                // 起点
                int left = _parentForm.Location.X + _parentForm.Width;
                int top = _parentForm.Location.Y;

                // 起点から
                left += AbsoluteLocationFromMainForm.X;
                top += AbsoluteLocationFromMainForm.Y;

                //RightTop 
                LogForm.Location = new Point(left, top);
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "FollowMainForm");
                return 0;
            }
        }
    }
}
