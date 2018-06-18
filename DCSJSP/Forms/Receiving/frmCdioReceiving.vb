Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Net
Imports DCSJSP.GeneralFunction

Public Class frmCdioReceiving

#Region ", Properties ."

    Private Class CDIO_input
        Private _cdio_id As String
        Property CDIO_ID() As String
            Get
                Return _cdio_id
            End Get
            Set(ByVal value As String)
                _cdio_id = value
            End Set
        End Property

        Private _cdio_no As String
        Property CDIO_NO() As String
            Get
                Return _cdio_no
            End Get
            Set(ByVal value As String)
                _cdio_no = value
            End Set
        End Property

        Private _production_date As Date
        Property PRODUCTION_DATE() As Date
            Get
                Return _production_date
            End Get
            Set(ByVal value As Date)
                _production_date = value
            End Set
        End Property

        Private _exporter_code As String
        Property EXPORTER_CODE() As String
            Get
                Return _exporter_code
            End Get
            Set(ByVal value As String)
                _exporter_code = value
            End Set
        End Property

        Private _exporter As String
        Property EXPORTER() As String
            Get
                Return _exporter
            End Get
            Set(ByVal value As String)
                _exporter = value
            End Set
        End Property

        Private _container_id As String
        Property CONTAINER_ID() As String
            Get
                Return _container_id
            End Get
            Set(ByVal value As String)
                _container_id = value
            End Set
        End Property

        Private _container_no As String
        Property CONTAINER_NO() As String
            Get
                Return _container_no
            End Get
            Set(ByVal value As String)
                _container_no = value
            End Set
        End Property

        Private _org_id As String
        Property ORG_ID() As String
            Get
                Return _org_id
            End Get
            Set(ByVal value As String)
                _org_id = value
            End Set
        End Property

        Private _scan_flag As Char
        Property SCAN_FLAG() As Char
            Get
                Return _scan_flag
            End Get
            Set(ByVal value As Char)
                _scan_flag = value
            End Set
        End Property
    End Class

    Private Class Module_Input
        Private _module_no As String = Nothing
        Property MODULE_NO() As String
            Get
                Return _module_no
            End Get
            Set(ByVal value As String)
                _module_no = value
            End Set
        End Property

        Private _pilling_no As String
        Property PILLING_NO() As String
            Get
                Return _pilling_no
            End Get
            Set(ByVal value As String)
                _pilling_no = value
            End Set
        End Property

        Private _gross_weight As Decimal
        Property GROSS_WEIGHT() As Decimal
            Get
                Return _gross_weight
            End Get
            Set(ByVal value As Decimal)
                _gross_weight = value
            End Set
        End Property

        Private _order_no As String = Nothing
        Property ORDER_NO() As String
            Get
                Return _order_no
            End Get
            Set(ByVal value As String)
                _order_no = value
            End Set
        End Property

        Private _reasonCd As String = Nothing
        Property REASON_CD() As String
            Get
                Return _reasonCd
            End Get
            Set(ByVal value As String)
                _reasonCd = value
            End Set
        End Property
    End Class

#End Region

#Region ". Variable Declaration ."

    Dim modScanOffline As Boolean = False
    Dim clsDataTransfer As New clsDataTransfer
    Dim msgCode As String = Nothing
    Dim msgDesc As String = Nothing
    Dim curr_BATCH As String = Nothing
    Dim prev_BATCH As String = Nothing
    Dim skipFlag As Boolean = False
    Dim cntPending As Integer = 0
    Dim cntScanned As Integer = 0
    Dim cntAbns As Integer = 0
    Dim cntAbnPost As Integer = 0
    Dim showError As Boolean = True
    Dim offline1stTFlag As Boolean = True
    Private Const strOnlineTitle As String = "CDIO Receiving"
    Private Const strOfflineTitle As String = "Abnormal CDIO Receiving"
    Public mode As Boolean = False
    Private CDIO As New CDIO_input()
    Private MODD As New Module_Input()

#End Region

#Region ". Main Navigation ."

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmMain_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not skipFlag Then
            If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Public Sub Init()
        Try
            Me.Text = strOnlineTitle
            footerStatusBar.Visible = False
            bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScan)
            Call InitCdioBatch()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region ". Form Events/Private Function ."

    Private Function CheckDataImportOnShedule() As Boolean
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader = Nothing

        Try
            CheckDataImportOnShedule = False

            dbReader = OpenRecordset(String.Format("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'SCHEDULE' AND SettingValue = '{0}'", Format(Now, "dddd")), objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    CheckDataImportOnShedule = True
                End If
            End If

            Return CheckDataImportOnShedule

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "CheckDataImportOnShedule")
        End Try
    End Function

    Private Function CheckDataImportOnToday() As Boolean
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader = Nothing

        Try
            CheckDataImportOnToday = False

            dbReader = OpenRecordset("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'IMPORTDATETIME' AND SettingValue >= '" & Format(Now, gStrTimeFormatSQLCE) & "' ", objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) = 0 Then
                    sSQL = "UPDATE TblSetting SET SettingValue = '" & Format(Now, gStrTimeFormatSQLCE) & "' WHERE SettingCode = 'IMPORTDATETIME' "
                    ExecuteSQL(sSQL)
                    CheckDataImportOnToday = True
                End If
            End If

            Return CheckDataImportOnShedule()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "CheckDataImportOnToday")
        End Try
    End Function

    Private Sub btnScanCdio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanCdio.Click
        Call LoadCDIO()
        mode = True
        bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvMain)
    End Sub

    Private Sub btnAbnormalCdio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalCdio.Click
        Call LoadAbnScanModule()
        mode = False
        timer.Enabled = True
        modScanOffline = True
        bringPanelToFront(pnlCdioRcvAbn, pnlCdioRcvMain)
    End Sub

    Private Sub btnNextScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextScan.Click
        Call LoadScanModule()
        bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvScan)
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        Call LoadCDIODetails()
        bringPanelToFront(pnlCdioRcvViewDet, pnlCdioRcvScanModule)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanModule.Click
        txtFSModuleNo.Focus()
        Call LoadForceScan()
        bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvScanModule)
    End Sub

    Private Sub btnFAbnScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFAbnScanModule.Click
        txtFSAbnModuleNo.Focus()
        Call LoadAbnForceScan()
        bringPanelToFront(pnlCdioRcvAbnFScan, pnlCdioRcvAbnScan)
    End Sub

    Private Sub btnCdioRcvAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnScan.Click
        Call LoadAbnScanModule()
        bringPanelToFront(pnlCdioRcvAbnScan, pnlCdioRcvAbn)
    End Sub

    Private Sub btnCdioRcvAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnView.Click
        Call LoadAbnDetails()
        bringPanelToFront(pnlCdioRcvAbnViewDet, pnlCdioRcvAbn)
    End Sub

    Private Sub btnCdioRcvAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnPost.Click
        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    If Counter("Pending") = 0 Then
                        txtUsername.Text = String.Empty
                        txtPwd.Text = String.Empty
                        txtUsername.Focus()
                        bringPanelToFront(pnlLogin, pnlCdioRcvAbn)
                    Else
                        MessageBox.Show("CDIO Not Completed Yet. Unable to post.")
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("No connection to post.")
        End Try
    End Sub

    Private Sub btnCdioRcvAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnDelete.Click
        Call LoadAbnDelete()
        bringPanelToFront(pnlCdioRcvDelete, pnlCdioRcvAbn)
    End Sub

    Private Sub btnBackRcvScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRcvScanModule.Click
        Me.Text = strOnlineTitle
        txtCDIONo.Focus()
        txtCDIONo.SelectAll()
        bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvScanModule)
    End Sub

    Private Sub btnBackFrViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFrViewDet.Click
        Call LoadScanModule()
        bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvViewDet)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Dim cnt As Integer = 0
        Dim listViews As ListViewItem = New ListViewItem()

        For i As Integer = 0 To lstViewRCVFScan.Items.Count - 1
            listViews = lstViewRCVFScan.Items(i)
            If Not listViews.Selected Then
                cnt = cnt + 1
            End If
            If cnt = lstViewRCVFScan.Items.Count Then
                MsgBox("Failed to save. Reason is required.", MsgBoxStyle.Critical, "Module Force Scan")
                Exit Sub
            End If
        Next
        MODD.MODULE_NO = txtFSModuleNo.Text.Trim
        MODD.ORDER_NO = txtFSOrderNo.Text.Trim
        MODD.REASON_CD = lstViewRCVFScan.FocusedItem.SubItems(1).Text
        MODD.PILLING_NO = "0"
        MODD.GROSS_WEIGHT = 0D
        Call ModuleScanChecker(True)
    End Sub

    Private Sub btnSaveAbnForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAbnForceScan.Click
        Dim cnt As Integer = 0
        Dim listViews As ListViewItem = New ListViewItem()

        For i As Integer = 0 To ListView3.Items.Count - 1
            listViews = ListView3.Items(i)
            If Not listViews.Selected Then
                cnt = cnt + 1
            End If
            If cnt = ListView3.Items.Count Then
                MsgBox("Failed to save. Reason is required.", MsgBoxStyle.Critical, "Abnormal Module Force Scan")
                Exit Sub
            End If
        Next
        MODD.MODULE_NO = txtFSAbnModuleNo.Text.Trim
        MODD.ORDER_NO = txtFSAbnOrderNo.Text.Trim
        MODD.PILLING_NO = "0"
        MODD.GROSS_WEIGHT = 0D
        MODD.REASON_CD = ListView3.FocusedItem.SubItems(1).Text
        Call AbnModuleScanChecker(True)
    End Sub

    Private Sub btnForce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
        If lstCdioRcvModule.FocusedItem Is Nothing Then
            MsgBox("Please select pending items to force scan.", MsgBoxStyle.Critical, "CDIO Force")
            Exit Sub
        End If
        Call LoadForceScan(True)
        bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvViewDet)
    End Sub

    Private Sub btnBackFrAbnormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFrAbnormal.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCdioRcvAbn, pnlCdioRcvAbnScan)
    End Sub

    Private Sub btnCloseRcvViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRcvViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCdioRcvAbn, pnlCdioRcvAbnViewDet)
    End Sub

    Private Sub btnCloseRcvPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRcvPosting.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCdioRcvAbn, pnlCdioRcvPosting)
    End Sub

    Private Sub btnCloseRcvDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRcvDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCdioRcvAbn, pnlCdioRcvDelete)
    End Sub

    Private Sub btnCloseAbnCdio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnCdio.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvAbn)
    End Sub

    Private Sub btnCloseCdioRcv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCdioRcv.Click
        Me.Close()
    End Sub

    Private Sub btnBackCdioRcvScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackCdioRcvScan.Click
        Me.Text = strOnlineTitle
        timer.Enabled = False
        timer.Dispose()
        bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScan)
    End Sub

    Private Sub btnFScanCdio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvScan)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call LoadScanModule()
        bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvFScan)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call LoadAbnScanModule()
        bringPanelToFront(pnlCdioRcvAbnScan, pnlCdioRcvAbnFScan)
    End Sub

    Private Sub txtFSModuleNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSModuleNo.TextChanged
        If Not String.IsNullOrEmpty(txtFSModuleNo.Text) Then
            If String.IsNullOrEmpty(txtFSOrderNo.Text) Then
                btnSaveForceScan.Enabled = False
            Else
                btnSaveForceScan.Enabled = True
            End If
        Else
            btnSaveForceScan.Enabled = False
        End If
    End Sub

    Private Sub txtFSOrderNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSOrderNo.TextChanged
        If Not String.IsNullOrEmpty(txtFSOrderNo.Text) Then
            If String.IsNullOrEmpty(txtFSModuleNo.Text) Then
                btnSaveForceScan.Enabled = False
            Else
                btnSaveForceScan.Enabled = True
            End If
        Else
            btnSaveForceScan.Enabled = False
        End If
    End Sub

    Private Sub txtFSAbnModuleNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSAbnModuleNo.TextChanged
        If Not String.IsNullOrEmpty(txtFSAbnModuleNo.Text) Then
            If String.IsNullOrEmpty(txtFSAbnOrderNo.Text) Then
                btnSaveAbnForceScan.Enabled = False
            Else
                btnSaveAbnForceScan.Enabled = True
            End If
        Else
            btnSaveAbnForceScan.Enabled = False
        End If
    End Sub

    Private Sub txtFSAbnOrderNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSAbnOrderNo.TextChanged
        If Not String.IsNullOrEmpty(txtFSAbnOrderNo.Text) Then
            If String.IsNullOrEmpty(txtFSAbnModuleNo.Text) Then
                btnSaveAbnForceScan.Enabled = False
            Else
                btnSaveAbnForceScan.Enabled = True
            End If
        Else
            btnSaveAbnForceScan.Enabled = False
        End If
    End Sub

#End Region

#Region ". CDIO Scan ."

    Private Sub LoadCDIO()
        Me.Text = strOnlineTitle
        txtCDIONo.Focus()
        txtCDIONo.Text = String.Empty
        lblRcvContainer.Text = String.Empty
        lblRcvExporter.Text = String.Empty
        btnNextScan.Enabled = False
        Label44.BackColor = Color.LimeGreen
        Label44.Text = "Scan.."
        Label3.Text = String.Empty
    End Sub

    Private Sub txtCDIONo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCDIONo.KeyDown

        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o n l i n e
                mode = True
                If Not String.IsNullOrEmpty(txtCDIONo.Text) Then
                    Call IsCDIOReadable(txtCDIONo.Text)
                    Call VerifyOrgId()
                    Call InitWebServices()
                    Call CDIOScanChecker()
                Else
                    Throw New Exception("Failed to read the CDIO QR.")
                End If
            Catch ex As WebException
                'o f f l i n e
                mode = False
                timer.Enabled = True
                Call ChangeProcess()
            Catch ex As CustomException
                MsgBox(String.Format("Access Restricted!{0}{1}", Environment.NewLine, ex.Message), MsgBoxStyle.Critical, "Organization ID Mismatch")
                skipFlag = True
                Me.Close()
            Catch ex As Exception
                MsgBox("CDIO scanned failed.", MsgBoxStyle.Critical, "CDIO Scan")
                Cursor.Current = Cursors.Default
                txtCDIONo.Focus()
                txtCDIONo.SelectAll()
            End Try
        End If

    End Sub

    Private Sub CDIOScanChecker()
        Cursor.Current = Cursors.WaitCursor
        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then '***
                msgCode = ws_validationClient.processValidation(curr_BATCH, gScannerID, _
                                    "CKDRECV", "101", CDIO.CDIO_ID, CDIO.CDIO_NO, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    CDIO.ORG_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, gScannerID, GetServerTime, gScannerID, Nothing, gScannerID, _
                                    GetServerTime, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, msgDesc)
                If msgCode = "OK" Then
                    Call GetModuleList()
                    msgDesc = "Succesfully Updated"
                    SetPanelForResult("PanelOk", msgCode, msgDesc)
                ElseIf msgCode = "NG" Then
                    SetPanelForResult("PanelError", msgCode, IIf(Not String.IsNullOrEmpty(msgDesc), msgDesc, "Invalid CDIO No"))
                End If
            End If
        End If
    End Sub

    Private Sub GetModuleList()
        Dim sqlStr As String = Nothing
        Dim dtModuleList As DataTable = New DataTable()
        Dim dbReader As SqlCeDataReader = Nothing
        Dim dt As DataTable = New DataTable()

        If ws_dcsClient.isConnected Then '***
            dtModuleList = ws_dcsClient.getData("*", "JSP_CDIO_DETAILS_VIEW", " AND CDIO_NO = " & SQLQuote(CDIO.CDIO_NO))
            If dtModuleList.Rows.Count > 0 Then
                For i As Integer = 0 To dtModuleList.Rows.Count - 1
                    dbReader = OpenRecordset("SELECT COUNT(*) FROM [" & CDIO_PENDING & "] WHERE MODULE_NO = " & _
                                             SQLQuote(dtModuleList.Rows(i).Item("MODULE_NO").ToString), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) = 0 Then
                            dt = getData("SELECT * FROM [" & CDIO_INTERFACE & "] WHERE MODULE_NO = " & SQLQuote(dtModuleList.Rows(i).Item("MODULE_NO").ToString))
                            If dt.Rows.Count = 0 Then
                                sqlStr = "INSERT INTO [" & CDIO_PENDING & "] (CDIO_ID, CDIO_NO, MODULE_ID, MODULE_NO, MODULE_NAME, ORDER_NO, ORG_ID)"
                                sqlStr = sqlStr & " VALUES (" & SQLQuote(dtModuleList.Rows(i).Item("CDIO_ID").ToString.PadLeft(10, "0")) & " , "
                                sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("CDIO_NO").ToString) & " , "
                                sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("MODULE_ID").ToString) & " , "
                                sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("MODULE_NO").ToString) & " , "
                                sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("MODULE_NAME").ToString) & " , "
                                sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("ORDER_NO").ToString) & " , "
                                sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("ORG_ID").ToString)
                                sqlStr = sqlStr & ")"
                                If ExecuteSQL(sqlStr) = False Then
                                    Throw New Exception("Failed to retrieved Module List from server.")
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#Region ". Scan Module ."

    Private Sub LoadScanModule()
        Me.Text = strOnlineTitle
        lblCdioNo.Text = CDIO.CDIO_NO
        txtModuleQR.Focus()
        txtModuleQR.Text = String.Empty
        txtModuleNo.Text = String.Empty
        txtOrderNo.Text = String.Empty
        Label60.BackColor = Color.LimeGreen
        Label60.Text = "Scan.."
        Label62.Text = String.Empty
        lblTotalScanned.Text = Counter("Scanned")
    End Sub

    Private Sub LoadForceScan(Optional ByVal IsPopulate As Boolean = Nothing)
        Me.Text = strOnlineTitle
        Call PopulateCDIOForceScan(Nothing)
        If IsPopulate Then
            txtFSModuleNo.Text = lstCdioRcvModule.FocusedItem.SubItems(2).Text
            txtFSOrderNo.Text = lstCdioRcvModule.FocusedItem.SubItems(1).Text
        Else
            txtFSModuleNo.Text = String.Empty
            txtFSOrderNo.Text = String.Empty
        End If
    End Sub

    Private Sub txtModuleQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtModuleQR.KeyDown

        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o n l i n e
                mode = True
                If Not String.IsNullOrEmpty(txtModuleQR.Text) Then
                    If Not IsModuleReadable(txtModuleQR.Text, Nothing) Then
                        Exit Sub
                    End If
                    Call ModuleScanChecker()
                Else
                    Throw New Exception("Failed to read the Module QR.")
                End If
            Catch ex As WebException
                'o f f l i n e
                mode = False
                timer.Enabled = True
                modScanOffline = True
                If offline1stTFlag Then
                    If MessageBox.Show("Connection is down. Continue scan with offline mode?", _
                                       "Module Scan Offline Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        offline1stTFlag = False
                        Call ModuleScanOffline()
                    Else
                        Cursor.Current = Cursors.Default
                        txtModuleQR.Focus()
                        txtModuleQR.SelectAll()
                        Exit Sub
                    End If
                Else
                    Call ModuleScanOffline()
                End If
            Catch ex As CustomException
                MsgBox(String.Format("Access Restricted!{0}{1}", Environment.NewLine, ex.Message), MsgBoxStyle.Critical, "Organization ID Mismatch")
                skipFlag = True
                Me.Close()
            Catch ex As Exception
                MsgBox("Module scanned failed.", MsgBoxStyle.Critical, "Module Scan")
                Cursor.Current = Cursors.Default
                txtModuleQR.Focus()
                txtModuleQR.SelectAll()
            End Try
        End If
    End Sub

    Private Sub ModuleScanChecker(Optional ByVal IsForceScan As Boolean = Nothing)
        Dim dt As DateTime = Nothing
        Dim currentDate As String = Nothing
        Cursor.Current = Cursors.WaitCursor

        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then '***
                dt = DateTime.ParseExact(GetServerTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
                currentDate = dt.ToString("dd-MM-yyyy hh:mm:ss tt")
                msgCode = ws_validationClient.processValidation(curr_BATCH, gScannerID, "CKDRECV", "102", _
                                   CDIO.CDIO_ID, CDIO.CDIO_NO, MODD.PILLING_NO, MODD.GROSS_WEIGHT.ToString, _
                                   Nothing, Nothing, IIf(IsForceScan, "Y", "N"), _
                                   IIf(IsForceScan, MODD.REASON_CD, Nothing), Nothing, MODD.MODULE_NO, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, MODD.ORDER_NO, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   CDIO.ORG_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, gScannerID, currentDate.ToString(), gScannerID, "N", gScannerID, currentDate.ToString(), _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, msgDesc)

                currentDate = dt.ToString("yyyy-MM-dd hh:mm:ss tt")

                If msgCode = "OK" Then
                    Call AddToCDIOInterface(CDIO.CDIO_ID, CDIO.CDIO_NO, Nothing, MODD.MODULE_NO, MODD.PILLING_NO, MODD.GROSS_WEIGHT, MODD.ORDER_NO, _
                                            CDIO.ORG_ID, IIf(IsForceScan, MODD.REASON_CD, Nothing), "N", currentDate)
                    Call UpdateCDIOPendingDetails(MODD.MODULE_NO, MODD.ORDER_NO)
                    Call UpdatePostStatus(MODD.MODULE_NO, MODD.ORDER_NO)
                    msgDesc = "Succesfully Updated"
                    SetPanelForResult("PanelModOk", msgCode, msgDesc, IsForceScan)
                ElseIf msgCode = "NG" Then
                    SetPanelForResult("PanelModError", msgCode, IIf(Not String.IsNullOrEmpty(msgDesc), msgDesc, "Invalid Module No"), IsForceScan)
                ElseIf msgCode = "CP" Then
                    Call AddToCDIOInterface(CDIO.CDIO_ID, CDIO.CDIO_NO, Nothing, MODD.MODULE_NO, MODD.PILLING_NO, MODD.GROSS_WEIGHT, MODD.ORDER_NO, _
                                            CDIO.ORG_ID, IIf(IsForceScan, MODD.REASON_CD, Nothing), "N", currentDate)
                    Call UpdateCDIOPendingDetails(MODD.MODULE_NO, MODD.ORDER_NO)
                    Call UpdatePostStatus(MODD.MODULE_NO, MODD.ORDER_NO)
                    If ws_dcsClient.isOracleConnected Then '***
                        msgCode = ws_inventoryClient.processInventoryConsumption(curr_BATCH, "CKDRECV", "103", CDIO.ORG_ID, Nothing, _
                                                                                 Nothing, Nothing, Nothing, Nothing, msgDesc)
                        If msgCode = "OK" Then
                            ExecuteSQL(String.Format("DELETE FROM [{0}] WHERE RCV_INTERFACE_BATCH_ID = {1}", CDIO_INTERFACE, SQLQuote(curr_BATCH)))
                            SetPanelForResult("PanelModPost", msgCode, msgDesc, IsForceScan)
                        Else
                            Throw New Exception(String.Format("Failed to Post Module. - {0}", msgDesc))
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ModuleScanOffline()
        'o f f l i n e
        Dim dtModuleList As DataTable = New DataTable()

        Try
            Cursor.Current = Cursors.WaitCursor
            dtModuleList = getData(String.Format("SELECT * FROM [{0}] WHERE MODULE_NO = {1}", CDIO_PENDING, SQLQuote(MODD.MODULE_NO)))
            If dtModuleList.Rows.Count > 0 Then
                Call AddToCDIOInterface(dtModuleList.Rows(0).Item("CDIO_ID"), _
                                        dtModuleList.Rows(0).Item("CDIO_NO").ToString, _
                                        dtModuleList.Rows(0).Item("MODULE_ID").ToString, _
                                        dtModuleList.Rows(0).Item("MODULE_NO").ToString, _
                                        MODD.PILLING_NO, _
                                        MODD.GROSS_WEIGHT, _
                                        dtModuleList.Rows(0).Item("ORDER_NO").ToString, _
                                        dtModuleList.Rows(0).Item("ORG_ID").ToString, _
                                        Nothing, _
                                        "Y", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"))
                Call UpdateCDIOPendingDetails(dtModuleList.Rows(0).Item("MODULE_NO").ToString, _
                                              dtModuleList.Rows(0).Item("ORDER_NO").ToString)
                msgCode = "OK"
                msgDesc = "Succesfully Updated"
                SetPanelForResult("PanelModOk", msgCode, msgDesc)
            Else
                msgCode = "NG"
                msgDesc = "Module Already Scanned."
                SetPanelForResult("PanelModError", msgCode, msgDesc)
            End If
        Catch ex As Exception
            MsgBox("Module scanned failed.", MsgBoxStyle.Critical, "Module Scan Offline Mode")
            Cursor.Current = Cursors.Default
            txtModuleQR.Focus()
            txtModuleQR.SelectAll()
        End Try
    End Sub

#End Region

#Region ". Global Helper ."

    Private Sub ChangeProcess()
        Cursor.Current = Cursors.Default
        If showError = True Then
            showError = False
            If modScanOffline Then
                showError = True
                Exit Sub
            Else
                If MessageBox.Show("Connection is down. Change to Abnormal Process?", "CDIO Process Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    timer.Enabled = False
                    timer.Dispose()
                    showError = True
                    bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScan)
                    Exit Sub
                Else
                    showError = True
                    txtCDIONo.Focus()
                    txtCDIONo.SelectAll()
                End If
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer.Tick
        Try
            If mode = False Then
                If ws_dcsClient.isConnected Then
                    If ws_dcsClient.isOracleConnected Then
                        MsgBox("Connection is back. Please Try Again to Scan.", MsgBoxStyle.Information, Me.Text)
                        mode = True
                        modScanOffline = False
                        timer.Enabled = False
                        timer.Dispose()
                    End If
                End If
            End If
        Catch ex As Exception
            Call ChangeProcess()
        End Try
    End Sub

    Private Sub SetPanelForResult(ByVal Panel As String, ByVal msg As String, ByVal desc As String, _
                                  Optional ByVal IsForceScan As Boolean = Nothing)
        Cursor.Current = Cursors.Default
        If Panel = "PanelOk" Then
            Label44.BackColor = Color.LimeGreen
            Label44.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label3.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            btnNextScan.Enabled = True
            lblRcvContainer.Text = CDIO.CONTAINER_NO
            lblRcvExporter.Text = CDIO.EXPORTER
            txtCDIONo.Focus()
            txtCDIONo.SelectAll()
        ElseIf Panel = "PanelError" Then
            Label44.BackColor = Color.Red
            Label44.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label3.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            btnNextScan.Enabled = False
            lblRcvContainer.Text = Nothing
            lblRcvExporter.Text = Nothing
            txtCDIONo.Focus()
            txtCDIONo.SelectAll()
        ElseIf Panel = "PanelModOk" Then
            Label60.BackColor = Color.LimeGreen
            Label60.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label62.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            txtModuleNo.Text = MODD.MODULE_NO
            txtOrderNo.Text = MODD.ORDER_NO
            lblTotalScanned.Text = Counter("Scanned")
            txtModuleQR.Focus()
            txtModuleQR.SelectAll()
            If Counter("Pending") = 0 Then
                MsgBox(successMsgModule, MsgBoxStyle.Information, Me.Text)
                Call LoadCDIO()
                If IsForceScan Then
                    bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvFScan)
                Else
                    bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvScanModule)
                End If
            End If
            If IsForceScan Then
                bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvFScan)
            End If
        ElseIf Panel = "PanelModError" Then
            Label60.BackColor = Color.Red
            Label60.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label62.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            txtModuleNo.Text = Nothing
            txtOrderNo.Text = Nothing
            txtModuleQR.Focus()
            txtModuleQR.SelectAll()
            If IsForceScan Then
                bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvFScan)
            End If
        ElseIf Panel = "PanelModPost" Then
            Label60.BackColor = Color.LimeGreen
            Label60.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label62.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            txtModuleNo.Text = MODD.MODULE_NO
            txtOrderNo.Text = MODD.ORDER_NO
            lblTotalScanned.Text = Counter("Scanned")
            MsgBox(successMsgModule, MsgBoxStyle.Information, Me.Text)
            Call LoadCDIO()
            If IsForceScan Then
                bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvFScan)
            Else
                bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvScanModule)
            End If
            Call UpdateBatch()
        ElseIf Panel = "PanelAbnModOk" Then
            lblScanAbn.BackColor = Color.LimeGreen
            lblScanAbn.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            lblScanDescAbn.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            Label13.Text = MODD.MODULE_NO
            Label2.Text = MODD.ORDER_NO
            lblTotalAScanned.Text = Counter("Abnormal")
            txtModuleQRAbn.Focus()
            txtModuleQRAbn.SelectAll()
            If IsForceScan Then
                bringPanelToFront(pnlCdioRcvAbnScan, pnlCdioRcvAbnFScan)
            End If
        ElseIf Panel = "PanelAbnModError" Then
            lblScanAbn.BackColor = Color.Red
            lblScanAbn.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            lblScanDescAbn.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            Label13.Text = Nothing
            Label2.Text = Nothing
            lblTotalAScanned.Text = Counter("Abnormal")
            txtModuleQRAbn.Focus()
            txtModuleQRAbn.SelectAll()
            If IsForceScan Then
                bringPanelToFront(pnlCdioRcvAbnScan, pnlCdioRcvAbnFScan)
            End If
        ElseIf Panel = "PanelAbnModPost" Then
            MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
            Call LoadAbnPost()
            Call UpdateBatch()
        End If
    End Sub

    Function Counter(ByVal state As String) As String
        Dim result As String = Nothing

        If state = "Scanned" Then
            Call PopulateCDIOScannedDetails(CDIO.CDIO_NO)
            result = cntScanned.ToString()
        ElseIf state = "Pending" Then
            Call PopulateCDIOPendingDetails(CDIO.CDIO_NO)
            result = cntPending.ToString()
        ElseIf state = "Abnormal" Then
            Call PopulateAbnPostDetails()
            result = cntAbnPost.ToString
        End If
        Return result
    End Function

    Private Sub AddToCDIOInterface(ByVal CDIO_ID As String, _
                                   ByVal CDIO_NO As String, _
                                   ByVal MODULE_ID As Integer, _
                                   ByVal MODULE_NO As String, _
                                   ByVal PILLING_NO As String, _
                                   ByVal GROSS_WEIGHT As Decimal, _
                                   ByVal ORDER_NO As String, _
                                   ByVal ORG_ID As String, _
                                   ByVal REASON_CD As String, _
                                   ByVal ONOFFLINE_FLAG As String, _
                                   ByVal DT As String)
        Dim dbReader As SqlCeDataReader = Nothing
        Dim sqlStr As String = Nothing

        dbReader = OpenRecordset("SELECT COUNT(*) FROM [" & CDIO_INTERFACE & "] WHERE MODULE_NO = " & SQLQuote(MODULE_NO), objConn)
        If dbReader.Read Then
            If CInt(dbReader(0)) = 0 Then
                sqlStr = "INSERT INTO [" & CDIO_INTERFACE & "] (RCV_INTERFACE_BATCH_ID, CDIO_ID, CDIO_NO, MODULE_ID, MODULE_NO, PILLING_ORDER, GROSS_WEIGHT, ORDER_NO, ORG_ID, " _
                       & "FORCE_MODULE_STATUS, FORCE_MODULE_REASON_ID, CREATED_BY, CREATED_DATE, ON_OFF_LINE_FLAG) "
                sqlStr = sqlStr & "VALUES ("
                sqlStr = sqlStr & SQLQuote(curr_BATCH) & " , "
                sqlStr = sqlStr & SQLQuote(CDIO_ID) & " , "
                sqlStr = sqlStr & SQLQuote(CDIO_NO) & " , "
                sqlStr = sqlStr & SQLQuote(MODULE_ID) & " , "
                sqlStr = sqlStr & SQLQuote(MODULE_NO) & " , "
                sqlStr = sqlStr & SQLQuote(PILLING_NO) & " , "
                sqlStr = sqlStr & SQLQuote(GROSS_WEIGHT) & " , "
                sqlStr = sqlStr & SQLQuote(ORDER_NO) & " , "
                sqlStr = sqlStr & SQLQuote(ORG_ID) & " , "
                sqlStr = sqlStr & SQLQuote(IIf(Not String.IsNullOrEmpty(REASON_CD), "Y", "N")) & " , "
                sqlStr = sqlStr & IIf(Not String.IsNullOrEmpty(REASON_CD), REASON_CD, "null") & ", "
                sqlStr = sqlStr & SQLQuote(gScannerID) & " , "
                sqlStr = sqlStr & SQLQuote(DT) & " , "
                sqlStr = sqlStr & SQLQuote(ONOFFLINE_FLAG)
                sqlStr = sqlStr & ")"
                If ExecuteSQL(sqlStr) = False Then
                    Throw New Exception("Failed to Create Local Module Table.")
                End If
            Else
                Throw New Exception("Module Already Existed On Local Database.")
            End If
        End If
    End Sub

    Private Sub UpdatePostStatus(ByVal MODULE_NO As String, ByVal ORDER_NO As String)
        If Not ExecuteSQL("UPDATE [" & CDIO_INTERFACE & "] " & _
                              "SET PROCESS_FLAG = 'POSTED' " & _
                              "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                              "AND ORDER_NO = " & SQLQuote(ORDER_NO)) Then
            Throw New Exception("Failed to Update Local Flag.")
        End If
    End Sub

    Private Sub UpdateCDIOPendingDetails(ByVal MODULE_NO As String, ByVal ORDER_NO As String)
        Dim dt As DataTable = New DataTable()

        dt = getData("SELECT * FROM [" & CDIO_PENDING & "] " & _
                                      "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                                      "AND ORDER_NO = " & SQLQuote(ORDER_NO))
        If dt.Rows.Count > 0 Then
            If Not ExecuteSQL("DELETE FROM [" & CDIO_PENDING & "] " & _
                         "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                         "AND ORDER_NO = " & SQLQuote(ORDER_NO)) Then
                Throw New Exception("Failed to Create Local Module Table.")
            End If
        End If
    End Sub

    Function GetServerTime() As String
        If mode = True Then
            Return ws_dcsClient.getTime
        ElseIf mode = False Then
            Return DateTime.Now.ToString
        End If
        Return Nothing
    End Function

    Private Sub VerifyOrgId()
        If Not org_ID = CDIO.ORG_ID.TrimStart("0"c) Then
            Throw New CustomException("Organization ID does not match Setting configuration.")
        End If
    End Sub

#End Region

#Region ". QR String Validation Helper ."
    'CDIO
    Private Sub IsCDIOReadable(ByVal input As String)
        Dim errorMsg As String = "Invalid CDIO."
        Dim temp_cdio_id As String = Nothing
        Dim temp_cdio_no As String = Nothing
        Dim format As String = "dd/MM/yyyy"
        Dim temp_prod_date As String = Nothing
        Dim formatted As Date = Nothing
        Dim temp_exporter_code As String = Nothing
        Dim expLength As Integer = Nothing
        Dim cidNewIndex As Integer = Nothing
        Dim temp_container_id As String = Nothing
        Dim cnoNewIndex As Integer = Nothing
        Dim temp_container_no As String = Nothing
        Dim orgidNewIndex As Integer = Nothing
        Dim temp_org_id As String = Nothing
        Dim scnflagNewIndex As Integer = Nothing
        Dim temp_scan_flag As String = Nothing
        Dim temp_exporter As String = Nothing

        'CDIO_ID - start
        temp_cdio_id = input.Substring(0, CDIO_ID_LENGTH)
        If Not String.IsNullOrEmpty(temp_cdio_id) And IsNumber(temp_cdio_id) Then
            CDIO.CDIO_ID = temp_cdio_id
        Else
            Throw New FormatException(errorMsg)
        End If
        'CDIO_ID - end

        'CDIO_NO - start
        temp_cdio_no = input.Substring(10, CDIO_NO_LENGTH)
        If Not String.IsNullOrEmpty(temp_cdio_no) Then
            CDIO.CDIO_NO = temp_cdio_no
        Else
            Throw New FormatException(errorMsg)
        End If
        'CDIO_NO - end

        'PRODUCTION_DATE - start

        temp_prod_date = input.Substring(23, PROD_DATE_LENGTH)
        If Not String.IsNullOrEmpty(temp_prod_date) Then
            Try
                formatted = Date.ParseExact(temp_prod_date, format, CultureInfo.InvariantCulture)
                CDIO.PRODUCTION_DATE = formatted
            Catch ex As FormatException
                Throw New FormatException(errorMsg)
            End Try
        Else
            Throw New FormatException(errorMsg)
        End If
        'PRODUCTION_DATE - end

        'EXPORTER_CODE - start
        temp_exporter_code = input.Substring(33, EXPORTER_CODE_LENGTH)
        If Not String.IsNullOrEmpty(temp_exporter_code) Then
            CDIO.EXPORTER_CODE = temp_exporter_code
        Else
            Throw New FormatException(errorMsg)
        End If
        'EXPORTER_CODE - end

        'EXPORTER - start
        expLength = input.Length - 36 - (CONTAINER_ID_LENGTH + CONTAINER_NO_LENGTH + ORG_ID_LENGTH + SCAN_FLAG_LENGTH)
        If expLength <= EXPORTER_LENGTH Then
            temp_exporter = input.Substring(36, expLength)
            If Not String.IsNullOrEmpty(temp_exporter) Then
                CDIO.EXPORTER = temp_exporter
            Else
                Throw New FormatException(errorMsg)
            End If
        Else
            Throw New FormatException(errorMsg)
        End If
        'EXPORTER - end

        'CONTAINER_ID - start
        cidNewIndex = 36 + expLength
        temp_container_id = input.Substring(cidNewIndex, CONTAINER_ID_LENGTH)
        If Not String.IsNullOrEmpty(temp_container_id) And IsNumber(temp_container_id) Then
            CDIO.CONTAINER_ID = temp_container_id
        Else
            Throw New FormatException(errorMsg)
        End If
        'CONTAINER_ID - end

        'CONTAINER_NO - start
        cnoNewIndex = cidNewIndex + CONTAINER_ID_LENGTH
        temp_container_no = input.Substring(cnoNewIndex, CONTAINER_NO_LENGTH)
        If Not String.IsNullOrEmpty(temp_container_no) Then
            CDIO.CONTAINER_NO = temp_container_no
        Else
            Throw New FormatException(errorMsg)
        End If
        'CONTAINER_NO - end

        'ORG_ID - start
        orgidNewIndex = cnoNewIndex + CONTAINER_NO_LENGTH
        temp_org_id = input.Substring(orgidNewIndex, ORG_ID_LENGTH)
        If Not String.IsNullOrEmpty(temp_org_id) And IsNumber(temp_org_id) Then
            CDIO.ORG_ID = temp_org_id
        Else
            Throw New FormatException(errorMsg)
        End If
        'ORG_ID - end

        'SCAN_FLAG - start

        scnflagNewIndex = orgidNewIndex + ORG_ID_LENGTH
        temp_scan_flag = input.Substring(scnflagNewIndex, SCAN_FLAG_LENGTH)
        If Not String.IsNullOrEmpty(temp_scan_flag) Then
            CDIO.SCAN_FLAG = temp_scan_flag
        Else
            Throw New FormatException(errorMsg)
        End If
        'SCAN_FLAG - end
    End Sub

    'Module
    Function IsModuleReadable(ByVal input As String, Optional ByVal type As String = Nothing) As Boolean
        Dim errorMsg As String = Nothing
        Dim temp_mod_no As String = Nothing
        Dim temp_pill_no As String = Nothing
        Dim temp_gross_weight As String = Nothing
        Dim temp_ord_no As String = Nothing
        Dim cont As MsgBoxResult = Nothing

        Try
            errorMsg = "Invalid Module."
            'MODULE_NO - start
            temp_mod_no = input.Substring(0, MODULE_NO_LENGTH)
            If Not String.IsNullOrEmpty(temp_mod_no) Then
                MODD.MODULE_NO = temp_mod_no
            Else
                Throw New FormatException(errorMsg)
            End If
            'MODULE_NO - end

            'PILLING_NO - start
            temp_pill_no = input.Substring(6, PILLING_NO_LENGTH)
            If Not String.IsNullOrEmpty(temp_pill_no) Then
                MODD.PILLING_NO = temp_pill_no
            Else
                Throw New FormatException(errorMsg)
            End If
            'PILLING_NO - end

            'GROSS_WEIGHT - start
            temp_gross_weight = input.Substring(7, GROSS_WEIGHT_LENGTH)
            If Not String.IsNullOrEmpty(temp_gross_weight) And IsNumber(temp_gross_weight) Then
                MODD.GROSS_WEIGHT = temp_gross_weight
            Else
                Throw New FormatException(errorMsg)
            End If
            'GROSS_WEIGHT - end

            'ORDER_NO - start
            temp_ord_no = input.Substring(12, ORDER_NO_LENGTH)
            If Not String.IsNullOrEmpty(temp_ord_no) Then
                MODD.ORDER_NO = temp_ord_no
            Else
                Throw New FormatException(errorMsg)
            End If
            'ORDER_NO - end

            Return True

        Catch ex As Exception
            If Not type = "Abn" Then
                txtModuleQR.Focus()
                txtModuleQR.SelectAll()
            Else
                txtModuleQRAbn.Focus()
                txtModuleQRAbn.SelectAll()
            End If
            'cont = MsgBox("CDIO scanned failed. Exception:" + ex.Message.ToString() + Environment.NewLine + _
            '              "Proceed to Force Scan?", MsgBoxStyle.Question, _
            '              IIf((Not type = "Abn"), "Module Scan", "Abnormal Module Scan"))
            cont = MsgBox("CDIO scanned failed." + Environment.NewLine + _
                          "Proceed to Force Scan?", MsgBoxStyle.Question, _
                          IIf((Not type = "Abn"), "Module Scan", "Abnormal Module Scan"))
            If cont = MsgBoxResult.Yes Then
                If Not type = "Abn" Then
                    bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvScanModule)
                Else
                    bringPanelToFront(pnlCdioRcvAbnFScan, pnlCdioRcvAbnScan)
                End If
            Else
                Exit Function
            End If
            Return False
        End Try
    End Function

    Private Function IsNumber(ByVal input As String) As Boolean
        Return Regex.IsMatch(input, "^[0-9 ]+$")
    End Function

#End Region

#Region ". Batch Helper ."

    Private Sub InitCdioBatch()
        curr_BATCH = GetBatchID("CDIO", "1")
    End Sub

    Private Sub UpdateBatch()
        Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "MM-dd-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
        If Not ExecuteSQL(String.Format("UPDATE [{0}] SET CURRENT_NO = {1} WHERE CATEGORY = 'CDIO'", TblBatch, SQLQuote(currentNo))) Then
            Throw New Exception("Failed to Update Batch.")
        End If
        prev_BATCH = curr_BATCH
        Call InitCdioBatch()
    End Sub

#End Region

#Region ". Force Scan Helper ."

    Private Sub PopulateCDIOForceScan(Optional ByVal type As String = Nothing)
        Dim lv As ListView = New ListView
        Dim newItem As ListViewItem = New ListViewItem()
        Dim dtReason As DataTable = New DataTable

        txtFSModuleNo.Text = Nothing
        txtFSOrderNo.Text = Nothing

        If Not type = "Abn" Then
            lv = lstViewRCVFScan
        Else
            lv = ListView3
        End If

        lv.Items.Clear()
        dtReason = getData("SELECT REASON_CODE, REASON FROM [" & TblJSPAbnormalReasonCodeDb & "]")
        If dtReason.Rows.Count > 0 Then
            For i As Integer = 0 To dtReason.Rows.Count - 1
                newItem = New ListViewItem
                newItem.Text = dtReason.Rows(i).Item("REASON").ToString
                newItem.SubItems.Add(dtReason.Rows(i).Item("REASON_CODE").ToString)
                lv.Items.Add(newItem)
            Next
        End If
    End Sub

#End Region

#Region ". View Details Helper ."

    Private Sub LoadCDIODetails()
        Me.Text = strOnlineTitle + " - View"
        lblHeaderVwDet.Text = IIf(Not String.IsNullOrEmpty(CDIO.CDIO_NO), "CDIO No : " _
                                  & CDIO.CDIO_NO, String.Empty)
        Call PopulateCDIOScannedDetails(CDIO.CDIO_NO)
        Call PopulateCDIOPendingDetails(CDIO.CDIO_NO)
        lblRCITotalScan.Text = cntPending.ToString
        lblRCIS2TotalScan.Text = cntScanned.ToString
    End Sub

    Private Sub PopulateCDIOScannedDetails(ByVal CDIO_NO As String)
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim lstDt As DataTable = New DataTable

        lstViewRCISummary.Items.Clear()
        If Not String.IsNullOrEmpty(CDIO_NO) Then
            lstDt = getData("SELECT MODULE_NO, ORDER_NO " & _
                            "FROM [" & CDIO_INTERFACE & "] " & _
                            "WHERE CDIO_NO = " & SQLQuote(CDIO_NO) & " " & _
                            "GROUP BY MODULE_NO, ORDER_NO " & _
                            "ORDER BY ORDER_NO")
            For i As Integer = 0 To lstDt.Rows.Count - 1
                lstViewItem = New ListViewItem
                lstViewItem.Text = i + 1
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
                lstViewRCISummary.Items.Add(lstViewItem)
            Next
            cntScanned = lstDt.Rows.Count
        End If
    End Sub

    Private Sub PopulateCDIOPendingDetails(ByVal CDIO_NO As String)
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim lstDt As DataTable = New DataTable

        lstCdioRcvModule.Items.Clear()
        If Not String.IsNullOrEmpty(CDIO_NO) Then
            lstDt = getData("SELECT MODULE_NO, ORDER_NO " & _
                            "FROM [" & CDIO_PENDING & "] " & _
                            "WHERE CDIO_NO = " & SQLQuote(CDIO_NO) & " " & _
                            "ORDER BY MODULE_NO")
            For i As Integer = 0 To lstDt.Rows.Count - 1
                lstViewItem = New ListViewItem
                lstViewItem.Text = i + 1
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
                lstCdioRcvModule.Items.Add(lstViewItem)
            Next
            cntPending = lstDt.Rows.Count
        End If
    End Sub

#End Region

#Region ". Offline Scan Module ."

    Private Sub LoadAbnScanModule()
        Me.Text = strOfflineTitle
        txtModuleQRAbn.Focus()
        txtModuleQRAbn.Text = String.Empty
        Label13.Text = String.Empty
        Label2.Text = String.Empty
        lblScanAbn.BackColor = Color.LimeGreen
        lblScanAbn.Text = "Scan.."
        lblScanDescAbn.Text = String.Empty
        lblTotalAScanned.Text = Counter("Abnormal")
    End Sub

    Private Sub txtModuleQRAbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtModuleQRAbn.KeyDown

        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                If Not String.IsNullOrEmpty(txtModuleQRAbn.Text) Then
                    If Not IsModuleReadable(txtModuleQRAbn.Text, "Abn") Then
                        Exit Sub
                    End If
                    Call InitWebServices()
                    Call AbnModuleScanChecker()
                Else
                    Throw New Exception("Failed to read the Module QR.")
                End If
            Catch ex As Exception
                MsgBox("Module scanned failed.", MsgBoxStyle.Critical, "Abnormal Module Scan")
                Cursor.Current = Cursors.Default
                txtModuleQRAbn.Focus()
                txtModuleQRAbn.SelectAll()
            End Try
        End If

    End Sub

    Private Sub AbnModuleScanChecker(Optional ByVal IsForceScan As Boolean = Nothing)
        Dim dtModuleList As DataTable = New DataTable()

        Cursor.Current = Cursors.WaitCursor
        'o f f l i n e
        dtModuleList = getData("SELECT * FROM [" & CDIO_INTERFACE & "] " & _
                               "WHERE MODULE_NO = " & SQLQuote(MODD.MODULE_NO) & " " & _
                               "AND ORDER_NO = " & SQLQuote(MODD.ORDER_NO))
        If dtModuleList.Rows.Count > 0 Then
            msgCode = "Failed"
            msgDesc = "Duplicate Module No."
            SetPanelForResult("PanelAbnModError", msgCode, msgDesc, IsForceScan)
        Else
            Call AddToCDIOInterface(Nothing, Nothing, Nothing, MODD.MODULE_NO, MODD.PILLING_NO, MODD.GROSS_WEIGHT, MODD.ORDER_NO, _
                                    Nothing, IIf(IsForceScan, MODD.REASON_CD, Nothing), "Y", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"))
            Call UpdateCDIOPendingDetails(MODD.MODULE_NO, MODD.ORDER_NO)
            msgCode = "Success"
            msgDesc = "Succesfully Updated"
            SetPanelForResult("PanelAbnModOk", msgCode, msgDesc, IsForceScan)
        End If
    End Sub

#End Region

#Region ". Offline Force Scan ."

    Private Sub LoadAbnForceScan()
        Me.Text = strOfflineTitle
        Call PopulateCDIOForceScan("Abn")
        txtFSAbnModuleNo.Text = String.Empty
        txtFSAbnOrderNo.Text = String.Empty
    End Sub

#End Region

#Region ". Offline Populate Post/Delete/Details ."

    Private Function AbnPopulatePostDT()
        Dim dt As DataTable = New DataTable()

        dt = getData("SELECT ORDER_NO, MODULE_NO " & _
                     "FROM [" & CDIO_INTERFACE & "] " & _
                     "WHERE ON_OFF_LINE_FLAG = 'Y' " & _
                     "AND PROCESS_FLAG IS NULL OR PROCESS_FLAG = '' " & _
                     "OR PROCESS_FLAG != 'POSTED' " & _
                     "GROUP BY ORDER_NO, MODULE_NO " & _
                     "ORDER BY ORDER_NO")
        Return dt
    End Function

    Private Function AbnPopulateDetailsDT()
        Dim dt As DataTable = New DataTable()

        dt = getData("SELECT ORDER_NO, MODULE_NO " & _
                     "FROM [" & CDIO_INTERFACE & "] " & _
                     "WHERE ON_OFF_LINE_FLAG = 'Y' " & _
                     "GROUP BY ORDER_NO, MODULE_NO " & _
                     "ORDER BY ORDER_NO")
        Return dt
    End Function

#End Region

#Region ". Offline View & Delete & Posting ."

    Private Sub LoadAbnDetails()
        Me.Text = strOfflineTitle + " - View"
        Call PopulateAbnScannedDetails()
        lblTotalRcvDetCnt.Text = "Total Records: " & cntAbns.ToString
    End Sub

    Private Sub LoadAbnDelete()
        Me.Text = strOfflineTitle + " - Delete"
        Call PopulateAbnDeleteDetails()
        Label6.Text = "Total Records: " & cntAbns.ToString
    End Sub

    Private Sub LoadAbnPost()
        Me.Text = strOnlineTitle + " - Posting"
        Call PopulateAbnPostDetails()
        Label26.Text = "Total Pending Posting Records: " & cntAbnPost.ToString
    End Sub

    Private Sub PopulateAbnScannedDetails()
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim lstDt As DataTable = New DataTable()

        lstViewRcvDet.Items.Clear()
        lstDt = AbnPopulateDetailsDT()
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString())
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
            lstViewRcvDet.Items.Add(lstViewItem)
        Next
        cntAbns = lstDt.Rows.Count
    End Sub

    Private Sub PopulateAbnDeleteDetails()
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim lstDt As DataTable = New DataTable()

        ListView2.Items.Clear()
        lstDt = AbnPopulateDetailsDT()
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString())
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
            ListView2.Items.Add(lstViewItem)
        Next
        cntAbns = lstDt.Rows.Count
    End Sub

    Private Sub PopulateAbnPostDetails()
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim lstDt As DataTable = New DataTable()

        ListView1.Items.Clear()
        lstDt = AbnPopulatePostDT()
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString())
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
            ListView1.Items.Add(lstViewItem)
        Next
        cntAbnPost = lstDt.Rows.Count
    End Sub

    Private Sub btnRcvDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRcvDelete.Click
        Dim contDelete As System.Windows.Forms.DialogResult = Nothing

        Try
            contDelete = MessageBox.Show("Confirm to delete all posted record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If contDelete = Windows.Forms.DialogResult.Yes Then
                Cursor.Current = Cursors.WaitCursor
                If Not ExecuteSQL("DELETE FROM [" & CDIO_INTERFACE & "] " & _
                                  "WHERE RCV_INTERFACE_BATCH_ID = " & SQLQuote(IIf(Not String.IsNullOrEmpty(prev_BATCH), prev_BATCH, curr_BATCH)) & " " & _
                                  "AND ON_OFF_LINE_FLAG = 'Y'") Then
                    Throw New Exception("Failed to delete records.")
                End If
                Cursor.Current = Cursors.Default
                Call LoadAbnDelete()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Delete operation failed.", MsgBoxStyle.Critical, "Abnormal Module Scan Delete")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btnRcvSubmitPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRcvSubmitPosting.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            'p r e - p o s t
            Call InitWebServices()

            'p o s t
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then '***
                    If (ValidateAbnRecords()) Then
                        msgCode = ws_inventoryClient.processInventoryConsumption(curr_BATCH, "CKDRECV", "105", org_ID, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, msgDesc)
                        If msgCode = "OK" Then
                            SetPanelForResult("PanelAbnModPost", msgCode, msgDesc)
                        Else
                            Throw New Exception(String.Format("Failed to Post Module. - {0}", msgDesc))
                        End If
                    Else
                        MsgBox("No records to post.", MsgBoxStyle.Critical, "Abnormal Module Scan Post")
                    End If
                    Cursor.Current = Cursors.Default
                End If
            End If
        Catch ex As WebException
            MsgBox("No connection to post.", MsgBoxStyle.Critical, "Abnormal Module Scan Post")
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("Posting failed.", MsgBoxStyle.Critical, "Abnormal Module Scan Post")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ValidateAbnRecords() As Boolean
        Dim dt As DataTable = New DataTable()
        Dim forceScanReasonID As String = String.Empty

        dt = getData("SELECT * FROM [" & CDIO_INTERFACE & "] " & _
                     "WHERE RCV_INTERFACE_BATCH_ID = " & SQLQuote(curr_BATCH) & " " & _
                     "AND ON_OFF_LINE_FLAG = 'Y'")
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If ws_dcsClient.isOracleConnected Then '***
                    If IsDBNull(dt.Rows(i).Item("FORCE_MODULE_REASON_ID")) Then
                        forceScanReasonID = Nothing
                    Else
                        forceScanReasonID = dt.Rows(i).Item("FORCE_MODULE_REASON_ID").ToString()
                    End If

                    msgCode = ws_validationClient.processValidation(curr_BATCH, gScannerID, "CKDRECV", "104", _
                                    IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("CDIO_ID").ToString), dt.Rows(i).Item("CDIO_ID").ToString, Nothing), _
                                    IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("CDIO_NO").ToString), dt.Rows(i).Item("CDIO_NO").ToString, Nothing), _
                                    dt.Rows(i).Item("PILLING_ORDER").ToString, dt.Rows(i).Item("GROSS_WEIGHT").ToString, Nothing, Nothing, _
                                    dt.Rows(i).Item("FORCE_MODULE_STATUS").ToString, forceScanReasonID, Nothing, _
                                    dt.Rows(i).Item("MODULE_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, dt.Rows(i).Item("ORDER_NO").ToString, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    SelectOrgIdForPosting(dt.Rows(i).Item("ORG_ID").ToString), Nothing, Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, gScannerID, GetServerTime, gScannerID, "Y", gScannerID, GetServerTime, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, msgDesc)
                    If msgCode = "OK" Then
                        Cursor.Current = Cursors.Default
                        Call UpdatePostStatus(dt.Rows(i).Item("MODULE_NO").ToString, dt.Rows(i).Item("ORDER_NO").ToString)
                    End If
                End If
            Next
            Return True
        Else
            Return False
        End If
    End Function

    Function SelectOrgIdForPosting(ByVal TempOrgID As String) As String
        Return IIf(Not String.IsNullOrEmpty(TempOrgID), IIf(Not TempOrgID = "0", TempOrgID, org_ID), org_ID)
    End Function

#End Region

#Region ". Abnormal Login ."

    Private Sub btnLoginSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginSubmit.Click
        Try
            If String.IsNullOrEmpty(txtUsername.Text) Then
                MsgBox("Username cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtUsername.Focus()
                txtUsername.SelectAll()
                Exit Sub
            ElseIf String.IsNullOrEmpty(txtPwd.Text) Then
                MsgBox("Password cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtPwd.Focus()
                txtPwd.SelectAll()
                Exit Sub
            End If

            If loginUser = txtUsername.Text.Trim() Then
                If loginPass = txtPwd.Text.Trim() Then
                    Call LoadAbnPost()
                    lblUsername.Text = String.Format("USER NAME: {0}", txtUsername.Text)
                    bringPanelToFront(pnlCdioRcvPosting, pnlLogin)
                Else
                    MessageBox.Show("Invalid password")
                End If
            Else
                MessageBox.Show("Invalid username")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLoginClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginClose.Click
        bringPanelToFront(pnlCdioRcvAbn, pnlLogin)
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsername.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Enter
                txtPwd.SelectAll()
                txtPwd.Focus()
        End Select
    End Sub

    Private Sub txtPwd_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPwd.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Enter
                btnLoginSubmit_Click(Nothing, Nothing)
        End Select
    End Sub

#End Region

End Class
