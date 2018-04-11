Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
'Imports System.Data.SqlClient

Public Class frmRobbing

    Friend cn As New clsConnection
    Dim bConnected As Boolean = False
    Dim clsDataTransfer As New clsDataTransfer
    Private Const strOnlineTitle As String = "Supply Robbing"
    Private Const strOfflineTitle As String = "Abnormal Supply Robbing"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmRobbing_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmRobbing_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
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
            bringPanelToFront(pnlRBMain, pnlRBScanPart)

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

    Private Sub btnAbnormalRB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalRB.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBMain)
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanSubmit.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlRBViewDet, pnlRBScanPart)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanPart.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBFScan, pnlRBScanPart)
    End Sub

    Private Sub btnRBAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbnScanPart, pnlRBAbn)
    End Sub

    Private Sub btnRBAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnView.Click
        Me.Text = strOfflineTitle + " - View"
        bringPanelToFront(pnlRBAbnViewDet, pnlRBAbn)
    End Sub

    Private Sub btnRBAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnPost.Click
        Me.Text = strOnlineTitle + " - Posting"
        bringPanelToFront(pnlRBPosting, pnlRBAbn)
    End Sub

    Private Sub btnRBAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnDelete.Click
        Me.Text = strOnlineTitle + " - Delete"
        bringPanelToFront(pnlRBDelete, pnlRBAbn)
    End Sub

    Private Sub btnBackRBScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBScanPartError, pnlRBScanPart)
    End Sub

    Private Sub pnlRBMain_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlRBMain.GotFocus
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBScanPart, pnlRBMain)
    End Sub

    Private Sub btnCloseRB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRB.Click
        Me.Close()
    End Sub

    Private Sub btnScanDetails_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlRBViewDet, pnlRBScanPart)
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBMain, pnlRBFScan)
    End Sub

    Private Sub btnBackRBViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBViewDet.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBMain, pnlRBViewDet)
    End Sub

    Private Sub btnCloseRBViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRBViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBAbnViewDet)
    End Sub

    Private Sub btnCloseRBPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePosting.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBPosting)
    End Sub

    Private Sub btnCloseDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBDelete)
    End Sub

    Private Sub btnScanRB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanRB.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBScanPart, pnlRBMain)
    End Sub

    Private Sub btnBackRBScanError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBScanError.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBFScan, pnlRBScanPartError)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBMain, pnlRBFScan)
    End Sub

    Private Sub btnBackRBAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbnScanPartError, pnlRBAbnScanPart)
    End Sub

    Private Sub btnCloseAbnRB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnRB.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBMain, pnlRBAbn)
    End Sub

    Private Sub btnBackRBAbnScanPartError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBAbnScanPartError.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBAbnScanPartError)
    End Sub
End Class
