using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Security.Cryptography;

namespace PipeTest1
{
    [DataContract]
    internal class Person
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
    }

    internal class Program
    {
        // 共通のエントロピー（任意）。送信側と受信側で同じ値を使うこと。
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("PipeEntropy_v1");

        static void Main(string[] args)
        {
            // 送信するオブジェクトを生成
            var person = new Person { Name = "太郎", Age = 30 };

            // 一意のパイプ名を生成して子プロセスに渡す
            string pipeName = "PipeCallApp_" + Guid.NewGuid().ToString("N");

            // PipeCallApp.exe の予想パス（環境に合わせて必要なら調整すること）
            string childExe = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "PipeCallApp", "bin", "Debug", "PipeCallApp.exe"));

            var psi = new ProcessStartInfo
            {
                FileName = childExe,
                Arguments = pipeName,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var proc = Process.Start(psi))
            {
                if (proc == null)
                {
                    Console.WriteLine("子プロセスを起動できませんでした。パスを確認してください: " + childExe);
                    return;
                }

                // 子プロセスから "READY" が出力されるのを待つ（最大5秒）
                string readyLine = null;
                var readyTimeout = DateTime.UtcNow.AddSeconds(5);
                while (DateTime.UtcNow < readyTimeout)
                {
                    if (!proc.StandardOutput.EndOfStream)
                    {
                        readyLine = proc.StandardOutput.ReadLine();
                        if (readyLine == "READY") break;
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }

                if (readyLine != "READY")
                {
                    Console.WriteLine("子プロセスが準備完了を報告しませんでした。続行します（接続で失敗する可能性あり）...");
                }

                // クライアントとしてパイプに接続してオブジェクトを送信（JSON を暗号化して送る）
                using (var client = new NamedPipeClientStream(".", pipeName, PipeDirection.Out))
                {
                    try
                    {
                        client.Connect(5000); // タイムアウト 5 秒
                    }
                    catch (TimeoutException)
                    {
                        Console.WriteLine("パイプへの接続がタイムアウトしました。");
                        return;
                    }

                    // JSON シリアライズ
                    var serializer = new DataContractJsonSerializer(typeof(Person));
                    using (var ms = new MemoryStream())
                    {
                        serializer.WriteObject(ms, person);
                        var plain = ms.ToArray();

                        // DPAPI で暗号化（CurrentUser スコープ）
                        var encrypted = ProtectedData.Protect(plain, Entropy, DataProtectionScope.CurrentUser);

                        // 暗号化データをそのままパイプに書き込む
                        client.Write(encrypted, 0, encrypted.Length);
                        client.Flush();
                    }
                }

                // 子プロセスの終了を待って結果を表示（任意）
                proc.WaitForExit(3000);
                Console.WriteLine("送信完了。子プロセス終了コード: " + proc.ExitCode);
            }
        }
    }
}
