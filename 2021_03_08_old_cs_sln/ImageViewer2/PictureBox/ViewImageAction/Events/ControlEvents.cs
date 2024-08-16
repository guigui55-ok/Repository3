using System;
using ErrorLog;
using System.Windows.Forms;
using System.Collections.Generic;
using ImageViewer2;
using System.Diagnostics;

namespace ViewImageAction
{
    // ViewImageControl //inner
    // DragDrop MouseClick(Right/Left)用
    class ControlEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Control _control;
        public Events.ViewImageMouseEventHandler _mouseEventHandler;
        public FileList.FileList FileListForRead;

        private bool IsDown = false;
        private bool IsDoClick = true;
        private bool IsMove=false;

        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }
        public ControlEvents(Control control)
        {
            if (IsMove) { IsMove = false; }
            ClearErrorForIsMove(IsMove);

            _control = control;
            _errorLog = GlobalErrloLog.ErrorLog;
            // イベントのインスタンスを生成
            _mouseEventHandler = new Events.ViewImageMouseEventHandler();
            // このクラス内のメソッドをイベントへ紐づけ
            _control.MouseClick += this.Control_MouseClick;
            _control.Click += this.Control_Click;
            _control.DragDrop += Control_DragDrop;
            _control.DragEnter += Control_DragEnter;
            _control.DragOver += Control_DragOver;
            _control.MouseWheel += Control_MouseWheel;

            _control.MouseDown += Control_MouseDown;
            _control.MouseUp += Control_MouseUp;
            _control.MouseMove += Control_MouseMove;
        }

        private void ClearErrorForIsMove(bool flag) { if (flag) { Debug.WriteLine("ClearErrorForIsMove" + IsMove); } }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            this.IsMove = true;
            try
            {
                if (IsDown)
                {
                    // マウスドラッグしたときは、クリック扱いしない
                    IsDoClick = false;
                }
            } catch { }
            finally
            {
                IsMove = false;
            }
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            IsDown = true;
        }
        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            IsDown = false;
            try
            {

            } catch
            {

            } finally
            {
                IsDoClick = true;
            }
        }

        private void Control_MouseWheel(object sender,MouseEventArgs e)
        {
            _mouseEventHandler.MouseWheel(sender, e);
        }

        private void Control_Click(object sender, EventArgs e) 
        {
            //Debug.WriteLine("Control_Click");
        }
        // MouseClick Event
        private void Control_MouseClick(object sender,MouseEventArgs e)
        {
            //Debug.WriteLine("Control_MouseClick");
            try
            {
                if (!IsDoClick) { return; }
                int flag = ClickPointIsRightSideOnControl(_control,e);
                if (flag == 1)
                {
                    // RightClick
                    _mouseEventHandler.RightClick(null, 2);

                } else if(flag == 2)
                {
                    // LeftClick
                    _mouseEventHandler.LeftClick(null, 1);
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "Control_MouseClick");
            }
        }
        // クリックされた場所がコントロールの右側化左側か
        public int ClickPointIsRightSideOnControl(Control control, MouseEventArgs e)
        {
            try
            {
                if (e.X > (control.Width / 2))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "ClickPointIsRightSideOnControl");
                return 0;
            }
        }
        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            //Debug.WriteLine("Control_DragDrop");
            try
            {
                // DragDrop の e を配列へ
                string[] files = new MouseEvents.MouseEvents().GetFilesByDragAndDrop(e);
                // 配列→Listへ
                List<string> list = new List<string>(files);


                int ret = this.FileListForRead.setFileList(list);
                if (ret < 1)
                {
                    MessageBox.Show("Control_DragDrop.setFileList Failed");
                }
                // リストからFileListへ登録
                //ret = this.FileListForRead.setFileListWithApplyConditions(list);
                //
                if (ret < 1)
                {
                    MessageBox.Show("Control_DragDrop.setFileListWithApplyConditions Failed");
                }
                else
                {
                    Debug.WriteLine("Control Type = " +  _control.GetType().ToString());
                    Debug.WriteLine("Count = " + FileListForRead.getListCount());
                    //MessageBox.Show(FileListForRead.get);
                    // RightClick
                    _mouseEventHandler.DragDrop(null, 2);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Control_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Control_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
    }
}
