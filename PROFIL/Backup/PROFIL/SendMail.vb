Imports System.Net
Imports System.Net.Mail

Public Class SendMail

    Dim fileDialogBox As New OpenFileDialog()

    Private Sub FormEnviarCorreo_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' NotifyIcon1.ShowBalloonTip(5000)
    End Sub
    Sub Enviar_Correo()
        Dim _Message As New System.Net.Mail.MailMessage()
        Dim _SMTP As New System.Net.Mail.SmtpClient
        _SMTP.Credentials = New System.Net.NetworkCredential("alescano@grupofibrafil.com", "Alf007+-1*")
        _SMTP.Host = "grupofibrafil.com"
        _SMTP.Port = 25
        _SMTP.EnableSsl = False

        ' CONFIGURACION_DEL_MENSAJE() 
        _Message.[To].Add("XA7477alflm19@gmail.com") 'Cuenta de Correo al que se le quiere enviar el e-mail 
        _Message.From = New System.Net.Mail.MailAddress("alescano@grupofibrafil.com", "FERRARI", System.Text.Encoding.UTF8) 'Quien lo env�a 
        _Message.Subject = "Compra de su auto" 'Sujeto del e-mail 
        _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion 
        _Message.Body = "Ud. Desea comprar un auto Ferrari FF" 'contenido del mail 
        _Message.BodyEncoding = System.Text.Encoding.UTF8
        _Message.Priority = System.Net.Mail.MailPriority.Normal
        _Message.IsBodyHtml = False
        _Message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
        ' _Message.CC.Add(txtCC.Text)

        'ENVIO() 
        Try
            _SMTP.Send(_Message)
            MessageBox.Show("Mensaje enviado correctamene", "Exito!", MessageBoxButtons.OK)
        Catch ex As System.Net.Mail.SmtpException
            MessageBox.Show(ex.ToString, "Error!", MessageBoxButtons.OK)
        End Try
        _Message = Nothing
        _SMTP = Nothing
    End Sub
    'Sub Enviar_Correo_SinCC()
    '    Dim _Message As New System.Net.Mail.MailMessage()
    '    Dim _SMTP As New System.Net.Mail.SmtpClient
    '    _SMTP.Credentials = New System.Net.NetworkCredential(txtCorreo.Text + cbxClienteCorreo.Text, txtContrase�a.Text)
    '    _SMTP.Host = txtSMTP.Text
    '    _SMTP.Port = txtPuerto.Text
    '    _SMTP.EnableSsl = True

    '    ' CONFIGURACION_DEL_MENSAJE() 
    '    _Message.[To].Add(Me.txtPara.Text.ToString) 'Cuenta de Correo al que se le quiere enviar el e-mail 
    '    _Message.From = New System.Net.Mail.MailAddress(txtCorreo.Text + cbxClienteCorreo.Text, txtNombre.Text, System.Text.Encoding.UTF8) 'Quien lo env�a 
    '    _Message.Subject = Me.txtAsunto.Text.ToString 'Sujeto del e-mail 
    '    _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion 
    '    _Message.Body = Me.txtMensaje.Text.ToString 'contenido del mail 
    '    _Message.BodyEncoding = System.Text.Encoding.UTF8
    '    _Message.Priority = System.Net.Mail.MailPriority.Normal
    '    _Message.IsBodyHtml = False
    '    _Message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure


    '    'ENVIO() 
    '    Try
    '        _SMTP.Send(_Message)
    '        MessageBox.Show("Mensaje enviado correctamene", "Exito!", MessageBoxButtons.OK)
    '    Catch ex As System.Net.Mail.SmtpException
    '        MessageBox.Show(ex.ToString, "Error!", MessageBoxButtons.OK)
    '    End Try
    '    _Message = Nothing
    '    _SMTP = Nothing
    'End Sub
    'Sub Enviar_Correo_Adjunto()
    '    Dim _Message As New System.Net.Mail.MailMessage()
    '    Dim _SMTP As New System.Net.Mail.SmtpClient
    '    _SMTP.Credentials = New System.Net.NetworkCredential(txtCorreo.Text + cbxClienteCorreo.Text, txtContrase�a.Text)
    '    _SMTP.Host = txtSMTP.Text
    '    _SMTP.Port = txtPuerto.Text
    '    _SMTP.EnableSsl = True

    '    ' CONFIGURACION_DEL_MENSAJE() 
    '    _Message.[To].Add(Me.txtPara.Text.ToString) 'Cuenta de Correo al que se le quiere enviar el e-mail 
    '    _Message.From = New System.Net.Mail.MailAddress(txtCorreo.Text + cbxClienteCorreo.Text, txtNombre.Text, System.Text.Encoding.UTF8) 'Quien lo env�a 
    '    _Message.Subject = Me.txtAsunto.Text.ToString 'Sujeto del e-mail 
    '    _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion 
    '    _Message.Body = Me.txtMensaje.Text.ToString 'contenido del mail 
    '    _Message.BodyEncoding = System.Text.Encoding.UTF8
    '    _Message.Priority = System.Net.Mail.MailPriority.Normal
    '    _Message.IsBodyHtml = False
    '    Dim adjunto = New System.Net.Mail.Attachment(txtUbicacion.Text + "\" + txtNombreArchivo.Text)
    '    _Message.Attachments.Add(adjunto)
    '    _Message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
    '    _Message.CC.Add(txtCC.Text)
    '    'ENVIO() 
    '    Try
    '        _SMTP.Send(_Message)
    '        MessageBox.Show("Mensaje enviado correctamene", "Exito! - www.JandC-Ec.webs.com", MessageBoxButtons.OK)
    '    Catch ex As System.Net.Mail.SmtpException
    '        MessageBox.Show(ex.ToString, "Error! - www.JandC-Ec.webs.com", MessageBoxButtons.OK)
    '    End Try
    '    _Message = Nothing
    '    _SMTP = Nothing
    'End Sub
    'Public Function AbrirArchivo() As String
    '    'declarar una cadena, esto se contendr� el nombre del archivo que volvamos 
    '    Dim strFileName = ""
    '    'declare a new open file dialog 


    '    'a�adir filtros de archivos, este paso es opcional, pero si se observa la captura de pantalla  
    '    'por encima de ella no se ve limpia si la deja fuera, le expliqu� con m�s  
    '    'detalle en mi sitio c�mo agregar / modificar estos valores 
    '    fileDialogBox.Filter = "Formato de texto enriquecido (*.rtf)|*.Rtf|" _
    '         & "Archivos de texto (*.txt)|*.Txt|" _
    '         & "Documentos de Word(*.Doc, *.docx)|*.Doc, *.Docx|" _
    '         & "Archivos de imagen(*.Bmp; *.jpg; *.gif) |*.bmp; *.Jpg, *.Gif|" _
    '         & "Documentos de PowerPoint (*.Pptx; *.ppt)|*.pptx; *.Ppt|" _
    '         & "Documentos de Excel (*.XLS;*.XLSX)|*.XLS;*.XLSX|" _
    '         & "Todos los archivos (*.*)|"
    '    'esto establece el filtro por defecto que hemos creado en la l�nea de arriba, si no lo hace  
    '    'establecer un FilterIndex se usar� por defecto autom�ticamente a 1 
    '    fileDialogBox.FilterIndex = 3
    '    'esta l�nea le dice a la caja de di�logo de archivo de la carpeta en que debe comenzar en la primera         'He escogido a los usuarios de mi carpeta de documentos 
    '    fileDialogBox.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

    '    'Check to see if the user clicked the open button 
    '    If (fileDialogBox.ShowDialog() = DialogResult.OK) Then
    '        strFileName = fileDialogBox.FileName
    '        'Else 
    '        '   MsgBox("You did not select a file!") 
    '    End If

    '    'return the name of the file 
    '    Return strFileName
    'End Function
    'Private Sub btnAdjuntar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdjuntar.Click
    '    btnQuitarAdjunto.Enabled = True
    '    btnAdjuntar.Enabled = False
    '    'declare a string to the filename 
    '    Dim strFileNameAndPath As String = AbrirArchivo()

    '    'check to see if they selected a file or just clicked cancel 
    '    If (strFileNameAndPath = "") Then
    '        'Chastise the user for not selecting a file :) 
    '        MsgBox("Usted no selecciono nig�n archivo")
    '    Else
    '        txtNombreArchivo.Text = System.IO.Path.GetFileName(strFileNameAndPath)
    '        txtUbicacion.Text = System.IO.Path.GetDirectoryName(strFileNameAndPath)
    '        txtExtencionArchivo.Text = System.IO.Path.GetExtension(strFileNameAndPath)
    '        txtCreadoArchivo.Text = System.IO.File.GetCreationTime(strFileNameAndPath)
    '        txtModificoArchivo.Text = System.IO.File.GetLastWriteTime(strFileNameAndPath)
    '        'Begin doing whatever it is you would normally do with the file! 
    '        'MsgBox("You selected this file: " & strFileNameAndPath & vbCrLf & _ 
    '        '   "The filename is: " & System.IO.Path.GetFileName(strFileNameAndPath) & vbCrLf & _ 
    '        '   "Located in: " & System.IO.Path.GetDirectoryName(strFileNameAndPath) & vbCrLf & _ 
    '        '   "It has the following extension: " & System.IO.Path.GetExtension(strFileNameAndPath) & vbCrLf & _ 
    '        '   "The file was created on " & System.IO.File.GetCreationTime(strFileNameAndPath) & vbCrLf & _ 
    '        '   "The file was last written to on " & System.IO.File.GetLastWriteTime(strFileNameAndPath) _ 
    '        ') 
    '    End If

    'End Sub
    'Private Function QuitarAdjunto(ByVal Quitar As String)
    '    fileDialogBox = New OpenFileDialog
    '    txtNombreArchivo.Text = Quitar
    '    txtExtencionArchivo.Text = Quitar
    '    txtCreadoArchivo.Text = Quitar
    '    txtModificoArchivo.Text = Quitar
    '    txtUbicacion.Text = Quitar
    '    Return False
    'End Function
    'Private Sub btnQuitarAdjunto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQuitarAdjunto.Click
    '    Cursor = Cursors.AppStarting
    '    QuitarAdjunto("")
    '    Cursor = Cursors.Arrow
    'End Sub
    'Private Function VaciarCorreo(ByVal vaciar As String)
    '    txtNombre.Text = ""
    '    txtCorreo.Text = ""
    '    txtContrase�a.Text = ""
    '    txtPara.Text = ""
    '    txtCC.Text = ""
    '    txtAsunto.Text = ""
    '    txtMensaje.Text = ""
    '    QuitarAdjunto("")
    '    Return False
    'End Function
    'Private Sub btnEnviarCorreo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnviarCorreo.Click
    '    Cursor = Cursors.AppStarting
    '    If txtCorreo.Text = "" Or txtContrase�a.Text = "" Or txtPara.Text = "" Then
    '        MessageBox.Show("No puede enviar el correo, verifique los datos ingresados", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Else
    '        If txtUbicacion.Text = "" Then
    '            If txtCC.Text = "" Then Enviar_Correo_SinCC() Else Enviar_Correo()
    '        Else
    '            If txtCC.Text = "" Then Enviar_Correo_SinCC() Else Enviar_Correo_Adjunto()
    '        End If
    '        VaciarCorreo("")
    '    End If
    '    Cursor = Cursors.Arrow
    'End Sub
    'Private Sub CerrarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Close()
    'End Sub

    'Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
    '    Process.Start("www.JandC-ec.webs.com")
    'End Sub

    'Private Sub EnviarCorreoToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EnviarCorreoToolStripMenuItem.Click
    '    Cursor = Cursors.AppStarting
    '    Dim res = DialogResult
    '    res = MessageBox.Show("�Desea enviar el correo?", "Enviar Correo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    If res = Windows.Forms.DialogResult.Yes Then
    '        btnEnviarCorreo_Click(btnEnviarCorreo, e)
    '    Else

    '    End If

    '    Cursor = Cursors.Arrow

    'End Sub
    'Private Sub NuevoCorreoToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NuevoCorreoToolStripMenuItem.Click
    '    Cursor = Cursors.AppStarting
    '    Dim res = DialogResult
    '    res = MessageBox.Show("�Desea crear un nuevo correo?", "Nuevo Correo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    If res = Windows.Forms.DialogResult.Yes Then
    '        VaciarCorreo("")
    '    Else

    '    End If

    '    Cursor = Cursors.Arrow
    'End Sub
    'Private Sub VisitarPaginaWebToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles VisitarPaginaWebToolStripMenuItem.Click
    '    MessageBox.Show("Gracias por apoyar nuesro trabajo", "Muchas gracias Atte. Josu� Chavez")
    '    Process.Start("www.JandC-ec.webs.com")
    'End Sub
    'Private Sub LikeEnFacebookToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LikeEnFacebookToolStripMenuItem.Click
    '    MessageBox.Show("Gracias por apoyar nuesro trabajo", "Muchas gracias Atte. Josu� Chavez")
    '    Process.Start("www.facebook.com/JandC.Ecuador")
    'End Sub
    'Private Sub S�guenosEnTwitterToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles S�guenosEnTwitterToolStripMenuItem.Click
    '    MessageBox.Show("Gracias por apoyar nuesro trabajo", "Muchas gracias Atte. Josu� Chavez")
    '    Process.Start("www.twitter.com/JandC_Ecuador")
    'End Sub

    'Private Sub SalirToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SalirToolStripMenuItem.Click
    '    Close()
    'End Sub

    'Private Sub FormEnviarCorreo_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
    '    NotifyIcon1.Visible = False
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Enviar_Correo()
    End Sub
End Class
