using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AppLoggerModule;

namespace TempFileNameTester
{
    public class MasterRunner
    {
        private readonly AppConfig _config;
        private readonly AppLogger _logger;

        public MasterRunner(AppConfig config, AppLogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public void Run()
        {
            Directory.CreateDirectory(_config.GetRunDir());

            _logger.PrintInfo("Master start.");
            _logger.PrintInfo("RunId=" + _config.RunId);
            _logger.PrintInfo("RunDir=" + _config.GetRunDir());

            WorkerProcessLauncher launcher = new WorkerProcessLauncher(_config, _logger);

            List<Process> processes = new List<Process>();

            for (int i = 1; i <= _config.ProcessCount; i++)
            {
                Process process = launcher.StartWorker(i);
                processes.Add(process);
            }

            foreach (Process process in processes)
            {
                process.WaitForExit();
                _logger.PrintInfo("Worker exited. PID=" + process.Id + ", ExitCode=" + process.ExitCode);
            }

            ResultMerger merger = new ResultMerger(_config, _logger);
            merger.Merge();

            _logger.PrintInfo("Master finished.");
        }
    }
}