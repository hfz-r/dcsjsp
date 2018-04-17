Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Net

Public Class frmCdioReceiving

#Region "Properties"

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
        Private _module_no As String
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

        Private _order_no As String
        Property ORDER_NO() As String
            Get
                Return _order_no
            End Get
            Set(ByVal value As String)
                _order_no = value
            End Set
        End Property

    End Class

#End Region

    Friend cn As New clsConnection
    Dim bConnected As Boolean = False
    Dim clsDataTransfer As New clsDataTransfer
    Dim msgCode As String = Nothing
    Dim msgDesc As String = Nothing
    Dim showError As Boolean = True
    Private Const strOnlineTitle As String = "CDIO Receiving"
    Private Const strOfflineTitle As String = "Abnormal CDIO Receiving"
    Public mode As Boolean = False
    Private CDIO As New CDIO_input()
    Private MODD As New Module_Input()

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmMain_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
            'ws_dcsClient.Dispose()
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Public Sub Init()

        Try
            Me.Text = strOnlineTitle
            footerStatusBar.Visible = False
            bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScan)

            Call CreateTable(CDIO_BATCH)

        Catch ex As Exception
            'setScannerDate()
        End Try
    End Sub

    Private Function getScannerId() As String
        Dim scnid As String = ""
        Try
            Dim dt As DataTable = New DataTable


            'dt = getData("SELECT SettingValue FROM TblSetting WHERE settingCategory='SCN' AND settingCode='SCNID' ")
            If dt.Rows.Count > 0 Then
                scnid = dt.Rows(0).Item("SettingValue").ToString
            End If


        Catch ex As Exception
            MsgBox("Error reading Scanner ID" & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try

        Return scnid
    End Function

    Private Sub btnDateSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    If SetLocalTime(dtScannerDate.Value.ToString(gStrTimeFormat)) Then
        '        'chkOffline.Checked = True
        '        'txtUsername.Focus()
        '        bringPanelToFront(pnlLogin, pnlSetDatetime)
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        'End Try
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ShowWait(True)
        'Check connection --> if offline --> force user to use offline mode --> if user don't want --> don't allow use
        'Try
        '    If chkOffline.Checked = False Then
        '        'ws_dcsClient.Url = gStrDCSWebServiceURL
        '        'ws_dcsClient.isConnected()
        '        mode = True
        '    Else
        '        mode = False
        '    End If

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    If MsgBox("No connection! Proceed abnormal mode?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
        '        chkOffline.Checked = True
        '        mode = True
        '    Else
        '        ShowWait(False)
        '        Exit Sub
        '    End If
        'End Try

        'Try
        '    mode = IIf(chkOffline.Checked, False, True)

        '    If txtUsername.Text.Trim = "" Or txtPassword.Text.Trim = "" Then
        '        MsgBox("Username/Password is blank!", MsgBoxStyle.Critical, Me.Text)
        '        txtUsername.Focus()
        '        ShowWait(False)
        '        Exit Sub
        '    End If

        '    If IsNumeric(txtUsername.Text.Trim) = False Then
        '        MsgBox("Invalid Username!", MsgBoxStyle.Critical, Me.Text)
        '        txtUsername.Text = ""
        '        txtUsername.Focus()
        '        ShowWait(False)
        '        Exit Sub
        '    End If

        '    gStrUsername = txtUsername.Text.Trim
        '    Dim pwd As String = txtPassword.Text.Trim
        '    If verifyLogin() = False Then
        '        txtUsername.Text = ""
        '        txtUsername.Focus()
        '        txtPassword.Text = ""
        '        MsgBox("Invalid Username or Password! Please try again.", MsgBoxStyle.Critical, Me.Text)
        '        ShowWait(False)
        '        Exit Sub
        '    Else
        '        footerStatusBar.Visible = True
        '        setStatusBar(footerStatusBar, gStrUsername, mode, getScannerId())
        '        bringPanelToFront(pnlMainMenu, pnlLogin)
        '    End If

        '    ShowWait(False)
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        'End Try

    End Sub

    'Private Sub btnServiceExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServiceExit.Click, btnSetting.Click
    '    If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
    '        'ws_dcsClient.Dispose()
    '        footerStatusBar.Visible = False
    '        txtUsername.Text = ""
    '        txtPassword.Text = ""
    '        txtUsername.Focus()
    '        bringPanelToFront(pnlLogin, pnlMainMenu)
    '    End If
    'End Sub

    Private Sub btnSDelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If mode = True Then
        '    Dim frm As New frmSHIPDeliveryOnline
        '    frm.AutoScroll = False
        '    If frm.Init() Then
        '        frm.ShowDialog()
        '    End If

        '    frm.Dispose() : frm = Nothing

        'Else
        '    Dim frm As New frmSHIPDeliveryOffline
        '    frm.AutoScroll = False
        '    frm.Init()
        '    frm.ShowDialog()
        '    frm.Dispose() : frm = Nothing
        'End If

    End Sub

    Private Sub btnSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmSetting
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Select Case e.KeyCode
        '    Case Keys.Return, Keys.Enter
        '        txtPassword.SelectAll()
        '        txtPassword.Focus()
        'End Select
    End Sub

    Private Function verifyLogin() As Boolean
        verifyLogin = False

        'mode = False 'TESTING

        If mode Then
            'Online
            Try

                'If ws_dcsClient.getData("LOGIN_ID", "SEP_LOGIN_V", " AND LOGIN_ID=" & SQLQuote(txtUsername.Text.Trim) & " AND PASSWORD=" & SQLQuote(txtPassword.Text.Trim)).Rows.Count > 0 Then
                '    verifyLogin = True
                'End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        Else
            'Abnormal/Offline
            Try
                Dim datatbl As DataTable = New DataTable

                'Dim strSQL As String = "SELECT LOGIN_ID,PASSWORD FROM SEP_LOGIN_V WHERE LOGIN_ID='" & txtUsername.Text.Trim & "' AND PASSWORD='" & txtPassword.Text.Trim & "' "

                'datatbl = getData(strSQL)

                If datatbl.Rows.Count > 0 Then
                    verifyLogin = True
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If


    End Function

    Private Function CheckDataImportOnShedule() As Boolean
        Try
            CheckDataImportOnShedule = False

            Dim sSQL As String = Nothing
            Dim dbReader As SqlCeDataReader

            dbReader = OpenRecordset("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'SCHEDULE' AND SettingValue = '" & Format(Now, "dddd") & "'", objConn)
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
        Try
            CheckDataImportOnToday = False

            Dim sSQL As String = Nothing
            Dim dbReader As SqlCeDataReader

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
        Me.Text = strOnlineTitle
        mode = True

        txtRCVSSEPIO.Text = ""
        lblRcvContainer.Text = ""
        lblRcvExporter.Text = ""
        txtRCVSSEPIO.Focus()

        If String.IsNullOrEmpty(txtRCVSSEPIO.Text) Then
            btnNextScan.Enabled = False
        End If
        bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvMain)
    End Sub

    Private Sub btnAbnormalCdio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalCdio.Click
        Me.Text = strOfflineTitle
        mode = False
        bringPanelToFront(pnlCdioRcvAbn, pnlCdioRcvMain)
    End Sub

    Private Sub btnNextScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextScan.Click
        Me.Text = strOnlineTitle
        If msgCode = "OK" Then
            bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvScan)
            Call LoadScanModule()
        End If
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlCdioRcvViewDet, pnlCdioRcvScanModule)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanModule.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvScanModule)
    End Sub

    Private Sub btnFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvScanError, pnlCdioRcvScan)
    End Sub

    Private Sub btnBackFrError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFrError.Click
        Me.Text = strOnlineTitle
        txtRCVSSEPIO.Focus()
        txtRCVSSEPIO.SelectAll()
        bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvScanError)
    End Sub

    Private Sub btnCdioRcvAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCdioRcvAbnScan, pnlCdioRcvAbn)
    End Sub

    Private Sub btnCdioRcvAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnView.Click
        Me.Text = strOfflineTitle + " - View"
        bringPanelToFront(pnlCdioRcvAbnViewDet, pnlCdioRcvAbn)
    End Sub

    Private Sub btnCdioRcvAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnPost.Click
        Me.Text = strOnlineTitle + " - Posting"
        bringPanelToFront(pnlCdioRcvPosting, pnlCdioRcvAbn)
    End Sub

    Private Sub btnCdioRcvAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvAbnDelete.Click
        Me.Text = strOnlineTitle + " - Delete"
        bringPanelToFront(pnlCdioRcvDelete, pnlCdioRcvAbn)
    End Sub

    Private Sub btnBackRcvScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRcvScanModule.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvScanModule)
    End Sub

    Private Sub btnBackFrViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFrViewDet.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvAbnViewDet)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvFScan)
    End Sub

    Private Sub btnForce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvViewDet)
    End Sub

    Private Sub btnCdioRcvModuleDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioRcvModuleDet.Click
        Me.Text = strOfflineTitle + " - View"
        bringPanelToFront(pnlCdioRcvAbnViewDet, pnlCdioRcvScanModuleError)
    End Sub

    Private Sub btnBackFrModuleError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFrModuleError.Click
        Me.Text = strOnlineTitle
        txtModuleQR.Focus()
        txtModuleQR.SelectAll()
        bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvScanModuleError)
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

    Private Sub btnCloseCdioRcvScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCdioRcvScan.Click
        Timer1.Enabled = False
        Timer1.Dispose()
        Me.Close()
    End Sub

#Region "CDIO Scan"

    Private Sub txtRCVSSEPIO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRCVSSEPIO.TextChanged
        If Not String.IsNullOrEmpty(txtRCVSSEPIO.Text) Then
            btnNextScan.Enabled = True
        Else
            btnNextScan.Enabled = False
        End If
    End Sub

    Private Sub txtRCVSSEPIO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRCVSSEPIO.KeyDown

        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                If Not String.IsNullOrEmpty(txtRCVSSEPIO.Text) Then

                    Call IsCDIOReadable(txtRCVSSEPIO.Text)

                    'If ws_dcsClient.isConnected() Then
                    If Not String.IsNullOrEmpty(txtRCVSSEPIO.Text) Then
                        'online
                        mode = True
                        If Not String.IsNullOrEmpty(CDIO.CDIO_ID) Then
                            Cursor.Current = Cursors.WaitCursor
                            Call CreateTable(CDIO_DETAILS_VIEW)

                            Dim sqlStr As String = Nothing
                            sqlStr = "INSERT INTO [" & CDIO_DETAILS_VIEW & "] VALUES ("
                            sqlStr = sqlStr & SQLQuote(CDIO.CDIO_ID) & " , "
                            sqlStr = sqlStr & SQLQuote(CDIO.CDIO_NO) & " , "
                            sqlStr = sqlStr & " 1, 'MOD666', 'MODULE SIX SIX SIX', "
                            sqlStr = sqlStr & SQLQuote(CDIO.ORG_ID) & " , "
                            sqlStr = sqlStr & SQLQuote(CDIO.EXPORTER) & " , "
                            sqlStr = sqlStr & SQLQuote(CDIO.CONTAINER_NO)
                            sqlStr = sqlStr & ")"
                            If ExecuteSQL(sqlStr) = False Then
                                msgCode = "NG"
                                msgDesc = "Invalid CDIO No"
                                bringPanelToFront(pnlCdioRcvScanError, pnlCdioRcvScan)
                                SetPanelCdioRcvScanText("PanelError", msgCode, msgDesc, txtRCVSSEPIO.Text)
                            Else
                                msgCode = "OK"
                                msgDesc = "Succesfully Updated"
                                SetPanelCdioRcvScanText("PanelOk", msgCode, msgDesc, Nothing)
                            End If

                            'Dim WHERE_CONDITION As String = " AND CDIO_NO=" & SQLQuote(CDIO.CDIO_NO) & _
                            '                                " AND SCAN_ID=" & SQLQuote(frmSetting.txtSTSCNID.Text.Trim) & _
                            '                                " AND BATCH_ID=" & SQLQuote(frmSetting.txtSTBatchID.Text.Trim) & _
                            '                                " AND PROCESS_TYPE='CKDRECV'" & _
                            '                                " AND PROCESS_CODE='101'" & _
                            '                                " AND ORG_ID=" & SQLQuote(CDIO.ORG_ID)
                            'If ws_dcsClient.getData("CDIO_ID", CDIO_INTERFACE, WHERE_CONDITION).Rows.Count > 0 Then
                            '    Dim dtModuleList As DataTable = New DataTable()
                            '    dtModuleList = ws_dcsClient.getData("*", CDIO_DETAILS_VIEW, " AND CDIO_NO=" & SQLQuote(CDIO.CDIO_NO))

                            '    If dtModuleList.Rows.Count > 0 Then
                            '        For i As Integer = 0 To dtModuleList.Rows.Count - 1
                            '            sqlStr = "INSERT INTO [" & CDIO_DETAILS_VIEW & "] "
                            '            sqlStr = sqlStr & " VALUES (" & SQLQuote(dtModuleList.Rows(i).Item("CDIO_NO").ToString) & " , "
                            '            sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("MODULE_ID").ToString) & " , "
                            '            sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("MODULE_NO").ToString) & " , "
                            '            sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("MODULE_NAME").ToString) & " , "
                            '            sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("ORG_ID").ToString) & " , "
                            '            sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("EXPORTER").ToString) & " , "
                            '            sqlStr = sqlStr & SQLQuote(dtModuleList.Rows(i).Item("CONTAINER_NO").ToString)
                            '            sqlStr = sqlStr & ")"

                            '            If ExecuteSQL(sqlStr) = False Then
                            '                MsgBox("Failed Saving To " & CDIO_DETAILS_VIEW, MsgBoxStyle.Critical, "CDIO")
                            '                Exit Sub
                            '            Else
                            '                lblRcvContainer.Text = dtModuleList.Rows(i).Item("CONTAINER_NO").ToString
                            '                lblRcvExporter.Text = dtModuleList.Rows(i).Item("EXPORTER").ToString
                            '            End If
                            '        Next
                            '    End If
                            'End If
                            Cursor.Current = Cursors.Default
                        End If
                    End If
                Else
                    Throw New Exception("Failed to read the CDIO No.")
                End If

            Catch ex As WebException
                'offline
                mode = False
                Timer1.Enabled = True
                Call ChangeProcess()
            Catch ex As Exception
                MsgBox("CDIO scanned is failed. Exception: " + ex.Message.ToString())
                Cursor.Current = Cursors.Default
            End Try
        End If

    End Sub

#End Region

    Private Sub ChangeProcess()
        If showError = True Then
            showError = False
            If MessageBox.Show("Connection is down. Change to Abnormal Process?", "CDIO Process Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Timer1.Enabled = False
                Timer1.Dispose()
                showError = True
                bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScan)
                Exit Sub
            Else
                showError = True
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If mode = False Then
                If ws_dcsClient.isConnected Then
                    If ws_dcsClient.isOracleConnected Then
                        mode = True
                        MsgBox("Connection is back. Please Try Again to Scan.", MsgBoxStyle.Information, Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            Call ChangeProcess()
        End Try
    End Sub

    Private Sub SetPanelCdioRcvScanText(ByVal Panel As String, ByVal msg As String, ByVal desc As String, Optional ByVal input As String = Nothing)
        If Panel = "PanelError" Then
            TextBox2.Text = (IIf(String.IsNullOrEmpty(input), Nothing, input))
            Label45.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label59.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
        ElseIf Panel = "PanelModError" Then
            TextBox3.Text = (IIf(String.IsNullOrEmpty(input), Nothing, input))
            Label64.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label63.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
        ElseIf Panel = "PanelOk" Then
            Label44.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label3.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            lblRcvContainer.Text = CDIO.CONTAINER_NO
            lblRcvExporter.Text = CDIO.EXPORTER
        ElseIf Panel = "PanelModOk" Then
            Label60.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label62.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            txtModuleNo.Text = MODD.MODULE_NO
            txtOrderNo.Text = MODD.ORDER_NO
            lblTotalScanned.Text = "1"
        End If
    End Sub

#Region "QR Code Readable Helper"
    'CDIO
    Private Sub IsCDIOReadable(ByVal input As String)
        'CDIO_ID - start
        Dim temp_cdio_id As String = input.Substring(0, CDIO_ID_LENGTH)
        If Not String.IsNullOrEmpty(temp_cdio_id) And IsNumber(temp_cdio_id) Then
            CDIO.CDIO_ID = temp_cdio_id
        Else
            Throw New FormatException("Format Error on CDIO ID.")
        End If
        'CDIO_ID - end

        'CDIO_NO - start
        Dim temp_cdio_no As String = input.Substring(10, CDIO_NO_LENGTH)
        If Not String.IsNullOrEmpty(temp_cdio_no) Then
            CDIO.CDIO_NO = temp_cdio_no
        Else
            Throw New FormatException("Format Error on CDIO NO.")
        End If
        'CDIO_NO - end

        'PRODUCTION_DATE - start
        Dim format As String = "dd/MM/yyyy"
        Dim temp_prod_date As String = input.Substring(23, PROD_DATE_LENGTH)
        If Not String.IsNullOrEmpty(temp_prod_date) Then
            Try
                Dim formatted As Date = Date.ParseExact(temp_prod_date, format, CultureInfo.InvariantCulture)
                CDIO.PRODUCTION_DATE = formatted
            Catch ex As FormatException
                Throw New FormatException("Format Error on Production Date.")
            End Try
        Else
            Throw New FormatException("Format Error on Production Date.")
        End If
        'PRODUCTION_DATE - end

        'EXPORTER_CODE - start
        Dim temp_exporter_code As String = input.Substring(33, EXPORTER_CODE_LENGTH)
        If Not String.IsNullOrEmpty(temp_exporter_code) Then
            CDIO.EXPORTER_CODE = temp_exporter_code
        Else
            Throw New FormatException("Format Error on Exporter Code.")
        End If
        'EXPORTER_CODE - end

        'EXPORTER - start
        Dim expLength As Integer = input.Length - 36 - (CONTAINER_ID_LENGTH + CONTAINER_NO_LENGTH + ORG_ID_LENGTH + SCAN_FLAG_LENGTH)
        If expLength <= EXPORTER_LENGTH Then
            Dim temp_exporter As String = input.Substring(36, expLength)
            If Not String.IsNullOrEmpty(temp_exporter) Then
                CDIO.EXPORTER = temp_exporter
            Else
                Throw New FormatException("Format Error on Exporter.")
            End If
        Else
            Throw New FormatException("Format Error on Exporter.")
        End If
        'EXPORTER - end

        'CONTAINER_ID - start
        Dim cidNewIndex As Integer = 36 + expLength
        Dim temp_container_id As String = input.Substring(cidNewIndex, CONTAINER_ID_LENGTH)
        If Not String.IsNullOrEmpty(temp_container_id) And IsNumber(temp_container_id) Then
            CDIO.CONTAINER_ID = temp_container_id
        Else
            Throw New FormatException("Format Error on Container ID.")
        End If
        'CONTAINER_ID - end

        'CONTAINER_NO - start
        Dim cnoNewIndex As Integer = cidNewIndex + CONTAINER_ID_LENGTH
        Dim temp_container_no As String = input.Substring(cnoNewIndex, CONTAINER_NO_LENGTH)
        If Not String.IsNullOrEmpty(temp_container_no) Then
            CDIO.CONTAINER_NO = temp_container_no
        Else
            Throw New FormatException("Format Error on Container No.")
        End If
        'CONTAINER_NO - end

        'ORG_ID - start
        Dim orgidNewIndex As Integer = cnoNewIndex + CONTAINER_NO_LENGTH
        Dim temp_org_id As String = input.Substring(orgidNewIndex, ORG_ID_LENGTH)
        If Not String.IsNullOrEmpty(temp_org_id) And IsNumber(temp_org_id) Then
            CDIO.ORG_ID = temp_org_id
        Else
            Throw New FormatException("Format Error on ORG ID.")
        End If
        'ORG_ID - end

        'SCAN_FLAG - start
        Dim scnflagNewIndex = orgidNewIndex + ORG_ID_LENGTH
        Dim temp_scan_flag As String = input.Substring(scnflagNewIndex, SCAN_FLAG_LENGTH)
        If Not String.IsNullOrEmpty(temp_scan_flag) Then
            CDIO.SCAN_FLAG = temp_scan_flag
        Else
            Throw New FormatException("Format Error on Scan Flag.")
        End If
        'SCAN_FLAG - end
    End Sub

    'Module
    Function IsModuleReadable(ByVal input As String) As Boolean
        Try
            'MODULE_NO - start
            Dim temp_mod_no As String = input.Substring(0, MODULE_NO_LENGTH)
            If Not String.IsNullOrEmpty(temp_mod_no) Then
                MODD.MODULE_NO = temp_mod_no
            Else
                Throw New FormatException("Format Error on MODULE NO.")
            End If
            'MODULE_NO - end

            'PILLING_NO - start
            Dim temp_pill_no As String = input.Substring(6, PILLING_NO_LENGTH)
            If Not String.IsNullOrEmpty(temp_pill_no) Then
                MODD.PILLING_NO = temp_pill_no
            Else
                Throw New FormatException("Format Error on PILLING NO.")
            End If
            'PILLING_NO - end

            'GROSS_WEIGHT - start
            Dim temp_gross_weight As String = input.Substring(7, GROSS_WEIGHT_LENGTH)
            If Not String.IsNullOrEmpty(temp_gross_weight) And IsNumber(temp_gross_weight) Then
                MODD.GROSS_WEIGHT = temp_gross_weight
            Else
                Throw New FormatException("Format Error on GROSS WEIGHT.")
            End If
            'GROSS_WEIGHT - end

            'ORDER_NO - start
            Dim temp_ord_no As String = input.Substring(12, ORDER_NO_LENGTH)
            If Not String.IsNullOrEmpty(temp_ord_no) Then
                MODD.ORDER_NO = temp_ord_no
            Else
                Throw New FormatException("Format Error on ORDER NO.")
            End If
            'ORDER_NO - end

            Return True

        Catch ex As Exception
            MsgBox("CDIO scanned is failed. Exception: " + ex.Message.ToString() + Environment.NewLine + _
                   "You`ll be route to Force Scan.")
            Return False
        End Try
    End Function

    Private Function IsNumber(ByVal input As String) As Boolean
        Return Regex.IsMatch(input, "^[0-9 ]+$")
    End Function

#End Region

#Region "CDIO Table Helper"

    Public Function CreateTable(ByVal TableName As String) As Boolean

        CreateTable = False

        Dim dbReader As SqlCeDataReader
        Dim strBuilder As System.Text.StringBuilder

        Select Case TableName.ToUpper.Trim
            Case CDIO_DETAILS_VIEW
                dbReader = OpenRecordset("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & SQLQuote(CDIO_DETAILS_VIEW), objConn)
                If dbReader.Read Then
                    If CInt(dbReader(0)) = 0 Then
                        strBuilder = Nothing
                        strBuilder = New System.Text.StringBuilder("")
                        strBuilder.Append("CREATE TABLE [" + CDIO_DETAILS_VIEW + "] (")
                        strBuilder.Append("CDIO_ID NVARCHAR(10) PRIMARY KEY,")
                        strBuilder.Append("CDIO_NO NVARCHAR(20) NOT NULL,")
                        strBuilder.Append("MODULE_ID INT NOT NULL,")
                        strBuilder.Append("MODULE_NO NVARCHAR(10) NOT NULL,")
                        strBuilder.Append("MODULE_NAME NVARCHAR(20) NOT NULL,")
                        strBuilder.Append("ORG_ID INT NOT NULL,")
                        strBuilder.Append("EXPORTER NVARCHAR(50) NOT NULL,")
                        strBuilder.Append("CONTAINER_NO NVARCHAR(20) NOT NULL")
                        strBuilder.Append(") ")
                        If ExecuteSQL(strBuilder.ToString) = False Then
                            Throw New Exception("Failed to create local scanner`s table.")
                        End If
                    End If
                End If

            Case CDIO_INTERFACE
                dbReader = OpenRecordset("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & SQLQuote(CDIO_INTERFACE), objConn)
                If dbReader.Read Then
                    If CInt(dbReader(0)) = 0 Then
                        strBuilder = Nothing
                        strBuilder = New System.Text.StringBuilder("")
                        strBuilder.Append("CREATE TABLE [" + CDIO_INTERFACE + "] (")
                        strBuilder.Append("RCV_INTERFACE_ID	INT IDENTITY,")
                        strBuilder.Append("RCV_INTERFACE_BATCH_ID INT NOT NULL,")
                        strBuilder.Append("CDIO_ID NVARCHAR(10) NOT NULL,")
                        strBuilder.Append("CDIO_NO NVARCHAR(20) NOT NULL,")
                        strBuilder.Append("MODULE_ID INT NOT NULL,")
                        strBuilder.Append("MODULE_NO NVARCHAR(10) NOT NULL,")
                        strBuilder.Append("PILLING_ORDER NVARCHAR(1) NOT NULL,")
                        strBuilder.Append("GROSS_WEIGHT INT NOT NULL,")
                        strBuilder.Append("ORDER_NO	NVARCHAR(12) NOT NULL,")
                        strBuilder.Append("ORG_ID INT NOT NULL,")
                        strBuilder.Append("RCV_BY NVARCHAR(20) NULL,")
                        strBuilder.Append("RCV_DATE	DATETIME NULL,")
                        strBuilder.Append("SCANNER_BATCH_ID	NVARCHAR(20) NULL,")
                        strBuilder.Append("SCANNER_HT_ID NVARCHAR(20) NULL,")
                        strBuilder.Append("PROCESS_DATE DATETIME NULL,")
                        strBuilder.Append("PROCESS_FLAG NVARCHAR(10) NULL,")
                        strBuilder.Append("CREATED_BY NVARCHAR(20) NOT NULL,")
                        strBuilder.Append("CREATED_DATE	DATETIME NOT NULL,")
                        strBuilder.Append("UPDATED_BY NVARCHAR(20) NULL,")
                        strBuilder.Append("UPDATED_DATE	DATETIME NULL,")
                        strBuilder.Append("ERROR_MSG NVARCHAR(700) NULL,")
                        strBuilder.Append("POST_DATE DATETIME NULL,")
                        strBuilder.Append("DELIVERY_DATE DATETIME NULL,")
                        strBuilder.Append("ON_OFF_LINE_FLAG	NVARCHAR(5) NULL,")
                        strBuilder.Append("SCAN_DATE DATETIME NULL,")
                        strBuilder.Append("FORCE_CDIO_STATUS NVARCHAR(1) NULL,")
                        strBuilder.Append("FORCE_CDIO_REASON_ID INT NULL,")
                        strBuilder.Append("FORCE_MODULE_STATUS NVARCHAR(1) NULL,")
                        strBuilder.Append("FORCE_MODULE_REASON_ID INT NULL")
                        strBuilder.Append(") ")
                        If ExecuteSQL(strBuilder.ToString) = False Then
                            Throw New Exception("Failed to create local scanner`s table.")
                        End If
                    End If
                End If

            Case CDIO_BATCH
                dbReader = OpenRecordset("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & SQLQuote(CDIO_BATCH), objConn)
                If dbReader.Read Then
                    If CInt(dbReader(0)) = 0 Then
                        strBuilder = Nothing
                        strBuilder = New System.Text.StringBuilder("")
                        strBuilder.Append("CREATE TABLE [" + CDIO_BATCH + "] (")
                        strBuilder.Append("ID INT IDENTITY,")
                        strBuilder.Append("CATEGORY NVARCHAR(10) NOT NULL,")
                        strBuilder.Append("PREFIX NVARCHAR(20) NULL,")
                        strBuilder.Append("SUFFIX NVARCHAR(20) NULL,")
                        strBuilder.Append("START_NO INT NOT NULL,")
                        strBuilder.Append("CURRENT_NO NVARCHAR(3) NOT NULL,")
                        strBuilder.Append("LENGTH INT NOT NULL")
                        strBuilder.Append(") ")
                        If ExecuteSQL(strBuilder.ToString) = False Then
                            Throw New Exception("Failed to create local scanner`s table.")
                        End If

                        Dim insrtStatmnt As String = Nothing
                        insrtStatmnt = "INSERT INTO [" + CDIO_BATCH + "] (CATEGORY, PREFIX, SUFFIX, START_NO, CURRENT_NO, LENGTH) " _
                                     & " VALUES ('CDIO','csssyyyyMMnnn', NULL, 1, '001', 13)"
                        If ExecuteSQL(insrtStatmnt) = False Then
                            Throw New Exception("Failed to create local scanner`s table.")
                        End If
                    End If
                End If
        End Select

        CreateTable = True

    End Function

#End Region

#Region "Batch Helper"

    Function GetBatchID() As String
        Try
            Dim Prefix As String = Nothing
            Dim CurrentNo As String = Nothing
            Dim Length As Integer
            Dim ID As Integer
            Dim dt As DataTable = New DataTable
            dt = getData("SELECT TOP(1) * FROM [" + CDIO_BATCH + "] ORDER BY ID DESC")

            For i As Integer = 0 To dt.Rows.Count - 1
                ID = dt.Rows(i).Item(0).ToString
                If dt.Rows(i).Item(2) = "PREFIX" Then
                    Prefix = dt.Rows(i).Item(2).ToString
                ElseIf dt.Rows(i).Item(5) = "CURRENT_NO" Then
                    CurrentNo = dt.Rows(i).Item(5).ToString
                ElseIf dt.Rows(i).Item(6) = "LENGTH" Then
                    Length = dt.Rows(i).Item(6).ToString
                End If
            Next

            Dim BatchId As System.Text.StringBuilder = New System.Text.StringBuilder("")

            Select Case True
                Case Prefix.Substring(0, 1).Contains("c")
                    BatchId.Append("1")
                Case Prefix.Substring(1, 3).Contains("sss")
                    BatchId.Append(frmSetting.txtSTSCNID.Text.Trim)
                Case Prefix.Substring(4, 4).Contains("yyyy")
                    BatchId.Append(Date.Now.Year.ToString())
                Case Prefix.Substring(8, 2).Contains("MM")
                    BatchId.Append(Date.Now.Month.ToString())
                Case Prefix.Substring(10, 3).Contains("nnn")
                    BatchId.Append(CurrentNo)
            End Select

            If BatchId.Length = Length Then
                'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sUser.Trim) & " WHERE SettingCode = 'USER'"

                If Not ExecuteSQL("UPDATE [" + CDIO_BATCH + "] SET CURRENT_NO = '" + CInt(CurrentNo + 1) + "' WHERE ID = '" + ID) Then
                    Throw New Exception()
                End If
            End If

            Return BatchId.ToString

        Catch ex As Exception
            MsgBox("Failed To Create Batch. Exception: " + ex.Message.ToString())
        End Try

        Return Nothing

    End Function

#End Region

#Region "Scan Module"

    Private Sub LoadScanModule()
        Me.Text = strOnlineTitle

        lblCdioNo.Text = CDIO.CDIO_NO
        txtModuleQR.Text = ""
        txtModuleQR.Focus()

        If String.IsNullOrEmpty(txtModuleQR.Text) Then
            btnScanDetails.Enabled = False
            btnFScanModule.Enabled = False
        End If
    End Sub

    Private Sub txtModuleQR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtModuleQR.TextChanged
        If Not String.IsNullOrEmpty(txtModuleQR.Text) Then
            btnScanDetails.Enabled = True
            btnFScanModule.Enabled = True
        Else
            btnScanDetails.Enabled = False
            btnFScanModule.Enabled = False
        End If
    End Sub

    Private Sub txtModuleQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtModuleQR.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                If Not String.IsNullOrEmpty(txtModuleQR.Text) Then

                    If Not IsModuleReadable(txtModuleQR.Text) Then
                        bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvScanModule)
                        Exit Sub
                    End If

                    'If ws_dcsClient.isConnected Then
                    If Not String.IsNullOrEmpty(txtModuleQR.Text) Then
                        'online
                        mode = True
                        If Not String.IsNullOrEmpty(MODD.MODULE_NO) Then
                            Cursor.Current = Cursors.WaitCursor
                            Call CreateTable(CDIO_INTERFACE)

                            Dim sqlStr As String = Nothing
                            sqlStr = "INSERT INTO [" & CDIO_INTERFACE & "] (RCV_INTERFACE_BATCH_ID, CDIO_ID, CDIO_NO, MODULE_ID, MODULE_NO, PILLING_ORDER, GROSS_WEIGHT, ORDER_NO, ORG_ID, CREATED_BY, CREATED_DATE) "
                            sqlStr = sqlStr & "VALUES ("
                            sqlStr = sqlStr & SQLQuote(GetBatchID()) & " , "
                            sqlStr = sqlStr & SQLQuote(CDIO.CDIO_ID) & " , "
                            sqlStr = sqlStr & SQLQuote(CDIO.CDIO_NO) & " , "
                            sqlStr = sqlStr & " 1, "
                            sqlStr = sqlStr & SQLQuote(MODD.MODULE_NO) & " , "
                            sqlStr = sqlStr & SQLQuote(MODD.PILLING_NO) & " , "
                            sqlStr = sqlStr & SQLQuote(MODD.GROSS_WEIGHT) & " , "
                            sqlStr = sqlStr & " 1, "
                            sqlStr = sqlStr & SQLQuote(CDIO.ORG_ID) & " , "
                            sqlStr = sqlStr & " HFZ, 'GETDATE()'"
                            sqlStr = sqlStr & ")"
                            If ExecuteSQL(sqlStr) = False Then
                                msgCode = "NG"
                                msgDesc = "Invalid Module No"
                                bringPanelToFront(pnlCdioRcvScanModuleError, pnlCdioRcvScanModule)
                                SetPanelCdioRcvScanText("PanelModError", msgCode, msgDesc, txtModuleQR.Text)
                            Else
                                msgCode = "OK"
                                msgDesc = "Succesfully Updated"
                                SetPanelCdioRcvScanText("PanelModOk", msgCode, msgDesc, Nothing)
                            End If

                            'Dim WHERE_CONDITION As String = " AND cdioID=" & SQLQuote(CDIO.CDIO_ID) & _
                            '                                " AND cdioNo=" & SQLQuote(CDIO.CDIO_NO) & _
                            '                                " AND moduleNo=" & SQLQuote(MODD.MODULE_NO) & _
                            '                                " AND pillingNo=" & SQLQuote(MODD.PILLING_NO) & _
                            '                                " AND grossWeight=" & SQLQuote(MODD.GROSS_WEIGHT) & _
                            '                                " AND orderNo=" & SQLQuote(MODD.ORDER_NO) & _
                            '                                " AND orgID=" & SQLQuote(CDIO.ORG_ID) & _
                            '                                " AND scanBy='TBA'" & _
                            '                                " AND scanDate='TBA'" & _
                            '                                " AND scannerID='TBA'" & _
                            '                                " AND forceCDIOStatus='TBA'" & _
                            '                                " AND forceCDIOResonID='TBA'" & _
                            '                                " AND forceModuleStatus='TBA'" & _
                            '                                " AND forceModuleReasonID='TBA'" & _
                            '                                " AND batchID='TBA'" & _
                            '                                " AND processType='CKDRECV'" & _
                            '                                " AND processCode='102'" & _
                            '                                " AND onOffLineFlag='N'"
                            'If ws_dcsClient.getData("CDIO_ID", CDIO_INTERFACE, WHERE_CONDITION).Rows.Count > 0 Then
                            '    'INSERT INTO JSP_RCV_CDIO_INTERFACE
                            'End If
                            Cursor.Current = Cursors.Default
                        End If
                    End If
                Else
                    Throw New Exception("Failed to read the Module QR.")
                End If
            Catch ex As WebException
                'offline
                mode = False
            Catch ex As Exception
                MsgBox("Module scanned is failed. Exception: " + ex.Message.ToString())
                Cursor.Current = Cursors.Default
            End Try
        End If
    End Sub

#End Region

End Class
