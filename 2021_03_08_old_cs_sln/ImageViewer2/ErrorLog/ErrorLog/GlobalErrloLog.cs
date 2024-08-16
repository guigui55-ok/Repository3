using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLog;

 namespace ErrorLog
{
    // エラーログ実行クラス
    static public class GlobalErrloLog
    {
        static public IErrorLog ErrorLog;

        static public void newErrorLog()
        {
            ErrorLog = new ErrorLog();
        }

        static public int setErrorLog(Object errorLog)
        {
            try
            {
                if (errorLog is IErrorLog)
                {
                    ErrorLog = (IErrorLog)errorLog;
                }
                else
                {
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                ErrorLog.addException(ex, "setErrorLog");
                return 0;
            }
        }
    }
}
