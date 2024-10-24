Imports System.IO
Imports System.IO.StreamWriter
Imports System.Text
Imports System.Data.SqlClient



Public Class frm_OP_to_SAP
    Dim dts_NR, dts_SD As New DataSet 'Norma de reparto


    Private Sub frm_OP_to_SAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_DATA(1, "", 0, "2000-01-01", "")
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Call Obtener_DATA(2, DataGridView1.Item("CODIGO", DataGridView1.CurrentRow.Index).Value.ToString, DataGridView1.Item("CANT", DataGridView1.CurrentRow.Index).Value.ToString, DataGridView1.Item("Fecha", DataGridView1.CurrentRow.Index).Value.ToString, DataGridView1.Item("Telar", DataGridView1.CurrentRow.Index).Value.ToString)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SAP.Click
        Dim x As Integer
        x = MessageBox.Show("Seguro de procesar esta elección.", "PROFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If x = 6 Then

            'This line of code creates a text file for the data export.
            Dim file = New System.IO.StreamWriter(Application.StartupPath & "\Temp\OP0.txt", False, System.Text.UnicodeEncoding.Unicode)
            Dim file2 = New System.IO.StreamWriter(Application.StartupPath & "\Temp\OP1.txt", False, System.Text.UnicodeEncoding.Unicode)
            Try
                Dim sLine0 As String, sLine As String = ""
                'This for loop loops through each row in the table
                For r As Integer = -2 To DataGridView1.Rows.Count - 1
                    'This for loop loops through each column, and the row number
                    'is passed from the for loop above.
                    If r <= -1 Then
                        '14-07-15 modificado
                        'sLine0 = "AbsoluteEntry	ProductionOrderStatus	ProductionOrderType	ItemNo	PlannedQuantity	ProducQuantity	PostingDate	Duedate"
                        sLine0 = "AbsoluteEntry	ProductionOrderStatus	ProductionOrderType	ItemNo	PlannedQuantity	PostingDate	Duedate	DistributionRule	U_FIB_PProfil	U_FIB_Telar	Warehouse"

                        file.WriteLine(sLine0)
                    Else
                        Dim ItemSelec As Integer
                        Dim dc_unidxpros As String
                        Dim dc_pesoFDU As String = 0

                        ItemSelec = DataGridView1.CurrentRow.Index

                        If r = ItemSelec Then

                            If rb_kilos.Checked = True Then
                                If rb_AddSi.Checked = True Then
                                    dc_unidxpros = (DataGridView1.Rows(r).Cells("peso").Value + DataGridView1.Rows(r).Cells("scrap").Value).ToString()
                                Else
                                    dc_unidxpros = DataGridView1.Rows(r).Cells("peso").Value.ToString()
                                End If
                                dc_pesoFDU = dc_unidxpros
                            Else
                                dc_unidxpros = DataGridView1.Rows(r).Cells("cant").Value.ToString()
                                If rb_AddSi.Checked = True Then
                                    dc_pesoFDU = (DataGridView1.Rows(r).Cells("peso").Value + DataGridView1.Rows(r).Cells("scrap").Value).ToString()
                                Else
                                    dc_pesoFDU = DataGridView1.Rows(r).Cells("peso").Value.ToString()
                                End If
                            End If

                            'sLine = "1" & vbTab & "P" & vbTab & "P" & vbTab & DataGridView1.Rows(r).Cells("codigo").Value.ToString & vbTab & dc_unidxpros & vbTab & dc_unidxpros & vbTab & CDate(DataGridView1.Rows(r).Cells("fecha").Value.ToString).ToString("yyyyMMdd") & vbTab & Date.Today.ToString("yyyyMMdd")
                            sLine = "1" & vbTab & "P" & vbTab & "P" & vbTab & DataGridView1.Rows(r).Cells("codigo").Value.ToString & vbTab & dc_unidxpros & vbTab & CDate(DataGridView1.Rows(r).Cells("fecha").Value.ToString).ToString("yyyyMMdd") & vbTab & Date.Today.ToString("yyyyMMdd") & vbTab & cmb_normr.SelectedValue & vbTab & dc_pesoFDU & vbTab & DataGridView1.Rows(r).Cells("TELAR").Value.ToString & vbTab & DataGridView1.Rows(r).Cells("WHS").Value.ToString

                            '//The exported text is written to the text file, one line at a time.
                            file.WriteLine(sLine)
                            sLine = ""
                        End If

                    End If

                Next


                Dim sLine01 As String, sLine02 As String = ""
                '    'This for loop loops through each row in the table
                For r As Integer = -2 To DataGridView2.Rows.Count - 1

                    If r <= -1 Then
                        '14-07-15 modificado
                        '                        sLine01 = "ParentKey	Linenum	ItemNo	BaseQuantity	PlannedQuantity	ProductionOrderIssueType"
                        sLine01 = "ParentKey	Linenum	ItemNo	BaseQuantity	ProductionOrderIssueType	Warehouse"
                        file2.WriteLine(sLine01)
                    Else
                        '14-07-15 modificado
                        'sLine02 = "1" & vbTab & DataGridView2.Rows(r).Cells("linenum").Value.ToString & vbTab & _
                        '           DataGridView2.Rows(r).Cells("ItemNo").Value.ToString & vbTab & DataGridView2.Rows(r).Cells("BaseQuantity").Value.ToString & vbTab & DataGridView2.Rows(r).Cells("PlannedQuantity").Value.ToString & vbTab & _
                        '           DataGridView2.Rows(r).Cells("ProductionOrderIssueType").Value.ToString
                        sLine02 = "1" & vbTab & DataGridView2.Rows(r).Cells("linenum").Value.ToString & vbTab & _
                                   DataGridView2.Rows(r).Cells("ItemNo").Value.ToString & vbTab & DataGridView2.Rows(r).Cells("BaseQuantity").Value.ToString & vbTab & _
                                   DataGridView2.Rows(r).Cells("ProductionOrderIssueType").Value.ToString & vbTab & DataGridView2.Rows(r).Cells("Warehouse").Value.ToString
                        file2.WriteLine(sLine02)
                        sLine02 = ""

                    End If
                Next

                file2.Close()
                file.Close()
                ' Mensaje de exportación de las plantillas de las OP
                System.Windows.Forms.MessageBox.Show("Export Complete.", "Program Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                file.Close()
                file2.Close()
            End Try

            '' ************************** PROCESO DE MIGRACION MEDIANTE DTW *****************************'
            ' Try
            '/*************/
            Dim Command As New Process 'Creamos la instancia Process
            Command.StartInfo.FileName = "cmd.exe" 'El proceso en si es el CMD
            Command.StartInfo.Arguments = "/c " & Application.StartupPath & "\DTW\DTW.exe -s" & Application.StartupPath & "\Temp\Transfer_OP.xml"


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


            'Para refrescar el datagridview luego de procesar el registro
            'Dim i_turn As Integer
            'If DataGridView1.Item("Turno", DataGridView1.CurrentRow.Index).Value.ToString = "D" Then
            '    i_turn = 1
            'Else
            '    i_turn = 2
            'End If
            GroupBox1.Visible = False
            Call Obtener_DATA(3, DataGridView1.Item("CODIGO", DataGridView1.CurrentRow.Index).Value.ToString, 0, DataGridView1.Item("Fecha", DataGridView1.CurrentRow.Index).Value.ToString, DataGridView1.Item("Telar", DataGridView1.CurrentRow.Index).Value.ToString)

        Else
            GroupBox1.Visible = False
            Exit Sub
        End If

        'Guardamos las salidas en un string
        '        ' RichTextBox1.Text = Output 'Desplegamos la salida en nuestro RichTextBox
        'Catch ex As Exception
        '        'En caso de cualquier error de ejecucion
        'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Sub Obtener_DATA(ByVal opcion As Integer, ByVal Item As String, ByVal Cant As Decimal, ByVal Fecha As Date, ByVal Telar As String)
        
        If OCN.State = ConnectionState.Closed Then OCN.Open()

        Try
            If Not dts_NR.Tables.Contains("tNormR") Then
                Dim obj_NORMR As New SqlDataAdapter("select OcrCode As 'ID', OcrName As 'NAME' from SBO_FIBRAFIL..OOCR order by OcrName", OCN)

                obj_NORMR.Fill(dts_NR, "ID")
                obj_NORMR.Fill(dts_NR, "NAME")

                obj_NORMR.SelectCommand.CommandType = CommandType.Text
                obj_NORMR.Fill(dts_NR, "tNormR")

                cmb_normr.DataSource = dts_NR.Tables("tNormR").DefaultView
                cmb_normr.DisplayMember = "NAME"
                cmb_normr.ValueMember = "ID"
            End If

        Catch ex As Exception
            MsgBox(ex.Message & "Error al cargar normas de reparto.")
        End Try
        
        Try

            If Not dts_SD.Tables.Contains("tSede") Then
                Dim obj_SD As New SqlDataAdapter("select ID_SEDE As 'ID', DSCPSD As 'NAME' from OFIBSEDE order by ID_SEDE", OCN)

                obj_SD.Fill(dts_SD, "ID")
                obj_SD.Fill(dts_SD, "NAME")

                obj_SD.SelectCommand.CommandType = CommandType.Text
                obj_SD.Fill(dts_SD, "tSede")

                cmb_sede.DataSource = dts_SD.Tables("tSede").DefaultView
                cmb_sede.DisplayMember = "NAME"
                cmb_sede.ValueMember = "ID"
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "Error al cargar sedes.")
        End Try

        

        Try

            'DATA
            Dim cmd As New SqlCommand("U_SP_ListOPtoSAP", OCN)
            cmd.CommandType = CommandType.StoredProcedure

            Dim dap As New SqlDataAdapter
            dap.SelectCommand = cmd

            Dim parm0 As New SqlParameter("@OPCION", SqlDbType.Int)
            parm0.Value = opcion
            cmd.Parameters.Add(parm0)


            Dim parm1 As New SqlParameter("@Itemcode", SqlDbType.VarChar)
            parm1.Value = Item
            cmd.Parameters.Add(parm1)

            Dim parm2 As New SqlParameter("@Cant", SqlDbType.Decimal)
            parm2.Value = Cant
            cmd.Parameters.Add(parm2)

            Dim parm3 As New SqlParameter("@Fecha", SqlDbType.SmallDateTime)
            parm3.Value = Fecha
            cmd.Parameters.Add(parm3)

            Dim parm4 As New SqlParameter("@TELAR", SqlDbType.VarChar)
            parm4.Value = Telar
            cmd.Parameters.Add(parm4)

            Dim parm5 As New SqlParameter("@SEDE", SqlDbType.VarChar)
            parm5.Value = cmb_sede.SelectedValue.ToString
            cmd.Parameters.Add(parm5)

            Dim dts As New DataSet


            If opcion <> 2 Then
                Dim Dvlista As DataView
                dap.Fill(dts, "vLIST")

                Dvlista = dts.Tables("vLIST").DefaultView
                DataGridView1.DataSource = Dvlista
                Dvlista.AllowEdit = False
                Dvlista.AllowNew = False
            Else
                Dim Dvlista1 As DataView
                dap.Fill(dts, "vLIST")

                Dvlista1 = dts.Tables("vLIST").DefaultView
                DataGridView2.DataSource = Dvlista1
                Dvlista1.AllowEdit = False
                Dvlista1.AllowNew = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            OCN.Close()
        End Try
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim X As Integer
        Dim PP As Integer
        Dim peso As Decimal
        For X = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(X).Cells("WareHouse").Value.ToString = "PP" Then
                PP = PP + 1
            End If
        Next
        If PP > 1 Then

        Else
            peso = (DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells("Peso").Value.ToString()) - (DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells("Cant").Value.ToString() * 0.75)
        End If
        MsgBox(peso, MsgBoxStyle.Information, "fib")
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        lbl_prod.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells("descripcion").Value.ToString
        lbl_peso.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells("peso").Value.ToString
        lbl_cant.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells("cant").Value.ToString

        rb_kilos.Checked = False
        rb_unid.Checked = False
        btn_SAP.Enabled = False

        GroupBox1.Visible = True

    End Sub

    Private Sub rb_unid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_unid.CheckedChanged, rb_kilos.CheckedChanged
        btn_SAP.Enabled = True
    End Sub

    Private Sub frm_OP_to_SAP_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim c As Control = CType(GroupBox1, Control)
        'le  establece el top y el Left dentro del Parent  
        With c
            .Top = (.Parent.ClientSize.Height - c.Height) \ 2
            .Left = (.Parent.ClientSize.Width - c.Width) \ 2
        End With
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing)
        Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing
        worksheet = workbook.Sheets("Hoja1")
        worksheet = workbook.ActiveSheet
        'Aca se agregan las cabeceras de nuestro datagrid.

        Dim s_noshow() As String
        ReDim s_noshow(1)
        s_noshow(0) = "Act. Estado"
        s_noshow(1) = "Estado"


        For i As Integer = 1 To Me.DataGridView1.Columns.Count
            worksheet.Cells(1, i) = Me.DataGridView1.Columns(i - 1).HeaderText
        Next

        'Aca se ingresa el detalle recorrera la tabla celda por celda

        For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
            For j As Integer = 0 To Me.DataGridView1.Columns.Count - 1
                worksheet.Cells(i + 2, j + 1) = Me.DataGridView1.Rows(i).Cells(j).Value.ToString()
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

    Private Sub cmb_sede_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_sede.SelectedIndexChanged
        Try
            Call Obtener_DATA(1, "", 0, "2000-01-01", "")
        Catch ex As Exception

        End Try

    End Sub
End Class
