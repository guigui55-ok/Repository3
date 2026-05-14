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