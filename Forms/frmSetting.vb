Imports System.Data
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

Public Class frmSetting

#Region ". Variable Declaration ."

    Dim sSQL As String = ""
    Dim dt As DataTable = New DataTable
    Dim gFullPath As String = gDBPath + gDatabaseName

#End Region

#Region ". Initialization ."

    Public Sub Init()
        Cursor.Current = Cursors.WaitCursor
        bringPanelToFront(pnlSetting, pnlAuthentication)
        'Get default values
        LoadSetting()
        TPSetting()
        TPOrganization()
        Cursor.Current = Cursors.Default

        'Display Authentication 
        bringPanelToFront(pnlAuthentication, pnlSetting)
        txtAutUser.Focus()
    End Sub

#End Region

#Region ". Tab Function ."

    Private Sub btnAuthenBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        boolsetting = True
        Me.Close()
    End Sub

    Private Sub FillDataSetting()
        Dim dtSetting As DataTable = New DataTable
        Dim test As String = Nothing

        Try
            sSQL = "SELECT SettingCode, SettingValue FROM TBLSetting"
            dtSetting = getData(sSQL)

            For i As Integer = 0 To dtSetting.Rows.Count - 1
                If dtSetting.Rows(i).Item("SettingCode") = "SCNID" Then
                    txtSTSCNID.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                ElseIf dtSetting.Rows(i).Item("SettingCode") = "AUTPWD" Then
                    txtSTPassword.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                ElseIf dtSetting.Rows(i).Item("SettingCode") = "AUTUSERID" Then
                    txtSTUsername.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLDCSJSP" Then
                    txtWSDCS.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORACLECHCK" Then
                    txtWSOracleChck.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                ElseIf dtSetting.Rows(i).Item("SettingCode") = "URLORACLECP" Then
                    txtWSOracleCP.Text = dtSetting.Rows(i).Item("SettingValue").ToString

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
                    'If sUser = "Y" Then chkUser = True Else chkUser = False

                ElseIf dtSetting.Rows(i).Item("SettingCode") = "SCN_NO" Then
                    txtSTBatchID.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                ElseIf dtSetting.Rows(i).Item("SettingCode") = "INTERVAL" Then
                    txtSTInterval.Text = dtSetting.Rows(i).Item("SettingValue").ToString

                End If
            Next

            LoadSetting()

        Catch ex As Exception
            test = ex.ToString
            MsgBox(String.Format("Failed to fill data setting !{0}", ex.ToString.Substring(ex.ToString.Length - 100)), MsgBoxStyle.Critical, "FillDataSetting")
        End Try

    End Sub

    Private Sub txtWS2Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWS2Save.Click
        Dim regex = New Regex("http[s]?://(([^/:\.[:space:]]+(\.[^/:\.[:space:]]+)*)|([0-9](\.[0-9]{3})))(:[0-9]+)?((/[^?#[:space:]]+)(\?[^#[:space:]]+)?(\#.+)?)?")

        Try
            If txtWSOracleChck.Text = String.Empty Then
                MsgBox("Please enter Checking WebService URL!", MsgBoxStyle.Critical, gAppName)
                txtWSOracleChck.Focus()
                txtWSOracleChck.SelectAll()
                Exit Sub
            End If

            If txtWSOracleCP.Text = String.Empty Then
                MsgBox("Please enter Complete WebService URL!", MsgBoxStyle.Critical, gAppName)
                txtWSOracleCP.Focus()
                txtWSOracleCP.SelectAll()
                Exit Sub
            End If

            If Not Regex.IsMatch(txtWSOracleChck.Text) Then
                MsgBox("Invalid Checking WebService URL, Please Retry!", MsgBoxStyle.Critical, gAppName)
                txtWSOracleChck.Focus()
                txtWSOracleChck.SelectAll()
                Exit Sub
            End If

            If Not Regex.IsMatch(txtWSOracleCP.Text) Then
                MsgBox("Invalid Complete WebService URL, Please Retry!", MsgBoxStyle.Critical, gAppName)
                txtWSOracleCP.Focus()
                txtWSOracleCP.SelectAll()
                Exit Sub
            End If
            Regex = Nothing

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'URLORACLECHCK' ", SQLQuote(txtWSOracleChck.Text.Trim))
            ExecuteSQL(sSQL)

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'URLORACLECP' ", SQLQuote(txtWSOracleCP.Text.Trim))
            ExecuteSQL(sSQL)

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'URLORAUSERID' ", SQLQuote(txtSTWSORAUserID.Text.Trim))
            ExecuteSQL(sSQL)

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'URLORAUSERPWD' ", SQLQuote(txtSTWSORAUserPwd.Text.Trim))
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, gAppName)

            FillDataSetting()

        Catch ex As Exception
            MsgBox(String.Format("Failed to update web services!{0}", ex.Message), MsgBoxStyle.Critical, gAppName)
        End Try
    End Sub

    Private Sub txtWS1Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWS1Save.Click
        Dim regex = New Regex("http[s]?://(([^/:\.[:space:]]+(\.[^/:\.[:space:]]+)*)|([0-9](\.[0-9]{3})))(:[0-9]+)?((/[^?#[:space:]]+)(\?[^#[:space:]]+)?(\#.+)?)?")

        Try
            If txtWSDCS.Text = String.Empty Then
                MsgBox("Please enter DCSJSP WebService URL!", MsgBoxStyle.Critical, gAppName)
                txtWSDCS.Focus()
                txtWSDCS.SelectAll()
                Exit Sub
            End If

            If Not Regex.IsMatch(txtWSDCS.Text) Then
                MsgBox("Invalid DCSJSP URL Had Entered, Please Retry!", MsgBoxStyle.Critical, gAppName)
                txtWSDCS.Focus()
                txtWSDCS.SelectAll()
                Exit Sub
            End If

            Regex = Nothing

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'URLDCSJSP' ", SQLQuote(txtWSDCS.Text.Trim))
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, gAppName)

            FillDataSetting()

        Catch ex As Exception
            MsgBox(String.Format("Failed to update web services!{0}", ex.Message), MsgBoxStyle.Critical, gAppName)
        End Try
    End Sub

    Private Sub btnInterSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInterSave.Click
        Try
            If txtSTInterval.Text = "" Then
                MsgBox("Please enter Interval!", MsgBoxStyle.Critical, gAppName)
                txtSTInterval.Focus()
                txtSTInterval.SelectAll()
                Exit Sub
            End If

            If Not IsNumeric(txtSTInterval.Text) Then
                MsgBox("Number Only!", MsgBoxStyle.Critical, gAppName)
                txtSTInterval.Focus()
                txtSTInterval.SelectAll()
                Exit Sub
            End If

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'INTERVAL' ", SQLQuote(txtSTInterval.Text.Trim))
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, gAppName)

            FillDataSetting()

        Catch ex As Exception
            MsgBox(String.Format("Failed to update!{0}", ex.Message), MsgBoxStyle.Critical, gAppName)
        End Try
    End Sub

    Private Sub btnBackAut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackAut.Click
        boolsetting = True
        Me.Close()
    End Sub

    Private Sub btnVerifyLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVerifyAut.Click
        Dim result As Boolean = False
        Dim sSQL As String = Nothing

        Try
            If String.IsNullOrEmpty(txtAutUser.Text) Then
                MsgBox("Username cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtAutUser.Focus()
                txtAutUser.SelectAll()
                Exit Sub
            ElseIf String.IsNullOrEmpty(txtAutPwd.Text) Then
                MsgBox("Password cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtAutPwd.Focus()
                txtAutPwd.SelectAll()
                Exit Sub
            End If

            sSQL = String.Format("SELECT LOGIN_ID, PASSWORD FROM " + TblUserDb + " WHERE LOGIN_ID = '{0}'", txtAutUser.Text.Trim())
            dt = getData(sSQL)
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(1) = txtAutPwd.Text Then
                    result = True
                    bringPanelToFront(pnlSetting, pnlAuthentication)
                End If
            Next
            If result = False Then
                MessageBox.Show("Invalid user")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnImportSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportSave.Click
        Try
            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'SCHEDULE' ", SQLQuote(cboImpDay.Text))
            ExecuteSQL(sSQL)

            MsgBox("Successful updated!", MsgBoxStyle.Information, gAppName)
            FillDataSetting()

        Catch ex As Exception
            MsgBox(String.Format("Failed to update!{0}", ex.Message), MsgBoxStyle.Critical, gAppName)
        End Try
    End Sub

    Private Sub btnOrgSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrgSave.Click
        Dim temp_OrgID As String = ""

        Try
            sSQL = String.Format("SELECT ORG_ID FROM {0} WHERE ORG_NAME = '{1}'", TblJSPOrganizationDb, cboOrganization.Text)
            dt = getData(sSQL)

            For i As Integer = 0 To dt.Rows.Count - 1
                temp_OrgID = dt.Rows(i).Item(0).ToString()
            Next

            sSQL = Nothing
            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'ORG_ID' ", SQLQuote(temp_OrgID))
            ExecuteSQL(sSQL)
            MsgBox("Successfully Saved.", MsgBoxStyle.Information, "Organization")
        Catch ex As Exception
            MsgBox(String.Format("Invalid: {0}", ex.Message), MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub TPSetting()
        Dim dt As DataTable = New DataTable
        Dim sSQL As String = Nothing

        Try
            If System.IO.File.Exists(gFullPath) Then
                sSQL = String.Format("SELECT * FROM {0}", TblSettingDb)
                dt = getData(sSQL)

                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item(1) = "DBPATH" Then
                        txtDBPath.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "DBNAME" Then
                        txtDBName.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "DBPASSWORD" Then
                        txtDBPwd.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "INTERVAL" Then
                        txtSTInterval.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "SCHEDULE" Then
                        cboImpDay.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "SCNID" Then
                        txtSTSCNID.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "SCN_NO" Then
                        txtSTBatchID.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "URLDCSJSP" Then
                        txtWSDCS.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "URLORACLECHCK" Then
                        txtWSOracleChck.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "URLORACLECP" Then
                        txtWSOracleCP.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "URLORAUSERID" Then
                        txtSTWSORAUserID.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "URLORAUSERPWD" Then
                        txtSTWSORAUserPwd.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "AUTUSERID" Then
                        txtSTUsername.Text = dt.Rows(i).Item(2).ToString()
                    ElseIf dt.Rows(i).Item(1) = "AUTPWD" Then
                        txtSTPassword.Text = dt.Rows(i).Item(1).ToString()
                    End If

                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub TPOrganization()
        Dim dt As DataTable = New DataTable
        Dim sSQL As String = Nothing

        Try
            sSQL = String.Format("SELECT * FROM {0}", TblJSPOrganizationDb)
            dt = getData(sSQL)
            cboOrganization.DisplayMember = "ORG_NAME"
            cboOrganization.ValueMember = "ORG_ID"
            cboOrganization.DataSource = dt

            If Not String.IsNullOrEmpty(org_ID) Then
                cboOrganization.SelectedValue = org_ID
            Else
                cboOrganization.SelectedValue = -1
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub btnSTBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTBack.Click
        LoadSetting()
        Me.Close()
    End Sub

    Private Sub btnSTAUTPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTAUTPwd.Click
        Try
            If String.IsNullOrEmpty(txtSTUsername.Text) Then
                MsgBox("Username cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtSTUsername.Focus()
                txtSTUsername.SelectAll()
                Exit Sub
            ElseIf String.IsNullOrEmpty(txtSTPassword.Text) Then
                MsgBox("Password cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtSTPassword.Focus()
                txtSTPassword.SelectAll()
                Exit Sub
            End If

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'AUTPWD' ", SQLQuote(txtSTPassword.Text.Trim))
            ExecuteSQL(sSQL)


            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'AUTUSERID' ", SQLQuote(txtSTUsername.Text.Trim))
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, gAppName)

            FillDataSetting()

        Catch ex As Exception
            MsgBox(String.Format("Failed to update authorize password!{0}", ex.Message), MsgBoxStyle.Critical, gAppName)
        End Try
    End Sub

    Private Sub btnBatchSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatchSave.Click
        Try
            If String.IsNullOrEmpty(txtSTBatchID.Text) Then
                MsgBox("Scanner No cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtSTUsername.Focus()
                txtSTUsername.SelectAll()
                Exit Sub
            End If

            sSQL = String.Format("UPDATE TBLSetting SET SettingValue = {0} WHERE SettingCode = 'SCN_NO' ", SQLQuote(txtSTBatchID.Text.Trim))
            ExecuteSQL(sSQL)

            MsgBox("Successful Updated!", MsgBoxStyle.Information, gAppName)

            FillDataSetting()

        Catch ex As Exception
            MsgBox(String.Format("Failed to update scanner no! {0}", ex.Message), MsgBoxStyle.Critical, gAppName)
        End Try
    End Sub

#End Region

#Region ". Authentication ."

    Private Sub txtUsername_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAutUser.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Enter
                txtAutPwd.SelectAll()
                txtAutPwd.Focus()
        End Select
    End Sub

    Private Sub txtAutPwd_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAutPwd.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Enter
                btnVerifyLogin_Click(Nothing, Nothing)
        End Select
    End Sub

#End Region

End Class
