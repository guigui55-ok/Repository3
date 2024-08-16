Public Class MenuStripTextLists
    Private mMenuStripItemText As String
    Private mToolStripItemTextLists As List(Of ToolStripItemTextLists) = New List(Of ToolStripItemTextLists)
    Private mCurrentIndex As Integer

    Public Sub New()

    End Sub

    Public Sub New(menuText As String)
        addListToPararel(menuText)
    End Sub

    Public Function getToolStripItemTextLists() As List(Of ToolStripItemTextLists)
        Try
            Return mToolStripItemTextLists
        Catch ex As Exception
            Console.WriteLine("getToolStripItemTextLists")
            Console.WriteLine(ex.Message)
            Return mToolStripItemTextLists
        End Try
    End Function

    '今の階層のIndexに追加　並列
    Public Sub addListToPararel(menuText As String)
        Try
            Dim indexAfterExtends As Integer = Me.getMaxIndex + 1
            If Not isListsExists() Then
                'Listが存在していない
            Else
                'Listが存在している
            End If
            mToolStripItemTextLists.Add(New ToolStripItemTextLists(menuText))
        Catch ex As Exception
            Console.WriteLine("extendIndexLists")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '今の階層からParentにChildをついか　直列
    Public Sub addListToChild(ParentText As String, ChildText As String)
        Try
            MoveCurrentIndex(ParentText)
            mToolStripItemTextLists(mCurrentIndex).addListToChild(ChildText)
        Catch ex As Exception
            Console.WriteLine("addToolStripList")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '今の階層のParentにChildをついか　直列　親指定しないIndexで
    Public Sub addListToChild(ChildText As String)
        Try
            mToolStripItemTextLists(mCurrentIndex).addListToChild(ChildText)
        Catch ex As Exception
            Console.WriteLine("extendIndexLists")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function existsParentText(parentText As String) As Boolean
        Try
            For Each tempToolStrip In mToolStripItemTextLists
                If tempToolStrip.getText() = parentText Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Console.WriteLine("existsParentText")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function getTextCurrentIndex() As String
        Try
            Return mToolStripItemTextLists(mCurrentIndex).getText
        Catch ex As Exception
            Console.WriteLine("addToolStripList")
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    Public Sub MoveCurrentIndex(ParentText As String)
        Try
            Dim count As Integer = 0
            For Each tempToolStrip In mToolStripItemTextLists
                If tempToolStrip.getText() = ParentText Then
                    mCurrentIndex = count
                    Exit For
                End If
                count += 1
            Next
        Catch ex As Exception
            Console.WriteLine("MoveCurrentIndex")
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    '最大要素数
    '拡張時に使う
    Private Function getMaxIndex() As Integer
        Try
            Return mToolStripItemTextLists.Count
        Catch ex As IndexOutOfRangeException
            Return 0
        Catch ex As Exception
            Console.WriteLine("getMaxIndex")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function

    Private Function isListsExists() As Boolean
        Try
            If mToolStripItemTextLists.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function




End Class
