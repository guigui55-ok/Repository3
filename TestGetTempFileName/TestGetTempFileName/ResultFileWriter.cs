using System.Collections.Generic;
using System.IO;
using System.Text;
using AppLoggerModule;

namespace TempFileNameTester
{
    public static class ResultFileWriter
    {
        public static void WriteWorkerFiles(
            AppConfig config,
            int pid,
            WorkerResult result,
            AppLogger logger)
        {
            string runDir = config.GetRunDir();
            Directory.CreateDirectory(runDir);

            string prefix = "worker_" + config.WorkerIndex.ToString("000") + "_" + pid;

            string resultPath = Path.Combine(runDir, prefix + "_result.txt");
            string detailPath = Path.Combine(runDir, prefix + "_detail.csv");
            string duplicatePath = Path.Combine(runDir, prefix + "_duplicates.txt");
            string errorPath = Path.Combine(runDir, prefix + "_errors.txt");

            WriteResultFile(resultPath, result.Records);
            WriteDetailCsv(detailPath, result.Records);
            WriteTextLines(duplicatePath, result.Duplicates);
            WriteTextLines(errorPath, result.Errors);

            logger.PrintInfo("Worker result written. " + resultPath);
            logger.PrintInfo("Worker detail written. " + detailPath);
            logger.PrintInfo("Worker duplicates written. " + duplicatePath);
            logger.PrintInfo("Worker errors written. " + errorPath);
        }

        private static void WriteResultFile(string path, List<TempFileRecord> records)
        {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (TempFileRecord record in records)
                {
                    writer.WriteLine(record.FileName);
                }
            }
        }

        private static void WriteDetailCsv(string path, List<TempFileRecord> records)
        {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine("FileName,FullPath,ProcessId,ThreadId,WorkerIndex");

                foreach (TempFileRecord record in records)
                {
                    writer.WriteLine(
                        Csv(record.FileName) + "," +
                        Csv(record.FullPath) + "," +
                        record.ProcessId + "," +
                        record.ThreadId + "," +
                        record.WorkerIndex);
                }
            }
        }

        private static void WriteTextLines(string path, List<string> lines)
        {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        private static string Csv(string value)
        {
            if (value == null)
            {
                return "";
            }

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}