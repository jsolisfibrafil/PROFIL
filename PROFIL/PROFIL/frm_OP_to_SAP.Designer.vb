<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_OP_to_SAP
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rb_AddSi = New System.Windows.Forms.RadioButton()
        Me.rb_AddNO = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_normr = New System.Windows.Forms.ComboBox()
        Me.lbl_cant = New System.Windows.Forms.Label()
        Me.btn_SAP = New System.Windows.Forms.Button()
        Me.lbl_peso = New System.Windows.Forms.Label()
        Me.lbl_prod = New System.Windows.Forms.Label()
        Me.rb_unid = New System.Windows.Forms.RadioButton()
        Me.rb_kilos = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmb_sede = New System.Windows.Forms.ComboBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Location = New System.Drawing.Point(8, 47)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(806, 696)
        Me.SplitContainer1.SplitterDistance = 322
        Me.SplitContainer1.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(802, 318)
        Me.DataGridView1.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmb_normr)
        Me.GroupBox1.Controls.Add(Me.lbl_cant)
        Me.GroupBox1.Controls.Add(Me.btn_SAP)
        Me.GroupBox1.Controls.Add(Me.lbl_peso)
        Me.GroupBox1.Controls.Add(Me.lbl_prod)
        Me.GroupBox1.Controls.Add(Me.rb_unid)
        Me.GroupBox1.Controls.Add(Me.rb_kilos)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(83, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(629, 139)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rb_AddSi)
        Me.GroupBox2.Controls.Add(Me.rb_AddNO)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox2.Location = New System.Drawing.Point(462, 81)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(134, 51)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Adicionar scrap"
        '
        'rb_AddSi
        '
        Me.rb_AddSi.AutoSize = True
        Me.rb_AddSi.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_AddSi.ForeColor = System.Drawing.Color.Red
        Me.rb_AddSi.Location = New System.Drawing.Point(18, 20)
        Me.rb_AddSi.Name = "rb_AddSi"
        Me.rb_AddSi.Size = New System.Drawing.Size(44, 28)
        Me.rb_AddSi.TabIndex = 14
        Me.rb_AddSi.Text = "SI"
        Me.rb_AddSi.UseVisualStyleBackColor = True
        '
        'rb_AddNO
        '
        Me.rb_AddNO.AutoSize = True
        Me.rb_AddNO.Checked = True
        Me.rb_AddNO.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_AddNO.ForeColor = System.Drawing.Color.Red
        Me.rb_AddNO.Location = New System.Drawing.Point(68, 20)
        Me.rb_AddNO.Name = "rb_AddNO"
        Me.rb_AddNO.Size = New System.Drawing.Size(57, 28)
        Me.rb_AddNO.TabIndex = 13
        Me.rb_AddNO.TabStop = True
        Me.rb_AddNO.Text = "NO"
        Me.rb_AddNO.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(403, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Norma Reparto:"
        '
        'cmb_normr
        '
        Me.cmb_normr.FormattingEnabled = True
        Me.cmb_normr.Location = New System.Drawing.Point(491, 45)
        Me.cmb_normr.Name = "cmb_normr"
        Me.cmb_normr.Size = New System.Drawing.Size(121, 21)
        Me.cmb_normr.TabIndex = 11
        '
        'lbl_cant
        '
        Me.lbl_cant.AutoSize = True
        Me.lbl_cant.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cant.ForeColor = System.Drawing.Color.Blue
        Me.lbl_cant.Location = New System.Drawing.Point(105, 50)
        Me.lbl_cant.Name = "lbl_cant"
        Me.lbl_cant.Size = New System.Drawing.Size(23, 16)
        Me.lbl_cant.TabIndex = 10
        Me.lbl_cant.Text = "***"
        '
        'btn_SAP
        '
        Me.btn_SAP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_SAP.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btn_SAP.Enabled = False
        Me.btn_SAP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_SAP.Location = New System.Drawing.Point(16, 89)
        Me.btn_SAP.Name = "btn_SAP"
        Me.btn_SAP.Size = New System.Drawing.Size(101, 39)
        Me.btn_SAP.TabIndex = 2
        Me.btn_SAP.Text = "&A SAP"
        Me.btn_SAP.UseVisualStyleBackColor = False
        '
        'lbl_peso
        '
        Me.lbl_peso.AutoSize = True
        Me.lbl_peso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_peso.ForeColor = System.Drawing.Color.Blue
        Me.lbl_peso.Location = New System.Drawing.Point(281, 50)
        Me.lbl_peso.Name = "lbl_peso"
        Me.lbl_peso.Size = New System.Drawing.Size(23, 16)
        Me.lbl_peso.TabIndex = 9
        Me.lbl_peso.Text = "***"
        '
        'lbl_prod
        '
        Me.lbl_prod.AutoSize = True
        Me.lbl_prod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_prod.ForeColor = System.Drawing.Color.Red
        Me.lbl_prod.Location = New System.Drawing.Point(94, 16)
        Me.lbl_prod.Name = "lbl_prod"
        Me.lbl_prod.Size = New System.Drawing.Size(23, 16)
        Me.lbl_prod.TabIndex = 8
        Me.lbl_prod.Text = "***"
        '
        'rb_unid
        '
        Me.rb_unid.AutoSize = True
        Me.rb_unid.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_unid.ForeColor = System.Drawing.Color.Red
        Me.rb_unid.Location = New System.Drawing.Point(135, 100)
        Me.rb_unid.Name = "rb_unid"
        Me.rb_unid.Size = New System.Drawing.Size(108, 28)
        Me.rb_unid.TabIndex = 7
        Me.rb_unid.TabStop = True
        Me.rb_unid.Text = "Unidades"
        Me.rb_unid.UseVisualStyleBackColor = True
        '
        'rb_kilos
        '
        Me.rb_kilos.AutoSize = True
        Me.rb_kilos.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_kilos.ForeColor = System.Drawing.Color.Red
        Me.rb_kilos.Location = New System.Drawing.Point(297, 100)
        Me.rb_kilos.Name = "rb_kilos"
        Me.rb_kilos.Size = New System.Drawing.Size(68, 28)
        Me.rb_kilos.TabIndex = 6
        Me.rb_kilos.TabStop = True
        Me.rb_kilos.Text = "Kilos"
        Me.rb_kilos.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(41, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cantidad:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(227, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Peso:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Producto:"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(802, 366)
        Me.DataGridView2.TabIndex = 3
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_Cancel.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btn_Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.Location = New System.Drawing.Point(93, 749)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 52)
        Me.btn_Cancel.TabIndex = 3
        Me.btn_Cancel.Text = "&Cancelar"
        Me.btn_Cancel.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button1.Location = New System.Drawing.Point(739, 747)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 52)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "&Cancelar"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(12, 749)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 52)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "&Confirmar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.Image = Global.PROFIL.My.Resources.Resources.excel
        Me.Button5.Location = New System.Drawing.Point(401, 759)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(43, 40)
        Me.Button5.TabIndex = 326
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(581, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 16)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "SEDE:"
        '
        'cmb_sede
        '
        Me.cmb_sede.FormattingEnabled = True
        Me.cmb_sede.Location = New System.Drawing.Point(641, 12)
        Me.cmb_sede.Name = "cmb_sede"
        Me.cmb_sede.Size = New System.Drawing.Size(170, 21)
        Me.cmb_sede.TabIndex = 16
        '
        'frm_OP_to_SAP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(823, 813)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.cmb_sede)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Button2)
        Me.Name = "frm_OP_to_SAP"
        Me.Text = "Exportacion de O/P a SAP"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents btn_SAP As System.Windows.Forms.Button
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_cant As System.Windows.Forms.Label
    Friend WithEvents lbl_peso As System.Windows.Forms.Label
    Friend WithEvents lbl_prod As System.Windows.Forms.Label
    Friend WithEvents rb_unid As System.Windows.Forms.RadioButton
    Friend WithEvents rb_kilos As System.Windows.Forms.RadioButton
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_normr As System.Windows.Forms.ComboBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_AddSi As System.Windows.Forms.RadioButton
    Friend WithEvents rb_AddNO As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_sede As System.Windows.Forms.ComboBox
End Class
