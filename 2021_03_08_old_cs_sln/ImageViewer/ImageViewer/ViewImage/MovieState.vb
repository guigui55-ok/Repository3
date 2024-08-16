Public Class MovieState

    'moviestate/ play start pause end stop few rev speed
    Private mMoviePlay As Boolean = False
    Private mMovieStop As Boolean = False
    Private mMoviePause As Boolean = False
    Private mMoviePlayBegin As Boolean = False

    Private mMovieStart As Boolean = False
    Private mMovieEnd As Boolean = False
    Private mMovieForward As Boolean = False
    Private mMovieRewind As Boolean = False

    Private mNowFrame As Integer

    Public Sub FrameCountUp(Optional value As Integer = -1)
        mNowFrame += 1
        If value >= 0 Then
            mNowFrame = value
        End If
    End Sub

    Public Sub FrameCountUp(argImage As Image)
        mNowFrame += 1
        Dim maxcount As Integer = New PlayGif().getCountFrame(argImage)
        If maxcount < mNowFrame Then
            mNowFrame = 0
        End If
    End Sub

    Public Function NowFrameCount() As Integer
        Return mNowFrame
    End Function

    Public Property MoviePlay As Boolean
        Set(value As Boolean)
            resetAll()
            mMoviePlay = value
        End Set
        Get
            Return mMoviePlay
        End Get
    End Property

    Public Property MovieStop As Boolean
        Set(value As Boolean)
            resetAll()
            mMovieStop = value
            mNowFrame = 0
        End Set
        Get
            Return mMovieStop
        End Get
    End Property

    Public Property MoviePause As Boolean
        Set(value As Boolean)
            resetAll()
            mMoviePause = value
        End Set
        Get
            Return mMoviePause
        End Get
    End Property

    Public Property MoviePlayBegin As Boolean
        Set(value As Boolean)
            'resetAll()
            mMoviePlayBegin = value
        End Set
        Get
            Return mMoviePlayBegin
        End Get
    End Property

    Public Property MovieEnd As Boolean
        Set(value As Boolean)
            resetAll()
            mMovieEnd = value
        End Set
        Get
            Return mMovieEnd
        End Get
    End Property

    Public Property MovieForward As Boolean
        Set(value As Boolean)
            resetAll()
            mMovieForward = value
        End Set
        Get
            Return mMovieForward
        End Get
    End Property

    Public Property MovieRewind As Boolean
        Set(value As Boolean)
            resetAll()
            mMovieRewind = value
        End Set
        Get
            Return mMovieRewind
        End Get
    End Property
    Public Sub resetFlagAll()
        resetAll()
    End Sub


    Public Sub resetAll()
        mMoviePlay = False
        mMovieStop = False
        mMoviePause = False
        mMoviePlayBegin = False
        mMovieEnd = False
        mMovieForward = False
        mMovieRewind = False
        addLog(9, "MovieState.resetAll")
    End Sub

    Public Function FlagIsFalseAll() As Boolean
        If mMoviePlay And mMovieStop And mMoviePause And mMoviePlayBegin _
            And mMovieEnd And mMovieForward And mMovieRewind Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function FlagIsMovieMode() As Boolean
        If mMoviePlay Or mMovieStop Or mMoviePause Or mMovieEnd Or mMovieForward Or mMovieRewind Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
