<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_upd_date
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
        Me.rb_manana = New System.Windows.Forms.RadioButton
        Me.txt_recordkey = New System.Windows.Forms.TextBox
        Me.dtp_fech = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.rb_noche = New System.Windows.Forms.RadioButton
        Me.lbl_producto = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txt_obs = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'rb_manana
        '
        Me.rb_manana.AutoSize = True
        Me.rb_manana.Checked = True
        Me.rb_manana.Location = New System.Drawing.Point(192, 110)
        Me.rb_manana.Name = "rb_manana"
        Me.rb_manana.Size = New System.Drawing.Size(64, 17)
        Me.rb_manana.TabIndex = 0
        Me.rb_manana.TabStop = True
        Me.rb_manana.Text = "Mañana"
        Me.rb_manana.UseVisualStyleBackColor = True
        '
        'txt_recordkey
        '
        Me.txt_recordkey.Location = New System.Drawing.Point(144, 8)
        Me.txt_recordkey.Name = "txt_recordkey"
        Me.txt_recordkey.Size = New System.Drawing.Size(100, 20)
        Me.txt_recordkey.TabIndex = 1
        '
        'dtp_fech
        '
        Me.dtp_fech.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_fech.Location = New System.Drawing.Point(96, 108)
        Me.dtp_fech.Name = "dtp_fech"
        Me.dtp_fech.Size = New System.Drawing.Size(80, 20)
        Me.dtp_fech.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "KEY - N° Producción"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(16, 224)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "&Actualizar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'rb_noche
        '
        Me.rb_noche.AutoSize = True
        Me.rb_noche.Location = New System.Drawing.Point(272, 110)
        Me.rb_noche.Name = "rb_noche"
        Me.rb_noche.Size = New System.Drawing.Size(57, 17)
        Me.rb_noche.TabIndex = 5
        Me.rb_noche.Text = "Noche"
        Me.rb_noche.UseVisualStyleBackColor = True
        '
        'lbl_producto
        '
        Me.lbl_producto.AutoSize = True
        Me.lbl_producto.Location = New System.Drawing.Point(56, 56)
        Me.lbl_producto.Name = "lbl_producto"
        Me.lbl_producto.Size = New System.Drawing.Size(11, 13)
        Me.lbl_producto.TabIndex = 6
        Me.lbl_producto.Text = "*"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Nueva Fecha"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(320, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Observación :"
        '
        'txt_obs
        '
        Me.txt_obs.Location = New System.Drawing.Point(96, 144)
        Me.txt_obs.Multiline = True
        Me.txt_obs.Name = "txt_obs"
        Me.txt_obs.Size = New System.Drawing.Size(280, 56)
        Me.txt_obs.TabIndex = 10
        '
        'frm_upd_date
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(387, 262)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_obs)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_producto)
        Me.Controls.Add(Me.rb_noche)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtp_fech)
        Me.Controls.Add(Me.txt_recordkey)
        Me.Controls.Add(Me.rb_manana)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_upd_date"
        Me.Text = "Actualización de fecha"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rb_manana As System.Windows.Forms.RadioButton
    Friend WithEvents txt_recordkey As System.Windows.Forms.TextBox
    Friend WithEvents dtp_fech As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents rb_noche As System.Windows.Forms.RadioButton
    Friend WithEvents lbl_producto As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_obs As System.Windows.Forms.TextBox
End Class
