﻿using CommonUtility.ControlUtility;
using ErrorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEditorSample.Menu
{
    public class MenuStripConstants
    {
        public readonly string FILE_MENU = "ファイル(&F)";
        public readonly string FILE_OPEN = "開く(&O)...";
        public readonly string FILE_APPEND = "上書き保存(&S)";
        public readonly string FILE_SAVEAS = "名前を付けて保存(&A)...";
        public readonly string FILE_EXIT = "終了(&X)";
        public readonly string OPTION_MENU = "オプション(&O)";
        //public readonly string OPTION_SETTINGS = "設定(&S)";
    }
    public class MenuStripManager
    {
        protected ErrorManager _err;
        protected Form _parentForm;
        protected MenuStrip _menuStrip;
        public MenuStripConstants Constants = new MenuStripConstants();
        MenuStripUtility Utility;

        public MenuStripManager(ErrorManager err)
        {
            _err = err;
            Utility = new MenuStripUtility(_err);
        }
        public MenuStripManager(ErrorManager err,MenuStrip menuStrip)
        {
            _err = err;
            Utility = new MenuStripUtility(_err);
            _menuStrip = menuStrip;
        }


        public void AddEventToMenu(string[] textArray, EventHandler menuClickEventHandler)
        {
            Utility.AddEventToMenu(_menuStrip, textArray, menuClickEventHandler);
        }

        public void Initialize(Form form)
        {
            try
            {
                MenuStripUtility util = new MenuStripUtility(_err);
                _parentForm = form;
                if (_menuStrip == null) { _menuStrip = new MenuStrip(); }                
                // ファイルメニューを作成する
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Text = this.Constants.FILE_MENU;
                // 開く メニューを追加する
                util.AddMenu(menuItem, this.Constants.FILE_OPEN, true, Keys.Control | Keys.O);
                // 上書き保存 メニューを追加する
                util.AddMenu(menuItem, this.Constants.FILE_APPEND, true, Keys.Control | Keys.S);
                // 名前を付けて保存 メニューを追加する
                util.AddMenu(menuItem, this.Constants.FILE_SAVEAS, true, Keys.Control | Keys.Shift | Keys.A);
                // セパレータを追加する
                menuItem.DropDownItems.Add(new ToolStripSeparator());
                // 終了 メニューを追加する
                util.AddMenu(menuItem, this.Constants.FILE_EXIT, true, Keys.Control | Keys.X);
                // MenuStrip に追加する
                _menuStrip.Items.Add(menuItem);

                // オプションメニューを作成する
                menuItem = new ToolStripMenuItem();
                menuItem.Text = this.Constants.OPTION_MENU;
                // 設定 メニューを追加する
                //util.AddMenu(menuItem, this.Constants.OPTION_SETTINGS, true, Keys.Control | Keys.Shift | Keys.S);
                // MenuStrip に追加する
                _menuStrip.Items.Add(menuItem);

                // 親フォームに追加する
                _parentForm.Controls.Add(_menuStrip);

            }
            catch (Exception ex)
            {
                _err.AddException(ex, this, "Initialize");
            }
        }
    }
}
