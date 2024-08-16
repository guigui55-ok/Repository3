
Imports System.Drawing
Imports System.Xaml
Imports System.IO
Imports System.Drawing.Imaging

Imports ImageViewer
Imports System.Threading

Public Class PlayGif
    Implements IViewImageMain
    Implements IDisposable

    Private gifImage As Image
    Private Canvas As Image
    Private MaxFrameCount As Integer
    Private NowFrameNum As Integer
    Private ControlSizeForPlay As Size
    Private DrawControl As AbstractControlBridge

    WithEvents PlayTimer As System.Windows.Forms.Timer

    Private mViewImageManager As ViewImageManager
    Public gMovieState As MovieState

    Public Sub New(argViewImageManager As ViewImageManager)
        mViewImageManager = argViewImageManager
        gMovieState = New MovieState()
        gMovieState.resetAll()
        addLog(3, "PlayGif New")

        DrawControl = mViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge
    End Sub

    Public Sub New()

    End Sub

    Public Sub Initialize(argPath As String)
        setImageFormPath(argPath)
        PlayTimer = New System.Windows.Forms.Timer()
        PlayTimer.Interval = 80
        AddHandler PlayTimer.Tick, New EventHandler(AddressOf PlayFileEvent)
    End Sub


    Public Sub excuteViewImage() Implements IViewImageMain.excuteViewImage
        Initialize(mViewImageManager.gImageFileList.getNowFilePath)
        Me.ControlSizeForPlay = mViewImageManager.gFunction.gPictureBox.getPictureBox.Size
        Me.PlayFile()
        gMovieState.MoviePlay = True
        mViewImageManager.gState.gMovie = Me.gMovieState
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
        doDispose()

        ' リソースは破棄されている
        isDisposed = True
    End Sub
    'Finalize
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    '===============================================================================================

    Protected Sub doDispose()
        Try
            If Not gifImage Is Nothing Then
                gifImage.Dispose()
                gifImage = Nothing
            End If
            If Not Me.Canvas Is Nothing Then
                Me.Canvas.Dispose()
                Canvas = Nothing
            End If
            NowFrameNum = 0
            If Not PlayTimer Is Nothing Then
                PlayTimer.Stop()
                PlayTimer.Dispose()
                PlayTimer = Nothing
            End If
            If Not DrawControl Is Nothing Then
                DrawControl.dispose()
                DrawControl = Nothing
            End If
        Catch ex As Exception
            Console.WriteLine("Dispose")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '=============================================================

    Public Sub setImage(argImage As Image) Implements IViewImageMain.setImage
        gifImage = argImage
    End Sub

    Public Sub setControlBridge(argControlBridge As AbstractControlBridge) Implements IViewImageMain.setControlBridge
        DrawControl = argControlBridge
    End Sub

    Public Sub setImageFormPath(argPath As String) Implements IViewImageMain.setImageFormPath
        gifImage = Image.FromFile(argPath)
        'FrameDimensionを取得する
        Dim fd As New FrameDimension(gifImage.FrameDimensionsList(0))
        'フレーム数を取得する
        MaxFrameCount = gifImage.GetFrameCount(fd)
        NowFrameNum = 0
    End Sub

    '=====================================================================================

    Public Sub PlayFile() Implements IViewImageMain.PlayFile
        If PlayTimer Is Nothing Then
            addLog(0, "PlayGif.PlayFile : PlayTimer Is Nothing")
            Exit Sub
        End If
        If PlayTimer.Enabled = False Then
            PlayTimer.Start()
        End If
    End Sub

    Public Sub PlayFileEvent(sender As Object, e As EventArgs) Implements IViewImageMain.PlayFileEvent
        If PlayTimer Is Nothing Then Exit Sub
        'PlayTimer.Start()
        countUpNowFrame()
        RePlayFile(ControlSizeForPlay, NowFrameNum)
    End Sub

    Public Sub ReplayFile(argControlSizeForPlay As Size) Implements IViewImageMain.ReplayFile
        RePlayFile(argControlSizeForPlay, Me.NowFrameNum)
    End Sub

    Public Sub StopFile() Implements IViewImageMain.StopFile
        If PlayTimer Is Nothing Then Exit Sub
        PlayTimer.Stop()
        If NowFrameNum = 0 Then Exit Sub
        NowFrameNum = 0
        RePlayFile(ControlSizeForPlay, 0)
    End Sub

    Public Sub PauseFile() Implements IViewImageMain.PauseFile
        PauseFile(ControlSizeForPlay)
    End Sub

    Public Sub PauseFile(argControlSizeForPlay As Size) Implements IViewImageMain.PauseFile
        If PlayTimer Is Nothing Then Exit Sub
        PlayTimer.Stop()
        RePlayFile(ControlSizeForPlay, NowFrameNum)
    End Sub

    'TimerEvent
    Public Sub RePlayFile(argControlSizeForPlay As Size, ByVal PlayFrameNumber As Integer) Implements IViewImageMain.ReplayFile
        Try
            If argControlSizeForPlay.Equals(New Size(0, 0)) Then
                addLogForSystemError("PlayGif.RePlayFile : argControlSizeForPlay is nothing")
            End If

            'PlayFrameNumber = NowFrameNum
            '描画先とするImageオブジェクトを作成する
            Canvas = New Bitmap(argControlSizeForPlay.Width, argControlSizeForPlay.Height)

            'ImageオブジェクトのGraphicsオブジェクトを作成する
            Dim g As Graphics = Graphics.FromImage(Canvas)

            'FrameDimensionを取得する
            Dim fd As New FrameDimension(gifImage.FrameDimensionsList(0))

            'ゼロ以下ならゼロに
            If PlayFrameNumber < 0 Then PlayFrameNumber = 0
            'Max以上になったらゼロに
            If MaxFrameCount <= PlayFrameNumber Then
                PlayFrameNumber = MaxFrameCount - 1
                PlayFrameNumber = 0
            End If

            'フレームを選択する
            PlayFrameNumber = gifImage.SelectActiveFrame(fd, NowFrameNum)
            '画像を描画する
            g.DrawImage(gifImage, 0, 0, argControlSizeForPlay.Width, argControlSizeForPlay.Height)

            g.Dispose()
            g = Nothing
            fd = Nothing

            DrawControl.setImage(Canvas)
        Catch ex As Exception
            Console.WriteLine("RePlayFile")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '=====================================================================================

    Public Sub ForwardFile() Implements IViewImageMain.ForwardFile
        Throw New NotImplementedException()
    End Sub

    Public Sub RewindFile() Implements IViewImageMain.RewindFile
        Throw New NotImplementedException()
    End Sub

    Public Function getDrawImage() As Image Implements IViewImageMain.getDrawImage
        Return Canvas
    End Function

    Public Function getFrameNow() As Integer Implements IViewImageMain.getFrameNow
        Return NowFrameNum
    End Function

    Public Sub setTimer(argTimer As System.Windows.Forms.Timer) Implements IViewImageMain.setTimer
        PlayTimer = argTimer
    End Sub

    Public Sub TimerStart() Implements IViewImageMain.TimerStart
        PlayTimer.Start()
    End Sub

    Public Sub timerStop() Implements IViewImageMain.timerStop
        If PlayTimer Is Nothing Then Exit Sub
        PlayTimer.Stop()
        NowFrameNum = 0
    End Sub


    Public Sub setDrawSize(argSize As Size) Implements IViewImageMain.setDrawSize
        ControlSizeForPlay = argSize
    End Sub

    Public Sub ChangeWindowSize() Implements IViewImageMain.ChangeWindowSize
        Throw New NotImplementedException()
    End Sub
    '=====================================================================================

    Public Function getCountFrame(argImage As Image) As Integer Implements IViewImageMain.getCountFrame
        Try
            If argImage Is Nothing Then
                Return -2
            Else
                Return argImage.GetFrameCount(New FrameDimension(argImage.FrameDimensionsList(0)))
            End If
        Catch ex As Exception
            Console.WriteLine("getFrameCount")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    Public Function isMovie(argPath As String) As Boolean Implements IViewImageMain.isMovie
        Try
            Dim imageFromPath As Image = Image.FromFile(argPath)
            If getCountFrame(imageFromPath) > 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("isMovie")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function isThisClassType(argPath As String) As Boolean Implements IViewImageMain.isThisClassType
        Try
            If Right(UCase(argPath), 4).Equals(".GIF") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("isThisClassType")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function


    Public Function isEndFrameNow() As Boolean Implements IViewImageMain.isEndFrameNow
        If NowFrameNum >= MaxFrameCount - 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub countUpNowFrame()
        NowFrameNum += 1
        If NowFrameNum >= MaxFrameCount Then
            NowFrameNum = 0
        End If
        If NowFrameNum < 0 Then
            NowFrameNum = 0
        End If
    End Sub

End Class
