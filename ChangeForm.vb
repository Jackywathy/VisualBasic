Public Class ChangeForm
    Private Sub ChangeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Dim RowInteger As Integer
    Public Sub FillForm(RowInteger As Integer, AwardType As String, Name As String, Year As String)
        NameTextBox.Text = Name
        YearTextBox.Text = Year
        Me.RowInteger = RowInteger
        If AwardType = "SCHOOL_TROPHY" Then
            Trophy_BUTTON.Checked = True
            Plaque_BUTTON.Checked = False
        ElseIf AwardType = "SCHOOL_PLAQUE" Then
            Trophy_BUTTON.Checked = False
            Plaque_BUTTON.Checked = True
        End If
    End Sub

    Private Sub AddToRow_Click(sender As Object, e As EventArgs) Handles AddToRow.Click
        Dim CurrentType As String
        If Trophy_BUTTON.Checked Then
            CurrentType = "SCHOOL_TROPHY"
        ElseIf Plaque_BUTTON.Checked Then
            CurrentType = "SCHOOL_PLAQUE"
        Else
            Throw New System.Exception("type is not eqyal to trophy or plaqye?")
        End If
        AwardGenerator.ChangeRowTable(RowInteger, CurrentType, NameTextBox.Text, YearTextBox.Text)
        Close()
    End Sub

    Private Sub CancelFromRow_Click(sender As Object, e As EventArgs) Handles CancelFromRow.Click
        Close()
    End Sub
End Class