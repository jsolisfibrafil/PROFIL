<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInItem
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.txtCant = New System.Windows.Forms.TextBox
        Me.txtDscSAP = New System.Windows.Forms.TextBox
        Me.btnConfirma = New System.Windows.Forms.Button
        Me.txtIdSAP = New System.Windows.Forms.TextBox
        Me.btnquery = New System.Windows.Forms.Button
        Me.chkEvents = New System.Windows.Forms.CheckBox
        Me.btnconnect = New System.Windows.Forms.Button
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NuevoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtdocnum = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPosi = New System.Windows.Forms.TextBox
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnBefore = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.txtCodebar = New System.Windows.Forms.TextBox
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(600, 333)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Cantidad"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Descripcion"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "ID Item"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(101, 324)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(79, 30)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "&Cancelar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtCant
        '
        Me.txtCant.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCant.Enabled = False
        Me.txtCant.Location = New System.Drawing.Point(656, 329)
        Me.txtCant.Name = "txtCant"
        Me.txtCant.ReadOnly = True
        Me.txtCant.Size = New System.Drawing.Size(77, 20)
        Me.txtCant.TabIndex = 13
        Me.txtCant.Text = "0"
        Me.txtCant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDscSAP
        '
        Me.txtDscSAP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDscSAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDscSAP.Enabled = False
        Me.txtDscSAP.Location = New System.Drawing.Point(88, 41)
        Me.txtDscSAP.Name = "txtDscSAP"
        Me.txtDscSAP.ReadOnly = True
        Me.txtDscSAP.Size = New System.Drawing.Size(304, 20)
        Me.txtDscSAP.TabIndex = 12
        '
        'btnConfirma
        '
        Me.btnConfirma.Location = New System.Drawing.Point(16, 324)
        Me.btnConfirma.Name = "btnConfirma"
        Me.btnConfirma.Size = New System.Drawing.Size(79, 30)
        Me.btnConfirma.TabIndex = 14
        Me.btnConfirma.Text = "&Ok"
        Me.btnConfirma.UseVisualStyleBackColor = True
        '
        'txtIdSAP
        '
        Me.txtIdSAP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIdSAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdSAP.Location = New System.Drawing.Point(88, 9)
        Me.txtIdSAP.Name = "txtIdSAP"
        Me.txtIdSAP.ReadOnly = True
        Me.txtIdSAP.Size = New System.Drawing.Size(152, 20)
        Me.txtIdSAP.TabIndex = 11
        '
        'btnquery
        '
        Me.btnquery.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnquery.ForeColor = System.Drawing.Color.Red
        Me.btnquery.Location = New System.Drawing.Point(240, 9)
        Me.btnquery.Name = "btnquery"
        Me.btnquery.Size = New System.Drawing.Size(24, 20)
        Me.btnquery.TabIndex = 319
        Me.btnquery.Text = "..."
        Me.btnquery.UseVisualStyleBackColor = True
        Me.btnquery.Visible = False
        '
        'chkEvents
        '
        Me.chkEvents.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkEvents.AutoSize = True
        Me.chkEvents.Location = New System.Drawing.Point(496, 56)
        Me.chkEvents.Name = "chkEvents"
        Me.chkEvents.Size = New System.Drawing.Size(87, 17)
        Me.chkEvents.TabIndex = 321
        Me.chkEvents.Text = "Leer balanza"
        Me.chkEvents.UseVisualStyleBackColor = True
        Me.chkEvents.Visible = False
        '
        'btnconnect
        '
        Me.btnconnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnconnect.Location = New System.Drawing.Point(608, 40)
        Me.btnconnect.Name = "btnconnect"
        Me.btnconnect.Size = New System.Drawing.Size(132, 48)
        Me.btnconnect.TabIndex = 320
        Me.btnconnect.Text = "Conectar balanza..."
        Me.btnconnect.UseVisualStyleBackColor = True
        Me.btnconnect.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NuevoToolStripMenuItem, Me.EliminarToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(118, 48)
        '
        'NuevoToolStripMenuItem
        '
        Me.NuevoToolStripMenuItem.Name = "NuevoToolStripMenuItem"
        Me.NuevoToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.NuevoToolStripMenuItem.Text = "Nuevo"
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EliminarToolStripMenuItem.Text = "Eliminar"
        '
        'txtdocnum
        '
        Me.txtdocnum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtdocnum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdocnum.Location = New System.Drawing.Point(680, 9)
        Me.txtdocnum.Name = "txtdocnum"
        Me.txtdocnum.ReadOnly = True
        Me.txtdocnum.Size = New System.Drawing.Size(56, 20)
        Me.txtdocnum.TabIndex = 324
        Me.txtdocnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(624, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 325
        Me.Label5.Text = "Nº Doc :"
        '
        'txtPosi
        '
        Me.txtPosi.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtPosi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtPosi.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPosi.Enabled = False
        Me.txtPosi.Location = New System.Drawing.Point(360, 328)
        Me.txtPosi.Multiline = True
        Me.txtPosi.Name = "txtPosi"
        Me.txtPosi.Size = New System.Drawing.Size(60, 23)
        Me.txtPosi.TabIndex = 328
        Me.txtPosi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnLast
        '
        Me.btnLast.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnLast.Location = New System.Drawing.Point(455, 328)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(32, 23)
        Me.btnLast.TabIndex = 330
        Me.btnLast.Text = ">>|"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnNext.Location = New System.Drawing.Point(422, 328)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(32, 23)
        Me.btnNext.TabIndex = 329
        Me.btnNext.Text = ">>"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnBefore
        '
        Me.btnBefore.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnBefore.Location = New System.Drawing.Point(326, 328)
        Me.btnBefore.Name = "btnBefore"
        Me.btnBefore.Size = New System.Drawing.Size(32, 23)
        Me.btnBefore.TabIndex = 327
        Me.btnBefore.Text = "<<"
        Me.btnBefore.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnFirst.Location = New System.Drawing.Point(293, 328)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(32, 23)
        Me.btnFirst.TabIndex = 326
        Me.btnFirst.Text = "|<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(16, 96)
        Me.ListBox1.MultiColumn = True
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(720, 212)
        Me.ListBox1.TabIndex = 16
        '
        'DataGridView1
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.Location = New System.Drawing.Point(16, 96)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView1.Size = New System.Drawing.Size(720, 216)
        Me.DataGridView1.TabIndex = 332
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.ForeColor = System.Drawing.Color.Blue
        Me.RadioButton1.Location = New System.Drawing.Point(416, 8)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(84, 17)
        Me.RadioButton1.TabIndex = 333
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "ENTRADA"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.ForeColor = System.Drawing.Color.Red
        Me.RadioButton2.Location = New System.Drawing.Point(512, 8)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(73, 17)
        Me.RadioButton2.TabIndex = 334
        Me.RadioButton2.Text = "SALIDA "
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'txtCodebar
        '
        Me.txtCodebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCodebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCodebar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodebar.Location = New System.Drawing.Point(88, 67)
        Me.txtCodebar.Name = "txtCodebar"
        Me.txtCodebar.Size = New System.Drawing.Size(174, 22)
        Me.txtCodebar.TabIndex = 335
        Me.txtCodebar.Visible = False
        '
        'frmInItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 363)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.txtCodebar)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtPosi)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnBefore)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.txtdocnum)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkEvents)
        Me.Controls.Add(Me.btnquery)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnconnect)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtCant)
        Me.Controls.Add(Me.txtDscSAP)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.btnConfirma)
        Me.Controls.Add(Me.txtIdSAP)
        Me.Name = "frmInItem"
        Me.Text = "Ajuste de inventario (Entrada / Salida)"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtCant As System.Windows.Forms.TextBox
    Friend WithEvents txtDscSAP As System.Windows.Forms.TextBox
    Friend WithEvents btnConfirma As System.Windows.Forms.Button
    Friend WithEvents txtIdSAP As System.Windows.Forms.TextBox
    Friend WithEvents btnquery As System.Windows.Forms.Button
    Friend WithEvents chkEvents As System.Windows.Forms.CheckBox
    Friend WithEvents btnconnect As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NuevoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtdocnum As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPosi As System.Windows.Forms.TextBox
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnBefore As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents txtCodebar As System.Windows.Forms.TextBox
End Class
