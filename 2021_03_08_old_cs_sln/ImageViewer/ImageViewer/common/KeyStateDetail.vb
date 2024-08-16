Public Class KeyStateDetail
    Private mKeyMemoryAry(256, 1) As Integer 'Keyの操作配列、（KeyCode,Key操作（下記））
    'Key操作→Nothing:0,Down:1,Hold(LongDown):2,Up:3
    'Key離したときに（Up）Nothing状態となるので、Up時はDeleteする
    Private ReadOnly MAX_KEY_STATE_LEVEL As Integer = 2

    Private mCount As Integer 'Keyの保存数、1からスタート

    Public Sub keystate()
        mCount = 0
    End Sub

    'Donw Hold したときにSaveすること
    Public Sub saveKey(keycode As Integer, keystate As Integer)
        If Not (keystate >= 0 And keystate <= MAX_KEY_STATE_LEVEL) Then
            'keyが操作以外の値はゼロにする
            keystate = 0
            Console.WriteLine("KeyState,saveKey keystate value error")
        End If

        '同じKeyを操作していれば更新
        Dim KeyCodeUse As Integer = isUseKeyCode(keycode)
        If KeyCodeUse > 0 Then
            mKeyMemoryAry(KeyCodeUse, 0) = keycode
            mKeyMemoryAry(KeyCodeUse, 1) = keystate
        Else
            '同じKeyは操作していない
            mCount += 1
            mKeyMemoryAry(mCount, 0) = keycode
            mKeyMemoryAry(mCount, 1) = keystate
        End If
    End Sub

    '同じKeyを操作しているか
    Private Function isUseKeyCode(keycode As Integer) As Integer
        Try
            For i = 1 To mCount
                If mKeyMemoryAry(i, 0) = keycode Then
                    Return i
                End If
            Next
            Return 0
        Catch ex As Exception
            Console.WriteLine("KeyState,isUseKeyCode")
            Console.WriteLine(ex.Message)
            Return 0
        End Try
    End Function


    Private Sub updateKey(keycode As Integer, keystate As Integer)

        '同じKeyを操作していれば更新
        Dim KeyCodeUse As Integer = isUseKeyCode(keycode)
        If KeyCodeUse > 0 Then
            'UpdateKey
            'キーが正しく操作されているか
            If isCorrectUseKey(mKeyMemoryAry(KeyCodeUse, 1), keystate) Then

                'KeyDown＋Donw→Hold、Hold＋Down→Hold
                'Holdにするかしないか,上記パターンの場合書き換える
                keystate = KeyStateToHoldWhenDownToDownOrHoldToDown(mKeyMemoryAry(KeyCodeUse, 1), keystate)

                mKeyMemoryAry(KeyCodeUse, 0) = keycode
                mKeyMemoryAry(KeyCodeUse, 1) = keystate

            Else
                '更新なし
                mKeyMemoryAry(KeyCodeUse, 0) = keycode
                mKeyMemoryAry(KeyCodeUse, 1) = 0
            End If
        Else
            'AddKey
            '同じKeyは操作していない
            If keystate <= 0 Then
                '同じキーは操作されていないので配列に追加
                mCount += 1
                mKeyMemoryAry(mCount, 0) = keycode
                mKeyMemoryAry(mCount, 1) = keystate

            End If
        End If

        If keystate = 0 Then
            'updateしたときにしか実行されないはず
            deleteKey(KeyCodeUse)
        End If
    End Sub

    'Holdにするかしないか
    Private Function KeyStateToHoldWhenDownToDownOrHoldToDown(beforestate As Integer, nowstate As Integer) As Integer
        Dim flag As Boolean
        If beforestate = 1 And nowstate = 1 Then
            'Down + Down -> Hold
            flag = True
        End If
        If beforestate = 2 And nowstate = 1 Then
            'Hold + Down -> Hold
            flag = True
        End If

        If flag Then
            Return 2
        Else
            Return nowstate
        End If
    End Function

    '間が抜けることがある
    '課題：マルチスレッド時の処理


    'KeyUp したときにDeleteすること
    Private Sub deleteKey(nowindex As Integer)
        '更新した後
        If nowindex = mCount Then
            mKeyMemoryAry(nowindex, 0) = 0
            mKeyMemoryAry(nowindex, 1) = 0
        Else
            Dim tempkeycode As Integer = mKeyMemoryAry(mCount, 0)
            Dim tempstate As Integer = mKeyMemoryAry(mCount, 1)

            '消したところに、最後のものを入れるとオーバーフローしないはず
            mKeyMemoryAry(mCount, 0) = tempkeycode
            mKeyMemoryAry(mCount, 1) = tempstate
        End If
    End Sub

    'キーが正しく操作されているか
    Private Function isCorrectUseKey(beforestate As Integer, nowstate As Integer) As Boolean
        '押していないのに離したorUp判定とかはNG
        '↓以下パターン
        '可　Down→Up(Nothing)、Down→Hold、Hold→Up(Nothing)、Nothing(Up)→Down
        '付加　Hold→Down、Nothing→Hold
        Dim flag As Boolean = True
        If beforestate = 2 And nowstate = 1 Then
            'Hold -> Down
            flag = False
        End If
        If beforestate = 0 And nowstate = 2 Then
            'Nothing(Up) -> Hold
            flag = False
        End If

        If Not flag Then
            'error
            Console.WriteLine("KeyState,isCorrectUseKey  key state change error")
        End If

        Return flag
    End Function

End Class
