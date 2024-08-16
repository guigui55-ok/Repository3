Public Class FileListFunction
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
    Public Function getNowFilePath(argViewImageManager As ViewImageManager) As String
        Return argViewImageManager.gImageFileList.getNowFilePath
    End Function

    'ファイルリストをセット
    Public Sub setFileList(argViewImageManager As ViewImageManager, listbuf As List(Of String))
        argViewImageManager.gImageFileList.setViewFileList(listbuf)
    End Sub

    'ファイル名のところまで移動
    Public Sub moveIndexByFileName(argViewImageManager As ViewImageManager, filename As String)
        argViewImageManager.gImageFileList.moveIndexToMatchEndWithValue(filename, 0)
    End Sub

    'リストの最後に移動
    Public Sub moveIndexLastOfList(argViewImageManager As ViewImageManager)
        argViewImageManager.gImageFileList.moveListIndex(
            argViewImageManager.gImageFileList.Count - 1, 1)
    End Sub

    'ファイルパスからファイルリストを作成 Hierarchy_読み込みフォルダ階層
    Public Function makeListFromFilePath(filepath As String, Hierarchy As Integer) As List(Of String)
        Try
            'New
            Dim listMaker As FileListMaker = New FileListMaker()
            '読み込み階層
            listMaker.setCountReadSubFolderHierarchy(Hierarchy) '読み込みフォルダ階層0
            'リスト作成
            listMaker.AddImageFileListFromSameFolderAllOfFile(filepath)
            'Log
            addLog(3, Me.ToString & ": makeListFromFilePath path = ", filepath)
            addLog(3, Me.ToString & ": makeListFromFilePath count = ", listMaker.getFileList.Count)
            Return listMaker.getFileList

        Catch ex As Exception
            addLogForSystemError("makeListFromFilePath")
            addLogForSystemError(ex.Message)
            Return Nothing
        End Try
    End Function

    'リストをファイル順に(ランダム解除)
    Public Sub ListToBeRandom(argViewImageManager As ViewImageManager)
        argViewImageManager.gImageFileList.resetListOrder()
        argViewImageManager.gState.gListRandom = False
    End Sub

    'リストをランダム順に ランダム適用
    Public Sub listOrderToBeRandom(argViewImageManager As ViewImageManager)
        'リストをランダムに
        argViewImageManager.gImageFileList.makeRandomListAndFileNameKeep()
    End Sub

End Class
