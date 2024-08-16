using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewImageAction
{
    public class ViewImageManager
    {
        ErrorLog.IErrorLog _errorLog;
        public List<ViewImageObjects> ViewImageObjectList;
        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }

        public ViewImageManager()
        {
            ViewImageObjectList = new List<ViewImageObjects>();
        }
        public List<ViewImageObjects> getActiveControl()
        {
            try
            {
                List<ViewImageObjects> retlist = new List<ViewImageObjects>();
                if (ViewImageObjectList is null)
                { _errorLog.addErrorNotException(this.ToString(), "getActiveControl list is null"); return null; }
                if (ViewImageObjectList.Count < 1)
                { _errorLog.addErrorNotException(this.ToString(), "getActiveControl list count 0"); return null; }
                foreach (ViewImageObjects value in ViewImageObjectList)
                {
                    if (value.ViewFrameControl.State.IsActiveControl)
                    {
                        retlist.Add(value);
                    }
                }
                return retlist;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewImage");
                return null;
            }
        }
        public void doFunction(Func<ViewImageObjects,int> func)
        {
            try
            {
                List<ViewImageObjects> list = this.getActiveControl();
                if (!((list is null) | (list.Count < 1)))
                {
                    foreach (ViewImageObjects value in list)
                    {
                        func(value);
                    }
                }
                else
                {
                    _errorLog.addErrorNotException(this.ToString() + " doFunction");
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " doFunction");
                return;
            }
        }

    }
}
