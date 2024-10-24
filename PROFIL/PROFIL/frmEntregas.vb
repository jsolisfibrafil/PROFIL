
Imports System.Text
Imports PROFIL.MForm
Imports System.Drawing.Color
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Imports System.IO
Imports System.Drawing.Imaging

Public Class frmEntregas

    Dim id, cadenanombre0, cadenanombre1 As String
    Private DvCabecera, DvDetalle, Dvresumen As DataView
    Dim DvdetalleListmat As DataView = Nothing
    Dim dts, _dsdetalle, dtsStatus As New DataSet
    Public Shared NQ As Integer 'NUMERO DE CONSULTA 
    Public Shared scode, sname As String
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros
    Dim cmd, cmd1, cmd3, cmdKARDEX As New SqlCommand()
    Public Shared s_iditem = "", s_descitem = "", s_codebars = "", s_codeBP = "", s_nameBP = "", s_TDOC As String = ""
    Public Shared i_cant, i_noSAP, i_keySAP, i_qxatender, i_NDetGuia, i_baseline As Integer ' Cantidad y Nº de orden SAP
    Public Shared dc_pesos As Decimal

    Dim WithEvents Fcodebars As New frmAddCodebars
    Dim WithEvents LisPedSAP As New frmPedSAP
    Event PasaVars()
    Private dtDEntrega As New DataTable
    Private dsource As BindingSource = New BindingSource()

    Dim Dragging As Boolean
    Dim cursorX, CursorY As Integer

    Private Sub frmEntregas_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = CierraForms("una", "Guía de remisión", btnConfirma.Text)
    End Sub

    Private Sub frmEntregas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
        Call LlenarData()

        Call ArmaGridRSMN()
        Call ArmaGridPackingList()
        Call ArmaGrid() 'Grid del listado de las recepciones 
    End Sub

    Sub Obtener_Data()
        'En caso de existir algun DTS activo lo libera para volverlo a ingresar, es util después de grabar
        If dts.Tables.Contains("vCguia") Then
            dts.Clear()
        End If

        'Dataset correspondiente a la cabecera de la guía.
        Dim dap0 As New SqlDataAdapter("U_SP_LISGUIA", OCN)
        dap0.SelectCommand.CommandType = CommandType.StoredProcedure
        dap0.Fill(dts, "vCguia")

        DvCabecera = dts.Tables("vCguia").DefaultView
        DvCabecera.AllowEdit = False
        DvCabecera.AllowNew = False

        'Dataset correspondiente al detalle de la guía
        Dim dap4 As New SqlDataAdapter("U_SP_SELDGUIA1", OCN)
        dap4.SelectCommand.CommandType = CommandType.StoredProcedure
        dap4.Fill(dts, "vDguia1")

        Dvresumen = dts.Tables("vDguia1").DefaultView
        Dvresumen.AllowEdit = True
        Dvresumen.AllowNew = False

        'Dataset correspondiente al transportista (COMBOBOX01)
        Dim dap2 As New SqlDataAdapter("U_SP_FIB_IMPORTTRANSPORTISTA", OCN)
        dap2.SelectCommand.CommandType = CommandType.StoredProcedure
        dap2.Fill(dts, "vtransportista")

        'Dataset correspondiente al transportista (COMBOBOX02)
        Dim dap3 As New SqlDataAdapter("U_SP_FIB_IMPORTTRANSPORTISTA", OCN)
        dap3.SelectCommand.CommandType = CommandType.StoredProcedure
        dap3.Fill(dts, "vtransportista2")

        'Se ubica en el ultimo registro
        Navega(3)

    End Sub

    Sub LlenarData()

        txtcliente.DataBindings.Add("text", dts.Tables("vCguia"), "CLIENTE")
        txtplpaisdest.DataBindings.Add("text", dts.Tables("vCguia"), "PAIS")
        cmbtransportista.DataBindings.Add("text", dts.Tables("vCguia"), "TRANSPORTISTA")
        cmbtransportista2.DataBindings.Add("text", dts.Tables("vCguia"), "TRANSPORTISTA2")
        txtplaca.DataBindings.Add("text", dts.Tables("vCguia"), "U_BPP_MDVC")
        txtconductor.DataBindings.Add("text", dts.Tables("vCguia"), "U_BPP_MDFN")
        txtlicencia.DataBindings.Add("text", dts.Tables("vCguia"), "U_BPP_MDFC")
        txtcontenedor.DataBindings.Add("text", dts.Tables("vCguia"), "U_STR_CONTENEDOR")
        txtprescinto1.DataBindings.Add("text", dts.Tables("vCguia"), "U_STR_NPRESCINTO")
        txtprescinto2.DataBindings.Add("text", dts.Tables("vCguia"), "U_FIB_PRESCINTO2")
        txtbultos.DataBindings.Add("text", dts.Tables("vCguia"), "U_FIB_NBULTOS")
        txtpeso.DataBindings.Add("text", dts.Tables("vCguia"), "U_FIB_KG")
        txtNped.DataBindings.Add("text", dts.Tables("vCguia"), "U_OrdPEDIDO")
        txtNfact.DataBindings.Add("text", dts.Tables("vCguia"), "U_NroFACTURA")
        txtserie.DataBindings.Add("text", dts.Tables("vCguia"), "U_BPV_SERI")
        txtnum.DataBindings.Add("text", dts.Tables("vCguia"), "U_BPV_NCON2")
        dtpFdoc.DataBindings.Add("text", dts.Tables("vCguia"), "DOCDATE")
        Label5.DataBindings.Add("text", dts.Tables("vCguia"), "Docnun")
        Label17.DataBindings.Add("text", dts.Tables("vCguia"), "Autorizado")
        ' datos de exportación
        txtdginter.DataBindings.Add("text", dts.Tables("vCguia"), "dginter")
        txtrucinter.DataBindings.Add("text", dts.Tables("vCguia"), "rucinter")
        txtdirinter.DataBindings.Add("text", dts.Tables("vCguia"), "direcinter")
        ' datos de packing list
        dtpfpl.DataBindings.Add("text", dts.Tables("vCguia"), "pl_fecha")
        txtplembarco.DataBindings.Add("text", dts.Tables("vCguia"), "pl_embarque")
        txtplpuerto1.DataBindings.Add("text", dts.Tables("vCguia"), "pl_puerto1")
        txtplpuerto2.DataBindings.Add("text", dts.Tables("vCguia"), "pl_puerto2")
        dtpplsalida.DataBindings.Add("text", dts.Tables("vCguia"), "pl_fchsalida")
        dtpplllegada.DataBindings.Add("text", dts.Tables("vCguia"), "pl_fcharribo")
        txtplpesobruto.DataBindings.Add("text", dts.Tables("vCguia"), "pl_pesototbruto")

        dtgRSMN.SetDataBinding(dts.Tables("vDguia1"), "")  ' datagrid detalle de entrega
        DataGrid2.SetDataBinding(dts.Tables("vDguia1"), "") ' datagrid de packing list
        DataGrid1.SetDataBinding(dts.Tables("vCguia"), "")
    End Sub

    Sub ArmaGridRSMN()
        With dtgRSMN
            .TableStyles.Clear()
            .CaptionText = "ITEMS A DESPACHAR"
            .CaptionBackColor = Color.Navy
            .CaptionForeColor = Color.Yellow
        End With
        Dim oEstiloGrid As New DataGridTableStyle
        With oEstiloGrid
            .MappingName = "vDguia1"
            .BackColor = Color.LightGoldenrodYellow
            .AlternatingBackColor = Color.Aquamarine
            If btnConfirma.Text = "Crear" Then
                .AllowSorting = False
            Else
                .AllowSorting = True
            End If
        End With

        Dim oColGrid As DataGridTextBoxColumn

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.ReadOnly = False
        oColGrid.HeaderText = "RecordKey"
        oColGrid.MappingName = "RecordKey"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.ReadOnly = False
        oColGrid.HeaderText = "Docnum"
        oColGrid.MappingName = "Docnum"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.TextBox.ReadOnly = True
        oColGrid.HeaderText = "#"
        oColGrid.MappingName = "LineNum"
        oColGrid.Width = 20
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "CODIGO"
        oColGrid.MappingName = "ItemNo"
        oColGrid.Width = 120
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "DESCRIPCION"
        oColGrid.MappingName = "ItemName"
        oColGrid.Width = 200
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "POR ATENDER"
        oColGrid.MappingName = "Qpedido"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "CANTIDAD"
        oColGrid.MappingName = "Quantity"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "PESO"
        oColGrid.MappingName = "U_FIB_PESO"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "CODIGOS DE BARRA"
        oColGrid.MappingName = "Codebars"
        oColGrid.Width = 350
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "Linea Orig"
        oColGrid.MappingName = "baseline"
        oColGrid.Width = 35
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        dtgRSMN.TableStyles.Add(oEstiloGrid)

    End Sub

    Sub ArmaGridPackingList()

        With DataGrid2
            .TableStyles.Clear()
            .CaptionText = "PACKING LIST"
            .CaptionBackColor = Color.Navy
            .CaptionBackColor = Color.Navy
            .CaptionForeColor = Color.Yellow
        End With

        Dim oEstiloGrid As New DataGridTableStyle
        With oEstiloGrid
            .MappingName = "vDguia1"
            .BackColor = Color.LightGoldenrodYellow
            .AlternatingBackColor = Color.Aquamarine
            If btnConfirma.Text = "Crear" Then
                .AllowSorting = False
            Else
                .AllowSorting = True
            End If
        End With


        Dim oColGrid As DataGridTextBoxColumn

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.ReadOnly = False
        oColGrid.HeaderText = "RecordKey"
        oColGrid.MappingName = "RecordKey"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.TextBox.ReadOnly = True
        oColGrid.HeaderText = "#"
        oColGrid.MappingName = "LineNum"
        oColGrid.Width = 20
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "CODIGO"
        oColGrid.MappingName = "ItemNo"
        oColGrid.Width = 120
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "DESCRIPCION"
        oColGrid.MappingName = "ItemName"
        oColGrid.Width = 210
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "CANTIDAD"
        oColGrid.MappingName = "Quantity"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "P. NETO"
        oColGrid.MappingName = "U_FIB_PESO"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "P. BRUTO"
        oColGrid.MappingName = "pBruto"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        DataGrid2.TableStyles.Add(oEstiloGrid)

    End Sub

    Sub ArmaGrid()
        DataGrid1.TableStyles.Clear()
        DataGrid1.CaptionText = ""
        '  DataGrid1.CaptionBackColor = Color.Navy
        DataGrid1.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        oEstiloGrid.MappingName = "vCguia"
        oEstiloGrid.SelectionBackColor = Color.SeaGreen
        oEstiloGrid.SelectionForeColor = Color.White
        oEstiloGrid.HeaderBackColor = System.Drawing.SystemColors.Control
        oEstiloGrid.BackColor = Color.FromArgb(255, 255, 192)
        oEstiloGrid.AlternatingBackColor = Color.FromArgb(192, 192, 255)

        Dim oColGrid As DataGridTextBoxColumn

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "ID"
        oColGrid.MappingName = "RECORDKEY"
        oColGrid.Width = 50
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "ID"
        oColGrid.MappingName = "CLIENTE"
        oColGrid.Width = 250
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "FECHA"
        oColGrid.MappingName = "DOCDATE"
        oColGrid.Width = 120
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "Nº PEDIDO"
        oColGrid.MappingName = "U_ORDPEDIDO"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "Nº FACTURA"
        oColGrid.MappingName = "u_NROFACTURA"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "SERIE GUIA"
        oColGrid.MappingName = "U_BPV_SERI"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "Nº GUIA"
        oColGrid.MappingName = "u_BPV_NCON2"
        oColGrid.Width = 100
        'oColGrid.Format = "###.####"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        DataGrid1.TableStyles.Add(oEstiloGrid)

    End Sub

    Private Sub btnClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        i_noSAP = Nothing
        Me.Close()
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        btnConfirma.Text = "Crear"

        btnquery.Visible = True
        btn_toPDA.Visible = True
        btn_trans.Visible = True
        Me.ContextMenuStrip = Nothing

        Call LimpiaCajas(Me.TabControl1)

        cmbtransportista.DataSource = dts.Tables("vtransportista")
        cmbtransportista.DisplayMember = "Cardname"
        cmbtransportista.ValueMember = "Cardcode"

        cmbtransportista2.DataSource = dts.Tables("vtransportista2")
        cmbtransportista2.DisplayMember = "Cardname"
        cmbtransportista2.ValueMember = "Cardcode"

        Dim da_docnum As New SqlClient.SqlDataAdapter("select Isnull(Max(Docnun)+1,1) As DOCNUM from OFIBCGUIA", OCN)
        da_docnum.SelectCommand.CommandType = CommandType.Text
        Dim ds As New DataSet()
        da_docnum.Fill(ds, "vDocnum")

        Label5.DataBindings.Clear()
        Label5.DataBindings.Add("text", ds.Tables("vDocnum"), "DOCNUM")
        dtpFdoc.Value = Now.Date
        txtpeso.Text = 0
        txtbultos.Text = 0
        txtplbultos.Text = 0
        txtplpesobruto.Text = 0
        txtplpesoneto.Text = 0

        AddNewDataRowView()

    End Sub

    Sub LimpiaCajas(ByVal Control As TabControl)
        Dim Pag As TabPage
        Dim Cajas As New Control

        Label5.DataBindings.Clear()
        For Each Pag In Control.TabPages
            For Each Cajas In Pag.Controls
                If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is ComboBox Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is DateTimePicker Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is Label Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is TextBox Then Cajas.Text = ""
            Next
        Next

        '  For Each Pag In Control.TabPages
        For Each Cajas In GroupBox2.Controls
            If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
            If TypeOf Cajas Is DateTimePicker Then Cajas.DataBindings.Clear()
            If TypeOf Cajas Is TextBox Then Cajas.Text = ""
            'Next
        Next
    End Sub

    Private Sub AddNewDataRowView()
        ' Evento llamado por el procedimiento NUEVO 

        DvDetalle = dts.Tables("vDguia1").DefaultView
        DvDetalle.AllowEdit = True
        DvDetalle.AllowNew = True
        DvDetalle.AllowDelete = False
        DvDetalle.AddNew()
        DvDetalle.RowFilter = "RECORDKEY = -1"

        dtgRSMN.FlatMode = False
        dtgRSMN.SetDataBinding(Nothing, Nothing)
        dtgRSMN.SetDataBinding(DvDetalle, "")

        DataGrid2.FlatMode = False
        DataGrid2.SetDataBinding(Nothing, Nothing)
        DataGrid2.SetDataBinding(DvDetalle, "")

        For X As Int32 = 0 To 100

            Dim rowView As DataRowView = DvDetalle.AddNew
            rowView("RECORDKEY") = "-1"
            rowView("Docnum") = 0
            rowView("LineNum") = X
            rowView("ItemNo") = ""
            rowView("ItemName") = ""
            rowView("Qpedido") = 0
            rowView("U_FIB_PESO") = 0
            rowView("pBruto") = 0
            rowView("Quantity") = 0
            rowView("Codebars") = ""
            rowView("BaseLine") = -99
            rowView.EndEdit()
        Next X

        DvDetalle.AllowEdit = False
        DvDetalle.AllowNew = False
        OCN.Close()

    End Sub

    Private Sub dtgRSMN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtgRSMN.KeyPress
        If btnConfirma.Text = "Crear" Then
            If (dtgRSMN.CurrentCell.ColumnNumber = 8) Then
                If e.KeyChar = ChrW(Keys.Return) Then
                    s_iditem = dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 3).ToString '), "", dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 3))
                    s_descitem = IIf(IsDBNull(dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 4)), "", dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 4))
                    i_qxatender = IIf(IsDBNull(dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 5)), 0, dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 5))
                    Dim frmAddCodebars As New frmAddCodebars
                    frmAddCodebars.ShowDialog()
                    Call Recibevar()
                End If
            End If
        End If
    End Sub

    Sub DatosInsUpd(ByVal NameProced As String, ByVal i_state As Integer)
        Try
            cmd.Parameters.Clear()
            cmd.Connection = OCN
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = NameProced

            cmd.Parameters.Add(New SqlParameter("@DOCNUN", SqlDbType.BigInt)).Value = Label5.Text  ' Numero profil
            cmd.Parameters.Add(New SqlParameter("@DOCDATE", SqlDbType.SmallDateTime)).Value = dtpFdoc.Value ' Fecha de guia
            cmd.Parameters.Add(New SqlParameter("@STATE", SqlDbType.Int)).Value = i_state  ' ESTADO
            cmd.Parameters.Add(New SqlParameter("@CARDCODE", SqlDbType.Text)).Value = s_codeBP ' Codigo cliente
            cmd.Parameters.Add(New SqlParameter("@U_BPP_MDCT", SqlDbType.Text)).Value = cmbtransportista.SelectedValue 'transportista 01
            cmd.Parameters.Add(New SqlParameter("@U_FIB_TRANS2", SqlDbType.Text)).Value = cmbtransportista2.SelectedValue  'transportista 02
            cmd.Parameters.Add(New SqlParameter("@U_BPP_MDVC", SqlDbType.Text)).Value = txtplaca.Text.Trim ' Placa
            cmd.Parameters.Add(New SqlParameter("@U_BPP_MDFN", SqlDbType.Text)).Value = txtconductor.Text.Trim  ' Conductor
            cmd.Parameters.Add(New SqlParameter("@U_BPP_MDFC", SqlDbType.Text)).Value = txtlicencia.Text.Trim  ' Licencia conducir
            cmd.Parameters.Add(New SqlParameter("@U_STR_CONTENEDOR", SqlDbType.Text)).Value = txtcontenedor.Text.Trim  ' Nro. Contenedor
            cmd.Parameters.Add(New SqlParameter("@U_STR_NPRESCINTO", SqlDbType.Text)).Value = txtprescinto1.Text.Trim  ' Prescinto  01
            cmd.Parameters.Add(New SqlParameter("@U_FIB_PRESCINTO2", SqlDbType.Text)).Value = txtprescinto2.Text.Trim  ' Prescinto  02
            cmd.Parameters.Add(New SqlParameter("@U_FIB_NBULTOS", SqlDbType.Int)).Value = txtbultos.Text.Trim   'Cant. Bultos
            cmd.Parameters.Add(New SqlParameter("@U_FIB_KG", SqlDbType.Decimal)).Value = CDec(txtpeso.Text.Trim) ' Peso
            cmd.Parameters.Add(New SqlParameter("@U_ORDPEDIDO", SqlDbType.Text)).Value = txtNped.Text.Trim  'Nro pedido
            cmd.Parameters.Add(New SqlParameter("@U_NROFACTURA", SqlDbType.Text)).Value = txtNfact.Text.Trim  ' Nro factura
            cmd.Parameters.Add(New SqlParameter("@U_BPV_SERI", SqlDbType.Text)).Value = txtserie.Text.Trim    'Serie guia
            cmd.Parameters.Add(New SqlParameter("@U_BPV_NCON2", SqlDbType.Text)).Value = txtnum.Text.Trim   'Numero guia
            cmd.Parameters.Add(New SqlParameter("@CREATEFOR", SqlDbType.Text)).Value = Form1.vs_idUser.Trim  'Codigo usuario

            cmd.Parameters.Add(New SqlParameter("@DESTINTER", SqlDbType.Text)).Value = txtdginter.Text.Trim    'Descripcion aduana
            cmd.Parameters.Add(New SqlParameter("@RUCINTER", SqlDbType.Text)).Value = txtrucinter.Text.Trim   'RUC aduana
            cmd.Parameters.Add(New SqlParameter("@DIRECINTER", SqlDbType.Text)).Value = txtdirinter.Text.Trim  'Direccion aduana

            cmd.Parameters.Add(New SqlParameter("@AUTORIZADO", SqlDbType.Text)).Value = "" 'autorizacion usuario

            cmd.Parameters.Add(New SqlParameter("@pl_fecha", SqlDbType.SmallDateTime)).Value = dtpfpl.Value
            cmd.Parameters.Add(New SqlParameter("@pl_embarque", SqlDbType.Text)).Value = txtplembarco.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@pl_puerto1", SqlDbType.Text)).Value = txtplpuerto1.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@pl_puerto2", SqlDbType.Text)).Value = txtplpuerto2.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@pl_fchsalida", SqlDbType.SmallDateTime)).Value = dtpplsalida.Value
            cmd.Parameters.Add(New SqlParameter("@pl_fcharribo", SqlDbType.SmallDateTime)).Value = dtpplllegada.Value
            cmd.Parameters.Add(New SqlParameter("@pl_pesototbruto", SqlDbType.Decimal)).Value = CDec(txtplpesobruto.Text.Trim)

            cmd.Parameters.Add(New SqlParameter("@pl_paisdest", SqlDbType.Text)).Value = txtplpaisdest.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@pl_paisori", SqlDbType.Text)).Value = txtpaisOri.Text.Trim

            cmd.Parameters.Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
            cmd.Parameters.Add(New SqlParameter("@FK", SqlDbType.BigInt)).Direction = ParameterDirection.Output
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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

        posi = CType(BindingContext(dts.Tables("vCguia")), CurrencyManager)

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
        Dvresumen.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))

    End Sub

#End Region

    Private Sub ImprimirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImprimirToolStripMenuItem.Click
        Try
            If Label17.Text = "Y" Then ' Con esto verifico si se tiene la autorizacion para la impresion de la guia
                frmReport.NFORM = "GUIA"
                frmReport.KEYGUIA = DvCabecera.Item(posi.Position)("Recordkey").ToString
                Dim frm_report As New frmReport
                frm_report.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnquery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnquery.Click
        Dim frmpedidos As New frmPedSAP
        frmpedidos.ShowDialog()
        Call Recibe_DataPedidos()
    End Sub

    Public Sub Recibe_DataPedidos() Handles LisPedSAP.PasaVars
        If i_noSAP.ToString <> txtNped.Text Then
            Obtienedetalleentrega()
        End If
        txtNped.Text = i_noSAP.ToString
        txtcliente.Text = s_nameBP
        Me.Text = "Entrega para cliente : " + s_codeBP + " - " + s_nameBP
    End Sub

    Sub Obtienedetalleentrega()
        If btnConfirma.Text = "Crear" Then
            Dim CMD As New SqlCommand("U_SP_FIB_LISPEDSAP", OCN)
            CMD.CommandType = CommandType.StoredProcedure

            Dim dap99 As New SqlDataAdapter
            dap99.SelectCommand = CMD
            CMD.Parameters.Add(New SqlParameter("@ID", SqlDbType.Int)).Value = i_keySAP   ' llave del informe  SAP
            CMD.Parameters.Add(New SqlParameter("@PD", SqlDbType.VarChar)).Value = "DET"  ' Parte de documento CAB o DET
            CMD.Parameters.Add(New SqlParameter("@TDOC", SqlDbType.VarChar)).Value = s_TDOC  ' No Aplica

            dap99.Fill(dts, "vListaMat")

            Dim DvdetalleListmat As DataView = Nothing
            DvdetalleListmat = dts.Tables("vListaMat").DefaultView

            Dim x As Integer
            For x = 0 To 100
                dtgRSMN.Item(x, 3) = ""
                dtgRSMN.Item(x, 4) = ""
                dtgRSMN.Item(x, 5) = 0
            Next
            Try
                For x = 0 To 100
                    dtgRSMN.Item(x, 3) = DvdetalleListmat.Item(x)("CODIGO").ToString
                    dtgRSMN.Item(x, 4) = DvdetalleListmat.Item(x)("PRODUCTO").ToString
                    dtgRSMN.Item(x, 5) = DvdetalleListmat.Item(x)("PENDIENTE").ToString
                    dtgRSMN.Item(x, 9) = DvdetalleListmat.Item(x)("LINEA").ToString
                Next
            Catch ex As Exception

            End Try

            dts.Tables("vListaMat").Clear()


        End If
    End Sub

    Public Sub Recibevar() Handles Fcodebars.PasaVars
        'este evento recibe los valores desde el form de ingreso de codigos de barra
        'Try
        '    dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 6) = i_cant
        '    dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 7) = dc_pesos
        '    dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 8) = s_codebars

        '    txtbultos.Text = SumarPesoCant("cant").ToString
        '    txtpeso.Text = SumarPesoCant("peso").ToString

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        ' If NQ = 2 Then
        dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 3) = scode 'IIf(scode = scode, "", scode)
        dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 4) = sname 'IIf(sname = sname, "", sname)
        dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 4) = i_baseline 'IIf(sname = sname, "", sname)
        ' End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GroupBox1.Visible = True
        txtpldestino.Text = txtcliente.Text
        txtplbultos.Text = txtbultos.Text()
        txtplpesoneto.Text = txtpeso.Text
    End Sub

    Private Sub ActualizaPackinglist(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnokpackinglst.Click
        If btnokpackinglst.Text = "Actualizar" Then
            Dim x As Integer
            x = MsgBox("Confirmar actualizar el packinglist", MsgBoxStyle.YesNo, "FIBRAFIL")
            If x = 6 Then
                Try
                    cmd.Parameters.Clear()
                    cmd.Connection = OCN
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "U_SP_FIB_UPD_CGUIA"

                    With cmd.Parameters
                        .Add(New SqlParameter("@RECORDKEY", SqlDbType.BigInt)).Value = DvCabecera.Item(posi.Position)("Recordkey").ToString  '
                        .Add(New SqlParameter("@U_STR_CONTENEDOR", SqlDbType.Text)).Value = txtcontenedor.Text ' 
                        .Add(New SqlParameter("@U_STR_NPRESCINTO", SqlDbType.Text)).Value = txtprescinto1.Text ' 
                        .Add(New SqlParameter("@U_FIB_PRESCINTO2", SqlDbType.Text)).Value = txtprescinto2.Text ' 
                        .Add(New SqlParameter("@U_FIB_NBULTOS", SqlDbType.BigInt)).Value = txtplbultos.Text
                        .Add(New SqlParameter("@U_FIB_KG", SqlDbType.Decimal)).Value = txtplpesoneto.Text  '
                        .Add(New SqlParameter("@pl_fecha", SqlDbType.SmallDateTime)).Value = dtpfpl.Value  ' 
                        .Add(New SqlParameter("@pl_embarque", SqlDbType.Text)).Value = txtplembarco.Text ' 
                        .Add(New SqlParameter("@pl_puerto1", SqlDbType.Text)).Value = txtplpuerto1.Text '

                        .Add(New SqlParameter("@paispldest", SqlDbType.Text)).Value = txtplpaisdest.Text '
                        .Add(New SqlParameter("@paisplori", SqlDbType.Text)).Value = txtpaisOri.Text '

                        .Add(New SqlParameter("@pl_puerto2", SqlDbType.Text)).Value = txtplpuerto2.Text '
                        .Add(New SqlParameter("@pl_fcharribo", SqlDbType.SmallDateTime)).Value = dtpplsalida.Value
                        .Add(New SqlParameter("@pl_fchsalida", SqlDbType.SmallDateTime)).Value = dtpplllegada.Value '
                        .Add(New SqlParameter("@pl_pesototbruto", SqlDbType.Decimal)).Value = txtplpesobruto.Text  '
                    End With
                    If OCN.State = ConnectionState.Closed Then OCN.Open()
                    cmd.ExecuteNonQuery()

                    With cmd1
                        .Parameters.Clear()
                        .Connection = OCN
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "U_SP_FIB_UPD_DGUIA"
                        .Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.BigInt))
                        .Parameters.Add(New SqlParameter("@Docnum", SqlDbType.BigInt))
                        .Parameters.Add(New SqlParameter("@LineNum", SqlDbType.BigInt))
                        .Parameters.Add(New SqlParameter("@Itemno", SqlDbType.Text))
                        .Parameters.Add(New SqlParameter("@U_FIB_PESO", SqlDbType.Decimal))
                        .Parameters.Add(New SqlParameter("@pl_pesobruto", SqlDbType.Decimal))

                        For k As Integer = 0 To Dvresumen.Count - 1
                            .Parameters("@RECORDKEY").Value = DvCabecera.Item(posi.Position)("Recordkey").ToString  '
                            .Parameters("@Docnum").Value = Label5.Text
                            .Parameters("@LineNum").Value = Dvresumen.Item(k)("LineNum")
                            .Parameters("@Itemno").Value = Dvresumen.Item(k)("Itemno")
                            .Parameters("@U_FIB_PESO").Value = Dvresumen.Item(k)("U_FIB_PESO")
                            .Parameters("@pl_pesobruto").Value = Dvresumen.Item(k)("pBruto")
                            .ExecuteNonQuery()
                        Next k
                    End With

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If

        GroupBox1.Visible = False
        btnokpackinglst.Text = "Aceptar"
    End Sub

    Private Sub btnPrintPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPL.Click
        Try
            ' If Label17.Text = "Y" Then ' Con esto verifico si se tiene la autorizacion para la impresion de la guia
            frmReport.NFORM = "PACKINGLIST"
            frmReport.KEYGUIA = DvCabecera.Item(posi.Position)("Recordkey").ToString
            Dim frm_report As New frmReport
            frm_report.Show()
            ' End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Actualizando_Packinglist(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtplembarco.KeyPress, txtplpuerto1.KeyPress, txtplpuerto2.KeyPress, dtpplsalida.KeyPress, dtpplllegada.KeyPress, txtcontenedor.KeyPress, txtplbultos.KeyPress, txtplpaisdest.KeyPress, txtplpesoneto.KeyPress, txtplpesobruto.KeyPress
        If btnConfirma.Text = "&Ok" Then
            btnokpackinglst.Text = "Actualizar"
        End If
    End Sub

    Private Sub txtpeso_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtpeso.MouseDoubleClick
        txtpeso.Text = SumarPesoCant("peso").ToString
    End Sub

    Private Sub txtbultos_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtbultos.MouseDoubleClick
        txtbultos.Text = SumarPesoCant("cant").ToString
    End Sub

    Function SumarPesoCant(ByVal Campo As String) As Decimal
        Dim Y As Integer
        Dim Pesocant As Decimal
        If Campo = "peso" Then
            For Y = 0 To DvDetalle.Count - 1 Step 1
                Pesocant = Pesocant + CDec(dtgRSMN.Item(Y, 7).ToString)
            Next
        Else
            For Y = 0 To DvDetalle.Count - 1 Step 1
                Pesocant = Pesocant + CDec(dtgRSMN.Item(Y, 6).ToString)
            Next
        End If
        Return Pesocant
    End Function

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

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()
            Case "Crear"
                GrabaGuia(5)
        End Select
    End Sub

    Private Sub btn_toPDA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_toPDA.Click
        GrabaGuia(2)
    End Sub

    Sub GrabaGuia(ByVal i_st As Integer)

        If OCN.State = ConnectionState.Closed Then OCN.Open()
        Try
            ' 2 enviado para el PDA
            ' 5 almacenamiento normal
            DatosInsUpd("U_SP_FIB_INS_GUIA", i_st)
            cmd.ExecuteNonQuery()

            'Graba el detalle de la guia
            If cmd.Parameters("@MSG").Value.ToString() = "" Then
                With cmd1
                    .Parameters.Clear()
                    .Connection = OCN
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "U_SP_FIB_INS_GUIA1"
                    .Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@Docnum", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@BaseEntry", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@BaseDoc", SqlDbType.Text))
                    .Parameters.Add(New SqlParameter("@LineNum", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@BaseLine", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@Itemno", SqlDbType.Text))
                    .Parameters.Add(New SqlParameter("@codebars", SqlDbType.Text))
                    .Parameters.Add(New SqlParameter("@U_FIB_PESO", SqlDbType.Decimal))
                    .Parameters.Add(New SqlParameter("@pl_pesobruto", SqlDbType.Decimal))
                    .Parameters.Add(New SqlParameter("@Qpedido", SqlDbType.Decimal))
                    .Parameters.Add(New SqlParameter("@Quantity", SqlDbType.Decimal))

                    For k As Integer = 0 To DvDetalle.Count - 1
                        .Parameters("@RECORDKEY").Value = cmd.Parameters("@FK").Value.ToString()
                        .Parameters("@Docnum").Value = Label5.Text
                        .Parameters("@BaseEntry").Value = i_keySAP
                        .Parameters("@BaseDoc").Value = s_TDOC
                        .Parameters("@LineNum").Value = DvDetalle.Item(k)("LineNum")
                        .Parameters("@BaseLine").Value = DvDetalle.Item(k)("BaseLine")
                        .Parameters("@Itemno").Value = DvDetalle.Item(k)("Itemno")
                        .Parameters("@Codebars").Value = DvDetalle.Item(k)("Codebars")
                        .Parameters("@U_FIB_PESO").Value = DvDetalle.Item(k)("U_FIB_PESO")
                        .Parameters("@pl_pesobruto").Value = DvDetalle.Item(k)("pbruto")
                        .Parameters("@Qpedido").Value = DvDetalle.Item(k)("Qpedido")
                        .Parameters("@Quantity").Value = DvDetalle.Item(k)("Quantity")
                        .ExecuteNonQuery()
                    Next k
                End With

                With cmd3
                    .Parameters.Clear()
                    .Connection = OCN
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "U_SP_UPD_RECORDIDGUIA"
                    .ExecuteNonQuery()
                End With


                'With cmdKARDEX
                '    .Parameters.Clear()
                '    .Connection = OCN
                '    .CommandType = CommandType.StoredProcedure
                '    .CommandText = "U_SP_FIB_INS_KARDEX"

                '    .Parameters.Add(New SqlParameter("@TIPOOP", SqlDbType.Text))
                '    .Parameters.Add(New SqlParameter("@FCHOPE", SqlDbType.SmallDateTime))
                '    .Parameters.Add(New SqlParameter("@NUMORI", SqlDbType.BigInt))
                '    .Parameters.Add(New SqlParameter("@NUMDOC", SqlDbType.BigInt))
                '    .Parameters.Add(New SqlParameter("@ITEMCODE", SqlDbType.VarChar))
                '    .Parameters.Add(New SqlParameter("@UM", SqlDbType.VarChar))
                '    .Parameters.Add(New SqlParameter("@QSini", SqlDbType.Decimal))
                '    .Parameters.Add(New SqlParameter("@QPlani", SqlDbType.Decimal))
                '    .Parameters.Add(New SqlParameter("@QProd", SqlDbType.Decimal))
                '    .Parameters.Add(New SqlParameter("@QSfin", SqlDbType.Decimal))

                '    For z As Integer = 0 To DvDetalle.Count - 1
                '        If DvDetalle.Item(z)("Itemno") <> "" Then
                '            .Parameters("@TIPOOP").Value = "O"                              ' Tipo Operacion I [IN entrada] O [OUT salida]
                '            .Parameters("@FCHOPE").Value = Date.Now                         ' Fecha operacion
                '            .Parameters("@NUMORI").Value = 0                                ' Numero de origen 
                '            .Parameters("@NUMDOC").Value = 99                               ' Numero de documento
                '            .Parameters("@ITEMCODE").Value = DvDetalle.Item(z)("Itemno")    ' Codigo de item
                '            .Parameters("@UM").Value = "UND"                                ' Unidad de medida de item (Obtenido en SP)
                '            .Parameters("@QSini").Value = 0                                 ' Saldo inicial (calculado en SP)
                '            .Parameters("@QPlani").Value = 0                                ' Cant. planificada
                '            .Parameters("@QProd").Value = DvDetalle.Item(z)("Quantity") * -1 ' Cant. producida
                '            .Parameters("@QSfin").Value = 0                                 ' Saldo
                '            .ExecuteNonQuery()
                '        End If
                '    Next
                'End With

                MsgBox("Guía creada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")

                btn_toPDA.Visible = False
                btnquery.Visible = False
                btn_trans.Visible = False

                Me.ContextMenuStrip = Me.ContextMenuStrip1
                btnConfirma.Text = "&Ok"

                Call LimpiaCajas(Me.TabControl1)

                Call Obtener_Data()
                Call LlenarData()
            Else
                MsgBox(cmd.Parameters("@MSG").Value.ToString(), MsgBoxStyle.Critical, "FIBRAFIL")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If OCN.State = ConnectionState.Closed Then OCN.Open()
        With cmd3
            .Parameters.Clear()
            .Connection = OCN
            .CommandType = CommandType.Text
            .CommandText = "Update OFIBCGUIA set STATE = 5 where RECORDKEY = " & Trim(Label5.Text)
            .ExecuteNonQuery()


            Creatxt()

            '' ************************** PROCESO DE MIGRACION MEDIANTE DTW *****************************'
            'Try
            Dim Command As New Process 'Creamos la instancia Process
            Command.StartInfo.FileName = "cmd.exe" 'El proceso en si es el CMD

            Command.StartInfo.Arguments = "/c " & Application.StartupPath & "\DTW\DTW.exe -s" & Application.StartupPath & "\Temp\Transfer_GR.xml"


            'Aqui le damos los parametros /c y el nombre del archivo a ejecutar
            'En tu caso sustituirias TextBox1.Text por "ndstool.exe -l game.nds"
            Command.StartInfo.RedirectStandardError = True 'Redirigimos los errores
            Command.StartInfo.RedirectStandardOutput = True 'Redirigimos la salida
            Command.StartInfo.UseShellExecute = False
            'Para redirigir la salida de este proceso esta propiedad debe ser false
            Command.StartInfo.CreateNoWindow = False
            'Para que no abra la ventana del CMD

            Command.Start()
            Dim Output As String = Command.StandardOutput.ReadToEnd() _
        & vbCrLf & Command.StandardError.ReadToEnd()



            If DvCabecera.Item(posi.Position)("State") <> 5 Then
                ''MsgBox("procede")
                With cmdKARDEX
                    .Parameters.Clear()
                    .Connection = OCN
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "U_SP_FIB_INS_KARDEX"

                    .Parameters.Add(New SqlParameter("@TIPOOP", SqlDbType.Text))
                    .Parameters.Add(New SqlParameter("@FCHOPE", SqlDbType.SmallDateTime))
                    .Parameters.Add(New SqlParameter("@NUMORI", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@NUMDOC", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@ITEMCODE", SqlDbType.VarChar))
                    .Parameters.Add(New SqlParameter("@UM", SqlDbType.VarChar))
                    .Parameters.Add(New SqlParameter("@QSini", SqlDbType.Decimal))
                    .Parameters.Add(New SqlParameter("@QPlani", SqlDbType.Decimal))
                    .Parameters.Add(New SqlParameter("@QProd", SqlDbType.Decimal))
                    .Parameters.Add(New SqlParameter("@QSfin", SqlDbType.Decimal))

                    For z As Integer = 0 To Dvresumen.Count - 1
                        If Dvresumen.Item(z)("Itemno") <> "" And Dvresumen.Item(z)("Quantity") <> 0 Then
                            .Parameters("@TIPOOP").Value = "O"                              ' Tipo Operacion I [IN entrada] O [OUT salida]
                            .Parameters("@FCHOPE").Value = Date.Now                         ' Fecha operacion
                            .Parameters("@NUMORI").Value = DvCabecera.Item(posi.Position)("RECORDKEY")                                ' Numero de origen 
                            .Parameters("@NUMDOC").Value = DvCabecera.Item(posi.Position)("DOCNUN")                               ' Numero de documento
                            .Parameters("@ITEMCODE").Value = Dvresumen.Item(z)("Itemno")    ' Codigo de item
                            .Parameters("@UM").Value = "" ' Unidad de medida de item (Obtenido en SP)
                            .Parameters("@QSini").Value = 0                                 ' Saldo inicial (calculado en SP)
                            .Parameters("@QPlani").Value = 0                                ' Cant. planificada
                            .Parameters("@QProd").Value = Dvresumen.Item(z)("Quantity") * -1 ' Cant. producida
                            .Parameters("@QSfin").Value = 0                                 ' Saldo
                            .ExecuteNonQuery()
                        End If
                    Next

                    Call Obtener_Data()

                End With
                ' Else
                '    MsgBox("Nao procede")
            End If
            OCN.Close()
        End With

       
    End Sub

    Private Sub ImprimirToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImprimirToolStripMenuItem1.Click
        'If PrintDialog1.ShowDialog = DialogResult.OK Then
        '    Dim dDataset, pDataSet As New DataSet
        '    Dim dap1 As New SqlClient.SqlDataAdapter("Select * from u_vw_guia where ID = '" & Trim(DvCabecera.Item(posi.Position)("Recordkey").ToString) & "'", OCN)

        '    dap1.SelectCommand.CommandType = CommandType.Text
        '    dap1.Fill(dDataset, "u_vw_guia")

        '    Dim pfs As New CrystalDecisions.Shared.ParameterFields
        '    Dim pf1 As New CrystalDecisions.Shared.ParameterField
        '    Dim pfDiscrete1 As New CrystalDecisions.Shared.ParameterDiscreteValue
        '    pDataSet = dDataset

        '    Dim Rpt As New ReportDocument
        '    'Rpt = New cr_ocp
        '    Rpt.FileName = Application.StartupPath & "\REPORTS\cr_guia.rpt"
        '    'Rpt.Load("reports\" + mrpt_name)
        '    Rpt.SetDataSource(pDataSet)
        '    Try
        '        Rpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
        '        Rpt.PrintToPrinter(1, False, 1, 1)

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_palm.Click
        Try
            frmReport.NFORM = "PackingAlmacen"
            frmReport.KEYGUIA = DvCabecera.Item(posi.Position)("Recordkey").ToString
            Dim frm_report As New frmReport
            frm_report.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub Creatxt()

        ' Dim dap1 As New SqlDataAdapter("select * from U_VW_EXPGUIA0 where CREATEFOR in ('CREATEFOR','" & Trim(Form1.vs_idUser) & "')", OCN)

        Dim dts_txt As New DataSet
        Dim DVTXT0, DVTXT1 As DataView
        Dim dap1 As New SqlDataAdapter("select * from Obtine_guia ('" & Trim(Label5.Text) & "')", OCN)
        Dim dap2 As New SqlDataAdapter("select * from Obtiene_detailguia ('" & Trim(Label5.Text) & "')", OCN)

        'Datos de cabecera de la guia
        dap1.SelectCommand.CommandType = CommandType.Text
        dap1.Fill(dts_txt, "TXT0")
        DVTXT0 = dts_txt.Tables("TXT0").DefaultView

        'Datos de detalle de la guia
        dap2.SelectCommand.CommandType = CommandType.Text
        dap2.Fill(dts_txt, "TXT1")
        DVTXT1 = dts_txt.Tables("TXT1").DefaultView



        If OCN.State = ConnectionState.Closed Then OCN.Open()

        Dim str0, str1 As New StringBuilder
        Dim i, j, x, y As Integer

        For i = 0 To dts_txt.Tables("TXT0").Rows.Count - 1
            For j = 0 To dts_txt.Tables("TXT0").Columns.Count - 1
                If j = dts_txt.Tables("TXT0").Columns.Count - 1 Then
                    If i <> dts_txt.Tables("TXT0").Rows.Count - 1 Then
                        cadenanombre0 = dts_txt.Tables("TXT0").Rows(i)(j).ToString() + vbCrLf
                        str0.Append(cadenanombre0)
                    End If
                Else
                    cadenanombre0 = dts_txt.Tables("TXT0").Rows(i)(j).ToString() + Chr(9)
                    str0.Append(cadenanombre0)
                End If
            Next
        Next


        For x = 0 To dts_txt.Tables("TXT1").Rows.Count - 1
            For y = 0 To dts_txt.Tables("TXT1").Columns.Count - 1
                If y = dts_txt.Tables("TXT1").Columns.Count - 1 Then
                    If x <> dts_txt.Tables("TXT1").Rows.Count - 1 Then
                        cadenanombre1 = dts_txt.Tables("TXT1").Rows(x)(y).ToString() + vbCrLf
                        str1.Append(cadenanombre1)
                    End If
                Else
                    cadenanombre1 = dts_txt.Tables("TXT1").Rows(x)(y).ToString() + Chr(9)
                    str1.Append(cadenanombre1)
                End If

            Next
        Next

        Try
            ' escribiendo la plantilla de cabecera de guía
            Dim FILE0, FILE1 As File
            If FILE0.Exists(Application.StartupPath & "\BATCH\GUIAS\GUIA0.txt") = True Then
                FILE0.Delete(Application.StartupPath & "\BATCH\GUIAS\GUIA0.txt")
                FILE1.Delete(Application.StartupPath & "\BATCH\GUIAS\GUIA1.txt")
            End If

            Dim writeFile0 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\BATCH\GUIAS\GUIA0.txt", True, System.Text.UnicodeEncoding.Unicode)
            writeFile0.WriteLine(str0.ToString)
            writeFile0.Flush()
            writeFile0.Close()
            writeFile0 = Nothing

            ' escribiendo la plantilla de detalle de guía
            Dim writeFile1 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\BATCH\GUIAS\GUIA1.txt", True, System.Text.UnicodeEncoding.Unicode)
            writeFile1.WriteLine(str1.ToString)
            writeFile1.Flush()
            writeFile1.Close()
            writeFile1 = Nothing

        Catch ex As IOException
            MsgBox(ex.ToString)
            OCN.Close()
        End Try
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_trans.Click

        If GroupBox3.Visible = True Then
            GroupBox3.Visible = False
        Else
            GroupBox3.Visible = True
            Dim dts_trans As New DataSet
            Dim cmd_trans As New SqlCommand("SELECT     Name AS 'Nombre', Licencia, VehAsig AS 'Vehiculo', Placa FROM OFIBTRANS", OCN)
            cmd_trans.CommandType = CommandType.Text

            Dim dap99 As New SqlDataAdapter
            dap99.SelectCommand = cmd_trans

            dap99.Fill(dts_trans, "vListaTrans")

            DvdetalleListmat = dts_trans.Tables("vListaTrans").DefaultView
            'Dvstop = dts_trans.Tables("vListaTrans").DefaultView
            Dim Dvstop As DataView = DvdetalleListmat

            DataGrid3.DataSource = DvdetalleListmat
            DvdetalleListmat.AllowNew = False
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim X As Integer
        X = DataGrid3.CurrentRowIndex

        txtplaca.Text = DvdetalleListmat.Item(X)("placa").ToString()
        txtconductor.Text = DvdetalleListmat.Item(X)("nombre").ToString()
        txtlicencia.Text = DvdetalleListmat.Item(X)("licencia").ToString()

        Me.GroupBox3.Visible = False
    End Sub
End Class

