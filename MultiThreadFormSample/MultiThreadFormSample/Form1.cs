using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiThreadFormSample
{
    public partial class Form1 : Form
    {
        Thread thread;
        bool IsClosing = false;
        bool buttonFlag = false;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            Console.WriteLine("Thread=" + Thread.CurrentThread.ManagedThreadId);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // サブスレッドは終了させる
                thread.Abort();
                thread.Join();
                IsClosing = true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
            count = 0;
            label1.Text = "";
            /*
             * Form_Load時に、カウントを継続するループ処理を実行開始しておく（アプリ終了まで実行される）
             * 以下の最初のUpdateTextは表示リセット用
             * ボタンを押下して、flag=Trueの時にだけ、カウントあを増やして、それをUpdateTextでフォームに表示している
             * ループ内の待機とフラグによる処理実行を、非同期で行うテスト用
             */
            // スレッドを生成して開始する
            thread = new Thread(new ThreadStart(ThreadProc));
            thread.Start();
            UpdateText();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // サブスレッドは終了させる
            thread.Abort();
            thread.Join();
            // フォームを閉じてプログラムを終了します
            this.Close();
        }

        private int count;

        public delegate void DelegateUpdateText();

        private void UpdateText()
        {
            try
            {
                //label1.Text = string.Format("{0}", count);
                // メイン処理と同じスレッドなら別スレッドから呼び出し
                // 別スレッドならそのままこのUpdateTextを実行する
                // InvokeRequired で判定して、分岐させている
                if (this.InvokeRequired)
                {
                    if (IsClosing) { Console.WriteLine("UpdateText End");  return; }
                    this.Invoke(new DelegateUpdateText(this.UpdateText));
                    return;
                }
                label1.Text = string.Format("{0}", count);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {

            }
        }

        private void ThreadProc()
        {
            Console.WriteLine("SubThread=" + Thread.CurrentThread.ManagedThreadId);
            // サブスレッドの処理
            while (true)
            {
                if (buttonFlag)
                {
                    // カウンタの値をフォームに表示する
                    UpdateText();

                    count++;
                    if (count >= int.MaxValue)
                    {
                        count = int.MinValue;
                    }
                    System.Threading.Thread.Sleep(20);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonFlag = !buttonFlag;
            Console.WriteLine("butonflag = "+buttonFlag);
        }
    }
}
