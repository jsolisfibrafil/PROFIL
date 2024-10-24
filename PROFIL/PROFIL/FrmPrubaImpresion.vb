Imports System.IO
Imports System.Web.UI.WebControls

Public Class FrmPrubaImpresion
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Imprime_Codebar("BARDODE: PRUEBA", 1500)
    End Sub

    Sub Imprime_Codebar(ByVal Codebar As String, ByVal Peso As Decimal) '06-10-14
        Try
            ''RUTA DEL ARCHIVO DE ETIQUETA 
            Dim RUTA As String

            If rdb_no.Checked = True Then
                RUTA = Application.StartupPath & "\" + "eti_SPeso.prn" '
            Else
                RUTA = Application.StartupPath & "\" + "eti_CPeso.prn" '
            End If

            ''LECTURA DEL ARCHIVO PARA GUARDARLO EN UN STRING
            Dim arc1 As New StreamReader(RUTA)
            Dim etiqueta As String = arc1.ReadToEnd()

            ''REEMPLAZA LOS VALORES DE LA ETIQUETA
            etiqueta = etiqueta.Replace("[NPD]", "PM")
            etiqueta = etiqueta.Replace("[FP]", Date.Today.ToShortDateString)
            etiqueta = etiqueta.Replace("[IDITEM]", "PRODUCTO: SOLO ES UNA PRUEBA")
            etiqueta = etiqueta.Replace("[DESPRO]", "PESO - PRUEBA")

            etiqueta = etiqueta.Replace("[codbar]", Codebar)
            ''SIGUINTE LINEA SOLO PARA EL AREA DE CABOS 
            If rdb_si.Checked = True Then
                etiqueta = etiqueta.Replace("[PESO]", Peso.ToString())
            End If
            etiqueta = etiqueta.Replace("\[""]", "''")

            ''IMPRIME USANDO DLL DE IMPRESION OPCION SendStringToPrinter()
            clsprinter.Class1.SendStringToPrinter("PROFIL", etiqueta)
            ' ''  MessageBox.Show("Etiqueta Impresa", "FIBRAFIL", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ''  End If

        Catch ex As Exception
            ' MessageBox.Show(ex.StackTrace)
        End Try
    End Sub
End Class