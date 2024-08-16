Public Class PlayFilesFactory
    Private PlayFileNow As IPlayFile
    Sub New()

    End Sub

    Public Function GetPlayFileObjectBySetPath(argPath As String) As IPlayFile

        If Right(UCase(argPath), 4).Equals(".GIF") Then
            'PlayFileNow = New PlayGif(argPath)
            Return PlayFileNow
        Else
            Return PlayFileNow
        End If
    End Function

End Class
