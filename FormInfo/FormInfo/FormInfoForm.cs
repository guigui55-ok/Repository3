using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormInfo
{

    public partial class FormInfoForm : Form
    {
        public SubInfoForm _subForm = new SubInfoForm();
        public FormInfoData _info = new FormInfoData();
        public bool _isOn = false;

        public HookMouseEvent _hookMouseEvent = new HookMouseEvent();
        public FormInfoForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _hookMouseEvent.Initialize();
            _hookMouseEvent.MouseMovedEvent += FormInfoForm_MouseMove_OutSideForm;
        }

        private void FormInfoForm_Load(object sender, EventArgs e)
        {
            //Spaceキーで、キャプチャ開始・停止
            //Enterキーで更新

            //半透明にする
            //十字キーで1ピクセルずつ移動、Ctrlで10ピクセルずつ移動
            //LastEvent
            //LastMouseClick
            _isOn = true;
            this.Text = string.Format("FormInfo [Capture:{0}]", _isOn);
            this.Location = new Point(300, 75);
        }

        private void FormInfoForm_DoubleClick(object sender, EventArgs e)
        {
            _subForm.Show();
        }

        private void FormInfoForm_Move(object sender, EventArgs e)
        {
            SaveInfoThisAndShowInfoToSub();
        }


        public void SaveInfoThisAndShowInfoToSub(int isOnint=-1)
        {
            bool flag;
            if (isOnint < ConstFormInfo.FORCE_OFF)
            {
                flag = _isOn;
            }
            else
            {
                if (isOnint == ConstFormInfo.FORCE_OFF)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            if (flag)
            {
                SaveInfoThis();
                _subForm.outputInfoToTextBox(_info);
            }
        }

        public void SaveInfoThis()
        {
            SaveInfo(this);
        }

        public void SaveInfo(Form form)
        {
            _info._title = form.Text;
            _info._size = form.Size;
            _info._location = form.Location;
            _info._dispRectangle = form.DisplayRectangle;
            _info._backColor = form.BackColor;
        }

        private void FormInfoForm_MouseMove(object sender, MouseEventArgs e)
        {
            //_info._nowMousePoint = MousePosition;  //クライアント座標
            //_info._nowMousePoint = Cursor.Position;
            //SaveInfoThisAndShowInfoToSub();
        }

        private void FormInfoForm_MouseMove_OutSideForm(object sender, MouseEventArgs e)
        {
            _info._nowMousePoint = Cursor.Position;
            SaveInfoThisAndShowInfoToSub();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveInfoThisAndShowInfoToSub();
            _subForm.Show();
        }

        private void FormInfoForm_Click(object sender, MouseEventArgs e)
        {
            SaveInfoThisAndShowInfoToSub();
        }

        private void FormInfoForm_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(string.Format("KeyDown {0}", e.KeyCode.ToString()));
            if (e.KeyCode == Keys.A)
            {
                SaveInfoThisAndShowInfoToSub(ConstFormInfo.FORCE_ON);
            }
            else if(e.KeyCode == Keys.Space)
            {
                _isOn = !_isOn;
                this.Text = string.Format("FormInfo [Capture:{0}]", _isOn);
            }
        }

        private void FormInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _hookMouseEvent.UnlockHook();
        }
    }
    public class ConstFormInfo
    {
        public const int FORCE_OFF = 0;
        public const int FORCE_ON = 1;
    }

}
