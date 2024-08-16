Public Class KeyState
    Public DownData As Integer
    Public UpData As Integer
    Public KeyDown As Boolean
    Public KeyUp As Boolean
    Public NowEvent As KeyEventArgs
    Public BeforeData As Integer
    Public mKeyStateDetail As KeyStateDetail = New KeyStateDetail()


    Public Sub clearKeys()
        KeyDown = Nothing
    End Sub

    Property setEvent As KeyEventArgs
        Set(e As KeyEventArgs)
            'monitor()
            NowEvent = e
            If e Is Nothing Then
                clearAll()
            End If
        End Set
        Get
            Return NowEvent
        End Get
    End Property

    Property Donw As Boolean
        Set(flag As Boolean)
            KeyDown = True
        End Set
        Get
            Return KeyDown
        End Get
    End Property
    Public Sub setDown(e As KeyEventArgs)
        DownData = True
        setEvent = e
        DownData = e.KeyData
        mKeyStateDetail.saveKey(NowEvent.KeyCode, 1)
    End Sub

    Property Up As Boolean
        Set(flag As Boolean)
            KeyUp = True
        End Set
        Get
            Return KeyDown
        End Get
    End Property
    Public Sub setUp(e As KeyEventArgs)
        UpData = True
        setEvent = e
        UpData = e.KeyData
        DownData = DownData - UpData 'check
        mKeyStateDetail.saveKey(NowEvent.KeyCode, 0)
    End Sub

    Public Sub clearUp()
        Try
            KeyUp = False
            UpData = 0
        Catch ex As Exception
            Console.WriteLine("clearAll")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub clearAll()
        Try
            KeyDown = False
            KeyUp = False
            DownData = 0
            UpData = 0
        Catch ex As Exception
            Console.WriteLine("clearAll")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function isKeyDownAnyKey() As Boolean
        Try
            If KeyDown Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Console.WriteLine("isKeyDownAnyKey")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function isKeyDown(TargetKeyData As Integer) As Boolean
        Try
            'If KeyDown Then
            '    Return False
            'Else
            '    Return True
            'End If
            If DownData = TargetKeyData Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("isKeyDown")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function


End Class
