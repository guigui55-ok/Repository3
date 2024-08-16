using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility.FileUtility.OpendFileUtility
{
    public enum Constants : int
    {
        RESULT_NONE,
        RESULT_SUCCESS,
        FILEOPEN_DIALOG_CANCEL,
    }
    public enum ErrorCodes : int
    {
        UNEXPECTED_ERROR,
        UNEXPECTED_ERROR_M,
        UNDEFINED_ERROR,
        FILEOPEN_FAILED,
        FILEOPEN_DIALOG_CANCEL,
    }
    static class ErrorConstatns
    {
        public static readonly string UNEXPECTED_ERROR = "不明なエラーです。(Unexpected Error)";
        static public readonly string[] ErrorMessages = new string[]{
            "不明なエラーです。(Unexpected Error)",
            "不明なエラーです。(Unexpected Error[-1])",
            "未定義のエラーです。(Undefined Error)",
            "ファイルオープンエラー",
            "ファイルオープンがキャンセルされました"
        };
    }
    static public class ConstConvert
    {
        public static string GetErrorMessage(CommonUtility.FileUtility.OpendFileUtility.ErrorCodes code)
        {
            return GetErrorMessageInExcelManager(code);
        }
        public static string GetErrorMessageInExcelManager(ErrorCodes code)
        {
            for (int i = 0; i < ErrorConstatns.ErrorMessages.Length; i++)
            {
                if ((int)code == i)
                {
                    return ErrorConstatns.ErrorMessages[i];
                }
            }
            return "CommonUtility.FileUtility.OpendFileUtility.ErrorConstatns.UNEXPECTED ERROR";
        }
    }
}
