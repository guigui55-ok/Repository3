Public Class MousePointer

    Public Sub New()

    End Sub

    Public Function IsCursorOnControl(picbox As PictureBox) As Boolean
        Try
            Dim mp As Point
            '指定した画面上のポイントを計算してクライアント座標を算出
            mp = picbox.PointToClient(System.Windows.Forms.Cursor.Position)

            If ((mp.X <= picbox.Width) And (mp.Y <= picbox.Height)) And
                ((mp.X >= 0) And (mp.Y >= 0)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("IsCursorOnControl")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function getMousePoint(form As Form) As Point
        Try
            Return form.PointToClient(System.Windows.Forms.Cursor.Position)
        Catch ex As Exception
            Console.WriteLine("IsCursorOnControl")
            Console.WriteLine(ex.Message)
            Return New Point(0, 0)
        End Try
    End Function
End Class
