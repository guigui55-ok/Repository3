Public Class ToolStripMenuItemEditor

    Public Sub New()

    End Sub


    'どちらかの値に合致したもの_設定OFFON用
    Public Function getToolStripMenuItemToMatchTowValues(ParentObject As Object, valueA As String, valueB As String) As ToolStripMenuItem
        Try
            Dim returnItem As ToolStripMenuItem
            returnItem = getToolStripMenuItem(ParentObject, valueA)
            If returnItem Is Nothing Then
                returnItem = getToolStripMenuItem(ParentObject, valueB)
            End If
            If returnItem Is Nothing Then
                addLog(1, "ToolStripMenuItemEditor.getToolStripMenuItemToMatchTowValues item is Nothing : " & valueA & "," & valueB)
                Return Nothing
            End If
            Return returnItem
        Catch ex As Exception
            addLogForSystemError("ToolStripMenuItemEditor.getToolStripMenuItemToMatchTowValues")
            addLogForSystemError(ex.Message)
            Return Nothing
        End Try
    End Function

    'ToolStripMenuItemの値をフラグによって変える
    Public Sub ChangeTextOfToolStripMenuItem(argToolStripMenuItem As ToolStripMenuItem, TrueText As String, FalseText As String, flag As Boolean)
        Try
            'setChangeText
            Dim changeText As String = ""
            If flag Then
                changeText = TrueText
            Else
                changeText = FalseText
            End If
            'Text変更
            If Not argToolStripMenuItem.Text = changeText Then
                argToolStripMenuItem.Text = changeText
            End If
        Catch ex As Exception
            addLogForSystemError("ToolStripMenuItemEditor.ChangeTextOfToolStripMenuItem")
            addLogForSystemError(ex.Message)
        End Try
    End Sub

    'フラグによってMenuStripItemのTextを切り替える
    Private Sub ChangeText(ParentObject As Object, TrueToFalseText As String, FalseToTrueText As String, flag As Boolean)
        Try
            If Not isUsedTypeToObject(ParentObject) Then
                Throw New ArgumentException("isUsedTypeToObject(ParentObject)")
            End If

            'Object取得前処理①
            Dim gettext As String
            If New ToolStripMenuItemEditor().isExistsValueInObjectHasMenuStripItem(ParentObject, TrueToFalseText) Then
                gettext = TrueToFalseText
            ElseIf New ToolStripMenuItemEditor().isExistsValueInObjectHasMenuStripItem(ParentObject, FalseToTrueText) Then
                gettext = FalseToTrueText
            Else
                addLog(1, "ContextMenuStripFunctionForViewImage.ChangeText Error")
                gettext = FalseToTrueText
            End If

            'Object取得→Text変更
            Dim tmpToolStrip As ToolStripMenuItem = New ToolStripMenuItemEditor().getToolStripMenuItem(ParentObject, gettext)
            Dim changeText As String = ""
            If flag Then
                changeText = TrueToFalseText
            Else
                changeText = FalseToTrueText
            End If

            'Text変更
            If Not tmpToolStrip.Text = changeText Then
                tmpToolStrip.Text = changeText
            End If
        Catch ex As Exception
            addLogForSystemError("ContextMenuStripFunctionForViewImage.ChangeText")
            addLogForSystemError(ex.Message)
        End Try
    End Sub


    'MenuStripItemを持っているオブジェクト(ContextMenu、MenuStrip等)に、value(Item.Text)が存在しているか
    Public Function isExistsValueInObjectHasMenuStripItem(parentObject As Object, value As String) As Boolean
        Try
            If Not isUsedTypeToObject(parentObject) Then
                Throw New ArgumentException("isUsedTypeToObject(ParentObject)")
            End If

            Dim item As ToolStripMenuItem
            For Each item In ParentObject.Items
                If item.Text = value Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Console.WriteLine("isExistsValueInObjectHasMenuStripItem")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Private Function isUsedTypeToObject(argObject As Object) As Boolean
        Try
            If VarType(argObject) = VarType(New MenuStrip()) Then
                Return True
            End If
            If VarType(argObject) = VarType(New ContextMenuStrip()) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Console.WriteLine("getToolStripMenuItem")
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function



    'MenuStrip内のToolStripMenuItemの、valueに合致したものを得る
    Public Function getToolStripMenuItem(ParentObject As Object, value As String) As ToolStripMenuItem
        Try
            If Not isUsedTypeToObject(ParentObject) Then
                Throw New ArgumentException("isUsedTypeToObject(ParentObject)")
            End If

            Dim item As ToolStripMenuItem
            For Each item In ParentObject.Items
                If item.Text = value Then
                    Return item
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Console.WriteLine("getToolStripMenuItem")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function


    'MenuStripの子メニューから、Valueに合致したものを得る
    Public Function getToolStripMenuItemInToolStripMenuItem(MenuItem As ToolStripMenuItem, value As String) As ToolStripMenuItem
        Try
            Dim item As ToolStripMenuItem
            If Not MenuItem.HasDropDown Then
                Return Nothing
            End If
            For Each item In MenuItem.DropDownItems
                If item.Text = value Then
                    Return item
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Console.WriteLine("getToolStripMenuItemInToolStripMenuItem")
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

    'ParetObjectに登録、既に存在する場合は登録しないver
    Public Sub addToolStripMenuItemToParentObjectWhenNotAddIItemAlreadyExists(ParentObject As Object, textAdded As String)
        Try
            If Not isUsedTypeToObject(ParentObject) Then
                Throw New ArgumentException("isUsedTypeToObject(ParentObject)")
            End If

            'CheckExists
            If Not Me.isExistsValueInObjectHasMenuStripItem(ParentObject, textAdded) Then
                'メニュー項目を作成する
                Me.addItemToMenuStrip(ParentObject, textAdded)
            Else
                addLog(3, "addToolStripMenuItemToParentObjectWhenNotAddIItemAlreadyExists : already exists = " & textAdded)
            End If

        Catch ex As Exception
            Console.WriteLine("addToolStripMenuItemToParentObjectWhenNotAddIfAlreadyExists")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'オブジェクト受け取らないVer
    Public Sub addItemToMenuStrip(ParentObject As Object, textAdded As String)
        Try
            'メニュー項目を作成する
            Dim tempMenuItem As New ToolStripMenuItem()
            tempMenuItem.Text = textAdded
            ParentObject.Items.Add(tempMenuItem) '追加する

        Catch ex As Exception
            Console.WriteLine("addItemToMenuStrip")
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
