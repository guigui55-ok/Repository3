using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormInfo
{
    public class HookMouseEvent : IDisposable
    {
        // フックIDを保持する変数
        private IntPtr _hookID = IntPtr.Zero;

        // マウスフックのコールバック関数
        private LowLevelMouseProc _mouseProc;

        public EventHandler<MouseEventArgs> MouseMovedEvent;
        // Track whether Dispose has been called.
        private bool _disposed = false;

        public HookMouseEvent()
        {
            //InitializeComponent();
            // フックのコールバックを設定
            _mouseProc = HookCallback;
        }

        // フォームがロードされた際にフックをセットする
        //private void MainForm_Load(object sender, EventArgs e)
        public void Initialize()
        {
            _hookID = SetHook(_mouseProc);
        }

        // フォームが閉じられた際にフックを解除する
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnlockHook();
        }

        public void UnlockHook()
        {
            Console.WriteLine(String.Format("UnhookWindowsHookEx(_hookID) = {0}", _hookID));
            UnhookWindowsHookEx(_hookID);
        }

        // フックを設定するメソッド
        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        // フックされた際に呼ばれるコールバック関数
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // マウスのイベントを検知
                if (wParam == (IntPtr)WM_LBUTTONDOWN)
                {
                    int x = Cursor.Position.X;
                    int y = Cursor.Position.Y;
                    Console.WriteLine(String.Format("Click_LeftButton_Down = {0}, {1}", x,y));
                }
                else if (wParam == (IntPtr)WM_MOUSEMOVE)
                {
                    //Console.WriteLine("MoveMouse");
                    MouseEventArgs e = new MouseEventArgs(MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0);
                    MouseMovedEvent?.Invoke(new object(), e);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        // フックの種類（グローバルマウスフック）
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_MOUSEMOVE = 0x0200;

        // コールバックデリゲートの定義
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        // WinAPI関数をインポート
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: Dispose managed resources here.
                    //_stream.Dispose();
                }

                // TODO: Free unmanaged resources here.
                //MyCloseHandle(_handle);
                //_handle = IntPtr.Zero;
                //UnhookWindowsHookEx(_hookID);
                UnlockHook();

                // Note disposing has been done.
                _disposed = true;
            }
        }
    }
}
