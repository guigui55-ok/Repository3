using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFileDialogManager
{
    public class DialogFilter
    {
        private List<string> FilterList;

        public DialogFilter()
        {
            MakeArray();
        }

        public string MakeFilter(string[] values) 
        {
            try
            {
                string ret="";
                string buf = "";
                for(int i=0;i< values.Length; i++)
                {
                    buf = GetFilterValue(values[i]);
                    if (i < values.Length)
                    {
                        if (!(i == 0))
                        {
                            if (!(buf == ""))
                            {
                                ret += "|" + buf;
                            }
                        } else
                        {
                            ret = buf;
                        }
                    } else
                    {
                        ret += buf;
                    }
                }
                return ret;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }        
        }
        private string GetFilterValue(string value)
        {
            string ret = "";
            foreach(string buf in this.FilterList)
            {
                if (buf.Contains(value))
                {
                    ret = buf;
                    return ret;
                }
            }
            return "";
        }

        private void MakeArray()
        {
            List<string> list = new List<string>();
            list.Add("HTMLファイル(*.html;*.htm)|*.html;*.htm");
            list.Add("すべてのファイル(*.*)|*.*");
            list.Add("ビットマップファイル(*.bmp,*.dib)|*.bmp,*.dib");
            list.Add("JPEG(*.jpg,*.jpeg,*.jpe,*.jfif)|*.jpg,*.jpeg,*.jpe,*.jfif");
            list.Add("GIF(*.gif)|*.gif");
            list.Add("TIFF(*.tif,*.tiff)|*.tif,*.tiff");
            list.Add("PNG(*.png)|*.png");
            list.Add("ICO(*.ico)|*.ico");
            list.Add("HEIC(*.heic)|*.heic");
            list.Add("WEBP(*.webp)|*.webp");
            list.Add("すべてのピクチャファイル|*.bmp,*.dib,*.jpg,*.jpeg,*.jpe,*.jfif,*.gif,*.tif,*.tiff,*.png,*.ico,*.heic,*.webp");
            this.FilterList = list;
        }
    }
}
