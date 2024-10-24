Public Class frmMain

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CallForms(1) ' Operarios
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CallForms(2)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        CallForms(3)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        CallForms(4)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        CallForms(5)
    End Sub

    Sub CallForms(ByVal N_form As Integer)
        If N_form = 1 Then
            Dim frmEmpl As New frmEmpl
            frmEmpl.Show()
            frmEmpl.MdiParent = MDIPROFIL
            'frmEmpl.Parent = Me.SplitContainer1.Panel2
        End If
        If N_form = 2 Then
            Dim frmItm As New frmItem
            frmItm.Show()
            frmItm.MdiParent = MDIPROFIL
        End If
        If N_form = 3 Then
            Dim frmOrdprod As New frm_prodMasiva
            frmOrdprod.Show()
            frmOrdprod.MdiParent = MDIPROFIL
        End If
        If N_form = 4 Then
            Dim frmRecprod As New frm_RcpItem
            frmRecprod.Show()
            frmRecprod.MdiParent = MDIPROFIL
        End If
        If N_form = 5 Then
            Dim frmguia As New frmEntregas
            frmguia.Show()
            frmguia.MdiParent = MDIPROFIL
        End If
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIPROFIL.MenuPrincipalToolStripMenuItem.Checked = False
        Me.Visible = False
        e.Cancel = True
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Left = 10
        Me.Top = 10
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Los botones 1,2 y 4 del menú principal solo estarán disponibles para usuarios administrador
        If Form1.vs_isADM <> "Y" Then
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Enabled = False
        End If
    End Sub

End Class
