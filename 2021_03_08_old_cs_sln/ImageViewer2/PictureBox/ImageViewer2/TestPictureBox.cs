using System;
using ErrorLog;
using System.Windows.Forms;

namespace ImageViewer2
{
    class TestPictureBox
    {
        ErrorLog.IErrorLog _errorLog;
        public PictureBoxMain _pictureBoxMain;
        public int setErrorLog(Object errorLog)
        {
            try
            {
                if (Object.ReferenceEquals(errorLog.GetType(), new ErrorLog.ErrorLog().GetType()))
                {
                    _errorLog = (IErrorLog)errorLog;
                }
                else
                {
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "setErrorLog");
                return 0;
            }
        }

        public int initialize(Control parentControl,Control paintControl)
        {
            try
            {
                _pictureBoxMain = new PictureBoxMain();
                int Ret;
                //Ret = _pictureBoxMain.setErrorLog(this._errorLog);
                //if (Ret < 1)
                //{
                //    _errorLog.addErrorNotException("setErrorLog");
                //    return -1;
                //}

                Ret = _pictureBoxMain.setControl(paintControl);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException("setControl");
                    return -2;
                }

                Ret = _pictureBoxMain.setParentControl(parentControl);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException("setParentControl");
                    return -3;
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "initialize");
                return 0;
            }
        }
        public int paint(String path)
        {
            int Ret=0;
            try
            {
                if (System.IO.File.Exists(path))
                {
                    _pictureBoxMain.setImageLocation(path);
                }
                else
                {
                    _errorLog.addErrorNotException("paint_FileNotExists:" + path);
                    return -1;
                }
                return Ret;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "paint");
                return 0;
            }

        }

        private void get_path_test_image_file(){
            string path = @"C:\ZMyFolder_2\default_file_path.txt";
            try
            {
                // ファイルを読み込んでその内容を表示する
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // エラーが発生した場合はエラーメッセージを表示する
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }


        public int test_paint(TestViewImageForm form,PictureBox pictureBox)
        {
            int Ret;
            ErrorLog.IErrorLog _errorLog = new ErrorLog.ErrorLog();
            try
            {
                TestPictureBox testPictureBox = new TestPictureBox();
                //testPictureBox.setErrorLog(_errorLog);
                // 初期化
                Ret = testPictureBox.initialize(form, pictureBox);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException("setParentControl");
                    return Ret;
                }
                // パスをセット
                string path = get_path_test_image_file();
                // 画像を表示
                Ret = this.paint(path);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException("setParentControl");
                    return Ret;
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "TestPictureBox_Paint");
                return 0;
            }
            finally
            {
                _errorLog.ShowErrorMessage();
            }
        }
        private void get_path_test_image_file(){
            string path = @"C:\ZMyFolder_2\default_file_path.txt";
            try
            {
                // ファイルを読み込んでその内容を表示する
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // エラーが発生した場合はエラーメッセージを表示する
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public int test_paint_fit(TestViewImageForm form, PictureBox pictureBox)
        {
            int Ret;
            ErrorLog.IErrorLog _errorLog = new ErrorLog.ErrorLog();
            try
            {
                TestPictureBox testPictureBox = new TestPictureBox();
                testPictureBox.setErrorLog(_errorLog);
                // 初期化
                Ret = testPictureBox.initialize(form, pictureBox);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException("setParentControl");
                    return Ret;
                }
                // パスをセット
                string path = get_path_test_image_file();


                // 左上に表示
                pictureBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                // 画像サイズを決める

                // PictureBoxサイズを決める
                //　設定


                // 画像を表示
                Ret = this.paint(path);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException("setParentControl");
                    return Ret;
                }
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, "TestPictureBox_Paint");
                return 0;
            }
            finally
            {
                _errorLog.ShowErrorMessage();
            }
        }
    }
}
