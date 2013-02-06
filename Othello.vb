Public Class Form1
    Dim turn As Integer = 0 'どちらかのターンかを管理する変数　1ならば白、0ならば黒
    Public Enum NUMBER As Integer
        Black
        White
        Cellcount = 8 '一列あたりのマスの数
        Base = 50 '升目の大きさ
    End Enum
    Public Enum STATE
        [Nothing]
        Black
        White
    End Enum
    Dim gridnum(NUMBER.Cellcount - 1, NUMBER.Cellcount - 1) As STATE '盤面の状態を数字で管理し仮想的な盤面を格納するためのもの
    Dim dx() As Integer = {-1, 0, 1, -1, 1, -1, 0, 1}
    Dim dy() As Integer = {-1, -1, -1, 0, 0, 1, 1, 1}
    Dim gsjudge As Boolean = False 'ゲームが始まったかどうかの判定
    Private Sub Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BlackButton.Click, WhiteButton.Click
        Dim g As Graphics = GamePicture.CreateGraphics
        gsjudge = True
        resetgridnum()
        firststonesetpaint()
        linewrite()
        If sender.Equals(WhiteButton) Then
            turn = NUMBER.White
        End If
        turntext()
        BlackNumLabel.Text = "黒" & ":" & 2
        WhiteNumLabel.Text = "白" & ":" & 2
        BlackButton.Enabled = False
        WhiteButton.Enabled = False
        PassButton.Enabled = False
        ResetButton.Enabled = False
    End Sub
    Private Sub PictureBox_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GamePicture.MouseDown
        Dim boardX As Integer = e.X \ NUMBER.Base
        Dim boardY As Integer = e.Y \ NUMBER.Base
        Dim ncashe As Integer = 0
        Dim whitenum As Integer = 0
        Dim blacknum As Integer = 0
        If boardX = NUMBER.Cellcount Then
            boardX = NUMBER.Cellcount - 1
        End If
        If boardY = NUMBER.Cellcount Then
            boardY = NUMBER.Cellcount - 1
        End If
        If gsjudge = True Then
            If gridnum(boardX, boardY) = STATE.Nothing Then
                paintstone(boardX, boardY)
                For Each c As Integer In gridnum
                    ncashe = c
                    If ncashe = NUMBER.Black Then
                        blacknum += 1
                    ElseIf ncashe = NUMBER.White Then
                        whitenum += 1
                    End If
                Next
                BlackNumLabel.Text = "黒" & ":" & blacknum
                WhiteNumLabel.Text = "白" & ":" & whitenum
                judge(blacknum, whitenum)
                If gridnum(boardX, boardY) = turn Then
                    turn = 1 - turn
                End If
                If IsPass() = False Then
                    PassButton.Enabled = True
                End If
                turntext()
            End If

        End If
    End Sub
    Private Sub ResetButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ResetButton.Click
        If gsjudge = True Then
            resetgridnum()
            resetstone()
            linewrite()
            firststonesetpaint()
            WhiteButton.Enabled = True
            BlackButton.Enabled = True
            ResetButton.Enabled = True
            PassButton.Enabled = True
            gsjudge = False
            WhiteNumLabel.Text = ""
            BlackNumLabel.Text = ""
            TurnLabel.Text = ""
        End If
    End Sub
    Private Sub Passbutton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassButton.Click
        turn = 1 - turn
        turntext()
        PassButton.Enabled = False
    End Sub
    Sub paintstone(ByVal x As Integer, ByVal y As Integer)
        Dim g As Graphics = GamePicture.CreateGraphics
        Dim rect As Rectangle
        rect.X = NUMBER.Base * x + 1
        rect.Y = NUMBER.Base * y + 1
        rect.Width = NUMBER.Base - 3
        rect.Height = NUMBER.Base - 3
        If IsSetStone(x, y, True) = True Then
            If turn = NUMBER.Black Then
                g.FillEllipse(Brushes.Black, rect)
                gridnum(x, y) = NUMBER.Black
            Else
                g.FillEllipse(Brushes.White, rect)
                gridnum(x, y) = NUMBER.White
            End If
        End If
    End Sub
    Sub stonenum(ByVal x As Integer, ByVal y As Integer)
        If turn = NUMBER.Black Then

        End If
    End Sub
    Function IsSetStone(ByVal x As Integer, ByVal y As Integer, ByVal z As Boolean) As Boolean
        'その場所に石が置けるかどうかの判定用メソッド
        Dim can As Boolean = False
        Dim nx As Integer
        Dim ny As Integer
        For i As Integer = 0 To NUMBER.Cellcount - 1
            If (x + dx(i) >= 0 And x + dx(i) <= NUMBER.Cellcount - 1) And (y + dy(i) >= 0 And y + dy(i) <= NUMBER.Cellcount - 1) Then
                If gridnum(x + dx(i), y + dy(i)) = 1 - turn Then
                    nx = x + dx(i)
                    ny = y + dy(i)
                    Do
                        nx += dx(i)
                        ny += dy(i)
                        If (nx < 0 Or nx > NUMBER.Cellcount - 1) Or (ny < 0 Or ny > NUMBER.Cellcount - 1) Then
                            Exit Do
                        ElseIf gridnum(nx, ny) = STATE.Nothing Then
                            Exit Do
                        ElseIf gridnum(nx, ny) = turn Then
                            can = True
                            If z = True Then 'zがTrueになるのは通常の石を置くメソッドから呼び出された時、Falseになるのはパスの判定を行うメソッドから呼び出されたとき
                                reversestone(nx, ny, i, x, y)
                            End If
                            Exit Do
                        End If
                    Loop
                End If
            Else
                Continue For
            End If
        Next
        Return can
    End Function
    Sub reversestone(ByVal x As Integer, ByVal y As Integer, ByVal i As Integer, ByVal clickX As Integer, ByVal clickY As Integer)
        '石を裏返すメソッド
        Dim g As Graphics = GamePicture.CreateGraphics
        Dim rect As Rectangle
        rect.Width = NUMBER.Base - 3
        rect.Height = NUMBER.Base - 3
        Do
            x += (-1 * dx(i))
            y += (-1 * dy(i))
            rect.X = NUMBER.Base * x + 1
            rect.Y = NUMBER.Base * y + 1
            If x = clickX And y = clickY Then
                Exit Do
            Else
                If turn = NUMBER.Black Then
                    g.FillEllipse(Brushes.Black, rect)
                    gridnum(x, y) = NUMBER.Black
                Else
                    g.FillEllipse(Brushes.White, rect)
                    gridnum(x, y) = NUMBER.White
                End If
            End If

        Loop
    End Sub
    Sub firststonesetpaint()
        '最初の石を描画するメソッド
        Dim i As Integer = 0 'ループが何回行われたかを数える変数
        Dim a As Integer = 3
        Dim b As Integer = 3
        Dim g As Graphics = GamePicture.CreateGraphics
        Dim rect As Rectangle
        rect.Width = NUMBER.Base - 3
        rect.Height = NUMBER.Base - 3
        Do
            rect.X = NUMBER.Base * a + 1
            rect.Y = NUMBER.Base * b + 1
            If a + b = 7 Then
                g.FillEllipse(Brushes.Black, rect)
                gridnum(a, b) = NUMBER.Black
            Else
                g.FillEllipse(Brushes.White, rect)
                gridnum(a, b) = NUMBER.White
            End If
            If a = 4 Then
                a = 3
                b += 1
            Else
                a += 1
            End If
            i += 1
        Loop Until i > 3
    End Sub
    Function IsPass()
        'パスできるかどうかを確かめるメソッド
        Dim can As Boolean = False 'この変数がTrueであればパスはできない、Falseであればパスできる
        For x As Integer = 0 To NUMBER.Cellcount - 1
            For y As Integer = 0 To NUMBER.Cellcount - 1
                If gridnum(x, y) = STATE.Nothing Then
                    gridnum(x, y) = turn
                    If IsSetStone(x, y, False) = True Then
                        can = True
                        gridnum(x, y) = STATE.Nothing
                        Exit For
                    Else
                        gridnum(x, y) = STATE.Nothing
                    End If
                End If
            Next
            If can = True Then
                Exit For
            End If
        Next
        Return can
    End Function
    Sub linewrite(ByVal e As System.Windows.Forms.PaintEventArgs)
        '盤面に線を引くメソッド
        Dim blackpen As New Pen(Brushes.Black)
        blackpen.Width = 2.0F
        Dim g As Graphics = GamePicture.CreateGraphics
        For n As Integer = 0 To NUMBER.Cellcount
            g.DrawLine(blackpen, NUMBER.Base * n, 0, NUMBER.Base * n, NUMBER.Base * NUMBER.Cellcount)
            g.DrawLine(blackpen, 0, NUMBER.Base * n, NUMBER.Base * NUMBER.Cellcount, NUMBER.Base * n)
        Next
    End Sub
    Sub resetstone()
        '盤面から石をすべて取り除くメソッド
        Dim rect As Rectangle
        Dim g As Graphics = GamePicture.CreateGraphics
        rect.Width = NUMBER.Base
        rect.Height = NUMBER.Base
        For x As Integer = 0 To NUMBER.Cellcount - 1
            For y As Integer = 0 To NUMBER.Cellcount - 1
                rect.X = NUMBER.Base * x
                rect.Y = NUMBER.Base * y
                g.FillEllipse(Brushes.DarkGreen, rect)
            Next
        Next
        GamePicture.BackColor = Color.DarkGreen
    End Sub
    Sub turntext()
        If turn = NUMBER.White Then
            TurnLabel.Text = "白の番です"
        Else
            TurnLabel.Text = "黒の番です"
        End If
    End Sub
    Sub judge(ByVal x As Integer, ByVal y As Integer)
        'どちらが勝ったかを判定するメソッド
        If gsjudge = True Then
            If x + y = NUMBER.Cellcount ^ 2 Then
                If x = y Then
                    MsgBox("引き分けです")
                    ResetButton.Enabled = True
                ElseIf x > y Then
                    MsgBox("黒の勝ちです")
                    ResetButton.Enabled = True
                Else
                    MsgBox("白の勝ちです")
                    ResetButton.Enabled = True
                End If
            ElseIf x = 0 Then
                MsgBox("白の勝ちです")
                ResetButton.Enabled = True
            ElseIf y = 0 Then
                MsgBox("黒の勝ちです")
                ResetButton.Enabled = True
            End If
        End If
    End Sub
    Sub resetgridnum()
        For nx As Integer = 0 To NUMBER.Cellcount - 1
            For ny As Integer = 0 To NUMBER.Cellcount - 1
                gridnum(nx, ny) = STATE.Nothing
            Next
        Next
    End Sub
    Private Sub GamePicture_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles GamePicture.Paint
        'MsgBox("ペイントイベントの発生タイミング")

        Dim rect As Rectangle
        rect.Width = NUMBER.Base - 3
        rect.Height = NUMBER.Base - 3
        If gsjudge = True Then
            linewrite()
            For x As Integer = 0 To NUMBER.Cellcount - 1
                For y As Integer = 0 To NUMBER.Cellcount - 1
                    rect.X = NUMBER.Base * x + 1
                    rect.Y = NUMBER.Base * y + 1
                    If gridnum(x, y) = NUMBER.Black Then
                        e.Graphics.FillEllipse(Brushes.Black, rect)
                    ElseIf gridnum(x, y) = NUMBER.White Then
                        e.Graphics.FillEllipse(Brushes.White, rect)
                    End If
                Next
            Next
        End If
    End Sub
End Class
