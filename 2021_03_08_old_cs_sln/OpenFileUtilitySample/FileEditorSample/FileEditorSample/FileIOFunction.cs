using ErrorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEditorSample.FileEditorSample
{
    public class FileIOFunction
    {
        protected ErrorManager _err;
        public readonly FileEditorSampleForm fileEditorSampleForm;
        public FileIOFunction(ErrorManager err,FileEditorSampleForm fileEditorSampleForm)
        {
            _err = err;
            this.fileEditorSampleForm = fileEditorSampleForm;
        }

        public void OpenFile(object sender,EventArgs e)
        {
            try
            {
                _err.AddLog(this,"OpenFile");
                fileEditorSampleForm.openedFileManager.SetFilePath();
                fileEditorSampleForm.editControl.ReadFileAfterEvent(
                    fileEditorSampleForm.openedFileManager.openedFile.GetPath(), EventArgs.Empty);
            } catch (Exception ex)
            {
                _err.AddException(ex,this, "OpenFile Failed");
            }
        }

        public void SaveFile(object sender, EventArgs e)
        {
            try
            {
                _err.ClearError();
                _err.AddLog(this,"SaveFile");
                // データを取得する。
                string writeData = fileEditorSampleForm.editControl.GetData();
                if (_err.hasAlert) { throw new Exception("editControl.GetData Failed"); }
                if (!IsSaveAs)
                {
                    // 保存する
                    fileEditorSampleForm.openedFileManager.openedFile.SaveData(writeData, _saveAsDefaultFileName, 1);
                }
                else
                {
                    // 名前を付けて保存
                    fileEditorSampleForm.openedFileManager.openedFile.SaveAsData(writeData, _saveAsDefaultFileName, 1);
                }
                if (_err.hasAlert) { throw new Exception("SaveData or SaveAsData Failed : IsSaveAs=" + IsSaveAs); }
                _err.AddLog("SavePath:" + this.fileEditorSampleForm.openedFileManager.openedFile.GetPath());
                // 状態を変更
                AppsState.IsEdited = false;
                ResetEdited();

            }
            catch (Exception ex)
            {
                _err.AddException(ex, this.ToString() + ".SaveDataGridViewData");
            }
            finally
            {
                //if (isShowMsg) { if (_err.hasAlert) { _err.Messenger.ShowAlertMessages(); } }
                //else { _err.ClearError(); }
            }
        }

        public void SaveAs(object sender, EventArgs e)
        {
            try
            {
                _err.AddLog(this, "SaveAs");
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "SaveAs Failed");
            }
        }
        public void CloseForm(object sender, EventArgs e)
        {
            try
            {
                _err.AddLog(this, "CloseForm");
                fileEditorSampleForm.Close();
            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "CloseForm Failed");
            }
        }
    }
}
