using System;
using System.Configuration;

namespace CopilotHistorySave
{
    /// <summary>
    /// コマンドライン引数と App.config の既定値から、実行に必要な入出力パスを決定する。
    /// </summary>
    internal sealed class AppOptions
    {
        internal string InputPath { get; private set; }

        internal string OutputPath { get; private set; }

        internal static bool TryParse(string[] args, out AppOptions options, out string error)
        {
            options = null;
            error = string.Empty;

            string inputPath = (args != null && args.Length >= 1) ? args[0] : ConfigurationManager.AppSettings["InputPath"];
            string outputPath = (args != null && args.Length >= 2) ? args[1] : ConfigurationManager.AppSettings["OutputPath"];

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                error = "入力パス(workspaceStorage ルート、ワークスペースフォルダ、chatSessions フォルダ、または .jsonl ファイル)を指定してください。";
                return false;
            }

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                error = "出力先フォルダを指定してください。";
                return false;
            }

            options = new AppOptions();
            options.InputPath = inputPath;
            options.OutputPath = outputPath;
            return true;
        }
    }
}
