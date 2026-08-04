using System;
using System.Collections.Generic;

namespace CopilotHistorySave
{
    internal static class ConsoleReporter
    {
        internal static void PrintUsage()
        {
            Console.WriteLine("使用方法: CopilotHistorySave.exe <入力パス> <出力先フォルダ>");
            Console.WriteLine("  入力パス: workspaceStorage ルート、個別ワークスペースのハッシュフォルダ、chatSessions フォルダ、または単一の .jsonl ファイル");
            Console.WriteLine("  出力先フォルダ: セッションを Markdown として保存する先のフォルダ");
        }

        internal static void PrintError(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }

        internal static void PrintWarning(string message)
        {
            Console.WriteLine("[WARN] " + message);
        }

        internal static void PrintTargets(IList<WorkspaceTarget> targets)
        {
            Console.WriteLine("対象ワークスペース数: " + targets.Count);
            foreach (WorkspaceTarget target in targets)
            {
                string workspaceLabel = target.WorkspaceFolderPath ?? ("(未解決: " + target.StorageHash + ")");
                Console.WriteLine("  - " + workspaceLabel + " : " + target.SessionFilePaths.Count + " 件");
            }

            Console.WriteLine();
        }

        internal static void PrintSessionSaved(ChatSessionRecord record, string writtenFilePath)
        {
            Console.WriteLine("保存: " + writtenFilePath + " (質問数: " + record.QuestionCount + ")");
        }

        internal static void PrintFooter(int savedCount, int warningCount)
        {
            Console.WriteLine();
            Console.WriteLine("保存済みセッション数: " + savedCount);
            Console.WriteLine("警告数: " + warningCount);
        }
    }
}
