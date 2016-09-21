Public Class AddForm
    Private Sub AddToRow_Click(sender As Object, e As EventArgs) Handles AddToRow.Click
        Dim YearInt As Integer

        If NameTextBox.Text <> String.Empty And YearTextBox.Text <> String.Empty Then
            If Integer.TryParse(YearTextBox.Text, YearInt) <> Nothing Then
                ' valid int
                If Trophy_BUTTON.Checked = True Then
                    AwardGenerator.AddRowTable("SCHOOL_TROPHY", NameTextBox.Text, YearTextBox.Text)
                Else
                    AwardGenerator.AddRowTable("SCHOOL_PLAQUE", NameTextBox.Text, YearTextBox.Text)
                End If
                Close()
            Else
                MsgBox("Please Enter a valid Int")
            End If
        Else
            MsgBox("Please input values in both Name and Year")
        End If

    End Sub

    Private Sub YearTextBox_TextChanged(sender As Object, e As EventArgs) Handles YearTextBox.TextChanged

    End Sub
    Private Sub CancelFromRow_Click(sender As Object, e As EventArgs) Handles CancelFromRow.Click
        Close()
    End Sub

    Private Sub AddForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class