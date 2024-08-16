using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FileList
{
    class ListBoxControl
    {
        public int setValueFromList(ListBox listbox,List<string> addList)
        {
            try
            {
                foreach(var val in addList)
                {
                    listbox.Items.Add(val);
                }
                return 1;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "setValueFromList");
                return 0;
            }
        }
        public List<string> getListFromListBox(ListBox listbox,char sepalator)
        {
            try
            {
                string buf = listbox.Text;
                return new List<string>(buf.Split(sepalator));
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "getListFromListBox");
                return new List<string>();
            }
        }

        public int setValueFromListString(ListBox listbox,List<string> list)
        {
            try
            {
                foreach(var value in list)
                {
                    
                }
                return 1;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "getListFromListBox");
                return 0;
            }
        }
    }
}
