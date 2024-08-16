Imports DirectShowLib


'Public Const MyMSG = WM_APP + 500

Public Class PlayMovieFunction
    Private MainProcessorObject As MainProcesser
    Private FirstPlayFlag As Boolean = False


    Public Sub New(argMainProcessor As MainProcesser)
        MainProcessorObject = argMainProcessor
    End Sub

    Public Sub setControlSize()

    End Sub


    Public Sub playMovie(argViewImageManager As ViewImageManager)
        Try
            With MainProcessorObject
                If Not New FileTypeMovie().isThisClassType(argViewImageManager.gImageFileList.getNowFilePath) Then
                    '.gState.Gif.resetFlagAll()
                    .gNowViewImageManager.gState.gMovie.resetAll()
                    Exit Sub
                End If
                Dim ControlForPaintObject As IControlForPaint = .gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint
                'If Not New FileTypeGif().isMovie(.ViewFileListObject.getNowFilePath) Then
                '    'FrameCountが１以上ならExit
                '    Exit Sub
                'End If
                If .gNowViewImageManager.gState.gMovie.MoviePlayBegin Then
                    'Begin
                    ''オブジェクトをセット
                    'If (Not .PlayFileNow Is Nothing) Then
                    '    .PlayFileNow.Dispose()
                    'End If
                    '.PlayFileNow =
                    '.PlayFilesFactoryObject.GetPlayFileObjectBySetPath(
                    '    argViewImageManager.gImageFileList.getNowFilePath)
                    'If (.PlayFileNow Is Nothing) Then Exit Sub
                    ''サイズをセット
                    '.PlayFileNow.setDrawSize(ControlForPaintObject.getSize)
                    ''Drawコントロールをセット
                    '.PlayFileNow.setControlBridge(ControlForPaintObject.getControlBridge)
                    ''再生初回
                    '.PlayFileNow.TimerStart()
                    ''フラグ変更PlayBegin→Play
                    .gNowViewImageManager.gState.gMovie.MoviePlay = True

                ElseIf .gNowViewImageManager.gState.gMovie.MoviePlay Then

                ElseIf .gNowViewImageManager.gState.gMovie.MoviePause Then

                ElseIf .gNowViewImageManager.gState.gMovie.MovieStop Then

                End If
                'if isMovie
                'isPlayContinue frameCountIsEnd
                'MovieState.play
                Playtest()
            End With
        Catch ex As Exception
            Console.WriteLine("playMovie")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Property FirstPlay As Boolean
        Get
            Return FirstPlayFlag
        End Get
        Set(value As Boolean)
            FirstPlayFlag = value
        End Set
    End Property

    Private mMedia As IMediaControl
    Private mGraph As IFilterGraph
    Private mRenderer As IBaseFilter
    Private mWindow As IVideoWindow
    Private mEventEx As IMediaEventEx
    Private mBasicVideo As IBasicVideo
    Private mMovieSize As Size
    Private mGraphBuilder As IGraphBuilder
    Private mFilterGraph As FilterGraph

    Private mVideoFrameStep As IVideoFrameStep
    Private mMediaPosition As IMediaPosition
    Private mFrameTime As Double
    Private mVideoTime As Double
    Public Sub Playtest()


        If New FileTypeMovie().isThisClassType(MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath) Then
        Else
            Exit Sub
        End If

        Dim Num As Integer = 6
        Select Case Num
            Case 0 : PlayTest5()
            Case 1 : PlayTest01()
            Case 2 : PlayTest02()
            Case 3 : PlayTest3()
            Case 4 : PlayTest4()
            Case 5 : PlayTest5()
            Case 6 : PlayTest6()
        End Select
    End Sub



    Public Sub PlayTest6()
        Try
            Exit Sub
            Dim filepath As String = MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath
            Dim PlayControl As PictureBox =
                MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl

            mFilterGraph = New FilterGraph()
            mMedia = mFilterGraph
            mGraphBuilder = DirectCast(mFilterGraph, IGraphBuilder)
            mRenderer = DirectCast(mGraphBuilder, IBaseFilter)

            Dim hr As Integer
            hr = mGraphBuilder.FindFilterByName("Haali Video Renderer", mRenderer)
            'Private mRenderer As IBaseFilter
            hr = mGraphBuilder.AddSourceFilter(filepath, filepath, mRenderer)
            hr = mGraphBuilder.AddFilter(mRenderer, "Microsoft DTV-DVD Video Decoder")


            hr = mGraphBuilder.RenderFile(filepath, Nothing)


            mBasicVideo = DirectCast(mGraphBuilder, IBasicVideo)
            '動画サイズ取得
            Dim width, height As Integer
            mBasicVideo.get_VideoWidth(width)
            mBasicVideo.get_VideoHeight(height)
            PlayControl.Size = New Size(width, height)


            mWindow = DirectCast(mGraphBuilder, IVideoWindow)
            'パネルに表示
            'ウィンドウスタイルを変更
            mWindow.put_WindowStyle(
                    WindowStyle.Child And        ' 子ウィンドウ化
                    WindowStyle.ClipSiblings) ' 兄弟関係の子ウィンドウをクリップ
            mWindow.SetWindowPosition(0, 0, PlayControl.Width, PlayControl.Height)
            mWindow.put_Owner(PlayControl.Handle)


            mMediaPosition = DirectCast(mGraphBuilder, IMediaPosition)
            '1フレームの時間取得
            mBasicVideo.get_AvgTimePerFrame(mFrameTime)
            mMediaPosition.get_Duration(mVideoTime)

            '実行開始
            Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Run()))
            'Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Pause()))
            'Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Run()))
            PlayControl.Select()

            MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlay = True
            'MoveTime()
            If FirstPlay Then
                FirstPlay = False
                MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlayBegin = False
            End If


        Catch ex As Exception
            Console.WriteLine("PlayTest6")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub PlayTest5()
        Try
            If Not mMedia Is Nothing Then
                mMedia.Stop()
            End If

            Dim filepath As String = MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath
            Dim PlayControl As PictureBox =
                MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl


            mFilterGraph = New FilterGraph()
            mGraphBuilder = DirectCast(mFilterGraph, IGraphBuilder)
            mMedia = DirectCast(mFilterGraph, IMediaControl)
            mWindow = DirectCast(mFilterGraph, IVideoWindow)
            mBasicVideo = DirectCast(mFilterGraph, IBasicVideo)

            mVideoFrameStep = DirectCast(mFilterGraph, IVideoFrameStep)
            mMediaPosition = DirectCast(mFilterGraph, IMediaPosition)
            Dim hr As Integer
            hr = mGraphBuilder.RenderFile(filepath, Nothing)
            'mGraphBuilder.AddFilter()

            'hr = mMedia.RenderFile(filepath)

            Dim width, height As Integer
            mBasicVideo.get_VideoWidth(width)
            mBasicVideo.get_VideoHeight(height)
            PlayControl.Size = New Size(width, height)

            'パネルに表示
            'ウィンドウスタイルを変更
            mWindow.put_WindowStyle(
                    WindowStyle.Child And        ' 子ウィンドウ化
                    WindowStyle.ClipSiblings) ' 兄弟関係の子ウィンドウをクリップ
            mWindow.SetWindowPosition(0, 0, PlayControl.Width, PlayControl.Height)
            mWindow.put_Owner(PlayControl.Handle)


            '1フレームの時間取得
            mBasicVideo.get_AvgTimePerFrame(mFrameTime)
            mMediaPosition.get_Duration(mVideoTime)

            '実行開始
            Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Run()))
            'Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Pause()))
            'Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Run()))
            PlayControl.Select()

            MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlay = True
            'MoveTime()
            If FirstPlay Then
                FirstPlay = False
                MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlayBegin = False
            End If
        Catch ex As Exception
            Console.WriteLine("PlayTest5")
            Console.WriteLine(ex.Message)
        End Try

    End Sub

    Public Sub PlayTest4()
        Try
            If Not mMedia Is Nothing Then
                mMedia.Stop()
            End If

            Dim hresult As Integer
            Dim filepath As String
            filepath = MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath

            If Not New FileTypeMovie().isThisClassType(MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath) Then
                MainProcessorObject.gNowViewImageManager.gState.gMovie.resetFlagAll()
                Exit Sub
            End If

            Dim Form1 As Form =
                MainProcessorObject.gMainForm.getForm
            'InitializeComponent

            'グラフビルダーの作成
            mGraph = New FilterGraph()
            'Dim graphbuilder As IGraphBuilder
            'graphbuilder.RenderFile()
            mMedia = mGraph



            'ファイル読み込み
            hresult = mMedia.RenderFile(filepath)
            Console.WriteLine(String.Format("ソースフィルタ コード:{0}", hresult))

            'レンダラーの取得

            'hresult = mGraph.AddFilter("WMVideo Decoder DMO", mRenderer)
            'hresult = mGraph.AddFilter(mRenderer, "WM ASF Reader")
            'hresult = mGraph.AddFilter(mRenderer, "Haali Video Render") 'x

            'hresult = mGraph.AddFilter(mRenderer, "Microsoft DTV-DVD Video Decoder")
            'hresult = mGraph.AddFilter(mRenderer, "Haali Media Splitter")
            If UCase(Right(filepath, 4)) = ".MP4" Then
                mGraphBuilder = mGraph
                hresult = mGraphBuilder.FindFilterByName("Haali Video Renderer", mRenderer)
                'hresult = mGraphBuilder.FindFilterByName("Haali Media Splitter (AR)", mRenderer)
                'hresult = mGraph.FindFilterByName("Haali Video Render", mRenderer)
                'hresult = mGraph.AddFilter(mRenderer, "Microsoft DTV-DVD Video Decoder")
                'hresult = mGraph.AddFilter(mRenderer, "Haali Media Splitter (AR)")
            Else
                hresult = mGraph.FindFilterByName("Video Renderer", mRenderer)
                'hresult = mGraph.AddFilter(mRenderer, "WMVideo Decoder DMO")
            End If

            'hresult = mGraph.FindFilterByName("Haali Media Splitter (AR)", mRenderer)


            mBasicVideo = DirectCast(mGraph, IBasicVideo)
            'mRenderer = DirectCast(mGraph, IBaseFilter)
            'mRenderer = DirectCast(mMedia, IBaseFilter)
            'mRenderer = DirectCast(mBasicVideo, IBaseFilter)
            Console.WriteLine(String.Format("レンダラー コード:{0}", hresult))

            mGraphBuilder = DirectCast(mGraph, IGraphBuilder)

            If Not mRenderer Is Nothing Then
                'レンダラーをウィンドウに登録
                'window = renderer As IVideoWindow
                mWindow = DirectCast(mGraph, IVideoWindow)

                'ウィンドウスタイルを変更
                hresult = mWindow.put_WindowStyle(
                    WindowStyle.Child And        ' 子ウィンドウ化
                    WindowStyle.ClipSiblings) ' 兄弟関係の子ウィンドウをクリップ

                Dim PlayFrame As Panel =
                    MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlFrame.getControl.getControl
                Dim PlayControl As PictureBox =
                MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl


                'Formウィンドウを親ウィンドウに設定
                hresult = mWindow.put_Owner(PlayControl.Handle)
                'hresult = mWindow.get_Owner(PlayControl.Handle) 'x
                'イベント
                Dim whndl As IntPtr
                whndl = mWindow.put_MessageDrain(PlayControl.Handle)
                'mWindow.get_MessageDrain(PlayControl.Handle)
                mEventEx = DirectCast(mGraph, IMediaEventEx)
                mEventEx.GetEventHandle(PlayControl.Handle)
                '18549748
                Console.WriteLine(PlayControl.Handle)
                mEventEx.SetNotifyFlags(NotifyFlags.NoNotify)
                mEventEx.GetEventHandle(PlayControl.Handle)
                'mEventEx.SetNotifyWindow(PlayControl.Handle, mEventEx.GetEventHandle()

                'その他設定
                mWindow.put_Visible(OABool.True)
                mWindow.put_AutoShow(OABool.True)
                mWindow.put_FullScreenMode(OABool.False)
                mWindow.SetWindowForeground(OABool.False)
                'ウィンドウサイズの取得

                Dim NowWindowState As System.Windows.Forms.FormWindowState
                mWindow.get_WindowState(NowWindowState)
                mWindow.put_WindowState(FormWindowState.Maximized)

                'Dim GraphBuilder As IGraphBuilder
                mBasicVideo = DirectCast(mGraph, IBasicVideo)
                Dim width, height As Integer
                mBasicVideo.get_VideoWidth(width)
                mBasicVideo.get_VideoHeight(height)


                'PlayControl.Size = PlayFrame.Size
                '取得したサイズに変更
                'PlayFrame.Width = width
                'PlayFrame.Height = height
                mMedia.StopWhenReady()

                '実行開始
                Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Pause()))
                'Media.Run()
                'PlayControl.Invalidate()
                'PlayControl.Location = PlayControl.Location
                'PlayControl.Invalidate()
                'Form1.Invalidate()
                'Form1.Location = New Point(Form1.Location.X + 1, Form1.Location.Y + 1)
                'Stop

                '再生終了まで待機
                'Dim code As EventCode
                'code = eventEx.WaitForCompletion(-1, code)
                mWindow.SetWindowPosition(0, 0, width, height)

                PlayControl.Size = New Size(width, height)
                'その他設定
                mWindow.put_Visible(OABool.True)
                'mWindow.put_AutoShow(OABool.True)
                mWindow.put_FullScreenMode(OABool.False)
                mWindow.SetWindowForeground(OABool.True)

                'PlayControl.Update()
                'PlayControl.Refresh()

                'PlayFrame.Size = New Size(0, 0)
                'MainProcessorObject.State.MoviePlayBegin = False
                'MainProcessorObject.State.ChangeFileBeginNow = False
                MainProcessorObject.gMainForm.getForm.Invalidate()
                'mRenderer.Stop()
                'MainProcessorObject.PaintMain()

                MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlay = True
                'MoveTime()
                If FirstPlay Then
                    'MainProcessorObject.NextPaintImage()
                    FirstPlay = False
                    MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlayBegin = False
                End If
            End If

        Catch ex As Exception
            Console.WriteLine("PlayTest")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub FormInvaridate()
        MainProcessorObject.gMainForm.getForm.Invalidate()
    End Sub



    'Private mMediaPosition As IMediaPosition
    Private mMovieLength As Double
    Public Sub MoveTime()
        Try
            mMediaPosition = DirectCast(mGraph, IMediaPosition)
            '長さを取得
            mMediaPosition.get_Duration(mMovieLength)
            'mMediaPosition.put_CurrentPosition(mMovieLength / 2)
            'mMedia.Stop()
            'mMedia.Run()
            'Stop
        Catch ex As Exception
            Console.WriteLine("MoveTime")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Public Sub MediaRun()
        '実行開始
        Console.WriteLine(String.Format("実行状態 コード:{0}", mMedia.Run()))
    End Sub


    Public Sub PlayTest3()
        Try
            'Dim TypeObjet As Type
            'IGraphBuilder作成
            'TypeObjet = Type.GetTypeFromCLSID(DefDirectShow.)
            'Dim ObjetCOM As Object = Activator.CreateInstance(TypeObjet)
            'mGraphBuilder = CType(ObjetCOM, IGraphBuilder)


            mGraphBuilder = DirectCast(mMedia, IMediaControl)

        Catch ex As Exception
            Console.WriteLine("PlayTest")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub ChangeLocation()
        Try

        Catch ex As Exception
            Console.WriteLine("ChangeLocation")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub StopMovie()
        Try
            mMedia.Stop()
        Catch ex As Exception
            Console.WriteLine("StopMovie")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub DisposeMovie()
        Try
            mMedia.StopWhenReady()
        Catch ex As Exception
            Console.WriteLine("DisposeMovie")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub ChangeWindowSize(argSize As Size)
        Try
            If Not MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlay Then Exit Sub

            mWindow.SetWindowPosition(0, 0, argSize.Width, argSize.Height)

        Catch ex As Exception
            Console.WriteLine("ChangeWindowSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub PlayTest02()
        Try

            Dim filepath As String
            filepath = MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath

            If Not New FileTypeMovie().isThisClassType(MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath) Then
                MainProcessorObject.gNowViewImageManager.gState.gMovie.resetFlagAll()
                Exit Sub
            End If
            'フィルタグラフCOMオブジェクトの作成

            Dim graph As IFilterGraph = New FilterGraph()
            Dim Media As IMediaControl = graph
            Dim eventEx As IMediaEventEx = Media
            Dim hresult As Integer

            '指定メディアファイルを描画できるフィルタを自動的に構成
            hresult = Media.RenderFile(filepath)
            Console.WriteLine(String.Format("ソースフィルタ コード:{0}", hresult))

            '実行
            hresult = Media.Run()
            Console.WriteLine(String.Format("処理開始 コード:{0}", hresult))

            '再生終了まで待機
            Dim code As EventCode
            code = eventEx.WaitForCompletion(-1, code)

            ''停止
            'Media.Stop()

            ''開放
            'eventEx = Nothing
            'Media = Nothing
            'graph = Nothing

            'エラーは出ないが再生されない
            MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlay = True

        Catch ex As Exception
            Console.WriteLine("PlayTest")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub PlayTest01()
        Try
            Dim hr As Integer
            Dim filepath As String

            Dim fg As FilterGraph
            Dim graphBuilder As IGraphBuilder
            Dim mediaControl As IMediaControl
            Dim videoWindow As IVideoWindow

            filepath = MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath

            If Not New FileTypeMovie().isThisClassType(MainProcessorObject.gNowViewImageManager.gImageFileList.getNowFilePath) Then
                MainProcessorObject.gNowViewImageManager.gState.gMovie.resetFlagAll()
                Exit Sub
            End If

            fg = New FilterGraph
            graphBuilder = DirectCast(fg, IGraphBuilder)
            mediaControl = DirectCast(fg, IMediaControl)
            videoWindow = DirectCast(fg, IVideoWindow)


            hr = graphBuilder.RenderFile(filepath, Nothing)

            InitializeControlSizeForPlay()
            Dim PlayControl As PictureBox =
                MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl
            Dim PlayFrame As Panel = MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlFrame.getControl.getControl
            Dim PlaySize As Size = MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getSize

            'パネルに表示する
            videoWindow.put_WindowStyle(WindowStyle.Child)
            videoWindow.SetWindowPosition(0, 0, PlaySize.Width, PlaySize.Height)
            videoWindow.put_Owner(PlayFrame.Handle)
            '再生
            mediaControl.Run()
            'エラーは出ないが再生されない

            MainProcessorObject.gNowViewImageManager.gState.gMovie.MoviePlay = True
        Catch ex As Exception
            Console.WriteLine("PlayTest")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub InitializeControlSizeForPlay()
        Try
            Dim PlayControl As PictureBox = MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlForPaint.getControlBridge.getControl
            Dim PlayFrame As Panel = MainProcessorObject.gNowViewImageManager.gImageManager.getPaintImage.getControlFrame.getControl.getControl

            If PlayControl.Size.Width = 0 Or PlayControl.Size.Height = 0 Then
                PlayControl.Size = PlayFrame.Size
            End If

        Catch ex As Exception
            Console.WriteLine("InitializeControlSizeForPlay")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
