Imports System.Threading

Public Class Sudoku_9x9

    Dim engine As New Engine9x9()
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub valueEntered(sender As Object, e As EventArgs) Handles txt_11.TextChanged, txt_12.TextChanged, txt_13.TextChanged, txt_14.TextChanged, txt_15.TextChanged, txt_16.TextChanged, txt_17.TextChanged, txt_18.TextChanged, txt_19.TextChanged,
                                                                       txt_21.TextChanged, txt_22.TextChanged, txt_23.TextChanged, txt_24.TextChanged, txt_25.TextChanged, txt_26.TextChanged, txt_27.TextChanged, txt_28.TextChanged, txt_29.TextChanged,
                                                                       txt_31.TextChanged, txt_32.TextChanged, txt_33.TextChanged, txt_34.TextChanged, txt_35.TextChanged, txt_36.TextChanged, txt_37.TextChanged, txt_38.TextChanged, txt_39.TextChanged,
                                                                       txt_41.TextChanged, txt_42.TextChanged, txt_43.TextChanged, txt_44.TextChanged, txt_45.TextChanged, txt_46.TextChanged, txt_47.TextChanged, txt_48.TextChanged, txt_49.TextChanged,
                                                                       txt_51.TextChanged, txt_52.TextChanged, txt_53.TextChanged, txt_54.TextChanged, txt_55.TextChanged, txt_56.TextChanged, txt_57.TextChanged, txt_58.TextChanged, txt_59.TextChanged,
                                                                       txt_61.TextChanged, txt_62.TextChanged, txt_63.TextChanged, txt_64.TextChanged, txt_65.TextChanged, txt_66.TextChanged, txt_67.TextChanged, txt_68.TextChanged, txt_69.TextChanged,
                                                                       txt_71.TextChanged, txt_72.TextChanged, txt_73.TextChanged, txt_74.TextChanged, txt_75.TextChanged, txt_76.TextChanged, txt_77.TextChanged, txt_78.TextChanged, txt_79.TextChanged,
                                                                       txt_81.TextChanged, txt_82.TextChanged, txt_83.TextChanged, txt_84.TextChanged, txt_85.TextChanged, txt_86.TextChanged, txt_87.TextChanged, txt_88.TextChanged, txt_89.TextChanged,
                                                                       txt_91.TextChanged, txt_92.TextChanged, txt_93.TextChanged, txt_94.TextChanged, txt_95.TextChanged, txt_96.TextChanged, txt_97.TextChanged, txt_98.TextChanged, txt_99.TextChanged
        Dim txtbox As TextBox = CType(sender, TextBox)
        Dim cordsString As String = txtbox.Name
        Dim xCor, yCor, number As Integer
        xCor = CInt(Mid(cordsString, 5, 1))
        yCor = CInt(Mid(cordsString, 6, 1))
        If txtbox.Text = "" Then
            engine.resetSquare(xCor, yCor)
            Exit Sub
        Else
            Try
                If CInt(txtbox.Text) < 1 Or 9 < CInt(txtbox.Text) Then

                    Throw New Exception()
                End If
            Catch
                txtbox.Text = ""
                MsgBox("Only numbers between 1-9 allowed.", MsgBoxStyle.OkOnly)
                Exit Sub

            End Try

            number = CInt(txtbox.Text)
            engine.setNumber(xCor, yCor, number, False)
        End If
    End Sub


    Public Sub addNumber(ByVal lblName As String, ByVal number As Integer)
        For Each element In Controls
            If element.name = lblName Then
                If number = 0 Then
                    element.text = ""
                Else
                    element.text = CStr(number)
                End If
                Exit For
            End If
        Next

    End Sub

    'Public Function checkNumber(ByVal xCor As Integer, yCor As Integer, number As Integer) As Boolean
    '    Dim numbers As List(Of Integer) = Board.getNumbers(xCor, yCor)
    '    If numbers.Contains(number) Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Private Sub btn_solve_Click(sender As Object, e As EventArgs) Handles btn_solveUntilDone.Click
        engine.solve(True)
    End Sub

    Private Sub btn_New_Click(sender As Object, e As EventArgs) Handles btn_New.Click
        eraseAll()
        startNewBoard()
    End Sub

    Private Sub eraseAll()
        Dim xCor, yCor As Integer
        xCor = 1
        yCor = 1
        For i As Integer = 1 To 81
            addNumber("txt_" + CStr(xCor) + CStr(yCor), 0)
            xCor += 1
            If xCor > 9 Then
                xCor = 1
                yCor += 1
            End If
        Next
    End Sub

    Private Sub startNewBoard()
        Dim number, xCor, yCor As Integer
        Dim sudokuString As String
        engine = New Engine9x9
        sudokuString = My.Resources.Sudokus.sudokuStrings.Substring(Int(50 * Rnd()) * 83, 81)
        xCor = 1
        yCor = 1
        For i As Integer = 1 To 81
            number = CInt(Mid(sudokuString, i, 1))
            addNumber("txt_" + CStr(xCor) + CStr(yCor), number)
            xCor += 1
            If xCor > 9 Then
                xCor = 1
                yCor += 1
            End If
        Next
    End Sub

    Private Sub btn_brute_Click(sender As Object, e As EventArgs) Handles btn_brute.Click
        engine.brute()
    End Sub

    Private Sub btn_solveOnce_Click(sender As Object, e As EventArgs) Handles btn_solveOnce.Click
        engine.solve(False)
    End Sub

    Private Sub btn_solveUntilDone_Click(sender As Object, e As EventArgs) Handles btn_solveUntilDone.Click
        engine.solve(True)
    End Sub

    Private Sub btn_solveAll_Click(sender As Object, e As EventArgs) Handles btn_solveAll.Click
        engine.solveAll()
    End Sub

    Private Sub btn_bruteAll_Click(sender As Object, e As EventArgs) Handles btn_bruteAll.Click
        engine.bruteAll()
    End Sub

    Private Sub btn_EraseAll_Click(sender As Object, e As EventArgs) Handles btn_EraseAll.Click
        eraseAll()
    End Sub
End Class