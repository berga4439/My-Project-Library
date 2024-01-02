<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gamerWin
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
        Me.components = New System.ComponentModel.Container()
        Me.collisionDetection = New System.Windows.Forms.Timer(Me.components)
        Me.pCharacter = New System.Windows.Forms.PictureBox()
        Me.gameTimer = New System.Windows.Forms.Timer(Me.components)
        Me.scoreLabel = New System.Windows.Forms.Label()
        Me.hpLabel = New System.Windows.Forms.Label()
        Me.rangeLabel = New System.Windows.Forms.Label()
        Me.damageLabel = New System.Windows.Forms.Label()
        Me.hBox = New System.Windows.Forms.PictureBox()
        Me.dBox = New System.Windows.Forms.PictureBox()
        Me.rBox = New System.Windows.Forms.PictureBox()
        Me.eHp = New System.Windows.Forms.Label()
        Me.eMph = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.finalLabel = New System.Windows.Forms.Label()
        CType(Me.pCharacter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.hBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'collisionDetection
        '
        Me.collisionDetection.Interval = 1
        '
        'pCharacter
        '
        Me.pCharacter.BackColor = System.Drawing.Color.Blue
        Me.pCharacter.Location = New System.Drawing.Point(0, 0)
        Me.pCharacter.Name = "pCharacter"
        Me.pCharacter.Size = New System.Drawing.Size(15, 15)
        Me.pCharacter.TabIndex = 0
        Me.pCharacter.TabStop = False
        '
        'gameTimer
        '
        Me.gameTimer.Interval = 1000
        '
        'scoreLabel
        '
        Me.scoreLabel.AutoSize = True
        Me.scoreLabel.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.scoreLabel.Location = New System.Drawing.Point(616, 93)
        Me.scoreLabel.Name = "scoreLabel"
        Me.scoreLabel.Size = New System.Drawing.Size(79, 28)
        Me.scoreLabel.TabIndex = 1
        Me.scoreLabel.Text = "Score:"
        '
        'hpLabel
        '
        Me.hpLabel.AutoSize = True
        Me.hpLabel.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hpLabel.Location = New System.Drawing.Point(616, 9)
        Me.hpLabel.Name = "hpLabel"
        Me.hpLabel.Size = New System.Drawing.Size(55, 28)
        Me.hpLabel.TabIndex = 2
        Me.hpLabel.Text = "Hp: "
        '
        'rangeLabel
        '
        Me.rangeLabel.AutoSize = True
        Me.rangeLabel.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rangeLabel.Location = New System.Drawing.Point(616, 37)
        Me.rangeLabel.Name = "rangeLabel"
        Me.rangeLabel.Size = New System.Drawing.Size(90, 28)
        Me.rangeLabel.TabIndex = 4
        Me.rangeLabel.Text = "Range: "
        '
        'damageLabel
        '
        Me.damageLabel.AutoSize = True
        Me.damageLabel.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.damageLabel.Location = New System.Drawing.Point(616, 65)
        Me.damageLabel.Name = "damageLabel"
        Me.damageLabel.Size = New System.Drawing.Size(80, 28)
        Me.damageLabel.TabIndex = 5
        Me.damageLabel.Text = "Dmg: "
        '
        'hBox
        '
        Me.hBox.BackColor = System.Drawing.Color.Violet
        Me.hBox.Location = New System.Drawing.Point(0, 0)
        Me.hBox.Name = "hBox"
        Me.hBox.Size = New System.Drawing.Size(15, 15)
        Me.hBox.TabIndex = 6
        Me.hBox.TabStop = False
        '
        'dBox
        '
        Me.dBox.BackColor = System.Drawing.Color.Purple
        Me.dBox.Location = New System.Drawing.Point(0, 0)
        Me.dBox.Name = "dBox"
        Me.dBox.Size = New System.Drawing.Size(15, 15)
        Me.dBox.TabIndex = 7
        Me.dBox.TabStop = False
        '
        'rBox
        '
        Me.rBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.rBox.Location = New System.Drawing.Point(0, 0)
        Me.rBox.Name = "rBox"
        Me.rBox.Size = New System.Drawing.Size(15, 15)
        Me.rBox.TabIndex = 8
        Me.rBox.TabStop = False
        '
        'eHp
        '
        Me.eHp.AutoSize = True
        Me.eHp.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eHp.Location = New System.Drawing.Point(616, 427)
        Me.eHp.Name = "eHp"
        Me.eHp.Size = New System.Drawing.Size(47, 28)
        Me.eHp.TabIndex = 9
        Me.eHp.Text = "Hp:"
        '
        'eMph
        '
        Me.eMph.AutoSize = True
        Me.eMph.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eMph.Location = New System.Drawing.Point(616, 455)
        Me.eMph.Name = "eMph"
        Me.eMph.Size = New System.Drawing.Size(80, 28)
        Me.eMph.TabIndex = 10
        Me.eMph.Text = "Speed:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Matura MT Script Capitals", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(616, 399)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(142, 28)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Enemy Info:"
        '
        'finalLabel
        '
        Me.finalLabel.AutoSize = True
        Me.finalLabel.Font = New System.Drawing.Font("Matura MT Script Capitals", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.finalLabel.Location = New System.Drawing.Point(185, 244)
        Me.finalLabel.Name = "finalLabel"
        Me.finalLabel.Size = New System.Drawing.Size(0, 50)
        Me.finalLabel.TabIndex = 12
        '
        'gamerWin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 600)
        Me.Controls.Add(Me.finalLabel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.eMph)
        Me.Controls.Add(Me.eHp)
        Me.Controls.Add(Me.rBox)
        Me.Controls.Add(Me.dBox)
        Me.Controls.Add(Me.hBox)
        Me.Controls.Add(Me.damageLabel)
        Me.Controls.Add(Me.rangeLabel)
        Me.Controls.Add(Me.hpLabel)
        Me.Controls.Add(Me.scoreLabel)
        Me.Controls.Add(Me.pCharacter)
        Me.Name = "gamerWin"
        Me.Text = "Dungeon Arena"
        CType(Me.pCharacter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.hBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents collisionDetection As Timer
    Friend WithEvents pCharacter As PictureBox
    Friend WithEvents gameTimer As Timer
    Friend WithEvents scoreLabel As Label
    Friend WithEvents hpLabel As Label
    Friend WithEvents rangeLabel As Label
    Friend WithEvents damageLabel As Label
    Friend WithEvents hBox As PictureBox
    Friend WithEvents dBox As PictureBox
    Friend WithEvents rBox As PictureBox
    Friend WithEvents eHp As Label
    Friend WithEvents eMph As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents finalLabel As Label
End Class
