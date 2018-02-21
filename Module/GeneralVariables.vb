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
    Private Const METHOD_BUFFERED As Int32 = 0
    Private Const FILE_ANY_ACCESS As Int32 = 0
    Private Const FILE_DEVICE_HAL As Int32 = &H101
    Public Const ERROR_NOT_SUPPORTED As Int32 = &H32
    Public Const ERROR_INSUFFICIENT_BUFFER As Int32 = &H7A
    'Public Const IOCTL_HAL_GET_DEVICEID As Int32 = (&H10000 * FILE_DEVICE_HAL) Or (&H4000 * FILE_ANY_ACCESS) Or (&H4 * 21) Or METHOD_BUFFERED


    Declare Function KernelIoControl Lib "CoreDll.dll" _
     (ByVal dwIoControlCode As Int32, _
     ByVal lpInBuf As IntPtr, _
     ByVal nInBufSize As Int32, _
     ByVal lpOutBuf() As Byte, _
     ByVal nOutBufSize As Int32, _
     ByRef lpBytesReturned As Int32) As Boolean

    Public lblMessage As Label
    Public progressBar As ProgressBar

    Public gAppName As String = "SERVICE PART"
    Public gProgPath As String = Path.GetDirectoryName([Assembly].GetExecutingAssembly.GetName.CodeBase) & "\" '+ System.Reflection.Assembly.GetExecutingAssembly.GetName.Name 'System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).ToString & " \ ""
    Public gDBPath As String = "/Application/DCSServicePart/"
    Public gScannerID As String = getDeviceID()
    Public gScnPrefix As String = ""
    Public gScnSuffix As String = ""
    Public gDateTimeFormat As String = ""
    Public gDatabaseName As String = "DBDCSServicePart.sdf"
    Public gDatabasePwd As String = ""
    Public ConnStr As String = "Data Source=" & gDBPath + gDatabaseName & ";password=" & gDatabasePwd
    Public gStrDCSWebServiceURL As String = "http://172.20.13.204:8086/" '"http://172.20.13.160:8888/"
    'Public ws_dcsClient As DCSWebService.DCSWebService = New DCSWebService.DCSWebService

    Public gStrOracleWebServiceURL As String = "http://10.1.115.94:4559/ws/perodua.eai.process.inventory.ws.servicePart:processServicePartService/perodua_eai_process_inventory_ws_servicePart_processServicePartService_Port"
    Public gStrOraUserID As String = "promiseusr"
    Public gStrOraUserPwd As String = "promiseusr"
    'Public ws_oracleClient As OraWebService.processServicePartService = New OraWebService.processServicePartService

    Public SQLServerName As String
    Public SQLServerPort As String
    Public SQLDatabaseName As String
    Public SQLCommandTimeOut As String
    Public SQLUserID As String
    Public SQLPassword As String
    Public SQLConnectionTimeOut As String
    Public DroptableValue As String

   
    '---Master Table -----------------------------------------------
    Public TblUserDb As String = "SEP_LOGIN_V"
    Public TblSettingDb As String = "TBLSetting"
    Public TblSEPPackVDb As String = "SEP_PACK_IMPORTER_V"
    Public TblSEPImporterVDb As String = "SEP_RB_IMPORTER_V"
    Public TblSEPCaseTypeVDb As String = "SEP_CASE_TYPE_V"
    Public TblSEPRBCurrLocDb As String = "SEP_RB_CURR_LC_V"
    Public TblSEPRBJobDb As String = "SEP_RB_JOB_V"
    Public TblSEPRBReasonDb As String = "SEP_RB_REASON_V"
    Public TblSEPRBTypeDb As String = "SEP_RB_TYPE_V"
    Public TblSEPStopperDb As String = "SEP_RB_STOPPER_QTY_V"
    Public TblSEPSupplierDb As String = "SEP_SUPPLIER_V"


    '----Main ------------
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

    '----Setting --------
    Public sUser As String = ""
    Public sReason As String = ""
    Public sRBType As String = ""
    Public sImporter As String = ""
    Public sVendor As String = ""
    Public sCustomer As String = ""
    Public sCaseType As String = ""
    Public sStopperType As String = ""
    Public iInterval As Integer = 0

    '----Others --------
    Public gStrRefErrorMsg As String = ""

    <DllImport("coredll.dll")> _
   Private Function SetSystemTime(ByRef time As SYSTEMTIME) As Boolean
    End Function

    Public Structure SYSTEMTIME
        Public wYear As UInt16
        Public wMonth As UInt16
        Public wDayOfWeek As UInt16
        Public wDay As UInt16
        Public wHour As UInt16
        Public wMinute As UInt16
        Public wSecond As UInt16
        Public wMilliseconds As UInt16
    End Structure

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

    Public Sub WriteLog(ByVal strMessage As String)
        Dim strPath As String, file As System.IO.StreamWriter
        strPath = gProgPath & "/IssueLog.txt"
        file = New System.IO.StreamWriter(strPath, True)
        file.WriteLine(Now & " => " & strMessage)
        file.Close()
    End Sub

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
        Try
            Dim dtDate As Date = Date.ParseExact(strDate, gStrTimeFormat, gStrculture)

        Catch ex As Exception
            Exit Function
        End Try

        Try
            Dim st As New SYSTEMTIME
            st.wDay = Convert.ToUInt16(strDate.Substring(0, 2)) '0 1
            st.wMonth = Convert.ToUInt16(strDate.Substring(3, 2)) '3 4
            st.wYear = Convert.ToUInt16(strDate.Substring(6, 4)) '6 7 8 9
            st.wHour = Convert.ToUInt16(strDate.Substring(11, 2)) '11 12
            st.wMinute = Convert.ToUInt16(strDate.Substring(14, 2)) '14 15
            st.wSecond = Convert.ToUInt16(strDate.Substring(17, 2)) '17 18

            SetSystemTime(st)
            SetLocalTime = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SERVICE PART")
        End Try
    End Function

    Public Sub bringPanelToFront(ByRef frontPanel As Panel, ByRef backPanel As Panel)
        frontPanel.Location = New Point(0, 0)
        frontPanel.Visible = True
        backPanel.Visible = False
    End Sub

    Public Sub setStatusBar(ByRef statusBar As StatusBar, ByVal username As String, ByVal mode As Boolean, ByVal scannerId As String)
        statusBar.Text = username.PadRight(30, " ") & scannerId.PadLeft(25, " ") & IIf(mode = True, "(Online)".PadLeft(10, " "), "(Offline)".PadLeft(10, " "))

    End Sub

#Region ". Get Scanner ID ."

   
    Public Function getDeviceID() As String

        getDeviceID = Net.Dns.GetHostName()

    End Function

    Public Function setDeviceName(ByVal deviceName As String) As Boolean
        setDeviceName = False
        Dim IDENTITY As String = "HKEY_LOCAL_MACHINE\Ident"
        Dim NAME As String = "Name"
        Registry.SetValue(IDENTITY, NAME, deviceName)
        setDeviceName = True
    End Function

    Public Function ReadUUID() As String

        'Read Universally unique identifier

        ' Initialize the output buffer to the size of a 
        ' Win32 DEVICE_ID structure 
        Dim outbuff(31) As Byte
        Dim dwOutBytes As Int32
        Dim done As Boolean = False
        Dim nBuffSize As Int32 = outbuff.Length
        Dim dwPresetIDOffset As Int32
        Dim dwPresetIDSize As Int32
        Dim dwPlatformIDOffset As Int32
        Dim dwPlatformIDSize As Int32
        Dim sb As New StringBuilder
        Dim i As Integer
        Dim rtnStr As String = String.Empty

        Try
            ' Set DEVICEID.dwSize to size of buffer.  Some platforms look at
            ' this field rather than the nOutBufSize param of KernelIoControl
            ' when determining if the buffer is large enough.
            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0)
            dwOutBytes = 0

            ' Loop until the device ID is retrieved or an error occurs.
            While Not done
                If KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, _
                    0, outbuff, nBuffSize, dwOutBytes) Then
                    done = True
                Else
                    Dim errnum As Integer = Marshal.GetLastWin32Error()
                    Select Case errnum
                        Case ERROR_NOT_SUPPORTED
                            Throw New NotSupportedException( _
                                "IOCTL_HAL_GET_DEVICEID is not supported on this device", _
                                New Win32Exception(errnum))

                        Case ERROR_INSUFFICIENT_BUFFER

                            ' The buffer is not big enough for the data.  The
                            ' required size is in the first 4 bytes of the output 
                            ' buffer (DEVICE_ID.dwSize).
                            nBuffSize = BitConverter.ToInt32(outbuff, 0)
                            outbuff = New Byte(nBuffSize) {}

                            ' Set DEVICEID.dwSize to size of buffer.  Some
                            ' platforms look at this field rather than the
                            ' nOutBufSize param of KernelIoControl when
                            ' determining if the buffer is large enough.
                            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0)

                        Case Else
                            Throw New Win32Exception(errnum, "Unexpected error")
                    End Select
                End If
            End While

            ' Copy the elements of the DEVICE_ID structure.
            dwPresetIDOffset = BitConverter.ToInt32(outbuff, &H4)
            dwPresetIDSize = BitConverter.ToInt32(outbuff, &H8)
            dwPlatformIDOffset = BitConverter.ToInt32(outbuff, &HC)
            dwPlatformIDSize = BitConverter.ToInt32(outbuff, &H10)

            For i = dwPresetIDOffset To (dwPresetIDOffset + dwPresetIDSize) - 1
                sb.Append(String.Format("{0:X2}", outbuff(i)))
            Next i

            'leave out the - from the serial number
            'sb.Append("-")

            For i = dwPlatformIDOffset To (dwPlatformIDOffset + dwPlatformIDSize) - 1
                sb.Append(String.Format("{0:X2}", outbuff(i)))
            Next i
        Catch ex As Exception
            If IsNothing(sb) = False Then
                sb = Nothing
            End If
        Finally
            If IsNothing(sb) = False Then
                If (sb.ToString().IndexOf("-") > 0) Then
                    'rtnStr = sb.ToString().Substring(0, sb.ToString().IndexOf("-"))
                    ' leave out the - from the serial number
                    rtnStr = sb.ToString().Replace("-", "")
                Else
                    rtnStr = sb.ToString()
                End If
            Else
                rtnStr = "-"
            End If
        End Try
        Return rtnStr
    End Function
#End Region

End Module
