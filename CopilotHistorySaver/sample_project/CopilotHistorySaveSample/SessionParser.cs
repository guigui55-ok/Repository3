using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace CopilotHistorySaveSample
{
    internal sealed class SessionFileSummary
    {
        internal string FilePath { get; set; }

        internal string SessionId { get; set; }

        internal string CustomTitle { get; set; }

        internal DateTime? CreationDate { get; set; }

        internal string InputText { get; set; }

        internal List<RequestSummary> Requests { get; set; }

        internal List<string> MatchedMarkers { get; set; }

        internal SessionFileSummary()
        {
            Requests = new List<RequestSummary>();
            MatchedMarkers = new List<string>();
        }
    }

    internal sealed class RequestSummary
    {
        internal string RequestId { get; set; }

        internal DateTime? Timestamp { get; set; }

        internal string ResponsePreview { get; set; }
    }

    internal static class SessionParser
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        internal static bool TryParseSessionFile(string sessionFilePath, out SessionFileSummary summary, out List<string> warnings)
        {
            summary = new SessionFileSummary();
            warnings = new List<string>();
            summary.FilePath = sessionFilePath;

            if (!File.Exists(sessionFilePath))
            {
                warnings.Add("ファイルが見つかりません: " + sessionFilePath);
                return false;
            }

            try
            {
                using (StreamReader reader = new StreamReader(sessionFilePath, Encoding.UTF8, true))
                {
                    string line;
                    int lineNumber = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        TrackKnownMarkers(line, summary);

                        object parsedLine;
                        try
                        {
                            parsedLine = Serializer.DeserializeObject(line);
                        }
                        catch (Exception ex)
                        {
                            warnings.Add("JSON パース失敗: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber + " - " + ex.Message);
                            continue;
                        }

                        IDictionary<string, object> lineMap = parsedLine as IDictionary<string, object>;
                        if (lineMap == null)
                        {
                            continue;
                        }

                        int kind = GetInt(lineMap, "kind");
                        if (kind == 0)
                        {
                            ApplySnapshot(summary, GetDictionary(lineMap, "v"));
                            continue;
                        }

                        if (kind == 1)
                        {
                            ApplyUpdate(summary, lineMap);
                            continue;
                        }

                        if (kind == 2)
                        {
                            object requestUpdate = null;
                            lineMap.TryGetValue("v", out requestUpdate);
                            ApplyRequests(summary, requestUpdate, warnings, sessionFilePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                warnings.Add("ファイル読み取り失敗: " + ex.Message);
                return false;
            }

            return true;
        }

        private static void ApplySnapshot(SessionFileSummary summary, IDictionary<string, object> snapshot)
        {
            if (snapshot == null)
            {
                return;
            }

            summary.SessionId = GetString(snapshot, "sessionId", summary.SessionId);
            summary.CustomTitle = GetString(snapshot, "customTitle", summary.CustomTitle);
            summary.CreationDate = GetDateTime(snapshot, "creationDate", summary.CreationDate);

            object requestsValue;
            if (snapshot.TryGetValue("requests", out requestsValue))
            {
                summary.Requests = ParseRequests(requestsValue);
            }

            object inputStateValue;
            if (snapshot.TryGetValue("inputState", out inputStateValue))
            {
                summary.InputText = GetNestedString(inputStateValue, "inputText", summary.InputText);
            }
        }

        private static void ApplyUpdate(SessionFileSummary summary, IDictionary<string, object> lineMap)
        {
            IList<object> path = GetList(lineMap, "k");
            if (path == null || path.Count == 0)
            {
                return;
            }

            object value = null;
            lineMap.TryGetValue("v", out value);

            string joinedPath = string.Join("/", path.Select(Convert.ToString));
            if (string.Equals(joinedPath, "customTitle", StringComparison.OrdinalIgnoreCase))
            {
                summary.CustomTitle = Convert.ToString(value);
                return;
            }

            if (string.Equals(joinedPath, "inputState/inputText", StringComparison.OrdinalIgnoreCase))
            {
                summary.InputText = Convert.ToString(value);
            }
        }

        private static void ApplyRequests(SessionFileSummary summary, object requestContainer, List<string> warnings, string sessionFilePath)
        {
            if (requestContainer == null)
            {
                return;
            }

            object requestsValue = requestContainer;
            IDictionary<string, object> map = requestContainer as IDictionary<string, object>;
            if (map != null)
            {
                object nested;
                if (!map.TryGetValue("requests", out nested))
                {
                    return;
                }

                requestsValue = nested;
            }

            summary.Requests = ParseRequests(requestsValue);
            if (summary.Requests.Count == 0)
            {
                warnings.Add("requests 配列を解析できませんでした: " + Path.GetFileName(sessionFilePath));
            }
        }

        private static List<RequestSummary> ParseRequests(object requestsValue)
        {
            List<RequestSummary> requests = new List<RequestSummary>();
            IEnumerable enumerable = requestsValue as IEnumerable;
            if (enumerable == null)
            {
                return requests;
            }

            foreach (object requestObject in enumerable)
            {
                IDictionary<string, object> requestMap = requestObject as IDictionary<string, object>;
                if (requestMap == null)
                {
                    continue;
                }

                RequestSummary request = new RequestSummary();
                request.RequestId = GetString(requestMap, "requestId", request.RequestId);
                request.Timestamp = GetTimestamp(requestMap, "timestamp", request.Timestamp);

                object responseValue;
                if (requestMap.TryGetValue("response", out responseValue))
                {
                    request.ResponsePreview = ExtractPreviewText(responseValue);
                }

                if (string.IsNullOrWhiteSpace(request.ResponsePreview))
                {
                    object resultValue;
                    if (requestMap.TryGetValue("result", out resultValue))
                    {
                        request.ResponsePreview = ExtractPreviewText(resultValue);
                    }
                }

                requests.Add(request);
            }

            return requests;
        }

        private static void TrackKnownMarkers(string line, SessionFileSummary summary)
        {
            foreach (string marker in KnownMarkerFinder.FindAll(line))
            {
                if (!summary.MatchedMarkers.Contains(marker))
                {
                    summary.MatchedMarkers.Add(marker);
                }
            }
        }

        private static int GetInt(IDictionary<string, object> map, string key)
        {
            object value;
            if (!map.TryGetValue(key, out value) || value == null)
            {
                return 0;
            }

            try
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }

        private static string GetString(IDictionary<string, object> map, string key, string fallback)
        {
            object value;
            if (!map.TryGetValue(key, out value) || value == null)
            {
                return fallback;
            }

            string text = Convert.ToString(value, CultureInfo.InvariantCulture);
            return string.IsNullOrWhiteSpace(text) ? fallback : text;
        }

        private static DateTime? GetDateTime(IDictionary<string, object> map, string key, DateTime? fallback)
        {
            object value;
            if (!map.TryGetValue(key, out value) || value == null)
            {
                return fallback;
            }

            try
            {
                long milliseconds = Convert.ToInt64(value, CultureInfo.InvariantCulture);
                return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).LocalDateTime;
            }
            catch
            {
                return fallback;
            }
        }

        private static DateTime? GetTimestamp(IDictionary<string, object> map, string key, DateTime? fallback)
        {
            object value;
            if (!map.TryGetValue(key, out value) || value == null)
            {
                return fallback;
            }

            try
            {
                if (value is string)
                {
                    return DateTime.Parse(Convert.ToString(value, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
                }

                long milliseconds = Convert.ToInt64(value, CultureInfo.InvariantCulture);
                return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).LocalDateTime;
            }
            catch
            {
                return fallback;
            }
        }

        private static IDictionary<string, object> GetDictionary(IDictionary<string, object> map, string key)
        {
            object value;
            if (!map.TryGetValue(key, out value))
            {
                return null;
            }

            return value as IDictionary<string, object>;
        }

        private static IList<object> GetList(IDictionary<string, object> map, string key)
        {
            object value;
            if (!map.TryGetValue(key, out value))
            {
                return null;
            }

            return value as IList<object>;
        }

        private static string GetNestedString(object value, string key, string fallback)
        {
            IDictionary<string, object> map = value as IDictionary<string, object>;
            if (map == null)
            {
                return fallback;
            }

            return GetString(map, key, fallback);
        }

        private static string ExtractPreviewText(object value)
        {
            string markerHit = FindMarkerText(value);
            if (!string.IsNullOrWhiteSpace(markerHit))
            {
                return markerHit;
            }

            string preview = FindReadableText(value);
            if (string.IsNullOrWhiteSpace(preview))
            {
                return string.Empty;
            }

            return preview.Length > 320 ? preview.Substring(0, 320) : preview;
        }

        private static string FindMarkerText(object value)
        {
            foreach (string text in EnumerateStrings(value))
            {
                if (text.IndexOf("TEST_HISTORY_", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return text;
                }
            }

            return string.Empty;
        }

        private static string FindReadableText(object value)
        {
            foreach (string text in EnumerateStrings(value))
            {
                if (!string.IsNullOrWhiteSpace(text) && text.Length > 10)
                {
                    return text.Trim();
                }
            }

            return string.Empty;
        }

        private static IEnumerable<string> EnumerateStrings(object value)
        {
            if (value == null)
            {
                yield break;
            }

            string text = value as string;
            if (text != null)
            {
                yield return text;
                yield break;
            }

            IDictionary<string, object> map = value as IDictionary<string, object>;
            if (map != null)
            {
                string[] preferredKeys = { "content", "text", "value", "inputText", "customTitle", "message" };
                foreach (string preferredKey in preferredKeys)
                {
                    object preferredValue;
                    if (map.TryGetValue(preferredKey, out preferredValue))
                    {
                        foreach (string nested in EnumerateStrings(preferredValue))
                        {
                            yield return nested;
                        }
                    }
                }

                foreach (KeyValuePair<string, object> entry in map)
                {
                    if (Array.IndexOf(preferredKeys, entry.Key) >= 0)
                    {
                        continue;
                    }

                    foreach (string nested in EnumerateStrings(entry.Value))
                    {
                        yield return nested;
                    }
                }

                yield break;
            }

            IEnumerable enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                foreach (object item in enumerable)
                {
                    foreach (string nested in EnumerateStrings(item))
                    {
                        yield return nested;
                    }
                }
            }
        }
    }

    internal static class KnownMarkerFinder
    {
        internal static IEnumerable<string> FindAll(string text)
        {
            const string prefix = "TEST_HISTORY_";
            int index = 0;
            while (!string.IsNullOrEmpty(text))
            {
                index = text.IndexOf(prefix, index, StringComparison.OrdinalIgnoreCase);
                if (index < 0)
                {
                    yield break;
                }

                int end = index + prefix.Length;
                while (end < text.Length)
                {
                    char ch = text[end];
                    if (char.IsLetterOrDigit(ch) || ch == '_')
                    {
                        end++;
                        continue;
                    }

                    break;
                }

                yield return text.Substring(index, end - index);
                index = end;
            }
        }
    }
}