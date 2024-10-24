Imports System.Data.SqlClient

Public Class frmAddCodebars

    Dim dtsitms As New DataSet
    Event PasaVars()

    Private Sub ListBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyDown
        If e.KeyCode = Keys.Delete Then
            Call DelItem()
        End If
    End Sub

    Private Sub btn_eliminaitemenlist(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call DelItem()
    End Sub

    Private Sub brnCerrar(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Aceptar()
    End Sub

    Sub Aceptar()
        Try
            Dim s_codebars As String
            Dim X As Integer
            Dim dc_pesos As Decimal
            For X = 0 To ListBox1.Items.Count - 1 Step 1
                s_codebars = s_codebars + " " + ListBox1.Items.Item(X).ToString
                dc_pesos = dc_pesos + CDec(CStr(ListBox1.Items.Item(X)).Substring(CStr(ListBox1.Items.Item(X)).Length - 5, 3) + "." + CStr(ListBox1.Items.Item(X)).Substring(CStr(ListBox1.Items.Item(X)).Length - 2, 2))
            Next
            frmEntregas.i_cant = ListBox1.Items.Count
            frmEntregas.s_codebars = s_codebars
            frmEntregas.dc_pesos = dc_pesos
            RaiseEvent PasaVars()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txtingresaCodebars(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinCodebar.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            '    Try
            '        Dim CMD As New SqlCommand("U_SP_ADDCB", OCN)
            '        CMD.CommandType = CommandType.StoredProcedure
            '        CMD.Parameters.Add(New SqlParameter("@CB", SqlDbType.Text)).Value = txtinCodebar.Text ' Cod de barras
            '        CMD.Parameters.Add(New SqlParameter("@ITM", SqlDbType.Text)).Value = TextBox1.Text ' Cod de item

            '        If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            '        OCN.Open()
            '        Try
            '            Dim DAP As New SqlDataAdapter(CMD)
            '            Dim INOUT As Integer
            '            DAP.Fill(dtsitms, "AddCB")
            '            If dtsitms.Tables("AddCB").Rows.Count > 0 Then
            '                INOUT = CInt(dtsitms.Tables("AddCB").Rows(0).Item(0))
            '            End If
            '            If INOUT <> 1 Then
            '                MessageBox.Show("Código no hallado", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '            Else
            '                If Trim(txtinCodebar.Text) <> "" Then
            '                    If ListBox1.Items.Count > 2999 Then
            '                        MsgBox("Límite de items excedido", MsgBoxStyle.Exclamation, "PROFIL")
            '                    Else
            '                        If CInt(txtqmax.Text) <= CInt(TextBox3.Text) Then
            '                            txtinCodebar.Enabled = False
            '                        Else
            '                            If ListBox1.Items.Contains(txtinCodebar.Text) Then
            '                                MsgBox("Item ya ingresado", MsgBoxStyle.Exclamation, "PROFIL")
            '                            Else
            ListBox1.Items.Add(txtinCodebar.Text)
            '                            End If
            txtinCodebar.Text = ""
            TextBox3.Text = ListBox1.Items.Count.ToString
            txtinCodebar.Focus()
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        Catch ex As SqlException
            '            MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Finally
            '            dtsitms.Dispose()
            '            dtsitms.Tables("AddCB").Rows.Clear()
            '            OCN.Close()
            '        End Try
            '    Catch ex As Exception
            '        ' MessageBox.Show("Código no hallado", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    End Try
        End If
    End Sub

    Sub DelItem()
        Dim items As Integer = ListBox1.Items.Count - 1
        If ListBox1.SelectedIndices.Count > 0 Then
            For n As Integer = items To 0 Step -1
                If ListBox1.GetSelected(n) Then
                    ListBox1.Items.RemoveAt(n)
                End If
            Next
        End If
        TextBox3.Text = ListBox1.Items.Count.ToString
        If CInt(txtqmax.Text) <= CInt(TextBox3.Text) Then
            txtinCodebar.Enabled = True
        End If
    End Sub

    Private Sub frmAddCodebars_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = frmEntregas.s_iditem
        TextBox2.Text = frmEntregas.s_descitem
        txtqmax.Text = frmEntregas.i_qxatender.ToString
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
End Class