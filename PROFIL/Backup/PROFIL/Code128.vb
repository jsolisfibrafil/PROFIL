Public Class Code128

    Private BarTextOut As String
    Private BarTextIn As String
    Private BarTextInA As String
    Private BarTextInB As String
    Private TempString As String
    Private BarTempOut As String
    Private BarCodeOut As String
    Private Sum As Long
    Private II As Integer
    Private ThisChar As Integer
    Private CharValue As Long
    Private CheckSumValue As Integer
    Private CheckSum As String
    Private Subset As Integer
    Private StartChar As String
    Private Weighting As Integer
    Private UCC As Integer

    Public Function Bar128A(ByVal BarTextInA As String) As String
        Bar128A = Bar128AB(BarTextInA, 0)
    End Function

    Public Function Bar128Aucc(ByVal BarTextInA As String) As String
        ' Añadir FNC1 al inicio de la cadena
        TempString = Chr(172) & BarTextInA
        Bar128Aucc = Bar128AB(TempString, 0)
    End Function

    Public Function Bar128B(ByVal BarTextInB As String) As String
        Bar128B = Bar128AB(BarTextInB, 1)
    End Function

    Public Function Bar128Bucc(ByVal BarTextInB As String) As String
        ' Añadir FNC1 al inicio de la cadena
        TempString = Chr(172) & BarTextInB
        Bar128Bucc = Bar128AB(TempString, 1)
    End Function

    '-----------------------------------------------------------------------------
    ' Convertir cadena de entrada a código de barras 128  formato A ó B, Pass Subset 0 = A, 1 = B
    '-----------------------------------------------------------------------------

    Public Function Bar128AB(ByVal BarTextIn As String, ByVal Subset As Integer) As String
        ' Dar formato de cadenas entrada y salida
        BarTextOut = ""
        BarTextIn = RTrim(LTrim(BarTextIn))
        ' Creado para el subconjunto en el que estamos
        If Subset = 0 Then
            Sum = 103
            StartChar = "{"
        Else
            Sum = 104
            StartChar = "|"
        End If

        ' Calcular la suma de comprobación, mod 103 y construir la cadenas de salida 
        For II = 1 To Len(BarTextIn)
            ' Encuentra el valor ASCII del carácter actual
            ThisChar = (Asc(Mid(BarTextIn, II, 1)))
            ' Calcular el valor del código de barras  128
            If ThisChar < 127 Then
                CharValue = ThisChar - 32
            Else
                CharValue = ThisChar - 103
            End If

            ' Añadir este valor a suma de comprobación
            Sum = Sum + (CharValue * II)

            'Ahora trabajo en cadena de producción, sin espacios en las fuentes TrueType, sustituye las comillas para Word mailmerge error
            If Mid(BarTextIn, II, 1) = " " Then
                BarTextOut = BarTextOut & Chr(228)
            ElseIf Asc(Mid(BarTextIn, II, 1)) = 34 Then
                BarTextOut = BarTextOut & Chr(226)
            ElseIf Asc(Mid(BarTextIn, II, 1)) = 123 Then
                BarTextOut = BarTextOut & Chr(194)
            ElseIf Asc(Mid(BarTextIn, II, 1)) = 124 Then
                BarTextOut = BarTextOut & Chr(195)
            ElseIf Asc(Mid(BarTextIn, II, 1)) = 125 Then
                BarTextOut = BarTextOut & Chr(196)
            ElseIf Asc(Mid(BarTextIn, II, 1)) = 126 Then
                BarTextOut = BarTextOut & Chr(197)
            Else
                BarTextOut = BarTextOut & Mid(BarTextIn, II, 1)
            End If
        Next II

        ' Encuentra el resto cuando se divide Suma de 103
        CheckSumValue = CInt((Sum Mod 103))
        ' traducir a un valor de caracteres ASCII
        If CheckSumValue > 90 Then
            CheckSum = Chr(CheckSumValue + 103)
        ElseIf CheckSumValue > 0 Then

            CheckSum = Chr(CheckSumValue + 32)

        Else

            CheckSum = Chr(228)

        End If

        'Construir cadena de salida, el espacio es rastrero para Windows rasterization error
        BarTempOut = StartChar & BarTextOut & CheckSum & "~ "
        'Devuelve la cadena
        Bar128AB = BarTempOut
    End Function

End Class