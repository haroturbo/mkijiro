﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class version
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CDEupdate = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CWCDATABASEEDITORMOD"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(71, 130)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(89, 14)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "GOOGLESVN"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 99)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "BUILD:2012/03/08 12:46"
        '
        'CDEupdate
        '
        Me.CDEupdate.Location = New System.Drawing.Point(74, 148)
        Me.CDEupdate.Margin = New System.Windows.Forms.Padding(4)
        Me.CDEupdate.Name = "CDEupdate"
        Me.CDEupdate.Size = New System.Drawing.Size(88, 27)
        Me.CDEupdate.TabIndex = 3
        Me.CDEupdate.Text = "CHECKVER"
        Me.CDEupdate.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(219, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Original SRC pasky,modded BY (ﾟ∀ﾟ)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(46, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 14)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "License GNU GPL v3"
        '
        'version
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(249, 178)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CDEupdate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "version"
        Me.Text = "バージョン情報"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CDEupdate As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
