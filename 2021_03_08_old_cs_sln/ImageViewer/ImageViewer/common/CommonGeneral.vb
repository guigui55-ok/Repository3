Public Class CommonGeneral
    Sub New()

    End Sub


    Public Sub changeFlag(ByRef ByRefFlag As Boolean)
        If ByRefFlag Then
            ByRefFlag = False
        Else
            ByRefFlag = True
        End If
    End Sub

    Public Function getAfterFlagWhenChangeFlag(ByRef ByRefFlag As Boolean) As Boolean
        If ByRefFlag Then
            ByRefFlag = False
        Else
            ByRefFlag = True
        End If
        Return ByRefFlag
    End Function
End Class
