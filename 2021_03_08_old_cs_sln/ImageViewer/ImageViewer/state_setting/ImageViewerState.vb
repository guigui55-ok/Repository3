Public Class ImageViewerState

    Public PaintFormInitialize As Boolean = True
    Public LoadLogWindow As Boolean = True

    Public gMouse As MouseState = New MouseState()

    Public gDebugFlag As Integer = 0
    Public ListRandom As Boolean = False
    '-----------------------------------------------------
    'Public ImageNothing As Boolean = True
    Public Initialize As Boolean = True

    Public KeyDownState As KeyEventArgs
    Public MouseDownState As MouseEventArgs
    Public KeyState As KeyState = New KeyState()
    '-----------------------------------------------------
    Public PaintNow As Boolean = False

    Public NowWindowSize As Rectangle
    Public WindowPosition As Rectangle
    'Public PaintCount As Integer = 0

    Public gNowForcusControl As String = "MainForm"

    'Public ImagecurrentAlphaPercent As Integer = 100

    Public Sub New()

    End Sub

    Public Sub disposeObjects()
        Try
            KeyDownState = Nothing
            MouseDownState = Nothing
            KeyState = Nothing
            gMouse = Nothing
        Catch ex As Exception
            Console.WriteLine(Me.ToString & "disposeObjects")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '-----------------------------------------------------
    'Public PictureBoxScrollBarVisible = True
    'Public SlideShowExecute As Boolean = False
    'Public ChangeFileBeginNow As Boolean = False
    'Public Fade As FadeState = New FadeState()
    'Public Gif As GifState = New GifState
    'Public MoviePlayBegin As Boolean = False
    'Public Movie As MovieState = New MovieState()
    'Public GifAnime As Boolean = False
    'Public GifAnimeBegin As Boolean = False
    'Public CountOfPaintAllFile As Integer
    'Public CantPaintAllFile As Boolean = False

    'Public gNowFolderPath As String = ""
    'Public ControlRaito As Double = 1.0
    'Public PaintVerticalAlign = 0 '0center 1top 2bottom
    'Public paintHorizontalAlign = 0 '0center 1left 2right
    'Public ImageRaito As Double = 1
    '-----------------------------------------------------


    '====================================================================================
    'Public Sub SlideShowPaintEnd()
    '    Fade.FadeInEnd = True
    'End Sub

    ''ファイルリストセット時に対応ファイルが存在しない場合の状態を判定、保持
    'Public Sub SaveStatusToCantPaintAllFile(argCount As Integer)
    '    Try
    '        If argCount < 0 Then
    '            CantPaintAllFile = True
    '        Else
    '            CantPaintAllFile = False
    '        End If
    '        CountOfPaintAllFile = argCount
    '    Catch ex As Exception
    '        Console.WriteLine("SaveStatusToCantPaintAllFile")
    '        Console.WriteLine(ex.Message)
    '        CantPaintAllFile = True
    '    End Try
    'End Sub
End Class
