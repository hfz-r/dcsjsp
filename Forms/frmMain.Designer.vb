<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.pnlSetDatetime = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.btnDateSave = New System.Windows.Forms.Button
        Me.dtScannerDate = New System.Windows.Forms.DateTimePicker
        Me.lblScannerDate = New System.Windows.Forms.Label
        Me.pnlMainMenu = New System.Windows.Forms.Panel
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.btnMainClose = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnSetting = New System.Windows.Forms.PictureBox
        Me.btnSupply = New System.Windows.Forms.PictureBox
        Me.btnUnpack = New System.Windows.Forms.PictureBox
        Me.btnCdioReceiving = New System.Windows.Forms.PictureBox
        Me.pnlMenuSupply = New System.Windows.Forms.Panel
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.btnRobbing = New System.Windows.Forms.PictureBox
        Me.btnChildPart = New System.Windows.Forms.PictureBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.btnBigPart = New System.Windows.Forms.PictureBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.btnProgressLane = New System.Windows.Forms.PictureBox
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.btnCloseSupply = New System.Windows.Forms.Button
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.footerStatusBar = New System.Windows.Forms.StatusBar
        Me.TimerCheckOnline = New System.Windows.Forms.Timer
        Me.pnlSetDatetime.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlMainMenu.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlMenuSupply.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSetDatetime
        '
        Me.pnlSetDatetime.BackColor = System.Drawing.Color.Transparent
        Me.pnlSetDatetime.Controls.Add(Me.Panel2)
        Me.pnlSetDatetime.Controls.Add(Me.btnDateSave)
        Me.pnlSetDatetime.Controls.Add(Me.dtScannerDate)
        Me.pnlSetDatetime.Controls.Add(Me.lblScannerDate)
        Me.pnlSetDatetime.Location = New System.Drawing.Point(5, 9)
        Me.pnlSetDatetime.Name = "pnlSetDatetime"
        Me.pnlSetDatetime.Size = New System.Drawing.Size(320, 275)
        Me.pnlSetDatetime.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.PowderBlue
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
        'pnlMainMenu
        '
        Me.pnlMainMenu.BackColor = System.Drawing.Color.Transparent
        Me.pnlMainMenu.Controls.Add(Me.Panel8)
        Me.pnlMainMenu.Controls.Add(Me.Panel1)
        Me.pnlMainMenu.Controls.Add(Me.Label17)
        Me.pnlMainMenu.Controls.Add(Me.Label16)
        Me.pnlMainMenu.Controls.Add(Me.Label15)
        Me.pnlMainMenu.Controls.Add(Me.Label14)
        Me.pnlMainMenu.Controls.Add(Me.btnSetting)
        Me.pnlMainMenu.Controls.Add(Me.btnSupply)
        Me.pnlMainMenu.Controls.Add(Me.btnUnpack)
        Me.pnlMainMenu.Controls.Add(Me.btnCdioReceiving)
        Me.pnlMainMenu.Location = New System.Drawing.Point(331, 9)
        Me.pnlMainMenu.Name = "pnlMainMenu"
        Me.pnlMainMenu.Size = New System.Drawing.Size(320, 275)
        Me.pnlMainMenu.Visible = False
        '
        'Panel8
        '
        Me.Panel8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel8.Controls.Add(Me.btnMainClose)
        Me.Panel8.Location = New System.Drawing.Point(0, 247)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(320, 24)
        '
        'btnMainClose
        '
        Me.btnMainClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnMainClose.Location = New System.Drawing.Point(200, 2)
        Me.btnMainClose.Name = "btnMainClose"
        Me.btnMainClose.Size = New System.Drawing.Size(110, 20)
        Me.btnMainClose.TabIndex = 34
        Me.btnMainClose.TabStop = False
        Me.btnMainClose.Text = "Close"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 24)
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(56, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(209, 18)
        Me.Label1.Text = "Main Menu"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label17.Location = New System.Drawing.Point(193, 227)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 20)
        Me.Label17.Text = "Setting"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label16.Location = New System.Drawing.Point(65, 227)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(80, 20)
        Me.Label16.Text = "Supply"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label15.Location = New System.Drawing.Point(193, 119)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 20)
        Me.Label15.Text = "Unpack"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label14.Location = New System.Drawing.Point(65, 119)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 20)
        Me.Label14.Text = "CDIO"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnSetting
        '
        Me.btnSetting.Image = CType(resources.GetObject("btnSetting.Image"), System.Drawing.Image)
        Me.btnSetting.Location = New System.Drawing.Point(193, 144)
        Me.btnSetting.Name = "btnSetting"
        Me.btnSetting.Size = New System.Drawing.Size(80, 80)
        Me.btnSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnSupply
        '
        Me.btnSupply.Image = CType(resources.GetObject("btnSupply.Image"), System.Drawing.Image)
        Me.btnSupply.Location = New System.Drawing.Point(65, 144)
        Me.btnSupply.Name = "btnSupply"
        Me.btnSupply.Size = New System.Drawing.Size(80, 80)
        Me.btnSupply.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnUnpack
        '
        Me.btnUnpack.Image = CType(resources.GetObject("btnUnpack.Image"), System.Drawing.Image)
        Me.btnUnpack.Location = New System.Drawing.Point(193, 36)
        Me.btnUnpack.Name = "btnUnpack"
        Me.btnUnpack.Size = New System.Drawing.Size(80, 80)
        Me.btnUnpack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnCdioReceiving
        '
        Me.btnCdioReceiving.Image = CType(resources.GetObject("btnCdioReceiving.Image"), System.Drawing.Image)
        Me.btnCdioReceiving.Location = New System.Drawing.Point(65, 36)
        Me.btnCdioReceiving.Name = "btnCdioReceiving"
        Me.btnCdioReceiving.Size = New System.Drawing.Size(80, 80)
        Me.btnCdioReceiving.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'pnlMenuSupply
        '
        Me.pnlMenuSupply.BackColor = System.Drawing.Color.Transparent
        Me.pnlMenuSupply.Controls.Add(Me.Label21)
        Me.pnlMenuSupply.Controls.Add(Me.Label20)
        Me.pnlMenuSupply.Controls.Add(Me.btnRobbing)
        Me.pnlMenuSupply.Controls.Add(Me.btnChildPart)
        Me.pnlMenuSupply.Controls.Add(Me.Label19)
        Me.pnlMenuSupply.Controls.Add(Me.btnBigPart)
        Me.pnlMenuSupply.Controls.Add(Me.Label18)
        Me.pnlMenuSupply.Controls.Add(Me.btnProgressLane)
        Me.pnlMenuSupply.Controls.Add(Me.Panel10)
        Me.pnlMenuSupply.Controls.Add(Me.Panel5)
        Me.pnlMenuSupply.Location = New System.Drawing.Point(657, 9)
        Me.pnlMenuSupply.Name = "pnlMenuSupply"
        Me.pnlMenuSupply.Size = New System.Drawing.Size(320, 275)
        Me.pnlMenuSupply.Visible = False
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label21.Location = New System.Drawing.Point(193, 227)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(80, 20)
        Me.Label21.Text = "Robbing"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label20.Location = New System.Drawing.Point(65, 227)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(80, 20)
        Me.Label20.Text = "Child Part"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnRobbing
        '
        Me.btnRobbing.Image = CType(resources.GetObject("btnRobbing.Image"), System.Drawing.Image)
        Me.btnRobbing.Location = New System.Drawing.Point(193, 144)
        Me.btnRobbing.Name = "btnRobbing"
        Me.btnRobbing.Size = New System.Drawing.Size(80, 80)
        Me.btnRobbing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnChildPart
        '
        Me.btnChildPart.Image = CType(resources.GetObject("btnChildPart.Image"), System.Drawing.Image)
        Me.btnChildPart.Location = New System.Drawing.Point(65, 144)
        Me.btnChildPart.Name = "btnChildPart"
        Me.btnChildPart.Size = New System.Drawing.Size(80, 80)
        Me.btnChildPart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label19.Location = New System.Drawing.Point(193, 119)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 20)
        Me.Label19.Text = "Big Part"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnBigPart
        '
        Me.btnBigPart.Image = CType(resources.GetObject("btnBigPart.Image"), System.Drawing.Image)
        Me.btnBigPart.Location = New System.Drawing.Point(193, 36)
        Me.btnBigPart.Name = "btnBigPart"
        Me.btnBigPart.Size = New System.Drawing.Size(80, 80)
        Me.btnBigPart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label18.Location = New System.Drawing.Point(60, 119)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(90, 20)
        Me.Label18.Text = "Progress Lane"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnProgressLane
        '
        Me.btnProgressLane.Image = CType(resources.GetObject("btnProgressLane.Image"), System.Drawing.Image)
        Me.btnProgressLane.Location = New System.Drawing.Point(65, 36)
        Me.btnProgressLane.Name = "btnProgressLane"
        Me.btnProgressLane.Size = New System.Drawing.Size(80, 80)
        Me.btnProgressLane.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'Panel10
        '
        Me.Panel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel10.Controls.Add(Me.btnCloseSupply)
        Me.Panel10.Location = New System.Drawing.Point(0, 247)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseSupply
        '
        Me.btnCloseSupply.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseSupply.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseSupply.Name = "btnCloseSupply"
        Me.btnCloseSupply.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseSupply.TabIndex = 34
        Me.btnCloseSupply.TabStop = False
        Me.btnCloseSupply.Text = "Close"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(320, 24)
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(56, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(209, 21)
        Me.Label2.Text = "Supply"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'footerStatusBar
        '
        Me.footerStatusBar.Location = New System.Drawing.Point(0, 431)
        Me.footerStatusBar.Name = "footerStatusBar"
        Me.footerStatusBar.Size = New System.Drawing.Size(977, 24)
        Me.footerStatusBar.Text = "StatusBar1"
        '
        'TimerCheckOnline
        '
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(638, 472)
        Me.ControlBox = False
        Me.Controls.Add(Me.footerStatusBar)
        Me.Controls.Add(Me.pnlMenuSupply)
        Me.Controls.Add(Me.pnlMainMenu)
        Me.Controls.Add(Me.pnlSetDatetime)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.Text = "DCS JSP"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSetDatetime.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlMainMenu.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlMenuSupply.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSetDatetime As System.Windows.Forms.Panel
    Friend WithEvents dtScannerDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblScannerDate As System.Windows.Forms.Label
    Friend WithEvents btnDateSave As System.Windows.Forms.Button
    Friend WithEvents pnlMainMenu As System.Windows.Forms.Panel
    Friend WithEvents btnSetting As System.Windows.Forms.PictureBox
    Friend WithEvents btnSupply As System.Windows.Forms.PictureBox
    Friend WithEvents btnUnpack As System.Windows.Forms.PictureBox
    Friend WithEvents btnCdioReceiving As System.Windows.Forms.PictureBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pnlMenuSupply As System.Windows.Forms.Panel
    Friend WithEvents footerStatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents btnMainClose As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnProgressLane As System.Windows.Forms.PictureBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseSupply As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnRobbing As System.Windows.Forms.PictureBox
    Friend WithEvents btnChildPart As System.Windows.Forms.PictureBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnBigPart As System.Windows.Forms.PictureBox
    Friend WithEvents TimerCheckOnline As System.Windows.Forms.Timer

End Class
