using ErrorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEditorSample.Menu
{
    public class MenuStripEvent
    {
        protected ErrorManager _err;
        protected MenuStripManager _menuStripManager;
        protected Form _mainForm;
        public readonly FileEditorSampleForm fileEditorSampleForm;
        public MenuStripEvent(ErrorManager err,MenuStripManager menuStripManager,Form form,FileEditorSampleForm fileEditorSampleForm)
        {
            _err = err;
            _menuStripManager = menuStripManager;
            _mainForm = form;
            this.fileEditorSampleForm = fileEditorSampleForm;
        }

        public void InitializeMenuStrip()
        {
            try
            {
                _err.AddLog(this, "InitializeMenuStrip");
                if(_menuStripManager == null) { _menuStripManager = new MenuStripManager(_err); }                
                _menuStripManager.Initialize(this._mainForm);
                if (_err.hasError) { _err.ReleaseErrorState(); }
                // MenuStrip の Event 紐づけ
                string[] tempary;
                //ToolStripMenuItem item;
                // ファイルを開く
                tempary = new string[] { _menuStripManager.Constants.FILE_MENU, _menuStripManager.Constants.FILE_OPEN };
                _menuStripManager.AddEventToMenu(tempary, fileEditorSampleForm.fileIOFunction.OpenFile);
                // 上書き保存
                tempary = new string[] { _menuStripManager.Constants.FILE_MENU, _menuStripManager.Constants.FILE_APPEND };
                _menuStripManager.AddEventToMenu(tempary, fileEditorSampleForm.fileIOFunction.SaveFile);
                // 名前を付けて保存
                tempary = new string[] { _menuStripManager.Constants.FILE_MENU, _menuStripManager.Constants.FILE_SAVEAS };
                _menuStripManager.AddEventToMenu(tempary, fileEditorSampleForm.fileIOFunction.SaveAs);
                // 終了
                tempary = new string[] { _menuStripManager.Constants.FILE_MENU, _menuStripManager.Constants.FILE_EXIT };
                _menuStripManager.AddEventToMenu(tempary, fileEditorSampleForm.fileIOFunction.CloseForm);
                // 設定
                //tempary = new string[] { _menuStripManager.Constants.OPTION_MENU, _menuStripManager.Constants.OPTION_SETTINGS };
                //MenuStripManager.AddEventToMenu(tempary, _excelCellsManagerMainEvent.ShowSettingsWindowEvent);

            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "InitializeMenuStrip");
            }
        }
    }
}
