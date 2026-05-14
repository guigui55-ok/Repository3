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