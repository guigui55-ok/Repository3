Public MustInherit Class AbstractControlBridge

    Sub New()

    End Sub

    Sub New(Control As Control)

    End Sub
    MustOverride Function getControl()
    MustOverride Sub setImage(argImage As Image)
    Sub dispose()

    End Sub
End Class
