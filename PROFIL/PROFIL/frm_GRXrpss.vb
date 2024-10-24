Imports System.Data.SqlClient

Public Class frm_GRXrpss

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_codebar.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) And Trim(txt_codebar.Text).Length > 0 Then
            lb_codebars.Items.Add(Trim(txt_codebar.Text))
            txt_codebar.Text = ""
            txt_codebar.Focus()
            lbl_nBulto.Text = lb_codebars.Items.Count.ToString
        End If
    End Sub

    Private Sub lb_codebars_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lb_codebars.KeyDown
        If e.KeyCode = Keys.Delete Then
            lb_codebars.Items.Remove(lb_codebars.SelectedItem)
            lbl_nBulto.Text = lb_codebars.Items.Count.ToString
        End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Ejec_Command("U_SP_INSGUIAXPRSS")
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim x As Integer
        x = MsgBox("Confirmar actualizar guia de remisión", MsgBoxStyle.YesNo, "FIBRAFIL")
        If x = 6 Then
            Ejec_Command("U_FIB_PROCGRXPRSS")
        End If
    End Sub


    Sub Ejec_Command(ByVal NameProce As String)
        Dim cmd As New SqlCommand
        cmd.Parameters.Clear()
        cmd.Connection = OCN
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = NameProce

        If OCN.State = ConnectionState.Closed Then OCN.Open()
        Select Case NameProce

            Case "U_SP_INSGUIAXPRSS"
                With cmd.Parameters
                    .Add(New SqlParameter("@NOV", SqlDbType.BigInt)).Value = txt_nOV.Text
                    .Add(New SqlParameter("@Codebar", SqlDbType.Text))
                    .Add(New SqlParameter("@nerror", SqlDbType.Int)).Direction = ParameterDirection.Output
                    .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                End With

                For Each item As String In lb_codebars.Items
                    Try
                        cmd.Parameters("@Codebar").Value = item
                        'MsgBox(cmd.Parameters("@MSG"), MsgBoxStyle.Information)
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Next

            Case "U_FIB_PROCGRXPRSS"
                With cmd.Parameters
                    .Add(New SqlParameter("@NGRKEY", SqlDbType.BigInt)).Value = txt_nOV.Text
                End With
                cmd.ExecuteNonQuery()
        End Select




    End Sub

End Class