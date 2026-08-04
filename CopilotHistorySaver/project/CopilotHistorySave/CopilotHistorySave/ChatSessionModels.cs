using System;
using System.Collections.Generic;

namespace CopilotHistorySave
{
    /// <summary>
    /// 1つの workspaceStorage 配下（1ワークスペース分）の走査対象を表す。
    /// </summary>
    internal sealed class WorkspaceTarget
    {
        internal string StorageHash { get; set; }

        /// <summary>
        /// workspace.json から解決した実際のワークスペースフォルダ。解決できない場合は null。
        /// </summary>
        internal string WorkspaceFolderPath { get; set; }

        internal List<string> SessionFilePaths { get; set; }

        internal WorkspaceTarget()
        {
            SessionFilePaths = new List<string>();
        }
    }

    /// <summary>
    /// 1件の chatSessions/*.jsonl を解析した結果。
    /// </summary>
    internal sealed class ChatSessionRecord
    {
        internal string SourceFilePath { get; set; }

        internal string WorkspaceFolderPath { get; set; }

        internal string StorageHash { get; set; }

        internal string SessionId { get; set; }

        internal string Title { get; set; }

        internal DateTime? CreationDate { get; set; }

        internal DateTime? LastUpdatedDate { get; set; }

        internal List<ChatExchange> Exchanges { get; set; }

        internal ChatSessionRecord()
        {
            Exchanges = new List<ChatExchange>();
        }

        internal int QuestionCount
        {
            get { return Exchanges.Count; }
        }
    }

    /// <summary>
    /// 1回の質問と、それに対応する回答。
    /// </summary>
    internal sealed class ChatExchange
    {
        internal string RequestId { get; set; }

        internal DateTime? QuestionTimestamp { get; set; }

        internal string QuestionText { get; set; }

        internal DateTime? AnswerTimestamp { get; set; }

        internal string AnswerText { get; set; }
    }
}
