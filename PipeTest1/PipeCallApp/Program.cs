using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Security.AccessControl;

namespace PipeCallApp
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
        // 送信側と同じエントロピー
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("PipeEntropy_v1");

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("パイプ名を引数に指定してください。");
                return;
            }

            string pipeName = args[0];

            // PipeSecurity を使って現在のユーザーにのみアクセスを許可する
            var pipeSecurity = new PipeSecurity();
            var currentUser = WindowsIdentity.GetCurrent();
            if (currentUser?.User != null)
            {
                pipeSecurity.AddAccessRule(new PipeAccessRule(currentUser.User, PipeAccessRights.FullControl, AccessControlType.Allow));
            }

            // サーバーとしてパイプを作成して接続を待つ。バッファサイズはデフォルト。
            using (var server = new NamedPipeServerStream(pipeName,
                                                         PipeDirection.In,
                                                         1,
                                                         PipeTransmissionMode.Byte,
                                                         PipeOptions.None,
                                                         0,
                                                         0,
                                                         pipeSecurity))
            {
                // 親プロセスに準備完了を通知（標準出力に "READY"）
                Console.WriteLine("READY");

                server.WaitForConnection();

                using (var ms = new MemoryStream())
                {
                    var buffer = new byte[4096];
                    int read;
                    while ((read = server.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    var encrypted = ms.ToArray();

                    try
                    {
                        // DPAPI で復号（CurrentUser スコープ）
                        var plain = ProtectedData.Unprotect(encrypted, Entropy, DataProtectionScope.CurrentUser);

                        using (var plainMs = new MemoryStream(plain))
                        {
                            var serializer = new DataContractJsonSerializer(typeof(Person));
                            var person = (Person)serializer.ReadObject(plainMs);
                            Console.WriteLine("受信した Person:");
                            Console.WriteLine("  Name: " + person.Name);
                            Console.WriteLine("  Age : " + person.Age);
                        }
                    }
                    catch (CryptographicException ex)
                    {
                        Console.WriteLine("復号に失敗しました: " + ex.Message);
                    }
                    catch (SerializationException ex)
                    {
                        Console.WriteLine("デシリアライズに失敗しました: " + ex.Message);
                    }
                }
            }
        }
    }
}
