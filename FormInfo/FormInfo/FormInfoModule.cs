using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormInfo
{
    public class FormInfoData
    {
        public Size _size;
        public Point _location;
        public Rectangle _dispRectangle;
        public Color _backColor;
        public string _title;
        public Point _nowMousePoint;
        // 子コントロールのリスト、
        public FormInfoData()
        {

        }

        public Dictionary<string, object> getInfoDict()
        {
            Dictionary<string, object> ret = new Dictionary<string, object> { };
            ret.Add("Title", _title.ToString());
            ret.Add("Size", _size.ToString());
            ret.Add("Location", _location.ToString());
            ret.Add("DisplayRectAngle", _dispRectangle.ToString());
            ret.Add("BackColor", _backColor.ToString());
            ret.Add("MousePoint", _nowMousePoint.ToString());
            return ret;
        }

        public void setMousePoint(Point mp)
        {
            _nowMousePoint = mp;
        }
        public string getInfoStr()
        {
            string ret = "";
            var dict = getInfoDict();
            foreach(KeyValuePair<string, object> item in dict)
            {
                ret += string.Format("{0,-5}:{1}", item.Key, item.Value.ToString()) + Environment.NewLine;
            }
            return ret;
        }
    }
}
