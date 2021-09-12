using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppendToRichTextBoxAsyncWithLoopSample
{
    public class DoLoop
    {
        protected ErrorManager.ErrorManager _err;
        public bool IsExcute = false;
        public bool IsExit = false;
        public Thread LoopThread;

        public int Count = 0;
        public ControlForAsync ControlForAsync;

        public DoLoop(ErrorManager.ErrorManager err)
        {
            _err = err;
        }

        public void ExcuteAsync()
        {
            try
            {
                _err.AddLog(this, "ExcuteAsync");
                if (LoopThread == null)
                {
                    Task task = Task.Run(() =>
                    {
                        try
                        {
                            LoopThread = Thread.CurrentThread;
                            Excute();
                        }
                        catch (Exception ex)
                        {
                            _err.AddException(ex, this, "ExcuteAsync Task");
                        }
                    });
                    _err.AddLog(this, "LoopThread == null");
                }
                else
                {
                    if (IsExcute)
                    {
                        // 多重起動しようとしている
                        _err.AddLog(this,"LoopMethod Dupulicate Excution Error");
                    }
                    else
                    {
                        // Loop停止状態から再開
                        IsExcute = true;
                    }
                }
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "ExcuteAsync");
            }
        }
        private void Excute()
        {
            try
            {
                _err.AddLog(this, "Excute");
                bool swFlag = false; // Stopwach 定期出力用
                Stopwatch sw = new Stopwatch();
                sw.Start();
                IsExcute = true;
                while (true)
                {
                    if (IsExcute)
                    {
                        // 1 秒間隔で実行する
                        if (sw.ElapsedMilliseconds % 1000 == 0)
                        {
                            // メイン処理
                            // カウントアップするのみ
                            MainMethod();
                            // コントロールを更新する
                            AppendTextToControl("AppendText " + Count + "\n");
                        }
                    }
                    // 定期的に動いているか示す用 (10秒間隔)
                    if (sw.ElapsedMilliseconds % (10 * 1000) == 0)
                    {
                        if (!swFlag) { _err.AddLog("SubThread Working : IsExcute="+IsExcute); swFlag = true; }
                    }
                    else { swFlag = false; }

                    // 終了フラグ true でループを終了する
                    if (IsExit) { _err.AddLog("SubThread Loop Exit"); break; }
                }
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "Excute");
            } finally
            {
                _err.AddLog("Excute Finally");
            }
        }

        public void AppendTextToControl(string value)
        {
            try
            {
                //_err.AddLog(this, "AppendTextToControl");
                string buf = value.Replace('\n', '\\');
                _err.AddLog(this,"AppendTextToControl :  value = " + buf);
                if (ControlForAsync != null)
                {
                    ControlForAsync.AppendTextToControlBySubThread(value);
                } else
                {
                    _err.AddLog(this, "ControlForAsync == null");
                }
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "AppendTextToControl");
            }
        }

        public void MainMethod()
        {
            try
            {
                Count++;
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "MainMethod");
            }
        }

        public void CloseDoLoop()
        {
            try
            {
                IsExcute = false;
                IsExit = true;//終了要求
                if (LoopThread != null)
                {
                    LoopThread.Join();//実際に終了するのを待つ
                    LoopThread = null;
                }
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "CloseDoLoop");
            }
        }
    }
}
