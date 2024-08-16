using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileList
{
    public class CommonString
    {
        public void DebugWriteList(List<string> list)
        {
            foreach(var val in list)
            {
                Debug.WriteLine(val);
            }
        }
        public string[] StringToArray(string value,char sepalator)
        {
            try
            {
                // 戻り値用
                string[] retAry = new string[0];
                // 切り出す用左
                int leftpos = 0;
                // 切り出す用右
                int rightpos = value.IndexOf(sepalator);
                // カウンタ
                int count = 1;
                // 1つも文字がない
                if (rightpos < 0) { return new List<string>().ToArray(); }
                // 切り出しループ
                while (rightpos >= 0)
                {
                    // 配列リサイズ
                    Array.Resize(ref retAry, count);
                    // 最後の要素に切り出した文字列を格納
                    retAry[count - 1] = value.Substring(leftpos, rightpos-1);
                    // すべてsepalatorなどの場合含まれることが考えられる
                    if (retAry[count - 1].Contains(sepalator))
                    {
                        // その場合はsepalatorを消す
                        retAry[count - 1].Replace(sepalator.ToString(), "");
                    }
                    // 次用にセット
                    leftpos = rightpos;
                    // 次を探す
                    rightpos = value.IndexOf(sepalator,leftpos+1);
                }
                return retAry;
            } catch (Exception ex)
            {
                Debug.WriteLine(this.ToString() + ".StringToArray");
                Debug.WriteLine(ex.Message);
                return new List<string>().ToArray();
            }
        }
        public List<string> getListFromString(string value, char sepalator)
        {
            try
            {
                return new List<string>(value.Split(sepalator));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.ToString() + ".getListFromString");
                Debug.WriteLine(ex.Message);
                return new List<string>();
            }
        }

        public string ListStringToString(List<string> list,string sepalator)
        {
            try
            {
                if (list == null) { return ""; }
                int count = 0;
                string buf="";
                foreach(var value in list)
                {
                    buf += value;
                    count++;
                    if (count < list.Count - 1)
                    {
                        buf += sepalator;
                    }
                }
                return buf;
            } catch ( Exception ex)
            {
                Debug.WriteLine(this.ToString() + ".ListStringToString");
                Debug.WriteLine(ex.Message);
                return "";
            }
        }
    }
}
