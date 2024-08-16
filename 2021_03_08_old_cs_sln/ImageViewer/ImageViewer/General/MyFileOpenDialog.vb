Public Class MyFileOpenDialog
    Implements IDisposable
    Private mOpenFileDialog As OpenFileDialog
    Private mFilePath As String

    Sub New()
        mOpenFileDialog = New OpenFileDialog()
    End Sub



    '===============================================================================================
    Private isDisposed As Boolean = False ' リソースが破棄(解放)されていることを表すフラグ

    ' IDisposable.Disposeの実装
    '// Dispose() calls Dispose(true)
    Public Sub Dispose() Implements IDisposable.Dispose
        ' アンマネージリソースと、マネージリソースの両方を破棄させる
        Dispose(True)
        ' すべてのリソースが破棄されているため、以後ファイナライザの実行は不要であることをガベージコレクタに通知する
        GC.SuppressFinalize(Me)
    End Sub
    ' リソースの解放処理を行うためのメソッド
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        ' 既にリソースが破棄されている場合は何もしない
        If isDisposed Then Return

        ' 破棄されていないアンマネージリソースの解放処理を行う
        If Not FormLog Is Nothing Then
            mOpenFileDialog.Dispose()
            mOpenFileDialog = Nothing
        End If
        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================

    Public Function getFileByDialog() As String
        Try
            'はじめのファイル名を指定する
            'はじめに「ファイル名」で表示される文字列を指定する
            mOpenFileDialog.FileName = ""
            'はじめに表示されるフォルダを指定する
            '指定しない（空の文字列）の時は、現在のディレクトリが表示される
            mOpenFileDialog.InitialDirectory = ""
            '[ファイルの種類]に表示される選択肢を指定する
            '指定しないとすべてのファイルが表示される
            'mOpenFileDialog.Filter = "HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*"
            '[ファイルの種類]ではじめに選択されるものを指定する
            '2番目の「すべてのファイル」が選択されているようにする
            mOpenFileDialog.FilterIndex = 2
            'タイトルを設定する
            mOpenFileDialog.Title = "開くファイルを選択してください"
            'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            mOpenFileDialog.RestoreDirectory = True
            '存在しないファイルの名前が指定されたとき警告を表示する
            'デフォルトでTrueなので指定する必要はない
            mOpenFileDialog.CheckFileExists = True
            '存在しないパスが指定されたとき警告を表示する
            'デフォルトでTrueなので指定する必要はない
            mOpenFileDialog.CheckPathExists = True

            'ダイアログを表示する
            If mOpenFileDialog.ShowDialog() = DialogResult.OK Then
                'OKボタンがクリックされたとき
                mFilePath = mOpenFileDialog.FileName
            End If
            Return mOpenFileDialog.FileName
        Catch ex As Exception
            Console.WriteLine("getFileByDialog")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    Public Function getFilePath() As String
        Return Me.mFilePath
    End Function

End Class
