Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Imports Microsoft.Reporting.WinForms

Public Class frmInformes
    Dim dts As New DataSet
    Dim i_ninforme As Integer
    Private bsource As BindingSource = New BindingSource()

    Dim dap As New SqlDataAdapter
    Dim dtPlan, dtProd, dtDespacho, dtKardex, dtProdDiario, dtCodebar, dtProdMes, dtDetProd As New DataView

    Private Sub btnprocess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprocess.Click
        Obtener_Data()
    End Sub

    Sub Obtener_Data()
        'el codigo comentedo relentiza en demasía la 
        'Try
        'If DataGridView1.RowCount >= 1 Then
        '    For i As Integer = 0 To DataGridView1.RowCount - 2
        '        DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
        '    Next
        'End If
        'Catch ex As InvalidOperationException ' Esta excepcion es por si ocurriera
        '    MsgBox("Esta fila no se puede eliminar", MsgBoxStyle.Critical, "Operación inválida : : : . . .")
        'End Try
        dts.Clear()

        DataGridView1.DataSource = Nothing
        DataGridView1.AutoResizeColumns()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells


        If OCN.State = ConnectionState.Closed Then OCN.Open()

        'DATA
        Dim CMD As New SqlCommand("U_SP_INF_INTEGRADO", OCN)
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add(New SqlParameter("@Ninforme", SqlDbType.Int)).Value = i_ninforme  ' Numero informe  
        CMD.Parameters.Add(New SqlParameter("@Item1", SqlDbType.Text)).Value = txtItm1.Text  ' Item 01  
        CMD.Parameters.Add(New SqlParameter("@Maq", SqlDbType.Text)).Value = txt_s_Maq.Text  ' Máquina
        CMD.Parameters.Add(New SqlParameter("@Fini", SqlDbType.SmallDateTime)).Value = dtpFini.Value ' Fecha 01  
        CMD.Parameters.Add(New SqlParameter("@Ffin", SqlDbType.SmallDateTime)).Value = dtpFfin.Value ' Fecha 02
        ' code add 13/3/18
        CMD.Parameters.Add(New SqlParameter("@Sede", SqlDbType.Text)).Value = cmb_sede.SelectedValue ' Sede

        dap.SelectCommand = CMD

        Try

            Select Case i_ninforme
                Case 1
                    dap.Fill(dts, "dtPlan")
                    dtPlan = dts.Tables("dtPlan").DefaultView
                    DataGridView1.DataSource = dtPlan
                    Exit Sub
                Case 2
                    dap.Fill(dts, "dtProd")
                    dtProd = dts.Tables("dtProd").DefaultView
                    DataGridView1.DataSource = dtProd
                    Exit Sub

                Case 3
                    dap.Fill(dts, "dtDespacho")
                    dtDespacho = dts.Tables("dtDespacho").DefaultView
                    DataGridView1.DataSource = dtDespacho
                    Exit Sub

                Case 4
                    dap.Fill(dts, "dtKardex")
                    dtKardex = dts.Tables("dtKardex").DefaultView
                    DataGridView1.DataSource = dtKardex
                    Exit Sub
                   
                Case 5
                    dap.Fill(dts, "dtProdDiario")
                    dtProdDiario = dts.Tables("dtProdDiario").DefaultView
                    DataGridView1.DataSource = dtProdDiario
                    Exit Sub

                  
                Case 6
                    dap.Fill(dts, "dtCodebar")
                    dtCodebar = dts.Tables("dtCodebar").DefaultView
                    DataGridView1.DataSource = dtCodebar
                    Exit Sub
                  
                Case 7

                    dap.Fill(dts, "dtProdMes")
                    dtProdMes = dts.Tables("dtProdMes").DefaultView
                    DataGridView1.DataSource = dtProdMes
                    Exit Sub 

                Case 8
                    dap.Fill(dts, "dtDetProd")
                    dtDetProd = dts.Tables("dtDetProd").DefaultView
                    DataGridView1.DataSource = dtDetProd
                    Exit Sub
                       
            End Select



        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            OCN.Close()
        End Try
    End Sub

    Private Sub op_INFORMES(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optKardex.CheckedChanged, optPlan.CheckedChanged, optProd.CheckedChanged, optDespacho.CheckedChanged, optDiario.CheckedChanged, optCodebar.CheckedChanged, optProdMes.CheckedChanged, optdetprod.CheckedChanged
        If optPlan.Checked = True Then i_ninforme = 1
        If optProd.Checked = True Then i_ninforme = 2
        If optDespacho.Checked = True Then i_ninforme = 3
        If optKardex.Checked = True Then i_ninforme = 4
        If optDiario.Checked = True Then i_ninforme = 5
        If optCodebar.Checked = True Then i_ninforme = 6
        If optProdMes.Checked = True Then i_ninforme = 7
        If optdetprod.Checked = True Then i_ninforme = 8

    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Archivo de Excel|*.xls|Archivo de Texto|*.txt|Archivo PDF|*.pdf"
        saveFileDialog1.Title = "Save a File"
        saveFileDialog1.ShowDialog()
        Try
            If saveFileDialog1.FileName <> "" Then
                Select Case saveFileDialog1.FilterIndex
                    Case 1
                        'Me.Button2.Image.Save(fs, Microsoft.Office.Interop.Excel.)
                        Dim xlApp As Excel.Application
                        Dim xlWorkBook As Excel.Workbook
                        Dim xlWorkSheet As Excel.Worksheet
                        Dim misValue As Object = System.Reflection.Missing.Value

                        Dim i As Int16, j As Int16

                        xlApp = New Excel.ApplicationClass
                        xlWorkBook = xlApp.Workbooks.Add(misValue)
                        xlWorkSheet = xlWorkBook.Sheets(1)

                        For i = 0 To DataGridView1.RowCount - 2
                            For j = 0 To DataGridView1.ColumnCount - 1
                                xlWorkSheet.Cells(i + 1, j + 1) = DataGridView1(j, i).Value.ToString()
                            Next
                        Next

                        xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, _
                         Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                        xlWorkBook.Close(True, misValue, misValue)
                        xlApp.Quit()

                        releaseObject(xlWorkSheet)
                        releaseObject(xlWorkBook)
                        releaseObject(xlApp)

                    Case 2

                    Case 3

                End Select

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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

    
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        'Me.ReportViewer1.LocalReport.ReportPath = Application.StartupPath & "\Reports" & "\Report1.rdlc"
        'Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1_U_VW_RQMNT"))

        'ReportViewer1.LocalReport.Refresh()
        'Me.ReportViewer1.RefreshReport()
    End Sub

    
    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            frmReport.NFORM = "INF_PESOS"
            frmReport.KEYGUIA = -99
            Dim frm_report As New frmReport
            frm_report.Show()
            ' End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    
    Private Sub frmInformes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ds As New DataSet()
        'COMBO SEDE
        Dim DAP_MAc As New SqlDataAdapter("select id_sede as 'code' , dscpsd as 'name' from OFIBSEDE", OCN)
        DAP_MAc.Fill(ds, "tSede")
        cmb_sede.DataSource = ds.Tables("tSede")
        cmb_sede.DisplayMember = "Name"
        cmb_sede.ValueMember = "Code"
    End Sub
End Class