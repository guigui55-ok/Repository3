using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace CopilotHistorySave
{
    /// <summary>
    /// workspaceStorage 配下から、ワークスペース単位で chatSessions/*.jsonl を探し出す。
    /// 入力には、workspaceStorage ルート、個別のワークスペースハッシュフォルダ、
    /// chatSessions フォルダ、または単一の .jsonl ファイルのいずれも指定できる。
    /// </summary>
    internal static class WorkspaceStorageScanner
    {
        private const string ChatSessionsFolderName = "chatSessions";
        private const string WorkspaceJsonFileName = "workspace.json";

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        internal static bool TryResolveTargets(string inputPath, out List<WorkspaceTarget> targets, out string warning)
        {
            targets = new List<WorkspaceTarget>();
            warning = string.Empty;

            string fullPath;
            if (!TryResolveFullPath(inputPath, out fullPath, out warning))
            {
                return false;
            }

            List<string> chatSessionsDirectories;
            if (!TryFindChatSessionsDirectories(fullPath, out chatSessionsDirectories, out warning))
            {
                return false;
            }

            foreach (string chatSessionsDirectory in chatSessionsDirectories)
            {
                targets.Add(BuildTarget(chatSessionsDirectory));
            }

            return true;
        }

        private static bool TryResolveFullPath(string inputPath, out string fullPath, out string warning)
        {
            fullPath = string.Empty;
            warning = string.Empty;

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                warning = "入力パスが空です。";
                return false;
            }

            try
            {
                fullPath = Path.GetFullPath(inputPath);
            }
            catch (Exception ex)
            {
                warning = "入力パスを解決できませんでした: " + ex.Message;
                return false;
            }

            if (!File.Exists(fullPath) && !Directory.Exists(fullPath))
            {
                warning = "指定されたパスが見つかりません: " + fullPath;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 入力が単一の .jsonl の場合はその1件、chatSessions フォルダの場合はその直下、
        /// それ以外はワークスペースストレージルートとみなし配下の chatSessions フォルダを再帰的に探す。
        /// </summary>
        private static bool TryFindChatSessionsDirectories(string fullPath, out List<string> directories, out string warning)
        {
            directories = new List<string>();
            warning = string.Empty;

            if (File.Exists(fullPath))
            {
                if (!string.Equals(Path.GetExtension(fullPath), ".jsonl", StringComparison.OrdinalIgnoreCase))
                {
                    warning = "入力がファイルの場合は .jsonl を指定してください。";
                    return false;
                }

                string parentDirectory = Path.GetDirectoryName(fullPath);
                if (parentDirectory != null)
                {
                    directories.Add(parentDirectory);
                }

                return true;
            }

            string leafName = new DirectoryInfo(fullPath).Name;
            if (leafName.Equals(ChatSessionsFolderName, StringComparison.OrdinalIgnoreCase))
            {
                directories.Add(fullPath);
                return true;
            }

            try
            {
                foreach (string directory in Directory.GetDirectories(fullPath, "*", SearchOption.AllDirectories))
                {
                    if (new DirectoryInfo(directory).Name.Equals(ChatSessionsFolderName, StringComparison.OrdinalIgnoreCase))
                    {
                        directories.Add(directory);
                    }
                }
            }
            catch (Exception ex)
            {
                warning = "chatSessions フォルダの列挙に失敗しました: " + ex.Message;
                return false;
            }

            return true;
        }

        private static WorkspaceTarget BuildTarget(string chatSessionsDirectory)
        {
            WorkspaceTarget target = new WorkspaceTarget();

            DirectoryInfo hashDirectory = new DirectoryInfo(chatSessionsDirectory).Parent;
            target.StorageHash = hashDirectory != null ? hashDirectory.Name : string.Empty;
            target.WorkspaceFolderPath = hashDirectory != null ? TryResolveWorkspaceFolder(hashDirectory.FullName) : null;

            // 対象がファイルとして渡された場合、単一ファイルのみを対象とする。
            try
            {
                target.SessionFilePaths.AddRange(Directory.GetFiles(chatSessionsDirectory, "*.jsonl", SearchOption.TopDirectoryOnly));
            }
            catch (Exception)
            {
                // 列挙不能なフォルダは空の対象として扱い、呼び出し元で件数0として処理する。
            }

            return target;
        }

        /// <summary>
        /// workspace.json の folder URI から実際のワークスペースフォルダパスを取得する。
        /// 取得できない場合は null を返し、呼び出し元でハッシュ名を代替表示に使う。
        /// </summary>
        private static string TryResolveWorkspaceFolder(string hashDirectoryPath)
        {
            string workspaceJsonPath = Path.Combine(hashDirectoryPath, WorkspaceJsonFileName);
            if (!File.Exists(workspaceJsonPath))
            {
                return null;
            }

            try
            {
                string json = File.ReadAllText(workspaceJsonPath);
                object parsed = Serializer.DeserializeObject(json);
                IDictionary<string, object> map = parsed as IDictionary<string, object>;
                if (map == null)
                {
                    return null;
                }

                object folderValue;
                if (!map.TryGetValue("folder", out folderValue) || folderValue == null)
                {
                    return null;
                }

                Uri uri;
                if (!Uri.TryCreate(Convert.ToString(folderValue), UriKind.Absolute, out uri))
                {
                    return null;
                }

                return uri.IsFile ? uri.LocalPath : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
