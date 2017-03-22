Class Engine9x9
    Dim board As New Board(9)

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Set a number on the board.
    ''' </summary>
    ''' <param name="xCor"></param>
    ''' <param name="yCor"></param>
    ''' <param name="number"></param>
    ''' <param name="printOut"></param>
    Public Sub setNumber(ByVal xCor As Integer, ByVal yCor As Integer, ByVal number As Integer, ByVal printOut As Boolean)
        board.setNumber(xCor, yCor, number)
        If printOut = True Then
            printOutBoard()
            Sudoku_9x9.Update()
        End If
    End Sub

    Public Sub resetSquare(ByVal xCor As Integer, ByVal yCor As Integer)
        board.resetSquare(xCor, yCor)
    End Sub

    Private Function solved(ByRef tempboard As Board, ByVal runTilDone As Boolean, ByVal testing As Boolean) As Integer
        Dim solvedThisRun, solvedLastRun As Integer
        solvedThisRun = 0
        solvedLastRun = -1
        Do While solvedThisRun > solvedLastRun
            solvedLastRun = solvedThisRun
            solvedThisRun = 0
            For x As Integer = 1 To 9
                For y As Integer = 1 To 9
                    If tempboard.solved(x, y) Then
                        solvedThisRun += 1
                        deleteFromOtherSquares(tempboard, x, y, tempboard.getSolvedSquare(x, y))
                    End If
                Next
            Next

            onlyOneOccurence(tempboard)

            If runTilDone = False Then
                Exit Do
            End If
        Loop
        Return solvedThisRun
    End Function

    ''' <summary>
    ''' Deletes a number out of other squares possible numbers sharing either row, column or box
    ''' </summary>
    ''' <param name="tempboard"></param>
    ''' <param name="xCor"></param>
    ''' <param name="yCor"></param>
    ''' <param name="number"></param>
    Private Sub deleteFromOtherSquares(ByRef tempboard As Board, ByVal xCor As Integer, ByVal yCor As Integer, ByVal number As Integer)
        Dim cornerX, cornerY As Integer
        'loops all squares that share row or column with current square
        For i As Integer = 1 To 9
            'check if loop is at the current square
            If i <> yCor Then
                'checks all Y's for the given X
                tempboard.removeNumber(xCor, i, number)
            End If
            'check if loop is at the current square
            If i <> xCor Then
                'checks all Y's for the given Y
                tempboard.removeNumber(i, yCor, number)
            End If
        Next
        cornerX = (((xCor - 1) \ 3) * 3)    'gets the X coordinates corner of current squares box
        cornerY = (((yCor - 1) \ 3) * 3)    'gets the Y coordinates corner of current squares box
        'loops the box
        For x2 As Integer = 1 To 3
            For y2 As Integer = 1 To 3
                If cornerX + x2 <> xCor Or cornerY + y2 <> yCor Then
                    tempboard.removeNumber(cornerX + x2, cornerY + y2, number)
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' Checks if there is a number that only appear once as a possible number in its row, column or box
    ''' </summary>
    ''' <param name="tempboard">board to check</param>
    Private Sub onlyOneOccurence(ByRef tempboard As Board)
        Dim squareListSize, number, cornerX, CornerY As Integer
        Dim onlyoccurenceY, onlyOccurenceX, onlyOccurenceBox As Boolean
        'loops all the squares on the board
        For x As Integer = 1 To 9
            For y As Integer = 1 To 9
                squareListSize = tempboard.getNumbers(x, y).Count
                'checks if the sqaure already is solved
                If squareListSize > 1 Then
                    'loops all the numbers in current square
                    For i As Integer = 1 To squareListSize
                        onlyOccurenceX = True
                        onlyoccurenceY = True
                        onlyOccurenceBox = True
                        number = tempboard.getNumbers(x, y).ElementAt(i - 1)
                        'loops all squares that share row or column with current square
                        For j As Integer = 1 To 9
                            'check if loop is at the current square
                            If j <> y Then
                                'checks all Y's for the given X
                                If tempboard.getNumbers(x, j).Contains(number) Then
                                    onlyoccurenceY = False
                                    'only exit loop if both X and Y have found duplicates
                                    If onlyOccurenceX = False Then
                                        Exit For
                                    End If
                                End If
                            End If
                            'check if loop is at the current square
                            If j <> x Then
                                'checks all the X's for a given Y
                                If tempboard.getNumbers(j, y).Contains(number) Then
                                    onlyOccurenceX = False
                                    'only exit loop if both X and Y have found duplicates
                                    If onlyoccurenceY = False Then
                                        Exit For
                                    End If
                                End If
                            End If

                        Next
                        cornerX = (((x - 1) \ 3) * 3)   'gets the X coordinates corner of current squares box
                        CornerY = (((y - 1) \ 3) * 3)   'gets the Y coordinates corner of current squares box
                        'loops the box
                        For x2 As Integer = 1 To 3
                            For y2 As Integer = 1 To 3
                                If cornerX + x2 <> x Or CornerY + y2 <> y Then  'check if loop is at the current square
                                    If tempboard.getNumbers(cornerX + x2, CornerY + y2).Contains(number) Then
                                        onlyOccurenceBox = False
                                        Exit For
                                    End If
                                End If
                            Next
                        Next
                        'set number if number was only occurence in either X, Y or box
                        If onlyOccurenceX = True Or onlyoccurenceY = True Or onlyOccurenceBox = True Then
                            tempboard.setNumber(x, y, number)
                            Exit For
                        End If
                    Next
                End If
            Next
        Next
    End Sub

    Public Sub printOutBoard()
        Dim solvedSquareNumber As Integer
        For x As Integer = 1 To 9
            For y As Integer = 1 To 9
                If board.solved(x, y) Then
                    solvedSquareNumber = board.getSolvedSquare(x, y)
                    Sudoku_9x9.addNumber("txt_" + CStr(x) + CStr(y), solvedSquareNumber)
                Else
                    Sudoku_9x9.addNumber("txt_" + CStr(x) + CStr(y), 0)
                End If
            Next
        Next

    End Sub

    ''' <summary>
    ''' Starts brute with the main board
    ''' </summary>
    Public Sub brute()
        brute(board)
    End Sub


    ''' <summary>
    ''' Try and solve a square by bruteforcing 
    ''' </summary>
    ''' <param name="tempBoard"></param>
    Public Function brute(ByRef tempBoard As Board) As Boolean
        'check if the whole board is solved already
        If solved(tempBoard, True, False) = 81 Then
            printOutBoard()
            Return True
        End If
        Try
            Dim testBoard As Board
            testBoard = tempBoard.clone()
            Dim bruteList As New List(Of Integer)
            Dim increment As Integer = 0
            Do
                increment += 1
                bruteList = findBruteSquare(testBoard, increment)

                For j As Integer = 1 To bruteList.ElementAt(0) Or j = 9
                    testBoard.setNumber(bruteList.ElementAt(1), bruteList.ElementAt(2), bruteList.ElementAt(3))
                    solved(testBoard, True, True)
                    If checkError(testBoard) = True Then
                        tempBoard.removeNumber(bruteList.ElementAt(1), bruteList.ElementAt(2), bruteList.ElementAt(3))
                        bruteList.RemoveAt(3)
                        increment = 0
                    End If
                    testBoard = tempBoard.clone()
                Next
                If bruteList.Count() = 4 Then
                    setNumber(bruteList.ElementAt(1), bruteList.ElementAt(2), bruteList.ElementAt(3), False)
                    printOutBoard()
                    Return True
                Else
                End If
            Loop
        Catch
            MsgBox("Couldn't find any guaranteed number.", MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Run solved once and then print out the board
    ''' </summary>
    Public Sub solve(ByVal untilDone As Boolean)
        Try
            solved(board, untilDone, False)
            printOutBoard()
        Catch
            MsgBox("Error", MsgBoxStyle.OkOnly)
        End Try
    End Sub

    ''' <summary>
    ''' Try solving the whole puzzle
    ''' </summary>
    Public Sub solveAll()
        Do
            If brute(board) = False Then
                Exit Do
            End If
            If solved(board, True, False) = 81 Then
                printOutBoard()
                Exit Sub
            End If
        Loop
    End Sub

    ''' <summary>
    ''' Find a sqaure to brutefroce, first tries to find a square with only 2 possible numbers then 3 then 4.
    ''' </summary>
    ''' <param name="board"></param>
    ''' <param name="requestedIncrement"></param>
    ''' <returns>A list containing:
    '''          [0] possible numbers in square
    '''          [1] x coordinates for square
    '''          [2] y coordinates for square
    '''          [3] first number
    '''          [4] second number
    '''          ([5,6,7]) possibly third, forth and fifth number
    ''' </returns>
    Private Function findBruteSquare(ByVal board As Board, ByVal requestedIncrement As Integer) As List(Of Integer)
        Dim bruteList As New List(Of Integer)
        Dim currentIncrement As Integer = 0
        'loops all squares looking for a square with only i (2,3,4,5) possibilities
        For i As Integer = 2 To 9
            For x As Integer = 1 To 9
                For y As Integer = 1 To 9
                    If board.getNumbers(x, y).Count = i Then
                        currentIncrement += 1
                        If requestedIncrement = currentIncrement Then
                            bruteList.Add(2)
                            bruteList.Add(x)
                            bruteList.Add(y)
                            bruteList.Add(board.getNumbers(x, y).ElementAt(0))
                            bruteList.Add(board.getNumbers(x, y).ElementAt(1))
                            If i >= 3 Then
                                bruteList.Add(board.getNumbers(x, y).ElementAt(2))
                                If i >= 4 Then
                                    bruteList.Add(board.getNumbers(x, y).ElementAt(3))
                                    If i >= 5 Then
                                        bruteList.Add(board.getNumbers(x, y).ElementAt(4))
                                        If i >= 6 Then
                                            bruteList.Add(board.getNumbers(x, y).ElementAt(5))
                                            If i >= 7 Then
                                                bruteList.Add(board.getNumbers(x, y).ElementAt(6))
                                                If i >= 8 Then
                                                    bruteList.Add(board.getNumbers(x, y).ElementAt(7))
                                                    If i >= 8 Then
                                                        bruteList.Add(board.getNumbers(x, y).ElementAt(8))
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            Return bruteList
                        End If
                    End If
                Next
            Next
        Next
    End Function

    ''' <summary>
    ''' Check if there is any square without any possible numbers
    ''' </summary>
    ''' <param name="board"></param>
    ''' <returns></returns>
    Private Function checkError(ByVal board As Board) As Boolean
        For x As Integer = 1 To 9
            For y As Integer = 1 To 9
                If board.getNumbers(x, y).Count = 0 Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Public Sub bruteAll()
        bruteAll(1, 1)
    End Sub

    Public Function bruteAll(ByVal xCor As Integer, ByVal yCor As Integer) As Boolean

        Do While board.solved(xCor, yCor) = True
            xCor += 1
            If xCor = 10 Then
                xCor = 1
                yCor += 1
                If yCor = 10 Then
                    Return True
                End If
            End If

        Loop
        For i As Integer = 1 To 9
            setNumber(xCor, yCor, i, True)
            If checkForCollision(xCor, yCor, i) = False Then
                If bruteAll(xCor, yCor) = True Then
                    Return True
                End If
            Else
                board.resetSquare(xCor, yCor)

            End If

        Next
        board.resetSquare(xCor, yCor)
        Return False


    End Function

    Private Function checkForCollision(ByVal xCor As Integer, ByVal yCor As Integer, ByVal number As Integer) As Boolean
        Dim boxX, boxY As Integer
        boxX = (((xCor - 1) \ 3) * 3) + 1  'gets the X coordinates corner of current squares box
        boxY = (((yCor - 1) \ 3) * 3) + 1  'gets the Y coordinates corner of current squares box
        Dim occursX, occursY, occursBox As Boolean
        occursX = False
        occursY = False
        occursBox = False
        For i As Integer = 1 To 9

            'check if loop is at the current square
            If i <> yCor Then
                'checks all Y's for the given X



                If board.solved(xCor, i) = True Then
                    If board.getSolvedSquare(xCor, i) = number Then
                        occursY = True
                        Exit For
                    End If
                End If
            End If

            'check if loop is at the current square
            If i <> xCor Then
                'checks all Y's for the given X



                If board.solved(i, yCor) = True Then
                    If board.getSolvedSquare(i, yCor) = number Then
                        occursX = True

                        Exit For

                    End If
                End If
            End If


            'loops the box
            If i = 4 Or i = 7 Then
                boxY += 1
                boxX -= 3
            End If
            If boxX <> xCor Or boxY <> yCor Then  'check if loop is at the current square

                If board.solved(boxX, boxY) Then

                    If board.getSolvedSquare(boxX, boxY) = number Then
                        occursBox = True

                        Exit For

                    End If
                End If
            End If
            boxX += 1
        Next
        If occursX = True Or occursY = True Or occursBox = True Then
            Return True
        Else
            Return False
        End If



    End Function
End Class

