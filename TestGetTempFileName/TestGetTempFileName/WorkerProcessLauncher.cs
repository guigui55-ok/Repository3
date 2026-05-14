using System;
using System.Diagnostics;
using System.IO;
using AppLoggerModule;

namespace TempFileNameTester
{
    public class WorkerProcessLauncher
    {
        private readonly AppConfig _config;
        private readonly AppLogger _logger;

        public WorkerProcessLauncher(AppConfig config, AppLogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public Process StartWorker(int workerIndex)
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;

            string args =
                "--mode worker " +
                "--worker-index " + workerIndex + " " +
                "--run-id " + Quote(_config.RunId) + " " +
                "--thread-count " + _config.ThreadCount + " " +
                "--loop-count " + _config.LoopCount + " " +
                "--wait-ms " + _config.WaitMs + " " +
                "--work-dir " + Quote(_config.WorkDir) + " " +
                "--temp-dir " + Quote(_config.TempDir) + " " +
                "--delete-created-files " + _config.DeleteCreatedFiles.ToString().ToLower() + " " +
                "--log-mode " + Quote(_config.LogMode);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = exePath;
            psi.Arguments = args;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = false;

            // 今回は temp-dir による TEMP/TMP 上書きは行わない方針
            // 必要になった場合は以下を有効化する
            /*
            if (!string.IsNullOrEmpty(_config.TempDir))
            {
                Directory.CreateDirectory(_config.TempDir);
                psi.EnvironmentVariables["TEMP"] = _config.TempDir;
                psi.EnvironmentVariables["TMP"] = _config.TempDir;
            }
            */

            _logger.PrintInfo("Start worker. Index=" + workerIndex + ", Args=" + args);

            return Process.Start(psi);
        }

        private static string Quote(string value)
        {
            if (value == null)
            {
                value = "";
            }

            return "\"" + value.Replace("\"", "\\\"") + "\"";
        }
    }
}