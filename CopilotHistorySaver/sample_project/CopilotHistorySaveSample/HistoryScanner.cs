using System;
using System.Collections.Generic;
using System.IO;

namespace CopilotHistorySaveSample
{
    internal enum TargetKind
    {
        ChatSessionsDirectory,
        WorkspaceStorageRoot
    }

    internal static class HistoryScanner
    {
        internal static bool TryResolveTarget(string inputPath, out TargetKind targetKind, out string resolvedPath, out string warning)
        {
            targetKind = TargetKind.WorkspaceStorageRoot;
            resolvedPath = string.Empty;
            warning = string.Empty;

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                warning = "入力パスが空です。";
                return false;
            }

            string fullPath;
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

            resolvedPath = fullPath;
            string leafName = new DirectoryInfo(fullPath).Name;
            if (leafName.Equals("chatSessions", StringComparison.OrdinalIgnoreCase))
            {
                targetKind = TargetKind.ChatSessionsDirectory;
            }

            return true;
        }

        internal static bool TryEnumerateSessionFiles(string resolvedPath, TargetKind targetKind, out List<string> sessionFiles, out string warning)
        {
            sessionFiles = new List<string>();
            warning = string.Empty;

            if (File.Exists(resolvedPath))
            {
                if (string.Equals(Path.GetExtension(resolvedPath), ".jsonl", StringComparison.OrdinalIgnoreCase))
                {
                    sessionFiles.Add(resolvedPath);
                    return true;
                }

                warning = "入力がファイルの場合は .jsonl を指定してください。";
                return false;
            }

            if (!Directory.Exists(resolvedPath))
            {
                warning = "入力ディレクトリが見つかりません: " + resolvedPath;
                return false;
            }

            try
            {
                if (targetKind == TargetKind.ChatSessionsDirectory)
                {
                    sessionFiles.AddRange(Directory.GetFiles(resolvedPath, "*.jsonl", SearchOption.TopDirectoryOnly));
                }
                else
                {
                    foreach (string directory in Directory.GetDirectories(resolvedPath, "*", SearchOption.AllDirectories))
                    {
                        string leafName = new DirectoryInfo(directory).Name;
                        if (!leafName.Equals("chatSessions", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        sessionFiles.AddRange(Directory.GetFiles(directory, "*.jsonl", SearchOption.TopDirectoryOnly));
                    }
                }
            }
            catch (Exception ex)
            {
                warning = "JSONL の列挙に失敗しました: " + ex.Message;
                return false;
            }

            return true;
        }
    }
}