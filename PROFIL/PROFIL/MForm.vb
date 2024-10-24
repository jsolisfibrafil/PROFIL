Public Class MForm

    Public Shared Function CierraForms(ByVal articulo As String, ByVal NameForm As String, ByVal Textoboton As String) As Boolean
        If Textoboton = "Crear" Or Textoboton = "&Crear" Then
            Dim i_cierre As Integer
            i_cierre = MessageBox.Show("Está procesando " + articulo + " " + NameForm + vbCrLf + "¿Desea cerrar el formulario?", "FIBRAFIL", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
            If i_cierre = 6 Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

End Class
