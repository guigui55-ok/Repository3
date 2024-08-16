using CommonUtility.FileUtility;
using ErrorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEditorSample
{
    public class EditControl
    {
        protected ErrorManager _err;
        protected RichTextBox _richTextBox;
        public EditControl(ErrorManager err,RichTextBox richTextBox) 
        {
            _err = err;
            _richTextBox = richTextBox;
        }

        public void ReadFileAfterEvent(object sender,EventArgs e)
        {
            try
            {
                _err.AddLog(this, "ReadFileAfterEvent");
                if (sender.GetType().Equals(typeof(string)))
                {
                    string readData = ReadFile((string)sender);
                    if (_err.hasAlert) { _err.AddLogAlert(" ReadFile Failed"); return; }
                    _richTextBox.Text = readData;
                }

            } catch (Exception ex)
            {
                _err.AddLogAlert(this, ex, "ReadFileAfterEvent Failed");
            }
        }

        public string GetData()
        {
            return _richTextBox.Text;
        }

        private string ReadFile(string path)
        {
            try
            {
                _err.AddLog("ReadFile");
                FileIO reader = new FileIO(_err);
                string data = "";
                data = reader.ReadFileAsString(path);
                return data;
            } catch (Exception ex)
            {
                _err.AddException(ex, this, "ReadFile Failed");
                return "";
            }
        }

        public void SetNewFile()
        {

        }
    }
}
