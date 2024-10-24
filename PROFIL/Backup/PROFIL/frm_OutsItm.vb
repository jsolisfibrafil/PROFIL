Imports System.Data.SqlClient

Public Class frm_OutsItm


    Public Shared NQ As Integer 'NUMERO DE CONSULTA 
    Dim i_IO As Integer = 0
    Public Shared scode, sname As String
    Dim WithEvents FQ As New frmConsulta
    Dim dts As DataSet
    Private DvCabecera, DvDetalle As DataView
    Public WithEvents posi As CurrencyManager 'Para la navegacion de los registros
    Private bsource As BindingSource = New BindingSource()
    Dim cmd, cmd1 As New SqlCommand()

    Private Sub btnquery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnquery.Click
        If i_IO = 1 Then
            Dim formQuery As New frmConsulta
            frmConsulta.id_object = 3
            cVars.NQP = 6 '
            NQ = 1
            formQuery.ShowDialog()
            Call Recibevar()
        End If
    End Sub

    Public Sub Recibevar() Handles FQ.PasaVars
        Try
            '-----> Obtiene el cliente <-----'
            If NQ = 1 Then
                txt_idclie.Text = scode
                txt_nomclie.Text = sname
            End If

            If NQ = 2 Then
                DataGridView1.Item("ITEM", DataGridView1.CurrentRow.Index).Value = scode
                DataGridView1.Item("DESCRIPCION", DataGridView1.CurrentRow.Index).Value = sname

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        If Habilita_SaveUpdate(True) Then
            Try
                cmd.Parameters.Clear()
                cmd.Connection = OCN
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "U_SP_FIB_INS_OUTsCAB"
                With cmd.Parameters
                    .Add(New SqlParameter("@Mtv", SqlDbType.Text)).Value = cmb_moti.Text
                    .Add(New SqlParameter("@fecha", SqlDbType.SmallDateTime)).Value = dtpFdoc.Value
                    .Add(New SqlParameter("@idCLie", SqlDbType.Text)).Value = txt_idclie.Text
                    .Add(New SqlParameter("@text", SqlDbType.Text)).Value = txt_text.Text
                    .Add(New SqlParameter("@HOST", SqlDbType.Text)).Value = My.Computer.Name
                    .Add(New SqlParameter("@Createfor", SqlDbType.Text)).Value = Form1.vs_idUser
                    .Add(New SqlParameter("@NError", SqlDbType.Int)).Direction = ParameterDirection.Output
                    .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                    .Add(New SqlParameter("@FK", SqlDbType.BigInt)).Direction = ParameterDirection.Output
                End With

                If OCN.State = ConnectionState.Closed Then OCN.Open()
                cmd.ExecuteNonQuery()



                If cmd.Parameters("@NError").Value = 0 Then
                    ' Graba el detalle del resumen de la orden de produccion
                    With cmd1
                        .Parameters.Clear()
                        .Connection = OCN
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "U_SP_FIB_INS_OUTsDET"
                        .Parameters.Add(New SqlParameter("@RECORDKEY", SqlDbType.BigInt))
                        .Parameters.Add(New SqlParameter("@Item", SqlDbType.VarChar))
                        .Parameters.Add(New SqlParameter("@Descripcion", SqlDbType.VarChar))
                        .Parameters.Add(New SqlParameter("@Cantidad", SqlDbType.Decimal))
                        .Parameters.Add(New SqlParameter("@Peso", SqlDbType.Decimal))
                        .Parameters.Add(New SqlParameter("@Almacen", SqlDbType.VarChar))


                        For k As Integer = 0 To DvDetalle.Count - 1
                            If (DvDetalle.Item(k)("Cantidad") > 0) Or (DvDetalle.Item(k)("Peso") > 0) Then
                                .Parameters("@RECORDKEY").Value = cmd.Parameters("@FK").Value.ToString
                                .Parameters("@Item").Value = DvDetalle.Item(k)("Item")
                                .Parameters("@Descripcion").Value = DvDetalle.Item(k)("Descripcion")
                                .Parameters("@Cantidad").Value = DvDetalle.Item(k)("Cantidad")
                                .Parameters("@Peso").Value = DvDetalle.Item(k)("Peso")
                                .Parameters("@Almacen").Value = DvDetalle.Item(k)("Almacen")
                                .ExecuteNonQuery()
                            End If
                        Next k
                    End With
                End If


                MessageBox.Show(cmd.Parameters("@msg").Value.ToString(), "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            i_IO = 0
            btnConfirma.Text = "&Ok"

        Else
            Exit Sub
        End If
    End Sub

    Function Habilita_SaveUpdate(ByVal VALOR As Boolean)
        If i_IO = 1 Then
            If dtpFdoc.Value > Date.Now Then
                MsgBox("La fecha de documento no puede ser mayor a la fecha actual.", MsgBoxStyle.Exclamation, "PROFIL")
                VALOR = False
            End If
            Return VALOR
        End If
    End Function


    Private Sub frm_OutsItm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Obtener_Data()
        LlenarData()
        Navega(3)
    End Sub


    Sub Obtener_Data()

        dts = New DataSet("SALIDAS")

        Dim CMD As New SqlCommand("U_LIS_CABFRM", OCN)
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add(New SqlParameter("@NOPT", SqlDbType.Int)).Value = 1  ' Numero informe  
        CMD.Parameters.Add(New SqlParameter("@PROP01", SqlDbType.Int)).Value = -99  ' 
        CMD.Parameters.Add(New SqlParameter("@PROP02", SqlDbType.Text)).Value = ""   ' 
        CMD.Parameters.Add(New SqlParameter("@NERROR", SqlDbType.Int)).Value = -1
        CMD.Parameters.Add(New SqlParameter("@MERROR", SqlDbType.Text)).Value = ""

        Dim dap As New SqlDataAdapter()
        dap.SelectCommand = CMD

        Dim CABECERA As DataTable = dts.Tables.Add("CAB_SALIDAS")
        dap.Fill(CABECERA)



        Dim CMD1 As New SqlCommand("U_LIS_DETFRM", OCN)
        CMD1.CommandType = CommandType.StoredProcedure
        CMD1.Parameters.Add(New SqlParameter("@NOPT", SqlDbType.Int)).Value = 1  ' Numero informe  
        CMD1.Parameters.Add(New SqlParameter("@KEY", SqlDbType.Int)).Value = txt_id.Text   ' 
        CMD1.Parameters.Add(New SqlParameter("@PROP02", SqlDbType.Text)).Value = ""   ' 
        CMD1.Parameters.Add(New SqlParameter("@NERROR", SqlDbType.Int)).Value = -1
        CMD1.Parameters.Add(New SqlParameter("@MERROR", SqlDbType.Text)).Value = ""

        Dim dap1 As New SqlDataAdapter()
        dap1.SelectCommand = CMD1

        Dim DETALLE As DataTable = dts.Tables.Add("DET_SALIDAS")
        dap1.Fill(DETALLE)

      

        DvDetalle = dts.Tables("DET_SALIDAS").DefaultView
        DvDetalle.AllowEdit = False
        DvDetalle.AllowNew = False

       
    End Sub

    Sub LlenarData()
        posi = CType(BindingContext(dts.Tables("CAB_SALIDAS")), CurrencyManager)
        posi.Position = 0
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)

        txt_id.DataBindings.Add("text", dts.Tables("CAB_SALIDAS"), "KEY")
        txt_docnum.DataBindings.Add("text", dts.Tables("CAB_SALIDAS"), "DocNum")
        dtpFdoc.DataBindings.Add("value", dts.Tables("CAB_SALIDAS"), "Fecha")
        cmb_moti.DataBindings.Add("text", dts.Tables("CAB_SALIDAS"), "Motivo")
        txt_idclie.DataBindings.Add("text", dts.Tables("CAB_SALIDAS"), "Cliente")
        txt_nomclie.DataBindings.Add("text", dts.Tables("CAB_SALIDAS"), "Descripcion")
        txt_text.DataBindings.Add("text", dts.Tables("CAB_SALIDAS"), "Comentario")


        DataGridView1.DataSource = DvDetalle
        If DvDetalle.Count > 0 Then
            DvDetalle.RowFilter = "Key = " + txt_id.Text
        End If
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        i_IO = 1
        btnConfirma.Text = "&Crear"
        Me.ContextMenuStrip = Nothing
        Limpiar_TextBox(Me)
        dtpFdoc.Value = DateAdd(DateInterval.Day, 0, Now.Date)
        AddNewDataRowView()

    End Sub

    Private Sub AddNewDataRowView()
        ' Evento llamado por el procedimiento NUEVO 
        DvDetalle = dts.Tables("DET_SALIDAS").DefaultView
        DvDetalle.AllowEdit = True
        DvDetalle.AllowNew = True

        DvDetalle.RowFilter = "Key = -99"

        For X As Int32 = 0 To 49
            Dim rowView As DataRowView = DvDetalle.AddNew
            rowView("KEY") = "-99"
            rowView("Item") = ""
            rowView("Descripcion") = ""
            rowView("Cantidad") = 0.0
            rowView("Peso") = 0.0
            rowView("Almacen") = ""
            rowView.EndEdit()
        Next X

        DataGridView1.DataSource = DvDetalle

        'DvDetalle.AllowEdit = False
        DvDetalle.AllowNew = False

        DataGridView1_Formato()

    End Sub

    Private Sub DataGridView1_Formato()
        Dim band0 As DataGridViewBand = DataGridView1.Columns("KEY")
        Dim band1 As DataGridViewBand = DataGridView1.Columns("ITEM")
        Dim band2 As DataGridViewBand = DataGridView1.Columns("DESCRIPCION")

        band0.Visible = False
        band1.ReadOnly = True
        band2.ReadOnly = True
    End Sub

#Region "Navegacion de registros"

    Public Sub Navega(ByVal N_form As Integer)
        Select Case N_form
            Case 0 ' First
                posi.Position = 0
            Case 1 ' Before
                If posi.Position = 0 Then
                    posi.Position = posi.Count - 1
                    MsgBox("Ha pasado al ultimo registro", MsgBoxStyle.Information, "PROFIL")
                Else
                    posi.Position -= 1
                End If
            Case 2 ' Next
                If posi.Position = posi.Count - 1 Then
                    posi.Position = 0
                    MsgBox("Ha pasado al primer registro", MsgBoxStyle.Information, "PROFIL")
                Else
                    posi.Position += 1
                End If
            Case 3
                posi.Position = posi.Count - 1
        End Select
        SubNavegacion()
    End Sub

    Sub SubNavegacion()
        txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        DvDetalle.RowFilter = "KEY = " + "'" + txt_id.Text + "'"
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


    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        If i_IO = 1 Then
            If (DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Columns("ITEM").Index) Or (DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Columns("DESCRIPCION").Index) Then
                frmConsulta.id_object = 3
                NQ = 2
                Dim Fq As New frmConsulta
                cVars.NQP = 5 'CONSULTA PARA ITEMS
                Fq.ShowDialog()
                Call Recibevar()
            End If
        End If
    End Sub
End Class