using System;
using System.Diagnostics;
using System.Reflection;
using TestAspServerDefault.App_Code;

namespace TestAspClientDefault
{
    internal class Program
    {
        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        static void Main(string[] args)
        {
            try
            {
                var result = CallTestWebMethod_Default();
                Debug.WriteLine("# TestWebMethod_Default");
                Debug.WriteLine(result.Result + ": " + result.Data + "\n");

                string value1;
                result = CallTestWebMethod_OutOne(out value1);
                Debug.WriteLine("# TestWebMethod_OutOne");
                Debug.WriteLine(result.Result + ": " + result.Data + " / out=" + value1 + "\n");

                result = CallTestWebMethod_InOne("input1");
                Debug.WriteLine("# TestWebMethod_InOne");
                Debug.WriteLine(result.Result + ": " + result.Data + "\n");

                string outValue1;
                result = CallTestWebMethod_OutIn(out outValue1, "input1");
                Debug.WriteLine("# TestWebMethod_OutIn");
                Debug.WriteLine(result.Result + ": " + result.Data + " / out=" + outValue1 + "\n");

                // OutIn_Fail_Example
                result = CallTestWebMethod_OutIn_Fail_Example(out outValue1, "input1");
                Debug.WriteLine("# TestWebMethod_OutIn_Fail_Example");
                Debug.WriteLine(result.Result + ": " + result.Data + " / out=" + outValue1 + "\n");

                result = CallTestWebMethod_InIn("input1", "input2");
                Debug.WriteLine("# TestWebMethod_InIn");
                Debug.WriteLine(result.Result + ": " + result.Data + "\n");

                string outValue2;
                result = CallTestWebMethod_OutOutIn(out outValue1, out outValue2, "input1");
                Debug.WriteLine("# TestWebMethod_OutOutIn");
                Debug.WriteLine(result.Result + ": " + result.Data + " / out1=" + outValue1 + " / out2=" + outValue2 + "\n");

                result = CallTestWebMethod_InOutOut("input1", out outValue1, out outValue2);
                Debug.WriteLine("# TestWebMethod_InOutOut");
                Debug.WriteLine(result.Result + ": " + result.Data + " / out1=" + outValue1 + " / out2=" + outValue2 + "\n");

                result = CallTestWebMethod_InOutIn("input1", out outValue1, "input2");
                Debug.WriteLine("# TestWebMethod_InOutIn");
                Debug.WriteLine(result.Result + ": " + result.Data + " / out=" + outValue1 + "\n");

                result = CallTestWebMethod_OutInOut(out outValue1, "input1", out outValue2);
                Debug.WriteLine("# TestWebMethod_OutInOut");
                Debug.WriteLine(result.Result + ": " + result.Data + " / out1=" + outValue1 + " / out2=" + outValue2 + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_Default()
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_Default();
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_OutOne(out string value1)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_OutOne(out value1);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_InOne(string inValue1)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_InOne(inValue1);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_OutIn(out string outValue1, string inValue1)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_OutIn(out outValue1, inValue1);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_OutIn_Fail_Example(out string outValue1, string inValue1)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_OutIn_Fail_Example(out outValue1, inValue1);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_InIn(string inValue1, string inValue2)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_InIn(inValue1, inValue2);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_OutOutIn(out string outValue1, out string outValue2, string inValue1)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_OutOutIn(out outValue1, out outValue2, inValue1);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_InOutOut(string inValue1, out string outValue1, out string outValue2)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_InOutOut(inValue1, out outValue1, out outValue2);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_InOutIn(string inValue1, out string outValue1, string inValue2)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_InOutIn(inValue1, out outValue1, inValue2);
        }

        [WebServiceUrl("http://localhost:51582/WebService.asmx")]
        public static ResultInfo CallTestWebMethod_OutInOut(out string outValue1, string inValue1, out string outValue2)
        {
            var _webServiceClient = new WebServiceClient();
            return _webServiceClient.TestWebMethod_OutInOut(out outValue1, inValue1, out outValue2);
        }
    }
}
