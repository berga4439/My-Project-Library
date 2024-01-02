Imports System.Drawing.Drawing2D
Imports System.Drawing
Public Class gamerWin
    Dim tileSize As Integer = 15
    Dim gridSize As Integer = 40
    Dim steps As Integer = 800
    Dim tiles((gridSize * gridSize) - 1) As Tile
    Dim rand As New Random()
    Dim g As Graphics
    Dim p As New Player(300, 300)
    Dim enemies As New List(Of Enemy)
    Dim enemyStep As Integer = 0
    Dim maxEnemy As Integer = 3
    Dim spawnable As New List(Of Tile)
    Dim enemyDifficulty As Integer = 1


    Private Sub gamerWin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        collisionDetection.Interval = 1
        collisionDetection.Enabled = True
        gameTimer.Enabled = True

        Dim c As Integer = 0
        For y As Integer = 0 To gridSize - 1
            For x As Integer = 0 To gridSize - 1
                tiles(c) = New Tile(x * tileSize, y * tileSize, 0)
                c += 1
            Next
        Next


        Dim selectedX As Integer = (gridSize * tileSize) \ 2
        Dim selectedY As Integer = (gridSize * tileSize) \ 2
        For s As Integer = 0 To steps
            For i As Integer = 0 To tiles.Length - 1
                If selectedX = tiles(i).xCoord And selectedY = tiles(i).yCoord Then
                    tiles(i).clr = 1
                End If
            Next
            If rand.Next(0, 2) = 0 Then
                If rand.Next(0, 2) = 0 Then
                    selectedX += tileSize
                Else
                    selectedX -= tileSize
                End If
            Else
                If rand.Next(0, 2) = 0 Then
                    selectedY += tileSize
                Else
                    selectedY -= tileSize
                End If
            End If
            If selectedX < 0 Then
                selectedX += tileSize
            End If
            If selectedX > (gridSize * tileSize) Then
                selectedX -= tileSize
            End If
            If selectedY < 0 Then
                selectedY += tileSize
            End If
            If selectedY > (gridSize * tileSize) Then
                selectedY -= tileSize
            End If

        Next


        For i As Integer = 0 To maxEnemy
            enemies.Add(New Enemy(0, (gridSize + 1) * tileSize))
            enemies(i).generate()
            Me.Controls.Add(enemies(i).dispEnemy)
        Next


        For Each tile As Tile In tiles
            If tile.clr = 1 Then
                spawnable.Add(tile)
            End If
        Next
        For i As Integer = 0 To maxEnemy
            Dim x As Integer = rand.Next(0, spawnable.Count - 1)
            enemies(i).x = spawnable(x).xCoord
            enemies(i).y = spawnable(x).yCoord
            enemies(i).oldx = enemies(i).x
            enemies(i).oldy = enemies(i).y
            enemies(i).isAlive = True
        Next


        g = CreateGraphics()
    End Sub
    Private Sub closedGame(sender As Object, e As EventArgs) Handles MyBase.Closed
        Application.Exit()
    End Sub

    Public Class Tile
        Public xCoord As Integer
        Public yCoord As Integer
        Public clr As Integer

        Public Sub New(x As Integer, y As Integer, col As Integer)
            xCoord = x
            yCoord = y
            clr = col

        End Sub

    End Class
    Public Class Player
        Public x
        Public y
        Public size = 15
        Public oldx As Integer
        Public oldy As Integer
        Public hp As Integer = 5
        Public range As Integer = 1
        Public dmg As Integer = 1

        Public Sub New(px As Integer, py As Integer)
            x = px
            y = py
            oldx = px
            oldy = py
        End Sub
    End Class
    Public Class Enemy
        Public x
        Public y
        Public oldx
        Public oldy
        Public dispEnemy As New PictureBox
        Public health = 1
        Public isAlive As Boolean = False
        Public Sub New(px As Integer, py As Integer)
            x = px
            y = py
            oldx = x
            oldy = y
        End Sub
        Public Sub generate()
            dispEnemy.Location = New Point(x, y)
            dispEnemy.Size = New Size(15, 15)
            dispEnemy.BackColor = Color.Red
            dispEnemy.Visible = True
        End Sub

    End Class
    Public Class upgrade
        Public x
        Public y
        Public type
        Public isDeployed = False
        Public Sub New(dx, dy, t)
            x = dx
            y = dy
            type = t
        End Sub
    End Class

    Private Sub DrawGame(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Drawback()
    End Sub
    Private Sub Drawback()
        For Each t As Tile In tiles
            If t.clr = 0 Then
                g.FillRectangle(Brushes.Gray, New Rectangle(New Point(t.xCoord, t.yCoord), New Size(tileSize, tileSize)))
            Else
                g.FillRectangle(Brushes.LightGreen, New Rectangle(New Point(t.xCoord, t.yCoord), New Size(tileSize, tileSize)))
            End If
        Next
    End Sub

    Dim upDist As Double = 0
    Dim downDist As Double = 0
    Dim leftDist As Double = 0
    Dim rightDist As Double = 0
    Dim min As Double = 99999
    Dim flag As Integer = 0
    Dim enemiesDefeated As Integer = 0
    Dim healthBox As New upgrade(1000, 1000, 0)
    Dim upgradeDmgBox As New upgrade(1000, 1000, 1)
    Dim upgradeRangeBox As New upgrade(1000, 1000, 2)
    Dim eSpeed As Integer = 25
    Private Sub CollisionDetection_Tick(sender As Object, e As EventArgs) Handles collisionDetection.Tick
        If enemyStep > eSpeed Then
            For Each enemy As Enemy In enemies
                If enemy.isAlive Then
                    upDist = Math.Sqrt(((p.x - enemy.x) ^ 2) + ((p.y - (enemy.y - tileSize)) ^ 2))
                    downDist = Math.Sqrt(((p.x - enemy.x) ^ 2) + ((p.y - (enemy.y + tileSize)) ^ 2))
                    leftDist = Math.Sqrt(((p.x - (enemy.x - tileSize)) ^ 2) + ((p.y - enemy.y) ^ 2))
                    rightDist = Math.Sqrt(((p.x - (enemy.x + tileSize)) ^ 2) + ((p.y - enemy.y) ^ 2))
                    If upDist < min Then
                        min = upDist
                        flag = 0
                    End If
                    If downDist < min Then
                        min = downDist
                        flag = 1
                    End If
                    If leftDist < min Then
                        min = leftDist
                        flag = 2
                    End If
                    If rightDist < min Then
                        min = rightDist
                        flag = 3
                    End If
                    If flag = 0 Then
                        enemy.oldx = enemy.x
                        enemy.oldy = enemy.y
                        enemy.y -= tileSize
                    ElseIf flag = 1 Then
                        enemy.oldx = enemy.x
                        enemy.oldy = enemy.y
                        enemy.y += tileSize
                    ElseIf flag = 2 Then
                        enemy.oldx = enemy.x
                        enemy.oldy = enemy.y
                        enemy.x -= tileSize
                    ElseIf flag = 3 Then
                        enemy.oldx = enemy.x
                        enemy.oldy = enemy.y
                        enemy.x += tileSize
                    End If
                    For Each t As Tile In tiles
                        If enemy.y = t.yCoord And enemy.x = t.xCoord Then
                            If t.clr = 0 Then
                                enemy.x = enemy.oldx
                                enemy.y = enemy.oldy
                            End If
                        End If
                    Next
                    Dim enemCount As Integer = 0
                    For Each i As Enemy In enemies
                        If enemy.x = i.x And enemy.y = i.y Then
                            enemCount += 1
                        End If
                    Next
                    If enemCount > 1 Then
                        enemy.x = enemy.oldx
                        enemy.y = enemy.oldy
                    End If
                    If enemy.x = p.x And enemy.y = p.y Then
                        p.hp -= 1
                    End If
                    min = 99999
                    enemy.dispEnemy.Location = New Point(enemy.x, enemy.y)
                End If

            Next
            enemyStep = 0
        Else
            enemyStep += 1
        End If
        If p.hp <= 0 Then
            pCharacter.Visible = False
            collisionDetection.Stop()
            gameTimer.Stop()
            finalLabel.Text = "Game Over!" & vbCrLf & "Final Score: " & score & vbCrLf & "Enemies defeated: " & enemiesDefeated
        End If
        If p.hp < 4 Then
            hpLabel.ForeColor = Color.Red
        Else
            hpLabel.ForeColor = Color.Black
        End If
        hpLabel.Text = "Hp: " & p.hp
        rangeLabel.Text = "Range: " & p.range
        damageLabel.Text = "Dmg: " & p.dmg
        eHp.Text = "Hp: " & enemyDifficulty
        eMph.Text = "Speed: " & (30 - eSpeed)
        pCharacter.Location = New Point(p.x, p.y)
        hBox.Location = New Point(healthBox.x, healthBox.y)
        dBox.Location = New Point(upgradeDmgBox.x, upgradeDmgBox.y)
        rBox.Location = New Point(upgradeRangeBox.x, upgradeRangeBox.y)
        For Each enemy In enemies
            If enemy.isAlive = False Then
                If rand.NextDouble() > 0.95 Then
                    respawn(enemy)
                End If
            End If
        Next
        If p.x = healthBox.x And p.y = healthBox.y Then
            healthBox.x = 1000
            healthBox.y = 1000
            healthBox.isDeployed = False
            p.hp += 1
        ElseIf p.x = upgradeDmgBox.x And p.y = upgradeDmgBox.y Then
            upgradeDmgBox.x = 1000
            upgradeDmgBox.y = 1000
            upgradeDmgBox.isDeployed = False
            p.dmg += 1
        ElseIf p.x = upgradeRangeBox.x And p.y = upgradeRangeBox.y Then
            upgradeRangeBox.x = 1000
            upgradeRangeBox.y = 1000
            upgradeRangeBox.isDeployed = False
            p.range += 1
        End If



    End Sub

    Private Sub gamerWin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.W
                p.oldy = p.y
                p.oldx = p.x
                p.y -= 15
            Case Keys.S
                p.oldy = p.y
                p.oldx = p.x
                p.y += 15
            Case Keys.A
                p.oldx = p.x
                p.oldy = p.y
                p.x -= 15
            Case Keys.D
                p.oldx = p.x
                p.oldy = p.y
                p.x += 15
            Case Keys.Space
                For Each i As Enemy In enemies
                    If (i.x >= p.x - (tileSize * p.range) And i.x <= p.x And i.y = p.y) Or (i.x <= p.x + (tileSize * p.range) And i.x >= p.x And i.y = p.y) Or (i.y <= p.y + (tileSize * p.range) And i.y >= p.y And i.x = p.x) Or (i.y >= p.y - (tileSize * p.range) And i.y <= p.y And i.x = p.x) Then
                        i.health -= p.dmg
                        If i.health <= 0 Then
                            i.isAlive = False
                            i.oldx = 0
                            i.oldy = (gridSize + 1) * tileSize
                            i.x = 0
                            i.y = (gridSize + 1) * tileSize
                            i.dispEnemy.Location = New Point(i.x, i.y)
                            enemiesDefeated += 1
                            score += enemyDifficulty * 10 * scoreMultiplier
                        End If
                    End If
                Next
        End Select
        For Each t As Tile In tiles
            If p.y = t.yCoord And p.x = t.xCoord Then
                If t.clr = 0 Then
                    p.x = p.oldx
                    p.y = p.oldy
                End If
            End If
        Next
        If p.x < 0 Then
            p.x = p.oldx
        End If
        If p.x > ((gridSize - 1) * tileSize) Then
            p.x = p.oldx
        End If
        If p.y < 0 Then
            p.y = p.oldy
        End If
        If p.y > ((gridSize - 1) * tileSize) Then
            p.y = p.oldy
        End If
    End Sub
    Dim tickPassed As Integer = 0
    Dim scoreMultiplier As Decimal = 0.4

    Private Sub respawn(enemy As Enemy)
        Dim x As Integer = rand.Next(0, spawnable.Count - 1)
        enemy.x = spawnable(x).xCoord
        enemy.y = spawnable(x).yCoord
        enemy.oldx = enemy.x
        enemy.oldy = enemy.y
        enemy.health = enemyDifficulty
        enemy.isAlive = True
    End Sub

    Dim score As Decimal = 0
    Dim loc As Tile

    Private Sub gameTimer_Tick(sender As Object, e As EventArgs) Handles gameTimer.Tick
        tickPassed += 1
        score += scoreMultiplier
        scoreLabel.Text = "Score: " & score
        If tickPassed Mod 30 = 0 Then
            scoreMultiplier += 0.2
            enemyDifficulty += 1
        End If
        If tickPassed Mod 20 = 0 Then
            If eSpeed > 5 Then
                eSpeed -= 1
            End If
        End If

        If tickPassed Mod 15 = 0 Then
            loc = spawnable(rand.Next(0, spawnable.Count))
            Select Case rand.Next(0, 3)
                Case 0
                    If healthBox.isDeployed = False Then
                        healthBox.x = loc.xCoord
                        healthBox.y = loc.yCoord
                        healthBox.isDeployed = True
                    End If
                Case 1
                    If upgradeDmgBox.isDeployed = False Then
                        upgradeDmgBox.x = loc.xCoord
                        upgradeDmgBox.y = loc.yCoord
                        upgradeDmgBox.isDeployed = True
                    End If

                Case 2
                    If upgradeRangeBox.isDeployed = False Then
                        upgradeRangeBox.x = loc.xCoord
                        upgradeRangeBox.y = loc.yCoord
                        upgradeRangeBox.isDeployed = True
                    End If

            End Select
        End If
    End Sub
End Class