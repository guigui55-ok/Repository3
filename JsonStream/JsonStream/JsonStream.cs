using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonStreamModule
{
    //Install-Package Newtonsoft.Json
    public class JsonStream
    {
        // #########
        // Member
        public string _path = "";

        // #########
        /// <summary>
        /// 入力をJSON文字列に変換します。(Json構造が複雑になったときもDictonaryにする）
        /// </summary>
        /// <param name="dict">Dictionary<string, int>型の入力</param>
        /// <returns>JSON文字列</returns>
        public string DictToJson(Dictionary<string, object> dict)
        {
            string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
            return json;
        }



        /// <summary>
        /// 入力のJSON文字列をDictionary型に変換します。
        /// </summary>
        /// <param name="json">JSON文字列</param>
        /// <returns>Dictionary<string, string>型の出力(入力が異常の場合は空のオブジェクト)</returns>
        public Dictionary<string, object> JsonToDict(string json)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object> { };
            if (String.IsNullOrEmpty(json))
            {
                return retDict;
            }
            try
            {
                // JSONデータからDictionaryにデシリアライズします
                retDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                return retDict;
            }
            catch (Exception ex)
            {
                ConsoleWriteLineError(ex, "JsonToDict");
                OutputWriteLine("==========");
                OutputWriteLine("json =");
                OutputWriteLine(json);
                OutputWriteLine("==========");
                return retDict;
            }
        }

        public void WriteFile(Dictionary<string, object> dict)
        {
            WriteFile(_path, dict);
        }

        public void WriteFile(string path, Dictionary<string, object> dict)
        {
            string json = DictToJson(dict);
            //ファイルへ書き込み
            File.WriteAllText(path, json);
        }


        public Dictionary<string, object> ReadFile()
        {
            return ReadFile(_path);
        }
        public Dictionary<string, object> ReadFile(string path)
        {
            string readData = "";
            //ファイルから読み込み
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found: {path}");
            }
            readData = File.ReadAllText(path);
            Dictionary<string, object> retDict = JsonToDict(readData);
            return retDict;
        }

        //public Dictionary<string, object> ReadFile(string path)
        //{
        //    string readData = "";
        //    // ファイルから読み込み
        //    if (!File.Exists(path))
        //    {
        //        throw new FileNotFoundException($"File not found: {path}");
        //    }
        //    readData = File.ReadAllText(path);

        //    // JSONデータをTに変換
        //    Dictionary<string, object> dict = JsonToDict(readData);
        //    return dict;
        //}

        /// <summary>
        /// このメソッドはジェネリクス型のテスト用です
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="testMethod"></param>
        /// <returns></returns>
        public T ReadFile<T>(string path, bool isGenerics)
        {
            string readData = "";
            // ファイルから読み込み
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found: {path}");
            }
            readData = File.ReadAllText(path);

            // JSONデータをTに変換
            Dictionary<string, object> dict = JsonToDict(readData);
            //T retDict = JsonToDict(readData);
            // Dictionary<string, object> をTにキャストする
            if (typeof(T) == typeof(Dictionary<string, object>))
            {
                return (T)(object)dict;
            }
            // Dictionary<string, object> を元にT型に変換 (もし他の型の場合)
            // ここでは Newtonsoft.Json を使って変換
            string json = JsonConvert.SerializeObject(dict);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Newtonsoft.Json.Linq.JObjectを Dictionary(string,object)に変換する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ConvertJObjectToDict(object value)
        {
            Dictionary<string, object> retDict;
            // objがJObjectであれば、Dictionary<string, object>に変換
            if (value is Newtonsoft.Json.Linq.JObject jObject)
            {
                retDict = jObject.ToObject<Dictionary<string, object>>();
            }
            else
            {
                // objがすでにDictionaryの場合や他の型の場合の処理
                retDict = (Dictionary<string, object>)value;
            }
            return retDict;
        }

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
