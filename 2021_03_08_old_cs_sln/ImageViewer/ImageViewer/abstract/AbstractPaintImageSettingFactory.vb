Imports ImageViewer

Public Class AbstractPaintImageSettingFactory
    Implements ISettingObjectFactory
    Friend PaintImageSettingObject As PaintImageSetting

    Public Sub setSettingObject(argSettingObject As IPaintImageSetting)
        PaintImageSettingObject = argSettingObject
    End Sub

    Public Sub DoSetting() Implements ISettingObjectFactory.DoSetting
        Throw New NotImplementedException()
    End Sub

    Public Function getSettingObject() As Object
        Return PaintImageSettingObject
    End Function
End Class
