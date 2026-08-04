using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace CopilotHistorySave
{
    /// <summary>
    /// 解析済みセッションを、ワークスペースごとのフォルダへ Markdown ファイルとして保存する。
    /// 同じセッションを再実行した場合は、常に現在の JSONL の内容で上書きするため、
    /// 不要な重複は発生しない。
    /// </summary>
    internal static class MarkdownSessionWriter
    {
        internal static bool TryWriteSession(string outputRoot, ChatSessionRecord record, out string writtenFilePath, out string warning)
        {
            writtenFilePath = string.Empty;
            warning = string.Empty;

            string workspaceFolderName = BuildWorkspaceFolderName(record);
            string sessionFileName = BuildSessionFileName(record);

            string targetDirectory = Path.Combine(outputRoot, workspaceFolderName);

            try
            {
                Directory.CreateDirectory(targetDirectory);
            }
            catch (Exception ex)
            {
                warning = "出力フォルダを作成できませんでした(" + targetDirectory + "): " + ex.Message;
                return false;
            }

            string targetFilePath = Path.Combine(targetDirectory, sessionFileName);
            string content = BuildMarkdown(record);

            try
            {
                File.WriteAllText(targetFilePath, content, new UTF8Encoding(false));
            }
            catch (Exception ex)
            {
                warning = "出力ファイルへ書き込めませんでした(" + targetFilePath + "): " + ex.Message;
                return false;
            }

            writtenFilePath = targetFilePath;
            return true;
        }

        private static string BuildWorkspaceFolderName(ChatSessionRecord record)
        {
            string basis = !string.IsNullOrWhiteSpace(record.WorkspaceFolderPath)
                ? record.WorkspaceFolderPath
                : "unknown_workspace_" + record.StorageHash;

            return SanitizeForFileSystem(basis);
        }

        private static string BuildSessionFileName(ChatSessionRecord record)
        {
            string sessionId = !string.IsNullOrWhiteSpace(record.SessionId) ? record.SessionId : Path.GetFileNameWithoutExtension(record.SourceFilePath);
            return SanitizeForFileSystem(sessionId) + ".md";
        }

        private static string SanitizeForFileSystem(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "unknown";
            }

            StringBuilder builder = new StringBuilder(value.Length);
            foreach (char ch in value)
            {
                bool isInvalid = Array.IndexOf(Path.GetInvalidFileNameChars(), ch) >= 0
                    || ch == '\\' || ch == '/' || ch == ':';
                builder.Append(isInvalid ? '_' : ch);
            }

            string sanitized = builder.ToString().Trim('_', ' ');
            return string.IsNullOrEmpty(sanitized) ? "unknown" : sanitized;
        }

        private static string BuildMarkdown(ChatSessionRecord record)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("# " + ValueOrPlaceholder(record.Title));
            builder.AppendLine();
            builder.AppendLine("- セッションID: " + ValueOrPlaceholder(record.SessionId));
            builder.AppendLine("- ワークスペース: " + ValueOrPlaceholder(record.WorkspaceFolderPath ?? record.StorageHash));
            builder.AppendLine("- 作成日時: " + FormatDate(record.CreationDate));
            builder.AppendLine("- 最終更新日時: " + FormatDate(record.LastUpdatedDate));
            builder.AppendLine("- 質問数: " + record.QuestionCount.ToString(CultureInfo.InvariantCulture));
            builder.AppendLine("- 取得元ファイル: " + record.SourceFilePath);
            builder.AppendLine();

            int index = 0;
            foreach (ChatExchange exchange in record.Exchanges)
            {
                index++;
                builder.AppendLine("## 質問 " + index.ToString(CultureInfo.InvariantCulture));
                builder.AppendLine();
                builder.AppendLine("- 質問日時: " + FormatDate(exchange.QuestionTimestamp));
                builder.AppendLine("- 回答日時: " + FormatDate(exchange.AnswerTimestamp));
                builder.AppendLine();
                builder.AppendLine("### User");
                builder.AppendLine();
                builder.AppendLine(ValueOrPlaceholder(exchange.QuestionText));
                builder.AppendLine();
                builder.AppendLine("### GitHub Copilot");
                builder.AppendLine();
                builder.AppendLine(ValueOrPlaceholder(exchange.AnswerText));
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static string ValueOrPlaceholder(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "(none)" : value;
        }

        private static string FormatDate(DateTime? value)
        {
            return value.HasValue ? value.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) : "(none)";
        }
    }
}
