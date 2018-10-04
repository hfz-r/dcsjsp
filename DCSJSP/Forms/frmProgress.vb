Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports DCSJSP.GeneralFunction
Imports DCSJSP.GeneralVariables
Imports System.Globalization
Imports System.Data

Public Class frmProgress

#Region ". Variable Declaration ."

    Dim clsDataTransfer As New clsDataTransfer
    Dim gFullPath As String = gDBPath + gDatabaseName

#End Region

#Region ". Private Function ."

    Private Sub setScannerDate()
        MsgBox("No connection. Please verify scanner date and time.")
        bringPanelToFront(pnlSetDatetime, pnlProgress)
        dtScannerDate.Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt")
    End Sub

    Private Sub DataImport()
        Dim sSQL As String = Nothing

        Try
            If clsDataTransfer.GetDataImport(lblMessage, ProgressBar) = True Then
                MsgBox("Successfully Imported", MsgBoxStyle.Information, "Import")
                gBoolAbnormal = False

                '---- UPDATE IMPORT FIRST TIME TO FALSE ONCE IMPORTED ----
                sSQL = "UPDATE TblSetting SET SettingValue = 'N' WHERE SettingCode = 'FIRSTTIME' "
                If Not ExecuteSQL(sSQL) Then
                    MsgBox("Error updating Import Time.", MsgBoxStyle.Information, "Import")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString(), MsgBoxStyle.Critical, "Data Import")
        End Try

    End Sub

    Private Function VerifyScannerID() As Boolean
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader

        Try
            VerifyScannerID = False
            dbReader = OpenRecordset(String.Format("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'SCNID' AND SettingValue = {0}", SQLQuote(gScannerID)), objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    VerifyScannerID = True
                End If
            End If

            Return VerifyScannerID

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "VerifyScannerID")
        End Try
    End Function

    Private Function CheckDataImportOnSchedule() As Boolean
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader
        Dim dt As DateTime = Nothing

        Try
            CheckDataImportOnSchedule = False
            dt = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt")
            dbReader = OpenRecordset(String.Format("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'SCHEDULE' AND SettingValue = '{0}'", dt.ToString("dddd")), objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    CheckDataImportOnSchedule = True
                End If
            End If

            Return CheckDataImportOnSchedule

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "CheckDataImportOnShedule")
        End Try
    End Function

    Private Function CheckFirstTime() As Boolean
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader

        Try
            CheckFirstTime = False
            dbReader = OpenRecordset("SELECT COUNT(SettingValue) FROM TBLSetting WHERE SettingCode = 'FIRSTTIME' AND SettingValue = 'Y'", objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    CheckFirstTime = True
                End If
            End If

            Return CheckFirstTime

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "CheckFirstTime")
        End Try
    End Function

    Private Sub ResetFirstTime(ByVal flag As String)
        Dim sSQL As String = Nothing

        Try
            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = '{0}' WHERE SettingCode = 'FIRSTTIME'", flag)
            ExecuteSQL(sSQL)
        Catch ex As Exception
            MsgBox(ex.Message.ToString(), MsgBoxStyle.Critical, "Error Reset First Time")
        End Try
    End Sub

    Private Function CheckDataImportOnToday() As Boolean
        Dim sSQL As String = Nothing
        Dim dbReader As SqlCeDataReader

        Try
            CheckDataImportOnToday = False
            dbReader = OpenRecordset(String.Format("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'IMPORTDATETIME' AND SettingValue >= '{0}' ", Format(Now, "yyyy-MM-dd")), objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) = 0 Then
                    sSQL = String.Format("UPDATE TblSetting SET SettingValue = '{0}' WHERE SettingCode = 'IMPORTDATETIME' ", Format(Now, gStrTimeFormatSQLCE))
                    If ExecuteSQL(sSQL) Then
                        CheckDataImportOnToday = True
                    End If
                    CheckDataImportOnToday = True
                End If
            End If

            Return CheckDataImportOnToday

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "CheckDataImportOnToday")
        End Try
    End Function

    Private Sub btnDateSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDateSave.Click
        Try
            If SetLocalTime(dtScannerDate.Value.ToString()) Then
                Me.Close()
                gBoolAbnormal = True

                Dim frm As New frmMain
                frm.AutoScroll = False
                frm.Init()
                frm.ShowDialog()
                frm.Dispose() : frm = Nothing

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub frmProgress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim errStr As String = "Config File not found"
        Dim XMLmsg As String = Nothing

        Me.Show()
        Me.Refresh()

        pnlProgress.Visible = True
        pnlDataTransfer.Visible = True
        pnlDataTransfer.BringToFront()
        Try
            XMLmsg = Initialize()
            If XMLmsg = errStr Then
                Me.Close()
            End If
        Catch ex As Exception
            Me.Close()
        End Try

        Call Init()
    End Sub
#End Region

#Region ". Public Function ."

    Public Sub Init()
        Dim dt As String = Nothing
        Try
            Cursor.Current = Cursors.WaitCursor

            If Not System.IO.Directory.Exists(gDBPath) Then
                System.IO.Directory.CreateDirectory(gDBPath)
            End If

            If Not System.IO.File.Exists(gDBPath + gDatabaseName) Then

                '----Create table if db not exist in local
                clsDataTransfer.PrepareTable(lblMessage, ProgressBar)
            End If

            Call LoadSetting()

            '-----CHECK ONLINE / OFFLINE MODE-----'
            If ws_dcsClient.isConnected() Then
                If ws_dcsClient.isOracleConnected() Then
                    dt = ws_dcsClient.getTimeSet()
                    SetLocalTime(dt)
                    gBoolAbnormal = False

                    '----- CHECK IMPORT SCHEDULE -----
                    '----- FIRST TIME WITH CLEAN DB ALWAYS IMPORT IS ON SCHEDULE BASED ON SERVER TIME -----
                    If CheckFirstTime() = True Or CheckDataImportOnSchedule() = True Then
                        '---- CHECK FIRST TIME -----
                        '---- PRE-IMPORT DEFAULT VALUE ALWAYS TRUE ----
                        DataImport()

                        '---- FIRST TIME IMPORT WILL BECOME RESET ----
                        '---- WHEN SCHEDULE DAY IS NOT TODAY ----
                        ResetFirstTime("N")
                    End If

                    Cursor.Current = Cursors.Default

                    Dim frm As New frmMain
                    frm.AutoScroll = False
                    frm.Init()
                    frm.ShowDialog()
                    frm.Dispose() : frm = Nothing
                Else
                    Cursor.Current = Cursors.Default
                    gBoolAbnormal = True
                End If
            Else
                Cursor.Current = Cursors.Default
                mode = False
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gBoolAbnormal = True
            mode = False
        End Try

        If gBoolAbnormal = True Then
            setScannerDate()
        End If
    End Sub

#End Region

End Class
