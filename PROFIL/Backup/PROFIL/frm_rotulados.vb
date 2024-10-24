Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class frm_rotulados
    Dim d_peso As Decimal
    Dim s_codigobarra As String

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            If ListBox1.Items.Contains(TextBox3.Text) Then
                MsgBox("Item ya ingresado", MsgBoxStyle.Exclamation, "PROFIL")
            Else
                If TextBox3.Text <> "" Then
                    Dim s_code0, i_num, i_lentero, i_ldecimal As String
                    ListBox1.Items.Add(TextBox3.Text)

                    s_code0 = TextBox3.Text.Substring(0, 8)
                    i_num = TextBox3.Text.Substring(TextBox3.Text.Length - 5, 5)
                    i_lentero = i_num.ToString.Substring(0, 3)
                    i_ldecimal = i_num.ToString.Substring(3, 2)
                    d_peso = d_peso + CDec(i_lentero + "." + i_ldecimal)
                    lbl_peso.Text = d_peso 'i_lentero + "." + i_ldecimal
                    lbl_tot.Text = ListBox1.Items.Count.ToString

                    s_codigobarra = TextBox3.Text ' s_code0 + "XX" + i_num

                    Dim printDoc As New PrintDocument
                    AddHandler printDoc.PrintPage, AddressOf print_PrintPage

                    printDoc.PrinterSettings.PrinterName = "PROFIL"
                    printDoc.Print()

                    TextBox3.Text = Nothing
                    TextBox3.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub print_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim prFont As New Font("Arial", 7, FontStyle.Regular)
        Dim codeFont As New Font("IDAutomationHC39M", 10, FontStyle.Regular)
        'Dim codeFont As New Font("IDAutomationSHbC128M", 32, FontStyle.Regular)

        ' Seteo del margen izquierdo
        Dim xPos As Single = 50 ' e.MarginBounds.Left
        ' Seteo del margen superior
        Dim yPos As Single = prFont.GetHeight(e.Graphics)

        ' Impresión del contenido de la etiqueta
        e.Graphics.DrawString("NPD: " & Label1.Text, prFont, Brushes.Black, xPos, yPos)
        e.Graphics.DrawString("FP: " & Date.Today.ToShortDateString, prFont, Brushes.Black, 280, yPos)
        'e.Graphics.DrawString(C_C128.Bar128A(TextBox10.Text), codeFont, Brushes.Blue, xPos, 80)
        e.Graphics.DrawString("*" + s_codigobarra + "*", codeFont, Brushes.Blue, xPos, 80)
        e.Graphics.DrawString("ID ITEM : " + txt_Code1.Text, prFont, Brushes.Black, xPos, 195)
        e.Graphics.DrawString("DESC PRODUCTO : " + txt_item1.Text, prFont, Brushes.Black, xPos, 220)
        ' Se indica que ya no hay nada más que imprimir(el valor predeterminado de esta propiedad es False)
        e.HasMorePages = False
    End Sub


   

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Dim codesbar As String
        Dim dtsitms As New DataSet

        For X As Integer = 0 To ListBox1.Items.Count - 1
            Select Case X
                Case 0
                    codesbar = "'" + ListBox1.Items.Item(X) + "'"
                Case Else
                    codesbar = codesbar + ",'" + ListBox1.Items.Item(X) + "'"
            End Select
        Next

        Try

            If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            OCN.Open()

            Dim CMD As New SqlCommand
            With CMD
                .Parameters.Clear()
                .Connection = OCN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "U_SP_Rotulados"
                .Parameters.Add(New SqlParameter("@opcion", SqlDbType.Int)).Value = 3 ' Opción a ejecutar
                .Parameters.Add(New SqlParameter("@itemno0", SqlDbType.Text)).Value = txt_Code0.Text ' Cod de barras
                .Parameters.Add(New SqlParameter("@itemno1", SqlDbType.Text)).Value = txt_Code1.Text ' Cod de barras
                .Parameters.Add(New SqlParameter("@codesbar", SqlDbType.Text)).Value = codesbar ' Cod de item
                .Parameters.Add(New SqlParameter("@msg", SqlDbType.VarChar, 2500)).Direction = ParameterDirection.Output
                .ExecuteNonQuery()
                MsgBox(.Parameters("@msg").Value.ToString(), MsgBoxStyle.Information, "Fibrafil")
            End With



        Catch ex As Exception
            MsgBox(ex.Message) ' MessageBox.Show("Código no hallado", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        txt_Code0.Text = codesbar
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class