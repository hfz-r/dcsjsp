Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlServerCe
Imports System.Collections
Imports System.Windows.Forms
Imports System.Data.Common
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports DCSJSP.clsDataTransfer
Imports System.Net

Module GeneralFunction

    Dim clsDataTransfer As New clsDataTransfer

	Public PlaySound As Boolean = False
	Public gErrorSoundDuration As Integer = 150
	Public gErrorSoundFreq As Integer = 2670 '2670

    'System time structure used to pass to P/Invoke...
    <StructLayoutAttribute(LayoutKind.Sequential)> _
    Private Structure SYSTEMTIME
        Public year As Short
        Public month As Short
        Public dayOfWeek As Short
        Public day As Short
        Public hour As Short
        Public minute As Short
        Public second As Short
        Public milliseconds As Short
    End Structure

    'P/Invoke dec for setting the system time...
    <DllImport("coredll.dll")> _
    Private Function SetLocalTime(ByRef time As SYSTEMTIME) As Boolean
    End Function

    Public Sub SetDeviceTime(ByVal SQLServerTime As String)
         Try
            Dim p_NewDate As Date = CDate(SQLServerTime)
            Dim st As SYSTEMTIME
            st.year = CShort(p_NewDate.Year)
            st.month = CShort(p_NewDate.Month)
            st.dayOfWeek = CShort(p_NewDate.DayOfWeek)
            st.day = CShort(p_NewDate.Day)
            st.hour = CShort(p_NewDate.Hour)
            st.minute = CShort(p_NewDate.Minute)
            st.second = CShort(p_NewDate.Second)
            st.milliseconds = CShort(p_NewDate.Millisecond)
            'Set the new time...
            SetLocalTime(st)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SetDeviceTime")
        Finally
            ' If Not IsNothing(rWebSync) Then rWebSync.Dispose() : rWebSync = Nothing
        End Try
    End Sub

	Public Sub PlayErrorSound()
		Try
			If PlaySound Then Exit Sub
			Application.DoEvents()
			'If Not IsNothing(MyAudioController) Then MyAudioController.PlayAudio(gErrorSoundDuration, gErrorSoundFreq) 'play Default beep
            'If Not IsNothing(MyAudioController) Then MyAudioController.PlayWaveFile(DefaultAppPath & "\Beep.wav")
			Threading.Thread.Sleep(gErrorSoundDuration)
		Catch ex As Exception
		Finally
			PlaySound = False
		End Try
	End Sub

	Public Sub ErrorBox(ByVal xMsg As String, ByVal xType As Microsoft.VisualBasic.MsgBoxStyle, Optional ByVal xTitle As String = "")
		PlayErrorSound()
		MsgBox(xMsg, xType, xTitle)
	End Sub

	Public Sub SuccessBox(ByVal xMsg As String, ByVal xType As Microsoft.VisualBasic.MsgBoxStyle, Optional ByVal xTitle As String = "")
		MsgBox(xMsg, xType, xTitle)
    End Sub

    'Public Sub ClsSqlRdr(ByVal pRdr As SqlCeDataReader)
    '    Try
    '        If Not IsNothing(pRdr) Then pRdr.Dispose() : pRdr = Nothing
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "ClsSqlRdr")
    '    End Try
    'End Sub

    Public Function getData(ByVal sSQL As String) As DataTable
        Dim dt As DataTable = New DataTable
        Try
            Dim dbReader As SqlCeDataReader = Nothing
            dbReader = OpenRecordset(sSQL, objConn)
            dt.Load(dbReader)
            dbReader.Close()
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

    Public Function postData(ByVal batchID As String, ByVal processType As Integer, ByRef msgCode As String, ByRef msgDesc As String) As Boolean
        postData = False
        Try
            If batchID = "" Or batchID = "NULL" Then
                msgCode = "F"
                msgDesc = "Batch ID is empty."
                Exit Function
            End If

            If processType = 0 Then
                msgCode = "F"
                msgDesc = "Incorrect Process Type"
                Exit Function
            End If

            Dim check As String = gStrDCSWebServiceURL

            If gStrDCSWebServiceURL.Contains("http://172.20.1") And gStrDCSWebServiceURL.Contains(":8084") Then
                'DEVELOPMENT BY TOSHIBA TEC
                'IP PC LIZA
                msgCode = "0" 'dummy for development purpose
                msgDesc = "Success"
            ElseIf gStrDCSWebServiceURL.Contains("http://192.168.0") And gStrDCSWebServiceURL.Contains(":8084") Then
                msgCode = "0" 'dummy for development purpose
                msgDesc = "Success"
            Else
                'LIVE PERODUA
                'msgCode = ws_oracleClient.processServicePart(batchID, processType.ToString, msgDesc)
            End If

            If msgCode = "0" And msgDesc = "Success" Then
                postData = True
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
    End Function

    Public Function formatPartNo(ByRef partNo As String, ByVal partNoLength As Integer) As Boolean
        formatPartNo = False
        Try
            If partNo.Length = PartNoLength Then
                Dim p1 As String = partNo.Substring(0, 5)
                Dim p2 As String = partNo.Substring(5, 5)
                Dim p3 As String = partNo.Substring(10, 2)
                partNo = p1 & "-" & p2 & "-" & p3
                formatPartNo = True
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
    End Function
    

    Public Function deleteTransaction(ByVal tableName As String, Optional ByVal where As String = "AND 1=1") As Boolean
        deleteTransaction = False
        Try
            'If ExecuteSQL("DELETE FROM [" & tableName & "] WHERE 1=1 " & where) Then
            '    deleteTransaction = True
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
    End Function

    Public Sub SortDataTable(ByRef dt As DataTable, ByVal sortStr As String)
        Dim dataView As New DataView(dt)
        dataView.Sort = " RB_TYPE DESC, RB_SERIAL_NO DESC"
        Dim dttemp As DataTable = dataView.ToTable()
        dt.Clear()
        dt = dttemp
        dttemp.Dispose()
    End Sub

    Public Function TranxTbl_isAllPosted(ByVal pStrAnyColumn_f As String, ByVal pStrTableName_I As String, ByRef rStrErrorMsg As String) As Boolean
        TranxTbl_isAllPosted = False
        Try
            Dim dt As DataTable = New DataTable

            'dt = getData("select " & pStrAnyColumn_f & " from " & pStrTableName_I & " where POST_FLAG <> 'Y'")
            If dt.Rows.Count <= 0 Then
                TranxTbl_isAllPosted = True
            End If
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            rStrErrorMsg = ex.Message
        End Try
    End Function

#Region "Load Main"

    Public Sub Main()

        Dim frm As New frmProgress
        frm.AutoScroll = False
        frm.ShowDialog()
        frm.Dispose() : frm = Nothing

    End Sub

    Public Function checkConnection() As Boolean

    End Function

    Public Function LoadSetting() As DataTable
        Dim dt As DataTable = New DataTable
        Dim dbReader As SqlCeDataReader = Nothing

        Try

            Dim sSQL As String = "SELECT * FROM TBLSETTING "
            dbReader = OpenRecordset(sSQL, objConn)
            dt.Load(dbReader)

            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("SettingCode") = "SCNID" Then
                    gScannerID = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "PREFIX" Then
                    gScnPrefix = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "SUFFIX" Then
                    gScnSuffix = dt.Rows(i).Item("SettingValue").ToString
                ElseIf dt.Rows(i).Item("SettingCode") = "URLDCSSP" Then
                    gStrDCSWebServiceURL = dt.Rows(i).Item("SettingValue").ToString
                    Dim ws_dcsClient As New DCSWebService.DCSWebService
                ElseIf dt.Rows(i).Item("SettingCode") = "URLORACLE" Then
                    gStrOracleWebServiceURL = dt.Rows(i).Item("SettingValue").ToString
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
                ElseIf dt.Rows(i).Item("SettingCode") = "INTERVAL" Then
                    iInterval = dt.Rows(i).Item("SettingValue")

                    '-- import
                    'ElseIf dt.Rows(i).Item("SettingCode") = "USER" Then
                    '    sUser = dt.Rows(i).Item("SettingValue").ToString
                    'ElseIf dt.Rows(i).Item("SettingCode") = "REASON" Then
                    '    sReason = dt.Rows(i).Item("SettingValue").ToString
                    'ElseIf dt.Rows(i).Item("SettingCode") = "RBTYPE" Then
                    '    sRBType = dt.Rows(i).Item("SettingValue").ToString
                    'ElseIf dt.Rows(i).Item("SettingCode") = "CASETYPE" Then
                    '    sCaseType = dt.Rows(i).Item("SettingValue").ToString
                    'ElseIf dt.Rows(i).Item("SettingCode") = "CUSTOMER" Then
                    '    sCustomer = dt.Rows(i).Item("SettingValue").ToString
                    'ElseIf dt.Rows(i).Item("SettingCode") = "IMPORTER" Then
                    '    sImporter = dt.Rows(i).Item("SettingValue").ToString
                    'ElseIf dt.Rows(i).Item("SettingCode") = "VENDOR" Then
                    '    sVendor = dt.Rows(i).Item("SettingValue").ToString
                    'ElseIf dt.Rows(i).Item("SettingCode") = "STOPPERTYPE" Then
                    '    sStopperType = dt.Rows(i).Item("SettingValue").ToString
                End If
            Next
            dbReader.Close()

            ConnStr = "Data Source=" & gDBPath + gDatabaseName & ";password=" & gDatabasePwd

            'ws_oracleClient.Url = gStrOracleWebServiceURL
            'Dim oraCred As NetworkCredential = New NetworkCredential(gStrOraUserID, gStrOraUserPwd)
            ''Dim oraCache As CredentialCache = New CredentialCache
            ''oraCache.Add(New Uri("www.contoso.com"), "Basic", myCred)
            ''oraCache.Add(New Uri("app.contoso.com"), "Basic", myCred)
            'ws_oracleClient.Credentials = oraCred
            'ws_oracleClient.PreAuthenticate = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gAppName)
        End Try
        Return dt
    End Function

#End Region

End Module
