<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_anulaCODE
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.txt_codigo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txt_mtvanul = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnConfirma = New System.Windows.Forms.Button
        Me.btn_close = New System.Windows.Forms.Button
        Me.txt_pswaccs = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(29, 71)
        Me.ListBox1.MultiColumn = True
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(547, 329)
        Me.ListBox1.TabIndex = 1
        '
        'txt_codigo
        '
        Me.txt_codigo.BackColor = System.Drawing.SystemColors.Info
        Me.txt_codigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_codigo.Location = New System.Drawing.Point(93, 32)
        Me.txt_codigo.Name = "txt_codigo"
        Me.txt_codigo.Size = New System.Drawing.Size(120, 20)
        Me.txt_codigo.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(29, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "CODIGO :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(218, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(138, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "MOTIVO ANULACION :"
        '
        'txt_mtvanul
        '
        Me.txt_mtvanul.BackColor = System.Drawing.SystemColors.Info
        Me.txt_mtvanul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_mtvanul.Location = New System.Drawing.Point(362, 29)
        Me.txt_mtvanul.Multiline = True
        Me.txt_mtvanul.Name = "txt_mtvanul"
        Me.txt_mtvanul.Size = New System.Drawing.Size(214, 36)
        Me.txt_mtvanul.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(501, 417)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 47)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "PSW ACCS"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'btnConfirma
        '
        Me.btnConfirma.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnConfirma.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnConfirma.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirma.Location = New System.Drawing.Point(34, 417)
        Me.btnConfirma.Name = "btnConfirma"
        Me.btnConfirma.Size = New System.Drawing.Size(72, 47)
        Me.btnConfirma.TabIndex = 5
        Me.btnConfirma.Text = "&Ok"
        Me.btnConfirma.UseVisualStyleBackColor = False
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_close.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_close.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_close.Location = New System.Drawing.Point(116, 417)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(72, 47)
        Me.btn_close.TabIndex = 4
        Me.btn_close.Text = "Cancelar"
        Me.btn_close.UseVisualStyleBackColor = False
        '
        'txt_pswaccs
        '
        Me.txt_pswaccs.BackColor = System.Drawing.SystemColors.Info
        Me.txt_pswaccs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_pswaccs.Location = New System.Drawing.Point(362, 432)
        Me.txt_pswaccs.Name = "txt_pswaccs"
        Me.txt_pswaccs.PasswordChar = Global.Microsoft.VisualBasic.ChrW(63)
        Me.txt_pswaccs.Size = New System.Drawing.Size(120, 20)
        Me.txt_pswaccs.TabIndex = 3
        Me.txt_pswaccs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txt_pswaccs.Visible = False
        '
        'frm_anulaCODE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 490)
        Me.Controls.Add(Me.txt_pswaccs)
        Me.Controls.Add(Me.btnConfirma)
        Me.Controls.Add(Me.btn_close)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_mtvanul)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_codigo)
        Me.Controls.Add(Me.ListBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_anulaCODE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Anulación de Códigos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents txt_codigo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_mtvanul As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Protected WithEvents btnConfirma As System.Windows.Forms.Button
    Protected WithEvents btn_close As System.Windows.Forms.Button
    Friend WithEvents txt_pswaccs As System.Windows.Forms.TextBox
End Class
