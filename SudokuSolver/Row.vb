Public Class Row
    Private squares As New List(Of Square)

    Public Sub New()

    End Sub

    Public Sub New(ByVal size As Integer)
        For i As Integer = 1 To size
            squares.Add(New Square(size))
        Next
    End Sub

    Public Sub setNumber(ByVal xCor As Integer, ByVal number As Integer)
        squares(xCor - 1).setNumber(number)
    End Sub

    Public Sub removeNumber(ByVal xCor As Integer, ByVal number As Integer)
        squares(xCor - 1).removeNumber(number)
    End Sub

    Public Function getSolvedSquare(ByVal xCor As Integer) As Integer
        Return squares(xCor - 1).getSolvedSquare()
    End Function



    Public Function getNumbers(ByVal xCor As Integer) As List(Of Integer)
        Return squares(xCor - 1).getNumbers
    End Function

    Public Sub resetSquare(ByVal xCor As Integer)
        squares(xCor - 1).resetSquare()
    End Sub


    Public Function solved(xCor As Integer) As Boolean
        If squares(xCor - 1).solved() Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function clone() As Row
        Dim copy As New Row()
        For i As Integer = 0 To 8
            copy.squares.Add(squares.ElementAt(i).clone())
        Next
        Return copy
    End Function
End Class
