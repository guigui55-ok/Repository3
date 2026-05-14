using System.Diagnostics;
using System.IO;
using AppLoggerModule;

namespace TempFileNameTester
{
    public static class LoggerFactory
    {
        public static AppLogger CreateMasterLogger(AppConfig config)
        {
            AppLogger logger = new AppLogger();

            logger.LoggerLogLevel = LogLevel.TRACE;
            logger.LogOutPutMode =
                OutputMode.DEBUG_WINDOW |
                OutputMode.CONSOLE |
                OutputMode.FILE;

            string runDir = config.GetRunDir();
            Directory.CreateDirectory(runDir);

            if (config.Mode == "worker")
            {
                int pid = Process.GetCurrentProcess().Id;
                string path = Path.Combine(
                    runDir,
                    "worker_" + config.WorkerIndex.ToString("000") + "_" + pid + ".log");

                logger.SetFilePath(path, "");
            }
            else
            {
                string path = Path.Combine(runDir, "master.log");
                logger.SetFilePath(path, "");
            }

            return logger;
        }
    }
}