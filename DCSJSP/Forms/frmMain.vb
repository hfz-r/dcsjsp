Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient

Public Class frmMain

#Region ". Initialization ."

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

            bringPanelToFront(pnlMainMenu, pnlSetDatetime)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmMain_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
            ws_dcsClient.Dispose()
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

#Region ". Private Function ."

    Private Function CheckDataImportOnShedule() As Boolean
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader

        Try
            CheckDataImportOnShedule = False
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
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader

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

    Private Function IsOrganizationSelected() As Boolean
        If String.IsNullOrEmpty(org_ID) Then
            MsgBox("Select organization in Setting.", MsgBoxStyle.Critical)
            Return False
        End If
        Return True
    End Function

    Private Function IsScannerNoValid() As Boolean
        If String.IsNullOrEmpty(gSCNNo) Then
            MsgBox("Key in Scanner No for Batch in Setting.", MsgBoxStyle.Critical)
            Return False
        End If
        Return True
    End Function
#End Region

#Region ". Event ."

    Private Sub TimerCheckOnline_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckOnline.Tick
        Try
            If mode = False Then
                If ws_dcsClient.isConnected Then
                    If ws_dcsClient.isOracleConnected Then
                        mode = True
                        MsgBox("Connection resolved.", MsgBoxStyle.Information, Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            'ws_dcsClient.isConnected throwing error means connection not resolved, ignore.
            MsgBox("Connection not resolved!", MsgBoxStyle.Critical, "Offline")
        End Try
    End Sub

#End Region

#Region ". Navigation  ."

    Private Sub btnClostAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        bringPanelToFront(pnlMainMenu, pnlSetDatetime)
    End Sub

    Private Sub btnMainClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainClose.Click
        If MessageBox.Show("Confirm to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub btnSetting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetting.Click

        Dim frm As New frmSetting
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnCDIOReceiving_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdioReceiving.Click
        If IsOrganizationSelected() Then 'Check if Org ID is selected
            If IsScannerNoValid() Then 'Check if Scanner No is entered
                Cursor.Current = Cursors.WaitCursor
                Dim frm As New frmCdioReceiving
                TimerCheckOnline.Enabled = False
                frm.AutoScroll = False
                frm.Init()
                frm.ShowDialog()
                frm.Dispose() : frm = Nothing
            End If
        End If
    End Sub

    Private Sub btnUnpack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpack.Click
        Try
            If IsOrganizationSelected() Then 'Check if Org ID is selected
                If IsScannerNoValid() Then 'Check if Scanner No is entered
                    Cursor.Current = Cursors.WaitCursor
                    Dim frm As New frmUnpack
                    TimerCheckOnline.Enabled = False
                    frm.AutoScroll = False
                    frm.Init()
                    frm.ShowDialog()
                    frm.Dispose() : frm = Nothing
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub btnSupply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupply.Click
        If IsOrganizationSelected() Then 'Check if Org ID is selected
            If IsScannerNoValid() Then 'Check if Scanner No is entered
                bringPanelToFront(pnlMenuSupply, pnlMainMenu)
            End If
        End If
    End Sub

    Private Sub btnChildPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChildPart.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frm As New frmChildPart
        TimerCheckOnline.Enabled = False
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnRobbing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRobbing.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frm As New frmRobbing
        TimerCheckOnline.Enabled = False
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnBigPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBigPart.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frm As New frmBigPart
        TimerCheckOnline.Enabled = False
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnProgressLane_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProgressLane.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frm As New frmProgressLane
        TimerCheckOnline.Enabled = False
        frm.AutoScroll = False
        frm.Init()
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing
    End Sub

    Private Sub btnCloseSupply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseSupply.Click
        bringPanelToFront(pnlMainMenu, pnlMenuSupply)
    End Sub

#End Region

End Class
