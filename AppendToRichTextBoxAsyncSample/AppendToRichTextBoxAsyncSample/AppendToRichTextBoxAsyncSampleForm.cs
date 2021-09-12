using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppendToRichTextBoxAsyncSample
{
    public partial class AppendToRichTextBoxAsyncSampleForm : Form
    {
        ErrorManager.ErrorManager _err;
        // ⑤ コントロールに渡すための変数を実装する
        string ToRichtextValue;
        int n = 0;
        // ② SubThread を実装する
        Thread subThread;
        public AppendToRichTextBoxAsyncSampleForm()
        {
            InitializeComponent();
            _err = new ErrorManager.ErrorManager(1);
        }

        // ③ 非同期でコントロールにアクセスするための delegate を実装する
        public delegate void DelegateAppendTextToRichTextBoxBySubThread();

        // ④ 非同期でコントロールにアクセスするための Method を実装する
        private void AppendTextToRichTextBoxBySubThread()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    _err.AddLog("InvokeRequired=true");
                    this.Invoke(new DelegateAppendTextToRichTextBoxBySubThread(this.AppendTextToRichTextBoxBySubThread));
                    return;
                }
                this.richTextBox1.AppendText(ToRichtextValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ① 非同期処理を実装する
            try
            {
                Task task = Task.Run(() =>
                {
                    try
                    {
                        n++;
                        //richTextBox1.AppendText("AppendText" + n); // 以下のエラーとなる
                        //有効ではないスレッド間の操作: コントロールが作成されたスレッド以外のスレッドからコントロール 'richTextBox1' がアクセスされました。
                        ToRichtextValue = "AppendText" + n + "\n";
                        AppendTextToRichTextBoxBySubThread();
                    } catch (Exception ex)
                    {
                        _err.AddException(ex, this, "button1_Click Task");
                    }
                });
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "button1_Click");
            }
        }

        private void AppendToRichTextBoxAsyncSampleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
