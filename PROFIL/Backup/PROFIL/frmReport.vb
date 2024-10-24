Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmReport

    Dim WithEvents fENTR As New frmEntregas
    Public Shared NFORM, KEYGUIA As String

    Private mrpt_doc As New ReportDocument
    Private mrpt_name As String
    Public pDataSet, dDataset As New DataSet 'Presentacion , Datos
    Public pParameters As CrystalDecisions.Shared.ParameterFields

    Public Property Rpt_Name() As String
        Get
            Return mrpt_name
        End Get
        Set(ByVal Value As String)
            mrpt_name = Value
        End Set
    End Property

    Private Sub frmReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case NFORM
            Case "GUIA"
                Dim dap1 As New SqlClient.SqlDataAdapter("Select * from u_vw_guia where ID = '" & Trim(KEYGUIA) & "'", OCN)
                dap1.SelectCommand.CommandType = CommandType.Text
                dap1.Fill(dDataset, "u_vw_guia")

                Me.Rpt_Name = "cr_guia.rpt"

            Case "PACKINGLIST"
                Dim dap2 As New SqlClient.SqlDataAdapter("Select * from U_VW_PACKINGLIST where ID = '" & Trim(KEYGUIA) & "'", OCN)
                dap2.SelectCommand.CommandType = CommandType.Text
                dap2.Fill(dDataset, "U_VW_PACKINGLIST")

                Me.Rpt_Name = "cr_packing.rpt"

            Case "PackingAlmacen"

                Dim dap As New SqlDataAdapter("U_SP_LIS_PALM", OCN)
                dap.SelectCommand.CommandType = CommandType.StoredProcedure
                dap.SelectCommand.Parameters.Add(New SqlParameter("@KEY", SqlDbType.BigInt)).Value = Trim(KEYGUIA)
                dap.Fill(dDataset, "packingalm")

                Me.Rpt_Name = "cr_packalm.rpt"

                'mrpt_doc.SetParameterValue("Key@", Trim(KEYGUIA))

                'Dim Rpt As New ReportDocument
                'Rpt.FileName = Application.StartupPath & "\REPORTS\cr_packalm.rpt"
                'Rpt.SetDataSource(pDataSet)

            Case "INF_PESOS"
                'rpt_PesoxDia1 [PROFIL.rpt_PesoxDia]
                Me.Rpt_Name = "rpt_PesoxDia.rpt"


        End Select

        Dim pfs1 As New CrystalDecisions.Shared.ParameterFields
        Dim pf2 As New CrystalDecisions.Shared.ParameterField
        Dim pfDiscrete2 As New CrystalDecisions.Shared.ParameterDiscreteValue
        Me.pDataSet = dDataset


        Me.Text = "Reporte Fibrafil: " + mrpt_name
        mrpt_doc.Load("reports\" + mrpt_name)

        If NFORM <> "INF_PESOS" Then
            mrpt_doc.SetDataSource(Me.pDataSet)
        Else
            mrpt_doc.DataSourceConnections.Item(s_Nserver, "PROFIL").SetLogon("sa", "cuerda$12")
        End If


        CrystalReportViewer1.ReportSource = mrpt_doc
        CrystalReportViewer1.DisplayGroupTree = False
    End Sub



    Private Sub CrystalReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load

    End Sub
End Class