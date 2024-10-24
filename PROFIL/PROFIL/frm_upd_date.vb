Imports System.Data.SqlClient



Public Class frm_upd_date

    Dim dts As New DataSet
    Dim DvUpdt As DataView
    Public Shared CIA, USER, DSCUSER, FCH As String


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim x As Integer
        x = MessageBox.Show("Seguro de procesar estas modificaciones.", "PROFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If x = 6 Then
            If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            OCN.Open()
            Try
                Dim OPT1 As String
                Dim cmd1 As New SqlCommand("U_SP_FCHPRD", OCN)

                If rb_manana.Checked = True Then OPT1 = "M" Else OPT1 = "N"


                cmd1.CommandType = CommandType.StoredProcedure
                With cmd1
                    .Parameters.Add(New SqlParameter("@KEY", SqlDbType.BigInt)).Value = txt_recordkey.Text ' Llave del registro a modificar
                    .Parameters.Add(New SqlParameter("@FCH", SqlDbType.SmallDateTime)).Value = dtp_fech.Value         ' Fecha de de modificación
                    .Parameters.Add(New SqlParameter("@TURN", SqlDbType.Text)).Value = OPT1         ' Turno 01
                    .Parameters.Add(New SqlParameter("@OBS", SqlDbType.Text)).Value = txt_obs.Text          ' Observaciones

                    .Parameters.Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                    .Parameters.Add(New SqlParameter("@Error", SqlDbType.Int)).Direction = ParameterDirection.Output
                End With

                cmd1.ExecuteNonQuery()
                If cmd1.Parameters("@Error").Value.ToString() = 0 Then
                    MsgBox(cmd1.Parameters("@msg").Value.ToString(), MsgBoxStyle.Information, "PROFIL")
                Else
                    MsgBox(cmd1.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
                End If



            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
           

        End If

    End Sub

    Private Sub frm_upd_date_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class