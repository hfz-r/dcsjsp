<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmBigPart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBigPart))
        Me.pnlBPMain = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnAbnormalBP = New System.Windows.Forms.PictureBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnScanBP = New System.Windows.Forms.PictureBox
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.btnCloseBP = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.footerStatusBar = New System.Windows.Forms.StatusBar
        Me.TimerCheckOnline = New System.Windows.Forms.Timer
        Me.pnlBPFScan = New System.Windows.Forms.Panel
        Me.txtFSModuleNo = New System.Windows.Forms.TextBox
        Me.Label76 = New System.Windows.Forms.Label
        Me.btnSaveForceScan = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnBackFScan = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.lstViewRCVFScan = New System.Windows.Forms.ListView
        Me.REASON_CODE = New System.Windows.Forms.ColumnHeader
        Me.txtFSOrderNo = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlBPScanModule = New System.Windows.Forms.Panel
        Me.btnScanDetails = New System.Windows.Forms.Button
        Me.cmbShop = New System.Windows.Forms.ComboBox
        Me.Label62 = New System.Windows.Forms.Label
        Me.lblStatusMsg = New System.Windows.Forms.Label
        Me.lblTotalScanned = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.btnScanSubmit = New System.Windows.Forms.Button
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.txtOrderNo = New System.Windows.Forms.Label
        Me.txtModuleNo = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnBackBPScanModule = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.btnFScanModule = New System.Windows.Forms.PictureBox
        Me.txtModuleQR = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.pnlBPAbnViewDet = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnCloseBPViewDet = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.lstViewRcvDet = New System.Windows.Forms.ListView
        Me.ColumnHeader13 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader14 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader15 = New System.Windows.Forms.ColumnHeader
        Me.lblHeaderAbnVwDet = New System.Windows.Forms.Label
        Me.pnlBPPosting = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.btnBPSubmitPosting = New System.Windows.Forms.Button
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.btnCloseBPPosting = New System.Windows.Forms.Button
        Me.Label23 = New System.Windows.Forms.Label
        Me.lstViewPosting = New System.Windows.Forms.ListView
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.Label24 = New System.Windows.Forms.Label
        Me.pnlBPDelete = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.btnCloseBPDelete = New System.Windows.Forms.Button
        Me.Label27 = New System.Windows.Forms.Label
        Me.lstViewDelete = New System.Windows.Forms.ListView
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.Label28 = New System.Windows.Forms.Label
        Me.pnlBPAbn = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.btnCloseAbnBP = New System.Windows.Forms.Button
        Me.Label32 = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.btnBPAbnDelete = New System.Windows.Forms.PictureBox
        Me.btnBPAbnPost = New System.Windows.Forms.PictureBox
        Me.btnBPAbnView = New System.Windows.Forms.PictureBox
        Me.btnBPAbnScan = New System.Windows.Forms.PictureBox
        Me.pnlBPViewDet = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnBackBPViewDet = New System.Windows.Forms.Button
        Me.Label54 = New System.Windows.Forms.Label
        Me.TabBPSummary = New System.Windows.Forms.TabControl
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.lblDetailTotalScan = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.lstViewRCISummary = New System.Windows.Forms.ListView
        Me.ColumnHeader17 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader18 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader19 = New System.Windows.Forms.ColumnHeader
        Me.lblHeaderVwDet = New System.Windows.Forms.Label
        Me.pnlBPAbnScan = New System.Windows.Forms.Panel
        Me.btnBPAbnScanDet = New System.Windows.Forms.Button
        Me.cmbShopAbn = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblStatusMsgAbn = New System.Windows.Forms.Label
        Me.lblTotalScannedAbn = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.txtOrderNoAbn = New System.Windows.Forms.Label
        Me.txtModuleNoAbn = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.btnBackBPAbnScan = New System.Windows.Forms.Button
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.btnBPAbnFScan = New System.Windows.Forms.PictureBox
        Me.txtModuleQRAbn = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.lblPostingTotalPdgAbn = New System.Windows.Forms.Label
        Me.lblDeleteTotalAbn = New System.Windows.Forms.Label
        Me.pnlBPMain.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlBPFScan.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlBPScanModule.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.pnlBPAbnViewDet.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlBPPosting.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.pnlBPDelete.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlBPAbn.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlBPViewDet.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TabBPSummary.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.pnlBPAbnScan.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBPMain
        '
        Me.pnlBPMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPMain.Controls.Add(Me.Label12)
        Me.pnlBPMain.Controls.Add(Me.btnAbnormalBP)
        Me.pnlBPMain.Controls.Add(Me.Label10)
        Me.pnlBPMain.Controls.Add(Me.btnScanBP)
        Me.pnlBPMain.Controls.Add(Me.Panel9)
        Me.pnlBPMain.Controls.Add(Me.Panel3)
        Me.pnlBPMain.Location = New System.Drawing.Point(5, 3)
        Me.pnlBPMain.Name = "pnlBPMain"
        Me.pnlBPMain.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPMain.Visible = False
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label12.Location = New System.Drawing.Point(193, 119)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 20)
        Me.Label12.Text = "Abnormal"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnAbnormalBP
        '
        Me.btnAbnormalBP.Image = CType(resources.GetObject("btnAbnormalBP.Image"), System.Drawing.Image)
        Me.btnAbnormalBP.Location = New System.Drawing.Point(193, 36)
        Me.btnAbnormalBP.Name = "btnAbnormalBP"
        Me.btnAbnormalBP.Size = New System.Drawing.Size(80, 80)
        Me.btnAbnormalBP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label10.Location = New System.Drawing.Point(65, 119)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 20)
        Me.Label10.Text = "Scan"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnScanBP
        '
        Me.btnScanBP.Image = CType(resources.GetObject("btnScanBP.Image"), System.Drawing.Image)
        Me.btnScanBP.Location = New System.Drawing.Point(65, 36)
        Me.btnScanBP.Name = "btnScanBP"
        Me.btnScanBP.Size = New System.Drawing.Size(80, 80)
        Me.btnScanBP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel9.Controls.Add(Me.btnCloseBP)
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Location = New System.Drawing.Point(0, 247)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseBP
        '
        Me.btnCloseBP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseBP.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseBP.Name = "btnCloseBP"
        Me.btnCloseBP.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseBP.TabIndex = 34
        Me.btnCloseBP.Text = "Close"
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(0, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(209, 18)
        Me.Label8.Text = "USER NAME:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel3.Location = New System.Drawing.Point(1, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(320, 24)
        '
        'footerStatusBar
        '
        Me.footerStatusBar.Location = New System.Drawing.Point(0, 559)
        Me.footerStatusBar.Name = "footerStatusBar"
        Me.footerStatusBar.Size = New System.Drawing.Size(1629, 24)
        Me.footerStatusBar.Text = "StatusBar1"
        '
        'TimerCheckOnline
        '
        '
        'pnlBPFScan
        '
        Me.pnlBPFScan.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPFScan.Controls.Add(Me.txtFSModuleNo)
        Me.pnlBPFScan.Controls.Add(Me.Label76)
        Me.pnlBPFScan.Controls.Add(Me.btnSaveForceScan)
        Me.pnlBPFScan.Controls.Add(Me.Panel1)
        Me.pnlBPFScan.Controls.Add(Me.lstViewRCVFScan)
        Me.pnlBPFScan.Controls.Add(Me.txtFSOrderNo)
        Me.pnlBPFScan.Controls.Add(Me.Label16)
        Me.pnlBPFScan.Controls.Add(Me.Label1)
        Me.pnlBPFScan.Location = New System.Drawing.Point(657, 3)
        Me.pnlBPFScan.Name = "pnlBPFScan"
        Me.pnlBPFScan.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPFScan.Visible = False
        '
        'txtFSModuleNo
        '
        Me.txtFSModuleNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtFSModuleNo.Location = New System.Drawing.Point(8, 58)
        Me.txtFSModuleNo.Name = "txtFSModuleNo"
        Me.txtFSModuleNo.Size = New System.Drawing.Size(302, 19)
        Me.txtFSModuleNo.TabIndex = 1
        '
        'Label76
        '
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label76.Location = New System.Drawing.Point(8, 84)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(68, 20)
        Me.Label76.Text = "Order No :"
        '
        'btnSaveForceScan
        '
        Me.btnSaveForceScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnSaveForceScan.Location = New System.Drawing.Point(223, 4)
        Me.btnSaveForceScan.Name = "btnSaveForceScan"
        Me.btnSaveForceScan.Size = New System.Drawing.Size(90, 20)
        Me.btnSaveForceScan.TabIndex = 4
        Me.btnSaveForceScan.Text = "Save"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnBackFScan)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(0, 247)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 24)
        '
        'btnBackFScan
        '
        Me.btnBackFScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBackFScan.Location = New System.Drawing.Point(200, 2)
        Me.btnBackFScan.Name = "btnBackFScan"
        Me.btnBackFScan.Size = New System.Drawing.Size(110, 20)
        Me.btnBackFScan.TabIndex = 5
        Me.btnBackFScan.Text = "Back"
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(0, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(209, 18)
        Me.Label4.Text = "USER NAME:"
        '
        'lstViewRCVFScan
        '
        Me.lstViewRCVFScan.Columns.Add(Me.REASON_CODE)
        Me.lstViewRCVFScan.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewRCVFScan.FullRowSelect = True
        Me.lstViewRCVFScan.Location = New System.Drawing.Point(8, 130)
        Me.lstViewRCVFScan.Name = "lstViewRCVFScan"
        Me.lstViewRCVFScan.Size = New System.Drawing.Size(305, 80)
        Me.lstViewRCVFScan.TabIndex = 3
        Me.lstViewRCVFScan.View = System.Windows.Forms.View.Details
        '
        'REASON_CODE
        '
        Me.REASON_CODE.Text = "Reason"
        Me.REASON_CODE.Width = 302
        '
        'txtFSOrderNo
        '
        Me.txtFSOrderNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtFSOrderNo.Location = New System.Drawing.Point(8, 105)
        Me.txtFSOrderNo.Name = "txtFSOrderNo"
        Me.txtFSOrderNo.Size = New System.Drawing.Size(302, 19)
        Me.txtFSOrderNo.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label16.Location = New System.Drawing.Point(8, 34)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(68, 20)
        Me.Label16.Text = "Module No :"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.PowderBlue
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(2, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(316, 20)
        '
        'pnlBPScanModule
        '
        Me.pnlBPScanModule.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPScanModule.Controls.Add(Me.btnScanDetails)
        Me.pnlBPScanModule.Controls.Add(Me.cmbShop)
        Me.pnlBPScanModule.Controls.Add(Me.Label62)
        Me.pnlBPScanModule.Controls.Add(Me.lblStatusMsg)
        Me.pnlBPScanModule.Controls.Add(Me.lblTotalScanned)
        Me.pnlBPScanModule.Controls.Add(Me.Label21)
        Me.pnlBPScanModule.Controls.Add(Me.btnScanSubmit)
        Me.pnlBPScanModule.Controls.Add(Me.Panel8)
        Me.pnlBPScanModule.Controls.Add(Me.Panel12)
        Me.pnlBPScanModule.Controls.Add(Me.Label33)
        Me.pnlBPScanModule.Controls.Add(Me.btnFScanModule)
        Me.pnlBPScanModule.Controls.Add(Me.txtModuleQR)
        Me.pnlBPScanModule.Controls.Add(Me.Label34)
        Me.pnlBPScanModule.Controls.Add(Me.Label35)
        Me.pnlBPScanModule.Location = New System.Drawing.Point(331, 3)
        Me.pnlBPScanModule.Name = "pnlBPScanModule"
        Me.pnlBPScanModule.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPScanModule.Visible = False
        '
        'btnScanDetails
        '
        Me.btnScanDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnScanDetails.Location = New System.Drawing.Point(6, 3)
        Me.btnScanDetails.Name = "btnScanDetails"
        Me.btnScanDetails.Size = New System.Drawing.Size(90, 20)
        Me.btnScanDetails.TabIndex = 3
        Me.btnScanDetails.Text = "Details"
        '
        'cmbShop
        '
        Me.cmbShop.Location = New System.Drawing.Point(80, 30)
        Me.cmbShop.Name = "cmbShop"
        Me.cmbShop.Size = New System.Drawing.Size(200, 23)
        Me.cmbShop.TabIndex = 1
        '
        'Label62
        '
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label62.Location = New System.Drawing.Point(0, 205)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(320, 20)
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblStatusMsg
        '
        Me.lblStatusMsg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusMsg.BackColor = System.Drawing.Color.Transparent
        Me.lblStatusMsg.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusMsg.Location = New System.Drawing.Point(0, 180)
        Me.lblStatusMsg.Name = "lblStatusMsg"
        Me.lblStatusMsg.Size = New System.Drawing.Size(320, 22)
        Me.lblStatusMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTotalScanned
        '
        Me.lblTotalScanned.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblTotalScanned.Location = New System.Drawing.Point(100, 225)
        Me.lblTotalScanned.Name = "lblTotalScanned"
        Me.lblTotalScanned.Size = New System.Drawing.Size(40, 20)
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label21.Location = New System.Drawing.Point(10, 225)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(90, 20)
        Me.Label21.Text = "Total Scanned :"
        '
        'btnScanSubmit
        '
        Me.btnScanSubmit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnScanSubmit.Location = New System.Drawing.Point(223, 3)
        Me.btnScanSubmit.Name = "btnScanSubmit"
        Me.btnScanSubmit.Size = New System.Drawing.Size(90, 20)
        Me.btnScanSubmit.TabIndex = 4
        Me.btnScanSubmit.Text = "Submit"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.LightBlue
        Me.Panel8.Controls.Add(Me.txtOrderNo)
        Me.Panel8.Controls.Add(Me.txtModuleNo)
        Me.Panel8.Controls.Add(Me.Label37)
        Me.Panel8.Controls.Add(Me.Label36)
        Me.Panel8.Location = New System.Drawing.Point(10, 80)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(293, 75)
        '
        'txtOrderNo
        '
        Me.txtOrderNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtOrderNo.Location = New System.Drawing.Point(75, 30)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(180, 15)
        '
        'txtModuleNo
        '
        Me.txtModuleNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtModuleNo.Location = New System.Drawing.Point(75, 10)
        Me.txtModuleNo.Name = "txtModuleNo"
        Me.txtModuleNo.Size = New System.Drawing.Size(180, 15)
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label37.Location = New System.Drawing.Point(7, 30)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(72, 15)
        Me.Label37.Text = "Order No :"
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label36.Location = New System.Drawing.Point(7, 10)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(72, 15)
        Me.Label36.Text = "Module No :"
        '
        'Panel12
        '
        Me.Panel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel12.Controls.Add(Me.btnBackBPScanModule)
        Me.Panel12.Controls.Add(Me.Label14)
        Me.Panel12.Location = New System.Drawing.Point(0, 247)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(320, 24)
        '
        'btnBackBPScanModule
        '
        Me.btnBackBPScanModule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBackBPScanModule.Location = New System.Drawing.Point(200, 2)
        Me.btnBackBPScanModule.Name = "btnBackBPScanModule"
        Me.btnBackBPScanModule.Size = New System.Drawing.Size(110, 20)
        Me.btnBackBPScanModule.TabIndex = 5
        Me.btnBackBPScanModule.Text = "Back"
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(0, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(209, 18)
        Me.Label14.Text = "USER NAME:"
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label33.Location = New System.Drawing.Point(10, 55)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(70, 15)
        Me.Label33.Text = "Module QR :"
        '
        'btnFScanModule
        '
        Me.btnFScanModule.Image = CType(resources.GetObject("btnFScanModule.Image"), System.Drawing.Image)
        Me.btnFScanModule.Location = New System.Drawing.Point(283, 54)
        Me.btnFScanModule.Name = "btnFScanModule"
        Me.btnFScanModule.Size = New System.Drawing.Size(30, 22)
        Me.btnFScanModule.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'txtModuleQR
        '
        Me.txtModuleQR.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtModuleQR.Location = New System.Drawing.Point(80, 55)
        Me.txtModuleQR.MaxLength = 24
        Me.txtModuleQR.Name = "txtModuleQR"
        Me.txtModuleQR.Size = New System.Drawing.Size(200, 19)
        Me.txtModuleQR.TabIndex = 2
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label34.Location = New System.Drawing.Point(8, 34)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(68, 20)
        Me.Label34.Text = "Shop :"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.PowderBlue
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label35.Location = New System.Drawing.Point(2, 2)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(316, 20)
        '
        'pnlBPAbnViewDet
        '
        Me.pnlBPAbnViewDet.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPAbnViewDet.Controls.Add(Me.Panel4)
        Me.pnlBPAbnViewDet.Controls.Add(Me.lstViewRcvDet)
        Me.pnlBPAbnViewDet.Controls.Add(Me.lblHeaderAbnVwDet)
        Me.pnlBPAbnViewDet.Location = New System.Drawing.Point(657, 284)
        Me.pnlBPAbnViewDet.Name = "pnlBPAbnViewDet"
        Me.pnlBPAbnViewDet.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPAbnViewDet.Visible = False
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.btnCloseBPViewDet)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Location = New System.Drawing.Point(0, 247)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseBPViewDet
        '
        Me.btnCloseBPViewDet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseBPViewDet.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseBPViewDet.Name = "btnCloseBPViewDet"
        Me.btnCloseBPViewDet.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseBPViewDet.TabIndex = 34
        Me.btnCloseBPViewDet.Text = "Close"
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(0, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(209, 18)
        Me.Label5.Text = "USER NAME:"
        '
        'lstViewRcvDet
        '
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader13)
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader14)
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader15)
        Me.lstViewRcvDet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewRcvDet.FullRowSelect = True
        Me.lstViewRcvDet.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstViewRcvDet.Location = New System.Drawing.Point(6, 27)
        Me.lstViewRcvDet.Name = "lstViewRcvDet"
        Me.lstViewRcvDet.Size = New System.Drawing.Size(309, 222)
        Me.lstViewRcvDet.TabIndex = 55
        Me.lstViewRcvDet.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "No"
        Me.ColumnHeader13.Width = 30
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Module No"
        Me.ColumnHeader14.Width = 80
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "Order No"
        Me.ColumnHeader15.Width = 195
        '
        'lblHeaderAbnVwDet
        '
        Me.lblHeaderAbnVwDet.BackColor = System.Drawing.Color.PowderBlue
        Me.lblHeaderAbnVwDet.Location = New System.Drawing.Point(2, 4)
        Me.lblHeaderAbnVwDet.Name = "lblHeaderAbnVwDet"
        Me.lblHeaderAbnVwDet.Size = New System.Drawing.Size(315, 20)
        Me.lblHeaderAbnVwDet.Text = "Total Record :"
        Me.lblHeaderAbnVwDet.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pnlBPPosting
        '
        Me.pnlBPPosting.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPPosting.Controls.Add(Me.lblPostingTotalPdgAbn)
        Me.pnlBPPosting.Controls.Add(Me.Label26)
        Me.pnlBPPosting.Controls.Add(Me.Label25)
        Me.pnlBPPosting.Controls.Add(Me.btnBPSubmitPosting)
        Me.pnlBPPosting.Controls.Add(Me.Panel13)
        Me.pnlBPPosting.Controls.Add(Me.lstViewPosting)
        Me.pnlBPPosting.Controls.Add(Me.Label24)
        Me.pnlBPPosting.Location = New System.Drawing.Point(983, 284)
        Me.pnlBPPosting.Name = "pnlBPPosting"
        Me.pnlBPPosting.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPPosting.Visible = False
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label26.Location = New System.Drawing.Point(2, 50)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(280, 15)
        Me.Label26.Text = "Total Pending Posting Records :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label25.Location = New System.Drawing.Point(2, 30)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(316, 15)
        Me.Label25.Text = "Posting Supply Big Part Data"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnBPSubmitPosting
        '
        Me.btnBPSubmitPosting.Location = New System.Drawing.Point(0, 224)
        Me.btnBPSubmitPosting.Name = "btnBPSubmitPosting"
        Me.btnBPSubmitPosting.Size = New System.Drawing.Size(320, 24)
        Me.btnBPSubmitPosting.TabIndex = 83
        Me.btnBPSubmitPosting.Text = "Submit"
        '
        'Panel13
        '
        Me.Panel13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel13.Controls.Add(Me.btnCloseBPPosting)
        Me.Panel13.Controls.Add(Me.Label23)
        Me.Panel13.Location = New System.Drawing.Point(0, 247)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseBPPosting
        '
        Me.btnCloseBPPosting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseBPPosting.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseBPPosting.Name = "btnCloseBPPosting"
        Me.btnCloseBPPosting.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseBPPosting.TabIndex = 34
        Me.btnCloseBPPosting.Text = "Close"
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(0, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(209, 18)
        Me.Label23.Text = "USER NAME:"
        '
        'lstViewPosting
        '
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader3)
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader4)
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader5)
        Me.lstViewPosting.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewPosting.FullRowSelect = True
        Me.lstViewPosting.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstViewPosting.Location = New System.Drawing.Point(6, 67)
        Me.lstViewPosting.Name = "lstViewPosting"
        Me.lstViewPosting.Size = New System.Drawing.Size(309, 157)
        Me.lstViewPosting.TabIndex = 55
        Me.lstViewPosting.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "No"
        Me.ColumnHeader3.Width = 30
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Module No"
        Me.ColumnHeader4.Width = 80
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Order No"
        Me.ColumnHeader5.Width = 195
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.PowderBlue
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label24.Location = New System.Drawing.Point(2, 4)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(315, 20)
        '
        'pnlBPDelete
        '
        Me.pnlBPDelete.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPDelete.Controls.Add(Me.lblDeleteTotalAbn)
        Me.pnlBPDelete.Controls.Add(Me.Label6)
        Me.pnlBPDelete.Controls.Add(Me.Label7)
        Me.pnlBPDelete.Controls.Add(Me.btnDelete)
        Me.pnlBPDelete.Controls.Add(Me.Panel7)
        Me.pnlBPDelete.Controls.Add(Me.lstViewDelete)
        Me.pnlBPDelete.Controls.Add(Me.Label28)
        Me.pnlBPDelete.Location = New System.Drawing.Point(1309, 284)
        Me.pnlBPDelete.Name = "pnlBPDelete"
        Me.pnlBPDelete.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPDelete.Visible = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label6.Location = New System.Drawing.Point(2, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(270, 15)
        Me.Label6.Text = "Total Records :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label7.Location = New System.Drawing.Point(2, 30)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(316, 15)
        Me.Label7.Text = "Delete Supply Big Part Data"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(0, 224)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(320, 24)
        Me.btnDelete.TabIndex = 83
        Me.btnDelete.Text = "Delete"
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel7.Controls.Add(Me.btnCloseBPDelete)
        Me.Panel7.Controls.Add(Me.Label27)
        Me.Panel7.Location = New System.Drawing.Point(0, 247)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseBPDelete
        '
        Me.btnCloseBPDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseBPDelete.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseBPDelete.Name = "btnCloseBPDelete"
        Me.btnCloseBPDelete.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseBPDelete.TabIndex = 34
        Me.btnCloseBPDelete.Text = "Close"
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(0, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(209, 18)
        Me.Label27.Text = "USER NAME:"
        '
        'lstViewDelete
        '
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader7)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader8)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader9)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader10)
        Me.lstViewDelete.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewDelete.FullRowSelect = True
        Me.lstViewDelete.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstViewDelete.Location = New System.Drawing.Point(6, 67)
        Me.lstViewDelete.Name = "lstViewDelete"
        Me.lstViewDelete.Size = New System.Drawing.Size(309, 157)
        Me.lstViewDelete.TabIndex = 55
        Me.lstViewDelete.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "No"
        Me.ColumnHeader7.Width = 30
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Module No"
        Me.ColumnHeader8.Width = 80
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Order No"
        Me.ColumnHeader9.Width = 145
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Shop"
        Me.ColumnHeader10.Width = 50
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.PowderBlue
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label28.Location = New System.Drawing.Point(2, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(315, 20)
        '
        'pnlBPAbn
        '
        Me.pnlBPAbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPAbn.Controls.Add(Me.Panel6)
        Me.pnlBPAbn.Controls.Add(Me.Panel14)
        Me.pnlBPAbn.Controls.Add(Me.Label39)
        Me.pnlBPAbn.Controls.Add(Me.Label40)
        Me.pnlBPAbn.Controls.Add(Me.Label41)
        Me.pnlBPAbn.Controls.Add(Me.Label42)
        Me.pnlBPAbn.Controls.Add(Me.btnBPAbnDelete)
        Me.pnlBPAbn.Controls.Add(Me.btnBPAbnPost)
        Me.pnlBPAbn.Controls.Add(Me.btnBPAbnView)
        Me.pnlBPAbn.Controls.Add(Me.btnBPAbnScan)
        Me.pnlBPAbn.Location = New System.Drawing.Point(5, 284)
        Me.pnlBPAbn.Name = "pnlBPAbn"
        Me.pnlBPAbn.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPAbn.Visible = False
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel6.Controls.Add(Me.btnCloseAbnBP)
        Me.Panel6.Controls.Add(Me.Label32)
        Me.Panel6.Location = New System.Drawing.Point(0, 247)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseAbnBP
        '
        Me.btnCloseAbnBP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseAbnBP.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseAbnBP.Name = "btnCloseAbnBP"
        Me.btnCloseAbnBP.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseAbnBP.TabIndex = 34
        Me.btnCloseAbnBP.Text = "Close"
        '
        'Label32
        '
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(0, 3)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(209, 18)
        Me.Label32.Text = "USER NAME:"
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(320, 24)
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label39.Location = New System.Drawing.Point(193, 227)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(80, 20)
        Me.Label39.Text = "Delete"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label40.Location = New System.Drawing.Point(65, 227)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(80, 20)
        Me.Label40.Text = "Post"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label41.Location = New System.Drawing.Point(193, 119)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(80, 20)
        Me.Label41.Text = "View"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label42.Location = New System.Drawing.Point(65, 119)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(80, 20)
        Me.Label42.Text = "Scan"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnBPAbnDelete
        '
        Me.btnBPAbnDelete.Image = CType(resources.GetObject("btnBPAbnDelete.Image"), System.Drawing.Image)
        Me.btnBPAbnDelete.Location = New System.Drawing.Point(193, 144)
        Me.btnBPAbnDelete.Name = "btnBPAbnDelete"
        Me.btnBPAbnDelete.Size = New System.Drawing.Size(80, 80)
        Me.btnBPAbnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnBPAbnPost
        '
        Me.btnBPAbnPost.Image = CType(resources.GetObject("btnBPAbnPost.Image"), System.Drawing.Image)
        Me.btnBPAbnPost.Location = New System.Drawing.Point(65, 144)
        Me.btnBPAbnPost.Name = "btnBPAbnPost"
        Me.btnBPAbnPost.Size = New System.Drawing.Size(80, 80)
        Me.btnBPAbnPost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnBPAbnView
        '
        Me.btnBPAbnView.Image = CType(resources.GetObject("btnBPAbnView.Image"), System.Drawing.Image)
        Me.btnBPAbnView.Location = New System.Drawing.Point(193, 36)
        Me.btnBPAbnView.Name = "btnBPAbnView"
        Me.btnBPAbnView.Size = New System.Drawing.Size(80, 80)
        Me.btnBPAbnView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnBPAbnScan
        '
        Me.btnBPAbnScan.Image = CType(resources.GetObject("btnBPAbnScan.Image"), System.Drawing.Image)
        Me.btnBPAbnScan.Location = New System.Drawing.Point(65, 36)
        Me.btnBPAbnScan.Name = "btnBPAbnScan"
        Me.btnBPAbnScan.Size = New System.Drawing.Size(80, 80)
        Me.btnBPAbnScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'pnlBPViewDet
        '
        Me.pnlBPViewDet.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPViewDet.Controls.Add(Me.Panel5)
        Me.pnlBPViewDet.Controls.Add(Me.TabBPSummary)
        Me.pnlBPViewDet.Controls.Add(Me.lblHeaderVwDet)
        Me.pnlBPViewDet.Location = New System.Drawing.Point(983, 3)
        Me.pnlBPViewDet.Name = "pnlBPViewDet"
        Me.pnlBPViewDet.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPViewDet.Visible = False
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.btnBackBPViewDet)
        Me.Panel5.Controls.Add(Me.Label54)
        Me.Panel5.Location = New System.Drawing.Point(0, 247)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(320, 24)
        '
        'btnBackBPViewDet
        '
        Me.btnBackBPViewDet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBackBPViewDet.Location = New System.Drawing.Point(200, 2)
        Me.btnBackBPViewDet.Name = "btnBackBPViewDet"
        Me.btnBackBPViewDet.Size = New System.Drawing.Size(110, 20)
        Me.btnBackBPViewDet.TabIndex = 34
        Me.btnBackBPViewDet.Text = "Back"
        '
        'Label54
        '
        Me.Label54.ForeColor = System.Drawing.Color.Black
        Me.Label54.Location = New System.Drawing.Point(0, 3)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(209, 18)
        Me.Label54.Text = "USER NAME:"
        '
        'TabBPSummary
        '
        Me.TabBPSummary.Controls.Add(Me.TabPage4)
        Me.TabBPSummary.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.TabBPSummary.Location = New System.Drawing.Point(3, 26)
        Me.TabBPSummary.Name = "TabBPSummary"
        Me.TabBPSummary.SelectedIndex = 0
        Me.TabBPSummary.Size = New System.Drawing.Size(314, 224)
        Me.TabBPSummary.TabIndex = 83
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.Window
        Me.TabPage4.Controls.Add(Me.lblDetailTotalScan)
        Me.TabPage4.Controls.Add(Me.Label47)
        Me.TabPage4.Controls.Add(Me.lstViewRCISummary)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(306, 198)
        Me.TabPage4.Text = "Scanned"
        '
        'lblDetailTotalScan
        '
        Me.lblDetailTotalScan.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDetailTotalScan.Location = New System.Drawing.Point(72, 180)
        Me.lblDetailTotalScan.Name = "lblDetailTotalScan"
        Me.lblDetailTotalScan.Size = New System.Drawing.Size(45, 21)
        '
        'Label47
        '
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label47.Location = New System.Drawing.Point(4, 180)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(62, 20)
        Me.Label47.Text = "Total Scan :"
        '
        'lstViewRCISummary
        '
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader17)
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader18)
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader19)
        Me.lstViewRCISummary.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewRCISummary.FullRowSelect = True
        Me.lstViewRCISummary.Location = New System.Drawing.Point(4, 5)
        Me.lstViewRCISummary.Name = "lstViewRCISummary"
        Me.lstViewRCISummary.Size = New System.Drawing.Size(299, 172)
        Me.lstViewRCISummary.TabIndex = 73
        Me.lstViewRCISummary.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "No"
        Me.ColumnHeader17.Width = 30
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Module No"
        Me.ColumnHeader18.Width = 80
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "Order No"
        Me.ColumnHeader19.Width = 185
        '
        'lblHeaderVwDet
        '
        Me.lblHeaderVwDet.BackColor = System.Drawing.Color.PowderBlue
        Me.lblHeaderVwDet.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeaderVwDet.Location = New System.Drawing.Point(2, 3)
        Me.lblHeaderVwDet.Name = "lblHeaderVwDet"
        Me.lblHeaderVwDet.Size = New System.Drawing.Size(315, 20)
        Me.lblHeaderVwDet.Text = "Shop :"
        '
        'pnlBPAbnScan
        '
        Me.pnlBPAbnScan.BackColor = System.Drawing.Color.Transparent
        Me.pnlBPAbnScan.Controls.Add(Me.btnBPAbnScanDet)
        Me.pnlBPAbnScan.Controls.Add(Me.cmbShopAbn)
        Me.pnlBPAbnScan.Controls.Add(Me.Label2)
        Me.pnlBPAbnScan.Controls.Add(Me.lblStatusMsgAbn)
        Me.pnlBPAbnScan.Controls.Add(Me.lblTotalScannedAbn)
        Me.pnlBPAbnScan.Controls.Add(Me.Label17)
        Me.pnlBPAbnScan.Controls.Add(Me.Panel10)
        Me.pnlBPAbnScan.Controls.Add(Me.Panel11)
        Me.pnlBPAbnScan.Controls.Add(Me.Label38)
        Me.pnlBPAbnScan.Controls.Add(Me.btnBPAbnFScan)
        Me.pnlBPAbnScan.Controls.Add(Me.txtModuleQRAbn)
        Me.pnlBPAbnScan.Controls.Add(Me.Label43)
        Me.pnlBPAbnScan.Controls.Add(Me.Label44)
        Me.pnlBPAbnScan.Location = New System.Drawing.Point(330, 284)
        Me.pnlBPAbnScan.Name = "pnlBPAbnScan"
        Me.pnlBPAbnScan.Size = New System.Drawing.Size(320, 275)
        Me.pnlBPAbnScan.Visible = False
        '
        'btnBPAbnScanDet
        '
        Me.btnBPAbnScanDet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBPAbnScanDet.Location = New System.Drawing.Point(223, 3)
        Me.btnBPAbnScanDet.Name = "btnBPAbnScanDet"
        Me.btnBPAbnScanDet.Size = New System.Drawing.Size(90, 20)
        Me.btnBPAbnScanDet.TabIndex = 100
        Me.btnBPAbnScanDet.Text = "Details"
        '
        'cmbShopAbn
        '
        Me.cmbShopAbn.Location = New System.Drawing.Point(80, 30)
        Me.cmbShopAbn.Name = "cmbShopAbn"
        Me.cmbShopAbn.Size = New System.Drawing.Size(200, 23)
        Me.cmbShopAbn.TabIndex = 98
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(0, 205)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(320, 20)
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblStatusMsgAbn
        '
        Me.lblStatusMsgAbn.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusMsgAbn.BackColor = System.Drawing.Color.Transparent
        Me.lblStatusMsgAbn.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusMsgAbn.Location = New System.Drawing.Point(0, 180)
        Me.lblStatusMsgAbn.Name = "lblStatusMsgAbn"
        Me.lblStatusMsgAbn.Size = New System.Drawing.Size(320, 22)
        Me.lblStatusMsgAbn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTotalScannedAbn
        '
        Me.lblTotalScannedAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblTotalScannedAbn.Location = New System.Drawing.Point(100, 225)
        Me.lblTotalScannedAbn.Name = "lblTotalScannedAbn"
        Me.lblTotalScannedAbn.Size = New System.Drawing.Size(40, 20)
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label17.Location = New System.Drawing.Point(10, 225)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 20)
        Me.Label17.Text = "Total Scanned :"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.LightBlue
        Me.Panel10.Controls.Add(Me.txtOrderNoAbn)
        Me.Panel10.Controls.Add(Me.txtModuleNoAbn)
        Me.Panel10.Controls.Add(Me.Label29)
        Me.Panel10.Controls.Add(Me.Label30)
        Me.Panel10.Location = New System.Drawing.Point(10, 80)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(293, 75)
        '
        'txtOrderNoAbn
        '
        Me.txtOrderNoAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtOrderNoAbn.Location = New System.Drawing.Point(75, 30)
        Me.txtOrderNoAbn.Name = "txtOrderNoAbn"
        Me.txtOrderNoAbn.Size = New System.Drawing.Size(180, 15)
        '
        'txtModuleNoAbn
        '
        Me.txtModuleNoAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtModuleNoAbn.Location = New System.Drawing.Point(75, 10)
        Me.txtModuleNoAbn.Name = "txtModuleNoAbn"
        Me.txtModuleNoAbn.Size = New System.Drawing.Size(180, 15)
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label29.Location = New System.Drawing.Point(7, 30)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(72, 15)
        Me.Label29.Text = "Order No :"
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label30.Location = New System.Drawing.Point(7, 10)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(72, 15)
        Me.Label30.Text = "Module No :"
        '
        'Panel11
        '
        Me.Panel11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel11.Controls.Add(Me.btnBackBPAbnScan)
        Me.Panel11.Controls.Add(Me.Label31)
        Me.Panel11.Location = New System.Drawing.Point(0, 247)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(320, 24)
        '
        'btnBackBPAbnScan
        '
        Me.btnBackBPAbnScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBackBPAbnScan.Location = New System.Drawing.Point(200, 2)
        Me.btnBackBPAbnScan.Name = "btnBackBPAbnScan"
        Me.btnBackBPAbnScan.Size = New System.Drawing.Size(110, 20)
        Me.btnBackBPAbnScan.TabIndex = 34
        Me.btnBackBPAbnScan.Text = "Back"
        '
        'Label31
        '
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(0, 3)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(209, 18)
        Me.Label31.Text = "USER NAME:"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label38.Location = New System.Drawing.Point(10, 55)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(70, 15)
        Me.Label38.Text = "Module QR :"
        '
        'btnBPAbnFScan
        '
        Me.btnBPAbnFScan.Image = CType(resources.GetObject("btnBPAbnFScan.Image"), System.Drawing.Image)
        Me.btnBPAbnFScan.Location = New System.Drawing.Point(283, 54)
        Me.btnBPAbnFScan.Name = "btnBPAbnFScan"
        Me.btnBPAbnFScan.Size = New System.Drawing.Size(30, 22)
        Me.btnBPAbnFScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'txtModuleQRAbn
        '
        Me.txtModuleQRAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtModuleQRAbn.Location = New System.Drawing.Point(80, 55)
        Me.txtModuleQRAbn.Name = "txtModuleQRAbn"
        Me.txtModuleQRAbn.Size = New System.Drawing.Size(200, 19)
        Me.txtModuleQRAbn.TabIndex = 13
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label43.Location = New System.Drawing.Point(8, 34)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(68, 20)
        Me.Label43.Text = "Shop :"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.PowderBlue
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label44.Location = New System.Drawing.Point(2, 2)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(316, 20)
        '
        'lblPostingTotalPdgAbn
        '
        Me.lblPostingTotalPdgAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblPostingTotalPdgAbn.Location = New System.Drawing.Point(228, 50)
        Me.lblPostingTotalPdgAbn.Name = "lblPostingTotalPdgAbn"
        Me.lblPostingTotalPdgAbn.Size = New System.Drawing.Size(70, 15)
        '
        'lblDeleteTotalAbn
        '
        Me.lblDeleteTotalAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDeleteTotalAbn.Location = New System.Drawing.Point(183, 50)
        Me.lblDeleteTotalAbn.Name = "lblDeleteTotalAbn"
        Me.lblDeleteTotalAbn.Size = New System.Drawing.Size(80, 15)
        '
        'frmBigPart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(638, 455)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlBPAbnScan)
        Me.Controls.Add(Me.pnlBPViewDet)
        Me.Controls.Add(Me.pnlBPAbn)
        Me.Controls.Add(Me.pnlBPDelete)
        Me.Controls.Add(Me.pnlBPPosting)
        Me.Controls.Add(Me.pnlBPAbnViewDet)
        Me.Controls.Add(Me.pnlBPScanModule)
        Me.Controls.Add(Me.pnlBPFScan)
        Me.Controls.Add(Me.footerStatusBar)
        Me.Controls.Add(Me.pnlBPMain)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBigPart"
        Me.Text = "DCS JSP"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlBPMain.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.pnlBPFScan.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlBPScanModule.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.pnlBPAbnViewDet.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlBPPosting.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.pnlBPDelete.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlBPAbn.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnlBPViewDet.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.TabBPSummary.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.pnlBPAbnScan.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlBPMain As System.Windows.Forms.Panel
    Friend WithEvents footerStatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents TimerCheckOnline As System.Windows.Forms.Timer
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnScanBP As System.Windows.Forms.PictureBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseBP As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnAbnormalBP As System.Windows.Forms.PictureBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlBPFScan As System.Windows.Forms.Panel
    Friend WithEvents lstViewRCVFScan As System.Windows.Forms.ListView
    Friend WithEvents REASON_CODE As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtFSOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSaveForceScan As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnBackFScan As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlBPScanModule As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnBackBPScanModule As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnFScanModule As System.Windows.Forms.PictureBox
    Friend WithEvents txtModuleQR As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents txtOrderNo As System.Windows.Forms.Label
    Friend WithEvents txtModuleNo As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents btnScanSubmit As System.Windows.Forms.Button
    Friend WithEvents pnlBPAbnViewDet As System.Windows.Forms.Panel
    Friend WithEvents lstViewRcvDet As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblHeaderAbnVwDet As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseBPViewDet As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTotalScanned As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pnlBPPosting As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseBPPosting As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lstViewPosting As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnBPSubmitPosting As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnlBPDelete As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseBPDelete As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lstViewDelete As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents pnlBPAbn As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseAbnBP As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents btnBPAbnDelete As System.Windows.Forms.PictureBox
    Friend WithEvents btnBPAbnPost As System.Windows.Forms.PictureBox
    Friend WithEvents btnBPAbnView As System.Windows.Forms.PictureBox
    Friend WithEvents btnBPAbnScan As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBPViewDet As System.Windows.Forms.Panel
    Friend WithEvents TabBPSummary As System.Windows.Forms.TabControl
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents lblDetailTotalScan As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lstViewRCISummary As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblHeaderVwDet As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnBackBPViewDet As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents lblStatusMsg As System.Windows.Forms.Label
    Friend WithEvents txtFSModuleNo As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents cmbShop As System.Windows.Forms.ComboBox
    Friend WithEvents btnScanDetails As System.Windows.Forms.Button
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnlBPAbnScan As System.Windows.Forms.Panel
    Friend WithEvents btnBPAbnScanDet As System.Windows.Forms.Button
    Friend WithEvents cmbShopAbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblStatusMsgAbn As System.Windows.Forms.Label
    Friend WithEvents lblTotalScannedAbn As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents txtOrderNoAbn As System.Windows.Forms.Label
    Friend WithEvents txtModuleNoAbn As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents btnBackBPAbnScan As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents btnBPAbnFScan As System.Windows.Forms.PictureBox
    Friend WithEvents txtModuleQRAbn As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblPostingTotalPdgAbn As System.Windows.Forms.Label
    Friend WithEvents lblDeleteTotalAbn As System.Windows.Forms.Label

End Class
