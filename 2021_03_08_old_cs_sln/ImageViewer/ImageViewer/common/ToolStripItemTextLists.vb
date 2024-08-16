Public Class ToolStripItemTextLists
    '当クラスは階層上のリストでこのクラスででメニューの位置を指定する
    '操作時位置をmCurrentIndexで保持することで作業の簡略化を図る
    Private mText As String
    Private mToolStripItemTextLists As List(Of ToolStripItemTextLists) = New List(Of ToolStripItemTextLists)
    Private mCurrentIndex As Integer

    Public Sub New(ItemText As String)
        setText(ItemText)
    End Sub

    Public Sub setText(value As String)
        mText = value
    End Sub

    Public Function getText() As String
        Return mText
    End Function

    '今の階層に追加　並列
    Public Sub addListToPararel(menuText As String)
        Try
            mToolStripItemTextLists.Add(New ToolStripItemTextLists(menuText))
        Catch ex As Exception
            Console.WriteLine("extendIndexLists")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '今の階層のParentにChildをついか　直列　親指定Ver
    Public Sub addListToChild(ParentText As String, ChildText As String)
        Try
            MoveCurrentIndex(ParentText)
            addListToChild(ChildText)
        Catch ex As Exception
            Console.WriteLine("extendIndexLists")
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

    'この階層のリストの中のTextを探して、mIndexを更新
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
End Class
