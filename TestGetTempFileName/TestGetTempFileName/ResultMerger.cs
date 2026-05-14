using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AppLoggerModule;

namespace TempFileNameTester
{
    public class ResultMerger
    {
        private readonly AppConfig _config;
        private readonly AppLogger _logger;

        public ResultMerger(AppConfig config, AppLogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public void Merge()
        {
            string runDir = _config.GetRunDir();

            string mergedResultPath = Path.Combine(runDir, "merged_result.txt");
            string mergedDetailPath = Path.Combine(runDir, "merged_detail.csv");
            string duplicatesPath = Path.Combine(runDir, "duplicates.txt");
            string summaryPath = Path.Combine(runDir, "summary.txt");
            string mergedLogPath = Path.Combine(runDir, "merged.log");

            List<string> allFileNames = new List<string>();

            using (StreamWriter mergedResult = new StreamWriter(mergedResultPath, false, Encoding.UTF8))
            using (StreamWriter mergedDetail = new StreamWriter(mergedDetailPath, false, Encoding.UTF8))
            using (StreamWriter mergedLog = new StreamWriter(mergedLogPath, false, Encoding.UTF8))
            {
                mergedDetail.WriteLine("FileName,FullPath,ProcessId,ThreadId,WorkerIndex");

                string[] resultFiles = Directory.GetFiles(runDir, "worker_*_result.txt");
                foreach (string resultFile in resultFiles)
                {
                    string[] lines = File.ReadAllLines(resultFile, Encoding.UTF8);
                    foreach (string line in lines)
                    {
                        mergedResult.WriteLine(line);
                        allFileNames.Add(line);
                    }
                }

                string[] detailFiles = Directory.GetFiles(runDir, "worker_*_detail.csv");
                foreach (string detailFile in detailFiles)
                {
                    string[] lines = File.ReadAllLines(detailFile, Encoding.UTF8);

                    for (int i = 1; i < lines.Length; i++)
                    {
                        mergedDetail.WriteLine(lines[i]);
                    }
                }

                string[] logFiles = Directory.GetFiles(runDir, "worker_*.log");
                foreach (string logFile in logFiles)
                {
                    mergedLog.WriteLine("===== " + Path.GetFileName(logFile) + " =====");
                    string[] lines = File.ReadAllLines(logFile, Encoding.UTF8);
                    foreach (string line in lines)
                    {
                        mergedLog.WriteLine(line);
                    }
                }
            }

            List<string> duplicates = DuplicateChecker.FindDuplicates(allFileNames);

            File.WriteAllLines(duplicatesPath, duplicates.ToArray(), Encoding.UTF8);

            WriteSummary(summaryPath, allFileNames.Count, duplicates.Count);

            _logger.PrintInfo("Merge finished.");
            _logger.PrintInfo("TotalCount=" + allFileNames.Count + ", DuplicateCount=" + duplicates.Count);
        }

        private void WriteSummary(string path, int totalCount, int duplicateCount)
        {
            List<string> lines = new List<string>();

            lines.Add("RunId=" + _config.RunId);
            lines.Add("ProcessCount=" + _config.ProcessCount);
            lines.Add("ThreadCount=" + _config.ThreadCount);
            lines.Add("LoopCount=" + _config.LoopCount);
            lines.Add("WaitMs=" + _config.WaitMs);
            lines.Add("TotalCount=" + totalCount);
            lines.Add("DuplicateCount=" + duplicateCount);
            lines.Add("CreatedAt=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            File.WriteAllLines(path, lines.ToArray(), Encoding.UTF8);
        }
    }
}