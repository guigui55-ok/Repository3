using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorLog;

namespace ViewImageAction.BaseForm
{
    public class MenuStripRegister
    {
        ErrorLog.IErrorLog _errorLog;
        MenuStrip _menuStrip;
        
        public MenuStripRegister(MenuStrip menuStrip)
        {
            _menuStrip = menuStrip;
        }
        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }


        public int registMenuToMenuStripFromToolStripLiistForRegistList(List<ToolStripLiistForRegist> lists)
        {
            try
            {
                if (lists is null)
                { _errorLog.addErrorNotException(this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegistList lists is null"); return -1; }
                if (lists.Count < 1)
                { _errorLog.addErrorNotException(this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegistList lists count < 1"); return -2; }

                int ret = 0;
                foreach (ToolStripLiistForRegist list in lists)
                {
                    ret = registMenuToMenuStripFromToolStripLiistForRegistList(list);
                    if (ret < 1)
                    {
                        _errorLog.addErrorNotException(this.ToString(),
                            "registMenuToMenuStripFromToolStripLiistForRegistList failed");
                    }
                }
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegistList");
                return 0;
            }
        }

        // メニュー一行登録、MenuStrip.Item を登録
        public int registMenuToMenuStripFromToolStripLiistForRegistList(ToolStripLiistForRegist list)
        {
            try
            {
                if (list is null)
                { _errorLog.addErrorNotException(this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegistList lists is null"); return -1; }
                if (list.Count < 1)
                { _errorLog.addErrorNotException(this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegistList lists count < 1"); return -2; }

                int count = 0;
                int ret = 0;
                ToolStripMenuItem parentItem = new ToolStripMenuItem();
                foreach (ToolStripItemForRegist item in list)
                {
                    if (count == 0)
                    {
                        // 1つめはTextのみ
                        ret = registToolStripMenuItemToParentMenu(parentItem,item.Value);
                        if (ret < 1)
                        {
                            _errorLog.addErrorNotException(this.ToString(),
                                "registMenuToMenuStripFromToolStripLiistForRegistList lists count < 1"); return -1;
                        }
                    } else
                    {
                        //Debug.WriteLine(parentItem.Text);
                        ret = registMenuToMenuStripFromToolStripItemForRegist(parentItem,item);
                        if (ret < 1)
                        {
                            _errorLog.addErrorNotException(this.ToString(),
                                "registMenuToMenuStripFromToolStripLiistForRegistList lists count < 1");
                            ret = 1;
                        }
                    }
                    count++;
                }

                if (ret > 0)
                {
                    _menuStrip.Items.Add(parentItem);
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegist");
                return 0;
            }
        }

        // parentItem
        public int registToolStripMenuItemToParentMenu(ToolStripMenuItem parentItem,string text)
        {
            try
            {
                if (text == "")
                { _errorLog.addErrorNotException(this.ToString(), "registMenuToMenuStripFromToolStripItemForRegist text is null"); return -1; }
                if (text.Length < 1)
                { _errorLog.addErrorNotException(this.ToString(), "registMenuToMenuStripFromToolStripItemForRegist text length 0"); return -2; }

                //parentItem = new ToolStripMenuItem();
                parentItem.Text = text;
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "registMenuToMenuStripFromToolStripLiistForRegist");
                return 0;
            }
        }
        // parentItem に登録する
        public int registMenuToMenuStripFromToolStripItemForRegist(ToolStripMenuItem parentItem,ToolStripItemForRegist item)
        {
            try
            {
                if (item.Value == "")
                { _errorLog.addErrorNotException(this.ToString(), 
                    "registMenuToMenuStripFromToolStripItemForRegist text is null"); return -1; }
                if (item.Value.Length < 1)
                { _errorLog.addErrorNotException(this.ToString(), 
                    "registMenuToMenuStripFromToolStripItemForRegist text length 0"); return -2; }

                // new ToolStripMenuItem(text,image,onclick,shorucutKeys);
                //ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(item.Value, null, null, item.Keys);
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Text = item.Value;
                toolStripMenuItem.ShortcutKeys = item.Keys;
                parentItem.DropDownItems.Add(toolStripMenuItem);
                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "registMenuToMenuStripFromToolStripItemForRegist");
                return 0;
            }
        }

        // MenuStripからItemを得る
        public ToolStripMenuItem getToolStripMenuItemFromText(string menuText,params string[] ary )
        {
            try
            {
                if (_menuStrip == null)
                { _errorLog.addErrorNotException(this.ToString(),
                      "getToolStripMenuItemFromText Menu is null"); return null; }
                if (_menuStrip.Items.Count < 1)
                { _errorLog.addErrorNotException(this.ToString(),
                      "getToolStripMenuItemFromText MenuCount 0"); return null; }
                if (menuText == "")
                { _errorLog.addErrorNotException(this.ToString(),
                        "getToolStripMenuItemFromText menuText is null"); return null;
                }

                ToolStripMenuItem retItem = null;
                foreach (ToolStripMenuItem item in _menuStrip.Items)
                {
                    if (menuText.CompareTo(item.Text) == 0)
                    {
                        // child search 
                        retItem = getToolStripMenuItemInToolStripMenuItemFromArray(item, 0, ary);
                        if (!(retItem is null))
                        {
                            // アイテムが見つかったら終了
                            break;
                        }
                    }
                }
                return retItem;

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getToolStripMenuItemFromText");
                return null;
            }
        }

        // 子アイテムをTextから取得する
        public ToolStripMenuItem getToolStripMenuItemInToolStripMenuItemFromArray(
            ToolStripMenuItem parentItem, int index,params string[] ary)
        {
            try
            {
                if (ary == null)
                { _errorLog.addErrorNotException(this.ToString(),
                            "getToolStripMenuItemFromText ary is null"); return null; }
                if (ary.Length < 1)
                { _errorLog.addErrorNotException(this.ToString(),
                              "getToolStripMenuItemFromText ary Count 0"); return null; }

                foreach (ToolStripMenuItem item in parentItem.DropDownItems)
                {
                    if (item.Text.CompareTo(ary[index]) == 0)
                    {
                        // 
                        // これが検索対象のラスト
                        if (index == (ary.Length - 1))
                        {
                            return item;
                        } else
                        {
                            // さらに検索する子アイテムがある
                            if (item.HasDropDownItems)
                            // 子アイテムを持っていたら
                            {
                                {
                                    return getToolStripMenuItemInToolStripMenuItemFromArray(
                                        item, index + 1, ary);
                                }
                            }
                            else
                            {
                                // 子アイテムがこれ以上ない
                                {
                                    return null;
                                }
                            }

                        }
                    } else
                    {
                        // 一致しない
                    }
                }

                return null;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "getToolStripMenuItemInToolStripMenuItemFromArray");
                return null;
            }
        }

        // --------------------------------------------
        public MenuStrip getMenuStrip()
        {
            return _menuStrip;
        }

        public int SetAppendMenuStripToForm(Form form)
        {
            try
            {
                if (form == null)
                { _errorLog.addErrorNotException(this.ToString(),
                              "getToolStripMenuItemFromText ary is null"); return -1; }

                bool flag = false;
                MenuStrip tmenu; // 設定用
                foreach (var item in form.Controls)
                {
                    if(item is MenuStrip)
                    {
                        flag = true;
                        tmenu = (MenuStrip)item;
                        tmenu = _menuStrip;
                        form.Controls.Add(_menuStrip);
                        form.MainMenuStrip = _menuStrip;
                    }
                }
                // 1つもなければ登録
                if (!flag)
                {
                    form.Controls.Add(_menuStrip);
                }

                return 1;
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "SetAppendMenuStripToForm");
                return 0;
            }
        }

        // Debug用MenuStrip存在確認
        public void IsExistsMenuStripInForm(Form form)
        {
            try
            {
                if (form == null)
                {
                    Debug.WriteLine("IsExistsMenuStripInForm form Is Nothing");
                    _errorLog.addErrorNotException(this.ToString(),
                                "IsExistsMenuStripInForm form is null"); return;
                }

                bool flag = false;
                MenuStrip tmenu; // 設定用
                foreach (var item in form.Controls)
                {
                    if (item is MenuStrip)
                    {
                        flag = true;
                        tmenu = (MenuStrip)item;
                        Debug.WriteLine(" *** MenuItemList ***");
                        foreach (ToolStripMenuItem menuitem in tmenu.Items)
                        {
                            Debug.WriteLine(menuitem.Text);
                        }
                    }
                }
                // 1つもなければ登録
                if (!flag)
                {
                    Debug.WriteLine("MenuStrip Is Nothing");
                }

                return;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "IsExistsMenuStripInForm");
                return;
            }
        }
    }
}
