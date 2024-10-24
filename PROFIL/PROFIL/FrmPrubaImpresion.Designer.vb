<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrubaImpresion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.rdb_si = New System.Windows.Forms.RadioButton()
        Me.rdb_no = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'btnImprimir
        '
        Me.btnImprimir.Location = New System.Drawing.Point(161, 99)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(75, 23)
        Me.btnImprimir.TabIndex = 0
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'rdb_si
        '
        Me.rdb_si.AutoSize = True
        Me.rdb_si.Checked = True
        Me.rdb_si.Location = New System.Drawing.Point(149, 57)
        Me.rdb_si.Name = "rdb_si"
        Me.rdb_si.Size = New System.Drawing.Size(34, 17)
        Me.rdb_si.TabIndex = 1
        Me.rdb_si.TabStop = True
        Me.rdb_si.Text = "Si"
        Me.rdb_si.UseVisualStyleBackColor = True
        '
        'rdb_no
        '
        Me.rdb_no.AutoSize = True
        Me.rdb_no.Location = New System.Drawing.Point(220, 57)
        Me.rdb_no.Name = "rdb_no"
        Me.rdb_no.Size = New System.Drawing.Size(39, 17)
        Me.rdb_no.TabIndex = 2
        Me.rdb_no.Text = "No"
        Me.rdb_no.UseVisualStyleBackColor = True
        '
        'FrmPrubaImpresion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 258)
        Me.Controls.Add(Me.rdb_no)
        Me.Controls.Add(Me.rdb_si)
        Me.Controls.Add(Me.btnImprimir)
        Me.Name = "FrmPrubaImpresion"
        Me.Text = "FrmPrubaImpresion"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnImprimir As Button
    Friend WithEvents rdb_si As RadioButton
    Friend WithEvents rdb_no As RadioButton
End Class
