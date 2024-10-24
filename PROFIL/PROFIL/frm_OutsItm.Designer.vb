<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_OutsItm
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt_nomclie = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txt_text = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmb_moti = New System.Windows.Forms.ComboBox
        Me.Transportista = New System.Windows.Forms.Label
        Me.txt_idclie = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpFdoc = New System.Windows.Forms.DateTimePicker
        Me.txt_id = New System.Windows.Forms.TextBox
        Me.txt_docnum = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtPosi = New System.Windows.Forms.TextBox
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnBefore = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnConfirma = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnquery = New System.Windows.Forms.Button
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NuevoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.GroupBox4.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(589, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 310
        Me.Label4.Text = "DocNum"
        '
        'txt_nomclie
        '
        Me.txt_nomclie.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txt_nomclie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_nomclie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_nomclie.Location = New System.Drawing.Point(243, 84)
        Me.txt_nomclie.MaxLength = 15
        Me.txt_nomclie.Name = "txt_nomclie"
        Me.txt_nomclie.ReadOnly = True
        Me.txt_nomclie.Size = New System.Drawing.Size(258, 20)
        Me.txt_nomclie.TabIndex = 305
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(40, 88)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(39, 13)
        Me.Label16.TabIndex = 309
        Me.Label16.Text = "Cliente"
        '
        'txt_text
        '
        Me.txt_text.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_text.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txt_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_text.Location = New System.Drawing.Point(75, 404)
        Me.txt_text.MaxLength = 50
        Me.txt_text.Multiline = True
        Me.txt_text.Name = "txt_text"
        Me.txt_text.Size = New System.Drawing.Size(304, 57)
        Me.txt_text.TabIndex = 304
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 405)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 308
        Me.Label8.Text = "Comentario"
        '
        'cmb_moti
        '
        Me.cmb_moti.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_moti.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.cmb_moti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_moti.FormattingEnabled = True
        Me.cmb_moti.Items.AddRange(New Object() {"Venta", "Trans. gratuita", "Trasl. entre almacenes"})
        Me.cmb_moti.Location = New System.Drawing.Point(85, 50)
        Me.cmb_moti.Name = "cmb_moti"
        Me.cmb_moti.Size = New System.Drawing.Size(132, 21)
        Me.cmb_moti.TabIndex = 301
        '
        'Transportista
        '
        Me.Transportista.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Transportista.AutoSize = True
        Me.Transportista.Location = New System.Drawing.Point(589, 23)
        Me.Transportista.Name = "Transportista"
        Me.Transportista.Size = New System.Drawing.Size(18, 13)
        Me.Transportista.TabIndex = 306
        Me.Transportista.Text = "ID"
        '
        'txt_idclie
        '
        Me.txt_idclie.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txt_idclie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_idclie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_idclie.Location = New System.Drawing.Point(85, 84)
        Me.txt_idclie.MaxLength = 7
        Me.txt_idclie.Name = "txt_idclie"
        Me.txt_idclie.ReadOnly = True
        Me.txt_idclie.Size = New System.Drawing.Size(132, 20)
        Me.txt_idclie.TabIndex = 303
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(589, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 307
        Me.Label9.Text = "Fecha"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 311
        Me.Label1.Text = "Motivo"
        '
        'dtpFdoc
        '
        Me.dtpFdoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFdoc.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFdoc.Location = New System.Drawing.Point(651, 84)
        Me.dtpFdoc.Name = "dtpFdoc"
        Me.dtpFdoc.Size = New System.Drawing.Size(87, 20)
        Me.dtpFdoc.TabIndex = 312
        '
        'txt_id
        '
        Me.txt_id.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txt_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_id.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_id.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_id.ForeColor = System.Drawing.Color.Red
        Me.txt_id.Location = New System.Drawing.Point(651, 19)
        Me.txt_id.MaxLength = 7
        Me.txt_id.Name = "txt_id"
        Me.txt_id.ReadOnly = True
        Me.txt_id.Size = New System.Drawing.Size(87, 18)
        Me.txt_id.TabIndex = 313
        Me.txt_id.Text = "-99"
        Me.txt_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_docnum
        '
        Me.txt_docnum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_docnum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txt_docnum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_docnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_docnum.Location = New System.Drawing.Point(651, 50)
        Me.txt_docnum.MaxLength = 7
        Me.txt_docnum.Name = "txt_docnum"
        Me.txt_docnum.ReadOnly = True
        Me.txt_docnum.Size = New System.Drawing.Size(87, 20)
        Me.txt_docnum.TabIndex = 314
        Me.txt_docnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.txtPosi)
        Me.GroupBox4.Controls.Add(Me.btnLast)
        Me.GroupBox4.Controls.Add(Me.btnNext)
        Me.GroupBox4.Controls.Add(Me.btnBefore)
        Me.GroupBox4.Controls.Add(Me.btnFirst)
        Me.GroupBox4.Controls.Add(Me.btnConfirma)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Location = New System.Drawing.Point(400, 412)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(369, 37)
        Me.GroupBox4.TabIndex = 316
        Me.GroupBox4.TabStop = False
        '
        'txtPosi
        '
        Me.txtPosi.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtPosi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txtPosi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPosi.Enabled = False
        Me.txtPosi.Location = New System.Drawing.Point(235, 9)
        Me.txtPosi.Multiline = True
        Me.txtPosi.Name = "txtPosi"
        Me.txtPosi.Size = New System.Drawing.Size(60, 23)
        Me.txtPosi.TabIndex = 4
        Me.txtPosi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnLast
        '
        Me.btnLast.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnLast.Location = New System.Drawing.Point(330, 9)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(32, 23)
        Me.btnLast.TabIndex = 6
        Me.btnLast.Text = ">>|"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnNext.Location = New System.Drawing.Point(297, 9)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(32, 23)
        Me.btnNext.TabIndex = 5
        Me.btnNext.Text = ">>"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnBefore
        '
        Me.btnBefore.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnBefore.Location = New System.Drawing.Point(201, 9)
        Me.btnBefore.Name = "btnBefore"
        Me.btnBefore.Size = New System.Drawing.Size(32, 23)
        Me.btnBefore.TabIndex = 3
        Me.btnBefore.Text = "<<"
        Me.btnBefore.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnFirst.Location = New System.Drawing.Point(168, 9)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(32, 23)
        Me.btnFirst.TabIndex = 2
        Me.btnFirst.Text = "|<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnConfirma
        '
        Me.btnConfirma.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnConfirma.Location = New System.Drawing.Point(3, 7)
        Me.btnConfirma.Name = "btnConfirma"
        Me.btnConfirma.Size = New System.Drawing.Size(75, 27)
        Me.btnConfirma.TabIndex = 0
        Me.btnConfirma.Text = "&Ok"
        Me.btnConfirma.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(85, 7)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 27)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "&Cancelar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnquery
        '
        Me.btnquery.ForeColor = System.Drawing.Color.Red
        Me.btnquery.Location = New System.Drawing.Point(218, 84)
        Me.btnquery.Name = "btnquery"
        Me.btnquery.Size = New System.Drawing.Size(24, 20)
        Me.btnquery.TabIndex = 317
        Me.btnquery.Text = "..."
        Me.btnquery.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NuevoToolStripMenuItem, Me.EliminarToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(121, 48)
        '
        'NuevoToolStripMenuItem
        '
        Me.NuevoToolStripMenuItem.Name = "NuevoToolStripMenuItem"
        Me.NuevoToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.NuevoToolStripMenuItem.Text = "Nuevo"
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.EliminarToolStripMenuItem.Text = "Cancelar"
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 110)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridView1.Size = New System.Drawing.Size(775, 282)
        Me.DataGridView1.TabIndex = 318
        '
        'frm_OutsItm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 470)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnquery)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txt_docnum)
        Me.Controls.Add(Me.txt_id)
        Me.Controls.Add(Me.dtpFdoc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_nomclie)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txt_text)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmb_moti)
        Me.Controls.Add(Me.Transportista)
        Me.Controls.Add(Me.txt_idclie)
        Me.Controls.Add(Me.Label9)
        Me.Name = "frm_OutsItm"
        Me.Text = "Salida de productos"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents txt_nomclie As System.Windows.Forms.TextBox
    Protected WithEvents Label16 As System.Windows.Forms.Label
    Protected WithEvents txt_text As System.Windows.Forms.TextBox
    Protected WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents cmb_moti As System.Windows.Forms.ComboBox
    Protected WithEvents Transportista As System.Windows.Forms.Label
    Protected WithEvents txt_idclie As System.Windows.Forms.TextBox
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents txt_id As System.Windows.Forms.TextBox
    Protected WithEvents txt_docnum As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPosi As System.Windows.Forms.TextBox
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnBefore As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Protected WithEvents btnConfirma As System.Windows.Forms.Button
    Protected WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnquery As System.Windows.Forms.Button
    Private WithEvents dtpFdoc As System.Windows.Forms.DateTimePicker
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NuevoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
