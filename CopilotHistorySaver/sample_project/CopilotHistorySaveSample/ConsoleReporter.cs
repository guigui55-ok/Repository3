using System;
using System.Collections.Generic;
using System.Globalization;

namespace CopilotHistorySaveSample
{
    internal static class ConsoleReporter
    {
        internal static void PrintUsage()
        {
            Console.WriteLine("使用方法: CopilotHistorySaveSample.exe <workspaceStorage ルート | chatSessions フォルダ | .jsonl ファイル>");
        }

        internal static void PrintWarning(string message)
        {
            Console.WriteLine("[WARN] " + message);
        }

        internal static void PrintTarget(string resolvedPath, TargetKind targetKind, int fileCount)
        {
            Console.WriteLine("対象: " + resolvedPath);
            Console.WriteLine("種別: " + targetKind);
            Console.WriteLine("対象ファイル数: " + fileCount);
            Console.WriteLine();
        }

        internal static void PrintSessionSummary(SessionFileSummary summary, List<string> warnings)
        {
            Console.WriteLine("=== " + summary.FilePath + " ===");
            Console.WriteLine("SessionId: " + ValueOrPlaceholder(summary.SessionId));
            Console.WriteLine("Title: " + ValueOrPlaceholder(summary.CustomTitle));
            Console.WriteLine("CreationDate: " + FormatDate(summary.CreationDate));
            Console.WriteLine("InputText: " + FirstLine(summary.InputText));
            Console.WriteLine("RequestCount: " + summary.Requests.Count);

            if (summary.MatchedMarkers.Count > 0)
            {
                Console.WriteLine("KnownMarkers: " + string.Join(", ", summary.MatchedMarkers));
            }

            foreach (RequestSummary request in summary.Requests)
            {
                Console.WriteLine("- RequestId: " + ValueOrPlaceholder(request.RequestId));
                Console.WriteLine("  Timestamp: " + FormatDate(request.Timestamp));
                Console.WriteLine("  Response: " + FirstLine(request.ResponsePreview));
            }

            foreach (string warning in warnings)
            {
                Console.WriteLine("[WARN] " + warning);
            }

            Console.WriteLine();
        }

        internal static void PrintFooter(int processedCount, int warningCount)
        {
            Console.WriteLine("処理済みファイル数: " + processedCount);
            Console.WriteLine("警告数: " + warningCount);
        }

        private static string ValueOrPlaceholder(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "(none)" : value;
        }

        private static string FormatDate(DateTime? value)
        {
            return value.HasValue ? value.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) : "(none)";
        }

        private static string FirstLine(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "(none)";
            }

            int lineBreak = value.IndexOfAny(new[] { '\r', '\n' });
            string firstLine = lineBreak >= 0 ? value.Substring(0, lineBreak) : value;
            if (firstLine.Length > 160)
            {
                firstLine = firstLine.Substring(0, 160);
            }

            return firstLine;
        }
    }
}