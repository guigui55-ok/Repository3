

Public Class Settings
    Public ReadFileTypeList() As String = New String() {".jpg", "jpeg", ".bmp", ".png", ".gif"}
    Public MovieFileTypeList() As String = New String() {".mp4", ".mkv", ".avi", ".wmv"}
    'サブフォルダ-以下を読み込む
    '0:読み込まない、１以上：読み込む(数値は階層)
    Public ReadSubFolder As Integer = 0

    Public SlideShowInterval As Integer = 4000
    Public SlideShowIntarvalInitialize As Integer = 5000

    Public ImageFadeIn As Boolean = True
    Public ImageFadeOut As Boolean = True
    Public FadeInOut As Boolean = True

    Public MenuStripAlwaysVisible As Boolean = False 'メニュー
    Public ToolStripVisible As Boolean = True
    Public StatusStripVisible As Boolean = True

    'ログ出力ウィンドウを表示する
    Public LogWindowForDebugVisible As Boolean = True
    'イメージ拡大時スクロールバーを表示
    Public ScrollBarVisible As Boolean = False
    '画像をWindowサイズに合わせる
    Public PictureBoxFitWindow As Boolean = False

    Public PlayUntilToLastWhenMovie As Boolean = True
    '--------------------------
    '拡大_マウスホイール
    Public ExpansionByMouseWheel As Double = 1.05
    '縮小_マウスホイール
    Public ReductionImageByMouseWheel As Double = 0.9523
    '--------------------------

    Public LogFormSize As Size = New Point(600, 400)
    Public Fade As FadeSetting
    Public SettingsFilePath As String
    Public DefaultSettings As DefaultSettings
    Sub New()
        Fade = New FadeSetting()
        DefaultSettings = New DefaultSettings()
    End Sub

    Sub disposeObjects()
        Try
            Fade = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
