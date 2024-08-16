using System.Collections.Generic;

namespace CommonList
{
    public class CommonList
    {
        static int Main()
        {
            return 1;
        }
        public int convertListToString(List<string> list,out string result,string sepaletor = "\n")
        {
            try
            {
                // リストがない場合は空文字
                if (list.Count < 1) { result = ""; return -1; }
                string ret = "";
                long count = 0;
                foreach(var buf in list)
                {
                    ret += buf;
                    count++;
                    if (count < list.Count)
                    {
                        // 最後だけSepaletorは追加しない
                        ret += sepaletor;
                    }
                }
                result = ret;
                return 1;
            } catch
            {
                result = "";
                return 0;
            }
        }
    }
}
