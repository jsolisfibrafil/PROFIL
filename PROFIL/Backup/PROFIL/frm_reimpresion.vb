Imports System.IO
Imports System.Data.SqlClient

Public Class frm_reimpresion

    Dim dts As New DataSet

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_codebar.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            Call Obtener_Data()
            Call LlenarData()
            txt_codebar.Text = ""
            txt_codebar.Focus()
        End If
    End Sub

    Sub Obtener_Data()
        If Not dts.Tables("vOP") Is Nothing Then dts.Tables("vOP").Clear()
        Dim dap4 As New SqlDataAdapter("U_SP_REIMPRIMECODEBAR", OCN)
        dap4.SelectCommand.CommandType = CommandType.StoredProcedure
        dap4.SelectCommand.Parameters.Add(New SqlParameter("@CODEBAR", SqlDbType.VarChar)).Value = txt_codebar.Text
        dap4.Fill(dts, "vOP")
    End Sub

    Sub LlenarData()
        Try
            For Each controlLabel As Control In Me.GroupBox1.Controls
                If TypeOf controlLabel Is Label Then
                    controlLabel.DataBindings.Clear()
                End If
            Next
            lbl_npd.DataBindings.Add("text", dts.Tables("vop"), "NPD")
            lbl_fp.DataBindings.Add("text", dts.Tables("vop"), "FCH")
            lbl_codebar.DataBindings.Add("text", dts.Tables("vop"), "IDBAR")
            lbl_iditm.DataBindings.Add("text", dts.Tables("vop"), "IDITM")
            lbl_dscitm.DataBindings.Add("text", dts.Tables("vop"), "DSCITM")
            lbl_peso.DataBindings.Add("text", dts.Tables("vop"), "PESO")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_print.Click
        'RUTA DEL ARCHIVO DE ETIQUETA 
        Dim RUTA As String

        If rdb_no.Checked = True Then
            RUTA = Application.StartupPath & "\" + "eti_SPeso.prn" '
        Else
            RUTA = Application.StartupPath & "\" + "eti_CPeso.prn" '
        End If


        'LECTURA DEL ARCHIVO PARA GUARDARLO EN UN STRING
        Dim arc1 As New StreamReader(RUTA)
        Dim etiqueta As String = arc1.ReadToEnd()

        'REEMPLAZA LOS VALORES DE LA ETIQUETA
        etiqueta = etiqueta.Replace("[NPD]", lbl_npd.Text)
        etiqueta = etiqueta.Replace("[FP]", lbl_fp.Text)
        etiqueta = etiqueta.Replace("[IDITEM]", lbl_iditm.Text)
        etiqueta = etiqueta.Replace("[DESPRO]", lbl_dscitm.Text)
        etiqueta = etiqueta.Replace("[codbar]", lbl_codebar.Text)
        If rdb_si.Checked = True Then
            etiqueta = etiqueta.Replace("[PESO]", lbl_peso.Text)
        End If
        etiqueta = etiqueta.Replace("\[""]", "''")

        'IMPRIME USANDO DLL DE IMPRESION OPCION SendStringToPrinter()
        If (clsprinter.Class1.SendStringToPrinter("PROFIL", etiqueta)) Then
            MessageBox.Show("Etiqueta Impresa", "FIBRAFIL", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub




End Class