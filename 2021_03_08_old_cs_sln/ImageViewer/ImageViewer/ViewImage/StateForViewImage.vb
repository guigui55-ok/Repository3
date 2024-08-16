Public Class StateForViewImage
    Public gFormInitialize As Boolean = True
    Public gListRandom As Boolean = False
    Public gNowFolderPath As String

    Public gFade As FadeState = New FadeState()
    Public gFadeSettings As New FadeSetting()

    Public gChangeFileBegin As Boolean = False

    Public gNowForcusControl As String = "MainForm"

    Public gViewModeChangeNow As Boolean = True 'Fade->Dfaultなど

    'SlideShow
    Public gSlideShowExecute As Boolean = False
    Public gSlideShowInterval As Integer = 4000


    'ファイルリストセット時に対応ファイルが存在しない場合の状態を判定、保持
    Public CountOfPaintAllFile As Integer
    Public CantPaintAllFile As Boolean = False

    Public gMovie As MovieState = New MovieState()

    '動画等は後で一考
    'Public Gif As GifState = New GifState
    'Public MoviePlayBegin As Boolean = False
    'Public GifAnimeBegin As Boolean = False

    'Mouse
    Public gMouse As MouseState = New MouseState()

    Public Sub New()

    End Sub
    Public Sub disposeObjects()
        Try
            gMouse = Nothing
            gMovie = Nothing
            gFadeSettings = Nothing
            gFade = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'ファイルリストセット時に対応ファイルが存在しない場合の状態を判定、保持
    Public Sub SaveStatusToCantPaintAllFile(argCount As Integer)
        Try
            If argCount < 0 Then
                CantPaintAllFile = True
            Else
                CantPaintAllFile = False
            End If
            CountOfPaintAllFile = argCount
        Catch ex As Exception
            Console.WriteLine("SaveStatusToCantPaintAllFile")
            Console.WriteLine(ex.Message)
            CantPaintAllFile = True
        End Try
    End Sub


End Class

