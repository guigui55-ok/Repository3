using System;

namespace TestAspClientDefault
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class WebServiceUrlAttribute : Attribute
    {
        public string Url { get; }
        public string Namespace { get; }

        public WebServiceUrlAttribute(string url, string xmlNamespace = "http://tempuri.org/")
        {
            Url = url;
            Namespace = xmlNamespace;
        }
    }
}
