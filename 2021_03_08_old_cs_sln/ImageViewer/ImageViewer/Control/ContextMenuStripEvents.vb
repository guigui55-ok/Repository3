Public Class ContextMenuStripEvents
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
    Public Sub New(argMainProcessorObject As MainProcesser, argContextMenuStrip As ContextMenuStrip)
        MainProcessorObject = argMainProcessorObject
        mContextMenuStrip = argContextMenuStrip
    End Sub
    Private WithEvents mContextMenuStrip As ContextMenuStrip
    Private mMenuList As List(Of String)
    '=======================================================================
    Public Sub setMenuList(argList As List(Of String))
        Try
            mMenuList = argList
        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunction.setMenuList")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    Private Sub UpdateMenuList()
        'mContextMenuStrip.Items.
    End Sub

    Public Sub MakeMenuStripDefault()
        Try
            Dim pContextMenuStrip As ContextMenuStrip = New ContextMenuStrip
            pContextMenuStrip.Items.Add("終了")
            pContextMenuStrip.Items.Add("スライドショーの停止")

        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunction.MakeMenuStripDefault")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

End Class
