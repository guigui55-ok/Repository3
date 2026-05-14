using System;
using AppLoggerModule;

namespace TempFileNameTester
{
    internal class Program
    {
        private static int Main(string[] args)
        {
#if DEBUG
            // デバッグ時に引数を直接指定したい場合
            if (args.Length == 0)
            {
                args = new string[]
                {
                    "--mode", "master",
                    "--process-count", "6",
                    "--thread-count", "4",
                    "--loop-count", "1000",
                    "--wait-ms", "0",
                    "--temp-dir", @"C:\Temp\GetTempFileNameTest",
                    "--delete-created-files", "false",
                    "--log-mode", "simple"
                };

                // 動作確認　実行メモ
                /*
                 * process:2 ,thread:2, loop:1000, wait:0, delete:false
                 * TotalCount=4000, DuplicateCount=0
                 * process:4 ,thread:2, loop:1000, wait:0, delete:false
                 * TotalCount=8000, DuplicateCount=0
                 * process:6 ,thread:4, loop:1000, wait:0, delete:false
                 * TotalCount=24000, DuplicateCount=0
                 */


                // ログの出力先について
                /*
                 * 現在の実装だと、引数に --work-dir を渡したらそちらが優先されます。
                 * つまり、を指定すれば別フォルダにできます。
                    "--work-dir", @"C:\Temp\TestResult",

                ファイルの削除について、
                    * 現在の実装だと、引数に --delete-created-files を渡すと、作成したファイルを削除するようになります。
                    * この場合、削除後に同じ削除済みと同じファイル名が生成され、重複が多くなります。（動作確認済み）
                    "--delete-created-files", "true",
                 *
                 */


            }
#endif

            AppConfig config = CommandLineParser.Parse(args);

            AppLogger logger = LoggerFactory.CreateMasterLogger(config);

            try
            {
                if (config.Mode == "master")
                {
                    MasterRunner runner = new MasterRunner(config, logger);
                    runner.Run();
                }
                else if (config.Mode == "worker")
                {
                    ProcessWorker worker = new ProcessWorker(config, logger);
                    worker.Run();
                }
                else
                {
                    Console.WriteLine("不明な mode です: " + config.Mode);
                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                logger.PrintError("アプリケーション異常終了: " + ex.ToString());
                return 9;
            }
        }
    }
}