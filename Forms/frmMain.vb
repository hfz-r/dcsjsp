Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient

Public Class frmReceiving

    Friend cn As New clsConnection
    Dim bConnected As Boolean = False
    Dim clsDataTransfer As New clsDataTransfer

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

    Private Sub btnClostAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClostAuth.Click
        bringPanelToFront(pnlMainMenu, pnlSetDatetime)
    End Sub

    Private Sub btnMainClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainClose.Click
        Me.Close()
    End Sub

    Public Sub Init()

        Try

            TimerCheckOnline.Interval = interval
            TimerCheckOnline.Enabled = True
            footerStatusBar.Visible = False

            If gBoolAbnormal = True Then
                chkOffline = True ' CHECK ONLINE OFFLINE FOR ABNORMAL OR NORMAL MODE UPON DISCONNECTION
            Else
                chkOffline = False
            End If

            'txtUsername.Focus()
            bringPanelToFront(pnlMainMenu, pnlSetDatetime)

        Catch ex As Exception
            setScannerDate()
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

    Private Sub btnDateSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDateSave.Click
        'Try
        '    If SetLocalTime(dtScannerDate.Value.ToString(gStrTimeFormat)) Then
        '        'chkOffline = True
        '        'txtUsername.Focus()
        '        bringPanelToFront(pnlLogin, pnlSetDatetime)
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        'End Try
        bringPanelToFront(pnlAuthentication, pnlSetDatetime)
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ShowWait(True)
        'Check connection --> if offline --> force user to use offline mode --> if user don't want --> don't allow use
        'Try
        '    If chkOffline = False Then
        '        'ws_dcsClient.Url = gStrDCSWebServiceURL
        '        'ws_dcsClient.isConnected()
        '        mode = True
        '    Else
        '        mode = False
        '    End If

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    If MsgBox("No connection! Proceed abnormal mode?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
        '        chkOffline = True
        '        mode = True
        '    Else
        '        ShowWait(False)
        '        Exit Sub
        '    End If
        'End Try

        'Try
        '    mode = IIf(chkOffline, False, True)

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

    Private Sub btnSetting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetting.Click
        Dim frm As New frmSetting
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
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



    Private Sub txtUsername_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Select Case e.KeyCode
        '    Case Keys.Return, Keys.Enter
        '        txtPassword.SelectAll()
        '        txtPassword.Focus()
        'End Select
    End Sub

#Region "Function"
    Private Sub setScannerDate()
        MsgBox("No connection. Please verify scanner date and time.", MsgBoxStyle.Critical, Me.Text)
        dtScannerDate.Value = DateTime.Now
        bringPanelToFront(pnlSetDatetime, pnlMainMenu)
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

#End Region

    Private Sub TimerCheckOnline_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckOnline.Tick
        Try
            If mode = False Then
                If ws_dcsClient.isConnected Then
                    If ws_dcsClient.isOracleConnected Then
                        mode = True
                        MsgBox("Connection resolved. Logout and start online mode?", MsgBoxStyle.Information, Me.Text)
                        Application.Exit()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Connection not resolved!", MsgBoxStyle.Critical, "Offline")
            'ws_dcsClient.isConnected throwing error means connection not resolved, ignore.
        End Try
    End Sub

    Private Sub btnCDIOReceiving_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioReceiving.Click
        Dim frm As New frmCdioReceiving
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnUnpack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpack.Click
        Try
            Dim frm As New frmUnpack
            frm.AutoScroll = False
            frm.Init()
            frm.ShowDialog()
            frm.Dispose() : frm = Nothing
            'If mode = True Then
            '    Dim frm As New frmRCVPartOnline
            '    frm.AutoScroll = False
            '    If frm.Init() = True Then
            '        frm.ShowDialog()
            '    End If
            '    frm.Dispose() : frm = Nothing
            'Else
            '    Dim frm As New frmRCVPartOffline
            '    frm.AutoScroll = False
            '    frm.Init()
            '    frm.ShowDialog()
            '    frm.Dispose() : frm = Nothing
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub btnSupply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupply.Click
        'bringPanelToFront(pnlMenuSupplyPMSB, pnlMainMenu)
        bringPanelToFront(pnlMenuSupplyPGMSB, pnlMainMenu)
    End Sub

    Private Sub btnChildPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChildPart.Click
        Dim frm As New frmChildPart
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnChildPart2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChildPart2.Click
        Dim frm As New frmChildPart
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnRobbing2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRobbing2.Click
        Dim frm As New frmRobbing
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnRobbing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRobbing.Click
        Dim frm As New frmRobbing
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnBigPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBigPart.Click
        Dim frm As New frmBigPart
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    'Private Sub btnEngineBig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEngineBig.Click
    '    Dim frm As New frmEngineBig
    '    frm.AutoScroll = False
    '    frm.Init()
    '    frm.ShowDialog()
    '    frm.Dispose() : frm = Nothing
    'End Sub

    Private Sub btnProgressLane_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProgressLane.Click
        Dim frm As New frmProgressLane
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    'Private Sub btnEngineMedium_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEngineMedium.Click
    '    Dim frm As New frmEngineMedium
    '    frm.AutoScroll = False
    '    frm.Init()
    '    frm.ShowDialog()
    '    frm.Dispose() : frm = Nothing
    'End Sub

    'Private Sub btnHako_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHako.Click
    '    Dim frm As New frmHako
    '    frm.AutoScroll = False
    '    frm.Init()
    '    frm.ShowDialog()
    '    frm.Dispose() : frm = Nothing
    'End Sub

    Private Sub btnCloseSupplyPGMSB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseSupplyPGMSB.Click
        bringPanelToFront(pnlMainMenu, pnlMenuSupplyPGMSB)
    End Sub

    'Private Sub btnCloseSupplyPMSB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseSupplyPMSB.Click
    '    bringPanelToFront(pnlMainMenu, pnlMenuSupplyPMSB)
    'End Sub

    'Private Sub btnAssyMedium_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssyMedium.Click
    '    Dim frm As New frmAssyMedium
    '    frm.AutoScroll = False
    '    frm.Init()
    '    frm.ShowDialog()
    '    frm.Dispose() : frm = Nothing
    'End Sub
End Class
