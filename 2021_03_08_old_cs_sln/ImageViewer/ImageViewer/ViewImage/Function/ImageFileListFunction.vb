Public Class ImageFileListFunction
    Inherits AbstractImageViewerChild

    'Friend mMainProcessorObject As MainProcesser
    'Friend mViewImageManager As ViewImageManager
    'Friend Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
    'Friend Sub New()
    'Friend Sub setMainProcessor(argMainProcessor As MainProcesser)
    'Friend Sub setViewImageManager(argViewImageManager As ViewImageManager)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser, argViewImageManager As ViewImageManager)
        MyBase.New(argMainProcessor, argViewImageManager)
        mMainProcessorObject = argMainProcessor
        mViewImageManager = argViewImageManager
    End Sub


    Public Function getNowFilePath() As String
        Return mViewImageManager.gImageFileList.getNowFilePath
    End Function

    Public Function getNowFolerPath() As String
        Return mViewImageManager.gImageFileList.getNowFolder
    End Function

    'ファイルリストをセット
    Public Sub setFileList(listbuf As List(Of String))
        mViewImageManager.gImageFileList.setViewFileList(listbuf)
    End Sub

    'ファイル名のところまで移動
    Public Sub moveIndexByFileName(filename As String)
        mViewImageManager.gImageFileList.moveIndexToMatchEndWithValue(filename, 0)
    End Sub

    'リストの最後に移動
    Public Sub moveIndexLastOfList()
        mViewImageManager.gImageFileList.moveListIndex(
            mViewImageManager.gImageFileList.Count - 1, 1)
    End Sub

    Public Function getFileTypeNow() As String
        Return mViewImageManager.gImageFileList.getFileType(mViewImageManager.gImageFileList.getNowFilePath)
    End Function

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
            addLogForSystemError("openFile")
            addLogForSystemError(ex.Message)
            Return Nothing
        End Try
    End Function

    'リストをファイル順に(ランダム解除)
    Public Sub ListToBeRandom()
        mViewImageManager.gImageFileList.resetListOrder()
        mViewImageManager.gState.gListRandom = False

    End Sub

    'リストをランダム順に ランダム適用
    Public Sub listOrderToBeRandom()
        'リストをランダムに
        mViewImageManager.gImageFileList.makeRandomListAndFileNameKeep()
    End Sub

End Class
