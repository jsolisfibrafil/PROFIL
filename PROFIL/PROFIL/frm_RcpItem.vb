Imports PROFIL.MForm
Imports System.Data.SqlClient

Public Class frm_RcpItem

    Dim dts As New DataSet
    Private DvDetalle, Dvlista, DvRsmnIN As DataView

    Dim Dragging As Boolean
    Dim cursorX, CursorY As Integer

    Dim X1 = 0, x2 = 0
    Dim CMD, CMD3 As New SqlCommand
    Private bsource As BindingSource = New BindingSource()

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        Me.ContextMenuStrip = Nothing
        Obtener_Data()
    End Sub

    Sub Obtener_Data()

        Dim da_docnum As New SqlClient.SqlDataAdapter("select Isnull(Max(Docnum)+1,1) As DOCNUM from ORPCFIB where year(Postdate)= " & Year(Date.Now), OCN)
        da_docnum.SelectCommand.CommandType = CommandType.Text
        Dim ds As New DataSet()
        da_docnum.Fill(ds, "vDocnum")
        txtDocnum.DataBindings.Clear()
        txtDocnum.DataBindings.Add("text", ds.Tables("vDocnum"), "DOCNUM")

        Try
            If DataGridView1.RowCount >= 1 Then
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
                Next
            End If
        Catch ex As InvalidOperationException ' Esta excepcion es por si ocurriera
            MsgBox("Esta fila no se puede eliminar", MsgBoxStyle.Critical, "Operación inválida : : : . . .")
        End Try

        DataGridView1.DataSource = Nothing

        DataGridView1.AutoResizeColumns()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        If OCN.State = ConnectionState.Closed Then OCN.Open()


        'DATA
        Dim cmd As New SqlCommand("U_SP_SELOP", OCN)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@sede", SqlDbType.Text)).Value = Form1.vs_sede ' codigo de sede


        Dim dap As New SqlDataAdapter(cmd)
        dap.Fill(dts, "dtinItem")

        DvDetalle = dts.Tables("dtinItem").DefaultView
        DvDetalle.AllowEdit = False
        DvDetalle.AllowNew = False

        Dim colCheckbox As New DataGridViewCheckBoxColumn()
        colCheckbox.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        colCheckbox.ThreeState = False
        colCheckbox.TrueValue = 1
        colCheckbox.FalseValue = 0

        colCheckbox.DataPropertyName = "Checkbox"
        colCheckbox.HeaderText = "PROCESAR"
        colCheckbox.Name = "Checkbox"
        colCheckbox.ReadOnly = False
        If Me.DataGridView1.Columns.Count <= 0 Then
            DataGridView1.Columns.Add(colCheckbox)
        End If

        DataGridView1.DataSource = DvDetalle
        DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)


    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        For k As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item(0, k).Value = 1 Then
                DataGridView1.Item(0, k).Value = 0
            End If
        Next

        txtitem.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value.ToString()
        txtdescitm.Text = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value.ToString()
        txtCant.Text = DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value.ToString()
        txtCant.Focus()
    End Sub

    Private Sub txtCant_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCant.KeyPress

        If GroupBox1.Visible = False Then GroupBox1.Visible = True

        ArmagridIngresos()
        If GroupBox1.Visible = False Then GroupBox1.Visible = True
        If e.KeyChar = ChrW(Keys.Return) Then
            '            DTGcmpt.SetDataBinding(DvDetalle, "")

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

            'TabControl2.SelectedTab = TabPage4
        End If
    End Sub

    Private Sub frm_RcpItem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = CierraForms("un", "Ingreso de mercadería", btnConfirma.Text)
    End Sub

    Private Sub frm_RcpItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' SELECCIONS LA LISTA DE LOS CODIGOS DE BARRAS DE LOS ITEM RECEPCIONADOS
        Dim dap3 As New SqlDataAdapter("U_SP_SELRP2DETRSM", OCN)
        dap3.SelectCommand.CommandType = CommandType.StoredProcedure
        dap3.Fill(dts, "vLIST3")

        DvRsmnIN = dts.Tables("vLIST3").DefaultView
    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click

        Dim X As Integer
        ' Recorre los items ( compara empezando desde el primero , de abajo hacia arriba)  
        For X = 0 To DvRsmnIN.Count - 1
            If dtgRSMN.Item(X, 4).ToString = "" Then
                MsgBox("Registros en blanco, no se generó la operación", 48, "FIBRAFIL")
                Exit Sub
            End If
        Next


        'Select Case btnConfirma.Text
        '    Case "&Ok"
        '        Me.Close()
        '    Case "&Crear"



        btnConfirma.Enabled = False
        DatosInsUpd("U_SP_FIB_INS_RPCAB")
        If OCN.State = ConnectionState.Closed Then OCN.Open()
        'Try
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

            MsgBox("Recibo de productos  creada satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
            btnConfirma.Text = "&Ok"
            Me.ContextMenuStrip = Me.ContextMenuStrip1
            GroupBox1.Visible = False
        Else
            MsgBox(CMD3.Parameters("@msg").Value.ToString(), MsgBoxStyle.Information, "Fibrafil")
        End If


        'Catch ex As Exception
        ' MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ' Finally
        OCN.Close()
        btnConfirma.Enabled = True
        Obtener_Data()
        'End Try

    End Sub

    Sub DatosInsUpd(ByVal NameProced As String)
        Try
            CMD.Parameters.Clear()
            CMD.Connection = OCN
            CMD.CommandType = CommandType.StoredProcedure
            CMD.CommandText = NameProced

            CMD.Parameters.Add(New SqlParameter("@Norigen", SqlDbType.BigInt)).Value = 0 ' Numero de orden de produccion
            CMD.Parameters.Add(New SqlParameter("@Docnum", SqlDbType.BigInt)).Value = txtDocnum.Text ' Numero de documento de recepcion
            CMD.Parameters.Add(New SqlParameter("@PostDate", SqlDbType.SmallDateTime)).Value = Date.Now  'fecha de recepcion
            CMD.Parameters.Add(New SqlParameter("@CodEmp", SqlDbType.VarChar)).Value = "" 'cmbEmpleado.SelectedValue 'responsable de la recepcion
            CMD.Parameters.Add(New SqlParameter("@Itemcode", SqlDbType.VarChar)).Value = txtitem.Text 'iTEM
            CMD.Parameters.Add(New SqlParameter("@Quantity", SqlDbType.VarChar)).Value = txtCant.Text 'cANTIDAD RECEPCIONADA
            CMD.Parameters.Add(New SqlParameter("@WhsCode", SqlDbType.Text)).Value = Form1.vs_sede  ' Almacen
            CMD.Parameters.Add(New SqlParameter("@MtvOper", SqlDbType.Text)).Value = "04" ' Motivo de recepcion 
            CMD.Parameters.Add(New SqlParameter("@Comments", SqlDbType.Text)).Value = txtcomment.Text ' Comentarios
            CMD.Parameters.Add(New SqlParameter("@Createfor", SqlDbType.VarChar)).Value = "" 'Form1.vs_idUser  'Usuario que registra operacion
            CMD.Parameters.Add(New SqlParameter("@FK", SqlDbType.BigInt)).Direction = ParameterDirection.Output
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

   
    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Close()
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

  
End Class
