Imports ImageViewer

Public Class ViewImageMainMethodFactory
    Implements IViewImageMain
    Implements IDisposable

    '※Factory兼ViewImageになっている

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)
    Private mViewImageManager As ViewImageManager
    Private mViewMain As IViewImageMain
    '=======================================================================
    Public Sub New(argViewImageManager As ViewImageManager)
        mViewImageManager = argViewImageManager
    End Sub

    Public Sub DisposeObjects()
        Try
            'If Not mViewImageManager Is Nothing Then
            '    mViewImageManager.disposeObjects()
            '    mViewImageManager = Nothing
            'End If
            If Not mViewMain Is Nothing Then
                mViewMain.Dispose()
                mViewMain = Nothing
            End If
        Catch ex As Exception
            addLogForSystemError("ViewImageMainMethodFactory.DisposeObjects")
            addLogForSystemError(ex.Message)
        End Try
    End Sub
    '===============================================================================================
    Private isDisposed As Boolean = False ' リソースが破棄(解放)されていることを表すフラグ

    ' IDisposable.Disposeの実装
    '// Dispose() calls Dispose(true)
    Public Sub Dispose() Implements IViewImageMain.Dispose, IDisposable.Dispose
        ' アンマネージリソースと、マネージリソースの両方を破棄させる
        Dispose(True)
        ' すべてのリソースが破棄されているため、以後ファイナライザの実行は不要であることをガベージコレクタに通知する
        GC.SuppressFinalize(Me)
    End Sub
    ' リソースの解放処理を行うためのメソッド
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        ' 既にリソースが破棄されている場合は何もしない
        If isDisposed Then Return

        ' 破棄されていないアンマネージリソースの解放処理を行う
        DisposeObjects()
        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================

    Public Sub excuteViewImage() Implements IViewImageMain.excuteViewImage
        Try
            If mViewMain Is Nothing Then
                modeChange()
            Else
                If mViewImageManager.gState.gFade.FadeInBegin Then modeChange()
                If mViewImageManager.gState.gFade.FadeOutBegin Then modeChange()
                If mViewImageManager.gState.gMovie.MoviePlayBegin Then modeChange()
            End If
            'ControlChange
            'FileType
            'Effect-fade->modeChange
            If isAbleViewNowPath() Then

            End If
            mViewMain.excuteViewImage()
        Catch ex As Exception
            addLogForSystemError("ViewImageMainMethodFactory.excuteViewImage")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Public Function isAbleViewNowPath() As Boolean
        If mViewImageManager.gImageFileList.FlagAsViewAbleFileListIsNothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub modeChange()
        Dim tPlayGif As PlayGif = New PlayGif()
        Try
            addLog(11, "ViewImageMainMethodFactory modeChange")
            If mViewImageManager.gState.gViewModeChangeNow Then
                Dispose()
                mViewMain = Nothing
                If mViewImageManager.gState.gFade.isNowFade Then
                    'Fade_True

                    mViewMain = New ViewImageWithFadeEffect(mViewImageManager)
                    'PaintFace->Fade_Start->MainProcessor->paintImageWithTransparncy
                Else
                    Dim nowFileType As String = mViewImageManager.gFunction.gImageFileLIst.getFileTypeNow()
                    Select Case UCase(nowFileType)
                        Case ".GIF"
                            If tPlayGif.isMovie(mViewImageManager.gImageFileList.getNowFilePath) Then
                                mViewMain = New PlayGif(mViewImageManager)
                            Else
                                mViewMain = New ViewMainDefault(mViewImageManager)
                            End If
                            'If Not tPlayGif Is Nothing Then
                            '    tPlayGif.Dispose()
                            '    tPlayGif = Nothing
                            'End If
                        Case Else
                            'Fade_False
                            mViewMain = New ViewMainDefault(mViewImageManager)
                    End Select
                End If
            End If
        Catch ex As Exception
            addLogForSystemError("ViewImageMainMethodFactory.excuteViewImage")
            addLogForSystemError(ex.Message)
        Finally
            If Not tPlayGif Is Nothing Then
                tPlayGif.Dispose()
                tPlayGif = Nothing
            End If
        End Try
    End Sub


    Public Sub setImage(argImage As Image) Implements IViewImageMain.setImage
        mViewMain.setImage(argImage)
    End Sub

    Public Sub setImageFormPath(argPath As String) Implements IViewImageMain.setImageFormPath
        mViewMain.setImageFormPath(argPath)
    End Sub

    Public Sub setControlBridge(argControlBridge As AbstractControlBridge) Implements IViewImageMain.setControlBridge
        mViewMain.setControlBridge(argControlBridge)
    End Sub

    Public Sub setDrawSize(argSize As Size) Implements IViewImageMain.setDrawSize
        mViewMain.setDrawSize(argSize)
    End Sub

    Public Sub PlayFile() Implements IViewImageMain.PlayFile
        mViewMain.PlayFile()
    End Sub

    Public Sub PlayFileEvent(sender As Object, e As EventArgs) Implements IViewImageMain.PlayFileEvent
        mViewMain.PlayFileEvent(sender, e)
    End Sub

    Public Sub ReplayFile(argControlSizeForPlay As Size) Implements IViewImageMain.ReplayFile
        mViewMain.ReplayFile(argControlSizeForPlay)
    End Sub

    Public Sub ReplayFile(argControlSizeForPlay As Size, argPlayFrameNumber As Integer) Implements IViewImageMain.ReplayFile
        mViewMain.ReplayFile(argControlSizeForPlay, argPlayFrameNumber)
    End Sub

    Public Sub StopFile() Implements IViewImageMain.StopFile
        mViewMain.StopFile()
    End Sub

    Public Sub PauseFile() Implements IViewImageMain.PauseFile
        If mViewMain Is Nothing Then
            Exit Sub
        End If
        mViewMain.PauseFile()
    End Sub

    Public Sub PauseFile(argControlSizeForPlay As Size) Implements IViewImageMain.PauseFile
        mViewMain.PauseFile(argControlSizeForPlay)
    End Sub

    Public Sub ForwardFile() Implements IViewImageMain.ForwardFile
        mViewMain.ForwardFile()
    End Sub

    Public Sub RewindFile() Implements IViewImageMain.RewindFile
        mViewMain.RewindFile()
    End Sub

    Public Sub ChangeWindowSize() Implements IViewImageMain.ChangeWindowSize
        mViewMain.RewindFile()
    End Sub

    Public Sub setTimer(argTimer As Timer) Implements IViewImageMain.setTimer
        mViewMain.setTimer(argTimer)
    End Sub

    Public Sub TimerStart() Implements IViewImageMain.TimerStart
        mViewMain.TimerStart()
    End Sub

    Public Sub timerStop() Implements IViewImageMain.timerStop
        Throw New NotImplementedException()
    End Sub

    Public Function getCountFrame(argImage As Image) As Integer Implements IViewImageMain.getCountFrame
        Throw New NotImplementedException()
    End Function

    Public Function isThisClassType(argPath As String) As Boolean Implements IViewImageMain.isThisClassType
        Throw New NotImplementedException()
    End Function

    Public Function isMovie(argPath As String) As Boolean Implements IViewImageMain.isMovie
        Throw New NotImplementedException()
    End Function

    Public Function isEndFrameNow() As Boolean Implements IViewImageMain.isEndFrameNow
        Throw New NotImplementedException()
    End Function

    Public Function getFrameNow() As Integer Implements IViewImageMain.getFrameNow
        Throw New NotImplementedException()
    End Function

    Public Function getDrawImage() As Image Implements IViewImageMain.getDrawImage
        Throw New NotImplementedException()
    End Function
End Class
