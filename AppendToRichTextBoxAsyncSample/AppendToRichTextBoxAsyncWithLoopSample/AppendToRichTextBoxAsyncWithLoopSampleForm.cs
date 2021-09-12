using ErrorManager;
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

namespace AppendToRichTextBoxAsyncWithLoopSample
{
    public partial class AppendToRichTextBoxAsyncWithLoopSampleForm : Form
    {
        ErrorManager.ErrorManager _err;
        // 何らかの処理を続けながら、コントロールにアクセスするクラス
        DoLoop _doLoop;
        // 非同期アクセスされるコントロールを持つクラス
        IHasControl _hasControl;
        // Control を非同期で使用するためのクラス
        ControlForAsync _controlForAsync;
        public AppendToRichTextBoxAsyncWithLoopSampleForm()
        {
            InitializeComponent();
            _err = new ErrorManager.ErrorManager(1);
            _doLoop = new DoLoop(_err);
            _hasControl = new HasControlRichTextBox(_err, this.richTextBox1);
            _controlForAsync = new ControlForAsync(_err, this, _hasControl);
            _doLoop.ControlForAsync = _controlForAsync;
            this.FormClosed += AppendToRichTextBoxAsyncWithLoopSampleForm_FormClosed;
            this.FormClosing += AppendToRichTextBoxAsyncWithLoopSampleForm_FormClosing;
        }

        private void AppendToRichTextBoxAsyncWithLoopSampleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // DoLoopMethod 終了まで表示されたままとなるため、Visible で見かけ上終了させる
            this.Visible = false;
        }

        private void AppendToRichTextBoxAsyncWithLoopSampleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if(_doLoop != null)
                {
                    // Loop 処理を終了し、Loop の Thread も破棄する
                    _doLoop.CloseDoLoop();
                }
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "AppendToRichTextBoxAsyncWithLoopSampleForm_FormClosed");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                _err.AddLog(this, "button1_Click");
                _doLoop.ExcuteAsync();
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "button1_Click");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _err.AddLog(this, "_doLoop.IsExcute = false");
                _doLoop.IsExcute = false;
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "button2_Click");
            }
        }
        private void AppendToRichTextBoxAsyncWithLoopSampleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
