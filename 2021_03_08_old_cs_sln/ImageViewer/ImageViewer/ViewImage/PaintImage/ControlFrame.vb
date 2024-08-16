Imports ImageViewer

Public Class ControlFrameAsPanel
    Implements IControlFrame

    Private mFramePanel As Panel
    Private m_size As Size

    Public Property Visible As Boolean Implements IControlFrame.Visible
        Get
            Return mFramePanel.Visible
        End Get
        Set(flag As Boolean)
            mFramePanel.Visible = flag
        End Set
    End Property

    Public Sub setFrameSize(size As PointF) Implements IControlFrame.setFrameSize
        Throw New NotImplementedException()
    End Sub

    Public Sub setControl(panel As Control) Implements IControlFrame.setControl
        mFramePanel = panel
    End Sub

    Public Sub setSize(size As Size) Implements IControlFrame.setSize
        m_size = size
    End Sub

    Public Sub applySize() Implements IControlFrame.applySize
        Try
            mFramePanel.Size = m_size
        Catch ex As Exception
            Console.WriteLine("applySize")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub changeSize(size As Size) Implements IControlFrame.changeSize
        setSize(size)
        applySize()
    End Sub

    Public Function getSize() As Size Implements IControlFrame.getSize
        Return mFramePanel.Size
    End Function

    Public Function getLocation() As Point Implements IControlFrame.getLocation
        Return mFramePanel.Location
    End Function

    Public Function getControl() As AbstractControlBridge Implements IControlFrame.getControl
        Return New ControlBridgeForPanel(Me.mFramePanel)
    End Function


    Public Function getMousePointOnThisControl() As Point Implements IControlFrame.getMousePointOnThisControl
        Try
            '指定した画面上のポイントを計算してクライアント座標を算出
            Dim mp As Point = mFramePanel.PointToClient(System.Windows.Forms.Cursor.Position)

            If ((mp.X <= mFramePanel.Width) And (mp.Y <= mFramePanel.Height)) Then
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

End Class
