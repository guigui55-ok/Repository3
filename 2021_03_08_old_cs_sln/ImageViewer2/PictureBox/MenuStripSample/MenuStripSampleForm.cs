using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorLog;
using ViewImageAction;

namespace MenuStripSample
{
    public partial class MenuStripSampleForm : Form
    {
        ErrorLog.IErrorLog _errorLog;
        MenuStripRegister _menustripRegister;
        MenuStripEvents _menuStripEvents;
        public MenuStripSampleForm()
        {

            InitializeComponent();

            //if (false)
            //{
            //    testAddMenu();
            //    return;
            //}
            // SetErrorLog
            GlobalErrloLog.ErrorLog = new ErrorLog.ErrorLog();
            _errorLog = new ErrorLog.ErrorLog();
            // MenuStrip を生成
            _menustripRegister = new MenuStripRegister(new MenuStrip());
            _menustripRegister.setErrorLog(_errorLog);
            // リストの元を作る
            List<ToolStripLiistForRegist> listsRegist = MakeMenuNameList();

            // MenuStripに追加
            int ret = _menustripRegister.registMenuToMenuStripFromToolStripLiistForRegistList(listsRegist);
            if (ret < 1)
            {
                _errorLog.addErrorNotException(this.ToString(),
                    "registMenuToMenuStripFromToolStripLiistForRegistList failed");
            }

            // イベントハンドラを追加する
            _menuStripEvents = new MenuStripEvents(_menustripRegister.getMenuStrip());
            ret = _menuStripEvents.RegistFunctionForMenuStripEvents();
            if (ret < 1)
            {
                _errorLog.addErrorNotException(this.ToString(),
                    "RegistFunctionForMenuStripEvents failed");
            }

            // Form に MenuStrip を追加
            this.Controls.Add(_menustripRegister.getMenuStrip());
            this.MainMenuStrip = _menustripRegister.getMenuStrip();
            //レイアウトロジックを再開する
            //MenuStrip tms = this.MainMenuStrip;
            //tms.ResumeLayout(false);
            //tms.PerformLayout();
            //this.ResumeLayout(false);
            //this.PerformLayout();

            if (_errorLog.haveError())
            {
                _errorLog.ShowErrorMessageAndClearError();
            }
        }

        private void testAddMenu()
        {
            try
            {
                MenuStrip menuStrip = new MenuStrip();


                //「ファイル(&F)」メニュー項目を作成する
                ToolStripMenuItem fileMenuItem = new ToolStripMenuItem();
                fileMenuItem.Text = "ファイル(&F)";
                //MenuStripに追加する
                menuStrip.Items.Add(fileMenuItem);


                //「開く(&O)...」メニュー項目を作成する
                ToolStripMenuItem openMenuItem = new ToolStripMenuItem();
                openMenuItem.Text = "開く(&O)...";
                //ショートカットキー「Ctrl+O」を設定する
                openMenuItem.ShortcutKeys = Keys.Control | Keys.O;
                openMenuItem.ShowShortcutKeys = true;
                //Clickイベントハンドラを追加する
                //openMenuItem.Click += openMenuItem_Click;
                //「ファイル(&F)」のドロップダウンメニューに追加する
                fileMenuItem.DropDownItems.Add(openMenuItem);

                // Form に MenuStrip を追加
                this.Controls.Add(menuStrip);
                this.MainMenuStrip = menuStrip;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                keys =  Keys.Right;
                listRegist.Add(constants.MENU_DISPLAY_FOWARD, keys);
                keys = Keys.Left;
                listRegist.Add(constants.MENU_DISPLAY_REVERSE, keys);
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
            } catch (Exception ex)
            {
                _errorLog.addException(ex,this.ToString(), "MakeMenuNameList");
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
            public readonly string MENU_DISPLAY = "前へ(&D)";
            public readonly string MENU_DISPLAY_FOWARD = "前へ(&P)";
            public readonly string MENU_DISPLAY_REVERSE = "前へ(&R)";
            public readonly string MENU_SETTINGS = "設定(&S)";
            public readonly string MENU_SETTINGS_OPTION = "オプション(&O)";
            public readonly string MENU_HELP = "ヘルプ(&H)";
            public readonly string MENU_HELP_HELP = "ヘルプ(&H)";
            public readonly string MENU_HELP_VERSION = "バージョン情報(&V)";
            public Constants() { }
        }


        private void MenuStripSampleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
