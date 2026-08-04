using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopilotHistorySave.Tests
{
    [TestClass]
    public class ChatSessionJsonlParserTests
    {
        private static string WriteTempJsonl(params string[] lines)
        {
            string path = Path.Combine(Path.GetTempPath(), "chsave_test_" + Guid.NewGuid().ToString("N") + ".jsonl");
            File.WriteAllText(path, string.Join("\n", lines), new UTF8Encoding(false));
            return path;
        }

        [TestMethod]
        public void ParsesSingleCompletedRequestFromSnapshot()
        {
            string line0 = "{\"kind\":0,\"v\":{\"version\":3,\"creationDate\":1785686340763,\"customTitle\":\"タイトル\",\"sessionId\":\"session-1\","
                + "\"requests\":[{\"requestId\":\"req-1\",\"timestamp\":1785686351653,\"responseTimestamp\":1785686360000,"
                + "\"message\":{\"text\":\"質問本文\"},"
                + "\"response\":[{\"kind\":\"mcpServersStarting\",\"didStartServerIds\":[]},{\"value\":\"回答本文\"}]}]}}";

            string path = WriteTempJsonl(line0);
            try
            {
                ChatSessionRecord record;
                List<string> warnings;
                bool ok = ChatSessionJsonlParser.TryParseSessionFile(path, out record, out warnings);

                Assert.IsTrue(ok);
                Assert.AreEqual("session-1", record.SessionId);
                Assert.AreEqual("タイトル", record.Title);
                Assert.AreEqual(1, record.QuestionCount);
                Assert.AreEqual("質問本文", record.Exchanges[0].QuestionText);
                Assert.AreEqual("回答本文", record.Exchanges[0].AnswerText);
                Assert.IsTrue(record.CreationDate.HasValue);
                Assert.IsTrue(record.LastUpdatedDate.HasValue);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [TestMethod]
        public void RebuildsMultipleRequestsFromIncrementalUpdates()
        {
            // kind0: リクエスト0件のスナップショット
            string line0 = "{\"kind\":0,\"v\":{\"creationDate\":1785686340763,\"customTitle\":\"初期タイトル\",\"sessionId\":\"session-2\",\"requests\":[]}}";
            // kind1: タイトル変更
            string line1 = "{\"kind\":1,\"k\":[\"customTitle\"],\"v\":\"更新後タイトル\"}";
            // kind2: requests へ1件目を追加(この時点で完了済み)
            string line2 = "{\"kind\":2,\"k\":[\"requests\"],\"v\":[{\"requestId\":\"req-1\",\"timestamp\":1785686351000,\"message\":{\"text\":\"質問1\"},\"response\":[{\"value\":\"回答1\"}]}]}";
            // kind2: requests へ2件目を追加(この時点では response が未完了)
            string line3 = "{\"kind\":2,\"k\":[\"requests\"],\"v\":[{\"requestId\":\"req-2\",\"timestamp\":1785686400000,\"message\":{\"text\":\"質問2\"},\"response\":[{\"kind\":\"mcpServersStarting\",\"didStartServerIds\":[]}]}]}";
            // kind1: 2件目の result を更新
            string line4 = "{\"kind\":1,\"k\":[\"requests\",1,\"result\"],\"v\":{\"errorDetails\":null}}";
            // kind2: 2件目の response 配列へ本文チャンクを追記
            string line5 = "{\"kind\":2,\"k\":[\"requests\",1,\"response\"],\"v\":[{\"value\":\"回答2\"}]}";
            // kind1: 2件目の responseTimestamp を設定
            string line6 = "{\"kind\":1,\"k\":[\"requests\",1,\"responseTimestamp\"],\"v\":1785686500000}";

            string path = WriteTempJsonl(line0, line1, line2, line3, line4, line5, line6);
            try
            {
                ChatSessionRecord record;
                List<string> warnings;
                bool ok = ChatSessionJsonlParser.TryParseSessionFile(path, out record, out warnings);

                Assert.IsTrue(ok, string.Join(" | ", warnings));
                Assert.AreEqual(2, record.QuestionCount);
                Assert.AreEqual("質問1", record.Exchanges[0].QuestionText);
                Assert.AreEqual("回答1", record.Exchanges[0].AnswerText);
                Assert.AreEqual("質問2", record.Exchanges[1].QuestionText);
                Assert.AreEqual("回答2", record.Exchanges[1].AnswerText);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [TestMethod]
        public void ContinuesProcessingWhenLinesAreEmptyOrMalformed()
        {
            string line0 = "{\"kind\":0,\"v\":{\"sessionId\":\"session-3\",\"customTitle\":\"タイトル3\",\"requests\":[{\"requestId\":\"req-1\",\"timestamp\":1,\"message\":{\"text\":\"質問\"},\"response\":[{\"value\":\"回答\"}]}]}}";
            string emptyLine = "";
            string brokenJson = "{not valid json";
            string unknownKind = "{\"kind\":99,\"v\":{}}";

            string path = WriteTempJsonl(line0, emptyLine, brokenJson, unknownKind);
            try
            {
                ChatSessionRecord record;
                List<string> warnings;
                bool ok = ChatSessionJsonlParser.TryParseSessionFile(path, out record, out warnings);

                Assert.IsTrue(ok);
                Assert.AreEqual(1, record.QuestionCount);
                Assert.IsTrue(warnings.Count >= 2);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [TestMethod]
        public void ReturnsFalseWhenSnapshotIsMissing()
        {
            string onlyUpdateLine = "{\"kind\":1,\"k\":[\"customTitle\"],\"v\":\"タイトル\"}";

            string path = WriteTempJsonl(onlyUpdateLine);
            try
            {
                ChatSessionRecord record;
                List<string> warnings;
                bool ok = ChatSessionJsonlParser.TryParseSessionFile(path, out record, out warnings);

                Assert.IsFalse(ok);
                Assert.IsTrue(warnings.Count > 0);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [TestMethod]
        public void ReturnsFalseWhenFileDoesNotExist()
        {
            string missingPath = Path.Combine(Path.GetTempPath(), "chsave_missing_" + Guid.NewGuid().ToString("N") + ".jsonl");

            ChatSessionRecord record;
            List<string> warnings;
            bool ok = ChatSessionJsonlParser.TryParseSessionFile(missingPath, out record, out warnings);

            Assert.IsFalse(ok);
            Assert.IsTrue(warnings.Count > 0);
        }

        [TestMethod]
        public void HandlesMissingOptionalFieldsGracefully()
        {
            // message や response が欠けているレコードでも例外を投げず、空文字として扱う。
            string line0 = "{\"kind\":0,\"v\":{\"sessionId\":\"session-4\",\"requests\":[{\"requestId\":\"req-1\",\"timestamp\":1}]}}";

            string path = WriteTempJsonl(line0);
            try
            {
                ChatSessionRecord record;
                List<string> warnings;
                bool ok = ChatSessionJsonlParser.TryParseSessionFile(path, out record, out warnings);

                Assert.IsTrue(ok);
                Assert.AreEqual(1, record.QuestionCount);
                Assert.AreEqual(string.Empty, record.Exchanges[0].QuestionText);
                Assert.AreEqual(string.Empty, record.Exchanges[0].AnswerText);
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
