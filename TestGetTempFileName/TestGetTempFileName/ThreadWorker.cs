using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using AppLoggerModule;

namespace TempFileNameTester
{
    public class ThreadWorker
    {
        private readonly AppConfig _config;
        private readonly AppLogger _logger;
        private readonly int _threadWorkerIndex;

        public WorkerResult Result { get; private set; }

        public ThreadWorker(AppConfig config, AppLogger logger, int threadWorkerIndex)
        {
            _config = config;
            _logger = logger;
            _threadWorkerIndex = threadWorkerIndex;
            Result = new WorkerResult();
        }

        public void Run()
        {
            int pid = Process.GetCurrentProcess().Id;
            int threadId = Thread.CurrentThread.ManagedThreadId;

            List<string> fileNameList = new List<string>();

            for (int i = 0; i < _config.LoopCount; i++)
            {
                try
                {
                    string fullPath = Path.GetTempFileName();
                    string fileName = Path.GetFileName(fullPath);

                    TempFileRecord record = new TempFileRecord();
                    record.FileName = fileName;
                    record.FullPath = fullPath;
                    record.ProcessId = pid;
                    record.ThreadId = threadId;
                    record.WorkerIndex = _config.WorkerIndex;

                    Result.Records.Add(record);
                    fileNameList.Add(fileName);

                    if (_config.IsDetailLog)
                    {
                        _logger.PrintInfo(
                            "PID=" + pid +
                            ", ThreadID=" + threadId +
                            ", FileName=" + fileName);
                    }

                    if (_config.WaitMs > 0)
                    {
                        Thread.Sleep(_config.WaitMs);
                    }
                }
                catch (Exception ex)
                {
                    string message =
                        "ThreadWorker ERROR. WorkerIndex=" + _config.WorkerIndex +
                        ", ThreadWorkerIndex=" + _threadWorkerIndex +
                        ", ThreadID=" + threadId +
                        ", " + ex.ToString();

                    Result.Errors.Add(message);
                    _logger.PrintError(message);
                }
            }

            List<string> duplicates = DuplicateChecker.FindDuplicates(fileNameList);
            Result.Duplicates.AddRange(duplicates);

            _logger.PrintInfo(
                "ThreadWorker finished. WorkerIndex=" + _config.WorkerIndex +
                ", ThreadWorkerIndex=" + _threadWorkerIndex +
                ", ThreadID=" + threadId +
                ", Count=" + Result.Records.Count +
                ", DuplicateCount=" + duplicates.Count +
                ", ErrorCount=" + Result.Errors.Count);
        }
    }
}