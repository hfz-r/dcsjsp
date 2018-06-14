Imports System.Xml
Imports System.Reflection
Imports System.IO
Imports System.Text
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports Microsoft.Win32

Module GeneralVariables

#Region ". Global Variables Declare ."

    Public Const IOCTL_HAL_GET_DEVICEID As Integer = &H1010054
    Public Const ERROR_NOT_SUPPORTED As Int32 = &H32
    Public Const ERROR_INSUFFICIENT_BUFFER As Int32 = &H7A

    Declare Function KernelIoControl Lib "CoreDll.dll" _
     (ByVal dwIoControlCode As Int32, _
     ByVal lpInBuf As IntPtr, _
     ByVal nInBufSize As Int32, _
     ByVal lpOutBuf() As Byte, _
     ByVal nOutBufSize As Int32, _
     ByRef lpBytesReturned As Int32) As Boolean

    Public lblMessage As Label
    Public progressBar As ProgressBar

    '------------ Message Display ------------
    Public successMsgPart As String = "All Parts Succesfully Scanned."
    Public successMsgModule As String = "All Modules Succesfully Scanned."
    Public successMsg As String = "Succesfully Scanned."

    Public gAppName As String = "JSP"
    Public gDBPath As String = "/Application/DCSJSP/"
    Public gConfig As String = String.Format("{0}\", Path.GetDirectoryName([Assembly].GetExecutingAssembly.GetName.CodeBase)) '+ System.Reflection.Assembly.GetExecutingAssembly.GetName.Name 'System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).ToString & " \ ""
    Public gScannerID As String = ReadUUID()
    Public gScnPrefix As String
    Public gScnSuffix As String
    Public gDateTimeFormat As String
    Public gDatabaseName As String
    Public gDatabasePwd As String
    Public ConnStr As String
    Public gStrDCSWebServiceURL As String
    Public gStrOracleChckWebServiceURL As String
    Public gStrOracleCpWebServiceURL As String
    Public gStrOraUserID As String
    Public gStrOraUserPwd As String

    Public ws_dcsClient As DCSWebService.DCSWebService = New DCSWebService.DCSWebService
    Public ws_validationClient As ValidationWebService.processValidationService = New ValidationWebService.processValidationService
    Public ws_inventoryClient As InventoryConsumptionWebService.processInventoryConsumptionService = New InventoryConsumptionWebService.processInventoryConsumptionService

    Public SQLServerName As String
    Public SQLServerPort As String
    Public SQLDatabaseName As String
    Public SQLCommandTimeOut As String
    Public SQLUserID As String
    Public SQLPassword As String
    Public SQLConnectionTimeOut As String
    Public DroptableValue As String

    '------------ Master Table ------------
    Public TblTxnCodeDb As String = "TblTxnCode"
    Public TblSettingDb As String = "TBLSetting"
    Public TblUserDb As String = "JSP_USER_LOGIN_VIEW"
    Public TblJSPOrganizationDb As String = "JSP_ORGANIZATION_HEADERS_VIEW"
    Public TblJSPSupplyBPHeaderDb As String = "JSP_SUPPLY_BP_HEADERS_VIEW"
    Public TblJSPSupplyBPDetailsView As String = "JSP_SUPPLY_BP_DETAILS_VIEW"
    Public TblJSPSupplyCPHeaderDb As String = "JSP_SUPPLY_CP_HEADERS_VIEW"
    Public TblJSPSupplyCPDetailsView As String = "JSP_SUPPLY_CP_DETAILS_VIEW"
    Public TblJSPAbnormalReasonCodeDb As String = "JSP_ABNORMAL_REASON_CODE_VIEW"
    Public TblJSPSupplyInterface As String = "JSP_SUPPLY_INTERFACE"
    Public TblJSPRobbingInterface As String = "JSP_ROBBING_INTERFACE"
    Public TblJSPRobbingInfoView As String = "JSP_ROBBING_INFO_VIEW"
    Public TblJSPSupplyPLDetailsView As String = "JSP_SUPPLY_PL_DETAILS_VIEW"
    Public TblJSPSupplyPLPartsIDView As String = "JSP_SUPPLY_PL_PARTS_ID_VIEW"
    Public TblJSPSupplyPLPendingView As String = "JSP_SUPPLY_PL_PENDING_VIEW"

    Public CDIO_PENDING As String = "JSP_CDIO_PENDING"
    Public CDIO_INTERFACE As String = "JSP_CDIO_RCV_INTERFACE"
    Public TblBatch As String = "TBLBatch"
    Public UNPACK_PENDING As String = "JSP_UNPACK_PENDING"
    Public UNPACK_INTERFACE As String = "JSP_UNPACK_INTERFACE"

    '------------ Main ------------
    Public boolsetting As Boolean = False
    Public gStrUsername As String = ""
    Public gBoolAbnormal As Boolean = False
    Public gStrculture As CultureInfo = CultureInfo.InvariantCulture
    Public gStrTimeFormat As String = "dd-MM-yyyy HH:mm:ss"
    Public gStrTimeFormatOracle As String = "dd-MM-yyyy hh24:mi:ss"
    Public gStrTimeFormatSQLCE As String = "yyyy-MM-dd HH:mm:ss"
    Public gSCNNo As String = ""
    Public mode As Boolean = False
    Public boolPackBack As Boolean = False
    Public interval As Integer = (1000 * 60) * 1 'minutes

    '------------ Setting ------------
    Public sUser As String = ""
    Public sOrganization As String = ""
    Public sReason As String = ""
    Public sShop As String = ""
    Public sSupply As String = ""
    Public iInterval As Integer = 0
    Public org_ID As String = ""
    Public loginUser As String = ""
    Public loginPass As String = ""


    Public ImpShop As Boolean = False
    Public ImpSupplier As Boolean = False
    Public ImpReason As Boolean = False
    Public ImpOrganization As Boolean = False
    Public ImpUser As Boolean = False

    Public chkOffline As Boolean = False

#End Region

#Region ". XML Setting ."

    Public Function Initialize() As String
        Dim XMLstr As String = GetXMLSetting()
        Return XMLstr
    End Function

    Public Function GetXMLSetting() As String
        Dim errMsg As String = "Config File not found"
        Try
            If File.Exists(gConfig + "Config.xml") = False Then
                MsgBox("Config File not found! Application will now exit.", MsgBoxStyle.Critical, "Error")
                Return errMsg
            End If

            Dim configReader As XmlReader = New XmlTextReader(gConfig + "Config.xml")
            While (configReader.Read())
                Dim type = configReader.NodeType
                If type = XmlNodeType.Element Then
                    If configReader.Name = "SQLServerName" Then
                        gDBPath = configReader.ReadInnerXml.ToString()
                    End If
                    If configReader.Name = "SQLDatabaseName" Then
                        gDatabaseName = configReader.ReadInnerXml.ToString()
                    End If
                    If configReader.Name = "SQLPassword" Then
                        gDatabasePwd = configReader.ReadInnerXml.ToString()
                    End If
                    If configReader.Name = "WSDcsURL" Then
                        gStrDCSWebServiceURL = configReader.ReadInnerXml.ToString()
                    End If
                    If configReader.Name = "WSOraChckURL" Then
                        gStrOracleChckWebServiceURL = configReader.ReadInnerXml.ToString()
                    End If
                    If configReader.Name = "WSOraCpURL" Then
                        gStrOracleCpWebServiceURL = configReader.ReadInnerXml.ToString()
                    End If
                    If configReader.Name = "WSOraUserID" Then
                        gStrOraUserID = configReader.ReadInnerXml.ToString()
                    End If
                    If configReader.Name = "WSOraPassword" Then
                        gStrOraUserPwd = configReader.ReadInnerXml.ToString()
                    End If
                End If
            End While
            ConnStr = "Data Source=" & gDBPath + gDatabaseName & ";password=" & gDatabasePwd
        Catch ex As Exception
            MsgBox(ex.Message.ToString(), MsgBoxStyle.Critical, "Config")
            Return errMsg
        End Try
        Return ConnStr
    End Function

#End Region

#Region ". Show Mouse Cursor, Busy or Not Busy ."
    Public Sub ShowWait(ByVal Status As Boolean)
        Try

            If Status = True Then
                'If System.Environment.OSVersion.Version.Major = 4 Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                'End If
            Else
                'If System.Environment.OSVersion.Version.Major = 4 Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region ". Public Function ."

    Public Enum EnumFilterQueryIndex
        CodeOnly = 0
        PercentCodePercent = 1
        CodePercent = 2
        PercentCode = 3
        AsteriskCodeAsterisk = 4
        CodeAsterisk = 5
        AsteriskCode = 6
    End Enum

    Public Function SQLQuote(ByVal oPrmQuery As String, _
        Optional ByVal oPrmWithFilter As EnumFilterQueryIndex = EnumFilterQueryIndex.CodeOnly, _
        Optional ByVal oPrmWithNationalKey As Boolean = False) As String

        Dim oQuery As String
        oQuery = oPrmQuery

        Select Case oPrmWithFilter

            Case EnumFilterQueryIndex.CodeOnly
                oQuery = oPrmQuery

            Case EnumFilterQueryIndex.CodePercent
                oQuery = oPrmQuery & "%"

            Case EnumFilterQueryIndex.PercentCode
                oQuery = "%" & oPrmQuery

            Case EnumFilterQueryIndex.PercentCodePercent
                oQuery = "%" & oPrmQuery & "%"

            Case EnumFilterQueryIndex.CodeAsterisk
                oQuery = oPrmQuery & "*"

            Case EnumFilterQueryIndex.AsteriskCode
                oQuery = "*" & oPrmQuery

            Case EnumFilterQueryIndex.AsteriskCodeAsterisk
                oQuery = "*" & oPrmQuery & "*"

            Case Else
                oQuery = "%" & oPrmQuery & "%"
        End Select

        oQuery = "'" & Replace(oQuery, "'", "''") & "'"

        If oPrmWithNationalKey Then
            oQuery = "N" & oQuery
        End If

        SQLQuote = oQuery
        Return SQLQuote
    End Function

    Public Function SetLocalTime(ByVal strDate As String) As Boolean
        SetLocalTime = False
        Dim d As DateTime
        Dim strTime As String = Convert.ToDateTime(strDate).ToString("hh:mm tt")
        Dim strYear As String = Convert.ToDateTime(strDate).ToString("yyyy")
        Dim strMonth As String = Convert.ToDateTime(strDate).ToString("MM")
        Dim strDay As String = Convert.ToDateTime(strDate).ToString("dd")

        Try
            d = strTime
            Microsoft.VisualBasic.TimeOfDay = d
            Microsoft.VisualBasic.DateString = String.Format("{0}-{1}-{2}", strMonth, strDay, strYear)
            SetLocalTime = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
    End Function

    Public Sub bringPanelToFront(ByRef frontPanel As Panel, ByRef backPanel As Panel)
        GC.Collect()
        GC.WaitForPendingFinalizers()
        frontPanel.Location = New Point(0, 0)
        frontPanel.Visible = True
        backPanel.Visible = False
    End Sub

    Public Sub setStatusBar(ByRef statusBar As StatusBar, ByVal username As String, ByVal mode As Boolean, ByVal scannerId As String)
        statusBar.Text = username.PadRight(30, " ") & scannerId.PadLeft(25, " ") & IIf(mode = True, "(Online)".PadLeft(10, " "), "(Offline)".PadLeft(10, " "))
    End Sub

#End Region

#Region ". Get Scanner ID ."

    Public Function ReadUUID() As String
        Dim terminalInfo As Symbol.ResourceCoordination.TerminalInfo = New Symbol.ResourceCoordination.TerminalInfo()
        Return terminalInfo.ESN.ToString()
    End Function

#End Region

#Region ". CDIO Module Constraint ."

    Public Const CDIO_ID_LENGTH As Integer = 10
    Public Const CDIO_NO_LENGTH As Integer = 13
    Public Const PROD_DATE_LENGTH As Integer = 10
    Public Const EXPORTER_CODE_LENGTH As Integer = 3
    Public Const EXPORTER_LENGTH As Integer = 50
    Public Const CONTAINER_ID_LENGTH As Integer = 5
    Public Const CONTAINER_NO_LENGTH As Integer = 11
    Public Const ORG_ID_LENGTH As Integer = 2
    Public Const SCAN_FLAG_LENGTH As Integer = 1

    Public Const MODULE_NO_LENGTH As Integer = 6
    Public Const PILLING_NO_LENGTH As Integer = 1
    Public Const GROSS_WEIGHT_LENGTH As Integer = 5
    Public Const ORDER_NO_LENGTH As Integer = 12

#End Region

#Region ". Unpack Module Constraint ."
    '----MODULE QR FORMAT
    Public Const UNPACK_MODULE_NO_LENGTH As Integer = 6 'MODULE_NO_LENGTH
    Public Const UNPACK_PILLING_NO_LENGTH As Integer = 1 'PILLING_NO_LENGTH
    Public Const UNPACK_GROSS_WEIGHT_LENGTH As Integer = 5 'GROSS_WEIGHT_LENGTH
    Public Const UNPACK_ORDER_NO_LENGTH As Integer = 12 'ORDER_NO_LENGTH
#End Region

#Region ". Progress Lane Module Constraint."

    '----- SHOPPING LIST LENGTH -----
    Public Const SL_PDIO_ID_LENGTH As Integer = 10
    Public Const SL_PDIO_NO_LENGTH As Integer = 12
    Public Const SL_PRODUCTION_DATE_LENGTH As Integer = 10
    Public Const SL_SHOP_ID_LENGTH As Integer = 4
    Public Const SL_LANE_ID_LENGTH As Integer = 2
    Public Const SL_ORG_ID_LENGTH As Integer = 2
    Public Const SL_SCAN_FLAG_LENGTH As Integer = 1

    '----- KANBAN QR LENGTH -----
    Public Const KB_PDIO_ID_LENGTH As Integer = 10
    Public Const KB_PDIO_NO_LENGTH As Integer = 12
    Public Const KB_DELIVERY_DATE_LENGTH As Integer = 10
    Public Const KB_DOCK_CODE_LENGTH As Integer = 4
    Public Const KB_ORDER_TYPE_LENGTH As Integer = 2
    Public Const KB_VENDOR_ID_LENGTH As Integer = 6
    Public Const KB_TRANSPORTER_ID_LENGTH As Integer = 6
    Public Const KB_LANE_ID_LENGTH As Integer = 2
    Public Const KB_TIER_LENGTH As Integer = 1
    Public Const KB_ORG_ID_LENGTH As Integer = 2
    Public Const KB_PART_NO_LENGTH As Integer = 14
    Public Const KB_BACK_NO_LENGTH As Integer = 6
    Public Const KB_SEQ_NO_LENGTH As Integer = 4
    Public Const KB_TRANSACTION_CODE_LENGTH As Integer = 2
    Public Const KB_QTY_ORDER_LENGTH As Integer = 4
    Public Const KB_DELIVERY_TYPE_LENGTH As Integer = 2
    Public Const KB_SCAN_FLAG_LENGTH As Integer = 1

#End Region

#Region ". Reusable Part QR Length."
    '----PART QR (DMC)
    Public Const MANUFACTURE_CODE_LENGTH As Integer = 2
    Public Const SUPPLIER_CODE_LENGTH As Integer = 4
    Public Const SUPPLIER_PLANT_CODE_LENGTH As Integer = 1
    Public Const SUPPLIER_SHIPPING_DOCK_LENGTH As Integer = 3
    Public Const BEFORE_PACKING_ROUTING_LENGTH As Integer = 6
    Public Const RECEIVING_COMPANY_CODE_LENGTH As Integer = 4
    Public Const RECEIVING_PLANT_CODE_LENGTH As Integer = 1
    Public Const RECEIVING_DOCK_CODE_LENGTH As Integer = 2
    Public Const PACKING_ROUTING_CODE_LENGTH As Integer = 6
    Public Const GRANTER_CODE_LENGTH As Integer = 4
    Public Const ORDER_TYPE_LENGTH As Integer = 1
    Public Const KANBAN_CLASSIFICATION_LENGTH As Integer = 1
    Public Const DELIVERY_DATE_LENGTH As Integer = 4
    Public Const DELIVERY_CODE_LENGTH As Integer = 2
    Public Const MROS_LENGTH As Integer = 2
    Public Const ORDER_NUMBER_LENGTH As Integer = 12
    Public Const DELIVERY_NUMBER_LENGTH As Integer = 5
    Public Const BACK_NUMBER_LENGTH As Integer = 4
    Public Const PARTS_NO_LENGTH As Integer = 10
    Public Const PART_NO_SFX_LENGTH As Integer = 2
    Public Const QTY_BOX_LENGTH As Integer = 5
    Public Const RUNOUT_FLAG_LENGTH As Integer = 1
    Public Const DELIVERY_CODE_2_LENGTH As Integer = 1
    Public Const BOX_TYPE_LENGTH As Integer = 8
    Public Const BRANCH_NUMBER_LENGTH As Integer = 4
    Public Const ADDRESS_LENGTH As Integer = 10
    Public Const DELIVERY_TIME_LENGTH As Integer = 5
    Public Const PACKING_DATE_LENGTH As Integer = 8
    Public Const KATASHIKI_JERSEY_NUMBER_LENGTH As Integer = 3
    Public Const LOT_NO_LENGTH As Integer = 4
    Public Const MODULE_CATEGORY_LENGTH As Integer = 2
    Public Const PART_SEQ_NUMBER_LENGTH As Integer = 2
    Public Const PART_BRANCH_NUMBER_LENGTH As Integer = 2
    Public Const DUMMY_LENGTH As Integer = 20
    Public Const VERSION_NO_LENGTH As Integer = 1

#End Region

End Module
