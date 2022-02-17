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
        Me.CODE_VB = New System.Windows.Forms.TextBox()
        Me.CODE_C = New System.Windows.Forms.TextBox()
        Me.Name = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VB2C = New System.Windows.Forms.Button()
        Me.C2VB = New System.Windows.Forms.Button()
        Me.btnRedim = New System.Windows.Forms.Button()
        Me.btnSetData = New System.Windows.Forms.Button()
        Me.btnGetData = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CODE_VB
        '
        Me.CODE_VB.Location = New System.Drawing.Point(24, 115)
        Me.CODE_VB.Multiline = True
        Me.CODE_VB.Name = "CODE_VB"
        Me.CODE_VB.Size = New System.Drawing.Size(265, 669)
        Me.CODE_VB.TabIndex = 0
        '
        'CODE_C
        '
        Me.CODE_C.Location = New System.Drawing.Point(424, 115)
        Me.CODE_C.Multiline = True
        Me.CODE_C.Name = "CODE_C"
        Me.CODE_C.Size = New System.Drawing.Size(270, 669)
        Me.CODE_C.TabIndex = 1
        '
        'Name
        '
        Me.Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name.Location = New System.Drawing.Point(160, 20)
        Me.Name.Name = "Name"
        Me.Name.Size = New System.Drawing.Size(414, 48)
        Me.Name.TabIndex = 2
        Me.Name.Text = "Convert Struct C And VB"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(511, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "CODE C"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(103, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 25)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "CODE VB"
        '
        'VB2C
        '
        Me.VB2C.Location = New System.Drawing.Point(310, 115)
        Me.VB2C.Name = "VB2C"
        Me.VB2C.Size = New System.Drawing.Size(86, 61)
        Me.VB2C.TabIndex = 5
        Me.VB2C.Text = ">>"
        Me.VB2C.UseVisualStyleBackColor = True
        '
        'C2VB
        '
        Me.C2VB.Location = New System.Drawing.Point(310, 182)
        Me.C2VB.Name = "C2VB"
        Me.C2VB.Size = New System.Drawing.Size(86, 58)
        Me.C2VB.TabIndex = 6
        Me.C2VB.Text = "<<Define"
        Me.C2VB.UseVisualStyleBackColor = True
        '
        'btnRedim
        '
        Me.btnRedim.Location = New System.Drawing.Point(310, 246)
        Me.btnRedim.Name = "btnRedim"
        Me.btnRedim.Size = New System.Drawing.Size(86, 58)
        Me.btnRedim.TabIndex = 7
        Me.btnRedim.Text = "<<Redim"
        Me.btnRedim.UseVisualStyleBackColor = True
        '
        'btnSetData
        '
        Me.btnSetData.Location = New System.Drawing.Point(310, 310)
        Me.btnSetData.Name = "btnSetData"
        Me.btnSetData.Size = New System.Drawing.Size(86, 59)
        Me.btnSetData.TabIndex = 8
        Me.btnSetData.Text = "<<SetData"
        Me.btnSetData.UseVisualStyleBackColor = True
        '
        'btnGetData
        '
        Me.btnGetData.Location = New System.Drawing.Point(310, 375)
        Me.btnGetData.Name = "btnGetData"
        Me.btnGetData.Size = New System.Drawing.Size(86, 59)
        Me.btnGetData.TabIndex = 9
        Me.btnGetData.Text = "<<GetData"
        Me.btnGetData.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(724, 796)
        Me.Controls.Add(Me.btnGetData)
        Me.Controls.Add(Me.btnSetData)
        Me.Controls.Add(Me.btnRedim)
        Me.Controls.Add(Me.C2VB)
        Me.Controls.Add(Me.VB2C)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Name)
        Me.Controls.Add(Me.CODE_C)
        Me.Controls.Add(Me.CODE_VB)
        Me.Text = "Tool Convert VB2C Ver 1.2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CODE_VB As TextBox
    Friend WithEvents CODE_C As TextBox
    Friend WithEvents Name As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents VB2C As Button
    Friend WithEvents C2VB As Button
    Friend WithEvents btnRedim As Button
    Friend WithEvents btnSetData As Button
    Friend WithEvents btnGetData As Button
End Class
