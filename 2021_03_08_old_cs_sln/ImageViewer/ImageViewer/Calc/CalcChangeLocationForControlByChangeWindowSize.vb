Public Class CalcChangeLocationForControlByChangeWindowSize
    Inherits AbstractCalcChangeLocationForControl
    'Friend OuterFrameSize As Size
    'Friend InnerControlSize As Size
    'Friend InnerControlNewLocation As PointF

    'MaxMinSizeを求める
    'Public Sub CalcPositionMoveAble()
    '計算後Pointでを得る
    'Public Function getPosition() As Point

    Public Sub New(argOuterFrameSize As Size, argInnerControlSize As Size)
        setSize(argOuterFrameSize, argInnerControlSize)
    End Sub

    Public Overloads Sub setSize(argOuterFrameSize As Size, argInnerControlSize As Size)
        InnerControlSize = argInnerControlSize
        OuterFrameSize = argOuterFrameSize
    End Sub

    Friend Overrides Function calcLocationMain(
            FlagInt As Integer, InnerSizeLength As Integer, OuterSizeLength As Integer) As Double
        Try
            'Width Height別に算出
            Dim returnLocation As Double = 0
            Dim Difference As Integer = OuterSizeLength - InnerSizeLength

            If Difference >= 0 Then
                'Panel> PictureBox中央に
                'center
                returnLocation = (OuterSizeLength - InnerSizeLength) / 2
            Else
                'PictureBox > Panel

            End If
            Return returnLocation
        Catch ex As Exception
            Console.WriteLine("calcLocationMain")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

End Class
