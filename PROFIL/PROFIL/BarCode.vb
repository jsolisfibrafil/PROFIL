Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.BarcodeEAN


Public NotInheritable Class BarCode
    Private Sub New()
    End Sub

#Region "EAN13"
    
    ''' <summary>
    ''' Esta función devuelve el dígito de control para un código EAN13
    ''' </summary>
    ''' <param name="_code">Código EAN de 12 *** DOCE *** números</param>
    ''' <returns>Se devuelve como un string el digito 13 que es el dígito de control</returns>
    ''' <remarks></remarks>
    Private Shared Function EAN13CalcChecksum(ByVal _code As String) As String
        Try
            ' Cálculo del dígito de control EAN
            Dim iSum As Integer = 0
            Dim iSumInpar As Integer = 0
            Dim iDigit As Integer = 0
            'Dim EAN As String = "590123412345"
            Dim EAN As String = _code

            EAN = EAN.PadLeft(17, "0"c)

            For i As Integer = EAN.Length To 1 Step -1
                iDigit = Convert.ToInt32(EAN.Substring(i - 1, 1))
                If i Mod 2 <> 0 Then
                    iSumInpar += iDigit
                Else
                    iSum += iDigit
                End If
            Next

            iDigit = (iSumInpar * 3) + iSum

            Dim iCheckSum As Integer = (10 - (iDigit Mod 10)) Mod 10
            Return iCheckSum.ToString
        Catch ex As Exception
            Throw New Exception("EAN13 calculation checksum error:" & ex.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Genera el código de barras EAN13 y auto-calcula el dígito de control
    ''' </summary>
    ''' <param name="_code">Código de 12 *** DOCE *** números</param>
    ''' <param name="GenerateChecksum"></param>
    ''' <param name="ChecksumText"></param>
    ''' <param name="Height"></param>
    ''' <param name="PrintTextInCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Shared Function CodeEAN13AutoGenerateChecksum(ByVal _code As String, Optional ByVal PrintTextInCode As Boolean = False, Optional ByVal Height As Single = 0, Optional ByVal GenerateChecksum As Boolean = True, Optional ByVal ChecksumText As Boolean = True) As Bitmap
    '    If _code.Trim = "" Then
    '        Return Nothing
    '    Else
    '        Dim barcode As New BarcodeEAN
    '        barcode.StartStopText = True
    '        barcode.GenerateChecksum = GenerateChecksum
    '        barcode.ChecksumText = ChecksumText
    '        If _code.Length <> 12 Then
    '            Throw New Exception("EAN13 code must be 12 digits lenght. Checksum value will be calculated automatically")
    '        End If

    '        If Height <> 0 Then barcode.BarHeight = Height
    '        _code = _code & EAN13CalcChecksum(_code)
    '        barcode.Code = _code
    '        Try
    '            Dim bm As New System.Drawing.Bitmap(barcode.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White))
    '            If PrintTextInCode = False Then
    '                Return bm
    '            Else
    '                Dim bmT As Image
    '                Dim xOffset As Integer = 10
    '                bmT = New Bitmap(bm.Width + xOffset, bm.Height + 14)
    '                Dim g As Graphics = Graphics.FromImage(bmT)
    '                g.FillRectangle(New SolidBrush(Color.White), 0, 0, bm.Width + xOffset, bm.Height + 14)

    '                Dim drawFont As New Font("Arial", 8)
    '                Dim drawBrush As New SolidBrush(Color.Black)

    '                Dim stringSize As New SizeF
    '                stringSize = g.MeasureString(_code, drawFont)
    '                Dim xCenter As Single = (bm.Width - stringSize.Width) / 2
    '                Dim x As Single = xCenter
    '                Dim y As Single = bm.Height

    '                Dim drawFormat As New StringFormat
    '                drawFormat.FormatFlags = StringFormatFlags.NoWrap

    '                g.DrawImage(bm, xOffset, 0)
    '                'g.DrawString(_code, drawFont, drawBrush, x, y, drawFormat)

    '                If xOffset < 10 Then
    '                    g.DrawString(_code.Substring(0, 1), drawFont, drawBrush, 0, y, drawFormat)
    '                Else
    '                    g.DrawString(_code.Substring(0, 1), drawFont, drawBrush, xOffset - 10, y, drawFormat)
    '                End If

    '                Dim x1 As Single = xOffset + 4
    '                g.DrawString(_code.Substring(1, 6), drawFont, drawBrush, x1, y, drawFormat)

    '                Dim x2 As Single = xOffset + 50
    '                g.DrawString(_code.Substring(7, 6), drawFont, drawBrush, x2, y, drawFormat)

    '                g.DrawLine(Pens.Black, xOffset + 0, 0, xOffset + 0, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 2, 0, xOffset + 2, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 46, 0, xOffset + 46, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 48, 0, xOffset + 48, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 92, 0, xOffset + 92, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 94, 0, xOffset + 94, bm.Height + 8)


    '                Return bmT
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception("Error generating EAN13 barcode. Desc:" & ex.Message)
    '        End Try
    '    End If
    'End Function

    ''' <summary>
    ''' Genera el código de barras EAN13. No se verifica el checksum
    ''' </summary>
    ''' <param name="_code">Código de 13 *** TRECE *** números</param>
    ''' <param name="GenerateChecksum"></param>
    ''' <param name="ChecksumText"></param>
    ''' <param name="Height"></param>
    ''' <param name="PrintTextInCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    'Public Shared Function CodeEAN13(ByVal _code As String, Optional ByVal PrintTextInCode As Boolean = False, Optional ByVal Height As Single = 0, Optional ByVal GenerateChecksum As Boolean = True, Optional ByVal ChecksumText As Boolean = True) As Bitmap
    '    If _code.Trim = "" Then
    '        Return Nothing
    '    Else
    '        Dim barcode As New BarcodeEAN
    '        'barcode.CodeType = iTextSharp.text.pdf.Barcode.UPCA
    '        barcode.StartStopText = True
    '        barcode.GenerateChecksum = GenerateChecksum
    '        barcode.ChecksumText = ChecksumText
    '        If _code.Length <> 13 Then
    '            Throw New Exception("EAN13 code must be 13 digits lenght")
    '        End If

    '        If Height <> 0 Then barcode.BarHeight = Height
    '        barcode.Code = _code
    '        Try
    '            Dim bm As New System.Drawing.Bitmap(barcode.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White))
    '            If PrintTextInCode = False Then
    '                Return bm
    '            Else
    '                Dim bmT As Image
    '                Dim xOffset As Integer = 10
    '                bmT = New Bitmap(bm.Width + xOffset, bm.Height + 14)
    '                Dim g As Graphics = Graphics.FromImage(bmT)
    '                g.FillRectangle(New SolidBrush(Color.White), 0, 0, bm.Width + xOffset, bm.Height + 14)

    '                Dim drawFont As New Font("Arial", 8)
    '                Dim drawBrush As New SolidBrush(Color.Black)

    '                Dim stringSize As New SizeF
    '                stringSize = g.MeasureString(_code, drawFont)
    '                Dim xCenter As Single = (bm.Width - stringSize.Width) / 2
    '                Dim x As Single = xCenter
    '                Dim y As Single = bm.Height

    '                Dim drawFormat As New StringFormat
    '                drawFormat.FormatFlags = StringFormatFlags.NoWrap

    '                g.DrawImage(bm, xOffset, 0)
    '                'g.DrawString(_code, drawFont, drawBrush, x, y, drawFormat)

    '                If xOffset < 10 Then
    '                    g.DrawString(_code.Substring(0, 1), drawFont, drawBrush, 0, y, drawFormat)
    '                Else
    '                    g.DrawString(_code.Substring(0, 1), drawFont, drawBrush, xOffset - 10, y, drawFormat)
    '                End If

    '                Dim x1 As Single = xOffset + 4
    '                g.DrawString(_code.Substring(1, 6), drawFont, drawBrush, x1, y, drawFormat)

    '                Dim x2 As Single = xOffset + 50
    '                g.DrawString(_code.Substring(7, 6), drawFont, drawBrush, x2, y, drawFormat)

    '                g.DrawLine(Pens.Black, xOffset + 0, 0, xOffset + 0, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 2, 0, xOffset + 2, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 46, 0, xOffset + 46, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 48, 0, xOffset + 48, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 92, 0, xOffset + 92, bm.Height + 8)
    '                g.DrawLine(Pens.Black, xOffset + 94, 0, xOffset + 94, bm.Height + 8)


    '                Return bmT
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception("Error generating EAN13 barcode. Desc:" & ex.Message)
    '        End Try
    '    End If
    'End Function

#End Region

End Class


