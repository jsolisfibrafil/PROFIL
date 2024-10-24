Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmConsulta
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros
    Public Shared id_object As Integer
    Private Dvlista As DataView
    Dim dts As New DataSet
    Dim cmd As New SqlCommand()
    Dim Vtab As String
    Event PasaVars()

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        frmplanifprod.scode = ""
        frmplanifprod.sdesc = ""
        frmList.scode0 = ""
        frmList.sname0 = ""
        frmList.scode = ""
        frmList.sname = ""
        frmList.sumed = ""
        frmEntregas.scode = ""
        frmEntregas.sname = ""
        frm_OutsItm.scode = ""
        frm_OutsItm.sname = ""
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call ObtenerData()
        Call LlenarData()
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
            parm.Value = cVars.NQP  ' N° 0 (cero) indica articulos
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

    Private Sub DataGrid1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGrid1.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            Aceptar()
        End If
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick, OK_Button.Click
        Aceptar()
    End Sub

    Sub Aceptar()
        ' Try
        Dim X As Integer
        X = DataGrid1.CurrentRowIndex
        If frmList.NQ = 1 Then  'BTNLIST
            frmList.scode0 = Dvlista.Item(X)("code").ToString
            frmList.sname0 = Dvlista.Item(X)("name").ToString
            frmList.swhs0 = Dvlista.Item(X)("almacen").ToString
        End If

        If frmList.NQ = 2 Then 'DATAGRID1
            frmList.scode = Dvlista.Item(X)("code").ToString
            frmList.sname = Dvlista.Item(X)("name").ToString
            frmList.sumed = Dvlista.Item(X)("UM").ToString
            frmList.swhs = Dvlista.Item(X)("ALMACEN").ToString
        End If

        If frmList.NQ = 3 Then 'BUTTON1
            frmList.swhs0 = Dvlista.Item(X)("code").ToString
        End If

        If frmplanifprod.NQ = 1 Then
            frmplanifprod.scode = Dvlista.Item(X)("code").ToString
            frmplanifprod.sdesc = Dvlista.Item(X)("name").ToString
        End If

        If frmplanifprod.NQ = 2 Then
            frmplanifprod.s_idcli = Dvlista.Item(X)("code").ToString
            frmplanifprod.s_dsccli = Dvlista.Item(X)("name").ToString
        End If

        Select Case id_object
            Case 1
                If frmOP2.NQ = 1 Then
                    frmOP2.scode = Dvlista.Item(X)("code").ToString
                    frmOP2.sname = Dvlista.Item(X)("name").ToString
                    frmOP2.sumed = Dvlista.Item(X)("um").ToString
                    frmOP2.scodebar = Dvlista.Item(X)("codebar").ToString
                End If

                If frmOP2.NQ = 2 Then
                    frmOP2.scode0 = Dvlista.Item(X)("code").ToString
                    frmOP2.sname0 = Dvlista.Item(X)("name").ToString
                    frmOP2.sumed0 = Dvlista.Item(X)("um").ToString
                    frmOP2.swhs0 = Dvlista.Item(X)("ALMACEN").ToString
                End If
            Case 2
                'If frmOrdprod.NQ = 1 Then
                '    frmOrdprod.scode = Dvlista.Item(X)("code").ToString
                '    frmOrdprod.sname = Dvlista.Item(X)("name").ToString
                '    frmOrdprod.sumed = Dvlista.Item(X)("um").ToString
                'End If

                'If frmOrdprod.NQ = 2 Then
                '    frmOrdprod.scode0 = Dvlista.Item(X)("code").ToString
                '    frmOrdprod.sname0 = Dvlista.Item(X)("name").ToString
                '    frmOrdprod.sumed0 = Dvlista.Item(X)("um").ToString
                '    frmOrdprod.swhs0 = Dvlista.Item(X)("ALMACEN").ToString
                'frmOrdprod.scodebar = Dvlista.Item(X)("codebar").ToString
                'End If

            Case 3
                If frm_OutsItm.NQ = 1 Then
                    frm_OutsItm.scode = Dvlista.Item(X)("code").ToString
                    frm_OutsItm.sname = Dvlista.Item(X)("name").ToString
                End If

                If frm_OutsItm.NQ = 2 Then
                    frm_OutsItm.scode = Dvlista.Item(X)("code").ToString
                    frm_OutsItm.sname = Dvlista.Item(X)("name").ToString
                End If

        End Select


      

        If frm_prodMasiva.NQ = 1 Then
            frm_prodMasiva.scode = Dvlista.Item(X)("code").ToString
            frm_prodMasiva.sname = Dvlista.Item(X)("name").ToString
            frm_prodMasiva.sumed = Dvlista.Item(X)("um").ToString
        End If


        If frmEntregas.NQ = 1 Then
            frmEntregas.scode = Dvlista.Item(X)("code").ToString
            frmEntregas.sname = Dvlista.Item(X)("name").ToString
        End If

        RaiseEvent PasaVars()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
       ' Catch ex As Exception
        '  MsgBox(ex.Message)
        ' End Try

    End Sub
End Class
