Imports ImageViewer

Public Class CalcDrawRegionFactory
    Implements ICalcDrawRagionFactory

    Private VerticalAlign As Integer
    Private HorizontalAlign As Integer
    Private ControlSize As Size
    Private ImageSize As Size

    Public Sub setRegionSetting(VerticalAlignFlag As Integer, HorizontalAlignFlag As Integer) Implements ICalcDrawRagionFactory.setRegionSetting
        VerticalAlign = VerticalAlignFlag
        HorizontalAlign = HorizontalAlignFlag
    End Sub

    Public Sub setSize(argControlSize As Size, argImageSize As Size)
        ControlSize = argControlSize
        ImageSize = argImageSize
    End Sub

    Public Function createCalcDrawRegion() As AbstractCalcDrawRegion Implements ICalcDrawRagionFactory.createCalcDrawRegion

        If VerticalAlign = 0 And HorizontalAlign = 0 Then
            Return New CalcDrawRegionCenter(ControlSize, ImageSize)
        Else
            Return New CalcDrawRegion(ControlSize, ImageSize)
        End If
    End Function
End Class
