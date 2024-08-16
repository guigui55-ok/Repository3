Public Class InitializeEvents
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



    Public Sub excuteEndMethod()        '数値があればそのまま
        addLog(3, Me.ToString & ".excuteEndMethod")
        '設定ファイルの値を適用
        'MainProcessorObject.gNowViewImageManager.gEvents.gSettings.setListByLastFolder()

        'Size_Location
        MainProcessorObject.gMainForm.gControls.initializeSizeAndLocation()
        'ToolStrip
        MainProcessorObject.gMainForm.gControls.gToolStripEvents.InitializeVisible()

        ' TextBox1 をアクティブなコントロールに設定する
        MainProcessorObject.gMainForm.getForm.ActiveControl = MainProcessorObject.gMainForm.getForm.PictureBox1

        MainProcessorObject.gMainForm.getForm.ActiveControl = Nothing
        'MainForm KeyPreview
        MainProcessorObject.gMainForm.getForm.KeyPreview = True


        '主要クラスのInitializeが終わったらViewImageのほうをInitialize、MainProcessorの値を反映させるものもあるため
        MainProcessorObject.gNowViewImageManager.gEvents.gInitialize.excute()

        If MainProcessorObject.gNowViewImageManager.gState.gSlideShowExecute Then
            MainProcessorObject.gNowViewImageManager.gState.gFade.currentAlphaPercent = 0
            MainProcessorObject.gNowViewImageManager.gState.gFade.FadeInBegin = True
            MainProcessorObject.gNowViewImageManager.gEvents.gViewer.excuteSlideShow()
        Else
            MainProcessorObject.gNowViewImageManager.gState.gFade.currentAlphaPercent = 100
        End If



        '描画へ
        MainProcessorObject.gNowViewImageManager.gState.gFade.FadeInBegin = True


        'Initializeを終えるまでTrueにする
        MainProcessorObject.gState.PaintFormInitialize = False
    End Sub

    Public Sub excute()
        Try
            '設定ファイル名　実行場所直下
            MainProcessorObject.gSettings.SettingsFilePath = MainProcessorObject.gFunction.gFileIo.getCurrentDirectory &
                "\" & "ImageViewerSettings.ini"
            'ログのファイル名
            MainProcessorObject.gLog.setFilePath(New CommonFile().getCurrentDirectory)
            MainProcessorObject.gLog.setFileName("ImageViewer.log")
            addLog(3, "set LogFilePath = " & MainProcessorObject.gLog.getLogFileFullpath)

            'ログを初期化
            'Sub initialize(mode As Integer, saveloglevel As Integer, windowVisible As Boolean)
            MainProcessorObject.gLog.initialize(1, 4, 1)
            addLog(3, "ImageViewerInitialize.excute : showLogWindow")


            '設定ファイルを読み込み
            MainProcessorObject.gEvents.gSettingsFile.Initialize()
            '設定ファイルを適用
            MainProcessorObject.gEvents.gSettingsFile.applySettings()
            '設定ファイルの値をViewImageManagerへ
            MainProcessorObject.gEvents.gSettingsFile.copyValueToViewImageObject()
            '設定ファイルをデバッグウィンドウへ
            'MainProcessorObject.gFunction.gSettingsFile.outputValueDefaultToCOnsole()
            'MainProcessorObject.gFunction.gSettingsFile.outputValueMainToCOnsole()
            '設定ファイルを適用-ViewImageManager

            'コマンドラインを読み込み
            'readCommandLine()
            MainProcessorObject.gEvents.gCommandLine.setVriousValueByReadCommandLine()

            '読み込み可能ファイルの設定
            MainProcessorObject.gNowViewImageManager.gImageFileList.setReadAbleFileTypeList(
            New List(Of String)(MainProcessorObject.gSettings.ReadFileTypeList))
            MainProcessorObject.gNowViewImageManager.gImageFileList.addReadAbleFileTypeList(
            New List(Of String)(MainProcessorObject.gSettings.MovieFileTypeList))




            'Control
            'PictureBox
            excuteForPictureBox(
                MainProcessorObject.gMainForm.getForm.PictureBox1)
            'MainForm
            MainProcessorObject.gMainForm.gInitializeEvents.excute()
            'Control_ViewImage
            MainProcessorObject.gViewImageManager.InitializeToViewImageControls()
            InitializeImageViewerControls(MainProcessorObject.gNowViewImageManager)
            'ToolStrip
            MainProcessorObject.gMainForm.gControls.gToolStripEvents.Initialize(MainProcessorObject.gMainForm.getForm)


        Catch ex As Exception
            Console.WriteLine("ImageViewerInitialize excute")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub InitializeImageViewerControls(argViewImageManager As ViewImageManager)
        addLog(3, Me.ToString & ".InitializeImageViewerControls")
        Try
            argViewImageManager.gImageManager.setControls(
                MainProcessorObject.gMainForm.getForm.Panel1, MainProcessorObject.gMainForm.getForm.PictureBox1)
            'ControlInitialize
            argViewImageManager.gFunction.gPictureBox.setPictureBox(MainProcessorObject.gMainForm.getForm.PictureBox1)
            argViewImageManager.gFunction.gPanel.setPanel(MainProcessorObject.gMainForm.getForm.Panel1)

            'argViewImageManager.gFunction.gPictureBox.getPictureBox.
            'argViewImageManager.gEvents.gPanel.getPanel.IsKeyLocked()

            'argViewImageManager.gFunction.gPictureBox.AlignLocationForPictureBox()
            argViewImageManager.gFunction.gPictureBox.ChangeSizePictureBox(1)

            'argViewImageManager.gImageManager.setControls(
            '    MainProcessorObject.gMainForm.getForm.Panel1,
            '    MainProcessorObject.gMainForm.getForm.PictureBox1)

            'argViewImageManager.gEvents.gPictureBox.setPictureBox(MainProcessorObject.gMainForm.getForm.PictureBox1)
            'argViewImageManager.gEvents.gPanel.setPanel(MainProcessorObject.gMainForm.getForm.Panel1)

        Catch ex As Exception
            Console.WriteLine("InitializeImageViewerControls")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Public Sub excuteForPictureBox(argPictureBox As PictureBox)
        Try
            argPictureBox.Size = New Size(0, 0)
            argPictureBox.AllowDrop = True
            'argPictureBox.Location = New Point(50, 50)
        Catch ex As Exception
            Console.WriteLine("excuteForPictureBox")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function getCommandlineAll() As String
        Try
            '1つの文字列に
            Dim buf As String = ""
            For Each value In System.Environment.GetCommandLineArgs()
                buf = buf & " " & value
            Next
            Return buf
        Catch ex As Exception
            Console.WriteLine("getCommandlineAll")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    Private Sub InitializeWindowSizeAndLocation()
        Try
            Dim WinRect As Rectangle = MainProcessorObject.gState.NowWindowSize
            If WinRect.Width <= 0 Then

            End If
        Catch ex As Exception
            Console.WriteLine("excuteForPictureBox")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
