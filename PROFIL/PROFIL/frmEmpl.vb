Imports PROFIL.MForm
Imports System.Data.SqlClient
Public Class frmEmpl
    Private Dvlista As DataView
    Dim WithEvents posi As CurrencyManager
    Dim dts As New DataSet
    Dim cmd As New SqlCommand()


    Private Sub frmEmpl_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = CierraForms("un", Me.Text, btnConfirma.Text)
    End Sub

    Private Sub frmEmpl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Obtener_Data()
        Call LlenarData()
    End Sub

    Sub LlenarData()
        TextBox1.DataBindings.Add("text", dts.Tables("vEMPL"), "NUM")
        TextBox2.DataBindings.Add("text", dts.Tables("vEMPL"), "CODIGO")
        TextBox3.DataBindings.Add("text", dts.Tables("vEMPL"), "NOMBRE")
        TextBox4.DataBindings.Add("text", dts.Tables("vEMPL"), "APELLIDOS")
        'TextBox7.DataBindings.Add("text", dts.Tables("vEMPL"), "PSWD")
        Label8.DataBindings.Add("text", dts.Tables("vEMPL"), "ADMIN")
        cmbCargo.DataBindings.Add("text", dts.Tables("vEMPL"), "CARGO")
        cmbArea.DataBindings.Add("text", dts.Tables("vEMPL"), "AREA")
        DataGrid1.SetDataBinding(dts.Tables("vEMPL"), "")
    End Sub

    Sub Obtener_Data()
        Try
            Try
                If OCN.State = ConnectionState.Closed Then
                    OCN.Open()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            'DATA
            Dim dap As New SqlDataAdapter("Select T0.Code As 'NUM', Name As 'CODIGO', U_FIB_Nombre As 'NOMBRE'," & _
                            "U_Fib_Apellidos As 'APELLIDOS', T1.Descripcion As 'CARGO', T2.Descripcion As 'AREA'," & _
                            "Isnull(IsAdm,'N') As 'ADMIN' from OFIBEMPL T0, OFIBCARGO T1, OFIBAREA T2 Where T0.U_Fib_Cargo = T1.Code and  T0.U_Fib_Area = T2.Code order by Cast(t0.Code As int) ", OCN)

            dap.SelectCommand.CommandType = CommandType.Text
            dap.Fill(dts, "vEMPL")

            Dim dap_cargo As New SqlDataAdapter("Select * from OFIBCARGO", OCN)
            dap_cargo.Fill(dts, "Cargo")
            cmbCargo.DataSource = dts.Tables("Cargo")
            cmbCargo.DisplayMember = "Descripcion"
            cmbCargo.ValueMember = "Code"

            Dim dap_arEA As New SqlDataAdapter("Select * from OFIBAREA", OCN)
            dap_arEA.Fill(dts, "Area")
            cmbArea.DataSource = dts.Tables("Area")
            cmbArea.DisplayMember = "Descripcion"
            cmbArea.ValueMember = "Code"

            DataGrid1.DataSource = dts.Tables("vEMPL").DefaultView
            posi = CType(BindingContext(dts.Tables("vEMPL")), CurrencyManager)
            posi.Position = posi.Count - 1
            txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Select Case btnConfirma.Text
            Case "&Ok"
                Me.Close()
            Case "&Actualizar"
                If Datosminimos(Trim(TextBox2.Text), Trim(TextBox3.Text), Trim(TextBox4.Text)) = True Then
                    If TextBox7.Text = TextBox5.Text Then 'Validacion de confirmacion de la contraseña
                        DatosInsUpd("U_SP_FIB_UPD_EMPL")
                        If OCN.State = ConnectionState.Closed Then OCN.Open()
                        Try
                            cmd.ExecuteNonQuery()
                            MsgBox("Empleado actualizado satisfactoriamente", MsgBoxStyle.Information, "Fibrafil")
                            btnConfirma.Text = "&Ok"
                        Catch ex As SqlException
                            MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Finally
                            OCN.Close()
                        End Try
                    Else
                        MsgBox("La contraseña de confirmacion no es igual a la contraseña base", MsgBoxStyle.Exclamation, "PROFIL")
                    End If
                End If
            Case "&Crear"
                If Datosminimos(Trim(TextBox2.Text), Trim(TextBox3.Text), Trim(TextBox4.Text)) = True Then
                    If Trim(TextBox7.Text) = Trim(TextBox5.Text) Then 'Validacion de confirmacion de la contraseña
                        DatosInsUpd("U_SP_FIB_INS_EMPL")
                        If OCN.State = ConnectionState.Closed Then OCN.Open()
                        Try
                            cmd.ExecuteNonQuery()
                            MsgBox("Empleado creado satisfactoriamente", MsgBoxStyle.Information, "FIBRAFIL")
                            btnConfirma.Text = "&Ok"
                            VisibilidadBotones(False)
                            Me.ContextMenuStrip = Me.ContextMenuStrip1
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Finally
                            OCN.Close()
                        End Try
                    Else
                        MsgBox("La contraseña de confirmacion no es igual a la contraseña base", MsgBoxStyle.Exclamation, "FIBRAFIL")
                    End If
                End If
        End Select
    End Sub

    Sub DatosInsUpd(ByVal NameProced As String)
        cmd.Parameters.Clear()
        cmd.Connection = OCN
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = NameProced
        cmd.Parameters.Add(New SqlParameter("@code", SqlDbType.Text)).Value = TextBox1.Text ' code de Item
        cmd.Parameters.Add(New SqlParameter("@Name", SqlDbType.Text)).Value = TextBox2.Text ' Nombre de item
        cmd.Parameters.Add(New SqlParameter("@U_FIB_NOMBRE", SqlDbType.Text)).Value = TextBox3.Text
        cmd.Parameters.Add(New SqlParameter("@U_FIB_APELLIDOS", SqlDbType.Text)).Value = TextBox4.Text
        cmd.Parameters.Add(New SqlParameter("@U_FIB_CARGO", SqlDbType.Text)).Value = cmbCargo.SelectedValue.ToString
        cmd.Parameters.Add(New SqlParameter("@U_FIB_AREA", SqlDbType.Text)).Value = cmbArea.SelectedValue.ToString
        cmd.Parameters.Add(New SqlParameter("@PSWD", SqlDbType.Text)).Value = TextBox7.Text
        cmd.Parameters.Add(New SqlParameter("@ISADM", SqlDbType.Text)).Value = Label8.Text
    End Sub

    Private Sub Label8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label8.TextChanged
        If Label8.Text = "Y" Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
    End Sub

    Private Sub TEXTOS(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress
        If btnConfirma.Text = "&Ok" Then
            btnConfirma.Text = "&Actualizar"
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Label8.Text = "Y"
        Else
            Label8.Text = "N"
        End If
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        LimpiaCajas(Me.TabControl1)
        btnConfirma.Text = "&Crear"
        Me.ContextMenuStrip = Nothing
        VisibilidadBotones(True)
        TextBox2.Focus()
    End Sub

    Sub LimpiaCajas(ByVal Control As TabControl)
        Dim Pag As TabPage
        Dim Cajas As New Control
        For Each Pag In Control.TabPages
            For Each Cajas In Pag.Controls
                If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is TextBox Then Cajas.Text = ""
                If TypeOf Cajas Is ComboBox Then Cajas.Text = ""
            Next
        Next
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Dim X As Integer
        X = MessageBox.Show("Desea eliminar este registro", "Fibrafil", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If X = 6 Then
            cmd.Connection = OCN
            cmd.CommandType = CommandType.Text
            cmd.CommandText = ("Delete from OFIBEMPL where Code = " & TextBox1.Text)

            If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
            OCN.Open()
            Try
                cmd.ExecuteNonQuery()
                MsgBox("Empleado : " + TextBox2.Text + " ha sido eliminado", MsgBoxStyle.Information, "Fibrafil")
                btnConfirma.Text = "&Ok"
            Catch ex As SqlException
                MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                OCN.Close()
            End Try
        End If
    End Sub

#Region "Navegacion  de registros"

    Public Sub Navega(ByVal N_form As Integer)
        Select Case N_form
            Case 0 ' First
                posi.Position = 0
                txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
            Case 1 ' Before
                If posi.Position = 0 Then
                    posi.Position = posi.Count - 1
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                    MsgBox("Ha pasado al ultimo registro")
                Else
                    posi.Position -= 1
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                End If
            Case 2 ' Next
                If posi.Position = posi.Count - 1 Then
                    posi.Position = 0
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                    MsgBox("Ha pasado al primer registro")
                Else
                    posi.Position += 1
                    txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
                End If
            Case 3
                posi.Position = posi.Count - 1
                txtPosi.Text = CType(posi.Position + 1, String) + "/" + CType(posi.Count, String)
        End Select
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

    Function Datosminimos(ByVal dato1 As String, ByVal dato2 As String, ByVal dato3 As String) As Boolean
        If dato1 = "" Or dato2 = "" Or dato3 = "" Then
            MsgBox("No cumple exigencia de datos mínimos.", MsgBoxStyle.Exclamation, "FIBRAFIL")
            Return False
        Else
            Return True
        End If
    End Function

    Sub VisibilidadBotones(ByVal valor As Boolean)
        TextBox1.Visible = Not valor
        Label1.Visible = Not valor
        btnFirst.Visible = Not valor
        btnBefore.Visible = Not valor
        btnNext.Visible = Not valor
        btnLast.Visible = Not valor
        txtPosi.Visible = Not valor
    End Sub
End Class