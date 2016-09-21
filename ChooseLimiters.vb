Public Class ChooseLimiters
    Private Sub ChooseLimiters_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles TrophyOutline.Click

    End Sub

    Private Sub TrophyCol3_Click(sender As Object, e As EventArgs) Handles TrophyCol3.Click
        If Trophy9.Checked And Trophy8.Checked Then
            Trophy9.Checked = False
            Trophy8.Checked = False
        Else
            Trophy9.Checked = True
            Trophy8.Checked = True
        End If
    End Sub

    Private Sub TrophyCol2_Click(sender As Object, e As EventArgs) Handles TrophyCol2.Click
        If Trophy4.Checked And Trophy5.Checked And Trophy6.Checked And Trophy7.Checked Then
            Trophy4.Checked = False
            Trophy5.Checked = False
            Trophy6.Checked = False
            Trophy7.Checked = False
        Else
            Trophy4.Checked = True
            Trophy5.Checked = True
            Trophy6.Checked = True
            Trophy7.Checked = True
        End If


    End Sub

    Private Sub TrophyCol1_Click(sender As Object, e As EventArgs) Handles TrophyCol1.Click
        If Trophy0.Checked And Trophy1.Checked And Trophy2.Checked And Trophy3.Checked Then
            Trophy0.Checked = False
            Trophy1.Checked = False
            Trophy2.Checked = False
            Trophy3.Checked = False
        Else
            Trophy0.Checked = True
            Trophy1.Checked = True
            Trophy2.Checked = True
            Trophy3.Checked = True
        End If
    End Sub

    Private Sub TrophyRowLeft4_Click(sender As Object, e As EventArgs) Handles TrophyRowLeft4.Click
        If Trophy3.Checked And Trophy7.Checked Then
            Trophy3.Checked = False
            Trophy7.Checked = False
        Else
            Trophy3.Checked = True
            Trophy7.Checked = True
        End If
    End Sub

    Private Sub TrophyRowLeft3_Click(sender As Object, e As EventArgs) Handles TrophyRowLeft3.Click
        If Trophy2.Checked And Trophy6.Checked Then
            Trophy2.Checked = False
            Trophy6.Checked = False
        Else
            Trophy2.Checked = True
            Trophy6.Checked = True
        End If
    End Sub

    Private Sub TrophyRowLeft2_Click(sender As Object, e As EventArgs) Handles TrophyRowLeft2.Click
        If Trophy1.Checked And Trophy5.Checked Then
            Trophy1.Checked = False
            Trophy5.Checked = False
        Else
            Trophy1.Checked = True
            Trophy5.Checked = True
        End If
    End Sub

    Private Sub TrophyRowLeft1_Click(sender As Object, e As EventArgs) Handles TrophyRowLeft1.Click
        If Trophy0.Checked And Trophy4.Checked Then
            Trophy0.Checked = False
            Trophy4.Checked = False
        Else
            Trophy0.Checked = True
            Trophy4.Checked = True
        End If
    End Sub

    Private Sub TrophyRowRight1_Click(sender As Object, e As EventArgs) Handles TrophyRowRight1.Click
        If Trophy8.Checked And Trophy4.Checked And Trophy0.Checked Then
            Trophy8.Checked = False
            Trophy4.Checked = False
            Trophy0.Checked = False
        Else
            Trophy8.Checked = True
            Trophy4.Checked = True
            Trophy0.Checked = True
        End If
    End Sub

    Private Sub TrophyRowRight2_Click(sender As Object, e As EventArgs) Handles TrophyRowRight2.Click
        If Trophy8.Checked And Trophy5.Checked And Trophy1.Checked Then
            Trophy8.Checked = False
            Trophy5.Checked = False
            Trophy1.Checked = False
        Else
            Trophy8.Checked = True
            Trophy5.Checked = True
            Trophy1.Checked = True
        End If
    End Sub

    Private Sub TrophyRowRight3_Click(sender As Object, e As EventArgs) Handles TrophyRowRight3.Click
        If Trophy9.Checked And Trophy6.Checked And Trophy2.Checked Then
            Trophy9.Checked = False
            Trophy6.Checked = False
            Trophy2.Checked = False
        Else
            Trophy9.Checked = True
            Trophy6.Checked = True
            Trophy2.Checked = True
        End If
    End Sub

    Private Sub TrophyRowRight4_Click(sender As Object, e As EventArgs) Handles TrophyRowRight4.Click
        If Trophy9.Checked And Trophy7.Checked And Trophy3.Checked Then
            Trophy9.Checked = False
            Trophy7.Checked = False
            Trophy3.Checked = False
        Else
            Trophy9.Checked = True
            Trophy7.Checked = True
            Trophy3.Checked = True
        End If
    End Sub

    Public Sub End_Form()
        Close()
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButtonTrophy.Click
        Close()
    End Sub
End Class