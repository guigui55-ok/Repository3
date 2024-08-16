Public Class CommonFile
    Public Sub New()

    End Sub

    Public Function isFilePathExists(path As String) As Boolean
        Return isFilePath(path)
    End Function


    Public Function isExistsPath(path As String) As Boolean
        Try
            If Me.PathIsFileOrFolder(path) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("isExistsPath")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    'ファイルパスが存在するか　ファイルであるか　フォルダ×
    Public Function isFilePath(path As String) As Boolean
        Try
            If System.IO.File.Exists(path) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("isFilePath")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
    '=======================================================================
    'パスはファイルか
    Public Function PathIsFile(path As String) As Boolean
        Try
            If PathIsFileOrFolder(path) = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("PathIsFile")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
    '=======================================================================
    'パスはフォルダーか
    Public Function PathIsFolder(path As String) As Boolean
        Try
            If PathIsFileOrFolder(path) = 2 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("PathIsFolder")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
    '=======================================================================
    '1ファイル２フォルダー0存在しない-1エラー
    Public Function PathIsFileOrFolder(path As String) As Integer
        Try
            If System.IO.File.Exists(path) = True Then
                Return 1
            ElseIf System.IO.Directory.Exists(path) = True Then
                Return 2
            Else
                Return 0
            End If
        Catch ex As Exception
            Console.WriteLine("PathIsFileOrFolder")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function
    '=======================================================================
    'ファイルパスからフォルダ部のみを抜き出す
    Public Function getFolderPathFromFilePath(path As String) As String
        Try
            'If Not isFilePathExists(path) Then
            '    Return ""
            'End If
            'If PathIsFolder(path) Then
            '    Return ""
            'End If
            If path.LastIndexOf("\") >= 0 Then
                Return path.Substring(0, path.LastIndexOf("\"))
            Else
                Return ""
            End If
        Catch ex As Exception
            Console.WriteLine("getFolderPathFromFilePath")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    'パスからファイル名を得る
    Public Function getFileNameFromPath(path As String) As String
        Try
            'If Not isFilePath(path) Then
            '    Return ""
            'End If
            'If PathIsFolder(path) Then
            '    Return ""
            'End If
            If path.LastIndexOf("\") >= 0 Then
                Return path.Substring(path.LastIndexOf("\") + 1, path.Length - path.LastIndexOf("\") - 1)
            Else
                Return ""
            End If
        Catch ex As Exception
            Console.WriteLine("getFolderPathFromFilePath")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    '一つ上の階層のフォルダを得る
    Public Function getParentFolder(path As String) As String
        Try
            Dim folderpath As String = path
            If PathIsFile(folderpath) Then
                'ファイルならフォルダ名を抜き出す
                folderpath = getFileNameFromPath(folderpath)
            End If
            Return System.IO.Directory.GetParent(folderpath).ToString

        Catch ex As Exception
            Console.WriteLine("getNextFolder")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    '同じ階層の次のフォルダを得る なければそのまま
    'フォルダが1つしかない　今のもの
    'フォルダがないことはない
    '今のフォルダが最後　そのまま
    Public Function getNextFolderInNowFolder(path As String) As String
        Try
            Dim nowFolderpath As String = getFolderPathFromFilePath(path) 'ファイル名の場合フォルダ名に
            Dim parentFolder As String = getParentFolder(nowFolderpath) '一つ上の階層を得る
            Dim FolderName As String = getFileNameFromPath(nowFolderpath) '今のフォルダ名
            Dim returnPath As String = ""
            For Each bufpath As String In System.IO.Directory.GetDirectories(parentFolder, "*", System.IO.SearchOption.TopDirectoryOnly)
                '下で保持したものがあればこれが次のフォルダ
                If Not returnPath = "" Then
                    '保持したもの（今のフォルダが最後なら）ここには来れないので、そのままになる
                    returnPath = bufpath
                    Return bufpath
                End If
                '一致したものがあれば保持する
                If bufpath = nowFolderpath Then
                    returnPath = bufpath
                End If
            Next
            '一致するものがないor今のフォルダしかないor今のフォルダが最後
            Return returnPath
        Catch ex As Exception
            Console.WriteLine("getNextFolderInNowFolder")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function


    '同じ階層の「前」のフォルダを得る なければそのまま
    'フォルダが1つしかない　今のもの
    'フォルダがないことはない
    '今のフォルダが「最初」→そのまま
    Public Function getPreviousFolderInNowFolder(path As String) As String
        Try
            Dim nowFolderpath As String = getFolderPathFromFilePath(path) 'ファイル名の場合フォルダ名に
            Dim parentFolder As String = getParentFolder(nowFolderpath) '一つ上の階層を得る
            Dim FolderName As String = getFileNameFromPath(nowFolderpath) '今のフォルダ名
            Dim returnPath As String = ""
            '------------------
            Dim bufPath As String = ""
            Dim folderList() As String = System.IO.Directory.GetDirectories(parentFolder, "*", System.IO.SearchOption.TopDirectoryOnly)
            For i = folderList.Count To 1 Step -1
                bufPath = folderList(i)
                '下で保持したものがあればこれが次のフォルダ
                If Not returnPath = "" Then
                    '保持したもの（今のフォルダが最後なら）ここには来れないので、そのままになる
                    returnPath = bufPath
                    Return bufPath
                End If
                '一致したものがあれば保持する
                If bufPath = nowFolderpath Then
                    returnPath = bufPath
                End If
            Next
            '------------------
            '一致するものがないor今のフォルダしかないor今のフォルダが最後
            Return returnPath
        Catch ex As Exception
            Console.WriteLine("getPreviousFolderInNowFolder")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    'カレントディレクトリを取得
    Public Function getCurrentDirectory() As String
        Return System.IO.Directory.GetCurrentDirectory()
    End Function


    ''カレントディレクトリを変更する
    'System.Environment.CurrentDirectory = "C:\"
    'System.IO.Directory.SetCurrentDirectory("C:\")

    Public Function getParentDirectory(path As String) As String
        Try
            Return System.IO.Directory.GetParent(path).ToString
        Catch ex As Exception
            Console.WriteLine("getParentDirectory")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    '階層を一つ下へ　深く
    '階層を一つ上へ　浅く
End Class

