using System;
using System.IO;

namespace CopilotHistorySave.Tests
{
    [TestClass]
    public class MarkdownSessionWriterTests
    {
        private string _outputRoot;

        [TestInitialize]
        public void SetUp()
        {
            _outputRoot = Path.Combine(Path.GetTempPath(), "chsave_out_" + Guid.NewGuid().ToString("N"));
        }

        [TestCleanup]
        public void TearDown()
        {
            if (Directory.Exists(_outputRoot))
            {
                Directory.Delete(_outputRoot, true);
            }
        }

        private static ChatSessionRecord CreateRecord(string workspaceFolder, string sessionId, string title, string question, string answer)
        {
            ChatSessionRecord record = new ChatSessionRecord();
            record.WorkspaceFolderPath = workspaceFolder;
            record.SessionId = sessionId;
            record.Title = title;
            record.CreationDate = new DateTime(2026, 8, 3, 0, 59, 0);
            record.LastUpdatedDate = new DateTime(2026, 8, 3, 1, 0, 0);
            record.Exchanges.Add(new ChatExchange
            {
                RequestId = "req-1",
                QuestionTimestamp = record.CreationDate,
                QuestionText = question,
                AnswerTimestamp = record.LastUpdatedDate,
                AnswerText = answer
            });

            return record;
        }

        [TestMethod]
        public void WritesMarkdownFileWithExpectedContent()
        {
            ChatSessionRecord record = CreateRecord(@"D:\git\Repository3\CopilotHistorySaver", "session-1", "テストタイトル", "質問本文", "回答本文");

            string writtenFilePath;
            string warning;
            bool ok = MarkdownSessionWriter.TryWriteSession(_outputRoot, record, out writtenFilePath, out warning);

            Assert.IsTrue(ok);
            Assert.IsTrue(File.Exists(writtenFilePath));

            string content = File.ReadAllText(writtenFilePath);
            StringAssert.Contains(content, "テストタイトル");
            StringAssert.Contains(content, "質問本文");
            StringAssert.Contains(content, "回答本文");
            StringAssert.Contains(content, "session-1");
        }

        [TestMethod]
        public void OverwritesExistingFileInsteadOfDuplicating()
        {
            ChatSessionRecord first = CreateRecord(@"D:\git\Repository3\CopilotHistorySaver", "session-2", "旧タイトル", "旧質問", "旧回答");
            ChatSessionRecord second = CreateRecord(@"D:\git\Repository3\CopilotHistorySaver", "session-2", "新タイトル", "新質問", "新回答");

            string firstPath;
            string secondPath;
            string warning;
            MarkdownSessionWriter.TryWriteSession(_outputRoot, first, out firstPath, out warning);
            MarkdownSessionWriter.TryWriteSession(_outputRoot, second, out secondPath, out warning);

            Assert.AreEqual(firstPath, secondPath);

            string[] files = Directory.GetFiles(_outputRoot, "*.md", SearchOption.AllDirectories);
            Assert.AreEqual(1, files.Length);

            string content = File.ReadAllText(secondPath);
            StringAssert.Contains(content, "新タイトル");
            Assert.IsFalse(content.Contains("旧タイトル"));
        }

        [TestMethod]
        public void GroupsSessionsByWorkspaceFolder()
        {
            ChatSessionRecord recordA = CreateRecord(@"D:\workspaceA", "session-a", "A", "qa", "aa");
            ChatSessionRecord recordB = CreateRecord(@"D:\workspaceB", "session-b", "B", "qb", "ab");

            string pathA;
            string pathB;
            string warning;
            MarkdownSessionWriter.TryWriteSession(_outputRoot, recordA, out pathA, out warning);
            MarkdownSessionWriter.TryWriteSession(_outputRoot, recordB, out pathB, out warning);

            Assert.AreNotEqual(Path.GetDirectoryName(pathA), Path.GetDirectoryName(pathB));
        }

        [TestMethod]
        public void FallsBackToStorageHashWhenWorkspaceFolderUnknown()
        {
            ChatSessionRecord record = CreateRecord(null, "session-unknown", "タイトル", "質問", "回答");
            record.StorageHash = "abcdef1234567890";

            string writtenFilePath;
            string warning;
            bool ok = MarkdownSessionWriter.TryWriteSession(_outputRoot, record, out writtenFilePath, out warning);

            Assert.IsTrue(ok);
            StringAssert.Contains(Path.GetDirectoryName(writtenFilePath), "abcdef1234567890");
        }

        [TestMethod]
        public void ReturnsFalseWhenOutputRootCannotBeCreated()
        {
            // 出力先の親パスとして、既存のファイルを流用し、フォルダ作成が失敗するケースを再現する。
            Directory.CreateDirectory(_outputRoot);
            string blockingFilePath = Path.Combine(_outputRoot, "blocking-file");
            File.WriteAllText(blockingFilePath, "dummy");
            string invalidRoot = Path.Combine(blockingFilePath, "child-folder");

            ChatSessionRecord record = CreateRecord(@"D:\workspace", "session-x", "タイトル", "質問", "回答");

            string writtenFilePath;
            string warning;
            bool ok = MarkdownSessionWriter.TryWriteSession(invalidRoot, record, out writtenFilePath, out warning);

            Assert.IsFalse(ok);
            Assert.IsFalse(string.IsNullOrEmpty(warning));
        }
    }
}
