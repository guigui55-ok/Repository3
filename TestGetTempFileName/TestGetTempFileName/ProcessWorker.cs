using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using AppLoggerModule;

namespace TempFileNameTester
{
    public class ProcessWorker
    {
        private readonly AppConfig _config;
        private readonly AppLogger _logger;

        public ProcessWorker(AppConfig config, AppLogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public void Run()
        {
            int pid = Process.GetCurrentProcess().Id;

            Directory.CreateDirectory(_config.GetRunDir());

            _logger.PrintInfo("Worker start. PID=" + pid + ", WorkerIndex=" + _config.WorkerIndex);
            _logger.PrintInfo("TempDir setting is currently not applied. Path.GetTempFileName uses OS temp directory.");

            List<ThreadWorker> workers = new List<ThreadWorker>();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < _config.ThreadCount; i++)
            {
                ThreadWorker worker = new ThreadWorker(_config, _logger, i + 1);
                workers.Add(worker);

                Thread thread = new Thread(worker.Run);
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            WorkerResult mergedResult = MergeThreadResults(workers);

            List<string> processDuplicates = DuplicateChecker.FindDuplicatesFromRecords(mergedResult.Records);
            mergedResult.Duplicates.AddRange(processDuplicates);

            ResultFileWriter.WriteWorkerFiles(_config, pid, mergedResult, _logger);

            if (_config.DeleteCreatedFiles)
            {
                DeleteCreatedFiles(mergedResult.Records);
            }

            _logger.PrintInfo(
                "Worker finished. PID=" + pid +
                ", WorkerIndex=" + _config.WorkerIndex +
                ", Count=" + mergedResult.Records.Count +
                ", DuplicateCount=" + mergedResult.Duplicates.Count +
                ", ErrorCount=" + mergedResult.Errors.Count);
        }

        private WorkerResult MergeThreadResults(List<ThreadWorker> workers)
        {
            WorkerResult result = new WorkerResult();

            foreach (ThreadWorker worker in workers)
            {
                result.Records.AddRange(worker.Result.Records);
                result.Errors.AddRange(worker.Result.Errors);
                result.Duplicates.AddRange(worker.Result.Duplicates);
            }

            return result;
        }

        private void DeleteCreatedFiles(List<TempFileRecord> records)
        {
            foreach (TempFileRecord record in records)
            {
                try
                {
                    if (!string.IsNullOrEmpty(record.FullPath) && File.Exists(record.FullPath))
                    {
                        File.Delete(record.FullPath);
                    }
                }
                catch (Exception ex)
                {
                    _logger.PrintError("Delete temp file error. Path=" + record.FullPath + ", " + ex.Message);
                }
            }
        }
    }
}