Public Class ShortCut
    '参照マネージャーの"COM"から
    '"Windows Script Host Object Model"
    'にチェックを入れます。

    Public Sub New()

    End Sub

    'argShortCutPathは拡張子.linkを読み込む
    Public Function getFilePathFromShortCut(argShortCutPath As String) As String
        Try
            Dim shell As IWshRuntimeLibrary.WshShell = New IWshRuntimeLibrary.WshShell()
            'ショートカットオブジェクトの取得
            Dim ShortCutObject As IWshRuntimeLibrary.IWshShortcut =
                CType(shell.CreateShortcut(argShortCutPath), IWshRuntimeLibrary.IWshShortcut)
            Dim TargetPath As String = ShortCutObject.TargetPath.ToString()

            Return TargetPath
        Catch ex As Exception
            Console.WriteLine("getFilePathFromShortCut")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function
End Class
