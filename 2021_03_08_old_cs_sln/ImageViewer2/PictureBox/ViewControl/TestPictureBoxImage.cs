using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLog;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ImageViewer2
{
    class TestPictureBoxImage
    {
        ErrorLog.IErrorLog _errorLog;
        PictureBox _picturebox;
        ViewImage _viewImage;
        
        public TestPictureBoxImage()
        {
            _errorLog = GlobalErrloLog.ErrorLog;
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

        public void testImageView(Form form,PictureBox picturebox)
        {
            // パスをセット
            string path = get_path_test_image_file();
            path = @"J:\ZMyFolder_2\jpgbest\gif_png_bmp\gif\160501001.gif";
            int Ret;
            Ret = initialize(form,picturebox,path);
            if (Ret < 1)
            {
                _errorLog.addErrorNotException(this.ToString() + "testImageView : initialize");
            }

        }
        public int initialize(Control parentControl, Control paintControl,String path)
        {
            try
            {

                TestPictureBox testPictureBox = new TestPictureBox();
                int Ret;
                // 初期化
                Ret = testPictureBox.initialize(parentControl, paintControl);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString() + "initialize");
                    return -1;
                }
                // PictureBox取得
                _picturebox = testPictureBox._pictureBoxMain.getPictureBox();

                // ViewImageインスタンス生成
                _viewImage = new ViewImage();

                // イメージ取得
                Ret = _viewImage.setPath(path);
                if (Ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString() + "setPath");
                    return -2;
                }
                // イメージをセット
                testPictureBox._pictureBoxMain.setImageWithDispose(_viewImage.getImage());

                // 位置変更
                //_picturebox.Location = new Point(100, 100);

                // アンカー
                // 左上に表示
                _picturebox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                // ※AnchorとLocationは、Locationが優先される

                _picturebox.SizeMode = PictureBoxSizeMode.CenterImage; // コントロールの中央＝画像の中央
                _picturebox.SizeMode = PictureBoxSizeMode.Zoom; // 画像にフィット



                // フォームの大きさに追随してピクチャボックスが伸縮するようになる
                _picturebox.Dock = DockStyle.Fill;
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " initialize");
                return 0;
            }
        }
    }
}
