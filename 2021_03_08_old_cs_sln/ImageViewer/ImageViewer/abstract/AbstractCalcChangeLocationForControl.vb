Public Class AbstractCalcChangeLocationForControl
    'PictureBoxの位置Locationを親コントロールPanelのサイズによって更新する
    Friend OuterFrameSize As Size
    Friend InnerControlSize As Size
    Friend InnerControlNewLocation As PointF

    'サイズを合わせるための基準 Criteria for matching sizes
    Friend FlagCriteriaHorizontalForMatchSize As Integer = 0
    Friend FlagCriteriaVerticalForMatchSize As Integer = 0

    'サイズを合わせるための基準 Criteria for matching sizes
    Public Sub setFlagCriteriaForMatchSize(argHorizontalFlag As Integer, argVerticalFlag As Integer)
        '0 OuterのTop_0
        '1 Outer_Center
        '2 Outer_Bottom_PanelHeight
        FlagCriteriaHorizontalForMatchSize = argHorizontalFlag
        FlagCriteriaVerticalForMatchSize = argVerticalFlag
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(
                  argOuterFrameSize As Size,
                  argInnerControlSize As Size,
                  argHorizontalFlag As Integer,
                  argVerticalFlag As Integer)
        setSize(argOuterFrameSize, argInnerControlSize, argHorizontalFlag, argVerticalFlag)
    End Sub

    Public Sub setSize(
                      argOuterFrameSize As Size,
                      argInnerControlSize As Size,
                      argHorizontalFlag As Integer,
                      argVerticalFlag As Integer)
        OuterFrameSize = argOuterFrameSize
        InnerControlSize = argInnerControlSize
        setFlagCriteriaForMatchSize(argHorizontalFlag, argVerticalFlag)
    End Sub

    Public Sub calcLocation()
        Try
            InnerControlNewLocation.X = calcLocationMain(
                FlagCriteriaHorizontalForMatchSize, InnerControlSize.Width, OuterFrameSize.Width)
            InnerControlNewLocation.Y = calcLocationMain(
                FlagCriteriaVerticalForMatchSize, InnerControlSize.Height, OuterFrameSize.Height)
        Catch ex As Exception
            Console.WriteLine("CalcPosition")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '計算後Pointでを得る
    Public Function getLocationAsPoint() As Point
        Return New Point(InnerControlNewLocation.X, InnerControlNewLocation.Y)
    End Function

    Public Function getLocationAsPointF() As PointF
        Return New PointF(InnerControlNewLocation.X, InnerControlNewLocation.Y)
    End Function

    Friend Overridable Function calcLocationMain(
            FlagInt As Integer, InnnerSizeLength As Integer, OuterSizeLength As Integer) As Double
        Try
            Dim returnLocation As Double = 0
            '大きい
            If FlagInt = 0 Then
                'Top
                returnLocation = 0
            ElseIf FlagInt = 1 Then
                'center
                returnLocation = (OuterSizeLength - InnnerSizeLength) / 2
            ElseIf FlagInt = 2 Then
                'bottom
                returnLocation = (OuterSizeLength - InnnerSizeLength)
            ElseIf FlagInt = 3 Then
                'SizeChange
                Console.WriteLine("calcLocationMain flag error")
            Else
                Console.WriteLine("calcLocationMain flag error")
            End If
            Return returnLocation
        Catch ex As Exception
            Console.WriteLine("calcLocationMain")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

End Class
