<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PScraps
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
        Me.lbl_idsede = New System.Windows.Forms.Label
        Me.lbl_sede = New System.Windows.Forms.Label
        Me.grp_combos = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lbl_ayud = New System.Windows.Forms.Label
        Me.cmb_ayud = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmb_ope = New System.Windows.Forms.ComboBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.btn_delpesos = New System.Windows.Forms.Button
        Me.lbl_cordia = New System.Windows.Forms.Label
        Me.lbl_item = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.txt_code = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txt_desc = New System.Windows.Forms.TextBox
        Me.btnQuery = New System.Windows.Forms.Button
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.lbl_linS = New System.Windows.Forms.Label
        Me.lbl_idlin = New System.Windows.Forms.Label
        Me.ListBox3 = New System.Windows.Forms.ListBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.lbl_pesotot_s = New System.Windows.Forms.Label
        Me.lbl_countbul_s = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.cmb_maq = New System.Windows.Forms.ComboBox
        Me.grp_combos.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbl_idsede
        '
        Me.lbl_idsede.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_idsede.AutoSize = True
        Me.lbl_idsede.Location = New System.Drawing.Point(688, 485)
        Me.lbl_idsede.Name = "lbl_idsede"
        Me.lbl_idsede.Size = New System.Drawing.Size(19, 13)
        Me.lbl_idsede.TabIndex = 341
        Me.lbl_idsede.Text = "***"
        '
        'lbl_sede
        '
        Me.lbl_sede.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_sede.AutoSize = True
        Me.lbl_sede.Location = New System.Drawing.Point(632, 485)
        Me.lbl_sede.Name = "lbl_sede"
        Me.lbl_sede.Size = New System.Drawing.Size(41, 13)
        Me.lbl_sede.TabIndex = 340
        Me.lbl_sede.Text = "Sede : "
        '
        'grp_combos
        '
        Me.grp_combos.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.grp_combos.Controls.Add(Me.Label7)
        Me.grp_combos.Controls.Add(Me.lbl_ayud)
        Me.grp_combos.Controls.Add(Me.cmb_ayud)
        Me.grp_combos.Controls.Add(Me.Label5)
        Me.grp_combos.Controls.Add(Me.cmb_ope)
        Me.grp_combos.Controls.Add(Me.Button3)
        Me.grp_combos.Location = New System.Drawing.Point(120, 24)
        Me.grp_combos.Name = "grp_combos"
        Me.grp_combos.Size = New System.Drawing.Size(487, 143)
        Me.grp_combos.TabIndex = 332
        Me.grp_combos.TabStop = False
        Me.grp_combos.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Yellow
        Me.Label7.Location = New System.Drawing.Point(11, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(209, 15)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Confirme los siguientes datos..."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbl_ayud
        '
        Me.lbl_ayud.AutoSize = True
        Me.lbl_ayud.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ayud.ForeColor = System.Drawing.Color.Yellow
        Me.lbl_ayud.Location = New System.Drawing.Point(251, 59)
        Me.lbl_ayud.Name = "lbl_ayud"
        Me.lbl_ayud.Size = New System.Drawing.Size(65, 15)
        Me.lbl_ayud.TabIndex = 6
        Me.lbl_ayud.Text = "Ayudante"
        '
        'cmb_ayud
        '
        Me.cmb_ayud.FormattingEnabled = True
        Me.cmb_ayud.Location = New System.Drawing.Point(320, 56)
        Me.cmb_ayud.Name = "cmb_ayud"
        Me.cmb_ayud.Size = New System.Drawing.Size(137, 21)
        Me.cmb_ayud.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Yellow
        Me.Label5.Location = New System.Drawing.Point(24, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 15)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Operario"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmb_ope
        '
        Me.cmb_ope.FormattingEnabled = True
        Me.cmb_ope.Location = New System.Drawing.Point(97, 56)
        Me.cmb_ope.Name = "cmb_ope"
        Me.cmb_ope.Size = New System.Drawing.Size(137, 21)
        Me.cmb_ope.TabIndex = 2
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(23, 100)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "&Confirmar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(312, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 15)
        Me.Label3.TabIndex = 319
        Me.Label3.Text = "Máquina"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.ForestGreen
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(16, 432)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(151, 58)
        Me.Button1.TabIndex = 323
        Me.Button1.Text = "Procesar producción"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btn_delpesos
        '
        Me.btn_delpesos.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_delpesos.BackColor = System.Drawing.Color.Red
        Me.btn_delpesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_delpesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delpesos.ForeColor = System.Drawing.Color.White
        Me.btn_delpesos.Location = New System.Drawing.Point(616, 432)
        Me.btn_delpesos.Name = "btn_delpesos"
        Me.btn_delpesos.Size = New System.Drawing.Size(122, 38)
        Me.btn_delpesos.TabIndex = 324
        Me.btn_delpesos.Text = "Borrar pesos"
        Me.btn_delpesos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_delpesos.UseVisualStyleBackColor = False
        '
        'lbl_cordia
        '
        Me.lbl_cordia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_cordia.AutoSize = True
        Me.lbl_cordia.Location = New System.Drawing.Point(392, 56)
        Me.lbl_cordia.Name = "lbl_cordia"
        Me.lbl_cordia.Size = New System.Drawing.Size(19, 13)
        Me.lbl_cordia.TabIndex = 331
        Me.lbl_cordia.Text = "***"
        '
        'lbl_item
        '
        Me.lbl_item.AutoSize = True
        Me.lbl_item.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_item.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lbl_item.Location = New System.Drawing.Point(197, 171)
        Me.lbl_item.Name = "lbl_item"
        Me.lbl_item.Size = New System.Drawing.Size(0, 29)
        Me.lbl_item.TabIndex = 330
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DarkOrange
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(40, 80)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(122, 25)
        Me.Button2.TabIndex = 321
        Me.Button2.Text = "Añadir a lista >>>"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(24, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 327
        Me.Label9.Text = "Codigo artículo"
        '
        'txt_code
        '
        Me.txt_code.BackColor = System.Drawing.SystemColors.Info
        Me.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_code.Location = New System.Drawing.Point(128, 13)
        Me.txt_code.Name = "txt_code"
        Me.txt_code.ReadOnly = True
        Me.txt_code.Size = New System.Drawing.Size(158, 20)
        Me.txt_code.TabIndex = 317
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(24, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 13)
        Me.Label10.TabIndex = 326
        Me.Label10.Text = "Descripcion artículo"
        '
        'txt_desc
        '
        Me.txt_desc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_desc.BackColor = System.Drawing.SystemColors.Info
        Me.txt_desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_desc.Location = New System.Drawing.Point(128, 44)
        Me.txt_desc.Name = "txt_desc"
        Me.txt_desc.ReadOnly = True
        Me.txt_desc.Size = New System.Drawing.Size(248, 20)
        Me.txt_desc.TabIndex = 320
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.DarkOrange
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuery.ForeColor = System.Drawing.Color.Red
        Me.btnQuery.Location = New System.Drawing.Point(288, 13)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(24, 20)
        Me.btnQuery.TabIndex = 325
        Me.btnQuery.Text = "..."
        Me.btnQuery.UseVisualStyleBackColor = False
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(24, 112)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(224, 259)
        Me.ListBox1.TabIndex = 322
        '
        'lbl_linS
        '
        Me.lbl_linS.AutoSize = True
        Me.lbl_linS.ForeColor = System.Drawing.Color.Red
        Me.lbl_linS.Location = New System.Drawing.Point(223, 260)
        Me.lbl_linS.Name = "lbl_linS"
        Me.lbl_linS.Size = New System.Drawing.Size(15, 13)
        Me.lbl_linS.TabIndex = 338
        Me.lbl_linS.Text = "**"
        '
        'lbl_idlin
        '
        Me.lbl_idlin.AutoSize = True
        Me.lbl_idlin.Location = New System.Drawing.Point(72, 491)
        Me.lbl_idlin.Name = "lbl_idlin"
        Me.lbl_idlin.Size = New System.Drawing.Size(15, 13)
        Me.lbl_idlin.TabIndex = 337
        Me.lbl_idlin.Text = "**"
        '
        'ListBox3
        '
        Me.ListBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 16
        Me.ListBox3.Location = New System.Drawing.Point(256, 112)
        Me.ListBox3.MultiColumn = True
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(424, 260)
        Me.ListBox3.TabIndex = 306
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(256, 96)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 13)
        Me.Label8.TabIndex = 304
        Me.Label8.Text = "Registro de pesos scrap"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(672, 91)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 16)
        Me.Label14.TabIndex = 308
        Me.Label14.Text = "Cant. bultos :"
        '
        'lbl_pesotot_s
        '
        Me.lbl_pesotot_s.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_pesotot_s.AutoSize = True
        Me.lbl_pesotot_s.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pesotot_s.Location = New System.Drawing.Point(507, 91)
        Me.lbl_pesotot_s.Name = "lbl_pesotot_s"
        Me.lbl_pesotot_s.Size = New System.Drawing.Size(13, 16)
        Me.lbl_pesotot_s.TabIndex = 311
        Me.lbl_pesotot_s.Text = "*"
        '
        'lbl_countbul_s
        '
        Me.lbl_countbul_s.AutoSize = True
        Me.lbl_countbul_s.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_countbul_s.Location = New System.Drawing.Point(769, 91)
        Me.lbl_countbul_s.Name = "lbl_countbul_s"
        Me.lbl_countbul_s.Size = New System.Drawing.Size(13, 16)
        Me.lbl_countbul_s.TabIndex = 309
        Me.lbl_countbul_s.Text = "*"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(433, 91)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 16)
        Me.Label12.TabIndex = 310
        Me.Label12.Text = "Peso total :"
        '
        'cmb_maq
        '
        Me.cmb_maq.FormattingEnabled = True
        Me.cmb_maq.Location = New System.Drawing.Point(376, 13)
        Me.cmb_maq.Name = "cmb_maq"
        Me.cmb_maq.Size = New System.Drawing.Size(136, 21)
        Me.cmb_maq.TabIndex = 318
        '
        'frm_PScraps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(807, 513)
        Me.Controls.Add(Me.grp_combos)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.lbl_idsede)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lbl_sede)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lbl_pesotot_s)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_countbul_s)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cmb_maq)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btn_delpesos)
        Me.Controls.Add(Me.lbl_cordia)
        Me.Controls.Add(Me.lbl_item)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_code)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_desc)
        Me.Controls.Add(Me.btnQuery)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.lbl_linS)
        Me.Controls.Add(Me.lbl_idlin)
        Me.Name = "frm_PScraps"
        Me.Text = "frm_PScraps"
        Me.grp_combos.ResumeLayout(False)
        Me.grp_combos.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lbl_idsede As System.Windows.Forms.Label
    Protected WithEvents lbl_sede As System.Windows.Forms.Label
    Friend WithEvents grp_combos As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbl_ayud As System.Windows.Forms.Label
    Friend WithEvents cmb_ayud As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_ope As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btn_delpesos As System.Windows.Forms.Button
    Protected WithEvents lbl_cordia As System.Windows.Forms.Label
    Protected WithEvents lbl_item As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents txt_code As System.Windows.Forms.TextBox
    Protected WithEvents Label10 As System.Windows.Forms.Label
    Protected WithEvents txt_desc As System.Windows.Forms.TextBox
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Protected WithEvents lbl_linS As System.Windows.Forms.Label
    Protected WithEvents lbl_idlin As System.Windows.Forms.Label
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Protected WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents Label14 As System.Windows.Forms.Label
    Protected WithEvents lbl_pesotot_s As System.Windows.Forms.Label
    Protected WithEvents lbl_countbul_s As System.Windows.Forms.Label
    Protected WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmb_maq As System.Windows.Forms.ComboBox
End Class
