using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ErrorLog;

namespace ViewImageAction
{
    public partial class ViewImageActionForm : Form
    {
        readonly ErrorLog.IErrorLog _errorLog;
        readonly TestViewImageAction _testViewImageAction;

        public ViewImageActionForm()
        {
            InitializeComponent();

            this.Location = new Point(150, 150);
            this.Size = new Size(620,780);

            GlobalErrloLog.ErrorLog = new ErrorLog.ErrorLog();
            _errorLog = GlobalErrloLog.ErrorLog;
            _testViewImageAction = new TestViewImageAction();
        }

        private string get_path_test_image_file(){
            string filePath = @"C:\ZMyFolder_2\default_file_path.txt";
            try
            {
                // ファイルを読み込んでその内容を表示する
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        return line;
                    }
                }
            }
            catch (Exception e)
            {
                // エラーが発生した場合はエラーメッセージを表示する
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return "";
        }


        private void ViewImageActionForm_Load(object sender, EventArgs e)
        {
            try
            {
                int ret = _testViewImageAction.Initialize(this,this.panel3,this.panel1,this.panel2, this.pictureBox1);
                if (ret < 1)
                {
                    MessageBox.Show("ViewImageActionForm_Load _testViewImageAction.initialize Faied");
                }

                // setPath
                string path = get_path_test_image_file();

                // ViewImage
                //ret = _testViewImageAction.ViewImage(path);
                //if (ret < 1)
                //{
                //    MessageBox.Show("ViewImageActionForm_Load _testViewImageAction.initialize Faied");
                //}

                List<string> list = new List<string>
                {
                    path
                };
                _testViewImageAction.Functions.BasicFunction.ViewImageAfterSetPath(_testViewImageAction.getFirstViewImageObject(), list);

                

                // File OK
                // Click NextPrevious OK
                // 拡大縮小
                // キーイベントは実装しない
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ViewImageActionForm_Load");
                _errorLog.addException(ex, this.ToString(), "ViewImageActionForm_Load");
            }
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Panel_MouseClick");
        }
    }
}
