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
        Dim gFullPath As String = gDBPath + gDatabaseName

        Call ShowWait(True)

        progressBar.Maximum = 100
        progressBar.Value = 0

        Dim sSQL As String = ""
        Dim dsSet As DataSet
        Dim dtMasterImport As DataTable = New DataTable

        ErrLoc = ".ImportMaster " & TableName
        progressBar.Value = progressBar.Value + 10

        '********************
        'Prepare Insert Statement 
        '********************
        Try

            lblMessage.Text = "Reading " & TableName & ""
            Application.DoEvents()

            Select Case TableName.ToUpper.Trim
                Case "SEP_LOGIN_V"
                    dsSet = New DataSet
                    dsSet = OpenRecordset("SELECT LOGIN_ID FROM [" & TableName & "]")
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL("DELETE FROM [" & TableName & "]")
                        'ExecuteSQL("ALTER TABLE [" & TableName & "] ALTER COLUMN ID IDENTITY (1,1)")
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("LOGIN_ID, PASSWORD", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then

                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = "INSERT INTO [" & TableName & "] (LOGIN_ID, PASSWORD) "
                            sSQL = sSQL & " VALUES(" & SQLQuote(dtMasterImport.Rows(i).Item("LOGIN_ID").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("PASSWORD").ToString)
                            sSQL = sSQL & ")"
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
                                ImpUser = False
                                If ImpUser Then
                                    sUser = "N"
                                    sSQL = Nothing
                                    sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sUser.Trim) & " WHERE SettingCode = 'USER'"
                                    ExecuteSQL(sSQL)
                                End If
                                Exit Function
                            Else
                                ImpUser = True
                            End If
                            'progressBar.Value = progressBar.Value + 2
                        Next
                        If ImpUser = True Then
                            sUser = "Y"
                            sSQL = Nothing
                            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sUser.Trim) & " WHERE SettingCode = 'USER'"
                            ExecuteSQL(sSQL)
                        End If
                        'progressBar.Maximum = dtMasterImport.Rows.Count
                    End If
                    'progressBar.Value = progressBar.Maximum

                Case "JSP_ORGANIZATION_HEADERS_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset("SELECT ORG_ID FROM [" & TableName & "]")
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL("DELETE FROM [" & TableName & "]")
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("ORG_ID, ORG_NAME", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = "INSERT INTO [" & TableName & "] (ORG_ID, ORG_NAME) "
                            sSQL = sSQL & " VALUES (" & SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_NAME").ToString)
                            sSQL = sSQL & ")"
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
                                ImpOrganization = False
                                If ImpOrganization Then
                                    sOrganization = "N"
                                    sSQL = Nothing
                                    sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sOrganization.Trim) & " WHERE SettingCode = 'ORGANIZATION'"
                                End If
                                Exit Function
                            Else
                                ImpOrganization = True
                            End If
                            'progressBar.Value = progressBar.Value + 2
                        Next
                        If ImpOrganization = True Then
                            sOrganization = "Y"
                            sSQL = Nothing
                            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sOrganization.Trim) & " WHERE SettingCode = 'ORGANIZATION'"
                            ExecuteSQL(sSQL)
                        End If
                        'progressBar.Maximum = dtMasterImport.Rows.Count
                    End If
                    'progressBar.Value = progressBar.Maximum

                Case "JSP_SUPPLY_BP_HEADERS_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset("SELECT SHOP_ID FROM [" & TableName & "]")
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL("DELETE FROM [" & TableName & "]")
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("SHOP_ID, SHOP_NAME, ORG_ID", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = "INSERT INTO [" & TableName & "] (SHOP_ID, SHOP_NAME, ORG_ID) "
                            sSQL = sSQL & " VALUES (" & SQLQuote(dtMasterImport.Rows(i).Item("SHOP_ID").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("SHOP_NAME").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString)
                            sSQL = sSQL & ")"
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
                                ImpShop = False
                                If ImpShop Then
                                    sShop = "N"
                                    sSQL = Nothing
                                    sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sShop.Trim) & " WHERE SettingCode = 'SHOP'"
                                End If
                                Exit Function
                            Else
                                ImpShop = True
                            End If
                            'progressBar.Value = progressBar.Value + 2
                        Next
                        If ImpShop = True Then
                            sShop = "Y"
                            sSQL = Nothing
                            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sShop.Trim) & " WHERE SettingCode = 'SHOP'"
                            ExecuteSQL(sSQL)
                        End If
                        'progressBar.Maximum = dtMasterImport.Rows.Count
                    End If
                    'progressBar.Value = progressBar.Maximum

                Case "JSP_SUPPLY_CP_HEADERS_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset("SELECT VENDOR_ID FROM [" & TableName & "]")
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL("DELETE FROM [" & TableName & "]")
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("VENDOR_ID, VENDOR_NAME, ORG_ID", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = "INSERT INTO [" & TableName & "] (VENDOR_ID, VENDOR_NAME, ORG_ID) "
                            sSQL = sSQL & " VALUES (" & SQLQuote(dtMasterImport.Rows(i).Item("VENDOR_ID").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("VENDOR_NAME").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString)
                            sSQL = sSQL & ")"
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
                                ImpSupplier = False
                                If ImpSupplier Then
                                    sSupply = "N"
                                    sSQL = Nothing
                                    sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sSupply.Trim) & " WHERE SettingCode = 'SUPPLIER'"
                                End If
                                Exit Function
                            Else
                                ImpSupplier = True
                            End If
                            'progressBar.Value = progressBar.Value + 2
                        Next
                        If ImpSupplier = True Then
                            sSupply = "Y"
                            sSQL = Nothing
                            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sSupply.Trim) & " WHERE SettingCode = 'SUPPLIER'"
                            ExecuteSQL(sSQL)
                        End If
                        'progressBar.Maximum = dtMasterImport.Rows.Count
                    End If
                    'progressBar.Value = progressBar.Maximum

                Case "JSP_ABNORMAL_REASON_CODE_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset("SELECT REASON_CODE FROM [" & TableName & "]")
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL("DELETE FROM [" & TableName & "]")
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("REASON_CODE, REASON, ORG_ID", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = "INSERT INTO [" & TableName & "] (REASON_CODE, REASON, ORG_ID) "
                            sSQL = sSQL & " VALUES (" & SQLQuote(dtMasterImport.Rows(i).Item("REASON_CODE").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("REASON").ToString) & " , "
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString)
                            sSQL = sSQL & ")"
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox("Failed Import To " & TableName, MsgBoxStyle.Critical, "Import")
                                ImpReason = False
                                If ImpReason Then
                                    sReason = "N"
                                    sSQL = Nothing
                                    sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sReason.Trim) & " WHERE SettingCode = 'REASON'"
                                End If
                                Exit Function
                            Else
                                ImpReason = True
                            End If
                            'progressBar.Value = progressBar.Value + 2
                        Next
                        ImpReason = True
                        If ImpReason Then
                            sReason = "Y"
                            sSQL = Nothing
                            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sReason.Trim) & " WHERE SettingCode = 'REASON'"
                            ExecuteSQL(sSQL)
                        End If
                        'progressBar.Maximum = dtMasterImport.Rows.Count
                    End If
                    'progressBar.Value = progressBar.Maximum

            End Select

            blnResult = True
            Call ShowWait(False)

        Catch ex As Exception
            UnHandledError(ex.Message, ErrLoc)
            Return False
        End Try

        GC.Collect()
        GC.WaitForPendingFinalizers()
        Return blnResult

        progressBar.Value = progressBar.Maximum
        Call ShowWait(False)
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
            serialNumber = "73E65B760649010800150D3072360605"

            If serialNumber <> "" And serialNumber <> Nothing Then
                ws_dcsClient.Url = "http://192.168.170.169:8084/DCSWebService.svc"

                ws_dcsClient.isConnected()
                ws_dcsClient.isOracleConnected()

                dtScnNum = ws_dcsClient.getData(" SCANNER_NUM ", TableName, " AND SERIAL_NO = " & SQLQuote(serialNumber))
                If dtScnNum.Rows.Count > 0 Then
                    'get the scanner number
                    scnNumber = Val(dtScnNum.Rows(0).Item("SCANNER_NUM").ToString)

                    'set to local setting table
                    If scnNumber > 0 Then
                        strSQL = "UPDATE TblSetting SET SettingValue=" & scnNumber & " WHERE settingCategory='BATCHID' AND settingCode='SCN_NO'"
                        If Not ExecuteSQL(strSQL) Then
                            MsgBox("Fail to update Scanner Number!", MsgBoxStyle.Critical, gAppName)
                            Return False
                        Else
                            Dim scanner_id As String = gScnPrefix & scnNumber.ToString.PadLeft(2, "0") & gScnSuffix
                            strSQL = "UPDATE TblSetting SET SettingValue=" & SQLQuote(scanner_id) & " WHERE settingCategory='SCN' AND settingCode='SCNID'"
                            If Not ExecuteSQL(strSQL) Then
                                MsgBox("Fail to update Scanner ID!", MsgBoxStyle.Critical, gAppName)
                                Return False
                            Else
                                setDeviceName(scanner_id)
                            End If
                        End If
                    Else
                        MsgBox("The reference value is incorrect!", MsgBoxStyle.Critical, gAppName)
                        Return False
                    End If

                Else
                    'get next scanner number
                    dtScnNum.Clear()
                    dtScnNum = ws_dcsClient.getData(" SCANNER_NUM ", TableName, " ORDER BY SCANNER_NUM DESC")
                    If dtScnNum.Rows.Count > 0 Then
                        If Not String.IsNullOrEmpty(dtScnNum.Rows(0).Item("SCANNER_NUM")) Then
                            scnNumber = Val(dtScnNum.Rows(0).Item("SCANNER_NUM").ToString) + 1
                        Else
                            scnNumber = 1
                        End If
                    Else
                        scnNumber = 1
                    End If

                    'insert into table
                    Dim insertSQL As String = "INSERT INTO " & TableName & " " _
                    & "(SERIAL_NO ,SCANNER_NUM ) " _
                    & " VALUES (" & SQLQuote(serialNumber) & " , " _
                    & " " & scnNumber & ")"
                    If Not ws_dcsClient.insertData(insertSQL) Then
                        MsgBox("Fail to register Scanner!", MsgBoxStyle.Critical, gAppName)
                    End If

                    'set to local setting table
                    strSQL = "UPDATE TblSetting SET SettingValue=" & scnNumber & " WHERE settingCategory='BATCHID' AND settingCode='SCN_NO'"
                    If Not ExecuteSQL(strSQL) Then
                        MsgBox("Fail to update Scanner Number!", MsgBoxStyle.Critical, gAppName)
                        Return False
                    Else
                        Dim scanner_id As String = gScnPrefix & scnNumber.ToString.PadLeft(2, "0") & gScnSuffix
                        strSQL = "UPDATE TblSetting SET SettingValue=" & SQLQuote(scanner_id) & " WHERE settingCategory='SCN' AND settingCode='SCNID'"
                        If Not ExecuteSQL(strSQL) Then
                            MsgBox("Fail to update Scanner ID!", MsgBoxStyle.Critical, gAppName)
                            Return False
                        Else
                            setDeviceName(scanner_id)
                        End If
                    End If

                End If

            Else
                MsgBox("Error getting device's serial number!", MsgBoxStyle.Critical, gAppName)
                Return False
            End If

            Return True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
            Return False
        End Try
    End Function

#End Region

End Class
