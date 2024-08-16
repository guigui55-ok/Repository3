using System;
using System.Collections.Generic;
using ErrorLog;

namespace FileList
{
    class TestReadList
    {
        ErrorLog.IErrorLog _errorLog;
        FileList _fileList;
        FileListForUse _fileListuse;

        public TestReadList()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
            _fileList = new FileList();
            _fileListuse = new FileListForUse();
        }

        public int setErrorLog(ErrorLog.ErrorLog errorLog)
        {
            try { _errorLog = errorLog; return 1; }
            catch(Exception ex) { _errorLog.addException(ex, this.ToString(), "setErrorLog"); return 0; }
        }

        public int excute(
            string[] list ,
            List<string> includeTypeList,
            List<string> notIncludeTypeList,
            List<string> includeFileNameList,
            List<string> notIncludeFileNameList)
        {
            try
            {
                int ret = _fileList.setErrorLog(_errorLog);
                if (ret < 1) {
                    _errorLog.addErrorNotException(this.ToString(), "excute : setErrorLog Failed");
                    return -1; 
                }

                _fileList.IncludeFileTypeList = includeTypeList;
                _fileList.NotIncludeFileTypeList = notIncludeTypeList;
                _fileList.IncludeFileNameList = includeFileNameList;
                _fileList.NotIncludeFileNameList = notIncludeFileNameList;

                ret = _fileList.setFileList(new List<string>(list));
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "excute : setFileList Failed");
                    return -1;
                }

                ret = setListToUse();
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "excute : setListToUse Failed");
                    return -1;
                }

                if (_errorLog.haveError())
                {
                    _errorLog.ShowErrorMessageAndClearError();
                }
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "excute");
                return 0;
            }
        }

        public List<string> getList()
        {
            return _fileListuse.getList();
        }

        public int setListToUse()
        {
            try
            {
                int ret = _fileListuse.setErrorLog(_errorLog);
                _fileListuse.FileList = _fileList.getList();
                if (_errorLog.haveError())
                {
                    return -1;
                }
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "excute");
                return 0;
            }
        }


        public int switchOrderToRandomOrCorrect()
        {
            try
            {
                int ret = _fileListuse.switchOrderToRandomOrCorrect();
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(), "switchOrderToRandomOrCorrect Failed");
                    return -1;
                }
                return 1;

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "switchOrderToRandomOrCorrect");
                return 0;
            }
        }
    }
}
