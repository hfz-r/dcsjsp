Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Text.RegularExpressions

Public Class frmSetting

    Dim sSQL As String = ""
    Dim bConnected As Boolean = False
    Friend cn As New clsConnection

    Public Sub Init()
        bringPanelToFront(pnlAuthentication, pnlSetting)
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

    'Private Sub FillDataSetting()

    '    Try
    '        txtSTSCNID.Text = getDeviceID()

    '        Dim dtSetting As DataTable = New DataTable

    '        sSQL = "SELECT SettingCode, SettingValue FROM TBLSetting"
    '        dtSetting = getData(sSQL)

    '        For i As Integer = 0 To dtSetting.Rows.Count - 1
    '            'If dtSetting.Rows(i).Item("SettingCode") = "SCNID" Then
    '            'txtSTSCNID.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            If dtSetting.Rows(i).Item("SettingCode") = "AUTPWD" Then
    '                txtSTPassword.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLDCSSP" Then
    '                txtSTWSDCSSP.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORACLE" Then
    '                txtSTWSOracle.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORAUSERID" Then
    '                txtSTWSORAUserID.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORAUSERPWD" Then
    '                txtSTWSORAUserPwd.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "DBPATH" Then
    '                txtDBPath.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "DBNAME" Then
    '                txtDBName.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "DBPASSWORD" Then
    '                txtDBPwd.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "SCHEDULE" Then
    '                cboImpDay.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "USER" Then
    '                sUser = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sUser = "Y" Then chkImpUser.Checked = True Else chkImpUser.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "REASON" Then
    '                sReason = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sReason = "Y" Then chkImpReason.Checked = True Else chkImpReason.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "RBTYPE" Then
    '                sRBType = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sRBType = "Y" Then chkImpRBType.Checked = True Else chkImpRBType.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "CASETYPE" Then
    '                sCaseType = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sCaseType = "Y" Then chkImpCaseType.Checked = True Else chkImpCaseType.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "CUSTOMER" Then
    '                sCustomer = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sCustomer = "Y" Then chkImpCustomer.Checked = True Else chkImpCustomer.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "IMPORTER" Then
    '                sImporter = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sImporter = "Y" Then chkImpImporter.Checked = True Else chkImpImporter.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "VENDOR" Then
    '                sVendor = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sVendor = "Y" Then chkImpVendor.Checked = True Else chkImpVendor.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "STOPPERTYPE" Then
    '                sStopperType = dtSetting.Rows(i).Item("SettingValue").ToString
    '                If sStopperType = "Y" Then chkImpStopperType.Checked = True Else chkImpStopperType.Checked = False
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "SCN_NO" Then
    '                txtSTBatchID.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            ElseIf dtSetting.Rows(i).Item("SettingCode") = "INTERVAL" Then
    '                txtSTInterval.Text = dtSetting.Rows(i).Item("SettingValue").ToString
    '            End If
    '        Next

    '        LoadSetting()

    '    Catch ex As Exception
    '        Dim test As String = ex.ToString
    '        MsgBox("Failed to fill data setting !" & ex.ToString.Substring(ex.ToString.Length - 100), MsgBoxStyle.Critical, "FillDataSetting")
    '    End Try

    'End Sub

    Private Sub btnSTBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTBack.Click
        'bringPanelToFront(pnlAuthentication, pnlSetting)
        bringPanelToFront(pnlSetting, pnlAuthentication)
        'txtAUTPWD.Text = ""
        'txtAUTPWD.Focus()
        Me.Close()
    End Sub

    'Private Sub btnSTAUTPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTAUTPwd.Click
    '    Try
    '        If txtSTPassword.Text = "" Then
    '            MsgBox("Please enter authorize Password!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTPassword.Focus()
    '            txtSTPassword.SelectAll()
    '            Exit Sub
    '        End If

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTPassword.Text.Trim) & " WHERE SettingCode = 'AUTPWD' "
    '        ExecuteSQL(sSQL)

    '        MsgBox("Successful Updated!", MsgBoxStyle.Information, "Service Part")

    '        FillDataSetting()

    '    Catch ex As Exception
    '        MsgBox("Failed to update authorize password!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
    '    End Try
    'End Sub

    'Private Sub txtSTWSSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSTWSSave.Click
    '    Try
    '        If txtSTWSDCSSP.Text = "" Then
    '            MsgBox("Please enter DCSSP Web Service!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTWSDCSSP.Focus()
    '            txtSTWSDCSSP.SelectAll()
    '            Exit Sub
    '        End If

    '        If txtSTWSOracle.Text = "" Then
    '            MsgBox("Please enter Oracle Web Service!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTWSOracle.Focus()
    '            txtSTWSOracle.SelectAll()
    '            Exit Sub
    '        End If

    '        Dim regex = New Regex("http[s]?://(([^/:\.[:space:]]+(\.[^/:\.[:space:]]+)*)|([0-9](\.[0-9]{3})))(:[0-9]+)?((/[^?#[:space:]]+)(\?[^#[:space:]]+)?(\#.+)?)?")
    '        If Not Regex.IsMatch(txtSTWSDCSSP.Text) Then
    '            MsgBox("Invalid DCSSP URL Had Entered, Please Retry!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTWSDCSSP.Focus()
    '            txtSTWSDCSSP.SelectAll()
    '            Exit Sub
    '        End If

    '        If Not Regex.IsMatch(txtSTWSOracle.Text) Then
    '            MsgBox("Invalid Oracle URL Had Entered, Please Retry!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTWSOracle.Focus()
    '            txtSTWSOracle.SelectAll()
    '            Exit Sub
    '        End If
    '        Regex = Nothing

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSDCSSP.Text.Trim) & " WHERE SettingCode = 'URLDCSSP' "
    '        ExecuteSQL(sSQL)

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSOracle.Text.Trim) & " WHERE SettingCode = 'URLORACLE' "
    '        ExecuteSQL(sSQL)

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSORAUserID.Text.Trim) & " WHERE SettingCode = 'URLORAUSERID' "
    '        ExecuteSQL(sSQL)

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTWSORAUserPwd.Text.Trim) & " WHERE SettingCode = 'URLORAUSERPWD' "
    '        ExecuteSQL(sSQL)

    '        MsgBox("Successful Updated!", MsgBoxStyle.Information, "Service Part")

    '        FillDataSetting()

    '    Catch ex As Exception
    '        MsgBox("Failed to update web services!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
    '    End Try
    'End Sub

    'Private Sub btnImportSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportSave.Click
    '    Try
    '        If chkImpCaseType.Checked = False And chkImpCustomer.Checked = False And chkImpImporter.Checked = False And chkImpRBType.Checked = False And chkImpReason.Checked = False And chkImpStopperType.Checked = False And chkImpUser.Checked = False And chkImpVendor.Checked = False Then
    '            MsgBox("You Must Select At Least ONE Master for Batch Import!", MsgBoxStyle.Critical, "Service Part")
    '            Exit Sub
    '        End If

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(cboImpDay.Text) & " WHERE SettingCode = 'SCHEDULE' "
    '        ExecuteSQL(sSQL)

    '        If chkImpUser.Checked = True Then sUser = "Y" Else sUser = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sUser) & " WHERE SettingCode = 'USER' "
    '        ExecuteSQL(sSQL)

    '        If chkImpReason.Checked = True Then sReason = "Y" Else sReason = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sReason) & " WHERE SettingCode = 'REASON' "
    '        ExecuteSQL(sSQL)

    '        If chkImpRBType.Checked = True Then sRBType = "Y" Else sUser = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sRBType) & " WHERE SettingCode = 'RBTYPE' "
    '        ExecuteSQL(sSQL)

    '        If chkImpCaseType.Checked = True Then sCaseType = "Y" Else sCaseType = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sCaseType) & " WHERE SettingCode = 'CASETYPE' "
    '        ExecuteSQL(sSQL)

    '        If chkImpCustomer.Checked = True Then sCustomer = "Y" Else sCustomer = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sCustomer) & " WHERE SettingCode = 'CUSTOMER' "
    '        ExecuteSQL(sSQL)

    '        If chkImpImporter.Checked = True Then sImporter = "Y" Else sImporter = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sImporter) & " WHERE SettingCode = 'IMPORTER' "
    '        ExecuteSQL(sSQL)

    '        If chkImpVendor.Checked = True Then sVendor = "Y" Else sVendor = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sVendor) & " WHERE SettingCode = 'VENDOR' "
    '        ExecuteSQL(sSQL)

    '        If chkImpStopperType.Checked = True Then sStopperType = "Y" Else sStopperType = "N"
    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(sStopperType) & " WHERE SettingCode = 'STOPPERTYPE' "
    '        ExecuteSQL(sSQL)

    '        MsgBox("Successful updated!", MsgBoxStyle.Information, "Service Part")

    '        FillDataSetting()

    '    Catch ex As Exception
    '        MsgBox("Failed to update!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
    '    End Try
    'End Sub

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

    'Private Sub btnInterSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInterSave.Click
    '    Try
    '        If txtSTInterval.Text = "" Then
    '            MsgBox("Please enter Scanner No!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTInterval.Focus()
    '            txtSTInterval.SelectAll()
    '            Exit Sub
    '        End If

    '        If Not IsNumeric(txtSTInterval.Text) Then
    '            MsgBox("Number Only!", MsgBoxStyle.Critical, "Service Part")
    '            txtSTInterval.Focus()
    '            txtSTInterval.SelectAll()
    '            Exit Sub
    '        End If

    '        sSQL = "UPDATE TBLSetting SET SettingValue = " & SQLQuote(txtSTInterval.Text.Trim) & " WHERE SettingCode = 'INTERVAL' "
    '        ExecuteSQL(sSQL)

    '        MsgBox("Successful Updated!", MsgBoxStyle.Information, "Service Part")

    '        FillDataSetting()

    '    Catch ex As Exception
    '        MsgBox("Failed to update!" & ex.Message, MsgBoxStyle.Critical, "Service Part")
    '    End Try
    'End Sub

    Private Sub btnCloseLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseLogin.Click
        Me.Close()
    End Sub

    Private Sub btnSubmitLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitLogin.Click
        bringPanelToFront(pnlSetting, pnlAuthentication)
    End Sub

End Class
