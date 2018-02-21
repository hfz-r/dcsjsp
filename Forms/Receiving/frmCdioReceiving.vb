Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient

Public Class frmCdioReceiving

    Friend cn As New clsConnection
    Dim bConnected As Boolean = False
    Dim clsDataTransfer As New clsDataTransfer
    Private Const strOnlineTitle As String = "CDIO Receiving"
    Private Const strOfflineTitle As String = "Abnormal CDIO Receiving"

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
            'TimerCheckOnline.Interval = interval
            'TimerCheckOnline.Enabled = True
            footerStatusBar.Visible = False

            'If gBoolAbnormal = True Then
            '    chkOffline.Checked = True
            'Else
            '    chkOffline.Checked = False
            'End If

            'txtUsername.Focus()
            bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScan)

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

#Region "Function"

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

#End Region

    Private Sub TimerCheckOnline_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckOnline.Tick
        Try
            If mode = False Then
                'If ws_dcsClient.isConnected Then
                '    mode = True
                '    MsgBox("Connection resolved. Logout and start online mode?", MsgBoxStyle.Information, Me.Text)
                '    Application.Exit()
                'End If
            End If
        Catch ex As Exception
            'ws_dcsClient.isConnected throwing error means connection not resolved, ignore.
        End Try
    End Sub

    Private Sub btnScanCdio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanCdio.Click
        Me.Text = "CDIO Receiving"
        bringPanelToFront(pnlCdioRcvScan, pnlCdioRcvMain)
    End Sub

    Private Sub btnAbnormalCdio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalCdio.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCdioRcvAbn, pnlCdioRcvMain)
    End Sub


    Private Sub btnNextScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvScanModule, pnlCdioRcvScan)
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlCdioRcvViewDet, pnlCdioRcvScanModule)
    End Sub


    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanModule.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvFScan, pnlCdioRcvScanModule)
    End Sub

    Private Sub btnFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvScanError, pnlCdioRcvScan)
    End Sub

    Private Sub btnBackFrError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFrError.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScanError)
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
        bringPanelToFront(pnlCdioRcvScanModuleError, pnlCdioRcvScanModule)
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
        bringPanelToFront(pnlCdioRcvMain, pnlCdioRcvScanModuleError)
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
End Class
