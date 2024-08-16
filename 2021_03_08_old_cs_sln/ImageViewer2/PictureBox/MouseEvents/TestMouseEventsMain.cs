using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLog;
using System.Windows.Forms;

namespace MouseEvents
{
    class TestMouseEventsMain
    {
        ErrorLog.IErrorLog _errorLog;
        Form _form;
        Panel _panel;
        ViewControlPanel _viewControlPanel;

        public TestMouseEventsMain(Form form,Panel panel,ErrorLog.IErrorLog errorlog)
        {
            _errorLog = errorlog;
            _form = form;
            _panel = panel;
            _viewControlPanel = new ViewControlPanel(panel);
        }
    }
}
