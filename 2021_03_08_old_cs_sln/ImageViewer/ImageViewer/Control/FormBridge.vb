Public Class FormBridge

    Private mMainForm As MainForm
    Public Sub New()

    End Sub

    Public Sub New(mainform As MainForm)
        mMainForm = mainform
    End Sub

    Public Function getForm() As Object
        Return mMainForm
    End Function
End Class
