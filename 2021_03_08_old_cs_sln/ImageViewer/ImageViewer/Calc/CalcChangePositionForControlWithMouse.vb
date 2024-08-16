Public Class CalcChangePositionForControlWithinMouse
    Inherits AbstractCalcChangeLocationForControl

    Friend NowControlLocation As Point
    Friend CalcMaxSize As Size
    Friend CalcMinSize As Size
    Friend NewSize As Size

    Private MousePoint As Point
    Private NowEvent As MouseEventArgs

    Public Sub New()

    End Sub

    Public Sub setEvent(argNowEvent As MouseEventArgs, argMousePoint As Point)
        MousePoint = argMousePoint
        NowEvent = argNowEvent
    End Sub

    'Public Sub New(argNowEvent As MouseEventArgs, argMousePoint As Point)
    '    setEvent(argNowEvent, argMousePoint)
    'End Sub

    Public Sub New(argFrameSize As Size, argControlSize As Size, argControlLocation As Point)
        setSize(argFrameSize, argControlSize, argControlLocation)
    End Sub

    Public Overloads Sub setSize(argOuterFrameSize As Size, argInnerControlSize As Size, argControlLocation As Point)
        OuterFrameSize = argOuterFrameSize
        InnerControlSize = argInnerControlSize
        NowControlLocation = argControlLocation
    End Sub


    Public Overloads Sub CalcLocation()
        Try
            CalcPositionMoveAble()
            calcChangeLocation(NowEvent, MousePoint)

        Catch ex As Exception
            Console.WriteLine("CalcLocation")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Overloads Sub CalcPositionMoveAble()
        Try
            '可動範囲
            Dim WidthMinMax As Integer() = calcLimitSize(OuterFrameSize.Width, InnerControlSize.Width)
            Dim HeightMinMax As Integer() = calcLimitSize(OuterFrameSize.Height, InnerControlSize.Height)
            CalcMaxSize = New Size(WidthMinMax(1), HeightMinMax(1))
            CalcMinSize = New Size(WidthMinMax(0), HeightMinMax(0))
        Catch ex As Exception
            Console.WriteLine("CalcPosition")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function calcLimitSize(OuterFrameLength As Integer, InnerControlLength As Integer) As Integer()
        Try
            Dim Difference As Integer = OuterFrameLength - InnerControlLength
            Dim MaxSize As Integer
            Dim Minsize As Integer
            If Difference > 0 Then
                'OuterFrame > InnerControl
                MaxSize = OuterFrameLength - InnerControlLength
                Minsize = 0
            Else
                'InnerControl > OuterFrame
                MaxSize = 0
                Minsize = OuterFrameLength - InnerControlLength
            End If

            Return {Minsize, MaxSize}

        Catch ex As Exception
            Console.WriteLine("calcLimitSize")
            Console.WriteLine(ex.Message)
            Return {0, 0}
        End Try
    End Function

    Public Sub calcChangeLocation(NowEvent As MouseEventArgs, MousePoint As Point)
        Try
            If (NowEvent.Button And MouseButtons.Left) = MouseButtons.Left Then
                'Console.WriteLine("Drag")
                Dim NewLeft As Integer = NowControlLocation.X + NowEvent.X - MousePoint.X
                Dim NewTop As Integer = NowControlLocation.Y + NowEvent.Y - MousePoint.Y

                NewLeft = calcRegionMoveAble(NewLeft, CalcMinSize.Width, CalcMaxSize.Width)
                NewTop = calcRegionMoveAble(NewTop, CalcMinSize.Height, CalcMaxSize.Height)

                'Console.WriteLine("  location:" & NewTop & "," & NewLeft)
                NewSize.Width = NewLeft
                NewSize.Height = NewTop
            Else
                NewSize.Width = NowControlLocation.X
                NewSize.Height = NowControlLocation.Y
            End If

            InnerControlNewLocation = New PointF(NewSize.Width, NewSize.Height)
            'Console.WriteLine(PicBox.Location.X & " : " & PicBox.Location.Y)

            'Frame < Innerのとき
            'Max <= => Min 入れ替える

        Catch ex As Exception
            Console.WriteLine("changeLocation")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function calcRegionMoveAble(DefaltPos As Integer, MinPos As Integer, MaxPos As Integer) As Integer
        Try
            If (DefaltPos) > MaxPos Then
                DefaltPos = MaxPos
            ElseIf (MinPos > DefaltPos) Then
                DefaltPos = MinPos
            End If
            Return DefaltPos
        Catch ex As Exception
            Console.WriteLine("calcRegionMoveAble")
            Console.WriteLine(ex.Message)
            Return DefaltPos
        End Try
    End Function

End Class
