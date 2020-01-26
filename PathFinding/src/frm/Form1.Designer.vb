<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbtnDrawToixous = New System.Windows.Forms.ToolStripButton
        Me.tscbTroposStartEktelesh = New System.Windows.Forms.ToolStripComboBox
        Me.btnRun = New System.Windows.Forms.ToolStripButton
        Me.tsBtnClearPath = New System.Windows.Forms.ToolStripButton
        Me.tsBtnClearOla = New System.Windows.Forms.ToolStripButton
        Me.tscbAlgos = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip
        Me.tsBtnStavros = New System.Windows.Forms.ToolStripButton
        Me.tsBtnCross = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tsBtnLoukoumas = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ToolStrip1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ElementHost1
        '
        Me.ElementHost1.Location = New System.Drawing.Point(3, 3)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(1201, 578)
        Me.ElementHost1.TabIndex = 0
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Nothing
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtnDrawToixous, Me.tscbTroposStartEktelesh, Me.btnRun, Me.tsBtnClearPath, Me.tsBtnClearOla, Me.tscbAlgos})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1296, 33)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbtnDrawToixous
        '
        Me.tsbtnDrawToixous.CheckOnClick = True
        Me.tsbtnDrawToixous.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbtnDrawToixous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnDrawToixous.Name = "tsbtnDrawToixous"
        Me.tsbtnDrawToixous.Size = New System.Drawing.Size(123, 30)
        Me.tsbtnDrawToixous.Text = "Draw Toixous"
        '
        'tscbTroposStartEktelesh
        '
        Me.tscbTroposStartEktelesh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscbTroposStartEktelesh.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.tscbTroposStartEktelesh.Name = "tscbTroposStartEktelesh"
        Me.tscbTroposStartEktelesh.Size = New System.Drawing.Size(250, 33)
        '
        'btnRun
        '
        Me.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(47, 30)
        Me.btnRun.Text = "Run"
        '
        'tsBtnClearPath
        '
        Me.tsBtnClearPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsBtnClearPath.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnClearPath.Name = "tsBtnClearPath"
        Me.tsBtnClearPath.Size = New System.Drawing.Size(95, 30)
        Me.tsBtnClearPath.Text = "Clear Path"
        '
        'tsBtnClearOla
        '
        Me.tsBtnClearOla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsBtnClearOla.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnClearOla.Name = "tsBtnClearOla"
        Me.tsBtnClearOla.Size = New System.Drawing.Size(87, 30)
        Me.tsBtnClearOla.Text = "Clear Ola"
        '
        'tscbAlgos
        '
        Me.tscbAlgos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscbAlgos.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.tscbAlgos.Name = "tscbAlgos"
        Me.tscbAlgos.Size = New System.Drawing.Size(200, 33)
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtnStavros, Me.tsBtnCross, Me.ToolStripSeparator1, Me.tsBtnLoukoumas})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 33)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1296, 39)
        Me.ToolStrip2.TabIndex = 2
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'tsBtnStavros
        '
        Me.tsBtnStavros.Checked = True
        Me.tsBtnStavros.CheckOnClick = True
        Me.tsBtnStavros.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsBtnStavros.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsBtnStavros.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.tsBtnStavros.Image = CType(resources.GetObject("tsBtnStavros.Image"), System.Drawing.Image)
        Me.tsBtnStavros.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnStavros.Name = "tsBtnStavros"
        Me.tsBtnStavros.Size = New System.Drawing.Size(36, 36)
        Me.tsBtnStavros.Text = "+"
        '
        'tsBtnCross
        '
        Me.tsBtnCross.CheckOnClick = True
        Me.tsBtnCross.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsBtnCross.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.tsBtnCross.Image = CType(resources.GetObject("tsBtnCross.Image"), System.Drawing.Image)
        Me.tsBtnCross.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnCross.Name = "tsBtnCross"
        Me.tsBtnCross.Size = New System.Drawing.Size(32, 36)
        Me.tsBtnCross.Text = "x"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'tsBtnLoukoumas
        '
        Me.tsBtnLoukoumas.CheckOnClick = True
        Me.tsBtnLoukoumas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsBtnLoukoumas.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.tsBtnLoukoumas.Image = CType(resources.GetObject("tsBtnLoukoumas.Image"), System.Drawing.Image)
        Me.tsBtnLoukoumas.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnLoukoumas.Name = "tsBtnLoukoumas"
        Me.tsBtnLoukoumas.Size = New System.Drawing.Size(34, 36)
        Me.tsBtnLoukoumas.Text = "o"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.ElementHost1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 72)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1296, 621)
        Me.Panel1.TabIndex = 3
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1296, 693)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbtnDrawToixous As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRun As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscbTroposStartEktelesh As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tsBtnClearPath As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtnClearOla As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsBtnStavros As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtnCross As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsBtnLoukoumas As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tscbAlgos As System.Windows.Forms.ToolStripComboBox

End Class
