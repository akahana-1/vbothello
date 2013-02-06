Public Class Form1
    Dim cnt As Integer = 0 'ピクチャボックスが何回押されたか
    Dim n() As Integer = {0, 50, 100, 150, 200, 250, 300, 350, 400}  '描写等に必要な配列、各線の値
    Dim gridnum(7, 7) As Integer  'どこのマスが選択されたかをこの配列に代入
    Dim whitenum, blacknum, tortal As Integer '黒と白がそれぞれいくつあるか
    Dim dx() As Integer = {-1, 0, 1, -1, 1, -1, 0, 1}
    Dim dy() As Integer = {-1, -1, -1, 0, 0, 1, 1, 1}
    Dim fieldstate As Boolean = False

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim num As Integer = 3
        PictureBox1.BackColor = Color.LawnGreen
        Dim numx As Integer = 4
        fieldstate = True
        For nx As Integer = 0 To 7 '配列に代入された値の数のリセット
            For ny As Integer = 0 To 7
                gridnum(nx, ny) = 0
            Next
        Next
        For n As Integer = 0 To 8 'ラインの描画
            g.DrawLine(Pens.Black, 50 * n, 0, 50 * n, 400)
        Next
        For n As Integer = 0 To 8 'ラインの描画
            g.DrawLine(Pens.Black, 0, 50 * n, 400, 50 * n)
        Next
        Do '初期配置石の描画
            g.FillEllipse(Brushes.Black, n(num), n(num), n(1), n(1))
            num += 1
        Loop While num = 4
        num -= 2
        Do '初期配置石の描画
            g.FillEllipse(Brushes.White, n(num), n(numx), n(1), n(1))
            num += 1
            numx -= 1
        Loop While num = 4
        If RadioButton1.Checked Then
            gridnum(3, 3) = 2 '初期配置の石をおく
            gridnum(3, 4) = 1 '上に同じ
            gridnum(4, 3) = 1 '上に同じ
            gridnum(4, 4) = 2 '上に同じ

        Else
            gridnum(3, 3) = 1 '初期配置の石をおく
            gridnum(3, 4) = 2 '上に同じ
            gridnum(4, 3) = 2 '上に同じ
            gridnum(4, 4) = 1 '上に同じ
        End If
        labeltext()
        RadioButton1.Enabled = False
        Button3.Enabled = False
    End Sub


    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        Dim boardX As Integer = e.X \ 50 '座標を50で割ったもの
        Dim boardY As Integer = e.Y \ 50 '上に同じ
        cnt += 1 'クリックされたのが何回目かの判断

        If cnt Mod 2 = 0 Then '押された回数が偶数か奇数かの選択、偶数であればTrue
            If RadioButton1.Checked Then
                setstoneblack(boardX, boardY)
            Else
                setstonewhite(boardX, boardY)
            End If
        Else
            If RadioButton1.Checked Then
                setstonewhite(boardX, boardY)
            Else
                setstoneblack(boardX, boardY)
            End If
        End If

        whitenum = 0 '白の数
        blacknum = 0 '黒の数
        tortal = 0
        If fieldstate = False Then 'スタートボタンが押されたか押されていないかの判定、押されればfieldstateはTrueになる
        Else
            For Each c As Integer In gridnum 'gridnum内に入っている値の偶数と奇数の値のチェック
                tortal = c
                If RadioButton1.Checked Then
                    If tortal = 0 Then
                    ElseIf tortal Mod 2 = 0 Then
                        blacknum += 1
                    Else
                        whitenum += 1
                    End If
                Else
                    If tortal = 0 Then
                    ElseIf tortal Mod 2 = 0 Then
                        whitenum += 1
                    Else
                        blacknum += 1
                    End If
                End If
            Next

            Label1.Text = "黒" & " : " & blacknum
            Label2.Text = "白" & " : " & whitenum
            judge(blacknum, whitenum)
        End If

    End Sub
    Sub setstoneblack(ByVal x As Integer, ByVal y As Integer)
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim numyoko As Integer = x 'boardXと同じ
        Dim numtate As Integer = y 'boardYと同じ
        Dim shikibetsu As Boolean

        For s As Integer = 0 To 7
            If gridnum(x, y) <> 0 Then
            Else
                If y + dy(s) < 0 Or x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) > 7 Then '座標が範囲外になった場合、処理をスルー
                Else
                    numyoko = x + dx(s)
                    numtate = y + dy(s)
                    If RadioButton1.Checked Then
                        If gridnum(numyoko, numtate) Mod 2 <> 0 Then 'マスに入っているのが今置いた石と反対の色かどうか
                            Do
                                numyoko += dx(s)
                                numtate += dy(s)
                                If numtate < 0 Or numyoko < 0 Or numyoko > 7 Or numtate > 7 Then
                                    Exit Do
                                ElseIf gridnum(numyoko, numtate) Mod 2 = 0 Then 'マスに入っているのが今置いた石と同じ色かどうか
                                    If gridnum(numyoko, numtate) <> 0 Then
                                        g.FillEllipse(Brushes.Black, n(x), n(y), n(1), n(1))
                                        gridnum(x, y) = cnt
                                        shikibetsu = True
                                        Exit Do
                                    End If
                                End If
                            Loop
                        End If
                    Else
                        If gridnum(numyoko, numtate) Mod 2 = 0 Then 'マスに入っているのが今置いた石と反対の色かどうか
                            If gridnum(numyoko, numtate) <> 0 Then
                                Do
                                    numyoko += dx(s)
                                    numtate += dy(s)
                                    If numtate < 0 Or numyoko < 0 Or numyoko > 7 Or numtate > 7 Then
                                        Exit Do
                                    ElseIf gridnum(numyoko, numtate) Mod 2 <> 0 Then 'マスに入っているのが今置いた石と同じ色かどうか
                                        If gridnum(numyoko, numtate) <> 0 Then
                                            g.FillEllipse(Brushes.Black, n(x), n(y), n(1), n(1))
                                            gridnum(x, y) = cnt
                                            shikibetsu = True
                                            Exit Do
                                        End If
                                    End If
                                Loop
                            End If
                        End If
                    End If
                End If
            End If

        Next

        If shikibetsu = True Then
            Changestone(x, y)
            labeltext()
            If stonestate() = False Then
                Button3.Enabled = True
            End If
        Else
            cnt -= 1
        End If

    End Sub
    Sub setstonewhite(ByVal x As Integer, ByVal y As Integer)
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim numyoko As Integer = x 'boardXと同じ
        Dim numtate As Integer = y 'boardYと同じ
        Dim shikibetsu As Boolean

        For s As Integer = 0 To 7
            If gridnum(x, y) <> 0 Then
            Else
                If y + dy(s) < 0 Or x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) > 7 Then 'マスに入っているのが今置いた石と反対の色かどうか
                Else
                    numyoko = x + dx(s)
                    numtate = y + dy(s)
                    If RadioButton1.Checked Then
                        If gridnum(numyoko, numtate) Mod 2 = 0 Then 'マスに入っているのが今置いた石と反対の色かどうか
                            If gridnum(numyoko, numtate) <> 0 Then
                                Do
                                    numyoko += dx(s)
                                    numtate += dy(s)
                                    If numtate < 0 Or numyoko < 0 Or numyoko > 7 Or numtate > 7 Then
                                        Exit Do
                                    ElseIf gridnum(numyoko, numtate) Mod 2 <> 0 Then
                                        If gridnum(numyoko, numtate) <> 0 Then 'マスに入っているのが今置いた石と同じ色かどうか
                                            g.FillEllipse(Brushes.White, n(x), n(y), n(1), n(1))
                                            gridnum(x, y) = cnt
                                            shikibetsu = True
                                            Exit Do
                                        End If
                                    End If
                                Loop
                            End If
                        End If
                    Else
                        If gridnum(numyoko, numtate) Mod 2 <> 0 Then 'マスに入っているのが今置いた石と反対の色かどうか
                            If gridnum(numyoko, numtate) <> 0 Then
                                Do
                                    numyoko += dx(s)
                                    numtate += dy(s)
                                    If numtate < 0 Or numyoko < 0 Or numyoko > 7 Or numtate > 7 Then
                                        Exit Do
                                    ElseIf gridnum(numyoko, numtate) Mod 2 = 0 Then
                                        If gridnum(numyoko, numtate) <> 0 Then 'マスに入っているのが今置いた石と同じ色かどうか
                                            g.FillEllipse(Brushes.White, n(x), n(y), n(1), n(1))
                                            gridnum(x, y) = cnt
                                            shikibetsu = True
                                            Exit Do
                                        End If
                                    End If
                                Loop
                            End If
                        End If
                    End If
                End If
            End If
        Next

        If shikibetsu = True Then 'そのマスに石が置かれているかいないかの判断
            Changestone(x, y)
            labeltext()
            If stonestate() = False Then
                Button3.Enabled = True
            End If
        Else 'もし置かれていなかった場合
            cnt -= 1
        End If

    End Sub
    Sub Changestone(ByVal x As Integer, ByVal y As Integer)
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim numx As Integer = x
        Dim numy As Integer = y
        Dim gridstate(7, 7) As Boolean
        Dim evax, evay As Integer


        If gridnum(numx, numy) Mod 2 = 0 Then
            For n As Integer = 0 To 7
                If y + dy(n) < 0 Or x + dx(n) < 0 Or x + dx(n) > 7 Or y + dy(n) > 7 Then 'まわりのマスが存在しない場合のみ処理を行わない
                Else
                    If gridnum(x + dx(n), y + dy(n)) Mod 2 <> 0 Then '置いた石と反対の色の石が置いてあるかの判断
                        gridstate(x + dx(n), y + dy(n)) = True
                    End If
                End If
            Next
        Else
            For n As Integer = 0 To 7
                If y + dy(n) < 0 Or x + dx(n) < 0 Or x + dx(n) > 7 Or y + dy(n) > 7 Then 'まわりのマスが存在しない場合のみ処理を行わない
                Else
                    If gridnum(x + dx(n), y + dy(n)) Mod 2 = 0 Then '置いた石と反対の色の石が置いてあるかの判断
                        If gridnum(x + dx(n), y + dy(n)) <> 0 Then
                            gridstate(x + dx(n), y + dy(n)) = True
                        End If
                    End If
                End If
            Next
        End If

        If RadioButton1.Checked Then '石を裏返す処理
            If gridnum(numx, numy) Mod 2 = 0 Then
                For s As Integer = 0 To 7
                    If y + dy(s) < 0 Or x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) > 7 Then 'まわりのマスが存在しない場合のみ処理を行わない
                    Else
                        If gridstate(x + dx(s), y + dy(s)) = True Then 'そのマスの石が置いた石と反対の色の場合
                            evax = x + dx(s)
                            evay = y + dy(s)
                            Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 '判定するマスが範囲外に行くまで処理
                                evax += dx(s)
                                evay += dy(s)
                                If evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 Then '判定するマスが範囲外にいった場合
                                Else
                                    If gridnum(evax, evay) Mod 2 <> 0 Then
                                    Else
                                        If gridnum(evax, evay) Mod 2 = 0 Then  'マスに置いてある石が置いた石と同じ色なら
                                            If gridnum(evax, evay) <> 0 Then
                                                Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7
                                                    evax += (-1 * (dx(s)))
                                                    evay += (-1 * (dy(s)))
                                                    If evax = numx And evay = numy Then '置いた石と同じ場所に戻ってきたら
                                                        Exit Do
                                                    Else
                                                        g.FillEllipse(Brushes.Black, n(evax), n(evay), n(1), n(1))
                                                        gridnum(evax, evay) = 2
                                                    End If
                                                Loop
                                            End If
                                            Exit Do
                                        End If
                                        Exit Do
                                    End If
                                End If
                            Loop
                        End If
                    End If
                Next
            Else
                For s As Integer = 0 To 7
                    If y + dy(s) < 0 Or x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) > 7 Then 'まわりのマスが存在しない場合のみ処理を行わない
                    Else
                        If gridstate(x + dx(s), y + dy(s)) = True Then 'そのマスの石が置いた石と反対の色の場合
                            evax = x + dx(s)
                            evay = y + dy(s)
                            Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 '判定するマスが範囲外に行くまで処理
                                evax += dx(s)
                                evay += dy(s)
                                If evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 Then '判定するマスが範囲外にいった場合
                                Else
                                    If gridnum(evax, evay) Mod 2 = 0 Then
                                        If gridnum(evax, evay) <> 0 Then
                                        Else
                                            Exit Do
                                        End If
                                    Else
                                        Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7
                                            evax += (-1 * (dx(s)))
                                            evay += (-1 * (dy(s)))
                                            If evax = numx And evay = numy Then '置いた石と同じ場所に戻ってきたら
                                                Exit Do
                                            Else
                                                g.FillEllipse(Brushes.White, n(evax), n(evay), n(1), n(1))
                                                gridnum(evax, evay) = 1
                                            End If
                                        Loop
                                        Exit Do
                                    End If
                                End If
                            Loop

                        End If
                    End If
                Next
            End If
        Else
            If gridnum(numx, numy) Mod 2 = 0 Then
                For s As Integer = 0 To 7
                    If y + dy(s) < 0 Or x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) > 7 Then 'まわりのマスが存在しない場合のみ処理を行わない
                    Else
                        If gridstate(x + dx(s), y + dy(s)) = True Then 'そのマスの石が置いた石と反対の色の場合
                            evax = x + dx(s)
                            evay = y + dy(s)
                            Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 '判定するマスが範囲外に行くまで処理
                                evax += dx(s)
                                evay += dy(s)
                                If evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 Then '判定するマスが範囲外にいった場合
                                Else
                                    If gridnum(evax, evay) Mod 2 <> 0 Then
                                    Else
                                        If gridnum(evax, evay) Mod 2 = 0 Then  'マスに置いてある石が置いた石と同じ色なら
                                            If gridnum(evax, evay) <> 0 Then
                                                Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7
                                                    evax += (-1 * (dx(s)))
                                                    evay += (-1 * (dy(s)))
                                                    If evax = numx And evay = numy Then '置いた石と同じ場所に戻ってきたら
                                                        Exit Do
                                                    Else
                                                        g.FillEllipse(Brushes.White, n(evax), n(evay), n(1), n(1))
                                                        gridnum(evax, evay) = 2
                                                    End If
                                                Loop
                                            End If
                                            Exit Do
                                        End If
                                        Exit Do
                                    End If
                                End If
                            Loop

                        End If
                    End If
                Next
            Else
                For s As Integer = 0 To 7
                    If y + dy(s) < 0 Or x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) > 7 Then 'まわりのマスが存在しない場合のみ処理を行わない
                    Else
                        If gridstate(x + dx(s), y + dy(s)) = True Then 'そのマスの石が置いた石と反対の色の場合
                            evax = x + dx(s)
                            evay = y + dy(s)
                            Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 '判定するマスが範囲外に行くまで処理
                                evax += dx(s)
                                evay += dy(s)
                                If evay < 0 Or evax < 0 Or evay > 7 Or evax > 7 Then '判定するマスが範囲外にいった場合
                                Else
                                    If gridnum(evax, evay) Mod 2 = 0 Then
                                        If gridnum(evax, evay) <> 0 Then
                                        Else
                                            Exit Do
                                        End If
                                    Else
                                        Do Until evay < 0 Or evax < 0 Or evay > 7 Or evax > 7
                                            evax += (-1 * (dx(s)))
                                            evay += (-1 * (dy(s)))
                                            If evax = numx And evay = numy Then '置いた石と同じ場所に戻ってきたら
                                                Exit Do
                                            Else
                                                g.FillEllipse(Brushes.Black, n(evax), n(evay), n(1), n(1))
                                                gridnum(evax, evay) = 1
                                            End If
                                        Loop
                                        Exit Do
                                    End If
                                End If
                            Loop

                        End If
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim g As Graphics = PictureBox1.CreateGraphics
        For a As Integer = 0 To 7 '盤面のリセット
            For i As Integer = 0 To 7
                g.FillEllipse(Brushes.LawnGreen, 50 * a, 50 * i, n(1), n(1))
            Next
        Next
        For nx As Integer = 0 To 7 '配列に代入された値の数のリセット
            For ny As Integer = 0 To 7
                gridnum(nx, ny) = 0
            Next
        Next
        '値のリセット
        cnt = 0
        Label1.Text = ""
        Label2.Text = ""
        Label3.Text = ""
        RadioButton1.Enabled = True
        RadioButton1.Checked = False
        fieldstate = False
        Button3.Enabled = False
        For n As Integer = 0 To 8 'ラインの描画
            g.DrawLine(Pens.Black, 50 * n, 0, 50 * n, 400)
        Next
        For n As Integer = 0 To 8 'ラインの描画
            g.DrawLine(Pens.Black, 0, 50 * n, 400, 50 * n)
        Next
    End Sub
    Function stonestate() As Boolean '石が置ける場所があるかどうかの判断
        Dim slct As Boolean = False 'その場所に次に石が置けるかどうか、Trueで置ける
        For a As Integer = 0 To 7 'X座標
            For b As Integer = 0 To 7 'Y座標
                If gridnum(a, b) <> 0 Then '0以外の値が入っている＝石が置いてある
                Else
                    If (cnt + 1) Mod 2 = 0 Then
                        Dim x As Integer = a
                        Dim y As Integer = b

                        For s As Integer = 0 To 7
                            If x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) < 0 Or y + dy(s) > 7 Then '範囲外の場合処理をスルー
                            Else

                                If gridnum(x + dx(s), y + dy(s)) <> 0 Then
                                    If gridnum(x + dx(s), y + dy(s)) Mod 2 <> 0 Then '置きたい石と反対の色かどうか
                                        Do
                                            x += dx(s)
                                            y += dy(s)
                                            If y < 0 Or x < 0 Or x > 7 Or y > 7 Then
                                            ElseIf gridnum(x, y) Mod 2 = 0 Then '置きたい石と同じ色かどうか
                                                If gridnum(x, y) <> 0 Then
                                                    If gridnum(x + (-1 * dx(s)), y + (-1 * dy(s))) Mod 2 <> 0 Then '置きたい石と同じ色の石があったとして、その一つ前の石が反対の色か
                                                        slct = True
                                                        Exit Do
                                                    End If
                                                End If
                                            End If
                                        Loop Until y < 1 Or x < 1 Or x > 6 Or y > 6
                                    End If
                                    If slct = True Then
                                        Exit For
                                    End If
                                    End If
                            End If
                            x = a
                            y = b
                        Next

                    Else
                        Dim x As Integer = a
                        Dim y As Integer = b
                        For s As Integer = 0 To 7
                            If x + dx(s) < 0 Or x + dx(s) > 7 Or y + dy(s) < 0 Or y + dy(s) > 7 Then '範囲外の場合処理をスルー
                            Else

                                If gridnum(x + dx(s), y + dy(s)) <> 0 Then
                                    If gridnum(x + dx(s), y + dy(s)) Mod 2 = 0 Then '置きたい石と反対の色かどうか
                                        Do
                                            x += dx(s)
                                            y += dy(s)
                                            If y < 0 Or x < 0 Or x > 7 Or y > 7 Then
                                            ElseIf gridnum(x, y) Mod 2 <> 0 Then
                                                If gridnum(x, y) <> 0 Then
                                                    If gridnum(x + (-1 * dx(s)), y + (-1 * dy(s))) Mod 2 = 0 Then '置きたい石と同じ色の石があったとして、その一つ前の石が反対の色か
                                                        If gridnum(x + (-1 * dx(s)), y + (-1 * dy(s))) <> 0 Then
                                                            slct = True
                                                            Exit Do
                                                        End If

                                                    End If
                                                End If
                                            End If
                                        Loop Until y < 1 Or x < 1 Or x > 6 Or y > 6 '置きたい石と同じ色かどうか
                                    End If
                                    If slct = True Then
                                        Exit For
                                    End If
                                End If
                            End If
                            x = a
                            y = b
                        Next
                    End If
                End If
            Next
        Next
        If slct = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        cnt += 1
        labeltext()
        Button3.Enabled = False
    End Sub

    Sub labeltext()
        If RadioButton1.Checked Then
            If cnt = 0 Or cnt Mod 2 = 0 Then
                Label3.Text = "白の番です"
            Else
                Label3.Text = "黒の番です"
            End If
        Else
            If cnt = 0 Or cnt Mod 2 = 0 Then
                Label3.Text = "黒の番です"
            Else
                Label3.Text = "白の番です"
            End If
        End If
    End Sub
    Sub judge(ByVal x As Integer, ByVal y As Integer)
        If fieldstate = False Then
        Else
            If x + y = 64 Then
                If x > y Then
                    MsgBox("黒の勝ちです")
                ElseIf x < y Then
                    MsgBox("白の勝ちです")
                Else
                    MsgBox("引き分けです")
                End If
            ElseIf x = 0 Then
                MsgBox("白の勝ちです")
            ElseIf y = 0 Then
                MsgBox("黒の勝ちです")
            End If
        End If
    End Sub


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Button3.Enabled = False
    
    End Sub
End Class

