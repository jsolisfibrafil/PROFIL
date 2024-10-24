Imports System.Data.SqlClient
Public Class frmplanifprod

    Dim WithEvents FQ As New frmConsulta
    Public Shared NQ As Integer 'NUMERO DE CONSULTA 
    Public Shared scode, sdesc, s_idcli, s_dsccli As String
    Dim cmd As New SqlCommand
    Private Dvlista As DataView
    Dim dts As New DataSet
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros

   

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#Region "Navegacion de registros"

    Public Sub Navega(ByVal N_form As Integer)
        Select Case N_form
            Case 0 ' First
                posi.Position = 0
            Case 1 ' Before
                If posi.Position = 0 Then
                    posi.Position = posi.Count - 1
                    MsgBox("Ha pasado al ultimo registro")
                Else
                    posi.Position -= 1
                End If
            Case 2 ' Next
                If posi.Position = posi.Count - 1 Then
                    posi.Position = 0
                    MsgBox("Ha pasado al primer registro")
                Else
                    posi.Position += 1
                End If
            Case 3
                posi.Position = posi.Count - 1
        End Select
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Navega(0)
    End Sub

    Private Sub btnBefore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBefore.Click
        Navega(1)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Navega(2)
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Navega(3)
    End Sub

#End Region

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Try
            Select Case btnConfirma.Text
                Case "&Ok"
                    Me.Close()
                Case "&Actualizar"

                    DatosInsUpd("U_SP_FIB_UPD_PLAN")
                    If OCN.State = ConnectionState.Closed Then OCN.Open()
                    cmd.ExecuteNonQuery()
                    MsgBox("Planificación de producción actualizada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
           
                Case "&Crear"

                    DatosInsUpd("U_SP_FIB_INS_PLAN")
                    If OCN.State = ConnectionState.Closed Then OCN.Open()
                    cmd.ExecuteNonQuery()
                    MsgBox("Planificación de producción creada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            OCN.Close()
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        dtp_prod.Value = Date.Today
        dtpffin.Value = Date.Today
        btnList.Visible = True
        btnquery.Visible = True
        LimpiaCajas(Me.TabControl1)
        btnConfirma.Text = "&Crear"
        btnquery.Focus()

        Me.ContextMenuStrip = Nothing
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Dim X As Integer
        X = MessageBox.Show("Desea eliminar este registro", "Fibrafil", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If X = 6 Then

            cmd.Connection = OCN
            cmd.CommandType = CommandType.Text
            cmd.CommandText = ("Delete from OFIBPLAN where RecordKey = " & txtId.Text)

            If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            OCN.Open()
            Try
                cmd.ExecuteNonQuery()
                MsgBox("Planificación de ítem : " + txtname.Text + " ha sido eliminada", MsgBoxStyle.Information, "Fibrafil")
                btnConfirma.Text = "&Ok"
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                OCN.Close()
            End Try
        End If
    End Sub

    Sub DatosInsUpd(ByVal NameProced As String)
        Dim ID As Integer
        ID = CInt(IIf(txtId.Text = "", 0, txtId.Text))
        cmd.Parameters.Clear()
        cmd.Connection = OCN
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = NameProced
        cmd.Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.Int)).Value = ID ' Id planif
        cmd.Parameters.Add(New SqlParameter("@Itemcode", SqlDbType.Text)).Value = txtCode.Text ' code de Item
        cmd.Parameters.Add(New SqlParameter("@Cardcode", SqlDbType.Text)).Value = txtidcli.Text ' code de cliente
        cmd.Parameters.Add(New SqlParameter("@FPROD", SqlDbType.SmallDateTime)).Value = dtp_prod.Value  ' FECHA  DE PRODUCCION
        cmd.Parameters.Add(New SqlParameter("@FFINPROD", SqlDbType.SmallDateTime)).Value = dtpffin.Value ' FECHA FIN
        ' If cmbArea.Text <> "" Then
        cmd.Parameters.Add(New SqlParameter("@AREAID", SqlDbType.Text)).Value = IIf(cmbArea.ValueMember.ToString Is DBNull.Value, 0, cmbArea.SelectedValue.ToString) ' AREA DE PRODUCCION
        ' Else
        ' MsgBox("Favor indicar area", MsgBoxStyle.Exclamation, "FIBRAFIL")
        ' End If
        ' If cmbMac.Text <> "" Then
        cmd.Parameters.Add(New SqlParameter("@MACID", SqlDbType.Text)).Value = IIf(cmbMac.ValueMember.ToString Is DBNull.Value, 0, cmbMac.SelectedValue.ToString) ' MAQUINA DE PRODUCCION
        ' Else
        '  MsgBox("Favor indicar máquina", MsgBoxStyle.Exclamation, "FIBRAFIL")
        ' End If
        If CDec(txtCant.Text) > 0 Then
            cmd.Parameters.Add(New SqlParameter("@QTOPROD", SqlDbType.Decimal)).Value = CDec(txtCant.Text) ' CANTIDAD A PRODUCIR
        Else
            MsgBox("Favor indicar cantidad mayor a cero (0)", MsgBoxStyle.Exclamation, "FIBRAFIL")
        End If
        cmd.Parameters.Add(New SqlParameter("@COMMENT", SqlDbType.Text)).Value = txtComment.Text  'COMENTARIO
        cmd.Parameters.Add(New SqlParameter("@CREATEFOR", SqlDbType.Text)).Value = Form1.vs_User 'USAURIO CREADOR
    End Sub

    Sub LimpiaCajas(ByVal Control As TabControl)
        Dim Pag As TabPage
        Dim Cajas As New Control
        For Each Pag In Control.TabPages
            For Each Cajas In Pag.Controls
                If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is TextBox Then Cajas.Text = ""
                If TypeOf Cajas Is ComboBox Then Cajas.Text = ""
            Next
        Next
    End Sub

    Private Sub frmplanifprod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
        Call LlenarData()
    End Sub

    Sub Obtener_Data()
        Try
            If OCN.State = ConnectionState.Closed Then
                OCN.Open()
            End If
            'DATA

            Dim dap As New SqlDataAdapter("U_SP_LISTPLANI", OCN)
            dap.SelectCommand.CommandType = CommandType.StoredProcedure
            dap.Fill(dts, "vplan")

            ' COMBOBOX AREAR
            Dim da As New SqlClient.SqlDataAdapter("Select * from OFIBAREA", OCN)
            Dim ds As New DataSet()
            da.Fill(ds)

            cmbArea.DisplayMember = "Descripcion"
            cmbArea.ValueMember = "Code"
            cmbArea.DataSource = ds.Tables(0)

            ' COMBOBOX MAQUINA
            Dim dap_mac As New SqlClient.SqlDataAdapter("Select Code, Descripcion from OFIBMAC", OCN)
            dap_mac.Fill(dts)
            cmbMac.DataSource = dts.Tables(1)
            cmbMac.DisplayMember = "Descripcion"
            cmbMac.ValueMember = "Code"

            posi = CType(BindingContext(dts.Tables("vplan")), CurrencyManager)
            posi.Position = posi.Count + 1
            txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub LlenarData()
        txtId.DataBindings.Add("text", dts.Tables("vPLan"), "id")
        txtCode.DataBindings.Add("text", dts.Tables("vPLan"), "CODIGO")
        txtname.DataBindings.Add("text", dts.Tables("vPLan"), "DESCRIPCION")
        txtidcli.DataBindings.Add("text", dts.Tables("vPLan"), "IDCLI")
        txtDsccli.DataBindings.Add("text", dts.Tables("vPLan"), "DSCCLI")
        dtp_prod.DataBindings.Add("text", dts.Tables("vPLan"), "FCH_PROD")
        dtpffin.DataBindings.Add("text", dts.Tables("vPLan"), "FCH_FIN")
        txtComment.DataBindings.Add("text", dts.Tables("vPLan"), "COMENTARIO")
        txtCant.DataBindings.Add("text", dts.Tables("vPlan"), "CANTIDAD")
        cmbArea.DataBindings.Add("Text", dts.Tables("vPLan"), "AREA")
        cmbMac.DataBindings.Add("Text", dts.Tables("vPLan"), "MAQUINA")

        DataGrid1.SetDataBinding(dts.Tables("vplan"), "")
    End Sub

    Private Sub TEXTOS(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress, txtCant.KeyPress, txtComment.KeyPress, txtname.KeyPress
        If btnConfirma.Text = "&Ok" Then
            btnConfirma.Text = "&Actualizar"
        End If
    End Sub

    Private Sub btnquery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnquery.Click
        cVars.NQP = 6 'CONSULTA PARA CLIENTES
        NQ = 2
        FQ.ShowDialog()
        Call Recibevar()
    End Sub

    Private Sub btn_list_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnList.Click
        cVars.NQP = 2 'CONSULTA PARA ITEMS
        NQ = 1
        FQ.ShowDialog()
        Call Recibevar()
    End Sub

    Public Sub Recibevar() Handles FQ.PasaVars
        If scode = Nothing Then scode = txtCode.Text
        If sdesc = Nothing Then sdesc = txtname.Text
        txtCode.Text = scode
        txtname.Text = sdesc

        If s_idcli = Nothing Then s_idcli = txtidcli.Text
        If s_dsccli = Nothing Then s_dsccli = txtDsccli.Text

        txtCode.Text = scode
        txtname.Text = sdesc
        txtidcli.Text = s_idcli
        txtDsccli.Text = s_dsccli
    End Sub
End Class