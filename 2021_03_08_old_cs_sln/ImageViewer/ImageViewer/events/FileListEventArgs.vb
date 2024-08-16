Public Class FileListEventArgs
    Inherits EventArgs
    Private NowFileName As String

    Public Property FileName()
        Set(value)
            NowFileName = value
        End Set
        Get
            Return NowFileName
        End Get
    End Property
End Class
