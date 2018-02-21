Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Reflection
Imports System.Data.SqlServerCe


Public Class clsDataTransfer

#Region ". Prepare Table For Scanner ."

    Public Function PrepareTable(ByRef lblRefMsg As Label, _
    ByRef PrgRef As ProgressBar) As Boolean

        Dim blnPrepareTable As Boolean = True
        Dim dbReader As SqlServerCe.SqlCeDataReader = Nothing

        lblMessage = lblRefMsg
        progressBar = PrgRef

        lblMessage.Text = "Creating database...please wait."
        Application.DoEvents()

        Call DatabaseInit(progressBar)
        Return blnPrepareTable

    End Function

    Public Function GetDataImport(ByRef lblRefMsg As Label, _
  ByRef PrgRef As ProgressBar) As Boolean

        Dim blnDataImport As Boolean = True
        Dim dbReader As SqlServerCe.SqlCeDataReader = Nothing

        lblMessage = lblRefMsg
        progressBar = PrgRef

        lblMessage.Text = "Import data in process...please wait."
        Application.DoEvents()

        If Not DownloadMasterData(progressBar) Then
            blnDataImport = False
        End If

        Return blnDataImport

    End Function

#End Region

#Region ". Process Import Master Data From DCSWebService ."

    Public Function ImportMasterData(ByVal TableName As String) As Boolean
        Dim ErrLoc As String = ""
        Dim blnResult As Boolean = False

        'Call ShowWait(True)

        '' progressBar.Maximum = 100
        ''progressBar.Value = 0

        'Dim sSQL As String = ""
        'Dim dsSet As DataSet
        'Dim dtMasterImport As DataTable = New DataTable

        'ErrLoc = ".ImportMaster " & TableName
        '' progressBar.Value = progressBar.Value + 10

        '' ********************
        '' Prepare Insert Statement 
        '' ********************
        'Try

        '    'lblMessage.Text = "Reading " & TableName & ""
        '    ' Application.DoEvents()

        '    Select Case TableName.ToUpper.Trim
        '        Case "SEP_LOGIN_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT LOGIN_ID FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("LOGIN_ID, PASSWORD", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                ' progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '                    For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                    sSQL = "INSERT INTO [" & TableName & "] (LOGIN_ID, PASSWORD) "
        '                        sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("LOGIN_ID").ToString) & " , "
        '                    sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("PASSWORD").ToString)
        '                        sSQL = sSQL & ")"
        '                       If ExecuteSQL(sSQL) = False Then
        '                          MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                             Exit Function
        '                        End If
        '                        ' progressBar.Value = progressBar.Value + 1
        '                    Next
        '                    ' progressBar.Maximum = dtMasterImport.Rows.Count
        '               End If
        '            ' progressBar.Value = progressBar.Maximum


        '        Case "SEP_RB_IMPORTER_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT CUSTOMER_ID FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("CUSTOMER_ID, CUSTOMER_NAME", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                ' progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '                     For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                        sSQL = "INSERT INTO [" & TableName & "] (CUSTOMER_ID, CUSTOMER_NAME) "
        '                        sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("CUSTOMER_ID").ToString)
        '                        sSQL = sSQL & ", " & SQLQuote(dtMasterImport.Rows(i).Item("CUSTOMER_NAME").ToString)
        '                        sSQL = sSQL & ")"
        '                       If ExecuteSQL(sSQL) = False Then
        '                          MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                             Exit Function
        '                        End If
        '                        '  progressBar.Value = progressBar.Value + 1
        '                    Next
        '                      ' progressBar.Maximum = dtMasterImport.Rows.Count
        '            End If
        '            '  progressBar.Value = progressBar.Maximum


        '        Case "SEP_RB_REASON_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT REASON_ID FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("REASON_ID, REASON_DESC", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                ' progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '              For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                        sSQL = "INSERT INTO [" & TableName & "] (REASON_ID, REASON_DESC) "
        '                        sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("REASON_ID").ToString)
        '                        sSQL = sSQL & ", " & SQLQuote(dtMasterImport.Rows(i).Item("REASON_DESC").ToString)
        '                        sSQL = sSQL & ")"
        '                       If ExecuteSQL(sSQL) = False Then
        '                          MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                             Exit Function
        '                        End If
        '                        '   progressBar.Value = progressBar.Value + 1
        '                    Next
        '                       '  progressBar.Maximum = dtMasterImport.Rows.Count
        '            End If
        '            '  progressBar.Value = progressBar.Maximum


        '        Case "SEP_RB_TYPE_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT RB_CODE FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("RB_CODE, RB_TYPE", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                '  progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '               For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                        sSQL = "INSERT INTO [" & TableName & "] (RB_CODE, RB_TYPE) "
        '                    sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("RB_CODE").ToString)
        '                        sSQL = sSQL & ", " & SQLQuote(dtMasterImport.Rows(i).Item("RB_TYPE").ToString)
        '                        sSQL = sSQL & ")"
        '                       If ExecuteSQL(sSQL) = False Then
        '                          MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                             Exit Function
        '                        End If
        '                        '    progressBar.Value = progressBar.Value + 1
        '                    Next
        '              '  progressBar.Maximum = dtMasterImport.Rows.Count
        '            End If
        '            '  progressBar.Value = progressBar.Maximum


        '        Case "SEP_SUPPLIER_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT SUPPLIER_ID FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("SUPPLIER_ID, SUPPLIER_NAME", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                ' progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '                 For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                        sSQL = "INSERT INTO [" & TableName & "] (SUPPLIER_ID, SUPPLIER_NAME) "
        '                        sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("SUPPLIER_ID").ToString)
        '                        sSQL = sSQL & ", " & SQLQuote(dtMasterImport.Rows(i).Item("SUPPLIER_NAME").ToString)
        '                        sSQL = sSQL & ")"
        '                       If ExecuteSQL(sSQL) = False Then
        '                          MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                             Exit Function
        '                        End If
        '                        '   progressBar.Value = progressBar.Value + 1
        '                    Next
        '                       ' progressBar.Maximum = dtMasterImport.Rows.Count
        '            End If
        '            '  progressBar.Value = progressBar.Maximum


        '        Case "SEP_PACK_IMPORTER_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT CUSTOMER_ID FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("CUSTOMER_ID, CUSTOMER_NAME", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                '  progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '                For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                    sSQL = "INSERT INTO [" & TableName & "] (CUSTOMER_ID, CUSTOMER_NAME) "
        '                    sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("CUSTOMER_ID").ToString)
        '                    sSQL = sSQL & ", " & SQLQuote(dtMasterImport.Rows(i).Item("CUSTOMER_NAME").ToString)
        '                    sSQL = sSQL & ")"
        '                    If ExecuteSQL(sSQL) = False Then
        '                        MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                        Exit Function
        '                    End If
        '                    '   progressBar.Value = progressBar.Value + 1
        '                Next
        '                '   progressBar.Maximum = dtMasterImport.Rows.Count
        '            End If
        '            '  progressBar.Value = progressBar.Maximum


        '        Case "SEP_CASE_TYPE_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT CASE_ID FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("CASE_ID, CASE_TYPE", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                '  progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '                For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                    sSQL = "INSERT INTO [" & TableName & "] (CASE_ID, CASE_TYPE) "
        '                    sSQL = sSQL & " VALUES(" & dtMasterImport.Rows(i).Item("CASE_ID").ToString
        '                    sSQL = sSQL & ", " & SQLQuote(dtMasterImport.Rows(i).Item("CASE_TYPE").ToString)
        '                    sSQL = sSQL & ")"
        '                    If ExecuteSQL(sSQL) = False Then
        '                        MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                        Exit Function
        '                    End If
        '                    '  progressBar.Value = progressBar.Value + 1
        '                Next
        '                '  progressBar.Maximum = dtMasterImport.Rows.Count
        '            End If
        '            '  progressBar.Value = progressBar.Maximum


        '        Case "SEP_RB_STOPPER_QTY_V"
        '            dsSet = New DataSet
        '            dsSet = OpenRecordset("SELECT STOPPER_TYPE FROM [" & TableName & "]")
        '            If dsSet.Tables(0).Rows.Count <> 0 Then
        '                ExecuteSQL("DELETE FROM [" & TableName & "]")
        '                dsSet.Clear() : dsSet = Nothing
        '            End If

        '            '---- Get data from WebService put into datatable -------------
        '            dtMasterImport = ws_dcsClient.getData("STOPPER_TYPE, BOX_TYPE, QTY", TableName, "")
        '            If dtMasterImport.Rows.Count > 0 Then

        '                '   progressBar.Value = 0
        '                Application.DoEvents()

        '                '---- Insert data into local database from datatable -------------
        '                    For i As Integer = 0 To dtMasterImport.Rows.Count - 1
        '                        sSQL = "INSERT INTO [" & TableName & "] (STOPPER_TYPE, BOX_TYPE, QTY) "
        '                        sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("STOPPER_TYPE").ToString)
        '                        sSQL = sSQL & ", " & SQLQuote(dtMasterImport.Rows(i).Item("BOX_TYPE").ToString)
        '                        sSQL = sSQL & ", " & dtMasterImport.Rows(i).Item("QTY").ToString
        '                        sSQL = sSQL & ")"
        '                        If ExecuteSQL(sSQL) = False Then
        '                          MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
        '                             Exit Function
        '                        End If
        '                        '    progressBar.Value = progressBar.Value + 1
        '                    Next
        '                        '   progressBar.Maximum = dtMasterImport.Rows.Count
        '            End If
        '            '   progressBar.Value = progressBar.Maximum

        '    End Select

        '    blnResult = True
        '    Call ShowWait(False)

        'Catch ex As Exception
        '    UnHandledError(ex.Message, ErrLoc)
        '    Return False
        'End Try

        'GC.Collect()
        'GC.WaitForPendingFinalizers()
        Return blnResult

        '  progressBar.Value = progressBar.Maximum
        'Call ShowWait(False)
    End Function

    Public Function ImportScannerNumber() As Boolean
        Dim ErrLoc As String = ".GetScannerNumber"
        Dim serialNumber As String = ""
        Dim dtScnNum As DataTable = New DataTable()
        Dim TableName As String = "ESPS_SC_ID"
        Dim strSQL As String = ""
        Dim scnNumber As Integer = 0

        Try
            'Get device serial number
            serialNumber = ReadUUID()
            'serialNumber = "73E65B760649010800150D3072360605"

            'If serialNumber <> "" And serialNumber <> Nothing Then
            '    dtScnNum = ws_dcsClient.getData(" SCANNER_NUM ", TableName, " AND SERIAL_NO = " & SQLQuote(serialNumber))
            '    If dtScnNum.Rows.Count > 0 Then
            '        'get the scanner number
            '        scnNumber = Val(dtScnNum.Rows(0).Item("SCANNER_NUM").ToString)

            '        'set to local setting table
            '        If scnNumber > 0 Then
            '            strSQL = "UPDATE TblSetting SET SettingValue=" & scnNumber & " WHERE settingCategory='BATCHID' AND settingCode='SCN_NO'"
            '            If Not ExecuteSQL(strSQL) Then
            '                MsgBox("Fail to update Scanner Number!", MsgBoxStyle.Critical, gAppName)
            '                Return False
            '            Else
            '                Dim scanner_id As String = gScnPrefix & scnNumber.ToString.PadLeft(2, "0") & gScnSuffix
            '                strSQL = "UPDATE TblSetting SET SettingValue=" & SQLQuote(scanner_id) & " WHERE settingCategory='SCN' AND settingCode='SCNID'"
            '                If Not ExecuteSQL(strSQL) Then
            '                    MsgBox("Fail to update Scanner ID!", MsgBoxStyle.Critical, gAppName)
            '                    Return False
            '                Else
            '                    setDeviceName(scanner_id)
            '                End If
            '            End If
            '        Else
            '            MsgBox("The reference value is incorrect!", MsgBoxStyle.Critical, gAppName)
            '            Return False
            '        End If

            '    Else
            '        'get next scanner number
            '        'dtScnNum.Clear()
            '        'dtScnNum = ws_dcsClient.getData(" SCANNER_NUM ", TableName, " ORDER BY SCANNER_NUM DESC")
            '        'If dtScnNum.Rows.Count > 0 Then
            '        '    If Not String.IsNullOrEmpty(dtScnNum.Rows(0).Item("SCANNER_NUM")) Then
            '        '        scnNumber = Val(dtScnNum.Rows(0).Item("SCANNER_NUM").ToString) + 1
            '        '    Else
            '        '        scnNumber = 1
            '        '    End If
            '        'Else
            '        '    scnNumber = 1
            '        'End If

            '        ''insert into table
            '        'Dim insertSQL As String = "INSERT INTO " & TableName & " " _
            '        '& "(SERIAL_NO ,SCANNER_NUM ) " _
            '        '& " VALUES (" & SQLQuote(serialNumber) & " , " _
            '        '& " " & scnNumber & ")"
            '        'If Not ws_dcsClient.insertData(insertSQL) Then
            '        '    MsgBox("Fail to register Scanner!", MsgBoxStyle.Critical, gAppName)
            '        'End If

            '        ''set to local setting table
            '        'strSQL = "UPDATE TblSetting SET SettingValue=" & scnNumber & " WHERE settingCategory='BATCHID' AND settingCode='SCN_NO'"
            '        'If Not ExecuteSQL(strSQL) Then
            '        '    MsgBox("Fail to update Scanner Number!", MsgBoxStyle.Critical, gAppName)
            '        '    Return False
            '        'Else
            '        '    Dim scanner_id As String = gScnPrefix & scnNumber.ToString.PadLeft(2, "0") & gScnSuffix
            '        '    strSQL = "UPDATE TblSetting SET SettingValue=" & SQLQuote(scanner_id) & " WHERE settingCategory='SCN' AND settingCode='SCNID'"
            '        '    If Not ExecuteSQL(strSQL) Then
            '        '        MsgBox("Fail to update Scanner ID!", MsgBoxStyle.Critical, gAppName)
            '        '        Return False
            '        '    Else
            '        '        setDeviceName(scanner_id)
            '        '    End If
            '        'End If

            '    End If

            'Else
            '    MsgBox("Error getting device's serial number!", MsgBoxStyle.Critical, gAppName)
            '    Return False
            'End If

            Return True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
            Return False
        End Try
    End Function

#End Region

End Class
