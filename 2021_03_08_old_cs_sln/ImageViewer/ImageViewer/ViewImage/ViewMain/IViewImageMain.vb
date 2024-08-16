Public Interface IViewImageMain

    'Sub New()
    Sub excuteViewImage()

    Sub Dispose()
    Sub setImage(argImage As Image)
    Sub setImageFormPath(argPath As String)
    Sub setControlBridge(argControlBridge As AbstractControlBridge)
    Sub setDrawSize(argSize As Size)

    Function getCountFrame(argImage As Image) As Integer
    Function isThisClassType(argPath As String) As Boolean
    Function isMovie(argPath As String) As Boolean
    Sub PlayFile()

    Sub PlayFileEvent(sender As Object, e As EventArgs)

    Sub ReplayFile(argControlSizeForPlay As Size)
    Sub ReplayFile(argControlSizeForPlay As Size, PlayFrameNumber As Integer)

    Sub StopFile()

    Sub PauseFile()
    Sub PauseFile(argControlSizeForPlay As Size)
    Sub ForwardFile()
    Sub RewindFile()

    Function isEndFrameNow() As Boolean
    Function getFrameNow() As Integer
    Function getDrawImage() As Image

    Sub ChangeWindowSize()

    Sub setTimer(argTimer As System.Windows.Forms.Timer)
    Sub TimerStart()
    Sub timerStop()
End Interface
