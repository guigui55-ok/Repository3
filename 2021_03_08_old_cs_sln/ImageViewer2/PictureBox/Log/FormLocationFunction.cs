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
    public class FormLocationFunction
    {
        ErrorLog.IErrorLog _errorLog;
        public FormLocationFunction(ErrorLog.IErrorLog errorLog)
        {
            _errorLog = errorLog;
        }
        // メインフォームに追従する
        public void FollowParentForm(Form parentForm,Form childForm ,Point AbsoluteLocationFromMainForm)
        {
            try
            {
                if (parentForm == null)
                {
                    // FormLoad_initialize時にエラーになる
                    //_errorLog.addErrorNotException(this.ToString(), "FollowMainForm : ParentForm is null");
                    return;
                }
                // 起点
                int left = parentForm.Location.X + parentForm.Width;
                int top = parentForm.Location.Y;

                // 起点から
                left += AbsoluteLocationFromMainForm.X;
                top += AbsoluteLocationFromMainForm.Y;

                //RightTop 
                childForm.Location = new Point(left, top);

            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "FollowMainForm");
            }
        }
    }
}
