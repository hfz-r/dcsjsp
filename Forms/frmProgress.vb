Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Text.RegularExpressions
Imports DCSJSP.GeneralFunction
Imports DCSJSP.GeneralVariables
Imports DCSJSP.DCSWebService.DCSWebService
Imports System.Diagnostics
Imports System.Xml
Imports System.Reflection
'Imports System.Text

Public Class frmProgress

    Dim clsDataTransfer As New clsDataTransfer
    Dim gFullPath As String = gDBPath + gDatabaseName

    Public Sub Init()

        Try
            If Not System.IO.File.Exists(gDBPath + gDatabaseName) Then

                '----Create table if db not exist in local
                clsDataTransfer.PrepareTable(lblMessage, ProgressBar)
            End If

            '-----CHECK ONLINE / OFFLINE MODE-----'
            ws_dcsClient.Url = gStrDCSWebServiceURL
            If ws_dcsClient.isConnected() Then
                If ws_dcsClient.isOracleConnected() Then
                    Dim dt As String = ws_dcsClient.getTime()
                    SetLocalTime(dt)
                    gBoolAbnormal = False

                    '------ Check Device Name -----
                    If VerifyScannerID() = False Then
                        Call UpdateScannerID()
                    End If

                    '----- CHECK IMPORT SCHEDULE -----
                    '----- FIRST TIME WITH CLEAN DB ALWAYS IMPORT IS ON SCHEDULE BASED ON SERVER TIME -----
                    If CheckDataImportOnSchedule() = True Then

                        '---- CHECK FIRST TIME -----
                        '---- PRE-IMPORT DEFAULT VALUE ALWAYS TRUE ----
                        If CheckFirstTime() = True Then
                            DataImport()
                        End If
                    Else
                        '---- FIRST TIME IMPORT WILL BECOME RESET ----
                        '---- WHEN SCHEDULE DAY IS NOT TODAY ----
                        ResetFirstTime()
                    End If

                    Call LoadSetting()
                    Me.Close()

                    Dim frm = New frmReceiving
                    frm.AutoScroll = False
                    frm.Init()
                    frm.ShowDialog()
                    frm.Dispose() : frm = Nothing
                Else
                    MsgBox("Oracle Database down!", MsgBoxStyle.Critical, Me.Text)
                    gBoolAbnormal = True
                    'MsgBox("Oracle database down. Logout and login abnormal!", MsgBoxStyle.Critical, Me.Text)
                    'Exit Sub
                End If
            Else
                mode = False
            End If
        Catch ex As Exception
            gBoolAbnormal = True
            mode = False
            MsgBox("No connection! Empty database. Please retry to import again.", MsgBoxStyle.Information, "Import")
            'Me.Close()
            'setScannerDate()
        End Try

        If gBoolAbnormal = True Then
            setScannerDate()
        End If
    End Sub

    Private Sub setScannerDate()
        MsgBox("No connection. Please verify scanner date and time.", MsgBoxStyle.Critical, Me.Text)
        dtScannerDate.Value = DateTime.Now
        bringPanelToFront(pnlSetDatetime, pnlProgress)
    End Sub

    Private Sub DataImport()
        Try
            If clsDataTransfer.GetDataImport(lblMessage, ProgressBar) = True Then
                MsgBox("Successfully Imported", MsgBoxStyle.Information, "Import")
                gBoolAbnormal = False

                '---- UPDATE IMPORT FIRST TIME TO FALSE ONCE IMPORTED ----
                Dim sSQL As String = "UPDATE TblSetting SET SettingValue = 'N' WHERE SettingCode = 'FIRSTTIME' "
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
        Try
            VerifyScannerID = False

            Dim sSQL As String = Nothing
            Dim dbReader As SqlCeDataReader

            dbReader = OpenRecordset("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'SCNID' AND SettingValue = " & SQLQuote(gScannerID) & "", objConn)
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

    Private Sub UpdateScannerID()

        Dim reader As System.IO.StreamReader
        Dim line As String
        Dim gOriginalFile As String = "\Application\Device-Name.reg"
        Dim gUpdateFile As String = "\Application\Device-Name_Temp.reg"
        Dim gUpdateScannerID As String = ""
        Dim writer As New StreamWriter(gUpdateFile)

        If File.Exists(gOriginalFile) Then
            reader = File.OpenText(gOriginalFile)

            'now loop through each line
            While reader.Peek <> -1
                line = reader.ReadLine()

                'Now check for your specific word
                If line.StartsWith("""Name") Then
                    gUpdateScannerID = line.Split("=")(1).Split("""")(1)
                    line = line.Replace(gUpdateScannerID, System.Net.Dns.GetHostName)
                End If

                writer.WriteLine(line)
            End While

            writer.Close()
            'close your reader

            reader.Close()

            File.Delete(gOriginalFile)
            File.Move(gUpdateFile, gOriginalFile)


            '--- Update scanner id at local sdf ----
            Dim sSQL As String = ""
            gScannerID = gUpdateScannerID

            sSQL = "UPDATE TblSetting SET SettingValue = " & SQLQuote(gScannerID) & " WHERE SettingCode = 'SCNID' "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to update scanner id", MsgBoxStyle.Critical, "Service Part")
                Exit Sub
            End If

        Else
            MsgBox("Device Name file is not found, Please setup Device Name file into Application folder.", MsgBoxStyle.Critical, "Service Part")
            Exit Sub
        End If

    End Sub

    Private Function CheckDataImportOnSchedule() As Boolean
        Try
            CheckDataImportOnSchedule = False

            Dim sSQL As String = Nothing
            Dim dbReader As SqlCeDataReader

            dbReader = OpenRecordset("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'SCHEDULE' AND SettingValue = '" & Format(Now, "dddd") & "'", objConn)
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
        Try
            CheckFirstTime = False
            Dim sSQL As String = Nothing
            Dim dbReader As SqlCeDataReader

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

    Private Sub ResetFirstTime()
        Try
            Dim sSQL As String = Nothing
            sSQL = "UPDATE TBLSetting SET SettingValue = 'Y' WHERE SettingCode = 'FIRSTTIME'"
            ExecuteSQL(sSQL)
        Catch ex As Exception
            MsgBox(ex.Message.ToString(), MsgBoxStyle.Critical, "Error Reset First Time")
        End Try
    End Sub

    Private Function CheckDataImportOnToday() As Boolean
        Try
            CheckDataImportOnToday = False

            Dim sSQL As String = Nothing
            Dim dbReader As SqlCeDataReader

            dbReader = OpenRecordset("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'IMPORTDATETIME' AND SettingValue >= '" & Format(Now, "yyyy-MM-dd") & "' ", objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) = 0 Then
                    sSQL = "UPDATE TblSetting SET SettingValue = '" & Format(Now, gStrTimeFormatSQLCE) & "' WHERE SettingCode = 'IMPORTDATETIME' "
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
            If SetLocalTime(dtScannerDate.Value.ToString(gStrTimeFormat)) Then
                Me.Close()
                gBoolAbnormal = True

                Dim frm As New frmReceiving
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
        Me.Show()
        Me.Refresh()

        pnlProgress.Visible = True
        pnlDataTransfer.Visible = True
        pnlDataTransfer.BringToFront()
        Try
            Dim errStr = "Config File not found"
            Dim XMLmsg = Initialize()
            If XMLmsg = errStr Then
                Me.Close()
            End If
        Catch ex As Exception
            Me.Close()
        End Try

        Call Init()
    End Sub

End Class
