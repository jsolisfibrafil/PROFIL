Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Data.SqlClient


Public Class MDIPROFIL
    Dim N_form As Integer
    Dim fmain As New frmMain
    Dim ctlMDI As MdiClient
    Public Shared X_form As Integer

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles smnu_Item.Click, NewToolStripButton.Click
        '' Cree una nueva instancia del formulario secundario.
        'Dim ChildForm As New System.Windows.Forms.Form
        '' Conviértalo en un elemento secundario de este formulario MDI antes de mostrarlo.
        'ChildForm.MdiParent = Me
        'm_ChildFormNumber += 1
        'ChildForm.Text = "Ventana " & m_ChildFormNumber
        'ChildForm.Show()
        CallForms(2)
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles smnu_grpitem.Click
        'Dim OpenFileDialog As New OpenFileDialog
        'OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        'OpenFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
        'If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
        '    Dim FileName As String = OpenFileDialog.FileName
        '    ' TODO: Agregar código aquí para abrir el archivo.
        'End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: agregar código aquí para guardar el contenido actual del formulario en un archivo.
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' Utilice My.Computer.Clipboard para insertar el texto o las imágenes seleccionadas en el Portapapeles
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Utilice My.Computer.Clipboard para insertar el texto o las imágenes seleccionadas en el Portapapeles
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Utilice My.Computer.Clipboard.GetText() o My.Computer.Clipboard.GetData para recuperar la información del Portapapeles.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Cierre todos los formularios secundarios del primario.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

#Region "Llamada de apertura distintos formularios dentro del MDI principal"
    Sub CallForms(ByVal N_form As Integer)
        If N_form = 1 Then
            Dim frmEmpl As New frmEmpl
            frmEmpl.Show()
            frmEmpl.MdiParent = Me
            ' frmEmpl.Parent = Me.SplitContainer1.Panel2
        End If
        If N_form = 2 Then
            Dim frmItm As New frmItem
            frmItm.Show()
            frmItm.MdiParent = Me
            'frmItm.Parent = Me.SplitContainer1.Panel2
        End If
        If N_form = 3 Then
            Dim frmOrdprod As New frmOP2
            frmOrdprod.Show()
            frmOrdprod.MdiParent = Me
            'frmOrdprod.Parent = Me.SplitContainer1.Panel2
        End If
        If N_form = 4 Then
            Dim frmRecprod As New frm_RcpItem
            frmRecprod.Show()
            frmRecprod.MdiParent = Me
            'frmRecprod.Parent = Me.SplitContainer1.Panel2
        End If
        If N_form = 5 Then
            Dim frmlist As New frmList
            frmlist.Show()
            frmlist.MdiParent = Me
            'frmlist.Parent = Me.SplitContainer1.Panel2
        End If
        If N_form = 6 Then
            Dim frmPlanif As New frmplanifprod
            frmPlanif.Show()
            frmPlanif.MdiParent = Me
            'frmPlanif.Parent = Me.SplitContainer1.Panel2
        End If
      
        If N_form = 8 Then
            Dim frmWHS As New frmWHS
            frmWHS.Show()
            frmWHS.MdiParent = Me
            'frmWHS.Parent = Me.SplitContainer1.Panel2
        End If

    End Sub

#End Region

#Region "Manejo de botones del menu principal del MDI"
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CallForms(2)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CallForms(1) ' Operarios
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CallForms(3)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CallForms(4)
    End Sub
    Private Sub smnu_plan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smnu_plan.Click
        CallForms(6)
    End Sub
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smnu_empl.Click
        CallForms(1)
    End Sub
    Private Sub ReciboDeProduccionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smnu_recibo.Click
        CallForms(4)
    End Sub
    Private Sub smnu_listitm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smnu_listitm.Click
        CallForms(5)
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CallForms(7)
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        CallForms(8)
    End Sub
#End Region

#Region "Manejo y funcionamiento del formulario MDI Principal"

#End Region

#Region "Llamada de acciones desde menu contextual"
    Private Sub OrdenDeProduccionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smnu_order.Click
        CallForms(3)
    End Sub

#End Region

    Private Sub MenuPrincipalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuPrincipalToolStripMenuItem.Click
        If fmain.Visible = False Then
            fmain.Visible = True
        Else
            fmain.Visible = False
        End If
    End Sub

    Private Sub MDIPROFIL_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub MDIPROFIL_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim i_cierre As Integer
        i_cierre = MessageBox.Show("Está procediendo a cerrar la aplicación. ¿Esta seguro de esta acción?", "FIBRAFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
        e.Cancel = CierraForm(i_cierre)
    End Sub

    Public Function CierraForm(ByVal x As Integer) As Boolean
        If x = 6 Then
            Dim CMD99 As New SqlCommand '
            Try
                If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
                OCN.Open()
                With CMD99
                    .Parameters.Clear()
                    .Connection = OCN
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "U_SP_INOUTSESIONES"
                    .Parameters.Add(New SqlParameter("@CODE", SqlDbType.VarChar)).Value = Form1.vs_idUser
                    .Parameters.Add(New SqlParameter("@option", SqlDbType.VarChar)).Value = 0
                    .ExecuteNonQuery()
                End With

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            Return True
        End If
    End Function



    Private Sub MDIPROFIL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ToolStripStatusLabel1.Text = "Area : " + Form1.vs_Area
        Me.ToolStripStatusLabel2.Text = " Maquina : " + Form1.vs_Mac
        Me.ToolStripStatusLabel3.Text = " Usuario : " + Form1.vs_User + " [" + Form1.vs_isADM + "]"
        'Call AsignaColor()


        If Form1.vs_isADM <> "Y" Then
            mnu_master.Visible = False
            CutToolStripMenuItem.Visible = False
        End If
        fmain.Show()
        fmain.MdiParent = Me

        Dim CMD As New SqlCommand("U_SP_LISTPPLANIF", OCN)
        Dim dts As New DataSet
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add(New SqlParameter("@IDAREA", SqlDbType.Text)).Value = Form1.vs_idArea ' Area  
        CMD.Parameters.Add(New SqlParameter("@IDMAC", SqlDbType.Text)).Value = Form1.vs_idMac  ' Maquina
        Try
            Dim dap As New SqlDataAdapter(CMD)
            dap.Fill(dts, "vLPLAN")
            If dts.Tables("vLPLAN").Rows.Count > 0 Then
                Dim f1 As New frmLISTPLAN
                f1.Show()
                f1.MdiParent = Me
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smnu_calc.Click
        Try
            Shell("Calc", AppWinStyle.NormalFocus)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Fibrafil")
        End Try
    End Sub

    Private Sub PlanificacionesProgramadasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlanificacionesProgramadasToolStripMenuItem.Click
        Dim f1 As New frmLISTPLAN
        f1.Show()
        f1.MdiParent = Me
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim Fref As New AboutBox1
        Fref.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Dim Titulo As String = "Sistema de Producción Fibrafil S.A."
            Me.Text = Me.Text + Titulo.Substring(Me.Text.Length, 1)
            If Me.Text.Length = 35 Then Timer1.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InformeIntegradoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformeIntegradoToolStripMenuItem.Click
        Dim finfo As New frmInformes
        finfo.Show()
        finfo.MdiParent = Me
    End Sub


    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        ProgressBar1.Value += 1
        If ProgressBar1.Value = ProgressBar1.Maximum Then
            ProgressBar1.Enabled = False
            ProgressBar1.Visible = False
            lblMensaje.Visible = False
            Timer2.Stop()
        End If
    End Sub


    Sub AsignaColor()
        Dim ctl As Control

        'Estamos buscando en control que representa el area cliente MDI 
        For Each ctl In Me.Controls

            Try
                ctlMDI = CType(ctl, MdiClient)
                ' Asignamos el color de fondo 
                ctlMDI.BackColor = Color.AliceBlue

                'Aquí asignamos el manejador para pintar el fondo con degradados o lo que
                'queramos. Si solo queremos cambiar el color de fondo no hace falta, ni las funciones siguientes tampoco
                AddHandler ctlMDI.Paint, AddressOf PintarFondo
            Catch ex As InvalidCastException

            End Try

        Next
    End Sub
    Private Sub PintarFondo(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim GradientePanel As New LinearGradientBrush(New RectangleF(0, 0, ctlMDI.Width, ctlMDI.Height), Color.FromArgb(170, 192, 240), Color.FromArgb(170, 192, 255), LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(GradientePanel, New RectangleF(0, 0, ctlMDI.Width, ctlMDI.Height))
    End Sub

    Private Sub MDIPROFIL_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If WindowState <> FormWindowState.Minimized Then
            If Not (Me.ctlMDI Is Nothing) Then
                Me.PintarFondo(Me.ctlMDI, New PaintEventArgs(Me.ctlMDI.CreateGraphics, New Rectangle(Me.ctlMDI.Location, Me.ctlMDI.Size)))
            End If
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f1 As New Form1
        f1.Show()
        f1.MdiParent = Me
    End Sub

    Private Sub DevolucionDeMercaderiaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DevolucionDeMercaderiaToolStripMenuItem.Click
       
    End Sub

    Private Sub AnularCodigosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnularCodigosToolStripMenuItem.Click
        Dim f1 As New frm_anulaCODE
        f1.ShowDialog()
    End Sub

    Private Sub ExportacionOrdenProduccionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportacionOrdenProduccionToolStripMenuItem.Click
        Dim f_toSAP1 As New frm_OP_to_SAP
        f_toSAP1.Show()
        f_toSAP1.MdiParent = Me
    End Sub

    Private Sub RotularProductosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RotularProductosToolStripMenuItem.Click
        Dim f_rotula As New frm_rotulados
        f_rotula.Show()
        f_rotula.MdiParent = Me
    End Sub

    Private Sub ReEtiquetarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReEtiquetarToolStripMenuItem.Click
        Dim frm_reimpresion As New frm_reimpresion
        frm_reimpresion.Show()
        frm_reimpresion.MdiParent = Me
    End Sub

    Private Sub ProducciónMasivaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProducciónMasivaToolStripMenuItem.Click
        Dim frm_prodmas As New frm_prodMasiva
        frm_prodmas.Show()
        frm_prodmas.MdiParent = Me
    End Sub

    Private Sub ExportarGuiasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportarGuiasToolStripMenuItem.Click
        Dim f_toSAP1 As New frm_GR_to_SAP
        f_toSAP1.Show()
        f_toSAP1.MdiParent = Me
    End Sub

    Private Sub SalidaDeProductosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalidaDeProductosToolStripMenuItem.Click
        Dim frm_OutsItm As New frm_OutsItm
        frm_OutsItm.Show()
        frm_OutsItm.MdiParent = Me
    End Sub

    Private Sub ActualizacionFechasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActualizacionFechasToolStripMenuItem.Click
        Dim frm_upd As New frm_upd_date
        frm_upd.Show()
        frm_upd.MdiParent = Me
    End Sub

    Private Sub GuíaDeRemisiónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuíaDeRemisiónToolStripMenuItem.Click
        Dim frm_GRsimple As New frm_GRXrpss
        frm_GRsimple.Show()
        frm_GRsimple.MdiParent = Me
    End Sub
End Class
