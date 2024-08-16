Imports ImageViewer

Public Class ControlForPaintAsPictureBox
    Implements IControlForPaint

    Private PicBox As PictureBox
    Private SizeUpdateStateFlag As Boolean = False
    Private mSizeBeforeChangeSize As Size
    Private mLocationBeforeChangeSize As Point


    Public ReadOnly Property SizeUpdateState As Boolean Implements IControlForPaint.SizeUpdateState
        Get
            Return SizeUpdateStateFlag
        End Get
    End Property


    Public Property ScrollVarVisible As Boolean Implements IControlForPaint.ScrollVarVisible
        Get
            If (PicBox.Anchor = AnchorStyles.Left + AnchorStyles.Top) Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(flag As Boolean)
            If flag Then
                PicBox.Anchor = AnchorStyles.Left + AnchorStyles.Top
            Else
                picbox.Anchor = AnchorStyles.Left And AnchorStyles.Top And AnchorStyles.Right And AnchorStyles.Bottom
            End If
        End Set
    End Property

    Public Property Visible As Boolean Implements IControlForPaint.Visible
        Get
            Return PicBox.Visible
        End Get
        Set(flag As Boolean)
            PicBox.Visible = flag
        End Set
    End Property


    Public Sub New()

    End Sub

    Public Sub New(argPictureBox As PictureBox)
        PicBox = argPictureBox
    End Sub

    Public Sub setImage(argImage As Image) Implements IControlForPaint.setImage
        PicBox.Image = argImage
    End Sub

    Public Sub setFrameRect(frameRect As RectangleF) Implements IControlForPaint.setFrameRect
        Throw New NotImplementedException()
    End Sub

    Public Sub setScrollBarVisible(flag As Boolean) Implements IControlForPaint.setScrollBarVisible
        Try
            If flag Then
                PicBox.Anchor = AnchorStyles.Left And AnchorStyles.Top And AnchorStyles.Right And AnchorStyles.Bottom
            Else
                PicBox.Anchor = AnchorStyles.Left + AnchorStyles.Top
            End If
        Catch ex As Exception
            Console.WriteLine("setScrollBarVisible")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub setControl(picturebox As Control) Implements IControlForPaint.setControl
        PicBox = picturebox
    End Sub

    Public Sub setImageNotDispose(argImage As Image) Implements IControlForPaint.setImageNotDispose
        PicBox.Image = argImage
    End Sub

    Public Sub setImageAndDispose(argImage As Image) Implements IControlForPaint.setImageAndDispose
        Try

            If Not PicBox.Image Is Nothing Then
                'PicBox.Image.Dispose()
            End If
            PicBox.Image = argImage
        Catch ex As Exception
            Console.WriteLine(Me.ToString & ".setImageAndDispose")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub setPaintImage(argImage As Image) Implements IControlForPaint.setPaintImage
        setImageAndDispose(argImage)
    End Sub


    Public Function getImage() As Image Implements IControlForPaint.getImage
        If PicBox.Image Is Nothing Then
            Return Nothing
        End If
        Return PicBox.Image
    End Function

    Public Function getSize() As Size Implements IControlForPaint.getSize
        If PicBox Is Nothing Then
            Return New Size(0, 0)
        Else
            Return PicBox.Size
        End If
    End Function

    Public Function getLocation() As Point Implements IControlForPaint.getLocation
        Return PicBox.Location
    End Function

    Public Sub setSize(argSize As Size) Implements IControlForPaint.setSize
        changeSize(argSize)
    End Sub


    Sub setImageSizeAndLocation(argSize As Size, argLocation As Point) Implements IControlForPaint.setImageSizeAndLocation
        Try
            If PicBox.Image Is Nothing Then
            Else
            End If
            Console.WriteLine("error setImageSizeAndLocation")
        Catch ex As Exception
            Console.WriteLine("setImageSizeAndLocation")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub changeSize(argSize As Size) Implements IControlForPaint.changeSize
        Try
            If PicBox.Size.Equals(New Size(0, 0)) Then
                addLog(0, "ControlForPaintAsPictureBox : PictureBox Size Is Zero")
                'Exit Sub
            End If
            If PicBox.Size.Width = argSize.Width And PicBox.Size.Height = argSize.Height Then
                'Console.WriteLine("ControlForPaintAsPictureBox : PictureBox Size Save argSize")
                Exit Sub
            End If
            SizeUpdateStateFlag = True
            PicBox.Size = argSize
            SizeUpdateStateFlag = False
        Catch ex As Exception
            Console.WriteLine("ChangeFormWindowSize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub changeSizeAndImage(argSize As Size)
        'changeSize()
    End Sub

    Public Sub setVisible(flag As Boolean)
        Try
            PicBox.Visible = flag
        Catch ex As Exception
            Console.WriteLine("setVisible")
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    '----------------------------------------------------------------------------------------
    '単に
    Public Sub changePosition(argPoint As Point) Implements IControlForPaint.changePosition
        PicBox.Location = argPoint
    End Sub

    '範囲無Ver
    Public Sub changeLocationWithEvent(e As MouseEventArgs, MousePoint As Point) Implements IControlForPaint.changeLocation
        Try
            If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
                PicBox.Left += e.X - MousePoint.X
                PicBox.Top += e.Y - MousePoint.Y
            End If
        Catch ex As Exception
            Console.WriteLine("changeLocationWithEvent")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'クラス受取Ver
    Public Overloads Sub changeLocation(CalcChangePositionObject As AbstractCalcChangeLocationForControl) _
        Implements IControlForPaint.changePosition
        '@@@
        CalcChangePositionObject.calcLocation()
        PicBox.Location = CalcChangePositionObject.getLocationAsPoint
    End Sub

    Public Overloads Sub changeLocation(argPoint As Point) Implements IControlForPaint.changeLocation
        Try
            PicBox.Location = argPoint
        Catch ex As Exception
            addLogForSystemError(Me.ToString & ".changeLocation")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    '全部Ver
    Public Sub changeLocationWithinFrame(argFrameSize As Size, e As MouseEventArgs, MouseBeginPoint As Point) Implements IControlForPaint.changeLocationWithinFrame
        Try
            '@@@
            Dim CalcChangePositionObject As New CalcChangePositionForControlWithinMouse(argFrameSize, PicBox.Size, PicBox.Location)
            CalcChangePositionObject.setEvent(e, MouseBeginPoint)
            CalcChangePositionObject.CalcLocation()
            PicBox.Location = CalcChangePositionObject.getLocationAsPoint()

        Catch ex As Exception
            Console.WriteLine("changeLocationWithinFrame")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function hasImageIsNothing() As Boolean Implements IControlForPaint.hasImageIsNothing
        Try
            If PicBox.Image Is Nothing Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("hasImageIsNothing")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function ImageIsError() As Boolean Implements IControlForPaint.ImageIsError
        Try
            If hasImageIsNothing() Then
                Return True
            End If
            If PicBox.Image Is Nothing Then
                Return True
            End If
            Dim tmp As String
            tmp = PicBox.Image.Flags
            tmp = tmp
            Return False
        Catch ex As Exception
            addLog(3, Me.ToString & ".ImageIsError")
            addLog(3, ex.Message)
            'Console.WriteLine(Me.ToString & ".ImageIsError")
            'Console.WriteLine(ex.Message)
            Return True
        End Try
    End Function

    Public Function getControlBridge() As AbstractControlBridge Implements IControlForPaint.getControlBridge
        Try
            Return New ControlBridgeForPictureBox(PicBox)
        Catch ex As Exception
            Console.WriteLine("getControl")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function getImageSize() As Size Implements IControlForPaint.getImageSize
        Try
            If PicBox.Image Is Nothing Then
                Return New Size(0, 0)
            Else
                Return PicBox.Image.Size
            End If
        Catch ex As Exception
            Console.WriteLine("getImageSize")
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Public Function getMousePointOnThisControl() As Point Implements IControlForPaint.getMousePointOnThisControl
        Try
            '指定した画面上のポイントを計算してクライアント座標を算出
            Dim mp As Point = PicBox.PointToClient(System.Windows.Forms.Cursor.Position)

            If ((mp.X <= PicBox.Width) And (mp.Y <= PicBox.Height)) Then
                'Return New Point(PicBox.Width, PicBox.Height
                Return mp
            End If

            If ((mp.X >= 0) And (mp.Y >= 0)) Then
                Return New Point(0, 0)
            End If
        Catch ex As Exception
            Console.WriteLine("getMousePointOnThisControl")
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Public Sub saveSizeAndLocationWhenSizeChangeByMouseWheel() Implements IControlForPaint.saveSizeAndLocationWhenSizeChangeByMouseWheel
        Try
            mLocationBeforeChangeSize = PicBox.Location
            mSizeBeforeChangeSize = PicBox.Size
        Catch ex As Exception
            Console.WriteLine("saveSizeAndLocationWhenSizeChangeByMouseWheel")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function getSizeBeforeSizeChange() As Size Implements IControlForPaint.getSizeBeforeSizeChange
        Return mSizeBeforeChangeSize
    End Function

    Public Function getLocationBeforeSizeChange() As Point Implements IControlForPaint.getLocationBeforeSizeChange
        Return mLocationBeforeChangeSize
    End Function

    '----------------------------------------------------------------------------------------
End Class
