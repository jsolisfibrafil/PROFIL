Imports System.Data.SqlClient
Public Class frm_anulaCODE

    Dim CMD As New SqlCommand

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim N, checksum As Double
        'Dim cadena As String
        'N = 0
        'CheckSUM = 104
        'cadena = "111310183002040"
        If txt_pswaccs.Visible = False Then
            txt_pswaccs.Visible = True
        Else
            txt_pswaccs.Visible = False
        End If

    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        btnConfirma.Enabled = False

        If Len(Trim(txt_mtvanul.Text)) <= 5 Then
            MsgBox("Comentario no válido", MsgBoxStyle.Critical, "FibrafiL")
            btnConfirma.Enabled = True
            Exit Sub
        End If
        'If (txt_pswaccs.Text <> "inka$12" Or txt_pswaccs.Text <> "inka$12") Then
        If (txt_pswaccs.Text <> "fibrafil15") Then
            MsgBox("Clave de autorización no válida", MsgBoxStyle.Critical, "FibrafiL")
            btnConfirma.Enabled = True
            Exit Sub
        End If
        Try
            With CMD
                .Parameters.Clear()
                .Connection = OCN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "U_SP_ANULACODE"

                .Parameters.Add(New SqlParameter("@MOTIVOANULA", SqlDbType.Text))
                .Parameters.Add(New SqlParameter("@CODEBAR", SqlDbType.Text))

                If OCN.State = ConnectionState.Closed Then OCN.Open()

                For k As Integer = 0 To ListBox1.Items.Count - 1
                    .Parameters("@MOTIVOANULA").Value = txt_mtvanul.Text
                    .Parameters("@CODEBAR").Value = ListBox1.Items.Item(k).ToString()
                    .ExecuteNonQuery()

                Next k
            End With
            MsgBox("Códigos de barra eliminados satisfactoriamente.", MsgBoxStyle.Information, "FIBRAFIL")
        Catch ex As Exception

        End Try

        btnConfirma.Enabled = True
    End Sub

    Private Sub txt_codigo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_codigo.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            If ListBox1.Items.Count > 100 Then
                MsgBox("Límite de items excedido", MsgBoxStyle.Exclamation, "PROFIL")
            Else
                If ListBox1.Items.Contains(txt_codigo.Text) Then
                    MsgBox("Item ya ingresado", MsgBoxStyle.Exclamation, "PROFIL")
                Else
                    ListBox1.Items.Add(txt_codigo.Text)
                End If
                txt_codigo.Text = ""
                ' TextBox3.Text = ListBox1.Items.Count.ToString
                txt_codigo.Focus()
            End If
        End If
    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub
End Class
