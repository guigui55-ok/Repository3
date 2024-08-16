Public Class MouseState
    Public MouseDown As Boolean
    Public MouseUp As Boolean
    Public MouseMove As Boolean
    Public MouseClick As Boolean
    Public NowEvent As MouseEventArgs
    Public DownPoint As Point
    Public MovePoint As Point

    Public Sub clearButton()
        monitor()
        MouseDown = False
        MouseUp = False
        MouseClick = False
    End Sub

    Property setEvent As MouseEventArgs
        Set(e As MouseEventArgs)
            monitor()
            NowEvent = e
            If e Is Nothing Then
                clearAll()
            End If
        End Set
        Get
            Return NowEvent
        End Get
    End Property


    Public Sub clearAll()
        Try
            MouseDown = False
            MouseUp = False
            MouseMove = False
            MouseClick = False
            MovePoint = New Point(0, 0)
            DownPoint = New Point(0, 0)
        Catch ex As Exception
            Console.WriteLine("clearEvent")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Move
    Property Move As Boolean
        Set(flag As Boolean)
            MouseMove = True
            'If MouseDown Then
            '    Stop
            'End If
            'If MovePoint = DownPoint Then
            '    MouseMove = False
            'End If
        End Set
        Get
            If NowEvent Is Nothing Then
                clearAll()
            End If
            Return MouseMove
        End Get
    End Property
    Public Sub setMove(e As MouseEventArgs)
        MovePoint = convertMouseEventArgsToPoint(e)
        Move = True
        setEvent = e
    End Sub

    'Down
    Property Down As Boolean
        Set(flag As Boolean)
            MouseDown = True
            MouseUp = False
            If MovePoint = DownPoint Then
                MouseMove = False
            End If
        End Set
        Get
            If NowEvent Is Nothing Then
                clearAll()
            End If
            Return MouseDown
        End Get
    End Property
    Public Sub setDown(e As MouseEventArgs)
        DownPoint = convertMouseEventArgsToPoint(e)
        Down = True
        setEvent = e
        'setPoint(DownPoint, e)
    End Sub

    'Up
    Property Up As Boolean
        Set(flag As Boolean)
            MouseDown = False
            MouseUp = True
            MouseMove = False
            'clickはMoveが入るとDragになる
            MouseClick = True
        End Set
        Get
            If NowEvent Is Nothing Then
                clearAll()
            End If
            Return MouseUp
        End Get
    End Property
    Public Sub setUp(e As MouseEventArgs)
        Up = True
        setEvent = e
    End Sub

    'Click
    Property Click As Boolean
        Set(flag As Boolean)
            'clickはMoveが入るとDragになる
            MouseClick = True
            MouseMove = False
        End Set
        Get
            If NowEvent Is Nothing Then
                clearAll()
            End If
            Return MouseClick
        End Get
    End Property
    Public Sub setClick(e As MouseEventArgs)
        Click = True
        setEvent = e
    End Sub

    Private Sub setPoint(ByRef argPoint As Point, e As MouseEventArgs)
        Try
            argPoint = convertMouseEventArgsToPoint(e)
        Catch ex As Exception
            Console.WriteLine("setPoint")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function convertMouseEventArgsToPoint(e As MouseEventArgs) As Point
        Try
            Return New Point(e.X, e.Y)

        Catch ex As Exception
            Console.WriteLine("convertMouseEventArgsToPoint")
            Console.WriteLine(ex.Message)
            Return New Point(0, 0)
        End Try
    End Function


    Public Function ButtonsLeft() As Boolean
        Try
            If NowEvent Is Nothing Then
                Return False
            End If
            If NowEvent.Button = MouseButtons.Left Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("MouseButtonsLeft")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function isMove() As Boolean
        Return Me.Move
    End Function

    Public Function isDown() As Boolean
        Return Me.Down
    End Function

    'isDrag
    Public Function isMoveAndDownLeft() As Boolean
        Try
            If MovePoint = DownPoint Then
                Return False
            Else
                Return True
            End If
            If Me.Down And Me.Move Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("isMoveAndDown")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function getPoint() As Point
        Try
            Return New Point(NowEvent.X, NowEvent.Y)
        Catch ex As Exception
            Console.WriteLine("getPoint")
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Private Sub monitor()
        'If False Then Exit Sub
        If True Then Exit Sub
        Dim now As String = ""
        If isMoveOnly() Then Exit Sub

        If isDrag() Then
            'now = now & "drag "
            Exit Sub
        End If

        If Down Then
            now = now & "down [ X=" & DownPoint.X & " , Y=" & DownPoint.Y & " ] "
        End If
        If Up Then
            now = now & "up "
        End If
        If Move Then
            now = now & "move "
        End If
        If Click Then
            now = now & "click "
        End If
        'Console.WriteLine(now)
        addLog(5, now)

    End Sub



    Public Function isDrag() As Boolean
        If Move And Down Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function isMoveOnly() As Boolean
        If Move And Down Then
            If MovePoint = DownPoint Then
                'Down only
                Return False
            Else
                'Down and Move
                Return True
            End If
        End If
        If Move And (Not (Down And Up And Click)) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
