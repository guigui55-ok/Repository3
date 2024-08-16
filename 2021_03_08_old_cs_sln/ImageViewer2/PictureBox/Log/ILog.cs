using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Log
{
    interface ILog
    {
        int showWindow();
        int setWindowLocationAndSize(Point location, Size size);
        void startLoging();
        void stopLoging();
        void addLog(int priority, string functionName, string parameter = "");
        //void addLogMain(string value);
        void initializeLogFile();
        void addLog(string value);
        void addLogToFile(string value);
        void addLogToList(string value);
        void consoleWriteLine(string str);
        int initialize(int mode, int saveLogLevel, bool windowVisible, string path, string filename);
    }
}
