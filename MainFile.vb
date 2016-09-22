Imports System.IO
Imports System.Reflection
Public Class AwardGenerator
    Public TROPHY_LIMITER As ArrayList
    Public PLAQUE_LIMITER As ArrayList
    Public EXE_PATH As String
    Public SaveType As String
    Public FilePath As String
    Public Shared OpenNewName As String = "backup.txt"
    Public Shared ConfigFolder As String = ".settings_awards"

    Public JustSaved As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataTable.DataMember = "AwardTable"
        JustSaved = True
        UpdateItems()
        PLAQUE_LIMITER = New ArrayList
        TROPHY_LIMITER = New ArrayList
        Create_Hidden(ConfigFolder)
        Extract_Exe()
        SaveType = "TXT"
        FilePath = ""
    End Sub

    Public Sub Create_Hidden(foldername As String)
        If Not Directory.Exists(foldername) Then
            Directory.CreateDirectory(foldername)
        End If

        Dim Dinfo As DirectoryInfo = New DirectoryInfo(foldername)
        If ((Dinfo.Attributes & FileAttributes.Hidden)) <> FileAttributes.Hidden Then
            Dinfo.Attributes = Dinfo.Attributes Or FileAttributes.Hidden
        End If
    End Sub

    Public Sub AddRowTable(Award As String, Name As String, Year As String)
        DataTable.Rows.Add(Award, Name, Year)
    End Sub



    Public Function IsValidRow(row As DataGridViewRow) As Boolean
        Dim Counter As Integer = 0
        For Each cell As DataGridViewCell In row.Cells
            If Counter = 3 Then
                Exit For
            End If
            If cell.Value = Nothing Then
                Return False
            End If
            Counter += 1
        Next
        Return True
    End Function

    Public Function CountCharacter(ByVal searchChar As Char, ByVal value As String) As Integer
        Dim count As Integer = 0
        For Each c As Char In value
            If c = searchChar Then
                count += 1
            End If
        Next
        Return count
    End Function

    Public Function NonEmptyElements(ByRef stringArray As String()) As Integer
        Dim count As Integer = 0

        For Each element As String In stringArray
            If element <> String.Empty Then
                count += 1
            Else
                Exit For
            End If
        Next
        Return count
    End Function

    Public Function ReadFully(input As Stream) As Byte()
        Dim buffer(input.Length) As Byte
        Using br As New BinaryReader(input)
            buffer = br.ReadBytes(input.Length)
        End Using
        Return buffer.ToArray
    End Function


    ' Export As CSV
    Public Sub ExportCSV(outPath As String)
        Try
            Using outStream As StreamWriter = New StreamWriter(outPath)
                If outStream IsNot Nothing Then
                    For Each row As DataGridViewRow In DataTable.Rows
                        If Not row.IsNewRow Then
                            Try
                                outStream.WriteLine(String.Format("""{0}"",""{1}""""{2}""", row.Cells(0).Value.ToString, row.Cells(1).Value.ToString), row.Cells(2).Value.ToString)
                            Catch ex As Exception
                                MsgBox("Error on line" & row.Cells(0).ToString)
                            End Try
                        End If
                    Next
                Else
                    MsgBox("the fileStream cannot be written to")
                End If
            End Using


        Catch ex As IOException
            MsgBox("The disk is full or file exists and is readonly")
        Catch ex As Exception
            MsgBox("an error occured! i dont know what happened >:L" & ex.ToString)
        End Try
    End Sub
    ' Read As CSV
    Private Sub ParseCsv(ResourcePath As String)
        Dim WrongLineNumber As New ArrayList
        'Dim WrongLineText As New ArrayList
        Dim WrongLineReason As New ArrayList
        Dim LineNumber As Integer = 0
        Dim TrophyNumber As Integer = 0
        Dim ErrorMessage As Text.StringBuilder = New Text.StringBuilder


        Dim Warning As Boolean = False
        Dim Fields() As String
        If File.Exists(ResourcePath) = True Then
            Try
                Using parser As FileIO.TextFieldParser = New FileIO.TextFieldParser(ResourcePath)
                    parser.TextFieldType = FileIO.FieldType.Delimited
                    parser.SetDelimiters(",")
                    parser.HasFieldsEnclosedInQuotes = True


                    While Not parser.EndOfData
                        ' first incremenet the counter vars
                        LineNumber += 1

                        Try
                            Fields = parser.ReadFields()
                        Catch ex As FileIO.MalformedLineException
                            WrongLineNumber.Add(LineNumber)
                            WrongLineReason.Add("Line cannot be parsed!")
                            Warning = True
                            Continue While
                        End Try

                        If 1 >= NonEmptyElements(Fields) Or NonEmptyElements(Fields) >= 3 Then
                            ' write it to Erorr
                            WrongLineNumber.Add(LineNumber)
                            If NonEmptyElements(Fields) <= 1 Then
                                WrongLineReason.Add("The line is too short, <= 1")
                            Else
                                WrongLineReason.Add("The line is too long, >= 3")
                            End If
                            Warning = True

                        Else
                            DataTable.Rows.Add(Fields(0), Fields(1))
                            TrophyNumber += 1
                        End If

                    End While

                    If Warning Then
                        MsgBox(WrongLineNumber.Count)
                        For counter As Integer = 0 To WrongLineNumber.Count - 1
                            Dim _LineNum As Integer = WrongLineNumber(counter)
                            'Dim _Text As String = WrongLineText(counter)
                            Dim _Reason As String = WrongLineReason(counter)
                            ErrorMessage.Append(String.Format("Error on Line {0}: {1}", _LineNum, _Reason) & Environment.NewLine)
                        Next counter
                        MessageBox.Show(ErrorMessage.ToString, "Non-fatal errors on parsing document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                    ' enable all the buttons
                    JustSaved = False

                End Using
            Catch ex As IOException
                MessageBox.Show("The file cannot be accessed. Ensure no other programs are using this file and retry",
                                "File IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An error occured :(" & ex.ToString)
            End Try
        Else
            MessageBox.Show("The File Can no longer be found?!")
        End If
    End Sub

    ' WRAPS ParseCsv, Error checking
    Public Sub ImportCSV(CsvPath As String)
        If CsvPath.StartsWith("~") Then
            CsvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & CsvPath.Remove(0, 1)
        End If
        If Not My.Computer.FileSystem.FileExists(CsvPath) Then
            MessageBox.Show("Path not found! Please double check value in text box", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        ' Passed all tests!
        ParseCsv(CsvPath)
    End Sub


    ' Write As Txt
    Public Sub ExportTxt(outPath As String)
        Using txtwriter As New StreamWriter(outPath)
            txtwriter.WriteLine("{CSV}")
            For Each row As DataGridViewRow In DataTable.Rows
                If IsValidRow(row) Then
                    MsgBox("ISVALID")
                    txtwriter.WriteLine(String.Format("""{0}""|""{1}""|""{2}""", row.Cells(0).Value.ToString, row.Cells(1).Value.ToString, row.Cells(2).Value.ToString))
                End If
            Next
        End Using
    End Sub
    ' Read As TXT
    Private Sub ParseTxt(ResourcePath As String)
        Dim CurrentIdentity As String = String.Empty
        Dim Splitted As String()
        For Each Line As String In File.ReadLines(ResourcePath)
            If Line.StartsWith("{CSV}") Then
                CurrentIdentity = "CSV"
            ElseIf Line.StartsWith("{TROPHY_LIMITER}") Then
                CurrentIdentity = "T_LIMIT"
            ElseIf Line.StartsWith("{PLAQUE_LIMITER}") Then
                CurrentIdentity = "P_LIMIT"
            Else
                Select Case (CurrentIdentity)
                    Case "P_LIMIT"
                        PLAQUE_LIMITER.Add(Line)
                    Case "T_LIMIT"
                        TROPHY_LIMITER.Add(Line)
                    Case "CSV"
                        Splitted = Line.Split("|")
                        If Splitted.Length <> 3 Then
                            MsgBox("Error" & Line)
                        Else
                            DataTable.Rows.Add(Splitted(0), Splitted(1), Splitted(2))
                        End If
                End Select
            End If

        Next
    End Sub

    ' Wraps ParseTxt, Error checking
    Public Sub ImportTxt(TxtPath As String)
        MsgBox(TxtPath & "start")
        If TxtPath.StartsWith("~") Then
            TxtPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & TxtPath.Remove(0, 1)
        End If
        If Not My.Computer.FileSystem.FileExists(TxtPath) Then
            MessageBox.Show("Path not found! Please double check value in text box", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MsgBox(TxtPath)
            Exit Sub
        End If
        ' Passed all tests!

        ParseTxt(TxtPath)
    End Sub








    Private Sub OnKeyDownForm(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.Control AndAlso e.Shift AndAlso (e.KeyCode = Keys.S)) Then
            ' When control and S press ave as
            SaveAsPromptWrapper()
        End If
        If (e.Control AndAlso (e.KeyCode = Keys.S)) Then
            ' control-s = SAVE
            SaveFilesAbsolute()
        End If
    End Sub


    Public Sub Write_Config(Path As String)

    End Sub

    Public Sub Read_Config(Path As String)

    End Sub



    Public Sub SaveAsPromptWrapper()
        If SaveAsPrompt.ShowDialog = DialogResult.OK Then
            Select Case SaveAsPrompt.FilterIndex
                ' selected txt
                Case 1
                    ExportTxt(SaveAsPrompt.FileName)
                Case 2
                    ExportCSV(SaveAsPrompt.FileName)
                Case Else
                    MsgBox("? BAD HAPPENED")
            End Select
            FilePath = SaveAsPrompt.FileName
        End If
    End Sub


    Public Sub OpenAsPromptWrapper()
        If OpenAsPrompt.ShowDialog() = DialogResult.OK Then
            MsgBox(OpenAsPrompt.FileName)
            Select Case OpenAsPrompt.FilterIndex
                ' selected txt
                Case 1
                    ImportTxt(OpenAsPrompt.FileName)
                Case 2
                    ImportCSV(OpenAsPrompt.FileName)
                Case Else
                    MsgBox("? happened")
            End Select
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If DataTable.Rows.Count <> 0 And JustSaved = False Then
            If MessageBox.Show("There is content in current table that would be lost. Are you sure?", "Verify", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Cancel Then
                End
            End If
        End If
        DataTable.Rows.Clear()
        OpenAsPromptWrapper()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTable.CellEndEdit
        JustSaved = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles AddRow.Click
        Dim SecondForm As New AddForm
        SecondForm.Show()
    End Sub




    Private Sub OpenToolStripMenuItem1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AddFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddFileToolStripMenuItem.Click
        If OpenAsPrompt.ShowDialog() = DialogResult.OK Then
            Try
                ParseCsv(OpenAsPrompt.FileName)
            Catch ex As Exception
                MsgBox("I dont know what happened >:(" & ex.ToString)
            End Try

        End If
    End Sub

    Private Sub FilePath_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AutoSaver_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)

    End Sub

    Private Sub SetLimiters_Click(sender As Object, e As EventArgs) Handles SetLimiters.Click
        Dim ThirdForm As New ChooseLimiters
        ThirdForm.Show()
    End Sub
    Public Sub UpdateItems()
        NumberAwards.Text = DataTable.Rows.Count
    End Sub

    Private Sub RemoveRow_Click(sender As Object, e As EventArgs) Handles RemoveRow.Click
        Dim RowDictionary As New SortedDictionary(Of Integer, Integer)
        For Each Cell As DataGridViewCell In DataTable.SelectedCells
            If Not RowDictionary.ContainsKey(Cell.RowIndex) Then
                RowDictionary.Add(Cell.RowIndex, 1)
            End If
        Next
        For Each Item As KeyValuePair(Of Integer, Integer) In RowDictionary.Reverse
            Try
                DataTable.Rows.RemoveAt(Item.Key)
            Catch ex As InvalidOperationException
            End Try
        Next
    End Sub

    Public Sub Extract_Exe(Optional exepath As String = Nothing, Optional exename As String = "parser.exe")
        If exepath Is Nothing Then
            exepath = ConfigFolder
        End If
        exepath = Path.Combine(exepath, exename)
        MsgBox(exepath)
        Dim currassembly As Assembly = Assembly.GetExecutingAssembly()
        Dim exestream As Stream = currassembly.GetManifestResourceStream("VisualProject.parser.exe")
        File.WriteAllBytes(exepath, ReadFully(exestream))
        EXE_PATH = exepath
    End Sub

    Public Sub SaveFilesAbsolute()
        If FilePath = String.Empty Then
            FilePath = (New DirectoryInfo(Path.Combine(ConfigFolder, OpenNewName))).FullName
        End If
        If SaveType = "TXT" Then
            ExportTxt(FilePath)
        ElseIf SaveType = "CSV" Then
            ExportCSV(FilePath)
        End If
    End Sub


    Private Sub exteact_Click(sender As Object, e As EventArgs) Handles exportFinal.Click
        If FilePath = String.Empty Then
            FilePath = ".settings_awards/temp.txt"
        End If
        SaveFilesAbsolute()
        Dim tempfile As String = Nothing
        If SaveType = "TXT" Then
            tempfile = "t"
        ElseIf SaveType = "CSV" Then
            tempfile = "c"
        End If
        MsgBox(EXE_PATH & " -n AmazingItWorked " & "-" & tempfile & " " & """" & FilePath & """")
        Shell(EXE_PATH & " -n AmazingItWorked " & "-" & tempfile & " " & """" & FilePath & """")
    End Sub



    Private Sub DataTable_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTable.CellContentClick

    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveAsPromptWrapper()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFilesAbsolute()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim TempFilePath As String
        If FilePath = String.Empty Then
            TempFilePath = "None"
        Else
            TempFilePath = FilePath
        End If
        MessageBox.Show("File Path: " & TempFilePath & Environment.NewLine & Environment.NewLine &
                        "File Type: " & SaveType & Environment.NewLine & Environment.NewLine
                        )
    End Sub

End Class
