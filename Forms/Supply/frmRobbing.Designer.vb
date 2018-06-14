<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRobbing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRobbing))
        Me.pnlRBMain = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnAbnormalRB = New System.Windows.Forms.PictureBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnScanRB = New System.Windows.Forms.PictureBox
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.btnCloseRB = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.footerStatusBar = New System.Windows.Forms.StatusBar
        Me.TimerCheckOnline = New System.Windows.Forms.Timer
        Me.pnlRBFScan = New System.Windows.Forms.Panel
        Me.txtFSBranch = New System.Windows.Forms.TextBox
        Me.txtFSSeqNo = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFSPartNo = New System.Windows.Forms.TextBox
        Me.Label76 = New System.Windows.Forms.Label
        Me.btnSaveForceScan = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnBackFScan = New System.Windows.Forms.Button
        Me.lstViewRCVFScan = New System.Windows.Forms.ListView
        Me.REASON_CODE = New System.Windows.Forms.ColumnHeader
        Me.txtFSModuleNo = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlRBScanPart = New System.Windows.Forms.Panel
        Me.btnScanDetails = New System.Windows.Forms.Button
        Me.lblRBStatusMsgDesc = New System.Windows.Forms.Label
        Me.lblRBStatusMsg = New System.Windows.Forms.Label
        Me.lblTotalScanned = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.btnScanSubmit = New System.Windows.Forms.Button
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.txtQty = New System.Windows.Forms.Label
        Me.txtBranchNo = New System.Windows.Forms.Label
        Me.txtSeqNo = New System.Windows.Forms.Label
        Me.txtPartNo = New System.Windows.Forms.Label
        Me.Label80 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnBackRBScanPart = New System.Windows.Forms.Button
        Me.Label33 = New System.Windows.Forms.Label
        Me.btnFScanPart = New System.Windows.Forms.PictureBox
        Me.txtPxPQR = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.pnlRBAbnViewDet = New System.Windows.Forms.Panel
        Me.lblHeaderAbnVwDet = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnCloseRBViewDet = New System.Windows.Forms.Button
        Me.lstViewRcvDet = New System.Windows.Forms.ListView
        Me.ColumnHeader13 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader14 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader15 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader21 = New System.Windows.Forms.ColumnHeader
        Me.Label61 = New System.Windows.Forms.Label
        Me.pnlRBPosting = New System.Windows.Forms.Panel
        Me.lblPostingTotalPdgAbn = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.btnRBSubmitPosting = New System.Windows.Forms.Button
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.lblUsername = New System.Windows.Forms.Label
        Me.btnCloseRBPosting = New System.Windows.Forms.Button
        Me.lstViewPosting = New System.Windows.Forms.ListView
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader20 = New System.Windows.Forms.ColumnHeader
        Me.Label24 = New System.Windows.Forms.Label
        Me.pnlRBDelete = New System.Windows.Forms.Panel
        Me.lblDeleteTotalAbn = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.btnCloseRBDelete = New System.Windows.Forms.Button
        Me.lstViewDelete = New System.Windows.Forms.ListView
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader16 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.Label28 = New System.Windows.Forms.Label
        Me.pnlRBAbn = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.btnCloseAbnRB = New System.Windows.Forms.Button
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.btnRBAbnDelete = New System.Windows.Forms.PictureBox
        Me.btnRBAbnPost = New System.Windows.Forms.PictureBox
        Me.btnRBAbnView = New System.Windows.Forms.PictureBox
        Me.btnRBAbnScan = New System.Windows.Forms.PictureBox
        Me.pnlRBViewDet = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnBackRBViewDet = New System.Windows.Forms.Button
        Me.TabRBSummary = New System.Windows.Forms.TabControl
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.lblDetailTotalScan = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.lstViewRCISummary = New System.Windows.Forms.ListView
        Me.ColumnHeader17 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader19 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader18 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader12 = New System.Windows.Forms.ColumnHeader
        Me.Label53 = New System.Windows.Forms.Label
        Me.pnlRBAbnScan = New System.Windows.Forms.Panel
        Me.btnRBAbnScanDet = New System.Windows.Forms.Button
        Me.lblRBStatusMsgDescAbn = New System.Windows.Forms.Label
        Me.lblRBStatusMsgAbn = New System.Windows.Forms.Label
        Me.lblTotalScannedAbn = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.txtQtyAbn = New System.Windows.Forms.Label
        Me.txtBranchNoAbn = New System.Windows.Forms.Label
        Me.txtSeqNoAbn = New System.Windows.Forms.Label
        Me.txtPartNoAbn = New System.Windows.Forms.Label
        Me.Label83 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.btnBackRBAbnScan = New System.Windows.Forms.Button
        Me.Label38 = New System.Windows.Forms.Label
        Me.btnRBAbnFScan = New System.Windows.Forms.PictureBox
        Me.txtPxPQRAbn = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.pnlLogin = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnLoginClose = New System.Windows.Forms.Button
        Me.btnLoginSubmit = New System.Windows.Forms.Button
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPwd = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlRBMain.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlRBFScan.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlRBScanPart.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.pnlRBAbnViewDet.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlRBPosting.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.pnlRBDelete.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlRBAbn.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlRBViewDet.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TabRBSummary.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.pnlRBAbnScan.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.pnlLogin.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlRBMain
        '
        Me.pnlRBMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBMain.Controls.Add(Me.Label12)
        Me.pnlRBMain.Controls.Add(Me.btnAbnormalRB)
        Me.pnlRBMain.Controls.Add(Me.Label10)
        Me.pnlRBMain.Controls.Add(Me.btnScanRB)
        Me.pnlRBMain.Controls.Add(Me.Panel9)
        Me.pnlRBMain.Controls.Add(Me.Panel3)
        Me.pnlRBMain.Location = New System.Drawing.Point(5, 3)
        Me.pnlRBMain.Name = "pnlRBMain"
        Me.pnlRBMain.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBMain.Visible = False
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
        'btnAbnormalRB
        '
        Me.btnAbnormalRB.Image = CType(resources.GetObject("btnAbnormalRB.Image"), System.Drawing.Image)
        Me.btnAbnormalRB.Location = New System.Drawing.Point(193, 36)
        Me.btnAbnormalRB.Name = "btnAbnormalRB"
        Me.btnAbnormalRB.Size = New System.Drawing.Size(80, 80)
        Me.btnAbnormalRB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
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
        'btnScanRB
        '
        Me.btnScanRB.Image = CType(resources.GetObject("btnScanRB.Image"), System.Drawing.Image)
        Me.btnScanRB.Location = New System.Drawing.Point(65, 36)
        Me.btnScanRB.Name = "btnScanRB"
        Me.btnScanRB.Size = New System.Drawing.Size(80, 80)
        Me.btnScanRB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel9.Controls.Add(Me.btnCloseRB)
        Me.Panel9.Location = New System.Drawing.Point(0, 247)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseRB
        '
        Me.btnCloseRB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseRB.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseRB.Name = "btnCloseRB"
        Me.btnCloseRB.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseRB.TabIndex = 1
        Me.btnCloseRB.TabStop = False
        Me.btnCloseRB.Text = "Close"
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
        'pnlRBFScan
        '
        Me.pnlRBFScan.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBFScan.Controls.Add(Me.txtFSBranch)
        Me.pnlRBFScan.Controls.Add(Me.txtFSSeqNo)
        Me.pnlRBFScan.Controls.Add(Me.Label11)
        Me.pnlRBFScan.Controls.Add(Me.Label3)
        Me.pnlRBFScan.Controls.Add(Me.txtFSPartNo)
        Me.pnlRBFScan.Controls.Add(Me.Label76)
        Me.pnlRBFScan.Controls.Add(Me.btnSaveForceScan)
        Me.pnlRBFScan.Controls.Add(Me.Panel1)
        Me.pnlRBFScan.Controls.Add(Me.lstViewRCVFScan)
        Me.pnlRBFScan.Controls.Add(Me.txtFSModuleNo)
        Me.pnlRBFScan.Controls.Add(Me.Label16)
        Me.pnlRBFScan.Controls.Add(Me.Label1)
        Me.pnlRBFScan.Location = New System.Drawing.Point(657, 3)
        Me.pnlRBFScan.Name = "pnlRBFScan"
        Me.pnlRBFScan.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBFScan.Visible = False
        '
        'txtFSBranch
        '
        Me.txtFSBranch.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtFSBranch.Location = New System.Drawing.Point(213, 86)
        Me.txtFSBranch.MaxLength = 2
        Me.txtFSBranch.Name = "txtFSBranch"
        Me.txtFSBranch.Size = New System.Drawing.Size(77, 19)
        Me.txtFSBranch.TabIndex = 4
        '
        'txtFSSeqNo
        '
        Me.txtFSSeqNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtFSSeqNo.Location = New System.Drawing.Point(75, 86)
        Me.txtFSSeqNo.MaxLength = 2
        Me.txtFSSeqNo.Name = "txtFSSeqNo"
        Me.txtFSSeqNo.Size = New System.Drawing.Size(77, 19)
        Me.txtFSSeqNo.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label11.Location = New System.Drawing.Point(158, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 20)
        Me.Label11.Text = "Branch :"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.Location = New System.Drawing.Point(8, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 20)
        Me.Label3.Text = "Seq No :"
        '
        'txtFSPartNo
        '
        Me.txtFSPartNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtFSPartNo.Location = New System.Drawing.Point(75, 60)
        Me.txtFSPartNo.MaxLength = 14
        Me.txtFSPartNo.Name = "txtFSPartNo"
        Me.txtFSPartNo.Size = New System.Drawing.Size(235, 19)
        Me.txtFSPartNo.TabIndex = 2
        '
        'Label76
        '
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label76.Location = New System.Drawing.Point(8, 60)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(68, 20)
        Me.Label76.Text = "Part No :"
        '
        'btnSaveForceScan
        '
        Me.btnSaveForceScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnSaveForceScan.Location = New System.Drawing.Point(223, 4)
        Me.btnSaveForceScan.Name = "btnSaveForceScan"
        Me.btnSaveForceScan.Size = New System.Drawing.Size(90, 20)
        Me.btnSaveForceScan.TabIndex = 6
        Me.btnSaveForceScan.Text = "Save"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnBackFScan)
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
        Me.btnBackFScan.TabIndex = 7
        Me.btnBackFScan.TabStop = False
        Me.btnBackFScan.Text = "Back"
        '
        'lstViewRCVFScan
        '
        Me.lstViewRCVFScan.Columns.Add(Me.REASON_CODE)
        Me.lstViewRCVFScan.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewRCVFScan.FullRowSelect = True
        Me.lstViewRCVFScan.Location = New System.Drawing.Point(8, 130)
        Me.lstViewRCVFScan.Name = "lstViewRCVFScan"
        Me.lstViewRCVFScan.Size = New System.Drawing.Size(305, 80)
        Me.lstViewRCVFScan.TabIndex = 5
        Me.lstViewRCVFScan.View = System.Windows.Forms.View.Details
        '
        'REASON_CODE
        '
        Me.REASON_CODE.Text = "Reason"
        Me.REASON_CODE.Width = 302
        '
        'txtFSModuleNo
        '
        Me.txtFSModuleNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtFSModuleNo.Location = New System.Drawing.Point(75, 34)
        Me.txtFSModuleNo.MaxLength = 6
        Me.txtFSModuleNo.Name = "txtFSModuleNo"
        Me.txtFSModuleNo.Size = New System.Drawing.Size(235, 19)
        Me.txtFSModuleNo.TabIndex = 1
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
        'pnlRBScanPart
        '
        Me.pnlRBScanPart.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBScanPart.Controls.Add(Me.btnScanDetails)
        Me.pnlRBScanPart.Controls.Add(Me.lblRBStatusMsgDesc)
        Me.pnlRBScanPart.Controls.Add(Me.lblRBStatusMsg)
        Me.pnlRBScanPart.Controls.Add(Me.lblTotalScanned)
        Me.pnlRBScanPart.Controls.Add(Me.Label21)
        Me.pnlRBScanPart.Controls.Add(Me.btnScanSubmit)
        Me.pnlRBScanPart.Controls.Add(Me.Panel8)
        Me.pnlRBScanPart.Controls.Add(Me.Panel12)
        Me.pnlRBScanPart.Controls.Add(Me.Label33)
        Me.pnlRBScanPart.Controls.Add(Me.btnFScanPart)
        Me.pnlRBScanPart.Controls.Add(Me.txtPxPQR)
        Me.pnlRBScanPart.Controls.Add(Me.Label35)
        Me.pnlRBScanPart.Location = New System.Drawing.Point(331, 3)
        Me.pnlRBScanPart.Name = "pnlRBScanPart"
        Me.pnlRBScanPart.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBScanPart.Visible = False
        '
        'btnScanDetails
        '
        Me.btnScanDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnScanDetails.Location = New System.Drawing.Point(6, 3)
        Me.btnScanDetails.Name = "btnScanDetails"
        Me.btnScanDetails.Size = New System.Drawing.Size(90, 20)
        Me.btnScanDetails.TabIndex = 2
        Me.btnScanDetails.Text = "Details"
        '
        'lblRBStatusMsgDesc
        '
        Me.lblRBStatusMsgDesc.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblRBStatusMsgDesc.Location = New System.Drawing.Point(0, 205)
        Me.lblRBStatusMsgDesc.Name = "lblRBStatusMsgDesc"
        Me.lblRBStatusMsgDesc.Size = New System.Drawing.Size(320, 20)
        Me.lblRBStatusMsgDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblRBStatusMsg
        '
        Me.lblRBStatusMsg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRBStatusMsg.BackColor = System.Drawing.Color.Transparent
        Me.lblRBStatusMsg.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblRBStatusMsg.Location = New System.Drawing.Point(0, 180)
        Me.lblRBStatusMsg.Name = "lblRBStatusMsg"
        Me.lblRBStatusMsg.Size = New System.Drawing.Size(320, 22)
        Me.lblRBStatusMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTotalScanned
        '
        Me.lblTotalScanned.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblTotalScanned.Location = New System.Drawing.Point(100, 225)
        Me.lblTotalScanned.Name = "lblTotalScanned"
        Me.lblTotalScanned.Size = New System.Drawing.Size(40, 20)
        Me.lblTotalScanned.Text = "0"
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
        Me.btnScanSubmit.TabIndex = 3
        Me.btnScanSubmit.Text = "Submit"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.LightBlue
        Me.Panel8.Controls.Add(Me.txtQty)
        Me.Panel8.Controls.Add(Me.txtBranchNo)
        Me.Panel8.Controls.Add(Me.txtSeqNo)
        Me.Panel8.Controls.Add(Me.txtPartNo)
        Me.Panel8.Controls.Add(Me.Label80)
        Me.Panel8.Controls.Add(Me.Label9)
        Me.Panel8.Controls.Add(Me.Label37)
        Me.Panel8.Controls.Add(Me.Label36)
        Me.Panel8.Location = New System.Drawing.Point(10, 80)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(293, 85)
        '
        'txtQty
        '
        Me.txtQty.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtQty.Location = New System.Drawing.Point(73, 45)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(80, 15)
        '
        'txtBranchNo
        '
        Me.txtBranchNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtBranchNo.Location = New System.Drawing.Point(75, 64)
        Me.txtBranchNo.Name = "txtBranchNo"
        Me.txtBranchNo.Size = New System.Drawing.Size(80, 15)
        '
        'txtSeqNo
        '
        Me.txtSeqNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtSeqNo.Location = New System.Drawing.Point(73, 26)
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.Size = New System.Drawing.Size(180, 15)
        '
        'txtPartNo
        '
        Me.txtPartNo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtPartNo.Location = New System.Drawing.Point(73, 7)
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.Size = New System.Drawing.Size(180, 15)
        '
        'Label80
        '
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label80.Location = New System.Drawing.Point(7, 64)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(60, 15)
        Me.Label80.Text = "Branch No :"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label9.Location = New System.Drawing.Point(7, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 15)
        Me.Label9.Text = "Qty :"
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label37.Location = New System.Drawing.Point(7, 26)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(60, 15)
        Me.Label37.Text = "Seq No :"
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label36.Location = New System.Drawing.Point(7, 7)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(60, 15)
        Me.Label36.Text = "Part No :"
        '
        'Panel12
        '
        Me.Panel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel12.Controls.Add(Me.btnBackRBScanPart)
        Me.Panel12.Location = New System.Drawing.Point(0, 247)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(320, 24)
        '
        'btnBackRBScanPart
        '
        Me.btnBackRBScanPart.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBackRBScanPart.Location = New System.Drawing.Point(200, 2)
        Me.btnBackRBScanPart.Name = "btnBackRBScanPart"
        Me.btnBackRBScanPart.Size = New System.Drawing.Size(110, 20)
        Me.btnBackRBScanPart.TabIndex = 4
        Me.btnBackRBScanPart.TabStop = False
        Me.btnBackRBScanPart.Text = "Back"
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label33.Location = New System.Drawing.Point(8, 35)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(90, 15)
        Me.Label33.Text = "PxP Kanban QR :"
        '
        'btnFScanPart
        '
        Me.btnFScanPart.Image = CType(resources.GetObject("btnFScanPart.Image"), System.Drawing.Image)
        Me.btnFScanPart.Location = New System.Drawing.Point(273, 34)
        Me.btnFScanPart.Name = "btnFScanPart"
        Me.btnFScanPart.Size = New System.Drawing.Size(30, 22)
        Me.btnFScanPart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'txtPxPQR
        '
        Me.txtPxPQR.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtPxPQR.Location = New System.Drawing.Point(100, 34)
        Me.txtPxPQR.Name = "txtPxPQR"
        Me.txtPxPQR.Size = New System.Drawing.Size(170, 19)
        Me.txtPxPQR.TabIndex = 1
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.PowderBlue
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label35.Location = New System.Drawing.Point(2, 2)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(316, 20)
        '
        'pnlRBAbnViewDet
        '
        Me.pnlRBAbnViewDet.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBAbnViewDet.Controls.Add(Me.lblHeaderAbnVwDet)
        Me.pnlRBAbnViewDet.Controls.Add(Me.Panel4)
        Me.pnlRBAbnViewDet.Controls.Add(Me.lstViewRcvDet)
        Me.pnlRBAbnViewDet.Controls.Add(Me.Label61)
        Me.pnlRBAbnViewDet.Location = New System.Drawing.Point(657, 284)
        Me.pnlRBAbnViewDet.Name = "pnlRBAbnViewDet"
        Me.pnlRBAbnViewDet.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBAbnViewDet.Visible = False
        '
        'lblHeaderAbnVwDet
        '
        Me.lblHeaderAbnVwDet.BackColor = System.Drawing.SystemColors.Window
        Me.lblHeaderAbnVwDet.Location = New System.Drawing.Point(2, 24)
        Me.lblHeaderAbnVwDet.Name = "lblHeaderAbnVwDet"
        Me.lblHeaderAbnVwDet.Size = New System.Drawing.Size(315, 20)
        Me.lblHeaderAbnVwDet.Text = "Total Record : "
        Me.lblHeaderAbnVwDet.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.btnCloseRBViewDet)
        Me.Panel4.Location = New System.Drawing.Point(0, 247)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseRBViewDet
        '
        Me.btnCloseRBViewDet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseRBViewDet.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseRBViewDet.Name = "btnCloseRBViewDet"
        Me.btnCloseRBViewDet.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseRBViewDet.TabIndex = 1
        Me.btnCloseRBViewDet.TabStop = False
        Me.btnCloseRBViewDet.Text = "Close"
        '
        'lstViewRcvDet
        '
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader13)
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader14)
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader15)
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader2)
        Me.lstViewRcvDet.Columns.Add(Me.ColumnHeader21)
        Me.lstViewRcvDet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewRcvDet.FullRowSelect = True
        Me.lstViewRcvDet.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstViewRcvDet.Location = New System.Drawing.Point(6, 47)
        Me.lstViewRcvDet.Name = "lstViewRcvDet"
        Me.lstViewRcvDet.Size = New System.Drawing.Size(309, 194)
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
        Me.ColumnHeader14.Text = "Part No"
        Me.ColumnHeader14.Width = 115
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "Seq No"
        Me.ColumnHeader15.Width = 50
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Qty"
        Me.ColumnHeader2.Width = 50
        '
        'ColumnHeader21
        '
        Me.ColumnHeader21.Text = "Branch No"
        Me.ColumnHeader21.Width = 60
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.PowderBlue
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label61.Location = New System.Drawing.Point(2, 4)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(315, 20)
        '
        'pnlRBPosting
        '
        Me.pnlRBPosting.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBPosting.Controls.Add(Me.lblPostingTotalPdgAbn)
        Me.pnlRBPosting.Controls.Add(Me.Label26)
        Me.pnlRBPosting.Controls.Add(Me.Label25)
        Me.pnlRBPosting.Controls.Add(Me.btnRBSubmitPosting)
        Me.pnlRBPosting.Controls.Add(Me.Panel13)
        Me.pnlRBPosting.Controls.Add(Me.lstViewPosting)
        Me.pnlRBPosting.Controls.Add(Me.Label24)
        Me.pnlRBPosting.Location = New System.Drawing.Point(983, 284)
        Me.pnlRBPosting.Name = "pnlRBPosting"
        Me.pnlRBPosting.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBPosting.Visible = False
        '
        'lblPostingTotalPdgAbn
        '
        Me.lblPostingTotalPdgAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblPostingTotalPdgAbn.Location = New System.Drawing.Point(237, 50)
        Me.lblPostingTotalPdgAbn.Name = "lblPostingTotalPdgAbn"
        Me.lblPostingTotalPdgAbn.Size = New System.Drawing.Size(70, 15)
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label26.Location = New System.Drawing.Point(11, 50)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(280, 15)
        Me.Label26.Text = "Total Pending Posting Records : "
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label25.Location = New System.Drawing.Point(2, 30)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(316, 15)
        Me.Label25.Text = "Posting Supply Robbing Data"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnRBSubmitPosting
        '
        Me.btnRBSubmitPosting.Location = New System.Drawing.Point(0, 224)
        Me.btnRBSubmitPosting.Name = "btnRBSubmitPosting"
        Me.btnRBSubmitPosting.Size = New System.Drawing.Size(320, 24)
        Me.btnRBSubmitPosting.TabIndex = 1
        Me.btnRBSubmitPosting.Text = "Submit"
        '
        'Panel13
        '
        Me.Panel13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel13.Controls.Add(Me.lblUsername)
        Me.Panel13.Controls.Add(Me.btnCloseRBPosting)
        Me.Panel13.Location = New System.Drawing.Point(0, 247)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(320, 24)
        '
        'lblUsername
        '
        Me.lblUsername.Location = New System.Drawing.Point(3, 2)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(191, 20)
        Me.lblUsername.Text = "USER NAME: "
        '
        'btnCloseRBPosting
        '
        Me.btnCloseRBPosting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseRBPosting.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseRBPosting.Name = "btnCloseRBPosting"
        Me.btnCloseRBPosting.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseRBPosting.TabIndex = 2
        Me.btnCloseRBPosting.TabStop = False
        Me.btnCloseRBPosting.Text = "Close"
        '
        'lstViewPosting
        '
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader3)
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader4)
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader5)
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader6)
        Me.lstViewPosting.Columns.Add(Me.ColumnHeader20)
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
        Me.ColumnHeader4.Text = "Part No"
        Me.ColumnHeader4.Width = 115
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Seq No"
        Me.ColumnHeader5.Width = 50
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Qty"
        Me.ColumnHeader6.Width = 50
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "Branch No"
        Me.ColumnHeader20.Width = 60
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.PowderBlue
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label24.Location = New System.Drawing.Point(2, 4)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(315, 20)
        '
        'pnlRBDelete
        '
        Me.pnlRBDelete.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBDelete.Controls.Add(Me.lblDeleteTotalAbn)
        Me.pnlRBDelete.Controls.Add(Me.Label6)
        Me.pnlRBDelete.Controls.Add(Me.Label7)
        Me.pnlRBDelete.Controls.Add(Me.btnDelete)
        Me.pnlRBDelete.Controls.Add(Me.Panel7)
        Me.pnlRBDelete.Controls.Add(Me.lstViewDelete)
        Me.pnlRBDelete.Controls.Add(Me.Label28)
        Me.pnlRBDelete.Location = New System.Drawing.Point(1309, 284)
        Me.pnlRBDelete.Name = "pnlRBDelete"
        Me.pnlRBDelete.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBDelete.Visible = False
        '
        'lblDeleteTotalAbn
        '
        Me.lblDeleteTotalAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDeleteTotalAbn.Location = New System.Drawing.Point(188, 50)
        Me.lblDeleteTotalAbn.Name = "lblDeleteTotalAbn"
        Me.lblDeleteTotalAbn.Size = New System.Drawing.Size(80, 15)
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label6.Location = New System.Drawing.Point(7, 50)
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
        Me.Label7.Text = "Delete Supply Robbing Data"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(0, 224)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(320, 24)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel7.Controls.Add(Me.btnCloseRBDelete)
        Me.Panel7.Location = New System.Drawing.Point(0, 247)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseRBDelete
        '
        Me.btnCloseRBDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseRBDelete.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseRBDelete.Name = "btnCloseRBDelete"
        Me.btnCloseRBDelete.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseRBDelete.TabIndex = 2
        Me.btnCloseRBDelete.TabStop = False
        Me.btnCloseRBDelete.Text = "Close"
        '
        'lstViewDelete
        '
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader7)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader8)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader9)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader10)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader16)
        Me.lstViewDelete.Columns.Add(Me.ColumnHeader1)
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
        Me.ColumnHeader8.Text = "Part No"
        Me.ColumnHeader8.Width = 115
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Seq No"
        Me.ColumnHeader9.Width = 50
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Qty"
        Me.ColumnHeader10.Width = 50
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "Branch No"
        Me.ColumnHeader16.Width = 60
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Status"
        Me.ColumnHeader1.Width = 60
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.PowderBlue
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label28.Location = New System.Drawing.Point(2, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(315, 20)
        '
        'pnlRBAbn
        '
        Me.pnlRBAbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBAbn.Controls.Add(Me.Panel6)
        Me.pnlRBAbn.Controls.Add(Me.Panel14)
        Me.pnlRBAbn.Controls.Add(Me.Label39)
        Me.pnlRBAbn.Controls.Add(Me.Label40)
        Me.pnlRBAbn.Controls.Add(Me.Label41)
        Me.pnlRBAbn.Controls.Add(Me.Label42)
        Me.pnlRBAbn.Controls.Add(Me.btnRBAbnDelete)
        Me.pnlRBAbn.Controls.Add(Me.btnRBAbnPost)
        Me.pnlRBAbn.Controls.Add(Me.btnRBAbnView)
        Me.pnlRBAbn.Controls.Add(Me.btnRBAbnScan)
        Me.pnlRBAbn.Location = New System.Drawing.Point(5, 284)
        Me.pnlRBAbn.Name = "pnlRBAbn"
        Me.pnlRBAbn.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBAbn.Visible = False
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel6.Controls.Add(Me.btnCloseAbnRB)
        Me.Panel6.Location = New System.Drawing.Point(0, 247)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseAbnRB
        '
        Me.btnCloseAbnRB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseAbnRB.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseAbnRB.Name = "btnCloseAbnRB"
        Me.btnCloseAbnRB.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseAbnRB.TabIndex = 1
        Me.btnCloseAbnRB.TabStop = False
        Me.btnCloseAbnRB.Text = "Close"
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
        'btnRBAbnDelete
        '
        Me.btnRBAbnDelete.Image = CType(resources.GetObject("btnRBAbnDelete.Image"), System.Drawing.Image)
        Me.btnRBAbnDelete.Location = New System.Drawing.Point(193, 144)
        Me.btnRBAbnDelete.Name = "btnRBAbnDelete"
        Me.btnRBAbnDelete.Size = New System.Drawing.Size(80, 80)
        Me.btnRBAbnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnRBAbnPost
        '
        Me.btnRBAbnPost.Image = CType(resources.GetObject("btnRBAbnPost.Image"), System.Drawing.Image)
        Me.btnRBAbnPost.Location = New System.Drawing.Point(65, 144)
        Me.btnRBAbnPost.Name = "btnRBAbnPost"
        Me.btnRBAbnPost.Size = New System.Drawing.Size(80, 80)
        Me.btnRBAbnPost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnRBAbnView
        '
        Me.btnRBAbnView.Image = CType(resources.GetObject("btnRBAbnView.Image"), System.Drawing.Image)
        Me.btnRBAbnView.Location = New System.Drawing.Point(193, 36)
        Me.btnRBAbnView.Name = "btnRBAbnView"
        Me.btnRBAbnView.Size = New System.Drawing.Size(80, 80)
        Me.btnRBAbnView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'btnRBAbnScan
        '
        Me.btnRBAbnScan.Image = CType(resources.GetObject("btnRBAbnScan.Image"), System.Drawing.Image)
        Me.btnRBAbnScan.Location = New System.Drawing.Point(65, 36)
        Me.btnRBAbnScan.Name = "btnRBAbnScan"
        Me.btnRBAbnScan.Size = New System.Drawing.Size(80, 80)
        Me.btnRBAbnScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'pnlRBViewDet
        '
        Me.pnlRBViewDet.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBViewDet.Controls.Add(Me.Panel5)
        Me.pnlRBViewDet.Controls.Add(Me.TabRBSummary)
        Me.pnlRBViewDet.Controls.Add(Me.Label53)
        Me.pnlRBViewDet.Location = New System.Drawing.Point(983, 3)
        Me.pnlRBViewDet.Name = "pnlRBViewDet"
        Me.pnlRBViewDet.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBViewDet.Visible = False
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.btnBackRBViewDet)
        Me.Panel5.Location = New System.Drawing.Point(0, 247)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(320, 24)
        '
        'btnBackRBViewDet
        '
        Me.btnBackRBViewDet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBackRBViewDet.Location = New System.Drawing.Point(200, 2)
        Me.btnBackRBViewDet.Name = "btnBackRBViewDet"
        Me.btnBackRBViewDet.Size = New System.Drawing.Size(110, 20)
        Me.btnBackRBViewDet.TabIndex = 1
        Me.btnBackRBViewDet.TabStop = False
        Me.btnBackRBViewDet.Text = "Back"
        '
        'TabRBSummary
        '
        Me.TabRBSummary.Controls.Add(Me.TabPage4)
        Me.TabRBSummary.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.TabRBSummary.Location = New System.Drawing.Point(3, 26)
        Me.TabRBSummary.Name = "TabRBSummary"
        Me.TabRBSummary.SelectedIndex = 0
        Me.TabRBSummary.Size = New System.Drawing.Size(314, 224)
        Me.TabRBSummary.TabIndex = 83
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
        Me.lblDetailTotalScan.Location = New System.Drawing.Point(90, 178)
        Me.lblDetailTotalScan.Name = "lblDetailTotalScan"
        Me.lblDetailTotalScan.Size = New System.Drawing.Size(45, 21)
        '
        'Label47
        '
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label47.Location = New System.Drawing.Point(4, 178)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(80, 20)
        Me.Label47.Text = "Total Scanned :"
        '
        'lstViewRCISummary
        '
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader17)
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader19)
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader18)
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader11)
        Me.lstViewRCISummary.Columns.Add(Me.ColumnHeader12)
        Me.lstViewRCISummary.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstViewRCISummary.FullRowSelect = True
        Me.lstViewRCISummary.Location = New System.Drawing.Point(4, 5)
        Me.lstViewRCISummary.Name = "lstViewRCISummary"
        Me.lstViewRCISummary.Size = New System.Drawing.Size(299, 170)
        Me.lstViewRCISummary.TabIndex = 73
        Me.lstViewRCISummary.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "No"
        Me.ColumnHeader17.Width = 30
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "Part No"
        Me.ColumnHeader19.Width = 100
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Seq No"
        Me.ColumnHeader18.Width = 50
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Qty"
        Me.ColumnHeader11.Width = 50
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Branch No"
        Me.ColumnHeader12.Width = 60
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.PowderBlue
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label53.Location = New System.Drawing.Point(2, 3)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(315, 20)
        '
        'pnlRBAbnScan
        '
        Me.pnlRBAbnScan.BackColor = System.Drawing.Color.Transparent
        Me.pnlRBAbnScan.Controls.Add(Me.btnRBAbnScanDet)
        Me.pnlRBAbnScan.Controls.Add(Me.lblRBStatusMsgDescAbn)
        Me.pnlRBAbnScan.Controls.Add(Me.lblRBStatusMsgAbn)
        Me.pnlRBAbnScan.Controls.Add(Me.lblTotalScannedAbn)
        Me.pnlRBAbnScan.Controls.Add(Me.Label17)
        Me.pnlRBAbnScan.Controls.Add(Me.Panel10)
        Me.pnlRBAbnScan.Controls.Add(Me.Panel11)
        Me.pnlRBAbnScan.Controls.Add(Me.Label38)
        Me.pnlRBAbnScan.Controls.Add(Me.btnRBAbnFScan)
        Me.pnlRBAbnScan.Controls.Add(Me.txtPxPQRAbn)
        Me.pnlRBAbnScan.Controls.Add(Me.Label43)
        Me.pnlRBAbnScan.Location = New System.Drawing.Point(331, 284)
        Me.pnlRBAbnScan.Name = "pnlRBAbnScan"
        Me.pnlRBAbnScan.Size = New System.Drawing.Size(320, 275)
        Me.pnlRBAbnScan.Visible = False
        '
        'btnRBAbnScanDet
        '
        Me.btnRBAbnScanDet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnRBAbnScanDet.Location = New System.Drawing.Point(223, 3)
        Me.btnRBAbnScanDet.Name = "btnRBAbnScanDet"
        Me.btnRBAbnScanDet.Size = New System.Drawing.Size(90, 20)
        Me.btnRBAbnScanDet.TabIndex = 2
        Me.btnRBAbnScanDet.Text = "Details"
        '
        'lblRBStatusMsgDescAbn
        '
        Me.lblRBStatusMsgDescAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblRBStatusMsgDescAbn.Location = New System.Drawing.Point(0, 205)
        Me.lblRBStatusMsgDescAbn.Name = "lblRBStatusMsgDescAbn"
        Me.lblRBStatusMsgDescAbn.Size = New System.Drawing.Size(320, 20)
        Me.lblRBStatusMsgDescAbn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblRBStatusMsgAbn
        '
        Me.lblRBStatusMsgAbn.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRBStatusMsgAbn.BackColor = System.Drawing.Color.Transparent
        Me.lblRBStatusMsgAbn.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblRBStatusMsgAbn.Location = New System.Drawing.Point(0, 180)
        Me.lblRBStatusMsgAbn.Name = "lblRBStatusMsgAbn"
        Me.lblRBStatusMsgAbn.Size = New System.Drawing.Size(320, 22)
        Me.lblRBStatusMsgAbn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTotalScannedAbn
        '
        Me.lblTotalScannedAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblTotalScannedAbn.Location = New System.Drawing.Point(100, 225)
        Me.lblTotalScannedAbn.Name = "lblTotalScannedAbn"
        Me.lblTotalScannedAbn.Size = New System.Drawing.Size(40, 20)
        Me.lblTotalScannedAbn.Text = "0"
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
        Me.Panel10.Controls.Add(Me.txtQtyAbn)
        Me.Panel10.Controls.Add(Me.txtBranchNoAbn)
        Me.Panel10.Controls.Add(Me.txtSeqNoAbn)
        Me.Panel10.Controls.Add(Me.txtPartNoAbn)
        Me.Panel10.Controls.Add(Me.Label83)
        Me.Panel10.Controls.Add(Me.Label19)
        Me.Panel10.Controls.Add(Me.Label29)
        Me.Panel10.Controls.Add(Me.Label30)
        Me.Panel10.Location = New System.Drawing.Point(10, 80)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(293, 85)
        '
        'txtQtyAbn
        '
        Me.txtQtyAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtQtyAbn.Location = New System.Drawing.Point(75, 45)
        Me.txtQtyAbn.Name = "txtQtyAbn"
        Me.txtQtyAbn.Size = New System.Drawing.Size(80, 15)
        '
        'txtBranchNoAbn
        '
        Me.txtBranchNoAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtBranchNoAbn.Location = New System.Drawing.Point(75, 64)
        Me.txtBranchNoAbn.Name = "txtBranchNoAbn"
        Me.txtBranchNoAbn.Size = New System.Drawing.Size(80, 15)
        '
        'txtSeqNoAbn
        '
        Me.txtSeqNoAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtSeqNoAbn.Location = New System.Drawing.Point(75, 26)
        Me.txtSeqNoAbn.Name = "txtSeqNoAbn"
        Me.txtSeqNoAbn.Size = New System.Drawing.Size(180, 15)
        '
        'txtPartNoAbn
        '
        Me.txtPartNoAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtPartNoAbn.Location = New System.Drawing.Point(75, 7)
        Me.txtPartNoAbn.Name = "txtPartNoAbn"
        Me.txtPartNoAbn.Size = New System.Drawing.Size(180, 15)
        '
        'Label83
        '
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label83.Location = New System.Drawing.Point(7, 64)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(60, 15)
        Me.Label83.Text = "Branch No :"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label19.Location = New System.Drawing.Point(7, 45)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(40, 15)
        Me.Label19.Text = "Qty :"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label29.Location = New System.Drawing.Point(7, 26)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(60, 15)
        Me.Label29.Text = "Seq No :"
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label30.Location = New System.Drawing.Point(7, 7)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 15)
        Me.Label30.Text = "Part No :"
        '
        'Panel11
        '
        Me.Panel11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel11.Controls.Add(Me.btnBackRBAbnScan)
        Me.Panel11.Location = New System.Drawing.Point(0, 247)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(320, 24)
        '
        'btnBackRBAbnScan
        '
        Me.btnBackRBAbnScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnBackRBAbnScan.Location = New System.Drawing.Point(200, 2)
        Me.btnBackRBAbnScan.Name = "btnBackRBAbnScan"
        Me.btnBackRBAbnScan.Size = New System.Drawing.Size(110, 20)
        Me.btnBackRBAbnScan.TabIndex = 3
        Me.btnBackRBAbnScan.TabStop = False
        Me.btnBackRBAbnScan.Text = "Back"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label38.Location = New System.Drawing.Point(8, 35)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(90, 15)
        Me.Label38.Text = "PxP Kanban QR :"
        '
        'btnRBAbnFScan
        '
        Me.btnRBAbnFScan.Image = CType(resources.GetObject("btnRBAbnFScan.Image"), System.Drawing.Image)
        Me.btnRBAbnFScan.Location = New System.Drawing.Point(273, 34)
        Me.btnRBAbnFScan.Name = "btnRBAbnFScan"
        Me.btnRBAbnFScan.Size = New System.Drawing.Size(30, 22)
        Me.btnRBAbnFScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'txtPxPQRAbn
        '
        Me.txtPxPQRAbn.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtPxPQRAbn.Location = New System.Drawing.Point(100, 34)
        Me.txtPxPQRAbn.Name = "txtPxPQRAbn"
        Me.txtPxPQRAbn.Size = New System.Drawing.Size(170, 19)
        Me.txtPxPQRAbn.TabIndex = 1
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.PowderBlue
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label43.Location = New System.Drawing.Point(2, 2)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(316, 20)
        '
        'pnlLogin
        '
        Me.pnlLogin.BackColor = System.Drawing.Color.Transparent
        Me.pnlLogin.Controls.Add(Me.Panel2)
        Me.pnlLogin.Controls.Add(Me.btnLoginSubmit)
        Me.pnlLogin.Controls.Add(Me.txtUsername)
        Me.pnlLogin.Controls.Add(Me.Label2)
        Me.pnlLogin.Controls.Add(Me.Panel15)
        Me.pnlLogin.Controls.Add(Me.txtPwd)
        Me.pnlLogin.Controls.Add(Me.Label5)
        Me.pnlLogin.Location = New System.Drawing.Point(1308, 3)
        Me.pnlLogin.Name = "pnlLogin"
        Me.pnlLogin.Size = New System.Drawing.Size(320, 275)
        Me.pnlLogin.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.btnLoginClose)
        Me.Panel2.Location = New System.Drawing.Point(0, 247)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(320, 24)
        '
        'btnLoginClose
        '
        Me.btnLoginClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnLoginClose.Location = New System.Drawing.Point(200, 1)
        Me.btnLoginClose.Name = "btnLoginClose"
        Me.btnLoginClose.Size = New System.Drawing.Size(110, 20)
        Me.btnLoginClose.TabIndex = 4
        Me.btnLoginClose.TabStop = False
        Me.btnLoginClose.Text = "Close"
        '
        'btnLoginSubmit
        '
        Me.btnLoginSubmit.Location = New System.Drawing.Point(0, 225)
        Me.btnLoginSubmit.Name = "btnLoginSubmit"
        Me.btnLoginSubmit.Size = New System.Drawing.Size(320, 22)
        Me.btnLoginSubmit.TabIndex = 3
        Me.btnLoginSubmit.Text = "Submit"
        '
        'txtUsername
        '
        Me.txtUsername.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtUsername.Location = New System.Drawing.Point(32, 81)
        Me.txtUsername.MaxLength = 50
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(265, 21)
        Me.txtUsername.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(32, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 20)
        Me.Label2.Text = "Username :"
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel15.Controls.Add(Me.Label4)
        Me.Panel15.Location = New System.Drawing.Point(0, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(320, 24)
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(56, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(209, 21)
        Me.Label4.Text = "Login"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPwd
        '
        Me.txtPwd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtPwd.Location = New System.Drawing.Point(32, 127)
        Me.txtPwd.MaxLength = 50
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwd.Size = New System.Drawing.Size(265, 21)
        Me.txtPwd.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(32, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 20)
        Me.Label5.Text = "Password :"
        '
        'frmRobbing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(638, 455)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlLogin)
        Me.Controls.Add(Me.pnlRBAbnScan)
        Me.Controls.Add(Me.pnlRBViewDet)
        Me.Controls.Add(Me.pnlRBAbn)
        Me.Controls.Add(Me.pnlRBDelete)
        Me.Controls.Add(Me.pnlRBPosting)
        Me.Controls.Add(Me.pnlRBAbnViewDet)
        Me.Controls.Add(Me.pnlRBScanPart)
        Me.Controls.Add(Me.pnlRBFScan)
        Me.Controls.Add(Me.footerStatusBar)
        Me.Controls.Add(Me.pnlRBMain)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRobbing"
        Me.Text = "DCS JSP"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlRBMain.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.pnlRBFScan.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlRBScanPart.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.pnlRBAbnViewDet.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlRBPosting.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.pnlRBDelete.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlRBAbn.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnlRBViewDet.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.TabRBSummary.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.pnlRBAbnScan.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.pnlLogin.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlRBMain As System.Windows.Forms.Panel
    Friend WithEvents footerStatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents TimerCheckOnline As System.Windows.Forms.Timer
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnScanRB As System.Windows.Forms.PictureBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseRB As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnAbnormalRB As System.Windows.Forms.PictureBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlRBFScan As System.Windows.Forms.Panel
    Friend WithEvents lstViewRCVFScan As System.Windows.Forms.ListView
    Friend WithEvents REASON_CODE As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtFSModuleNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSaveForceScan As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnBackFScan As System.Windows.Forms.Button
    Friend WithEvents pnlRBScanPart As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnBackRBScanPart As System.Windows.Forms.Button
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnFScanPart As System.Windows.Forms.PictureBox
    Friend WithEvents txtPxPQR As System.Windows.Forms.TextBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents btnScanSubmit As System.Windows.Forms.Button
    Friend WithEvents pnlRBAbnViewDet As System.Windows.Forms.Panel
    Friend WithEvents lstViewRcvDet As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseRBViewDet As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblTotalScanned As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblHeaderAbnVwDet As System.Windows.Forms.Label
    Friend WithEvents pnlRBPosting As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseRBPosting As System.Windows.Forms.Button
    Friend WithEvents lstViewPosting As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnRBSubmitPosting As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlRBDelete As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseRBDelete As System.Windows.Forms.Button
    Friend WithEvents lstViewDelete As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents pnlRBAbn As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseAbnRB As System.Windows.Forms.Button
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents btnRBAbnDelete As System.Windows.Forms.PictureBox
    Friend WithEvents btnRBAbnPost As System.Windows.Forms.PictureBox
    Friend WithEvents btnRBAbnView As System.Windows.Forms.PictureBox
    Friend WithEvents btnRBAbnScan As System.Windows.Forms.PictureBox
    Friend WithEvents pnlRBViewDet As System.Windows.Forms.Panel
    Friend WithEvents TabRBSummary As System.Windows.Forms.TabControl
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents lblDetailTotalScan As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lstViewRCISummary As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnBackRBViewDet As System.Windows.Forms.Button
    Friend WithEvents lblRBStatusMsgDesc As System.Windows.Forms.Label
    Friend WithEvents lblRBStatusMsg As System.Windows.Forms.Label
    Friend WithEvents txtFSPartNo As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnScanDetails As System.Windows.Forms.Button
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtFSBranch As System.Windows.Forms.TextBox
    Friend WithEvents txtFSSeqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlRBAbnScan As System.Windows.Forms.Panel
    Friend WithEvents btnRBAbnScanDet As System.Windows.Forms.Button
    Friend WithEvents lblRBStatusMsgDescAbn As System.Windows.Forms.Label
    Friend WithEvents lblRBStatusMsgAbn As System.Windows.Forms.Label
    Friend WithEvents lblTotalScannedAbn As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents btnBackRBAbnScan As System.Windows.Forms.Button
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents btnRBAbnFScan As System.Windows.Forms.PictureBox
    Friend WithEvents txtPxPQRAbn As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.Label
    Friend WithEvents txtBranchNo As System.Windows.Forms.Label
    Friend WithEvents txtSeqNo As System.Windows.Forms.Label
    Friend WithEvents txtPartNo As System.Windows.Forms.Label
    Friend WithEvents txtQtyAbn As System.Windows.Forms.Label
    Friend WithEvents txtBranchNoAbn As System.Windows.Forms.Label
    Friend WithEvents txtSeqNoAbn As System.Windows.Forms.Label
    Friend WithEvents txtPartNoAbn As System.Windows.Forms.Label
    Friend WithEvents lblPostingTotalPdgAbn As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblDeleteTotalAbn As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlLogin As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnLoginClose As System.Windows.Forms.Button
    Friend WithEvents btnLoginSubmit As System.Windows.Forms.Button
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader

End Class
