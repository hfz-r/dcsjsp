Imports System.Data
Imports System.Data.SqlServerCe
Imports System.Data.Common
Imports System.Reflection
Imports System.Globalization
Imports System.Net

Module SQLCEFunction

#Region ". Variable Declaration ."
    Public objConn As New SqlCeConnection
    Public mydate As String = Now.ToString("HH:mm:ss")
    Public SelectedData As String
    Public gTrans As SqlCeTransaction = Nothing
    Private progressbar As ProgressBar

#End Region

#Region ". Checking and Create of SQL CE Database ."
    Public Function DatabaseInit(ByRef prgProgress As ProgressBar) As Boolean

        Dim clsDataTransfer As New clsDataTransfer
        Dim ErrLoc As String = ".DatabaseInit"
        Dim SQLEngine As SqlCeEngine

        progressbar = prgProgress
        progressbar.Value = 0
        prgProgress.Maximum = 50

        ConnStr = "data source=" & gDBPath + gDatabaseName

        Try
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
            End If

            If Not System.IO.File.Exists(gDBPath + gDatabaseName) Then
                Application.DoEvents()

                SQLEngine = New SqlCeEngine(ConnStr)
                SQLEngine.CreateDatabase()
                SQLEngine.Dispose()
                Call CreateMasterTable()
            End If
            progressbar.Value = prgProgress.Value + 5

        Catch ex As Exception
            'File still use
            progressbar.Value = prgProgress.Value + 5
        End Try

        progressbar.Value = prgProgress.Maximum

    End Function

    Public Function GetServerDateTime() As String
        Dim gDay As String = ""
        Try
            If ws_dcsClient.isConnected() Then
                gDay = WeekdayName(Weekday(ws_dcsClient.getTime())) 'Error
            Else
                gDay = "Monday"
            End If
        Catch ex As Exception
            gDay = "Monday"
        End Try

        Return gDay
    End Function

    Public Function DownloadMasterData(ByRef prgProgress As ProgressBar) As Boolean

        Dim clsDataTransfer As New clsDataTransfer
        Dim status As Boolean = True

        progressbar = prgProgress
        progressbar.Value = 0
        prgProgress.Maximum = 50

        ConnStr = "data source=" & gDBPath + gDatabaseName

        Try
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
            End If

            If System.IO.File.Exists(gDBPath + gDatabaseName) Then
                Application.DoEvents()

                clsDataTransfer.ImportMasterData(TblUserDb)
                sUser = "Y"

                clsDataTransfer.ImportMasterData(TblJSPOrganizationDb)
                sOrganization = "Y"

                clsDataTransfer.ImportMasterData(TblJSPAbnormalReasonCodeDb)
                sReason = "Y"

                clsDataTransfer.ImportMasterData(TblJSPSupplyBPHeaderDb)
                sShop = "Y"

                clsDataTransfer.ImportMasterData(TblJSPSupplyCPHeaderDb)
                sSupply = "Y"

            End If
            progressbar.Value = prgProgress.Value + 20

        Catch ex As Exception
            progressbar.Value = prgProgress.Value + 20
        End Try

        progressbar.Value = prgProgress.Maximum
        Return status

    End Function

    Private Sub UpdateTBLSettingOnSchedule(ByVal YesNo As String, ByVal SettingCode As String)
        Dim sSQL As String = String.Format("UPDATE TBLSetting SET = 'Y' WHERE SettingCode = '{0}'", SettingCode)
        ExecuteSQL(sSQL)
    End Sub

#End Region

#Region ". SQL Create Table Statement ."

    Public Function CreateMasterTable() As Boolean

        CreateMasterTable = False
        Dim ErrLoc As String = ".CreateMasterTable"
        Dim SQL As System.Text.StringBuilder
        Dim objCon As New SqlCeConnection
        Dim sSQL As String = Nothing

        Try
            If Not TblUserDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblUserDb))
                SQL.Append("LOGIN_ID NVARCHAR(100) ")
                SQL.Append(",PASSWORD NVARCHAR(100) ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox(String.Format("Failed to create table {0}", TblUserDb), MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If
            If Not TblJSPOrganizationDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPOrganizationDb))
                SQL.Append("ORG_ID INTEGER ")
                SQL.Append(",ORG_NAME NVARCHAR(100) ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox(String.Format("Failed to create table {0}", TblJSPOrganizationDb), MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblJSPSupplyBPHeaderDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPSupplyBPHeaderDb))
                SQL.Append("SHOP_ID INTEGER ")
                SQL.Append(",SHOP_NAME NVARCHAR(20) ")
                SQL.Append(",ORG_ID INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox(String.Format("Failed to create table {0}", TblJSPSupplyBPHeaderDb), MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblJSPSupplyCPHeaderDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPSupplyCPHeaderDb))
                SQL.Append("VENDOR_ID INTEGER ")
                SQL.Append(",VENDOR_NAME NVARCHAR(100) ")
                SQL.Append(",ORG_ID INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox(String.Format("Failed to create table {0}", TblJSPSupplyCPHeaderDb), MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblJSPAbnormalReasonCodeDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPAbnormalReasonCodeDb))
                SQL.Append("REASON_CODE INTEGER ")
                SQL.Append(",REASON NVARCHAR(100) ")
                SQL.Append(",ORG_ID INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox(String.Format("Failed to create table {0}", TblJSPAbnormalReasonCodeDb), MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblSettingDb.Trim.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblSettingDb))
                SQL.Append("SettingCategory NVARCHAR(20) ")
                SQL.Append(",SettingCode NVARCHAR(50) ")
                SQL.Append(",SettingValue NVARCHAR(200) ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table setting!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & "VALUES ('SCN','SCNID'," & SQLQuote(gScannerID) & ")"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert scanner id!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & "VALUES ('SCN','PREFIX'," & SQLQuote("SCN") & ")"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert scanner id!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & "VALUES ('SCN','SUFFIX'," & SQLQuote("") & ")"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert scanner id!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('AUTH','AUTPWD','1111') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert password!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('AUTH','AUTUSERID','user') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert password!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('WS','URLDCSJSP','" & gStrDCSWebServiceURL & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert url DCS WebService!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('WS','URLORACLECHCK','" & gStrOracleChckWebServiceURL & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert url Oracle WebService!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('WS','URLORACLECP','" & gStrOracleCpWebServiceURL & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert url Oracle WebService!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('WS','URLORAUSERID','" & gStrOraUserID & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert User ID for Oracle WebService!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('WS','URLORAUSERPWD','" & gStrOraUserPwd & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert User Password for Oracle WebService!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('DB','DBPATH','" + gDBPath + "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert database path!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('DB','DBNAME','" & gDatabaseName & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert database name!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('DB','DBPASSWORD','" & gDatabasePwd & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert database password!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('IMPORT','SCHEDULE','" + GetServerDateTime() + "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import schedule!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('IMPORT','USER','N') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import user master data!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('IMPORT','REASON','N') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import reason master data!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('IMPORT','SUPPLIER','N')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import supplier master data!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('IMPORT','SHOP','N')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import shop master data!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('IMPORT','ORGANIZATION','N')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import organization master data!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('CHKIMPORT','FIRSTTIME','Y')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert chkimport First Time!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('ORGANIZATION','ORG_ID', '')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert chkimport First Time!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('BATCHID','SCN_NO',null)"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import Scanner No!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('CHK_ONLINE','INTERVAL','300000')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert time interval!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblTxnCodeDb.Trim.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblTxnCodeDb))
                SQL.Append("TXN_CODE NVARCHAR(3) ")
                SQL.Append(",TXN_VALUE NVARCHAR(2) ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create transaction code table!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TblTxnCode (TXN_CODE, TXN_VALUE) " _
                & "VALUES ('JSP','01')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert transaction code!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TblTxnCode (TXN_CODE, TXN_VALUE) " _
                & "VALUES ('MSP','02')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert transaction code!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not "TblJSPSupplyInterface" = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPSupplyInterface))
                SQL.Append("RCV_INTERFACE_ID INT")
                SQL.Append(",RCV_INTERFACE_BATCH_ID NVARCHAR(30) ")
                SQL.Append(",MODULE_ID INT ")
                SQL.Append(",MODULE_NO NVARCHAR(20) ")
                SQL.Append(",PXP_PART_ID INT ")
                SQL.Append(",PXP_PART_NO NVARCHAR(20) ")
                SQL.Append(",PXP_PART_NO_SFX NVARCHAR(2) ")
                SQL.Append(",PXP_PART_SEQ_NO NVARCHAR(5) ")
                SQL.Append(",QTY_BOX INT ")
                SQL.Append(",MANUFACTURE_CODE NVARCHAR(2) ")
                SQL.Append(",SUPPLIER_CODE NVARCHAR(4) ")
                SQL.Append(",SUPPLIER_PLANT_CODE NVARCHAR(1) ")
                SQL.Append(",SUPPLIER_SHIPPING_DOCK NVARCHAR(3) ")
                SQL.Append(",BEFORE_PACKING_ROUTING NVARCHAR(6) ")
                SQL.Append(",RECEIVING_COMPANY_CODE NVARCHAR(4) ")
                SQL.Append(",RECEIVING_PLANT_CODE NVARCHAR(1) ")
                SQL.Append(",RECEIVING_DOCK_CODE NVARCHAR(2) ")
                SQL.Append(",PACKING_ROUTING_CODE NVARCHAR(6) ")
                SQL.Append(",GRANTER_CODE NVARCHAR(4) ")
                SQL.Append(",ORDER_TYPE NVARCHAR(1) ")
                SQL.Append(",KANBAN_CLASSIFICATION NVARCHAR(1) ")
                SQL.Append(",DELIVERY_CODE NVARCHAR(2) ")
                SQL.Append(",MROS NVARCHAR(2) ")
                SQL.Append(",ORDER_NO NVARCHAR(12) ")
                SQL.Append(",DELIVERY_NO NVARCHAR(5) ")
                SQL.Append(",BACK_NUMBER NVARCHAR(4) ")
                SQL.Append(",RUNOUT_FLAG NVARCHAR(1) ")
                SQL.Append(",BOX_TYPE NVARCHAR(8) ")
                SQL.Append(",BRANCH_NO NVARCHAR(4) ")
                SQL.Append(",ADDRESS NVARCHAR(10) ")
                SQL.Append(",PACKING_DATE NVARCHAR(8) ")
                SQL.Append(",KATASHIKI_JERSEY_NO NVARCHAR(3) ")
                SQL.Append(",LOT_NO NVARCHAR(4) ")
                SQL.Append(",MODULE_CATEGORY NVARCHAR(2) ")
                SQL.Append(",PART_BRANCH_NO NVARCHAR(2) ")
                SQL.Append(",DUMMY NVARCHAR(20) ")
                SQL.Append(",VERSION_NO NVARCHAR(1) ")
                SQL.Append(",PDIO_ID INT ")
                SQL.Append(",PDIO_NO NVARCHAR(20) ")
                SQL.Append(",DOCK_CODE NVARCHAR(10) ")
                SQL.Append(",PDIO_ORDER_TYPE NVARCHAR(20) ")
                SQL.Append(",VENDOR_ID INT ")
                SQL.Append(",TRANSPORTER_ID INT ")
                SQL.Append(",LANE_ID INT ")
                SQL.Append(",TIER INT ")
                SQL.Append(",BACK_NO NVARCHAR(20)")
                SQL.Append(",P2_PART_NO NVARCHAR(20) ")
                SQL.Append(",P2_PART_SEQ_NO INT ")
                SQL.Append(",ORG_ID INT ")
                SQL.Append(",SCANNER_BATCH_ID NVARCHAR(20) ")
                SQL.Append(",SCANNER_HT_ID NVARCHAR(20) ")
                SQL.Append(",PROCESS_DATE DATETIME ")
                SQL.Append(",PROCESS_FLAG NVARCHAR(10) ")
                SQL.Append(",CREATED_BY NVARCHAR(20) ")
                SQL.Append(",CREATED_DATE DATETIME ")
                SQL.Append(",UPDATED_BY NVARCHAR(20) ")
                SQL.Append(",UPDATED_DATE DATETIME ")
                SQL.Append(",ERROR_MSG NVARCHAR(700) ")
                SQL.Append(",POST_DATE DATETIME ")
                SQL.Append(",DELIVERY_DATE DATETIME ")
                SQL.Append(",ON_OFF_LINE_FLAG NVARCHAR(5) ")
                SQL.Append(",SCAN_DATE DATETIME ")
                SQL.Append(",FORCE_PDIO_STATUS NVARCHAR(1) ")
                SQL.Append(",FORCE_PDIO_REASON_ID INT ")
                SQL.Append(",FORCE_PXP_STATUS NVARCHAR(1) ")
                SQL.Append(",FORCE_PXP_REASON_ID INT ")
                SQL.Append(",SCANNER_SCREEN_CODE NVARCHAR(2) ")
                SQL.Append(",FORCE_P2_STATUS NVARCHAR(1) ")
                SQL.Append(",FORCE_P2_REASON_ID INT ")
                SQL.Append(",SUPPLY_BY NVARCHAR(20) ")
                SQL.Append(",SUPPLY_DATE DATETIME ")
                SQL.Append(",SHOP_ID INT ")
                SQL.Append(",FORCE_MODULE_STATUS NVARCHAR(1) ")
                SQL.Append(",FORCE_MODULE_REASON_ID INT ")
                SQL.Append(",PART_NO NVARCHAR(20) ")
                SQL.Append(",SEQNO_KEY INT ")
                SQL.Append(",DELIVERY_TYPE INT ")
                SQL.Append(",PRODUCTION_DATE DATETIME ")
                SQL.Append(",EXPORTER_CODE INT ")
                SQL.Append(",PROD_LINE NVARCHAR(10) ")
                SQL.Append(",CYCLE INT ")
                SQL.Append(",ROUTE NVARCHAR(30) ")
                SQL.Append(",TOTAL_BOX INT ")
                SQL.Append(",DELIVERY_TYPE2 INT ")
                SQL.Append(",RETURN_VAL NVARCHAR(2) ")
                SQL.Append(",POSTED NVARCHAR(1) ")
                SQL.Append(",QTY_ORDER INT ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP Supply Interface!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "CREATE INDEX idxJSP_SUPPLY_INTERFACE ON JSP_SUPPLY_INTERFACE(PART_NO ASC, PXP_PART_NO ASC)"
                ExecuteSQL(sSQL)

                progressbar.Value = progressbar.Value + 2
            End If

            If Not "TblJSPRobbingInterface" = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPRobbingInterface))
                SQL.Append("RCV_INTERFACE_ID INT")
                SQL.Append(",RCV_INTERFACE_BATCH_ID NVARCHAR(30) ")
                SQL.Append(",MODULE_ID INT ")
                SQL.Append(",MODULE_NO NVARCHAR(20) ")
                SQL.Append(",PART_ID INT ")
                SQL.Append(",PART_NO NVARCHAR(20) ")
                SQL.Append(",QTY_BOX INT ")
                SQL.Append(",PXP_PART_SEQ_NO NVARCHAR(5) ")
                SQL.Append(",MANUFACTURE_CODE NVARCHAR(2) ")
                SQL.Append(",SUPPLIER_CODE NVARCHAR(4) ")
                SQL.Append(",SUPPLIER_PLANT_CODE NVARCHAR(1) ")
                SQL.Append(",SUPPLIER_SHIPPING_DOCK NVARCHAR(3) ")
                SQL.Append(",BEFORE_PACKING_ROUTING NVARCHAR(6) ")
                SQL.Append(",RECEIVING_COMPANY_CODE NVARCHAR(4) ")
                SQL.Append(",RECEIVING_PLANT_CODE NVARCHAR(1) ")
                SQL.Append(",RECEIVING_DOCK_CODE NVARCHAR(2) ")
                SQL.Append(",PACKING_ROUTING_CODE NVARCHAR(6) ")
                SQL.Append(",GRANTER_CODE NVARCHAR(4) ")
                SQL.Append(",ORDER_TYPE NVARCHAR(1) ")
                SQL.Append(",KANBAN_CLASSIFICATION NVARCHAR(1) ")
                SQL.Append(",DELIVERY_CODE NVARCHAR(2) ")
                SQL.Append(",MROS NVARCHAR(2) ")
                SQL.Append(",ORDER_NO NVARCHAR(12) ")
                SQL.Append(",DELIVERY_NO NVARCHAR(5) ")
                SQL.Append(",BACK_NUMBER NVARCHAR(4) ")
                SQL.Append(",PXP_PART_NO_SFX NVARCHAR(2) ")
                SQL.Append(",RUNOUT_FLAG NVARCHAR(1) ")
                SQL.Append(",BOX_TYPE NVARCHAR(8) ")
                SQL.Append(",BRANCH_NO NVARCHAR(4) ")
                SQL.Append(",ADDRESS NVARCHAR(10) ")
                SQL.Append(",PACKING_DATE NVARCHAR(8) ")
                SQL.Append(",KATASHIKI_JERSEY_NO NVARCHAR(3) ")
                SQL.Append(",LOT_NO NVARCHAR(4) ")
                SQL.Append(",MODULE_CATEGORY NVARCHAR(2) ")
                SQL.Append(",PART_BRANCH_NO NVARCHAR(2) ")
                SQL.Append(",DUMMY NVARCHAR(20) ")
                SQL.Append(",VERSION_NO NVARCHAR(1) ")
                SQL.Append(",ORG_ID INT ")
                SQL.Append(",ROBBING_BY NVARCHAR(20) ")
                SQL.Append(",ROBBING_DATE DATETIME ")
                SQL.Append(",SCANNER_BATCH_ID NVARCHAR(20) ")
                SQL.Append(",SCANNER_HT_ID NVARCHAR(20) ")
                SQL.Append(",PROCESS_DATE DATETIME ")
                SQL.Append(",PROCESS_FLAG NVARCHAR(10) ")
                SQL.Append(",UPDATED_BY NVARCHAR(20) ")
                SQL.Append(",UPDATED_DATE DATETIME ")
                SQL.Append(",ERROR_MSG NVARCHAR(700) ")
                SQL.Append(",POST_DATE DATETIME ")
                SQL.Append(",DELIVERY_DATE DATETIME ")
                SQL.Append(",ON_OFF_LINE_FLAG NVARCHAR(5) ")
                SQL.Append(",SCAN_DATE DATETIME ")
                SQL.Append(",PXP_PART_NO NVARCHAR(10) ")
                SQL.Append(",FORCE_PXP_STATUS NVARCHAR(1) ")
                SQL.Append(",FORCE_PXP_REASON_ID INT ")
                SQL.Append(",SEQNO_KEY INT ")
                SQL.Append(",RETURN_VAL NVARCHAR(2) ")
                SQL.Append(",POSTED NVARCHAR(1) ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP Robbing Interface!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not "TblJSPSupplyPLDetailsView" = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPSupplyPLDetailsView))
                SQL.Append("PDIO_ID INT, ")
                SQL.Append("PDIO_NO NVARCHAR(30) NULL, ")
                SQL.Append("PART_NO NVARCHAR(20) NULL, ")
                SQL.Append("BACK_NUMBER NVARCHAR(10) NULL, ")
                SQL.Append("SEQ_NO INT NULL, ")
                SQL.Append("QTY_PER_BOX INT NULL, ")
                SQL.Append("TRANSACTION_CODE NVARCHAR(2) NULL, ")
                SQL.Append("ORG_ID NVARCHAR(8) NULL")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP Supply Pending View!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not "TblJSPSupplyPLPendingView" = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append(String.Format("CREATE TABLE [{0}] (", TblJSPSupplyPLPendingView))
                SQL.Append("PDIO_ID INT, ")
                SQL.Append("PDIO_NO NVARCHAR(30) NULL, ")
                SQL.Append("PART_NO NVARCHAR(20) NULL, ")
                SQL.Append("SEQ_NO INT NULL, ")
                SQL.Append("ADVICEQTY INT NULL, ")
                SQL.Append("TRANSACTION_CODE NVARCHAR(2) NULL, ")
                SQL.Append("BACK_NUMBER NVARCHAR(10) NULL, ")
                SQL.Append("ORG_ID INT NULL")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP Supply Pending View!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            'table JSP_CDIO_PENDING_DETAILS; use by CDIO
            'start
            If Not CDIO_PENDING = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + CDIO_PENDING + "] (")
                SQL.Append("ID INT IDENTITY,")
                SQL.Append("CDIO_ID NVARCHAR(10) NOT NULL,")
                SQL.Append("CDIO_NO NVARCHAR(20) NOT NULL,")
                SQL.Append("MODULE_ID INT NULL,")
                SQL.Append("MODULE_NO NVARCHAR(10) NULL,")
                SQL.Append("MODULE_NAME NVARCHAR(20) NULL,")
                SQL.Append("ORDER_NO NVARCHAR(12) NOT NULL,")
                SQL.Append("ORG_ID INT NOT NULL")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table " & CDIO_PENDING & "!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If
            'end

            'table JSP_CDIO_INTERFACE; use by CDIO
            'start
            If Not CDIO_INTERFACE = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + CDIO_INTERFACE + "] (")
                SQL.Append("RCV_INTERFACE_ID INT IDENTITY,")
                SQL.Append("RCV_INTERFACE_BATCH_ID NVARCHAR(30) NOT NULL,")
                SQL.Append("CDIO_ID NVARCHAR(10) NULL,")
                SQL.Append("CDIO_NO NVARCHAR(20) NULL,")
                SQL.Append("MODULE_ID INT NOT NULL,")
                SQL.Append("MODULE_NO NVARCHAR(10) NOT NULL,")
                SQL.Append("PILLING_ORDER NVARCHAR(1) NOT NULL,")
                SQL.Append("GROSS_WEIGHT INT NOT NULL,")
                SQL.Append("ORDER_NO NVARCHAR(12) NOT NULL,")
                SQL.Append("ORG_ID INT NOT NULL,")
                SQL.Append("RCV_BY NVARCHAR(20) NULL,")
                SQL.Append("RCV_DATE DATETIME NULL,")
                SQL.Append("SCANNER_BATCH_ID NVARCHAR(20) NULL,")
                SQL.Append("SCANNER_HT_ID NVARCHAR(20) NULL,")
                SQL.Append("PROCESS_DATE DATETIME NULL,")
                SQL.Append("PROCESS_FLAG NVARCHAR(10) NULL,")
                SQL.Append("CREATED_BY NVARCHAR(20) NOT NULL,")
                SQL.Append("CREATED_DATE DATETIME NOT NULL,")
                SQL.Append("UPDATED_BY NVARCHAR(20) NULL,")
                SQL.Append("UPDATED_DATE DATETIME NULL,")
                SQL.Append("ERROR_MSG NVARCHAR(700) NULL,")
                SQL.Append("POST_DATE DATETIME NULL,")
                SQL.Append("DELIVERY_DATE DATETIME NULL,")
                SQL.Append("ON_OFF_LINE_FLAG NVARCHAR(5) NULL,")
                SQL.Append("SCAN_DATE DATETIME NULL,")
                SQL.Append("FORCE_CDIO_STATUS NVARCHAR(1) NULL,")
                SQL.Append("FORCE_CDIO_REASON_ID INT NULL,")
                SQL.Append("FORCE_MODULE_STATUS NVARCHAR(1) NULL,")
                SQL.Append("FORCE_MODULE_REASON_ID INT NULL")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table " & CDIO_INTERFACE & "!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If
            'end

            'table TBLBatch; will replace TBLAutoNumber
            'start
            If Not TblBatch = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + TblBatch + "] (")
                SQL.Append("ID INT IDENTITY,")
                SQL.Append("CATEGORY NVARCHAR(50) NOT NULL,")
                SQL.Append("PREFIX NVARCHAR(30) NULL,")
                SQL.Append("SUFFIX NVARCHAR(20) NULL,")
                SQL.Append("CURRENT_NO NVARCHAR(14) NOT NULL,")
                SQL.Append("MODIFIED_DATETIME DATETIME NOT NULL,")
                SQL.Append("RESET_FLAG NVARCHAR(1) NULL")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox(String.Format("Failed to create table {0}!", TblBatch), MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                Dim dt As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd hh:mm:ss tt")
                Dim currentNo = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")

                Try
                    sSQL = "INSERT INTO " & TblBatch & "(CATEGORY, PREFIX, SUFFIX, CURRENT_NO, MODIFIED_DATETIME, RESET_FLAG) " _
                    & " VALUES ('CDIO','csssyyyyMMddHHmmss', NULL, " & SQLQuote(currentNo) & ", " & SQLQuote(dt) & ", NULL)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If

                    sSQL = "INSERT INTO " & TblBatch & "(CATEGORY, PREFIX, SUFFIX, CURRENT_NO, MODIFIED_DATETIME, RESET_FLAG) " _
                    & " VALUES ('UNPACK','csssyyyyMMddHHmmss', NULL, " & SQLQuote(currentNo) & ", " & SQLQuote(dt) & ", NULL)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If

                    sSQL = "INSERT INTO " & TblBatch & "(CATEGORY, PREFIX, SUFFIX, CURRENT_NO, MODIFIED_DATETIME, RESET_FLAG) " _
                    & " VALUES ('BIG_PART','csssyyyyMMddHHmmss', NULL, " & SQLQuote(currentNo) & ", " & SQLQuote(dt) & ", NULL)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If

                    sSQL = "INSERT INTO " & TblBatch & "(CATEGORY, PREFIX, SUFFIX, CURRENT_NO, MODIFIED_DATETIME, RESET_FLAG) " _
                    & " VALUES ('PROGRESS_LANE','csssyyyyMMddHHmmss', NULL, " & SQLQuote(currentNo) & ", " & SQLQuote(dt) & ", NULL)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If

                    sSQL = "INSERT INTO " & TblBatch & "(CATEGORY, PREFIX, SUFFIX, CURRENT_NO, MODIFIED_DATETIME, RESET_FLAG) " _
                    & " VALUES ('ROBBING','csssyyyyMMddHHmmss', NULL, " & SQLQuote(currentNo) & ", " & SQLQuote(dt) & ", NULL)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If

                    sSQL = "INSERT INTO " & TblBatch & "(CATEGORY, PREFIX, SUFFIX, CURRENT_NO, MODIFIED_DATETIME, RESET_FLAG) " _
                    & " VALUES ('CHILD_PART','csssyyyyMMddHHmmss', NULL, " & SQLQuote(currentNo) & ", " & SQLQuote(dt) & ", NULL)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If

                Catch ex As Exception
                    MsgBox(String.Format("Failed to insert data on {0}", TblBatch), MsgBoxStyle.Critical, "Import")
                    Exit Function
                End Try
                progressbar.Value = progressbar.Value + 2
            End If
            'end

            'table JSP_UNPACK_INTERFACE; use by UNPACK
            'start
            If Not UNPACK_INTERFACE = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + UNPACK_INTERFACE + "] (")
                SQL.Append("RCV_INTERFACE_ID INT IDENTITY")
                SQL.Append(",RCV_INTERFACE_BATCH_ID NVARCHAR(30) NOT NULL")
                SQL.Append(",MODULE_ID INT NULL")
                SQL.Append(",MODULE_NO NVARCHAR(20) NULL")
                SQL.Append(",PART_ID INT NULL")
                SQL.Append(",PART_NO NVARCHAR(20) NULL")
                SQL.Append(",QTY_BOX INT NULL")
                SQL.Append(",PXP_PART_SEQ_NO INT NULL")
                SQL.Append(",MANUFACTURE_CODE NVARCHAR(2) NULL")
                SQL.Append(",SUPPLIER_CODE NVARCHAR(4) NULL")
                SQL.Append(",SUPPLIER_PLANT_CODE NVARCHAR(1) NULL")
                SQL.Append(",SUPPLIER_SHIPPING_DOCK NVARCHAR(3) NULL")
                SQL.Append(",BEFORE_PACKING_ROUTING NVARCHAR(6) NULL")
                SQL.Append(",RECEIVING_COMPANY_CODE NVARCHAR(4) NULL")
                SQL.Append(",RECEIVING_PLANT_CODE NVARCHAR(1) NULL")
                SQL.Append(",RECEIVING_DOCK_CODE NVARCHAR(2) NULL")
                SQL.Append(",PACKING_ROUTING_CODE NVARCHAR(6) NULL")
                SQL.Append(",GRANTER_CODE NVARCHAR(4) NULL")
                SQL.Append(",ORDER_TYPE NVARCHAR(1) NULL")
                SQL.Append(",KANBAN_CLASSIFICATION NVARCHAR(1) NULL")
                SQL.Append(",DELIVERY_CODE NVARCHAR(2) NULL")
                SQL.Append(",MROS NVARCHAR(2) NULL")
                SQL.Append(",ORDER_NO NVARCHAR(12) NULL")
                SQL.Append(",DELIVERY_NO NVARCHAR(5) NULL")
                SQL.Append(",BACK_NUMBER NVARCHAR(4) NULL")
                SQL.Append(",PXP_PART_NO_SFX NVARCHAR(2) NULL")
                SQL.Append(",RUNOUT_FLAG NVARCHAR(1) NULL")
                SQL.Append(",BOX_TYPE NVARCHAR(8) NULL")
                SQL.Append(",BRANCH_NO NVARCHAR(4) NULL")
                SQL.Append(",ADDRESS NVARCHAR(10) NULL")
                SQL.Append(",PACKING_DATE NVARCHAR(8) NULL")
                SQL.Append(",KATASHIKI_JERSEY_NO NVARCHAR(3) NULL")
                SQL.Append(",LOT_NO NVARCHAR(4) NULL")
                SQL.Append(",MODULE_CATEGORY NVARCHAR(2) NULL")
                SQL.Append(",PART_BRANCH_NO NVARCHAR(2) NULL")
                SQL.Append(",DUMMY NVARCHAR(20) NULL")
                SQL.Append(",VERSION_NO NVARCHAR(2) NULL")
                SQL.Append(",ORG_ID INT NULL")
                SQL.Append(",UNPACK_BY NVARCHAR(20) NULL")
                SQL.Append(",UNPACK_DATE DATETIME NULL")
                SQL.Append(",SCANNER_BATCH_ID NVARCHAR(20) NULL")
                SQL.Append(",SCANNER_HT_ID NVARCHAR(20) NULL")
                SQL.Append(",PROCESS_DATE DATETIME NULL")
                SQL.Append(",PROCESS_FLAG NVARCHAR(10) NULL")
                SQL.Append(",CREATED_BY NVARCHAR(20) NULL")
                SQL.Append(",CREATED_DATE DATETIME NULL")
                SQL.Append(",UPDATED_BY NVARCHAR(20) NULL")
                SQL.Append(",UPDATED_DATE DATETIME NULL")
                SQL.Append(",ERROR_MSG NVARCHAR(700) NULL")
                SQL.Append(",POST_DATE DATETIME NULL")
                SQL.Append(",DELIVERY_DATE NVARCHAR(5) NULL")
                SQL.Append(",ON_OFF_LINE_FLAG NVARCHAR(5) NULL")
                SQL.Append(",SCAN_DATE DATETIME NULL")
                SQL.Append(",FORCE_MODULE_STATUS NVARCHAR(1) NULL")
                SQL.Append(",FORCE_MODULE_REASON_ID INT NULL")
                SQL.Append(",FORCE_PXP_STATUS NVARCHAR(1) NULL")
                SQL.Append(",FORCE_PXP_REASON_ID INT NULL")
                SQL.Append(",PXP_PART_NO NVARCHAR(10) NULL")
                SQL.Append(",SEQNO_KEY INT NULL")
                SQL.Append(",ROBB_MODULE_NO NVARCHAR(20) DEFAULT 'N'")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table " & UNPACK_INTERFACE & "!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                'Indexes - start
                Try
                    Dim dt As DataTable = getData("SELECT index_name FROM INFORMATION_SCHEMA.INDEXES WHERE index_name = 'IX_UNPACK_INTERFACE'")
                    If dt.Rows.Count > 0 Then
                        sSQL = "DROP INDEX JSP_UNPACK_INTERFACE.IX_UNPACK_INTERFACE"
                        If ExecuteSQL(sSQL) = False Then
                            Throw New Exception()
                        End If
                    End If

                    sSQL = "CREATE NONCLUSTERED INDEX IX_UNPACK_INTERFACE ON " & UNPACK_INTERFACE & " " + _
                           "(MODULE_NO, PART_NO, PXP_PART_SEQ_NO, PART_BRANCH_NO)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If
                Catch ex As Exception
                    MsgBox("Failed to insert data on " + UNPACK_INTERFACE + " with exception: " _
                           + ex.Message.ToString, MsgBoxStyle.Critical, "Import")
                    Exit Function
                End Try
                'Indexes - end
                progressbar.Value = progressbar.Value + 2
            End If
            'end

            'table JSP_UNPACK_PENDING; use by UNPACK
            'start
            If Not UNPACK_PENDING = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + UNPACK_PENDING + "] (")
                SQL.Append("ID INT IDENTITY,")
                SQL.Append("MODULE_ID INT,") ' ASSUME MODULE_ID IS GUID, OTHERWISE REMOVE ID IDENTITY
                SQL.Append("MODULE_NO NVARCHAR(20) NULL,")
                SQL.Append("PART_NO NVARCHAR(20) NULL,")
                SQL.Append("PART_NO_SFX NVARCHAR(2) NULL,")
                SQL.Append("PART_NAME NVARCHAR(50) NULL,")
                SQL.Append("QUANTITY_BOX INT NULL,")
                SQL.Append("PART_SEQ_NO INT NULL,")
                SQL.Append("BRANCH_NO NVARCHAR(42) NULL,")
                SQL.Append("ORG_ID INT NULL,")
                SQL.Append("ROBB_MODULE_ID INT DEFAULT 0,")
                SQL.Append("ROBB_MODULE_NO NVARCHAR(20) DEFAULT 'N'")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table " & UNPACK_PENDING & "!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                'Indexes - start
                Try
                    Dim dt As DataTable = getData("SELECT index_name FROM INFORMATION_SCHEMA.INDEXES WHERE index_name = 'IX_UNPACK_PEDING'")
                    If dt.Rows.Count > 0 Then
                        sSQL = "DROP INDEX JSP_UNPACK_PENDING.IX_UNPACK_PEDING"
                        If ExecuteSQL(sSQL) = False Then
                            Throw New Exception()
                        End If
                    End If

                    sSQL = "CREATE NONCLUSTERED INDEX IX_UNPACK_PEDING ON " & UNPACK_PENDING & " " + _
                           "(MODULE_NO, PART_NO, PART_SEQ_NO, BRANCH_NO)"
                    If ExecuteSQL(sSQL) = False Then
                        Throw New Exception()
                    End If
                Catch ex As Exception
                    MsgBox("Failed to insert data on " + UNPACK_PENDING + " with exception: " _
                           + ex.Message.ToString, MsgBoxStyle.Critical, "Import")
                    Exit Function
                End Try
                'Indexes - end
                progressbar.Value = progressbar.Value + 2
            End If
            'end

            CreateMasterTable = True
        Catch ex As WebException
            Throw ex
        Catch ex As Exception
            UnHandledError(ex.ToString(), ErrLoc)
        Finally
            objConn.Close()
            objConn.Dispose()
        End Try

        progressbar.Value = progressbar.Value + 1

    End Function

#End Region

#Region ". SQL CE Connection ."
    Public Function OpenConnection() As SqlCeConnection

        Dim objConnection As New SqlCeConnection

        If objConn Is Nothing Then
            objConnection.ConnectionString = ConnStr
            objConnection.Open()
            Return objConnection
        End If

        If objConn.State <> ConnectionState.Open Then
            objConnection.ConnectionString = ConnStr
            objConnection.Open()
            Return objConnection
        Else
            Return objConn
        End If

        objConnection.ConnectionString = ConnStr
        objConnection.Open()
        Return objConnection

    End Function
#End Region

#Region ". SQL UnHandle Error ."
    Public Sub UnHandledError(ByVal Exception As String, ByVal Location As String)
        MsgBox(Location & " - " & Exception.ToString, MsgBoxStyle.Critical)
    End Sub
#End Region

#Region ". SQL CE OpenRecordSet With SqlCeDataReader ."

    Public Function OpenRecordset(ByVal strSQL As String, ByRef ceConn As SqlCeConnection) As SqlCeDataReader

        Dim ErrLoc As String = ".OpenTable"
        Dim dbReader As SqlCeDataReader = Nothing
        Dim SqlCmd As SqlCeCommand = Nothing

        Try
            If objConn.State = ConnectionState.Closed Then
                objConn = OpenConnection()
            End If

            SqlCmd = New SqlCeCommand(strSQL, ceConn)
            dbReader = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

            'Return dbReader
        Catch ex As Exception
            UnHandledError(ex.ToString(), ErrLoc)
        Finally
            SqlCmd.Dispose()
            SqlCmd = Nothing
        End Try
        Return dbReader
    End Function
#End Region

#Region ". SQL CE OpenRecorSet With DataSet ."
    Public Function OpenRecordset(ByVal SQL As String) As DataSet

        Dim ErrLoc As String = ".OpenRecordSet"
        Dim ds As New DataSet
        Dim objcmd As SqlCeDataAdapter = Nothing

        Try
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
            End If

            objConn = OpenConnection()
            objcmd = New SqlCeDataAdapter(SQL, objConn)

            objcmd.Fill(ds)
            Return ds
        Catch ex As Exception
            UnHandledError(ex.ToString(), ErrLoc)
        Finally
            objcmd.Dispose()
            objConn.Close()
        End Try
        Return ds
    End Function
#End Region

#Region ". Execute SQL With Boolean Connection ."
    Private Function ExecuteSQL(ByVal SQL As String, ByVal objConn As SqlCeConnection, _
      Optional ByVal blnShowMessage As Boolean = True) As Boolean

        Dim ErrLoc As String = ".Execute SQL, Connection, Boolean"
        Dim objCmd As SqlCeCommand = Nothing

        Try
            If objConn.State = ConnectionState.Closed Then
                objConn = OpenConnection()
            End If

            objCmd = New SqlCeCommand(SQL, objConn)
            objCmd.CommandType = CommandType.Text
            objCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            If blnShowMessage Then UnHandledError(ex.ToString() + vbCr + SQL, ErrLoc)
            Return False
        Finally
            objCmd.Dispose()
            objCmd = Nothing
        End Try

    End Function
#End Region

#Region ". Execute SQL String ."
    Public Function ExecuteSQL(ByVal SQL As String) As Boolean
        Dim ErrLoc As String = ".Execute SQL, String"
        Dim objCmd As SqlCeCommand = Nothing

        Try
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
            End If

            objConn = OpenConnection()

            objCmd = New SqlCeCommand(SQL, objConn)
            objCmd.CommandType = CommandType.Text
            objCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            UnHandledError(ex.ToString() + vbCr + SQL, ErrLoc)
            Return False
        Finally
            objCmd.Dispose()
            objConn.Close()
        End Try

    End Function
#End Region

#Region ". SQL Drop Table Statement ."
    Public Sub DropAllTable()

        Dim ErrLoc As String = ".Drop Table"
        Dim objCon As New SqlCeConnection

        Try
            If TblUserDb <> "" Then
                ExecuteSQL("Drop Table " & TblUserDb, objCon, CType(DroptableValue, Boolean))
            End If

            If TblSettingDb <> "" Then
                ExecuteSQL("Drop Table " & TblSettingDb, objCon, CType(DroptableValue, Boolean))
            End If

            If TblJSPOrganizationDb <> "" Then
                ExecuteSQL("Drop Table " & TblJSPOrganizationDb, objCon, CType(DroptableValue, Boolean))
            End If

            If TblJSPSupplyBPHeaderDb <> "" Then
                ExecuteSQL("Drop Table " & TblJSPSupplyBPHeaderDb, objCon, CType(DroptableValue, Boolean))
            End If

            If TblJSPSupplyCPHeaderDb <> "" Then
                ExecuteSQL("Drop Table " & TblJSPSupplyCPHeaderDb, objCon, CType(DroptableValue, Boolean))
            End If
        Catch ex As Exception
            UnHandledError(ex.ToString(), ErrLoc)
        Finally
            objConn.Close()
            objConn.Dispose()
        End Try
    End Sub
#End Region

#Region ". Encryption  and Decryption ."
    Public Function GetEncrypt(ByVal sSource As String) As String
        Dim sTemp As String
        Dim lStr As Long
        Dim i As Long
        Dim S As String

        On Error Resume Next
        GetEncrypt = ""
        lStr = Len(sSource)
        sTemp = ""

        For i = 1 To lStr
            ' convert to Hex
            S = Mid(sSource, CType(i, Integer), 1)
            S = Chr(CType(Asc(S) Xor (i Mod 30) * 8, Integer))
            S = Hex(Asc(S))

            If Len(S) = 1 Then S = "0" & S ' for single char hex code add '0' infront
            sTemp = sTemp & S
        Next

        sTemp = StrReverse(sTemp)
        GetEncrypt = sTemp
    End Function
#End Region

#Region ". Compact SQL CE Database ."
    Public Function CompactCE() As Boolean

        If System.IO.File.Exists(gDBPath + gDatabaseName) Then
            Dim CmdLog As SqlCeCommand
            Dim BackupSDF As String = gDBPath + "temp.sdf.temp"
            Dim EngineCE As New SqlCeEngine("Data Source = " & gDBPath & gDatabaseName)

            EngineCE.Compact("Data Source = " & BackupSDF)
            EngineCE.Dispose()

            System.IO.File.Delete(gDBPath & gDatabaseName)
            System.IO.File.Move(BackupSDF, gDBPath & gDatabaseName)

            objConn = New SqlServerCe.SqlCeConnection("Data Source = " & gDBPath & gDatabaseName)
            objConn.Open()

            CmdLog = New SqlCeCommand
            CmdLog.Connection = objConn

        End If

    End Function
#End Region

#Region "Transaction"

    Public Sub BeginTransaction(ByRef tx As SqlCeTransaction)

        If objConn.State = ConnectionState.Open Then
            objConn.Close()
        End If

        objConn = OpenConnection()

        tx = objConn.BeginTransaction()
    End Sub

    Public Function ExecuteSQL_trans(ByVal SQL As String, ByRef tx As SqlCeTransaction) As Boolean
        Dim ErrLoc As String = ".Execute SQL, String"

        Dim gCmd As SqlCeCommand = objConn.CreateCommand()

        gCmd.Transaction = tx
        Try
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
            End If

            objConn = OpenConnection()

            gCmd = New SqlCeCommand(SQL, objConn)
            gCmd.CommandType = CommandType.Text
            gCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            UnHandledError(ex.ToString() + vbCr + SQL, ErrLoc)
            Return False
        End Try

    End Function
#End Region

End Module
