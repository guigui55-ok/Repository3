using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowMessageToStatusBar
{
    public class ShowMessageStatusBar : IShowErrorMessage
    {
        protected ErrorManager.ErrorManager _error;
        StatusStrip _satatusStrip;
        Form _parentForm;

        public ShowMessageStatusBar(ErrorManager.ErrorManager error,Form parentForm,StatusStrip statusStrip)
        {
            _error = error;
            _satatusStrip = statusStrip;
            _parentForm = parentForm;
        }

        public void Initialize()
        {
            try
            {

            } catch (Exception ex)
            {
                _error.AddException(ex,this.ToString()+ ".Initialize");
            }
        }
    }
}
