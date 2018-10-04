Imports System.Data
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Net
Imports System.Globalization

Public Class frmChildPart

#Region ". Variable Declaration ."
    Dim showError As Boolean = True
    Private Const strOnlineTitle As String = "Supply Child Parts"
    Private Const strOfflineTitle As String = "Abnormal Supply Child Parts"
    Dim isNormal As Boolean = True
    Dim msgCode As String = Nothing
    Dim msgDesc As String = Nothing
    Dim currentPanel As Panel = Nothing
#End Region

#Region ". General Function ."

    Private Sub GetVendor(ByVal vendor As System.Windows.Forms.ComboBox)
        vendor.DataSource = getDTData(String.Format("SELECT * FROM {0} WHERE ORG_ID = {1} ORDER BY VENDOR_NAME ASC", TblJSPSupplyCPHeaderDb, org_ID))
        vendor.DisplayMember = "VENDOR_NAME"
        vendor.ValueMember = "VENDOR_ID"
        vendor.SelectedIndex = -1
        vendor.Focus()
    End Sub

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
                bringPanelToFront(pnlCPAbn, pnlCPScanPart)
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
            MessageBox.Show("Invalid Branch No format")
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
        lstDt = getDTData(String.Format("SELECT PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND SCANNER_SCREEN_CODE = '{2}' AND ON_OFF_LINE_FLAG = 'N'", TblJSPSupplyInterface, GetBatchID("CHILD_PART", "4"), "CP"))
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

    Private Sub loadlstView(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = New DataTable()

        lstView.Items.Clear()
        lstDt = getDTData(String.Format("SELECT PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND SCANNER_SCREEN_CODE = '{2}' AND ON_OFF_LINE_FLAG = 'Y'", TblJSPSupplyInterface, GetBatchID("CHILD_PART", "4"), "CP"))
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
        lstDt = getDTData(String.Format("SELECT PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO FROM {0} WHERE SCANNER_SCREEN_CODE = '{1}' AND ON_OFF_LINE_FLAG = 'N'", TblJSPSupplyInterface, "CP"))
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

    Private Sub ClearFS()
        txtFSBranch.Text = String.Empty
        txtFSModuleNo.Text = String.Empty
        txtFSPartNo.Text = String.Empty
        txtFSSeqNo.Text = String.Empty
        lstViewRCVFScan.Items.Item(0).Focused = True
    End Sub

    Private Sub VerifyOrgId(ByVal MODULE_NO As String)
        Dim dt As DataTable = New DataTable()

        dt = ws_dcsClient.getData("*", TblJSPSupplyCPDetailsView, " AND MODULE_NO = " & SQLQuote(MODULE_NO))
        If dt.Rows.Count > 0 Then
            If Not org_ID = dt.Rows(0).Item("ORG_ID").ToString.TrimStart("0"c) Then
                Throw New CustomException("Organization ID does not match Setting configuration.")
            End If
        End If
    End Sub

#End Region

#Region ". Create Table ."

    Private Sub InsertTable(ByVal partNo As String, ByVal lotNo As String, ByVal moduleCat As String, ByVal partNoSfx As String, ByVal seqNo As String, ByVal qty As String, ByVal branch As String, ByVal screenCode As String, ByVal vendorID As String, ByVal currentDate As String, ByVal pxp As String, ByVal forceStatus As String, Optional ByVal isNormal As String = Nothing, Optional ByVal FSReasonID As Integer = Nothing)
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("INSERT INTO [{0}] (RCV_INTERFACE_ID, RCV_INTERFACE_BATCH_ID, MODULE_ID, MODULE_NO, PXP_PART_ID, PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX," _
                               & "MANUFACTURE_CODE, SUPPLIER_CODE, SUPPLIER_PLANT_CODE, SUPPLIER_SHIPPING_DOCK, BEFORE_PACKING_ROUTING, RECEIVING_COMPANY_CODE, RECEIVING_PLANT_CODE," _
                               & " RECEIVING_DOCK_CODE, PACKING_ROUTING_CODE, GRANTER_CODE, ORDER_TYPE, KANBAN_CLASSIFICATION, DELIVERY_CODE, MROS, ORDER_NO, DELIVERY_NO, BACK_NUMBER," _
                               & " RUNOUT_FLAG, BOX_TYPE, BRANCH_NO, ADDRESS, PACKING_DATE, KATASHIKI_JERSEY_NO, LOT_NO, MODULE_CATEGORY, PART_BRANCH_NO, DUMMY, VERSION_NO, PDIO_ID, " _
                               & " PDIO_NO, DOCK_CODE, PDIO_ORDER_TYPE, VENDOR_ID, TRANSPORTER_ID, LANE_ID, TIER, BACK_NO, P2_PART_NO, P2_PART_SEQ_NO, ORG_ID, SCANNER_BATCH_ID, SCANNER_HT_ID," _
                               & " PROCESS_DATE, PROCESS_FLAG, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, ERROR_MSG, POST_DATE, DELIVERY_DATE, ON_OFF_LINE_FLAG, SCAN_DATE, FORCE_PDIO_STATUS," _
                               & " FORCE_PDIO_REASON_ID, FORCE_PXP_STATUS, FORCE_PXP_REASON_ID, SCANNER_SCREEN_CODE, FORCE_P2_STATUS, FORCE_P2_REASON_ID, SUPPLY_BY, SUPPLY_DATE, SHOP_ID, FORCE_MODULE_STATUS," _
                               & " FORCE_MODULE_REASON_ID, PART_NO, SEQNO_KEY, DELIVERY_TYPE, PRODUCTION_DATE, EXPORTER_CODE, PROD_LINE, CYCLE, ROUTE, TOTAL_BOX, DELIVERY_TYPE2, RETURN_VAL, POSTED, QTY_ORDER" _
                               & " ) ", TblJSPSupplyInterface)
        sqlStr = String.Format("{0}VALUES (", sqlStr)
        sqlStr = String.Format("{0}null, ", sqlStr) 'RCV_INTERFACE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(GetBatchID("CHILD_PART", "4"))) 'RCV_INTERFACE_BATCH_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'MODULE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(moduleCat + lotNo)) 'MODULE_NO
        sqlStr = String.Format("{0}null, ", sqlStr) 'PXP_PART_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(partNo)) 'PXP_PART_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(partNoSfx)) 'PXP_PART_NO_SFX
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(seqNo)) 'PXP_PART_SEQ_NO
        If qty = Nothing Then
            sqlStr = String.Format("{0}null , ", sqlStr) 'QTY_BOX
        Else
            sqlStr = String.Format("{0}{1} , ", sqlStr, qty) 'QTY_BOX
        End If
        If forceStatus <> "Y" Then
            If Not String.IsNullOrEmpty(pxp) Then
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(0, 2).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(0, 2)))) 'MANUFACTURE_CODE
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
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(39, 2).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(39, 2)))) 'DELIVERY_CODE
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(41, 2).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(41, 2)))) 'MROS
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(43, 12).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(43, 12)))) 'ORDER_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(55, 5).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(55, 5)))) 'DELIVERY_NO
                sqlStr = String.Format("{0}{1} , ", sqlStr, IIf(pxp.Substring(60, 4).Trim() = Nothing, SQLQuote(String.Empty), SQLQuote(pxp.Substring(60, 4)))) 'BACK_NUMBER
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
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'MANUFACTURE_CODE
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
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_CODE
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'MROS
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ORDER_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_NO
            sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BACK_NUMBER
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

        sqlStr = String.Format("{0}null, ", sqlStr) 'PDIO_ID
        sqlStr = String.Format("{0}null , ", sqlStr) 'PDIO_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'DOCK_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'PDIO_ORDER_TYPE
        sqlStr = String.Format("{0}{1} , ", sqlStr, vendorID) 'VENDOR_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'TRANSPORTER_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'LANE_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'TIER
        sqlStr = String.Format("{0}null , ", sqlStr) 'BACK_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'P2_PART_NO
        sqlStr = String.Format("{0}null, ", sqlStr) 'P2_PART_SEQ_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, org_ID) 'ORG_ID
        sqlStr = String.Format("{0}null , ", sqlStr) 'SCANNER_BATCH_ID
        sqlStr = String.Format("{0}null , ", sqlStr) 'SCANNER_HT_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'PROCESS_DATE
        sqlStr = String.Format("{0}null , ", sqlStr) 'PROCESS_FLAG
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(gScannerID)) 'CREATED_BY
        sqlStr = String.Format("{0}GETDATE(), ", sqlStr) 'CREATED_DATE
        sqlStr = String.Format("{0}null , ", sqlStr) 'UPDATED_BY
        sqlStr = String.Format("{0}GETDATE(), ", sqlStr) 'UPDATED_DATE
        sqlStr = String.Format("{0}null , ", sqlStr) 'ERROR_MSG
        sqlStr = String.Format("{0}null, ", sqlStr) 'POST_DATE
        sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(isNormal)) 'ON_OFF_LINE_FLAG
        sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(currentDate)) 'SCAN_DATE
        sqlStr = String.Format("{0}null , ", sqlStr) 'FORCE_PDIO_STATUS
        sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_PDIO_REASON_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(forceStatus)) 'FORCE_PXP_STATUS
        If FSReasonID <> 0 Then
            sqlStr = String.Format("{0}{1}, ", sqlStr, FSReasonID) 'FORCE_PXP_REASON_ID
        Else
            sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_PXP_REASON_ID
        End If
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(screenCode)) 'SCANNER_SCREEN_CODE        
        sqlStr = String.Format("{0}null , ", sqlStr) 'FORCE_P2_STATUS
        sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_P2_REASON_ID
        sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(gScannerID)) 'SUPPLY_BY
        sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(currentDate)) 'SUPPLY_DATE
        sqlStr = String.Format("{0}null, ", sqlStr) 'SHOP_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_MODULE_STATUS
        sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_MODULE_REASON_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'PART_NO
        sqlStr = String.Format("{0}null, ", sqlStr) 'SEQNO_KEY
        sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_TYPE
        sqlStr = String.Format("{0}null, ", sqlStr) 'PRODUCTION_DATE
        sqlStr = String.Format("{0}null, ", sqlStr) 'EXPORTER_CODE
        sqlStr = String.Format("{0}null, ", sqlStr) 'PROD_LINE
        sqlStr = String.Format("{0}null, ", sqlStr) 'CYCLE
        sqlStr = String.Format("{0}null, ", sqlStr) 'ROUTE
        sqlStr = String.Format("{0}null, ", sqlStr) 'TOTAL_BOX
        sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_TYPE
        sqlStr = String.Format("{0}null, ", sqlStr) 'RETURN_VAL
        sqlStr = String.Format("{0}null, ", sqlStr) 'POSTED
        sqlStr = String.Format("{0}null ", sqlStr) 'QTY_ORDER
        sqlStr = String.Format("{0})", sqlStr)

        If ExecuteSQL(sqlStr) = True Then
            'MessageBox.Show(String.Format("Part No:{0} successfully inserted", partNo))
        End If
    End Sub

    Private Sub DeleteTable()
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("DELETE FROM {0} WHERE SCANNER_SCREEN_CODE = 'CP' AND RCV_INTERFACE_BATCH_ID = '{1}'", TblJSPSupplyInterface, GetBatchID("CHILD_PART", "4"))
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyInterface)
        End If
    End Sub

    Private Sub DeleteTableAbn()
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("DELETE FROM {0} WHERE SCANNER_SCREEN_CODE = 'CP' AND POSTED = 'Y'", TblJSPSupplyInterface)
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyInterface)
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

            GetVendor(cmbVendor)
            GetVendor(cmbVendorAbn)
            'GetReason()
            Call GetAbnReasonCode(lstViewRCVFScan)
            InitWebServices()
            bringPanelToFront(pnlCPMain, pnlCPScanPart)
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCloseCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCP.Click
        Me.Close()
    End Sub

    Private Sub btnScanCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanCP.Click
        Call LoadCP()
        bringPanelToFront(pnlCPScanPart, pnlCPMain)
    End Sub

    Private Sub btnAbnormalCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalCP.Click
        cmbVendorAbn.Focus()
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCPAbn, pnlCPMain)
    End Sub

#End Region

#Region ". Event ."

    Private Sub cmbVendor_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.SelectedValueChanged
        txtPxPQR.Focus()
    End Sub

    Private Sub cmbVendorAbn_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendorAbn.SelectedValueChanged
        txtPxPQRAbn.Focus()
    End Sub

#End Region

#Region ". Normal Mode Navigation and Private Function ."

    Private Sub LoadCP()
        Me.Text = strOnlineTitle
        GetVendor(cmbVendor)
        lblCPStatusMsg.Text = String.Empty
        lblCPStatusMsgDesc.Text = String.Empty
        lblCPStatusMsg.BackColor = Color.Transparent
        txtPxPQR.Text = String.Empty
        txtPartNo.Text = String.Empty
        txtSeqNo.Text = String.Empty
        txtBranchNo.Text = String.Empty
        txtQty.Text = String.Empty
        lblTotalScanned.Text = lstViewRCISummary.Items.Count
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        lvlHeader.Text = cmbVendor.Text 'To display selected shop as title
        lblDetailTotalScan.Text = String.Empty
        lblCPStatusMsg.Text = String.Empty
        lblCPStatusMsgDesc.Text = String.Empty
        lblCPStatusMsg.BackColor = Color.Transparent
        txtPxPQR.Text = String.Empty
        txtPartNo.Text = String.Empty
        txtSeqNo.Text = String.Empty
        txtQty.Text = String.Empty
        txtBranchNo.Text = String.Empty
        loadlstView(lstViewRCISummary, lblDetailTotalScan)
        Me.Text = String.Format("{0} - View", strOnlineTitle)
        bringPanelToFront(pnlCPViewDet, pnlCPScanPart)
    End Sub

    Private Sub txtModuleQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPxPQR.KeyDown
        Dim dbReader As SqlCeDataReader = Nothing
        Dim moduleNo As String = Nothing

        Try
            If e.KeyCode = Keys.Enter Then
                If String.IsNullOrEmpty(txtPxPQR.Text) Then
                    MsgBox("PxP QR is required", MsgBoxStyle.Critical, gAppName)
                    txtPxPQR.Focus()
                    txtPxPQR.SelectAll()
                    Exit Sub
                Else
                    moduleNo = txtPxPQR.Text.Substring(125, 2) + txtPxPQR.Text.Substring(121, 4)
                    Call VerifyOrgId(moduleNo)

                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PXP_PART_NO = '{1}' AND MODULE_NO = '{2}' AND PXP_PART_SEQ_NO = '{3}' AND PART_BRANCH_NO = '{4}' AND VENDOR_ID = {5}", TblJSPSupplyInterface, txtPxPQR.Text.Substring(64, 10), moduleNo, txtPxPQR.Text.Substring(127, 2), txtPxPQR.Text.Substring(129, 2), cmbVendor.SelectedValue), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) > 0 Then
                            txtPxPQR.Focus()
                            txtPxPQR.SelectAll()
                            lblCPStatusMsg.BackColor = Color.Red
                            lblCPStatusMsg.Text = "NG"
                            lblCPStatusMsgDesc.Text = "Duplicate Part No"
                            Cursor.Current = Cursors.Default
                        Else
                            If Not String.IsNullOrEmpty(cmbVendor.Text) Then
                                InsertModuleQR(txtPxPQR.Text.Substring(64, 10), txtPxPQR.Text.Substring(121, 4), txtPxPQR.Text.Substring(125, 2), txtPxPQR.Text.Substring(74, 2), txtPxPQR.Text.Substring(127, 2), txtPxPQR.Text.Substring(76, 5), txtPxPQR.Text.Substring(129, 2), txtPxPQR.Text, cmbVendor.SelectedValue)
                            Else
                                MessageBox.Show("Select Vendor")
                                cmbVendor.Focus()
                            End If
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
                MessageBox.Show("Failed to read PxP QR format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub InsertModuleQR(ByVal partNo As String, ByVal lotNo As String, ByVal module_cat As String, ByVal partNoSfx As String, ByVal seqNo As String, ByVal qty As String, ByVal branch As String, ByVal pxp As String, ByVal vendor As String, Optional ByVal reason As String = Nothing)
        Dim dt As DateTime = Nothing
        Dim currentDate As String = Nothing
        Dim moduleno As String = Nothing
        Dim forceStatus As String = "N"
        Dim ptNo As String = Nothing
        Dim dtDetail As DataTable = New DataTable()

        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then
                InitWebServices()
                Cursor.Current = Cursors.WaitCursor
                dt = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
                currentDate = dt.ToString("dd-MM-yyyy hh:mm:ss tt")
                moduleno = module_cat + lotNo
                If reason <> 0 Then
                    forceStatus = "Y"
                Else
                    reason = Nothing
                End If

                If forceStatus = "Y" Then
                    ptNo = partNo.Insert(5, "-") & "-" & partNoSfx
                    dtDetail = ws_dcsClient.getData("QTY, MODULE_ID", TblJSPSupplyCPDetailsView, _
                                                    " AND PART_NUMBER = " & SQLQuote(ptNo) & _
                                                    " AND MODULE_NO = " & SQLQuote(moduleno) & _
                                                    " AND BRANCH_NO = " & branch & _
                                                    " AND SEQUENCE_NO = " & seqNo)
                    If dtDetail.Rows.Count > 0 Then
                        msgCode = ws_validationClient.processValidation(GetBatchID("CHILD_PART", "4"), gScannerID, "SUPPLY", "601", Nothing, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                     dtDetail.Rows(0).Item("MODULE_ID").ToString(), moduleno, partNo, partNoSfx, seqNo, _
                                                     dtDetail.Rows(0).Item("QTY").ToString(), Nothing, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                     Nothing, lotNo, module_cat, branch, Nothing, Nothing, Nothing, _
                                                     Nothing, Nothing, Nothing, Nothing, vendor, Nothing, Nothing, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                     org_ID, Nothing, gScannerID, currentDate, Nothing, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, gScannerID, "Y", _
                                                     gScannerID, currentDate, Nothing, Nothing, forceStatus, reason, Nothing, _
                                                     Nothing, "CP", Nothing, msgDesc)
                    Else
                        Cursor.Current = Cursors.Default
                        MessageBox.Show("Part does not exist.")
                        Exit Sub
                    End If
                Else
                    msgCode = ws_validationClient.processValidation(GetBatchID("CHILD_PART", "4"), gScannerID, "SUPPLY", "601", Nothing, _
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
                                                    IIf(pxp.Substring(43, 12).Trim() = Nothing, Nothing, pxp.Substring(43, 12)), _
                                                    IIf(pxp.Substring(39, 2).Trim() = Nothing, Nothing, pxp.Substring(39, 2)), _
                                                    IIf(pxp.Substring(55, 5).Trim() = Nothing, Nothing, pxp.Substring(55, 5)), _
                                                    IIf(pxp.Substring(60, 4).Trim() = Nothing, Nothing, pxp.Substring(60, 4)), _
                                                    IIf(pxp.Substring(81, 1).Trim() = Nothing, Nothing, pxp.Substring(81, 1)), _
                                                    IIf(pxp.Substring(83, 8).Trim() = Nothing, Nothing, pxp.Substring(83, 8)), _
                                                    IIf(pxp.Substring(91, 4).Trim() = Nothing, Nothing, pxp.Substring(91, 4)), _
                                                    IIf(pxp.Substring(95, 10).Trim() = Nothing, Nothing, pxp.Substring(95, 10)), _
                                                    IIf(pxp.Substring(110, 8).Trim() = Nothing, Nothing, pxp.Substring(110, 8)), _
                                                    IIf(pxp.Substring(118, 3).Trim() = Nothing, Nothing, pxp.Substring(118, 3)), _
                                                    lotNo, module_cat, branch, _
                                                    IIf(pxp.Substring(131, 20).Trim() = Nothing, Nothing, pxp.Substring(131, 20)), _
                                                    IIf(pxp.Substring(151, 1).Trim() = Nothing, Nothing, pxp.Substring(151, 1)), _
                                                    Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, vendor, Nothing, Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, org_ID, Nothing, gScannerID, currentDate, Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, gScannerID, "Y", _
                                                    gScannerID, currentDate, Nothing, Nothing, forceStatus, reason, Nothing, _
                                                    Nothing, "CP", Nothing, msgDesc)
                End If

                currentDate = dt.ToString("yyyy-MM-dd hh:mm:ss tt")

                If msgCode = "OK" Then
                    txtPartNo.Text = partNo
                    txtSeqNo.Text = seqNo
                    txtQty.Text = qty
                    txtBranchNo.Text = branch

                    If reason = Nothing Then
                        Call InsertTable(partNo, lotNo, module_cat, partNoSfx, seqNo, qty, branch, "CP", cmbVendor.SelectedValue, currentDate, pxp, forceStatus, "Y")
                    Else
                        Call InsertTable(partNo, lotNo, module_cat, partNoSfx, seqNo, qty, branch, "CP", cmbVendor.SelectedValue, currentDate, pxp, forceStatus, "Y", reason)
                    End If

                    lblCPStatusMsg.BackColor = Color.LimeGreen
                    lblCPStatusMsg.Text = msgCode
                    lblCPStatusMsgDesc.Text = msgDesc
                    txtPxPQR.Text = String.Empty
                    txtPxPQR.Focus()
                    Cursor.Current = Cursors.Default
                    loadlstView(lstViewRCISummary, lblDetailTotalScan)
                    lblTotalScanned.Text = lstViewRCISummary.Items.Count
                Else
                    txtPxPQR.Focus()
                    txtPxPQR.SelectAll()
                    lblCPStatusMsg.BackColor = Color.Red
                    lblCPStatusMsg.Text = msgCode
                    lblCPStatusMsgDesc.Text = msgDesc
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
                    InitWebServices()
                    Cursor.Current = Cursors.WaitCursor
                    lblCPStatusMsg.BackColor = Color.Transparent
                    lblCPStatusMsg.Text = String.Empty
                    lblCPStatusMsgDesc.Text = String.Empty

                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}'", TblJSPSupplyInterface, GetBatchID("CHILD_PART", "4")), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) > 0 Then
                            msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("CHILD_PART", "4"), "SUPPLY", "602", org_ID, Nothing, Nothing, _
                                                                                     Nothing, Nothing, Nothing, msgDesc)
                            If msgCode = "OK" Then
                                Call DeleteTable()
                                lblCPStatusMsg.BackColor = Color.LimeGreen
                                lblCPStatusMsg.Text = msgCode
                                lblCPStatusMsgDesc.Text = msgDesc
                                cmbVendor.SelectedIndex = -1
                                txtPxPQR.Text = String.Empty
                                txtPartNo.Text = String.Empty
                                txtSeqNo.Text = String.Empty
                                txtQty.Text = String.Empty
                                txtBranchNo.Text = String.Empty
                                lblTotalScanned.Text = String.Empty

                                'Update increment current running no of batch by 1
                                Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
                                sSQL = String.Format("UPDATE {0} SET CURRENT_NO = {1} WHERE CATEGORY = 'CHILD_PART'", TblBatch, SQLQuote(currentNo))
                                If ExecuteSQL(sSQL) = False Then
                                    Throw New Exception()
                                End If
                                Cursor.Current = Cursors.Default
                                MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
                            Else
                                lblCPStatusMsg.BackColor = Color.Red
                                lblCPStatusMsg.Text = msgCode
                                lblCPStatusMsgDesc.Text = msgDesc
                                Cursor.Current = Cursors.Default
                            End If
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

    Private Sub btnBackCPScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackCPScanPart.Click
        Me.Text = strOnlineTitle
        TimerCheckOnline.Enabled = False
        TimerCheckOnline.Dispose()
        bringPanelToFront(pnlCPMain, pnlCPScanPart)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanModule.Click
        If String.IsNullOrEmpty(cmbVendor.Text) Then
            MessageBox.Show("Select Vendor")
            Return
        End If

        txtPxPQR.Text = String.Empty
        txtPartNo.Text = String.Empty
        txtSeqNo.Text = String.Empty
        txtQty.Text = String.Empty
        txtBranchNo.Text = String.Empty
        txtPartNo.Text = String.Empty
        lblCPStatusMsg.Text = String.Empty
        lblCPStatusMsgDesc.Text = String.Empty
        lblCPStatusMsg.BackColor = Color.Transparent
        txtFSModuleNo.Focus()
        txtFSModuleNo.SelectAll()
        isNormal = True
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCPFScan, pnlCPScanPart)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Dim partNo As String = Nothing
        Dim dbReader As SqlCeDataReader = Nothing

        Try
            If Validate() Then 'Validate required fields
                partNo = txtFSPartNo.Text.Replace("-", "")

                If (isNormal) Then
                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PXP_PART_NO = '{1}' AND MODULE_NO = '{2}' AND PXP_PART_SEQ_NO = '{3}' AND PART_BRANCH_NO = '{4}' AND VENDOR_ID = {5}", TblJSPSupplyInterface, partNo, txtFSModuleNo.Text, txtFSSeqNo.Text, txtFSBranch.Text, cmbVendor.SelectedValue), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) > 0 Then
                            MessageBox.Show("Duplicate Part No")
                            Cursor.Current = Cursors.Default
                        Else
                            InsertModuleQR(partNo.Substring(0, 10), txtFSModuleNo.Text.Substring(2, 4), txtFSModuleNo.Text.Substring(0, 2), partNo.Substring(10, 2), txtFSSeqNo.Text, Nothing, txtFSBranch.Text, Nothing, cmbVendor.SelectedValue, lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                            Me.Text = strOnlineTitle
                            bringPanelToFront(pnlCPScanPart, pnlCPFScan)
                        End If
                    End If
                Else
                    InsertModuleQRAbn(partNo.Substring(0, 10), txtFSModuleNo.Text.Substring(2, 4), txtFSModuleNo.Text.Substring(0, 2), partNo.Substring(10, 2), txtFSSeqNo.Text, Nothing, txtFSBranch.Text, cmbVendor.SelectedValue, lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                    Me.Text = strOfflineTitle
                    bringPanelToFront(pnlCPAbnScan, pnlCPFScan)
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
                MessageBox.Show("Failed to read PxP format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        ClearFS()
        If isNormal Then
            Me.Text = strOnlineTitle
            bringPanelToFront(pnlCPScanPart, pnlCPFScan)
            txtPxPQR.Focus()
            txtPxPQR.SelectAll()
        Else
            Me.Text = strOfflineTitle
            bringPanelToFront(pnlCPAbnScan, pnlCPFScan)
            txtPxPQRAbn.Focus()
            txtPxPQRAbn.SelectAll()
        End If
    End Sub

    Private Sub btnBackCPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackCPViewDet.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCPScanPart, pnlCPViewDet)
        txtPxPQR.Focus()
        txtPxPQR.SelectAll()
    End Sub

#End Region

#Region ". Abnormal Mode Navigation and Private Function ."

    Private Sub LoadCPAbn()
        Me.Text = strOfflineTitle
        GetVendor(cmbVendorAbn)
        lblCPStatusMsgAbn.Text = String.Empty
        lblCPStatusMsgDescAbn.Text = String.Empty
        lblCPStatusMsgAbn.BackColor = Color.Transparent
        txtPxPQRAbn.Text = String.Empty
        txtPartNoAbn.Text = String.Empty
        txtSeqNoAbn.Text = String.Empty
        txtQtyAbn.Text = String.Empty
        txtBranchNoAbn.Text = String.Empty
        lblTotalScannedAbn.Text = lstViewRcvDet.Items.Count
    End Sub

    Private Sub btnCPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPAbnScan.Click
        Call LoadCPAbn()
        bringPanelToFront(pnlCPAbnScan, pnlCPAbn)
    End Sub

    Private Sub btnCPAbnScanDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPAbnScanDet.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}
        lvlHeaderAbn.Text = String.Format("Vendor Name: {0}", cmbVendorAbn.Text) 'To display selected shop as title
        loadlstViewAbn(lstViewRcvDet, total)
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        lblCPStatusMsgAbn.Text = String.Empty
        lblCPStatusMsgDescAbn.Text = String.Empty
        lblCPStatusMsgAbn.BackColor = Color.Transparent
        cmbVendorAbn.SelectedIndex = -1
        txtPxPQRAbn.Text = String.Empty
        txtPartNoAbn.Text = String.Empty
        txtSeqNoAbn.Text = String.Empty
        txtQtyAbn.Text = String.Empty
        txtBranchNoAbn.Text = String.Empty
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCPAbnViewDet, pnlCPAbnScan)
        currentPanel = pnlCPAbnScan
    End Sub

    Private Sub btnCPAbnFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPAbnFScan.Click
        If String.IsNullOrEmpty(cmbVendorAbn.Text) Then
            MessageBox.Show("Select Vendor")
            Return
        End If

        txtPxPQRAbn.Text = String.Empty
        txtPartNoAbn.Text = String.Empty
        txtSeqNoAbn.Text = String.Empty
        txtQtyAbn.Text = String.Empty
        txtBranchNo.Text = String.Empty
        lblCPStatusMsgAbn.Text = String.Empty
        lblCPStatusMsgDescAbn.Text = String.Empty
        lblCPStatusMsgAbn.BackColor = Color.Transparent
        txtFSModuleNo.Focus()
        txtFSModuleNo.SelectAll()
        'GetReason()
        Call GetAbnReasonCode(lstViewRCVFScan)
        isNormal = False
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCPFScan, pnlCPAbnScan)
    End Sub

    Private Sub btnBackCPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackCPAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCPAbn, pnlCPAbnScan)
    End Sub

    Private Sub btnCPAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPAbnView.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}
        loadlstViewAbn(lstViewRcvDet, total)
        lvlHeaderAbn.Text = String.Empty
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        Me.Text = String.Format("{0} - View", strOfflineTitle)
        bringPanelToFront(pnlCPAbnViewDet, pnlCPAbn)
        currentPanel = pnlCPAbn
    End Sub

    Private Sub btnCloseCPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCPViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(currentPanel, pnlCPAbnViewDet)
        txtPxPQRAbn.Focus()
        txtPxPQRAbn.SelectAll()
    End Sub

    Private Sub btnCPAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPAbnPost.Click
        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    bringPanelToFront(pnlLogin, pnlCPAbn)
                    txtUsername.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("No connection to post.")
        End Try
    End Sub

    Private Sub btnCloseCPPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCPPosting.Click
        txtUsername.Text = String.Empty
        txtPwd.Text = String.Empty
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCPAbn, pnlCPPosting)
    End Sub

    Private Sub btnCPAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPAbnDelete.Click
        loadlstViewDelete(lstViewDelete, lblDeleteTotalAbn)
        Me.Text = String.Format("{0} - Delete", strOnlineTitle)
        bringPanelToFront(pnlCPDelete, pnlCPAbn)
    End Sub

    Private Sub btnCloseCPDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCPDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlCPAbn, pnlCPDelete)
    End Sub

    Private Sub btnCloseAbnCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnCP.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlCPMain, pnlCPAbn)
    End Sub

    Private Sub txtPxPQRAbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPxPQRAbn.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not String.IsNullOrEmpty(cmbVendorAbn.Text) Then
                    If String.IsNullOrEmpty(txtPxPQRAbn.Text) Then
                        MsgBox("PxP QR is required", MsgBoxStyle.Critical, gAppName)
                        txtPxPQRAbn.Focus()
                        txtPxPQRAbn.SelectAll()
                        Exit Sub
                    Else
                        InsertModuleQRAbn(txtPxPQRAbn.Text.Substring(64, 10), txtPxPQRAbn.Text.Substring(121, 4), txtPxPQRAbn.Text.Substring(125, 2), txtPxPQRAbn.Text.Substring(74, 2), txtPxPQRAbn.Text.Substring(127, 2), txtPxPQRAbn.Text.Substring(76, 5), txtPxPQRAbn.Text.Substring(129, 2), cmbVendorAbn.SelectedValue)
                    End If
                Else
                    MessageBox.Show("Select Vendor")
                End If
                txtPxPQRAbn.Focus()
                txtPxPQRAbn.SelectAll()
            End If
        Catch ex As Exception
            If ex.Message = "Specified argument was out of the range of valid values." Then
                txtPxPQRAbn.SelectAll()
                txtPxPQRAbn.Focus()
                MessageBox.Show("Failed to read PxP QR format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub InsertModuleQRAbn(ByVal partNo As String, ByVal lotNo As String, ByVal module_cat As String, ByVal partNoSfx As String, ByVal seqNo As String, ByVal qty As String, ByVal branch As String, ByVal vendor As String, Optional ByVal reason As String = Nothing)
        Dim moduleno As String = module_cat + lotNo
        Dim dbReader As SqlCeDataReader
        Dim currentDate As String = Nothing
        Dim forceStatus As String = "N"

        If Not String.IsNullOrEmpty(cmbVendorAbn.Text) Then
            Cursor.Current = Cursors.WaitCursor
            moduleno = module_cat + lotNo

            dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PXP_PART_NO = '{1}' AND MODULE_NO = '{2}' AND PXP_PART_SEQ_NO = '{3}' AND PART_BRANCH_NO = '{4}' AND VENDOR_ID = {5}", TblJSPSupplyInterface, partNo, moduleno, seqNo, branch, cmbVendorAbn.SelectedValue), objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    txtPxPQRAbn.Focus()
                    txtPxPQRAbn.SelectAll()
                    lblCPStatusMsgAbn.BackColor = Color.Red
                    lblCPStatusMsgAbn.Text = "Duplicated Part No"
                    Cursor.Current = Cursors.Default
                Else
                    txtPartNoAbn.Text = partNo
                    txtSeqNoAbn.Text = seqNo
                    txtQtyAbn.Text = qty
                    txtBranchNoAbn.Text = branch
                    currentDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")

                    If reason <> 0 Then
                        forceStatus = "Y"
                    Else
                        reason = String.Empty
                    End If

                    Try
                        If reason = Nothing Then
                            Call InsertTable(partNo, lotNo, module_cat, partNoSfx, seqNo, qty, branch, "CP", cmbVendorAbn.SelectedValue, currentDate, txtPxPQRAbn.Text, forceStatus, "N")
                        Else
                            Call InsertTable(partNo, lotNo, module_cat, partNoSfx, seqNo, qty, branch, "CP", cmbVendorAbn.SelectedValue, currentDate, txtPxPQRAbn.Text, forceStatus, "N", reason)
                        End If

                        lblCPStatusMsgAbn.BackColor = Color.LimeGreen
                        lblCPStatusMsgAbn.Text = "OK"
                        lblCPStatusMsgDescAbn.Text = String.Empty
                        txtPxPQRAbn.Text = String.Empty
                        txtPxPQRAbn.Focus()
                        Cursor.Current = Cursors.Default
                        loadlstViewAbn(lstViewRcvDet, lblTotalScannedAbn)
                    Catch ex As Exception
                        txtPxPQRAbn.SelectAll()
                        lblCPStatusMsgAbn.BackColor = Color.Red
                        lblCPStatusMsgAbn.Text = "Scan Process Error"
                        lblCPStatusMsgDescAbn.Text = ex.Message
                        Cursor.Current = Cursors.Default
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Select Vendor")
        End If
    End Sub

    Private Sub btnCPSubmitPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPSubmitPosting.Click
        Dim sSQL As String = Nothing
        Dim forceScanReasonID As String = Nothing
        Dim lstDt As DataTable = New DataTable()
        Dim qty As String = Nothing
        Dim moduleID As String = Nothing
        Dim dtDetail As DataTable = New DataTable()

        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    InitWebServices()
                    Cursor.Current = Cursors.WaitCursor
                    lstDt = getDTData(String.Format("SELECT * FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND SCANNER_SCREEN_CODE = '{2}'", TblJSPSupplyInterface, GetBatchID("CHILD_PART", "4"), "CP"))
                    If lstDt.Rows.Count > 0 Then
                        For i As Integer = 0 To lstDt.Rows.Count - 1
                            If IsDBNull(lstDt.Rows(i).Item("FORCE_PXP_REASON_ID")) Then
                                forceScanReasonID = Nothing
                            End If
                            If forceScanReasonID = Nothing Then
                                If Not lstDt.Rows(i).Item("QTY_BOX").ToString() = String.Empty Then
                                    qty = IIf(Not String.IsNullOrEmpty(lstDt.Rows(i).Item("QTY_BOX").ToString), _
                                              lstDt.Rows(i).Item("QTY_BOX").ToString, Nothing)
                                End If
                            Else
                                dtDetail = ws_dcsClient.getData("QTY, MODULE_ID", TblJSPSupplyCPDetailsView, _
                                                                                 " AND PART_NUMBER = " & SQLQuote(lstDt.Rows(i).Item("PXP_PART_NO").ToString().Insert(5, "-") & "-" & lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString()) & _
                                                                                 " AND MODULE_NO = " & SQLQuote(lstDt.Rows(i).Item("MODULE_NO").ToString()) & _
                                                                                 " AND BRANCH_NO = " & SQLQuote(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString()) & _
                                                                                 " AND SEQUENCE_NO = " & SQLQuote(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString()))
                                If dtDetail.Rows.Count > 0 Then
                                    moduleID = IIf(Not String.IsNullOrEmpty(dtDetail.Rows(0).Item("MODULE_ID").ToString), _
                                                   dtDetail.Rows(0).Item("MODULE_ID").ToString, Nothing)
                                    qty = IIf(Not String.IsNullOrEmpty(dtDetail.Rows(0).Item("QTY").ToString), _
                                              dtDetail.Rows(0).Item("QTY").ToString, Nothing)
                                End If
                            End If
                            msgCode = ws_validationClient.processValidation(GetBatchID("CHILD_PART", "4"), GetScannerId(), "SUPPLY", "603", Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                    moduleID, _
                                                    IIf(lstDt.Rows(i).Item("MODULE_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("MODULE_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("PXP_PART_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PXP_PART_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString()), _
                                                    qty, _
                                                    IIf(lstDt.Rows(i).Item("MANUFACTURE_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("MANUFACTURE_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("SUPPLIER_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("SUPPLIER_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("SUPPLIER_PLANT_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("SUPPLIER_PLANT_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("SUPPLIER_SHIPPING_DOCK").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("SUPPLIER_SHIPPING_DOCK").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("BEFORE_PACKING_ROUTING").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("BEFORE_PACKING_ROUTING").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("RECEIVING_COMPANY_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("RECEIVING_COMPANY_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("RECEIVING_PLANT_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("RECEIVING_PLANT_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("RECEIVING_DOCK_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("RECEIVING_DOCK_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("PACKING_ROUTING_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PACKING_ROUTING_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("GRANTER_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("GRANTER_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("ORDER_TYPE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("ORDER_TYPE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("KANBAN_CLASSIFICATION").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("KANBAN_CLASSIFICATION").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("MROS").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("MROS").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("ORDER_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("ORDER_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("DELIVERY_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("DELIVERY_CODE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("DELIVERY_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("DELIVERY_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("BACK_NUMBER").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("BACK_NUMBER").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("RUNOUT_FLAG").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("RUNOUT_FLAG").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("BOX_TYPE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("BOX_TYPE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("BRANCH_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("BRANCH_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("ADDRESS").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("ADDRESS").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("PACKING_DATE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PACKING_DATE").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("KATASHIKI_JERSEY_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("KATASHIKI_JERSEY_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("LOT_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("LOT_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("MODULE_CATEGORY").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("MODULE_CATEGORY").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PART_BRANCH_NO").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("DUMMY").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("DUMMY").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("VERSION_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("VERSION_NO").ToString()), _
                                                    Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, _
                                                    IIf(lstDt.Rows(i).Item("VENDOR_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("VENDOR_ID").ToString()), _
                                                    Nothing, Nothing, _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, org_ID, Nothing, _
                                                    IIf(lstDt.Rows(i).Item("SUPPLY_BY") = String.Empty, Nothing, lstDt.Rows(i).Item("SUPPLY_BY").ToString()), _
                                                    IIf(lstDt.Rows(i).Item("SUPPLY_DATE").ToString() = String.Empty, Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SUPPLY_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt")), _
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                    IIf(lstDt.Rows(i).Item("CREATED_BY") = String.Empty, Nothing, lstDt.Rows(i).Item("CREATED_BY").ToString()), _
                                                    Nothing, Nothing, _
                                                    IIf(lstDt.Rows(i).Item("SCAN_DATE").ToString() = String.Empty, Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SCAN_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt")), _
                                                    Nothing, Nothing, Nothing, Nothing, _
                                                    IIf(lstDt.Rows(i).Item("FORCE_PXP_STATUS").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("FORCE_PXP_STATUS").ToString()), _
                                                    forceScanReasonID, "CP", Nothing, msgDesc)

                            sSQL = String.Format("UPDATE {0} SET RETURN_VAL = '{1}' WHERE PDIO_NO = '{2}' AND PXP_PART_NO = '{3}' AND PXP_PART_SEQ_NO = {4} AND PART_BRANCH_NO = '{5}' AND VENDOR_ID = {6}", TblJSPSupplyInterface, _
                                                 msgCode, lstDt.Rows(i).Item("MODULE_NO").ToString(), lstDt.Rows(i).Item("PXP_PART_NO").ToString(), lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString(), _
                                                 lstDt.Rows(i).Item("PART_BRANCH_NO").ToString(), lstDt.Rows(i).Item("VENDOR_ID").ToString())
                            ExecuteSQL(sSQL)
                        Next

                        msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("CHILD_PART", "4"), "SUPPLY", "604", org_ID, Nothing, Nothing, _
                                                                                 Nothing, Nothing, Nothing, msgDesc)
                        If msgCode = "OK" Then
                            sSQL = String.Format("UPDATE {0} SET POSTED = '{1}' WHERE RCV_INTERFACE_BATCH_ID = '{2}'", TblJSPSupplyInterface, _
                                                 "Y", GetBatchID("CHILD_PART", "4"))
                            If ExecuteSQL(sSQL) = True Then
                                Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
                                sSQL = String.Format("UPDATE {0} SET CURRENT_NO = {1} WHERE CATEGORY = 'CHILD_PART'", TblBatch, SQLQuote(currentNo))
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
        End Try
    End Sub

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
                    bringPanelToFront(pnlCPPosting, pnlLogin)
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
        bringPanelToFront(pnlCPAbn, pnlLogin)
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
