using CommonUtility.FileUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFileUtilitySample
{
    public class OpenedFileEvent
    {
        protected ErrorUtility.ErrorManager _err;
        public OpenedFileEvent(ErrorUtility.ErrorManager err,OpenedFile openedFile)
        {
            _err = err;
            if(openedFile != null)
            {
                openedFile.ChangeFilePathEvent += OpenedFile_ChangeFilePathEvent;
            } else
            {
                _err.AddLogAlert(this,"openedFile is Null");
            }
        }

        private void OpenedFile_ChangeFilePathEvent(object sender, EventArgs e)
        {
            try
            {
                _err.AddLog("OpenedFile_ChangeFilePathEvent");
            } catch (Exception ex)
            {
                _err.AddLogAlert(this, ex, "OpenedFile_ChangeFilePathEvent");
            }
        }
    }
}
