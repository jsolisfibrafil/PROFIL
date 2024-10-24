Imports System.Net.Dns

Imports System
Imports System.Net
Imports PROFIL.Module1
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation

Public Class Form1
    Public Shared vs_User, vs_Area, vs_Mac, vs_idUser, vs_idArea, vs_idMac, vs_isADM, vs_iArea, vs_Host, vs_MacAddres, vs_sede As String
    Dim dts As New DataSet
    Dim i_cierre As Integer
    ' Dim LocalHostName As String = Dns.GetHostName
    'Dim LocalIP As IPHostEntry = Dns.GetHostEntry(LocalHostName)
    Dim IPAdd As System.Net.NetworkInformation.IPAddressInformation


    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If i_cierre = 0 Then
            Application.Exit()
        End If
    End Sub

    Function getMacAddress()
        Dim nics() As NetworkInterface = _
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function

    Function RetIPAddress(ByVal mStrHost As String) As String
        Try
            Dim mIpHostEntry As IPHostEntry = GetHostByName(mStrHost)
            Dim mIpAddLst As IPAddress() = mIpHostEntry.AddressList()
            'Devuelve la primera IP
            Return mIpAddLst(0).ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        vs_Host = My.Computer.Name
        vs_MacAddres = getMacAddress()

        Dim CMD As New SqlCommand("U_SP_LISVFHOST", OCN)
        With CMD.Parameters
            CMD.CommandType = CommandType.StoredProcedure
            .Add(New SqlParameter("@namehost", SqlDbType.Text)).Value = My.Computer.Name
            .Add(New SqlParameter("@MAcAddres", SqlDbType.Text)).Value = getMacAddress()
            .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
            .Add(New SqlParameter("@NERROR", SqlDbType.Int)).Direction = ParameterDirection.Output '
        End With

        If OCN.State = ConnectionState.Open Then OCN.Close()
        OCN.Open()
        Try
            Dim DAP As New SqlDataAdapter(CMD)
            Dim XTSHOST As Integer
            DAP.Fill(dts, "host")

            XTSHOST = CInt(dts.Tables("host").Rows(0).Item(0))

            Select Case XTSHOST
                Case 1
                    TextBox1.Text = My.Computer.Info.OSFullName
                    TextBox2.Text = My.Computer.Info.OSVersion
                    TextBox3.Text = vs_MacAddres
                    TextBox4.Text = vs_Host
                    TextBox5.Text = RetIPAddress(GetHostName())
                    Panel1.Visible = True
                Case Else
                    Dim dap0 As New SqlDataAdapter("Select IDsede from ofibhost WHERE  [MacAddres] = '" & Form1.vs_MacAddres & "' and [NameHost]= '" & Form1.vs_Host & "'", OCN)
                    dap0.SelectCommand.CommandType = CommandType.Text

                    'If dts.Tables.Contains("vLISTTMP") Then
                    '    dts.Tables("vLISTTMP").Clear()
                    'End If
                    dap0.Fill(dts, "vIDSEDE")

                    Dim dt0 As DataTable = dts.Tables("vIDSEDE")
                    lbl_sede.DataBindings.Add("text", dts.Tables("vIDSEDE"), "IDsede")
                    vs_sede = lbl_sede.Text
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            OCN.Close()
        End Try


        ' Importacion del codigo y nombre de empleados para el combobox de usuarios
        Try

            Dim DAP_User As New SqlDataAdapter("Select Code, Name As 'Empleado',isnull(ISADM,'-')AS 'ISADM' from  OFIBEMPL inner join OFIBUSER on Code = Id_User where isnull(INACTIVO,'')<>'Y'", OCN)
            DAP_User.SelectCommand.CommandType = CommandType.Text
            DAP_User.Fill(dts, "tEmpl")

            cmbUser.DataSource = dts.Tables("tEmpl")
            cmbUser.DisplayMember = "Empleado"
            cmbUser.ValueMember = "Code"

            vs_isADM = Label5.DataBindings.Add("text", dts.Tables("tEmpl"), "ISADM").ToString

            Dim DAP_Area As New SqlDataAdapter("Select Code, Descripcion As 'Area' from  OFIBAREA where locked <>'Y'", OCN)
            DAP_Area.SelectCommand.CommandType = CommandType.Text
            DAP_Area.Fill(dts, "tArea")

            cmbArea.DataSource = dts.Tables("tArea")
            cmbArea.DisplayMember = "Area"
            cmbArea.ValueMember = "Code"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnIngreso(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIN.Click
        Dim CMD As New SqlCommand("U_SP_lOGIN", OCN)
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add(New SqlParameter("@COD_EMP", SqlDbType.Text)).Value = cmbUser.SelectedValue.ToString ' Cod Empleado
        CMD.Parameters.Add(New SqlParameter("@PWD", SqlDbType.Text)).Value = txtpsw.Text ' Password
        If OCN.State = ConnectionState.Open Then OCN.Close() ' Para el caso d q 2 usuarios quieren realizar el mantenimiento a la misma vez 
        OCN.Open()
        Try
            Dim DAP As New SqlDataAdapter(CMD)
            Dim INOUT As Integer
            DAP.Fill(dts, "Login")

            INOUT = CInt(dts.Tables("Login").Rows(0).Item(0))

            Select Case INOUT
                Case 1
                    vs_idMac = "01" 'cmbMac.SelectedValue.ToString
                    vs_idArea = cmbArea.SelectedValue.ToString
                    vs_idUser = cmbUser.SelectedValue.ToString
                    vs_isADM = Label5.Text
                    vs_Mac = "01" 'cmbMac.Text
                    vs_User = cmbUser.Text
                    MsgBox("Acceso autorizado", MsgBoxStyle.Information)

                    Dim CMD99 As New SqlCommand '
                    With CMD99
                        .Parameters.Clear()
                        .Connection = OCN
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "U_SP_INOUTSESIONES"
                        .Parameters.Add(New SqlParameter("@CODE", SqlDbType.VarChar)).Value = vs_idUser
                        .Parameters.Add(New SqlParameter("@option", SqlDbType.VarChar)).Value = 1
                        .ExecuteNonQuery()
                    End With

                    MDIPROFIL.Show()
                    MDIPROFIL.ToolStripStatusLabel1.Text = "Area : " + Form1.vs_Area
                    MDIPROFIL.ToolStripStatusLabel2.Text = " Maquina : " + Form1.vs_Mac
                    MDIPROFIL.ToolStripStatusLabel3.Text = " Usuario : " + Form1.vs_User + " [" + Form1.vs_isADM + "]"

                    i_cierre = 1
                    Me.Close()
                Case 0
                    If INOUT = 0 Then
                        MessageBox.Show("Contraseña incorrecta", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("Usuario no registrado o no activo", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Case 2
                    MessageBox.Show("Este usuario ya tiene una sesion iniciada", "PROFIL", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Select

        Catch ex As SqlException
            MessageBox.Show(ex.Message, "Fibrafil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            dts.Dispose()
            dts.Tables("Login").Rows.Clear()
            OCN.Close()
        End Try
    End Sub

    Private Sub btnOUT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOUT.Click
        Me.Close()
    End Sub

    Private Sub cmbArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbArea.SelectedIndexChanged
        vs_idArea = cmbArea.SelectedValue.ToString
        vs_Area = Trim(cmbArea.GetItemText(cmbArea.SelectedItem))
    End Sub

    'Private Sub cmbMac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMac.Click
    '    Dim lista_Maqs As List(Of String) = New List(Of String)
    '    Dim Lista As New ArrayList
    '    ' Dim DAP_MAc As New SqlDataAdapter("Select Code, Descripcion As 'Maquina' from  OFIBMAC where STDMAC='A' and IDAREA ='" + vs_idArea + "'", OCN) 'cmbArea.SelectedValue.ToString'
    '    Try
    '        Dim com As SqlCommand = New SqlCommand("Select Code, Descripcion As 'Maquina' from  OFIBMAC where STDMAC='A' and IDAREA ='" + vs_idArea + "'", OCN)
    '        If OCN.State = ConnectionState.Closed Then OCN.Open()

    '        Dim dr As SqlDataReader = com.ExecuteReader()
    '        ' Recorremos el dataReader
    '        While (dr.Read())
    '            lista_Maqs.Add(dr(0).ToString())
    '            Lista.Add(dr.GetValue(1).ToString) ', dr.GetValue(1).ToString))
    '        End While
    '        dr.Close()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        OCN.Close()
    '    End Try

    'Asignamos la lista de bases de datos como origen
    'de datos del combobox

    'DAP_MAc.SelectCommand.CommandType = CommandType.Text
    'DAP_MAc.Fill(dts, "tMac")

    'cmbMac.DataSource = Nothing
    'cmbMac.Items.Clear()
    'cmbMac.Text = ""

    ' cmbMac.DataSource = dts.Tables("tMac")
    ' Me.cmbMac.DataSource = lista_Maqs
    'cmbMac.DisplayMember = "Maquina"
    ' cmbMac.ValueMember = "Code"
    '    With cmbMac
    '' .DropDownStyle = ComboBoxStyle.DropDown
    ''.AutoCompleteMode = AutoCompleteMode.Suggest
    ''.AutoCompleteSource = AutoCompleteSource.ListItems
    '        .DataSource = Lista
    ''.ValueMember = "Code"
    ''.DisplayMember = "Maquina"
    ''.DisplayMember = "code"
    '    End With
    'End Sub

    'Private Sub cmbMac_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMac.SelectedIndexChanged
    '    'vs_idMac = cmbMac.SelectedValue.ToString
    '    'vs_Mac = Trim(cmbMac.GetItemText(cmbMac.SelectedItem))
    'End Sub

    Private Sub cmbUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUser.SelectedIndexChanged
        'vs_idUser = cmbUser.SelectedValue.ToString
        'vs_User = Trim(cmbUser.GetItemText(cmbUser.SelectedItem))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frm_planxTurno.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try
            Dim cmd As New SqlCommand()
            cmd.Parameters.Clear()
            cmd.Connection = OCN
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "U_SP_REGHOST"
            With cmd.Parameters
                .Add(New SqlParameter("@namehost", SqlDbType.Text)).Value = My.Computer.Name
                .Add(New SqlParameter("@iphost", SqlDbType.Text)).Value = RetIPAddress(GetHostName())
                .Add(New SqlParameter("@MAcAddres", SqlDbType.Text)).Value = getMacAddress()
                .Add(New SqlParameter("@SOName", SqlDbType.Text)).Value = My.Computer.Info.OSFullName
                .Add(New SqlParameter("@Winver", SqlDbType.Text)).Value = My.Computer.Info.OSVersion
                .Add(New SqlParameter("@MSG", SqlDbType.VarChar, 250)).Direction = ParameterDirection.Output
                .Add(New SqlParameter("@NERROR", SqlDbType.Int)).Direction = ParameterDirection.Output '


                If OCN.State = ConnectionState.Closed Then OCN.Open()
                cmd.ExecuteNonQuery()

                If cmd.Parameters("@NERROR").Value.ToString() = 1 Then
                    MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Critical, "PROFIL")
                    Exit Sub
                Else
                    MsgBox(cmd.Parameters("@msg").Value.ToString(), MsgBoxStyle.Information, "PROFIL")
                    Panel1.Visible = False
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            OCN.Close()
        End Try
    End Sub

   
End Class
