Imports System.Drawing.Color
Imports System.Data.SqlClient

Public Class frm_planxTurno

    Dim dts As New DataSet
    Dim i_cierre As Integer
    Dim Dragging As Boolean
    Private Dvlista As DataView
    Dim cursorX, CursorY As Integer

    Private bsource As BindingSource = New BindingSource()

    Private Sub frm_planxTurno_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Obtener_Data()
    End Sub

    Sub Obtener_Data()
        Try
            If DataGridView1.RowCount >= 1 Then
                For i As Integer = 0 To DataGridView1.RowCount - 2
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
        Dim CMD As New SqlCommand("U_SP_SEL_PLANDIA", OCN)
        CMD.CommandType = CommandType.StoredProcedure

        Try
            Dim dap As New SqlDataAdapter(CMD)
            Dim dtPlan, dtProd, dtDespacho As New DataTable

            dap.Fill(dtPlan)
            bsource.DataSource = dtPlan 'DataGridView1.DataSource = dvDespacho

            DataGridView1.DataSource = bsource

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

   Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        Try
            If DataGridView1.RowCount >= 1 Then
                For i As Integer = 0 To DataGridView1.RowCount - 2
                    DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
                Next
            End If
        Catch ex As InvalidOperationException ' Esta excepcion es por si ocurriera
            MsgBox("Esta fila no se puede eliminar", MsgBoxStyle.Critical, "Operación inválida : : : . . .")
        End Try

        Combos()
        btnConfirma.Text = "&Crear"
        Me.ContextMenuStrip = Nothing
    End Sub

    Sub Combos()
        Dim cboMac, cboOper, cboAyud As New DataGridViewComboBoxColumn()
        'Dim btnProd As New DataGridViewButtonCell

        'cboArea.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        'cmb_area.DataPropertyName = "Combobox"
        'cboArea.Name = "cmb_area"
        'cboArea.HeaderText = "Area"
        'Me.DataGridView1.Columns.Add(cboArea)

        cboMac.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        cboMac.DataPropertyName = "Combobox"
        cboMac.Name = "cmb_maquina"
        cboMac.HeaderText = "Maquina"
        Me.DataGridView1.Columns.Add(cboMac)

        cboOper.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        cboOper.DataPropertyName = "Combobox"
        cboOper.Name = "cmb_operario"
        cboOper.HeaderText = "Operario"
        Me.DataGridView1.Columns.Add(cboOper)

        cboAyud.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        cboAyud.DataPropertyName = "Combobox"
        cboAyud.Name = "cmb_ayudante"
        cboAyud.HeaderText = "Ayudante"
        Me.DataGridView1.Columns.Add(cboAyud)

        Dim dts_Area, dts_Mac, dts_Ope, dts_Ayud As New DataSet()
        Dim obj_area As New SqlDataAdapter("SELECT [Code]As 'ID',[Descripcion] As 'NAME'FROM [OFIBAREA]T0", OCN)
        Dim obj_mac As New SqlDataAdapter("select Code As 'ID', Descripcion As 'NAME' from [OFIBMAC] T0  where STDMAC='A'", OCN)
        Dim obj_oper As New SqlDataAdapter("Select T0.Code As 'ID', T0.Name As 'NAME' from OFIBEMPL T0 inner join OFIBAREA T1 on T0.U_FIB_AREA = T1.CODE where T0.u_FIB_CARGO  = 'O' and T1.PP='Y' AND ISNULL(T0.INACTIVO,'N')<>'Y'", OCN)
        Dim obj_ayud As New SqlDataAdapter("Select T0.Code As 'ID', T0.Name As 'NAME' from OFIBEMPL T0 inner join OFIBAREA T1 on T0.U_FIB_AREA = T1.CODE where T0.u_FIB_CARGO  = 'A' and T1.PP='Y' AND ISNULL(T0.INACTIVO,'N')<>'Y'", OCN)

        'De la misma forma pasamos la consulta al(DataSet)
        obj_area.Fill(dts_Area, "ID")
        obj_area.Fill(dts_Area, "NAME")

        obj_mac.Fill(dts_Mac, "ID")
        obj_mac.Fill(dts_Mac, "NAME")

        obj_oper.Fill(dts_Ope, "ID")
        obj_oper.Fill(dts_Ope, "NAME")

        obj_ayud.Fill(dts_Ayud, "ID")
        obj_ayud.Fill(dts_Ayud, "NAME")

        'Ahora esta consulta la pasamos a un DataGridViewComboBoxColumn()

        obj_area.SelectCommand.CommandType = CommandType.Text
        obj_area.Fill(dts_Area, "tArea")

        cmb_area.DataSource = dts_Area.Tables("tArea").DefaultView
        cmb_area.DisplayMember = "NAME"
        cmb_area.ValueMember = "ID"
        'cboArea.DataSource = dts_Area.Tables(0).DefaultView
        'cboArea.DisplayMember = "NAME"
        'cboArea.ValueMember = "ID"

        cboMac.DataSource = dts_Mac.Tables(0).DefaultView
        cboMac.DisplayMember = "NAME"
        cboMac.ValueMember = "ID"

        cboOper.DataSource = dts_Ope.Tables(0).DefaultView
        cboOper.DisplayMember = "NAME"
        cboOper.ValueMember = "ID"

        cboAyud.DataSource = dts_Ayud.Tables(0).DefaultView
        cboAyud.DisplayMember = "NAME"
        cboAyud.ValueMember = "ID"

        DataGridView1.Columns("area").Visible = False
        DataGridView1.Columns("Maquina").Visible = False
        DataGridView1.Columns("operario").Visible = False
        DataGridView1.Columns("ayudante").Visible = False
    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        If btnConfirma.Text = "&Crear" Then
            Dim imsg As Integer
            imsg = MessageBox.Show("Se reiniciaran los valores", "FIBRAFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If imsg = 6 Then
                'Try
                If OCN.State = ConnectionState.Closed Then OCN.Open()

                Dim cmd As New SqlCommand()
                cmd.Connection = OCN
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "Delete from OFIBPLANDIA where id_Area = '" + cmb_area.SelectedValue + "'"
                cmd.ExecuteNonQuery()

                Dim c_err As Integer = 0
                For k As Integer = 0 To DataGridView1.RowCount - 2
                    '30-01-15                    If DataGridView1.Item("cmb_area", k).Value = "" Then c_err = c_err + 1
                    If DataGridView1.Item("cmb_maquina", k).Value = "" Then c_err = c_err + 1
                    If DataGridView1.Item("producto", k).Value = "" Then c_err = c_err + 1
                    If DataGridView1.Item("cmb_operario", k).Value = "" Then c_err = c_err + 1
                    If DataGridView1.Item("cmb_ayudante", k).Value = "" Then c_err = c_err + 1
                Next
                If c_err = 0 Then

                    Dim cmd1 As New SqlCommand()
                    ''''''If OCN.State = ConnectionState.Closed Then OCN.Open()

                    With cmd1.Parameters
                        cmd1.Parameters.Clear()
                        cmd1.Connection = OCN
                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.CommandText = "U_SP_INS_PLANDIA"

                        .Add(New SqlParameter("@ID_AREA", SqlDbType.Text))
                        .Add(New SqlParameter("@ID_MAC", SqlDbType.Text))
                        .Add(New SqlParameter("@ID_ITEM", SqlDbType.Text))
                        .Add(New SqlParameter("@ID_OPER", SqlDbType.Text))
                        .Add(New SqlParameter("@ID_AYUD", SqlDbType.Text))
                        .Add(New SqlParameter("@FCH_PLAN", SqlDbType.Date))
                        .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                    End With

                    With cmd1
                        For k As Integer = 0 To DataGridView1.RowCount - 2
                            .Parameters("@ID_AREA").Value = cmb_area.SelectedValue 'DataGridView1.Item("cmb_area", k).Value
                            .Parameters("@ID_MAC").Value = DataGridView1.Item("cmb_maquina", k).Value
                            .Parameters("@ID_ITEM").Value = DataGridView1.Item("producto", k).Value
                            .Parameters("@ID_OPER").Value = DataGridView1.Item("cmb_operario", k).Value
                            .Parameters("@ID_AYUD").Value = DataGridView1.Item("cmb_ayudante", k).Value
                            .Parameters("@FCH_PLAN").Value = Date.Now
                            .ExecuteNonQuery()
                        Next k
                    End With
                    If Trim(cmd1.Parameters("@msg").Value.ToString()) = "" Then
                        MsgBox("La planificación del presente turno ha sido  registrado satisfactoriamente.", MsgBoxStyle.Information, "Fibrafil")
                        Call Obtener_Data()
                    Else
                        MsgBox(cmd1.Parameters("@msg").Value.ToString(), MsgBoxStyle.Information, "Fibrafil")
                        Exit Sub
                    End If
                Else
                    MsgBox("CAMPOS INVALIDOS", MsgBoxStyle.Information, "FIBRAFIL")
                    Exit Sub
                End If
                'Catch ex As Exception
                'MsgBox(ex.Message)
                'Exit Sub
                ' End Try
            End If
        End If
        i_cierre = 1
        Me.Close()
    End Sub

    Private Sub Label1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseDown
        Dragging = True
        ' Note positions of cursor when pressed
        cursorX = e.X
        CursorY = e.Y
    End Sub

    Private Sub Label1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseMove
        If Dragging Then
            Dim ctrl As Control = CType(GroupBox1, Control)
            ' Move the control according to mouse movement
            ctrl.Left = (ctrl.Left + e.X) - cursorX
            ctrl.Top = (ctrl.Top + e.Y) - CursorY
            ' Ensure moved control stays on top of anything it is dragged on to
            ctrl.BringToFront()
        End If
    End Sub

    Private Sub Label1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseUp
        Dragging = False
    End Sub

    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        If DataGridView1.CurrentCell.ColumnIndex = 4 Then
            GroupBox1.Visible = True
            ObtenerData()
            LlenarData()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim X As Integer
        X = DataGrid1.CurrentRowIndex
        
        Me.GroupBox1.Visible = False

        DataGridView1.CurrentCell.Value = Dvlista.Item(X)("code").ToString()

    End Sub

    Sub ObtenerData()
        Try
            If OCN.State = ConnectionState.Closed Then OCN.Open()
            dts.Clear()
            'DATA
            Dim cmd As New SqlCommand("u_sp_query", OCN)
            cmd.CommandType = CommandType.StoredProcedure

            Dim dap As New SqlDataAdapter
            dap.SelectCommand = cmd

            Dim parm As New SqlParameter("@vTAB", SqlDbType.Char)
            parm.Value = 2  ' N° 0 (cero) indica articulos
            cmd.Parameters.Add(parm)
            dap.Fill(dts, "vQUERY")

            Dvlista = dts.Tables("vQUERY").DefaultView

            Dvlista.AllowEdit = False
            Dvlista.AllowNew = False

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            OCN.Close()
        End Try
    End Sub

    Sub LlenarData()
        DataGrid1.SetDataBinding(dts.Tables("vITM"), "")
        DataGrid1.DataSource = Nothing
        DataGrid1.DataSource = Dvlista
        DataGrid1.SetDataBinding(Dvlista, "") ' con manejo de dataview
        Arma_Grid()
    End Sub

    Sub Arma_Grid()
        DataGrid1.TableStyles.Clear()
        Me.DataGrid1.CaptionBackColor = Color.Navy
        Me.DataGrid1.CaptionForeColor = Color.Yellow
        Dim oEstiloGrid As New DataGridTableStyle
        Me.DataGrid1.CaptionText = "Listado"

        oEstiloGrid.MappingName = "vQUERY"
        oEstiloGrid.BackColor = Color.LightGoldenrodYellow
        oEstiloGrid.AlternatingBackColor = Color.Aquamarine

        Dim oColGrid As DataGridTextBoxColumn
        oColGrid = New DataGridTextBoxColumn
        oColGrid.HeaderText = "N° ID"
        oColGrid.MappingName = "id"
        oColGrid.Width = 50
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.HeaderText = "Codigo"
        oColGrid.MappingName = "Code"
        oColGrid.Width = 100
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        oColGrid = New DataGridTextBoxColumn
        oColGrid.HeaderText = "Descripcion"
        oColGrid.MappingName = "name"
        oColGrid.Width = 250
        oEstiloGrid.GridColumnStyles.Add(oColGrid)
        oColGrid = Nothing

        If cVars.NQP <> 6 Then

            oColGrid = New DataGridTextBoxColumn
            oColGrid.HeaderText = "U. Med"
            oColGrid.MappingName = "UM"
            oColGrid.Width = 50
            oEstiloGrid.GridColumnStyles.Add(oColGrid)
            oColGrid = Nothing

            oColGrid = New DataGridTextBoxColumn
            oColGrid.HeaderText = "ALMACEN"
            oColGrid.MappingName = "ALMACEN"
            oColGrid.Width = 50
            oEstiloGrid.GridColumnStyles.Add(oColGrid)
            oColGrid = Nothing
        End If
        DataGrid1.TableStyles.Add(oEstiloGrid)
    End Sub

    Private Sub txtId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtId.TextChanged
        Dvlista.RowFilter = "code like '%" & txtId.Text & "%'"
    End Sub

    Private Sub txtDesc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDesc.TextChanged
        Dvlista.RowFilter = "name like '%" & txtDesc.Text & "%'"
    End Sub

End Class