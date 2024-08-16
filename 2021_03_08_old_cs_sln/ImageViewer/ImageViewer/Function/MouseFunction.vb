Public Class MouseFunction
    Inherits AbstractFunction

    'Friend MainProcessorObject As MainProcesser
    'Public Sub New(argMainProcessor As MainProcesser)
    '    MainProcessorObject = argMainProcessor
    'End Sub
    'Public Sub New()
    'End Sub
    'Private Sub setMainProcessor(argMainProcessor As MainProcesser)

    Public Sub New(argMainProcessorObject As MainProcesser)
        MainProcessorObject = argMainProcessorObject
    End Sub
    '=======================================================================
    'ドラッグアンドドロップでのリストを得る
    Public Function getFileNamesByDragAndDrop(sender As Object, e As DragEventArgs) As List(Of String)
        Dim buflist As List(Of String) = New List(Of String)
        Try
            buflist = New FileListMaker().getFileNameByDragAndDrop(sender, e)

            For Each buf In buflist
                Console.WriteLine(buf)
            Next
            Return buflist
        Catch ex As Exception
            addLogForSystemError("getFileNamesByDragAndDrop")
            addLogForSystemError(ex.Message)
            Return buflist
        End Try
    End Function
    '=======================================================================


End Class
