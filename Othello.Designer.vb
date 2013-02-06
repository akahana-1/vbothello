<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GamePicture = New System.Windows.Forms.PictureBox()
        Me.BlackButton = New System.Windows.Forms.Button()
        Me.BlackNumLabel = New System.Windows.Forms.Label()
        Me.ResetButton = New System.Windows.Forms.Button()
        Me.WhiteNumLabel = New System.Windows.Forms.Label()
        Me.PassButton = New System.Windows.Forms.Button()
        Me.WhiteButton = New System.Windows.Forms.Button()
        Me.TurnLabel = New System.Windows.Forms.Label()
        CType(Me.GamePicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GamePicture
        '
        Me.GamePicture.BackColor = System.Drawing.Color.DarkGreen
        Me.GamePicture.Location = New System.Drawing.Point(0, 0)
        Me.GamePicture.Margin = New System.Windows.Forms.Padding(0)
        Me.GamePicture.Name = "GamePicture"
        Me.GamePicture.Size = New System.Drawing.Size(401, 401)
        Me.GamePicture.TabIndex = 0
        Me.GamePicture.TabStop = False
        '
        'BlackButton
        '
        Me.BlackButton.Location = New System.Drawing.Point(409, 5)
        Me.BlackButton.Name = "BlackButton"
        Me.BlackButton.Size = New System.Drawing.Size(80, 30)
        Me.BlackButton.TabIndex = 1
        Me.BlackButton.Text = "黒スタート"
        Me.BlackButton.UseVisualStyleBackColor = True
        '
        'BlackNumLabel
        '
        Me.BlackNumLabel.AutoSize = True
        Me.BlackNumLabel.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BlackNumLabel.Location = New System.Drawing.Point(406, 314)
        Me.BlackNumLabel.Name = "BlackNumLabel"
        Me.BlackNumLabel.Size = New System.Drawing.Size(0, 13)
        Me.BlackNumLabel.TabIndex = 2
        '
        'ResetButton
        '
        Me.ResetButton.Location = New System.Drawing.Point(409, 77)
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(80, 30)
        Me.ResetButton.TabIndex = 4
        Me.ResetButton.Text = "リセット"
        Me.ResetButton.UseVisualStyleBackColor = True
        '
        'WhiteNumLabel
        '
        Me.WhiteNumLabel.AutoSize = True
        Me.WhiteNumLabel.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.WhiteNumLabel.Location = New System.Drawing.Point(406, 361)
        Me.WhiteNumLabel.Name = "WhiteNumLabel"
        Me.WhiteNumLabel.Size = New System.Drawing.Size(0, 13)
        Me.WhiteNumLabel.TabIndex = 5
        '
        'PassButton
        '
        Me.PassButton.Location = New System.Drawing.Point(409, 115)
        Me.PassButton.Name = "PassButton"
        Me.PassButton.Size = New System.Drawing.Size(80, 30)
        Me.PassButton.TabIndex = 6
        Me.PassButton.Text = "パス"
        Me.PassButton.UseVisualStyleBackColor = True
        '
        'WhiteButton
        '
        Me.WhiteButton.Location = New System.Drawing.Point(409, 41)
        Me.WhiteButton.Name = "WhiteButton"
        Me.WhiteButton.Size = New System.Drawing.Size(80, 30)
        Me.WhiteButton.TabIndex = 7
        Me.WhiteButton.Text = "白スタート"
        Me.WhiteButton.UseVisualStyleBackColor = True
        '
        'TurnLabel
        '
        Me.TurnLabel.AutoSize = True
        Me.TurnLabel.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TurnLabel.ForeColor = System.Drawing.Color.Black
        Me.TurnLabel.Location = New System.Drawing.Point(406, 160)
        Me.TurnLabel.Name = "TurnLabel"
        Me.TurnLabel.Size = New System.Drawing.Size(0, 13)
        Me.TurnLabel.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 412)
        Me.Controls.Add(Me.TurnLabel)
        Me.Controls.Add(Me.WhiteButton)
        Me.Controls.Add(Me.PassButton)
        Me.Controls.Add(Me.WhiteNumLabel)
        Me.Controls.Add(Me.ResetButton)
        Me.Controls.Add(Me.BlackNumLabel)
        Me.Controls.Add(Me.BlackButton)
        Me.Controls.Add(Me.GamePicture)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "オセロ"
        CType(Me.GamePicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GamePicture As System.Windows.Forms.PictureBox
    Friend WithEvents BlackButton As System.Windows.Forms.Button
    Friend WithEvents BlackNumLabel As System.Windows.Forms.Label
    Friend WithEvents ResetButton As System.Windows.Forms.Button
    Friend WithEvents WhiteNumLabel As System.Windows.Forms.Label
    Friend WithEvents PassButton As System.Windows.Forms.Button
    Friend WithEvents WhiteButton As System.Windows.Forms.Button
    Friend WithEvents TurnLabel As System.Windows.Forms.Label

End Class
