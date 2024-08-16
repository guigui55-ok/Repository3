Public Class FileIoFunction
    Inherits AbstractFunction

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)

    Public Sub New(argMainProcessorObject As MainProcesser)
        MainProcessorObject = argMainProcessorObject
    End Sub
    '=======================================================================
    Public Sub setFileList()

    End Sub

    Protected Overrides Sub Finalize()
        Me.Dispose()
        MyBase.Finalize()
    End Sub

    Public Sub Dispose()
        Try
            GC.SuppressFinalize(Me)
        Catch ex As Exception
            addLogForSystemError("Dispose")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Function getCurrentDirectory() As String
        Return New CommonFile().getCurrentDirectory()
    End Function

    Public Function getFilePathFromOpenDialog() As String
        'ファイルを開くダイアログからファイルパスを得る
        Dim fileOpen As MyFileOpenDialog = New MyFileOpenDialog()
        Try
            Dim filepath As String = fileOpen.getFileByDialog()
            'Dispose->Nothing
            If Not fileOpen Is Nothing Then
                fileOpen.Dispose()
            End If
            fileOpen = Nothing
            Return filepath
        Catch ex As Exception
            addLogForSystemError("openFile")
            addLogForSystemError(ex.Message)
            Return ""
        Finally
            If Not fileOpen Is Nothing Then
                fileOpen.Dispose()
            End If
            fileOpen = Nothing
        End Try
    End Function

    '次のフォルダを得る　一つ前の階層の次のフォルダのこと
    Public Function getNextFolder(path As String) As String
        Return New MyFile().getNextFolderInNowFolder(path)
    End Function

    '前のフォルダを得る　一つ前の階層の次のフォルダのこと
    Public Function getPreviousFolder(path As String) As String
        Return New MyFile().getPreviousFolderInNowFolder(path)
    End Function

    '一つ下の階層のフォルダへ（子フォルダへ）
    '一つ上の階層のフォルダへ（親フォルダへ）

    Public Sub openFile(argViewImageManager As ViewImageManager)
        Dim fileOpen As MyFileOpenDialog = New MyFileOpenDialog()
        Try
            'ファイルを開くダイアログからファイルパスを得る
            Dim filepath As String = fileOpen.getFileByDialog()

            addLog(3, Me.ToString & "openFile ", filepath)

            Dim listMaker As FileListMaker = New FileListMaker()
            listMaker.setCountReadSubFolderHierarchy(0) '読み込みフォルダ階層0
            'リスト作成
            listMaker.AddImageFileListFromSameFolderAllOfFile(filepath)
            'リストをアプリ側にコピー
            argViewImageManager.gImageFileList.setViewFileList(listMaker.getFileList)
            'ファイル名
            Dim filename As String = New MyFile().getFileNameFromPath(filepath)

            'ランダム中ならランダムに？
            'リストをファイル順に(ランダム解除)
            argViewImageManager.gImageFileList.resetListOrder()
            argViewImageManager.gState.gListRandom = False

            'ファイル名のところまで移動
            argViewImageManager.gImageFileList.moveIndexToMatchEndWithValue(filepath, 0)
            'Dispose->Nothing
            If Not fileOpen Is Nothing Then
                fileOpen.Dispose()
            End If
            fileOpen = Nothing
        Catch ex As Exception
            addLogForSystemError("openFile")
            addLogForSystemError(ex.Message)
        Finally
            If Not fileOpen Is Nothing Then
                fileOpen.Dispose()
            End If
            fileOpen = Nothing
        End Try
    End Sub

End Class
