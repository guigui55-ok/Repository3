using MenuStripSample;
using System;
using System.Windows.Forms;

namespace ViewImageAction
{
    public class ViewImageMenuStripEvents
    {
        public CommonFunctions Functions;
        public ViewImageAction.BaseForm.MainFormManager.Constants Constants;
        public ViewImageAction.BaseForm.MainFormState MainFormState;
        //
        ErrorLog.IErrorLog _errorLog;
        MenuStrip _menuStrip;
        MenuStripRegister _register;
        private bool IsVisible = true;

        public ViewImageMenuStripEvents(MenuStrip menuStrip)
        {
            _menuStrip = menuStrip;
            _register = new MenuStripRegister(_menuStrip);
        }

        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }
        public MenuStrip getMenuStrip() { return _menuStrip; }
        // 実行したい機能のクラス
        //public TestFunction TestFunction;

        public int RegistFunctionForMenuStripEvents()
        {
            try
            {
                ToolStripMenuItem item;
                // 機能クラスを読み込んでおく　外部で設定する
                //TestFunction = new TestFunction();
                // 定数クラスを読み込んでおく 外部で設定する
                //Constants = new MenuStripSampleForm.Constants();

                // イベントをセット
                _menuStrip.Paint += MenuStrip_Paint;

                // メニューイベントをセット
                // OpenFile
                item = _register.getToolStripMenuItemFromText(Constants.MENU_FILE, Constants.MENU_FILE_OPEN);
                item.Click += MenuStrip_OpenFile;
                // SaveAsName
                item = _register.getToolStripMenuItemFromText(Constants.MENU_FILE, Constants.MENU_FILE_SAVE_AS_NAME);
                item.Click += MenuStrip_SaveAsName;
                // Print
                item = _register.getToolStripMenuItemFromText(Constants.MENU_FILE, Constants.MENU_FILE_PRINT);
                item.Click += MenuStrip_Print;
                // Exit
                item = _register.getToolStripMenuItemFromText(Constants.MENU_FILE, Constants.MENU_FILE_EXIT);
                item.Click += MenuStrip_Exit;
                // DisplayFoward
                item = _register.getToolStripMenuItemFromText(Constants.MENU_DISPLAY, Constants.MENU_DISPLAY_FOWARD);
                item.Click += MenuStrip_DisplayFoward;
                // DisplayReverse
                item = _register.getToolStripMenuItemFromText(Constants.MENU_DISPLAY, Constants.MENU_DISPLAY_REVERSE);
                item.Click += MenuStrip_DisplayReverse;
                // DisplayMenuVisible
                item = _register.getToolStripMenuItemFromText(Constants.MENU_DISPLAY, Constants.MENU_DISPLAY_MENUBAR_VISIBLE);
                item.Click += MenuStrip_MenuVisible;
                // SettingsOption
                item = _register.getToolStripMenuItemFromText(Constants.MENU_SETTINGS, Constants.MENU_SETTINGS_OPTION);
                item.Click += MenuStrip_SettingsOption;
                // ShowHelp
                item = _register.getToolStripMenuItemFromText(Constants.MENU_HELP, Constants.MENU_HELP_HELP);
                item.Click += MenuStrip_ShowHelp;
                // ShowVersion
                item = _register.getToolStripMenuItemFromText(Constants.MENU_HELP, Constants.MENU_HELP_VERSION);
                item.Click += MenuStrip_ShowVersion;
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "RegistFunctionForMenuStripEvents");
                return 0;
            }
        }

        private void MenuStrip_Paint(object sender, EventArgs e)
        {
            if (!MainFormState.IsVisibleMenuStrip)
            {
                //Debug.WriteLine("MenuStrip_Paint State Visible  false -> true");
                // false -> true
                // コントロールを変化させるタイミング
                MainFormState.IsVisibleMenuStrip = true;
                //Debug.WriteLine("Height = " + _menuStrip.Height);
            }
            else
            {
                //Debug.WriteLine("MenuStrip_Paint State Visible true ");
                //Debug.WriteLine("Height = " + _menuStrip.Height);
            }
        }
        private void MenuStrip_OpenFile(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_OpenFile");
        }
        private void MenuStrip_SaveAsName(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_SaveAsName");
        }
        private void MenuStrip_Print(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_Print");
        }
        private void MenuStrip_Exit(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_Exit");
        }
        private void MenuStrip_DisplayFoward(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_DisplayFoward");
        }
        private void MenuStrip_DisplayReverse(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_DisplayReverse");
        }
        private void MenuStrip_MenuVisible(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_MenuVisible");
        }
        private void MenuStrip_SettingsOption(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_SettingsOption");
        }
        private void MenuStrip_ShowHelp(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_ShowHelp");
        }
        private void MenuStrip_ShowVersion(object sender, EventArgs e)
        {
            Functions.testFunction("MenuStrip_ShowVersion");
        }
    }
}
