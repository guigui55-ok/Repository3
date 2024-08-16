Public Class MainFormInitializeEvents
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


    Public Sub excute()
        Try
            addLog(3, "MainFormInitialize.excute")

            Dim mForm As MainForm
            mForm = MainProcessorObject.gMainForm.getForm

            'KeyPreview
            mForm.KeyPreview = True

            'メインウィンドウのサイズ位置設定
            Dim MainFormSize = New Size(MainProcessorObject.gState.NowWindowSize.Size)
            Dim MainFormLocation = New Point(MainProcessorObject.gState.NowWindowSize.Location)

            If MainFormSize.Equals(New Size(0, 0)) Then
                MainFormSize = New Size(460, 700)
            End If
            If MainFormLocation = New Point(0, 0) Then
                MainFormLocation = New Point(100, 100)
            End If

            mForm.Location = MainFormLocation
            mForm.Size = MainFormSize
            'mForm.Text = "ImageViewer" 'タイトルバー
            addLog(3, "MainFormInitialize.showWindow : mForm.FormBorderStyle")
            mForm.FormBorderStyle = FormBorderStyle.Sizable '枠のスタイル
            'mForm.FormBorderStyle = FormBorderStyle.SizableToolWindow '枠のスタイル

            mForm.KeyPreview = True

            'メニューバー
            'MainProcessorObject.MainFormManagerObject.ControlEventsObject.MenuStripEventsObject.Initialize()

            '下へほかのコントロール
            InitializeEvents()

        Catch ex As Exception
            Console.WriteLine("MainFormInitialize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Public Sub InitializeEvents()
        Try

            'ImageAnimator.Animate(
            '    MainProcessorObject.ImageManagerObject.getImageFromControl,
            '    New EventHandler(AddressOf MainProcessorObject.MovieEventsObject.Image_FrameChanged))
            'ImageAnimator.StopAnimate(
            '    MainProcessorObject.ImageManagerObject.getImageFromControl,
            '    New EventHandler(AddressOf MainProcessorObject.MovieEventsObject.Image_FrameStoped))

            Dim MainForm As MainForm = MainProcessorObject.gMainForm.getForm

            'Panel_Size
            MainForm.Panel1.Size = New Size(MainForm.Size.Width, MainForm.Size.Height - MainForm.StatusStrip1.Height)
            MainForm.Panel1.AllowDrop = True

            'MenuStripの表示を設定に追従
            MainProcessorObject.gMainForm.gControls.gMenuStripResister.InitializeItem()
            'MenuStripの表示非表示
            'MainProcessorObject.gMainFormManager.gControlEvents.MenuStripEventsObject.InitializeVisible()
            MainProcessorObject.gMainForm.gControls.gMenuStripEvents.InitializeVisible()
            'MenuStripイベント登録
            MainProcessorObject.gMainForm.gControls.gMenuStripEvents.resistEvents()

            'MenuStrip1.Visible = True
            'MenuStrip1.Location = New Point(0, 0)
            'ステータスバー StatusStrip
            MainForm.MenuStrip1.Parent = MainForm
            MainForm.StatusStrip1.Visible = True
            MainForm.StatusStrip1.Items(0).Text = "Initialize"

            'ChangeFormWindowSize()

            'フォーカスをPictureBoxに
            MainForm.PictureBox1.Select()
        Catch ex As Exception
            Console.WriteLine("InitializeEvents")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub ChangeFormWindowSize()
        Try
            'LogForm移動するか？
        Catch ex As Exception
            Console.WriteLine("ChangeFormWindowSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
