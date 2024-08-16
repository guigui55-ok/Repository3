using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewImageAction.Events
{
    public class BaseFormEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Form _baseForm;
        //Control _recieveEventControl;
        public ImageViewer2.IViewControlState State;
        public Function.CommonFunctions Functions;

        public void setErrorLog(ErrorLog.IErrorLog erorLog) { _errorLog = erorLog; }
        public BaseFormEvents(Form form)
        {
            _baseForm = form;
        }

        public int initialize()
        {
            try
            {
                _baseForm.MouseHover += BaseForm_MouseHover;
                _baseForm.SizeChanged += BaseForm_SizeChanged;
                _baseForm.ClientSizeChanged += BaseForm_ClientSizeChanged;
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize Failed");
                return 0;
            }
        }
        private void BaseForm_ClientSizeChanged(object sender, EventArgs e)
        {
            //_viewFrameControl.saveRatioFromContentscControl();
            Functions.MainFormFunction.changeContentSize(_baseForm.ClientSize);
                }
        private void BaseForm_LocationChanged(object sender, EventArgs e)
        {
            //_viewFrameControl.saveRatioFromContentscControl();
        }
        private void BaseForm_SizeChanged(object sender, EventArgs e)
        {
            //_viewFrameControl.saveRatioFromContentscControl();
        }
        private void BaseForm_MouseHover(object sender,EventArgs e)
        {
            try
            {

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "BaseForm_OnMouse Failed");
                return;
            }
        }
    }
}
