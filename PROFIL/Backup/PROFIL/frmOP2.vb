Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports PROFIL.MForm
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Imports Excel = Microsoft.Office.Interop.Excel
'Imports PROFIL.Code128

'RS232
Imports System.IO.Ports
Imports System.Configuration


Public Class frmOP2

    Dim FILE0, FILE1 As File
    Dim C_C128 As New Code128
    Public Shared scode, sname, sumed, swhs, scodebar, scode0, sname0, swhs0, sumed0 As String

    Public Shared NQ As Integer 'NUMERO DE CONSULTA 
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros
    Private bsource As BindingSource = New BindingSource()
    Dim WithEvents FQ As New frmConsulta
    Dim dts As New DataSet
    Dim dt_correlativo As New DataTable
    Dim cmd, cmd1, cmd2, cmd3, cmdKARDEX As New SqlCommand()
    Dim id, cadenanombre0, cadenanombre1, cadenacodebar, cadenaPESO As String ' Para el armado del contenido del TXT
    Private DvCabecera, DvDetalle, Dvresumen, Dvstop, Dvlistamat, DVTXT0, DVTXT1 As DataView
    Dim _dsdetalle, dtsStatus As New DataSet
    Event PasaVars()
    'Lectura de datos desde la balanza mediante puerto RS232
    Private miComPort As Integer
    Private WithEvents moRS232 As Rs232

    Dim buffer As String

    'Private mlTicks As Long
    'Private Delegate Sub CommEventUpdate(ByVal source As Rs232, ByVal mask As Rs232.EventMasks)
    ''********************** Para ajustar el formato de impresion de etiquetas **************************''
    '  Private WithEvents printButton As System.Windows.Forms.Button
    ' Private printFont As Font
    'Private streamToPrint As StreamReader
    'Genera la imagen para el codigo EAN13
    Dim bm As Bitmap = Nothing

    'RS232
    Delegate Sub SetTextCallback(ByVal text As String)
    Dim DataIn As String = String.Empty


    Private Enum eTiposCodigo
        Ean13
        Ean13Nochecksum
    End Enum

    Private ActualCode As eTiposCodigo

    Private Sub frmOrdprod_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If CierraForms("una", Me.Text, btnConfirma.Text) = False Then
            If Not moRS232 Is Nothing Then
                ' Deshabilita el evento si es que estuviese activo
                moRS232.DisableEvents()
                If moRS232.IsOpen Then moRS232.Close()
                e.Cancel = False
            End If
        Else
            e.Cancel = True
        End If
    End Sub
    'metodo

    'Declarar el evento disparo fin de recepcion    
    Public Event RxFin(ByVal Trama As String)
    '    
    'String de recepcion utilizado como buffer    
    Private PortSerie_Recepcion As String = ""
 
    Private Sub frmOrdprod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Anadir el manipulador de recepcion en la sub New, Load…    
        AddHandler sppuerto.DataReceived, AddressOf Rx

        Call Obtener_Data()
        Call LlenarData()
        'Call DibujaEAN13()

        Call ArmaGridRSM()
        Call ArmaGrid()
        Call ArmaGridSTP()

    End Sub

    Public Sub Recibevar() Handles FQ.PasaVars
        Try

            '-----> Obtiene el producto principal a producir <-----'
            If NQ = 1 Then
                TextBox6.Text = -1
                TextBox1.Text = scode
                TextBox5.Text = sname
                TextBox2.Text = scodebar
                lblUM.Text = sumed


                Dim cmd As New SqlCommand("U_SP_LISTDETLMAT", OCN)
                cmd.CommandType = CommandType.StoredProcedure

                Dim dap99 As New SqlDataAdapter
                dap99.SelectCommand = cmd

                Dim parm As New SqlParameter("@Itemcodepa", SqlDbType.VarChar)
                parm.Value = TextBox1.Text
                cmd.Parameters.Add(parm)
                dap99.Fill(dts, "vListaMat")
                Dim DvdetalleListmat As DataView

                DvdetalleListmat = dts.Tables("vListaMat").DefaultView
                Dim x As Integer

                For x = 0 To 25
                    dtgCMPT.Item(x, 3) = ""
                    dtgCMPT.Item(x, 4) = ""
                    dtgCMPT.Item(x, 5) = ""
                Next
                For x = 0 To DvdetalleListmat.Count - 1
                    dtgCMPT.Item(x, 3) = DvdetalleListmat.Item(x)("ITEMCODE").ToString
                    dtgCMPT.Item(x, 4) = DvdetalleListmat.Item(x)("ITEMNAME").ToString
                    dtgCMPT.Item(x, 5) = DvdetalleListmat.Item(x)("INVENTORYUOM").ToString
                Next
                dts.Tables("vListaMat").Clear()

            End If

            '-----> Obtiene el producto componetep a producir <-----'
            If NQ = 2 Then
                dtgCMPT.Item(dtgCMPT.CurrentRowIndex, 3) = IIf(scode0 = scode, "", scode0)
                dtgCMPT.Item(dtgCMPT.CurrentRowIndex, 4) = IIf(scode0 = scode, "", sname0)
                dtgCMPT.Item(dtgCMPT.CurrentRowIndex, 5) = IIf(scode0 = scode, "", sumed0)
                dtgCMPT.CurrentCell = New DataGridCell(dtgCMPT.CurrentRowIndex, 6)
            End If

            If sumed = "KG" Then
                lblUM.Location = New Point(lblUM.Location.X, TextBox8.Location.Y)
            Else
                lblUM.Location = New Point(lblUM.Location.X, TextBox9.Location.Y)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub Obtener_Data()
        Dim dap1 As New SqlDataAdapter("U_SP_SELCMPTOP", OCN)
        dap1.SelectCommand.CommandType = CommandType.StoredProcedure
        dap1.Fill(dts, "vLIST1")

        DvDetalle = dts.Tables("vLIST1").DefaultView
        DvDetalle.AllowEdit = False
        DvDetalle.AllowNew = False

        Dim dap2 As New SqlDataAdapter("U_SP_SELDETRSM", OCN)
        dap2.SelectCommand.CommandType = CommandType.StoredProcedure
        dap2.Fill(dts, "vLIST2")

        Dvresumen = dts.Tables("vLIST2").DefaultView
        Dvresumen.AllowEdit = False
        Dvresumen.AllowNew = False

        Dim dap3 As New SqlDataAdapter("U_SP_SELDETSTP", OCN)
        dap3.SelectCommand.CommandType = CommandType.StoredProcedure
        dap3.Fill(dts, "vLIST3")

        Dvstop = dts.Tables("vLIST3").DefaultView
        Dvstop.AllowEdit = False
        Dvstop.AllowNew = False


        Dim dap4 As New SqlDataAdapter("U_SP_LISTPROD", OCN)
        dap4.SelectCommand.CommandType = CommandType.StoredProcedure
        dap4.Fill(dts, "vOP")

        'DataGrid1.DataSource = dts.Tables("vOP").DefaultView

        DvCabecera = dts.Tables("vOP").DefaultView
        DvCabecera.AllowEdit = False
        DvCabecera.AllowNew = False

        'recordid = Trim(Label15.Text)
        posi = CType(BindingContext(dts.Tables("vop")), CurrencyManager)
        posi.Position = 0
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)

        Try
            If DvDetalle.Count > 0 Then
                DvDetalle.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))
                Dvresumen.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))
                Dvstop.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim Xml0, Xml1 As XmlDocument
        Dim NodeList As XmlNodeList
        Dim Node, Node1 As XmlNode

        'Try
        LlenarDS()
        Xml0 = New XmlDocument()
        Xml0.Load(Application.StartupPath & "\setTproduction.xml") '
        Node = Xml0.FirstChild
        ' Xml0 = Xml0.DocumentElement
        NodeList = Xml0.SelectNodes("/typeproduction/type")
        For Each Node In NodeList
            Dim rw As DataRow = _dsdetalle.Tables(0).NewRow
            rw("tptipo") = Node.Attributes.GetNamedItem("tpnombre").Value
            rw("tpval") = Node.Attributes.GetNamedItem("tpval").Value
            _dsdetalle.Tables(0).Rows.Add(rw)
        Next
        ComboBox6.DisplayMember = "tptipo"
        ComboBox6.ValueMember = "tpval"
        ComboBox6.DataSource = _dsdetalle.Tables(0)

        'LlenarDS1()
        'Xml1 = New XmlDocument()
        'Xml1.Load(Application.StartupPath & "\setSproduction.xml") '
        'NodeList = Xml1.SelectNodes("/statusproduction/status")
        'For Each Node1 In NodeList
        '    Dim rwst As DataRow = dtsStatus.Tables(0).NewRow
        '    rwst("stpnombre") = Node1.Attributes.GetNamedItem("Stpnombre").Value
        '    rwst("Stpval") = Node1.Attributes.GetNamedItem("Stpval").Value
        '    dtsStatus.Tables(0).Rows.Add(rwst)
        'Next
        'ComboBox1.DisplayMember = "Stpnombre"
        'ComboBox1.ValueMember = "Stpval"
        'ComboBox1.DataSource = dtsStatus.Tables(0)

        Try
            If OCN.State = ConnectionState.Closed Then
                OCN.Open()
            End If
            'DATA

            Dim dap As New SqlDataAdapter("U_SP_LISTPLANI", OCN)
            dap.SelectCommand.CommandType = CommandType.StoredProcedure
            dap.Fill(dts, "vplan")

            'COMBOBOX OPERARIO'
            Dim da_ope As New SqlClient.SqlDataAdapter("Select T0.Code, T0.Name from OFIBEMPL T0 inner join OFIBAREA T1 on T0.U_FIB_AREA = T1.CODE where T0.u_FIB_CARGO  = 'O' and T1.PP='Y' AND ISNULL(T0.INACTIVO,'N')<>'Y'", OCN)
            Dim ds As New DataSet()
            da_ope.Fill(ds)
            ComboBox5.DisplayMember = "Name"
            ComboBox5.ValueMember = "Code"
            ComboBox5.DataSource = ds.Tables(0)

            'COMBOBOX AYUDANTE'
            Dim dap_mac As New SqlClient.SqlDataAdapter("Select T0.Code, T0.Name from OFIBEMPL T0 inner join OFIBAREA T1 on T0.U_FIB_AREA = T1.CODE where T0.u_FIB_CARGO  = 'A' and T1.PP='Y' AND ISNULL(T0.INACTIVO,'N')<>'Y'", OCN)
            dap_mac.Fill(dts, "Ayudante")
            ComboBox4.DataSource = dts.Tables("Ayudante")
            ComboBox4.DisplayMember = "Name"
            ComboBox4.ValueMember = "Code"
            'posi = CType(BindingContext(dts.Tables("vplan")), CurrencyManager)
            'posi.Position = posi.Count + 1
            'txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Sub LlenarData()

        Label15.DataBindings.Add("text", dts.Tables("vop"), "Recordkey")
        TextBox1.DataBindings.Add("text", dts.Tables("vop"), "codigo")
        Label6.DataBindings.Add("text", dts.Tables("vop"), "Serie")
        TextBox7.DataBindings.Add("text", dts.Tables("vop"), "Docnum")
        TextBox6.DataBindings.Add("text", dts.Tables("vop"), "Norigen")
        DateTimePicker1.DataBindings.Add("text", dts.Tables("vop"), "Fecha")
        DateTimePicker2.DataBindings.Add("text", dts.Tables("vop"), "Venc")
        TextBox4.DataBindings.Add("text", dts.Tables("vop"), "Cantidad")
        TextBox5.DataBindings.Add("text", dts.Tables("vop"), "item")
        TextBox9.DataBindings.Add("text", dts.Tables("vop"), "Qprod")
        TextBox8.DataBindings.Add("text", dts.Tables("vop"), "Wprod")
        txtScrap.DataBindings.Add("text", dts.Tables("vop"), "Scrap")
        txtSaldos.DataBindings.Add("text", dts.Tables("vop"), "Saldo")
        TextBox3.DataBindings.Add("text", dts.Tables("vop"), "peso")
        TextBox2.DataBindings.Add("text", dts.Tables("vop"), "codebar")

        DataGrid1.SetDataBinding(dts.Tables("vop"), "")

        dtgCMPT.SetDataBinding(DvDetalle, "")
        'dtgRSMN.SetDataBinding(Dvresumen, "")
        dtgSTP.SetDataBinding(Dvstop, "")
        DataGridView1.DataSource = Dvresumen

        Dim CboLocalidadesColumn As New DataGridViewComboBoxColumn()
        CboLocalidadesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        CboLocalidadesColumn.DataPropertyName = "Combobox"
        CboLocalidadesColumn.Name = "Distrito"
        CboLocalidadesColumn.HeaderText = "Scrap / Saldo"
        Me.DataGridView1.Columns.Add(CboLocalidadesColumn)
        '--------------
        Dim c_objds As New DataSet()
        Dim objda As New SqlDataAdapter("SELECT ID, [Desc]  FROM OFIBRSTP", OCN)

        'De la misma forma pasamos la consulta al(DataSet)
        objda.Fill(c_objds, "ID")
        objda.Fill(c_objds, "Desc")

        'Ahora esta consulta la pasamos a un         DataGridViewComboBoxColumn()
        CboLocalidadesColumn.DataSource = c_objds.Tables(0).DefaultView
        CboLocalidadesColumn.DisplayMember = "Desc"
        CboLocalidadesColumn.ValueMember = "ID"

        ' Me.DataGridView1.Columns.RemoveAt(0)
        'Me.DataGridView1.Columns.Insert(9, CboLocalidadesColumn)

    End Sub

    Private Sub LlenarDS()
        'Carga combobox tipo de produccion
        _dsdetalle = New DataSet
        _dsdetalle.Tables.Add(0)
        With _dsdetalle.Tables(0).Columns
            .Add("tpTIPO", System.Type.GetType("System.String"))
            .Add("tpVAL", System.Type.GetType("System.String"))
        End With
    End Sub

    Private Sub LlenarDS1()
        ' Cargar combobox estado de produccion
        dtsStatus = New DataSet
        dtsStatus.Tables.Add(0)
        With dtsStatus.Tables(0).Columns
            .Add("stpnombre", System.Type.GetType("System.String"))
            .Add("Stpval", System.Type.GetType("System.String"))
        End With
    End Sub

    Private Sub BotonConsulta(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim formQuery As New frmConsulta
        frmConsulta.id_object = 1
        cVars.NQP = 2 'CONSULTA PARA ITEMS
        NQ = 1
        formQuery.ShowDialog()
        Call Recibevar()
    End Sub

    Private Sub AddNewDataRowView()
        ' Evento llamado por el procedimiento NUEVO 
        DvDetalle = dts.Tables("vLIST1").DefaultView
        DvDetalle.AllowEdit = True
        DvDetalle.AllowNew = True

        DvDetalle.AddNew()
        DvDetalle.RowFilter = "RECORDKEY = -1"
        dtgCMPT.FlatMode = False
        dtgCMPT.SetDataBinding(Nothing, Nothing)
        dtgCMPT.SetDataBinding(DvDetalle, "")
        For X As Int32 = 0 To 25
            Dim rowView As DataRowView = DvDetalle.AddNew
            rowView("RECORDKEY") = "-1"
            rowView("LineNum") = X
            rowView("CODIGO") = ""
            rowView("DESCRIPCION") = ""
            rowView("UOM") = ""
            rowView("CANTIDAD") = 0
            ' rowView("ALMACEN") = 0
            rowView("PLANIFICADO") = 0
            rowView.EndEdit()
        Next X

        '----------------'

        Dvresumen = dts.Tables("vLIST2").DefaultView
        Dvresumen.AllowEdit = True
        Dvresumen.AllowNew = True

        ' Dvresumen.AddNew()
        Dvresumen.RowFilter = "RecordKey = -1"

        For X As Int32 = 0 To 249
            Dim rowView As DataRowView = Dvresumen.AddNew
            rowView("RecordKey") = "-1"
            rowView("LineNum") = X
            rowView("BOBINA") = ""
            rowView("ANCHO") = 0
            rowView("COLOR") = ""
            rowView("ESPESOR") = 0
            rowView("PESO") = 0
            rowView("CODEBAR") = ""
            rowView("COMMENT") = ""
            rowView.EndEdit()
        Next X

        '----------'

        Dvstop = dts.Tables("vLIST3").DefaultView
        Dvstop.AllowEdit = True
        Dvstop.AllowNew = True

        Dvstop.RowFilter = "RecordKey = -1"
        dtgSTP.SetDataBinding(Nothing, Nothing)
        dtgSTP.SetDataBinding(Dvstop, "")
        For X As Int32 = 0 To 9 '''''''''''''''''
            Dim rowView As DataRowView = Dvstop.AddNew
            rowView("RecordKey") = "-1"
            rowView("Docnum") = 0 ' TextBox6.Text
            rowView("LineNum") = X
            rowView("HoraIni") = DBNull.Value
            rowView("HoraFin") = DBNull.Value
            rowView("Comments") = ""
            rowView.EndEdit()
        Next X

        Dvstop.AllowNew = False
        Dvstop.AllowNew = False
        OCN.Close()
        'ArmaGrid()
        'ArmaGridRSM()
        

    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        btnConfirma.Text = "Crear"
        VisibilidadBotones(True)
        Me.ContextMenuStrip = ContextMenuStrip2

        Dvresumen = dts.Tables("vLIST2").DefaultView
        Dvresumen.AllowEdit = True
        Dvresumen.AllowNew = True

        AddNewDataRowView()

     


        Limpiar_TextBox_Tabs(Me, TabControl1)

        Label6.ForeColor = Color.Red
        Label6.Text = "NP" ' No planificado

        DateTimePicker1.Value = Now.Date
        DateTimePicker2.Value = DateAdd(DateInterval.Day, 0, Now.Date)

        Dim da_docnum As New SqlClient.SqlDataAdapter("select Isnull(Max(Docnum)+1,1) As DOCNUM from OPROFIB where serie = " & "'NP' and  year(Postingdate)= " & Year(DateTimePicker1.Value), OCN)
        da_docnum.SelectCommand.CommandType = CommandType.Text

        Dim ds As New DataSet()
        da_docnum.Fill(ds, "vDocnum")
        TextBox7.DataBindings.Add("text", ds.Tables("vDocnum"), "DOCNUM")

        Dim da_plandia As New SqlClient.SqlDataAdapter("select Id_item As Item, id_oper As Operario, Id_Ayud As Ayudante , ItemName As 'DscItem' from ofibplandia inner join SBO_FIBRAFIL..OITM on Id_item = ItemCode  where Id_mac = " & Form1.vs_idMac, OCN)
        da_plandia.SelectCommand.CommandType = CommandType.Text
        da_plandia.Fill(ds, "vplandia")

        TextBox1.DataBindings.Add("text", ds.Tables("vplandia"), "Item")
        TextBox5.DataBindings.Add("text", ds.Tables("vplandia"), "DscItem")
        ComboBox5.DataBindings.Add("selectedvalue", ds.Tables("vplandia"), "Operario")
        ComboBox4.DataBindings.Add("selectedvalue", ds.Tables("vplandia"), "Ayudante")


        NQ = 1
        scode = TextBox1.Text
        sname = TextBox5.Text
        Recibevar()


        Dim cmd As New SqlCommand("u_sp_correlativos", OCN)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@obj", SqlDbType.Int)).Value = 1 ' Cod 
        End With

        Dim da_Cordia As New SqlDataAdapter(cmd)


        da_Cordia.Fill(dt_correlativo)
        bsource.DataSource = dt_correlativo

        da_Cordia.Fill(ds, "Cordia")


        Me.Label19.DataBindings.Add("Text", bsource, "cordia", True)

        txtSaldos.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        txtScrap.Text = 0
        TextBox8.Text = 0
        TextBox9.Text = 0
        DataGridView1.DataSource = Dvresumen
        DataGridView1_Formato()


    End Sub




    'MODIFICADO PARA EL DATAGRIDVIEW OBTENCION DE CODIGO DE BARRAS, REVISAR FUNCIONAMIENTO
    Sub dtgRSMN_KeyPress()
        If btnConfirma.Text = "Crear" Then
            ' se añadira unas lineas de codigo donde verifiquen la  existencia de los archivos temporales de produccion 
            ' de existir alguno a una produccion anterior se eliminará y volverá a generar para empezar a cargar 
            ' los items adicionados en cada pesada.

            'Try
            '    If FILE0.Exists(Application.StartupPath & "\temp\OP_CAB.txt") = False Then

            '        Dim writeFile0 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\temp\OP_CAB.txt", True, System.Text.UnicodeEncoding.Unicode)
            '        writeFile0.Flush()
            '        writeFile0.Close()
            '        writeFile0 = Nothing
            '    End If
            '    If FILE1.Exists(Application.StartupPath & "\temp\OP_DET.txt") = False Then

            '        Dim writeFile0 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\temp\OP_DET.txt", True, System.Text.UnicodeEncoding.Unicode)
            '        writeFile0.Flush()
            '        writeFile0.Close()
            '        writeFile0 = Nothing
            '    End If
            'Catch ex As IOException
            '    MsgBox(ex.ToString)
            'End Try

            Dim DD, MM, AA, LINE, PESO, DEC, ENT, sSuma As String

            ' Asignacion de la variable de 02 digitos para el día
            If (DateTimePicker1.Value.Day).ToString.Length = 1 Then
                DD = "0" + CStr(DateTimePicker1.Value.Day)
            Else
                DD = CStr(DateTimePicker1.Value.Day)
            End If

            ' Asignacion de la variable de 02 digitos para el mes
            If (DateTimePicker1.Value.Month).ToString.Length = 1 Then
                MM = "0" + CStr(DateTimePicker1.Value.Month)
            Else
                MM = CStr(DateTimePicker1.Value.Month)
            End If

            ' Asignacion de la variable de 02 digitos para el año
            AA = CStr(DateTimePicker1.Value.Year).Substring(2, 2)

            PESO = DataGridView1.Item(7, DataGridView1.CurrentRow.Index).Value.ToString

            If PESO.Contains(".") = True Then
                'Obtiene la parte entera 
                ENT = PESO.Substring(0, PESO.IndexOf("."))

                'Obtiene la parte decimal solo 02 dígitos
                Try
                    DEC = PESO.Substring(PESO.IndexOf(".") + 1, 2)
                Catch ex As Exception
                    DEC = PESO.Substring(PESO.IndexOf(".") + 1, 1)
                    DEC = DEC & "0"
                End Try


                PESO = ENT + "" + DEC

                Select Case PESO.Length
                    Case 3
                        PESO = "00" + PESO
                    Case 4
                        PESO = "0" + PESO
                    Case 5
                        PESO = PESO
                End Select
            Else
                Select Case PESO.Length
                    Case 1
                        PESO = "00" + PESO + "00"
                    Case 2
                        PESO = "0" + PESO + "00"
                    Case 3
                        PESO = PESO + "00"
                End Select

            End If

            'Validacion de # de digitos en el docnum

            Select Case DataGridView1.CurrentRow.Index.ToString.Length 'dtgRSMN.CurrentRowIndex.ToString().Length '
                Case 1
                    LINE = "0" + DataGridView1.CurrentRow.Index.ToString 'dtgRSMN.CurrentRowIndex.ToString()
                Case 2
                    LINE = DataGridView1.CurrentRow.Index.ToString 'dtgRSMN.CurrentRowIndex.ToString()
            End Select

            'If DataGridView1.CurrentCell.Value.ToString = "7" Then '(dtgRSMN.CurrentCell.ColumnNumber = 6) Then" Then
            '  dtgRSMN.Item(0, 4).ToString()
            DataGridView1.Item(8, DataGridView1.CurrentRow.Index).Value = LINE + AA + MM + DD + Trim(Label19.Text) + PESO 'dtgRSMN.Item(dtgRSMN.CurrentRowIndex, 7) = LINE + AA + MM + DD + sSuma + PESO
            'End If
        End If

        Dim dc_pesototal As Decimal
        Dim i_canttotal, x As Integer

        For x = 0 To 249
            dc_pesototal = dc_pesototal + CDec(DataGridView1.Item(7, x).Value.ToString)
            If DataGridView1.Item(7, x).Value.ToString <> 0 Then
                i_canttotal = i_canttotal + 1
            End If
        Next
        TextBox8.Text = dc_pesototal.ToString
        TextBox9.Text = i_canttotal.ToString
        GeneraTemp()
    End Sub

    Private Sub btnCerrar(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()
            Case "&Actualizar"
                DatosInsUpd("U_SP_FIB_UPD_OPFIB")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Orden de produccion actualizada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
                    VisibilidadBotones(False)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    OCN.Close()
                End Try
            Case "Crear"
                btnConfirma.Enabled = False
                Dim s_msg As String = ""

                If CCC(s_msg) = "" Then

                    Calculasaldoscrap()
                    DatosInsUpd("U_SP_FIB_INS_OPCAB") '

                    If OCN.State = ConnectionState.Closed Then OCN.Open()
                    ' Try
                    cmd.ExecuteNonQuery()
                    If cmd.Parameters("@msg").Value.ToString() = "" Then
                        ' Graba el detalle del resumen de la orden de produccion
                        With cmd2
                            .Parameters.Clear()
                            .Connection = OCN
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "U_SP_FIB_INS_OPRSM"
                            .Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@Docnum", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@LineNum", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@BobinaN", SqlDbType.VarChar))
                            .Parameters.Add(New SqlParameter("@Ancho", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@Color", SqlDbType.VarChar))
                            .Parameters.Add(New SqlParameter("@Espesor", SqlDbType.VarChar))
                            .Parameters.Add(New SqlParameter("@PesoBob", SqlDbType.Decimal))
                            .Parameters.Add(New SqlParameter("@CODEBAR", SqlDbType.VarChar))
                            .Parameters.Add(New SqlParameter("@COMMENT", SqlDbType.VarChar))
                            .Parameters.Add(New SqlParameter("@TIPPROD", SqlDbType.Int))


                            For k As Integer = 0 To Dvresumen.Count - 1
                                .Parameters("@RECORDKEY").Value = cmd.Parameters("@FK").Value.ToString
                                .Parameters("@Docnum").Value = TextBox7.Text
                                .Parameters("@LineNum").Value = CInt(Dvresumen.Item(k)("LineNum"))
                                .Parameters("@BobinaN").Value = Dvresumen.Item(k)("BOBINA")
                                .Parameters("@Ancho").Value = Dvresumen.Item(k)("ANCHO")
                                .Parameters("@Color").Value = Dvresumen.Item(k)("COLOR")
                                .Parameters("@Espesor").Value = Dvresumen.Item(k)("ESPESOR")
                                .Parameters("@PesoBob").Value = Dvresumen.Item(k)("PESO")
                                .Parameters("@CODEBAR").Value = Dvresumen.Item(k)("codebar")
                                .Parameters("@COMMENT").Value = Dvresumen.Item(k)("comment")
                                .Parameters("@TIPPROD").Value = Dvresumen.Item(k)("TIPPROD")
                                .ExecuteNonQuery()
                            Next k
                        End With

                        'Grabando el detalle de los componentes de la OP
                        If Form1.vs_Area <> "Telares" Then
                            With cmd
                                .Parameters.Clear()
                                .Connection = OCN
                                .CommandType = CommandType.StoredProcedure
                                .CommandText = "U_SP_FIB_INS_OPCMP"
                                .Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.Int))
                                .Parameters.Add(New SqlParameter("@Docnum", SqlDbType.Int))
                                .Parameters.Add(New SqlParameter("@LineNum", SqlDbType.Int))
                                .Parameters.Add(New SqlParameter("@ItemNo", SqlDbType.VarChar))
                                .Parameters.Add(New SqlParameter("@BaseformKG", SqlDbType.VarChar))
                                .Parameters.Add(New SqlParameter("@BaseQuantity", SqlDbType.Decimal))
                                .Parameters.Add(New SqlParameter("@PlannedQuantity", SqlDbType.Decimal))

                                For k As Integer = 0 To DvDetalle.Count - 1
                                    .Parameters("@RECORDKEY").Value = -99
                                    .Parameters("@Docnum").Value = TextBox7.Text
                                    .Parameters("@LineNum").Value = CInt(DvDetalle.Item(k)("LineNum"))
                                    .Parameters("@ItemNo").Value = DvDetalle.Item(k)("CODIGO")
                                    .Parameters("@BaseformKG").Value = DvDetalle.Item(k)("UOM")
                                    .Parameters("@BaseQuantity").Value = CDec(DvDetalle.Item(k)("Planificado"))
                                    .Parameters("@PlannedQuantity").Value = CDec(DvDetalle.Item(k)("CANTIDAD"))
                                    .ExecuteNonQuery()
                                Next k
                            End With
                        Else
                            ' Aqui graba los componentes ocultos y determinados por la vista creada 
                            With cmd
                                .Parameters.Clear()
                                .Connection = OCN
                                .CommandType = CommandType.StoredProcedure
                                .CommandText = "U_SP_INS_CMPNTHIDE"
                                .Parameters.Add(New SqlParameter("@Num", SqlDbType.Int)).Value = TextBox7.Text 'Docnum
                                .Parameters.Add(New SqlParameter("@Item", SqlDbType.VarChar)).Value = TextBox1.Text ' item
                                .Parameters.Add(New SqlParameter("@Cant", SqlDbType.Decimal)).Value = TextBox9.Text  'Cant
                                .ExecuteNonQuery()
                            End With

                        End If
                        'Graba el detalle de las paradas de máquina
                        With cmd1
                            .Parameters.Clear()
                            .Connection = OCN
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "U_SP_FIB_INS_OPSTP"
                            .Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@Docnum", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@LineNum", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@HoraIni", SqlDbType.DateTime))
                            .Parameters.Add(New SqlParameter("@HoraFin", SqlDbType.DateTime))
                            .Parameters.Add(New SqlParameter("@Comments", SqlDbType.VarChar))

                            For k As Integer = 0 To Dvstop.Count - 1
                                .Parameters("@RECORDKEY").Value = -99
                                .Parameters("@Docnum").Value = TextBox7.Text
                                .Parameters("@LineNum").Value = CInt(Dvstop.Item(k)("LineNum"))
                                .Parameters("@HoraIni").Value = Dvstop.Item(k)("Horaini")
                                .Parameters("@HoraFIn").Value = Dvstop.Item(k)("Horafin")
                                .Parameters("@Comments").Value = Dvstop.Item(k)("Comments")
                                .ExecuteNonQuery()
                            Next k
                        End With



                        With cmd
                            .Parameters.Clear()
                            .Connection = OCN
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "U_SP_FIB_UPD_STAPLAN"
                            .Parameters.Add(New SqlParameter("@numop", SqlDbType.Int)).Value = TextBox6.Text
                            .ExecuteNonQuery()
                        End With    'OFIBPLAN

                        With cmd3
                            .Parameters.Clear()
                            .Connection = OCN
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "U_SP_UPD_RECORDIDOP"
                            .ExecuteNonQuery()
                        End With

                        Try
                            With cmdKARDEX
                                .Parameters.Clear()
                                .Connection = OCN
                                .CommandType = CommandType.StoredProcedure
                                .CommandText = "U_SP_FIB_INS_KARDEX"

                                .Parameters.Add(New SqlParameter("@TIPOOP", SqlDbType.Text)).Value = "I" ' Tipo Operacion I [IN entrada] O [OUT salida]
                                .Parameters.Add(New SqlParameter("@FCHOPE", SqlDbType.SmallDateTime)).Value = DateTimePicker1.Value ' Fecha operacion
                                .Parameters.Add(New SqlParameter("@NUMORI", SqlDbType.BigInt)).Value = 0 'Numerode origen 
                                .Parameters.Add(New SqlParameter("@NUMDOC", SqlDbType.BigInt)).Value = TextBox7.Text 'Numero de documento
                                .Parameters.Add(New SqlParameter("@ITEMCODE", SqlDbType.VarChar)).Value = TextBox1.Text ' Codigo de item
                                .Parameters.Add(New SqlParameter("@UM", SqlDbType.VarChar)).Value = "KG" 'Unidad de medida de item (Obtenido en SP)
                                .Parameters.Add(New SqlParameter("@QSini", SqlDbType.Decimal)).Value = 0 ' Saldo inicial (calculado en SP)
                                .Parameters.Add(New SqlParameter("@QPlani", SqlDbType.Decimal)).Value = 0 'Cant. planificada
                                .Parameters.Add(New SqlParameter("@QProd", SqlDbType.Decimal)).Value = CDec(TextBox9.Text) ' Cant. producida
                                .Parameters.Add(New SqlParameter("@QSfin", SqlDbType.Decimal)).Value = 0 ' Saldo final (calculado en SP)
                                .ExecuteNonQuery()
                            End With
                            'Catch ex As Exception
                            '    MsgBox(ex.Message)
                            'End Try

                            MsgBox("Orden de producción creada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")

                            If Not moRS232 Is Nothing Then
                                moRS232.DisableEvents()
                                If moRS232.IsOpen Then moRS232.Close()
                            End If

                            ' Creatxt()

                            '    Try
                            '        Dim Command As New Process 'Creamos la instancia Process
                            '        Command.StartInfo.FileName = "cmd.exe" 'El proceso en si es el CMD
                            '        Command.StartInfo.Arguments = "/c " & Application.StartupPath & "\BATCH\DTW.exe -s" & Application.StartupPath & "\BATCH\OPROD.xml"
                            '        'Aqui le damos los parametros /c y el nombre del archivo a ejecutar
                            '        Command.StartInfo.RedirectStandardError = True 'Redirigimos los errores
                            '        Command.StartInfo.RedirectStandardOutput = True 'Redirigimos la salida
                            '        Command.StartInfo.UseShellExecute = False
                            '        'Para redirigir la salida de este proceso esta propiedad debe ser false
                            '        Command.StartInfo.CreateNoWindow = False
                            '        'Para que no abra la ventana del CMD

                            '        Try
                            '            Command.Start()
                            '            Dim Output As String = Command.StandardOutput.ReadToEnd() _
                            '        & vbCrLf & Command.StandardError.ReadToEnd() 'Guardamos las salidas en un string
                            '            ' RichTextBox1.Text = Output 'Desplegamos la salida en nuestro RichTextBox
                            '        Catch ex As Exception
                            '            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '            'En caso de cualquier error de ejecucion
                            '        End Try
                            '    Catch ex As Exception

                            '    End Try

                            btnConfirma.Text = "&Ok"
                            VisibilidadBotones(False)
                            Me.ContextMenuStrip = Me.ContextMenuStrip1

                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Finally
                            OCN.Close()
                        End Try
                    Else
                        MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
                    End If
                    btnConfirma.Enabled = True
                End If
        End Select

    End Sub

    Sub Creatxt()
        Dim dap1 As New SqlDataAdapter("select * from U_VW_EXPOPROD0 where CREATEFOR in ('CREATEFOR','" & Trim(Form1.vs_idUser) & "')", OCN)
        Dim dap2 As New SqlDataAdapter


        If Form1.vs_Area <> "Telares" Then
            dap2.SelectCommand = New SqlCommand("select * from U_VW_EXPOPROD1", OCN)
        Else
            dap2.SelectCommand = New SqlCommand("select * from U_VW_EXPOPROD1_TELARES", OCN)
        End If

        'Datos de cabecera de la guia
        dap1.SelectCommand.CommandType = CommandType.Text
        dap1.Fill(dts, "TXT0")
        DVTXT0 = dts.Tables("TXT0").DefaultView
        'Datos de detalle de la guia
        dap2.SelectCommand.CommandType = CommandType.Text
        dap2.Fill(dts, "TXT1")
        DVTXT1 = dts.Tables("TXT1").DefaultView

        If OCN.State = ConnectionState.Closed Then OCN.Open()

        Dim str0, str1 As New StringBuilder
        Dim i, j, x, y As Integer

        For i = 0 To dts.Tables("TXT0").Rows.Count - 1
            For j = 0 To dts.Tables("TXT0").Columns.Count - 1
                If j = dts.Tables("TXT0").Columns.Count - 1 Then
                    cadenanombre0 = dts.Tables("TXT0").Rows(i)(j).ToString() + vbCrLf
                    str0.Append(cadenanombre0)
                Else
                    cadenanombre0 = dts.Tables("TXT0").Rows(i)(j).ToString() + Chr(9)
                    str0.Append(cadenanombre0)
                End If

            Next
        Next

        For x = 0 To dts.Tables("TXT1").Rows.Count - 1
            For y = 0 To dts.Tables("TXT1").Columns.Count - 1
                If y = dts.Tables("TXT1").Columns.Count - 1 Then
                    cadenanombre1 = dts.Tables("TXT1").Rows(x)(y).ToString() + vbCrLf
                    str1.Append(cadenanombre1)
                Else
                    cadenanombre1 = dts.Tables("TXT1").Rows(x)(y).ToString() + Chr(9)
                    str1.Append(cadenanombre1)
                End If

            Next
        Next

        Dim FILE0, FILE1 As File
        'Elimina los plantillas de las OP si es que estas ya existiensen ya sea por una migracion anterior u otra accion previa.
        If FILE0.Exists(Application.StartupPath & "\BATCH\OP\OPROD0.txt") = True Then
            FILE0.Delete(Application.StartupPath & "\BATCH\OP\OPROD0.txt")
            FILE1.Delete(Application.StartupPath & "\BATCH\OP\OPROD1.txt")
        End If


        Try
            ' escribiendo la plantilla de cabecera de guía
            Dim writeFile0 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\BATCH\OP\OPROD0.txt", True, System.Text.UnicodeEncoding.Unicode)
            writeFile0.WriteLine(str0.ToString)
            writeFile0.Flush()
            writeFile0.Close()
            writeFile0 = Nothing
            ' escribiendo la plantilla de detalle de guía
            Dim writeFile1 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\BATCH\OP\OPROD1.txt", True, System.Text.UnicodeEncoding.Unicode)
            writeFile1.WriteLine(str1.ToString)
            writeFile1.Flush()
            writeFile1.Close()
            writeFile1 = Nothing

        Catch ex As IOException
            MsgBox(ex.ToString)
            OCN.Close()
        End Try
    End Sub

    Sub DatosInsUpd(ByVal NameProced As String)
        Try
            cmd.Parameters.Clear()
            cmd.Connection = OCN
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = NameProced
            With cmd.Parameters
                .Add(New SqlParameter("@Serie", SqlDbType.Text)).Value = Label6.Text ' Serie
                'cmd.Parameters.Add(New SqlParameter("@Docnum", SqlDbType.Int)).Value = TextBox7.Text ' Docnum
                'cmd.Parameters.Add(New SqlParameter("@Norigen", SqlDbType.BigInt)).Value = -1 ' Numero de origen -1 sin numero de origen
                .Add(New SqlParameter("@postingdate", SqlDbType.SmallDateTime)).Value = DateTimePicker1.Value 'fecha creacion de orden
                .Add(New SqlParameter("@duedate", SqlDbType.SmallDateTime)).Value = DateTimePicker2.Value 'fecha vencimiento de orden
                .Add(New SqlParameter("@statusproduction", SqlDbType.Text)).Value = "P" ' ' Estado de la produccion P planificado
                .Add(New SqlParameter("@typeOP", SqlDbType.Text)).Value = "S" ' Tipo Orden de produccion Standar
                .Add(New SqlParameter("@ItemNo", SqlDbType.Text)).Value = TextBox1.Text ' Codigo de item
                .Add(New SqlParameter("@PlannedQuantity", SqlDbType.Decimal)).Value = CDec(TextBox4.Text) ' Cant planificada
                .Add(New SqlParameter("@ProducQuantity", SqlDbType.Decimal)).Value = CDec(TextBox9.Text) ' Cant producida
                .Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = CDec(TextBox8.Text) ' Peso producido
                .Add(New SqlParameter("@COMMENT", SqlDbType.Text)).Value = "" ' Comentario NO IMPLEMENTADO EN ESTE FORM
                .Add(New SqlParameter("@U_FIB_OPERARIO", SqlDbType.Text)).Value = ComboBox5.SelectedValue 'codigo de operario
                .Add(New SqlParameter("@U_FIB_AYUDANTE", SqlDbType.Text)).Value = ComboBox4.SelectedValue 'codigo de ayudante
                .Add(New SqlParameter("@U_FIB_TELAR", SqlDbType.Text)).Value = Form1.vs_idMac 'codigo de maquina
                .Add(New SqlParameter("@U_FIB_AREA", SqlDbType.Text)).Value = Form1.vs_idArea 'Area de produccion

                .Add(New SqlParameter("@U_FIB_ORILLOS", SqlDbType.Decimal)).Value = 0 'orillos"
                .Add(New SqlParameter("@U_FIB_TSCRAP", SqlDbType.Decimal)).Value = CDec(txtScrap.Text) 'scrap
                .Add(New SqlParameter("@U_FIB_SCANT", SqlDbType.Decimal)).Value = CDec(txtSaldos.Text) 'saldo cantidad
                .Add(New SqlParameter("@U_FIB_SPESO", SqlDbType.Decimal)).Value = CDec(TextBox3.Text) 'saldo peso
                'Añadido el 25/11/14
                .Add(New SqlParameter("@HOST", SqlDbType.Text)).Value = My.Computer.Name
                .Add(New SqlParameter("@Createfor", SqlDbType.Text)).Value = Form1.vs_idUser  'usuario
                'Añadido el 24/11/14
                .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                .Add(New SqlParameter("@FK", SqlDbType.BigInt)).Direction = ParameterDirection.Output '
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub dtgCMPT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtgCMPT.KeyPress
        If btnConfirma.Text = "Crear" Then
            If (dtgCMPT.CurrentCell.ColumnNumber = 3 Or dtgCMPT.CurrentCell.ColumnNumber = 4) Then
                frmConsulta.id_object = 1
                If e.KeyChar = ChrW(Keys.Return) Then
                    NQ = 2
                    Dim Fq As New frmConsulta
                    cVars.NQP = 4 'CONSULTA PARA ITEMS
                    Fq.ShowDialog()
                    Call Recibevar()
                End If
            End If
        End If
    End Sub

    Function CCC(ByVal s_datamin As String)
        If TextBox9.Text <= CStr(0) Then
            s_datamin = "La cantidad producida debe ser mayor a cero"
            CCC = s_datamin
        End If
        If Trim(TextBox1.Text).Length = 0 Then
            s_datamin = "Por favor ingresar Item para producir"
            CCC = s_datamin
        End If
    End Function

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
        SubNavegacion()
        DibujaEAN13()
    End Sub

    Sub SubNavegacion()
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        DvDetalle.RowFilter = "RecordKey = " + "'" + Trim(DvCabecera.Item(posi.Position)("Recordkey")) + "'"
        Dvresumen.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))
        Dvstop.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))
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

#Region "Configuracion, parametrizaciones y conexion a la balanza"

    Private Sub btnconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconnect.Click
        'Codigo generado el 05-01-12 Para adaptar a la actualizacion a WIN7

        'Obtiene el listado de puertos habilitado a un combobox

        'Dim SelPort As String
        'Dim Puertos() As String = SerialPort.GetPortNames
        'For Each SelPort In Puertos
        '    Me.cmbPort.Items.Add(SelPort)
        'Next
        If Me.sppuerto.IsOpen Then
            Me.sppuerto.Close()
        End If

        Me.sppuerto.PortName = "COM1"

        Try
            sppuerto.Open()
            MsgBox("Conexion con balanza satisfactoria", MsgBoxStyle.Information, "FIBRAFIL")
        Catch ex As Exception
            MessageBox.Show("El puerto no está disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        'txtrecibe.Text = Var_publicas.IdProcesoEmpaque
        'Control.Enabled = True


        ''Codigo dado de baja el 05-01-15 para usar en las versiones de windows 7

        'MsgBox("Estableciendo conexion con balanza...")
        'moRS232 = New Rs232()
        'Try
        '    '// Setup parameters
        '    With moRS232
        '        .Port = 1
        '        .BaudRate = 9600
        '        .DataBit = 8
        '        .StopBit = Rs232.DataStopBit.StopBit_1
        '        .Parity = Rs232.DataParity.Parity_None
        '        .Timeout = 1500
        '    End With
        '    '// Initializes port
        '    moRS232.Open()
        '    '// Set state of RTS / DTS
        '    moRS232.Dtr = True ' (chkDTR.CheckState = CheckState.Checked)
        '    moRS232.Rts = True '(chkRTS.CheckState = CheckState.Checked)
        '    If chkEvents.Checked Then moRS232.EnableEvents()
        '    chkEvents.Enabled = True
        '    chkEvents.Checked = True

        TabControl2.SelectedTab = TabPage4
        'Catch Ex As Exception
        '    MessageBox.Show(Ex.Message, "Connection Error", MessageBoxButtons.OK)
        'End Try
    End Sub

    Private Sub chkEvents_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEvents.CheckedChanged
        Try
            If Not moRS232 Is Nothing Then
                moRS232.RxBufferThreshold = 2
            End If
            If chkEvents.Checked Then
                moRS232.EnableEvents()
            Else
                moRS232.DisableEvents()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub moRS232_CommEvent(ByVal source As Rs232, ByVal Mask As Rs232.EventMasks) Handles moRS232.CommEvent
        '===================================================
        '		©2003 www.codeworks.it All rights reserved
        '
        '	Description	:	Events raised when a comunication event occurs
        '	Created			:	15/07/03 - 15:13:46
        '	Author			:	Corrado Cavalli
        '   Modify          :   Alfredo Lescano
        '						*Parameters Info*
        '===================================================
        Dim s_cadena As String
        Dim i_posi As Integer
        Dim d_peso As Decimal

        Debug.Assert(Me.InvokeRequired = False)

        Dim iPnt As Int32, sBuf As String, Buffer() As Byte
        Debug.Assert(Me.InvokeRequired = False)
        'lbAsync.Items.Add("Mask: " & Mask.ToString)
        If (Mask And Rs232.EventMasks.RxChar) > 0 Then

            Try
                s_cadena = source.InputStreamString.ToString
                s_cadena = s_cadena.Replace("Gross", "")
                s_cadena = s_cadena.Replace("kg", "")
                s_cadena = s_cadena.Replace(Chr(10), Chr(64))
                s_cadena = s_cadena.Replace(Chr(13), Chr(64))
                s_cadena = s_cadena.Replace(Chr(32), Chr(64))
                s_cadena = s_cadena.Replace("@", "")

                DataGridView1.Item(7, DataGridView1.CurrentRow.Index).Value = CDec(Trim(s_cadena))

                Call dtgRSMN_KeyPress()
                'Invoco al evento para la impresion de etiquetas 
                ' Uso una clase del tipo PrintDocument
                If SuspenderImpresiónToolStripMenuItem.Checked = False Then

                    Call Imprime_Codebar() 'Agregado y modificado el 06-10-14

                    'Dim printDoc As New PrintDocument
                    '' Asigno el método de evento para cada página a imprimir
                    'AddHandler printDoc.PrintPage, AddressOf print_PrintPage
                    '' Indico donde quiero imprimir y que quiero imprimir
                    'Try
                    '    'printDoc.PrinterSettings.PrinterName = "PROFIL"
                    '    printDoc.Print()
                    'Catch ex As Exception
                    '    MsgBox(ex.Message)
                    'End Try
                End If
                'Luego de imprimir debe pasar a la siguiente linea
                '  SendKeys.Send("{DOWN}")
                ' dtgRSMN.CurrentRowIndex += 1
                Dim MyDesiredIndex As Integer = 0
                If DataGridView1.CurrentRow.Index < DataGridView1.RowCount - 1 Then
                    MyDesiredIndex = DataGridView1.CurrentRow.Index + 1
                End If
                DataGridView1.ClearSelection()
                DataGridView1.CurrentCell = DataGridView1.Rows(MyDesiredIndex).Cells(7)
            Catch ex As Exception

            End Try
        End If

    End Sub
#End Region

#Region "Dar formato a los datagrids"

    Sub ArmaGrid()
        dtgCMPT.TableStyles.Clear()
        dtgCMPT.CaptionText = "COMPONENTES DEL ARTICULO"
        dtgCMPT.CaptionBackColor = Color.Navy
        dtgCMPT.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        With oEstiloGrid
            .MappingName = "vLIST1"
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
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ND"
        oColGrid.MappingName = "RECORDKEY"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "Docnum"
        oColGrid.MappingName = "Docnum"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "#"
        oColGrid.MappingName = "LineNum"
        oColGrid.Width = 20
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "CODIGO"
        oColGrid.MappingName = "CODIGO"
        oColGrid.Width = 120
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ARTICULO"
        oColGrid.MappingName = "DESCRIPCION"
        oColGrid.Width = 200
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "U. MED"
        oColGrid.MappingName = "UOM"
        oColGrid.Width = 100

        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.HeaderText = "CANTIDAD"
        oColGrid.MappingName = "CANTIDAD"
        oColGrid.Width = 90
        oColGrid.Format = "###.####"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.HeaderText = "BASE "
        oColGrid.MappingName = "Planificado"
        oColGrid.Width = 90
        oColGrid.Format = "###.####"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        dtgCMPT.TableStyles.Add(oEstiloGrid)
    End Sub

    Sub ArmaGridRSM()
        dtgRSMN.TableStyles.Clear()
        dtgRSMN.CaptionText = "RESUMEN DE PRODUCCION"
        dtgRSMN.CaptionBackColor = Color.Navy
        dtgRSMN.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        With oEstiloGrid
            .MappingName = "vLIST2"
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
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ND"
        oColGrid.MappingName = "Docentry"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "#"
        oColGrid.MappingName = "LineNum"
        oColGrid.Width = 20
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "BOBINA"
        oColGrid.MappingName = "BOBINA"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ANCHO"
        oColGrid.MappingName = "ANCHO"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.HeaderText = "COLOR"
        oColGrid.MappingName = "COLOR"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.HeaderText = "ESPESOR"
        oColGrid.MappingName = "ESPESOR"
        oColGrid.Width = 80
        oColGrid.Format = "###.##"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.HeaderText = "PESO"
        oColGrid.MappingName = "PESO"
        oColGrid.Width = 80
        oColGrid.Format = "###.##"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.HeaderText = "CODIGO BARRA"
        oColGrid.MappingName = "CODEBAR"
        oColGrid.Width = 250
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.HeaderText = "COMENTARIO"
        oColGrid.MappingName = "Comment"
        oColGrid.Width = 150
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        dtgRSMN.TableStyles.Add(oEstiloGrid)
    End Sub

    Sub ArmaGridSTP()
        dtgSTP.TableStyles.Clear()
        dtgSTP.CaptionText = "DETALLE DE PARADAS"
        dtgSTP.CaptionBackColor = Color.Navy
        dtgSTP.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        With oEstiloGrid
            .MappingName = "vLIST3"
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
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ND"
        oColGrid.MappingName = "Docentry"
        oColGrid.Width = 80
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing


        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "Docnum"
        oColGrid.MappingName = "Docnum"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing


        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.TextBox.ReadOnly = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "#"
        oColGrid.MappingName = "LineNum"
        oColGrid.Width = 20
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Center
        oColGrid.Format = "HH:mm"
        oColGrid.HeaderText = "Hora Inicio"
        oColGrid.MappingName = "HoraIni"
        oColGrid.Width = 65
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Center
        oColGrid.Format = "HH:mm"
        oColGrid.HeaderText = "Hora Fin"
        oColGrid.MappingName = "HoraFin"
        oColGrid.Width = 65
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.HeaderText = "Comentario"
        oColGrid.MappingName = "Comments"
        oColGrid.Width = 350

        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        dtgSTP.TableStyles.Add(oEstiloGrid)
    End Sub

#End Region

    Private Sub dtgCMPT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtgCMPT.Validating
        ' Dim DGList As New List(Of String)
        Dim X, J As Integer
        ' Recorre los items ( compara empezando desde el primero , de abajo hacia arriba)  
        For X = 0 To DvDetalle.Count - 1
            For J = DvDetalle.Count - 1 To X + 1 Step -1
                ' ... si es el mismo  
                Do While (dtgCMPT.Item(X, 3).ToString = dtgCMPT.Item(J, 3).ToString And dtgCMPT.Item(X, 3).ToString <> "" And dtgCMPT.Item(J, 3).ToString <> "")
                    MsgBox("Item duplicado: " & dtgCMPT.Item(X, 3) + ", favor de corregir de lo contrario no podrá registrar esta operación", 48, "FIBRAFIL")
                    X = DvDetalle.Count - 1
                Loop
            Next
        Next

    End Sub

    Sub Calculasaldoscrap()
        Dim X As Integer
        Dim dc_scrap, dc_saldo, i_saldo As Decimal

        For X = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item("distrito", X).Value = 2 Then
                dc_saldo = dc_saldo + CDec(DataGridView1.Item(7, X).Value)
                i_saldo = i_saldo + 1
            End If

            If DataGridView1.Item("distrito", X).Value = 3 Then
                dc_scrap = dc_scrap + CDec(DataGridView1.Item(7, X).Value)
            End If
            TextBox3.Text = CStr(dc_saldo)
            txtSaldos.Text = CStr(i_saldo)
            txtScrap.Text = CStr(dc_scrap)

            DataGridView1.Item(10, X).Value = DataGridView1.Item("distrito", X).Value
        Next
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 7 Or e.ColumnIndex = 8 Or e.ColumnIndex = 10 Then
            e.CellStyle.BackColor = Color.Gainsboro
        End If
    End Sub

    Private Sub DataGridView1_Formato()
        Dim band0 As DataGridViewBand = DataGridView1.Columns(0)
        Dim band1 As DataGridViewBand = DataGridView1.Columns(1)
        Dim band6 As DataGridViewBand = DataGridView1.Columns("PESO")
        Dim band7 As DataGridViewBand = DataGridView1.Columns("CODEBAR")
        Dim band9 As DataGridViewBand = DataGridView1.Columns("COMMENT")

        band0.Visible = False
        band1.Visible = False

        band6.ReadOnly = True
        band7.ReadOnly = True
    End Sub

    Private Sub DataGridView1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DataGridView1.Validating
        CalcularCantProc()
    End Sub

#Region " Visualización de codigo EAN13"

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        If Me.PictureBox1.Visible = True Then
            PictureBox1.Visible = False
        End If
    End Sub

    Private Sub VerCodigoDeBarraEANToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerCodigoDeBarraEANToolStripMenuItem.Click, TextBox2.Click
        If Me.PictureBox1.Visible = False Then
            DibujaEAN13()
            PictureBox1.Visible = True
        End If
    End Sub

    Sub DibujaEAN13()
        ActualCode = eTiposCodigo.Ean13
        Try
            'bm = BarCode.CodeEAN13(TextBox2.Text, True, 0)
            If Not IsNothing(bm) Then
                PictureBox1.Image = bm
            Else
                PictureBox1.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

    Sub VisibilidadBotones(ByVal valor As Boolean)
        btnQuery.Visible = valor
        btnconnect.Visible = valor
        'chkEvents.Visible = valor

        btnFirst.Visible = Not valor
        btnBefore.Visible = Not valor
        btnNext.Visible = Not valor
        btnLast.Visible = Not valor
        txtPosi.Visible = Not valor
    End Sub


    Sub Imprime_Codebar() '06-10-14
        Try
            'RUTA DEL ARCHIVO DE ETIQUETA 
            Dim RUTA As String = Application.StartupPath & "\" + "eti1.prn" '

            'LECTURA DEL ARCHIVO PARA GUARDARLO EN UN STRING
            Dim arc1 As New StreamReader(RUTA)
            Dim etiqueta As String = arc1.ReadToEnd()

            'REEMPLAZA LOS VALORES DE LA ETIQUETA
            etiqueta = etiqueta.Replace("[NPD]", Label19.Text)
            etiqueta = etiqueta.Replace("[FP]", Date.Today.ToShortDateString)
            etiqueta = etiqueta.Replace("[IDITEM]", TextBox1.Text)
            etiqueta = etiqueta.Replace("[DESPRO]", TextBox5.Text)

            etiqueta = etiqueta.Replace("[codbar]", DataGridView1.Item(8, DataGridView1.CurrentRow.Index).Value.ToString)
            'SIGUINTE LINEA SOLO PARA EL AREA DE CABOS 
            Try
                etiqueta = etiqueta.Replace("[PESO]", Decimal.Round(DataGridView1.Item(7, DataGridView1.CurrentRow.Index).Value, 3))
            Catch ex As Exception

            End Try

            etiqueta = etiqueta.Replace("\[""]", "''")

            'IMPRIME USANDO DLL DE IMPRESION OPCION SendStringToPrinter()
            clsprinter.Class1.SendStringToPrinter("PROFIL", etiqueta)
            ''  MessageBox.Show("Etiqueta Impresa", "FIBRAFIL", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '  End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub print_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        '(01) Opcion de codigo  alternativo  --06-10-14

        ''' codigo de impresion de codigo inicial del proyecto modificado el 29-04-14
        '' Este evento se producirá cada vez que se pese un item, para la impresion de etiquetas

        '' La fuente a usar
        'Dim prFont As New Font("Arial", 8, FontStyle.Bold)
        'Dim codeFont As New Font("IDAutomationHC39M", 10, FontStyle.Regular)
        ''Dim codeFont As New Font("IDAutomationSHbC128M", 32, FontStyle.Regular)

        '' Seteo del margen izquierdo
        'Dim xPos As Single = 50 ' e.MarginBounds.Left
        '' Seteo del margen superior
        'Dim yPos As Single = prFont.GetHeight(e.Graphics)

        '' Impresión del contenido de la etiqueta
        'e.Graphics.DrawString("NPD: " & Label19.Text, prFont, Brushes.Black, xPos, yPos)
        'e.Graphics.DrawString("FP: " & Date.Today.ToShortDateString, prFont, Brushes.Black, 280, yPos)
        ''e.Graphics.DrawString(C_C128.Bar128A(TextBox10.Text), codeFont, Brushes.Blue, xPos, 80)
        'e.Graphics.DrawString("*" + DataGridView1.Item(8, DataGridView1.CurrentRow.Index).Value.ToString.ToUpper + "*", codeFont, Brushes.Blue, xPos, 80)
        'e.Graphics.DrawString("ID ITEM : " + TextBox1.Text, prFont, Brushes.Black, xPos, 195)
        'e.Graphics.DrawString("DESC PRODUCTO : " + TextBox5.Text, prFont, Brushes.Black, xPos, 220)
        '' Se indica que ya no hay nada más que imprimir(el valor predeterminado de esta propiedad es False)
        'e.HasMorePages = False
        '(02) Opcion de codigo  alternativo 

        '/*********************************************************************************\

        'Dim barcode As Aspper.Barcode.BarcodeDll = New Aspper.Barcode.BarcodeDll
        'barcode.BCType = Aspper.Barcode.BCType.Code128
        'barcode.BCData = DataGridView1.Item(8, DataGridView1.CurrentRow.Index).Value.ToString.ToUpper

        'barcode.BCHeight = 60
        'barcode.BCWidth = 150
        'barcode.DPI = 72
        'barcode.Rotation = Aspper.Barcode.Rotation.Degree0
        'barcode.DisplayText = False
        'barcode.drawBarcode("d:\vb128.jpg")

        'Dim Rpt As New ReportDocument
        'Rpt.FileName = "D:\Profil\cr_etiqueta.Rpt"

        'Dim tbx As TextObject

        ''Section2 = Page Header 
        ''Text2 = Es el nombre del textbox que quiero cambiar en el reporte 

        'tbx = Rpt.ReportDefinition.Sections("Section1").ReportObjects("txt_ID")
        'tbx.Text = TextBox1.Text

        'tbx = Rpt.ReportDefinition.Sections("Section1").ReportObjects("txt_Item")
        'tbx.Text = TextBox5.Text

        ''tbx = Rpt.ReportDefinition.Sections("Section1").ReportObjects("txt_Codebar")
        ''tbx.Text = "7750182001205"

        'Rpt.PrintToPrinter(1, False, 1, 1)

        'Me.CrystalReportViewer1.ReportSource = Rpt
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        ' Limpia los campos del Datagrid al pulsar Delete 
        e.Handled = True

        If e.KeyCode = Keys.Delete Then
            DataGridView1.Item("BOBINA", DataGridView1.CurrentRow.Index).Value = ""
            DataGridView1.Item("ANCHO", DataGridView1.CurrentRow.Index).Value = 0
            DataGridView1.Item("COLOR", DataGridView1.CurrentRow.Index).Value = ""
            DataGridView1.Item("ESPESOR", DataGridView1.CurrentRow.Index).Value = 0
            DataGridView1.Item("PESO", DataGridView1.CurrentRow.Index).Value = 0.0
            DataGridView1.Item("CODEBAR", DataGridView1.CurrentRow.Index).Value = ""
            DataGridView1.Item("COMMENT", DataGridView1.CurrentRow.Index).Value = ""

            CalcularCantProc()
        End If

    End Sub

    Sub GeneraTemp()
        Dim i, j, x, y As Integer
        Dim id, dsc, qplan, scrap, qprod, pprod, scant, speso As String
        Dim str0, str1 As New StringBuilder

        If id = Nothing Or id <> TextBox1.Text Then
            id = TextBox1.Text
            dsc = TextBox5.Text
            qplan = TextBox4.Text
            scrap = txtScrap.Text
            qprod = TextBox9.Text
            pprod = TextBox8.Text
            scant = txtSaldos.Text
            speso = TextBox3.Text

            str1.Append(id + vbTab + dsc + vbTab + qplan + vbTab + scrap + vbTab + qprod + vbTab + pprod + vbTab + scant + vbTab + speso)
        End If

        For i = 0 To Dvresumen.Count - 1
            If DataGridView1.Item("CODEBAR", i).Value.ToString <> "" Then
                cadenaPESO = DataGridView1.Item("PESO", i).Value.ToString
                cadenacodebar = DataGridView1.Item("CODEBAR", i).Value.ToString

                If i = Dvresumen.Count - 1 Then
                    cadenacodebar = DataGridView1.Item("CODEBAR", i).Value.ToString  'salto de linea
                Else
                    cadenacodebar = DataGridView1.Item("CODEBAR", i).Value.ToString + vbCrLf 'salto de linea
                End If
                str0.Append(cadenaPESO + vbTab + cadenacodebar)
            End If
        Next

        Try
            ' escribiendo la orden de la mesa selecciona
            ' el parametro FALSE no permite duplicar la cadena a pasar al TXT (para el caso dejar siempre en false)
            Dim writeFile0 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\TEMP\OP_DET.txt", False, System.Text.UnicodeEncoding.Unicode)
            writeFile0.WriteLine(str0.ToString)
            writeFile0.Flush()
            writeFile0.Close()
            writeFile0 = Nothing

            Dim writeFile1 As System.IO.TextWriter = New StreamWriter(Application.StartupPath & "\TEMP\OP_CAB.txt", False, System.Text.UnicodeEncoding.Unicode)
            writeFile1.WriteLine(str1.ToString)
            writeFile1.Flush()
            writeFile1.Close()
            writeFile1 = Nothing
        Catch ex As IOException
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub RecuperarUltimoTeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecuperarUltimoTeToolStripMenuItem.Click
        Try
            Dim objStreamReader As StreamReader
            Dim strLine As String
            Dim X As Integer

            If FILE0.Exists(Application.StartupPath & "\temp\OP_CAB.txt") = True Then
                'Se lee el archivo para ver los productos que se han ido atendiendo durante la toma de pedido (pedido abierto o pendiente)
                objStreamReader = New StreamReader(Application.StartupPath & "\temp\OP_CAB.txt")
                strLine = objStreamReader.ReadLine
                'Recorro registro x registro y se van presentando en la visualizacion de pedido.
                Do While Not strLine Is Nothing
                    Dim TestArray() As String = Split(strLine, vbTab)
                    TextBox1.Text = TestArray(0)
                    TextBox5.Text = TestArray(1)
                    TextBox4.Text = TestArray(2)
                    txtScrap.Text = TestArray(3)
                    TextBox9.Text = TestArray(4)
                    TextBox8.Text = TestArray(5)
                    txtSaldos.Text = TestArray(6)
                    TextBox3.Text = TestArray(7)
                    strLine = objStreamReader.ReadLine
                Loop
                'Cierro fichero 
                objStreamReader.Close()
            End If

            If FILE1.Exists(Application.StartupPath & "\temp\OP_DET.txt") = True Then
                objStreamReader = New StreamReader(Application.StartupPath & "\temp\OP_DET.txt")
                strLine = objStreamReader.ReadLine

                'Recorro registro x registro y se van presentando en la visualizacion de pedido.
                Do While Not strLine Is Nothing
                    If strLine <> "" Then
                        Dim TestArray1() As String = Split(strLine, vbTab)

                        DataGridView1.Item(7, X).Value = TestArray1(0)
                        DataGridView1.Item(8, X).Value = TestArray1(1)
                    End If

                    strLine = objStreamReader.ReadLine
                    X = X + 1

                Loop
                'Cierro fichero 
                objStreamReader.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub CalcularCantProc()
        Dim X, J, Y As Integer 'dtgrsmn.Item(5
        Dim SumPeso As Decimal
        For X = 0 To Dvresumen.Count - 1 Step 1
            '  contabliza los items producidos (ubicados en pestaña resumen) por el codigo de barra
            ' 19-01-15 pasa unicamente las cantidades al campo de cant ya no pesos
            ' If Trim(lblUM.Text.ToUpper) <> "KG" Then
            If (DataGridView1.Item(8, X).Value.ToString() <> "") Then
                J = J + 1
                TextBox9.Text = J.ToString
            End If
            'Else
            'SumPeso = SumPeso + CDec(DataGridView1.Item(7, X).Value.ToString())
            'TextBox9.Text = SumPeso.ToString
            'End If
        Next
        'Calcula la cantidad base por cada item producido
        If CInt(TextBox9.Text) >= 1 Then
            For Y = 0 To DvDetalle.Count - 1 Step 1
                dtgCMPT.Item(Y, 7) = dtgCMPT.Item(Y, 6) / CDec(TextBox9.Text)
            Next
        End If

        Dim dc_pesototal As Decimal

        For X = 0 To 249
            dc_pesototal = dc_pesototal + CDec(DataGridView1.Item(7, X).Value.ToString)
        Next
        TextBox8.Text = dc_pesototal.ToString
        Calculasaldoscrap()
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    '    Dim barcode As Aspper.Barcode.BarcodeDll = New Aspper.Barcode.BarcodeDll
    '    barcode.BCType = Aspper.Barcode.BCType.Code128
    '    barcode.BCData = "ALFA09"

    '    barcode.BCHeight = 60
    '    barcode.BCWidth = 150
    '    barcode.DPI = 72
    '    barcode.Rotation = Aspper.Barcode.Rotation.Degree0
    '    barcode.DisplayText = True
    '    barcode.drawBarcode("d:\vb128.jpg")

    '    Dim Rpt As New ReportDocument
    '    Rpt.FileName = "D:\Profil\cr_etiqueta.Rpt"

    '    Dim tbx As TextObject
    '    'Dim Archivo As New StdPicture

    '    'Archivo = LoadPicture(Campo4.Value) 'Campo4 es el campo Ruta 
    '    'Imagen1.FormattedPicture = Archivo
    '    'Section2 = Page Header 
    '    'Text2 = Es el nombre del textbox que quiero cambiar en el reporte 

    '    tbx = Rpt.ReportDefinition.Sections("Section1").ReportObjects("txt_ID")
    '    tbx.Text = TextBox1.Text

    '    tbx = Rpt.ReportDefinition.Sections("Section1").ReportObjects("txt_Item")
    '    tbx.Text = TextBox5.Text

    '    ' Rpt.DataDefinition.FormulaFields("PicFileName").Text = "D:\vb128.jpg"
    '    '        .CrystalReport.DataDefinition.FormulaFields("PicFileName").Text = "vb128.jpg"
    '    'tbx = Rpt.ReportDefinition.Sections("Section1").ReportObjects("PicFileName")
    '    'tbx.Text = "D:\vb128.jpg"
    '    'tbx = Rpt.DataDefinition.FormulaFields("PicFileName").Text = "vb128.jpg"

    '    'tbx = Rpt.ReportDefinition.Sections("Section1").ReportObjects("txt_Codebar")
    '    'tbx.Text = "7750182001205"
    '    Dim myPath As String = "d:\vb128.jpg"

    '    Dim pct As PictureObject = Rpt.ReportDefinition.ReportObjects("Picture8")

    '    '  pct = File.ReadAllBytes(myPath)


    '    Rpt.PrintOptions.PrinterName = "PDF"
    '    Rpt.PrintToPrinter(1, False, 1, 1)

    '    Me.CrystalReportViewer1.ReportSource = Rpt
    'End Sub


    'Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles sppuerto.DataReceived
    '    buffer = sppuerto.ReadExisting
    '    Me.Invoke(New EventHandler(AddressOf DoUpdate))
    'End Sub

    Function ReceiveSerialData() As String
        ' Receive strings from a serial port.
        Dim returnStr As String = ""

        Dim com1 As IO.Ports.SerialPort = Nothing
        Try
            com1 = My.Computer.Ports.OpenSerialPort("COM1")
            'com1.Port = 1
            com1.BaudRate = 9600
            com1.DataBits = 8
            com1.StopBits = Rs232.DataStopBit.StopBit_1
            com1.Parity = Rs232.DataParity.Parity_None
            com1.ReadTimeout = 1500

            Do
                Dim Incoming As String = com1.ReadLine()
                If Incoming Is Nothing Then
                    Exit Do
                Else
                    returnStr &= Incoming & vbCrLf
                End If
            Loop
        Catch ex As TimeoutException
            returnStr = "Error: Serial Port read timed out."
        Finally
            If com1 IsNot Nothing Then com1.Close()
        End Try

        Return returnStr

    End Function

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing)
        Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing
        worksheet = workbook.Sheets("Hoja1")
        worksheet = workbook.ActiveSheet
        'Aca se agregan las cabeceras de nuestro datagrid.

        For i As Integer = 1 To Me.DataGridView1.Columns.Count
            worksheet.Cells(1, i) = Me.DataGridView1.Columns(i - 1).HeaderText
        Next

        'Aca se ingresa el detalle recorrera la tabla celda por celda

        For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
            For j As Integer = 1 To Me.DataGridView1.Columns.Count - 1
                worksheet.Cells(i + 2, j + 1) = DataGridView1.Rows(i).Cells(j).Value.ToString()
            Next
        Next

        'Aca le damos el formato a nuestro excel
        worksheet.Rows.Item(1).Font.Bold = 1
        worksheet.Rows.Item(1).HorizontalAlignment = 3


        worksheet.Columns.AutoFit()
        worksheet.Columns.HorizontalAlignment = 2

        app.Visible = True
        app = Nothing
        workbook = Nothing
        worksheet = Nothing
        FileClose(1)
        GC.Collect()
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString())
        Finally
            GC.Collect()
        End Try
    End Sub

    'RS232 26-12-14
    Private Sub PtoSerial_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles sppuerto.DataReceived
        DataIn = Me.sppuerto.ReadExisting
        If DataIn <> String.Empty Then
            SetText(DataIn)
        End If
    End Sub

    'RS232 26-12-14
    Private Sub SetText(ByVal text As String)
        'If Me.txtpeso.InvokeRequired Then
        '    Dim d As New SetTextCallback(AddressOf SetText)
        '    Me.Invoke(d, New Object() {text})
        'Else
        '    Me.txtpeso.Text = text
        Try

            text = Replace(text, " ", "")
            text = Replace(text, "k", "")
            text = Replace(text, "g", "")
            text = Replace(text, " kg ", "")
            text = Replace(text, Chr(13), "")
            text = Replace(text, Chr(10), "")

            DataGridView1.Item(7, DataGridView1.CurrentRow.Index).Value = CDec(Trim(text))

            Call dtgRSMN_KeyPress()
            'Invoco al evento para la impresion de etiquetas 
            ' Uso una clase del tipo PrintDocument
            If SuspenderImpresiónToolStripMenuItem.Checked = False Then
                Call Imprime_Codebar() 'Agregado y modificado el 06-10-14

            End If
            'Luego de imprimir debe pasar a la siguiente linea
            '  SendKeys.Send("{DOWN}")
            ' dtgRSMN.CurrentRowIndex += 1
            Dim MyDesiredIndex As Integer = 0
            If DataGridView1.CurrentRow.Index < DataGridView1.RowCount - 1 Then
                MyDesiredIndex = DataGridView1.CurrentRow.Index + 1
            End If
            DataGridView1.ClearSelection()
            DataGridView1.CurrentCell = DataGridView1.Rows(MyDesiredIndex).Cells(7)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "PROFIL")
        End Try
        '        End If
    End Sub

    Protected Sub Rx(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        Try
            'Añadir la recepcion actual al buffer     
            System.Threading.Thread.Sleep(100)
            PortSerie_Recepcion += sppuerto.ReadExisting
            If PortSerie_Recepcion.Contains(Chr(13)) Then
                RaiseEvent RxFin(PortSerie_Recepcion)
                PortSerie_Recepcion = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message) 'En caso de excepción        
        End Try
    End Sub


    
End Class


