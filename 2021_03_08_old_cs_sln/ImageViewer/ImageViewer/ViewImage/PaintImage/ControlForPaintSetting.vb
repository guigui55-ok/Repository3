Imports ImageViewer

Public Class ControlForPaintSetting
    Implements IControlForPaintSetting

    Private InnerLocation As Point
    Private InnerSize As Size

    Public Sub New()

    End Sub

    Public Sub New(argInnerSize As Size, argInnerLocation As Point)
        InnerLocation = argInnerLocation
        InnerSize = argInnerSize
    End Sub

    Public Sub setInnerControlLocation(argLocation As Point) Implements IControlForPaintSetting.setInnerControlLocation
        InnerLocation = argLocation
    End Sub

    Public Sub setInnerControlSize(argSize As Size) Implements IControlForPaintSetting.setInnerControlSize
        InnerSize = argSize
    End Sub

    'PictureBox>Panel LocationChange
    Public Sub resetLocationWhenPictureBoxGreatherThanPanel(argOuterSize As Size) Implements IControlForPaintSetting.resetLocationWhenPictureBoxGreatherThanPanel
        Try
            'MaxMin値の制御
            Dim MaxLocation As Point
            Dim MinLocation As Point
            'Width
            If argOuterSize.Width - InnerSize.Width >= 0 Then
                'Inner小さい
                MaxLocation.X = argOuterSize.Width - InnerSize.Width
                MinLocation.X = 0
            Else
                'Inner大きい
                MaxLocation.X = 0
                MinLocation.X = argOuterSize.Width - InnerSize.Width
            End If
            'Height
            If argOuterSize.Height - InnerSize.Height >= 0 Then
                'Inner小さい
                MaxLocation.Y = argOuterSize.Height - InnerSize.Height
                MinLocation.Y = 0
            Else
                'Inner大きい
                MaxLocation.Y = 0
                MinLocation.Y = argOuterSize.Height - InnerSize.Height
            End If

            If InnerLocation.X > MaxLocation.X Then
                InnerLocation.X = MaxLocation.X
            ElseIf MinLocation.x > InnerLocation.X Then
                InnerLocation.X = MinLocation.X
            End If

            If InnerLocation.Y > MaxLocation.Y Then
                InnerLocation.Y = MaxLocation.Y
            ElseIf MinLocation.y > InnerLocation.y Then
                InnerLocation.Y = MinLocation.Y
            End If

        Catch ex As Exception
            Console.WriteLine("SetInnerSizeByCalcSizeByImageRaito")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '20/7/8
    'PictureBox移動時に範囲内に収める用
    Private Function getLocationIfOverMaxOrMin(
        OuterSize As Size, InnerSize As Size, NewLocation As Point) As Point
        Try
            Dim minX, minY, maxX, maxY As Integer
            Dim differentX As Integer = InnerSize.Width - OuterSize.Width
            Dim differentY As Integer = InnerSize.Height - OuterSize.Height

            'PictureBoxとPanelのサイズによってLocationの最大最小値が変わる_X
            If differentX > 0 Then
                'InnerSize.Width > OuterSizeNow.Width
                minX = -differentX
                maxX = 0
            Else
                'InnerSize.Width < OuterSizeNow.Width
                minX = 0
                maxX = -differentX
            End If
            'PictureBoxとPanelのサイズによってLocationの最大最小値が変わる_Y
            If differentY > 0 Then
                'InnerSize.Width > OuterSizeNow.Width
                minY = -differentY
                maxY = 0
            Else
                'InnerSize.Width < OuterSizeNow.Width
                minY = 0
                maxY = differentY * (-1)
            End If

            'Location.Xを決定
            If NewLocation.X < minX Then
                NewLocation.X = minX
            End If
            If NewLocation.X > maxX Then
                NewLocation.X = maxX
            End If
            'Location.Yを決定
            If NewLocation.Y < minY Then
                NewLocation.Y = minY
            End If
            If NewLocation.Y > maxY Then
                NewLocation.Y = maxY
            End If

            'addLog(3, "minX:" & minX & "  maxX:" & maxX)
            'addLog(3, "minY:" & minY & "  maxY:" & maxY)

            Return NewLocation
        Catch ex As Exception
            addLogForSystemError("SetInnerSizeByCalcSizeByImageRaito")
            addLogForSystemError(ex.Message)
            Return New Point(0, 0)
        End Try
    End Function

    '20/07/08
    'Panel_MouseWheelイベントでのサイズ変更時のLocationを取得
    'PanelSize＞PictureBoxSize(Width_Heightどちらも小さいとき)用
    '中央に寄せるようにする
    Public Function getLocationWhenSizeChangePictureBoxByMouseWheelWhenPictureBoxSmallerThanPanel(
        raito As Double, InnerSizeNow As Size, OuterSizeNow As Size) As Point _
        Implements IControlForPaintSetting.getLocationWhenSizeChangePictureBoxByMouseWheelWhenPictureBoxSmallerThanPanel
        '元の位置:InnerLocation
        Dim tPoint As Point
        Try
            '移動する距離
            tPoint.X = ((InnerSizeNow.Width * raito) - InnerSizeNow.Width) / 2
            tPoint.Y = ((InnerSizeNow.Height * raito) - InnerSizeNow.Height) / 2

            '元の位置＋移動する距離
            tPoint.Y = InnerLocation.Y - tPoint.Y
            tPoint.X = InnerLocation.X - tPoint.X

            'PicBoxの移動範囲内に収める
            tPoint = Me.getLocationIfOverMaxOrMin(OuterSizeNow, InnerSizeNow, tPoint)

            Return tPoint
        Catch ex As Exception
            addLogForSystemError("getLocationWhenSizeChangePictureBoxByMouseWheelWhenPictureBoxSmallerThanPanel")
            addLogForSystemError(ex.Message)
            Return New Point(0, 0)
        Finally
            tPoint = Nothing
        End Try
    End Function

    '20/07/08
    'Panel_MouseWheelイベントでのサイズ変更時のLocationを取得
    'PanelSize＜PictureBoxSize(Width_Heightどちらかが大きいとき)用
    '現在のMPがPictureBoxのどこを指しているか
    'そこを中心に拡大
    'Innner.LocationはOuter.Sizeをはみ出ないこと
    Public Function getLocationWhenSizeChangePictureBoxByMouseWheel(
        BeforeMP As Point, raito As Double, BeforeInnerSize As Size, OuterSizeNow As Size) As Point _
        Implements IControlForPaintSetting.getLocationWhenSizeChangePictureBoxByMouseWheel
        Dim tPoint As Point
        Try
            'マウスPosがPicBox外の時の処理
            If BeforeMP.X < 0 Then BeforeMP.X = 0
            If BeforeMP.Y < 0 Then BeforeMP.Y = 0

            '現在の位置から移動する距離
            tPoint.X = (BeforeMP.X - (BeforeInnerSize.Width / 2))
            tPoint.Y = (BeforeMP.Y - (BeforeInnerSize.Height / 2))
            tPoint.X = tPoint.X * raito * (-1)
            tPoint.Y = tPoint.Y * raito * (-1)

            'addLog(3, "tPoint.x:" & tPoint.X & "  tPoint.y:" & tPoint.Y)
            'NewPictureBoxSize
            Dim InnerSizeNow As Size = New Size(BeforeInnerSize.Width * raito, BeforeInnerSize.Height * raito)

            'addLog(3, "InnerLocation.x:" & InnerLocation.X & "  InnerLocation.y:" & InnerLocation.Y)
            '元の位置＋移動する距離
            tPoint.Y = tPoint.Y + InnerLocation.Y
            tPoint.X = tPoint.X + InnerLocation.X

            'PicBoxの移動範囲内に収める
            tPoint = Me.getLocationIfOverMaxOrMin(OuterSizeNow, InnerSizeNow, tPoint)
            'addLog(3, "InnerLocation.x:" & InnerLocation.X & "  InnerLocation.y:" & InnerLocation.Y)

            Return tPoint
        Catch ex As Exception
            addLogForSystemError("SetInnerSizeByCalcSizeByImageRaito")
            addLogForSystemError(ex.Message)
            Return New Point(0, 0)
        Finally
            tPoint = Nothing
        End Try
    End Function

    'xxx200708
    'PictureBox>Panel LocationChange
    Public Sub resetLocationWhenPictureBoxGreatherThanPanelByMousePointAndPictureBoxSize(
        OuterSizeNow As Size, innerSizeNow As Size, locationNow As Point, raito As Double, beforeMP As Point,
        BeforeInnerSize As Size, BeforeLocation As Point) _
        Implements IControlForPaintSetting.resetLocationWhenPictureBoxGreatherThanPanelByMousePointAndPictureBoxSize
        Try
            If beforeMP.X < 0 Then beforeMP.X = 0
            If beforeMP.Y < 0 Then beforeMP.Y = 0
            '③PictureBox上のマウスポイントBeforeMPからPictureBoxの真ん中の座標を引いたもの
            '③×Raito×(-1)＝NewLocationになる
            '(BeforeMP－BeforeInnerSize)*Raito
            Dim tPoint As Point
            tPoint.X = (beforeMP.X - (BeforeInnerSize.Width / 2))
            tPoint.Y = (beforeMP.Y - (BeforeInnerSize.Height / 2))
            tPoint.X = tPoint.X * raito * (-1)
            tPoint.Y = tPoint.Y * raito * (-1)

            addLog(3, "tPoint.x:" & tPoint.X & "  tPoint.y:" & tPoint.Y)

            'Max=different,Min=different*(-1)

            Dim differentX As Double = InnerSize.Width - OuterSizeNow.Width
            Dim differentY As Double = InnerSize.Height - OuterSizeNow.Height
            If (differentX < 0 And differentY < 0) Then
                'PictureBoxのほうが小さい
                'NowLocation
                'addLog(3, "resetLocationWhenPictureBoxGreatherThanPanelByMousePointAndPictureBoxSize")
                'addLog(3, "InnerLocation.x:" & InnerLocation.X & "  InnerLocation.y:" & InnerLocation.Y)
                'Exit Sub
            End If

            InnerLocation = Me.getLocationIfOverMaxOrMin(OuterSizeNow, innerSizeNow, tPoint)
            addLog(3, "InnerLocation.x:" & InnerLocation.X & "  InnerLocation.y:" & InnerLocation.Y)

            If True Then
                Exit Sub
            End If

            Dim minX, minY, maxX, maxY As Integer

            'PictureBoxとPanelのサイズによってLocationの最大最小値が変わる_X
            If differentX > 0 Then
                'InnerSize.Width > OuterSizeNow.Width
                minX = -differentX
                maxX = differentX
            Else
                'InnerSize.Width < OuterSizeNow.Width
                minX = 0
                maxX = -differentX
            End If
            'PictureBoxとPanelのサイズによってLocationの最大最小値が変わる_Y
            If differentY > 0 Then
                'InnerSize.Width > OuterSizeNow.Width
                minY = -differentY
                maxY = differentY
            Else
                'InnerSize.Width < OuterSizeNow.Width
                minY = 0
                maxY = -differentY
            End If
            addLog(3, "minX:" & minX & "  maxX:" & maxX)
            addLog(3, "minY:" & minY & "  maxY:" & maxY)

            'Location.Xを決定
            If tPoint.X < minX Then
                tPoint.X = minX
            End If
            If tPoint.X > maxX Then
                tPoint.X = maxX
            End If
            'Location.Yを決定
            If tPoint.Y < minY Then
                tPoint.Y = minY
            End If
            If tPoint.Y > maxY Then
                tPoint.Y = maxY
            End If

            'PictureBoxよりPanelが大きいとき
            'InnerSize.Width < OuterSizeNow.Width
            If differentY < 0 Then
                tPoint.Y = tPoint.Y + InnerLocation.Y
            End If
            If differentX < 0 Then
                tPoint.X = tPoint.X + InnerLocation.X
            End If

            Me.InnerLocation.X = tPoint.X
            Me.InnerLocation.Y = tPoint.Y
            addLog(3, "InnerLocation.x:" & InnerLocation.X & "  InnerLocation.y:" & InnerLocation.Y)

            If True Then
                Exit Sub
            End If

            '前ののLocation BeforeLocation、この分Locationがずれる①
            InnerLocation = BeforeLocation

            'Inner(PictureBox_Image)前のサイズと今のサイズの差　この分Locationがずれる②
            Dim resultWidth As Integer = innerSizeNow.Width - BeforeInnerSize.Width
            Dim resultHeight As Integer = innerSizeNow.Height - BeforeInnerSize.Height
            InnerLocation.X = InnerLocation.X - resultWidth
            InnerLocation.Y = InnerLocation.Y - resultHeight

            'mp中心との差 この分Locationがずれる③
            Dim aftermp As Point
            aftermp.X = (OuterSizeNow.Width / 2) - (beforeMP.X)
            aftermp.Y = (OuterSizeNow.Height / 2) - beforeMP.Y
            InnerLocation.X -= aftermp.X
            InnerLocation.Y -= aftermp.Y

            addLog(3, Me.ToString & "resetLocation_ : ")
            Dim logmp As String
            logmp = "0x:" & aftermp.X & "  0y:" & aftermp.Y & "  mp  =>  "
            logmp = logmp & "1x:" & InnerLocation.X & "  1y:" & InnerLocation.Y
            addLog(3, logmp)


            'MaxMin値の制御
            Dim MaxLocation As Point
            Dim MinLocation As Point
            'Width
            If OuterSizeNow.Width - InnerSize.Width >= 0 Then
                'Inner小さい
                MaxLocation.X = OuterSizeNow.Width - InnerSize.Width
                MinLocation.X = 0
            Else
                'Inner大きい
                MaxLocation.X = 0
                MinLocation.X = OuterSizeNow.Width - InnerSize.Width
            End If
            'Height
            If OuterSizeNow.Height - InnerSize.Height >= 0 Then
                'Inner小さい
                MaxLocation.Y = OuterSizeNow.Height - InnerSize.Height
                MinLocation.Y = 0
            Else
                'Inner大きい
                MaxLocation.Y = 0
                MinLocation.Y = OuterSizeNow.Height - InnerSize.Height
            End If


            '計算後InnerがMaxMinより大きい小さい場合X
            If InnerLocation.X > MaxLocation.X Then
                InnerLocation.X = MaxLocation.X
            ElseIf MinLocation.X > InnerLocation.X Then
                InnerLocation.X = MinLocation.X
            End If

            '計算後InnerがMaxMinより大きい小さい場合Y
            If InnerLocation.Y > MaxLocation.Y Then
                InnerLocation.Y = MaxLocation.Y
            ElseIf MinLocation.Y > InnerLocation.Y Then
                InnerLocation.Y = MinLocation.Y
            End If

            addLog(3, "2x:" & InnerLocation.X & "  2y:" & InnerLocation.Y)

        Catch ex As Exception
            Console.WriteLine("SetInnerSizeByCalcSizeByImageRaito")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Imageのサイズの比率をPictureBoxに適用する
    Public Sub SetInnerSizeByCalcSizeByImageRaito(argImageSize As Size, MatchFlag As Integer) Implements IControlForPaintSetting.SetInnerSizeByCalcSizeByImageRaito
        Try
            'A のサイズをBの比率に合わせる Match the size of A to the ratio of B            '
            Dim CalcInnerSizeFromOuterSizeObject As AbstractCalcInnerSizeFromOuterSize _
                = New AbstractCalcInnerSizeFromOuterSize(argImageSize, InnerSize, MatchFlag)
            CalcInnerSizeFromOuterSizeObject.calcSize()
            InnerSize = CalcInnerSizeFromOuterSizeObject.getNewSize
        Catch ex As Exception
            Console.WriteLine("SetInnerSizeByCalcSizeByImageRaito")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub setInnerSizeByCalcSizeForFitOuterSize(argOuterSize As Size) Implements IControlForPaintSetting.setInnerSizeByCalcSizeForFitOuterSize
        Try
            Dim CalcFitSizeFormToOuterObject As CalcFitSizeFormToOuter _
                = New CalcFitSizeFormToOuter(argOuterSize, InnerSize)
            CalcFitSizeFormToOuterObject.calcSize()
            InnerSize = CalcFitSizeFormToOuterObject.getNewSize
        Catch ex As Exception
            Console.WriteLine("setInnerSizeByCalcSizeForFitOuterSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '描画しなおし用
    'PictureBoxのLocationをPanelSizeによって変える
    Public Sub SetInnerLocationByCalcLocation(
            argOuterSize As Size, HorizontalLocationFlag As Integer, VerticalLocationFlag As Integer) Implements IControlForPaintSetting.SetInnerLocationByCalcLocation
        '0 OuterのTop_0
        '1 Outer_Center
        '2 Outer_Bottom_PanelHeight
        Try
            Dim CalcChangeLocationForControlObject As AbstractCalcChangeLocationForControl _
                = New AbstractCalcChangeLocationForControl(argOuterSize, InnerSize, HorizontalLocationFlag, VerticalLocationFlag)
            CalcChangeLocationForControlObject.calcLocation()
            InnerLocation = CalcChangeLocationForControlObject.getLocationAsPoint
        Catch ex As Exception
            Console.WriteLine("SetInnerSizeByCalcSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'WindowResize用
    Public Sub SetInnerLocationByCalcLocationForWindowResize(
            argOuterSize As Size) Implements IControlForPaintSetting.SetInnerLocationByCalcLocationForWindowResize
        Try
            Dim CalcChangeLocationForControlObject As AbstractCalcChangeLocationForControl _
                = New CalcChangeLocationForControlByChangeWindowSize(argOuterSize, InnerSize)
            CalcChangeLocationForControlObject.calcLocation()
            InnerLocation = CalcChangeLocationForControlObject.getLocationAsPoint
        Catch ex As Exception
            Console.WriteLine("SetInnerSizeByCalcSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function getInnerControlLocation() As Point Implements IControlForPaintSetting.getInnerControlLocation
        'addLog(3, "getInnerControlLocation.x = ", InnerLocation.X & " , " & "getInnerControlLocation.y" & InnerLocation.Y)
        Return Me.InnerLocation
    End Function

    Public Function getInnerControlSize() As Size Implements IControlForPaintSetting.getInnerControlSize
        Return InnerSize
    End Function
End Class
