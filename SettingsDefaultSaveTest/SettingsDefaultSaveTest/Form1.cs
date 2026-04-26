using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsDefaultSaveTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ※プロパティ設定があり、書き込み処理ありの場合

            // 変更前の値を確認（DebugConsoleに出力）
            Debug.WriteLine("変更前のUserName: " + Properties.Settings.Default.UserName);

            // ユーザ設定のUserNameに現在日時を保存する
            Properties.Settings.Default.UserName =
                DateTime.Now.ToString();
            
            // 変更後の値を確認（DebugConsoleに出力）
            Debug.WriteLine("変更後のUserName: " + Properties.Settings.Default.UserName);

            Properties.Settings.Default.Save();

            MessageBox.Show("Save完了");
        }
    }
}
