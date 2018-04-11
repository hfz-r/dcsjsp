Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Text.RegularExpressions

Public Class frmSetting

    Dim sSQL As String = ""
    Dim bConnected As Boolean = False
    Dim gFullPath As String = gProgPath + gDatabaseName
    Dim IsOnline As Boolean = False
    Friend cn As New clsConnection

    Public Sub Init()
        ' Need to create Offline mode and Online mode import data from Master to localDB before Login Dialog
        Try
            If ws_dcsClient.isConnected() Then
                If ws_dcsClient.isOracleConnected() Then
                    IsOnline = True
                    MsgBox("Connection Status: Online.")
                Else
                    IsOnline = False
                End If
            End If

            If IsOnline = False Then
                MsgBox("Connection Status: Offline.")
            End If
        Catch ex As Exception
            MsgBox("Connection is not resolved!", MsgBoxStyle.Critical, "No Connection")
        End Try


        bringPanelToFront(pnlSetting, pnlAuthentication)

        TPDatabase()
        TPInterval()
        TPImport()
        TPWebService()
        TPBatchID()
        TPScanner()
        TPOrganization()
        'bringPanelToFront(pnlSetting, pnlAuthentication)
        'txtAUTPWD.Focus()
    End Sub

    Private Sub btnAuthenBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        boolsetting = True
        Me.Close()
    End Sub

    'Private Sub btnAuthenVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthenVerify.Click

    '    If Not System.IO.File.Exists(gDBPath + gDatabaseName) Then
    '        MsgBox("No database.Failed to update setting.", MsgBoxStyle.Exclamation, "Service Part")
    '        Exit Sub
    '    End If

    '    Dim dt As DataTable = New DataTable

    '    Try
    '        If txtAUTPWD.Text = "" Then
    '            MsgBox("Please enter your password!", MsgBoxStyle.Critical, "Service Part")
    '            txtAUTPWD.Focus()
    '            txtAUTPWD.SelectAll()
    '            Exit Sub
    '        End If

    '        dt = getData("SELECT SettingValue FROM TBLSetting WHERE SettingValue = '" & txtAUTPWD.Text.Trim & "' AND SettingCode = 'AUTPWD'")

    '        If dt.Rows.Count > 0 Then
    '            FillDataSetting()

    '            bringPanelToFront(pnlSetting, pnlAuthentication)
    '            TabSetting.SelectedIndex = 0
    '        Else
    '            MsgBox("Wrong password! Please re-enter password.", MsgBoxStyle.Critical, "Service Part")
    '            txtAUTPWD.Focus()
    '            txtAUTPWD.SelectAll()
    '            Exit Sub
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
    '    End Try
    'End Sub

    Private Sub FillDataSetting()

        Try
            txtSTSCNID.Text = getDeviceID()

            Dim dtSetting As DataTable = New DataTable

            sSQL = "SELECT SettingCode, SettingValue FROM TBLSetting"
            dtSetting = getData(sSQL)

            For i As Integer = 0 To dtSetting.Rows.Count - 1
                If dtSetting.Rows(i).Item("SettingCode") = "SCNID" Then
                    txtSTSCNID.Text = dtSetting.Rows(i).Item("SettingValue").ToString
                    If dtSetting.Rows(i).Item("SettingCode") = "AUTPWD" Then
                        txtSTPassword.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLDCSSP" Then
                        txtSTWSDCSSP.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORACLE" Then
                        txtSTWSOracle.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORAUSERID" Then
                        txtSTWSORAUserID.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORAUSERPWD" Then
                        txtSTWSORAUserPwd.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "DBPATH" Then
                        txtDBPath.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "DBNAME" Then
                        txtDBName.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "DBPASSWORD" Then
                        txtDBPwd.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "SCHEDULE" Then
                        cboImpDay.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "USER" Then
                        sUser = dtSetting.Rows(i).Item("SettingValue").ToString
                        If sUser = "Y" Then chkUser = True Else chkUser = False

                        'ElseIf dtSetting.Rows(i).Item("SettingCode") = "REASON" Then
                        '    sReason = dtSetting.Rows(i).Item("SettingValue").ToString
                        '    If sReason = "Y" Then chkReason = True Else chkReason = False

                        'ElseIf dtSetting.Rows(i).Item("SettingCode") = "RBTYPE" Then
                        '    sRBType = dtSetting.Rows(i).Item("SettingValue").ToString
                        '    If sRBType = "Y" Then chkRBType = True Else chkRBType = False

                        'ElseIf dtSetting.Rows(i).Item("SettingCode") = "CASETYPE" Then
                        '    sCaseType = dtSetting.Rows(i).Item("SettingValue").ToString
                        '    If sCaseType = "Y" Then chkCaseType = True Else chkCaseType = False

                        'ElseIf dtSetting.Rows(i).Item("SettingCode") = "CUSTOMER" Then
                        '    sCustomer = dtSetting.Rows(i).Item("SettingValue").ToString
                        '    If sCustomer = "Y" Then chkCustomer = True Else chkCustomer = False

                        'ElseIf dtSetting.Rows(i).Item("SettingCode") = "IMPORTER" Then
                        '    sImporter = dtSetting.Rows(i).Item("SettingValue").ToString
                        '    If sImporter = "Y" Then chkImporter = True Else chkImporter = False

                        'ElseIf dtSetting.Rows(i).Item("SettingCode") = "VENDOR" Then
                        '    sVendor = dtSetting.Rows(i).Item("SettingValue").ToString
                        '    If sVendor = "Y" Then chkVendor = True Else chkVendor = False

                        'ElseIf dtSetting.Rows(i).Item("SettingCode") = "STOPPERTYPE" Then
                        '    sStopperType = dtSetting.Rows(i).Item("SettingValue").ToString
                        '    If sStopperType = "Y" Then chkStopperType = True Else chkStopperType = False

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "SCN_NO" Then
                        txtSTBatchID.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                    ElseIf dtSetting.Rows(i).Item("SettingCode") = "INTERVAL" Then
                        txtSTInterval.Text = dtSetting.Rows(i).Item("SettingValue").ToString
                    End If
                End If
            Next

            LoadSetting()

        Catch ex As Exception
            Dim test As String = ex.ToString
            MsgBox("Failed to fill data setting !" & ex.ToString.Substring(ex.ToString.Length - 100), MsgBoxStyle.Critical, "FillDataSetting")
        End Try

    End Sub

    Private Sub btnSTBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTBack.Click
        'bringPanelToFront(pnlAuthentication, pnlSetting)
        bringPanelToFront(pnlSetting, pnlAuthentication)
        'txtAUTPWD.Text = ""
        'txtAUTPWD.Focus()
        Me.Close()
    End Sub

    Private Sub btnSTAUTPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTAUTPwd.Click
        Try
            If txtSTPassword.Text = "" Or txtSTUsername.Text = "" Then
                MsgBox("Username or Password cannot be blank!", MsgBoxStyle.Critical, "Service Part")
                txtSTPassword.Focus()
                txtSTPassword.SelectAll()
                Exit Sub
            End If

            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTPassword.Text.Trim) & " WHERE SettingCode = 'AUTPWD' "
            ExecuteSQL(sSQL)

            sSQL = "UPDATE SEP_LOGIN_V SET LOGIN_ID = " & SQLQuote(txtSTUsername.Text.Trim) & " WHERE LOGIN_ID = '" + txtSTUsername.Text + "'"
            If ExecuteSQL(sSQL) = False Then
                MsgBox("")
            End If

            sSQL = "UPDATE SEP_LOGIN_V SET PASSWORD = " & SQLQuote(txtSTPassword.Text.Trim) & " WHERE LOGIN_ID = '" + txtSTUsername.Text + "'"
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, "Service Part")

            FillDataSetting()

        Catch ex As Exception
            MsgBox("Failed to update authorize password!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
        End Try
    End Sub

    Private Sub txtSTWSSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSTWSSave.Click
        Try
            If txtSTWSDCSSP.Text = "" Then
                MsgBox("Please enter DCSSP Web Service!", MsgBoxStyle.Critical, "Service Part")
                txtSTWSDCSSP.Focus()
                txtSTWSDCSSP.SelectAll()
                Exit Sub
            End If

            If txtSTWSOracle.Text = "" Then
                MsgBox("Please enter Oracle Web Service!", MsgBoxStyle.Critical, "Service Part")
                txtSTWSOracle.Focus()
                txtSTWSOracle.SelectAll()
                Exit Sub
            End If

            Dim regex = New Regex("http[s]?://(([^/:\.[:space:]]+(\.[^/:\.[:space:]]+)*)|([0-9](\.[0-9]{3})))(:[0-9]+)?((/[^?#[:space:]]+)(\?[^#[:space:]]+)?(\#.+)?)?")
            If Not Regex.IsMatch(txtSTWSDCSSP.Text) Then
                MsgBox("Invalid DCSSP URL Had Entered, Please Retry!", MsgBoxStyle.Critical, "Service Part")
                txtSTWSDCSSP.Focus()
                txtSTWSDCSSP.SelectAll()
                Exit Sub
            End If

            If Not Regex.IsMatch(txtSTWSOracle.Text) Then
                MsgBox("Invalid Oracle URL Had Entered, Please Retry!", MsgBoxStyle.Critical, "Service Part")
                txtSTWSOracle.Focus()
                txtSTWSOracle.SelectAll()
                Exit Sub
            End If
            Regex = Nothing

            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSDCSSP.Text.Trim) & " WHERE SettingCode = 'URLDCSSP' "
            ExecuteSQL(sSQL)

            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSOracle.Text.Trim) & " WHERE SettingCode = 'URLORACLE' "
            ExecuteSQL(sSQL)

            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSORAUserID.Text.Trim) & " WHERE SettingCode = 'URLORAUSERID' "
            ExecuteSQL(sSQL)

            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSORAUserPwd.Text.Trim) & " WHERE SettingCode = 'URLORAUSERPWD' "
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, "Service Part")

            FillDataSetting()

        Catch ex As Exception
            MsgBox("Failed to update web services!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
        End Try
    End Sub

    Private Sub btnInterSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInterSave.Click
        Try
            If txtSTInterval.Text = "" Then
                MsgBox("Please enter Scanner No!", MsgBoxStyle.Critical, "Service Part")
                txtSTInterval.Focus()
                txtSTInterval.SelectAll()
                Exit Sub
            End If

            If Not IsNumeric(txtSTInterval.Text) Then
                MsgBox("Number Only!", MsgBoxStyle.Critical, "Service Part")
                txtSTInterval.Focus()
                txtSTInterval.SelectAll()
                Exit Sub
            End If

            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTInterval.Text.Trim) & " WHERE SettingCode = 'INTERVAL' "
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, "Service Part")

            FillDataSetting()

        Catch ex As Exception
            MsgBox("Failed to update!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
        End Try
    End Sub

    Private Sub btnCloseLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseLogin.Click
        Me.Close()
    End Sub

    Private Sub btnSubmitLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitLogin.Click
        Dim dt As DataTable = New DataTable
        Dim result As Boolean = False
        If Not String.IsNullOrEmpty(txtAUTUNM.Text) Then
            Dim sSQL As String = "SELECT LOGIN_ID, PASSWORD FROM SEP_LOGIN_V WHERE LOGIN_ID = '" + txtAUTUNM.Text + "'"
            dt = getData(sSQL)
            'dt = ws_dcsClient.getData("LOGIN_ID, PASSWORD", "SEP_LOGIN_V", " AND LOGIN_ID = '" + txtAUTUNM.Text + "'")
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(1) = txtAUTPWD.Text Then
                    result = True
                    bringPanelToFront(pnlSetting, pnlAuthentication)
                    TPAuthorize(txtAUTUNM.Text)
                End If
            Next
            If result = False Then
                MsgBox("Invalid Account")
            End If
        End If
    End Sub

    Private Sub btnDBBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDBBrowse.Click

    End Sub

    Private Sub btnImportSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportSave.Click
        Try
            'If chkCaseType = False And chkCustomer = False And chkImporter = False And chkRBType = False And chkReason = False And chkStopperType = False And chkUser = False And chkVendor = False Then
            '    MsgBox("You Must Select At Least ONE Master for Batch Import!", MsgBoxStyle.Critical, "Service Part")
            '    Exit Sub
            'End If

            sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(cboImpDay.Text) & " WHERE SettingCode = 'SCHEDULE' "
            ExecuteSQL(sSQL)

            'If chkUser = True Then sUser = "Y" Else sUser = "N"
            'sUser = "Y"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sUser) & " WHERE SettingCode = 'USER' "
            'ExecuteSQL(sSQL)

            'If chkReason = True Then sReason = "Y" Else sReason = "N"
            'sReason = "Y"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sReason) & " WHERE SettingCode = 'REASON' "
            'ExecuteSQL(sSQL)

            'If chkRBType = True Then sRBType = "Y" Else sUser = "N"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sRBType) & " WHERE SettingCode = 'RBTYPE' "
            'ExecuteSQL(sSQL)

            'If chkCaseType = True Then sCaseType = "Y" Else sCaseType = "N"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sCaseType) & " WHERE SettingCode = 'CASETYPE' "
            'ExecuteSQL(sSQL)

            'If chkCustomer = True Then sCustomer = "Y" Else sCustomer = "N"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sCustomer) & " WHERE SettingCode = 'CUSTOMER' "
            'ExecuteSQL(sSQL)

            'If chkImporter = True Then sImporter = "Y" Else sImporter = "N"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sImporter) & " WHERE SettingCode = 'IMPORTER' "
            'ExecuteSQL(sSQL)

            'If chkVendor = True Then sVendor = "Y" Else sVendor = "N"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sVendor) & " WHERE SettingCode = 'VENDOR' "
            'ExecuteSQL(sSQL)

            'If chkStopperType = True Then sStopperType = "Y" Else sStopperType = "N"
            'sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sStopperType) & " WHERE SettingCode = 'STOPPERTYPE' "
            'ExecuteSQL(sSQL)

            MsgBox("Successful updated!", MsgBoxStyle.Information, "Service Part")

            FillDataSetting()

        Catch ex As Exception
            MsgBox("Failed to update!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
        End Try
    End Sub

#Region ". Tab Pages ."

    Private Sub TPDatabase()
        Try
            If System.IO.File.Exists(gFullPath) Then
                Dim dt As DataTable = New DataTable
                Dim sSQL As String = "SELECT * FROM TBLSetting"
                dt = getData(sSQL)

                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item(2) = "DBPATH" Then
                        txtDBPath.Text = dt.Rows(i).Item(3).ToString()
                    ElseIf dt.Rows(i).Item(2) = "DBNAME" Then
                        txtDBName.Text = dt.Rows(i).Item(3).ToString()
                    ElseIf dt.Rows(i).Item(2) = "DBPASSWORD" Then
                        txtDBPwd.Text = dt.Rows(i).Item(3).ToString()
                    End If
                Next
            Else
                MsgBox("TPDatabase failed to update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub TPInterval()
        Try
            If System.IO.File.Exists(gFullPath) Then
                Dim dt As DataTable = New DataTable
                Dim sSQL As String = "SELECT * FROM TBLSetting"
                dt = getData(sSQL)
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item(2) = "INTERVAL" Then
                        txtSTInterval.Text = dt.Rows(i).Item(3).ToString()
                    End If
                Next
            Else
                MsgBox("TPInterval failed to update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub TPOrganization()
        ' NEED TO IMPORT THIS DATA TO OFFLINE MODE USE
        Dim dt As DataTable = New DataTable
        If IsOnline = True Then
            Try
                dt = ws_dcsClient.getData("ORG_NAME", "JSP_ORGANIZATION_HEADERS", "")
                For i As Integer = 0 To dt.Rows.Count - 1
                    cboOrganization.Items.Add(dt.Rows(i).Item(0).ToString())
                Next

                If String.IsNullOrEmpty(cboOrganization.Text) Then
                    cboOrganization.Text = cboOrganization.Items.Item(0)
                End If
            Catch ex As Exception
                MsgBox("TPOrganization failed to update. Error: " + ex.Message.ToString())
            End Try
        Else
            Try
                Dim sSQL As String = "SELECT * FROM JSP_ORGANIZATION_HEADERS"
                dt = getData(sSQL)
                For i As Integer = 0 To dt.Rows.Count - 1
                    cboOrganization.Items.Add(dt.Rows(i).Item(2).ToString())
                Next

                If String.IsNullOrEmpty(cboOrganization.Text) Then
                    cboOrganization.Text = cboOrganization.Items.Item(0)
                End If
            Catch ex As Exception
                MsgBox("TPOrganization failed to update! Error: " + ex.Message.ToString())
            End Try
        End If
        
    End Sub

    Private Sub TPImport()
        Try
            If System.IO.File.Exists(gFullPath) Then
                Dim dt As DataTable = New DataTable
                Dim sSQL As String = "SELECT * FROM TBLSetting"
                dt = getData(sSQL)
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item(2) = "SCHEDULE" Then
                        cboImpDay.Text = dt.Rows(i).Item(3).ToString()
                    End If
                Next
            Else
                MsgBox("TPImport failed to update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub TPBatchID()
        Try
            If System.IO.File.Exists(gFullPath) Then
                Dim dt As DataTable = New DataTable
                Dim sSQL As String = "SELECT * FROM TBLSetting"
                dt = getData(sSQL)
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item(2) = "SCN_NO" Then
                        txtSTBatchID.Text = dt.Rows(i).Item(3).ToString()
                    End If
                Next
            Else
                MsgBox("TPBatchID failed to update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub TPWebService()
        Try
            If System.IO.File.Exists(gFullPath) Then
                Dim dt As DataTable = New DataTable
                Dim sSQL As String = "SELECT * FROM TBLSetting"
                dt = getData(sSQL)
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item(2) = "URLDCSSP" Then
                        txtSTWSDCSSP.Text = dt.Rows(i).Item(3).ToString()
                    ElseIf dt.Rows(i).Item(2) = "URLORACLE" Then
                        txtSTWSOracle.Text = dt.Rows(i).Item(3).ToString()
                    ElseIf dt.Rows(i).Item(2) = "URLORAUSERID" Then
                        txtSTWSORAUserID.Text = dt.Rows(i).Item(3).ToString()
                    ElseIf dt.Rows(i).Item(2) = "URLORAUSERPWD" Then
                        txtSTWSORAUserPwd.Text = dt.Rows(i).Item(3).ToString()
                    End If
                Next
            Else
                MsgBox("TPWebService failed to update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub TPScanner()
        Try
            If System.IO.File.Exists(gFullPath) Then
                Dim dt As DataTable = New DataTable
                Dim sSQL As String = "SELECT * FROM TBLSetting"
                dt = getData(sSQL)
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item(2) = "SCNID" Then
                        txtSTSCNID.Text = dt.Rows(i).Item(3).ToString()
                    End If
                Next
            Else
                MsgBox("TPScanner failed to update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub TPAuthorize(ByVal LoginID As String)
        Try
            If System.IO.File.Exists(gFullPath) Then
                Dim DTLogin As DataTable = New DataTable
                Dim sSQL As String = "SELECT LOGIN_ID, PASSWORD FROM SEP_LOGIN_V WHERE LOGIN_ID = '" + LoginID + "'"
                DTLogin = getData(sSQL)
                For i As Integer = 0 To DTLogin.Rows.Count - 1
                    If DTLogin.Rows(i).Item(0) = LoginID Then
                        txtSTUsername.Text = LoginID
                        txtSTPassword.Text = DTLogin.Rows(i).Item(1).ToString()
                    End If
                Next
            Else
                MsgBox("TPAuthorize failed to update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

#End Region

    'Private Sub btnSTBTSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If txtSTBatchID.Text = "" Then
    '            MsgBox("Please enter Scanner No!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTBatchID.Focus()
    '            txtSTBatchID.SelectAll()
    '            Exit Sub
    '        End If

    '        If Not IsNumeric(txtSTBatchID.Text) Then
    '            MsgBox("Number Only!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTBatchID.Focus()
    '            txtSTBatchID.SelectAll()
    '            Exit Sub
    '        End If

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTBatchID.Text.Trim) & " WHERE SettingCode = 'SCN_NO' "
    '        ExecuteSQL(sSQL)

    '        MsgBox("Successful Updated!", MsgBoxStyle.Information, "Service Part")

    '        FillDataSetting()

    '    Catch ex As Exception
    '        MsgBox("Failed to update!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
    '    End Try
    'End Sub

    Private Sub btnOrgSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrgSave.Click

    End Sub
End Class
