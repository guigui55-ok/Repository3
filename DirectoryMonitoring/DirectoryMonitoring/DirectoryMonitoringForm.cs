using DragAndDrop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderMonitoring
{
    public partial class DirectoryMonitoringForm : Form
    {
        ErrorManager.ErrorManager _err;
        // 何らかの処理を続けながら、コントロールにアクセスするクラス
        DoLoop _doLoop;
        // 非同期アクセスされるコントロールを持つクラス
        IHasControl _hasControl;
        // Control を非同期で使用するためのクラス
        ControlForAsync _controlForAsync;
        // Directory を監視するクラス
        DirectryMonitoring _directoryMonitoring;
        // DragAndDrop でディレクトリパスを入力するクラス
        DragAndDropOnControl _dragAndDropOnControl;
        DragAndDropForFile _dragAndDropForFile;
        public DirectoryMonitoringForm()
        {
            InitializeComponent();
            _err = new ErrorManager.ErrorManager(1);
            _doLoop = new DoLoop(_err);
            _hasControl = new HasControlRichTextBox(_err, this.richTextBox1);
            _controlForAsync = new ControlForAsync(_err, this, _hasControl);
            this.KeyDown += FolderMonitoringForm_KeyDown;
            _directoryMonitoring = new DirectryMonitoring(_err, _controlForAsync);
            _dragAndDropOnControl = new DragAndDropOnControl(_err, this);
            _dragAndDropOnControl.AddRecieveControls(
                new Control[] {richTextBox1,textBox1,textBox2,label1,label2,label3 }
             );
            _dragAndDropForFile = new DragAndDropForFile(_err, _dragAndDropOnControl);
            _dragAndDropForFile.DragAndDropEventAfterEventForFile += DragAndDropEventAfterEventForFile;
            textBox1.Text = "";
            textBox2.Text = "*.*";
            this.FormClosed += DirectoryMonitoringForm_FormClosed;

        }
        private void DragAndDropEventAfterEventForFile(object sender,EventArgs e)
        {
            try
            {
                _err.AddLog("  GetPath=" + _dragAndDropForFile.Files[0]);
                this.textBox1.Text = _dragAndDropForFile.Files[0];
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "DragAndDropEventAfterEventForFile");
                _err.ClearError();
            }
        }

        private void DirectoryMonitoringForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Visible = false;
                _directoryMonitoring.Close();

                if(richTextBox1.Text != "")
                {
                    string fileName = "dirmon_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
                    string filePath = System.IO.Directory.GetCurrentDirectory() + "\\" + fileName;
                    _err.AddLog(this, "WriteFile : path=" + filePath);
                    Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                    using (StreamWriter writer = new StreamWriter(filePath, true, sjisEnc))
                    {
                        writer.Write(richTextBox1.Text);
                    }
                }
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "DirectoryMonitoringForm_FormClosed");
                _err.ClearError();
            }
        }

        private void FolderMonitoringForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        _err.AddLog(this,"KeyDown = Keys.Enter");
                        break;
                    default:
                        break;

                }
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "FolderMonitoringForm_KeyDown");
                _err.ClearError();
            }
        }

        private void FolderMonitoringForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _err.AddLog("button1_Click");
                _directoryMonitoring = new DirectryMonitoring(_err, _controlForAsync);
                _directoryMonitoring.SetPath(textBox1.Text);
                if (_err.hasError) { return; }
                _directoryMonitoring.SetFilterForMonitorFile(textBox2.Text);
                if (_err.hasError) { return; }
                _directoryMonitoring.LoopExcute();

            } catch (Exception ex)
            {
                _err.AddException(ex, this, "button1_Click");
                _err.ClearError();
            } finally
            {
                if (_err.hasError)
                {
                    this.richTextBox1.AppendText(String.Join("\n", _err.GetLastErrorMessagesAsArray(4,true)) + "\n");
                    this.richTextBox1.ScrollToCaret();
                    _err.ClearError();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _err.AddLog("button2_Click");
            //_directoryMonitoring.IsLoopExcute = false;
            //_directoryMonitoring.LoopStop();
            //_directoryMonitoring.Close();
            try
            {
                _ = Task.Run(() =>
                {
                    try
                    {
                        _directoryMonitoring.AppendTextToControl("Monitoring Stop\n");
                        _directoryMonitoring.Close();
                    }
                    catch (OperationCanceledException ex)
                    {
                        Console.WriteLine("OperationCanceledException");
                        Console.WriteLine(ex.Message);
                    }
                    catch (ThreadAbortException ex)
                    {
                        _err.AddLogWarning(this.ToString(), "button2_Click" + ".Task ThreadAbortException : thread=" + Thread.CurrentThread.ManagedThreadId, ex);
                        //_err.AddException(ex, this, " ThreadAbortException");
                    }
                    catch (Exception ex)
                    {
                        _err.AddException(ex, this, "button2_Click.Task");
                    }
                });
                //_directoryMonitoring = null;
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "button2_Click");
                richTextBox1.Text = ex.Message + "\n" + ex.StackTrace + "\n";
            } finally
            {
                _err.AddLog("button2_Click Finally");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
