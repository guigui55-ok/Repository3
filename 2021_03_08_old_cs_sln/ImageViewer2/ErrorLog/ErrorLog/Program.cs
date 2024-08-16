using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ファイルに出力
 * その設定
 * メッセージボックスに出力設定？
 */


namespace ErrorLog
{
    class Program
    {
        static IErrorLog _errorLog;
        static void Main(string[] args)
        {
            _errorLog = new ErrorLog();
            try
            {
                if (TestErrorLog() < 1)
                {
                    _errorLog.ShowErrorMessage();
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        static int TestErrorLog()
        {
            _errorLog = new ErrorLog();
            try
            {
                //throw new NullReferenceException();
                throw new Exception();
                //return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _errorLog.addException(ex, "TestErrorLog");
                return 0;
            }
            finally
            {
            }
        }
    }
}
