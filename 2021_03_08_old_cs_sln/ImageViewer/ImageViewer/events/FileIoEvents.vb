Public Class FileIoEvents
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

    Public Sub openFile(argViewImageManager As ViewImageManager)
        Try
            'ファイルを開くダイアログからファイルパスを得る
            Dim filepath As String = MainProcessorObject.gFunction.gFileIo.getFilePathFromOpenDialog()
            'Log
            addLog(3, Me.ToString & "openFile ", filepath)
            'ファイルパスからファイルリストを作成 Hierarchy_読み込みフォルダ階層
            Dim listbuf As List(Of String) = MainProcessorObject.gFunction.gFileList.makeListFromFilePath(filepath, 0)

            'ファイルを開く＞キャンセルでリストCount0になる
            If listbuf.Count <= 0 Then
                addLog(3, Me.ToString & "openFile Cancel : listbuf.Count Is Zero,path = ", filepath)
                Exit Sub
            End If

            'リストをアプリ側にコピー
            MainProcessorObject.gFunction.gFileList.setFileList(argViewImageManager, listbuf)

            'ランダム中ならランダムに
            If argViewImageManager.gState.gListRandom Then
                MainProcessorObject.gFunction.gFileList.ListToBeRandom(argViewImageManager)
            End If

            'ファイル名を抽出
            Dim filename As String = New MyFile().getFileNameFromPath(filepath)
            'ファイル名のところまで移動
            argViewImageManager.gFunction.gImageFileLIst.moveIndexByFileName(filepath)
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ": openFile")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub setListByPathToListFunction(argViewImageManager As ViewImageManager, path As String)
        Try
            'パスはチェック済み
            '読み込み時にフォルダをセット
            argViewImageManager.gState.gNowFolderPath = New MyFile().getFolderPathFromFilePath(path)
            'ファイルパスからファイルリストを作成 Hierarchy_読み込みフォルダ階層
            Dim listbuf As List(Of String) = MainProcessorObject.gFunction.gFileList.makeListFromFilePath(path, 0)
            'リストをアプリ側にコピー
            argViewImageManager.gFunction.gImageFileLIst.setFileList(listbuf)
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ": setListToFileFunction")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub moveFolder(argViewImageManager As ViewImageManager, path As String)
        Try
            '今のパス
            Dim NowPath As String = argViewImageManager.gFunction.gImageFileLIst.getNowFilePath()
            '移動するフォルダ
            Dim MoveFolderPath As String = path
            '読み込み時にフォルダをセット
            argViewImageManager.gState.gNowFolderPath = New MyFile().getFolderPathFromFilePath(MoveFolderPath)
            'ファイルパスからファイルリストを作成 Hierarchy_読み込みフォルダ階層
            Dim listbuf As List(Of String) = argViewImageManager.gFunction.gImageFileLIst.makeListFromFilePath(MoveFolderPath, 0)
            'リストをアプリ側にコピー
            argViewImageManager.gFunction.gImageFileLIst.setFileList(listbuf)
            'ランダム中ならランダムに
            If argViewImageManager.gState.gListRandom Then
                argViewImageManager.gFunction.gImageFileLIst.ListToBeRandom()
            End If
            '表示
            argViewImageManager.gState.gChangeFileBegin = True
            argViewImageManager.gEvents.gViewer.DoFadeOutBeginFlagToBeTrueIfFadeSettingON()
            argViewImageManager.gFunction.gViewer.PauseFile()
            argViewImageManager.gEvents.gViewer.View()
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ": moveFolder")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub moveNextFolder(argViewImageManager As ViewImageManager)
        Try
            addLog(3, "MainProcessorObject.argViewImageManager.gEvents.FileIoEvents:moveNextFolder")
            '今のパス
            Dim NowPath As String = argViewImageManager.gFunction.gImageFileLIst.getNowFilePath()
            '次のフォルダ
            Dim NextFolderPath As String = MainProcessorObject.gFunction.gFileIo.getNextFolder(NowPath)
            '移動
            moveFolder(argViewImageManager, NextFolderPath)
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ": moveNextFolder")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Sub moveBeforeFolder(argViewImageManager As ViewImageManager)
        Try
            addLog(3, "MainProcessorObject.argViewImageManager.gEvents.FileIoEvents:moveBeforeFolder")
            '今のパス
            Dim NowPath As String = argViewImageManager.gFunction.gImageFileLIst.getNowFilePath()
            '次のフォルダ
            Dim NextFolderPath As String = MainProcessorObject.gFunction.gFileIo.getPreviousFolder(NowPath)
            '移動
            moveFolder(argViewImageManager, NextFolderPath)
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ": moveBeforeFolder")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
End Class
