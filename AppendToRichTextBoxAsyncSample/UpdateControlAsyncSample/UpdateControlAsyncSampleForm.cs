using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateControlAsyncSample
{
    public partial class UpdateControlAsyncSampleForm : Form
    {
        ErrorManager.ErrorManager _err;
        RichTextBoxForSubThread _richTextBoxForSubThread;
        UpdateControlAsync _updateControlAsync;
        public UpdateControlAsyncSampleForm()
        {
            InitializeComponent();
            _err = new ErrorManager.ErrorManager(1);
            _richTextBoxForSubThread = new RichTextBoxForSubThread(_err, richTextBox1);
            _updateControlAsync = new UpdateControlAsync(_err, this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _richTextBoxForSubThread.AppendValue = "AppendValue\n";
            // 有効ではないスレッド間の操作: コントロールが作成されたスレッド以外のスレッドから
            // コントロール 'richTextBox1' がアクセスされました。
            //_richTextBoxForSubThread.AppendTextFromSubThread();
            // SubThreadから実行するAction
            _richTextBoxForSubThread.ActionForSubThread = _updateControlAsync.ExcuteUpdateControlBySubThread;
            // RichTextBoxをAppendするAction
            _updateControlAsync.UpdateControlAction = _richTextBoxForSubThread.AppendText;
            // RichTextBox.Append を SubThread から実行する
            _richTextBoxForSubThread.ExcuteActionFromSubThread();
        }

        private void UpdateControlAsyncSampleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
