﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSnoopObjects
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
    Me.treObjects = New System.Windows.Forms.TreeView()
    Me.lvwProperties = New System.Windows.Forms.ListView()
    Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.btnSelectObject = New System.Windows.Forms.Button()
    Me.btnSelectAnotherFile = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'treObjects
    '
    Me.treObjects.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.treObjects.Location = New System.Drawing.Point(13, 13)
    Me.treObjects.Name = "treObjects"
    Me.treObjects.Size = New System.Drawing.Size(255, 482)
    Me.treObjects.TabIndex = 0
    '
    'lvwProperties
    '
    Me.lvwProperties.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvwProperties.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
    Me.lvwProperties.Location = New System.Drawing.Point(274, 13)
    Me.lvwProperties.MultiSelect = False
    Me.lvwProperties.Name = "lvwProperties"
    Me.lvwProperties.Size = New System.Drawing.Size(429, 482)
    Me.lvwProperties.TabIndex = 1
    Me.lvwProperties.UseCompatibleStateImageBehavior = False
    Me.lvwProperties.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Name"
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Type"
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "Value"
    '
    'btnSelectObject
    '
    Me.btnSelectObject.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSelectObject.Location = New System.Drawing.Point(559, 496)
    Me.btnSelectObject.Name = "btnSelectObject"
    Me.btnSelectObject.Size = New System.Drawing.Size(143, 23)
    Me.btnSelectObject.TabIndex = 3
    Me.btnSelectObject.Text = "Select object"
    Me.btnSelectObject.UseVisualStyleBackColor = True
    '
    'btnSelectAnotherFile
    '
    Me.btnSelectAnotherFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnSelectAnotherFile.Location = New System.Drawing.Point(13, 496)
    Me.btnSelectAnotherFile.Name = "btnSelectAnotherFile"
    Me.btnSelectAnotherFile.Size = New System.Drawing.Size(143, 23)
    Me.btnSelectAnotherFile.TabIndex = 4
    Me.btnSelectAnotherFile.Text = "Select another file..."
    Me.btnSelectAnotherFile.UseVisualStyleBackColor = True
    '
    'frmSnoopObjects
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(715, 531)
    Me.Controls.Add(Me.btnSelectAnotherFile)
    Me.Controls.Add(Me.btnSelectObject)
    Me.Controls.Add(Me.lvwProperties)
    Me.Controls.Add(Me.treObjects)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmSnoopObjects"
    Me.ShowInTaskbar = False
    Me.Text = "Snoop Civil 3D Database"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents treObjects As System.Windows.Forms.TreeView
  Friend WithEvents lvwProperties As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnSelectObject As System.Windows.Forms.Button
  Friend WithEvents btnSelectAnotherFile As System.Windows.Forms.Button
End Class
