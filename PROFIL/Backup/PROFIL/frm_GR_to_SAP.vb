Imports System.Data.SqlClient

Public Class frm_GR_to_SAP

    Private Sub frm_GR_to_SAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_DATA(1, "", 0, "2000-01-01", "")
    End Sub

    Sub Obtener_DATA(ByVal opcion As Integer, ByVal Item As String, ByVal Cant As Decimal, ByVal Fecha As Date, ByVal Telar As String)


        If OCN.State = ConnectionState.Closed Then OCN.Open()
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

        Try
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

  



End Class