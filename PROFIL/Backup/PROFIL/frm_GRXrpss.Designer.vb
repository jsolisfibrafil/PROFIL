<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GRXrpss
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
        Me.lb_codebars = New System.Windows.Forms.ListBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.dt_fech = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.txt_nOV = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt_client = New System.Windows.Forms.TextBox
        Me.lbl_nBulto = New System.Windows.Forms.Label
        Me.txt_codebar = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lb_codebars
        '
        Me.lb_codebars.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lb_codebars.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_codebars.FormattingEnabled = True
        Me.lb_codebars.ItemHeight = 16
        Me.lb_codebars.Location = New System.Drawing.Point(8, 96)
        Me.lb_codebars.MultiColumn = True
        Me.lb_codebars.Name = "lb_codebars"
        Me.lb_codebars.Size = New System.Drawing.Size(808, 404)
        Me.lb_codebars.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(8, 512)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 48)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "&Cargar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dt_fech
        '
        Me.dt_fech.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt_fech.Location = New System.Drawing.Point(328, 16)
        Me.dt_fech.Name = "dt_fech"
        Me.dt_fech.Size = New System.Drawing.Size(88, 20)
        Me.dt_fech.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "N° Orden de venta :"
        '
        'txt_nOV
        '
        Me.txt_nOV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_nOV.Location = New System.Drawing.Point(128, 16)
        Me.txt_nOV.MaxLength = 6
        Me.txt_nOV.Name = "txt_nOV"
        Me.txt_nOV.Size = New System.Drawing.Size(100, 20)
        Me.txt_nOV.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(256, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Fecha O/V :"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(648, 512)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Cantidad bultos :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Cliente :"
        '
        'txt_client
        '
        Me.txt_client.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txt_client.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_client.Location = New System.Drawing.Point(128, 48)
        Me.txt_client.Name = "txt_client"
        Me.txt_client.ReadOnly = True
        Me.txt_client.Size = New System.Drawing.Size(288, 20)
        Me.txt_client.TabIndex = 2
        '
        'lbl_nBulto
        '
        Me.lbl_nBulto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_nBulto.AutoSize = True
        Me.lbl_nBulto.Location = New System.Drawing.Point(744, 512)
        Me.lbl_nBulto.Name = "lbl_nBulto"
        Me.lbl_nBulto.Size = New System.Drawing.Size(15, 13)
        Me.lbl_nBulto.TabIndex = 15
        Me.lbl_nBulto.Text = "**"
        Me.lbl_nBulto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_codebar
        '
        Me.txt_codebar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txt_codebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_codebar.Font = New System.Drawing.Font("Arial Narrow", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_codebar.Location = New System.Drawing.Point(512, 32)
        Me.txt_codebar.MaxLength = 20
        Me.txt_codebar.Name = "txt_codebar"
        Me.txt_codebar.Size = New System.Drawing.Size(272, 41)
        Me.txt_codebar.TabIndex = 3
        Me.txt_codebar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(584, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(122, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "CODIGO DE BARRA"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(96, 512)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 48)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "&Actualizar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frm_GRXrpss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 566)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_codebar)
        Me.Controls.Add(Me.lbl_nBulto)
        Me.Controls.Add(Me.txt_client)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_nOV)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dt_fech)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lb_codebars)
        Me.Name = "frm_GRXrpss"
        Me.Text = "Guía Remisión Simplificada"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lb_codebars As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dt_fech As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_nOV As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_client As System.Windows.Forms.TextBox
    Friend WithEvents lbl_nBulto As System.Windows.Forms.Label
    Friend WithEvents txt_codebar As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
