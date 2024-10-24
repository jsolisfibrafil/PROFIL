Imports PROFIL.MForm
Imports System.Data.SqlClient ' Importacion de las librerias correspondientes del SQL
Public Class frmItem
    Public Shared X_form As Integer
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros
    Private Dvlista As DataView
    Dim dts As New DataSet
    Dim cmd As New SqlCommand()
#Region "Eventos del formulario"
    Private Sub frmItem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = CierraForms("un", Me.Text, btnConfirma.Text)
    End Sub

    Private Sub frmItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
        Call LlenarData()
        ' Call ArmaGrid()
    End Sub
#End Region

#Region "Manejo de datos en formulario (visualizacion)"

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

            Dim dap As New SqlDataAdapter("select T0.RECORDKEY As 'ID',T0.ItemCode As 'CODIGO',T0.ItemName aS 'ITEM'," & _
            "T0.CodeBar, T0.TIPITM AS 'TIPO', T2.ItmsGrpNam aS 'GRUPO',T0.LOCKED AS 'BLOQUEADO', T0.InventoryUOM AS 'UMEDIDA'," & _
            "ISNULL(T0.U_FIB_PESO,0.00) AS 'PESO', '' AS 'COLOR',T0.U_FIB_PORCENTAJE AS 'SOMBRA' , T0.U_FIB_MEDIDA " & _
            "AS 'MEDIDA', Isnull(T0.Usertext,'') AS 'COMENTARIO',ONHAND AS 'EN STOCK' FROM dbo.OFIBITM T0 left join " & _
            "dbo.OGRPITM T2 on  T0.ItemsGroupCode = T2.ItmsGrpCod order by cast(T0.RECORDKEY as int)", OCN)
            dap.SelectCommand.CommandType = CommandType.Text
            dap.Fill(dts, "vitm")
            ' COMBOBOX COLOR
            'Dim da As New SqlClient.SqlDataAdapter("Select * from OFIBCOLOR", OCN)
            'Dim ds As New DataSet()
            'da.Fill(ds)

            'cmbColor.DisplayMember = "Dscrptn"
            'cmbColor.ValueMember = "Id_Color"
            'cmbColor.DataSource = ds.Tables(0)


            ' COMBOBOX GRUPO ITM
            Dim da_GRP As New SqlClient.SqlDataAdapter("Select * from OGRPITM", OCN)
            da_GRP.Fill(dts)
            cmbGrp.DataSource = dts.Tables(1)
            cmbGrp.DisplayMember = "ItmsGrpNam"
            cmbGrp.ValueMember = "ItmsGrpCod"


            DataGrid1.DataSource = dts.Tables("vitm").DefaultView
            posi = CType(BindingContext(dts.Tables("vitm")), CurrencyManager)
            posi.Position = posi.Count + 1
            txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Sub LlenarData()
        txtId.DataBindings.Add("text", dts.Tables("vITM"), "ID")
        txtCode.DataBindings.Add("text", dts.Tables("vITM"), "CODIGO")
        txtName.DataBindings.Add("text", dts.Tables("vITM"), "ITEM")
        txtCodeBar.DataBindings.Add("text", dts.Tables("vITM"), "CodeBar")
        cmbTip.DataBindings.Add("text", dts.Tables("vITM"), "TIPO")
        cmbGrp.DataBindings.Add("text", dts.Tables("vITM"), "GRUPO")
        txtLock.DataBindings.Add("Text", dts.Tables("vITM"), "BLOQUEADO")
        txtComment.DataBindings.Add("text", dts.Tables("vITM"), "COMENTARIO")
        txtInstock.DataBindings.Add("text", dts.Tables("vITM"), "EN STOCK")
        txtPeso.DataBindings.Add("text", dts.Tables("vITM"), "PESO")
        txtUM.DataBindings.Add("TEXT", dts.Tables("vITM"), "UMEDIDA")
        cmbMed.DataBindings.Add("Text", dts.Tables("vITM"), "MEDIDA")
        cmbPercent.DataBindings.Add("Text", dts.Tables("vITM"), "SOMBRA")
        cmbColor.DataBindings.Add("Text", dts.Tables("vITM"), "COLOR")

        DataGrid1.SetDataBinding(dts.Tables("vITM"), "")
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
        Dim Pag As TabPage
        Dim Cajas As New Control
        For Each Pag In Control.TabPages
            For Each Cajas In Pag.Controls
                If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is TextBox Then Cajas.Text = ""
                If TypeOf Cajas Is ComboBox Then Cajas.Text = ""
            Next
        Next
        txtInstock.Text = 0.0
    End Sub

#End Region
#Region "Botones del formulario"

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()
            Case "&Actualizar"
                DatosInsUpd("U_SP_FIB_UPD_ITM")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Artículo actualizado satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                    btnConfirma.Text = "&Ok"
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    OCN.Close()
                End Try
            Case "&Crear"
                DatosInsUpd("U_SP_FIB_INS_ITM")
                If OCN.State = ConnectionState.Closed Then OCN.Open()
                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Artículo creado satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
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

    Private Sub txtLock_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLock.TextChanged
        If Me.txtLock.Text = "Y" Then
            chklock.Checked = True
        Else
            chklock.Checked = False
        End If
    End Sub

    Private Sub chklock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chklock.CheckedChanged
        If chklock.Checked = True Then
            txtLock.Text = "Y"
        Else
            txtLock.Text = "N"
        End If
    End Sub

    Private Sub NoLetras(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPeso.KeyPress, txtInstock.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            Beep()
        End If
    End Sub

    Private Sub TEXTOS(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtComment.KeyPress, txtCode.KeyPress, txtInstock.KeyPress, txtLock.KeyPress, txtName.KeyPress, txtUM.KeyPress, txtPeso.KeyPress
        If btnConfirma.Text = "&Ok" Then
            btnConfirma.Text = "&Actualizar"
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        LimpiaCajas(Me.TabControl1)
        btnConfirma.Text = "&Crear"
        txtCode.ReadOnly = False
        txtCode.Focus()
    End Sub

    Sub DatosInsUpd(ByVal NameProced As String)
        Dim d_pErcent As Decimal
        d_pErcent = CDec(cmbPercent.SelectedItem) 'ToString Is DBNull.Value, 0, cmbPercent.SelectedValue.ToString)
        Try
            cmd.Parameters.Clear()
            cmd.Connection = OCN
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = NameProced
            cmd.Parameters.Add(New SqlParameter("@Itemcode", SqlDbType.Text)).Value = txtCode.Text ' code de Item
            cmd.Parameters.Add(New SqlParameter("@ItemName", SqlDbType.Text)).Value = txtName.Text ' Nombre de item
            cmd.Parameters.Add(New SqlParameter("@CodeBar", SqlDbType.Text)).Value = txtCodeBar.Text ' Nombre de item
            cmd.Parameters.Add(New SqlParameter("@TipItm", SqlDbType.Int)).Value = 1 ' IIf(cmbTip.Text Is DBNull.Value, 0, cmbTip.Text)  ' Tipo de item
            cmd.Parameters.Add(New SqlParameter("@ItmsGrpCod", SqlDbType.Int)).Value = IIf(cmbGrp.ValueMember.ToString Is DBNull.Value, 0, cmbGrp.SelectedValue.ToString) ' Grupo de articulo 
            cmd.Parameters.Add(New SqlParameter("@DfltWh", SqlDbType.Text)).Value = "XXX"
            cmd.Parameters.Add(New SqlParameter("@FrozenFor", SqlDbType.Text)).Value = txtLock.Text ' Bloqueo item
            cmd.Parameters.Add(New SqlParameter("@InvntryUOM", SqlDbType.Text)).Value = txtUM.Text ' Unidad de medida
            cmd.Parameters.Add(New SqlParameter("@NumInsale", SqlDbType.Decimal)).Value = 1
            cmd.Parameters.Add(New SqlParameter("@U_FIB_PESO", SqlDbType.Decimal)).Value = IIf(txtPeso.Text Is DBNull.Value, 0, txtPeso.Text) 'Peso
            cmd.Parameters.Add(New SqlParameter("@U_FIB_COLOR", SqlDbType.Text)).Value = cmbColor.SelectedItem.ToString  'Color
            cmd.Parameters.Add(New SqlParameter("@U_FIB_PORCENTAJE", SqlDbType.Decimal)).Value = d_pErcent ' Porcentaje
            cmd.Parameters.Add(New SqlParameter("@U_FIB_MEDIDA", SqlDbType.Text)).Value = cmbMed.SelectedItem.ToString    'Medida
            cmd.Parameters.Add(New SqlParameter("@Usertext", SqlDbType.Text)).Value = txtComment.Text ' Comentario
            cmd.Parameters.Add(New SqlParameter("@OnHand", SqlDbType.Decimal)).Value = CDec(txtInstock.Text) ' Q en almacen
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Dim X As Integer
        X = MessageBox.Show("Desea eliminar este registro", "Fibrafil", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If X = 6 Then

            cmd.Connection = OCN
            cmd.CommandType = CommandType.Text
            cmd.CommandText = ("Delete from OFIBITM where RecordKey = " & txtId.Text)

            If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            OCN.Open()
            Try
                cmd.ExecuteNonQuery()
                MsgBox("Artículo : " + txtName.Text + " ha sido eliminado", MsgBoxStyle.Information, "Fibrafil")
                btnConfirma.Text = "&Ok"
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                OCN.Close()
            End Try
        End If
    End Sub

    Sub ArmaGrid()
        DataGrid1.TableStyles.Clear()
        DataGrid1.CaptionText = "LISTADO DE ARTICULOS"
        DataGrid1.CaptionBackColor = Color.Navy
        DataGrid1.CaptionForeColor = Color.Yellow

        Dim oEstiloGrid As New DataGridTableStyle
        oEstiloGrid.MappingName = "vitm"
        oEstiloGrid.BackColor = Color.LightGoldenrodYellow
        oEstiloGrid.AlternatingBackColor = Color.Aquamarine

        Dim oColGrid As DataGridTextBoxColumn

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Center
        oColGrid.HeaderText = "ID"
        oColGrid.MappingName = "id"
        oColGrid.Width = 50
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "Codigo"
        oColGrid.MappingName = "CODIGO"
        oColGrid.Width = 150
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "N° Item"
        oColGrid.MappingName = "Item"
        oColGrid.Width = 350
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "TIPO"
        oColGrid.MappingName = "tipo"
        oColGrid.Width = 50
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = False
        'oColGrid.Alignment = HorizontalAlignment.Left
        oColGrid.HeaderText = "GRUPO"
        oColGrid.MappingName = "GRUPO"
        oColGrid.Width = 90
        ' oColGrid.Format = "dd/mm/yyyy"
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.HeaderText = "UMEDIDA"
        oColGrid.MappingName = "UMEDIDA"
        oColGrid.Width = 50
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "PESO"
        oColGrid.MappingName = "PESO"
        oColGrid.Format = "###.####"
        oColGrid.Width = 70
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.TextBox.Enabled = True
        oColGrid.Alignment = HorizontalAlignment.Right
        oColGrid.HeaderText = "EN STOCK"
        oColGrid.MappingName = "EN STOCK"
        oColGrid.Width = 70
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        DataGrid1.TableStyles.Add(oEstiloGrid)
    End Sub
End Class