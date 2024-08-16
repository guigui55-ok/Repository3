using OpenFileSample;
using System;
using System.Windows.Forms;

namespace OpenFileDialogManager
{
    public class OpenFileDialogManager
    {
        //OpenFileDialogクラスのインスタンスを作成
        protected OpenFileDialog openFileDialog;
        public readonly ErrorManager.ErrorManager Error;
        
        public OpenFileDialogManager(ErrorManager.ErrorManager error)
        {
            openFileDialog = new OpenFileDialog();
            Error = error;
        }

        public OpenFileDialog GetOpenFileDialog() { return this.openFileDialog; }

        public int Initialize()
        {
            try
            {
                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                openFileDialog.FileName = "";
                //はじめに表示されるフォルダを指定する
                //指定しない（空の文字列）の時は、現在のディレクトリが表示される
                openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                // 複数のファイルを選択できるようにする
                openFileDialog.Multiselect = true;

                DialogFilter filter = new DialogFilter();
                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                openFileDialog.Filter =filter.MakeFilter(
                    new string[] { "HTML", "すべて", "JPEG","GIF","TIFF","PNG","ICO","HEIC","ピクチャ" });

                //[ファイルの種類]ではじめに選択されるものを指定する
                //2番目の「すべてのファイル」が選択されているようにする
                openFileDialog.FilterIndex = 2;
                //タイトルを設定する
                openFileDialog.Title = "開くファイルを選択してください";
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                openFileDialog.RestoreDirectory = true;
                //存在しないファイルの名前が指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                openFileDialog.CheckFileExists = true;
                //存在しないパスが指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                openFileDialog.CheckPathExists = true;

                return 1;
            } catch (Exception ex)
            {
                Error.AddException(ex,this.ToString()+ ".Initialize");
                return 0;
            }
        }

        public int ShowDialog()
        {
            try
            {
                //ダイアログを表示する
                DialogResult ret = openFileDialog.ShowDialog();
                if (ret == DialogResult.OK) { return 1; } 
                else { return -1; }

            } catch (Exception ex)
            { Error.AddException(ex, this.ToString() + ".Initialize"); return 0; }
        }
        public int OpenFileSample()
        {
            try
            {

                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                openFileDialog.FileName = "default.html";
                //はじめに表示されるフォルダを指定する
                //指定しない（空の文字列）の時は、現在のディレクトリが表示される
                openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                //[ファイルの種類]に表示される選択肢を指定する
                //指定しないとすべてのファイルが表示される
                openFileDialog.Filter = "HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
                //[ファイルの種類]ではじめに選択されるものを指定する
                //2番目の「すべてのファイル」が選択されているようにする
                openFileDialog.FilterIndex = 2;
                //タイトルを設定する
                openFileDialog.Title = "開くファイルを選択してください";
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                openFileDialog.RestoreDirectory = true;
                //存在しないファイルの名前が指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                openFileDialog.CheckFileExists = true;
                //存在しないパスが指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                openFileDialog.CheckPathExists = true;

                //ダイアログを表示する
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //OKボタンがクリックされたとき、選択されたファイル名を表示する
                    Console.WriteLine(openFileDialog.FileName);
                }
                return 1;
            } catch (Exception ex)
            {
                Error.AddException(ex, this.ToString() + ".Initialize");
                return 0;
            }
        }
    }
}
