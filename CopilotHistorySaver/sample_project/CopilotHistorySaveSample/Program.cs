using System;
using System.Collections.Generic;

namespace CopilotHistorySaveSample
{
    internal class Program
    {
        private static int Main(string[] args)
        {
#if DEBUG
            args = new[] { 
                @"D:\git\Repository3\CopilotHistorySaver\docs\動作確認フォルダ_all" 
                };
#endif
            if (args == null || args.Length == 0)
            {
                ConsoleReporter.PrintWarning("入力パスを指定してください。");
                ConsoleReporter.PrintUsage();
                return 1;
            }

            string inputPath = args[0];

            if (!HistoryScanner.TryResolveTarget(inputPath, out TargetKind targetKind, out string resolvedPath, out string resolveWarning))
            {
                ConsoleReporter.PrintWarning(resolveWarning);
                return 1;
            }

            if (!HistoryScanner.TryEnumerateSessionFiles(resolvedPath, targetKind, out List<string> sessionFiles, out string enumerateWarning))
            {
                ConsoleReporter.PrintWarning(enumerateWarning);
                return 1;
            }

            if (sessionFiles.Count == 0)
            {
                ConsoleReporter.PrintWarning("対象の chatSessions JSONL が見つかりませんでした。");
                return 1;
            }

            ConsoleReporter.PrintTarget(resolvedPath, targetKind, sessionFiles.Count);

            int processedCount = 0;
            int warningCount = 0;

            foreach (string sessionFile in sessionFiles)
            {
                SessionFileSummary summary;
                List<string> warnings;
                if (!SessionParser.TryParseSessionFile(sessionFile, out summary, out warnings))
                {
                    warningCount++;
                    ConsoleReporter.PrintWarning("解析に失敗しました: " + sessionFile);
                    continue;
                }

                processedCount++;
                warningCount += warnings.Count;
                ConsoleReporter.PrintSessionSummary(summary, warnings);
            }

            ConsoleReporter.PrintFooter(processedCount, warningCount);
            return 0;
        }
    }
}
