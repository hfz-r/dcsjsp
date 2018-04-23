Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe

Public Class frmBigPart

#Region "Variable Declaration"
    Dim showError As Boolean = True
    Private Const strOnlineTitle As String = "Supply Big Parts"
    Private Const strOfflineTitle As String = "Abnormal Supply Big Parts"
    Dim isNormal As Boolean = True
    Dim isAllowDelete As Boolean = False
    Dim listbpSupply As List(Of BPSupply) = New List(Of BPSupply)

    Private Class BPSupply
        Private _module_No As String
        Property Module_No() As String
            Get
                Return _module_No
            End Get
            Set(ByVal value As String)
                _module_No = value
            End Set
        End Property

        Private _order_No As String
        Property Order_No() As String
            Get
                Return _order_No
            End Get
            Set(ByVal value As String)
                _order_No = value
            End Set
        End Property
    End Class
#End Region

#Region "General Function"

    Private Function GetShop() As DataTable
        Return getDTData("SELECT * FROM JSP_SUPPLY_BP_HEADERS")
    End Function

    Private Sub GetReason()
        lstViewRCVFScan.Items.Clear()
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = getDTData("SELECT REASON, REASON_CODE FROM JSP_ABNORMAL_REASON_CODE")
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = lstDt.Rows(i).Item("REASON").ToString
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("REASON_CODE").ToString)
            lstViewRCVFScan.Items.Add(lstViewItem)
        Next
    End Sub

    Private Sub ChangeProcess()
        If showError = True Then
            showError = False
            'TODO: If abnormal, redirect user or?
            If MessageBox.Show("Connection is down. Change to Abnormal Process?", "Supply Big Parts Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                TimerCheckOnline.Enabled = False
                TimerCheckOnline.Dispose()
                showError = True
                bringPanelToFront(pnlBPAbn, pnlBPScanModule)
                Exit Sub
            Else
                TimerCheckOnline.Enabled = True
                TimerCheckOnline.Interval = 10000 'TO DO
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
            Return False
        ElseIf String.IsNullOrEmpty(txtFSOrderNo.Text) Then
            MessageBox.Show("Order No is required")
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Create Table"

    Private Sub InsertTable(ByVal moduleNo As String, ByVal orderNo As String, ByVal screenCode As String, ByVal shopID As String, Optional ByVal FSReasonID As Integer = Nothing)
        Dim sqlStr As String = Nothing
        sqlStr = String.Format("INSERT INTO [{0}] (RCV_INTERFACE_ID, RCV_INTERFACE_BATCH_ID, MODULE_ID, MODULE_NO, PXP_PART_ID, PXP_PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, MANUFACTURE_CODE, SUPPLIER_CODE, SUPPLIER_PLANT_CODE, SUPPLIER_SHIPPING_DOCK, BEFORE_PACKING_ROUTING, RECEIVING_COMPANY_CODE, RECEIVING_PLANT_CODE, RECEIVING_DOCK_CODE, PACKING_ROUTING_CODE, GRANTER_CODE, ORDER_TYPE, KANBAN_CLASSIFICATION, DELIVERY_CODE, MROS, ORDER_NO, DELIVERY_NO, BACK_NUMBER, RUNOUT_FLAG, BOX_TYPE, BRANCH_NO, ADDRESS, PACKING_DATE, KATASHIKI_JERSEY_NO, LOT_NO, MODULE_CATEGORY, PART_BRANCH_NO, DUMMY, VERSION_NO, PDIO_ID, PDIO_NO, DOCK_CODE, PDIO_ORDER_TYPE, VENDOR_ID, TRANSPORTER_ID, LANE_ID, TIER, BACK_NO, P2_PART_NO, P2_PART_SEQ_NO, ORG_ID, SCANNER_BATCH_ID, SCANNER_HT_ID, PROCESS_DATE, PROCESS_FLAG, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, ERROR_MSG, POST_DATE, DELIVERY_DATE, ON_OFF_LINE_FLAG, SCAN_DATE, FORCE_PDIO_STATUS, FORCE_PDIO_REASON_ID, FORCE_PXP_STATUS, FORCE_PXP_REASON_ID, SCANNER_SCREEN_CODE, FORCE_P2_STATUS, FORCE_P2_REASON_ID, SUPPLY_BY, SUPPLY_DATE, SHOP_ID, FORCE_MODULE_STATUS, FORCE_MODULE_REASON_ID, PART_NO, SEQNO_KEY, DELIVERY_TYPE, PRODUCTION_DATE, EXPORTER_CODE, PROD_LINE, CYCLE, ROUTE, TOTAL_BOX, DELIVERY_TYPE2) ", TblJSPSupplyInterface)
        sqlStr = String.Format("{0}VALUES (", sqlStr)
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'RCV_INTERFACE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'RCV_INTERFACE_BATCH_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'MODULE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(moduleNo)) 'MODULE_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PXP_PART_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PXP_PART_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PXP_PART_NO_SFX
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PXP_PART_SEQ_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'QTY_BOX
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
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(orderNo)) 'ORDER_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BACK_NUMBER
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'RUNOUT_FLAG
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BOX_TYPE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BRANCH_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ADDRESS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PACKING_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'KATASHIKI_JERSEY_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'LOT_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'MODULE_CATEGORY
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PART_BRANCH_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DUMMY
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'VERSION_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PDIO_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PDIO_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DOCK_CODE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PDIO_ORDER_TYPE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'VENDOR_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'TRANSPORTER_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'LANE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'TIER
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'BACK_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'P2_PART_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'P2_PART_SEQ_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ORG_ID                     TODO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SCANNER_BATCH_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SCANNER_HT_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PROCESS_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PROCESS_FLAG
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'CREATED_BY
        sqlStr = String.Format("{0}GETDATE(), ", sqlStr) 'CREATED_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'UPDATED_BY
        sqlStr = String.Format("{0}GETDATE(), ", sqlStr) 'UPDATED_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ERROR_MSG
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'POST_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ON_OFF_LINE_FLAG
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SCAN_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'FORCE_PDIO_STATUS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'FORCE_PDIO_REASON_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'FORCE_PXP_STATUS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'FORCE_PXP_REASON_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(screenCode)) 'SCANNER_SCREEN_CODE        
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'FORCE_P2_STATUS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'FORCE_P2_REASON_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SUPPLY_BY
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SUPPLY_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(shopID)) 'SHOP_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'FORCE_MODULE_STATUS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(FSReasonID)) 'FORCE_MODULE_REASON_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PART_NO
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'SEQNO_KEY
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_TYPE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PRODUCTION_DATE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'EXPORTER_CODE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'PROD_LINE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'CYCLE
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(String.Empty)) 'ROUTE
        sqlStr = String.Format("{0}{1} ,", sqlStr, SQLQuote(String.Empty)) 'TOTAL_BOX
        sqlStr = String.Format("{0}{1} ", sqlStr, SQLQuote(String.Empty)) 'DELIVERY_TYPE
        sqlStr = String.Format("{0})", sqlStr)

        If ExecuteSQL(sqlStr) = True Then
            MessageBox.Show(String.Format("Module:{0}{1} successfully inserted", moduleNo, orderNo))
        End If
    End Sub

    Private Sub DeleteTable()
        Dim sqlStr As String = String.Format("DELETE FROM {0}", TblJSPSupplyInterface)
        If ExecuteSQL(sqlStr) = True Then
            MessageBox.Show("Successfully deleted")
        End If
    End Sub
#End Region

#Region "Main Menu Navigation"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmBigPart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmBigPart_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
            ws_dcsClient.Dispose()
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Public Sub Init()

        Try
            Me.Text = strOnlineTitle
            footerStatusBar.Visible = False

            GetReason()
            bringPanelToFront(pnlBPMain, pnlBPScanModule)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCloseBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBP.Click
        Me.Close()
    End Sub

    Private Sub btnScanBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanBP.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPScanModule, pnlBPMain)
        cmbShop.DataSource = GetShop()
        cmbShop.DisplayMember = "SHOP_NAME"
        cmbShop.ValueMember = "SHOP_ID"
        cmbShop.SelectedIndex = -1
        cmbShop.Focus()
    End Sub

    Private Sub btnBPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnScan.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbnScan, pnlBPAbn)
        cmbShopAbn.DataSource = GetShop()
        cmbShopAbn.DisplayMember = "SHOP_NAME"
        cmbShopAbn.ValueMember = "SHOP_ID"
        cmbShopAbn.SelectedIndex = -1
        cmbShopAbn.Focus()
    End Sub
#End Region

#Region "Normal Mode Navigation and Private Function"

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        lblHeaderVwDet.Text = String.Format("Shop: {0}", cmbShop.Text) 'To display selected shop as title
        lblDetailTotalScan.Text = String.Empty
        lblStatusMsg.Text = String.Empty
        lblStatusMsg.BackColor = Color.Transparent
        loadlstView(lstViewRCISummary, lblDetailTotalScan)
        Me.Text = String.Format("{0} - View", strOnlineTitle)
        bringPanelToFront(pnlBPViewDet, pnlBPScanModule)
    End Sub

    Private Sub txtModuleQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtModuleQR.KeyDown
        Try
            If e.KeyCode = keys.Enter Then
                If Not String.IsNullOrEmpty(cmbShop.Text) Then
                    InsertModuleQR(txtModuleQR.Text.Substring(0, 6), txtModuleQR.Text.Substring(12), cmbShop.Text)
                Else
                    MessageBox.Show("Select Shop")
                End If
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InsertModuleQR(ByVal moduleQR As String, ByVal orderNo As String, ByVal shop As String, Optional ByVal reason As Integer = Nothing)
        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then
                Dim result As String = String.Empty
                Dim msgDesc As String = String.Empty
                Cursor.Current = Cursors.WaitCursor
                'Check shop ID
                'PROCESS_TYPE = SUPPLY
                'SCANNER_SCREEN_CODE = BP
                'PROCESS_CODE = 401
                'BATCH_ID = Call general function

                If result = "OK" Then
                    txtModuleNo.Text = moduleQR
                    txtOrderNo.Text = orderNo
                    If reason = 0 Or reason = Nothing Then
                        Call InsertTable(moduleQR, orderNo, "BP", cmbShop.SelectedValue)
                    Else
                        Call InsertTable(moduleQR, orderNo, "BP", cmbShop.SelectedValue, reason)
                    End If

                    btnScanSubmit.Enabled = True
                    lblStatusMsg.BackColor = Color.LimeGreen
                    lblStatusMsg.Text = result
                    txtModuleQR.Text = String.Empty
                    txtModuleQR.Focus()
                    txtModuleNo.Text = String.Empty
                    txtOrderNo.Text = String.Empty
                    cmbShop.SelectedIndex = -1
                    Cursor.Current = Cursors.Default
                    loadlstView(lstViewRCISummary, lblDetailTotalScan)
                    lblTotalScanned.Text = lstViewRCISummary.Items.Count
                Else
                    txtModuleQR.Focus()
                    txtModuleQR.SelectAll()
                    btnScanSubmit.Enabled = False
                    lblStatusMsg.BackColor = Color.Red
                    lblStatusMsg.Text = "Invalid Module QR"
                    Cursor.Current = Cursors.Default
                End If
            End If
        Else
            Call ChangeProcess()
        End If
    End Sub

    Private Sub btnScanSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanSubmit.Click
        Try
            Dim submitResult As String = ""

            'Update post flag
            'Post WS - process code 402
            If submitResult = "OK" Then
                Call DeleteTable()
                lblStatusMsg.BackColor = Color.Red
                lblStatusMsg.Text = submitResult
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBackBPScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPScanModule.Click
        TimerCheckOnline.Enabled = False
        TimerCheckOnline.Dispose()
        lblStatusMsg.Text = String.Empty
        lblStatusMsg.BackColor = Color.Transparent
        txtModuleQR.Text = String.Empty
        txtModuleNo.Text = String.Empty
        txtOrderNo.Text = String.Empty
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPMain, pnlBPScanModule)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanModule.Click
        If String.IsNullOrEmpty(cmbShop.Text) Then
            MessageBox.Show("Select Shop")
            Return
        End If

        lblStatusMsg.Text = String.Empty
        lblStatusMsg.BackColor = Color.Transparent
        txtFSModuleNo.Focus()
        txtFSModuleNo.SelectAll()
        isNormal = True
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPFScan, pnlBPScanModule)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Try
            If Validate() Then 'Validate required fields
                If (isNormal) Then
                    InsertModuleQR(txtFSModuleNo.Text, txtFSOrderNo.Text, cmbShop.Text, lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                    Me.Text = strOnlineTitle
                    bringPanelToFront(pnlBPScanModule, pnlBPFScan)
                Else
                    InsertModuleQRAbn(txtFSModuleNo.Text, txtFSOrderNo.Text, cmbShopAbn.Text, lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                    Me.Text = strOnlineTitle
                    bringPanelToFront(pnlBPAbnScan, pnlBPFScan)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Me.Text = strOnlineTitle
        If isNormal Then
            bringPanelToFront(pnlBPScanModule, pnlBPFScan)
        Else
            bringPanelToFront(pnlBPAbnScan, pnlBPFScan)
        End If
    End Sub

    Private Sub btnBackBPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPViewDet.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPScanModule, pnlBPViewDet)
    End Sub
#End Region

#Region "Abnormal Navigation and Private Function"

    Private Sub btnAbnormalBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalBP.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPMain)
    End Sub

    Private Sub btnBPAbnScanDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnScanDet.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}
        loadlstView(lstViewRcvDet, total)
        lblStatusMsgAbn.Text = String.Empty
        lblStatusMsgAbn.BackColor = Color.Transparent
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbnViewDet, pnlBPAbnScan)
    End Sub

    Private Sub btnBPAbnFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnFScan.Click
        If String.IsNullOrEmpty(cmbShopAbn.Text) Then
            MessageBox.Show("Select Shop")
            Return
        End If

        lblStatusMsgAbn.Text = String.Empty
        lblStatusMsgAbn.BackColor = Color.Transparent
        txtFSModuleNo.Focus()
        txtFSModuleNo.SelectAll()
        GetReason()
        isNormal = False
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPFScan, pnlBPAbnScan)
    End Sub

    Private Sub btnBackBPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPAbnScan.Click
        cmbShopAbn.SelectedIndex = -1
        lblStatusMsgAbn.Text = String.Empty
        lblStatusMsgAbn.BackColor = Color.Transparent
        txtModuleQRAbn.Text = String.Empty
        txtModuleNoAbn.Text = String.Empty
        txtOrderNoAbn.Text = String.Empty
        lblTotalScannedAbn.Text = String.Empty
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPAbnScan)
    End Sub

    Private Sub btnBPAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnView.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}
        loadlstView(lstViewRcvDet, total)
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        Me.Text = String.Format("{0} - View", strOfflineTitle)
        bringPanelToFront(pnlBPAbnViewDet, pnlBPAbn)
    End Sub

    Private Sub btnCloseBPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPAbnViewDet)
    End Sub

    Private Sub btnBPAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnPost.Click
        loadlstView(lstViewPosting, lblPostingTotalPdgAbn)

        Me.Text = String.Format("{0} - Posting", strOnlineTitle)
        bringPanelToFront(pnlBPPosting, pnlBPAbn)
    End Sub

    Private Sub loadlstView(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        lstView.Items.Clear()
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = getDTData("SELECT MODULE_NO, ORDER_NO FROM JSP_SUPPLY_INTERFACE")
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString)
            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub btnCloseBPPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPPosting.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPPosting)
    End Sub

    Private Sub btnBPAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnDelete.Click
        loadlstView(lstViewDelete, lblDeleteTotalAbn)
        Me.Text = String.Format("{0} - Delete", strOnlineTitle)
        bringPanelToFront(pnlBPDelete, pnlBPAbn)
    End Sub

    Private Sub btnCloseBPDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPDelete)
    End Sub

    Private Sub btnCloseAbnBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnBP.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPMain, pnlBPAbn)
    End Sub

    Private Sub txtModuleQRAbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtModuleQRAbn.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not String.IsNullOrEmpty(cmbShopAbn.Text) Then
                    InsertModuleQRAbn(txtModuleQRAbn.Text.Substring(0, 6), txtModuleQRAbn.Text.Substring(12), cmbShop.Text)
                Else
                    MessageBox.Show("Select Shop")
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InsertModuleQRAbn(ByVal moduleQR As String, ByVal orderNo As String, ByVal shop As String, Optional ByVal reason As Integer = Nothing)
        If Not String.IsNullOrEmpty(cmbShopAbn.Text) Then
            Cursor.Current = Cursors.WaitCursor
            Dim sqlStr As String = String.Format("SELECT MODULE_NO FROM {0} WHERE MODULE_NO LIKE TRIM('{1}')", TblJSPSupplyInterface, moduleQR)
            If ExecuteSQL(sqlStr) = True Then
                Cursor.Current = Cursors.Default
                MessageBox.Show("Duplicate Module No")
            Else
                txtModuleNoAbn.Text = moduleQR
                txtOrderNoAbn.Text = orderNo

                Try
                    If reason = 0 Or reason = Nothing Then
                        Call InsertTable(moduleQR, orderNo, String.Empty, cmbShopAbn.SelectedValue)

                    Else
                        Call InsertTable(moduleQR, orderNo, String.Empty, cmbShopAbn.SelectedValue, reason)
                    End If

                    lblStatusMsgAbn.BackColor = Color.LimeGreen
                    lblStatusMsgAbn.Text = "OK"
                    txtModuleQRAbn.Text = String.Empty
                    txtModuleQRAbn.Focus()
                    txtModuleNoAbn.Text = String.Empty
                    txtOrderNoAbn.Text = String.Empty
                    cmbShopAbn.SelectedIndex = -1
                    Cursor.Current = Cursors.Default
                    loadlstView(lstViewRCISummary, lblDetailTotalScan)
                    lblTotalScannedAbn.Text = lstViewRCISummary.Items.Count
                Catch ex As Exception
                    txtModuleQRAbn.SelectAll()
                    lblStatusMsgAbn.BackColor = Color.Red
                    lblStatusMsgAbn.Text = "Scan Process Error"
                    Cursor.Current = Cursors.Default
                End Try
            End If
        Else
            MessageBox.Show("Select Shop")
        End If
    End Sub

    Private Sub btnBPSubmitPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPSubmitPosting.Click
        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then

                    Dim result As String = String.Empty
                    Dim msgDesc As String = String.Empty
                    Cursor.Current = Cursors.WaitCursor
                    'PROCESS_TYPE = SUPPLY
                    'SCANNER_SCREEN_CODE = BP
                    'PROCESS_CODE = 403
                    'BATCH_ID = Call general function

                    If result = "OK" Then
                        'PROCESS_TYPE = SUPPLY
                        'SCANNER_SCREEN_CODE = BP
                        'PROCESS_CODE = 404
                        'BATCH_ID = Call general function
                        Dim innerResult As String = String.Empty
                        'lblStatusMsg.BackColor = Color.LimeGreen
                        'lblStatusMsg.Text = result
                        If innerResult = "OK" Then
                            isAllowDelete = True
                            MessageBox.Show("Successfully posted")
                        Else
                            isAllowDelete = False
                            MessageBox.Show("Failed to post")
                        End If
                        Cursor.Current = Cursors.Default
                    Else
                        'lblStatusMsg.BackColor = Color.Red
                        'lblStatusMsg.Text = "Invalid Module QR"                   
                    End If
                Else
                    MessageBox.Show("No connection to post.")
                End If
            Else
                MessageBox.Show("No connection to post.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (isAllowDelete) Then
            If MessageBox.Show("Confirm to delete all?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                DeleteTable()
                MessageBox.Show("Successfully deleted.")
                loadlstView(lstViewDelete, lblDeleteTotalAbn)
            End If
        Else
            MessageBox.Show("Unable to delete. Record not posted")
        End If
    End Sub
#End Region

End Class
