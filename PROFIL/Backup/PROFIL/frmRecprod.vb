Imports PROFIL.MForm
Imports System.Data.SqlClient

Public Class frmRecprod
    Dim Dragging As Boolean
    Dim cursorX, CursorY As Integer

    Private bsource As BindingSource = New BindingSource()
    Dim dts, dtscmb, dtscmb2 As New DataSet
    Dim DvCabecera, DvDetalle, Dvresumen, DvRsmnIN As DataView
    Dim CMD, CMD3 As New SqlCommand
    Public WithEvents posi As CurrencyManager

    Private Sub frmRecprod_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = CierraForms("un", "Ingreso de mercadería", btnConfirma.Text)
    End Sub

    Private Sub frmRecprod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' DTGcmpt.SetDataBinding(Nothing, Nothing)
        ObtenerData()
        LlenarData()
        ArmaGrid()
        ArmagridIngresos()
    End Sub

    Sub ObtenerData()
        ' SELECCIONAR CABECERA DE RECIBOS DE PRODUCCION 
        Dim dap0 As New SqlDataAdapter("U_SP_LISRECPROD", OCN)
        dap0.SelectCommand.CommandType = CommandType.StoredProcedure
        dap0.Fill(dts, "vLIST0")

        DvCabecera = dts.Tables("vLIST0").DefaultView
        DvCabecera.AllowEdit = False
        DvCabecera.AllowNew = False


        Call ItemsxRecep()



       

        Dim dap2 As New SqlDataAdapter("U_SP_SELDETRSM", OCN)
        dap2.SelectCommand.CommandType = CommandType.StoredProcedure
        dap2.Fill(dts, "vLIST2")

        Dvresumen = dts.Tables("vLIST2").DefaultView

        ' SELECCIONS LA LISTA DE LOS CODIGOS DE BARRAS DE LOS ITEM RECEPCIONADOS
        Dim dap3 As New SqlDataAdapter("U_SP_SELRP2DETRSM", OCN)
        dap3.SelectCommand.CommandType = CommandType.StoredProcedure
        dap3.Fill(dts, "vLIST3")

        DvRsmnIN = dts.Tables("vLIST3").DefaultView

        'ComboBox(Encargado recepcion) 
        Dim da_ope As New SqlClient.SqlDataAdapter("Select Code, Name from OFIBEMPL where u_FIB_ARea  = 'A'", OCN)
        da_ope.Fill(dtscmb)
        cmbEmpleado.DisplayMember = "Name"
        cmbEmpleado.ValueMember = "Code"
        cmbEmpleado.DataSource = dtscmb.Tables(0)

        'Combobox Tipo operación
        Dim da_tope As New SqlClient.SqlDataAdapter("Select Code, Name from OFIBTOP where TIPOPER  = 'I'", OCN)
        da_tope.Fill(dtscmb2)

        'recordid = Trim(Label15.Text)
        posi = CType(BindingContext(dts.Tables("vLIST0")), CurrencyManager)
        posi.Position = posi.Count - 1
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)

    End Sub

    Sub LlenarData()
        cmbEmpleado.DisplayMember = "Name"
        cmbEmpleado.ValueMember = "Code"
        cmbEmpleado.DataSource = dtscmb.Tables(0)

        cmbTipOpe.DisplayMember = "Name"
        cmbTipOpe.ValueMember = "Code"
        cmbTipOpe.DataSource = dtscmb2.Tables(0)


        ' Label6.DataBindings.Add("text", dts.Tables("vop"), "Serie")
        cmbEmpleado.DataBindings.Add("text", dts.Tables("vLIST0"), "Empleado")
        DateTimePicker1.DataBindings.Add("text", dts.Tables("vLIST0"), "Fecha")
        txtNorigen.DataBindings.Add("text", dts.Tables("vLIST0"), "Norigen")
        txtDocnum.DataBindings.Add("text", dts.Tables("vLIST0"), "Docnum")
        txtcomment.DataBindings.Add("text", dts.Tables("vLIST0"), "Comentario")
        cmbTipOpe.DataBindings.Add("text", dts.Tables("vLIST0"), "Motivo")
        DTGcmpt.SetDataBinding(DvDetalle, "")
        DataGrid1.SetDataBinding(dts.Tables("vLIST0"), "")
    End Sub

    Sub LlenarGrids()
        DTGcmpt.SetDataBinding(DvDetalle, "")
        dtgRSMN.SetDataBinding(Dvresumen, "")
    End Sub


#Region "Navegacion"
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

    Public Sub Navega(ByVal N_form As Integer)
        Select Case N_form
            Case 0 ' First
                posi.Position = 0
                SubNavegacion()
            Case 1 ' Before
                If posi.Position = 0 Then
                    posi.Position = posi.Count - 1
                    SubNavegacion()
                    MsgBox("Ha pasado al ultimo registro")
                Else
                    posi.Position -= 1
                    SubNavegacion()
                End If
            Case 2 ' Next
                If posi.Position = posi.Count - 1 Then
                    posi.Position = 0
                    SubNavegacion()
                    MsgBox("Ha pasado al primer registro")
                Else
                    posi.Position += 1
                    SubNavegacion()
                End If
            Case 3
                posi.Position = posi.Count - 1
                SubNavegacion()
        End Select
    End Sub

    Sub SubNavegacion()
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
    End Sub

#End Region

#Region "Formato Datagrids"

    Sub ArmaGrid()
        DTGcmpt.TableStyles.Clear()
        DTGcmpt.CaptionText = "INGRESOS"
        DTGcmpt.CaptionBackColor = Color.Navy
        DTGcmpt.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        oEstiloGrid.MappingName = "vLIST1"
        oEstiloGrid.BackColor = Color.LightGoldenrodYellow
        oEstiloGrid.AlternatingBackColor = Color.Aquamarine

        Dim oColGrid As DataGridTextBoxColumn

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "ID"
        oColGrid.MappingName = "ID"
        oColGrid.Width = 30
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "SERIE"
        oColGrid.MappingName = "SERIE"
        oColGrid.Width = 30
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "DOCUMENTO"
        oColGrid.MappingName = "DOCUMENTO"
        oColGrid.Width = 30
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "FECHA"
        oColGrid.MappingName = "FECHA"
        oColGrid.Width = 60
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "CODIGO"
        oColGrid.MappingName = "CODIGO"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "PRODUCTO"
        oColGrid.MappingName = "PRODUCTO"
        oColGrid.Width = 159
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "CANTIDAD"
        oColGrid.MappingName = "Q. Prod"
        oColGrid.Width = 90
        oColGrid.Format = "###.####"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        DTGcmpt.TableStyles.Add(oEstiloGrid)

    End Sub

    Sub ArmagridIngresos()
        '///////////////////º\\\\\\\\\\\\\\\\\\\\\'
        '   Datagrid de los items recepcionados   '
        '///////////////////º\\\\\\\\\\\\\\\\\\\\\'

        dtgRSMN.TableStyles.Clear()
        dtgRSMN.CaptionText = "Recepcion de Items"
        dtgRSMN.CaptionBackColor = Color.Navy
        dtgRSMN.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid2 As New DataGridTableStyle
        oEstiloGrid2.MappingName = "vLIST3"
        oEstiloGrid2.BackColor = Color.LightGoldenrodYellow
        oEstiloGrid2.AlternatingBackColor = Color.Aquamarine

        Dim oColGrid2 As DataGridTextBoxColumn

        oColGrid2 = New DataGridTextBoxColumn
        oColGrid2.TextBox.Enabled = False
        oColGrid2.HeaderText = "ID"
        oColGrid2.MappingName = "RECORDKEY"
        oColGrid2.Width = 0
        oEstiloGrid2.GridColumnStyles.Add(oColGrid2)
        oColGrid2 = Nothing

        oColGrid2 = New DataGridTextBoxColumn
        oColGrid2.TextBox.Enabled = False
        oColGrid2.HeaderText = "LineNum"
        oColGrid2.MappingName = "LineNum"
        oColGrid2.Width = 0
        oEstiloGrid2.GridColumnStyles.Add(oColGrid2)
        oColGrid2 = Nothing

        oColGrid2 = New DataGridTextBoxColumn
        oColGrid2.TextBox.Enabled = False
        oColGrid2.HeaderText = "DOC"
        oColGrid2.MappingName = "DOCNUM"
        oColGrid2.Width = 0
        oEstiloGrid2.GridColumnStyles.Add(oColGrid2)
        oColGrid2 = Nothing

        oColGrid2 = New DataGridTextBoxColumn
        oColGrid2.TextBox.Enabled = False
        oColGrid2.HeaderText = "NumOP"
        oColGrid2.MappingName = "NumOp"
        oColGrid2.Width = 0
        oEstiloGrid2.GridColumnStyles.Add(oColGrid2)
        oColGrid2 = Nothing

        oColGrid2 = New DataGridTextBoxColumn
        oColGrid2.TextBox.Enabled = True
        oColGrid2.HeaderText = "CODEBAR"
        oColGrid2.MappingName = "CODEBARIN"
        oColGrid2.Width = 300
        oEstiloGrid2.GridColumnStyles.Add(oColGrid2)
        oColGrid2 = Nothing

        dtgRSMN.TableStyles.Add(oEstiloGrid2)

    End Sub

#End Region

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        txtNorigen.Text = ""
        Llama_OP()

        If GroupBox1.Visible = False Then GroupBox1.Visible = True
        DateTimePicker1.Value = Now.Date

        Dim da_docnum As New SqlClient.SqlDataAdapter("select Isnull(Max(Docnum)+1,1) As DOCNUM from ORPCFIB where year(Postdate)= " & Year(DateTimePicker1.Value), OCN)
        da_docnum.SelectCommand.CommandType = CommandType.Text
        Dim ds As New DataSet()
        da_docnum.Fill(ds, "vDocnum")
        txtDocnum.DataBindings.Clear()
        txtDocnum.DataBindings.Add("text", ds.Tables("vDocnum"), "DOCNUM")

        'DvDetalle = dts.Tables("vLIST1").DefaultView
        'DvDetalle.AllowEdit = True
        'DvDetalle.AllowNew = False
        Me.ContextMenuStrip = Nothing
        AddNewDataRowView()

    End Sub

    Sub DatosInsUpd(ByVal NameProced As String)
        Try
            CMD.Parameters.Clear()
            CMD.Connection = OCN
            CMD.CommandType = CommandType.StoredProcedure
            CMD.CommandText = NameProced

            CMD.Parameters.Add(New SqlParameter("@Norigen", SqlDbType.BigInt)).Value = 0 ' Numero de orden de produccion
            CMD.Parameters.Add(New SqlParameter("@Docnum", SqlDbType.BigInt)).Value = txtDocnum.Text ' Numero de documento de recepcion
            CMD.Parameters.Add(New SqlParameter("@PostDate", SqlDbType.SmallDateTime)).Value = DateTimePicker1.Value 'fecha de recepcion
            CMD.Parameters.Add(New SqlParameter("@CodEmp", SqlDbType.VarChar)).Value = cmbEmpleado.SelectedValue 'responsable de la recepcion
            CMD.Parameters.Add(New SqlParameter("@Itemcode", SqlDbType.VarChar)).Value = txtitem.Text 'iTEM
            CMD.Parameters.Add(New SqlParameter("@Quantity", SqlDbType.VarChar)).Value = txtCant.Text 'cANTIDAD RECEPCIONADA
            CMD.Parameters.Add(New SqlParameter("@WhsCode", SqlDbType.Text)).Value = Form1.vs_sede  ' Almacen
            CMD.Parameters.Add(New SqlParameter("@MtvOper", SqlDbType.Text)).Value = cmbTipOpe.SelectedValue ' Motivo de recepcion 
            CMD.Parameters.Add(New SqlParameter("@Comments", SqlDbType.Text)).Value = txtcomment.Text ' Comentarios
            CMD.Parameters.Add(New SqlParameter("@Createfor", SqlDbType.VarChar)).Value = Form1.vs_idUser  'Usuario que registra operacion
            CMD.Parameters.Add(New SqlParameter("@FK", SqlDbType.BigInt)).Direction = ParameterDirection.Output
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub dtgRSMN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtgRSMN.Validating
        Dim X, J As Integer
        ' Recorre los items ( compara empezando desde el primero , de abajo hacia arriba)  
        For X = 0 To DvRsmnIN.Count - 1
            For J = DvRsmnIN.Count - 1 To X + 1 Step -1
                ' ... si es el mismo  
                Do While (dtgRSMN.Item(X, 4).ToString = dtgRSMN.Item(J, 4).ToString And dtgRSMN.Item(X, 4).ToString <> "" And dtgRSMN.Item(J, 4).ToString <> "")
                    MsgBox("Item duplicado, favor de corregir de lo contrario no podrá registrar esta operación", 48, "FIBRAFIL")
                    X = DvRsmnIN.Count - 1
                    Exit Do
                Loop
            Next
        Next
    End Sub

    Private Sub AddNewDataRowView()
        ' Evento llamado por el procedimiento NUEVO 
        'DvDetalle = dts.Tables("vLIST1").DefaultView
        DvDetalle.AllowEdit = True
        DvDetalle.AllowNew = True

        DvDetalle.AddNew()
        'DvDetalle.RowFilter = "ID = -1"
        DTGcmpt.FlatMode = False
        DTGcmpt.SetDataBinding(Nothing, Nothing)
        DTGcmpt.SetDataBinding(DvDetalle, "")
        For X As Int32 = 1 To 1
            Dim rowView As DataRowView = DvDetalle.AddNew
            'rowView("ID") = "-1"
            ' rowView("SERIE") = X
            ' rowView("Documento") = 0
            rowView("Fecha") = ""
            rowView("Codigo") = ""
            rowView("Producto") = ""
            'rowView("Cantidad") = 0
            rowView("Q. Prod") = 0
            rowView.EndEdit()
        Next X

        ArmaGrid()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Llama_OP()
    End Sub

    Private Sub GroupBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GroupBox1.MouseDown
        Dragging = True
        ' Note positions of cursor when pressed
        cursorX = e.X
        CursorY = e.Y
    End Sub

    Private Sub GroupBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GroupBox1.MouseMove
        If Dragging Then
            Dim ctrl As Control = CType(GroupBox1, Control)
            ' Move the control according to mouse movement
            ctrl.Left = (ctrl.Left + e.X) - cursorX
            ctrl.Top = (ctrl.Top + e.Y) - CursorY
            ' Ensure moved control stays on top of anything it is dragged on to
            ctrl.BringToFront()
        End If
    End Sub

    Private Sub GroupBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GroupBox1.MouseUp
        ' Reset the flag
        Dragging = False
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        For k As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item(0, k).Value = 1 Then
                DataGridView1.Item(0, k).Value = 0
            End If
        Next
    End Sub

    Private Sub btnokpackinglst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnokpackinglst.Click
        txtitem.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value.ToString()
        txtdescitm.Text = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value.ToString()
        txtCant.Text = DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value.ToString()
        txtCant.Focus()
        GroupBox1.Visible = False
    End Sub

    Sub Llama_OP()
        If GroupBox1.Visible = False Then
            GroupBox1.Visible = True

            Call ItemsxRecep()

            'Dim CMD As New SqlCommand("U_SP_SELOP", OCN)
            'CMD.CommandType = CommandType.StoredProcedure

            Try
                Dim dtCPendientes As New DataTable
                Dim dap As New SqlDataAdapter(CMD)
                dap.Fill(dtCPendientes)
                bsource.DataSource = dtCPendientes

                Dim colCheckbox As New DataGridViewCheckBoxColumn()
                colCheckbox.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                colCheckbox.ThreeState = False
                colCheckbox.TrueValue = 1
                colCheckbox.FalseValue = 0

                colCheckbox.DataPropertyName = "Checkbox"
                colCheckbox.HeaderText = "PROCESAR"
                colCheckbox.Name = "Checkbox"
                colCheckbox.ReadOnly = False
                If Me.DataGridView1.Columns.Count - 1 <= 0 Then
                    DataGridView1.Columns.Add(colCheckbox)
                End If

                DataGridView1.DataSource = bsource
                DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            GroupBox1.Visible = False
        End If
    End Sub

    Private Sub txtCant_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCant.KeyPress
        btnConfirma.Text = "&Crear"
        If btnConfirma.Text = "&Crear" Then
            If e.KeyChar = ChrW(Keys.Return) Then
                DTGcmpt.SetDataBinding(DvDetalle, "")
                
                DvRsmnIN.RowFilter = "RECORDKEY = -1"
                dtgRSMN.FlatMode = False
                dtgRSMN.SetDataBinding(Nothing, Nothing)
                dtgRSMN.SetDataBinding(DvRsmnIN, "")

                If DvRsmnIN.Count <> 0 Then
                    For Gi As Int32 = DvRsmnIN.Count - 1 To 0 Step -1
                        DvRsmnIN.Delete(Gi)
                    Next
                End If
                DvRsmnIN.AllowNew = True

                For X As Int32 = 0 To CInt(txtCant.Text) - 1 'Dvresumen.Item(1)("CANTIDAD")
                    Dim rowView As DataRowView = DvRsmnIN.AddNew
                    rowView("RECORDKEY") = "-1"
                    rowView("LineNum") = X
                    rowView("Docnum") = 1
                    rowView("CODEBARIN") = ""
                    rowView.EndEdit()
                Next X

                DvRsmnIN = dts.Tables("vLIST3").DefaultView
                DvRsmnIN.AllowEdit = True
                DvRsmnIN.AllowNew = False
                dtgRSMN.Focus()
                dtgRSMN.CurrentRowIndex = 0
                dtgRSMN.Item(0, 1) = 0

                TabControl2.SelectedTab = TabPage4
            End If
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()

            Case "&Crear"
                btnConfirma.Enabled = False
                DatosInsUpd("U_SP_FIB_INS_RPCAB")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                ' Try
                CMD.ExecuteNonQuery()
                '  Grabando del recibo de produccion
                With CMD3
                    .Parameters.Clear()
                    .Connection = OCN
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "U_SP_FIB_INS_RP2DET"

                    .Parameters.Add(New SqlParameter("@Docentry", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@Docnum", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@NumOP", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@Linenum", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@Codebarin", SqlDbType.VarChar))
                    .Parameters.Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output

                    For k As Integer = 0 To DvRsmnIN.Count - 1
                        .Parameters("@Docentry").Value = CMD.Parameters("@FK").Value.ToString()
                        .Parameters("@Docnum").Value = CInt(txtDocnum.Text)
                        .Parameters("@NumOP").Value = -99
                        .Parameters("@Linenum").Value = k
                        .Parameters("@Codebarin").Value = DvRsmnIN.Item(k)("CODEBARIN")
                        .ExecuteNonQuery()
                    Next k
                End With

                If CMD3.Parameters("@msg").Value.ToString() = "" Then
                    Dim S_Tin As String
                    S_Tin = Trim(cmbTipOpe.Text)

                    MsgBox("Recibo de productos por: " + S_Tin + ", creada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
                    Me.ContextMenuStrip = Me.ContextMenuStrip1
                Else
                    MsgBox(CMD3.Parameters("@msg").Value.ToString(), MsgBoxStyle.Information, "Fibrafil")
                End If


                '  Catch ex As Exception
                'MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ' Finally
                OCN.Close()
                ' End Try
                btnConfirma.Enabled = True
        End Select
    End Sub

    Sub ItemsxRecep()
        ' SELECCIONAR OPRDENES DE PRODUCCION PARA LA RECEPCION
        Dim cmd As New SqlCommand("U_SP_SELOP", OCN)
        cmd.CommandType = CommandType.StoredProcedure

        Dim dap As New SqlDataAdapter
        dap.SelectCommand = cmd

        Dim parm0 As New SqlParameter("@sede", SqlDbType.Text)
        parm0.Value = Form1.vs_sede
        cmd.Parameters.Add(parm0)

        dap.Fill(dts, "vLIST")

        DvDetalle = dts.Tables("vLIST").DefaultView
        ' DataGridView1.DataSource = Dvlista
        DvDetalle.AllowEdit = False
        DvDetalle.AllowNew = False
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class

