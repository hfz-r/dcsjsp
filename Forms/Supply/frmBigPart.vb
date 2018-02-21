Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient

Public Class frmBigPart

    Friend cn As New clsConnection
    Dim bConnected As Boolean = False
    Dim clsDataTransfer As New clsDataTransfer
    Private Const strOnlineTitle As String = "Supply Big Parts"
    Private Const strOfflineTitle As String = "Abnormal Supply Big Parts"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmBigPart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmBigPart_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
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
            bringPanelToFront(pnlBPMain, pnlBPScanModule)

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

    Private Sub btnOGVendor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If mode = True Then
            '    Dim frm As New frmOGVendorOnline
            '    frm.AutoScroll = False
            '    If frm.Init() = True Then
            '        frm.ShowDialog()
            '    End If
            '    frm.Dispose() : frm = Nothing
            'Else
            '    Dim frm As New frmOGVendorOffline
            '    frm.AutoScroll = False
            '    frm.Init()
            '    frm.ShowDialog()
            '    frm.Dispose() : frm = Nothing
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub btnOGShop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If mode = True Then
            '    Dim frm As New frmOGShopOnline
            '    frm.AutoScroll = False
            '    If frm.Init() = True Then
            '        frm.ShowDialog()
            '    End If
            '    frm.Dispose() : frm = Nothing
            'Else
            '    Dim frm As New frmOGShopOffline
            '    frm.AutoScroll = False
            '    If frm.Init() = True Then
            '        frm.ShowDialog()
            '    End If
            '    frm.Dispose() : frm = Nothing
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
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

    Private Sub btnAbnormalBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalBP.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPMain)
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanSubmit.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlBPViewDet, pnlBPScanModule)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanModule.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPFScan, pnlBPScanModule)
    End Sub

    Private Sub btnBPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbnScan, pnlBPAbn)
    End Sub

    Private Sub btnBPAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnView.Click
        Me.Text = strOfflineTitle + " - View"
        bringPanelToFront(pnlBPAbnViewDet, pnlBPAbn)
    End Sub

    Private Sub btnBPAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnPost.Click
        Me.Text = strOnlineTitle + " - Posting"
        bringPanelToFront(pnlBPPosting, pnlBPAbn)
    End Sub

    Private Sub btnBPAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnDelete.Click
        Me.Text = strOnlineTitle + " - Delete"
        bringPanelToFront(pnlBPDelete, pnlBPAbn)
    End Sub

    Private Sub btnBackBPScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPScanModule.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPScanError, pnlBPScanModule)
    End Sub

    Private Sub btnCloseBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBP.Click
        Me.Close()
    End Sub

    Private Sub btnScanDetails_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlBPViewDet, pnlBPScanModule)
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPMain, pnlBPFScan)
    End Sub

    Private Sub btnBackBPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPViewDet.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPMain, pnlBPViewDet)
    End Sub

    Private Sub BtnBackScanError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBackScanError.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPMain, pnlBPScanError)
    End Sub

    Private Sub btnBackBPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbnScanError, pnlBPAbnScan)
    End Sub

    Private Sub btnCloseBPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPAbnViewDet)
    End Sub

    Private Sub btnCloseBPPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPPosting.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPPosting)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPDelete)
    End Sub

    Private Sub btnScanBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanBP.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPScanModule, pnlBPMain)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlBPViewDet, pnlBPFScan)
    End Sub

    Private Sub btnBPScanDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPScanDet.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlBPViewDet, pnlBPScanError)
    End Sub

    Private Sub btnCloseAbnBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnBP.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPMain, pnlBPAbn)
    End Sub

    Private Sub btnBackBPAbnScanError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPAbnScanError.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPAbnScanError)
    End Sub
End Class
