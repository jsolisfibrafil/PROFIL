'Conexion SQL
Imports System.Data.SqlClient

'RS232
Imports System.IO
Imports System.IO.Ports
Imports System.Configuration

Public Class frm_prodMasiva
    ' Variables para uso del movimiento del groupbox "grp_combos"
    Dim Dragging As Boolean
    Dim cursorX, CursorY As Integer

    Public Shared scode, sname, sumed As String
    Dim WithEvents FQ As New frmConsulta
    Public Shared NQ As Integer 'NUMERO DE CONSULTA 

    Dim dts As New DataSet
    Dim cmd As New SqlCommand()
    ' Variable para la recepcion de datos del puesto COM
    Dim DataIn As String = String.Empty


#Region "CONSULTA DE PRODUCTO MAESTRO "

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim formQuery As New frmConsulta
        frmConsulta.id_object = 1
        cVars.NQP = 2 'CONSULTA PARA ITEMS
        NQ = 1
        formQuery.ShowDialog()
        Call Recibevar()
    End Sub

    Public Sub Recibevar() Handles FQ.PasaVars
        Try
            '-----> Obtiene el producto principal a producir <-----'
            If NQ = 1 Then
                txt_code.Text = scode
                txt_desc.Text = sname
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

    'Declarar el evento disparo fin de recepcion    
    Public Event RxFin(ByVal Trama As String)
    'String de recepcion utilizado como buffer    
    Private PortSerie_Recepcion As String = ""

#Region "MANEJO DEL LISTBOX DE ITEMS"

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            lbl_item.Text = ListBox1.Text  '.Substring(InStr(ListBox1.SelectedItem.ToString, "|"), ListBox1.SelectedItem.ToString.Length - InStr(ListBox1.SelectedItem.ToString, "|"))
            Call CargaPesos()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Añade_item(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.Items.Count > 19 Then
            MsgBox("Límite de items (20) excedido", MsgBoxStyle.Exclamation, "PROFIL")
        Else
            If dts.Tables.Contains("vLISTTMP") Then
                Dim foundRow() As Data.DataRow
                foundRow = dts.Tables("vLISTTMP").Select("ItemNo = '" & txt_code.Text & "'")

                If foundRow.Length > 0 Then
                    MsgBox("Item ya ingresado", MsgBoxStyle.Exclamation, "PROFIL")
                Else
                    DatosInsUpd("U_SP_FIB_INS_OPROM0", 0, False) '
                    If OCN.State = ConnectionState.Closed Then OCN.Open()
                    Try
                        cmd.ExecuteNonQuery()
                        If cmd.Parameters("@msg").Value.ToString() <> "" Then
                            MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "FIBRAFIL")
                    End Try

                    Call CargaDatos()
                End If
            End If

            txt_code.Text = ""
            txt_desc.Text = ""
            txt_code.Focus()
        End If
    End Sub

    
#End Region

#Region "Eliminaciones"

    Private Sub BTN_Elimina_Pesos(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delpesos.Click
        Dim X, X1 As Integer
        X = MessageBox.Show("Desea eliminar todos los pesos?", "PROFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If X = 6 Then
            X1 = MessageBox.Show("Recuerde, de eliminar todos los pesos no podra recuperarlos, procederá con eliminarlos?", "PROFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If X1 = 6 Then
                DatosInsUpd("U_SP_FIB_DEL_OPROM", 3, False) '
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()
                    If cmd.Parameters("@msg").Value.ToString() <> "" Then
                        MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "FIBRAFIL")
                End Try

                Call CargaDatos()
            End If
        End If
    End Sub

    Private Sub ListBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyDown 'Elimina item
        If e.KeyCode = Keys.Delete Then
            Call DelItem()
        End If
    End Sub

    Private Sub ListBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox2.KeyDown  ' Elimina item produccion
        If e.KeyCode = Keys.Delete Then
            Call DelPeso(False)
        End If
    End Sub

    Private Sub ListBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox3.KeyDown   'Elimina scrap
        If e.KeyCode = Keys.Delete Then
            Call DelPeso(True)
        End If
    End Sub

    Sub DelItem()
        If ListBox2.Items.Count > 0 Then
            MessageBox.Show("Para eliminar un  ítem, primero elimine todos sus pesos asociados.", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Else

            DatosInsUpd("U_SP_FIB_DEL_OPROM", 1, False) '
            If OCN.State = ConnectionState.Closed Then OCN.Open()
            Try
                cmd.ExecuteNonQuery()
                If cmd.Parameters("@msg").Value.ToString() <> "" Then
                    MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "FIBRAFIL")
            End Try

            Call CargaDatos()

            If 20 <= CInt(lbl_count.Text) Then
                txt_code.Enabled = True
            End If
        End If
    End Sub

    Sub DelPeso(ByVal is_Scrap As Boolean)
        Dim X As Integer
        X = MessageBox.Show("Seguro de eliminar peso?", "PROFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If X = 6 Then

            DatosInsUpd("U_SP_FIB_DEL_OPROM", 2, is_Scrap) '
            If OCN.State = ConnectionState.Closed Then OCN.Open()
            Try
                cmd.ExecuteNonQuery()
                If cmd.Parameters("@msg").Value.ToString() <> "" Then
                    MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "FIBRAFIL")
            End Try


            Call CargaPesos()

        End If
    End Sub

#End Region

    Private Sub frm_prodMasiva_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

        Call CargaDatos()
        Call CargaCombos()

    End Sub

    Sub Imprime_Codebar(ByVal Codebar As String, ByVal Peso As Decimal) '06-10-14
        Try
            ''RUTA DEL ARCHIVO DE ETIQUETA 
            Dim RUTA As String

            If rdb_no.Checked = True Then
                RUTA = Application.StartupPath & "\" + "eti_SPeso.prn" '
            Else
                RUTA = Application.StartupPath & "\" + "eti_CPeso.prn" '
            End If

            ''LECTURA DEL ARCHIVO PARA GUARDARLO EN UN STRING
            Dim arc1 As New StreamReader(RUTA)
            Dim etiqueta As String = arc1.ReadToEnd()

            ''REEMPLAZA LOS VALORES DE LA ETIQUETA
            etiqueta = etiqueta.Replace("[NPD]", "PM")
            etiqueta = etiqueta.Replace("[FP]", Date.Today.ToShortDateString)
            etiqueta = etiqueta.Replace("[IDITEM]", ListBox1.SelectedValue.ToString)
            etiqueta = etiqueta.Replace("[DESPRO]", lbl_item.Text)

            etiqueta = etiqueta.Replace("[codbar]", Codebar)
            ''SIGUINTE LINEA SOLO PARA EL AREA DE CABOS 
            If rdb_si.Checked = True Then
                etiqueta = etiqueta.Replace("[PESO]", Peso)
            End If
            etiqueta = etiqueta.Replace("\[""]", "''")

            ''IMPRIME USANDO DLL DE IMPRESION OPCION SendStringToPrinter()
            clsprinter.Class1.SendStringToPrinter("PROFIL", etiqueta)
            ' ''  MessageBox.Show("Etiqueta Impresa", "FIBRAFIL", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ''  End If

        Catch ex As Exception
            ' MessageBox.Show(ex.StackTrace)
        End Try
    End Sub

    Private Sub SetText(ByVal text As String)
        If ListBox1.SelectedIndex >= 0 Then
            Try
                'validacion solo para balanza chilca  --20/07/17
                If Form1.vs_sede = "01" Then
                    text = Replace(text, "kg", "")
                Else
                    If text.Length > 0 And text.Contains("kg") And text.Contains("Net") Then
                        text = Replace(text, "Net", "")
                        text = Replace(text, "kg", "")
                    End If
                End If



        DatosInsUpd("U_SP_FIB_INS_OPROM1", Trim(text), False) '
        ''''' TEST

        cmd.Parameters.Clear()
        cmd.Connection = OCN
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "U_SP_FIB_INS_OPROM1"
        With cmd.Parameters
            .Add(New SqlParameter("@ItemNo", SqlDbType.Text)).Value = ListBox1.SelectedValue.ToString  ' Codigo de item
            If rb_pofic.Checked = True Then
                .Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = CDec(Trim(text)) ' Peso producido,
            Else
                .Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = CDec(Trim(text)) * -1 ' Peso scrap,
                    End If

                    .Add(New SqlParameter("@U_FIB_SEDE", SqlDbType.Text)).Value = Form1.vs_sede
                    .Add(New SqlParameter("@U_FIB_TELAR", SqlDbType.VarChar)).Value = cmb_maq.SelectedValue
                    .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output



        End With


        If OCN.State = ConnectionState.Closed Then OCN.Open()
        cmd.ExecuteNonQuery()
        Call CargaPesos()

        If rb_pofic.Checked = True Then Call Imprime_Codebar(cmd.Parameters("@msg").Value.ToString(), CDec(Trim(text)))

            Catch ex As Exception
            'MsgBox(text & vbCrLf & _
            '    "Message ---" & vbCrLf & ex.Message & vbCrLf & _
            '    "HelpLink ---" & vbCrLf & ex.HelpLink & vbCrLf & _
            '    "Source ---" & vbCrLf & ex.Source & vbCrLf & _
            '    "StackTrace ---" & vbCrLf & ex.StackTrace & vbCrLf & _
            '    "TargetSite ---" & vbCrLf & ex.TargetSite.ToString(), MsgBoxStyle.Critical, "PROFIL")
        End Try
        Else
            MsgBox("Antes de pesar debe seleccionar un ítem.", MsgBoxStyle.Exclamation, "PROFIL")
        End If
    End Sub

    Sub Contadores()
        Try
            Dim in_pesos As Integer = 0
            Dim in_pesosS As Integer = 0

            For X As Integer = 0 To ListBox2.Items.Count - 1
                If ListBox2.GetItemText(ListBox2.Items.Item(X)) > 0 Then
                    in_pesos = in_pesos + 1
                End If
            Next

            For X As Integer = 0 To ListBox3.Items.Count - 1
                If ListBox3.GetItemText(ListBox3.Items.Item(X)) > 0 Then
                    in_pesosS = in_pesosS + 1
                End If
            Next

            Dim db_sumpeso As Double
            Dim db_sumpesoS As Double

            For X As Integer = 0 To ListBox2.Items.Count - 1
                db_sumpeso = db_sumpeso + ListBox2.GetItemText(ListBox2.Items.Item(X))
            Next

            For X As Integer = 0 To ListBox3.Items.Count - 1
                db_sumpesoS = db_sumpesoS + ListBox3.GetItemText(ListBox3.Items.Item(X))
            Next

            lbl_count.Text = ListBox1.Items.Count.ToString
            lbl_countbul.Text = in_pesos
            lbl_pesotot.Text = db_sumpeso

            lbl_countbul_s.Text = in_pesosS
            lbl_pesotot_s.Text = db_sumpesoS
        Catch ex As Exception
            ' MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Sub CargaDatos()
        '  ListBox1.DataSource = Nothing
        lbl_idsede.Text = Form1.vs_sede

        Dim dap1 As New SqlDataAdapter("Select	ItemNo, ItemName from OPROM0    inner join SBO_Fibrafil..OITM  on Itemcode  collate SQL_Latin1_General_CP850_CI_AS =  ItemNo  collate SQL_Latin1_General_CP850_CI_AS where U_FIB_AREA = '" & Form1.vs_idArea & "' and 	U_FIB_SEDE =   '" & Form1.vs_sede & "' and U_fib_telar = '" & cmb_maq.SelectedValue & "' order by RECORDKEY", OCN)
        dap1.SelectCommand.CommandType = CommandType.Text

        If dts.Tables.Contains("vLISTTMP") Then
            dts.Tables("vLISTTMP").Clear()
        End If
        dap1.Fill(dts, "vLISTTMP")

        ' Referenciamos el objeto DataTable
        Dim dt As DataTable = dts.Tables("vLISTTMP")

        With ListBox1
            .DataSource = dt
            .DisplayMember = "ItemName"
            .ValueMember = "Itemno"
        End With

        Contadores()
    End Sub

    Sub CargaPesos()
        Try
            ' ListBox1.DataSource = Nothing
            Dim dap1 As New SqlDataAdapter("Select Isnull(Convert(decimal(14,2), pesobob,2),0.0) As 'PesoBob', idlin as 'Lin'   from OPROM1 T1 inner join OPROM0 T0 on T1.recordkey = T0.recordkey  where Isnull(isScrap,'')<>'Y' and  T0.ItemNo  = '" & ListBox1.SelectedValue.ToString & "' and U_FIB_SEDE =   '" & Form1.vs_sede & "' and U_fib_telar = '" & cmb_maq.SelectedValue & "' order by idlin asc", OCN)
            dap1.SelectCommand.CommandType = CommandType.Text
            If dts.Tables.Contains("vLISPESOS") Then
                dts.Tables("vLISPESOS").Clear()
            End If

            dap1.Fill(dts, "vLISPESOS")

            ' Referenciamos el objeto DataTable
            Dim dt As DataTable = dts.Tables("vLISPESOS")

            With ListBox2
                .DataSource = dt
                .DisplayMember = "PesoBob"
                .ValueMember = "PesoBob"
            End With

            ' ListBox2.DataSource = Nothing
            Dim dap2 As New SqlDataAdapter("Select Isnull(Convert(decimal(14,2), pesobob,2),0.0) As 'PesoBob', idlin as 'Lin'   from OPROM1 T1 inner join OPROM0 T0 on T1.recordkey = T0.recordkey  where Isnull(isScrap,'')='Y' and  T0.ItemNo  ='" & ListBox1.SelectedValue.ToString & "' and 	U_FIB_SEDE =   '" & Form1.vs_sede & "' and U_fib_telar = '" & cmb_maq.SelectedValue & "' order by idlin asc", OCN)
            dap2.SelectCommand.CommandType = CommandType.Text
            If dts.Tables.Contains("vLISPESOSS") Then
                dts.Tables("vLISPESOSS").Clear()
            End If

            dap2.Fill(dts, "vLISPESOSS")

            ' Referenciamos el objeto DataTable
            Dim dt2 As DataTable = dts.Tables("vLISPESOSS")

            With ListBox3
                .DataSource = dt2
                .DisplayMember = "PesoBob"
                .ValueMember = "PesoBob"
            End With

            Contadores()

            lbl_idlin.DataBindings.Add("text", dts.Tables("vLISPESOS"), "lin")
            lbl_linS.DataBindings.Add("text", dts.Tables("vLISPESOSS"), "lin")
        Catch ex As Exception

        End Try

    End Sub

    Sub CargaCombos()
        Dim ds As New DataSet()

        'COMBO MAQUINA
        Dim DAP_MAc As New SqlDataAdapter("Select Code, Descripcion As 'Maquina' from  OFIBMAC where STDMAC='A' and u_fib_sede = '" + Form1.vs_sede + "'", OCN)
        DAP_MAc.Fill(ds, "tMac")
        cmb_maq.DataSource = ds.Tables("tMac")
        cmb_maq.DisplayMember = "Maquina"
        cmb_maq.ValueMember = "Code"

        'COMBOBOX OPERARIO'
        Dim dap_ope As New SqlClient.SqlDataAdapter("Select T0.Code, T0.Name from OFIBEMPL T0 inner join OFIBAREA T1 on T0.U_FIB_AREA = T1.CODE where T0.u_FIB_CARGO  = 'O' and T1.PP='Y' AND ISNULL(T0.INACTIVO,'N')<>'Y'", OCN)
        dap_ope.Fill(ds, "Operario")
        cmb_ope.DataSource = ds.Tables("Operario")
        cmb_ope.DisplayMember = "Name"
        cmb_ope.ValueMember = "Code"

        'COMBOBOX AYUDANTE'
        Dim dap_ayud As New SqlClient.SqlDataAdapter("Select T0.Code, T0.Name from OFIBEMPL T0 inner join OFIBAREA T1 on T0.U_FIB_AREA = T1.CODE where T0.u_FIB_CARGO  = 'A' and T1.PP='Y' AND ISNULL(T0.INACTIVO,'N')<>'Y'", OCN)
        dap_ayud.Fill(ds, "Ayudante")
        cmb_ayud.DataSource = ds.Tables("Ayudante")
        cmb_ayud.DisplayMember = "Name"
        cmb_ayud.ValueMember = "Code"

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If lbl_item.Text <> "" Then
            If lbl_countbul.Text = 0 Or lbl_pesotot.Text = 0 Then
                MsgBox("Por favor, revisar. No hay items por procesar.", MsgBoxStyle.Exclamation, "PROFIL")
            Else
                If grp_combos.Visible = False Then
                    grp_combos.Visible = True
                End If
            End If
        Else
            MsgBox("Por favor, seleccione un Item correctamente.", MsgBoxStyle.Exclamation, "PROFIL")
        End If
    End Sub

    Sub DatosInsUpd(ByVal NameProced As String, ByVal db_peso As Double, ByVal is_Scrap As Boolean)
        If NameProced = "U_SP_FIB_INS_OPROM0" Then
            Try
                cmd.Parameters.Clear()
                cmd.Connection = OCN
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = NameProced
                With cmd.Parameters
                    .Add(New SqlParameter("@ItemNo", SqlDbType.Text)).Value = txt_code.Text ' Codigo de item
                    .Add(New SqlParameter("@ProducQuantity", SqlDbType.Decimal)).Value = 0 ' Cant producida, se manda 0 pq hasta ese momento solo se busca registrar los items
                    .Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = 0 ' Peso producido, se manda 0 pq hasta ese momento solo se busca registrar los items
                    .Add(New SqlParameter("@COMMENT", SqlDbType.Text)).Value = "" ' Comentario NO IMPLEMENTADO EN ESTE FORM
                    .Add(New SqlParameter("@U_FIB_OPERARIO", SqlDbType.Text)).Value = cmb_ope.SelectedValue  'ComboBox5.SelectedValue() 'codigo de operario
                    .Add(New SqlParameter("@U_FIB_AYUDANTE", SqlDbType.Text)).Value = cmb_ayud.SelectedValue  'ComboBox4.SelectedValue 'codigo de ayudante
                    .Add(New SqlParameter("@U_FIB_TELAR", SqlDbType.Text)).Value = cmb_maq.SelectedValue 'codigo de maquina
                    .Add(New SqlParameter("@U_FIB_AREA", SqlDbType.Text)).Value = Form1.vs_idArea 'Area de produccion
                    .Add(New SqlParameter("@U_FIB_SEDE", SqlDbType.Text)).Value = Form1.vs_sede ' SEDE DE LA EMPRESA
                    .Add(New SqlParameter("@HOST", SqlDbType.Text)).Value = My.Computer.Name
                    .Add(New SqlParameter("@Createfor", SqlDbType.Text)).Value = Form1.vs_idUser  'usuario

                    .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                    .Add(New SqlParameter("@FK", SqlDbType.BigInt)).Direction = ParameterDirection.Output '
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        If NameProced = "U_SP_FIB_INS_OPROM1" Then
            Try
                cmd.Parameters.Clear()
                cmd.Connection = OCN
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = NameProced
                With cmd.Parameters
                    .Add(New SqlParameter("@ItemNo", SqlDbType.Text)).Value = ListBox1.SelectedValue.ToString ' Codigo de item
                    .Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = db_peso ' Peso producido, se manda 0 pq hasta ese momento solo se busca registrar los items
                    .Add(New SqlParameter("@U_FIB_SEDE", SqlDbType.VarChar)).Value = Form1.vs_sede  '
                    .Add(New SqlParameter("@U_FIB_TELAR", SqlDbType.VarChar)).Value = cmb_maq.SelectedValue  '
                    .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        If NameProced = "U_SP_FIB_DEL_OPROM" Then
            'Try
            cmd.Parameters.Clear()
            cmd.Connection = OCN
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = NameProced

            Dim d_peso As Decimal
            Dim i_nline As Long
            Try
                If is_Scrap = True Then
                    i_nline = IIf(lbl_linS.Text = "", 0, lbl_linS.Text)
                    d_peso = IIf(IsDBNull(ListBox3.SelectedValue.ToString()), 0, ListBox3.SelectedValue.ToString())
                Else
                    i_nline = IIf(lbl_idlin.Text = "", 0, lbl_idlin.Text)
                    d_peso = IIf(IsDBNull(ListBox2.SelectedValue.ToString()), 0, ListBox2.SelectedValue.ToString())
                End If
            Catch ex As Exception

            End Try

            With cmd.Parameters
                .Add(New SqlParameter("@ItemNo", SqlDbType.Text)).Value = ListBox1.SelectedValue.ToString ' Codigo de item
                If db_peso = 1 Then
                    .Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = 0  ' Peso producido, se manda 0 pq hasta ese momento solo se busca registrar los items
                Else
                    .Add(New SqlParameter("@ProducWeight", SqlDbType.Decimal)).Value = d_peso
                End If

                .Add(New SqlParameter("@IndexPeso", SqlDbType.Int)).Value = i_nline
                .Add(New SqlParameter("@U_FIB_SEDE", SqlDbType.Text)).Value = Form1.vs_sede ' SEDE DE LA EMPRESA
                .Add(New SqlParameter("@U_FIB_TELAR", SqlDbType.VarChar)).Value = cmb_maq.SelectedValue  '
                .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                .Add(New SqlParameter("@opt", SqlDbType.Int)).Value = db_peso
            End With
            ' Catch ex As Exception
            'MsgBox(ex.Message)
            'End Try
        End If

        If NameProced = "U_FIB_PROD_MAS" Then
            Try
                cmd.Parameters.Clear()
                cmd.Connection = OCN
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = NameProced
                With cmd.Parameters
                    .Add(New SqlParameter("@Item", SqlDbType.Text)).Value = ListBox1.SelectedValue.ToString ' Codigo de item
                    .Add(New SqlParameter("@Operario", SqlDbType.VarChar)).Value = cmb_ope.SelectedValue ' Peso producido, se manda 0 pq hasta ese momento solo se busca registrar los items
                    .Add(New SqlParameter("@Ayudante", SqlDbType.VarChar)).Value = cmb_ayud.SelectedValue ' Codigo de item
                    .Add(New SqlParameter("@Maquina", SqlDbType.VarChar)).Value = cmb_maq.SelectedValue ' Peso producido, se manda 0 pq hasta ese momento solo se busca registrar los items
                    .Add(New SqlParameter("@sede", SqlDbType.VarChar)).Value = Form1.vs_sede '
                    .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        DatosInsUpd("U_FIB_PROD_MAS", 0, False) '
        If OCN.State = ConnectionState.Closed Then OCN.Open()
        Try
            cmd.ExecuteNonQuery()
            If cmd.Parameters("@msg").Value.ToString() <> "" Then
                MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
            Else
                MsgBox("OK")
                Call CargaDatos()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "FIBRAFIL")
        Finally
            grp_combos.Visible = False
        End Try
    End Sub

    Private Sub frm_prodMasiva_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        If grp_combos.Visible = True Then grp_combos.Visible = False
    End Sub

    Private Sub cmb_maq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_maq.Click
        ''Dim lista_Maqs As List(Of String) = New List(Of String)
        ''Dim Lista As New ArrayList

        'Dim choices = New Dictionary(Of String, String)()

        '' Dim DAP_MAc As New SqlDataAdapter("Select Code, Descripcion As 'Maquina' from  OFIBMAC where STDMAC='A' and IDAREA ='" + vs_idArea + "'", OCN) 'cmbArea.SelectedValue.ToString'
        'Try
        '    Dim com As SqlCommand = New SqlCommand("Select Code, Descripcion As 'Maquina' from  OFIBMAC where STDMAC='A' and IDAREA ='" + Form1.vs_idArea + "'", OCN)
        '    If OCN.State = ConnectionState.Closed Then OCN.Open()

        '    Dim dr As SqlDataReader = com.ExecuteReader()
        '    ' Recorremos el dataReader

        '    '        lista_Maqs.Add(dr.GetValue(0).ToString())
        '    '        Lista.Add(dr.GetValue(1).ToString) ', dr.GetValue(1).ToString))


        '    While (dr.Read())
        '        choices(dr.GetValue(0)) = dr.GetValue(1)
        '    End While
        '    dr.Close()


        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'Finally
        '    OCN.Close()
        'End Try

        ''cmb_maq.DataSource = Lista
        ''cmb_maq.DisplayMember = "Maquina"
        ''cmb_maq.ValueMember = "Code"


        'cmb_maq.DataSource = New BindingSource(choices, Nothing)
        'cmb_maq.DisplayMember = "Maquina"
        'cmb_maq.ValueMember = "Code"
    End Sub

#Region "Mueve groupbox de combos"

    Private Sub GroupBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grp_combos.MouseDown
        Dragging = True
        ' Note positions of cursor when pressed
        cursorX = e.X
        CursorY = e.Y
    End Sub

    Private Sub GroupBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grp_combos.MouseMove
        If Dragging Then
            Dim ctrl As Control = CType(grp_combos, Control)
            ' Move the control according to mouse movement
            ctrl.Left = (ctrl.Left + e.X) - cursorX
            ctrl.Top = (ctrl.Top + e.Y) - CursorY
            ' Ensure moved control stays on top of anything it is dragged on to
            ctrl.BringToFront()
        End If
    End Sub

    Private Sub GroupBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grp_combos.MouseUp
        ' Reset the flag
        Dragging = False
    End Sub

#End Region

#Region "COMUNICATION COM - PRINTER"

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

    Private Sub PtoSerial_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles sppuerto.DataReceived
        'codigo original hasta el 20-07-17
        '*********************************
        '*********************************
        If Form1.vs_sede = "01" Then
            DataIn = Me.sppuerto.ReadExisting
            If DataIn <> String.Empty Then
                SetText(DataIn)
            End If
        Else
            'codigo para la balanza de Chilca MT-IND231   (3/8/17)
            DataIn = Me.sppuerto.ReadLine
            DataIn = DataIn.Replace(vbCrLf, " ")
            DataIn = Replace(DataIn, Chr(13), "")
            DataIn = Replace(DataIn, Chr(10), "")
            DataIn = Replace(DataIn, " ", "")

            If DataIn <> String.Empty Then
                If DataIn.Contains("Net") = 1 Then
                    DataIn = "Net" & DataIn
                End If
                SetText(DataIn)
            End If
        End If



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

#End Region


    Private Sub cmb_maq_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_maq.Validated
        Call CargaDatos()
    End Sub
End Class

