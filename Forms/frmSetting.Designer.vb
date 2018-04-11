<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSetting
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
        Me.pnlSetting = New System.Windows.Forms.Panel
        Me.btnSTBack = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblTitle = New System.Windows.Forms.Label
        Me.TabSetting = New System.Windows.Forms.TabControl
        Me.TabOrganization = New System.Windows.Forms.TabPage
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboOrganization = New System.Windows.Forms.ComboBox
        Me.btnOrgSave = New System.Windows.Forms.Button
        Me.TabScanner = New System.Windows.Forms.TabPage
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSTSCNID = New System.Windows.Forms.TextBox
        Me.TabAuthorized = New System.Windows.Forms.TabPage
        Me.txtSTUsername = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.btnSTAUTPwd = New System.Windows.Forms.Button
        Me.txtSTPassword = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TabScannerNo = New System.Windows.Forms.TabPage
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtSTBatchID = New System.Windows.Forms.TextBox
        Me.TabWebServices = New System.Windows.Forms.TabPage
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtSTWSORAUserPwd = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSTWSORAUserID = New System.Windows.Forms.TextBox
        Me.txtSTWSSave = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSTWSOracle = New System.Windows.Forms.TextBox
        Me.txtSTWSDCSSP = New System.Windows.Forms.TextBox
        Me.TabImport = New System.Windows.Forms.TabPage
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboImpDay = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnImportSave = New System.Windows.Forms.Button
        Me.TabDatabase = New System.Windows.Forms.TabPage
        Me.txtDBName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDBPwd = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnDBBrowse = New System.Windows.Forms.Button
        Me.txtDBPath = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TabInterval = New System.Windows.Forms.TabPage
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnInterSave = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtSTInterval = New System.Windows.Forms.TextBox
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.pnlAuthentication = New System.Windows.Forms.Panel
        Me.btnSubmitLogin = New System.Windows.Forms.Button
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.btnCloseLogin = New System.Windows.Forms.Button
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtAUTUNM = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtAUTPWD = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlSetting.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabSetting.SuspendLayout()
        Me.TabOrganization.SuspendLayout()
        Me.TabScanner.SuspendLayout()
        Me.TabAuthorized.SuspendLayout()
        Me.TabScannerNo.SuspendLayout()
        Me.TabWebServices.SuspendLayout()
        Me.TabImport.SuspendLayout()
        Me.TabDatabase.SuspendLayout()
        Me.TabInterval.SuspendLayout()
        Me.pnlAuthentication.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSetting
        '
        Me.pnlSetting.BackColor = System.Drawing.Color.Transparent
        Me.pnlSetting.Controls.Add(Me.btnSTBack)
        Me.pnlSetting.Controls.Add(Me.Panel1)
        Me.pnlSetting.Controls.Add(Me.TabSetting)
        Me.pnlSetting.Location = New System.Drawing.Point(5, 3)
        Me.pnlSetting.Name = "pnlSetting"
        Me.pnlSetting.Size = New System.Drawing.Size(320, 275)
        Me.pnlSetting.Visible = False
        '
        'btnSTBack
        '
        Me.btnSTBack.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnSTBack.Location = New System.Drawing.Point(182, 217)
        Me.btnSTBack.Name = "btnSTBack"
        Me.btnSTBack.Size = New System.Drawing.Size(134, 23)
        Me.btnSTBack.TabIndex = 1
        Me.btnSTBack.Text = "Back"
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
        Me.lblTitle.Text = "Setting"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TabSetting
        '
        Me.TabSetting.Controls.Add(Me.TabOrganization)
        Me.TabSetting.Controls.Add(Me.TabScanner)
        Me.TabSetting.Controls.Add(Me.TabAuthorized)
        Me.TabSetting.Controls.Add(Me.TabScannerNo)
        Me.TabSetting.Controls.Add(Me.TabWebServices)
        Me.TabSetting.Controls.Add(Me.TabImport)
        Me.TabSetting.Controls.Add(Me.TabDatabase)
        Me.TabSetting.Controls.Add(Me.TabInterval)
        Me.TabSetting.Location = New System.Drawing.Point(0, 24)
        Me.TabSetting.Name = "TabSetting"
        Me.TabSetting.SelectedIndex = 0
        Me.TabSetting.Size = New System.Drawing.Size(320, 187)
        Me.TabSetting.TabIndex = 36
        '
        'TabOrganization
        '
        Me.TabOrganization.Controls.Add(Me.Label5)
        Me.TabOrganization.Controls.Add(Me.cboOrganization)
        Me.TabOrganization.Controls.Add(Me.btnOrgSave)
        Me.TabOrganization.Location = New System.Drawing.Point(4, 25)
        Me.TabOrganization.Name = "TabOrganization"
        Me.TabOrganization.Size = New System.Drawing.Size(312, 158)
        Me.TabOrganization.Text = "Organization"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.Location = New System.Drawing.Point(18, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 20)
        Me.Label5.Text = "Organization :"
        '
        'cboOrganization
        '
        Me.cboOrganization.Location = New System.Drawing.Point(110, 32)
        Me.cboOrganization.Name = "cboOrganization"
        Me.cboOrganization.Size = New System.Drawing.Size(142, 23)
        Me.cboOrganization.TabIndex = 5
        '
        'btnOrgSave
        '
        Me.btnOrgSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnOrgSave.Location = New System.Drawing.Point(218, 121)
        Me.btnOrgSave.Name = "btnOrgSave"
        Me.btnOrgSave.Size = New System.Drawing.Size(86, 23)
        Me.btnOrgSave.TabIndex = 4
        Me.btnOrgSave.Text = "Save"
        '
        'TabScanner
        '
        Me.TabScanner.Controls.Add(Me.Label1)
        Me.TabScanner.Controls.Add(Me.txtSTSCNID)
        Me.TabScanner.Location = New System.Drawing.Point(4, 25)
        Me.TabScanner.Name = "TabScanner"
        Me.TabScanner.Size = New System.Drawing.Size(312, 158)
        Me.TabScanner.Text = "Scanner"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(6, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 20)
        Me.Label1.Text = "Scanner ID :"
        '
        'txtSTSCNID
        '
        Me.txtSTSCNID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTSCNID.Location = New System.Drawing.Point(88, 24)
        Me.txtSTSCNID.Name = "txtSTSCNID"
        Me.txtSTSCNID.ReadOnly = True
        Me.txtSTSCNID.Size = New System.Drawing.Size(194, 21)
        Me.txtSTSCNID.TabIndex = 0
        '
        'TabAuthorized
        '
        Me.TabAuthorized.Controls.Add(Me.txtSTUsername)
        Me.TabAuthorized.Controls.Add(Me.Label15)
        Me.TabAuthorized.Controls.Add(Me.btnSTAUTPwd)
        Me.TabAuthorized.Controls.Add(Me.txtSTPassword)
        Me.TabAuthorized.Controls.Add(Me.Label3)
        Me.TabAuthorized.Location = New System.Drawing.Point(4, 25)
        Me.TabAuthorized.Name = "TabAuthorized"
        Me.TabAuthorized.Size = New System.Drawing.Size(312, 158)
        Me.TabAuthorized.Text = "Authorize"
        '
        'txtSTUsername
        '
        Me.txtSTUsername.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTUsername.Location = New System.Drawing.Point(95, 23)
        Me.txtSTUsername.MaxLength = 50
        Me.txtSTUsername.Name = "txtSTUsername"
        Me.txtSTUsername.Size = New System.Drawing.Size(182, 21)
        Me.txtSTUsername.TabIndex = 7
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label15.Location = New System.Drawing.Point(18, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 20)
        Me.Label15.Text = "Username :"
        '
        'btnSTAUTPwd
        '
        Me.btnSTAUTPwd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnSTAUTPwd.Location = New System.Drawing.Point(218, 121)
        Me.btnSTAUTPwd.Name = "btnSTAUTPwd"
        Me.btnSTAUTPwd.Size = New System.Drawing.Size(86, 23)
        Me.btnSTAUTPwd.TabIndex = 3
        Me.btnSTAUTPwd.Text = "Save"
        '
        'txtSTPassword
        '
        Me.txtSTPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTPassword.Location = New System.Drawing.Point(95, 69)
        Me.txtSTPassword.MaxLength = 50
        Me.txtSTPassword.Name = "txtSTPassword"
        Me.txtSTPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSTPassword.Size = New System.Drawing.Size(182, 21)
        Me.txtSTPassword.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.Location = New System.Drawing.Point(18, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 20)
        Me.Label3.Text = "Password :"
        '
        'TabScannerNo
        '
        Me.TabScannerNo.Controls.Add(Me.Label24)
        Me.TabScannerNo.Controls.Add(Me.Label23)
        Me.TabScannerNo.Controls.Add(Me.txtSTBatchID)
        Me.TabScannerNo.Location = New System.Drawing.Point(4, 25)
        Me.TabScannerNo.Name = "TabScannerNo"
        Me.TabScannerNo.Size = New System.Drawing.Size(312, 158)
        Me.TabScannerNo.Text = "Batch"
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label24.Location = New System.Drawing.Point(26, 40)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(173, 20)
        Me.Label24.Text = "(to represent each handheld) :"
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label23.Location = New System.Drawing.Point(26, 20)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(137, 20)
        Me.Label23.Text = "Scanner No in Batch ID"
        '
        'txtSTBatchID
        '
        Me.txtSTBatchID.Location = New System.Drawing.Point(26, 63)
        Me.txtSTBatchID.Name = "txtSTBatchID"
        Me.txtSTBatchID.Size = New System.Drawing.Size(124, 23)
        Me.txtSTBatchID.TabIndex = 0
        '
        'TabWebServices
        '
        Me.TabWebServices.Controls.Add(Me.Label7)
        Me.TabWebServices.Controls.Add(Me.txtSTWSORAUserPwd)
        Me.TabWebServices.Controls.Add(Me.Label6)
        Me.TabWebServices.Controls.Add(Me.txtSTWSORAUserID)
        Me.TabWebServices.Controls.Add(Me.txtSTWSSave)
        Me.TabWebServices.Controls.Add(Me.Label9)
        Me.TabWebServices.Controls.Add(Me.Label8)
        Me.TabWebServices.Controls.Add(Me.txtSTWSOracle)
        Me.TabWebServices.Controls.Add(Me.txtSTWSDCSSP)
        Me.TabWebServices.Location = New System.Drawing.Point(4, 25)
        Me.TabWebServices.Name = "TabWebServices"
        Me.TabWebServices.Size = New System.Drawing.Size(312, 158)
        Me.TabWebServices.Text = "Web Services"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(7, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 20)
        Me.Label7.Text = "Password :"
        '
        'txtSTWSORAUserPwd
        '
        Me.txtSTWSORAUserPwd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTWSORAUserPwd.Location = New System.Drawing.Point(85, 123)
        Me.txtSTWSORAUserPwd.MaxLength = 100
        Me.txtSTWSORAUserPwd.Name = "txtSTWSORAUserPwd"
        Me.txtSTWSORAUserPwd.Size = New System.Drawing.Size(127, 21)
        Me.txtSTWSORAUserPwd.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(7, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 20)
        Me.Label6.Text = "User ID :"
        '
        'txtSTWSORAUserID
        '
        Me.txtSTWSORAUserID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTWSORAUserID.Location = New System.Drawing.Point(85, 100)
        Me.txtSTWSORAUserID.MaxLength = 100
        Me.txtSTWSORAUserID.Name = "txtSTWSORAUserID"
        Me.txtSTWSORAUserID.Size = New System.Drawing.Size(127, 21)
        Me.txtSTWSORAUserID.TabIndex = 10
        '
        'txtSTWSSave
        '
        Me.txtSTWSSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTWSSave.Location = New System.Drawing.Point(218, 121)
        Me.txtSTWSSave.Name = "txtSTWSSave"
        Me.txtSTWSSave.Size = New System.Drawing.Size(86, 23)
        Me.txtSTWSSave.TabIndex = 6
        Me.txtSTWSSave.Text = "Save"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(7, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(159, 20)
        Me.Label9.Text = "WebService Oracle URL : "
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(7, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(159, 20)
        Me.Label8.Text = "WebService DCSJSP URL :"
        '
        'txtSTWSOracle
        '
        Me.txtSTWSOracle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTWSOracle.Location = New System.Drawing.Point(7, 77)
        Me.txtSTWSOracle.MaxLength = 200
        Me.txtSTWSOracle.Name = "txtSTWSOracle"
        Me.txtSTWSOracle.Size = New System.Drawing.Size(297, 21)
        Me.txtSTWSOracle.TabIndex = 5
        '
        'txtSTWSDCSSP
        '
        Me.txtSTWSDCSSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTWSDCSSP.Location = New System.Drawing.Point(7, 29)
        Me.txtSTWSDCSSP.MaxLength = 100
        Me.txtSTWSDCSSP.Name = "txtSTWSDCSSP"
        Me.txtSTWSDCSSP.Size = New System.Drawing.Size(297, 21)
        Me.txtSTWSDCSSP.TabIndex = 4
        '
        'TabImport
        '
        Me.TabImport.Controls.Add(Me.Label21)
        Me.TabImport.Controls.Add(Me.Label20)
        Me.TabImport.Controls.Add(Me.Label19)
        Me.TabImport.Controls.Add(Me.Label17)
        Me.TabImport.Controls.Add(Me.Label10)
        Me.TabImport.Controls.Add(Me.cboImpDay)
        Me.TabImport.Controls.Add(Me.Label11)
        Me.TabImport.Controls.Add(Me.btnImportSave)
        Me.TabImport.Location = New System.Drawing.Point(4, 25)
        Me.TabImport.Name = "TabImport"
        Me.TabImport.Size = New System.Drawing.Size(312, 158)
        Me.TabImport.Text = "Import"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(69, 87)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(100, 20)
        Me.Label21.Text = "Organization"
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(69, 69)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(100, 20)
        Me.Label20.Text = "Reason"
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(69, 51)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(100, 20)
        Me.Label19.Text = "Supplier"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(69, 33)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(100, 20)
        Me.Label17.Text = "Shop"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label10.Location = New System.Drawing.Point(10, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 20)
        Me.Label10.Text = "Master: "
        '
        'cboImpDay
        '
        Me.cboImpDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.cboImpDay.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.cboImpDay.Items.Add("Monday")
        Me.cboImpDay.Items.Add("Tuesday")
        Me.cboImpDay.Items.Add("Wednesday")
        Me.cboImpDay.Items.Add("Thursday")
        Me.cboImpDay.Items.Add("Friday")
        Me.cboImpDay.Items.Add("Saturday")
        Me.cboImpDay.Items.Add("Sunday")
        Me.cboImpDay.Location = New System.Drawing.Point(74, 5)
        Me.cboImpDay.Name = "cboImpDay"
        Me.cboImpDay.Size = New System.Drawing.Size(177, 21)
        Me.cboImpDay.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label11.Location = New System.Drawing.Point(10, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 20)
        Me.Label11.Text = "On every: "
        '
        'btnImportSave
        '
        Me.btnImportSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnImportSave.Location = New System.Drawing.Point(218, 121)
        Me.btnImportSave.Name = "btnImportSave"
        Me.btnImportSave.Size = New System.Drawing.Size(86, 23)
        Me.btnImportSave.TabIndex = 16
        Me.btnImportSave.Text = "Save"
        '
        'TabDatabase
        '
        Me.TabDatabase.Controls.Add(Me.txtDBName)
        Me.TabDatabase.Controls.Add(Me.Label2)
        Me.TabDatabase.Controls.Add(Me.txtDBPwd)
        Me.TabDatabase.Controls.Add(Me.Label12)
        Me.TabDatabase.Controls.Add(Me.btnDBBrowse)
        Me.TabDatabase.Controls.Add(Me.txtDBPath)
        Me.TabDatabase.Controls.Add(Me.Label18)
        Me.TabDatabase.Location = New System.Drawing.Point(4, 25)
        Me.TabDatabase.Name = "TabDatabase"
        Me.TabDatabase.Size = New System.Drawing.Size(312, 158)
        Me.TabDatabase.Text = "Database"
        '
        'txtDBName
        '
        Me.txtDBName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtDBName.Location = New System.Drawing.Point(74, 51)
        Me.txtDBName.MaxLength = 50
        Me.txtDBName.Name = "txtDBName"
        Me.txtDBName.ReadOnly = True
        Me.txtDBName.Size = New System.Drawing.Size(177, 21)
        Me.txtDBName.TabIndex = 28
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(7, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 20)
        Me.Label2.Text = "DB Name: "
        '
        'txtDBPwd
        '
        Me.txtDBPwd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtDBPwd.Location = New System.Drawing.Point(74, 78)
        Me.txtDBPwd.MaxLength = 50
        Me.txtDBPwd.Name = "txtDBPwd"
        Me.txtDBPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDBPwd.ReadOnly = True
        Me.txtDBPwd.Size = New System.Drawing.Size(177, 21)
        Me.txtDBPwd.TabIndex = 18
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label12.Location = New System.Drawing.Point(7, 84)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 20)
        Me.Label12.Text = "Password: "
        '
        'btnDBBrowse
        '
        Me.btnDBBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnDBBrowse.ForeColor = System.Drawing.Color.Black
        Me.btnDBBrowse.Location = New System.Drawing.Point(257, 27)
        Me.btnDBBrowse.Name = "btnDBBrowse"
        Me.btnDBBrowse.Size = New System.Drawing.Size(30, 20)
        Me.btnDBBrowse.TabIndex = 25
        Me.btnDBBrowse.Text = "..."
        Me.btnDBBrowse.Visible = False
        '
        'txtDBPath
        '
        Me.txtDBPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtDBPath.Location = New System.Drawing.Point(41, 24)
        Me.txtDBPath.MaxLength = 1000
        Me.txtDBPath.Name = "txtDBPath"
        Me.txtDBPath.ReadOnly = True
        Me.txtDBPath.Size = New System.Drawing.Size(210, 21)
        Me.txtDBPath.TabIndex = 17
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label18.Location = New System.Drawing.Point(7, 30)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(36, 20)
        Me.Label18.Text = "File: "
        '
        'TabInterval
        '
        Me.TabInterval.Controls.Add(Me.Label14)
        Me.TabInterval.Controls.Add(Me.btnInterSave)
        Me.TabInterval.Controls.Add(Me.Label13)
        Me.TabInterval.Controls.Add(Me.txtSTInterval)
        Me.TabInterval.Location = New System.Drawing.Point(4, 25)
        Me.TabInterval.Name = "TabInterval"
        Me.TabInterval.Size = New System.Drawing.Size(312, 158)
        Me.TabInterval.Text = "Interval"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label14.Location = New System.Drawing.Point(85, 56)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(210, 35)
        Me.Label14.Text = "in miliseconds to check for connection during Abnormal"
        '
        'btnInterSave
        '
        Me.btnInterSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnInterSave.Location = New System.Drawing.Point(218, 121)
        Me.btnInterSave.Name = "btnInterSave"
        Me.btnInterSave.Size = New System.Drawing.Size(86, 23)
        Me.btnInterSave.TabIndex = 7
        Me.btnInterSave.Text = "Save"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.Label13.Location = New System.Drawing.Point(19, 32)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 20)
        Me.Label13.Text = "Interval :"
        '
        'txtSTInterval
        '
        Me.txtSTInterval.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtSTInterval.Location = New System.Drawing.Point(85, 32)
        Me.txtSTInterval.Name = "txtSTInterval"
        Me.txtSTInterval.Size = New System.Drawing.Size(210, 21)
        Me.txtSTInterval.TabIndex = 1
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 431)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(651, 24)
        '
        'pnlAuthentication
        '
        Me.pnlAuthentication.BackColor = System.Drawing.Color.Transparent
        Me.pnlAuthentication.Controls.Add(Me.btnSubmitLogin)
        Me.pnlAuthentication.Controls.Add(Me.Panel9)
        Me.pnlAuthentication.Controls.Add(Me.txtAUTUNM)
        Me.pnlAuthentication.Controls.Add(Me.Label16)
        Me.pnlAuthentication.Controls.Add(Me.Panel2)
        Me.pnlAuthentication.Controls.Add(Me.txtAUTPWD)
        Me.pnlAuthentication.Controls.Add(Me.Label4)
        Me.pnlAuthentication.Location = New System.Drawing.Point(331, 3)
        Me.pnlAuthentication.Name = "pnlAuthentication"
        Me.pnlAuthentication.Size = New System.Drawing.Size(320, 275)
        Me.pnlAuthentication.Visible = False
        '
        'btnSubmitLogin
        '
        Me.btnSubmitLogin.Location = New System.Drawing.Point(0, 225)
        Me.btnSubmitLogin.Name = "btnSubmitLogin"
        Me.btnSubmitLogin.Size = New System.Drawing.Size(320, 22)
        Me.btnSubmitLogin.TabIndex = 92
        Me.btnSubmitLogin.Text = "Submit"
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel9.Controls.Add(Me.btnCloseLogin)
        Me.Panel9.Controls.Add(Me.Label22)
        Me.Panel9.Location = New System.Drawing.Point(0, 247)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(320, 24)
        '
        'btnCloseLogin
        '
        Me.btnCloseLogin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.btnCloseLogin.Location = New System.Drawing.Point(200, 2)
        Me.btnCloseLogin.Name = "btnCloseLogin"
        Me.btnCloseLogin.Size = New System.Drawing.Size(110, 20)
        Me.btnCloseLogin.TabIndex = 34
        Me.btnCloseLogin.Text = "Close"
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(0, 3)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(209, 18)
        Me.Label22.Text = "USER NAME:"
        '
        'txtAUTUNM
        '
        Me.txtAUTUNM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtAUTUNM.Location = New System.Drawing.Point(32, 81)
        Me.txtAUTUNM.MaxLength = 50
        Me.txtAUTUNM.Name = "txtAUTUNM"
        Me.txtAUTUNM.Size = New System.Drawing.Size(265, 21)
        Me.txtAUTUNM.TabIndex = 86
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(32, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 20)
        Me.Label16.Text = "Username :"
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
        Me.Label30.Location = New System.Drawing.Point(56, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(209, 21)
        Me.Label30.Text = "Login"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtAUTPWD
        '
        Me.txtAUTPWD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular)
        Me.txtAUTPWD.Location = New System.Drawing.Point(32, 127)
        Me.txtAUTPWD.MaxLength = 50
        Me.txtAUTPWD.Name = "txtAUTPWD"
        Me.txtAUTPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtAUTPWD.Size = New System.Drawing.Size(265, 21)
        Me.txtAUTPWD.TabIndex = 82
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(32, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 20)
        Me.Label4.Text = "Password :"
        '
        'frmSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(638, 472)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlAuthentication)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.pnlSetting)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetting"
        Me.Text = "JSP"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSetting.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabSetting.ResumeLayout(False)
        Me.TabOrganization.ResumeLayout(False)
        Me.TabScanner.ResumeLayout(False)
        Me.TabAuthorized.ResumeLayout(False)
        Me.TabScannerNo.ResumeLayout(False)
        Me.TabWebServices.ResumeLayout(False)
        Me.TabImport.ResumeLayout(False)
        Me.TabDatabase.ResumeLayout(False)
        Me.TabInterval.ResumeLayout(False)
        Me.pnlAuthentication.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSetting As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents TabSetting As System.Windows.Forms.TabControl
    Friend WithEvents TabScanner As System.Windows.Forms.TabPage
    Friend WithEvents TabAuthorized As System.Windows.Forms.TabPage
    Friend WithEvents TabWebServices As System.Windows.Forms.TabPage
    Friend WithEvents TabImport As System.Windows.Forms.TabPage
    Friend WithEvents TabDatabase As System.Windows.Forms.TabPage
    Friend WithEvents txtSTSCNID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSTBack As System.Windows.Forms.Button
    Friend WithEvents btnSTAUTPwd As System.Windows.Forms.Button
    Friend WithEvents txtSTPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSTWSOracle As System.Windows.Forms.TextBox
    Friend WithEvents txtSTWSDCSSP As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSTWSSave As System.Windows.Forms.Button
    Friend WithEvents btnImportSave As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboImpDay As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDBPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnDBBrowse As System.Windows.Forms.Button
    Friend WithEvents txtDBPath As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents pnlAuthentication As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtAUTPWD As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDBName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSTWSORAUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSTWSORAUserPwd As System.Windows.Forms.TextBox
    Friend WithEvents TabInterval As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSTInterval As System.Windows.Forms.TextBox
    Friend WithEvents btnInterSave As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TabOrganization As System.Windows.Forms.TabPage
    Friend WithEvents txtSTUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtAUTUNM As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cboOrganization As System.Windows.Forms.ComboBox
    Friend WithEvents btnOrgSave As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseLogin As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnSubmitLogin As System.Windows.Forms.Button
    Friend WithEvents TabScannerNo As System.Windows.Forms.TabPage
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtSTBatchID As System.Windows.Forms.TextBox

End Class
