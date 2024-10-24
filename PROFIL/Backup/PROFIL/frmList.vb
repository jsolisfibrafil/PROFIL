Imports System.Data.SqlClient
Imports System.Windows.Forms
Public Class frmList

    Public Shared X_form As Integer
    Public Shared scode, sname, sumed, swhs, scode0, sname0, swhs0 As String
    Public Shared NQ As Integer 'NUMERO DE CONSULTA 
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros
    Dim WithEvents FQ As New frmConsulta
    Dim dts As New DataSet
    Dim cmd As New SqlCommand()
    Dim id As String
    Event PasaVars()
    Private DvDetalle As DataView
    Private DvCabecera As DataView
#Region "Manejo de data Ingresar - Actualizar y Eliminacion"

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()
            Case "&Actualizar"
                DatosInsUpd("U_SP_FIB_UPD_LMAT")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Artículo actualizado satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
                    btn_list.Enabled = False
                    Button1.Enabled = False
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    OCN.Close()
                End Try
            Case "&Crear"
                DatosInsUpd("U_SP_FIB_INS_LMAT")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()

                    '  Grabando Detalle...

                    With cmd
                        .Parameters.Clear()
                        .Connection = OCN
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "U_SP_FIB_INS_LMAT1"
                        .Parameters.Add(New SqlParameter("@ItemcodePa", SqlDbType.Char))
                        .Parameters.Add(New SqlParameter("@LineNum", SqlDbType.Int))
                        .Parameters.Add(New SqlParameter("@ItemCode", SqlDbType.VarChar))
                        .Parameters.Add(New SqlParameter("@UOM", SqlDbType.VarChar))
                        .Parameters.Add(New SqlParameter("@Quantity", SqlDbType.Decimal))
                        .Parameters.Add(New SqlParameter("@Whscode1", SqlDbType.VarChar))

                        For k As Integer = 0 To DvDetalle.Count - 1
                            .Parameters("@ItemcodePa").Value = txtCode.Text
                            .Parameters("@LineNum").Value = CInt(DvDetalle.Item(k)("LineNum"))
                            .Parameters("@ItemCode").Value = DvDetalle.Item(k)("CODIGO")
                            .Parameters("@UOM").Value = DvDetalle.Item(k)("UOM")
                            .Parameters("@Quantity").Value = CDec(DvDetalle.Item(k)("CANTIDAD"))
                            .Parameters("@Whscode1").Value = DvDetalle.Item(k)("ALMACEN")
                            .ExecuteNonQuery()
                        Next k
                    End With
                    MsgBox("Lista creada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
                    Me.ContextMenuStrip = Me.ContextMenuStrip1
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    OCN.Close()
                End Try
        End Select
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Dim X As Integer
        X = MessageBox.Show("Desea eliminar este registro", "Fibrafil", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If X = 6 Then
            cmd.Connection = OCN
            cmd.CommandType = CommandType.Text
            cmd.CommandText = ("Delete from OFIBLIST0 where Docentry = " & txtID.Text)
            If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            OCN.Open()
            Try
                cmd.ExecuteNonQuery()
                MsgBox("Lista : " + txtDesc.Text + " ha sido eliminada", MsgBoxStyle.Information, "Fibrafil")
                btnConfirma.Text = "&Ok"
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                OCN.Close()
            End Try
        End If
    End Sub

#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub DatosInsUpd(ByVal NameProced As String)
        'Dim d_pErcent As Decimal
        'd_pErcent = CDec(cmbPercent.SelectedItem) 'ToString Is DBNull.Value, 0, cmbPercent.SelectedValue.ToString)

        cmd.Parameters.Clear()
        cmd.Connection = OCN
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = NameProced

        ' cmd.Parameters.Add(New SqlParameter("@Docentry", SqlDbType.Int)).Value = txtID.Text ' code de Item
        cmd.Parameters.Add(New SqlParameter("@Itemcodepa", SqlDbType.Text)).Value = txtCode.Text ' code de Item
        cmd.Parameters.Add(New SqlParameter("@Qitm", SqlDbType.Decimal)).Value = CDec(txtCant.Text) ' Cantidad de item a producir
        cmd.Parameters.Add(New SqlParameter("@Whscode", SqlDbType.Text)).Value = txtAlmacen.Text  ' Codigo de almacen de item padre
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        Me.ContextMenuStrip = Nothing
        LimpiaCajas(Me.TabControl1)
        btn_list.Enabled = True
        Button1.Enabled = True
        btnConfirma.Text = "&Crear"
        btn_list.Focus()

        posi.Position = posi.Count - 1

        DvDetalle = dts.Tables("vLIST1").DefaultView
        DvDetalle.AllowEdit = True
        DvDetalle.AllowNew = True

        '  DvDetalle.AddNew()
        DvDetalle.RowFilter = "Docentry = -1"
        dtgdata1.SetDataBinding(Nothing, Nothing)
        dtgdata1.SetDataBinding(DvDetalle, "")
        AddNewDataRowView()

        btnFirst.Enabled = False
        btnBefore.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False
    End Sub

            
    Sub LimpiaCajas(ByVal Control As TabControl)
        Dim Pag As TabPage
        Dim Cajas As New Control
        For Each Pag In Control.TabPages
            For Each Cajas In Pag.Controls
                If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is TextBox Then Cajas.Text = ""
                'If TypeOf Cajas Is ComboBox Then Cajas.Text = ""
            Next
        Next
    End Sub

    Private Sub frmList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
        Call LlenarData()
        Call ArmaGrid()
        Call ArmaGridList()
        If btnConfirma.Text = "&Ok" Then btn_list.Enabled = False
        If btnConfirma.Text = "&Ok" Then Button1.Enabled = False
    End Sub

#Region "Manejo de datos en formulario (visualizacion)"
    Sub LlenarData()
        txtID.DataBindings.Add("text", dts.Tables("vLIST"), "ID")
        txtCode.DataBindings.Add("text", dts.Tables("vLIST"), "CODIGO")
        txtDesc.DataBindings.Add("text", dts.Tables("vLIST"), "DESCRIPCION")
        txtAlmacen.DataBindings.Add("text", dts.Tables("vLIST"), "ALMACEN")
        txtCant.DataBindings.Add("text", dts.Tables("vLIST"), "CANT")
        dtgdata1.SetDataBinding(DvDetalle, "")
        dtgList.SetDataBinding(dts.Tables("vlist"), "")
        DvDetalle.RowFilter = "Docentry = " + txtID.Text
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
            Dim dap As New SqlDataAdapter("select T0.Docentry As 'ID',T0.ItemCode As 'CODIGO', T1.ItemName As 'DESCRIPCION', T0.Qitm AS 'CANT', T0.WhsCode aS 'ALMACEN'" & _
             "FROM dbo.OFIBLIST0 t0, dbo.OFIBITM T1 where T0.Itemcode = T1.Itemcode order by cast(T0.Docentry as int)", OCN)
            'dbo.OFIBCOLOR T1, dbo.OGRPITM T2 WHERE T0.ItemsGroupCode = T2.ItmsGrpCod AND T0.U_FIB_COLOR = T1.Id_Color", OCN)
            dap.SelectCommand.CommandType = CommandType.Text
            dap.Fill(dts, "vLIST")

            DvCabecera = dts.Tables("vlist").DefaultView

            posi = CType(BindingContext(dts.Tables("vLIST")), CurrencyManager)
            posi.Position = posi.Count + 1
            txtposi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)


            Dim dap1 As New SqlDataAdapter("U_SP_SELDETLISTA", OCN)
            dap1.SelectCommand.CommandType = CommandType.StoredProcedure
            dap1.Fill(dts, "vLIST1")

            dtgList.DataSource = dts.Tables("vLIST").DefaultView
            DvDetalle = dts.Tables("vLIST1").DefaultView
            DvDetalle.AllowEdit = False
            DvDetalle.AllowNew = False

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

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
        txtposi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        DvDetalle.RowFilter = "Docentry = " + txtID.Text
    End Sub

#End Region

#Region "Navegacion entre registros"
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

    Private Sub txtID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
        id = txtID.Text
    End Sub

    Sub ArmaGrid()
        dtgdata1.TableStyles.Clear()
        dtgdata1.CaptionText = "COMPONENTES DEL ARTICULO"
        dtgdata1.CaptionBackColor = Color.Navy
        dtgdata1.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        oEstiloGrid.MappingName = "vLIST1"
        oEstiloGrid.BackColor = Color.LightGoldenrodYellow
        oEstiloGrid.AlternatingBackColor = Color.Aquamarine

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
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "Line"
        oColGrid.MappingName = "LineNum"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "N° Item"
        oColGrid.MappingName = "CODIGO"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ARTICULO"
        oColGrid.MappingName = "DESCRIPCION"
        oColGrid.Width = 200
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        'oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "U.MED"
        oColGrid.MappingName = "UOM"
        oColGrid.Width = 90
        ' oColGrid.Format = "dd/mm/yyyy"
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
        oColGrid.HeaderText = "ALMACEN "
        oColGrid.MappingName = "ALMACEN"
        oColGrid.Width = 90
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        dtgdata1.TableStyles.Add(oEstiloGrid)
    End Sub

    Sub ArmaGridList()
        dtgList.TableStyles.Clear()
        dtgList.CaptionText = "LISTADO DE MATERIALES"
        dtgList.CaptionBackColor = Color.Navy
        dtgList.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        oEstiloGrid.MappingName = "vLIST1"
        oEstiloGrid.BackColor = Color.LightGoldenrodYellow
        oEstiloGrid.AlternatingBackColor = Color.Aquamarine

        Dim oColGrid As DataGridTextBoxColumn

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ID"
        oColGrid.MappingName = "ID"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.ReadOnly = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "CODIGO"
        oColGrid.MappingName = "CODIGO"
        oColGrid.Width = 0
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "DESCRIPCION"
        oColGrid.MappingName = "DESCRIPCION"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.ReadOnly = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "CANTIDAD"
        oColGrid.MappingName = "cant"
        oColGrid.Width = 200
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        'oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "ALMACEN"
        oColGrid.MappingName = "ALMACEN"
        oColGrid.Width = 90
        ' oColGrid.Format = "dd/mm/yyyy"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        dtgList.TableStyles.Add(oEstiloGrid)
    End Sub

    Private Sub AddNewDataRowView()
        Try
            'If OCN.State = 0 Then OCN.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        For X As Int32 = 0 To 49
            Dim rowView As DataRowView = DvDetalle.AddNew
            rowView("Docentry") = "-1"
            rowView("LineNum") = X
            rowView("DESCRIPCION") = ""
            rowView("CODIGO") = "" 'R2
            rowView("UOM") = ""
            rowView("CANTIDAD") = 0
            rowView("ALMACEN") = ""
            rowView.EndEdit()
        Next X

        DvDetalle.AllowNew = False
        'OCN.Close()
        ArmaGrid()
    End Sub

    Private Sub dtgdata1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtgdata1.KeyPress
        If btnConfirma.Text = "&Crear" Then
            If (dtgdata1.CurrentCell.ColumnNumber = 2 Or dtgdata1.CurrentCell.ColumnNumber = 3) Then
                If e.KeyChar = ChrW(Keys.Return) Then
                    NQ = 2
                    Dim Fq As New frmConsulta
                    cVars.NQP = 5 'CONSULTA PARA ITEMS
                    Fq.ShowDialog()
                    Call Recibevar()
                End If
            End If
        End If
    End Sub

    Public Sub Recibevar() Handles FQ.PasaVars
        If scode0 = Nothing Then scode0 = txtCode.Text
        If sname0 = Nothing Then sname0 = txtDesc.Text
        If swhs0 = Nothing Then swhs0 = txtAlmacen.Text
        txtCode.Text = scode0
        txtDesc.Text = sname0
        txtAlmacen.Text = swhs0

        If scode = Nothing Then scode = dtgdata1.Item(dtgdata1.CurrentRowIndex, 2).ToString
        If sname = Nothing Then sname = dtgdata1.Item(dtgdata1.CurrentRowIndex, 3).ToString
        If sumed = Nothing Then sumed = dtgdata1.Item(dtgdata1.CurrentRowIndex, 4).ToString
        If swhs = Nothing Then swhs = dtgdata1.Item(dtgdata1.CurrentRowIndex, 6).ToString
        dtgdata1.Item(dtgdata1.CurrentRowIndex, 2) = IIf(scode = txtCode.Text, "", scode)
        dtgdata1.Item(dtgdata1.CurrentRowIndex, 3) = IIf(scode = txtCode.Text, "", sname)
        dtgdata1.Item(dtgdata1.CurrentRowIndex, 4) = IIf(scode = txtCode.Text, "", sumed)
        dtgdata1.Item(dtgdata1.CurrentRowIndex, 6) = IIf(scode = txtCode.Text, "", swhs)
    End Sub

    Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
        Dim formQuery As New frmConsulta
        cVars.NQP = 5 'CONSULTA PARA ITEMS
        NQ = 1
        formQuery.ShowDialog()
        Call Recibevar()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim formQuery As New frmConsulta
        cVars.NQP = 3 'CONSULTA PARA ALMACENES
        NQ = 3
        formQuery.ShowDialog()
        Call Recibevar()
    End Sub

    Private Sub txtCant_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCant.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            Beep()
        End If
    End Sub

    Private Sub TEXTOS(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress, txtAlmacen.KeyPress, txtCant.KeyPress, txtDesc.KeyPress
        If btnConfirma.Text = "&Ok" Then
            btnConfirma.Text = "&Actualizar"
            btn_list.Enabled = True
            Button1.Enabled = True
        End If
    End Sub

    Private Sub dtgdata1_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles dtgdata1.Navigate

    End Sub
End Class