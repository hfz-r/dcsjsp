Imports System
Imports System.Data
Imports System.Data.SqlServerCe
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports DCSJSP.clsDataTransfer
Imports System.Net
Imports System.Reflection

Module GeneralFunction

#Region ". Get Function ."

    Public Function getData(ByVal sSQL As String) As DataTable
        Dim dt As DataTable = New DataTable
        Try
            Dim dbReader As SqlCeDataReader = Nothing
            dbReader = OpenRecordset(sSQL, objConn)
            dt.Load(dbReader)
            dbReader.Close()
            dbReader.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
        Return dt
    End Function

    Public Function getDTData(ByVal sSQL As String) As DataTable
        Dim dt As DataTable = New DataTable
        Try
            Dim dbAdapter As SqlCeDataAdapter = Nothing
            dbAdapter = New SqlCeDataAdapter(sSQL, objConn)
            dbAdapter.Fill(dt)
            objConn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
        Return dt
    End Function

#End Region

#Region ". Batch & Scanner Helper ."

    Public Function GetBatchID(ByVal category As String, ByVal categoryValue As String) As String
        Try
            Dim Prefix As String = Nothing
            Dim CurrentNo As String = Nothing
            Dim ID As Integer
            Dim CurrentDate As DateTime
            Dim dt As DataTable = New DataTable

            dt = getData(String.Format("SELECT *, GETDATE() FROM [{0}] WHERE CATEGORY LIKE {1}", TblBatch, SQLQuote(category)))
            If dt.Rows.Count > 0 Then
                ID = dt.Rows(0).Item(0).ToString
                Prefix = dt.Rows(0).Item(2).ToString
                CurrentNo = dt.Rows(0).Item(4).ToString
                CurrentDate = dt.Rows(0).Item(5)
            End If

            Dim BatchId As System.Text.StringBuilder = New System.Text.StringBuilder("")
            If Prefix.Substring(0, 1).Contains("c") Then
                BatchId.Append(categoryValue)
            End If
            If Prefix.Substring(1, 3).Contains("sss") Then
                BatchId.Append(gSCNNo.ToString().PadLeft(3, "0"))
            End If
            If Not String.IsNullOrEmpty(CurrentNo) Then
                BatchId.Append(CurrentNo)
            End If

            Return BatchId.ToString()

        Catch ex As Exception
            Throw New Exception("Failed To Create Batch.")
        End Try
    End Function

    Public Function GetScannerId() As String
        Dim scnid As String = ""
        Try
            Dim dt As DataTable = New DataTable

            dt = getData("SELECT SettingValue FROM TblSetting WHERE settingCategory='SCN' AND settingCode='SCNID' ")
            If dt.Rows.Count > 0 Then
                scnid = dt.Rows(0).Item("SettingValue").ToString
            End If

        Catch ex As Exception
            MsgBox(String.Format("Error reading Scanner ID{0}", ex.Message), MsgBoxStyle.Critical)
        End Try

        Return scnid
    End Function

#End Region

#Region ". Load Main ."

    Public Sub Main()
        Dim frm As New frmProgress
        Try
            Dim name As String = Assembly.GetExecutingAssembly().GetName().Name
            Dim mutexHandle As IntPtr = CreateMutex(IntPtr.Zero, True, name)
            Dim [error] As Long = Marshal.GetLastWin32Error()
            If [error] <> ERROR_ALREADY_EXISTS Then
                frm.AutoScroll = False
                frm.ShowDialog()
            Else
                Throw New Exception("Application Already Running.")
            End If
            ReleaseMutex(mutexHandle)
            frm.Dispose() : frm = Nothing
        Catch ex As Exception
            frm.Dispose() : frm = Nothing
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
    End Sub

    Public Function LoadSetting() As DataTable
        Dim dt As DataTable = New DataTable
        Dim dbReader As SqlCeDataReader = Nothing

        Try

            Dim sSQL As String = String.Format("SELECT * FROM {0}", TblSettingDb)
            dbReader = OpenRecordset(sSQL, objConn)
            dt.Load(dbReader)

            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("SettingCode") = "SCNID" Then
                    gScannerID = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "SCN_NO" Then
                    gSCNNo = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "PREFIX" Then
                    gScnPrefix = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "SUFFIX" Then
                    gScnSuffix = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "URLDCSJSP" Then
                    gStrDCSWebServiceURL = dt.Rows(i).Item("SettingValue").ToString
                    ws_dcsClient.Url = gStrDCSWebServiceURL
                ElseIf dt.Rows(i).Item("SettingCode") = "URLORACLECHCK" Then
                    gStrOracleChckWebServiceURL = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "URLORACLECP" Then
                    gStrOracleCpWebServiceURL = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "URLORAUSERID" Then
                    gStrOraUserID = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "URLORAUSERPWD" Then
                    gStrOraUserPwd = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "DBPATH" Then
                    gDBPath = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "DBNAME" Then
                    gDatabaseName = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "DBPASSWORD" Then
                    gDatabasePwd = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "ORG_ID" Then
                    org_ID = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "INTERVAL" Then
                    iInterval = dt.Rows(i).Item("SettingValue")
                ElseIf dt.Rows(i).Item("SettingCode") = "AUTUSERID" Then
                    loginUser = dt.Rows(i).Item("SettingValue")
                ElseIf dt.Rows(i).Item("SettingCode") = "AUTPWD" Then
                    loginPass = dt.Rows(i).Item("SettingValue")
                End If
            Next
            dbReader.Close()

            ConnStr = String.Format("Data Source={0}{1};password={2}", gDBPath, gDatabaseName, gDatabasePwd)
            InitWebServices()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
        Return dt
    End Function

    Public Sub InitWebServices()

        ws_validationClient.Url = gStrOracleChckWebServiceURL
        ws_validationClient.PreAuthenticate = True
        ws_validationClient.Credentials = New NetworkCredential(gStrOraUserID, gStrOraUserPwd)
        ws_validationClient.eaiHeaderValue = New ValidationWebService.eaiHeader

        ws_inventoryClient.Url = gStrOracleCpWebServiceURL
        ws_inventoryClient.PreAuthenticate = True
        ws_inventoryClient.Credentials = New NetworkCredential(gStrOraUserID, gStrOraUserPwd)
        Dim header As New InventoryConsumptionWebService.eaiHeader
        header.from = "DCS"
        header.msgId = "0"
        ws_inventoryClient.eaiHeaderValue = header
    End Sub

#End Region

#Region ". Custom Exception ."

    Public Class CustomException
        Inherits Exception

        Public Sub New()
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub
    End Class

#End Region

#Region "Single Instance Application"

    <DllImport("coredll.dll", SetLastError:=True)> _
    Function CreateMutex(ByVal Attr As IntPtr, ByVal Own As Boolean, ByVal Name As String) As IntPtr
    End Function

    <DllImport("coredll.dll", SetLastError:=True)> _
    Function ReleaseMutex(ByVal hMutex As IntPtr) As Boolean
    End Function

    Const ERROR_ALREADY_EXISTS As Long = 183

#End Region

End Module
