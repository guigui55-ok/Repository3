using System;
using System.Collections.Generic;

namespace TempFileNameTester
{
    public static class CommandLineParser
    {
        public static AppConfig Parse(string[] args)
        {
            AppConfig config = new AppConfig();

            Dictionary<string, string> map = ToDictionary(args);

            config.Mode = GetString(map, "--mode", config.Mode);
            config.ProcessCount = GetInt(map, "--process-count", config.ProcessCount);
            config.ThreadCount = GetInt(map, "--thread-count", config.ThreadCount);
            config.LoopCount = GetInt(map, "--loop-count", config.LoopCount);
            config.WaitMs = GetInt(map, "--wait-ms", config.WaitMs);

            config.WorkerIndex = GetInt(map, "--worker-index", config.WorkerIndex);
            config.RunId = GetString(map, "--run-id", config.RunId);

            config.WorkDir = GetString(map, "--work-dir", config.WorkDir);
            config.TempDir = GetString(map, "--temp-dir", config.TempDir);

            config.DeleteCreatedFiles = GetBool(map, "--delete-created-files", config.DeleteCreatedFiles);
            config.LogMode = GetString(map, "--log-mode", config.LogMode);

            if (string.IsNullOrEmpty(config.RunId))
            {
                config.RunId = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            }

            return config;
        }

        private static Dictionary<string, string> ToDictionary(string[] args)
        {
            Dictionary<string, string> map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < args.Length; i++)
            {
                string key = args[i];

                if (!key.StartsWith("--"))
                {
                    continue;
                }

                string value = "";

                if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
                {
                    value = args[i + 1];
                    i++;
                }

                map[key] = value;
            }

            return map;
        }

        private static string GetString(Dictionary<string, string> map, string key, string defaultValue)
        {
            if (map.ContainsKey(key))
            {
                return map[key];
            }

            return defaultValue;
        }

        private static int GetInt(Dictionary<string, string> map, string key, int defaultValue)
        {
            if (!map.ContainsKey(key))
            {
                return defaultValue;
            }

            int value;
            if (int.TryParse(map[key], out value))
            {
                return value;
            }

            return defaultValue;
        }

        private static bool GetBool(Dictionary<string, string> map, string key, bool defaultValue)
        {
            if (!map.ContainsKey(key))
            {
                return defaultValue;
            }

            bool value;
            if (bool.TryParse(map[key], out value))
            {
                return value;
            }

            return defaultValue;
        }
    }
}