Imports System.Data
Imports System.Data.SqlServerCe
Imports System.Data.Common
Imports System.Reflection

Module SQLCEFunction

	Public objConn As New SqlCeConnection
    'Public ConnStr As String
	Public mydate As String = Now.ToString("HH:mm:ss")
    Public SelectedData As String

    Public gTrans As SqlCeTransaction = Nothing
    
	Private progressbar As ProgressBar


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
            'DropAllTable()
            progressbar.Value = prgProgress.Value + 5
        End Try

        'Call CompactCE()
        progressbar.Value = prgProgress.Maximum

    End Function

    Public Function GetServerDateTime() As String
        Dim gDay As String = ""
        Try
            If ws_dcsClient.isConnected() Then
                gDay = DateTime.Now.DayOfWeek.ToString()
                'Dim serverDate As String = ws_dcsClient.getTime()
                'gDay = WeekdayName(Weekday(serverDate)) 'Error
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
        Dim ErrLoc As String = ".DownloadMasterData"
        Dim status As Boolean = True
        Dim chkFirstTime As Boolean = False

        Dim sSQL As String = ""
        Dim dt As DataTable = New DataTable

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

                'clsDataTransfer.ImportMasterData(TblJSPUnpackInterfaceDb)
                'sUnpackInterface = "Y"

                'clsDataTransfer.ImportMasterData(TblJSPUnpackingDetailsDb)
                'sUnpackDetails = "Y"

                'clsDataTransfer.ImportMasterData(TblJSPUnpackPendingDb)
                'sUnpackPending = "y"

            End If
            progressbar.Value = prgProgress.Value + 20

        Catch ex As Exception
            'File still use
            'DropAllTable()
            progressbar.Value = prgProgress.Value + 20
        End Try

        'Call CompactCE()
        progressbar.Value = prgProgress.Maximum
        Return status

    End Function

    Private Sub UpdateTBLSettingOnSchedule(ByVal YesNo As String, ByVal SettingCode As String)
        Dim sSQL As String = "UPDATE TBLSetting SET = 'Y' WHERE SettingCode = '" + SettingCode + "'"
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
            If Not TblUserDb.Trim.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + TblUserDb + "] (")
                SQL.Append("LOGIN_ID NVARCHAR(20) ")
                SQL.Append(",PASSWORD NVARCHAR(20) ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table login!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblJSPOrganizationDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + TblJSPOrganizationDb + "] (")
                SQL.Append("ORG_ID INTEGER ")
                SQL.Append(",ORG_NAME NVARCHAR(100) ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP_Organization_Headers_View!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblJSPSupplyBPHeaderDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + TblJSPSupplyBPHeaderDb + "] (")
                SQL.Append("SHOP_ID INTEGER ")
                SQL.Append(",SHOP_NAME NVARCHAR(20) ")
                SQL.Append(",ORG_ID INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP_SUPPLY_BP_HEADERS_VIEW!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblJSPSupplyCPHeaderDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + TblJSPSupplyCPHeaderDb + "] (")
                SQL.Append("VENDOR_ID INTEGER ")
                SQL.Append(",VENDOR_NAME NVARCHAR(100) ")
                SQL.Append(",ORG_ID INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP_SUPPLY_CP_HEADERS_VIEW!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            If Not TblJSPAbnormalReasonCodeDb.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + TblJSPAbnormalReasonCodeDb + "] (")
                SQL.Append("REASON_CODE INTEGER ")
                SQL.Append(",REASON NVARCHAR(100) ")
                SQL.Append(",ORG_ID INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table JSP_ABNORMAL_REASON_CODE_VIEW!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            ' MERGE THIS TABLE INTO UNPACK FORM
            'If Not TblJSPUnpackInterfaceDb.Equals("") Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE [" + TblJSPUnpackInterfaceDb + "] (")
            '    SQL.Append("RCV_INTERFACE_ID INTEGER ")
            '    SQL.Append(",RCV_INTERFACE_BATCH_ID INTEGER ")
            '    SQL.Append(",MODULE_ID INTEGER ")
            '    SQL.Append(",MODULE_NO NVARCHAR(20) ")
            '    SQL.Append(",PART_ID INTEGER ")
            '    SQL.Append(",PXP_PART_NO NVARCHAR(10) ")
            '    SQL.Append(",QTY_BOX INTEGER ")
            '    SQL.Append(",PXP_PART_SEQ_NO INTEGER ")
            '    SQL.Append(",MANUFACTURE_CODE NVARCHARA(2) ")
            '    SQL.Append(",SUPPLIER_CODE NVARCHAR(4) ")
            '    SQL.Append(",SUPPLIER_PLANT_CODE NVARCHAR(1) ")
            '    SQL.Append(",SUPPLIER_SHIPPING_DOCK NVARCHAR(3) ")
            '    SQL.Append(",BEFORE_PACKING_ROUTING NVARCHAR(6) ")
            '    SQL.Append(",RECEIVING_COMPANY_CODE NVARCHAR(4) ")
            '    SQL.Append(",RECEIVING_PLANT_CODE NVARCHAR(1) ")
            '    SQL.Append(",RECEIVING_DOCK_CODE NVARCHAR(1) ")
            '    SQL.Append(",PACKING_ROUTING_CODE NVARCHAR(6) ")
            '    SQL.Append(",GRANTER_CODE NVARCHAR(4) ")
            '    SQL.Append(",ORDER_TYPE NVARCHAR(1) ")
            '    SQL.Append(",KANBAN_CLASSIFICATION NVARCHAR(1) ")
            '    SQL.Append(",DELIVERY_CODE NVARCHAR(2) ")
            '    SQL.Append(",MROS NVARCHAR(2) ")
            '    SQL.Append(",ORDER_NO NVARCHAR(12) ")
            '    SQL.Append(",DELIVERY_NO NVARCHAR(5) ")
            '    SQL.Append(",BACK_NUMBER NVARCHAR(4) ")
            '    SQL.Append(",PXP_PART_NO_SFX NVARCHAR(2) ")
            '    SQL.Append(",RUNOUT_FLAG NVARCHAR(1) ")
            '    SQL.Append(",BOX_TYPE NVARCHAR(8) ")
            '    SQL.Append(",BRANCH_NO NVARCHAR(4) ")
            '    SQL.Append(",ADDRESS NVARCHAR(10) ")
            '    SQL.Append(",PACKING_DATE NVARCHAR(8) ")
            '    SQL.Append(",KATASHIKI_JERSEY_NO NVARCHAR(3) ")
            '    SQL.Append(",LOT_NO NVARCHAR(4) ")
            '    SQL.Append(",MODULE_CATEGORY NVARCHAR(2) ")
            '    SQL.Append(",PART_BRANCH_NO NVARCHAR(2) ")
            '    SQL.Append(",DUMMY NVARCHAR(20) ")
            '    SQL.Append(",VERSION_NO NVARCHAR(2) ")
            '    SQL.Append(",ORG_ID INTEGER ")
            '    SQL.Append(",UNPACK_BY NVARCHAR(20) ")
            '    SQL.Append(",UNPACK_DATE DATETIME ")
            '    SQL.Append(",SCANNER_BATCH_ID NVARCHAR(20) ")
            '    SQL.Append(",SCANNER_HT_ID NVARCHAR(20) ")
            '    SQL.Append(",PROCESS_DATE DATETIME ")
            '    SQL.Append(",PROCESS_FLAG NVARCHAR(10) ")
            '    SQL.Append(",CREATED_BY NVARCHAR(20) ")
            '    SQL.Append(",CREATED_DATE DATETIME ")
            '    SQL.Append(",UPDATED_BY NVARCHAR(20) ")
            '    SQL.Append(",UPDATED_DATE DATETIME ")
            '    SQL.Append(",ERROR_MSG NVARCHAR(700) ")
            '    SQL.Append(",POST_DATE DATETIME ")
            '    SQL.Append(",DELIVERY_DATE DATETIME ")
            '    SQL.Append(",ON_OFF_LINE_FLAG NVARCHAR(5) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",FORCE_MODULE_STATUS NVARCHAR(1) ")
            '    SQL.Append(",FORCE_MODULE_REASON_ID INTEGER ")
            '    SQL.Append(",FORCE_PXP_STATUS NVARCHAR(1) ")
            '    SQL.Append(",FORCE_PXP_REASON_ID INTEGER ")
            '    SQL.Append(",PART_NO NVARCHAR(20) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table JSP_UNPACK_INTERFACE_VIEW_VIEW!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If

            'If Not TblJSPUnpackPendingDb.Trim.Equals("") Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE [" + TblJSPUnpackPendingDb + "] (")
            '    SQL.Append("MODULE_ID INTEGER ")
            '    SQL.Append(",MODULE_NO NVARCHYAR(20) ")
            '    SQL.Append(",PART_NO NVARCHAR(20) ")
            '    SQL.Append(",PART_NAME NVARCHAR(50) ")
            '    SQL.Append(",QUANTITY_BOX INTEGER ")
            '    SQL.Append(",PART_SEQ_NO INTEGER ")
            '    SQL.Append(",BRANCH_NO NVARCHAR(42) ")
            '    SQL.Append(",ORG_ID INTEGER ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table JSP_UNPACK_PENDING_VIEW!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            'End If

            'If Not TblJSPUnpackingDetailsDb.Trim.Equals("") Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE [" + TblJSPUnpackingDetailsDb + "] (")
            '    SQL.Append("MODULE_ID INTEGER ")
            '    SQL.Append(",MODULE_NO NVARCHAR(20) ")
            '    SQL.Append(",PART_ID INTEGER ")
            '    SQL.Append(",PART_NO NVARCHAR(20) ")
            '    SQL.Append(",PART_NAME NVARCHAR(50) ")
            '    SQL.Append(",QUANTITY_BOX INTEGER ")
            '    SQL.Append(",PART_SEQ_NO INTEGER ")
            '    SQL.Append(",BRANCH_NO NVARCHAR(42) ")
            '    SQL.Append(",ORG_ID INTEGER ")
            '    SQL.Append(",PART_STATUS NVARCHAR(10) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table JSP_UNPACKING_DETAILS_VIEW!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            'End If

            If Not TblSettingDb.Trim.Equals("") Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE [" + TblSettingDb + "] (")
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
                & " VALUES ('WS','URLDCSSP','" & gStrDCSWebServiceURL & "') "
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert url DCS WebService!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If

                sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                & " VALUES ('WS','URLORACLE','" & gStrOracleWebServiceURL & "') "
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
                & " VALUES ('DB','DBPATH','" + gProgPath + "') "
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

                'sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                '& " VALUES ('IMPORT','UNPACK_INTERFACE_VIEW','N')"
                'If ExecuteSQL(sSQL) = False Then
                '    MsgBox("Failed to insert import UNPACK_INTERFACE_VIEW master data!", MsgBoxStyle.Critical, "Import")
                '    Exit Function
                'End If

                'sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                '& " VALUES ('IMPORT','UNPACK_DETAILS_VIEW','N')"
                'If ExecuteSQL(sSQL) = False Then
                '    MsgBox("Failed to insert import UNPACK_DETAILS_VIEW master data!", MsgBoxStyle.Critical, "Import")
                '    Exit Function
                'End If

                'sSQL = "INSERT INTO TBLSetting (SettingCategory, SettingCode, SettingValue) " _
                '& " VALUES ('IMPORT','UNPACK_PENDING_VIEW','N')"
                'If ExecuteSQL(sSQL) = False Then
                '    MsgBox("Failed to insert import UNPACK_PENDING_VIEW master data!", MsgBoxStyle.Critical, "Import")
                '    Exit Function
                'End If

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
                & " VALUES ('BATCHID','SCN_NO','" + getDeviceID() + "')"
                If ExecuteSQL(sSQL) = False Then
                    MsgBox("Failed to insert import batch id!", MsgBoxStyle.Critical, "Import")
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

            '----- Table Transaction -----------------------------------------------------'
            If Not "QRCODE" = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE QRCODE (")
                SQL.Append(" ID INT IDENTITY ")
                SQL.Append(",DOCUMENT NVARCHAR(30) ")
                SQL.Append(",ITEM NVARCHAR(50) ")
                SQL.Append(",LENGTH INTEGER ")
                SQL.Append(",START_POS INTEGER ")
                SQL.Append(",END_POS INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table QR Code!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            sSQL = "INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
              & " VALUES ('JOB_NO','JOB_NO',10,1,10) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
            & " VALUES ('JOB_NO','VENDOR_ID',6,11,16) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
            & " VALUES ('JOB_NO','VENDOR_NAME',50,17,66)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
            & " VALUES ('RETURNABLE_BOX','BOX_TYPE',6,1,6) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
            & " VALUES ('RETURNABLE_BOX','SERIAL_NO',10,7,16)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
            & " VALUES ('MODULE_NO','MODULE_NO',12,1,12) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
            & " VALUES ('MODULE_NO','GROSS_WEIGHT',6,13,18)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT, ITEM, LENGTH, START_POS, END_POS) " _
            & " VALUES ('MODULE_NO','ORDER_NO',12,19,30)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_NO_MDL','SEPIO_NO',12,1,12) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_NO_MDL','ORDER_NO',12,13,24) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_NO_MDL','PART_NO',20,25,44) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_NO_MDL','SEQUENCE',2,45,46) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_NO_MDL','QUANTITY',5,47,51) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_NO_MDL','PART_NAME',50,52,101)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_NO_MDL','COMPANY_CODE',2,102,103)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','BLANK1',64,1,64) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','PART_NO',12,65,76) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','QUANTITY',5,77,81) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','BLANK2',29,82,110) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','PACKING DATE',8,111,118) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','BLANK3',3,119,121) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','MODULE_NO',12,122,133) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','SEQ',2,134,135) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','COMPANY CODE',2,136,137) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_WITH_MDL','BLANK4',21,138,158) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('SEPIO','SEPIO_NO',12,1,12) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('SEPIO','VENDOR_ID',10,13,22) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_PSSB','SEPPIS',15,1,15) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_PSSB','ORDER_NO',20,16,35) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_PSSB','PART_NO',20,36,55) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_PSSB','SEQUENCE',4,56,59) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_PSSB','QUANTITY',4,60,63) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_PSSB','PART_NAME',50,64,113) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('LBL_PSSB','COMPANY_CODE',2,114,115) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('VIS','SERIAL_NO',12,1,12) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('VIS','PACKING_MONTH',2,13,14) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('VIS','PACKING_YEAR',4,15,18) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('VIS','IMPORTER_ID',10,19,28) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('VIS','VANNING_GROUP_ID',10,29,38) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('VIS','COINTAINER_ID',11,39,49) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('VIS','ETD',10,50,59) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('DO_PSSB','DO_NUMBER',20,1,20) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            & " VALUES ('DO_PSSB','ORDER_NO',20,21,40) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            'sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            '& " VALUES ('DO_PSSB','PART_NO',20,11,30) "
            'If ExecuteSQL(sSQL) = False Then
            '    MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
            '    Exit Function
            'End If

            'sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            '& " VALUES ('DO_PSSB','TOTAL_PALLET',2,31,32) "
            'If ExecuteSQL(sSQL) = False Then
            '    MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
            '    Exit Function
            'End If

            'sSQL = " INSERT INTO QRCODE (DOCUMENT,ITEM,LENGTH,START_POS,END_POS) " _
            '& " VALUES ('DO_PSSB','QTY',5,33,37) "
            'If ExecuteSQL(sSQL) = False Then
            '    MsgBox("Failed to insert table QR Code!", MsgBoxStyle.Critical, "Import")
            '    Exit Function
            'End If


            If Not "TBLAutoNumber" = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE TBLAutoNumber (")
                SQL.Append(" ANID INT IDENTITY ")
                SQL.Append(",ANCategory NVARCHAR(50) ")
                SQL.Append(",ANDesc NVARCHAR(100) ")
                SQL.Append(",ANPrefix NVARCHAR(10) ")
                SQL.Append(",ANSuffix NVARCHAR(10) ")
                SQL.Append(",ANStartNum INTEGER ")
                SQL.Append(",ANCurrentNum INTEGER ")
                SQL.Append(",ANLen INTEGER ")
                SQL.Append(",ANMD DATETIME ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table Auto Number!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            sSQL = "INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
             & " VALUES ('BATCHID_OUT_V','','csssyyyyMM',NULL,1,1,3,GETDATE()) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
           & " VALUES ('BATCHID_OUT_SHOP','','csssyyyyMM',NULL,1,1,3,GETDATE()) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
            & " VALUES ('BATCHID_REC_V','','csssyyyyMM',NULL,1,1,3,GETDATE()) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
            & " VALUES ('BATCHID_REC_IMP','','csssyyyyMM',NULL,1,1,3,GETDATE()) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
            & " VALUES ('BATCHID_REC_PART','','csssyyyyMM',NULL,1,1,3,GETDATE()) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
            & " VALUES ('BATCHID_PACKING_MDL','','csssyyyyMM',NULL,1,1,3,GETDATE())"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
            & " VALUES ('BATCHID_PACKING_NOMDL','','csssyyyyMM',NULL,1,1,3,GETDATE()) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
            & " VALUES ('BATCHID_PACKING_INT','','csssyyyyMM',NULL,1,1,3,GETDATE())"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
           & " VALUES ('BATCHID_SHIPPING_VAN','','csssyyyyMM',NULL,1,1,3,GETDATE())"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table auto number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
           & " VALUES ('BATCHID_SHIPPING_DO','','csssyyyyMM',NULL,1,1,3,GETDATE())"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table auto number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLAutoNumber (ANCategory, ANDesc, ANPrefix, ANSuffix, ANStartNum, ANCurrentNum, ANLen, ANMD) " _
            & " VALUES ('SCANNERID','',NULL,NULL,NULL,NULL,3,GETDATE())"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table Auto Number!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If


            If Not "TBLLocation" = "" Then
                SQL = Nothing
                SQL = New System.Text.StringBuilder("")
                SQL.Append("CREATE TABLE TBLLocation (")
                SQL.Append(" ID INT IDENTITY ")
                SQL.Append(",LOCATION NVARCHAR(10) ")
                SQL.Append(",CODE INTEGER ")
                SQL.Append(",CURRENT_CODE INTEGER ")
                SQL.Append(") ")
                If ExecuteSQL(SQL.ToString) = False Then
                    MsgBox("Failed to create table Location!", MsgBoxStyle.Critical, "Import")
                    Exit Function
                End If
                progressbar.Value = progressbar.Value + 2
            End If

            sSQL = "INSERT INTO TBLLocation (LOCATION, CODE, CURRENT_CODE) " _
               & " VALUES ('OUT',1,3) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table location!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLLocation (LOCATION, CODE, CURRENT_CODE) " _
               & " VALUES ('REC_V',2,1) "
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table location!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLLocation (LOCATION, CODE, CURRENT_CODE) " _
               & " VALUES ('REC_I',3,6)" ' change based on SCR P2-SCR_R16_001
            '& " VALUES ('REC_I',3,4)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table location!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If

            sSQL = " INSERT INTO TBLLocation (LOCATION, CODE, CURRENT_CODE) " _
               & " VALUES ('PACKING',4,2)"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("Failed to insert table location!", MsgBoxStyle.Critical, "Import")
                Exit Function
            End If



            ''----- OutGoing Module --------------'
            'If Not "ESPS_SC_OUT_VDR_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_OUT_VDR_INTERFACE  (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",JOB_NO INTEGER ")
            '    SQL.Append(",VENDORS_NAME NVARCHAR(100) ")
            '    SQL.Append(",VENDORS_ID INTEGER ")
            '    SQL.Append(",RB_TYPE NVARCHAR(10) ")
            '    'SQL.Append(",RB_SERIAL_NO BIGINT ")
            '    SQL.Append(",RB_SERIAL_NO NVARCHAR(10) ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table outgoing vendor!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            'If Not "ESPS_SC_OUT_SHOP_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_OUT_SHOP_INTERFACE  (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",MODULE_NO NVARCHAR(12) ")
            '    SQL.Append(",ORDER_NO NVARCHAR(13) ")
            '    SQL.Append(",GROSS_WEIGHT INTEGER  ")
            '    SQL.Append(",RB_TYPE NVARCHAR(10) ")
            '    'SQL.Append(",RB_SERIAL_NO BIGINT ")
            '    SQL.Append(",RB_SERIAL_NO NVARCHAR(10) ")
            '    SQL.Append(",RB_QTY INTEGER ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table outgoing shop!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            ''----- Receiving Module --------------'
            'If Not "ESPS_SC_RCV_VDR_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_RCV_VDR_INTERFACE  (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",SEPIO_NO NVARCHAR(20) ")
            '    SQL.Append(",VENDOR_ID INTEGER ")
            '    SQL.Append(",RB_TYPE NVARCHAR(10) ")
            '    SQL.Append(",RB_QTY INTEGER ")
            '    'SQL.Append(",RB_SERIAL_NO BIGINT ")
            '    SQL.Append(",RB_SERIAL_NO NVARCHAR(10) ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table receiving vendor!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            'If Not "ESPS_SC_RCV_IMP_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_RCV_IMP_INTERFACE  (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",DOCUMENT_NO NVARCHAR(50) ")
            '    SQL.Append(",CUSTOMER_NAME NVARCHAR(50) ")
            '    SQL.Append(",CUSTOMER_ID INTEGER ")
            '    SQL.Append(",RB_TYPE NVARCHAR(10) ")
            '    'SQL.Append(",RB_SERIAL_NO BIGINT ")
            '    SQL.Append(",RB_SERIAL_NO NVARCHAR(10) ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(",NEW_BOX NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table receiving importer", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            'If Not "ESPS_SC_RCV_PART_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_RCV_PART_INTERFACE (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",SEPIO_NO NVARCHAR(20) ")
            '    SQL.Append(",PART_NO NVARCHAR(50) ")
            '    SQL.Append(",PART_QTY INTEGER ")
            '    SQL.Append(",SEQ_NO INTEGER ")
            '    SQL.Append(",COMPANY_CODE INTEGER ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table receiving part!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If

            ''-------------Packing ---------------------------------------------------
            'If Not "ESPS_SC_PACK_INT_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_PACK_INT_INTERFACE  (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",SEPPIS NVARCHAR(20) ")
            '    SQL.Append(",PART_NO NVARCHAR(50) ")
            '    SQL.Append(",PART_QTY INTEGER ")
            '    SQL.Append(",SEQ_NO INTEGER ")
            '    SQL.Append(",ORDER_NO NVARCHAR(20) ") '20092016
            '    SQL.Append(",PART_NAME NVARCHAR(50) ")
            '    SQL.Append(",COMPANY_CODE INTEGER ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table packing internal!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            'If Not "ESPS_SC_PACK_MDL_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_PACK_MDL_INTERFACE (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",SEPIO_NO NVARCHAR(20) ")
            '    SQL.Append(",MODULE_NO NVARCHAR(12) ")
            '    SQL.Append(",ORDER_NO NVARCHAR(13) ")
            '    SQL.Append(",PART_NO NVARCHAR(50) ")
            '    SQL.Append(",PART_QTY INTEGER ")
            '    SQL.Append(",SEQ_NO INTEGER ")
            '    SQL.Append(",COMPANY_CODE INTEGER ")
            '    SQL.Append(",LABEL_SCAN_STATUS NVARCHAR(20) ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table packing module!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            'If Not "ESPS_SC_PACK_NOMDL_INTERFACE" = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_PACK_NOMDL_INTERFACE (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",CUSTOMER_NAME NVARCHAR(50) ")
            '    SQL.Append(",CUSTOMER_ID INTEGER ")
            '    SQL.Append(",CASE_TYPE NVARCHAR(10) ")
            '    SQL.Append(",CASE_SERIAL_NO INTEGER ")
            '    SQL.Append(",PART_NO NVARCHAR(50) ")
            '    SQL.Append(",SEPIO_NO NVARCHAR(20) ")
            '    SQL.Append(",QTY INTEGER ")
            '    SQL.Append(",SEQ_NO INTEGER ")
            '    SQL.Append(",COMPANY_CODE INTEGER ")
            '    SQL.Append(",ORDER_NO NVARCHAR(13) ")
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",RR_NUMBER NVARCHAR(50) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(",GROUP_BATCH_ID BIGINT  ")
            '    SQL.Append(",PROCESS_END NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table packing no module!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            ''---------------- Shipping -------------------------------------------------
            'If Not "ESPS_SC_SHIP_DO_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_SHIP_DO_INTERFACE  (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",DO_NO NVARCHAR(50) ")
            '    SQL.Append(",PART_NO NVARCHAR(50) ")
            '    SQL.Append(",TOTAL_PALLET INTEGER ")
            '    SQL.Append(",QTY INTEGER ")
            '    SQL.Append(",SEQ_NO INTEGER ")
            '    SQL.Append(",ORDER_NO NVARCHAR(20) ") '20092016
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",REASON_ID NVARCHAR(100) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table shipping delivery!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If


            'If Not "ESPS_SC_SHIP_VAN_INTERFACE " = "" Then
            '    SQL = Nothing
            '    SQL = New System.Text.StringBuilder("")
            '    SQL.Append("CREATE TABLE ESPS_SC_SHIP_VAN_INTERFACE  (")
            '    SQL.Append(" ID INT IDENTITY ")
            '    SQL.Append(",VIS_NO NVARCHAR(20) ")
            '    SQL.Append(",MODULE_NO NVARCHAR(12) ")
            '    SQL.Append(",ORDER_NO NVARCHAR(13) ")
            '    SQL.Append(",CONTAINER_NO NVARCHAR(12) ")
            '    SQL.Append(",SEAL_NO NVARCHAR(20) ") 'INTEGER
            '    SQL.Append(",PROCESS_TYPE INTEGER ")
            '    SQL.Append(",ABNORMAL_FLAG NVARCHAR(1) ")
            '    SQL.Append(",SCAN_DATE DATETIME ")
            '    SQL.Append(",SCANNER_ID NVARCHAR(20) ")
            '    SQL.Append(",PIC NVARCHAR(20) ")
            '    SQL.Append(",BATCH_ID BIGINT ")
            '    SQL.Append(",POST_FLAG NVARCHAR(1) ")
            '    SQL.Append(") ")
            '    If ExecuteSQL(SQL.ToString) = False Then
            '        MsgBox("Failed to create table shipping vanning!", MsgBoxStyle.Critical, "Import")
            '        Exit Function
            '    End If
            '    progressbar.Value = progressbar.Value + 2
            'End If
            CreateMasterTable = True

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
		'MsgBox("[-->" + Location + "<--]" + Exception)
        'MsgBox("Data Length not match")
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

            'If TblJSPUnpackInterfaceDb <> "" Then
            '    ExecuteSQL("Drop Table " & TblJSPUnpackInterfaceDb, objCon, CType(DroptableValue, Boolean))
            'End If

            'If TblJSPUnpackPendingDb <> "" Then
            '    ExecuteSQL("Drop Table " & TblJSPUnpackPendingDb, objCon, CType(DroptableValue, Boolean))
            'End If

            'If TblJSPUnpackingDetailsDb <> "" Then
            '    ExecuteSQL("Drop Table " & TblJSPUnpackingDetailsDb, objCon, CType(DroptableValue, Boolean))
            'End If

        Catch ex As Exception
            UnHandledError(ex.ToString(), ErrLoc)
        Finally
            objConn.Close()
            objConn.Dispose()
        End Try
    End Sub
#End Region

#Region ". Encryption  and Decryption ."
    'Public Function GetDecrypt(ByVal sSource As String)

    '    Dim sTemp, sDecrypt, s As String
    '    Dim lStr, i As Long

    '    Try
    '        lStr = sSource.Trim.Length
    '        If (lStr / 2) <> (sSource.Trim.Length / 2) Then
    '            Exit Function
    '        End If

    '        sTemp = StrReverse(sSource)
    '        For i = lStr To 1 Step -2
    '            s = Chr(Val("&H" & Mid(sTemp, i - 1, 2)))
    '            s = Chr(Asc(s) Xor ((i / 2) Mod 30) * 8)
    '            sDecrypt = s & sDecrypt
    '        Next
    '        Return sDecrypt
    '    Catch ex As Exception
    '    End Try

    'End Function
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
