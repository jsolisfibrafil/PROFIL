<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInformes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.U_VW_RQMNTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnprocess = New System.Windows.Forms.Button
        Me.btnexport = New System.Windows.Forms.Button
        Me.btnprint = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optdetprod = New System.Windows.Forms.RadioButton
        Me.Button1 = New System.Windows.Forms.Button
        Me.optProdMes = New System.Windows.Forms.RadioButton
        Me.optCodebar = New System.Windows.Forms.RadioButton
        Me.optDiario = New System.Windows.Forms.RadioButton
        Me.optKardex = New System.Windows.Forms.RadioButton
        Me.optDespacho = New System.Windows.Forms.RadioButton
        Me.optProd = New System.Windows.Forms.RadioButton
        Me.optPlan = New System.Windows.Forms.RadioButton
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmb_sede = New System.Windows.Forms.ComboBox
        Me.txt_s_Maq = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtItm1 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtpFfin = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtpFini = New System.Windows.Forms.DateTimePicker
        CType(Me.U_VW_RQMNTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupBox2.Controls.Add(Me.btnprocess)
        Me.GroupBox2.Controls.Add(Me.btnexport)
        Me.GroupBox2.Controls.Add(Me.btnprint)
        Me.GroupBox2.ForeColor = System.Drawing.Color.DarkBlue
        Me.GroupBox2.Location = New System.Drawing.Point(221, 382)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(488, 56)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        '
        'btnprocess
        '
        Me.btnprocess.Location = New System.Drawing.Point(16, 16)
        Me.btnprocess.Name = "btnprocess"
        Me.btnprocess.Size = New System.Drawing.Size(136, 32)
        Me.btnprocess.TabIndex = 0
        Me.btnprocess.Text = "Procesar informe"
        Me.btnprocess.UseVisualStyleBackColor = True
        '
        'btnexport
        '
        Me.btnexport.Location = New System.Drawing.Point(176, 16)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(136, 32)
        Me.btnexport.TabIndex = 1
        Me.btnexport.Text = "Exportar informe"
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(336, 16)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(136, 32)
        Me.btnprint.TabIndex = 2
        Me.btnprint.Text = "Imprimir Informe"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optdetprod)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.optProdMes)
        Me.GroupBox1.Controls.Add(Me.optCodebar)
        Me.GroupBox1.Controls.Add(Me.optDiario)
        Me.GroupBox1.Controls.Add(Me.optKardex)
        Me.GroupBox1.Controls.Add(Me.optDespacho)
        Me.GroupBox1.Controls.Add(Me.optProd)
        Me.GroupBox1.Controls.Add(Me.optPlan)
        Me.GroupBox1.ForeColor = System.Drawing.Color.DarkBlue
        Me.GroupBox1.Location = New System.Drawing.Point(16, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(495, 128)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Informes"
        '
        'optdetprod
        '
        Me.optdetprod.AutoSize = True
        Me.optdetprod.BackColor = System.Drawing.Color.Transparent
        Me.optdetprod.Location = New System.Drawing.Point(312, 56)
        Me.optdetprod.Name = "optdetprod"
        Me.optdetprod.Size = New System.Drawing.Size(114, 17)
        Me.optdetprod.TabIndex = 5
        Me.optdetprod.TabStop = True
        Me.optdetprod.Text = "Detalle producción"
        Me.optdetprod.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(336, 80)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(136, 34)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Ver Pesos x  dia (Mes actual)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'optProdMes
        '
        Me.optProdMes.AutoSize = True
        Me.optProdMes.BackColor = System.Drawing.Color.Transparent
        Me.optProdMes.Location = New System.Drawing.Point(312, 19)
        Me.optProdMes.Name = "optProdMes"
        Me.optProdMes.Size = New System.Drawing.Size(173, 17)
        Me.optProdMes.TabIndex = 2
        Me.optProdMes.TabStop = True
        Me.optProdMes.Text = "Informe de producción mensual"
        Me.optProdMes.UseVisualStyleBackColor = False
        '
        'optCodebar
        '
        Me.optCodebar.AutoSize = True
        Me.optCodebar.BackColor = System.Drawing.Color.Transparent
        Me.optCodebar.Location = New System.Drawing.Point(168, 96)
        Me.optCodebar.Name = "optCodebar"
        Me.optCodebar.Size = New System.Drawing.Size(128, 17)
        Me.optCodebar.TabIndex = 7
        Me.optCodebar.TabStop = True
        Me.optCodebar.Text = "Búsqueda CODEBAR"
        Me.optCodebar.UseVisualStyleBackColor = False
        '
        'optDiario
        '
        Me.optDiario.AutoSize = True
        Me.optDiario.BackColor = System.Drawing.Color.Transparent
        Me.optDiario.Location = New System.Drawing.Point(16, 96)
        Me.optDiario.Name = "optDiario"
        Me.optDiario.Size = New System.Drawing.Size(107, 17)
        Me.optDiario.TabIndex = 6
        Me.optDiario.TabStop = True
        Me.optDiario.Text = "Produccion diaria"
        Me.optDiario.UseVisualStyleBackColor = False
        '
        'optKardex
        '
        Me.optKardex.AutoSize = True
        Me.optKardex.BackColor = System.Drawing.Color.Transparent
        Me.optKardex.Location = New System.Drawing.Point(168, 56)
        Me.optKardex.Name = "optKardex"
        Me.optKardex.Size = New System.Drawing.Size(111, 17)
        Me.optKardex.TabIndex = 4
        Me.optKardex.TabStop = True
        Me.optKardex.Text = "Informe de Kardex"
        Me.optKardex.UseVisualStyleBackColor = False
        '
        'optDespacho
        '
        Me.optDespacho.AutoSize = True
        Me.optDespacho.BackColor = System.Drawing.Color.Transparent
        Me.optDespacho.Location = New System.Drawing.Point(16, 56)
        Me.optDespacho.Name = "optDespacho"
        Me.optDespacho.Size = New System.Drawing.Size(130, 17)
        Me.optDespacho.TabIndex = 3
        Me.optDespacho.TabStop = True
        Me.optDespacho.Text = "Informe de despachos"
        Me.optDespacho.UseVisualStyleBackColor = False
        '
        'optProd
        '
        Me.optProd.AutoSize = True
        Me.optProd.BackColor = System.Drawing.Color.Transparent
        Me.optProd.Location = New System.Drawing.Point(168, 19)
        Me.optProd.Name = "optProd"
        Me.optProd.Size = New System.Drawing.Size(131, 17)
        Me.optProd.TabIndex = 1
        Me.optProd.TabStop = True
        Me.optProd.Text = "Informe de producción"
        Me.optProd.UseVisualStyleBackColor = False
        '
        'optPlan
        '
        Me.optPlan.AutoSize = True
        Me.optPlan.BackColor = System.Drawing.Color.Transparent
        Me.optPlan.Location = New System.Drawing.Point(16, 19)
        Me.optPlan.Name = "optPlan"
        Me.optPlan.Size = New System.Drawing.Size(137, 17)
        Me.optPlan.TabIndex = 0
        Me.optPlan.TabStop = True
        Me.optPlan.Text = "Informe de planificacion"
        Me.optPlan.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(9, 144)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(912, 232)
        Me.DataGridView1.TabIndex = 7
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.cmb_sede)
        Me.GroupBox3.Controls.Add(Me.txt_s_Maq)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.txtItm1)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.dtpFfin)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.dtpFini)
        Me.GroupBox3.ForeColor = System.Drawing.Color.DarkBlue
        Me.GroupBox3.Location = New System.Drawing.Point(520, 8)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(408, 128)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Parametros desde"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(224, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 323
        Me.Label2.Text = "Sede"
        '
        'cmb_sede
        '
        Me.cmb_sede.FormattingEnabled = True
        Me.cmb_sede.Location = New System.Drawing.Point(272, 24)
        Me.cmb_sede.Name = "cmb_sede"
        Me.cmb_sede.Size = New System.Drawing.Size(121, 21)
        Me.cmb_sede.TabIndex = 322
        '
        'txt_s_Maq
        '
        Me.txt_s_Maq.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txt_s_Maq.Location = New System.Drawing.Point(61, 56)
        Me.txt_s_Maq.Name = "txt_s_Maq"
        Me.txt_s_Maq.Size = New System.Drawing.Size(155, 20)
        Me.txt_s_Maq.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 321
        Me.Label6.Text = "Máquina"
        '
        'txtItm1
        '
        Me.txtItm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txtItm1.Location = New System.Drawing.Point(61, 24)
        Me.txtItm1.Name = "txtItm1"
        Me.txtItm1.Size = New System.Drawing.Size(155, 20)
        Me.txtItm1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Fch Ini"
        '
        'dtpFfin
        '
        Me.dtpFfin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFfin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFfin.Location = New System.Drawing.Point(304, 88)
        Me.dtpFfin.Name = "dtpFfin"
        Me.dtpFfin.Size = New System.Drawing.Size(88, 20)
        Me.dtpFfin.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Item"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(248, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Fch Fin"
        '
        'dtpFini
        '
        Me.dtpFini.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.dtpFini.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFini.Location = New System.Drawing.Point(61, 89)
        Me.dtpFini.Name = "dtpFini"
        Me.dtpFini.Size = New System.Drawing.Size(88, 20)
        Me.dtpFini.TabIndex = 2
        '
        'frmInformes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(930, 447)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "frmInformes"
        Me.Text = "Informes de Sistema"
        CType(Me.U_VW_RQMNTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents U_VW_RQMNTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnprocess As System.Windows.Forms.Button
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optCodebar As System.Windows.Forms.RadioButton
    Friend WithEvents optDiario As System.Windows.Forms.RadioButton
    Friend WithEvents optKardex As System.Windows.Forms.RadioButton
    Friend WithEvents optDespacho As System.Windows.Forms.RadioButton
    Friend WithEvents optProd As System.Windows.Forms.RadioButton
    Friend WithEvents optPlan As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_s_Maq As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtItm1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFfin As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpFini As System.Windows.Forms.DateTimePicker
    Friend WithEvents optProdMes As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents optdetprod As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_sede As System.Windows.Forms.ComboBox

   
End Class
