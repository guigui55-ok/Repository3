Imports ImageViewer

'DirectShowで再生
'DirectShowを使うには参照設定で ActiveMovie control type library を追加

Public Class FileTypeMovie
    Implements IPlayFile

    Public PlayAbleList() As String = New String() {".mp4", ".mkv", ".avi", ".wmv"}

    Private gifImage As Image
    Private Canvas As Image
    Private MaxFrameCount As Integer
    Private NowFrameNum As Integer
    Private ControlSizeForPlay As Size
    Private DrawControl As AbstractControlBridge


    Public Sub Dispose() Implements IPlayFile.Dispose
        Throw New NotImplementedException()
    End Sub

    Public Sub setImage(argImage As Image) Implements IPlayFile.setImage
        Throw New NotImplementedException()
    End Sub

    Public Sub setImageFormPath(argPath As String) Implements IPlayFile.setImageFormPath
        Throw New NotImplementedException()
    End Sub

    Public Sub setControlBridge(argControlBridge As AbstractControlBridge) Implements IPlayFile.setControlBridge
        Throw New NotImplementedException()
    End Sub

    Public Sub setDrawSize(argSize As Size) Implements IPlayFile.setDrawSize
        Throw New NotImplementedException()
    End Sub

    Public Sub PlayFile() Implements IPlayFile.PlayFile
        Throw New NotImplementedException()
    End Sub

    Public Sub PlayFileEvent(sender As Object, e As EventArgs) Implements IPlayFile.PlayFileEvent
        Throw New NotImplementedException()
    End Sub

    Public Sub ReplayFile(argControlSizeForPlay As Size) Implements IPlayFile.ReplayFile
        Throw New NotImplementedException()
    End Sub

    Public Sub ReplayFile(argControlSizeForPlay As Size, PlayFrameNumber As Integer) Implements IPlayFile.ReplayFile
        Throw New NotImplementedException()
    End Sub

    Public Sub StopFile() Implements IPlayFile.StopFile
        Throw New NotImplementedException()
    End Sub

    Public Sub PauseFile() Implements IPlayFile.PauseFile
        Throw New NotImplementedException()
    End Sub

    Public Sub PauseFile(argControlSizeForPlay As Size) Implements IPlayFile.PauseFile
        Throw New NotImplementedException()
    End Sub

    Public Sub ForwardFile() Implements IPlayFile.ForwardFile
        Throw New NotImplementedException()
    End Sub

    Public Sub RewindFile() Implements IPlayFile.RewindFile
        Throw New NotImplementedException()
    End Sub

    Public Sub setTimer(argTimer As Timer) Implements IPlayFile.setTimer
        Throw New NotImplementedException()
    End Sub

    Public Sub TimerStart() Implements IPlayFile.TimerStart
        Throw New NotImplementedException()
    End Sub

    Public Sub timerStop() Implements IPlayFile.timerStop
        Throw New NotImplementedException()
    End Sub

    Public Function getCountFrame(argImage As Image) As Integer Implements IPlayFile.getCountFrame
        Throw New NotImplementedException()
    End Function

    Private Function isThisClassTypeSingle(argPath As String, argType As String) As Boolean
        Try
            If Right(UCase(argPath), 4).Equals(UCase(argType)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("isThisClassTypeSingle")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function isThisClassType(argPath As String) As Boolean Implements IPlayFile.isThisClassType
        Dim flag As Boolean = False
        Try
            For i = 0 To Me.PlayAbleList.Count - 1
                If isThisClassTypeSingle(argPath, PlayAbleList(i)) Then
                    Return True
                End If
            Next
            Return flag
        Catch ex As Exception
            Console.WriteLine("Movie isThisClassType")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function isMovie(argPath As String) As Boolean Implements IPlayFile.isMovie
        Throw New NotImplementedException()
    End Function

    Public Function isEndFrameNow() As Boolean Implements IPlayFile.isEndFrameNow
        Throw New NotImplementedException()
    End Function

    Public Function getFrameNow() As Integer Implements IPlayFile.getFrameNow
        Throw New NotImplementedException()
    End Function

    Public Function getDrawImage() As Image Implements IPlayFile.getDrawImage
        Throw New NotImplementedException()
    End Function

    Public Sub ChangeWindowSize() Implements IPlayFile.ChangeWindowSize
        Throw New NotImplementedException()
    End Sub
End Class
