using System;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Web.Services.Description;
using TestAspServerDefault.App_Code;

namespace TestAspClientDefault
{
    public class TestWebServiceClient : SoapHttpClientProtocol
    {
        public TestWebServiceClient(string url)
        {
            this.Url = url;
        }

        [DebuggerStepThrough]
        [SoapDocumentMethod("http://tempuri.org/TestWebMethod_Default", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ResultInfo TestWebMethod_Default()
        {
            object[] results = this.Invoke("TestWebMethod_Default", new object[] { });
            return ((ResultInfo)(results[0]));
        }
    }
}
