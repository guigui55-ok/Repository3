using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorLog;
using ViewImageAction.BaseForm;

namespace ViewImageAction.Function
{
    public class MainFormFunction
    {
        public CommonFunctions Functions;
        ErrorLog.IErrorLog _errorLog;
        Form _mainForm;
        ContentsContorl _contentsControl;
        public MainFormFunction(Form form)
        {
            _mainForm = form;
        }
        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }
        public void setContentsControl(ContentsContorl contentsControl) { _contentsControl = contentsControl; }

        public void ChangeVisibleMenuWithProcess()
        {
            try
            {
                int beforeWidth = _contentsControl.getSize().Width;
                int beforeHeight = _contentsControl.getSize().Height;

                // MenuVisible
                int afterWidth = _mainForm.Size.Width;
                int afterHeight = _mainForm.Size.Height;

                afterWidth = beforeWidth;
                afterHeight = beforeHeight;

                MenuStrip menu = getMenuStrip();
                if (menu.Visible)
                {
                    afterHeight -= menu.Height;
                }

                // ContentsControl SizeChange
                _contentsControl.changeSize(new Size(afterWidth, afterHeight));

                // 倍率
                //double raito = afterHeight / beforeHeight;

                // FrameControl 縮小 位置変更
                //Functions.ControlFunction.ChangeSizeViewControl(raito);
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "ChangeVisibleMenuWithProcess Failed");
                return;
            }
        }

        public void changeContentSize(Size size)
        {
            try
            {
                _contentsControl.changeSize(size);
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "changeContentSize Failed");
                return;
            }
        }

        public void chcangeVisibleMenuStrip()
        {
            try
            {
                MenuStrip menu = getMenuStrip();
                if (menu.Visible)
                {
                    menu.Visible = false;
                } else { 
                    menu.Visible = true; 
                }
                ChangeVisibleMenuWithProcess();
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "chcangeVisibleMenuStrip Failed");
                return;
            }
        }

        private MenuStrip getMenuStrip()
        {
            try
            {
                foreach(Control item in _mainForm.Controls)
                {
                    if (item is MenuStrip)
                    {
                        return (MenuStrip)item;
                    }
                }
                return null;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "chcangeVisibleMenuStrip Failed");
                return null;
            }
        }
    }
}
