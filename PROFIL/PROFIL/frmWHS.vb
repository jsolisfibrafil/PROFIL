Imports System.Data.SqlClient
Public Class frmWHS
    Public Shared X_form As Integer
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros
    Private Dvlista As DataView
    Dim dts As New DataSet
    Dim cmd As New SqlCommand()

    Private Sub frmWHS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
        Call LlenarData()
    End Sub

    Sub Obtener_Data()
        Try
            Try
                If OCN.State = ConnectionState.Closed Then
                    OCN.Open()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            'DATA
            Dim dap As New SqlDataAdapter("U_SP_LISWHS", OCN)
            dap.SelectCommand.CommandType = CommandType.StoredProcedure
            dap.Fill(dts, "vWHS")

            posi = CType(BindingContext(dts.Tables("vWHS")), CurrencyManager)
            posi.Position = posi.Count + 1
            txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
            DataGrid1.DataSource = dts.Tables("vwhs").DefaultView
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub LlenarData()
        txtCode.DataBindings.Add("text", dts.Tables("vWHS"), "CODIGO")
        txtName.DataBindings.Add("text", dts.Tables("vWHS"), "NOMBRE")
        TXTLOCK.DataBindings.Add("TEXT", dts.Tables("vWHS"), "BLOQUEADO")
        txtDIREC.DataBindings.Add("text", dts.Tables("vWHS"), "DIRECCION")
        TXTCIU.DataBindings.Add("TEXT", dts.Tables("vWHS"), "CIUDAD")
        TXTPROV.DataBindings.Add("TEXT", dts.Tables("vWHS"), "PROVINCIA")
        TXTDPTO.DataBindings.Add("Text", dts.Tables("vWHS"), "DEPARTAMENTO")
        txtPais.DataBindings.Add("text", dts.Tables("vWHS"), "PAIS")
        DataGrid1.SetDataBinding(dts.Tables("vWHS"), "")
    End Sub

    Public Sub Navega(ByVal N_form As Integer)
        Select Case N_form
            Case 0 ' First
                posi.Position = 0
                txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
            Case 1 ' Before
                If posi.Position = 0 Then
                    posi.Position = posi.Count - 1
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                    MsgBox("Ha pasado al ultimo registro")
                Else
                    posi.Position -= 1
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                End If
            Case 2 ' Next
                If posi.Position = posi.Count - 1 Then
                    posi.Position = 0
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                    MsgBox("Ha pasado al primer registro")
                Else
                    posi.Position += 1
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                End If
            Case 3
                posi.Position = posi.Count - 1
                txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        End Select
    End Sub

    Sub LimpiaCajas(ByVal Control As TabControl)
        ' Recorrer todos los controles del formulario indicado 
        Dim Pag As TabPage
        Dim Cajas As New Control
        For Each Pag In Control.TabPages
            For Each Cajas In Pag.Controls
                If TypeOf Cajas Is TextBox Then Cajas.Text = "" ' eliminar el texto  
                If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
            Next
        Next
    End Sub


#Region "Botones del formulario"

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()
            Case "&Actualizar"
                DatosInsUpd("U_SP_FIB_UPD_WHS")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Almacen actualizado satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    OCN.Close()
                End Try
            Case "&Crear"
                DatosInsUpd("U_SP_FIB_INS_WHS")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Almacen creado satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    OCN.Close()
                End Try
        End Select
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Btn_FirstItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Navega(0)
    End Sub

    Private Sub Btn_BeforeItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBefore.Click
        Navega(1)
    End Sub

    Private Sub Btn_NextItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Navega(2)
    End Sub

    Private Sub Btn_LastItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Navega(3)
    End Sub
#End Region

    Sub DatosInsUpd(ByVal NameProced As String)
        cmd.Parameters.Clear()
        cmd.Connection = OCN
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = NameProced
        cmd.Parameters.Add(New SqlParameter("@WHSCODE", SqlDbType.Text)).Value = txtCode.Text ' 
        cmd.Parameters.Add(New SqlParameter("@WHSNAME", SqlDbType.Text)).Value = txtName.Text '
        cmd.Parameters.Add(New SqlParameter("@LOCKED", SqlDbType.Text)).Value = TXTLOCK.Text() '
        cmd.Parameters.Add(New SqlParameter("@ADDRESS", SqlDbType.Text)).Value = txtDIREC.Text '        
        cmd.Parameters.Add(New SqlParameter("@CITY", SqlDbType.Text)).Value = TXTCIU.Text  ' 
        cmd.Parameters.Add(New SqlParameter("@COUNTY", SqlDbType.Text)).Value = TXTPROV.Text '
        cmd.Parameters.Add(New SqlParameter("@STATE", SqlDbType.Text)).Value = TXTDPTO.Text '
        cmd.Parameters.Add(New SqlParameter("@COUNTRY", SqlDbType.Text)).Value = txtPais.Text '
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        LimpiaCajas(Me.TabControl1)
        btnConfirma.Text = "&Crear"
        txtCode.ReadOnly = False
        txtCode.Focus()
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Dim X As Integer
        X = MessageBox.Show("Desea eliminar este registro", "Fibrafil", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If X = 6 Then
            cmd.Connection = OCN
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = ("U_SP_FIB_DEL_WHS")
            cmd.Parameters.Add(New SqlParameter("@WHSCODE", SqlDbType.Text)).Value = txtCode.Text ' 

            If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            OCN.Open()
            Try
                cmd.ExecuteNonQuery()
                MsgBox("Almacen : " + txtName.Text + " ha sido eliminado", MsgBoxStyle.Information, "Fibrafil")
                btnConfirma.Text = "&Ok"
                Call LimpiaCajas(Me.TabControl1)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                OCN.Close()
            End Try
        End If
    End Sub

    Sub TEXTOS(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDPTO.KeyPress, TXTCIU.KeyPress, txtDIREC.KeyPress, TXTLOCK.KeyPress, txtName.KeyPress, txtPais.KeyPress, TXTPROV.KeyPress
        If btnConfirma.Text = "&Ok" Then
            btnConfirma.Text = "&Actualizar"
        End If
    End Sub

    Private Sub TXTLOCK_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTLOCK.TextChanged
        If Me.TXTLOCK.Text = "Y" Then
            chklock.Checked = True
        Else
            chklock.Checked = False
        End If
    End Sub

    Private Sub chklock_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chklock.CheckedChanged
        If chklock.Checked = True Then
            TXTLOCK.Text = "Y"
        Else
            TXTLOCK.Text = "N"
        End If
    End Sub
End Class