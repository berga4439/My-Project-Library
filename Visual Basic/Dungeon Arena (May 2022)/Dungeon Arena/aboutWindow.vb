Public Class aboutWindow
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dungeon_Arena.Menu.Show()
        Dungeon_Arena.Menu.Visible = True
        Me.Visible = False
        Me.Hide()
    End Sub
    Private Sub closedGame(sender As Object, e As EventArgs) Handles MyBase.Closed
        Dungeon_Arena.Menu.Show()
        Dungeon_Arena.Menu.Visible = True
        Me.Visible = False
        Me.Hide()
    End Sub


End Class