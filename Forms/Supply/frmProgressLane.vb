Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
'Imports System.Data.SqlClient

Public Class frmProgressLane

    Friend cn As New clsConnection
    Dim bConnected As Boolean = False
    Dim clsDataTransfer As New clsDataTransfer
    Private Const strOnlineTitle As String = "Supply Progress Lane"
    Private Const strOfflineTitle As String = "Abnormal Supply Progress Lane"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmProgressLane_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmProgressLane_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
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
            bringPanelToFront(pnlPLMain, pnlPLScanShopping)

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

    Private Sub btnNextScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanIntPart, pnlPLScanShopping)
    End Sub

    Private Sub btnPLAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnScan.Click
        Me.Text = strOfflineTitle
        'pnlPLAbnScanPartError
        'bringPanelToFront(pnlPLAbnScanPart, pnlPLAbn)
        bringPanelToFront(pnlPLAbnFScanIntPart, pnlPLAbn)
    End Sub

    Private Sub btnPLAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnView.Click
        Me.Text = strOfflineTitle + " - View"
        bringPanelToFront(pnlPLAbnViewDet, pnlPLAbn)
    End Sub

    Private Sub btnPLAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnPost.Click
        Me.Text = strOnlineTitle + " - Posting"
        bringPanelToFront(pnlPLPosting, pnlPLAbn)
    End Sub

    Private Sub btnPLAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnDelete.Click
        Me.Text = strOnlineTitle + " - Delete"
        bringPanelToFront(pnlPLDelete, pnlPLAbn)
    End Sub

    Private Sub btnBackCPScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackScanShopping.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLScanShopping)
    End Sub

    Private Sub btnScanDetails_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlPLViewDet, pnlPLScanShopping)
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLFScanShopping)
    End Sub

    Private Sub btnCloseDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCloseDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLDelete)
    End Sub

    Private Sub btnScanPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanPL.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanShopping, pnlPLMain)
    End Sub

    Private Sub btnAbnormalPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalPL.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLMain)
    End Sub

    Private Sub btnClosePL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePL.Click
        Me.Close()
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanShopping.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanShoppingError, pnlPLScanShopping)
    End Sub

    Private Sub btnBackPLScanShoppingError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLScanShoppingError.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLScanShoppingError)
    End Sub

    Private Sub btnBackScanIntPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackScanIntPart.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLFScanIntPart)
    End Sub

    Private Sub btnPLIntPartDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLIntPartDet.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlPLViewDet, pnlPLFScanIntPart)
    End Sub

    Private Sub btnBackScanExtPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackScanExtPart.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanPartError, pnlPLScanExtPart)
    End Sub

    Private Sub btnBackFScanIntPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScanIntPart.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLFScanExtPart, pnlPLFScanIntPart)
    End Sub

    Private Sub btnBackPLViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLViewDet.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLViewDet)
    End Sub

    Private Sub btnBackPLAbnScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLAbnScanPart.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbnScanPartError, pnlPLAbnScanPart)
    End Sub

    Private Sub btnBackPLAbnScanPartError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLAbnScanPartError.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLAbnScanPartError)
    End Sub

    Private Sub btnClosePLAbnViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePLAbnViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLAbnViewDet)
    End Sub

    Private Sub btnClosePLPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePLPosting.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLPosting)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanIntPart, pnlPLFScanShopping)
    End Sub

    Private Sub btnPLFScanIntPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLFScanIntPart.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanExtPart, pnlPLScanIntPart)
    End Sub

    Private Sub btnPLFScanExtPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLFScanExtPart.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLFScanIntPart, pnlPLScanExtPart)
    End Sub

    Private Sub btnPLExtPartDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLExtPartDet.Click
        Me.Text = strOnlineTitle + " - View"
        bringPanelToFront(pnlPLViewDet, pnlPLScanExtPart)
    End Sub

    Private Sub btnCloseAbnPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnPL.Click
        Me.Text = "strOnlineTitle"
        bringPanelToFront(pnlPLMain, pnlPLAbn)
    End Sub

    Private Sub btnBackPLAbnFScanIntPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLAbnFScanIntPart.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbnFScanExtPart, pnlPLAbnFScanIntPart)
    End Sub

    Private Sub btnPLAbnFScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnFScanPart.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbnScanPartError, pnlPLAbnScanPartError)
    End Sub

    Private Sub btnFScanShoppingError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanShoppingError.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLFScanShopping, pnlPLScanShoppingError)
    End Sub

    Private Sub btnBackPLScanPartError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLScanPartError.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanPartError, pnlPLScanPartError)
    End Sub

    Private Sub btnBackPLAbnFScanExtPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLAbnFScanExtPart.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLAbnFScanExtPart)
    End Sub

    Private Sub btnBackFScanExtPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScanExtPart.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLFScanExtPart)
    End Sub
End Class
