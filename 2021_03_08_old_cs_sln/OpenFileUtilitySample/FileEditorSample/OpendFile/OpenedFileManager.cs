using CommonUtility.FileUtility.OpendFileUtility;
using ErrorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility.FileUtility
{
    public class OpenedFileManager
    {
        protected ErrorManager _err;
        public readonly  OpenedFile openedFile;

        public OpenedFileManager(ErrorManager err,OpenedFile openedFile)
        {
            _err = err;
            this.openedFile = openedFile;
        }


        public void SetFilePath()
        {
            try
            {
                int ret = openedFile.SetPathFromDilog();
                if (ret < 1) { throw new Exception(ErrorConstatns.ErrorMessages[(int)ErrorCodes.FILEOPEN_FAILED]); }
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "SetFilePath");
            }
        }


    }
}
