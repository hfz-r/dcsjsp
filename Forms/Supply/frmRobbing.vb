Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Globalization
Imports System.Net

Public Class frmRobbing

#Region ". Variable Declaration ."
    Dim showError As Boolean = True
    Private Const strOnlineTitle As String = "Supply Robbing"
    Private Const strOfflineTitle As String = "Abnormal Supply Robbing"
    Dim isNormal As Boolean = True
    Dim isAllowDelete As Boolean = False
    Dim msgCode As String = Nothing
    Dim msgDesc As String = Nothing
#End Region

#Region ". General Function ."

    Private Sub GetReason()
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = New DataTable()

        lstViewRCVFScan.Items.Clear()
        lstDt = getDTData(String.Format("SELECT REASON, REASON_CODE FROM {0}", TblJSPAbnormalReasonCodeDb))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = lstDt.Rows(i).Item("REASON").ToString
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("REASON_CODE").ToString)
            lstViewRCVFScan.Items.Add(lstViewItem)
        Next
    End Sub

    Private Sub ChangeProcess()
        Cursor.Current = Cursors.Default
        If showError = True Then
            showError = False
            If MessageBox.Show("Connection is down. Change to Abnormal Process?", "Supply Big Parts Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                TimerCheckOnline.Enabled = False
                TimerCheckOnline.Dispose()
                showError = True
                bringPanelToFront(pnlRBAbn, pnlRBScanPart)
                Exit Sub
            Else
                TimerCheckOnline.Enabled = True
                TimerCheckOnline.Interval = iInterval
            End If
        End If
    End Sub

    Private Sub TimerCheckOnline_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckOnline.Tick
        Try
            If mode = False Then
                If ws_dcsClient.isConnected Then
                    If ws_dcsClient.isOracleConnected Then
                        mode = True
                        MsgBox("Connection is back.", MsgBoxStyle.Information, Me.Text)
                        txtModuleQR_KeyDown(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            Call ChangeProcess()
        End Try
    End Sub

    Private Function Validate() As Boolean
        If String.IsNullOrEmpty(txtFSModuleNo.Text) Then
            MessageBox.Show("Module No is required")
            txtFSModuleNo.Focus()
            txtFSModuleNo.SelectAll()
            Return False
        ElseIf txtFSModuleNo.Text.Length <> 6 Then
            MessageBox.Show("Invalid Module No format")
            txtFSModuleNo.Focus()
            txtFSModuleNo.SelectAll()
            Return False
        ElseIf String.IsNullOrEmpty(txtFSPartNo.Text) Then
            MessageBox.Show("Part No is required")
            txtFSPartNo.Focus()
            txtFSPartNo.SelectAll()
            Return False
        ElseIf txtFSPartNo.Text.Length <> 14 Then
            MessageBox.Show("Invalid Part No format")
            txtFSPartNo.Focus()
            txtFSPartNo.SelectAll()
            Return False
        ElseIf String.IsNullOrEmpty(txtFSSeqNo.Text) Then
            MessageBox.Show("Seq No is required")
            txtFSSeqNo.Focus()
            txtFSSeqNo.SelectAll()
            Return False
        ElseIf txtFSSeqNo.Text.Length <> 2 Then
            MessageBox.Show("Invalid Seq No format")
            txtFSSeqNo.Focus()
            txtFSSeqNo.SelectAll()
            Return False
        ElseIf String.IsNullOrEmpty(txtFSBranch.Text) Then
            MessageBox.Show("Branch is required")
            txtFSBranch.Focus()
            txtFSBranch.SelectAll()
            Return False
        ElseIf txtFSBranch.Text.Length <> 2 Then
            MessageBox.Show("Invalid Branch format")
            txtFSBranch.Focus()
            txtFSBranch.SelectAll()
            Return False
        ElseIf (lstViewRCVFScan.FocusedItem Is Nothing) Then
            MessageBox.Show("Reason is required")
            Return False
        End If
        Return True
    End Function

    Private Sub loadlstViewAbn(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = New DataTable()

        lstView.Items.Clear()
        lstDt = getDTData(String.Format("SELECT PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND ON_OFF_LINE_FLAG = 'N'", TblJSPRobbingInterface, GetBatchID("ROBBING", "5")))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(String.Format("{0}-{1}", lstDt.Rows(i).Item("PXP_PART_NO").ToString().Insert(5, "-"), lstDt.Rows(i).Item("PXP_PART_NO_SFX")))
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_BOX").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString)
            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub loadlstView(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = New DataTable()

        lstView.Items.Clear()
        lstDt = getDTData(String.Format("SELECT PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND ON_OFF_LINE_FLAG = 'Y'", TblJSPRobbingInterface, GetBatchID("ROBBING", "5")))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_NO").ToString().Insert(5, "-") & "-" & lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString())
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_BOX").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString)
            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub loadlstViewDelete(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = New DataTable()

        lstView.Items.Clear()
        lstDt = getDTData(String.Format("SELECT PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, " & _
                                                         "PART_BRANCH_NO, RETURN_VAL FROM {0} WHERE ON_OFF_LINE_FLAG = 'N'", TblJSPRobbingInterface))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_NO").ToString().Insert(5, "-") & "-" & lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString())
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_BOX").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("RETURN_VAL").ToString)
            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Function CheckOrgId(ByVal TempOrgID As String) As String
        Return IIf(Not String.IsNullOrEmpty(TempOrgID), IIf(Not TempOrgID = "0", TempOrgID, org_ID), org_ID)
    End Function

    Private Sub VerifyOrgId(ByVal MODULE_NO As String)
        Dim dt As DataTable = New DataTable()

        dt = ws_dcsClient.getData("*", TblJSPRobbingInfoView, " AND MODULE_NO = " & SQLQuote(MODULE_NO))
        If dt.Rows.Count > 0 Then
            If Not org_ID = dt.Rows(0).Item("ORG_ID").ToString.TrimStart("0"c) Then
                Throw New CustomException("Organization ID does not match Setting configuration.")
            End If
        End If
    End Sub

#End Region

#Region ". Create Table ."

    Private Sub InsertTable(ByVal lotNo As String, ByVal moduleCat As String, ByVal partNo As String, ByVal partNoSfx As String, ByVal seqNo As String, ByVal qty As String, ByVal branch As String, ByVal pxp As String, ByVal currentDate As String, ByVal forceStatus As String, ByVal isNormal As String, Optional ByVal FSReasonID As Integer = Nothing)
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("INSERT INTO [{0}] (RCV_INTERFACE_ID, RCV_INTERFACE_BATCH_ID, MODULE_ID, MODULE_NO, PART_ID, PART_NO, QTY_BOX, PXP_PART_SEQ_NO, MANUFACTURE_CODE, SUPPLIER_CODE, SUPPLIER_PLANT_CODE, SUPPLIER_SHIPPING_DOCK," _
                               & " BEFORE_PACKING_ROUTING, RECEIVING_COMPANY_CODE, RECEIVING_PLANT_CODE, RECEIVING_DOCK_CODE, PACKING_ROUTING_CODE, GRANTER_CODE, ORDER_TYPE, KANBAN_CLASSIFICATION, DELIVERY_CODE, MROS, ORDER_NO, DELIVERY_NO," _
                               & " BACK_NUMBER, PXP_PART_NO_SFX, RUNOUT_FLAG, BOX_TYPE, BRANCH_NO, ADDRESS, PACKING_DATE, KATASHIKI_JERSEY_NO, LOT_NO, MODULE_CATEGORY, PART_BRANCH_NO, DUMMY, VERSION_NO, ORG_ID, ROBBING_BY, ROBBING_DATE," _
                               & " SCANNER_BATCH_ID, SCANNER_HT_ID, PROCESS_DATE, PROCESS_FLAG, UPDATED_BY, UPDATED_DATE, ERROR_MSG, POST_DATE, DELIVERY_DATE, ON_OFF_LINE_FLAG, SCAN_DATE, PXP_PART_NO, FORCE_PXP_STATUS, FORCE_PXP_REASON_ID," _
                               & " SEQNO_KEY, RETURN_VAL, POSTED) ", TblJSPRobbingInterface)
        sqlStr = String.Format("{0}VALUES (", sqlStr)
        sqlStr = String.Format("{0} null, ", sqlStr) 'RCV_INTERFACE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(GetBatchID("ROBBING", "5"))) 'RCV_INTERFACE_BATCH_ID
        sqlStr = String.Format("{0} null, ", sqlStr) 'MODULE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(moduleCat + lotNo)) 'MODULE_NO
        sqlStr = String.Format("{0} null , ", sqlStr) 'PART_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(partNo)) 'PART_NO
        If qty = Nothing Then
            sqlStr = String.Format("{0}null , ", sqlStr) 'QTY_BOX
        Else
            sqlStr = String.Format("{0}{1} , ", sqlStr, qty) 'QTY_BOX
        End If
        sqlStr = String.Format("{0}{1} , ", sqlStr, seqNo) 'PXP_PART_SEQ_NO 
        If forceStatus <> "Y" Then
            If Not String.IsNullOrEmpty(pxp) Then
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(0, 2).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(0, 2)))) 'MANUFACTURE_CODE               -TO PASS start
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(2, 4).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(2, 4)))) 'SUPPLIER_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(6, 1).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(6, 1)))) 'SUPPLIER_PLANT_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(7, 3).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(7, 3)))) 'SUPPLIER_SHIPPING_DOCK
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(10, 6).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(10, 6)))) 'BEFORE_PACKING_ROUTING
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(16, 4).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(16, 4)))) 'RECEIVING_COMPANY_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(20, 1).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(20, 1)))) 'RECEIVING_PLANT_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(21, 2).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(21, 2)))) 'RECEIVING_DOCK_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(23, 6).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(23, 6)))) 'PACKING_ROUTING_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(29, 4).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(29, 4)))) 'GRANTER_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(33, 1).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(33, 1)))) 'ORDER_TYPE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(34, 1).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(34, 1)))) 'KANBAN_CLASSIFICATION 
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(39, 2).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(39, 2)))) 'DELIVERY_CODE                  - TO TEST
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(41, 2).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(41, 2)))) 'MROS
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(43, 12).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(43, 12)))) 'ORDER_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(55, 5).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(55, 5)))) 'DELIVERY_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(60, 4).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(60, 4)))) 'BACK_NUMBER
                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(partNoSfx)) 'PXP_PART_NO_SFX
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(81, 1).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(81, 1)))) 'RUNOUT_FLAG
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(83, 8).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(83, 8)))) 'BOX_TYPE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(91, 4).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(91, 4)))) 'BRANCH_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(95, 10).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(95, 10)))) 'ADDRESS
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(110, 8).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(110, 8)))) 'PACKING_DATE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(118, 3).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(118, 3)))) 'KATASHIKI_JERSEY_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(lotNo)) 'LOT_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(moduleCat)) 'MODULE_CATEGORY
                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(branch)) 'PART_BRANCH_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(131, 20).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(131, 20)))) 'DUMMY
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(151, 1).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(151, 1)))) 'VERSION_NO  
            End If
        Else
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'MANUFACTURE_CODE               -TO PASS start
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SUPPLIER_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SUPPLIER_PLANT_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SUPPLIER_SHIPPING_DOCK
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BEFORE_PACKING_ROUTING
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'RECEIVING_COMPANY_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'RECEIVING_PLANT_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'RECEIVING_DOCK_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PACKING_ROUTING_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'GRANTER_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ORDER_TYPE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'KANBAN_CLASSIFICATION 
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_CODE                  - TO TEST
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'MROS
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ORDER_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BACK_NUMBER
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(partNoSfx)) 'PXP_PART_NO_SFX
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'RUNOUT_FLAG
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BOX_TYPE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BRANCH_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ADDRESS
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PACKING_DATE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'KATASHIKI_JERSEY_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(lotNo)) 'LOT_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(moduleCat)) 'MODULE_CATEGORY
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(branch)) 'PART_BRANCH_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DUMMY
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'VERSION_NO  
        End If

        sqlStr = String.Format("{0}{1} , ", sqlStr, org_ID) 'ORG_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(gScannerID)) 'ROBBING_BY
        sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(currentDate)) 'ROBBING_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SCANNER_BATCH_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SCANNER_HT_ID
        sqlStr = String.Format("{0}null , ", sqlStr) 'PROCESS_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PROCESS_FLAG
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'UPDATED_BY
        sqlStr = String.Format("{0}GETDATE(), ", sqlStr) 'UPDATED_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ERROR_MSG
        sqlStr = String.Format("{0}null , ", sqlStr) 'POST_DATE
        sqlStr = String.Format("{0}null , ", sqlStr) 'DELIVERY_DATE                      -TO PASS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(isNormal)) 'ON_OFF_LINE_FLAG
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(currentDate)) 'SCAN_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(partNo)) 'PXP_PART_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(forceStatus)) 'FORCE_PXP_STATUS
        If FSReasonID <> 0 Then
            sqlStr = String.Format("{0}{1} , ", sqlStr, FSReasonID) 'FORCE_PXP_REASON_ID
        Else
            sqlStr = String.Format("{0}null , ", sqlStr) 'FORCE_PXP_REASON_ID
        End If

        sqlStr = String.Format("{0}null , ", sqlStr) 'SEQNO_KEY
        sqlStr = String.Format("{0} null , ", sqlStr) 'RETURN_VAL
        sqlStr = String.Format("{0} null ", sqlStr) 'POSTED
        sqlStr = String.Format("{0})", sqlStr)

        If ExecuteSQL(sqlStr) = True Then
            'MessageBox.Show(String.Format("Part No:{0} successfully inserted", partNo))
        End If
    End Sub

    Private Sub DeleteTable()
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("DELETE FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}'", TblJSPRobbingInterface, GetBatchID("ROBBING", "5"))
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPRobbingInterface)
        End If
    End Sub

    Private Sub DeleteTableAbn()
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("DELETE FROM {0} WHERE POSTED = 'Y'", TblJSPRobbingInterface)
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPRobbingInterface)
        End If
    End Sub

#End Region

#Region ". Main Menu Navigation ."

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmMain_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Public Sub Init()

        Try
            Me.Text = strOnlineTitle
            footerStatusBar.Visible = False
            GetReason()
            InitWebServices()
            bringPanelToFront(pnlRBMain, pnlRBScanPart)
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCloseCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRB.Click
        Me.Close()
    End Sub

    Private Sub btnScanCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanRB.Click
        Call LoadRob()
        bringPanelToFront(pnlRBScanPart, pnlRBMain)
    End Sub

    Private Sub btnAbnormalCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalRB.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBMain)
    End Sub

#End Region

#Region ". Normal Mode Navigation and Private Function ."

    Private Sub LoadRob()
        Me.Text = strOnlineTitle
        txtPxPQR.Focus()
        txtPxPQR.Text = String.Empty
        lblRBStatusMsg.Text = String.Empty
        lblRBStatusMsgDesc.Text = String.Empty
        lblRBStatusMsg.BackColor = Color.Transparent
        txtPartNo.Text = String.Empty
        txtSeqNo.Text = String.Empty
        txtBranchNo.Text = String.Empty
        txtQty.Text = String.Empty
        lblTotalScanned.Text = lstViewRCISummary.Items.Count
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        lblDetailTotalScan.Text = String.Empty
        lblRBStatusMsg.Text = String.Empty
        lblRBStatusMsgDesc.Text = String.Empty
        lblRBStatusMsg.BackColor = Color.Transparent
        loadlstView(lstViewRCISummary, lblDetailTotalScan)
        Me.Text = String.Format("{0} - View", strOnlineTitle)
        bringPanelToFront(pnlRBViewDet, pnlRBScanPart)
    End Sub

    Private Sub txtModuleQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPxPQR.KeyDown
        Dim dbReader As SqlCeDataReader = Nothing

        Try
            If e.KeyCode = Keys.Enter Then
                If String.IsNullOrEmpty(txtPxPQR.Text) Then
                    MsgBox("PxP Kanban QR is required", MsgBoxStyle.Critical, gAppName)
                    txtPxPQR.Focus()
                    txtPxPQR.SelectAll()
                    Exit Sub
                Else
                    Call VerifyOrgId(txtPxPQR.Text.Substring(125, 2) + txtPxPQR.Text.Substring(121, 4))
                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PXP_PART_NO = '{1}' AND PXP_PART_SEQ_NO = '{2}' AND PART_BRANCH_NO = '{3}'", TblJSPRobbingInterface, txtPxPQR.Text.Substring(64, 10), txtPxPQR.Text.Substring(127, 2), txtPxPQR.Text.Substring(129, 2)), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) > 0 Then
                            btnScanSubmit.Enabled = False
                            txtPxPQR.Text = String.Empty
                            txtPxPQR.Focus()
                            lblRBStatusMsg.BackColor = Color.Red
                            lblRBStatusMsg.Text = "Duplicated Part No"
                            lblRBStatusMsgDesc.Text = String.Empty
                            Cursor.Current = Cursors.Default
                        Else
                            InsertModuleQR(txtPxPQR.Text.Substring(125, 2), txtPxPQR.Text.Substring(121, 4), txtPxPQR.Text.Substring(74, 2), txtPxPQR.Text.Substring(64, 10), txtPxPQR.Text.Substring(127, 2), txtPxPQR.Text.Substring(76, 5), txtPxPQR.Text.Substring(129, 2), txtPxPQR.Text)
                        End If
                    End If
                End If
                txtPxPQR.Focus()
                txtPxPQR.SelectAll()
            End If
        Catch ex As WebException
            Cursor.Current = Cursors.Default
            mode = False
            TimerCheckOnline.Enabled = True
            Call ChangeProcess()
        Catch ex As CustomException
            MsgBox("Access Restricted!" + Environment.NewLine + ex.Message.ToString(), MsgBoxStyle.Critical, "Organization ID Mismatch")
            Me.Close()
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                txtPxPQR.SelectAll()
                txtPxPQR.Focus()
                MessageBox.Show("Failed to read PxP Kanban QR format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub InsertModuleQR(ByVal module_cat As String, ByVal lotNo As String, ByVal partNoSfx As String, ByVal partNo As String, ByVal seqNo As String, ByVal qty As String, ByVal branch As String, ByVal pxp As String, Optional ByVal reason As String = Nothing)
        Dim moduleno As String = Nothing
        Dim forceStatus As String = "N"
        Dim dt As DateTime = Nothing
        Dim currentDate As String = Nothing
        Dim ptNo As String = Nothing
        Dim dtDetail As DataTable = New DataTable()

        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then
                InitWebServices()
                Cursor.Current = Cursors.WaitCursor
                moduleno = module_cat + lotNo
                If reason <> 0 Then
                    forceStatus = "Y"
                Else
                    reason = String.Empty
                End If

                dt = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
                currentDate = dt.ToString("dd-MM-yyyy hh:mm:ss tt")

                If forceStatus = "Y" Then
                    ptNo = partNo.Insert(5, "-") & "-" & partNoSfx
                    dtDetail = ws_dcsClient.getData("QTY, MODULE_ID", TblJSPRobbingInfoView, _
                                                                       " AND PART_NO = " & SQLQuote(ptNo) & _
                                                                       " AND MODULE_NO = " & SQLQuote(moduleno) & _
                                                                       " AND SEQUENCE_NO = " & seqNo)
                    If dtDetail.Rows.Count > 0 Then
                        qty = dtDetail.Rows(0).Item("QTY").ToString()
                        msgCode = ws_validationClient.processValidation(GetBatchID("ROBBING", "5"), GetScannerId(), "ROBBING", "501", Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                    dtDetail.Rows(0).Item("MODULE_ID").ToString(), moduleno, partNo, partNoSfx, seqNo, _
                                                    dtDetail.Rows(0).Item("QTY").ToString(), Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                    Nothing, lotNo, module_cat, branch, Nothing, Nothing, Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, CheckOrgId(dtDetail.Rows(0).Item("ORG_ID").ToString), _
                                                    Nothing, Nothing, Nothing, Nothing, _
                                                    Nothing, gScannerID, currentDate, Nothing, Nothing, Nothing, Nothing, _
                                                    Nothing, currentDate, Nothing, Nothing, forceStatus, reason, Nothing, _
                                                    Nothing, Nothing, Nothing, msgDesc)
                    Else
                        Cursor.Current = Cursors.Default
                        MessageBox.Show("Part does not exist.")
                        Exit Sub
                    End If
                ElseIf forceStatus = "N" Then
                    msgCode = ws_validationClient.processValidation(GetBatchID("ROBBING", "5"), GetScannerId(), "ROBBING", "501", Nothing, _
                                                           Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, moduleno, partNo, partNoSfx, seqNo, qty, _
                                                            IIf(pxp.Substring(0, 2).Trim() = Nothing, Nothing, pxp.Substring(0, 2)), _
                                                            IIf(pxp.Substring(2, 4).Trim() = Nothing, Nothing, pxp.Substring(2, 4)), _
                                                            IIf(pxp.Substring(6, 1).Trim() = Nothing, Nothing, pxp.Substring(6, 1)), _
                                                            IIf(pxp.Substring(7, 3).Trim() = Nothing, Nothing, pxp.Substring(7, 3)), _
                                                            IIf(pxp.Substring(10, 6).Trim() = Nothing, Nothing, pxp.Substring(10, 6)), _
                                                            IIf(pxp.Substring(16, 4).Trim() = Nothing, Nothing, pxp.Substring(16, 4)), _
                                                            IIf(pxp.Substring(20, 1).Trim() = Nothing, Nothing, pxp.Substring(20, 1)), _
                                                            IIf(pxp.Substring(21, 2).Trim() = Nothing, Nothing, pxp.Substring(21, 2)), _
                                                            IIf(pxp.Substring(23, 6).Trim() = Nothing, Nothing, pxp.Substring(23, 6)), _
                                                            IIf(pxp.Substring(29, 4).Trim() = Nothing, Nothing, pxp.Substring(29, 4)), _
                                                            IIf(pxp.Substring(33, 1).Trim() = Nothing, Nothing, pxp.Substring(33, 1)), _
                                                            IIf(pxp.Substring(34, 1).Trim() = Nothing, Nothing, pxp.Substring(34, 1)), _
                                                            IIf(pxp.Substring(41, 2).Trim() = Nothing, Nothing, pxp.Substring(41, 2)), _
                                                            IIf(pxp.Substring(43, 12).Trim() = Nothing, Nothing, pxp.Substring(43, 12)), Nothing, _
                                                            IIf(pxp.Substring(55, 5).Trim() = Nothing, Nothing, pxp.Substring(55, 5)), _
                                                            IIf(pxp.Substring(60, 4).Trim() = Nothing, Nothing, pxp.Substring(60, 4)), _
                                                            IIf(pxp.Substring(81, 1).Trim() = Nothing, Nothing, pxp.Substring(81, 1)), _
                                                            IIf(pxp.Substring(83, 8).Trim() = Nothing, Nothing, pxp.Substring(83, 8)), _
                                                            IIf(pxp.Substring(91, 3).Trim() = Nothing, Nothing, pxp.Substring(91, 3)), _
                                                            IIf(pxp.Substring(95, 10).Trim() = Nothing, Nothing, pxp.Substring(95, 10)), _
                                                            IIf(pxp.Substring(110, 8).Trim() = Nothing, Nothing, pxp.Substring(110, 8)), _
                                                            IIf(pxp.Substring(118, 3).Trim() = Nothing, Nothing, pxp.Substring(118, 3)), lotNo, module_cat, branch, _
                                                            IIf(pxp.Substring(131, 20).Trim() = Nothing, Nothing, pxp.Substring(131, 20)), _
                                                            IIf(pxp.Substring(151, 1).Trim() = Nothing, Nothing, pxp.Substring(151, 1)), Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, org_ID, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, gScannerID, currentDate, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, currentDate, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, msgDesc)
                End If

                currentDate = dt.ToString("yyyy-MM-dd hh:mm:ss tt")

                If msgCode = "OK" Then
                    txtPartNo.Text = partNo
                    txtSeqNo.Text = seqNo
                    txtQty.Text = qty
                    txtBranchNo.Text = branch

                    If reason.Length = 0 Or reason = Nothing Then
                        Call InsertTable(lotNo, module_cat, partNo, partNoSfx, seqNo, qty, branch, pxp, currentDate, forceStatus, "Y")
                    Else
                        Call InsertTable(lotNo, module_cat, partNo, partNoSfx, seqNo, qty, branch, pxp, currentDate, forceStatus, "Y", reason)
                    End If

                    btnScanSubmit.Enabled = True
                    lblRBStatusMsg.BackColor = Color.LimeGreen
                    lblRBStatusMsg.Text = msgCode
                    lblRBStatusMsgDesc.Text = msgDesc
                    txtPxPQR.Text = String.Empty
                    txtPxPQR.Focus()
                    Cursor.Current = Cursors.Default
                    loadlstView(lstViewRCISummary, lblDetailTotalScan)
                    lblTotalScanned.Text = lstViewRCISummary.Items.Count
                Else
                    txtPxPQR.Focus()
                    txtPxPQR.SelectAll()
                    btnScanSubmit.Enabled = False
                    lblRBStatusMsg.BackColor = Color.Red
                    lblRBStatusMsg.Text = "Invalid PxP Kanban QR"
                    lblRBStatusMsgDesc.Text = msgDesc
                    Cursor.Current = Cursors.Default
                End If
            End If
            Cursor.Current = Cursors.Default
        Else
            Call ChangeProcess()
        End If
    End Sub

    Private Sub btnScanSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanSubmit.Click
        Dim dbReader As SqlCeDataReader = Nothing
        Dim sSQL As String = Nothing

        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    Cursor.Current = Cursors.WaitCursor
                    InitWebServices()
                    'Update post flag
                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}'", TblJSPRobbingInterface, GetBatchID("ROBBING", "5")), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) > 0 Then
                            msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("ROBBING", "5"), "ROBBING", "502", org_ID, Nothing, Nothing, _
                                                                           Nothing, Nothing, Nothing, msgDesc)
                            If msgCode = "OK" Then
                                Call DeleteTable()
                                txtPartNo.Text = String.Empty
                                txtSeqNo.Text = String.Empty
                                txtBranchNo.Text = String.Empty
                                txtQty.Text = String.Empty
                                lblTotalScanned.Text = String.Empty
                                lblRBStatusMsg.BackColor = Color.LimeGreen
                                Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
                                sSQL = String.Format("UPDATE {0} SET CURRENT_NO = {1} WHERE CATEGORY = 'ROBBING'", TblBatch, SQLQuote(currentNo))
                                If ExecuteSQL(sSQL) = False Then
                                    Throw New Exception()
                                End If
                                Cursor.Current = Cursors.Default
                                MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
                            Else
                                lblRBStatusMsg.BackColor = Color.Red
                                Cursor.Current = Cursors.Default
                            End If
                            lblRBStatusMsg.Text = msgCode
                            lblRBStatusMsgDesc.Text = msgDesc
                        Else
                            MessageBox.Show("No record to post")
                        End If
                    End If
                End If
            Else
                Call ChangeProcess()
            End If
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBackCPScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBScanPart.Click
        TimerCheckOnline.Enabled = False
        TimerCheckOnline.Dispose()
        bringPanelToFront(pnlRBMain, pnlRBScanPart)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanPart.Click
        lblRBStatusMsg.Text = String.Empty
        lblRBStatusMsg.BackColor = Color.Transparent
        txtFSModuleNo.Focus()
        txtFSModuleNo.SelectAll()
        isNormal = True
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBFScan, pnlRBScanPart)
    End Sub

    Private Sub ClearFS()
        txtFSBranch.Text = String.Empty
        txtFSModuleNo.Text = String.Empty
        txtFSPartNo.Text = String.Empty
        txtFSSeqNo.Text = String.Empty
        lstViewRCVFScan.Items.Item(0).Focused = True
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Dim dbReader As SqlCeDataReader = Nothing

        Try
            If Validate() Then 'Validate required fields
                If (isNormal) Then
                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PXP_PART_NO = '{1}' AND PXP_PART_SEQ_NO = '{2}' AND PART_BRANCH_NO = '{3}'", TblJSPRobbingInterface, txtFSPartNo.Text.Replace("-", "").Substring(0, 10), txtFSSeqNo.Text, txtFSBranch.Text), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) > 0 Then
                            MessageBox.Show("Duplicate Part No")
                            Cursor.Current = Cursors.Default
                        Else
                            InsertModuleQR(txtFSModuleNo.Text.Substring(0, 2), txtFSModuleNo.Text.Substring(2, 4), txtFSPartNo.Text.Substring(txtFSPartNo.Text.Length - 2, 2), txtFSPartNo.Text.Replace("-", "").Substring(0, 10), txtFSSeqNo.Text, Nothing, txtFSBranch.Text, String.Empty, lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                            Me.Text = strOnlineTitle
                            txtPxPQR.Focus()
                            bringPanelToFront(pnlRBScanPart, pnlRBFScan)
                        End If
                    End If
                Else
                    InsertModuleQRAbn(txtFSModuleNo.Text.Substring(2, 4), _
                                      txtFSModuleNo.Text.Substring(0, 2), _
                                      txtFSPartNo.Text.Replace("-", "").Substring(0, 10), _
                                      txtFSPartNo.Text.Substring(txtFSPartNo.Text.Length - 2, 2), _
                                      txtFSSeqNo.Text, _
                                      Nothing, _
                                      txtFSBranch.Text, _
                                      String.Empty, _
                                      lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                    Me.Text = strOnlineTitle
                    txtPxPQRAbn.Focus()
                    bringPanelToFront(pnlRBAbnScan, pnlRBFScan)
                End If
                ClearFS()
            End If
        Catch ex As WebException
            Cursor.Current = Cursors.Default
            mode = False
            TimerCheckOnline.Enabled = True
            Call ChangeProcess()
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                MessageBox.Show("Failed to read PxP Kanban format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Me.Text = strOnlineTitle
        ClearFS()
        If isNormal Then
            txtPxPQR.Focus()
            bringPanelToFront(pnlRBScanPart, pnlRBFScan)
        Else
            txtPxPQRAbn.Focus()
            bringPanelToFront(pnlRBAbnScan, pnlRBFScan)
        End If
    End Sub

    Private Sub btnBackCPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBViewDet.Click
        txtPxPQR.Focus()
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBScanPart, pnlRBViewDet)
    End Sub

#End Region

#Region ". Abnormal Mode Navigation and Private Function ."

    Private Sub LoadRobAbn()
        Me.Text = strOfflineTitle
        txtPxPQRAbn.Focus()
        txtPxPQRAbn.Text = String.Empty
        lblRBStatusMsgAbn.Text = String.Empty
        lblRBStatusMsgDescAbn.Text = String.Empty
        lblRBStatusMsgAbn.BackColor = Color.Transparent
        txtPartNoAbn.Text = String.Empty
        txtSeqNoAbn.Text = String.Empty
        txtQtyAbn.Text = String.Empty
        txtBranchNoAbn.Text = String.Empty
        lblTotalScannedAbn.Text = lstViewRcvDet.Items.Count
    End Sub

    Private Sub btnCPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnScan.Click
        Call LoadRobAbn()
        bringPanelToFront(pnlRBAbnScan, pnlRBAbn)
    End Sub

    Private Sub btnCPAbnScanDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnScanDet.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}
        loadlstViewAbn(lstViewRcvDet, total)
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        lblRBStatusMsgAbn.Text = String.Empty
        lblRBStatusMsgDescAbn.Text = String.Empty
        lblRBStatusMsgAbn.BackColor = Color.Transparent
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbnViewDet, pnlRBAbnScan)
    End Sub

    Private Sub btnCPAbnFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnFScan.Click
        lblRBStatusMsgAbn.Text = String.Empty
        lblRBStatusMsgAbn.BackColor = Color.Transparent
        txtFSModuleNo.Focus()
        txtFSModuleNo.SelectAll()
        GetReason()
        isNormal = False
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBFScan, pnlRBAbnScan)
    End Sub

    Private Sub btnBackCPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackRBAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBAbnScan)
    End Sub

    Private Sub btnCPAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnView.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}
        loadlstViewAbn(lstViewRcvDet, total)
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        Me.Text = String.Format("{0} - View", strOfflineTitle)
        bringPanelToFront(pnlRBAbnViewDet, pnlRBAbn)
    End Sub

    Private Sub btnCloseCPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRBViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBAbnViewDet)
    End Sub

    Private Sub btnCPAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnPost.Click
        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    bringPanelToFront(pnlLogin, pnlRBAbn)
                    txtUsername.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("No connection to post.")
        End Try
    End Sub

    Private Sub btnCloseCPPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRBPosting.Click
        txtUsername.Text = String.Empty
        txtPwd.Text = String.Empty
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBPosting)
    End Sub

    Private Sub btnCPAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBAbnDelete.Click
        loadlstViewDelete(lstViewDelete, lblDeleteTotalAbn)
        Me.Text = String.Format("{0} - Delete", strOnlineTitle)
        bringPanelToFront(pnlRBDelete, pnlRBAbn)
    End Sub

    Private Sub btnCloseCPDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRBDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlRBAbn, pnlRBDelete)
    End Sub

    Private Sub btnCloseAbnCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnRB.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlRBMain, pnlRBAbn)
    End Sub

    Private Sub txtPxPQRAbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPxPQRAbn.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If String.IsNullOrEmpty(txtPxPQRAbn.Text) Then
                    MsgBox("PxP Kanban QR is required", MsgBoxStyle.Critical, gAppName)
                    txtPxPQRAbn.Focus()
                    txtPxPQRAbn.SelectAll()
                    Exit Sub
                Else
                    InsertModuleQRAbn(txtPxPQRAbn.Text.Substring(121, 4), txtPxPQRAbn.Text.Substring(125, 2), txtPxPQRAbn.Text.Substring(64, 10), txtPxPQRAbn.Text.Substring(74, 2), txtPxPQRAbn.Text.Substring(127, 2), txtPxPQRAbn.Text.Substring(76, 5), txtPxPQRAbn.Text.Substring(129, 2), txtPxPQRAbn.Text)
                End If
                txtPxPQRAbn.Focus()
                txtPxPQRAbn.SelectAll()
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                txtPxPQRAbn.SelectAll()
                txtPxPQRAbn.Focus()
                MessageBox.Show("Failed to read PxP Kanban QR format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub InsertModuleQRAbn(ByVal lotNo As String, ByVal module_cat As String, ByVal partNo As String, ByVal partNoSfx As String, ByVal seqNo As String, ByVal qty As String, ByVal branch As String, ByVal pxp As String, Optional ByVal reason As String = Nothing)
        Dim moduleno As String = Nothing
        Dim dbReader As SqlCeDataReader = Nothing
        Dim forceStatus As String = "N"
        Dim currentDate As String = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            moduleno = module_cat + lotNo
            dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PXP_PART_NO = '{1}' AND PXP_PART_SEQ_NO = '{2}' AND PART_BRANCH_NO = '{3}'", TblJSPRobbingInterface, partNo, seqNo, branch), objConn)

            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    btnScanSubmit.Enabled = False
                    txtPxPQR.Focus()
                    txtPxPQR.SelectAll()
                    lblRBStatusMsgAbn.BackColor = Color.Red
                    MessageBox.Show("Duplicate Part No")
                    Cursor.Current = Cursors.Default
                Else
                    txtPartNo.Text = partNo
                    txtSeqNo.Text = seqNo
                    txtQty.Text = qty
                    txtBranchNo.Text = branch
                    If reason <> 0 Then
                        forceStatus = "Y"
                    Else
                        reason = String.Empty
                    End If

                    currentDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")

                    Try
                        If reason.Length = 0 Or reason = Nothing Then
                            Call InsertTable(lotNo, module_cat, partNo, partNoSfx, seqNo, qty, branch, pxp, currentDate, forceStatus, "N")
                        Else
                            Call InsertTable(lotNo, module_cat, partNo, partNoSfx, seqNo, qty, branch, pxp, currentDate, forceStatus, "N", reason)
                        End If

                        lblRBStatusMsgAbn.BackColor = Color.LimeGreen
                        lblRBStatusMsgAbn.Text = "OK"
                        lblRBStatusMsgDescAbn.Text = String.Empty
                        txtPxPQRAbn.Text = String.Empty
                        txtPxPQRAbn.Focus()
                        txtPartNoAbn.Text = partNo
                        txtSeqNoAbn.Text = seqNo
                        txtBranchNoAbn.Text = branch
                        txtQtyAbn.Text = qty
                        Cursor.Current = Cursors.Default
                        loadlstViewAbn(lstViewRcvDet, lblTotalScannedAbn)
                    Catch ex As Exception
                        txtPxPQRAbn.SelectAll()
                        lblRBStatusMsgAbn.BackColor = Color.Red
                        lblRBStatusMsgAbn.Text = "Scan Process Error"
                        lblRBStatusMsgDescAbn.Text = ex.Message
                        Cursor.Current = Cursors.Default
                    End Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCPSubmitPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRBSubmitPosting.Click
        Dim forceScanReasonID As String = String.Empty
        Dim sSQL As String = Nothing
        Dim lstDt As DataTable = New DataTable()
        Dim dtDetail As DataTable = New DataTable()

        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    InitWebServices()
                    Cursor.Current = Cursors.WaitCursor

                    lstDt = getDTData(String.Format("SELECT * FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}'", TblJSPRobbingInterface, GetBatchID("ROBBING", "5")))
                    If lstDt.Rows.Count > 0 Then
                        For i As Integer = 0 To lstDt.Rows.Count - 1
                            If IsDBNull(lstDt.Rows(i).Item("FORCE_PXP_REASON_ID")) Then
                                forceScanReasonID = Nothing
                            End If

                            If forceScanReasonID = Nothing Then
                                msgCode = ValidateAbnRecsForPost(lstDt.Rows(i), Nothing, Nothing, Nothing)
                            Else
                                dtDetail = ws_dcsClient.getData("QTY, MODULE_ID", TblJSPRobbingInfoView, _
                                                                                 " AND PART_NO = " & SQLQuote(lstDt.Rows(i).Item("PXP_PART_NO").ToString().Insert(5, "-") & "-" & lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString()) & _
                                                                                 " AND MODULE_NO = " & SQLQuote(lstDt.Rows(i).Item("MODULE_NO").ToString()) & _
                                                                                 " AND SEQUENCE_NO = " & SQLQuote(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString()))
                                If dtDetail.Rows.Count > 0 Then
                                    msgCode = ValidateAbnRecsForPost(lstDt.Rows(i), _
                                                                   dtDetail.Rows(0).Item("MODULE_ID").ToString, _
                                                                   dtDetail.Rows(0).Item("QTY").ToString, _
                                                                   forceScanReasonID)
                                End If
                            End If

                            ExecuteSQL(String.Format("UPDATE {0} SET RETURN_VAL = '{1}' WHERE MODULE_NO = '{2}' AND LOT_NO = '{3}' AND PXP_PART_NO = '{4}' AND PXP_PART_SEQ_NO = {5} AND PART_BRANCH_NO = '{6}'", TblJSPRobbingInterface, _
                                                     msgCode, lstDt.Rows(i).Item("MODULE_NO").ToString(), lstDt.Rows(i).Item("LOT_NO").ToString(), lstDt.Rows(i).Item("PXP_PART_NO").ToString(), lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString(), _
                                                     lstDt.Rows(i).Item("PART_BRANCH_NO").ToString()))
                        Next

                        msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("ROBBING", "5"), "ROBBING", "504", org_ID, Nothing, Nothing, _
                                                                        Nothing, Nothing, Nothing, msgDesc)
                        If msgCode = "OK" Then
                            sSQL = String.Format("UPDATE {0} SET POSTED = '{1}' WHERE RCV_INTERFACE_BATCH_ID = '{2}'", TblJSPRobbingInterface, _
                                                               "Y", GetBatchID("ROBBING", "5"))
                            If ExecuteSQL(sSQL) = True Then
                                Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
                                sSQL = String.Format("UPDATE {0} SET CURRENT_NO = {1} WHERE CATEGORY = 'ROBBING'", TblBatch, SQLQuote(currentNo))
                                If ExecuteSQL(sSQL) = False Then
                                    Throw New Exception()
                                Else
                                    loadlstView(lstViewPosting, lblPostingTotalPdgAbn)
                                    MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
                                End If
                            End If
                        Else
                            MessageBox.Show(msgDesc)
                        End If
                    Else
                        Cursor.Current = Cursors.Default
                        MsgBox("No records to post.", MsgBoxStyle.Critical, Me.Text)
                    End If
                Else
                    MessageBox.Show("Failed to post")
                End If
                Cursor.Current = Cursors.Default
            Else
                MessageBox.Show("No connection to post.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ValidateAbnRecsForPost(ByVal dr As DataRow, _
                                            Optional ByVal MODULE_ID As Object = Nothing, _
                                            Optional ByVal QTY As Object = Nothing, _
                                            Optional ByVal REASON_ID As String = Nothing) As String
        Return ws_validationClient.processValidation(GetBatchID("ROBBING", "5"), GetScannerId(), "SUPPLY", "503", _
                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                      IIf(Not String.IsNullOrEmpty(dr.Item("MODULE_NO").ToString), dr.Item("MODULE_NO").ToString, Nothing), _
                      Nothing, _
                      IIf(MODULE_ID IsNot Nothing, MODULE_ID, dr.Item("MODULE_CATEGORY").ToString() + dr.Item("LOT_NO").ToString()), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("PXP_PART_NO").ToString), dr.Item("PXP_PART_NO").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("PXP_PART_NO_SFX").ToString), dr.Item("PXP_PART_NO_SFX").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("PXP_PART_SEQ_NO").ToString), dr.Item("PXP_PART_SEQ_NO").ToString, Nothing), _
                      IIf(QTY IsNot Nothing, QTY, IIf(Not String.IsNullOrEmpty(dr.Item("QTY_BOX").ToString), dr.Item("QTY_BOX").ToString, Nothing)), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("MANUFACTURE_CODE").ToString), dr.Item("MANUFACTURE_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("SUPPLIER_CODE").ToString), dr.Item("SUPPLIER_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("SUPPLIER_PLANT_CODE").ToString), dr.Item("SUPPLIER_PLANT_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("SUPPLIER_SHIPPING_DOCK").ToString), dr.Item("SUPPLIER_SHIPPING_DOCK").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("BEFORE_PACKING_ROUTING").ToString), dr.Item("BEFORE_PACKING_ROUTING").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("RECEIVING_COMPANY_CODE").ToString), dr.Item("RECEIVING_COMPANY_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("RECEIVING_PLANT_CODE").ToString), dr.Item("RECEIVING_PLANT_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("RECEIVING_DOCK_CODE").ToString), dr.Item("RECEIVING_DOCK_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("PACKING_ROUTING_CODE").ToString), dr.Item("PACKING_ROUTING_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("GRANTER_CODE").ToString), dr.Item("GRANTER_CODE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("ORDER_TYPE").ToString), dr.Item("ORDER_TYPE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("KANBAN_CLASSIFICATION").ToString), dr.Item("KANBAN_CLASSIFICATION").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("MROS").ToString), dr.Item("MROS").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("ORDER_NO").ToString), dr.Item("ORDER_NO").ToString, Nothing), _
                      Nothing, _
                      IIf(Not String.IsNullOrEmpty(dr.Item("DELIVERY_NO").ToString), dr.Item("DELIVERY_NO").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("BACK_NUMBER").ToString), dr.Item("BACK_NUMBER").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("RUNOUT_FLAG").ToString), dr.Item("RUNOUT_FLAG").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("BOX_TYPE").ToString), dr.Item("BOX_TYPE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("BRANCH_NO").ToString), dr.Item("BRANCH_NO").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("ADDRESS").ToString), dr.Item("ADDRESS").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("PACKING_DATE").ToString), dr.Item("PACKING_DATE").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("KATASHIKI_JERSEY_NO").ToString), dr.Item("KATASHIKI_JERSEY_NO").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("LOT_NO").ToString), dr.Item("LOT_NO").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("MODULE_CATEGORY").ToString), dr.Item("MODULE_CATEGORY").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("PART_BRANCH_NO").ToString), dr.Item("PART_BRANCH_NO").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("DUMMY").ToString), dr.Item("DUMMY").ToString, Nothing), _
                      IIf(Not String.IsNullOrEmpty(dr.Item("VERSION_NO").ToString), dr.Item("VERSION_NO").ToString, Nothing), _
                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                      Nothing, Nothing, Nothing, Nothing, Nothing, _
                      CheckOrgId(dr.Item("ORG_ID").ToString), _
                      Nothing, Nothing, Nothing, Nothing, _
                      Nothing, _
                      dr.Item("ROBBING_BY").ToString(), _
                      Convert.ToDateTime(dr.Item("ROBBING_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt"), _
                      Nothing, Nothing, Nothing, Nothing, Nothing, _
                      Convert.ToDateTime(dr.Item("SCAN_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt"), _
                      Nothing, Nothing, _
                      dr.Item("FORCE_PXP_STATUS").ToString(), _
                      IIf(REASON_ID IsNot Nothing, REASON_ID, Nothing), _
                      Nothing, Nothing, Nothing, Nothing, msgDesc)
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Confirm to delete all posted record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            DeleteTableAbn()
            loadlstViewDelete(lstViewDelete, lblDeleteTotalAbn)
        End If
    End Sub

    Private Sub btnLoginSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginSubmit.Click
        Try
            If String.IsNullOrEmpty(txtUsername.Text) Then
                MsgBox("Username cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtUsername.Focus()
                txtUsername.SelectAll()
                Exit Sub
            ElseIf String.IsNullOrEmpty(txtPwd.Text) Then
                MsgBox("Password cannot be blank!", MsgBoxStyle.Critical, gAppName)
                txtPwd.Focus()
                txtPwd.SelectAll()
                Exit Sub
            End If

            If loginUser = txtUsername.Text.Trim() Then
                If loginPass = txtPwd.Text.Trim() Then
                    loadlstViewAbn(lstViewPosting, lblPostingTotalPdgAbn)
                    Me.Text = String.Format("{0} - Posting", strOnlineTitle)
                    lblUsername.Text = String.Format("USER NAME: {0}", txtUsername.Text)
                    bringPanelToFront(pnlRBPosting, pnlLogin)
                Else
                    MessageBox.Show("Invalid password")
                End If
            Else
                MessageBox.Show("Invalid username")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLoginClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginClose.Click
        bringPanelToFront(pnlRBAbn, pnlLogin)
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsername.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Enter
                txtPwd.SelectAll()
                txtPwd.Focus()
        End Select
    End Sub

    Private Sub txtPwd_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPwd.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Enter
                btnLoginSubmit_Click(Nothing, Nothing)
        End Select
    End Sub

#End Region

End Class