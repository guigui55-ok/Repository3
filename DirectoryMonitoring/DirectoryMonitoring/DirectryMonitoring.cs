using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FolderMonitoring
{
    public class DirectryMonitoring
    {
        protected ErrorManager.ErrorManager _err;
        protected System.IO.FileSystemWatcher _watcher;
        protected ControlForAsync _controlForAsync;

        public bool IsLoopExcute = false;
        public bool IsLoopExit = false;
        protected Thread LoopThread;
        protected string _changedLastLog;
        // 非同期処理をCancelするためのTokenを取得.
        CancellationTokenSource _tokenSource = new CancellationTokenSource();
        CancellationToken _cancelToken;
        Task _task;

        public DirectryMonitoring(ErrorManager.ErrorManager err,ControlForAsync controlForAsync)
        {
            _err = err;
            _controlForAsync = controlForAsync;
            _watcher = new System.IO.FileSystemWatcher();
            Initialize();
            CancellationToken cancelToken = _tokenSource.Token;
        }

        public void Close()
        {
            try
            {
                if(_task != null)
                {
                    if (!_task.IsCompleted)
                    {
                        // タスクをキャンセルする
                        _err.AddLog(this, "Close" + ".tokenSource.Cance. task.IsCompleted=" + _task.IsCompleted);
                        _tokenSource.Cancel();
                        // キャンセルされたらTaskを終了する.
                        _err.AddLog(" Task Canceled");
                    }
                    if (_cancelToken.IsCancellationRequested)
                    {
                    }
                }

                IsLoopExcute = false;
                IsLoopExit = true; // 終了要求
                if (LoopThread != null)
                {
                    LoopThread.Abort();
                    LoopThread.Join(); // 実際に終了するのを待つ
                    LoopThread = null;
                    _err.AddLog("LoopThread = null");
                } else
                {
                    _err.AddLog("LoopThread != null");
                }
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "Close");
            }
        }
        private void Initialize()
        {
            try
            {
                //ファイル名とディレクトリ名と最終書き込む日時の変更を監視
                _watcher.NotifyFilter =
                    System.IO.NotifyFilters.FileName
                    | System.IO.NotifyFilters.DirectoryName
                    | System.IO.NotifyFilters.LastWrite;
                //サブディレクトリは監視しない
                _watcher.IncludeSubdirectories = false;
                //必要に応じてバッファサイズを変更
                //_wather.InternalBufferSize = 4096
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "Initialize");
            }
        }
        public void SetPath(string path)
        {
            try
            {
                if (System.IO.Directory.Exists(path))
                {
                    _watcher.Path = path;
                    _err.AddLog(this,"SetPath ="+path);
                } else
                {
                    throw new Exception("Directory Not Exists. path=" + path);
                    //_err.AddLogAlert(this,"Directory Not Exists. path="+path);
                }
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "SetPath");
            }
        }
        public void SetFilterForMonitorFile(string filter)
        {
            _watcher.Filter = filter;
        }

        public void LoopStop()
        {
            AppendTextToControl("Monitor Stop\n");
            IsLoopExcute = false;
        }

        public void LoopExcute()
        {
            try
            {
                _err.AddLog(this, "LoopExcute");
                if (LoopThread == null)
                {
                    _cancelToken = _tokenSource.Token;
                    _task = Task.Run(() =>
                    {
                        try
                        {
                            AppendTextToControl("Monitor Start\n");
                            LoopThread = Thread.CurrentThread;
                            LoopMain();
                        }
                        catch (OperationCanceledException ex)
                        {
                            Console.WriteLine("OperationCanceledException");
                            Console.WriteLine(ex.Message);
                        }
                        catch (ThreadAbortException ex)
                        {
                            _err.AddLogWarning(this.ToString(), "LoopExcute" + ".Task ThreadAbortException : thread=" + Thread.CurrentThread.ManagedThreadId, ex);
                            //_err.AddException(ex, this, " ThreadAbortException");
                        }
                        catch (Exception ex)
                        {
                            _err.AddException(ex, this, "LoopExcute Task");
                        }
                    }, _tokenSource.Token);
                    _err.AddLog(this, "LoopThread == null");
                }
                else
                {
                    if (IsLoopExcute)
                    {
                        // 多重起動しようとしている
                        _err.AddLog(this, "LoopMethod Dupulicate Excution Error");
                    }
                    else
                    {
                        // Loop停止状態から再開
                        AppendTextToControl("Monitor ReStart\n");
                        IsLoopExcute = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "LoopExcute");
            }
        }

        private void LoopMain()
        {
            try
            {
                _err.AddLog(this, "LoopMain");
                bool swFlag = false; // Stopwach 定期出力用
                Stopwatch sw = new Stopwatch();
                sw.Start();
                IsLoopExcute = true;
                while (true)
                {
                    if (IsLoopExcute)
                    {
                        // メイン処理
                        // カウントアップするのみ
                        Excute();
                        // コントロールを更新する
                        AppendTextToControl(_changedLastLog + "\n");
                    }
                    // 定期的に動いているか示す用 (10秒間隔)
                    if (sw.ElapsedMilliseconds % (10 * 1000) == 0)
                    {
                        if (!swFlag) { _err.AddLog("SubThread Working : IsExcute=" + IsLoopExcute); swFlag = true; }
                    }
                    else { swFlag = false; }

                    // 終了フラグ true でループを終了する
                    if (IsLoopExit) { _err.AddLog("SubThread Loop Exit"); break; }
                }
            }
            catch (Exception ex)
            {
                //_err.AddException(ex, this, "LoopMain");
                _err.AddLogAlert(this, "LoopMain", "Error:DirectoryMonitoring.LoopMain", ex);
            }
            finally
            {
                _err.AddLog("Excute Finally");
            }
        }

        public void Excute()
        {
            try
            {
                _err.AddLog(this,"Excute Monitoring");
                //同期的に監視を開始する
                System.IO.WaitForChangedResult changedResult =
                    _watcher.WaitForChanged(System.IO.WatcherChangeTypes.All);

                if (changedResult.TimedOut)
                {
                    _err.AddLogAlert("タイムアウトしました。");
                    return;
                }
                string ret = "";
                //変更があったときに結果を表示する
                switch (changedResult.ChangeType)
                {
                    case System.IO.WatcherChangeTypes.Changed:
                        ret = "[Changed] " + _watcher.Path + " [" + changedResult.Name + "]";
                        break;
                    case System.IO.WatcherChangeTypes.Created:
                        ret = "[Created] " + _watcher.Path + " [" + changedResult.Name + "]";
                        break;
                    case System.IO.WatcherChangeTypes.Deleted:
                        ret = "[Deleted] " + _watcher.Path + " [" + changedResult.Name + "]";
                        break;
                    case System.IO.WatcherChangeTypes.Renamed:
                        ret = "[Renamed] " + _watcher.Path + " ["+ changedResult.OldName + "] =>"  +" [" + changedResult.Name + "]";
                        break;
                }
                ret = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss FFFFFFF ") + ret;
                _err.AddLog("  " + ret);
                _changedLastLog = ret;
            } catch (Exception ex)
            {
                //_err.AddException(ex, this, "Excute");
                _err.AddLogAlert(this,"Excute","Error:DirectoryMonitoring.Excute Failed",ex);
            }
        }
        public void AppendTextToControl(string value)
        {
            try
            {
                string buf = value.Replace("\n", "");
                _err.AddLog(this, "AppendTextToControl :  value = " + buf);
                if (_controlForAsync != null)
                {
                    _controlForAsync.AppendTextToControlBySubThread(value);
                }
                else
                {
                    _err.AddLog(this, "ControlForAsync == null");
                }
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "AppendTextToControl");
            }
        }
    }
}
