using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewImageAction.Events
{
    public class ViewImageMouseEventHandler : IDisposable
    {
        public event EventHandler<int> OnRightClick;
        public event EventHandler<int> OnLeftClick;
        public event EventHandler<int> OnDragDrop;
        public event EventHandler<MouseEventArgs> OnMouseWheel;
        public event EventHandler<MouseEventArgs> OnMouseMove;
        public event EventHandler<MouseEventArgs> OnMouseDown;
        public event EventHandler<MouseEventArgs> OnMouseUp;

        public void MouseUp(object sender, MouseEventArgs e) { OnMouseUp?.Invoke(sender, e); }
        public void MouseDown(object sender, MouseEventArgs e) { OnMouseDown?.Invoke(sender, e); }
        public void MouseMove(object sender, MouseEventArgs e) { OnMouseMove?.Invoke(sender, e); }
        public void MouseWheel(object sender,MouseEventArgs e)
        {
            OnMouseWheel?.Invoke(sender,e);
        }
        public void RightClick(Exception e, int message)
        {
            try
            {
                OnRightClick?.Invoke(e, message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RightClick");
                Debug.WriteLine(ex.Message);
            }
        }
        public void LeftClick(Exception e, int message)
        {
            try
            {
                OnLeftClick?.Invoke(e, message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LeftClick");
                Debug.WriteLine(ex.Message);
            }
        }
        public void DragDrop(Exception e, int message)
        {
            try
            {
                OnDragDrop?.Invoke(e, message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnDragDrop");
                Debug.WriteLine(ex.Message);
            }
        }
        public void Dispose()
        {
            //④Dispose()実行
            OnDragDrop?.Invoke(null, 0);
        }
    }
}
