using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Web.Services.Description;
using TestAspServerDefault.App_Code;

namespace TestAspClientDefault
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    [DebuggerStepThrough]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "WebServiceSoap", Namespace = "http://tempuri.org/")]
    public class WebServiceClient : SoapHttpClientProtocol
    {
        public WebServiceClient()
        {
            this.Url = "http://localhost:51582/WebService.asmx";
        }

        public WebServiceClient(string url)
        {
            this.Url = url;
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_Default",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_Default()
        {
            object[] results = this.Invoke("TestWebMethod_Default", new object[] { });
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_OutOne",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_OutOne(out string value1)
        {
            object[] results = this.Invoke("TestWebMethod_OutOne", new object[] { });
            value1 = (string)results[1];
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_InOne",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_InOne(string inValue1)
        {
            object[] results = this.Invoke("TestWebMethod_InOne", new object[] { inValue1 });
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_OutIn",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_OutIn(out string outValue1, string inValue1)
        {
            object[] results = this.Invoke("TestWebMethod_OutIn", new object[] { inValue1 });
            outValue1 = (string)results[1];
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_OutIn",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_OutIn_Fail_Example(out string outValue1, string inValue1)
        {
            object[] results = this.Invoke("TestWebMethod_OutIn", new object[] { "outValue1", inValue1 });
            outValue1 = (string)results[1];
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_InIn",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_InIn(string inValue1, string inValue2)
        {
            object[] results = this.Invoke("TestWebMethod_InIn", new object[] { inValue1, inValue2 });
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_OutOutIn",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_OutOutIn(out string outValue1, out string outValue2, string inValue1)
        {
            object[] results = this.Invoke("TestWebMethod_OutOutIn", new object[] { inValue1 });
            outValue1 = (string)results[1];
            outValue2 = (string)results[2];
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_InOutOut",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_InOutOut(string inValue1, out string outValue1, out string outValue2)
        {
            object[] results = this.Invoke("TestWebMethod_InOutOut", new object[] { inValue1 });
            outValue1 = (string)results[1];
            outValue2 = (string)results[2];
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_InOutIn",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_InOutIn(string inValue1, out string outValue1, string inValue2)
        {
            object[] results = this.Invoke("TestWebMethod_InOutIn", new object[] { inValue1, inValue2 });
            outValue1 = (string)results[1];
            return (ResultInfo)results[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_OutInOut",
            RequestNamespace = "http://tempuri.org/",
            ResponseNamespace = "http://tempuri.org/",
            Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_OutInOut(out string outValue1, string inValue1, out string outValue2)
        {
            object[] results = this.Invoke("TestWebMethod_OutInOut", new object[] { inValue1 });
            outValue1 = (string)results[1];
            outValue2 = (string)results[2];
            return (ResultInfo)results[0];
        }
    }
}
