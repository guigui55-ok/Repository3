using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAspServerDefault.App_Code
{
    public class ResultInfo
    {
        public ResultInfo()
        {
            Result = ResultCode.None;
            Data = string.Empty;
        }

        public ResultInfo(ResultCode result, string data)
        {
            Result = result;
            Data = data;
        }

        public ResultCode Result { get; set; }
        public string Data { get; set; }
    }

    public enum ResultCode
    {
        None,
        OK,
        Fail,
        Unexpected,
        Error
    }
}