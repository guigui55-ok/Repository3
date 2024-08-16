using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ErrorLog;

namespace FileList
{
    public partial class FileListForm : Form
    {

        ErrorLog.IErrorLog _errorLog;
        TestReadList _testReadList;
        public FileListForm()
        {
            InitializeComponent();
            GlobalErrloLog.newErrorLog();
            _errorLog = GlobalErrloLog.ErrorLog;
            _testReadList = new TestReadList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //textBox_includeType.Text = "url";
            //textBox_notIncludeType.Text = "url";
            textBox_includeFilename.Text = "c#";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();

                char sepalator = ',';
                List<string> list = new CommonString().getListFromString(richTextBox1.Text, '\n');
                List<string> IncludeTypeList = new List<string>(textBox_includeType.Text.Split(sepalator));
                new CommonString().DebugWriteList(IncludeTypeList);
                List<string> notIncludeTypeList = new List<string>(textBox_notIncludeType.Text.Split(sepalator));
                List<string> IncludeFilename = new List<string>(textBox_includeFilename.Text.Split(sepalator));
                List<string> notIncludeFilename = new List<string>(textBox_notIncludeFilename.Text.Split(sepalator));
                int ret = _testReadList.excute(
                    list.ToArray(),
                    IncludeTypeList,
                    notIncludeTypeList,
                    IncludeFilename,
                    notIncludeFilename);
                if (ret < 1)
                {
                    MessageBox.Show("_testReadList.excute Failed");
                }

                //this.listBox1.Text
                //this.listBox1.Text = new CommonString().ListStringToString(_testReadList.getList(), "\n");
                //listBox1.li = _testReadList.getList();
                ret = new ListBoxControl().setValueFromList(listBox1, _testReadList.getList());
                if (ret < 1)
                {
                    MessageBox.Show("ListBoxControl.setValueFromList Failed");
                }


                MessageBox.Show("success");
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString(), "button1_Click");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //if (e.Data.GetDataPresent(DataFormats.Text))
                //{
                //    this.textBox1.Text = (string)e.Data.GetData(DataFormats.Text);
                //}

                string[] files = new MouseEvents.MouseEvents().GetFilesByDragAndDrop(e);
                string buf = "";
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        buf += files[i];
                        if (i < files.Length)
                        {
                            buf += "\n";
                        }
                    }
                }
                richTextBox1.Text = buf;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int ret = _testReadList.switchOrderToRandomOrCorrect();
                if (ret < 1)
                {
                    MessageBox.Show("button2_Click.ListToRandom Failed");
                }

                listBox1.Items.Clear();

                ret = new ListBoxControl().setValueFromList(listBox1, _testReadList.getList());
                if (ret < 1)
                {
                    MessageBox.Show("ListBoxControl.setValueFromList Failed");
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

    }
}
