using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLog;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace Log
{
    public class Log : ILog
    {
        private ErrorLog.IErrorLog _errorLog;
        // ログを記録しているか
        private int _logingNow = 1;
        // ログリスト
        private List<string> _logList = new List<string>();
        
        // ログファイルパス
        private string _logPath;
        // ログファイル名
        private string _logFileName;
        // ログフルパス
        private string _logFullPath;
        // ログモード
        // Mode ログOFF:0、単にログを取って表示:1、ログを取ってファイルのみ追記:2、ログを取ってファイルにも追記:3
        private int _logMode = 1;
        // ログレベル
        // LogLevel 0:ERROR 1:WARN 2:DEBUG 3:TRACE
        private int _saveLogLevel = 0;
        // フォームに書き込むか
        public bool IsOutPutLogToForm = true;
        // encoding
        private Encoding _enc;
        // suppress 抑える
        public bool IsSuppressLogWhenAddValueSameLastOfList = true;
        // 出力フォーム
        private LogFormManager _logFormManager;


        public Log()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
            _logFormManager = new LogFormManager();
            _enc = Encoding.GetEncoding("Shift_JIS");
            _logingNow = 1;
            _logFormManager.LogForm._logFormManager = _logFormManager;
        }

        public bool TractabilityWithMainWindow
        {
            get { return _logFormManager.tractabilityWithMainWindow; }
            set { _logFormManager.tractabilityWithMainWindow = value; }
        }

        public void FollowParentWindow()
        {
            _logFormManager.FollowParentForm();
        }

        public int setParintForm(Object form)
        {
            return _logFormManager.setParentForm(form);
        }
        public int showWindow()
        {
            try
            {
                _logFormManager.LogForm._logFormManager = _logFormManager;
                int Ret = _logFormManager.show();
                if (Ret < 1) { _errorLog.addErrorNotException("showWindow"); }
                return Ret;
            } catch (Exception ex)
            {
                _errorLog.addException(ex,this.ToString(), "showWindow");
                return 0;
            }
        }
        public int setWindowLocationAndSize(Point location,Size size)
        {
            try
            {
                _logFormManager.LogForm.Location = location;
                _logFormManager.LogForm.Size = size;
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setWindowLocationAndSize");
                return 0;
            }
        }

        public void updateLogWindow(string value)
        {
            try
            {
                _logFormManager.updateWindow(value);
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "updateLogWindow");
            }
        }

        public void startLoging(){ _logingNow = 1; }
        public void stopLoging() { _logingNow = 0; }


        public void addLog(int priority, string className,string functionName, string parameter = "")
        {

        }

        // ログに追記
        public void addLog(int priority,string functionName,string parameter = "") 
        {
            try
            {
                if (_logingNow < 1) { return; }
                if (5 < priority) { return; }
                //string pristr = PriorityToString(priority);

                string value = makeLog(priority, functionName, parameter);

                addLogMain(value);
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "addLog");
            }
        }


        public void initializeLogFile()
        {
            string value = "---------------------------------------------------";
            addLog(5, value);
            value = makeLog(5, "");
            addLog(0, value);
        }

        private string makeLog(int priority, string functionName, string parameter = "")
        {
            try
            {
                string ret = "";
                // 時間
                if (priority != 5)
                {
                    ret += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " ";
                }

                // priority
                switch (priority)
                {
                    case 0: ret += "ERROR "; break;
                    case 1: ret += "WARN  "; break;
                    case 2: ret += "DEBUG "; break;
                    case 3: ret += "TRACE "; break;
                    case 4: ret += ""; break; // 日付のみ
                    case 5: ret = ""; break; // ログのみ
                    default: ret += "NOTHN"; break;
                }
                // 内容
                if (priority != 5)
                {
                    ret += functionName;
                    if (!(parameter == ""))
                    {
                        ret += " : " + parameter;
                    }
                    ret += "\n";
                }
                else
                {
                    ret += functionName;
                    ret += "\n";
                }

                return ret;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "makeLog");
                return "";
            }
        }

        // valueのみはPriority0として処理
        public void addLog(string value)
        {
            addLog(0, value);
        }

        private void addLogMain(string value)
        {
            switch (_logMode)
            {
                case 0:break;
                case 1: addLogToList(value); break;
                case 2:addLogToFile(value); break;
                case 3:addLogToList(value); addLogToFile(value); break;
                default: break;
            }

        }

        //private string PriorityToString(int priority)
        //{
        //    string Ret;
        //    switch (priority)
        //    {
        //        case 0: Ret = "ERROR"; break;
        //        case 1: Ret = "WARN "; break;
        //        case 2: Ret = "DEBUG"; break;
        //        case 3: Ret = "TRACE"; break;
        //        case 4: Ret = ""; break;
        //        case 5: Ret = ""; break;
        //        default: Ret = "NOTHN"; break;
        //    }
        //    return Ret;
        //}

        public void addLogToFile(string value)
        {
            try
            {
                StreamWriter writer = new StreamWriter(_logFullPath, true, _enc);
                writer.Write(value);
                writer.Close();
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "addLogToFile");
            }
        }

        public void addLogToList(string value)
        {
            try
            {
                if (IsAdd_ValueLastLineCountUpWhenAddValueSameLastOfList(value))
                {
                    // リスト追記
                    _logList.Add(value);
                    // 画面更新
                    if (IsOutPutLogToForm) { updateLogWindow(value); }
                }

                
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "addLogToList");
            }
        }

        private bool IsAdd_ValueLastLineCountUpWhenAddValueSameLastOfList(string value)
        {
            try
            {
                if (_logList.Count < 2) { return true; }
                // 抑える
                if (!IsSuppressLogWhenAddValueSameLastOfList) { return true; }
                // デバッグ出力
                //Debug.WriteLine(_logList.Last() + "  " + _logList.Last().CompareTo(value).ToString());
                if (_logList.Last().CompareTo(value) == 0)
                {
                    // 最後が全く同じ
                    // 最後の1つ前が数字である
                    int tmpint = -1;              // 変換のための一時的な変数
                    string tmpstr = _logList[_logList.Count - 2];
                    if (int.TryParse(tmpstr, out tmpint))
                    {
                        // 数字である
                        // 最後の1つ前のカウンタをUp?

                        return false;
                    } else
                    {
                        // 変換できなかった
                        // 数字でない
                        return false;
                    }
                } else
                {
                    // 同じでない
                    return true;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "IsAdd_ValueLastLineCountUpWhenAddValueSameLastOfList");
                return false;
            }
        }

        public void consoleWriteLine(string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// ログクラス初期化
        /// <param name="mode">Mode、0:ログOFF、1:単にログを取って表示、2:ログを取ってファイルのみ追記、3：ログを取ってファイルにも追記<br></br></param>
        /// <param name="saveLogLevel">LogLevel 0:ERROR 1:WARN 2:DEBUG 3:TRACE<br></br></param>
        /// <param name="windowVisible">ログウィンドウの表示可否</param>
        /// </summary>
        public int initialize(int mode,int saveLogLevel, bool windowVisible,string path,string filename)
        {
            try
            {
                _saveLogLevel = saveLogLevel;
                setLogFullPath(path, filename);
                setMode(mode);

                if (windowVisible) { 
                    showWindow();
                    _logFormManager.FollowParentForm();
                }
                //this.setWindowLocationAndSize(new Point(0,10),new Size(600,720));
                initializeLogFile();
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initialize");
                return 0;
            }
        }
        public int setPath(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    _logPath = path;
                    return 1;
                } else
                {
                    _logPath = Directory.GetCurrentDirectory();
                    _errorLog.addErrorNotException(this.ToString(), "setPath");
                    return -1;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setPath");
                return 0;
            }
        }

        public int setFileName(string filename)
        {
            try
            {
                if (filename == "")
                {
                    _logFileName = "Log.Log";
                    _errorLog.addErrorNotException(this.ToString(), "setFileName");
                    return -1;
                } else {
                    _logFileName = filename;
                    return 1;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "setFileName");
                return 0;
            }
        }

        public void setLogFullPath(string path,string filename)
        {
            setPath(path);
            setFileName(filename);
            _logFullPath = _logPath + @"\" + _logFileName;
        }

        public string getLogFullPath() { return _logPath + @"\" + _logFileName; }

        public void setMode(int mode)
        {
            try
            {
                if ((mode < 0) && (mode > 3))
                {
                    // ログモードが0以下3以上
                    _errorLog.addErrorNotException(this.ToString(), "setMode Failed " + mode.ToString());
                    _logMode = 0;
                } else
                {
                    _logMode = mode;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getLogFullPath");
            }
        }


        public int writeLogAllToFile()
        {
            try
            {
                if (!File.Exists(_logFullPath))
                {
                    _errorLog.addErrorNotException(this.ToString(), "writeLogAllToFile NotExists " + _logFullPath);
                    return -1;
                }
                if (_logList.Count < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "writeLogAllToFile Count 0");
                    return -2;
                }

                string wdata = "";
                int Ret = 0;
                // List->string
                Ret = new CommonList.CommonList().convertListToString(_logList, out wdata);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "convertListToString Failed");
                    return -3;
                }

                // writeData
                Ret = writeData(_logFullPath, wdata);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "writeLogAllToFile Failed");
                    return -4;
                }
                return 1;

            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "writeLogAllToFile");
                return 0;
            }
        }

        private int writeData(string path,string wdata,Encoding enc = null)
        {
            try
            {
                if (enc == null) { enc = Encoding.GetEncoding("Shift_JIS"); }
                StreamWriter writer = new StreamWriter(path, true, enc);
                writer.WriteLine(wdata);
                writer.Close();
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "writeData : path = " + path );
                return 0;
            }
        }


    }
}
