Public Class ImageFileListEvents
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

    'Public Event ChangeFile(ByVal sender As Object, ByVal FileName As String)
    'D&Dで得たパスリストから、ファイルリストを得る
    Public Function getFileListFromPathList(pathList As List(Of String)) As List(Of String)
        Dim filelist As List(Of String) = New List(Of String)
        Try
            'ファイル存在チェック
            For Each value In pathList
                If Not New MyFile().isExistsPath(value) Then
                    addLog(0, Me.ToString & ": getFileListFromPathList : path not exists : " & value)
                Else
                    addLog(3, Me.ToString & ": getFileListFromPathList : DragAndDrop ToMakeList : " & value)
                End If
            Next
            'MakeList
            Dim listMaker As FileListMaker = New FileListMaker()
            listMaker.MakeFileListFromPathList(pathList)
            Return listMaker.getFileList
        Catch ex As Exception
            addLogForSystemError("getFileListFromPathList")
            addLogForSystemError(ex.Message)
            Return filelist
        End Try
    End Function


    'pathからリストを作成_メイン
    Public Sub makeListFromFilePath(path As String, hierarchy As Integer)
        Try
            addLog(3, Me.ToString & ".makeListFromFilePath : path = " & path)
            'リスト作成
            Dim buflist As List(Of String) = mViewImageManager.gFunction.gImageFileLIst.makeListFromFilePath(path, hierarchy)
            'CountZeroなら抜ける
            If buflist.Count <= 0 Then
                addLog(3, Me.ToString & ".makeListFromFilePath : List.Count is Zero")
                Exit Sub
            End If
            'リスト登録
            mViewImageManager.gFunction.gImageFileLIst.setFileList(buflist)
            '読み込み時にフォルダをセット
            mViewImageManager.gState.gNowFolderPath = New MyFile().getFolderPathFromFilePath(path)
        Catch ex As Exception
            addLogForSystemError("makeListFromFilePath")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'FileIoEventと被り
    Public Sub openFile()
        mMainProcessorObject.gFunction.gFileIo.openFile(mViewImageManager)
        addLog(3, Me.ToString & "openFile FileListCount=", mMainProcessorObject.gViewImageManager.gImageFileList.Count)
        mMainProcessorObject.gViewImageManager.gState.gFade.FadeOutBegin = True

        mViewImageManager.gEvents.gViewer.View()
    End Sub

    Public Sub changeModeListOrder()
        If mViewImageManager.gState.gListRandom Then
            'Random true -> random false
            listOrderToBeNormal()
        Else
            'random false -> random true
            listOrderToBeRandom()
        End If
        addLog(3, "changeModeListOrder : mViewImageManager.gState.gListRandom = " & mViewImageManager.gState.gListRandom)
    End Sub

    Public Sub listOrderToBeRandom()
        'リストをランダムに
        'リストをランダム順に ランダム適用
        mViewImageManager.gFunction.gImageFileLIst.listOrderToBeRandom()
        '-----
        'Stateを変更
        mViewImageManager.gState.gListRandom = True
        'MenuStripを変更ランダムに
        mMainProcessorObject.gMainForm.gControls.gMenuStripEvents.changeTextForRandom(mViewImageManager)
        '表示
        mViewImageManager.gState.gChangeFileBegin = True
        mViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        mViewImageManager.gFunction.gViewer.PauseFile()
        mViewImageManager.gEvents.gViewer.View()
    End Sub

    Public Sub listOrderToBeNormal()
        'リストをランダムからNormal（ファイル名順）に
        'リストをファイル順に(ランダム解除) makeRandomList
        mViewImageManager.gImageFileList.resetListOrder()
        '-----
        'Stateを変更
        mMainProcessorObject.gState.ListRandom = False
        mViewImageManager.gState.gListRandom = False
        'MenuStripを変更ランダムに
        mMainProcessorObject.gMainForm.gControls.gMenuStripEvents.changeTextForRandom(mViewImageManager)
        '表示
        mViewImageManager.gState.gChangeFileBegin = True
        mViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
        mViewImageManager.gFunction.gViewer.PauseFile()
        mViewImageManager.gEvents.gViewer.View()
    End Sub

    'リストインデックスを移動
    Public Sub MoveListIndex(argIndex As Integer, DirectionFlag As Integer)
        mViewImageManager.gImageFileList.moveListIndex(argIndex, DirectionFlag)
        'mMainProcessorObject.gState.Gif.resetFlagAll()
        'mViewImageManager.gState.gif
    End Sub

    Public Sub moveLastIndex()
        mViewImageManager.gFunction.gImageFileLIst.moveIndexLastOfList()
    End Sub
End Class
