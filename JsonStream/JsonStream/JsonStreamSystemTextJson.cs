using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;


//対象のフレームワークが .NET Framework 4.7.* や4.6のプロジェクトはツリーから[参照]-[参照の追加]で開く参照マネージャー、[アセンブリ]-[拡張]で
//System.Text.Encodings.Web, System.Text.Jsonにチェックを入れます。
//Stream.Memoryも必要
namespace JsonStreamModule
{
    /// <summary>
    /// Json形式データのファイル読み書きをする（System.Text.Jsonを使用）
    /// <para></para>
    /// 240921  受け取りの型とJsonのデータ構造は、専用クラスでしかやり取りできない？
    /// 、動作未確認。
    /// </summary>
    public class JsonStreamSystemTextJson
    {

        /// <summary>
        /// Dictionary<string, int>型の入力をJSON文字列に変換します。
        /// </summary>
        /// <param name="dict">Dictionary<string, int>型の入力</param>
        /// <returns>JSON文字列</returns>
        public string DictToJson(Dictionary<string, int> dict)
        {
            try
            {
                var json = JsonSerializer.Serialize(dict, this.GetOptions());
                return json;
            }
            catch (Exception e)
            {
                ConsoleWriteLineError(e, "DictToJson");
                return string.Empty;
            }
        }

        /// <summary>
        /// Dictionary<string, string>型の入力をJSON文字列に変換し、指定したファイルに書き込みます。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="dict">Dictionary<string, object>型の入力</param>
        public void WriteFile(string path, Dictionary<string, object> data)
        {
            // JSONファイルに書き込むメソッド
            try
            {
                // DictionaryをJSON形式の文字列にシリアライズ
                string jsonString = JsonSerializer.Serialize(data, GetOptions());

                // ファイルに書き込む
                File.WriteAllText(path, jsonString);
            }
            catch (Exception ex)
            {
                ConsoleWriteLineError(ex, "WriteFile");
                throw;
            }
        }

        /// <summary>
        /// 指定したファイルからJSONを読み込み、Dictionary<string, object>型に変換します。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>Dictionary<string, string>型の出力（エラー時は空のディクショナリ）</returns>
        public object ReadFile(string path)
        {
            // JSONファイルを読み込むメソッド
            try
            {
                // ファイルの内容を全て文字列として読み込む
                //string jsonString = File.ReadAllText(path);

                // 読み込んだ文字列をJSONとしてデシリアライズする
                //var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString, GetOptions());

                string jsonString = File.ReadAllText(path);
                OutputWriteLine(string.Format("jsonString = {0}", jsonString));

                // FileStreamを使用してファイルを同期的に読み込む
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    // デシリアライズする
                    //var jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString, GetOptions());
                    //var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
                    var jsonData = JsonSerializer.Deserialize<object>(jsonString, GetOptions());
                    return jsonData; // スコープの問題を解消
                }
            }
            catch (Exception ex)
            {
                ConsoleWriteLineError(ex, "ReadFile");
                throw ex;
            }
        }

        /// <summary>
        /// 入力のJSON文字列をDictionary型に変換します。
        /// </summary>
        /// <param name="json">JSON文字列</param>
        /// <returns>Dictionary<string, string>型の出力（エラー時は空のディクショナリ）</returns>
        public Dictionary<string, string> JsonToDict(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<string, string>();
            }

            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, string>>(json, GetOptions()) ?? new Dictionary<string, string>();
            }
            catch (JsonException e)
            {
                ConsoleWriteLineError(e, "JsonToDict");
                return new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// JSONのシリアライズ/デシリアライズのオプションを設定します。
        /// </summary>
        /// <returns>JsonSerializerOptions型のオプション</returns>
        private JsonSerializerOptions GetOptions()
        {
            /// // JsonSerializerのオプションを設定
            return new JsonSerializerOptions
            {
                //WriteIndented = true, // 出力をインデントして整形
                //Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 特殊文字のエスケープを無効化（これが問題の原因であることが多い）
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) // 特殊文字を含むすべてのUnicode範囲を許可
            };
        }


        //private JsonSerializerOptions GetOption()
        //{
        //    return new JsonSerializerOptions
        //    {
        //        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        //        WriteIndented = true, // JSONをインデントして見やすくする
        //    };
        //}

        /// <summary>
        /// エラーメッセージをコンソールに表示します。
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="msg">エラーメッセージ</param>
        private void ConsoleWriteLineError(Exception ex, string msg)
        {
            OutputWriteLine("==========");
            OutputWriteLine("ERROR : " + msg);
            OutputWriteLine(ex.Message);
            OutputWriteLine(ex.StackTrace);
            OutputWriteLine("==========");
        }

        private void OutputWriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}

