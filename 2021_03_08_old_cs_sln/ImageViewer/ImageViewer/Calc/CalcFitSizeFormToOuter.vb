Public Class CalcFitSizeFormToOuter
    Inherits AbstractCalcInnerSizeFromOuterSize

    Public Sub New(argOuterSize As Size, argInnerSize As Size)
        setSize(argOuterSize, argInnerSize, 0)
    End Sub

    'PictureBoxのサイズをPanelサイズにFitさせる
    '＝PictureBoxのサイズをPanelサイズからはみ出さないようにする

    'サイズを合わせるための基準 Criteria for matching sizes
    'Public Overloads Sub setFlagCriteriaForMatchSize(argFlag As Integer)
    '0 Image OuterFit
    '1 Inner_Horizontal 幅はInnerで高さにOuterの比率をかける
    '2 Inner_Vertical　高さはInnerで幅にOuterの比率をかける
    '    FlagCriteriaForMatchSize = argFlag
    'End Sub
    'フラグ関係なくFit計算する
    '0 Inner_Fit
    '(縦横がすべて収まる_and_OuterとInnerのWidthHeightそれぞれ倍率の小さいほうを基準にする)
    Private Sub CalcForFit()
        Try
            Dim AspectRaito As Double = InnerSize.Width / InnerSize.Height
            Dim NewInnerRaito As Double

            Dim VerticalRaito As Double = OuterSize.Height / InnerSize.Height
            Dim HorizontalRaito As Double = OuterSize.Width / InnerSize.Width
            Dim NewWidth As Double
            Dim NewHeight As Double

            If InnerSize.Equals(New Size(0, 0)) Then
                Console.WriteLine("CalcFitSizeFormToOuter.CalcForFit : InnerSize Is Zero")
                NewSize.Width = 0
                NewSize.Height = 0
                Exit Sub
            End If

            '倍率が小さいほうを設定
            If VerticalRaito < HorizontalRaito Then
                '高さに合わせる
                NewHeight = OuterSize.Height * 1
                'NewWidth = InnerSize.Width * VerticalRaito
                'NewWidth = InnerSize.Width * HorizontalRaito
                NewInnerRaito = NewHeight / InnerSize.Height
                NewWidth = InnerSize.Height * NewInnerRaito * AspectRaito
            Else
                '幅に合わせる
                'NewHeight = InnerSize.Height * HorizontalRaito
                NewWidth = OuterSize.Width * 1
                NewInnerRaito = NewWidth / InnerSize.Width
                'NewInnerRaito = InnerSize.Width / NewWidth

                NewHeight = InnerSize.Height * NewInnerRaito '* AspectRaito

                'NewHeight = NewWidth * NewInnerRaito
            End If
            NewSize.Width = NewWidth
            NewSize.Height = NewHeight

            'Console.WriteLine(NewSize.Width & ":" & NewSize.Height)
        Catch ex As Exception
            Console.WriteLine("CalcFitSizeFormToOuter.CalcForFit")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Overloads Sub calcSize()
        Try
            CalcForFit()
        Catch ex As Exception
            Console.WriteLine("calcSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
