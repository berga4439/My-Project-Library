<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Menu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.playButton = New System.Windows.Forms.Button()
        Me.helpButton = New System.Windows.Forms.Button()
        Me.aboutButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Matura MT Script Capitals", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(83, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 42)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dungeon Arena"
        '
        'playButton
        '
        Me.playButton.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.playButton.Location = New System.Drawing.Point(144, 109)
        Me.playButton.Name = "playButton"
        Me.playButton.Size = New System.Drawing.Size(145, 52)
        Me.playButton.TabIndex = 1
        Me.playButton.Text = "Play"
        Me.playButton.UseVisualStyleBackColor = True
        '
        'helpButton
        '
        Me.helpButton.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.helpButton.Location = New System.Drawing.Point(144, 167)
        Me.helpButton.Name = "helpButton"
        Me.helpButton.Size = New System.Drawing.Size(145, 52)
        Me.helpButton.TabIndex = 2
        Me.helpButton.Text = "Help"
        Me.helpButton.UseVisualStyleBackColor = True
        '
        'aboutButton
        '
        Me.aboutButton.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aboutButton.Location = New System.Drawing.Point(144, 225)
        Me.aboutButton.Name = "aboutButton"
        Me.aboutButton.Size = New System.Drawing.Size(145, 52)
        Me.aboutButton.TabIndex = 3
        Me.aboutButton.Text = "About"
        Me.aboutButton.UseVisualStyleBackColor = True
        '
        'Menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(434, 356)
        Me.Controls.Add(Me.aboutButton)
        Me.Controls.Add(Me.helpButton)
        Me.Controls.Add(Me.playButton)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Menu"
        Me.Text = "Dungeon Arena"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents playButton As Button
    Friend WithEvents helpButton As Button
    Friend WithEvents aboutButton As Button
End Class
