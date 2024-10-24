<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_reimpresion
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txt_codebar = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lbl_peso = New System.Windows.Forms.Label
        Me.lbl_dscitm = New System.Windows.Forms.Label
        Me.lbl_iditm = New System.Windows.Forms.Label
        Me.lbl_codebar = New System.Windows.Forms.Label
        Me.lbl_npd = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lbl_fp = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btn_print = New System.Windows.Forms.Button
        Me.rdb_no = New System.Windows.Forms.RadioButton
        Me.rdb_si = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(179, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Ingrese codigo a re-etiquetar :"
        '
        'txt_codebar
        '
        Me.txt_codebar.Location = New System.Drawing.Point(223, 17)
        Me.txt_codebar.Name = "txt_codebar"
        Me.txt_codebar.Size = New System.Drawing.Size(202, 20)
        Me.txt_codebar.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lbl_peso)
        Me.GroupBox1.Controls.Add(Me.lbl_dscitm)
        Me.GroupBox1.Controls.Add(Me.lbl_iditm)
        Me.GroupBox1.Controls.Add(Me.lbl_codebar)
        Me.GroupBox1.Controls.Add(Me.lbl_npd)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lbl_fp)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 59)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(412, 243)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(328, 161)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 24)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Kg"
        '
        'lbl_peso
        '
        Me.lbl_peso.AutoSize = True
        Me.lbl_peso.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_peso.Location = New System.Drawing.Point(251, 161)
        Me.lbl_peso.Name = "lbl_peso"
        Me.lbl_peso.Size = New System.Drawing.Size(0, 24)
        Me.lbl_peso.TabIndex = 9
        '
        'lbl_dscitm
        '
        Me.lbl_dscitm.AutoSize = True
        Me.lbl_dscitm.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dscitm.Location = New System.Drawing.Point(105, 207)
        Me.lbl_dscitm.Name = "lbl_dscitm"
        Me.lbl_dscitm.Size = New System.Drawing.Size(0, 12)
        Me.lbl_dscitm.TabIndex = 8
        '
        'lbl_iditm
        '
        Me.lbl_iditm.AutoSize = True
        Me.lbl_iditm.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_iditm.Location = New System.Drawing.Point(105, 184)
        Me.lbl_iditm.Name = "lbl_iditm"
        Me.lbl_iditm.Size = New System.Drawing.Size(0, 12)
        Me.lbl_iditm.TabIndex = 7
        '
        'lbl_codebar
        '
        Me.lbl_codebar.AutoSize = True
        Me.lbl_codebar.Font = New System.Drawing.Font("IDAutomationHC39M", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_codebar.Location = New System.Drawing.Point(3, 68)
        Me.lbl_codebar.Name = "lbl_codebar"
        Me.lbl_codebar.Size = New System.Drawing.Size(127, 73)
        Me.lbl_codebar.TabIndex = 6
        Me.lbl_codebar.Text = "*ABC*"
        Me.lbl_codebar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_npd
        '
        Me.lbl_npd.AutoSize = True
        Me.lbl_npd.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_npd.Location = New System.Drawing.Point(68, 16)
        Me.lbl_npd.Name = "lbl_npd"
        Me.lbl_npd.Size = New System.Drawing.Size(0, 12)
        Me.lbl_npd.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(28, 207)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "DSC ITEM:"
        '
        'lbl_fp
        '
        Me.lbl_fp.AutoSize = True
        Me.lbl_fp.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_fp.Location = New System.Drawing.Point(343, 16)
        Me.lbl_fp.Name = "lbl_fp"
        Me.lbl_fp.Size = New System.Drawing.Size(0, 12)
        Me.lbl_fp.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 184)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 12)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "ID ITEM:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(309, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "FP:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "NPD:"
        '
        'btn_print
        '
        Me.btn_print.Image = Global.PROFIL.My.Resources.Resources.printer
        Me.btn_print.Location = New System.Drawing.Point(17, 321)
        Me.btn_print.Name = "btn_print"
        Me.btn_print.Size = New System.Drawing.Size(61, 40)
        Me.btn_print.TabIndex = 1
        Me.btn_print.UseVisualStyleBackColor = True
        '
        'rdb_no
        '
        Me.rdb_no.AutoSize = True
        Me.rdb_no.Checked = True
        Me.rdb_no.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdb_no.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdb_no.Location = New System.Drawing.Point(97, 25)
        Me.rdb_no.Name = "rdb_no"
        Me.rdb_no.Size = New System.Drawing.Size(41, 17)
        Me.rdb_no.TabIndex = 4
        Me.rdb_no.TabStop = True
        Me.rdb_no.Text = "NO"
        Me.rdb_no.UseVisualStyleBackColor = True
        '
        'rdb_si
        '
        Me.rdb_si.AutoSize = True
        Me.rdb_si.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdb_si.Location = New System.Drawing.Point(29, 25)
        Me.rdb_si.Name = "rdb_si"
        Me.rdb_si.Size = New System.Drawing.Size(37, 17)
        Me.rdb_si.TabIndex = 5
        Me.rdb_si.TabStop = True
        Me.rdb_si.Text = "SI"
        Me.rdb_si.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rdb_no)
        Me.GroupBox2.Controls.Add(Me.rdb_si)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Red
        Me.GroupBox2.Location = New System.Drawing.Point(259, 308)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(166, 53)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Incluir peso"
        '
        'frm_reimpresion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 373)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txt_codebar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_print)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frm_reimpresion"
        Me.Text = "Re - etiquetado"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_print As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_codebar As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbl_fp As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_dscitm As System.Windows.Forms.Label
    Friend WithEvents lbl_iditm As System.Windows.Forms.Label
    Friend WithEvents lbl_codebar As System.Windows.Forms.Label
    Friend WithEvents lbl_npd As System.Windows.Forms.Label
    Friend WithEvents lbl_peso As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rdb_no As System.Windows.Forms.RadioButton
    Friend WithEvents rdb_si As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
