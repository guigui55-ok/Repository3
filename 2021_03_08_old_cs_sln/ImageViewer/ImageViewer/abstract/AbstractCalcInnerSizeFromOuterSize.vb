Public Class AbstractCalcInnerSizeFromOuterSize
    'Imageのサイズにピクチャーボックスを合わせる、比率そのまま用

    Friend OuterSize As Size
    Friend InnerSize As Size
    'サイズを合わせるための基準 Criteria for matching sizes
    Friend FlagCriteriaForMatchSize As Integer = 0
    Friend NewSize As Size

    Public Sub New()

    End Sub

    Public Sub New(argOuterSize As Size, argInnerSize As Size, argFlagCriteriaForMatchSize As Integer)
        setSize(argOuterSize, argInnerSize, argFlagCriteriaForMatchSize)
    End Sub

    Public Sub setSize(argOuterSize As Size, argInnerSize As Size, argFlagCriteriaForMatchSize As Integer)
        OuterSize = argOuterSize
        InnerSize = argInnerSize
        FlagCriteriaForMatchSize = argFlagCriteriaForMatchSize
    End Sub

    'サイズを合わせるための基準 Criteria for matching sizes
    Public Sub setFlagCriteriaForMatchSize(argFlag As Integer)
        '0 Image OuterFit
        '1 Inner_Horizontal 幅はInnerで高さにOuterの比率をかける
        '2 Inner_Vertical　高さはInnerで幅にOuterの比率をかける

        FlagCriteriaForMatchSize = argFlag
    End Sub

    Public Overridable Sub calcSize()
        Try
            InnerSizeChangeToMatchOuterSize()
        Catch ex As Exception
            Console.WriteLine("calcSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub CalcSizeInnerSizeChangeToMatchOuterSize()
        Try
            InnerSizeChangeToMatchOuterSize()
        Catch ex As Exception
            Console.WriteLine("CalcSizeInnerSizeChangeToMatchOuterSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '計算後Pointでを得る
    Public Function getNewSizeAtPoint() As Point
        Return New Point(NewSize.Width, NewSize.Height)
    End Function

    '計算後Pointでを得る
    Public Function getNewSize() As Size
        Return NewSize
    End Function

    'イメージサイズをコントロールサイズに合わせる、イメージの比率のまま
    'InnerサイズをOuterサイズに合わせる、イメージの比率のまま
    Public Sub InnerSizeChangeToMatchOuterSize()
        Try
            'Dim NewWidth As Double
            'Dim NewHeight As Double
            'Dim Raito As Double
            '縦横比  Aspect ratio
            '横を基準１
            Dim ImageAspectHorizontalRaito As Double = 1
            '横を基準１としたときの、縦比率
            Dim ImageAspectVerticalRaito As Double = OuterSize.Height / OuterSize.Width

            Select Case FlagCriteriaForMatchSize
                Case 0
                    'Outer(Image)に合わせる
                    NewSize.Width = OuterSize.Width
                    NewSize.Height = OuterSize.Height
                    Exit Select
                Case 1
                    'Horizon(Inner_Control)に合わせる
                    NewSize.Width = InnerSize.Width * ImageAspectHorizontalRaito
                    NewSize.Height = InnerSize.Width * ImageAspectVerticalRaito
                    Exit Select
                Case 2
                    'Vertical(Inner_Control)に合わせる
                    NewSize.Width = InnerSize.Height / ImageAspectVerticalRaito
                    NewSize.Height = InnerSize.Height * 1
                    Exit Select
                Case Else
            End Select
        Catch ex As Exception
            Console.WriteLine("InnerSizeChangeToMatchOuterSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


End Class
