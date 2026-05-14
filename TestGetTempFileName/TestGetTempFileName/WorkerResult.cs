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