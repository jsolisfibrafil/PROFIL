Imports System.Data.SqlClient

Public Class frmPedSAP

    Dim dts As New DataSet
    Dim i_keySAP, i_numSAP, i_baseline As Integer
    Dim s_codeBP, s_nameBP, s_tdoc As String
    Private bsource As BindingSource = New BindingSource()
    Private dsource As BindingSource = New BindingSource()
    Private dtCPedidos, dtDPedidos As New DataTable
    Private dvPEDSAP As DataView
    Event PasaVars()

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmPedSAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
    End Sub

    Sub Obtener_Data()

        Dim CMD As New SqlCommand("U_SP_FIB_LISPEDSAP", OCN)
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add(New SqlParameter("@ID", SqlDbType.Int)).Value = -1  ' Numero informe  
        CMD.Parameters.Add(New SqlParameter("@PD", SqlDbType.VarChar)).Value = "CAB"  ' Parte de documento CAB o DET
        CMD.Parameters.Add(New SqlParameter("@TDOC", SqlDbType.VarChar)).Value = "" ' No Aplica

        ' Try
        Dim dap As New SqlDataAdapter(CMD)
        'dap.Fill(dtCPedidos)
        dap.Fill(dts, "vQUERY")
        ' bsource.DataSource = dtCPedidos

        dvPEDSAP = dts.Tables("vQUERY").DefaultView

        Dim colCheckbox As New DataGridViewCheckBoxColumn()
        ' Size the column width so it is wide enough to display the header
        colCheckbox.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        colCheckbox.ThreeState = False
        colCheckbox.TrueValue = 1
        colCheckbox.FalseValue = 0
        'colCheckbox.IndeterminateValue = System.DBNull.Value
        colCheckbox.DataPropertyName = "Checkbox"
        colCheckbox.HeaderText = "PROCESAR"
        colCheckbox.Name = "Checkbox"
        colCheckbox.ReadOnly = False
        DataGridView1.Columns.Add(colCheckbox)

        '  DataGridView1.DataSource = bsource
        DataGridView1.DataSource = dvPEDSAP

        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try


    End Sub

    Private Sub DataGridView1_CellContentClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Selecciona y deselecciona el optionbox del grid
        For k As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item(0, k).Value = 1 Then
                DataGridView1.Item(0, k).Value = 0
            End If
        Next
        i_keySAP = CInt(DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value.ToString)
        i_numSAP = CInt(DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value.ToString)
        s_tdoc = DataGridView1.Item("T-Doc", DataGridView1.CurrentRow.Index).Value.ToString
        s_codeBP = DataGridView1.Item("CODIGO", DataGridView1.CurrentRow.Index).Value.ToString
        s_nameBP = DataGridView1.Item("CLIENTE", DataGridView1.CurrentRow.Index).Value.ToString

        Label2.Text = "Se procesará el documento Nº: " + i_numSAP.ToString + " con identificador Nº: " + i_keySAP.ToString + "."

        Try
            If DataGridView2.RowCount >= 1 Then
                For i As Integer = 0 To DataGridView2.RowCount - 2
                    DataGridView2.Rows.Remove(DataGridView2.CurrentRow)
                Next
            End If
        Catch ex As InvalidOperationException ' Esta excepcion es por si ocurriera
            MsgBox("Esta fila no se puede eliminar", MsgBoxStyle.Critical, "Operación inválida : : : . . .")
        End Try

        DataGridView2.DataSource = Nothing

        DataGridView2.AutoResizeColumns()
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells


        Dim CMD As New SqlCommand("U_SP_FIB_LISPEDSAP", OCN)
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add(New SqlParameter("@ID", SqlDbType.Int)).Value = i_keySAP  ' llave del informe  SAP
        CMD.Parameters.Add(New SqlParameter("@PD", SqlDbType.VarChar)).Value = "DET"  ' Parte de documento CAB o DET
        CMD.Parameters.Add(New SqlParameter("@TDOC", SqlDbType.VarChar)).Value = s_tdoc ' Indica el tipo de documento
        Try
            Dim dap As New SqlDataAdapter(CMD)
            dap.Fill(dtDPedidos)
            dsource.DataSource = dtDPedidos
            DataGridView2.DataSource = dsource
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
   
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Call Aceptar()
    End Sub

    Sub Aceptar()
        Try
            frmEntregas.i_keySAP = i_keySAP
            frmEntregas.i_noSAP = i_numSAP

            frmEntregas.s_codeBP = s_codeBP
            frmEntregas.s_nameBP = s_nameBP
            frmEntregas.s_TDOC = s_tdoc
            frmEntregas.i_baseline = i_baseline

            RaiseEvent PasaVars()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txt_nov_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_nov.TextChanged
        dvPEDSAP.RowFilter = "documento like '%" & txt_nov.Text & "%'"
    End Sub
End Class