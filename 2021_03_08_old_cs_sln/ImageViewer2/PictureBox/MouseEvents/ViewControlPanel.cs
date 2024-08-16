using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLog;
using System.Windows.Forms;

namespace MouseEvents
{
    class ViewControlPanel
    {
        private ErrorLog.IErrorLog _errorLog;
        private Panel _panel;
        private MouseEvents _mouseEvents;

        public ViewControlPanel(Panel panel)
        {
            _errorLog = GlobalErrloLog.ErrorLog;
            _panel = panel;
            _panel.MouseClick += Panel_MouseClick;
            _mouseEvents = new MouseEvents(_panel);

            _panel.DragDrop += Panel_DragDrop;
        }

        public void Panel_MouseClick(Object sender,MouseEventArgs e)
        {
            try
            {
                int ret = _mouseEvents.ClickPointIsRightSideOnControl(e);
                if (ret == 1)
                {
                    MessageBox.Show("right click");
                } else if(ret == 2)
                {
                    MessageBox.Show("left click");
                } else
                {
                    MessageBox.Show("Errro click");
                    _errorLog.addErrorNotException(this.ToString(), "Panel_MouseClick");
                }
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "Panel_MouseClick");
            }
        }

        public void Panel_DragDrop(Object sender, DragEventArgs e)
        {
            try
            {

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "Panel_DragDrop");
            }
        }
    }
}
