using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CopilotHistorySave.Tests
{
    [TestClass]
    public class WorkspaceStorageScannerTests
    {
        private string _root;

        [TestInitialize]
        public void SetUp()
        {
            _root = Path.Combine(Path.GetTempPath(), "chsave_scan_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_root);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (Directory.Exists(_root))
            {
                Directory.Delete(_root, true);
            }
        }

        private string CreateWorkspaceHash(string hashName, string workspaceFolder, params string[] sessionFileNames)
        {
            string hashDir = Path.Combine(_root, hashName);
            string chatSessionsDir = Path.Combine(hashDir, "chatSessions");
            Directory.CreateDirectory(chatSessionsDir);

            if (workspaceFolder != null)
            {
                string uri = new Uri(workspaceFolder).AbsoluteUri;
                File.WriteAllText(Path.Combine(hashDir, "workspace.json"), "{\"folder\":\"" + uri.Replace("\\", "/") + "\"}");
            }

            foreach (string sessionFileName in sessionFileNames)
            {
                File.WriteAllText(Path.Combine(chatSessionsDir, sessionFileName), "{\"kind\":0,\"v\":{\"sessionId\":\"x\",\"requests\":[]}}");
            }

            return chatSessionsDir;
        }

        [TestMethod]
        public void FindsAllChatSessionsUnderStorageRoot()
        {
            string workspaceOnePath = Path.Combine(Path.GetTempPath(), "wsOne_" + Guid.NewGuid().ToString("N"));
            string workspaceTwoPath = Path.Combine(Path.GetTempPath(), "wsTwo_" + Guid.NewGuid().ToString("N"));
            CreateWorkspaceHash("hash1", workspaceOnePath, "a.jsonl", "b.jsonl");
            CreateWorkspaceHash("hash2", workspaceTwoPath, "c.jsonl");

            List<WorkspaceTarget> targets;
            string warning;
            bool ok = WorkspaceStorageScanner.TryResolveTargets(_root, out targets, out warning);

            Assert.IsTrue(ok);
            Assert.AreEqual(2, targets.Count);
            Assert.AreEqual(3, targets.Sum(t => t.SessionFilePaths.Count));
        }

        [TestMethod]
        public void ResolvesWorkspaceFolderFromWorkspaceJson()
        {
            string workspacePath = Path.Combine(Path.GetTempPath(), "wsThree_" + Guid.NewGuid().ToString("N"));
            CreateWorkspaceHash("hash3", workspacePath, "a.jsonl");

            List<WorkspaceTarget> targets;
            string warning;
            WorkspaceStorageScanner.TryResolveTargets(_root, out targets, out warning);

            WorkspaceTarget target = targets.Single();
            Assert.IsNotNull(target.WorkspaceFolderPath);
            Assert.AreEqual(
                Path.GetFullPath(workspacePath).TrimEnd('\\'),
                Path.GetFullPath(target.WorkspaceFolderPath).TrimEnd('\\'));
        }

        [TestMethod]
        public void FallsBackToHashWhenWorkspaceJsonMissing()
        {
            CreateWorkspaceHash("hash-no-workspace-json", null, "a.jsonl");

            List<WorkspaceTarget> targets;
            string warning;
            WorkspaceStorageScanner.TryResolveTargets(_root, out targets, out warning);

            WorkspaceTarget target = targets.Single();
            Assert.IsNull(target.WorkspaceFolderPath);
            Assert.AreEqual("hash-no-workspace-json", target.StorageHash);
        }

        [TestMethod]
        public void ResolvesSingleChatSessionsDirectoryDirectly()
        {
            string workspacePath = Path.Combine(Path.GetTempPath(), "wsFour_" + Guid.NewGuid().ToString("N"));
            string chatSessionsDir = CreateWorkspaceHash("hash4", workspacePath, "a.jsonl");

            List<WorkspaceTarget> targets;
            string warning;
            bool ok = WorkspaceStorageScanner.TryResolveTargets(chatSessionsDir, out targets, out warning);

            Assert.IsTrue(ok);
            Assert.AreEqual(1, targets.Count);
            Assert.AreEqual(1, targets[0].SessionFilePaths.Count);
        }

        [TestMethod]
        public void ResolvesSingleJsonlFileDirectly()
        {
            string workspacePath = Path.Combine(Path.GetTempPath(), "wsFive_" + Guid.NewGuid().ToString("N"));
            string chatSessionsDir = CreateWorkspaceHash("hash5", workspacePath, "a.jsonl", "b.jsonl");
            string singleFile = Path.Combine(chatSessionsDir, "a.jsonl");

            List<WorkspaceTarget> targets;
            string warning;
            bool ok = WorkspaceStorageScanner.TryResolveTargets(singleFile, out targets, out warning);

            Assert.IsTrue(ok);
            Assert.AreEqual(1, targets.Count);
            Assert.AreEqual(2, targets[0].SessionFilePaths.Count); // chatSessions フォルダ全体が対象になる
        }

        [TestMethod]
        public void ReturnsFalseWhenInputPathDoesNotExist()
        {
            string missingPath = Path.Combine(_root, "does-not-exist");

            List<WorkspaceTarget> targets;
            string warning;
            bool ok = WorkspaceStorageScanner.TryResolveTargets(missingPath, out targets, out warning);

            Assert.IsFalse(ok);
            Assert.IsFalse(string.IsNullOrEmpty(warning));
        }
    }
}
