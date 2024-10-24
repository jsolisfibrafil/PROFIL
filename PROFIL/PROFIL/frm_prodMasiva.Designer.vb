<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_prodMasiva
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
        Me.components = New System.ComponentModel.Container
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btnQuery = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.txt_code = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txt_desc = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbl_count = New System.Windows.Forms.Label
        Me.lbl_item = New System.Windows.Forms.Label
        Me.lbl_cordia = New System.Windows.Forms.Label
        Me.sppuerto = New System.IO.Ports.SerialPort(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbl_countbul = New System.Windows.Forms.Label
        Me.lbl_pesotot = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btn_delpesos = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.grp_combos = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lbl_ayud = New System.Windows.Forms.Label
        Me.cmb_ayud = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmb_ope = New System.Windows.Forms.ComboBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmb_maq = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.rb_pofic = New System.Windows.Forms.RadioButton
        Me.rb_scarp = New System.Windows.Forms.RadioButton
        Me.lbl_pesotot_s = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lbl_countbul_s = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox
        Me.lbl_idlin = New System.Windows.Forms.Label
        Me.lbl_linS = New System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.ListBox3 = New System.Windows.Forms.ListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdb_no = New System.Windows.Forms.RadioButton
        Me.rdb_si = New System.Windows.Forms.RadioButton
        Me.lbl_sede = New System.Windows.Forms.Label
        Me.lbl_idsede = New System.Windows.Forms.Label
        Me.grp_combos.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(12, 123)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(370, 349)
        Me.ListBox1.TabIndex = 3
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.DarkOrange
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuery.ForeColor = System.Drawing.Color.Red
        Me.btnQuery.Location = New System.Drawing.Point(276, 9)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(24, 20)
        Me.btnQuery.TabIndex = 282
        Me.btnQuery.Text = "..."
        Me.btnQuery.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 284
        Me.Label9.Text = "Codigo artículo"
        '
        'txt_code
        '
        Me.txt_code.BackColor = System.Drawing.SystemColors.Info
        Me.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_code.Location = New System.Drawing.Point(117, 9)
        Me.txt_code.Name = "txt_code"
        Me.txt_code.ReadOnly = True
        Me.txt_code.Size = New System.Drawing.Size(158, 20)
        Me.txt_code.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 13)
        Me.Label10.TabIndex = 283
        Me.Label10.Text = "Descripcion artículo"
        '
        'txt_desc
        '
        Me.txt_desc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_desc.BackColor = System.Drawing.SystemColors.Info
        Me.txt_desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_desc.Location = New System.Drawing.Point(117, 36)
        Me.txt_desc.Name = "txt_desc"
        Me.txt_desc.ReadOnly = True
        Me.txt_desc.Size = New System.Drawing.Size(308, 20)
        Me.txt_desc.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DarkOrange
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(12, 81)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(122, 25)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Añadir a lista >>>"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 486)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 16)
        Me.Label1.TabIndex = 288
        Me.Label1.Text = "Artículos por producir:"
        '
        'lbl_count
        '
        Me.lbl_count.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_count.AutoSize = True
        Me.lbl_count.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_count.Location = New System.Drawing.Point(150, 486)
        Me.lbl_count.Name = "lbl_count"
        Me.lbl_count.Size = New System.Drawing.Size(13, 16)
        Me.lbl_count.TabIndex = 289
        Me.lbl_count.Text = "*"
        '
        'lbl_item
        '
        Me.lbl_item.AutoSize = True
        Me.lbl_item.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_item.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lbl_item.Location = New System.Drawing.Point(174, 75)
        Me.lbl_item.Name = "lbl_item"
        Me.lbl_item.Size = New System.Drawing.Size(0, 29)
        Me.lbl_item.TabIndex = 290
        '
        'lbl_cordia
        '
        Me.lbl_cordia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_cordia.AutoSize = True
        Me.lbl_cordia.Location = New System.Drawing.Point(967, 13)
        Me.lbl_cordia.Name = "lbl_cordia"
        Me.lbl_cordia.Size = New System.Drawing.Size(19, 13)
        Me.lbl_cordia.TabIndex = 291
        Me.lbl_cordia.Text = "***"
        '
        'sppuerto
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(173, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 16)
        Me.Label2.TabIndex = 293
        Me.Label2.Text = "Cant. bultos :"
        '
        'lbl_countbul
        '
        Me.lbl_countbul.AutoSize = True
        Me.lbl_countbul.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_countbul.Location = New System.Drawing.Point(257, 11)
        Me.lbl_countbul.Name = "lbl_countbul"
        Me.lbl_countbul.Size = New System.Drawing.Size(13, 16)
        Me.lbl_countbul.TabIndex = 294
        Me.lbl_countbul.Text = "*"
        '
        'lbl_pesotot
        '
        Me.lbl_pesotot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_pesotot.AutoSize = True
        Me.lbl_pesotot.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pesotot.Location = New System.Drawing.Point(498, 11)
        Me.lbl_pesotot.Name = "lbl_pesotot"
        Me.lbl_pesotot.Size = New System.Drawing.Size(13, 16)
        Me.lbl_pesotot.TabIndex = 296
        Me.lbl_pesotot.Text = "*"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(424, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 16)
        Me.Label4.TabIndex = 295
        Me.Label4.Text = "Peso total :"
        '
        'btn_delpesos
        '
        Me.btn_delpesos.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_delpesos.BackColor = System.Drawing.Color.Red
        Me.btn_delpesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_delpesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delpesos.ForeColor = System.Drawing.Color.White
        Me.btn_delpesos.Location = New System.Drawing.Point(872, 507)
        Me.btn_delpesos.Name = "btn_delpesos"
        Me.btn_delpesos.Size = New System.Drawing.Size(122, 38)
        Me.btn_delpesos.TabIndex = 6
        Me.btn_delpesos.Text = "Borrar pesos"
        Me.btn_delpesos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_delpesos.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.ForestGreen
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(15, 520)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(151, 58)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Procesar producción"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button1.UseVisualStyleBackColor = False
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
        Me.grp_combos.Location = New System.Drawing.Point(280, 192)
        Me.grp_combos.Name = "grp_combos"
        Me.grp_combos.Size = New System.Drawing.Size(487, 143)
        Me.grp_combos.TabIndex = 299
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
        Me.Label3.Location = New System.Drawing.Point(308, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Máquina"
        '
        'cmb_maq
        '
        Me.cmb_maq.FormattingEnabled = True
        Me.cmb_maq.Location = New System.Drawing.Point(376, 8)
        Me.cmb_maq.Name = "cmb_maq"
        Me.cmb_maq.Size = New System.Drawing.Size(136, 21)
        Me.cmb_maq.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(23, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 13)
        Me.Label6.TabIndex = 303
        Me.Label6.Text = "Registro de pesos validos"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(23, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 13)
        Me.Label8.TabIndex = 304
        Me.Label8.Text = "Registro de pesos scrap"
        '
        'rb_pofic
        '
        Me.rb_pofic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb_pofic.AutoSize = True
        Me.rb_pofic.Checked = True
        Me.rb_pofic.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_pofic.ForeColor = System.Drawing.Color.Blue
        Me.rb_pofic.Location = New System.Drawing.Point(457, 36)
        Me.rb_pofic.Name = "rb_pofic"
        Me.rb_pofic.Size = New System.Drawing.Size(335, 24)
        Me.rb_pofic.TabIndex = 306
        Me.rb_pofic.TabStop = True
        Me.rb_pofic.Text = "PESANDO PRODUCTO EN PROCESO"
        Me.rb_pofic.UseVisualStyleBackColor = True
        '
        'rb_scarp
        '
        Me.rb_scarp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb_scarp.AutoSize = True
        Me.rb_scarp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_scarp.ForeColor = System.Drawing.Color.Red
        Me.rb_scarp.Location = New System.Drawing.Point(798, 36)
        Me.rb_scarp.Name = "rb_scarp"
        Me.rb_scarp.Size = New System.Drawing.Size(177, 24)
        Me.rb_scarp.TabIndex = 307
        Me.rb_scarp.Text = "PESANDO SCRAP"
        Me.rb_scarp.UseVisualStyleBackColor = True
        '
        'lbl_pesotot_s
        '
        Me.lbl_pesotot_s.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_pesotot_s.AutoSize = True
        Me.lbl_pesotot_s.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pesotot_s.Location = New System.Drawing.Point(512, 5)
        Me.lbl_pesotot_s.Name = "lbl_pesotot_s"
        Me.lbl_pesotot_s.Size = New System.Drawing.Size(13, 16)
        Me.lbl_pesotot_s.TabIndex = 311
        Me.lbl_pesotot_s.Text = "*"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(438, 5)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 16)
        Me.Label12.TabIndex = 310
        Me.Label12.Text = "Peso total :"
        '
        'lbl_countbul_s
        '
        Me.lbl_countbul_s.AutoSize = True
        Me.lbl_countbul_s.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_countbul_s.Location = New System.Drawing.Point(279, 5)
        Me.lbl_countbul_s.Name = "lbl_countbul_s"
        Me.lbl_countbul_s.Size = New System.Drawing.Size(13, 16)
        Me.lbl_countbul_s.TabIndex = 309
        Me.lbl_countbul_s.Text = "*"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(182, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 16)
        Me.Label14.TabIndex = 308
        Me.Label14.Text = "Cant. bultos :"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(342, 565)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(516, 19)
        Me.CheckedListBox1.TabIndex = 302
        Me.CheckedListBox1.Visible = False
        '
        'lbl_idlin
        '
        Me.lbl_idlin.AutoSize = True
        Me.lbl_idlin.Location = New System.Drawing.Point(57, 164)
        Me.lbl_idlin.Name = "lbl_idlin"
        Me.lbl_idlin.Size = New System.Drawing.Size(15, 13)
        Me.lbl_idlin.TabIndex = 312
        Me.lbl_idlin.Text = "**"
        '
        'lbl_linS
        '
        Me.lbl_linS.AutoSize = True
        Me.lbl_linS.ForeColor = System.Drawing.Color.Red
        Me.lbl_linS.Location = New System.Drawing.Point(200, 164)
        Me.lbl_linS.Name = "lbl_linS"
        Me.lbl_linS.Size = New System.Drawing.Size(15, 13)
        Me.lbl_linS.TabIndex = 313
        Me.lbl_linS.Text = "**"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Location = New System.Drawing.Point(388, 123)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_pesotot)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_countbul)
        Me.SplitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListBox3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label14)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbl_pesotot_s)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbl_countbul_s)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label12)
        Me.SplitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SplitContainer1.Size = New System.Drawing.Size(626, 349)
        Me.SplitContainer1.SplitterDistance = 238
        Me.SplitContainer1.TabIndex = 314
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 16
        Me.ListBox2.Location = New System.Drawing.Point(0, 34)
        Me.ListBox2.MultiColumn = True
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(624, 212)
        Me.ListBox2.TabIndex = 5
        '
        'ListBox3
        '
        Me.ListBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 16
        Me.ListBox3.Location = New System.Drawing.Point(0, 26)
        Me.ListBox3.MultiColumn = True
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(624, 84)
        Me.ListBox3.TabIndex = 306
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupBox2.Controls.Add(Me.rdb_no)
        Me.GroupBox2.Controls.Add(Me.rdb_si)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Red
        Me.GroupBox2.Location = New System.Drawing.Point(503, 492)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(166, 53)
        Me.GroupBox2.TabIndex = 304
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Incluir peso"
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
        Me.rdb_si.Size = New System.Drawing.Size(35, 17)
        Me.rdb_si.TabIndex = 5
        Me.rdb_si.TabStop = True
        Me.rdb_si.Text = "SI"
        Me.rdb_si.UseVisualStyleBackColor = True
        '
        'lbl_sede
        '
        Me.lbl_sede.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_sede.AutoSize = True
        Me.lbl_sede.Location = New System.Drawing.Point(888, 560)
        Me.lbl_sede.Name = "lbl_sede"
        Me.lbl_sede.Size = New System.Drawing.Size(41, 13)
        Me.lbl_sede.TabIndex = 315
        Me.lbl_sede.Text = "Sede : "
        '
        'lbl_idsede
        '
        Me.lbl_idsede.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_idsede.AutoSize = True
        Me.lbl_idsede.Location = New System.Drawing.Point(944, 560)
        Me.lbl_idsede.Name = "lbl_idsede"
        Me.lbl_idsede.Size = New System.Drawing.Size(19, 13)
        Me.lbl_idsede.TabIndex = 316
        Me.lbl_idsede.Text = "***"
        '
        'frm_prodMasiva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 595)
        Me.Controls.Add(Me.lbl_idsede)
        Me.Controls.Add(Me.lbl_sede)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.rb_scarp)
        Me.Controls.Add(Me.rb_pofic)
        Me.Controls.Add(Me.grp_combos)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.cmb_maq)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btn_delpesos)
        Me.Controls.Add(Me.lbl_cordia)
        Me.Controls.Add(Me.lbl_item)
        Me.Controls.Add(Me.lbl_count)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_code)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_desc)
        Me.Controls.Add(Me.btnQuery)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.lbl_linS)
        Me.Controls.Add(Me.lbl_idlin)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frm_prodMasiva"
        Me.Text = "Producción Masiva"
        Me.grp_combos.ResumeLayout(False)
        Me.grp_combos.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents txt_code As System.Windows.Forms.TextBox
    Protected WithEvents Label10 As System.Windows.Forms.Label
    Protected WithEvents txt_desc As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents lbl_count As System.Windows.Forms.Label
    Protected WithEvents lbl_item As System.Windows.Forms.Label
    Protected WithEvents lbl_cordia As System.Windows.Forms.Label
    Friend WithEvents sppuerto As System.IO.Ports.SerialPort
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents lbl_countbul As System.Windows.Forms.Label
    Protected WithEvents lbl_pesotot As System.Windows.Forms.Label
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_delpesos As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents grp_combos As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_ayud As System.Windows.Forms.Label
    Friend WithEvents cmb_ayud As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_ope As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmb_maq As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rb_pofic As System.Windows.Forms.RadioButton
    Friend WithEvents rb_scarp As System.Windows.Forms.RadioButton
    Protected WithEvents lbl_pesotot_s As System.Windows.Forms.Label
    Protected WithEvents Label12 As System.Windows.Forms.Label
    Protected WithEvents lbl_countbul_s As System.Windows.Forms.Label
    Protected WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Protected WithEvents lbl_idlin As System.Windows.Forms.Label
    Protected WithEvents lbl_linS As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdb_no As System.Windows.Forms.RadioButton
    Friend WithEvents rdb_si As System.Windows.Forms.RadioButton
    Protected WithEvents lbl_sede As System.Windows.Forms.Label
    Private WithEvents lbl_idsede As System.Windows.Forms.Label
End Class
