
Imports System.Drawing
Imports System.Xaml
Imports System.IO
Imports System.Drawing.Imaging

Public Class GifState
    Public GifAnime As Boolean = False
    Private GifPlayNow As Boolean = False
    Private GifStopNow As Boolean = False
    Private GifPauseNow As Boolean = False
    Private GifPlayBeginNow As Boolean = False

    Private GifNowFrame As Integer

    Public Sub FrameCountUp(Optional value As Integer = -1)
        GifNowFrame += 1
        If value >= 0 Then
            GifNowFrame = value
        End If
    End Sub

    Public Function getCountFrame(argImage As Image) As Integer
        Try
            If argImage Is Nothing Then
                Return -2
            Else
                Return argImage.GetFrameCount(New FrameDimension(argImage.FrameDimensionsList(0)))
            End If
        Catch ex As Exception
            Console.WriteLine("argImage")
            Console.WriteLine(ex.Message)
            Return -1
        End Try
    End Function

    Public Sub FrameCountUp(argImage As Image)
        GifNowFrame += 1
        Dim maxcount As Integer = Me.getCountFrame(argImage)
        If maxcount < GifNowFrame Then
            GifNowFrame = 0
        End If
    End Sub

    Public Function NowFrameCount() As Integer
        Return GifNowFrame
    End Function

    Public Property GifPlay As Boolean
        Set(value As Boolean)
            resetAll()
            GifPlayNow = value
        End Set
        Get
            Return GifPlayNow
        End Get
    End Property

    Public Property GifStop As Boolean
        Set(value As Boolean)
            resetAll()
            GifStopNow = value
            GifNowFrame = 0
        End Set
        Get
            Return GifStopNow
        End Get
    End Property

    Public Property GifPause As Boolean
        Set(value As Boolean)
            resetAll()
            GifPauseNow = value
        End Set
        Get
            Return GifPauseNow
        End Get
    End Property

    Public Property GifPlayBegin As Boolean
        Set(value As Boolean)
            'resetAll()
            GifPlayBeginNow = value
        End Set
        Get
            Return GifPlayBeginNow
        End Get
    End Property

    Public Sub resetFlagAll()
        resetAll()
    End Sub


    Private Sub resetAll()
        GifPlayNow = False
        GifStopNow = False
        GifPauseNow = False
        GifPlayBeginNow = False
    End Sub

    Public Function FlagIsFalseAll() As Boolean
        If GifPlayNow And GifStopNow And GifPauseNow And GifPlayBeginNow Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function FlagIsGifMode() As Boolean
        If GifPlayNow Or GifStopNow Or GifPauseNow Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
