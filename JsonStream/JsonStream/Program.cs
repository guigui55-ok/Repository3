using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace JsonStreamModule
{
    class Program
    {
        static void Main(string[] args)
        {

            // JsonStreamクラスのインスタンスを作成
            JsonStream jsonStream = new JsonStream();

            string dirPath = GetParentDirectory(Directory.GetCurrentDirectory(), 1);
            string fileName = "";
            fileName = "test2.json";
            fileName = "test.json";
            string filePath = Path.Combine(dirPath, fileName);
            Console.WriteLine($"filePath = {filePath}");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            // ファイルから読み込みテスト
            var readData = jsonStream.ReadFile(filePath);
            Console.WriteLine("\nReadFile出力:");
            Console.WriteLine(String.Format("dict json = {0}", readData));
            PrintDict(readData);

            Console.WriteLine("Program Is Done. Press Any Key.");
            Console.ReadKey();

        }


        static void Main_A(string[] args)
        {
            //エラーなし
            // dictBをそのままSerializeObjectに渡す
            Type dataType = typeof(Dictionary<string, object>);
            Dictionary<string, object> dictA = new Dictionary<string, object>()
            {
                { "Name", "N_A" },
                { "Age", 16 }
            };
            Dictionary<string, object> dictB = new Dictionary<string, object>()
            {
                { "Name", "N_B" },
                { "Age", 17 }
            };

            List<Dictionary<string, object>> data_list = new List<Dictionary<string, object>>();
            data_list.Add(dictA);
            data_list.Add(dictB);

            //string json = JsonConvert.SerializeObject(data_list, Formatting.Indented);//この場合DictA、DictBのようにはKeyがないデータができてしまうのでエラーとなる。
            string json = JsonConvert.SerializeObject(dictB, Formatting.Indented);
            Console.WriteLine($"Serialized JSON: ");


            //json = @"{
            //                     'href'  : '/account/login.aspx',
            //                     'target': '_blank'
            //                 }";
            //json = @"{
            //                    ""href""  : ""/account/login.aspx"",
            //                     ""target"": ""_blank""
            //                 }";

            json = RemoveOutsideBrackets(json);
            json = json.Trim();
            //json = "{\n" + json + "\n}";
            Console.WriteLine(json);
            //Console.WriteLine($"Serialized JSON: {json}");

            // JSONデータからDictionaryにデシリアライズします
            Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            Console.WriteLine(String.Format("dict json = {0}", jsonDict));
            PrintDict(jsonDict);
            //// ファイルへ保存
            //SaveToJsonFile(data_list, "file.json");

            //// ファイルから読み出し
            //var loadedData = LoadFromJsonFile<List<DataModel>>("file.json");

            Console.ReadKey();

        }

        static void PrintDict(Dictionary<string, object> dict, string num_str = "", bool withDataType = true)
        {
            int count = 0;
            foreach (string key in dict.Keys)
            {
                if (dict[key].GetType().ToString().IndexOf("Dictionary") > 0)
                {
                    Dictionary<string, object> dictB = (Dictionary<string, object>)dict[key];
                    if (num_str != "") { num_str += "-" + count.ToString(); }
                    PrintDict(dictB, num_str);
                }
                else
                {
                    string num_strB;
                    if (num_str != "") { num_strB = num_str + "-" + count.ToString(); }
                    else { num_strB = count.ToString(); }
                    string dataTypeStr = "";
                    if (withDataType)
                    {
                        dataTypeStr = dict[key].GetType().ToString();
                        dataTypeStr = string.Format(" {{{0}}} ", dataTypeStr);
                    }
                    Console.WriteLine(String.Format("i={0},{3}{1} : {2}", num_strB, key, dict[key], dataTypeStr));
                }
                count++;
            }
        }


        public static string RemoveOutsideBrackets(string input)
        {
            // 最初の "[" のインデックスを取得
            int startIndex = input.IndexOf('[');

            // 最後の "]" のインデックスを取得
            int endIndex = input.LastIndexOf(']');

            // "[" と "]" が両方とも存在し、正しい順序であれば
            if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
            {
                // "[" と "]" の間の部分を返す
                return input.Substring(startIndex + 1, endIndex - startIndex - 1);
            }

            // 条件が満たされない場合は元の文字列を返す
            return input;
        }



        static void JsonStreamSystemTextJson_Main(string[] args)
        {

            // JsonStreamクラスのインスタンスを作成
            JsonStreamSystemTextJson jsonStream = new JsonStreamSystemTextJson();

            string dirPath = GetParentDirectory(Directory.GetCurrentDirectory(), 1);
            //string filePath = Path.Combine(dirPath, "test.json");
            string filePath = Path.Combine(dirPath, "test2.json");
            Console.WriteLine($"filePath = {filePath}");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            // ファイルから読み込みテスト
            var readData = jsonStream.ReadFile(filePath);
            Console.WriteLine("\nReadFile出力:");
            //foreach (var item in readData)
            //{
            //    Console.WriteLine($"{item.Key}: {item.Value}");
            //}
            Console.WriteLine(readData.ToString());

            Console.WriteLine("Program Is Done. Press Any Key.");
            Console.ReadKey();
        }

        static string GetParentDirectory(string filePath, int count=0)
        {
            // DirectoryInfoを使用する場合
            DirectoryInfo dirInfo = new DirectoryInfo(filePath);

            // 1回目の親ディレクトリ取得
            DirectoryInfo parentDirInfo = dirInfo?.Parent;
            Console.WriteLine("Parent Directory (DirectoryInfo): " + parentDirInfo.FullName);
            if (count <= 0)
            {
                return parentDirInfo.FullName;
            }
            else
            {
                // 2回目以降の親ディレクトリ取得
                for (int i = 0; i < count; i++)
                {
                    parentDirInfo = parentDirInfo?.Parent;
                    Console.WriteLine("Grandparent Directory (DirectoryInfo): " + parentDirInfo?.FullName);
                }
                return parentDirInfo.FullName;
            }
        }
    }
}
