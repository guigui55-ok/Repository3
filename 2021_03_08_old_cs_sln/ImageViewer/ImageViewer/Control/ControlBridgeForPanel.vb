Public Class ControlBridgeForPanel
    Inherits AbstractControlBridge

    Private panel As Panel

    Public Sub New(argPanel As Panel)
        panel = argPanel
    End Sub

    Public Overrides Function getControl() As Object
        Return panel
    End Function

    Public Overrides Sub setImage(argImage As Image)
        panel.BackgroundImage = argImage
    End Sub
End Class
