Public Class ChooseLimiters
    Sub New(ByVal TROPHY_L As List(Of List(Of String)), ByVal PLAQUE_L As List(Of List(Of String)), ByVal TotalTrophies As Integer, ByVal TotalPlaque As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        TROPHY_LIMITER = TROPHY_L
        PLAQUE_LIMITER = PLAQUE_L
        Me.TotalTrophies = TotalTrophies
        Me.TotalPlaque = TotalPlaque

        AddCheckBoxes()
        FillArrays()

        AddAllPreviousSheets(1)
        AddAllPreviousSheets(2)
    End Sub

    Dim TrophyEnabled As Boolean
    Dim TrophyNumberSheets As Integer
    Dim PlaqueNumberSheets As Integer
    Dim TotalTrophies As Integer
    Dim TotalPlaque As Integer

    Dim TROPHY_LIMITER As List(Of List(Of String))
    Dim PLAQUE_LIMITER As List(Of List(Of String))

    Private CheckBoxList As List(Of CheckBox)
    Private Columns As List(Of List(Of CheckBox))

    Private Sub ChooseLimiters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NoTrophy.Text = Me.TotalTrophies.ToString
    End Sub

    Private Sub SetAllCheckBoxesAsDisabled()
        For Each I As CheckBox In CheckBoxList
            If Not I.Checked Then
                I.Enabled = False
            End If
        Next
    End Sub

    Public Function GetCurrentlySelected() As Integer
        Dim ret = 0
        For Each i As CheckBox In CheckBoxList
            If i.Checked Then
                ret += 1
            End If
        Next
        Return ret
    End Function

    Public Sub ProcessCheckBox(IntArray As Integer())
        If AllChecked(IntArray) = True Or TrophyEnabled = False Then
            For Each I As Integer In IntArray
                CheckBoxList(I).Checked = False
            Next
        Else
            Dim Before = AddAllPreviousSheets(Integer.Parse(SheetDropBox.Text))
            Dim CurrentCounter = GetCurrentlySelected()

            For Each i As Integer In IntArray
                If Before + CurrentCounter >= TotalTrophies Then
                    Exit Sub
                Else
                    If Not CheckBoxList(i).Checked Then
                        CurrentCounter += 1
                        CheckBoxList(i).Checked = True
                    End If
                End If


            Next
        End If
    End Sub

    Private Function AddAllPreviousSheets(Upto As Integer) As Integer
        Dim ret = 0
        If Upto = 1 Then ' first sheet, no prev sheets
            Return 0
        ElseIf Upto < 1 Then
            MsgBox("WHAT DA HELL? the sheet should not be less than 1....")
            Close()
        Else
            For i = 0 To Upto - 2
                ret += TROPHY_LIMITER(i).Count
            Next

        End If
        Return ret
    End Function


    Private Function AllChecked(IntArray As Integer()) As Boolean
        For Each i As Integer In IntArray
            If Not CheckBoxList(i).Checked Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub AddCheckBoxes()
        CheckBoxList = New List(Of CheckBox)
        CheckBoxList.Add(Trophy0)
        CheckBoxList.Add(Trophy1)
        CheckBoxList.Add(Trophy2)
        CheckBoxList.Add(Trophy3)
        CheckBoxList.Add(Trophy4)
        CheckBoxList.Add(Trophy5)
        CheckBoxList.Add(Trophy6)
        CheckBoxList.Add(Trophy7)
        CheckBoxList.Add(Trophy8)
        CheckBoxList.Add(Trophy9)

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
        ProcessCheckBox(New Integer() {7, 6, 5, 4})

        'If Trophy4.Checked AndAlso Trophy5.Checked AndAlso Trophy6.Checked AndAlso Trophy7.Checked Then
        '    Trophy4.Checked = False
        '    Trophy5.Checked = False
        '    Trophy6.Checked = False
        '    Trophy7.Checked = False
        'Else
        '    Trophy4.Checked = True
        '    Trophy5.Checked = True
        '    Trophy6.Checked = True
        '    Trophy7.Checked = True
        'End If


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


    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButtonTrophy.Click
        Close()
    End Sub

    Private TrophyCount As Integer
    Private PlaqueCount As Integer
    Public Function EnumerateFromZeroToInt(upto As Integer, Optional skip As List(Of Integer) = Nothing) As List(Of String)
        If skip Is Nothing Then
            skip = New List(Of Integer)
        End If
        Dim retlist As New List(Of String)
        For i As Integer = 0 To upto
            If Not skip.Contains(i) Then
                retlist.Add(i.ToString)
            End If
        Next

        Return retlist
    End Function

    Private Function GetAllocTrophyCount(Optional Upto As Integer = Nothing)
        Dim ret = 0
        If Upto = Nothing Then
            Upto = TROPHY_LIMITER.Count() - 1
        End If
        For i = 0 To Upto
            ret += TROPHY_LIMITER(i).Count
        Next
        Return ret
    End Function

    Private Sub FillTrophyArray()
        Dim AccountedTrophies = GetAllocTrophyCount()
        If AccountedTrophies < TotalTrophies Then
            While AccountedTrophies < TotalTrophies
                If (TotalTrophies - AccountedTrophies) < 10 Then
                    ' almost done!
                    MsgBox((TotalTrophies - AccountedTrophies).ToString)
                    TROPHY_LIMITER.Add(EnumerateFromZeroToInt(TotalTrophies - AccountedTrophies - 1))
                    AccountedTrophies += TotalTrophies - AccountedTrophies
                Else
                    TROPHY_LIMITER.Add(EnumerateFromZeroToInt(9))
                    AccountedTrophies += 10
                End If
            End While
            For Each tl As List(Of String) In TROPHY_LIMITER
                MsgBox(String.Join("|EachLine|", tl))
            Next
            NumberSheet.Text = TROPHY_LIMITER.Count()
        End If
    End Sub

    Private Sub FillPlaqueArray()

    End Sub


    Public Sub FillArrays()
        FillTrophyArray()
        FillPlaqueArray()
        RefreshTrophyDropBox()
        RefreshTrophyUI()
        RefreshPlaqueUI()
    End Sub

    Private Sub TrophyPage_Click(sender As Object, e As EventArgs) Handles TrophyPage.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FillTrophyArray()
    End Sub


    Public Sub RefreshTrophyDropBox()
        SheetDropBox.Items.Clear()
        If TROPHY_LIMITER.Count > 0 Then
            For i As Integer = 1 To TROPHY_LIMITER.Count
                SheetDropBox.Items.Add(i.ToString)
            Next
        Else
            MsgBox("no sheets? quitting")
            Close()
        End If
        SheetDropBox.SelectedIndex = 0
    End Sub

    Private Sub RefreshTrophyUI()
        Dim currentlist As List(Of String) = TROPHY_LIMITER(Integer.Parse(SheetDropBox.SelectedItem) - 1)
        For Each check_box As CheckBox In CheckBoxList
            check_box.Checked = False
        Next
        For Each i As String In currentlist
            CheckBoxList(Integer.Parse(i)).Checked = True

        Next

    End Sub
    Private Sub RefreshPlaqueUI()

    End Sub

    Private Sub SaveToSheet_Click(sender As Object, e As EventArgs) Handles SaveToSheet.Click
        SaveToSheetWrapper()
    End Sub
    Private Sub SaveToSheetWrapper()
        TROPHY_LIMITER(Integer.Parse(SheetDropBox.SelectedItem)) = GetSelectedItems()
        FillArrays()
    End Sub

    Public Function GetSelectedItems() As List(Of String)
        Dim retList As New List(Of String)
        For i As Integer = 0 To 9
            If CheckBoxList(i).Checked Then
                retList.Add(i.ToString)
            End If
        Next
        Return retList
    End Function

    Private Sub DisableAll()
        TrophyEnabled = False
        For Each i As CheckBox In CheckBoxList
            If Not i.Checked Then
                i.Enabled = False
            End If
        Next
    End Sub
    Private Sub EnableAll()
        TrophyEnabled = True
        For Each i As CheckBox In CheckBoxList
            i.Enabled = True
        Next
    End Sub

    Private Sub CheckIfAllFilled()
        If AddAllPreviousSheets(Integer.Parse(SheetDropBox.SelectedItem)) + 1 + GetCurrentlySelected() > TotalTrophies Then
            DisableAll()
        Else
            EnableAll()
        End If
    End Sub

    Private Sub TrophyButton_CheckedChanged(sender As Object, e As EventArgs) Handles Trophy0.CheckedChanged, Trophy1.CheckedChanged, Trophy2.CheckedChanged, Trophy3.CheckedChanged,
            Trophy4.CheckedChanged, Trophy5.CheckedChanged, Trophy6.CheckedChanged, Trophy7.CheckedChanged, Trophy8.CheckedChanged, Trophy9.CheckedChanged
        CheckIfAllFilled()

    End Sub
End Class