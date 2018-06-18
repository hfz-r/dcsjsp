<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmProgress
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlProgress = New System.Windows.Forms.Panel
        Me.btnSTBack = New System.Windows.Forms.Button
        Me.pnlDataTransfer = New System.Windows.Forms.Panel
        Me.ProgressBar = New System.Windows.Forms.ProgressBar
        Me.lblMessage = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblTitle = New System.Windows.Forms.Label
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.pnlSetDatetime = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.btnDateSave = New System.Windows.Forms.Button
        Me.dtScannerDate = New System.Windows.Forms.DateTimePicker
        Me.lblScannerDate = New System.Windows.Forms.Label
        Me.pnlProgress.SuspendLayout()
        Me.pnlDataTransfer.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlSetDatetime.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlProgress
        '
        Me.pnlProgress.BackColor = System.Drawing.Color.Transparent
        Me.pnlProgress.Controls.Add(Me.btnSTBack)
        Me.pnlProgress.Controls.Add(Me.pnlDataTransfer)
        Me.pnlProgress.Controls.Add(Me.Panel1)
        Me.pnlProgress.Location = New System.Drawing.Point(5, 3)
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Size = New System.Drawing.Size(320, 275)
        '
        'btnSTBack
        '
        Me.btnSTBack.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnSTBack.Location = New System.Drawing.Point(179, 185)
        Me.btnSTBack.Name = "btnSTBack"
        Me.btnSTBack.Size = New System.Drawing.Size(124, 26)
        Me.btnSTBack.TabIndex = 1
        Me.btnSTBack.TabStop = False
        Me.btnSTBack.Text = "Back"
        Me.btnSTBack.Visible = False
        '
        'pnlDataTransfer
        '
        Me.pnlDataTransfer.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlDataTransfer.Controls.Add(Me.ProgressBar)
        Me.pnlDataTransfer.Controls.Add(Me.lblMessage)
        Me.pnlDataTransfer.Location = New System.Drawing.Point(14, 99)
        Me.pnlDataTransfer.Name = "pnlDataTransfer"
        Me.pnlDataTransfer.Size = New System.Drawing.Size(289, 77)
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(4, 34)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(282, 20)
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblMessage.Location = New System.Drawing.Point(3, 8)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(283, 20)
        Me.lblMessage.Text = "Starting..."
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblTitle)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 24)
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTitle.Location = New System.Drawing.Point(108, 1)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(108, 20)
        Me.lblTitle.Text = "In process.."
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 414)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(651, 24)
        '
        'pnlSetDatetime
        '
        Me.pnlSetDatetime.BackColor = System.Drawing.Color.Transparent
        Me.pnlSetDatetime.Controls.Add(Me.Panel2)
        Me.pnlSetDatetime.Controls.Add(Me.btnDateSave)
        Me.pnlSetDatetime.Controls.Add(Me.dtScannerDate)
        Me.pnlSetDatetime.Controls.Add(Me.lblScannerDate)
        Me.pnlSetDatetime.Location = New System.Drawing.Point(331, 4)
        Me.pnlSetDatetime.Name = "pnlSetDatetime"
        Me.pnlSetDatetime.Size = New System.Drawing.Size(320, 275)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label30)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(320, 24)
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(56, 3)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(209, 19)
        Me.Label30.Text = "Scanner Date Time Setting"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnDateSave
        '
        Me.btnDateSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnDateSave.Location = New System.Drawing.Point(160, 184)
        Me.btnDateSave.Name = "btnDateSave"
        Me.btnDateSave.Size = New System.Drawing.Size(124, 26)
        Me.btnDateSave.TabIndex = 33
        Me.btnDateSave.Text = "Save"
        '
        'dtScannerDate
        '
        Me.dtScannerDate.CustomFormat = "yyyy-MM-dd hh:mm tt"
        Me.dtScannerDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.dtScannerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtScannerDate.Location = New System.Drawing.Point(132, 54)
        Me.dtScannerDate.Name = "dtScannerDate"
        Me.dtScannerDate.Size = New System.Drawing.Size(152, 22)
        Me.dtScannerDate.TabIndex = 32
        Me.dtScannerDate.Value = New Date(2015, 11, 17, 17, 5, 30, 0)
        '
        'lblScannerDate
        '
        Me.lblScannerDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblScannerDate.Location = New System.Drawing.Point(37, 56)
        Me.lblScannerDate.Name = "lblScannerDate"
        Me.lblScannerDate.Size = New System.Drawing.Size(93, 20)
        Me.lblScannerDate.Text = "Scanner Date :"
        '
        'frmProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(638, 455)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlSetDatetime)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.pnlProgress)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProgress"
        Me.Text = "JSP"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlProgress.ResumeLayout(False)
        Me.pnlDataTransfer.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlSetDatetime.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlProgress As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents btnSTBack As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlDataTransfer As System.Windows.Forms.Panel
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents pnlSetDatetime As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents btnDateSave As System.Windows.Forms.Button
    Friend WithEvents dtScannerDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblScannerDate As System.Windows.Forms.Label

End Class
