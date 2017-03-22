Public Class Square
    Private numbers As New List(Of Integer)
    Public Sub New()

    End Sub
    Public Sub New(ByVal size As Integer)
        For i As Integer = 1 To size
            numbers.Add(i)
        Next
    End Sub

    Public Function solved() As Boolean
        If numbers.Count = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function getSolvedSquare() As Integer
        Return numbers(0)
    End Function

    Public Function getNumbers() As List(Of Integer)
        Return numbers
    End Function

    Public Sub setNumber(ByVal number As Integer)
        numbers.Clear()
        numbers.Add(number)
    End Sub

    Public Sub removeNumber(ByVal number As Integer)
        If numbers.Contains(number) Then
            numbers.Remove(number)
        End If
    End Sub

    Public Sub resetSquare()
        numbers.Clear()
        For i As Integer = 1 To 9
            numbers.Add(i)
        Next
    End Sub

    Public Function clone() As Square
        Dim copy As New Square()
        copy.numbers.AddRange(numbers)

        Return copy
    End Function

End Class
