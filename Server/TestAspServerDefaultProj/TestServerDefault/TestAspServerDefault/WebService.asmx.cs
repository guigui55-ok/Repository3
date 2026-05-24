using System;
using System.Web.Services;
using TestAspServerDefault.App_Code;

namespace TestAspServerDefault
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service : WebService
    {
        [WebMethod]
        public ResultInfo TestWebMethod_Default()
        {
            return CreateOk("Default called");
        }

        [WebMethod]
        public ResultInfo TestWebMethod_OutOne(out string value1)
        {
            value1 = "out value1";
            return CreateOk("OutOne called");
        }

        [WebMethod]
        public ResultInfo TestWebMethod_InOne(string inValue1)
        {
            return CreateOk("InOne called. inValue1=" + inValue1);
        }

        [WebMethod]
        public ResultInfo TestWebMethod_OutIn(out string outValue1, string inValue1)
        {
            outValue1 = "out value1";
            return CreateOk("OutIn called. inValue1=" + inValue1);
        }

        [WebMethod]
        public ResultInfo TestWebMethod_InIn(string inValue1, string inValue2)
        {
            return CreateOk("InIn called. inValue1=" + inValue1 + ", inValue2=" + inValue2);
        }

        [WebMethod]
        public ResultInfo TestWebMethod_OutOutIn(out string outValue1, out string outValue2, string inValue1)
        {
            outValue1 = "out value1";
            outValue2 = "out value2";
            return CreateOk("OutOutIn called. inValue1=" + inValue1);
        }

        [WebMethod]
        public ResultInfo TestWebMethod_InOutOut(string inValue1, out string outValue1, out string outValue2)
        {
            outValue1 = "out value1";
            outValue2 = "out value2";
            return CreateOk("InOutOut called. inValue1=" + inValue1);
        }

        [WebMethod]
        public ResultInfo TestWebMethod_InOutIn(string inValue1, out string outValue1, string inValue2)
        {
            outValue1 = "out value1";
            return CreateOk("InOutIn called. inValue1=" + inValue1 + ", inValue2=" + inValue2);
        }

        [WebMethod]
        public ResultInfo TestWebMethod_OutInOut(out string outValue1, string inValue1, out string outValue2)
        {
            outValue1 = "out value1";
            outValue2 = "out value2";
            return CreateOk("OutInOut called. inValue1=" + inValue1);
        }

        private ResultInfo CreateOk(string data)
        {
            return new ResultInfo(ResultCode.OK, data);
        }
    }
}