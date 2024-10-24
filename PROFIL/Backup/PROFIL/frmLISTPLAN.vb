Imports System
Imports System.IO
Imports System.Data.SqlClient
Public Class frmLISTPLAN

    Private Dvlista As DataView
    Dim WithEvents posi As CurrencyManager
    Dim dts As New DataSet
    Dim cmd As New SqlCommand()
    Event PasaVars()

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmLISTPLAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = 30
        Me.Left = MDIPROFIL.Width - (Me.Width + 50)
        Call Obtener_Data()
        Label2.Text = "Total :" & Format(Sumar("CANTIDAD", DataGridView1), "#").ToString
    End Sub

    Sub Obtener_Data()

        If OCN.State = ConnectionState.Closed Then
            OCN.Open()
        End If
        'DATA
        Dim CMD As New SqlCommand("U_SP_LISTPPLANIF", OCN)
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add(New SqlParameter("@IDAREA", SqlDbType.Text)).Value = Form1.vs_idArea ' Area  
        CMD.Parameters.Add(New SqlParameter("@IDMAC", SqlDbType.Text)).Value = Form1.vs_idMac  ' Maquina
        Try
            Dim dap As New SqlDataAdapter(CMD)
            dap.Fill(dts, "vLPLAN")

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

            Dvlista = dts.Tables("vLPLAN").DefaultView
            DataGridView1.DataSource = Dvlista

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        'Devuelve valores para la produccion en formulario orden de produccion
        For k As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item(0, k).Value = 1 Then
                'Dim frmop As New frmOrdprod
                'frmOrdprod.X_op = CInt(DataGridView1.Item(1, k).Value.ToString)
                'frmOrdprod.scode = DataGridView1.Item(2, k).Value.ToString
                'frmOrdprod.sname = DataGridView1.Item(3, k).Value.ToString
                'frmOrdprod.X_qplanif = CDec(DataGridView1.Item(9, k).Value.ToString)
                'frmOrdprod.sumed = DataGridView1.Item(10, k).Value.ToString
                'frmOrdprod.d_pendiente = CDec(DataGridView1.Item(11, k).Value.ToString)
                'frmOrdprod.scodebar = DataGridView1.Item(13, k).Value.ToString
                'RaiseEvent PasaVars()
                'frmop.Show()
            End If
        Next
    End Sub

    Private Function Sumar(ByVal nombre_Columna As String, ByVal Dgv As DataGridView) As Double
        Dim total As Double = 0
        ' recorrer las filas y obtener los items de la columna indicada en "nombre_Columna"   
        Try
            For i As Integer = 0 To Dgv.RowCount - 1
                total = total + CDbl(Dgv.Item(nombre_Columna.ToLower, i).Value)
            Next

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        ' retornar el valor   
        Return total

    End Function

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Selecciona y deselecciona el optionbox del grid
        For k As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item(0, k).Value = 1 Then
                DataGridView1.Item(0, k).Value = 0
            End If
        Next
    End Sub

End Class
