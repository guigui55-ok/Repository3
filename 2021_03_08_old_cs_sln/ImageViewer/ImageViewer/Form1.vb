

Public Class MainForm
    WithEvents MainProcessorObject As MainProcesser


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            '------------------------------
            MainProcessorObject = New MainProcesser(Me)
            MainProcessorObject.setLog(New Log())
            'ImageViewer限定クラスでaddLogメソッドを楽に使えるようにGlobalのModuleのLogクラスに割り当てる
            setLogForGlobal(MainProcessorObject.gLog)
            'Initialize
            MainProcessorObject.gEvents.gInitialize.excute()
            'Initializeの終わりに実行
            MainProcessorObject.gEvents.gInitialize.excuteEndMethod()
        Catch ex As Exception
            Console.WriteLine("Form1_Load")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub MainForm_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles MyBase.PreviewKeyDown

        addLog(3, "MainForm_PreviewKeyDown : " & e.KeyValue)
        'MainProcessorObject.PictureBoxKeyDown(sender, CType(e, KeyEventArgs))
    End Sub
End Class

