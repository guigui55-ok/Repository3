### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\AppConfig.cs
```csharp
using System;
using System.IO;

namespace TempFileNameTester
{
    public class AppConfig
    {
        public string Mode { get; set; } = "master";

        public int ProcessCount { get; set; } = 1;
        public int ThreadCount { get; set; } = 1;
        public int LoopCount { get; set; } = 100;
        public int WaitMs { get; set; } = 0;

        public int WorkerIndex { get; set; } = 0;
        public string RunId { get; set; } = "";
        public string WorkDir { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        public string TempDir { get; set; } = "";

        public bool DeleteCreatedFiles { get; set; } = true;

        // simple / detail
        public string LogMode { get; set; } = "simple";

        public bool IsDetailLog
        {
            get { return string.Equals(this.LogMode, "detail", StringComparison.OrdinalIgnoreCase); }
        }

        public string GetRunDir()
        {
            return System.IO.Path.Combine(this.WorkDir, "run_" + this.RunId);
        }
    }
}
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\AppLogger.cs
```csharp

using System;
using System.Diagnostics;
using System.IO;

namespace AppLoggerModule
{

    public enum LogLevel
    {
        DEF,
        CRITICAL,
        ERR,
        WARN,
        NORMAL,
        INFO,
        DEBUG,
        TRACE
    }

    [Flags]
    public enum OutputMode
    {
        NONE = 0,               // 0000
        DEBUG_WINDOW = 1,       // 0001
        CONSOLE = 2,            // 0010
        FILE = 4                // 0100
    }

    public class AppLogger
    {
        public LogLevel LoggerLogLevel { get; set; } = LogLevel.INFO;
        public string FilePath { get; set; } = "";
        public string LogFileTimeFormat { get; set; } = "_yyyyMMdd_HHmmss";
        public OutputMode LogOutPutMode { get; set; } = OutputMode.DEBUG_WINDOW;
        public bool AddTime { get; set; } = true;

        public AppLogger() { }

        public void MakeLogDir()
        {
            string dirPath = Path.GetDirectoryName(this.FilePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                Debug.Print("Log CreateDirectory Path= " + dirPath);
            }
        }

        /// <summary>
        /// ログのファイルパスを設定する
        /// </summary>
        /// <remarks>
        /// logFileTimeFormat または this.logFileTimeFormat が設定されているときは
        /// log_[TimeFormat].logというように、時間書式が追加される
        /// </remarks>
        /// <param name="filePath"></param>
        /// <param name="logFileTimeFormat"></param>
        public void SetFilePath(string filePath, string logFileTimeFormat = "")
        {
            if (string.IsNullOrEmpty(logFileTimeFormat))
            {
                logFileTimeFormat = this.LogFileTimeFormat;
            }

            if (string.IsNullOrEmpty(logFileTimeFormat))
            {
                this.FilePath = filePath;
            }
            else
            {
                string dirPath = Path.GetDirectoryName(filePath);
                string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
                string datetimeStr = DateTime.Now.ToString(logFileTimeFormat);
                string ext = Path.GetExtension(filePath);
                this.FilePath = $"{dirPath}\\{fileNameOnly}{datetimeStr}{ext}";
            }
            this.MakeLogDir();
        }

        public void PrintCritical(string value)
        {
            if (LogLevel.CRITICAL <= this.LoggerLogLevel)
            {
                this.Print(value);
            }
        }

        public void PrintError(string value)
        {
            if (LogLevel.ERR <= this.LoggerLogLevel)
            {
                this.Print(value);
            }
        }

        public void PrintWarn(string value)
        {
            if (LogLevel.WARN <= this.LoggerLogLevel)
            {
                this.Print(value);
            }
        }

        public void PrintInfo(string value)
        {
            if (LogLevel.INFO <= this.LoggerLogLevel)
            {
                this.Print(value);
            }
        }

        public void PrintDebug(string value)
        {
            if (LogLevel.DEBUG <= this.LoggerLogLevel)
            {
                this.Print(value);
            }
        }

        public void PrintTrace(string value)
        {
            if (LogLevel.TRACE <= this.LoggerLogLevel)
            {
                this.Print(value);
            }
        }

        private string AddTimeValue(string value)
        {
            if (this.AddTime)
            {
                return this.GetTimeStr() + "    " + value;
            }
            return value;
        }

        private string GetTimeStr()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyy/MM/dd HH:mm:ss ffffff");
        }

        private void Print(string value)
        {
            value = this.AddTimeValue(value);
            if ((this.LogOutPutMode & OutputMode.DEBUG_WINDOW) == OutputMode.DEBUG_WINDOW)
            {
                Debug.WriteLine(value);
            }
            if ((this.LogOutPutMode & OutputMode.CONSOLE) == OutputMode.CONSOLE)
            {
                Console.WriteLine(value);
            }
            if ((this.LogOutPutMode & OutputMode.FILE) == OutputMode.FILE)
            {
                this.PrintToFile(value);
            }
        }

        private void PrintToFile(string value)
        {
            if (!string.IsNullOrEmpty(this.FilePath))
            {
                this.WriteToFile(value);
            }
        }

        private void WriteToFile(string value)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.FilePath, true))
                {
                    writer.WriteLine(value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WriteToFile ERROR: " + ex.Message);
            }
        }
    }
}
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\CommandLineParser.cs
```csharp
using System;
using System.Collections.Generic;

namespace TempFileNameTester
{
    public static class CommandLineParser
    {
        public static AppConfig Parse(string[] args)
        {
            AppConfig config = new AppConfig();

            Dictionary<string, string> map = ToDictionary(args);

            config.Mode = GetString(map, "--mode", config.Mode);
            config.ProcessCount = GetInt(map, "--process-count", config.ProcessCount);
            config.ThreadCount = GetInt(map, "--thread-count", config.ThreadCount);
            config.LoopCount = GetInt(map, "--loop-count", config.LoopCount);
            config.WaitMs = GetInt(map, "--wait-ms", config.WaitMs);

            config.WorkerIndex = GetInt(map, "--worker-index", config.WorkerIndex);
            config.RunId = GetString(map, "--run-id", config.RunId);

            config.WorkDir = GetString(map, "--work-dir", config.WorkDir);
            config.TempDir = GetString(map, "--temp-dir", config.TempDir);

            config.DeleteCreatedFiles = GetBool(map, "--delete-created-files", config.DeleteCreatedFiles);
            config.LogMode = GetString(map, "--log-mode", config.LogMode);

            if (string.IsNullOrEmpty(config.RunId))
            {
                config.RunId = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            }

            return config;
        }

        private static Dictionary<string, string> ToDictionary(string[] args)
        {
            Dictionary<string, string> map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < args.Length; i++)
            {
                string key = args[i];

                if (!key.StartsWith("--"))
                {
                    continue;
                }

                string value = "";

                if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
                {
                    value = args[i + 1];
                    i++;
                }

                map[key] = value;
            }

            return map;
        }

        private static string GetString(Dictionary<string, string> map, string key, string defaultValue)
        {
            if (map.ContainsKey(key))
            {
                return map[key];
            }

            return defaultValue;
        }

        private static int GetInt(Dictionary<string, string> map, string key, int defaultValue)
        {
            if (!map.ContainsKey(key))
            {
                return defaultValue;
            }

            int value;
            if (int.TryParse(map[key], out value))
            {
                return value;
            }

            return defaultValue;
        }

        private static bool GetBool(Dictionary<string, string> map, string key, bool defaultValue)
        {
            if (!map.ContainsKey(key))
            {
                return defaultValue;
            }

            bool value;
            if (bool.TryParse(map[key], out value))
            {
                return value;
            }

            return defaultValue;
        }
    }
}
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\DuplicateChecker.cs
```csharp
using System.Collections.Generic;
using System.Linq;

namespace TempFileNameTester
{
    public static class DuplicateChecker
    {
        public static List<string> FindDuplicates(List<string> values)
        {
            HashSet<string> set = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase);
            HashSet<string> duplicates = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase);

            foreach (string value in values)
            {
                if (!set.Add(value))
                {
                    duplicates.Add(value);
                }
            }

            return duplicates.ToList();
        }

        public static List<string> FindDuplicatesFromRecords(List<TempFileRecord> records)
        {
            List<string> names = new List<string>();

            foreach (TempFileRecord record in records)
            {
                names.Add(record.FileName);
            }

            return FindDuplicates(names);
        }
    }
}
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\LoggerFactory.cs
```csharp
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\MasterRunner.cs
```csharp
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\obj\Debug\.NETFramework,Version=v4.7.2.AssemblyAttributes.cs
```csharp
// <autogenerated />
using System;
using System.Reflection;
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.7.2", FrameworkDisplayName = ".NET Framework 4.7.2")]
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\ProcessWorker.cs
```csharp
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\Program.cs
```csharp
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
                    "--process-count", "8",
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
                 * TotalCount=24000, DuplicateCount=
                 * process:8 ,thread:4, loop:1000, wait:0, delete:false
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\Properties\AssemblyInfo.cs
```csharp
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般的な情報は、次の方法で制御されます。
// 制御されます。アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更します。
[assembly: AssemblyTitle("TestGetTempFileName")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("TestGetTempFileName")]
[assembly: AssemblyCopyright("Copyright c  2026")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、このアセンブリ内の型は COM コンポーネントから
// 参照できなくなります。COM からこのアセンブリ内の型にアクセスする必要がある場合は、
// その型の ComVisible 属性を true に設定します。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
[assembly: Guid("6865353b-366d-491b-af7c-e674dd172a06")]

// アセンブリのバージョン情報は次の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      リビジョン
//
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\ResultFileWriter.cs
```csharp
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\ResultMerger.cs
```csharp
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\TempFileRecord.cs
```csharp
namespace TempFileNameTester
{
    public class TempFileRecord
    {
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public int ProcessId { get; set; }
        public int ThreadId { get; set; }
        public int WorkerIndex { get; set; }

        public TempFileRecord()
        {
            this.FileName = "";
            this.FullPath = "";
        }
    }
}
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\ThreadWorker.cs
```csharp
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\WorkerProcessLauncher.cs
```csharp
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
```

### D:\git\Repository3\TestGetTempFileName\TestGetTempFileName\WorkerResult.cs
```csharp
using System.Collections.Generic;

namespace TempFileNameTester
{
    public class WorkerResult
    {
        public List<TempFileRecord> Records { get; private set; }
        public List<string> Errors { get; private set; }
        public List<string> Duplicates { get; private set; }

        public WorkerResult()
        {
            this.Records = new List<TempFileRecord>();
            this.Errors = new List<string>();
            this.Duplicates = new List<string>();
        }
    }
}
```

