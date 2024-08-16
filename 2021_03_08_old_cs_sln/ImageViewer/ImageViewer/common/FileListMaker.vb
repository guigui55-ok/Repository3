Public Class FileListMaker
    Private FileList As List(Of String) = New List(Of String)
    Private CountReadSubFolderHierarchy As Integer = 1
    Private CountReadSubFolder As Integer

    Public Sub InitializeImageList()
        FileList = New List(Of String)
    End Sub

    'フォルダの階層 Folder hierarchy
    Public Sub setCountReadSubFolderHierarchy(argValue As Integer)
        CountReadSubFolderHierarchy = argValue
    End Sub

    Public Function getFileList() As List(Of String)
        Return FileList
    End Function

    '=======================================================================
    Public Function getFileNameByDragAndDrop(sender As Object, e As DragEventArgs) As List(Of String)
        Dim buflist As List(Of String) = New List(Of String)
        Try
            If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
                'ドラッグ中のファイルやディレクトリの取得
                Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
                For i = 0 To files.Length - 1
                    buflist.Add(files(i))
                    Console.WriteLine(files(i))
                Next
                'e.Effect = DragDropEffects.Copy
            End If
            Return buflist
        Catch ex As Exception
            Console.WriteLine("getFileNameByDragAndDrop")
            Console.WriteLine(ex.Message)
            Return buflist
        End Try
    End Function
    '=======================================================================
    'ファイルパスリスト(D&Dから得たもの)からファイルリストを作成
    'Privateのリストに格納
    Public Sub MakeFileListFromPathList(pathList As List(Of String))
        Try
            For Each nowValue In pathList
                If (Right(nowValue, 4).Equals(".lnk")) Then
                    'ショートカット
                    Dim link As String = New ShortCut().getFilePathFromShortCut(nowValue)
                    AddImageFileList(link)
                Else
                    'ショートカット以外
                    AddImageFileList(nowValue)
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("MakeFileListFromPathList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '=======================================================================
    'DragAndDrop
    Public Sub MakeFileListByDradAndDrop(sender As Object, e As DragEventArgs)
        Try
            InitializeImageList()
            If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
                'ドラッグ中のファイルやディレクトリの取得
                Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

                For i = 0 To files.Length - 1
                    If (Right(files(i), 4).Equals(".lnk")) Then
                        'ショートカット
                        Dim link As String = New ShortCut().getFilePathFromShortCut(files(i))
                        AddImageFileList(link)
                    Else
                        'ショートカット以外
                        AddImageFileList(files(i))
                    End If
                Next
                e.Effect = DragDropEffects.Copy
            End If
        Catch ex As Exception
            Console.WriteLine("MakeFileListByDradAndDrop")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '=======================================================================
    Public Sub AddImageFileList(ArgFilePath As String)
        Try
            'ファイルorフォルダ判定
            If (System.IO.File.Exists(ArgFilePath)) Then
                'リンクなら中身を取得
                If Right(ArgFilePath, 4).Equals(".lnk") Then
                    Dim link As String = New ShortCut().getFilePathFromShortCut(ArgFilePath)
                    'ファイルならリストに追加
                    AddImageFileList(link)
                Else
                    'リンク以外
                    'ファイルならリストに追加
                    FileList.Add(ArgFilePath)
                End If
            ElseIf (System.IO.Directory.Exists(ArgFilePath)) Then
                'フォルダなら
                'サブフォルダ読み込み設定値が現在のサブフォルダカウント以上なら
                If CountReadSubFolderHierarchy >= Me.CountReadSubFolder Then
                    Me.CountReadSubFolder = Me.CountReadSubFolder + 1 '現在のサブフォルダカウントをUP
                    'フォルダ内のファイル一覧を取得しファイルリスト(再帰的)
                    'フォルダ内のファイル一覧を取得
                    Dim files As String() = System.IO.Directory.GetFiles(
                        ArgFilePath, "*", System.IO.SearchOption.AllDirectories)
                    For i = 0 To files.Length - 1
                        AddImageFileList(files(i))
                    Next
                    Me.CountReadSubFolder = Me.CountReadSubFolder - 1 '現在のサブフォルダカウントをDown
                End If
            End If

        Catch ex As Exception
            Console.WriteLine("AddImageFileList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '=======================================================================
    'イメージファイルを追加
    Public Sub AddImageFileListFromSameFolderAllOfFile(path As String)
        Try
            '存在チェック
            If Not New MyFile().isExistsPath(path) Then
                Console.WriteLine(Me.ToString & " : AddImageFileListFolderAll  FilePath Is Nothing")
                Console.WriteLine(Me.ToString & " : AddImageFileListFolderAll  path = " & path)
                addLog(3, Me.ToString & " : AddImageFileListFolderAll  FilePath Is Nothing")
                addLog(3, Me.ToString & " : AddImageFileListFolderAll  path = " & path)
                Exit Sub
            End If
            Dim folderPath As String = path
            'ファイルか
            If New MyFile().PathIsFile(folderPath) Then
                'ファイルならフォルダパスを取り出す
                folderPath = New MyFile().getFolderPathFromFilePath(folderPath)
            End If

            'リストに追加
            AddImageFileList(folderPath)

        Catch ex As Exception
            Console.WriteLine("AddImageFileListFolderAll")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '=======================================================================
    Public Function IsMatchFileType() As Boolean
        IsMatchFileType = True
    End Function
    '=======================================================================
    '拡張子を判定　表示できるファイル数をカウント
    Public Function CountFileListIsPaintAble(ReadFileTypeList() As String) As Integer
        Try
            Dim count As Integer = 0
            For i = 0 To Me.FileList.Count - 1
                For j = 0 To ReadFileTypeList.Count - 1
                    If Me.CharactorOfRightFourOfStringAIsMatchStringB(
                        Me.FileList(i), ReadFileTypeList(j)) Then
                        count += 1
                    End If
                Next
            Next
            CountFileListIsPaintAble = count
        Catch ex As Exception
            Console.WriteLine("FileIsPaintAble")
            Console.WriteLine(ex.Message)
            CountFileListIsPaintAble = 0
        End Try
    End Function

    'Settingsで設定された表示可能拡張子リストに合致するか
    Public Function FileIsPaintAble(argFilePath As String, ReadFileTypeList() As String) As Boolean
        Try
            FileIsPaintAble = False
            For i = 0 To ReadFileTypeList.Count - 1
                If CharactorOfRightFourOfStringAIsMatchStringB(
                    argFilePath, ReadFileTypeList(i)) Then
                    FileIsPaintAble = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("FileIsPaintAble")
            Console.WriteLine(ex.Message)
            FileIsPaintAble = False
        End Try
    End Function

    '拡張子を判定　合致すればTRUE
    Public Function CharactorOfRightFourOfStringAIsMatchStringB(StringA As String, StringB As String) As Boolean
        Try
            If Right(UCase(StringA), 4).Equals(UCase(StringB)) Then
                CharactorOfRightFourOfStringAIsMatchStringB = True
            Else
                CharactorOfRightFourOfStringAIsMatchStringB = False
            End If
        Catch ex As Exception
            Console.WriteLine("FileIsPaintAble")
            Console.WriteLine(ex.Message)
            CharactorOfRightFourOfStringAIsMatchStringB = False
        End Try
    End Function
End Class
