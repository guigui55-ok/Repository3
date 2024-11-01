using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLoggerModule
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            // Loggerの初期化
            AppLogger logger = new AppLogger();

            //// ファイルパスを設定
            //logger.SetFilePath("C:\\logs\\test_log.log");

            // ファイルパスをCurrentDirectoryに設定
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory; // または Environment.CurrentDirectory も使用可能
            string logFilePath = System.IO.Path.Combine(currentDirectory, "__test_log.log");
            logger.LogFileTimeFormat = "";
            logger.SetFilePath(logFilePath);
            Console.WriteLine(string.Format("logFilePath = {0}", logFilePath));


            // ログレベルをINFOに設定
            logger.LoggerLogLevel = LogLevel.INFO;

            // ログをコンソールとファイルに出力するように設定
            logger.LogOutPutMode = OutputMode.CONSOLE | OutputMode.FILE;

            // ログメッセージのテスト
            logger.PrintCritical("これはクリティカルログです。");
            logger.PrintError("これはエラーログです。");
            logger.PrintWarn("これは警告ログです。");
            logger.PrintInfo("これは情報ログです。");
            logger.PrintDebug("これはデバッグログです。");
            logger.PrintTrace("これはトレースログです。");

            Console.WriteLine("ログテストが完了しました。");
            Console.ReadKey();
        }
    }

}
