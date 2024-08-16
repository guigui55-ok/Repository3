using CommonUtility.FileUtility;
using CommonUtility.FileUtility.OpendFileUtility;
using ErrorUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Constants = CommonUtility.FileUtility.OpendFileUtility.Constants;

namespace OpenFileUtilitySample
{
    public partial class OpenFileUtioitySampleForm : Form
    {
        protected ErrorManager _err;
        protected IErrorMessenger _errorMessenger;
        protected OpenedFile _openedFile;
        

        public OpenFileUtioitySampleForm()
        {
            InitializeComponent();
            _err = new ErrorManager(1);
            _openedFile = new OpenedFile(_err);
            _errorMessenger = new ErrorMessengerMessageBox(_err);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetFilePath();
        }

        private void SetFilePath()
        {
            try
            {
                int ret = _openedFile.SetPathFromDilog();
                if (ret < 1) { throw new Exception(ErrorConstatns.ErrorMessages[(int)ErrorCodes.FILEOPEN_FAILED]); }
                if (_err.hasAlert) { _errorMessenger.ShowAlertMessages(); }

                textBox1.Text = _openedFile.GetPath();
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "SetFilePath");
            }
        }
    }
}
