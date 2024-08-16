Public Class FileListEvents
    Inherits AbstractEvents

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)
    '=======================================================================
    Public Sub New(argMainProcessor As MainProcesser)
        MainProcessorObject = argMainProcessor
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

    'pathからリストを作成_メインNew

    '描画機能コントロールすべてを複数で使えるようにするため、FileListをViewImageManagerクラスに移動2020/06/09
    Public Sub makeListFromFilePath(path As String, hierarchy As Integer, controlIndex As Integer)
        Try
            'リスト作成
            Dim buflist As List(Of String) = MainProcessorObject.gFunction.gFileList.makeListFromFilePath _
                (path, hierarchy)
            'リスト登録
            MainProcessorObject.gViewImageManager.gFunction.gImageFileLIst.setFileList(buflist)


        Catch ex As Exception
            addLogForSystemError("makeListFromFilePath")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    'pathからリストを作成_メイン
    Public Sub makeListFromFilePath(argViewImageManager As ViewImageManager, path As String, hierarchy As Integer)
        Try
            'リスト作成
            Dim buflist As List(Of String) = MainProcessorObject.gFunction.gFileList.makeListFromFilePath _
                (path, hierarchy)
            'リスト登録
            MainProcessorObject.gFunction.gFileList.setFileList(argViewImageManager, buflist)
            '読み込み時にフォルダをセット
            argViewImageManager.gState.gNowFolderPath = New MyFile().getFolderPathFromFilePath(path)
        Catch ex As Exception
            addLogForSystemError("makeListFromFilePath")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'FileIoEventと被り
    Public Sub openFile(argViewImageManager As ViewImageManager)
        MainProcessorObject.gFunction.gFileIo.openFile(argViewImageManager)
        addLog(3, Me.ToString & "openFile FileListCount=", argViewImageManager.gImageFileList.Count)

        'argViewImageManager.gState.gFade.FadeOutBegin = True
        'MainProcessorObject.gFunction.gPaint.PaintMain(argViewImageManager)
        MainProcessorObject.gNowViewImageManager.gEvents.gViewer.SetViewTriger()
    End Sub

    Public Sub changeModeListOrder(argViewImageManager As ViewImageManager)
        If argViewImageManager.gState.gListRandom Then
            'Random true -> random false
            listOrderToBeNormal(argViewImageManager)
        Else
            'random false -> random true
            listOrderToBeRandom(argViewImageManager)
        End If
    End Sub

    Public Sub listOrderToBeRandom(argViewImageManager As ViewImageManager)
        'リストをランダムに
        'リストをランダム順に ランダム適用
        argViewImageManager.gFunction.gImageFileLIst.listOrderToBeRandom()
        '-----
        'Stateを変更
        argViewImageManager.gState.gListRandom = True
        'MenuStripを変更ランダムに
        MainProcessorObject.gMainForm.gControls.gMenuStripEvents.changeTextForRandom(argViewImageManager)
        '表示
        'argViewImageManager.gState.gFade.FadeOutBegin = True
        'MainProcessorObject.gFunction.gPaint.PaintMain(argViewImageManager)
        argViewImageManager.gEvents.gViewer.SetViewTriger()
    End Sub

    Public Sub listOrderToBeNormal(argViewImageManager As ViewImageManager)
        'リストをランダムからNormal（ファイル名順）に
        'リストをファイル順に(ランダム解除) makeRandomList
        argViewImageManager.gImageFileList.resetListOrder()
        '-----
        'Stateを変更
        argViewImageManager.gState.gListRandom = False
        'MenuStripを変更ランダムに
        MainProcessorObject.gMainForm.gControls.gMenuStripEvents.changeTextForRandom(argViewImageManager)
        '表示
        'argViewImageManager.gState.gFade.FadeOutBegin = True
        'MainProcessorObject.gFunction.gPaint.PaintMain(argViewImageManager)
        argViewImageManager.gEvents.gViewer.SetViewTriger()
    End Sub

    Public Sub MoveListIndex(argViewImageManager As ViewImageManager, argIndex As Integer, DirectionFlag As Integer)
        argViewImageManager.gImageFileList.moveListIndex(argIndex, DirectionFlag)
        'MainProcessorObject.gState.Gif.resetFlagAll()
        'argViewImageManager.gState.Gif.resetFlagAll()
        'addLogForSystemError("MainProcessorObject.State.Gif.resetFlagAll()")
    End Sub

    Public Sub moveLastIndex(argViewImageManager As ViewImageManager)
        MainProcessorObject.gFunction.gFileList.moveIndexLastOfList(argViewImageManager)
    End Sub
End Class
