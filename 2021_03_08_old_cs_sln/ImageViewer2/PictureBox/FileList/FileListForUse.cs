using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLog;

namespace FileList
{
    public class FileListForUse
    {
        ErrorLog.IErrorLog _errorLog;
        protected List<string> _fileList;
        protected List<int> _randomList;
        protected List<int> _indexList;
        public int NowIndex = 0;
        protected bool IsRandom = false;
        public bool IsLoopList = true;
        public FileListForUse()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
            initialize();
        }
        public FileListForUse(List<string> list)
        {
            //_fileList = list;
            initialize();
            this.FileList = list;
        }

        private void initialize()
        {
            try
            {
                _randomList = new List<int>();
                _indexList = new List<int>();
            }
            catch (Exception ex) { _errorLog.addException(ex, this.ToString(), "initialize Failed"); return; }
        }
        public int setErrorLog(ErrorLog.IErrorLog errorLog)
        {
            try
            {
                if (errorLog is ErrorLog.ErrorLog)
                {
                    _errorLog = errorLog;
                    return 1;
                }
                else { return -1; }
            }
            catch (Exception ex) { _errorLog.addException(ex, this.ToString(), "setErrorLog Failed"); return 0; }
        }

        // MoveNext
        public void MoveNext()
        {
            if (_fileList == null) { return; }
            if (NowIndex >= _fileList.Count - 1)
            {
                NowIndex = 0;
            } else
            {
                NowIndex++;
            }
        }
        // MovePrevious
        public void MovePrevious()
        {
            if (_fileList == null) { return; }
            if (NowIndex <= 0)
            {
                NowIndex = _fileList.Count -1;
            } else
            {
                NowIndex--;
            }
        }
        // GetNowValue
        public string GetNowValue()
        {
            try
            {
                if (_fileList == null) { return""; }
                if (!IsRandom)
                {
                    return _fileList[NowIndex];
                } else
                {
                    return _fileList[_randomList[NowIndex]];
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "GetNowValue Failed");
                return "";
            }
        }



        public List<string> FileList
        {
            get { return _fileList; }
            set
            {
                try
                {
                    _indexList = new List<int>();
                    _fileList = value;
                    if ((_fileList != null) && (_fileList.Count > 0)) {
                        int ret = resetListOrder();
                        if (ret < 1) { _errorLog.addErrorNotException(this.ToString(), "FileList Property:resetListOrder"); return; }
                    }
                    NowIndex = 0;

                } catch (Exception ex)
                {
                    _errorLog.addException(ex, this.ToString(), "FileList Property");
                }
            }
        }

        public List<string> getList()
        {
            try
            {
                if ((_fileList == null) || (_randomList == null)) { return _fileList; }

                if (IsRandom)
                {
                    List<string> retList = new List<string>();
                    for (int i = 0; i < _fileList.Count; i++)
                    {
                        int now = _randomList[i];
                        retList.Add(_fileList[now]);
                    }
                    return retList;
                } else
                {
                    return _fileList;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getListRandom");
                return _fileList;
            }
        }

        private List<string> makeList()
        {
            try
            {
                return _fileList;

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "makeList");
                return _fileList;
            }
        }

        public int switchOrderToRandomOrCorrect()
        {
            try
            {
                int ret=0;
                if (IsRandom)
                {
                    IsRandom = false;
                    return 1;
                }
                else
                {
                    ret = ListOrtderToRandom();
                }
                return ret;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "switchOrderToRandomOrCorrect");
                return 0;
            }
        }

        public int ListOrtderToRandom()
        {
            try
            {
                IsRandom = true;
                _randomList = new MyRandom().ListToRandom(_indexList);
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "ListOrtderToRandom");
                return 0;
            }
        }
        private int resetListOrder()
        {
            try
            {
                for(int i = 0; i < _fileList.Count; i++)
                {
                    _indexList.Add(i);
                }
                if (_indexList.Count != _fileList.Count)
                {
                    _errorLog.addErrorNotException("List Count Is Difference");
                }
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "makeIndexList");
                return 0;
            }
        }

    }
}
