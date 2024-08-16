using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ErrorLog;

namespace ViewImageAction.BaseForm
{
    public class MainFormManager
    {
        ErrorLog.IErrorLog _errorLog;
        Form _mainForm;
        MenuStripRegister _menuRegister;
        MenuStripEvents _menuEvents;
        public ViewImageAction.Settings.ImageViewerSettings Settings;
        public ViewImageAction.Function.CommonFunctions Functions;
        public MainFormState State;
        public MainFormManager(Form form)
        {
            _mainForm = form;
        }
        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }

        public int initialize()
        {
            try
            {
                int ret = 0;
                // MenuStrip
                ret = initMenuStrip();
                if (ret < 1)
                { _errorLog.addErrorNotException(this.ToString(),
                          "SetAppendMenuStripToForm failed"); }

                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegistList");
                return 0;
            }
        }

        private int initMenuStrip()
        {
            try
            {
                // SetErrorLog
                //GlobalErrloLog.ErrorLog = new ErrorLog.ErrorLog();
                //_errorLog = new ErrorLog.ErrorLog();
                // MenuStrip を生成
                _menuRegister = new MenuStripRegister(new MenuStrip());
                _menuRegister.setErrorLog(_errorLog);
                // リストの元を作る
                List<ToolStripLiistForRegist> listsRegist = MakeMenuNameList();

                // MenuStripに追加
                int ret = _menuRegister.registMenuToMenuStripFromToolStripLiistForRegistList(listsRegist);
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(),
                        "registMenuToMenuStripFromToolStripLiistForRegistList failed");
                }

                // イベントハンドラを追加する
                _menuEvents = new MenuStripEvents(_menuRegister.getMenuStrip());
                _menuEvents.Constants = new ViewImageAction.BaseForm.MainFormManager.Constants();
                _menuEvents.Functions = Functions;
                _menuEvents.MainFormState = State;
                ret = _menuEvents.RegistFunctionForMenuStripEvents();
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString(),
                        "RegistFunctionForMenuStripEvents failed");
                }

                // Form に MenuStrip を追加
                //this.Controls.Add(_menustripRegister.getMenuStrip());
                //_mainForm.menustrip = _menustripRegister.getMenuStrip();
                ret = _menuRegister.SetAppendMenuStripToForm(_mainForm);
                if (ret < 1)
                { _errorLog.addErrorNotException(this.ToString(),
                        "SetAppendMenuStripToForm failed"); }
                // Debug用
                //_menuRegister.IsExistsMenuStripInForm(_mainForm);

                // 設定値からVisibleを設定
                if (Settings.IsMenuBarVisibleOnStartUp)
                { 
                    _menuEvents.getMenuStrip().Visible = true;
                    State.IsVisibleMenuStrip = true;
                }
                else { 
                    _menuEvents.getMenuStrip().Visible = false;
                    State.IsVisibleMenuStrip = false;
                }

                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "initMenuStrip");
                return 0;
            }
        }

        private List<ToolStripLiistForRegist> MakeMenuNameList()
        {
            try
            {
                Constants constants = new Constants();
                Keys keys;
                // メニューリスト 2次元リスト
                List<ToolStripLiistForRegist> listsRegist = new List<ToolStripLiistForRegist>();
                // ファイルメニュー
                ToolStripLiistForRegist listRegist = new ToolStripLiistForRegist();

                // ファイルメニュー
                listRegist.Add(constants.MENU_FILE);
                keys = Keys.Control | Keys.O;
                listRegist.Add(constants.MENU_FILE_OPEN, keys);
                keys = Keys.Control | Keys.A;
                listRegist.Add(constants.MENU_FILE_SAVE_AS_NAME, keys);
                //listRegist.Add(constants.MENU_SEPALATOR);
                keys = Keys.Control | Keys.P;
                listRegist.Add(constants.MENU_FILE_PRINT, keys);
                keys = Keys.Control | Keys.X;
                listRegist.Add(constants.MENU_FILE_EXIT, keys);
                // Add_Clear
                listsRegist.Add(listRegist.getListCopied());
                listRegist.Clear();

                // 表示メニュー
                listRegist.Add(constants.MENU_DISPLAY);
                keys = Keys.Right;
                listRegist.Add(constants.MENU_DISPLAY_FOWARD, keys);
                keys = Keys.Left;
                listRegist.Add(constants.MENU_DISPLAY_REVERSE, keys);
                keys = Keys.V;
                listRegist.Add(constants.MENU_DISPLAY_MENUBAR_VISIBLE, keys);
                // Add_Clear
                listsRegist.Add(listRegist.getListCopied());
                listRegist.Clear();

                // 設定メニュー
                listRegist.Add(constants.MENU_SETTINGS);
                keys = Keys.Control | Keys.O;
                listRegist.Add(constants.MENU_SETTINGS_OPTION, keys);
                // Add_Clear
                listsRegist.Add(listRegist.getListCopied());
                listRegist.Clear();

                // ヘルプメニュー
                listRegist.Add(constants.MENU_HELP);
                keys = Keys.Control | Keys.H;
                listRegist.Add(constants.MENU_HELP_HELP, keys);
                keys = Keys.Control | Keys.V;
                listRegist.Add(constants.MENU_HELP_VERSION, keys);
                listsRegist.Add(listRegist.getListCopied());

                return listsRegist;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "MakeMenuNameList");
                return null;
            }
        }
        public class Constants
        {
            public readonly string MENU_FILE = "ファイル(&F)";
            public readonly string MENU_FILE_OPEN = "開く(&O)...";
            public readonly string MENU_FILE_SAVE_AS_NAME = "名前を付けて保存(&A)...";
            public readonly string MENU_SEPALATOR = "_MenuSepalator";
            public readonly string MENU_FILE_PRINT = "印刷(&P)";
            public readonly string MENU_FILE_EXIT = "終了(&X)";
            public readonly string MENU_DISPLAY = "表示(&D)";
            public readonly string MENU_DISPLAY_FOWARD = "前へ(&P)";
            public readonly string MENU_DISPLAY_REVERSE = "前へ(&R)";
            public readonly string MENU_DISPLAY_MENUBAR_VISIBLE = "メニューバーを表示する";
            public readonly string MENU_SETTINGS = "設定(&S)";
            public readonly string MENU_SETTINGS_OPTION = "オプション(&O)";
            public readonly string MENU_HELP = "ヘルプ(&H)";
            public readonly string MENU_HELP_HELP = "ヘルプ(&H)";
            public readonly string MENU_HELP_VERSION = "バージョン情報(&V)";
        }
    }
}
