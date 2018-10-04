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

        ErrLoc = String.Format(".ImportMaster {0}", TableName)
        progressBar.Value = progressBar.Value + 10

        '********************
        'Prepare Insert Statement 
        '********************
        Try

            lblMessage.Text = String.Format("Reading {0}", TableName)
            Application.DoEvents()

            Select Case TableName.ToUpper.Trim
                Case TblUserDb
                    dsSet = New DataSet
                    dsSet = OpenRecordset(String.Format("SELECT LOGIN_ID FROM [{0}]", TableName))
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL(String.Format("DELETE FROM [{0}]", TableName))
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("LOGIN_ID, PASSWORD", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then

                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = String.Format("INSERT INTO [{0}] (LOGIN_ID, PASSWORD) ", TableName)
                            sSQL = String.Format("{0} VALUES({1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("LOGIN_ID").ToString))
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("PASSWORD").ToString)
                            sSQL = String.Format("{0})", sSQL)
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox(String.Format("Failed Import To {0}", TableName), MsgBoxStyle.Critical, "Import")
                                ImpUser = False
                                If ImpUser Then
                                    sUser = "N"
                                    sSQL = Nothing
                                    sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'USER'", SQLQuote(sUser.Trim))
                                    ExecuteSQL(sSQL)
                                End If
                                Exit Function
                            Else
                                ImpUser = True
                            End If
                        Next
                        If ImpUser = True Then
                            sUser = "Y"
                            sSQL = Nothing
                            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'USER'", SQLQuote(sUser.Trim))
                            ExecuteSQL(sSQL)
                        End If
                    End If

                Case "JSP_ORGANIZATION_HEADERS_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset(String.Format("SELECT ORG_ID FROM [{0}]", TableName))
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL(String.Format("DELETE FROM [{0}]", TableName))
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("ORG_ID, ORG_NAME", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = String.Format("INSERT INTO [{0}] (ORG_ID, ORG_NAME) ", TableName)
                            sSQL = String.Format("{0} VALUES ({1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString))
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_NAME").ToString)
                            sSQL = String.Format("{0})", sSQL)
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox(String.Format("Failed Import To {0}", TableName), MsgBoxStyle.Critical, "Import")
                                ImpOrganization = False
                                If ImpOrganization Then
                                    sOrganization = "N"
                                    sSQL = Nothing
                                    sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'ORGANIZATION'", SQLQuote(sOrganization.Trim))
                                End If
                                Exit Function
                            Else
                                ImpOrganization = True
                            End If
                        Next
                        If ImpOrganization = True Then
                            sOrganization = "Y"
                            sSQL = Nothing
                            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'ORGANIZATION'", SQLQuote(sOrganization.Trim))
                            ExecuteSQL(sSQL)
                        End If
                    End If

                Case "JSP_SUPPLY_BP_HEADERS_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset(String.Format("SELECT SHOP_ID FROM [{0}]", TableName))
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL(String.Format("DELETE FROM [{0}]", TableName))
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("SHOP_ID, SHOP_NAME, ORG_ID", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = String.Format("INSERT INTO [{0}] (SHOP_ID, SHOP_NAME, ORG_ID) ", TableName)
                            sSQL = String.Format("{0} VALUES ({1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("SHOP_ID").ToString))
                            sSQL = String.Format("{0}{1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("SHOP_NAME").ToString))
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString)
                            sSQL = String.Format("{0})", sSQL)
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox(String.Format("Failed Import To {0}", TableName), MsgBoxStyle.Critical, "Import")
                                ImpShop = False
                                If ImpShop Then
                                    sShop = "N"
                                    sSQL = Nothing
                                    sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'SHOP'", SQLQuote(sShop.Trim))
                                End If
                                Exit Function
                            Else
                                ImpShop = True
                            End If
                        Next
                        If ImpShop = True Then
                            sShop = "Y"
                            sSQL = Nothing
                            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'SHOP'", SQLQuote(sShop.Trim))
                            ExecuteSQL(sSQL)
                        End If
                    End If

                Case "JSP_SUPPLY_CP_HEADERS_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset(String.Format("SELECT VENDOR_ID FROM [{0}]", TableName))
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL(String.Format("DELETE FROM [{0}]", TableName))
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("VENDOR_ID, VENDOR_NAME, ORG_ID", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = String.Format("INSERT INTO [{0}] (VENDOR_ID, VENDOR_NAME, ORG_ID) ", TableName)
                            sSQL = String.Format("{0} VALUES ({1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("VENDOR_ID").ToString))
                            sSQL = String.Format("{0}{1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("VENDOR_NAME").ToString))
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString)
                            sSQL = String.Format("{0})", sSQL)
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox(String.Format("Failed Import To {0}", TableName), MsgBoxStyle.Critical, "Import")
                                ImpSupplier = False
                                If ImpSupplier Then
                                    sSupply = "N"
                                    sSQL = Nothing
                                    sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'SUPPLIER'", SQLQuote(sSupply.Trim))
                                End If
                                Exit Function
                            Else
                                ImpSupplier = True
                            End If
                        Next
                        If ImpSupplier = True Then
                            sSupply = "Y"
                            sSQL = Nothing
                            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'SUPPLIER'", SQLQuote(sSupply.Trim))
                            ExecuteSQL(sSQL)
                        End If
                    End If

                Case "JSP_ABNORMAL_REASON_CODE_VIEW"
                    dsSet = New DataSet
                    dsSet = OpenRecordset(String.Format("SELECT REASON_CODE FROM [{0}]", TableName))
                    If dsSet.Tables(0).Rows.Count <> 0 Then
                        ExecuteSQL(String.Format("DELETE FROM [{0}]", TableName))
                        dsSet.Clear() : dsSet = Nothing
                    End If

                    '---- Get data from WebService put into datatable -------------
                    dtMasterImport = ws_dcsClient.getData("REASON_CODE, REASON, ORG_ID", TableName, "")
                    If dtMasterImport.Rows.Count > 0 Then
                        progressBar.Value = 0
                        Application.DoEvents()

                        '---- Insert data into local database from datatable -------------
                        For i As Integer = 0 To dtMasterImport.Rows.Count - 1
                            sSQL = String.Format("INSERT INTO [{0}] (REASON_CODE, REASON, ORG_ID) ", TableName)
                            sSQL = String.Format("{0} VALUES ({1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("REASON_CODE").ToString))
                            sSQL = String.Format("{0}{1} , ", sSQL, SQLQuote(dtMasterImport.Rows(i).Item("REASON").ToString))
                            sSQL = sSQL & SQLQuote(dtMasterImport.Rows(i).Item("ORG_ID").ToString)
                            sSQL = String.Format("{0})", sSQL)
                            If ExecuteSQL(sSQL) = False Then
                                MsgBox(String.Format("Failed Import To {0}", TableName), MsgBoxStyle.Critical, "Import")
                                ImpReason = False
                                If ImpReason Then
                                    sReason = "N"
                                    sSQL = Nothing
                                    sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'REASON'", SQLQuote(sReason.Trim))
                                End If
                                Exit Function
                            Else
                                ImpReason = True
                            End If
                        Next
                        ImpReason = True
                        If ImpReason Then
                            sReason = "Y"
                            sSQL = Nothing
                            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'REASON'", SQLQuote(sReason.Trim))
                            ExecuteSQL(sSQL)
                        End If
                    End If

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
#End Region

End Class
