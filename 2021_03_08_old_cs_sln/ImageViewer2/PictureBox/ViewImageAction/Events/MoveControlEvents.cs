using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ErrorLog;

namespace ViewImageAction
{
    // InnerContrl ControlMove用　ChangeLocation
    public class MoveControlEvents
    {
        ErrorLog.IErrorLog _errorLog;
        Control _control;
        Control _controlGetEvent;
        public Events.ViewImageMouseEventHandler _mouseEventHandler;
        private bool IsDown = false;
        private bool IsMove = false;
        private Point mpBegin;

        public Events.ViewImageMouseEventHandler MouseEventHandler { get => _mouseEventHandler; set => _mouseEventHandler = value; }
        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }

        public MoveControlEvents(Control control,Control controlGetEvent)
        {
            if (IsMove) { IsMove = false; }
            _control = control;
            _controlGetEvent = controlGetEvent;
            _errorLog = GlobalErrloLog.ErrorLog;
            // イベントのインスタンスを生成
            _mouseEventHandler = new Events.ViewImageMouseEventHandler();
            // このクラス内のメソッドをイベントへ紐づけ
            _controlGetEvent.MouseMove += Control_MouseMove;
            _controlGetEvent.MouseDown += Control_MouseDown;
            _controlGetEvent.MouseUp += Control_MouseUp;
        }

        private void changeLocationByMouse(object sender, MouseEventArgs e,Point mp)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    _control.Left += e.X - mp.X;
                    _control.Top += e.Y - mp.Y;
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "changeLocationByMouse");
            }
        }
        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            IsMove = true;
            try
            {
                if (IsDown)
                {
                    changeLocationByMouse(sender, e, mpBegin);
                }
                _mouseEventHandler.MouseMove(sender, e);
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "Control_MouseMove");
            } finally
            {
                IsMove = false;
            }
        }
        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Control_MouseDown");
            IsDown = true;
            try
            {
                mpBegin = e.Location;
                _mouseEventHandler.MouseDown(sender, e);
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "Control_MouseDown");
            }
            finally
            {
                //IsDown = false;
            }
        }
        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            IsDown = false;
            try
            {
                _mouseEventHandler.MouseUp(sender, e);
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "Control_MouseUp");
            }
            finally
            {
                //IsDown = false;
            }
        }
    }
}
