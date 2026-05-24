using System;
using System.Reflection;
using System.Web.Services.Protocols;

namespace TestAspClientDefault
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "WebServiceSoap", Namespace = "http://tempuri.org/")]
    public class WebServiceInvoker : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        /// <remarks/>
        public WebServiceInvoker()
        {
            this.Url = "http://localhost/WebService.asmx";
        }

        /// <remarks/>
        public WebServiceInvoker(string url)
        {
            this.Url = url;
        }

        /// <summary>
        /// Invokes a web service method using the Invoke method of SoapHttpClientProtocol.
        /// </summary>
        public T Invoke<T>(string methodName, object[] args, string ns = "http://tempuri.org/")
        {
            object[] results = this.Invoke(methodName, args);
            return (T)results[0];
        }

        /// <summary>
        /// Calls a web service method with a specified URL.
        /// </summary>
        public static T CallWebService<T>(string methodName, object[] args, string url, string ns = "http://tempuri.org/")
        {
            var invoker = new WebServiceInvoker(url);
            return invoker.Invoke<T>(methodName, args, ns);
        }

        /// <summary>
        /// Calls a web service method using reflection-based proxy lookup.
        /// </summary>
        public static T CallViaProxy<T>(string methodName, object[] args, string url)
        {
            try
            {
                var proxyType = Type.GetType("TestAspClientDefault.TestWebServiceClient, TestAspClientDefault");
                if (proxyType != null)
                {
                    var proxy = Activator.CreateInstance(proxyType, new object[] { url });
                    var m = proxyType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
                    if (m != null)
                    {
                        var result = m.Invoke(proxy, args ?? new object[0]);
                        return (T)result;
                    }
                }
            }
            catch
            {
                // Fall through to exception below
            }

            throw new InvalidOperationException($"Could not invoke method '{methodName}' via proxy class.");
        }
    }
}
