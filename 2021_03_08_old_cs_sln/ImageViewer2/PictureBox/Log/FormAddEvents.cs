using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log
{
    public class FormAddEvents
    {
        private Form1 _form;
        public bool Loading = false;
        public bool FirlsPaint = true;


        //イベントハンドラのデリゲートを定義
        //public event PaintEventHandler form_Paint;


        public FormAddEvents(Form1 form)
        {
            _form = form;
            _form.Paint += Form_Paint;
        }

        private void Form_Paint(object sender,EventArgs e)
        {
            if (FirlsPaint)
            {
                _form._log.FollowParentWindow();
                FirlsPaint = false;
            }

        }

    }

}
