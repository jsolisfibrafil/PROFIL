Imports System.IO
Imports System.Text
Imports PROFIL.MForm
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class frmInItem
    Dim dtsitms As New DataSet
    'Lectura de datos desde la balanza mediante puerto RS232
    Private miComPort As Integer
    Private WithEvents moRS232 As Rs232
    Private mlTicks As Long
    Private Delegate Sub CommEventUpdate(ByVal source As Rs232, ByVal mask As Rs232.EventMasks)
    ' Para ajustar el formato de impresion de etiquetas
    Private WithEvents printButton As System.Windows.Forms.Button
    Private printFont As Font
    Private streamToPrint As StreamReader
    Dim S_CODEBAR As String
    Public Shared scode, sname, sumed, swhs, scode0, sname0, swhs0, sumed0, scodebar As String
    Dim dts As New DataSet
    Private DvCabecera, Dvresumen As DataView
    Public Shared NQ As Integer 'NUMERO DE CONSULTA 
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros

    Dim WithEvents FQ As New frmConsulta
    Dim cmd, cmd1, cmd2, cmd3, cmdKARDEX As New SqlCommand()


    Private Sub btnconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconnect.Click
        MsgBox("Estableciendo conexion con balanza")
        moRS232 = New Rs232()
        Try
            With moRS232
                .Port = 1
                .BaudRate = 9600
                .DataBit = 8
                .StopBit = Rs232.DataStopBit.StopBit_1
                .Parity = Rs232.DataParity.Parity_None
                .Timeout = 1500
            End With
            moRS232.Open()
            moRS232.Dtr = True
            moRS232.Rts = True
            If chkEvents.Checked Then moRS232.EnableEvents()
            chkEvents.Enabled = True
            chkEvents.Checked = True
            MsgBox("Conexion con balanza satisfactoria", MsgBoxStyle.Information, "FIBRAFIL")
        Catch Ex As Exception
            MessageBox.Show(Ex.Message, "Connection Error", MessageBoxButtons.OK)
        End Try
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
        Dim s_cadena As String
        Dim i_posi As Integer
        Dim d_peso As Decimal

        Debug.Assert(Me.InvokeRequired = False)

        Dim iPnt As Int32, sBuf As String, Buffer() As Byte
        Debug.Assert(Me.InvokeRequired = False)

        If (Mask And Rs232.EventMasks.RxChar) > 0 Then

            Try
                s_cadena = source.InputStreamString.ToString
                s_cadena = s_cadena.Replace("Gross", "")
                s_cadena = s_cadena.Replace("kg", "")
                s_cadena = s_cadena.Replace(Chr(10), Chr(64))
                s_cadena = s_cadena.Replace(Chr(13), Chr(64))
                s_cadena = s_cadena.Replace(Chr(32), Chr(64))
                s_cadena = s_cadena.Replace("@", "")

                DataGridView1.Item("PESO", DataGridView1.CurrentRow.Index).Value = CDec(Trim(s_cadena))

                Call dtgRSMN_KeyPress()

                'Invoco al evento para la impresion de etiquetas 
                ' Uso una clase del tipo PrintDocument
                Dim printDoc As New PrintDocument
                ' Asigno el método de evento para cada página a imprimir
                AddHandler printDoc.PrintPage, AddressOf print_PrintPage
                ' Indico donde quiero imprimir y que quiero imprimir
                Try
                    printDoc.PrinterSettings.PrinterName = "PROFIL"
                    printDoc.Print()
                    CalculaCant()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                Dim MyDesiredIndex As Integer = 0
                If DataGridView1.CurrentRow.Index < DataGridView1.RowCount - 1 Then
                    MyDesiredIndex = DataGridView1.CurrentRow.Index + 1
                End If
                DataGridView1.ClearSelection()
                DataGridView1.CurrentCell = DataGridView1.Rows(MyDesiredIndex).Cells(6)
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub print_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        ' Este evento se producirá cada vez que se pese un item, para la impresion de etiquetas
        ' La fuente a usar
        Dim prFont As New Font("Arial", 8, FontStyle.Bold)
        Dim codeFont As New Font("IDAutomationHC39M", 10, FontStyle.Regular)

        ' Seteo del margen izquierdo
        Dim xPos As Single = 50 ' e.MarginBounds.Left
        ' Seteo del margen superior
        Dim yPos As Single = prFont.GetHeight(e.Graphics)

        ' Impresión del contenido de la etiqueta
        e.Graphics.DrawString("FP: " & Date.Today.ToShortDateString, prFont, Brushes.Black, 280, yPos)
        e.Graphics.DrawString("*" + DataGridView1.Item("CODEBAR", DataGridView1.CurrentRow.Index).Value.ToString.ToUpper + "*", codeFont, Brushes.Blue, xPos, 80)
        e.Graphics.DrawString("ID ITEM : " + txtIdSAP.Text, prFont, Brushes.Black, xPos, 195)
        e.Graphics.DrawString("DESC PRODUCTO : " + txtDscSAP.Text, prFont, Brushes.Black, xPos, 220)
        ' Se indica que ya no hay nada más que imprimir(el valor predeterminado de esta propiedad es False)
        e.HasMorePages = False
    End Sub

    Sub dtgRSMN_KeyPress()
        Dim DD, MM, AA, LINE, PESO, DEC, ENT, sSuma As String
        ' Asignacion de la variable de 02 digitos para el día
        If Today.Day.ToString.Length = 1 Then
            Today.Day.ToString()
            DD = "0" + Today.Day.ToString()
        Else
            DD = Today.Day.ToString()
        End If

        ' Asignacion de la variable de 02 digitos para el mes
        If Today.Month.ToString.Length = 1 Then
            MM = "0" + Today.Month.ToString
        Else
            MM = Today.Month.ToString
        End If

        ' Asignacion de la variable de 02 digitos para el año
        AA = Today.Year.ToString.Substring(2, 2)

        PESO = DataGridView1.Item("peso", DataGridView1.CurrentRow.Index).Value.ToString

        If PESO.Contains(".") = True Then
            'Obtiene la parte entera 
            ENT = PESO.Substring(0, PESO.IndexOf("."))

            'Obtiene la parte decimal solo 02 dígitos
            DEC = PESO.Substring(PESO.IndexOf(".") + 1, 1)

            PESO = ENT + "" + DEC + "0"

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
        Dim suma As Integer = 0
        suma = suma + (-1)

        LINE = "IN" 'para casos de ajuste de inventario 

        sSuma = "00" + suma.ToString
        S_CODEBAR = LINE + AA + MM + DD + sSuma + PESO
        DataGridView1.Item("CODEBAR", DataGridView1.CurrentRow.Index).Value = S_CODEBAR
        CalculaCant()
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        btnConfirma.Text = "Crear"
        btnquery.Visible = True
        If RadioButton1.Checked = True Then VisualizarObj(False)
        If RadioButton2.Checked = True Then VisualizarObj(True)
        Me.ContextMenuStrip = Nothing

        AddNewDataRowView()

        Call LimpiaCajas()

        Dim da_docnum As New SqlClient.SqlDataAdapter("select Isnull(Max(Docnum)+1,1) As DOCNUM from OPROFIB where serie = " & "'NP' and  year(Postingdate)= " & Year(Date.Now), OCN)
        da_docnum.SelectCommand.CommandType = CommandType.Text
        Dim ds As New DataSet()
        da_docnum.Fill(ds, "vDocnum")

        txtdocnum.DataBindings.Add("text", ds.Tables("vDocnum"), "DOCNUM")

        DataGridView1_Formato()
    End Sub

    Private Sub btnquery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnquery.Click
        Dim formQuery As New frmConsulta
        cVars.NQP = 2 'CONSULTA PARA ITEMS
        NQ = 1
        formQuery.ShowDialog()
        Call Recibevar()
    End Sub

    Public Sub Recibevar() Handles FQ.PasaVars
        Try
            If NQ = 1 Then
                If scode = Nothing Then scode = txtIdSAP.Text
                If sname = Nothing Then sname = txtDscSAP.Text
                txtIdSAP.Text = scode
                txtDscSAP.Text = sname
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()
            Case "Crear"
                'DatosInsUpd("U_SP_FIB_INS_OPCAB")

                'Call CalculaCant()

                'If OCN.State = ConnectionState.Closed Then OCN.Open()
                'Try
                '    cmd.ExecuteNonQuery()

                '    ' Graba el detalle del resumen de la orden de produccion
                '    With cmd2
                '        .Parameters.Clear()
                '        .Connection = OCN
                '        .CommandType = CommandType.StoredProcedure
                '        .CommandText = "U_SP_FIB_INS_OPRSM"
                '        .Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.Int))
                '        .Parameters.Add(New SqlParameter("@Docnum", SqlDbType.Int))
                '        .Parameters.Add(New SqlParameter("@LineNum", SqlDbType.Int))
                '        .Parameters.Add(New SqlParameter("@BobinaN", SqlDbType.VarChar))
                '        .Parameters.Add(New SqlParameter("@Ancho", SqlDbType.Int))
                '        .Parameters.Add(New SqlParameter("@Color", SqlDbType.VarChar))
                '        .Parameters.Add(New SqlParameter("@Espesor", SqlDbType.VarChar))
                '        .Parameters.Add(New SqlParameter("@PesoBob", SqlDbType.Decimal))
                '        .Parameters.Add(New SqlParameter("@CODEBAR", SqlDbType.VarChar))
                '        .Parameters.Add(New SqlParameter("@COMMENT", SqlDbType.VarChar))
                '        .Parameters.Add(New SqlParameter("@TIPPROD", SqlDbType.Int))
                '        If RadioButton1.Checked = True Then
                '            For k As Integer = 0 To Dvresumen.Count - 1
                '                .Parameters("@RECORDKEY").Value = -99
                '                .Parameters("@Docnum").Value = txtdocnum.Text
                '                .Parameters("@LineNum").Value = k
                '                .Parameters("@BobinaN").Value = ""
                '                .Parameters("@Ancho").Value = 0
                '                .Parameters("@Color").Value = ""
                '                .Parameters("@Espesor").Value = ""
                '                .Parameters("@PesoBob").Value = Dvresumen.Item(k)("PESO")
                '                .Parameters("@CODEBAR").Value = Dvresumen.Item(k)("codebar")
                '                .Parameters("@COMMENT").Value = ""
                '                .Parameters("@TIPPROD").Value = 1
                '                .ExecuteNonQuery()
                '            Next k
                '        End If
                '        If RadioButton2.Checked = True Then
                '            For k As Integer = 0 To ListBox1.Items.Count - 1
                '                .Parameters("@RECORDKEY").Value = -99
                '                .Parameters("@Docnum").Value = txtdocnum.Text
                '                .Parameters("@LineNum").Value = k
                '                .Parameters("@BobinaN").Value = ""
                '                .Parameters("@Ancho").Value = 0
                '                .Parameters("@Color").Value = ""
                '                .Parameters("@Espesor").Value = ""
                '                Dim s_ent, s_dec As String

                '                s_ent = ListBox1.Items.Item(k).ToString.Substring(ListBox1.Items.Item(k).ToString.Length - 5, 3)
                '                s_dec = ListBox1.Items.Item(k).ToString.Substring(ListBox1.Items.Item(k).ToString.Length - 2, 2)
                '                .Parameters("@PesoBob").Value = s_ent + "." + s_dec
                '                .Parameters("@CODEBAR").Value = ListBox1.Items.Item(k)
                '                .Parameters("@COMMENT").Value = ""
                '                .Parameters("@TIPPROD").Value = 1
                '                .ExecuteNonQuery()
                '            Next
                '        End If

                '    End With


                '    'Procedimiento almacenado que actualiza el recordkey de los detalles de la OP y ajustes
                '    With cmd3
                '        .Parameters.Clear()
                '        .Connection = OCN
                '        .CommandType = CommandType.StoredProcedure
                '        .CommandText = "U_SP_UPD_RECORDIDOP"
                '        .ExecuteNonQuery()
                '    End With

                '    Try
                '        With cmdKARDEX
                '            .Parameters.Clear()
                '            .Connection = OCN
                '            .CommandType = CommandType.StoredProcedure
                '            .CommandText = "U_SP_FIB_INS_KARDEX"

                '            .Parameters.Add(New SqlParameter("@TIPOOP", SqlDbType.Text)).Value = "I" ' Tipo Operacion I [IN entrada] O [OUT salida]
                '            .Parameters.Add(New SqlParameter("@FCHOPE", SqlDbType.SmallDateTime)).Value = Date.Now ' Fecha operacion
                '            .Parameters.Add(New SqlParameter("@NUMORI", SqlDbType.BigInt)).Value = -1 'Numero de origen 
                '            .Parameters.Add(New SqlParameter("@NUMDOC", SqlDbType.BigInt)).Value = CInt(txtdocnum.Text) 'Numero de documento
                '            .Parameters.Add(New SqlParameter("@ITEMCODE", SqlDbType.VarChar)).Value = txtIdSAP.Text ' Codigo de item
                '            .Parameters.Add(New SqlParameter("@UM", SqlDbType.VarChar)).Value = sumed  'Unidad de medida de item (Obtenido en SP)
                '            .Parameters.Add(New SqlParameter("@QSini", SqlDbType.Decimal)).Value = 0 ' Saldo inicial (calculado en SP)
                '            .Parameters.Add(New SqlParameter("@QPlani", SqlDbType.Decimal)).Value = 0 'Cant. planificada
                '            .Parameters.Add(New SqlParameter("@QProd", SqlDbType.Decimal)).Value = CDec(txtCant.Text) ' Cant. producida
                '            .Parameters.Add(New SqlParameter("@QSfin", SqlDbType.Decimal)).Value = 0 ' Saldo final (calculado en SP)
                '            .ExecuteNonQuery()
                '        End With
                '    Catch ex As Exception
                '        MsgBox(ex.Message)
                '    End Try

                '    MsgBox("Ajuste de inventatio creado satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")

                '    btnConfirma.Text = "&Ok"

                btnconnect.Visible = False
                chkEvents.Visible = False
                moRS232.DisableEvents() ''''
                If moRS232.IsOpen Then moRS232.Close()

                btnquery.Enabled = False
                Me.ContextMenuStrip = Me.ContextMenuStrip1

                'Catch ex As Exception
                '    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Finally
                '    OCN.Close()
                'End Try
        End Select

    End Sub

    Sub DatosInsUpd(ByVal NameProced As String)
        Try
            cmd.Parameters.Clear()
            cmd.Connection = OCN
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = NameProced

            cmd.Parameters.Add(New SqlParameter("@Serie", SqlDbType.Text)).Value = "NP" ' Serie
            cmd.Parameters.Add(New SqlParameter("@Docnum", SqlDbType.Int)).Value = CInt(txtdocnum.Text) ' Docnum
            cmd.Parameters.Add(New SqlParameter("@Norigen", SqlDbType.BigInt)).Value = -1 ' Numero de origen -1 sin numero de origen
            cmd.Parameters.Add(New SqlParameter("@postingdate", SqlDbType.SmallDateTime)).Value = Date.Now 'fecha creacion de orden
            cmd.Parameters.Add(New SqlParameter("@duedate", SqlDbType.SmallDateTime)).Value = Date.Now 'fecha vencimiento de orden
            cmd.Parameters.Add(New SqlParameter("@statusproduction", SqlDbType.Text)).Value = "P" 'ComboBox1.SelectedValue  ' Estado de la produccion
            cmd.Parameters.Add(New SqlParameter("@typeOP", SqlDbType.Text)).Value = "I" ' Tipo Orden de produccion (I) Ingreso
            cmd.Parameters.Add(New SqlParameter("@ItemNo", SqlDbType.Text)).Value = txtIdSAP.Text ' Codigo de item
            cmd.Parameters.Add(New SqlParameter("@PlannedQuantity", SqlDbType.Decimal)).Value = 0 ' Cant planificada
            cmd.Parameters.Add(New SqlParameter("@ProducQuantity", SqlDbType.Decimal)).Value = CDec(txtCant.Text) ' Cant producida
            cmd.Parameters.Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = 0 ' Peso producido

            If RadioButton1.Checked = True Then cmd.Parameters.Add(New SqlParameter("@COMMENT", SqlDbType.Text)).Value = "Ingreso - por ajuste de inventario" ' Comentario NO IMPLEMENTADO EN ESTE FORM
            If RadioButton2.Checked = True Then cmd.Parameters.Add(New SqlParameter("@COMMENT", SqlDbType.Text)).Value = "Salida - por ajuste de inventario" ' Comentario NO IMPLEMENTADO EN ESTE FORM

            cmd.Parameters.Add(New SqlParameter("@U_FIB_OPERARIO", SqlDbType.Text)).Value = "" 'codigo de operario
            cmd.Parameters.Add(New SqlParameter("@U_FIB_AYUDANTE", SqlDbType.Text)).Value = "" 'codigo de ayudante
            cmd.Parameters.Add(New SqlParameter("@U_FIB_TELAR", SqlDbType.Text)).Value = Form1.vs_idMac 'codigo de maquina

            cmd.Parameters.Add(New SqlParameter("@U_FIB_AREA", SqlDbType.Text)).Value = Form1.vs_idArea 'Area de produccion

            cmd.Parameters.Add(New SqlParameter("@U_FIB_ORILLOS", SqlDbType.Decimal)).Value = 0 'orillos"
            cmd.Parameters.Add(New SqlParameter("@U_FIB_TSCRAP", SqlDbType.Decimal)).Value = 0 'scrap
            cmd.Parameters.Add(New SqlParameter("@U_FIB_SCANT", SqlDbType.Decimal)).Value = 0 'saldo cantidad
            cmd.Parameters.Add(New SqlParameter("@U_FIB_SPESO", SqlDbType.Decimal)).Value = 0 'saldo peso
            cmd.Parameters.Add(New SqlParameter("@Createfor", SqlDbType.Text)).Value = Form1.vs_idUser  'usuario

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub AddNewDataRowView()
        Dvresumen = dts.Tables("vLIST2").DefaultView
        Dvresumen.AllowEdit = True
        Dvresumen.AllowNew = True

        '  Dvresumen.AddNew()
        Dvresumen.RowFilter = "RecordKey = -1"
        'dtgRSMN.SetDataBinding(Nothing, Nothing)
        ' dtgRSMN.SetDataBinding(Dvresumen, "")

        For X As Int32 = 0 To 99
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
    End Sub

    Private Sub frmInItem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If CierraForms("un", "Ajuste de inventario", btnConfirma.Text) = False Then
            If Not moRS232 Is Nothing Then
                ' Deshabilita el evento si es que estuviese activo
                moRS232.DisableEvents()
                If moRS232.IsOpen Then moRS232.Close()
                e.Cancel = CierraForms("un", "Ajuste de inventario", btnConfirma.Text)
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub frmInItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
        Call LlenarData()
    End Sub

    Sub Obtener_Data()
        Dim dap4 As New SqlDataAdapter("U_SP_LISTAJUSTES", OCN)
        dap4.SelectCommand.CommandType = CommandType.StoredProcedure
        dap4.Fill(dts, "vOP")

        DvCabecera = dts.Tables("vOP").DefaultView
        DvCabecera.AllowEdit = False
        DvCabecera.AllowNew = False

        Dim dap2 As New SqlDataAdapter("U_SP_SELDETRSM", OCN)
        dap2.SelectCommand.CommandType = CommandType.StoredProcedure
        dap2.Fill(dts, "vLIST2")

        Dvresumen = dts.Tables("vLIST2").DefaultView
        Dvresumen.AllowEdit = False
        Dvresumen.AllowNew = False

        posi = CType(BindingContext(dts.Tables("vop")), CurrencyManager)
        posi.Position = posi.Count - 1
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)

        Try
            If DvCabecera.Count > 0 Then
                Dvresumen.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub LlenarData()
        txtIdSAP.DataBindings.Add("text", dts.Tables("vop"), "codigo")
        txtdocnum.DataBindings.Add("text", dts.Tables("vop"), "Docnum")
        txtDscSAP.DataBindings.Add("text", dts.Tables("vop"), "item")
        txtCant.DataBindings.Add("text", dts.Tables("vop"), "Qprod")
        DataGridView1.DataSource = Dvresumen
    End Sub

#Region "Formato datagridview"
    Private Sub DataGridView1_Formato()
        Dim band0 As DataGridViewBand = DataGridView1.Columns(0)
        Dim band1 As DataGridViewBand = DataGridView1.Columns(1)
        Dim band2 As DataGridViewBand = DataGridView1.Columns(2)
        Dim band3 As DataGridViewBand = DataGridView1.Columns(3)
        Dim band4 As DataGridViewBand = DataGridView1.Columns(4)
        Dim band5 As DataGridViewBand = DataGridView1.Columns(5)
        Dim band6 As DataGridViewBand = DataGridView1.Columns(6)
        Dim band7 As DataGridViewBand = DataGridView1.Columns(7)
        Dim band8 As DataGridViewBand = DataGridView1.Columns(8)
        Dim band9 As DataGridViewBand = DataGridView1.Columns(9)

        band0.Visible = False

        band2.Visible = False
        band3.Visible = False
        band4.Visible = False
        band5.Visible = False
        band8.Visible = False
        band9.Visible = False

        band1.ReadOnly = True
        band6.ReadOnly = True
        band7.ReadOnly = True
        band6.DefaultCellStyle.Format = "###.##"

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 1 Then
            e.CellStyle.BackColor = Color.Gainsboro
        End If
    End Sub
#End Region

#Region "Navegacion de registros"

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
        Dvresumen.RowFilter = "RecordKey = " + Trim(DvCabecera.Item(posi.Position)("Recordkey"))
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

    Sub LimpiaCajas()
        Dim Cajas As New Control

        For Each Cajas In Controls
            If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
            If TypeOf Cajas Is TextBox Then Cajas.Text = ""
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Sub CalculaCant()
        Dim X As Integer
        Dim i_cant As Integer
        If RadioButton1.Checked = True Then
            For X = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(7, X).Value <> "" Then
                    i_cant = i_cant + 1
                End If
            Next
        Else
            i_cant = ListBox1.Items.Count
        End If
        txtCant.Text = i_cant.ToString
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        VisualizarObj(False)
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        VisualizarObj(True)
    End Sub

    Sub VisualizarObj(ByVal A As Boolean)
        DataGridView1.Visible = Not A
        ListBox1.Visible = A
        txtCodebar.Visible = A
        btnconnect.Visible = Not A
        chkEvents.Visible = Not A
    End Sub

    Private Sub txtCodebar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodebar.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            Try
                Dim CMD As New SqlCommand("U_SP_ADDCB", OCN)
                CMD.CommandType = CommandType.StoredProcedure
                CMD.Parameters.Add(New SqlParameter("@CB", SqlDbType.Text)).Value = txtCodebar.Text ' Cod de barras
                CMD.Parameters.Add(New SqlParameter("@ITM", SqlDbType.Text)).Value = txtIdSAP.Text ' Cod de item

                If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
                OCN.Open()
                Try
                    Dim DAP As New SqlDataAdapter(CMD)
                    Dim INOUT As Integer
                    DAP.Fill(dtsitms, "AddCB")
                    If dtsitms.Tables("AddCB").Rows.Count > 0 Then
                        INOUT = CInt(dtsitms.Tables("AddCB").Rows(0).Item(0))
                    End If
                    If INOUT <> 1 Then
                        MessageBox.Show("Código no hallado", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        If Trim(txtCodebar.Text) <> "" Then
                            If ListBox1.Items.Contains(txtCodebar.Text) Then
                                MsgBox("Item ya ingresado", MsgBoxStyle.Exclamation, "PROFIL")
                            Else
                                ListBox1.Items.Add(txtCodebar.Text)
                            End If
                            txtCodebar.Text = ""
                            txtCant.Text = ListBox1.Items.Count.ToString
                            txtCodebar.Focus()
                        End If
                    End If
                Catch ex As SqlException
                    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    dtsitms.Dispose()
                    dtsitms.Tables("AddCB").Rows.Clear()
                    OCN.Close()
                End Try
            Catch ex As Exception

            End Try
        End If
    End Sub

End Class