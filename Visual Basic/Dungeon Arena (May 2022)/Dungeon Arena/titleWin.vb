Public Class Menu
    Private Sub playButton_Click(sender As Object, e As EventArgs) Handles playButton.Click
        gamerWin.Show()
        gamerWin.Visible = True
        Me.Visible = False
    End Sub

    Private Sub helpButton_Click(sender As Object, e As EventArgs) Handles helpButton.Click
        helpWindow.Show()
        helpWindow.Visible = True
        Me.Hide()
    End Sub

    Private Sub aboutButton_Click(sender As Object, e As EventArgs) Handles aboutButton.Click
        aboutWindow.Show()
        aboutWindow.Visible = True
        Me.Hide()

    End Sub
End Class
