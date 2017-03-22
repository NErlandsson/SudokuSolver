Public Class Board
    Private rows As New List(Of Row)

    Public Sub New()

    End Sub

    Public Sub New(size As Integer)
        For i As Integer = 1 To size
            rows.Add(New Row(size))
        Next
    End Sub


    Public Sub setNumber(ByVal xCor As Integer, ByVal yCor As Integer, ByVal number As Integer)
        rows(yCor - 1).setNumber(xCor, number)
    End Sub

    Public Sub removeNumber(ByVal xCor As Integer, ByVal yCor As Integer, ByVal number As Integer)
        rows(yCor - 1).removeNumber(xCor, number)
    End Sub

    Public Sub resetSquare(ByVal xCor As Integer, ByVal yCor As Integer)
        rows(yCor - 1).resetSquare(xCor)
    End Sub

    Public Function getSolvedSquare(ByVal xCor As Integer, ByVal yCor As Integer) As Integer
        Return rows(yCor - 1).getSolvedSquare(xCor)
    End Function

    Public Function getNumbers(ByVal xCor As Integer, ByVal yCor As Integer) As List(Of Integer)
        Return rows(yCor - 1).getNumbers(xCor)
    End Function

    Public Function solved(xCor As Integer, ycor As Integer) As Boolean
        If rows(ycor - 1).solved(xCor) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function clone() As Board
        Dim copy As New Board()
        For i As Integer = 0 To 8
            copy.rows.Add(rows.ElementAt(i).clone())
        Next
        Return copy
    End Function


End Class
