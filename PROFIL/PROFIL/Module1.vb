
Imports System.IO
Imports System.Xml
Imports System.Reflection
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Collections.Specialized

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Security

Module Module1

    Public OCN As New SqlConnection
    Private MiRdR As New RecursoDeRed
    ' Private mCfg As Profil.info.Util.ConfigXml
    Private CodigoElemento As String
    Private TextoElemento As String
    Public s_Nserver As String

    Sub main()
        'Actualiza la nueva version del programa copia y el batch se encarga del resto
        'Conectar()


        Try
            Dim info_ORIG, info_UPD As System.IO.FileInfo
            info_ORIG = My.Computer.FileSystem.GetFileInfo(Application.StartupPath & "\PROFIL.EXE")
            'System.Diagnostics.Process.Start("\\serverts\DATAFIBRA\Fibrafil\profil", "Administrador", "inka$12", "fibrafil")
            ' string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";            Process.Start(path + "HelloWorld.exe", uname, password, domain);

            'info_UPD = My.Computer.FileSystem.GetFileInfo("\\SRVFIBLUR02\Fibrafil\Publico\PROFIL\PROFIL.EXE")
            'If info_UPD.LastWriteTime > info_ORIG.LastWriteTime Then
            '    Dim X As Integer
            '    X = MessageBox.Show("Existe una versión recientemente actualizada, ¿Desea actualizar el sistema?", "FIBRAFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            '    If X = 6 Then

            '        Dim Source As String = info_UPD.ToString
            '        Dim Destination As String

            '        Destination = Application.StartupPath & "\PROFIL2.exe" '
            '        File.Copy(Source, Destination, True)

            '        Dim pro As New Process
            '        pro.StartInfo.WorkingDirectory = Application.StartupPath
            '        pro.StartInfo.FileName = "update.bat"
            '        pro.Start()
            '    End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Dim objMyCOmponent As Conection.Class1
        objMyCOmponent = New Conection.Class1

        ' The following code shows how to read the configuration file.
        Dim codeBase As String = [Assembly].GetExecutingAssembly().CodeBase
        Dim codeBaseDir As String = Path.GetDirectoryName(codeBase)
        Dim configFilename As String = Path.Combine(codeBaseDir, "conect.config")

        ' Load the configuration file in XML document.
        Dim doc As XmlDocument = New XmlDocument()
        doc.Load(configFilename)

        ' Get the databaseconnection node.
        Dim xNode As XmlNode = doc.GetElementsByTagName("databaseconnection").Item(0)

        ' Use the NameValueSectionHandler class to get the actual databaseconnection setting.
        Dim csh As IConfigurationSectionHandler = New NameValueSectionHandler()
        Dim nvc As NameValueCollection = CType(csh.Create(Nothing, Nothing, xNode), NameValueCollection)

        ' Call the method in Component.
        Dim blnStatus As Boolean
        blnStatus = objMyCOmponent.OpenConnection(nvc("Server"), nvc("Database"), nvc("user id"), nvc("pwd"))
        OCN.ConnectionString = ("Server=" + nvc("Server") + ";Database=" + nvc("Database") + ";User Id=" + nvc("user id") + ";PWD=" + nvc("PWD"))
        s_Nserver = nvc("Server")
        ' Display results.
        If blnStatus = True Then
            Dim FrmIni As New SplashScreen1
            Application.Run(FrmIni)
            '  mCfg = New Profil.info.Util.ConfigXml(System.IO.Directory.GetCurrentDirectory() & "\prueba.cfg", True)
        Else
            MsgBox("Database connection was not opened.", MsgBoxStyle.Critical, "FIBRAFIL")
        End If
    End Sub


    Public Class ElementoCombo
        Public Sub New(ByVal NuevoCodigo As String, ByVal NuevoTexto As String)
            CodigoElemento = NuevoCodigo
            TextoElemento = NuevoTexto
        End Sub

        Public ReadOnly Property Codigo() As String
            Get
                Return CodigoElemento
            End Get
        End Property

        Public ReadOnly Property Texto() As String
            Get
                Return TextoElemento
            End Get
        End Property
    End Class

    Private Sub Conectar()
        'Definir el tipo de conexión
        'Segun documentacion del API
        MiRdR.dwScope = Recurso.CONNECTED
        MiRdR.dwType = Recurso_Tipo.DISK
        MiRdR.dwDisplayType = Recurso_TipoVista.SHARE
        MiRdR.dwUsage = Recurso_TipoUso.CONNECTABLE

        'Definir letra de unidad y nombre del recurso
        MiRdR.lpLocalName = "Z:"
        MiRdR.lpRemoteName = "\\serverts\DATAFIBRA\Fibrafil\profil"
        MiRdR.lpComment = "** Data Profil**"
        MiRdR.lpProvider = ""

        'Si las credenciales son diferentes a las actuales
        'Sustituir por las adecuadas
        Dim Usuario = "Administrador" 'vbNullString  'Nombre de usuario
        Dim Clave = "inka$12" 'vbNullString    'Clave de acceso

        'Llamar a la funcion AñadirUnidad para WNetAddConnection2A 
        'CONNECT_UPDATE_PROFILE = &H1
        MessageBox.Show( _
             CodigoError( _
              AñadirUnidad(MiRdR, Clave, Usuario, &H1)), _
              "Conectando..." + MiRdR.lpLocalName + " a " + MiRdR.lpRemoteName)
    End Sub

    Declare Function AñadirUnidad Lib "mpr.dll" Alias "WNetAddConnection2A" (ByRef lpRecursoDeRed As RecursoDeRed, ByVal lpPassword As String, ByVal lpUserName As String, ByVal dwFlags As Long) As Integer

    '
    ' Estructura para llamar a la funcion de añadir
    '
    Structure RecursoDeRed
        Public dwScope As Integer
        Public dwType As Integer
        Public dwDisplayType As Integer
        Public dwUsage As Integer
        Public lpLocalName As String
        Public lpRemoteName As String
        Public lpComment As String
        Public lpProvider As String
    End Structure
    '
    ' Definir el recurso
    '
    Enum Recurso
        CONNECTED = &H1
        REMEMBERED = &H3
        GLOBALNET = &H2
    End Enum
    '
    ' Definir el tipo
    '
    Enum Recurso_Tipo
        DISK = &H1
        PRINT = &H2
        ANY = &H0
    End Enum
    '
    ' Definir la vista
    '
    Enum Recurso_TipoVista
        DOMAIN = &H1
        GENERIC = &H0
        SERVER = &H2
        SHARE = &H3
    End Enum

    ' Definir el uso
    '
    Enum Recurso_TipoUso
        CONNECTABLE = &H1
        CONTAINER = &H2
    End Enum
    '
    ' Asignar codigos de retorno errores
    '
    Function CodigoError(ByVal Codigo As Integer) As String
        Select Case Codigo
            Case 0 : Return "La unidad se agrego correctamente."
            Case 5 : Return "Acceso denegado!"
            Case 66 : Return "El tipo de dispositivo, no es correcto."
            Case 67 : Return "El nombre de red, no es correcto."
            Case 85 : Return "La unidad ya esta asignada!"
            Case 86 : Return "La clave de acceso no es valida!"
            Case 170 : Return "Ocupado!"
            Case 1200 : Return "Dispositivo Incorrecto."
            Case 1202 : Return "El dispositivo se encuentra registrado"
            Case 1203 : Return "Sin red o ruta incorrecta!"
            Case 1204 : Return "El suministrador es Incorrecto."
            Case 1205 : Return "No se puede abrir el perfil"
            Case 1206 : Return "El perfil es Incorrecto."
            Case 1208 : Return "Error extendido"
            Case 1223 : Return "Cancelado!"
            Case Else
                Return "ERROR, Codigo desconocido:" + Codigo.ToString
        End Select
    End Function

    'Public Class Example
    Public Sub SMain()
        ' Instantiate the secure string.
        Dim securePwd As New SecureString()
        Dim key As ConsoleKeyInfo

        'Console.Write("Enter password: ")
        Do
            key = Console.ReadKey(True)

            ' Ignore any key out of range
            If CInt(key.Key) >= 65 And CInt(key.Key <= 90) Then
                ' Append the character to the password.
                securePwd.AppendChar(key.KeyChar)
                Console.Write("*")
            End If
            ' Exit if Enter key is pressed.
        Loop While key.Key <> ConsoleKey.Enter
        Console.WriteLine()

        Try
            Process.Start("Notepad.exe", "MyUser", securePwd, "MYDOMAIN")
        Catch e As Win32Exception
            Console.WriteLine(e.Message)
        End Try
    End Sub
    'End Class


    Public Sub Limpiar_TextBox(ByVal formulario As Form)
        'Recorremos todos los controles del formulario que enviamos    
        For Each control As Control In formulario.Controls
            'Filtramos solo aquellos de tipo TextBox   
            If TypeOf control Is TextBox Then
                control.Text = "" ' eliminar el texto   
                control.DataBindings.Clear()
            End If
        Next
    End Sub

    Public Sub Limpiar_TextBox_Tabs(ByVal formulario As Form, ByVal Control As TabControl)
        Dim Pag As TabPage
        Dim Cajas As New Control

        For Each Pag In Control.TabPages
            For Each Cajas In Pag.Controls
                If TypeOf Cajas Is TextBox Then Cajas.DataBindings.Clear()
                If TypeOf Cajas Is TextBox Then Cajas.Text = ""
            Next
        Next
    End Sub

End Module
