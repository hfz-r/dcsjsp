Imports System.Data
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Globalization
Imports System.Net

Public Class frmBigPart

#Region ". Variable Declaration ."

    Dim showError As Boolean = True
    Private Const strOnlineTitle As String = "Supply Big Parts"
    Private Const strOfflineTitle As String = "Abnormal Supply Big Parts"
    Dim isNormal As Boolean = True
    Dim isAllowDelete As Boolean = False
    Dim msgCode As String = Nothing
    Dim msgDesc As String = Nothing

#End Region

#Region ". General Function ."

    Private Sub GetShop(ByVal shop As System.Windows.Forms.ComboBox)
        shop.DataSource = getDTData(String.Format("SELECT * FROM {0} WHERE ORG_ID = {1} ORDER BY SHOP_NAME ASC", TblJSPSupplyBPHeaderDb, org_ID))
        shop.DisplayMember = "SHOP_NAME"
        shop.ValueMember = "SHOP_ID"
        shop.SelectedIndex = -1
        shop.Focus()
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
                bringPanelToFront(pnlBPAbn, pnlBPScanModule)
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
        ElseIf String.IsNullOrEmpty(txtFSOrderNo.Text) Then
            MessageBox.Show("Order No is required")
            txtFSOrderNo.Focus()
            txtFSOrderNo.SelectAll()
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
        lstDt = getDTData(String.Format("SELECT MODULE_NO, ORDER_NO FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND SCANNER_SCREEN_CODE = '{2}' AND ON_OFF_LINE_FLAG = 'N'", TblJSPSupplyInterface, GetBatchID("BIG_PART", "4"), "BP"))

        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString)
            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub loadlstView(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = New DataTable()

        lstView.Items.Clear()
        lstDt = getDTData(String.Format("SELECT MODULE_NO, ORDER_NO FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND SCANNER_SCREEN_CODE = '{2}' AND ON_OFF_LINE_FLAG = 'Y'", TblJSPSupplyInterface, GetBatchID("BIG_PART", "4"), "BP"))

        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString)
            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub loadlstViewDelete(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = New DataTable()

        lstView.Items.Clear()
        lstDt = getDTData(String.Format("SELECT MODULE_NO, ORDER_NO FROM {0} WHERE SCANNER_SCREEN_CODE = '{1}' AND ON_OFF_LINE_FLAG = 'N'", TblJSPSupplyInterface, "BP"))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("MODULE_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ORDER_NO").ToString)
            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

#End Region

#Region ". Create Table ."

    Private Sub InsertTable(ByVal moduleNo As String, ByVal orderNo As String, ByVal screenCode As String, ByVal shopID As String, ByVal currentDate As String, ByVal forceStatus As String, Optional ByVal isNormal As String = Nothing, Optional ByVal FSReasonID As Integer = Nothing)
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
        sqlStr = String.Format("{0} null, ", sqlStr) 'RCV_INTERFACE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(GetBatchID("BIG_PART", "4"))) 'RCV_INTERFACE_BATCH_ID
        sqlStr = String.Format("{0}null, ", sqlStr) 'MODULE_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(moduleNo)) 'MODULE_NO
        sqlStr = String.Format("{0}null, ", sqlStr) 'PXP_PART_ID
        sqlStr = String.Format("{0}null , ", sqlStr) 'PXP_PART_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'PXP_PART_NO_SFX
        sqlStr = String.Format("{0}null , ", sqlStr) 'PXP_PART_SEQ_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'QTY_BOX
        sqlStr = String.Format("{0}null , ", sqlStr) 'MANUFACTURE_CODE               -TO PASS start
        sqlStr = String.Format("{0}null , ", sqlStr) 'SUPPLIER_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'SUPPLIER_PLANT_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'SUPPLIER_SHIPPING_DOCK
        sqlStr = String.Format("{0}null , ", sqlStr) 'BEFORE_PACKING_ROUTING
        sqlStr = String.Format("{0}null , ", sqlStr) 'RECEIVING_COMPANY_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'RECEIVING_PLANT_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'RECEIVING_DOCK_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'PACKING_ROUTING_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'GRANTER_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'ORDER_TYPE
        sqlStr = String.Format("{0}null , ", sqlStr) 'KANBAN_CLASSIFICATION 
        sqlStr = String.Format("{0}null , ", sqlStr) 'DELIVERY_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'MROS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(orderNo)) 'ORDER_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'DELIVERY_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'BACK_NUMBER
        sqlStr = String.Format("{0}null , ", sqlStr) 'RUNOUT_FLAG
        sqlStr = String.Format("{0}null , ", sqlStr) 'BOX_TYPE
        sqlStr = String.Format("{0}null , ", sqlStr) 'BRANCH_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'ADDRESS
        sqlStr = String.Format("{0}null , ", sqlStr) 'PACKING_DATE
        sqlStr = String.Format("{0}null , ", sqlStr) 'KATASHIKI_JERSEY_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'LOT_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'MODULE_CATEGORY
        sqlStr = String.Format("{0}null , ", sqlStr) 'PART_BRANCH_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'DUMMY
        sqlStr = String.Format("{0}null , ", sqlStr) 'VERSION_NO                       -TO PASS end
        sqlStr = String.Format("{0}null, ", sqlStr) 'PDIO_ID
        sqlStr = String.Format("{0}null , ", sqlStr) 'PDIO_NO
        sqlStr = String.Format("{0}null , ", sqlStr) 'DOCK_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'PDIO_ORDER_TYPE
        sqlStr = String.Format("{0}null , ", sqlStr) 'VENDOR_ID
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
        sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_DATE                      -TO PASS
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(isNormal)) 'ON_OFF_LINE_FLAG
        sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(currentDate)) 'SCAN_DATE
        sqlStr = String.Format("{0}null , ", sqlStr) 'FORCE_PDIO_STATUS
        sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_PDIO_REASON_ID
        sqlStr = String.Format("{0}null , ", sqlStr) 'FORCE_PXP_STATUS
        sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_PXP_REASON_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(screenCode)) 'SCANNER_SCREEN_CODE        
        sqlStr = String.Format("{0}null , ", sqlStr) 'FORCE_P2_STATUS
        sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_P2_REASON_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(gScannerID)) 'SUPPLY_BY
        sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(currentDate)) 'SUPPLY_DATE
        sqlStr = String.Format("{0}{1}, ", sqlStr, shopID) 'SHOP_ID
        sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(forceStatus)) 'FORCE_MODULE_STATUS
        If FSReasonID <> 0 Then
            sqlStr = String.Format("{0}{1} , ", sqlStr, FSReasonID) 'FORCE_MODULE_REASON_ID
        Else
            sqlStr = String.Format("{0}null , ", sqlStr) 'FORCE_MODULE_REASON_ID
        End If
        sqlStr = String.Format("{0}null , ", sqlStr) 'PART_NO
        sqlStr = String.Format("{0}null, ", sqlStr) 'SEQNO_KEY
        sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_TYPE
        sqlStr = String.Format("{0}null, ", sqlStr) 'PRODUCTION_DATE
        sqlStr = String.Format("{0}null, ", sqlStr) 'EXPORTER_CODE
        sqlStr = String.Format("{0}null , ", sqlStr) 'PROD_LINE
        sqlStr = String.Format("{0}null, ", sqlStr) 'CYCLE
        sqlStr = String.Format("{0}null , ", sqlStr) 'ROUTE
        sqlStr = String.Format("{0}null, ", sqlStr) 'TOTAL_BOX
        sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_TYPE
        sqlStr = String.Format("{0}null , ", sqlStr) 'RETURN_VAL
        sqlStr = String.Format("{0}null ,", sqlStr) 'POSTED
        sqlStr = String.Format("{0}null ", sqlStr) 'QTY_ORDER
        sqlStr = String.Format("{0})", sqlStr)

        If ExecuteSQL(sqlStr) = False Then
            'MessageBox.Show(String.Format("Module No:{0} successfully inserted", moduleNo))
        End If
    End Sub

    Private Sub DeleteTable()
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("DELETE FROM {0} WHERE SCANNER_SCREEN_CODE = 'BP' AND RCV_INTERFACE_BATCH_ID = '{1}'", TblJSPSupplyInterface, GetBatchID("BIG_PART", "4"))
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyInterface)
        End If
    End Sub

    Private Sub DeleteTableAbn()
        Dim sqlStr As String = Nothing

        sqlStr = String.Format("DELETE FROM {0} WHERE SCANNER_SCREEN_CODE = 'BP' AND POSTED = 'Y'", TblJSPSupplyInterface)
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyInterface)
        End If
    End Sub

#End Region

#Region ". Main Menu Navigation ."

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmBigPart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmBigPart_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Public Sub Init()

        Try
            Me.Text = strOnlineTitle
            footerStatusBar.Visible = False

            GetShop(cmbShop)
            GetShop(cmbShopAbn)
            GetReason()
            InitWebServices()
            bringPanelToFront(pnlBPMain, pnlBPScanModule)
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCloseBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBP.Click
        Me.Close()
    End Sub

    Private Sub btnScanBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanBP.Click
        Call LoadBP()
        bringPanelToFront(pnlBPScanModule, pnlBPMain)
    End Sub

    Private Sub btnAbnormalBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalBP.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPMain)
    End Sub

#End Region

#Region ". Event ."

    Private Sub cmbShop_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShop.SelectedValueChanged
        txtModuleQR.Focus()
    End Sub

    Private Sub cmbShopAbn_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShopAbn.SelectedValueChanged
        txtModuleQRAbn.Focus()
    End Sub

#End Region

#Region ". Normal Mode Navigation and Private Function ."

    Private Sub LoadBP()
        Me.Text = strOnlineTitle
        GetShop(cmbShop)
        lblStatusMsg.Text = String.Empty
        lblStatusMsgDesc.Text = String.Empty
        lblStatusMsg.BackColor = Color.Transparent
        txtModuleQR.Text = String.Empty
        txtModuleNo.Text = String.Empty
        txtOrderNo.Text = String.Empty
        lblTotalScanned.Text = lstViewRCISummary.Items.Count
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        lblHeaderVwDet.Text = String.Format("Shop: {0}", cmbShop.Text) 'To display selected shop as title
        lblDetailTotalScan.Text = String.Empty
        lblStatusMsg.Text = String.Empty
        lblStatusMsgDesc.Text = String.Empty
        lblStatusMsg.BackColor = Color.Transparent
        loadlstView(lstViewRCISummary, lblDetailTotalScan)
        Me.Text = String.Format("{0} - View", strOnlineTitle)
        bringPanelToFront(pnlBPViewDet, pnlBPScanModule)
    End Sub

    Private Sub txtModuleQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtModuleQR.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not String.IsNullOrEmpty(cmbShop.Text) Then
                    If String.IsNullOrEmpty(txtModuleQR.Text) Then
                        MsgBox("Module QR is required", MsgBoxStyle.Critical, gAppName)
                        txtModuleQR.Focus()
                        txtModuleQR.SelectAll()
                        Exit Sub
                    Else
                        InsertModuleQR(txtModuleQR.Text.Substring(0, 6), txtModuleQR.Text.Substring(12), cmbShop.SelectedValue, txtModuleQR.Text.Trim)
                    End If
                Else
                    MessageBox.Show("Select Shop")
                    cmbShop.Focus()
                End If
                txtModuleQR.Focus()
                txtModuleQR.SelectAll()
            End If
        Catch ex As WebException
            Cursor.Current = Cursors.Default
            mode = False
            TimerCheckOnline.Enabled = True
            Call ChangeProcess()
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                txtModuleQR.SelectAll()
                txtModuleQR.Focus()
                MessageBox.Show("Failed to read Module QR format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub InsertModuleQR(ByVal moduleNo As String, ByVal orderNo As String, ByVal shop As String, ByVal moduleQR As String, Optional ByVal reason As String = Nothing)
        Dim forceStatus As String = "N"
        Dim dt As DateTime = Nothing
        Dim currentDate As String = Nothing
        Dim moduleID = Nothing
        Dim orgID = Nothing
        Dim dtDetail As DataTable = New DataTable()

        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then
                InitWebServices()
                Cursor.Current = Cursors.WaitCursor
                If reason <> 0 Then
                    forceStatus = "Y"
                Else
                    reason = String.Empty
                End If

                dt = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
                currentDate = dt.ToString("dd-MM-yyyy hh:mm:ss tt")
                dtDetail = ws_dcsClient.getData("MODULE_ID, ORG_ID", TblJSPSupplyBPDetailsView, _
                                                                      " AND MODULE_NO = " & SQLQuote(moduleNo))

                If dtDetail.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDetail.Rows.Count - 1
                        moduleID = dtDetail.Rows(i).Item("MODULE_ID").ToString()
                        orgID = dtDetail.Rows(i).Item("ORG_ID").ToString()
                    Next
                Else
                    Cursor.Current = Cursors.Default
                    MessageBox.Show("Module does not exist.")
                    Exit Sub
                End If

                If orgID <> org_ID Then
                    MessageBox.Show("Organization ID Mismatch")
                    Exit Sub
                End If

                If forceStatus = "Y" Then
                    msgCode = ws_validationClient.processValidation(GetBatchID("BIG_PART", "4"), GetScannerId(), "SUPPLY", "401", Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            moduleID, moduleNo, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, orderNo, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, org_ID, Nothing, gScannerID, currentDate, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, gScannerID, Nothing, _
                                                            Nothing, currentDate, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, "BP", shop, msgDesc)
                ElseIf forceStatus = "N" Then
                    msgCode = ws_validationClient.processValidation(GetBatchID("BIG_PART", "4"), GetScannerId(), "SUPPLY", "401", Nothing, _
                                                            Nothing, moduleQR.Substring(6, 1), moduleQR.Substring(7, 5), Nothing, Nothing, Nothing, Nothing, _
                                                            moduleID, moduleNo, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, orderNo, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, org_ID, Nothing, gScannerID, currentDate, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, gScannerID, Nothing, _
                                                            Nothing, currentDate, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, "BP", shop, msgDesc)
                End If

                currentDate = dt.ToString("yyyy-MM-dd hh:mm:ss tt")

                If msgCode = "OK" Then
                    txtModuleNo.Text = moduleNo
                    txtOrderNo.Text = orderNo
                    If reason.Length = 0 Or reason = Nothing Then
                        Call InsertTable(moduleNo, orderNo, "BP", shop, currentDate, forceStatus, "Y")
                    Else
                        Call InsertTable(moduleNo, orderNo, "BP", shop, currentDate, forceStatus, "Y", reason)
                    End If

                    lblStatusMsg.BackColor = Color.LimeGreen
                    lblStatusMsg.Text = msgCode
                    lblStatusMsgDesc.Text = msgDesc
                    txtModuleQR.Text = String.Empty
                    txtModuleQR.Focus()
                    Cursor.Current = Cursors.Default
                    loadlstView(lstViewRCISummary, lblDetailTotalScan)
                    lblTotalScanned.Text = lstViewRCISummary.Items.Count
                Else
                    txtModuleQR.Focus()
                    txtModuleQR.SelectAll()
                    lblStatusMsg.BackColor = Color.Red
                    lblStatusMsg.Text = "Invalid Module QR"
                    lblStatusMsgDesc.Text = msgDesc
                    Cursor.Current = Cursors.Default
                End If
            End If
            Cursor.Current = Cursors.Default
        Else
            Call ChangeProcess()
        End If
    End Sub

    Private Sub btnScanSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanSubmit.Click
        Dim sSQL As String = Nothing

        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    InitWebServices()
                    'Update post flag
                    msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("BIG_PART", "4"), "SUPPLY", "402", org_ID, Nothing, Nothing, _
                                                                             Nothing, Nothing, Nothing, msgDesc)
                    If msgCode = "OK" Then
                        Call DeleteTable()
                        txtModuleNo.Text = String.Empty
                        txtOrderNo.Text = String.Empty
                        lblTotalScanned.Text = String.Empty
                        lblStatusMsg.BackColor = Color.LimeGreen
                        Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
                        sSQL = String.Format("UPDATE {0} SET CURRENT_NO = {1} WHERE CATEGORY = 'BIG_PART'", TblBatch, SQLQuote(currentNo))
                        If ExecuteSQL(sSQL) = False Then
                            Throw New Exception()
                        End If
                        Cursor.Current = Cursors.Default
                        MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
                    Else
                        Cursor.Current = Cursors.Default
                        lblStatusMsg.BackColor = Color.Red
                    End If
                    lblStatusMsg.Text = msgCode
                    lblStatusMsgDesc.Text = msgDesc
                End If
            Else
                Call ChangeProcess()
            End If
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBackBPScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackBPScanModule.Click
        TimerCheckOnline.Enabled = False
        TimerCheckOnline.Dispose()
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
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlBPFScan, pnlBPScanModule)
    End Sub

    Private Sub ClearFS()
        txtFSModuleNo.Text = String.Empty
        txtFSOrderNo.Text = String.Empty
        lstViewRCVFScan.Items.Item(0).Focused = True
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Try
            If Validate() Then 'Validate required fields
                If (isNormal) Then
                    InsertModuleQR(txtFSModuleNo.Text, txtFSOrderNo.Text, cmbShop.SelectedValue, txtModuleQR.Text.Trim, lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                    Me.Text = strOnlineTitle
                    bringPanelToFront(pnlBPScanModule, pnlBPFScan)
                Else
                    InsertModuleQRAbn(txtFSModuleNo.Text, txtFSOrderNo.Text, cmbShopAbn.SelectedValue, lstViewRCVFScan.FocusedItem.SubItems(1).Text)
                    Me.Text = strOnlineTitle
                    bringPanelToFront(pnlBPAbnScan, pnlBPFScan)
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
                MessageBox.Show("Failed to read Module format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Me.Text = strOnlineTitle
        ClearFS()
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

#Region ". Abnormal Navigation and Private Function ."

    Private Sub LoadBPAbn()
        Me.Text = strOfflineTitle
        GetShop(cmbShopAbn)
        lblStatusMsgAbn.Text = String.Empty
        lblStatusMsgDescAbn.Text = String.Empty
        lblStatusMsgAbn.BackColor = Color.Transparent
        txtModuleQRAbn.Text = String.Empty
        txtModuleNoAbn.Text = String.Empty
        txtOrderNoAbn.Text = String.Empty
        lblTotalScannedAbn.Text = lstViewRcvDet.Items.Count
    End Sub

    Private Sub btnBPAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnScan.Click
        Call LoadBPAbn()
        bringPanelToFront(pnlBPAbnScan, pnlBPAbn)
    End Sub

    Private Sub btnBPAbnScanDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnScanDet.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}

        loadlstViewAbn(lstViewRcvDet, total)
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        lblStatusMsgAbn.Text = String.Empty
        lblStatusMsgDescAbn.Text = String.Empty
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
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPAbnScan)
    End Sub

    Private Sub btnBPAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnView.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}

        loadlstViewAbn(lstViewRcvDet, total)
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        Me.Text = String.Format("{0} - View", strOfflineTitle)
        bringPanelToFront(pnlBPAbnViewDet, pnlBPAbn)
    End Sub

    Private Sub btnCloseBPViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPAbnViewDet)
    End Sub

    Private Sub btnBPAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnPost.Click
        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    bringPanelToFront(pnlLogin, pnlBPAbn)
                    txtUsername.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("No connection to post.")
        End Try
    End Sub

    Private Sub btnCloseBPPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBPPosting.Click
        txtUsername.Text = String.Empty
        txtPwd.Text = String.Empty
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlBPAbn, pnlBPPosting)
    End Sub

    Private Sub btnBPAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPAbnDelete.Click
        loadlstViewDelete(lstViewDelete, lblDeleteTotalAbn)
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
                    If String.IsNullOrEmpty(txtModuleQRAbn.Text) Then
                        MsgBox("Module QR is required", MsgBoxStyle.Critical, gAppName)
                        txtModuleQRAbn.SelectAll()
                        txtModuleQRAbn.Focus()
                        Exit Sub
                    Else
                        InsertModuleQRAbn(txtModuleQRAbn.Text.Substring(0, 6), txtModuleQRAbn.Text.Substring(12), cmbShopAbn.SelectedValue)
                    End If
                Else
                    MessageBox.Show("Select Shop")
                End If
                txtModuleQRAbn.SelectAll()
                txtModuleQRAbn.Focus()
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                txtModuleQRAbn.SelectAll()
                txtModuleQRAbn.Focus()
                MessageBox.Show("Failed to read PxP QR format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub InsertModuleQRAbn(ByVal moduleNo As String, ByVal orderNo As String, ByVal shop As String, Optional ByVal reason As String = Nothing)
        Dim dbReader As SqlCeDataReader
        Dim forceStatus As String = "N"
        Dim currentDate As String = Nothing

        If Not String.IsNullOrEmpty(cmbShopAbn.Text) Then
            Cursor.Current = Cursors.WaitCursor
            dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE MODULE_NO = '{1}' AND SHOP_ID = {2}", TblJSPSupplyInterface, moduleNo, cmbShopAbn.SelectedValue), objConn)
            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    txtModuleQRAbn.Focus()
                    txtModuleQRAbn.SelectAll()
                    lblStatusMsgAbn.BackColor = Color.Red
                    lblStatusMsgAbn.Text = "Duplicated Part No"
                    Cursor.Current = Cursors.Default
                Else
                    txtModuleNoAbn.Text = moduleNo
                    txtOrderNoAbn.Text = orderNo
                    If reason <> 0 Then
                        forceStatus = "Y"
                    Else
                        reason = String.Empty
                    End If

                    currentDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")

                    Try
                        If reason.Length = 0 Or reason = Nothing Then
                            Call InsertTable(moduleNo, orderNo, "BP", cmbShopAbn.SelectedValue, currentDate, forceStatus, "N")

                        Else
                            Call InsertTable(moduleNo, orderNo, "BP", cmbShopAbn.SelectedValue, currentDate, forceStatus, "N", reason)
                        End If

                        lblStatusMsgAbn.BackColor = Color.LimeGreen
                        lblStatusMsgAbn.Text = "OK"
                        lblStatusMsgDescAbn.Text = String.Empty
                        txtModuleQRAbn.Text = String.Empty
                        txtModuleQRAbn.Focus()
                        Cursor.Current = Cursors.Default
                        loadlstViewAbn(lstViewRcvDet, lblTotalScannedAbn)
                    Catch ex As Exception
                        txtModuleQRAbn.SelectAll()
                        lblStatusMsgAbn.BackColor = Color.Red
                        lblStatusMsgAbn.Text = "Scan Process Error"
                        lblStatusMsgDescAbn.Text = ex.Message
                        Cursor.Current = Cursors.Default
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Select Shop")
        End If
    End Sub

    Private Sub btnBPSubmitPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPSubmitPosting.Click
        Dim forceScanReasonID As String = String.Empty
        Dim sSQL As String = Nothing
        Dim lstDt As DataTable = Nothing

        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    InitWebServices()

                    Cursor.Current = Cursors.WaitCursor
                    lstDt = getDTData(String.Format("SELECT * FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND SCANNER_SCREEN_CODE = '{2}'", TblJSPSupplyInterface, GetBatchID("BIG_PART", "4"), "BP"))
                    If lstDt.Rows.Count > 0 Then
                        For i As Integer = 0 To lstDt.Rows.Count - 1
                            If IsDBNull(lstDt.Rows(i).Item("FORCE_MODULE_REASON_ID")) Then
                                forceScanReasonID = String.Empty
                            End If

                            If forceScanReasonID.Length = 0 Then
                                msgCode = ws_validationClient.processValidation(GetBatchID("BIG_PART", "4"), GetScannerId(), "SUPPLY", "403", Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, lstDt.Rows(i).Item("FORCE_MODULE_STATUS").ToString(), Nothing, _
                                                                  Nothing, lstDt.Rows(i).Item("MODULE_NO").ToString(), Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, lstDt.Rows(i).Item("ORDER_NO").ToString(), Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  org_ID, Nothing, lstDt.Rows(i).Item("SUPPLY_BY").ToString(), Convert.ToDateTime(lstDt.Rows(i).Item("SUPPLY_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt"), Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, lstDt.Rows(i).Item("CREATED_BY").ToString(), Nothing, _
                                                                  Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SCAN_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt"), Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, "BP", lstDt.Rows(i).Item("SHOP_ID").ToString(), msgDesc)
                            Else
                                msgCode = ws_validationClient.processValidation(GetBatchID("BIG_PART", "4"), GetScannerId(), "SUPPLY", "403", Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, lstDt.Rows(i).Item("FORCE_MODULE_STATUS").ToString(), forceScanReasonID, _
                                                                  Nothing, lstDt.Rows(i).Item("MODULE_NO").ToString(), Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, lstDt.Rows(i).Item("ORDER_NO").ToString(), Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  org_ID, Nothing, lstDt.Rows(i).Item("SUPPLY_BY").ToString(), Convert.ToDateTime(lstDt.Rows(i).Item("SUPPLY_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt"), Nothing, _
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, lstDt.Rows(i).Item("CREATED_BY").ToString(), Nothing, _
                                                                  Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SCAN_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt"), Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                                  Nothing, "BP", lstDt.Rows(i).Item("SHOP_ID").ToString(), msgDesc)
                            End If


                            sSQL = String.Format("UPDATE {0} SET RETURN_VAL = '{1}' WHERE MODULE_NO = '{2}' AND SHOP_ID = {3}", TblJSPSupplyInterface, _
                                                    msgCode, lstDt.Rows(i).Item("MODULE_NO").ToString(), lstDt.Rows(i).Item("SHOP_ID").ToString())
                            ExecuteSQL(sSQL)

                        Next

                        msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("BIG_PART", "4"), "SUPPLY", "404", org_ID, Nothing, Nothing, _
                                                                        Nothing, Nothing, Nothing, msgDesc)

                        If msgCode = "OK" Then
                            sSQL = String.Format("UPDATE {0} SET POSTED = '{1}' WHERE RCV_INTERFACE_BATCH_ID = '{2}'", TblJSPSupplyInterface, _
                                                               "Y", GetBatchID("BIG_PART", "4"))
                            If ExecuteSQL(sSQL) = True Then
                                Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
                                sSQL = String.Format("UPDATE {0} SET CURRENT_NO = {1} WHERE CATEGORY = 'BIG_PART'", TblBatch, SQLQuote(currentNo))
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
            loadlstView(lstViewDelete, lblDeleteTotalAbn)
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
                    bringPanelToFront(pnlBPPosting, pnlLogin)
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
        bringPanelToFront(pnlBPAbn, pnlLogin)
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