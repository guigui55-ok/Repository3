using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace CopilotHistorySave
{
    /// <summary>
    /// chatSessions/*.jsonl を解析する。
    /// 実データ調査により、各行は次のいずれかであることを確認済み。
    /// kind 0: セッション全体のスナップショット（先頭行に1回）。
    /// kind 1: パス k が指すプロパティを値 v へ置き換える。
    /// kind 2: パス k が指す配列の末尾へ、v に含まれる要素を追記する。
    /// </summary>
    internal static class ChatSessionJsonlParser
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        internal static bool TryParseSessionFile(string sessionFilePath, out ChatSessionRecord record, out List<string> warnings)
        {
            record = new ChatSessionRecord { SourceFilePath = sessionFilePath };
            warnings = new List<string>();

            if (!File.Exists(sessionFilePath))
            {
                warnings.Add("ファイルが見つかりません: " + sessionFilePath);
                return false;
            }

            IDictionary<string, object> document = null;

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

                        ApplyLine(ref document, line, sessionFilePath, lineNumber, warnings);
                    }
                }
            }
            catch (Exception ex)
            {
                warnings.Add("ファイル読み取り失敗: " + Path.GetFileName(sessionFilePath) + " - " + ex.Message);
                return false;
            }

            if (document == null)
            {
                warnings.Add("スナップショット(kind:0)が見つかりませんでした: " + Path.GetFileName(sessionFilePath));
                return false;
            }

            FillRecordFromDocument(record, document, sessionFilePath, warnings);
            return true;
        }

        private static void ApplyLine(ref IDictionary<string, object> document, string line, string sessionFilePath, int lineNumber, List<string> warnings)
        {
            object parsedLine;
            try
            {
                parsedLine = Serializer.DeserializeObject(line);
            }
            catch (Exception ex)
            {
                warnings.Add("JSON パース失敗: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber + " - " + ex.Message);
                return;
            }

            IDictionary<string, object> lineMap = parsedLine as IDictionary<string, object>;
            if (lineMap == null)
            {
                warnings.Add("未知のレコード形式: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            int kind = GetInt(lineMap, "kind");
            switch (kind)
            {
                case 0:
                    document = GetDictionary(lineMap, "v");
                    if (document != null)
                    {
                        NormalizeArrays(document);
                    }
                    else
                    {
                        warnings.Add("スナップショットの内容を解析できませんでした: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                    }

                    return;

                case 1:
                    ApplySet(document, lineMap, sessionFilePath, lineNumber, warnings);
                    return;

                case 2:
                    ApplyAppend(document, lineMap, sessionFilePath, lineNumber, warnings);
                    return;

                default:
                    warnings.Add("未知の kind 値(" + kind + "): " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                    return;
            }
        }

        private static void ApplySet(IDictionary<string, object> document, IDictionary<string, object> lineMap, string sessionFilePath, int lineNumber, List<string> warnings)
        {
            if (document == null)
            {
                warnings.Add("スナップショット前の更新行を無視しました: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            IList path = GetList(lineMap, "k");
            if (path == null || path.Count == 0)
            {
                warnings.Add("更新パスを解析できませんでした: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            object value;
            lineMap.TryGetValue("v", out value);
            value = NormalizeArrays(value);

            string navigationError;
            object parent = NavigateOrCreate(document, path, path.Count - 1, false, out navigationError);
            if (parent == null)
            {
                warnings.Add("更新先が見つかりませんでした(" + navigationError + "): " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            SetLeafValue(parent, path[path.Count - 1], value);
        }

        private static void ApplyAppend(IDictionary<string, object> document, IDictionary<string, object> lineMap, string sessionFilePath, int lineNumber, List<string> warnings)
        {
            if (document == null)
            {
                warnings.Add("スナップショット前の追記行を無視しました: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            IList path = GetList(lineMap, "k");
            if (path == null || path.Count == 0)
            {
                warnings.Add("追記パスを解析できませんでした: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            object itemsValue;
            lineMap.TryGetValue("v", out itemsValue);
            IEnumerable itemsToAppend = itemsValue as IEnumerable;
            if (itemsToAppend == null || itemsValue is string)
            {
                warnings.Add("追記対象が配列ではありませんでした: " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            string navigationError;
            object arrayNode = NavigateOrCreate(document, path, path.Count, true, out navigationError);
            IList targetList = arrayNode as IList;
            if (targetList == null)
            {
                warnings.Add("追記先が配列ではありませんでした(" + navigationError + "): " + Path.GetFileName(sessionFilePath) + ":" + lineNumber);
                return;
            }

            foreach (object item in itemsToAppend)
            {
                targetList.Add(NormalizeArrays(item));
            }
        }

        /// <summary>
        /// path の先頭 count 要素をたどり、そこにあるコンテナ(Dictionary または IList)を返す。
        /// 途中のプロパティが存在しない場合は、Dictionary(または createArrayLeaf 指定時は末尾のみ ArrayList)を作成して補う。
        /// </summary>
        private static object NavigateOrCreate(IDictionary<string, object> document, IList path, int count, bool createArrayLeaf, out string error)
        {
            object current = document;
            for (int i = 0; i < count; i++)
            {
                object segment = path[i];
                bool isLeaf = i == count - 1;

                int index;
                if (TryAsIndex(segment, out index))
                {
                    IList list = current as IList;
                    if (list == null || index < 0 || index >= list.Count)
                    {
                        error = "配列インデックス " + index + " が見つかりません";
                        return null;
                    }

                    current = list[index];
                    continue;
                }

                string key = Convert.ToString(segment, CultureInfo.InvariantCulture);
                IDictionary<string, object> dict = current as IDictionary<string, object>;
                if (dict == null)
                {
                    error = "プロパティ " + key + " の親がオブジェクトではありません";
                    return null;
                }

                object next;
                if (!dict.TryGetValue(key, out next) || next == null)
                {
                    next = isLeaf && createArrayLeaf ? (object)new List<object>() : new Dictionary<string, object>();
                    dict[key] = next;
                }

                current = next;
            }

            error = null;
            return current;
        }

        private static void SetLeafValue(object parent, object lastSegment, object value)
        {
            int index;
            if (TryAsIndex(lastSegment, out index))
            {
                IList list = parent as IList;
                if (list != null && index >= 0 && index < list.Count)
                {
                    list[index] = value;
                }

                return;
            }

            IDictionary<string, object> dict = parent as IDictionary<string, object>;
            if (dict != null)
            {
                dict[Convert.ToString(lastSegment, CultureInfo.InvariantCulture)] = value;
            }
        }

        private static bool TryAsIndex(object segment, out int index)
        {
            index = 0;
            if (segment is int)
            {
                index = (int)segment;
                return true;
            }

            if (segment is long)
            {
                index = (int)(long)segment;
                return true;
            }

            if (segment is double)
            {
                double d = (double)segment;
                index = (int)d;
                return d == index;
            }

            return false;
        }

        private static void FillRecordFromDocument(ChatSessionRecord record, IDictionary<string, object> document, string sessionFilePath, List<string> warnings)
        {
            record.SessionId = GetString(document, "sessionId");
            record.Title = GetString(document, "customTitle");
            record.CreationDate = GetUnixMillis(document, "creationDate");

            IList requests = GetList(document, "requests");
            if (requests == null)
            {
                warnings.Add("requests 配列が見つかりませんでした: " + Path.GetFileName(sessionFilePath));
                return;
            }

            foreach (object requestObject in requests)
            {
                IDictionary<string, object> requestMap = requestObject as IDictionary<string, object>;
                if (requestMap == null)
                {
                    warnings.Add("requests 内に未知の要素があります: " + Path.GetFileName(sessionFilePath));
                    continue;
                }

                record.Exchanges.Add(BuildExchange(requestMap));
            }

            record.LastUpdatedDate = ComputeLastUpdatedDate(record);
        }

        private static ChatExchange BuildExchange(IDictionary<string, object> requestMap)
        {
            ChatExchange exchange = new ChatExchange();
            exchange.RequestId = GetString(requestMap, "requestId");
            exchange.QuestionTimestamp = GetUnixMillis(requestMap, "timestamp");
            exchange.QuestionText = GetMessageText(requestMap);
            exchange.AnswerTimestamp = GetUnixMillis(requestMap, "responseTimestamp") ?? exchange.QuestionTimestamp;
            exchange.AnswerText = ExtractAnswerText(requestMap);
            return exchange;
        }

        private static DateTime? ComputeLastUpdatedDate(ChatSessionRecord record)
        {
            DateTime? latest = record.CreationDate;
            foreach (ChatExchange exchange in record.Exchanges)
            {
                if (exchange.AnswerTimestamp.HasValue && (!latest.HasValue || exchange.AnswerTimestamp.Value > latest.Value))
                {
                    latest = exchange.AnswerTimestamp;
                }

                if (exchange.QuestionTimestamp.HasValue && (!latest.HasValue || exchange.QuestionTimestamp.Value > latest.Value))
                {
                    latest = exchange.QuestionTimestamp;
                }
            }

            return latest;
        }

        private static string GetMessageText(IDictionary<string, object> requestMap)
        {
            IDictionary<string, object> message = GetDictionary(requestMap, "message");
            return message == null ? string.Empty : GetString(message, "text");
        }

        /// <summary>
        /// response 配列から、実際の回答本文らしき断片を抽出して結合する。
        /// 実データ確認の結果、kind を持たず value(文字列) を持つ要素が回答本文であることを確認済み。
        /// mcpServersStarting / autoModeResolution / thinking / toolInvocationSerialized などの
        /// 制御用要素は kind を持つため除外する。
        /// </summary>
        private static string ExtractAnswerText(IDictionary<string, object> requestMap)
        {
            IList response = GetList(requestMap, "response");
            if (response == null)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            foreach (object item in response)
            {
                IDictionary<string, object> map = item as IDictionary<string, object>;
                if (map == null || map.ContainsKey("kind"))
                {
                    continue;
                }

                object valueObject;
                string text = map.TryGetValue("value", out valueObject) ? valueObject as string : null;
                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }

                if (builder.Length > 0)
                {
                    builder.Append("\n\n");
                }

                builder.Append(text);
            }

            return builder.ToString();
        }

        /// <summary>
        /// JavaScriptSerializer は JSON 配列を固定長の object[] として返すため、
        /// 追記(kind:2)で要素を追加できるよう List&lt;object&gt; へ変換する。
        /// </summary>
        private static object NormalizeArrays(object node)
        {
            IDictionary<string, object> dict = node as IDictionary<string, object>;
            if (dict != null)
            {
                List<string> keys = new List<string>(dict.Keys);
                foreach (string key in keys)
                {
                    dict[key] = NormalizeArrays(dict[key]);
                }

                return dict;
            }

            object[] array = node as object[];
            if (array != null)
            {
                List<object> list = new List<object>(array.Length);
                foreach (object item in array)
                {
                    list.Add(NormalizeArrays(item));
                }

                return list;
            }

            return node;
        }

        private static int GetInt(IDictionary<string, object> map, string key)
        {
            object value;
            if (!map.TryGetValue(key, out value) || value == null)
            {
                return -1;
            }

            try
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return -1;
            }
        }

        private static string GetString(IDictionary<string, object> map, string key)
        {
            object value;
            if (map == null || !map.TryGetValue(key, out value) || value == null)
            {
                return string.Empty;
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        private static DateTime? GetUnixMillis(IDictionary<string, object> map, string key)
        {
            object value;
            if (map == null || !map.TryGetValue(key, out value) || value == null)
            {
                return null;
            }

            try
            {
                long milliseconds = Convert.ToInt64(value, CultureInfo.InvariantCulture);
                return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).LocalDateTime;
            }
            catch
            {
                return null;
            }
        }

        private static IDictionary<string, object> GetDictionary(IDictionary<string, object> map, string key)
        {
            object value;
            if (map == null || !map.TryGetValue(key, out value))
            {
                return null;
            }

            return value as IDictionary<string, object>;
        }

        private static IList GetList(IDictionary<string, object> map, string key)
        {
            object value;
            if (map == null || !map.TryGetValue(key, out value))
            {
                return null;
            }

            return value as IList;
        }
    }
}
