using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewImageAction.Function
{
    public class CommonFunctions
    {
        public ViewImageControlFunction ControlFunction;
        public ViewImageBasicFunction BasicFunction;
        public MainFormFunction MainFormFunction;
        public ViewImageObjects ViewImageObjects;
        public void testFunction(string value)
        {
            MessageBox.Show(value);
        }
    }
}
