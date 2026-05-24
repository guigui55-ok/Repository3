using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TestAspServerDefault.App_Code;

namespace TestAspServerDefault
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service : System.Web.Services.WebService
    {
        [WebMethod]
        public ResultInfo TestWebMethod_Default()
        {
            return new ResultInfo { Result = ResultCode.OK, Data = "OK" };
        }

        [WebMethod]
        public ResultInfo TestWebMethod_OutOne(out string outValue)
        {
            outValue = "out value";
            return new ResultInfo { Result = ResultCode.OK, Data = "OutOne" };
        }

        [WebMethod]
        public ResultInfo TestWebMethod_InOne(string input)
        {
            return new ResultInfo { Result = ResultCode.OK, Data = input };
        }

        // 他のパターンも同様に実装
    }
}