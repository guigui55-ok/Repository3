Public Class MouseEvents
    Inherits AbstractEvents

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

    Public Sub MakeFileListByDragAndDrop(sender As Object, e As DragEventArgs)
        Try
            Dim FileListMakerObject As New FileListMaker()
            '読み込み階層数をセット
            FileListMakerObject.setCountReadSubFolderHierarchy(MainProcessorObject.gSettings.ReadSubFolder)
            'イベントからリストを得る
            FileListMakerObject.MakeFileListByDradAndDrop(sender, e)

            'ファイルリストチェック
            '対応ファイルが存在しない場合は描画エラーになるため、その判定を保持
            MainProcessorObject.gNowViewImageManager.gState.SaveStatusToCantPaintAllFile(
                MainProcessorObject.gNowViewImageManager.gImageFileList.CountFileListIsViewAble())

            '新たに追加したファイルリスト
            MainProcessorObject.gNowViewImageManager.gImageFileList.setViewFileList(FileListMakerObject.getFileList)
            'リストの最初に移動
            MainProcessorObject.gFunction.gPaint.MoveListIndex(MainProcessorObject.gNowViewImageManager, 0, 0)
            'リストをファイル順に(ランダム解除)
            MainProcessorObject.gNowViewImageManager.gState.gListRandom = False
            MainProcessorObject.gNowViewImageManager.gEvents.gImageFileList.changeModeListOrder()
            '指定したPictureBoxに表示
            MainProcessorObject.gNowViewImageManager.gState.gChangeFileBegin = True
            MainProcessorObject.gNowViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
            MainProcessorObject.gNowViewImageManager.gFunction.gViewer.PauseFile()
            MainProcessorObject.gNowViewImageManager.gEvents.gViewer.View()
        Catch ex As Exception
            addLogForSystemError("MakeFileListByDragAndDrop")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
End Class
