using System;
using System.Collections.Generic;
using System.IO;

namespace CopilotHistorySave
{
    internal class Program
    {
        private static int Main(string[] args)
        {
#if DEBUG

// デバッグ用：引数を手動設定する
// 引数
/*
 * 使用方法: CopilotHistorySave.exe <入力パス> <出力先フォルダ>
入力パス: workspaceStorage ルート、個別ワークスペースのハッシュフォルダ、chatSessions フォルダ、または単一の .jsonl ファイル
出力先フォルダ: セッションを Markdown として保存する先のフォルダ
*/
string exeDirPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
args = new string[]
{
    @"D:\git\Repository3\CopilotHistorySaver\docs\descovery\動作確認フォルダ_all",
    Path.Combine(exeDirPath, DateTime.Now.ToString("yyyyMMdd_HHmmss"))
};

#endif


AppOptions options;
string optionError;
if (!AppOptions.TryParse(args, out options, out optionError))
{
    ConsoleReporter.PrintError(optionError);
    ConsoleReporter.PrintUsage();
    return 1;
}

List<WorkspaceTarget> targets;
string resolveWarning;
if (!WorkspaceStorageScanner.TryResolveTargets(options.InputPath, out targets, out resolveWarning))
{
    ConsoleReporter.PrintError(resolveWarning);
    return 1;
}

if (targets.Count == 0)
{
    ConsoleReporter.PrintWarning("対象の chatSessions が見つかりませんでした: " + options.InputPath);
    return 1;
}

ConsoleReporter.PrintTargets(targets);

int savedCount;
int warningCount;
ProcessTargets(targets, options.OutputPath, out savedCount, out warningCount);

ConsoleReporter.PrintFooter(savedCount, warningCount);
return 0;
}

/// <summary>
/// 各ワークスペースの各セッションファイルを解析し、Markdown として保存する。
/// 個別ファイルの失敗は警告として記録し、他のファイルの処理は継続する。
/// </summary>
private static void ProcessTargets(List<WorkspaceTarget> targets, string outputPath, out int savedCount, out int warningCount)
{
savedCount = 0;
warningCount = 0;

foreach (WorkspaceTarget target in targets)
{
    foreach (string sessionFilePath in target.SessionFilePaths)
    {
        ChatSessionRecord record;
        List<string> parseWarnings;
        if (!ChatSessionJsonlParser.TryParseSessionFile(sessionFilePath, out record, out parseWarnings))
        {
            warningCount += parseWarnings.Count > 0 ? parseWarnings.Count : 1;
            ConsoleReporter.PrintWarning("解析に失敗しました: " + sessionFilePath);
            PrintWarnings(parseWarnings);
            continue;
        }

        warningCount += parseWarnings.Count;
        PrintWarnings(parseWarnings);

        record.WorkspaceFolderPath = target.WorkspaceFolderPath;
        record.StorageHash = target.StorageHash;

        string writtenFilePath;
        string writeWarning;
        if (!MarkdownSessionWriter.TryWriteSession(outputPath, record, out writtenFilePath, out writeWarning))
        {
            warningCount++;
            ConsoleReporter.PrintWarning(writeWarning);
            continue;
        }

        savedCount++;
        ConsoleReporter.PrintSessionSaved(record, writtenFilePath);
    }
}
}

private static void PrintWarnings(List<string> warnings)
{
foreach (string warning in warnings)
{
    ConsoleReporter.PrintWarning(warning);
}
}
}
}
