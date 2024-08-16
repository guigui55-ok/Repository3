using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLog
{
    public interface IErrorLog
    {
        //void ErrorLog();
        void resetError();
        void addException(Exception exception, string FunctionName);
        void addException(Exception exception, string ClassName,string FunctionName);
        void ShowErrorMessage(Exception exception);
        //void ShowErrorMessage();
        bool ShowErrorMessage();
        void ShowErrorMessageAndClearError();
        void addErrorNotException(string FunctionName);
        void addErrorNotException(string ClassName, string FunctionName);
        bool haveError();
    }
}
