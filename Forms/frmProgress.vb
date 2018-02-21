Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Text.RegularExpressions
Imports DCSJSP.GeneralFunction

Public Class frmProgress

    Dim clsDataTransfer As New clsDataTransfer

    Public Sub Init()

        If Not System.IO.File.Exists(gDBPath + gDatabaseName) Then
            Try
                '----Create table if db not exist in local
                If clsDataTransfer.PrepareTable(lblMessage, ProgressBar) = True Then

                    Call LoadSetting()

                    Try
                        'ws_dcsClient.Url = gStrDCSWebServiceURL
                        'If ws_dcsClient.isConnected() Then
                        '    'verify if Oracle inside webservice is connected
                        '    If ws_dcsClient.isOracleConnected() Then
                        '        Dim dt As String = ws_dcsClient.getTime()
                        '        SetLocalTime(dt)
                        '        mode = True
                        '    Else
                        '        MsgBox("Oracle database down. Logout and login abnormal!", MsgBoxStyle.Critical, Me.Text)
                        '        Exit Sub
                        '    End If
                        'Else
                        '    mode = False
                        'End If
                    Catch ex As Exception
                        mode = False
                        MsgBox("No connection! Empty database. Please retry to import again.", MsgBoxStyle.Information, "Import")
                        'Exit Sub
                        Me.Close()
                    End Try

                    If clsDataTransfer.GetDataImport(lblMessage, ProgressBar) = True Then
                        MsgBox("Successfully Imported", MsgBoxStyle.Information, "Import")

                        Me.Close()
                        gBoolAbnormal = False

                        Dim frm As New frmReceiving
                        frm.AutoScroll = False
                        frm.Init()
                        frm.ShowDialog()
                        frm.Dispose() : frm = Nothing
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SERVICE PART")
                Exit Sub
            End Try
        End If

        Call LoadSetting()

        'check online / webservice connected
        Try
            'ws_dcsClient.Url = gStrDCSWebServiceURL
            'If ws_dcsClient.isConnected() Then
            '    'verify if Oracle inside webservice is connected
            '    If ws_dcsClient.isOracleConnected() Then
            '        Dim dt As String = ws_dcsClient.getTime()
            '        SetLocalTime(dt)
            '        mode = True
            '    Else
            '        MsgBox("Oracle database down. Logout and login abnormal!", MsgBoxStyle.Critical, Me.Text)
            '        Exit Sub
            '    End If
            'Else
            '    mode = False
            'End If
        Catch ex As Exception
            mode = False

        End Try

        If mode = True Then

            Try
                '------ Check Device Name -----
                'If VerifyScannerID() = False Then
                '    Call UpdateScannerID()
                'End If


                'test to set the import time to yesterday so can invoke import
                'Dim lala As String = "UPDATE TblSetting SET SettingValue = '" & Format(DateTime.Today.AddDays(-1), gStrTimeFormatSQLCE) & "' WHERE SettingCode = 'IMPORTDATETIME' "
                'If Not ExecuteSQL(lala) Then
                '    MsgBox("Error updating Import Time.", MsgBoxStyle.Information, "Import")
                '    Exit Sub
                'End If

                '------check day schedule ----
                If CheckDataImportOnSchedule() = True Then
                    If CheckDataImportOnToday() = True Then

                        'Process Import
                        If clsDataTransfer.GetDataImport(lblMessage, ProgressBar) = True Then
                            MsgBox("Successfully Imported", MsgBoxStyle.Information, "Import")

                            'update last import time
                            Dim sSQL As String = "UPDATE TblSetting SET SettingValue = '" & Format(Now, gStrTimeFormatSQLCE) & "' WHERE SettingCode = 'IMPORTDATETIME' "
                            If Not ExecuteSQL(sSQL) Then
                                MsgBox("Error updating Import Time.", MsgBoxStyle.Information, "Import")
                                Exit Sub
                            End If

                        Else
                            MsgBox("Error updating scanner ID", MsgBoxStyle.Critical, "Import")
                            Me.Close()
                            Exit Sub
                        End If
                    End If
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try

            Me.Close()
            gBoolAbnormal = False

            Dim frm As New frmReceiving
            frm.AutoScroll = False
            frm.Init()
            frm.ShowDialog()
            frm.Dispose() : frm = Nothing
        Else
            setScannerDate()
        End If

    End Sub

    Private Sub setScannerDate()
        MsgBox("No connection. Please verify scanner date and time.", MsgBoxStyle.Critical, Me.Text)
        dtScannerDate.Value = DateTime.Now
        bringPanelToFront(pnlSetDatetime, pnlProgress)
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

    Private Function CheckDataImportOnToday() As Boolean
        Try
            CheckDataImportOnToday = False

            Dim sSQL As String = Nothing
            Dim dbReader As SqlCeDataReader

            dbReader = OpenRecordset("SELECT COUNT(SettingValue) FROM TblSetting WHERE SettingCode = 'IMPORTDATETIME' AND SettingValue >= '" & Format(Now, "yyyy-MM-dd") & "' ", objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) = 0 Then
                    'sSQL = "UPDATE TblSetting SET SettingValue = '" & Format(Now, gStrTimeFormatSQLCE) & "' WHERE SettingCode = 'IMPORTDATETIME' "
                    'If ExecuteSQL(sSQL) Then
                    '    CheckDataImportOnToday = True
                    'End If
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

        Call Init()
    End Sub
End Class
