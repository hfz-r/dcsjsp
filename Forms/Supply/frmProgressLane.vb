Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Net

Public Class frmProgressLane

#Region ". Variable Declaration ."
    Dim showError As Boolean = True
    Dim offline1stTFlag As Boolean = True
    Dim modScanOffline As Boolean = False
    Private Const strOnlineTitle As String = "Supply Progress Lane"
    Private Const strOfflineTitle As String = "Abnormal Supply Progress Lane"
    Dim isNormal As Boolean = True
    Dim isForceScan As Boolean = False
    Dim msgCode As String = Nothing
    Dim msgDesc As String = Nothing
    Private SHOP As New Shopping_Input()
    Private KB As New KanbanQR_Input()
    Private PART As New PartQR_Input()
    Dim reasonShop As String = Nothing
    Dim reasonInt As String = Nothing
    Dim reasonExt As String = Nothing
#End Region

#Region ". Properties ."
    Private Class Shopping_Input
        Private _pdio_id As String
        Property PDIO_ID() As String
            Get
                Return _pdio_id
            End Get
            Set(ByVal value As String)
                _pdio_id = value
            End Set
        End Property

        Private _pdio_no As String
        Property PDIO_NO() As String
            Get
                Return _pdio_no
            End Get
            Set(ByVal value As String)
                _pdio_no = value
            End Set
        End Property

        Private _production_date As Date
        Property PRODUCTION_DATE() As Date
            Get
                Return _production_date
            End Get
            Set(ByVal value As Date)
                _production_date = value
            End Set
        End Property

        Private _shop_id As String
        Property SHOP_ID() As String
            Get
                Return _shop_id
            End Get
            Set(ByVal value As String)
                _shop_id = value
            End Set
        End Property

        Private _lane_id As String
        Property LANE_ID() As String
            Get
                Return _lane_id
            End Get
            Set(ByVal value As String)
                _lane_id = value
            End Set
        End Property

        Private _org_id As String
        Property ORG_ID() As String
            Get
                Return _org_id
            End Get
            Set(ByVal value As String)
                _org_id = value
            End Set
        End Property

        Private _scan_flag As Char
        Property SCAN_FLAG() As Char
            Get
                Return _scan_flag
            End Get
            Set(ByVal value As Char)
                _scan_flag = value
            End Set
        End Property
    End Class

    Private Class KanbanQR_Input
        Private _pdio_id As String = String.Empty
        Property PDIO_ID() As String
            Get
                Return _pdio_id
            End Get
            Set(ByVal value As String)
                _pdio_id = value
            End Set
        End Property

        Private _pdio_no As String = String.Empty
        Property PDIO_NO() As String
            Get
                Return _pdio_no
            End Get
            Set(ByVal value As String)
                _pdio_no = value
            End Set
        End Property

        Private _delivery_date As String = String.Empty
        Property DELIVERY_DATE() As String
            Get
                Return _delivery_date
            End Get
            Set(ByVal value As String)
                _delivery_date = value
            End Set
        End Property

        Private _dock_code As String = String.Empty
        Property DOCK_CODE() As String
            Get
                Return _dock_code
            End Get
            Set(ByVal value As String)
                _dock_code = value
            End Set
        End Property

        Private _order_type As String = String.Empty
        Property ORDER_TYPE() As String
            Get
                Return _order_type
            End Get
            Set(ByVal value As String)
                _order_type = value
            End Set
        End Property

        Private _vendor_id As String = String.Empty
        Property VENDOR_ID() As String
            Get
                Return _vendor_id
            End Get
            Set(ByVal value As String)
                _vendor_id = value
            End Set
        End Property

        Private _transporter_id As String = String.Empty
        Property TRANSPORTER_ID() As String
            Get
                Return _transporter_id
            End Get
            Set(ByVal value As String)
                _transporter_id = value
            End Set
        End Property

        Private _lane_id As String = String.Empty
        Property LANE_ID() As String
            Get
                Return _lane_id
            End Get
            Set(ByVal value As String)
                _lane_id = value
            End Set
        End Property

        Private _tier As String = String.Empty
        Property TIER() As String
            Get
                Return _tier
            End Get
            Set(ByVal value As String)
                _tier = value
            End Set
        End Property

        Private _org_id As String = String.Empty
        Property ORG_ID() As String
            Get
                Return _org_id
            End Get
            Set(ByVal value As String)
                _org_id = value
            End Set
        End Property

        Private _part_no As String = String.Empty
        Property PART_NO() As String
            Get
                Return _part_no
            End Get
            Set(ByVal value As String)
                _part_no = value
            End Set
        End Property

        Private _back_no As String = String.Empty
        Property BACK_NO() As String
            Get
                Return _back_no
            End Get
            Set(ByVal value As String)
                _back_no = value
            End Set
        End Property

        Private _seq_no As String = String.Empty
        Property SEQ_NO() As String
            Get
                Return _seq_no
            End Get
            Set(ByVal value As String)
                _seq_no = value
            End Set
        End Property

        Private _transaction_code As String = String.Empty
        Property TRANSACTION_CODE() As String
            Get
                Return _transaction_code
            End Get
            Set(ByVal value As String)
                _transaction_code = value
            End Set
        End Property

        Private _qty_order As String = String.Empty
        Property QTY_ORDER() As String
            Get
                Return _qty_order
            End Get
            Set(ByVal value As String)
                _qty_order = value
            End Set
        End Property

        Private _delivery_type As String = String.Empty
        Property DELIVERY_TYPE() As String
            Get
                Return _delivery_type
            End Get
            Set(ByVal value As String)
                _delivery_type = value
            End Set
        End Property

        Private _scan_flag As String = String.Empty
        Property SCAN_FLAG() As String
            Get
                Return _scan_flag
            End Get
            Set(ByVal value As String)
                _scan_flag = value
            End Set
        End Property
    End Class

    Private Class PartQR_Input
        Private _manufacture_code As String = String.Empty
        Property MANUFACTURE_CODE() As String
            Get
                Return _manufacture_code
            End Get
            Set(ByVal value As String)
                _manufacture_code = value
            End Set
        End Property

        Private _module_id As String = String.Empty
        Property MODULE_ID() As String
            Get
                Return _module_id
            End Get
            Set(ByVal value As String)
                _module_id = value
            End Set
        End Property

        Private _module_no As String = String.Empty
        Property MODULE_NO() As String
            Get
                Return _module_no
            End Get
            Set(ByVal value As String)
                _module_no = value
            End Set
        End Property

        Private _part_id As String = String.Empty
        Property PART_ID() As String
            Get
                Return _part_id
            End Get
            Set(ByVal value As String)
                _part_id = value
            End Set
        End Property

        Private _supplier_code As String = String.Empty
        Property SUPPLIER_CODE() As String
            Get
                Return _supplier_code
            End Get
            Set(ByVal value As String)
                _supplier_code = value
            End Set
        End Property

        Private _supplier_plant_code As String = String.Empty
        Property SUPPLIER_PLANT_CODE() As String
            Get
                Return _supplier_plant_code
            End Get
            Set(ByVal value As String)
                _supplier_plant_code = value
            End Set
        End Property

        Private _supplier_shipping_dock As String = String.Empty
        Property SUPPLIER_SHIPPING_DOCK() As String
            Get
                Return _supplier_shipping_dock
            End Get
            Set(ByVal value As String)
                _supplier_shipping_dock = value
            End Set
        End Property

        Private _before_packing_routing As String = String.Empty
        Property BEFORE_PACKING_ROUTING() As String
            Get
                Return _before_packing_routing
            End Get
            Set(ByVal value As String)
                _before_packing_routing = value
            End Set
        End Property

        Private _receiving_company_code As String = String.Empty
        Property RECEIVING_COMPANY_CODE() As String
            Get
                Return _receiving_company_code
            End Get
            Set(ByVal value As String)
                _receiving_company_code = value
            End Set
        End Property

        Private _receiving_plant_code As String = String.Empty
        Property RECEIVING_PLANT_CODE() As String
            Get
                Return _receiving_plant_code
            End Get
            Set(ByVal value As String)
                _receiving_plant_code = value
            End Set
        End Property

        Private _receiving_dock_code As String = String.Empty
        Property RECEIVING_DOCK_CODE() As String
            Get
                Return _receiving_dock_code
            End Get
            Set(ByVal value As String)
                _receiving_dock_code = value
            End Set
        End Property

        Private _packing_routing_code As String = String.Empty
        Property PACKING_ROUTING_CODE() As String
            Get
                Return _packing_routing_code
            End Get
            Set(ByVal value As String)
                _packing_routing_code = value
            End Set
        End Property

        Private _granter_code As String = String.Empty
        Property GRANTER_CODE() As String
            Get
                Return _granter_code
            End Get
            Set(ByVal value As String)
                _granter_code = value
            End Set
        End Property

        Private _order_type As String = String.Empty
        Property ORDER_TYPE() As String
            Get
                Return _order_type
            End Get
            Set(ByVal value As String)
                _order_type = value
            End Set
        End Property

        Private _kanban_classification As String = String.Empty
        Property KANBAN_CLASSIFICATION() As String
            Get
                Return _kanban_classification
            End Get
            Set(ByVal value As String)
                _kanban_classification = value
            End Set
        End Property

        Private _delivery_date As String = String.Empty
        Property DELIVERY_DATE() As String
            Get
                Return _delivery_date
            End Get
            Set(ByVal value As String)
                _delivery_date = value
            End Set
        End Property

        Private _delivery_code As String = String.Empty
        Property DELIVERY_CODE() As String
            Get
                Return _delivery_code
            End Get
            Set(ByVal value As String)
                _delivery_code = value
            End Set
        End Property

        Private _mros As String = String.Empty
        Property MROS() As String
            Get
                Return _mros
            End Get
            Set(ByVal value As String)
                _mros = value
            End Set
        End Property

        Private _order_number As String = String.Empty
        Property ORDER_NUMBER() As String
            Get
                Return _order_number
            End Get
            Set(ByVal value As String)
                _order_number = value
            End Set
        End Property

        Private _delivery_number As String = String.Empty
        Property DELIVERY_NUMBER() As String
            Get
                Return _delivery_number
            End Get
            Set(ByVal value As String)
                _delivery_number = value
            End Set
        End Property

        Private _back_number As String = String.Empty
        Property BACK_NUMBER() As String
            Get
                Return _back_number
            End Get
            Set(ByVal value As String)
                _back_number = value
            End Set
        End Property

        Private _parts_no As String = String.Empty
        Property PARTS_NO() As String
            Get
                Return _parts_no
            End Get
            Set(ByVal value As String)
                _parts_no = value
            End Set
        End Property

        Private _part_no_sfx As String = String.Empty
        Property PART_NO_SFX() As String
            Get
                Return _part_no_sfx
            End Get
            Set(ByVal value As String)
                _part_no_sfx = value
            End Set
        End Property

        Private _qty_box As String = String.Empty
        Property QTY_BOX() As String
            Get
                Return _qty_box
            End Get
            Set(ByVal value As String)
                _qty_box = value
            End Set
        End Property

        Private _runout_flag As String = String.Empty
        Property RUNOUT_FLAG() As String
            Get
                Return _runout_flag
            End Get
            Set(ByVal value As String)
                _runout_flag = value
            End Set
        End Property

        Private _delivery_code_2 As String = String.Empty
        Property DELIVERY_CODE_2() As String
            Get
                Return _delivery_code_2
            End Get
            Set(ByVal value As String)
                _delivery_code_2 = value
            End Set
        End Property

        Private _box_type As String = String.Empty
        Property BOX_TYPE() As String
            Get
                Return _box_type
            End Get
            Set(ByVal value As String)
                _box_type = value
            End Set
        End Property

        Private _branch_number As String = String.Empty
        Property BRANCH_NUMBER() As String
            Get
                Return _branch_number
            End Get
            Set(ByVal value As String)
                _branch_number = value
            End Set
        End Property

        Private _address As String = String.Empty
        Property ADDRESS() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        Private _delivery_time As String = String.Empty
        Property DELIVERY_TIME() As String
            Get
                Return _delivery_time
            End Get
            Set(ByVal value As String)
                _delivery_time = value
            End Set
        End Property

        Private _packing_date As String = String.Empty
        Property PACKING_DATE() As String
            Get
                Return _packing_date
            End Get
            Set(ByVal value As String)
                _packing_date = value
            End Set
        End Property

        Private _katashiki_jersey_number As String = String.Empty
        Property KATASHIKI_JERSEY_NUMBER() As String
            Get
                Return _katashiki_jersey_number
            End Get
            Set(ByVal value As String)
                _katashiki_jersey_number = value
            End Set
        End Property

        Private _lot_no As String = String.Empty
        Property LOT_NO() As String
            Get
                Return _lot_no
            End Get
            Set(ByVal value As String)
                _lot_no = value
            End Set
        End Property

        Private _module_category As String = String.Empty
        Property MODULE_CATEGORY() As String
            Get
                Return _module_category
            End Get
            Set(ByVal value As String)
                _module_category = value
            End Set
        End Property

        Private _part_seq_number As String = String.Empty
        Property PART_SEQ_NUMBER() As String
            Get
                Return _part_seq_number
            End Get
            Set(ByVal value As String)
                _part_seq_number = value
            End Set
        End Property

        Private _part_branch_number As String = String.Empty
        Property PART_BRANCH_NUMBER() As String
            Get
                Return _part_branch_number
            End Get
            Set(ByVal value As String)
                _part_branch_number = value
            End Set
        End Property

        Private _dummy As String = String.Empty
        Property DUMMY() As String
            Get
                Return _dummy
            End Get
            Set(ByVal value As String)
                _dummy = value
            End Set
        End Property

        Private _version_no As String = String.Empty
        Property VERSION_NO() As String
            Get
                Return _version_no
            End Get
            Set(ByVal value As String)
                _version_no = value
            End Set
        End Property

    End Class
#End Region

#Region ". General Function ."

    Private Sub GetReason(ByVal lstView As System.Windows.Forms.ListView)
        lstView.Items.Clear()
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = getDTData(String.Format("SELECT REASON, REASON_CODE FROM {0}", TblJSPAbnormalReasonCodeDb))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = lstDt.Rows(i).Item("REASON").ToString
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("REASON_CODE").ToString)
            lstView.Items.Add(lstViewItem)
        Next
    End Sub

    Private Sub GetTxnCode()
        Dim comboSource As New Dictionary(Of String, String)()
        Dim lstDt As DataTable = getDTData(String.Format("SELECT TXN_CODE, TXN_VALUE FROM {0}", TblTxnCodeDb))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            comboSource.Add(lstDt.Rows(i).Item("TXN_VALUE").ToString, lstDt.Rows(i).Item("TXN_CODE").ToString)
        Next

        cmbBoxTxnCode.DataSource = New BindingSource(comboSource, Nothing)
        cmbBoxTxnCode.DisplayMember = "Value"
        cmbBoxTxnCode.ValueMember = "Key"
        cmbBoxTxnCode.SelectedIndex = -1
    End Sub

    Private Function ConnectionStatus() As Boolean
        Dim isOnline As Boolean = False
        ConnectionStatus = False
        Try
            If ws_dcsClient.isConnected() Then
                If ws_dcsClient.isOracleConnected() Then
                    isOnline = True
                End If
            Else
                TimerCheckOnline.Enabled = True
                TimerCheckOnline.Interval = iInterval
                isOnline = False
            End If
        Catch ex As Exception
            TimerCheckOnline.Enabled = True
            TimerCheckOnline.Interval = iInterval
            isOnline = False
        End Try
        ConnectionStatus = isOnline
        Return ConnectionStatus
    End Function

    Private Sub ChangeProcess()
        Cursor.Current = Cursors.Default
        If showError = True Then
            showError = False
            If modScanOffline Then
                showError = True
                Exit Sub
            Else
                If MessageBox.Show("Connection is down. Change to Abnormal Process?", "PDIO Process Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    TimerCheckOnline.Enabled = False
                    TimerCheckOnline.Dispose()
                    showError = True
                    bringPanelToFront(pnlPLAbn, pnlPLScanShopping)
                    Exit Sub
                Else
                    showError = True
                    txtShoppingNo.Focus()
                    txtShoppingNo.SelectAll()
                End If
            End If
        End If
    End Sub

    Private Sub TimerCheckOnline_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckOnline.Tick
        Try
            If mode = False Then
                If ws_dcsClient.isConnected Then
                    If ws_dcsClient.isOracleConnected Then
                        MsgBox("Connection is back.", MsgBoxStyle.Information, Me.Text)
                        mode = True
                        offline1stTFlag = True
                        modScanOffline = True
                        TimerCheckOnline.Enabled = False
                        TimerCheckOnline.Dispose()
                    End If
                End If
            End If
        Catch ex As Exception
            Call ChangeProcess()
        End Try
    End Sub

    Private Sub loadlstView(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        lstView.Items.Clear()
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = getDTData(String.Format( _
                                           "SELECT PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, " & _
                                           "P2_PART_NO, P2_PART_SEQ_NO, QTY_ORDER FROM {0} WHERE " & _
                                           "SCANNER_SCREEN_CODE = '{1}' AND ON_OFF_LINE_FLAG = 'Y' AND PDIO_NO = {2}", TblJSPSupplyInterface, "PL", SQLQuote(SHOP.PDIO_NO)))

        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            If IsDBNull(lstDt.Rows(i).Item("PXP_PART_SEQ_NO")) Then
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(String.Empty)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_ORDER").ToString)
                lstViewItem.SubItems.Add(String.Empty)
                lstViewItem.SubItems.Add(String.Empty)
            Else
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_ORDER").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_BOX").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString)
            End If

            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub loadlstViewAbn(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        lstView.Items.Clear()
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = getDTData(String.Format( _
                                           "SELECT PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, " & _
                                           "P2_PART_NO, P2_PART_SEQ_NO, QTY_ORDER FROM {0} WHERE " & _
                                           "SCANNER_SCREEN_CODE = '{1}' AND ON_OFF_LINE_FLAG = 'N' " & _
                                           "AND POSTED IS NULL OR POSTED = '' " & _
                                           "OR POSTED != 'Y'", TblJSPSupplyInterface, "PL"))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            If IsDBNull(lstDt.Rows(i).Item("PXP_PART_SEQ_NO")) Then
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(String.Empty)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_ORDER").ToString)
                lstViewItem.SubItems.Add(String.Empty)
                lstViewItem.SubItems.Add(String.Empty)
            Else
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_ORDER").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_BOX").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString)
            End If

            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub loadlstViewAbnDelete(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        lstView.Items.Clear()
        Dim lstViewItem As ListViewItem
        Dim lstDt As DataTable = getDTData(String.Format( _
                                           "SELECT PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, " & _
                                           "P2_PART_NO, P2_PART_SEQ_NO, QTY_ORDER FROM {0} WHERE" & _
                                           " SCANNER_SCREEN_CODE = '{1}' AND ON_OFF_LINE_FLAG = 'N'", TblJSPSupplyInterface, "PL"))

        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            If IsDBNull(lstDt.Rows(i).Item("PXP_PART_SEQ_NO")) Then
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(String.Empty)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_ORDER").ToString)
                lstViewItem.SubItems.Add(String.Empty)
                lstViewItem.SubItems.Add(String.Empty)
            Else
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_ORDER").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("QTY_BOX").ToString)
                lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PART_BRANCH_NO").ToString)
            End If

            lstView.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub loadlstPendingView(ByVal lstView As System.Windows.Forms.ListView, ByVal total As System.Windows.Forms.Label)
        lstView.Items.Clear()
        Dim lstViewItem As ListViewItem = New ListViewItem

        Dim lstDt As DataTable = getDTData(String.Format("SELECT * FROM {0} WHERE PDIO_NO = {1}", TblJSPSupplyPLPendingView, SQLQuote(SHOP.PDIO_NO)))
        For i As Integer = 0 To lstDt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PDIO_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("PART_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("SEQ_NO").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("ADVICEQTY").ToString)
            lstViewItem.SubItems.Add(lstDt.Rows(i).Item("BACK_NUMBER").ToString)
            lstViewPendingSummary.Items.Add(lstViewItem)
        Next
        total.Text = lstView.Items.Count
    End Sub

    Private Sub VerifyOrgId(ByVal qrShopID As String)
        If Not org_ID = qrShopID Then
            Throw New CustomException("Organization ID does not match Setting configuration.")
        End If
    End Sub

    Private Function IsNumber(ByVal input As String) As Boolean
        Return Regex.IsMatch(input, "^[0-9 ]+$")
    End Function

    Private Function ValidatePDIO() As Boolean
        If String.IsNullOrEmpty(txtFSPDIO.Text) Then
            MessageBox.Show("PDIO No is required")
            Return False
        ElseIf txtFSPDIO.Text.Length > 13 Then
            MessageBox.Show("Invalid PDIO No format")
            Return False
        ElseIf (lstViewPDIOFScan.FocusedItem Is Nothing) Then
            MessageBox.Show("Reason is required")
            Return False
        End If

        reasonShop = lstViewPDIOFScan.FocusedItem.SubItems(1).Text

        Return True
    End Function

    Private Function ValidateInt() As Boolean
        If String.IsNullOrEmpty(txtFSKanbanPartNo.Text) Then
            MessageBox.Show("Part No is required")
            txtFSKanbanPartNo.SelectAll()
            txtFSKanbanPartNo.Focus()
            Return False
        ElseIf txtFSKanbanPartNo.Text.Length <> 14 Then
            MessageBox.Show("Invalid Part No format")
            txtFSKanbanPartNo.SelectAll()
            txtFSKanbanPartNo.Focus()
            Return False
        ElseIf String.IsNullOrEmpty(txtFSKanbanSeqNo.Text) Then
            MessageBox.Show("Seq No is required")
            txtFSKanbanSeqNo.SelectAll()
            txtFSKanbanSeqNo.Focus()
            Return False
        ElseIf txtFSKanbanSeqNo.Text.Length <> 2 Then
            MessageBox.Show("Invalid Seq No format")
            txtFSKanbanSeqNo.SelectAll()
            txtFSKanbanSeqNo.Focus()
            Return False
        ElseIf Not isNormal Then
            If cmbBoxTxnCode.SelectedIndex = -1 Then
                MessageBox.Show("Transaction Code is required")
                cmbBoxTxnCode.SelectAll()
                cmbBoxTxnCode.Focus()
                Return False
            End If
        ElseIf (lstViewFScanInt.FocusedItem Is Nothing) Then
            MessageBox.Show("Reason is required")
            Return False
        End If

        reasonInt = lstViewFScanInt.FocusedItem.SubItems(1).Text

        Return True
    End Function

    Private Function ValidateExt() As Boolean
        If String.IsNullOrEmpty(txtFSPxPModuleNo.Text) Then
            MessageBox.Show("Module No is required")
            txtFSPxPModuleNo.SelectAll()
            txtFSPxPModuleNo.Focus()
            Return False
        ElseIf txtFSPxPModuleNo.Text.Length <> 6 Then
            MessageBox.Show("Invalid Module No format")
            txtFSPxPModuleNo.SelectAll()
            txtFSPxPModuleNo.Focus()
            Return False
        ElseIf String.IsNullOrEmpty(txtFSPxPPartNo.Text) Then
            MessageBox.Show("Part No is required")
            txtFSPxPPartNo.SelectAll()
            txtFSPxPPartNo.Focus()
            Return False
        ElseIf txtFSPxPPartNo.Text.Length <> 14 Then
            MessageBox.Show("Invalid Part No format")
            txtFSPxPPartNo.SelectAll()
            txtFSPxPPartNo.Focus()
            Return False
        ElseIf String.IsNullOrEmpty(txtFSPxPSeqNo.Text) Then
            MessageBox.Show("Seq No is required")
            txtFSPxPSeqNo.SelectAll()
            txtFSPxPSeqNo.Focus()
            Return False
        ElseIf txtFSPxPSeqNo.Text.Length <> 2 Then
            MessageBox.Show("Invalid Seq No format")
            txtFSPxPSeqNo.SelectAll()
            txtFSPxPSeqNo.Focus()
            Return False
        ElseIf String.IsNullOrEmpty(txtFSPxPBranch.Text) Then
            MessageBox.Show("Branch No is required")
            txtFSPxPBranch.SelectAll()
            txtFSPxPBranch.Focus()
            Return False
        ElseIf txtFSPxPBranch.Text.Length <> 2 Then
            MessageBox.Show("Invalid Branch No format")
            txtFSPxPBranch.SelectAll()
            txtFSPxPBranch.Focus()
            Return False
        ElseIf (lstViewFScanExt.FocusedItem Is Nothing) Then
            MessageBox.Show("Reason is required")
            Return False
        End If

        reasonExt = lstViewFScanExt.FocusedItem.SubItems(1).Text

        Return True
    End Function

    Private Sub ClearFSInt()
        txtFSKanbanPartNo.Text = String.Empty
        txtFSKanbanSeqNo.Text = String.Empty
        lstViewFScanInt.Items.Item(0).Focused = True
    End Sub

    Private Sub ClearFSExt()
        txtFSPxPModuleNo.Text = String.Empty
        txtFSPxPPartNo.Text = String.Empty
        txtFSPxPSeqNo.Text = String.Empty
        txtFSPxPBranch.Text = String.Empty
        lstViewFScanExt.Focus()
    End Sub

#End Region

#Region ". Read Barcode Helper ."

    Private Function IsPDIOReadable(ByVal input As String) As Boolean
        Dim errorMsg As String = "Invalid Shopping/PDIO QR"
        IsPDIOReadable = False
        Try
            If Not String.IsNullOrEmpty(input) And input.Length > 41 Then
                Throw New FormatException(errorMsg)
            End If

            Dim temp_pdio_id As String = input.Substring(0, SL_PDIO_ID_LENGTH)
            If Not String.IsNullOrEmpty(temp_pdio_id) And IsNumber(temp_pdio_id) Then
                SHOP.PDIO_ID = temp_pdio_id
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim pdioNoLength As Integer = input.Length - 10 - (SL_PRODUCTION_DATE_LENGTH + SL_SHOP_ID_LENGTH + SL_LANE_ID_LENGTH + SL_ORG_ID_LENGTH + SL_SCAN_FLAG_LENGTH)
            If pdioNoLength <= SL_PDIO_NO_LENGTH Then
                Dim temp_pdio_no As String = input.Substring(10, pdioNoLength)
                If Not String.IsNullOrEmpty(temp_pdio_no) Then
                    SHOP.PDIO_NO = temp_pdio_no.Trim()
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim pdNewIndex As Integer = 10 + pdioNoLength
            Dim format As String = "dd/MM/yyyy"
            Dim temp_prod_date As String = input.Substring(pdNewIndex, SL_PRODUCTION_DATE_LENGTH)
            If Not String.IsNullOrEmpty(temp_prod_date) Then
                Try
                    Dim formatted As Date = Date.ParseExact(temp_prod_date, format, CultureInfo.InvariantCulture)
                    SHOP.PRODUCTION_DATE = formatted
                Catch ex As Exception
                    Throw New FormatException(errorMsg)
                End Try
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim shopNewIndex As Integer = pdNewIndex + SL_PRODUCTION_DATE_LENGTH
            Dim ShopIdLength As Integer = input.Length - shopNewIndex - (SL_LANE_ID_LENGTH + SL_ORG_ID_LENGTH + SL_SCAN_FLAG_LENGTH)
            If ShopIdLength <= SL_SHOP_ID_LENGTH Then
                Dim temp_shop_id As String = input.Substring(shopNewIndex, ShopIdLength)
                If Not String.IsNullOrEmpty(temp_shop_id) Then
                    SHOP.SHOP_ID = temp_shop_id.Trim()
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim laneNewIndex As Integer = shopNewIndex + SL_SHOP_ID_LENGTH
            Dim laneIdLength As Integer = input.Length - laneNewIndex - (SL_ORG_ID_LENGTH + SL_SCAN_FLAG_LENGTH)
            If laneIdLength <= SL_LANE_ID_LENGTH Then
                Dim temp_lane_id As String = input.Substring(laneNewIndex, laneIdLength)
                If Not String.IsNullOrEmpty(temp_lane_id) Then
                    SHOP.LANE_ID = temp_lane_id.Trim()
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim orgIdNewIndex As Integer = laneNewIndex + SL_LANE_ID_LENGTH
            Dim temp_org_id As String = input.Substring(orgIdNewIndex, SL_ORG_ID_LENGTH)
            If Not String.IsNullOrEmpty(temp_org_id) Then
                SHOP.ORG_ID = temp_org_id
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim scanFlagNewIndex As Integer = orgIdNewIndex + SL_ORG_ID_LENGTH
            Dim temp_scan_flag As String = input.Substring(scanFlagNewIndex, SL_SCAN_FLAG_LENGTH)
            If Not String.IsNullOrEmpty(temp_scan_flag) Then
                SHOP.SCAN_FLAG = temp_scan_flag
            Else
                Throw New FormatException(errorMsg)
            End If

            IsPDIOReadable = True
        Catch ex As Exception
            IsPDIOReadable = False
            If ex.Message = "Specified argument was out of the range of valid values." Then
                MessageBox.Show("Failed to read PDIO format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try

        Return IsPDIOReadable
    End Function

    Private Function IsKanbanQRReadable(ByVal input As String) As Boolean
        Dim errorMsg As String = "Invalid Kanban Part QR"
        IsKanbanQRReadable = False
        Try
            If Not String.IsNullOrEmpty(input) And input.Length > 88 Then
                Throw New FormatException(errorMsg)
            End If

            Dim temp_pdio_id As String = input.Substring(0, KB_PDIO_ID_LENGTH)
            If Not String.IsNullOrEmpty(temp_pdio_id) And IsNumber(temp_pdio_id) Then
                KB.PDIO_ID = temp_pdio_id
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim pdioNoLength As Integer = input.Length - 10 - (KanbanIntLength(KB_PDIO_NO_LENGTH))
            If pdioNoLength <= KB_PDIO_NO_LENGTH Then
                Dim temp_pdio_no As String = input.Substring(10, pdioNoLength)
                If Not String.IsNullOrEmpty(temp_pdio_no) Then
                    KB.PDIO_NO = temp_pdio_no.Trim()
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim ddNewIndex As Integer = 10 + pdioNoLength
            Dim temp_delivery_date As String = input.Substring(ddNewIndex, KB_DELIVERY_DATE_LENGTH)
            If Not String.IsNullOrEmpty(temp_delivery_date) Then
                KB.DELIVERY_DATE = temp_delivery_date
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim dcNewIndex As Integer = ddNewIndex + KB_DELIVERY_DATE_LENGTH
            Dim dockcodeLength As Integer = input.Length - dcNewIndex - (KanbanIntLength(KB_DOCK_CODE_LENGTH))
            If dockcodeLength <= KB_DOCK_CODE_LENGTH Then
                Dim temp_dock_code As String = input.Substring(dcNewIndex, KB_DOCK_CODE_LENGTH)
                If Not String.IsNullOrEmpty(temp_dock_code) Then
                    KB.DOCK_CODE = temp_dock_code
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim odNewIndex As Integer = dcNewIndex + KB_DOCK_CODE_LENGTH
            Dim ordertypeLength As Integer = input.Length - odNewIndex - (KanbanIntLength(KB_ORDER_TYPE_LENGTH))
            If ordertypeLength <= KB_ORDER_TYPE_LENGTH Then
                Dim temp_order_type As String = input.Substring(odNewIndex, KB_ORDER_TYPE_LENGTH)
                If Not String.IsNullOrEmpty(temp_order_type) Then
                    KB.ORDER_TYPE = temp_order_type
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim vIDnewIndex As Integer = odNewIndex + KB_ORDER_TYPE_LENGTH
            Dim vIDLength As Integer = input.Length - vIDnewIndex - (KanbanIntLength(KB_VENDOR_ID_LENGTH))
            If vIDLength <= KB_VENDOR_ID_LENGTH Then
                Dim temp_vendor_id As String = input.Substring(vIDnewIndex, KB_VENDOR_ID_LENGTH)
                If Not String.IsNullOrEmpty(temp_vendor_id) Then
                    KB.VENDOR_ID = temp_vendor_id
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim tIDnewIndex As Integer = vIDnewIndex + KB_VENDOR_ID_LENGTH
            Dim tIDLength As Integer = input.Length - tIDnewIndex - (KanbanIntLength(KB_TRANSPORTER_ID_LENGTH))
            If tIDLength <= KB_TRANSPORTER_ID_LENGTH Then
                Dim temp_transporter_id As String = input.Substring(tIDnewIndex, KB_TRANSPORTER_ID_LENGTH)
                If Not String.IsNullOrEmpty(temp_transporter_id) Then
                    KB.TRANSPORTER_ID = temp_transporter_id
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim lIDnewIndex As Integer = tIDnewIndex + KB_TRANSPORTER_ID_LENGTH
            Dim lIDLength As Integer = input.Length - lIDnewIndex - (KanbanIntLength(KB_LANE_ID_LENGTH))
            If lIDLength <= KB_LANE_ID_LENGTH Then
                Dim temp_lane_id As String = input.Substring(lIDnewIndex, KB_LANE_ID_LENGTH)
                If Not String.IsNullOrEmpty(temp_lane_id) Then
                    KB.LANE_ID = temp_lane_id
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim tierNewIndex As Integer = lIDnewIndex + KB_LANE_ID_LENGTH
            Dim temp_tier As String = input.Substring(tierNewIndex, KB_TIER_LENGTH)
            If Not String.IsNullOrEmpty(temp_tier) Then
                KB.TIER = temp_tier
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim orgIdNewIndex As Integer = tierNewIndex + KB_TIER_LENGTH
            Dim temp_org_id As String = input.Substring(orgIdNewIndex, KB_ORG_ID_LENGTH)
            If Not String.IsNullOrEmpty(temp_org_id) Then
                KB.ORG_ID = temp_org_id
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim partNoNewIndex As Integer = orgIdNewIndex + KB_ORG_ID_LENGTH
            Dim temp_part_no As String = input.Substring(partNoNewIndex, KB_PART_NO_LENGTH)
            If Not String.IsNullOrEmpty(temp_part_no) Then
                KB.PART_NO = temp_part_no
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim bnNewIndex As Integer = partNoNewIndex + KB_PART_NO_LENGTH
            Dim bnLength As Integer = input.Length - bnNewIndex - (KanbanIntLength(KB_BACK_NO_LENGTH))
            If bnLength <= KB_BACK_NO_LENGTH Then
                Dim temp_back_no As String = input.Substring(bnNewIndex, KB_BACK_NO_LENGTH)
                If Not String.IsNullOrEmpty(temp_back_no) Then
                    KB.BACK_NO = temp_back_no
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim seqnoNewIndex As Integer = bnNewIndex + KB_BACK_NO_LENGTH
            Dim seqnoLength As Integer = input.Length - seqnoNewIndex - (KanbanIntLength(KB_SEQ_NO_LENGTH))
            If seqnoLength <= KB_SEQ_NO_LENGTH Then
                Dim temp_seqno As String = input.Substring(seqnoNewIndex, KB_SEQ_NO_LENGTH)
                If Not String.IsNullOrEmpty(temp_seqno) Then
                    KB.SEQ_NO = temp_seqno
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim transCodeNewIndex As Integer = seqnoNewIndex + KB_SEQ_NO_LENGTH
            Dim transCodeLength As Integer = input.Length - transCodeNewIndex - (KanbanIntLength(KB_TRANSACTION_CODE_LENGTH))
            If transCodeLength <= KB_TRANSACTION_CODE_LENGTH Then
                Dim temp_trans_code As String = input.Substring(transCodeNewIndex, KB_TRANSACTION_CODE_LENGTH)
                If Not String.IsNullOrEmpty(temp_trans_code) Then
                    KB.TRANSACTION_CODE = temp_trans_code
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim qorderNewIndex As Integer = transCodeNewIndex + KB_TRANSACTION_CODE_LENGTH
            Dim qorderLength As Integer = input.Length - qorderNewIndex - (KanbanIntLength(KB_QTY_ORDER_LENGTH))
            If qorderLength <= KB_QTY_ORDER_LENGTH Then
                Dim temp_qty As String = input.Substring(qorderNewIndex, KB_QTY_ORDER_LENGTH)
                If Not String.IsNullOrEmpty(temp_qty) Then
                    KB.QTY_ORDER = temp_qty
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim dtypeNewIndex As Integer = qorderNewIndex + KB_QTY_ORDER_LENGTH
            Dim dtypeLength As Integer = input.Length - dtypeNewIndex - (KanbanIntLength(KB_DELIVERY_TYPE_LENGTH))
            If dtypeLength <= KB_DELIVERY_TYPE_LENGTH Then
                Dim temp_del_type As String = input.Substring(dtypeNewIndex, KB_DELIVERY_TYPE_LENGTH)
                If Not String.IsNullOrEmpty(temp_del_type) Then
                    KB.DELIVERY_TYPE = temp_del_type
                Else
                    Throw New FormatException(errorMsg)
                End If
            End If

            Dim scanFlagNewIndex As Integer = dtypeNewIndex + KB_DELIVERY_TYPE_LENGTH
            Dim temp_scan_flag As String = input.Substring(scanFlagNewIndex, KB_SCAN_FLAG_LENGTH)
            If Not String.IsNullOrEmpty(temp_scan_flag) Then
                KB.SCAN_FLAG = temp_scan_flag
            Else
                Throw New FormatException(errorMsg)
            End If

            IsKanbanQRReadable = True
        Catch ex As Exception
            IsKanbanQRReadable = False
            If ex.Message = "Specified argument was out of the range of valid values." Then
                MessageBox.Show("Failed to read Kanban format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
        Return IsKanbanQRReadable
    End Function

    Private Function IsKanbanPartQRReadable(ByVal input As String) As Boolean
        Try
            Dim errorMsg As String = "Invalid PxP Part QR"

            If Not String.IsNullOrEmpty(input) And input.Length > 152 Then
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_manufacture_code As String = input.Substring(0, MANUFACTURE_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_manufacture_code) Then
                PART.MANUFACTURE_CODE = _temp_manufacture_code
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_supplier_code As String = input.Substring(2, SUPPLIER_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_supplier_code) Then
                PART.SUPPLIER_CODE = _temp_supplier_code
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_supplier_plant_code As String = input.Substring(6, SUPPLIER_PLANT_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_supplier_plant_code) Then
                PART.SUPPLIER_PLANT_CODE = _temp_supplier_plant_code
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_supplier_shipping_dock As String = input.Substring(7, SUPPLIER_SHIPPING_DOCK_LENGTH)
            If Not String.IsNullOrEmpty(_temp_supplier_shipping_dock) Then
                PART.SUPPLIER_SHIPPING_DOCK = _temp_supplier_shipping_dock.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_before_packing_routing As String = input.Substring(10, BEFORE_PACKING_ROUTING_LENGTH)
            If Not String.IsNullOrEmpty(_temp_before_packing_routing) Then
                PART.BEFORE_PACKING_ROUTING = _temp_before_packing_routing.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_receiving_company_code As String = input.Substring(16, RECEIVING_COMPANY_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_receiving_company_code) Then
                PART.RECEIVING_COMPANY_CODE = _temp_receiving_company_code.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_receiving_plant_code As String = input.Substring(20, RECEIVING_PLANT_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_receiving_plant_code) Then
                PART.RECEIVING_PLANT_CODE = _temp_receiving_plant_code.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_receiving_dock_code As String = input.Substring(21, RECEIVING_DOCK_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_receiving_dock_code) Then
                PART.RECEIVING_DOCK_CODE = _temp_receiving_dock_code.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_packing_routing_code As String = input.Substring(23, PACKING_ROUTING_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_packing_routing_code) Then
                PART.PACKING_ROUTING_CODE = _temp_packing_routing_code.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_granter_code As String = input.Substring(29, GRANTER_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_granter_code) Then
                PART.GRANTER_CODE = _temp_granter_code.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_order_type As String = input.Substring(33, ORDER_TYPE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_order_type) Then
                PART.ORDER_TYPE = _temp_order_type.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_kanban_classification As String = input.Substring(34, KANBAN_CLASSIFICATION_LENGTH)
            If Not String.IsNullOrEmpty(_temp_kanban_classification) Then
                PART.KANBAN_CLASSIFICATION = _temp_kanban_classification.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_delivery_date As String = input.Substring(35, DELIVERY_DATE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_date) Then
                PART.DELIVERY_DATE = _temp_delivery_date.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_delivery_code As String = input.Substring(39, DELIVERY_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_code) Then
                PART.DELIVERY_CODE = _temp_delivery_code.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_mros As String = input.Substring(41, MROS_LENGTH)
            If Not String.IsNullOrEmpty(_temp_mros) Then
                PART.MROS = _temp_mros.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_order_number As String = input.Substring(43, ORDER_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_order_number) Then
                PART.ORDER_NUMBER = _temp_order_number.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_delivery_number As String = input.Substring(55, DELIVERY_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_number) Then
                PART.DELIVERY_NUMBER = _temp_delivery_number.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_back_number As String = input.Substring(60, BACK_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_back_number) Then
                PART.BACK_NUMBER = _temp_back_number.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_parts_no As String = input.Substring(64, PARTS_NO_LENGTH)
            If Not String.IsNullOrEmpty(_temp_parts_no) Then
                PART.PARTS_NO = _temp_parts_no.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_part_no_sfx As String = input.Substring(74, PART_NO_SFX_LENGTH)
            If Not String.IsNullOrEmpty(_temp_part_no_sfx) Then
                PART.PART_NO_SFX = _temp_part_no_sfx.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_qty_box As String = input.Substring(76, QTY_BOX_LENGTH)
            If Not String.IsNullOrEmpty(_temp_qty_box) Then
                PART.QTY_BOX = _temp_qty_box.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_runout_flag As String = input.Substring(81, RUNOUT_FLAG_LENGTH)
            If Not String.IsNullOrEmpty(_temp_runout_flag) Then
                PART.RUNOUT_FLAG = _temp_runout_flag.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_delivery_code_2 As String = input.Substring(82, DELIVERY_CODE_2_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_code_2) Then
                PART.DELIVERY_CODE_2 = _temp_delivery_code_2.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_box_type As String = input.Substring(83, BOX_TYPE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_box_type) Then
                PART.BOX_TYPE = _temp_box_type.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_branch_number As String = input.Substring(91, BRANCH_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_branch_number) Then
                PART.BRANCH_NUMBER = _temp_branch_number.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_address As String = input.Substring(95, ADDRESS_LENGTH)
            If Not String.IsNullOrEmpty(_temp_address) Then
                PART.ADDRESS = _temp_address.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim TimeFormat As String = "hh:mm"
            Dim _temp_delivery_time As String = input.Substring(105, DELIVERY_TIME_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_time) Then
                PART.DELIVERY_TIME = _temp_delivery_time.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_packing_date As String = input.Substring(110, PACKING_DATE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_packing_date) Then
                PART.PACKING_DATE = _temp_packing_date.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_katashiki_jersey_num As String = input.Substring(118, KATASHIKI_JERSEY_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_katashiki_jersey_num) Then
                PART.KATASHIKI_JERSEY_NUMBER = _temp_katashiki_jersey_num.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_lot_no As String = input.Substring(121, LOT_NO_LENGTH)
            If Not String.IsNullOrEmpty(_temp_lot_no) Then
                PART.LOT_NO = _temp_lot_no.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_mod_category As String = input.Substring(125, MODULE_CATEGORY_LENGTH)
            If Not String.IsNullOrEmpty(_temp_mod_category) Then
                PART.MODULE_CATEGORY = _temp_mod_category.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_part_seq_num As String = input.Substring(127, PART_SEQ_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_part_seq_num) Then
                PART.PART_SEQ_NUMBER = _temp_part_seq_num.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_part_branch_num As String = input.Substring(129, PART_BRANCH_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_part_branch_num) Then
                PART.PART_BRANCH_NUMBER = _temp_part_branch_num.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_dummy As String = input.Substring(131, DUMMY_LENGTH)
            If Not String.IsNullOrEmpty(_temp_dummy) Then
                PART.DUMMY = _temp_dummy.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            Dim _temp_version_no As String = input.Substring(151, VERSION_NO_LENGTH)
            If Not String.IsNullOrEmpty(_temp_version_no) Then
                PART.VERSION_NO = _temp_version_no.Trim
            Else
                Throw New FormatException(errorMsg)
            End If

            PART.MODULE_NO = PART.MODULE_CATEGORY & PART.LOT_NO

            Return True
        Catch ex As Exception
            If ex.Message = "Specified argument was out of the range of valid values." Then
                MessageBox.Show("Failed to read PxP Kanban format.")
            Else
                MessageBox.Show(ex.Message)
            End If
            Return False
        End Try
    End Function

    Private Function KanbanIntLength(ByVal Condition As Integer) As Integer
        KanbanIntLength = 0
        Select Case Condition
            Case KB_PDIO_NO_LENGTH
                KanbanIntLength = KB_DELIVERY_DATE_LENGTH + KB_DOCK_CODE_LENGTH + KB_ORDER_TYPE_LENGTH + KB_VENDOR_ID_LENGTH + _
                KB_TRANSPORTER_ID_LENGTH + KB_LANE_ID_LENGTH + KB_TIER_LENGTH + KB_ORG_ID_LENGTH + KB_PART_NO_LENGTH + KB_BACK_NO_LENGTH + _
                KB_SEQ_NO_LENGTH + KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_DOCK_CODE_LENGTH
                KanbanIntLength = KB_ORDER_TYPE_LENGTH + KB_VENDOR_ID_LENGTH + KB_TRANSPORTER_ID_LENGTH + KB_LANE_ID_LENGTH + KB_TIER_LENGTH + _
                KB_ORG_ID_LENGTH + KB_PART_NO_LENGTH + KB_BACK_NO_LENGTH + KB_SEQ_NO_LENGTH + KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + _
                KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_ORDER_TYPE_LENGTH
                KanbanIntLength = KB_VENDOR_ID_LENGTH + KB_TRANSPORTER_ID_LENGTH + KB_LANE_ID_LENGTH + KB_TIER_LENGTH + _
                KB_ORG_ID_LENGTH + KB_PART_NO_LENGTH + KB_BACK_NO_LENGTH + KB_SEQ_NO_LENGTH + KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + _
                KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_VENDOR_ID_LENGTH
                KanbanIntLength = KB_TRANSPORTER_ID_LENGTH + KB_LANE_ID_LENGTH + KB_TIER_LENGTH + _
                KB_ORG_ID_LENGTH + KB_PART_NO_LENGTH + KB_BACK_NO_LENGTH + KB_SEQ_NO_LENGTH + KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + _
                KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_TRANSPORTER_ID_LENGTH
                KanbanIntLength = KB_LANE_ID_LENGTH + KB_TIER_LENGTH + KB_ORG_ID_LENGTH + KB_PART_NO_LENGTH + KB_BACK_NO_LENGTH + KB_SEQ_NO_LENGTH + _
                KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_LANE_ID_LENGTH
                KanbanIntLength = KB_TIER_LENGTH + KB_ORG_ID_LENGTH + KB_PART_NO_LENGTH + KB_BACK_NO_LENGTH + KB_SEQ_NO_LENGTH + _
                KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_BACK_NO_LENGTH
                KanbanIntLength = KB_SEQ_NO_LENGTH + KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_SEQ_NO_LENGTH
                KanbanIntLength = KB_TRANSACTION_CODE_LENGTH + KB_QTY_ORDER_LENGTH + KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_TRANSACTION_CODE_LENGTH
                KanbanIntLength = KB_QTY_ORDER_LENGTH + KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_QTY_ORDER_LENGTH
                KanbanIntLength = KB_DELIVERY_TYPE_LENGTH + KB_SCAN_FLAG_LENGTH

            Case KB_DELIVERY_TYPE_LENGTH
                KanbanIntLength = KB_SCAN_FLAG_LENGTH

        End Select
        Return KanbanIntLength
    End Function

#End Region

#Region ". Create Table ."

    Private Sub DeleteTable()
        Dim sqlStr As String = String.Format("DELETE FROM {0} WHERE SCANNER_SCREEN_CODE = 'PL'", TblJSPSupplyInterface, GetBatchID("PROGRESS_LANE", "4"))
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyInterface)
        End If
    End Sub

    Private Sub DeletePendingRecord()
        Dim sqlStr As String = "DELETE FROM " & TblJSPSupplyPLPendingView & " WHERE PDIO_ID = " & KB.PDIO_ID & " AND PDIO_NO = " & SQLQuote(KB.PDIO_NO) & _
        " AND PART_NO = " & SQLQuote(KB.PART_NO) & " AND SEQ_NO = " & SQLQuote(KB.SEQ_NO) & " AND BACK_NUMBER = " & SQLQuote(KB.BACK_NO) & " AND TRANSACTION_CODE = " & SQLQuote(KB.TRANSACTION_CODE)
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyPLPendingView)
        End If
    End Sub

    Private Sub DeleteAllTable()
        Dim sqlStr As String = String.Format("DELETE FROM {0} WHERE SCANNER_SCREEN_CODE = 'PL'", TblJSPSupplyInterface, GetBatchID("PROGRESS_LANE", "4"))
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyInterface)
        End If

        sqlStr = String.Format("DELETE FROM {0}", TblJSPSupplyPLPendingView)
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyPLPendingView)
        End If
    End Sub

    Private Function CreateTable(ByVal TableName As String) As Boolean
        CreateTable = False

        Dim dbReader As SqlCeDataReader
        Dim strBuilder As System.Text.StringBuilder

        Select Case TableName.ToUpper.Trim()
            Case TblJSPSupplyPLPendingView
                dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = {0}", SQLQuote(TblJSPSupplyPLPendingView)), objConn)
                If dbReader.Read Then
                    If CInt(dbReader(0)) = 0 Then
                        strBuilder = Nothing
                        strBuilder = New System.Text.StringBuilder
                        strBuilder.Append(String.Format("CREATE TABLE [{0}] (", TblJSPSupplyPLPendingView))
                        strBuilder.Append("PDIO_ID INT IDENTITY, ")
                        strBuilder.Append("PDIO_NO NVARCHAR(30) NULL, ")
                        strBuilder.Append("PART_NO NVARCHAR(20) NULL, ")
                        strBuilder.Append("ADVICEQTY INT NULL, ")
                        strBuilder.Append("BACK_NUMBER NVARCHAR(10) NULL, ")
                        strBuilder.Append("ORG_ID INT NULL")
                        strBuilder.Append(")")
                        If ExecuteSQL(strBuilder.ToString()) = False Then
                            Throw New Exception(String.Format("Failed to create local scanner's table: {0}", TblJSPSupplyPLPendingView))
                        End If
                    End If
                End If
        End Select
        CreateTable = True
    End Function

    Private Sub InsertKanbanTable(ByVal KB As KanbanQR_Input, _
                                  ByVal PART As PartQR_Input, _
                                  ByVal currentDate As String, _
                                  ByVal screenCode As String, _
                                  ByVal isOnline As String, _
                                  ByVal FSReasonIDShop As String, _
                                  ByVal FSReasonIDKB As String, _
                                  ByVal FSReasonIDKBP As String)

        Dim dbReader As SqlCeDataReader
        dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM [{0}] WHERE PART_NO = {1}", TblJSPSupplyInterface, SQLQuote(KB.PART_NO)), objConn)
        If dbReader.Read Then
            If CInt(dbReader(0)) = 0 Then
                Dim sqlStr As String = Nothing
                Dim forceStatusShop = "N"
                Dim forceStatusKB = "N"
                Dim forceStatusKBP = "N"
                If FSReasonIDShop <> Nothing Then
                    forceStatusShop = "Y"
                End If
                If FSReasonIDKB <> Nothing Then
                    forceStatusKB = "Y"
                End If
                If FSReasonIDKBP <> Nothing Then
                    forceStatusKBP = "Y"
                End If

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
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(GetBatchID("PROGRESS_LANE", "4"))) 'RCV_INTERFACE_BATCH_ID
                If KB.TRANSACTION_CODE = "01" Then
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(PART.MODULE_ID = Nothing, "null", IIf(PART.MODULE_ID.Trim() = String.Empty, "null", PART.MODULE_ID))) 'MODULE_ID
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.MODULE_NO.Trim() = String.Empty, SQLQuote(PART.MODULE_NO), "null")) 'MODULE_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.PART_ID.Trim() = String.Empty, PART.PART_ID, "null")) 'PXP_PART_ID
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.PARTS_NO.Trim() = String.Empty, SQLQuote(PART.PARTS_NO), "null")) 'PXP_PART_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.PART_NO_SFX.Trim() = String.Empty, SQLQuote(PART.PART_NO_SFX), "null")) 'PXP_PART_NO_SFX
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.PART_SEQ_NUMBER.Trim() = String.Empty, SQLQuote(PART.PART_SEQ_NUMBER), "null")) 'PXP_PART_SEQ_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.QTY_BOX.Trim() = String.Empty, SQLQuote(PART.QTY_BOX), "null")) 'QTY_BOX
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.MANUFACTURE_CODE.Trim() = String.Empty, SQLQuote(PART.MANUFACTURE_CODE), "null")) 'MANUFACTURE_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.SUPPLIER_CODE.Trim() = String.Empty, SQLQuote(PART.SUPPLIER_CODE), "null")) 'SUPPLIER_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.SUPPLIER_PLANT_CODE.Trim() = String.Empty, SQLQuote(PART.SUPPLIER_PLANT_CODE), "null")) 'SUPPLIER_PLANT_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.SUPPLIER_SHIPPING_DOCK.Trim() = String.Empty, SQLQuote(PART.SUPPLIER_SHIPPING_DOCK), "null")) 'SUPPLIER_SHIPPING_DOCK
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.BEFORE_PACKING_ROUTING.Trim() = String.Empty, SQLQuote(PART.BEFORE_PACKING_ROUTING), "null")) 'BEFORE_PACKING_ROUTING
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.RECEIVING_COMPANY_CODE.Trim() = String.Empty, SQLQuote(PART.RECEIVING_COMPANY_CODE), "null")) 'RECEIVING_COMPANY_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.RECEIVING_PLANT_CODE.Trim() = String.Empty, SQLQuote(PART.RECEIVING_PLANT_CODE), "null")) 'RECEIVING_PLANT_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.RECEIVING_DOCK_CODE.Trim() = String.Empty, SQLQuote(PART.RECEIVING_DOCK_CODE), "null")) 'RECEIVING_DOCK_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.PACKING_ROUTING_CODE.Trim() = String.Empty, SQLQuote(PART.PACKING_ROUTING_CODE), "null")) 'PACKING_ROUTING_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.GRANTER_CODE.Trim() = String.Empty, SQLQuote(PART.GRANTER_CODE), "null")) 'GRANTER_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.ORDER_TYPE.Trim() = String.Empty, SQLQuote(PART.ORDER_TYPE), "null")) 'ORDER_TYPE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.KANBAN_CLASSIFICATION.Trim() = String.Empty, SQLQuote(PART.KANBAN_CLASSIFICATION), "null")) 'KANBAN_CLASSIFICATION 
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.DELIVERY_CODE.Trim() = String.Empty, SQLQuote(PART.DELIVERY_CODE), "null")) 'DELIVERY_CODE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.MROS.Trim() = String.Empty, SQLQuote(PART.MROS), "null")) 'MROS
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.ORDER_NUMBER.Trim() = String.Empty, SQLQuote(PART.ORDER_NUMBER), "null")) 'ORDER_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.DELIVERY_NUMBER.Trim() = String.Empty, SQLQuote(PART.DELIVERY_NUMBER), "null")) 'DELIVERY_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.BACK_NUMBER.Trim() = String.Empty, SQLQuote(PART.BACK_NUMBER), "null")) 'BACK_NUMBER
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.RUNOUT_FLAG = Nothing, SQLQuote(PART.RUNOUT_FLAG), "null")) 'RUNOUT_FLAG
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.BOX_TYPE.Trim() = Nothing, SQLQuote(PART.BOX_TYPE), "null")) 'BOX_TYPE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.BRANCH_NUMBER.Trim() = String.Empty, SQLQuote(PART.BRANCH_NUMBER), "null")) 'BRANCH_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.ADDRESS.Trim() = String.Empty, SQLQuote(PART.ADDRESS), "null")) 'ADDRESS
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.PACKING_DATE.Trim() = String.Empty, SQLQuote(PART.PACKING_DATE), "null")) 'PACKING_DATE
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.KATASHIKI_JERSEY_NUMBER.Trim() = String.Empty, SQLQuote(PART.KATASHIKI_JERSEY_NUMBER), "null")) 'KATASHIKI_JERSEY_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.LOT_NO.Trim() = String.Empty, SQLQuote(PART.LOT_NO), "null")) 'LOT_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.MODULE_CATEGORY.Trim() = String.Empty, SQLQuote(PART.MODULE_CATEGORY), "null")) 'MODULE_CATEGORY
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.PART_BRANCH_NUMBER.Trim() = String.Empty, SQLQuote(PART.PART_BRANCH_NUMBER), "null")) 'PART_BRANCH_NO
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.DUMMY.Trim() = String.Empty, SQLQuote(PART.DUMMY), "null")) 'DUMMY
                    sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not PART.VERSION_NO.Trim() = String.Empty, SQLQuote(PART.VERSION_NO), "null")) 'VERSION_NO
                Else
                    sqlStr = String.Format("{0}null, ", sqlStr) 'MODULE_ID
                    sqlStr = String.Format("{0}null, ", sqlStr) 'MODULE_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'PXP_PART_ID
                    sqlStr = String.Format("{0}null, ", sqlStr) 'PXP_PART_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'PXP_PART_NO_SFX
                    sqlStr = String.Format("{0}null, ", sqlStr) 'PXP_PART_SEQ_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'QTY_BOX
                    sqlStr = String.Format("{0}null, ", sqlStr) 'MANUFACTURE_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'SUPPLIER_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'SUPPLIER_PLANT_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'SUPPLIER_SHIPPING_DOCK
                    sqlStr = String.Format("{0}null, ", sqlStr) 'BEFORE_PACKING_ROUTING
                    sqlStr = String.Format("{0}null, ", sqlStr) 'RECEIVING_COMPANY_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'RECEIVING_PLANT_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'RECEIVING_DOCK_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'PACKING_ROUTING_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'GRANTER_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'ORDER_TYPE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'KANBAN_CLASSIFICATION 
                    sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_CODE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'MROS
                    sqlStr = String.Format("{0}null, ", sqlStr) 'ORDER_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'BACK_NUMBER
                    sqlStr = String.Format("{0}null, ", sqlStr) 'RUNOUT_FLAG
                    sqlStr = String.Format("{0}null, ", sqlStr) 'BOX_TYPE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'BRANCH_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'ADDRESS
                    sqlStr = String.Format("{0}null, ", sqlStr) 'PACKING_DATE
                    sqlStr = String.Format("{0}null, ", sqlStr) 'KATASHIKI_JERSEY_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'LOT_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'MODULE_CATEGORY
                    sqlStr = String.Format("{0}null, ", sqlStr) 'PART_BRANCH_NO
                    sqlStr = String.Format("{0}null, ", sqlStr) 'DUMMY
                    sqlStr = String.Format("{0}null, ", sqlStr) 'VERSION_NO

                End If
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(KB.PDIO_ID = String.Empty Or KB.PDIO_ID Is Nothing, "null", KB.PDIO_ID))  'PDIO_ID
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(KB.PDIO_NO = String.Empty Or KB.PDIO_NO Is Nothing, "null", SQLQuote(KB.PDIO_NO))) 'PDIO_NO
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not KB.DOCK_CODE Is Nothing, SQLQuote(KB.DOCK_CODE), "null")) 'DOCK_CODE
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not KB.ORDER_TYPE Is Nothing, SQLQuote(KB.ORDER_TYPE), "null")) 'PDIO_ORDER_TYPE
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(KB.VENDOR_ID = String.Empty Or KB.VENDOR_ID Is Nothing, "null", KB.VENDOR_ID)) 'VENDOR_ID
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(KB.TRANSPORTER_ID = String.Empty Or KB.TRANSPORTER_ID Is Nothing, "null", KB.TRANSPORTER_ID)) 'TRANSPORTER_ID
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(KB.LANE_ID = String.Empty Or KB.LANE_ID Is Nothing, "null", KB.LANE_ID)) 'LANE_ID
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(KB.TIER = String.Empty Or KB.TIER Is Nothing, "null", KB.TIER)) 'TIER
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not KB.BACK_NO Is Nothing, SQLQuote(KB.BACK_NO), "null")) 'BACK_NO
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not KB.PART_NO Is Nothing, SQLQuote(KB.PART_NO), "null")) 'P2_PART_NO
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(Not KB.SEQ_NO Is Nothing, KB.SEQ_NO, "null")) 'P2_PART_SEQ_NO
                sqlStr = String.Format("{0}{1}, ", sqlStr, org_ID) 'ORG_ID
                sqlStr = String.Format("{0}null, ", sqlStr) 'SCANNER_BATCH_ID
                sqlStr = String.Format("{0}null, ", sqlStr) 'SCANNER_HT_ID
                sqlStr = String.Format("{0}null, ", sqlStr) 'PROCESS_DATE
                sqlStr = String.Format("{0}null, ", sqlStr) 'PROCESS_FLAG
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(gScannerID)) 'CREATED_BY
                sqlStr = String.Format("{0}GETDATE(), ", sqlStr) 'CREATED_DATE
                sqlStr = String.Format("{0}null, ", sqlStr) 'UPDATED_BY
                sqlStr = String.Format("{0}GETDATE(), ", sqlStr) 'UPDATED_DATE
                sqlStr = String.Format("{0}null, ", sqlStr) 'ERROR_MSG
                sqlStr = String.Format("{0}null, ", sqlStr) 'POST_DATE

                Dim delDate As String = Nothing
                If KB.DELIVERY_DATE = String.Empty Or KB.DELIVERY_DATE = Nothing Then
                    delDate = "null"
                ElseIf Not KB.DELIVERY_DATE Is Nothing Then
                    delDate = SQLQuote(DateTime.ParseExact(KB.DELIVERY_DATE, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM-dd-yyyy"))
                End If
                sqlStr = String.Format("{0}{1}, ", sqlStr, delDate) 'DELIVERY_DATE
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(isOnline)) 'ON_OFF_LINE_FLAG
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(currentDate)) 'SCAN_DATE
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(forceStatusShop)) 'FORCE_PDIO_STATUS
                If Not FSReasonIDShop = Nothing Then
                    sqlStr = String.Format("{0}{1}, ", sqlStr, FSReasonIDShop) 'FORCE_PDIO_REASON_ID
                Else
                    sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_PDIO_REASON_ID
                End If
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(forceStatusKBP)) 'FORCE_PXP_STATUS
                If Not FSReasonIDKBP = Nothing Then
                    sqlStr = String.Format("{0}{1}, ", sqlStr, FSReasonIDKBP) 'FORCE_PXP_REASON_ID
                Else
                    sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_PDIO_REASON_ID
                End If
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(screenCode)) 'SCANNER_SCREEN_CODE       
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(forceStatusKB)) 'FORCE_P2_STATUS
                If Not FSReasonIDKB = Nothing Then
                    sqlStr = String.Format("{0}{1}, ", sqlStr, FSReasonIDKB) 'FORCE_P2_REASON_ID
                Else
                    sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_P2_REASON_ID
                End If
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(gScannerID)) 'SUPPLY_BY
                sqlStr = String.Format("{0}{1}, ", sqlStr, SQLQuote(currentDate)) 'SUPPLY_DATE
                sqlStr = String.Format("{0}null, ", sqlStr) 'SHOP_ID
                sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_MODULE_STATUS
                sqlStr = String.Format("{0}null, ", sqlStr) 'FORCE_MODULE_REASON_ID
                sqlStr = String.Format("{0}null, ", sqlStr) 'PART_NO
                sqlStr = String.Format("{0}null, ", sqlStr) 'SEQNO_KEY
                sqlStr = String.Format("{0}{1}, ", sqlStr, IIf(KB.DELIVERY_TYPE = String.Empty Or KB.DELIVERY_TYPE Is Nothing, "null", KB.DELIVERY_TYPE)) 'DELIVERY_TYPE
                sqlStr = String.Format("{0}null, ", sqlStr) 'PRODUCTION_DATE
                sqlStr = String.Format("{0}null, ", sqlStr) 'EXPORTER_CODE
                sqlStr = String.Format("{0}null, ", sqlStr) 'PROD_LINE
                sqlStr = String.Format("{0}null, ", sqlStr) 'CYCLE
                sqlStr = String.Format("{0}null, ", sqlStr) 'ROUTE
                sqlStr = String.Format("{0}null, ", sqlStr) 'TOTAL_BOX
                sqlStr = String.Format("{0}null, ", sqlStr) 'DELIVERY_TYPE
                sqlStr = String.Format("{0}null, ", sqlStr) 'RETURN_VAL
                sqlStr = String.Format("{0}null, ", sqlStr) 'POSTED
                sqlStr = String.Format("{0}{1} ", sqlStr, IIf(KB.QTY_ORDER = String.Empty Or KB.QTY_ORDER Is Nothing, "null", KB.QTY_ORDER)) 'QTY_ORDER
                sqlStr = String.Format("{0})", sqlStr)
                If ExecuteSQL(sqlStr) = False Then
                    Throw New Exception("Failed to Create Local Module Table.")
                End If
            Else
                Throw New Exception("Part No Already Existed On Local Database.")
            End If
        End If
    End Sub

    Private Sub DeleteTableAbn()
        Dim sqlStr As String = String.Format("DELETE FROM {0} WHERE SCANNER_SCREEN_CODE = 'PL' AND POSTED = 'Y'", TblJSPSupplyInterface)
        If ExecuteSQL(sqlStr) = False Then
            MessageBox.Show("Failed to delete {0}", TblJSPSupplyInterface)
        End If
    End Sub

#End Region

#Region ". Main Menu Navigation ."

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmProgressLane_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmProgressLane_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Public Sub Init()

        Try
            Me.Text = strOnlineTitle
            footerStatusBar.Visible = False
            InitWebServices()
            bringPanelToFront(pnlPLMain, pnlPLScanShopping)
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClosePL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePL.Click
        'DeleteAllTable()
        Me.Close()
    End Sub

    Private Sub btnScanPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanPL.Click
        isNormal = True
        reasonShop = Nothing
        reasonInt = Nothing
        reasonExt = Nothing
        txtShoppingNo.Focus()
        btnNextPLScanPart.Enabled = False
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanShopping, pnlPLMain)
    End Sub

    Private Sub btnAbnormalPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalPL.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLMain)
    End Sub

#End Region

#Region ". Normal Mode Navigation and Private Function ."

    Private Sub btnNextScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextPLScanPart.Click
        If Not String.IsNullOrEmpty(txtShoppingNo.Text) Then
            txtKanbanQR.Focus()
            txtPxPKanbanQR.Enabled = False
            lblPLIntExtStatusMsg.Text = String.Empty
            lblPLIntExtStatusMsg.BackColor = Color.Transparent
            lblPLIntExtStatusMsgDesc.Text = String.Empty
            lblTitleBranchExt.Visible = False
            lblBranchExt.Visible = False
            btnFScanExt.Enabled = False
            lblShoppingNo.Text = "Shopping No: " & SHOP.PDIO_NO
            Me.Text = strOnlineTitle
            bringPanelToFront(pnlPLScanIntPart, pnlPLScanShopping)
        Else
            txtShoppingNo.Focus()
            MessageBox.Show("Shopping/PDIO No. is required")
        End If
    End Sub

    Private Sub btnBackCPScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLScanPart.Click
        txtShoppingNo.Text = String.Empty
        txtProdDate.Text = String.Empty
        txtShop.Text = String.Empty
        txtLane.Text = String.Empty
        lblPLStatusMsg.BackColor = Color.Transparent
        lblPLStatusMsg.Text = String.Empty
        lblPLStatusMsgDesc.Text = String.Empty
        TimerCheckOnline.Enabled = False
        TimerCheckOnline.Dispose()
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLScanShopping)
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanShopping.Click
        Me.Text = strOnlineTitle
        txtFSPDIO.Focus()
        GetReason(lstViewPDIOFScan)
        bringPanelToFront(pnlPLFScanShopping, pnlPLScanShopping)
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Try
            If ValidatePDIO() Then 'Validate required fields
                Dim dt As DataTable = ws_dcsClient.getData("*", TblJSPSupplyPLDetailsView, " AND PDIO_NO = " & SQLQuote(txtFSPDIO.Text.Trim))
                If dt.Rows.Count > 0 Then
                    SHOP.PDIO_ID = IIf(Not String.IsNullOrEmpty(dt.Rows(0).Item("PDIO_ID").ToString), _
                                       dt.Rows(0).Item("PDIO_ID").ToString, Nothing)
                    SHOP.PDIO_NO = IIf(Not String.IsNullOrEmpty(dt.Rows(0).Item("PDIO_NO").ToString), _
                                       dt.Rows(0).Item("PDIO_NO").ToString, Nothing)
                    SHOP.ORG_ID = IIf(Not String.IsNullOrEmpty(dt.Rows(0).Item("ORG_ID").ToString), _
                                       dt.Rows(0).Item("ORG_ID").ToString, Nothing)
                    SHOP.LANE_ID = Nothing
                    SHOP.SHOP_ID = Nothing
                End If
                txtShoppingNo.Text = SHOP.PDIO_NO
                Call PDIOScanChecker(lstViewPDIOFScan.FocusedItem.SubItems(1).Text)
                bringPanelToFront(pnlPLScanShopping, pnlPLFScanShopping)
                txtFSPDIO.Text = String.Empty
                lstViewPDIOFScan.Items.Item(0).Focused = True
            End If
            txtFSPDIO.SelectAll()
            txtFSPDIO.Focus()
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                MessageBox.Show("Failed to read PDIO format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Me.Text = strOnlineTitle
        txtFSPDIO.Text = String.Empty
        txtShoppingNo.Focus()
        txtShoppingNo.SelectAll()
        lstViewPDIOFScan.Items.Item(0).Focused = True
        bringPanelToFront(pnlPLScanShopping, pnlPLFScanShopping)
    End Sub

    Private Sub btnPLIntPartDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetailsInt.Click
        lblHeaderVwDet.Text = String.Format("Shopping No: {0}", SHOP.PDIO_NO)
        loadlstView(lstViewScannedSummary, lblTotalScannedView)
        loadlstPendingView(lstViewPendingSummary, lblTotalPending)
        Me.Text = String.Format("{0} - View", strOnlineTitle)
        bringPanelToFront(pnlPLViewDet, pnlPLScanIntPart)
    End Sub

    Private Sub btnFScanInt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanInt.Click
        GetReason(lstViewFScanInt)
        txtFSKanbanPartNo.Focus()
        GetTxnCode()
        cmbBoxTxnCode.Visible = False
        lblTxnCode.Visible = False
        Me.Text = String.Format("{0} - View", strOnlineTitle)
        bringPanelToFront(pnlPLFScanIntPart, pnlPLScanIntPart)
    End Sub

    Private Sub btnPLFScanIntPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanExt.Click
        GetReason(lstViewFScanExt)
        txtFSPxPModuleNo.Focus()
        Me.Text = String.Format("{0} - View", strOnlineTitle)
        bringPanelToFront(pnlPLFScanExtPart, pnlPLScanIntPart)
    End Sub

    Private Sub btnBackScanIntPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackScanInt.Click
        txtKanbanQR.Text = String.Empty
        txtPxPKanbanQR.Text = String.Empty
        lblPartNoInt.Text = String.Empty
        lblSeqNoInt.Text = String.Empty
        lblQtyInt.Text = String.Empty
        lblBranchExt.Visible = False
        lblTitleBranchExt.Visible = False
        lblBranchExt.Text = String.Empty
        lblPartType.Text = String.Empty
        txtKanbanQR.BackColor = Color.LimeGreen
        txtPxPKanbanQR.BackColor = Color.Transparent
        txtPxPKanbanQR.Enabled = False
        lblPLIntExtStatusMsg.BackColor = Color.Transparent
        lblPLIntExtStatusMsg.Text = String.Empty
        lblPLIntExtStatusMsgDesc.Text = String.Empty
        txtShoppingNo.SelectAll()
        txtShoppingNo.Focus()
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanShopping, pnlPLScanIntPart)
    End Sub

    Private Sub btnSaveForceScanInt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScanInt.Click
        Try
            If ValidateInt() Then 'Validate required fields
                isForceScan = True
                'KB.PART_NO = txtFSKanbanPartNo.Text.Replace("-", "").Substring(0, 10)
                KB.PART_NO = txtFSKanbanPartNo.Text
                KB.SEQ_NO = txtFSKanbanSeqNo.Text

                If (isNormal) Then
                    KanbanScanChecker()
                    Me.Text = strOnlineTitle
                    bringPanelToFront(pnlPLScanIntPart, pnlPLFScanIntPart)
                Else
                    KB.TRANSACTION_CODE = cmbBoxTxnCode.SelectedValue
                    KanbanAbnScanChecker()
                    Me.Text = strOfflineTitle
                    bringPanelToFront(pnlPLAbnScanPart, pnlPLFScanIntPart)
                End If
                ClearFSInt()
                txtPxPKanbanQRAbn.Focus()
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                MessageBox.Show("Failed to read Kanban format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub btnBackFScanIntPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScanInt.Click
        Me.Text = strOfflineTitle
        ClearFSInt()
        If isNormal Then
            bringPanelToFront(pnlPLScanIntPart, pnlPLFScanIntPart)
        Else
            bringPanelToFront(pnlPLAbnScanPart, pnlPLFScanIntPart)
        End If
    End Sub

    Private Sub btnSaveForceScanExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScanExt.Click
        Try
            If ValidateExt() Then 'Validate required fields
                isForceScan = True
                PART.PARTS_NO = txtFSPxPPartNo.Text.Replace("-", "").Substring(0, 10)
                PART.PART_NO_SFX = txtFSPxPPartNo.Text.Replace("-", "").Substring(10, 2)
                PART.PART_SEQ_NUMBER = txtFSPxPSeqNo.Text
                PART.PART_BRANCH_NUMBER = txtFSPxPBranch.Text
                PART.MODULE_NO = txtFSPxPModuleNo.Text
                PART.MODULE_CATEGORY = txtFSPxPModuleNo.Text.Substring(0, 2)
                PART.LOT_NO = txtFSPxPModuleNo.Text.Substring(2, 4)
                reasonExt = lstViewFScanExt.FocusedItem.SubItems(1).Text

                If (isNormal) Then
                    Call KanbanPartScanChecker()
                    Me.Text = strOnlineTitle
                    bringPanelToFront(pnlPLScanIntPart, pnlPLFScanExtPart)
                    txtKanbanQR.Focus()
                Else
                    Call KanbanPartAbnScanChecker()
                    Me.Text = strOnlineTitle
                    bringPanelToFront(pnlPLAbnScanPart, pnlPLFScanExtPart)
                    txtKanbanQRAbn.Focus()
                End If
                ClearFSExt()
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If ex.Message = "Specified argument was out of the range of valid values." Then
                MessageBox.Show("Failed to read PxP Kanban format.")
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Private Sub btnBackFScanExtPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScanExt.Click
        Me.Text = strOnlineTitle
        ClearFSExt()
        If isNormal Then
            bringPanelToFront(pnlPLScanIntPart, pnlPLFScanExtPart)
        Else
            bringPanelToFront(pnlPLAbnScanPart, pnlPLFScanExtPart)
        End If
    End Sub

    Private Sub txtFSPDIO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFSPDIO.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            txtFSPDIO.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub txtFSKanbanPartNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFSKanbanPartNo.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            txtFSKanbanPartNo.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub txtFSPxPModuleNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFSPxPModuleNo.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            txtFSPxPModuleNo.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub txtFSPxPPartNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFSPxPPartNo.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            txtFSPxPPartNo.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub btnBackPLViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLViewDet.Click
        If (txtKanbanQR.BackColor = Color.LimeGreen) Then
            txtKanbanQR.SelectAll()
            txtKanbanQR.Focus()
        ElseIf (txtPxPKanbanQR.BackColor = Color.LimeGreen) Then
            txtPxPKanbanQR.SelectAll()
            txtPxPKanbanQR.Focus()
        End If

        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLScanIntPart, pnlPLViewDet)
    End Sub

#Region ". Shopping List QR ."

    Private Sub ClearPDIO()
        txtProdDate.Text = String.Empty
        txtShop.Text = String.Empty
        txtLane.Text = String.Empty
        lblPLStatusMsg.BackColor = Color.Transparent
        lblPLStatusMsg.Text = String.Empty
        lblPLStatusMsgDesc.Text = String.Empty
    End Sub

    Private Sub txtShoppingNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtShoppingNo.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o n l i n e
                mode = True
                If Not String.IsNullOrEmpty(txtShoppingNo.Text) Then
                    isForceScan = False
                    If IsPDIOReadable(txtShoppingNo.Text) Then
                        Call VerifyOrgId(SHOP.ORG_ID.Trim())
                        Call PDIOScanChecker()
                    Else
                        ClearPDIO()
                    End If
                Else
                    ClearPDIO()
                    MessageBox.Show("Shopping/PDIO No. is required")
                End If
                txtShoppingNo.SelectAll()
                txtShoppingNo.Focus()
            Catch ex As WebException
                'o f f l i n e
                Cursor.Current = Cursors.Default
                mode = False
                modScanOffline = False
                TimerCheckOnline.Enabled = True
                Call ChangeProcess()
            Catch ex As CustomException
                MsgBox(String.Format("Access Restricted!{0}{1}", Environment.NewLine, ex.Message), MsgBoxStyle.Critical, "Organization ID Mismatch")
                ClearPDIO()
                txtShoppingNo.Focus()
                txtShoppingNo.SelectAll()
            Catch ex As Exception
                Cursor.Current = Cursors.Default
                MsgBox("Shopping/PDIO scanned failed.", MsgBoxStyle.Critical, "Shopping/PDIO Scan")
                Cursor.Current = Cursors.Default
                txtShoppingNo.Focus()
                txtShoppingNo.SelectAll()
            End Try
        End If
    End Sub

    Private Sub PDIOScanChecker(Optional ByVal reason As String = Nothing)
        Cursor.Current = Cursors.WaitCursor
        If ws_dcsClient.isOracleConnected Then '***
            InitWebServices()
            Dim dt As DateTime = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            Dim currentDate As String = dt.ToString("dd-MM-yyyy hh:mm:ss tt")

            Dim forceStatus As String = "N"
            If reason <> 0 Then
                forceStatus = "Y"
            End If

            msgCode = ws_validationClient.processValidation(GetBatchID("PROGRESS_LANE", 4), gScannerID, _
                                "SUPPLY", "301", Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, Nothing, SHOP.PDIO_ID, SHOP.PDIO_NO, Nothing, _
                                Nothing, Nothing, Nothing, SHOP.LANE_ID, Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, _
                                org_ID, Nothing, gScannerID, currentDate, Nothing, Nothing, Nothing, _
                                Nothing, Nothing, Nothing, gScannerID, "Y", gScannerID, _
                                currentDate, forceStatus, reason, Nothing, Nothing, Nothing, Nothing, "PL", _
                                SHOP.SHOP_ID, msgDesc)

            If msgCode = "OK" Then
                Call PDIORetrieveOnline()
                lblPLStatusMsg.BackColor = Color.LimeGreen
                lblPLStatusMsg.Text = msgCode
                lblPLStatusMsgDesc.Text = msgDesc
                btnNextPLScanPart.Enabled = True
                If forceStatus <> "Y" Then
                    txtProdDate.Text = SHOP.PRODUCTION_DATE
                    txtShop.Text = SHOP.SHOP_ID
                    txtLane.Text = SHOP.LANE_ID
                End If
            Else
                lblPLStatusMsg.BackColor = Color.Red
                lblPLStatusMsg.Text = msgCode
                lblPLStatusMsgDesc.Text = msgDesc
                btnNextPLScanPart.Enabled = False
                txtProdDate.Text = Nothing
                txtShop.Text = Nothing
                txtLane.Text = Nothing
                txtShoppingNo.Focus()
                txtShoppingNo.SelectAll()
            End If
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub PDIORetrieveOnline()
        If ws_dcsClient.isConnected Then '***
            Dim sqlStr As String = Nothing
            'TO ASK
            Dim dtPartList As DataTable = ws_dcsClient.getData("*", TblJSPSupplyPLDetailsView, " AND PDIO_ID = " & SQLQuote(SHOP.PDIO_ID))
            If dtPartList.Rows.Count > 0 Then
                For i As Integer = 0 To dtPartList.Rows.Count - 1
                    Dim dbReader As SqlCeDataReader
                    dbReader = OpenRecordset("SELECT COUNT(*) FROM [" & TblJSPSupplyPLPendingView & "] WHERE " & _
                                             "PART_NO = " & SQLQuote(dtPartList.Rows(i).Item("PART_NO").ToString) & " " & _
                                             "AND SEQ_NO = " & SQLQuote(dtPartList.Rows(i).Item("SEQ_NO").ToString), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) = 0 Then
                            Dim dt As DataTable = getData(String.Format("SELECT * FROM [{0}] WHERE PXP_PART_NO = {1} AND PXP_PART_SEQ_NO = {2}", _
                                                                        TblJSPSupplyInterface, _
                                                                        SQLQuote(dtPartList.Rows(i).Item("PART_NO").ToString), _
                                                                        SQLQuote(dtPartList.Rows(i).Item("SEQ_NO").ToString)))
                            If dt.Rows.Count = 0 Then
                                sqlStr = String.Format("INSERT INTO [{0}] (PDIO_ID, PDIO_NO, PART_NO, SEQ_NO, ADVICEQTY, BACK_NUMBER, TRANSACTION_CODE, ORG_ID)", TblJSPSupplyPLPendingView)
                                sqlStr = String.Format("{0} VALUES ({1} , ", sqlStr, dtPartList.Rows(i).Item("PDIO_ID").ToString.PadLeft(10, "0"))
                                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(dtPartList.Rows(i).Item("PDIO_NO").ToString))
                                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(dtPartList.Rows(i).Item("PART_NO").ToString))
                                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(dtPartList.Rows(i).Item("SEQ_NO").ToString))
                                sqlStr = String.Format("{0}{1} , ", sqlStr, dtPartList.Rows(i).Item("QTY_PER_BOX").ToString)
                                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(dtPartList.Rows(i).Item("BACK_NUMBER").ToString))
                                sqlStr = String.Format("{0}{1} , ", sqlStr, SQLQuote(dtPartList.Rows(i).Item("TRANSACTION_CODE").ToString))
                                sqlStr = String.Format("{0}{1}", sqlStr, dtPartList.Rows(i).Item("ORG_ID").ToString)
                                sqlStr = String.Format("{0})", sqlStr)
                                If ExecuteSQL(sqlStr) = False Then
                                    Throw New Exception("Failed to retrieved Part List from server.")
                                Else

                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#Region ". Kanban QR ."

    Private Sub ClearKanban()
        lblPartNoInt.Text = String.Empty
        lblSeqNoInt.Text = String.Empty
        lblQtyInt.Text = String.Empty
        lblBranchExt.Text = String.Empty
        lblPLIntExtStatusMsg.Text = String.Empty
        lblPLIntExtStatusMsg.BackColor = Color.Transparent
        lblPLIntExtStatusMsgDesc.Text = String.Empty
        lblPartType.Text = String.Empty
        lblTotalScannedInt.Text = String.Empty
    End Sub

    Private Sub txtKanbanQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKanbanQR.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o n l i n e
                mode = True
                If Not String.IsNullOrEmpty(txtKanbanQR.Text) Then
                    isForceScan = False
                    If IsKanbanQRReadable(txtKanbanQR.Text) Then
                        Call VerifyOrgId(SHOP.ORG_ID.Trim())
                        Call KanbanScanChecker()
                    Else
                        ClearKanban()
                        txtKanbanQR.SelectAll()
                        txtKanbanQR.Focus()
                    End If
                Else
                    ClearKanban()
                    MessageBox.Show("Kanban QR is required")
                    txtKanbanQR.SelectAll()
                    txtKanbanQR.Focus()
                End If
            Catch ex As WebException
                Call OfflineKanban()
            Catch ex As CustomException
                MsgBox(String.Format("Access Restricted!{0}{1}", Environment.NewLine, ex.Message), MsgBoxStyle.Critical, "Organization ID Mismatch")
                ClearKanban()
                txtKanbanQR.Focus()
                txtKanbanQR.SelectAll()
            Catch ex As Exception
                MsgBox("Kanban QR scanned failed.", MsgBoxStyle.Critical, "Kanban Scan")
                Cursor.Current = Cursors.Default
                txtKanbanQR.Focus()
                txtKanbanQR.SelectAll()
            End Try
        End If
    End Sub

    Private Sub OfflineKanban()
        'o f f l i n e
        mode = False
        modScanOffline = True
        TimerCheckOnline.Enabled = True
        If offline1stTFlag Then
            If MessageBox.Show("Connection is down. Continue scan with offline mode?", _
                               "Kanban Scan Offline Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                offline1stTFlag = False
                Call KanbanScanOffline()
            Else
                Cursor.Current = Cursors.Default
                txtKanbanQR.Focus()
                txtKanbanQR.SelectAll()
                Exit Sub
            End If
        Else
            Call KanbanScanOffline()
        End If
    End Sub

    Private Sub ProcessMSP(ByVal currentDate As String)
        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then
                InitWebServices()
                Cursor.Current = Cursors.WaitCursor
                Call InsertKanbanTable(KB, Nothing, currentDate, "PL", "Y", reasonShop, reasonInt, reasonExt) 'KB reason
                Call UpdateKanbanPendingDetails(KB.PART_NO, KB.SEQ_NO)
                Call UpdatePostStatus(msgCode, KB.PART_NO)

                msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("PROGRESS_LANE", "4"), "SUPPLY", "304", org_ID, Nothing, _
                                                                         Nothing, Nothing, Nothing, Nothing, msgDesc)
                If msgCode = "OK" Then
                    lblPLIntExtStatusMsg.BackColor = Color.LimeGreen
                    lblPLIntExtStatusMsg.Text = msgCode
                    lblPLIntExtStatusMsgDesc.Text = msgDesc
                    lblPartNoInt.Text = KB.PART_NO
                    lblSeqNoInt.Text = KB.SEQ_NO
                    lblQtyInt.Text = KB.QTY_ORDER
                    lblBranchExt.Text = Nothing
                    lblTitleBranchExt.Visible = False
                    txtKanbanQR.Text = String.Empty
                    txtKanbanQR.SelectAll()
                    txtKanbanQR.Focus()
                    Call DeletePendingRecord()
                    Call UpdateBatch()
                    Dim dbReader As SqlCeDataReader
                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0}", TblJSPSupplyPLPendingView), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) = 0 Then
                            Call DeleteTable()
                            txtProdDate.Text = String.Empty
                            txtShop.Text = String.Empty
                            txtLane.Text = String.Empty
                            txtShoppingNo.Text = String.Empty
                            txtShoppingNo.Focus()
                            lblPLStatusMsg.Text = String.Empty
                            lblPLStatusMsgDesc.Text = String.Empty
                            lblPartNoInt.Text = String.Empty
                            lblSeqNoInt.Text = String.Empty
                            lblQtyInt.Text = String.Empty
                            lblBranchExt.Text = String.Empty
                            lblTitleBranchExt.Visible = False
                            lblTotalScannedInt.Text = String.Empty
                            MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
                            bringPanelToFront(pnlPLScanShopping, pnlPLScanIntPart)
                        End If
                    End If
                    Cursor.Current = Cursors.Default
                Else
                    lblPLIntExtStatusMsg.BackColor = Color.Red
                    lblPLIntExtStatusMsg.Text = msgCode
                    lblPLIntExtStatusMsgDesc.Text = msgDesc
                    lblPartNoInt.Text = String.Empty
                    lblSeqNoInt.Text = String.Empty
                    lblQtyInt.Text = String.Empty
                    lblBranchExt.Text = Nothing
                    lblTitleBranchExt.Visible = False
                    Cursor.Current = Cursors.Default
                End If

                loadlstView(lstViewScannedSummary, lblTotalScannedView)
                lblTotalScannedInt.Text = lstViewScannedSummary.Items.Count
            End If
        Else
            Call ChangeProcess()
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub KanbanScanChecker()
        Cursor.Current = Cursors.WaitCursor
        If ws_dcsClient.isOracleConnected Then '***
            InitWebServices()
            Dim datet As DateTime = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            Dim currentDate As String = datet.ToString("dd-MM-yyyy hh:mm:ss tt")

            Dim forceStatus As String = "N"
            If reasonInt <> Nothing Then
                forceStatus = "Y"
            End If

            Dim dt As DataTable = ws_dcsClient.getData("*", TblJSPSupplyPLDetailsView, String.Format(" AND PDIO_NO = {0} AND PART_NO = {1} AND SEQ_NO = {2}", SQLQuote(KB.PDIO_NO), SQLQuote(KB.PART_NO), KB.SEQ_NO))
            If dt.Rows.Count <> 0 Then
                KB.PDIO_ID = dt.Rows(0).Item("PDIO_ID").ToString
                KB.PDIO_NO = dt.Rows(0).Item("PDIO_NO").ToString
                KB.PART_NO = dt.Rows(0).Item("PART_NO").ToString
                KB.BACK_NO = dt.Rows(0).Item("BACK_NUMBER").ToString
                KB.QTY_ORDER = dt.Rows(0).Item("QTY_PER_BOX").ToString
                KB.ORG_ID = dt.Rows(0).Item("ORG_ID").ToString
                KB.TRANSACTION_CODE = dt.Rows(0).Item("TRANSACTION_CODE").ToString
            End If

            If forceStatus = "Y" Then
                msgCode = ws_validationClient.processValidation(GetBatchID("PROGRESS_LANE", "4"), gScannerID, "SUPPLY", "302", _
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                               Nothing, Nothing, Nothing, Nothing, KB.PDIO_ID, KB.PDIO_NO, KB.DOCK_CODE, _
                               KB.ORDER_TYPE, KB.TRANSPORTER_ID, KB.VENDOR_ID, KB.LANE_ID, KB.TIER, KB.PART_NO, KB.SEQ_NO, _
                               KB.BACK_NO, KB.QTY_ORDER, KB.DELIVERY_TYPE, KB.ORG_ID, KB.DELIVERY_DATE, gScannerID, currentDate, _
                               Nothing, Nothing, Nothing, _
                               Nothing, Nothing, Nothing, gScannerID, "Y", gScannerID, currentDate, _
                               Nothing, Nothing, Nothing, Nothing, forceStatus, reasonInt, "PL", _
                               SHOP.SHOP_ID, msgDesc)
            Else
                msgCode = ws_validationClient.processValidation(GetBatchID("PROGRESS_LANE", "4"), gScannerID, "SUPPLY", "302", _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, Nothing, KB.PDIO_ID, KB.PDIO_NO, KB.DOCK_CODE, _
                                   KB.ORDER_TYPE, KB.TRANSPORTER_ID, KB.VENDOR_ID, KB.LANE_ID, KB.TIER, KB.PART_NO, KB.SEQ_NO, _
                                   KB.BACK_NO, KB.QTY_ORDER, KB.DELIVERY_TYPE, KB.ORG_ID, KB.DELIVERY_DATE, gScannerID, currentDate, _
                                   Nothing, Nothing, Nothing, _
                                   Nothing, Nothing, Nothing, gScannerID, "Y", gScannerID, currentDate, _
                                   Nothing, Nothing, Nothing, Nothing, forceStatus, reasonInt, "PL", _
                                   SHOP.SHOP_ID, msgDesc)
            End If

            currentDate = datet.ToString("yyyy-MM-dd hh:mm:ss tt")

            If msgCode = "OK" Then
                If forceStatus = "Y" Then
                    bringPanelToFront(pnlPLScanIntPart, pnlPLScanIntPart)
                End If

                lblPartNoInt.Text = KB.PART_NO
                lblSeqNoInt.Text = KB.SEQ_NO
                lblQtyInt.Text = KB.QTY_ORDER
                lblTitleBranchExt.Visible = False
                lblBranchExt.Visible = False
                lblBranchExt.Text = String.Empty
                lblPLIntExtStatusMsg.BackColor = Color.LimeGreen
                lblPLIntExtStatusMsg.Text = msgCode
                lblPLIntExtStatusMsgDesc.Text = msgDesc
                txtPxPKanbanQR.Enabled = True
                btnFScanExt.Enabled = True

                If KB.TRANSACTION_CODE = "01" Then 'JSP
                    lblPartType.Text = Nothing
                    ProcessJSP()
                ElseIf KB.TRANSACTION_CODE = "02" Then 'MSP
                    lblPartType.Text = "MSP"
                    ProcessMSP(currentDate)
                End If
            ElseIf msgCode = "NG" Then
                lblPLIntExtStatusMsg.BackColor = Color.Red
                lblPLIntExtStatusMsg.Text = msgCode
                lblPLIntExtStatusMsgDesc.Text = msgDesc
                lblPartNoInt.Text = Nothing
                lblSeqNoInt.Text = Nothing
                lblQtyInt.Text = Nothing
                lblBranchExt.Text = Nothing
                lblTitleBranchExt.Visible = False
                txtKanbanQR.Focus()
                txtKanbanQR.SelectAll()
                txtPxPKanbanQR.Enabled = False
                btnFScanExt.Enabled = False
            End If
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub KanbanScanOffline()
        'o f f l i n e
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim dtModuleList As DataTable = New DataTable()
            dtModuleList = getData(String.Format("SELECT * FROM [{0}] WHERE PART_NO = {1} AND SEQ_NO = {2}", _
                                                 TblJSPSupplyPLPendingView, _
                                                 SQLQuote(KB.PART_NO), _
                                                 SQLQuote(KB.SEQ_NO)))
            If dtModuleList.Rows.Count > 0 Then
                KB.PDIO_ID = dtModuleList.Rows(0).Item("PDIO_ID")
                KB.PDIO_NO = dtModuleList.Rows(0).Item("PDIO_NO")
                KB.PART_NO = dtModuleList.Rows(0).Item("PART_NO")
                KB.SEQ_NO = dtModuleList.Rows(0).Item("SEQ_NO")
                KB.ORG_ID = dtModuleList.Rows(0).Item("ORG_ID")
                KB.PDIO_ID = dtModuleList.Rows(0).Item("PDIO_ID")
                KB.PDIO_ID = dtModuleList.Rows(0).Item("PDIO_ID")
                KB.TRANSACTION_CODE = dtModuleList.Rows(0).Item("TRANSACTION_CODE")

                Dim currentDate As String = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt")

                If KB.TRANSACTION_CODE = "01" Then
                    lblPartType.Text = "JSP"
                    ProcessJSP()
                ElseIf KB.TRANSACTION_CODE = "02" Then
                    lblPartType.Text = "MSP"
                    lblPLStatusMsg.Text = "OK"
                    lblPLStatusMsgDesc.Text = String.Empty
                    lblPartNoInt.Text = KB.PART_NO
                    lblSeqNoInt.Text = KB.SEQ_NO
                    lblQtyInt.Text = KB.QTY_ORDER
                    Call InsertKanbanTable(KB, Nothing, currentDate, "PL", "Y", reasonShop, reasonInt, reasonExt)
                    Call UpdatePostStatus(msgCode, KB.PART_NO)
                End If
            Else
                lblPLStatusMsg.Text = "NG"
                lblPLStatusMsgDesc.Text = "Part existed."
            End If
        Catch ex As Exception
            MsgBox("Part scanned failed.", MsgBoxStyle.Critical, "Kanban Scan Offline Mode")
            Cursor.Current = Cursors.Default
            txtKanbanQR.Focus()
            txtKanbanQR.SelectAll()
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub UpdateBatch()
        Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
        If Not ExecuteSQL(String.Format("UPDATE [{0}] SET CURRENT_NO = {1} WHERE CATEGORY = 'PROGRESS_LANE'", TblBatch, SQLQuote(currentNo))) Then
            Throw New Exception("Failed to Update Batch.")
        End If
    End Sub

    Private Sub UpdatePostStatus(ByVal MsgCode As String, ByVal PartNo As String)
        Dim dt As DataTable = New DataTable()
        dt = getData("SELECT * FROM [" & TblJSPSupplyInterface & "] " & _
                               "WHERE PART_NO = " & SQLQuote(PartNo))
        If dt.Rows.Count > 0 Then
            If Not ExecuteSQL("UPDATE [" & TblJSPSupplyInterface & "] " & _
                              "SET RETURN_VAL = " & SQLQuote(MsgCode) & " " & _
                              "WHERE ORDER_NO = " & SQLQuote(PartNo)) Then
                Throw New Exception
            End If
        End If

    End Sub

    Private Sub UpdateKanbanPendingDetails(ByVal PART_NO As String, ByVal SEQ_NO As String)
        Dim dtModuleList As DataTable = New DataTable()
        dtModuleList = getData(String.Format("SELECT * FROM [{0}] WHERE PART_NO = {1} AND SEQ_NO = {2}", _
                                             TblJSPSupplyPLPendingView, _
                                             SQLQuote(PART_NO), _
                                             SQLQuote(SEQ_NO)))
        If dtModuleList.Rows.Count > 0 Then
            If Not ExecuteSQL(String.Format("DELETE FROM [{0}] WHERE PART_NO = {1} AND SEQ_NO = {2}", _
                                            TblJSPSupplyPLPendingView, _
                                            SQLQuote(PART_NO), _
                                            SQLQuote(SEQ_NO))) Then
                Throw New Exception("Failed to Create Local Module Table.")
            End If
        End If
    End Sub

#End Region

#Region ". PxP Kanban QR ."

    Private Sub ProcessJSP()
        txtKanbanQR.BackColor = Color.Transparent
        txtPxPKanbanQR.Enabled = True
        txtPxPKanbanQR.BackColor = Color.LimeGreen
        txtPxPKanbanQR.Text = String.Empty
        txtPxPKanbanQR.SelectAll()
        txtPxPKanbanQR.Focus()
    End Sub

    Private Sub txtPxPKanbanQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPxPKanbanQR.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o n l i n e
                mode = True
                If Not String.IsNullOrEmpty(txtPxPKanbanQR.Text) Then
                    isForceScan = False
                    If IsKanbanPartQRReadable(txtPxPKanbanQR.Text) Then
                        Dim tempPartNo As String = KB.PART_NO.Replace("-", "")
                        tempPartNo = tempPartNo.Substring(0, 10)
                        If tempPartNo = PART.PARTS_NO Then
                            Call KanbanPartScanChecker()
                        Else
                            Throw New Exception("Part Not Matched.")
                        End If
                    Else
                        txtPxPKanbanQR.SelectAll()
                        txtPxPKanbanQR.Focus()
                    End If
                Else
                    MessageBox.Show("PxP Kanban QR is required")
                    txtPxPKanbanQR.SelectAll()
                    txtPxPKanbanQR.Focus()
                End If
            Catch ex As WebException
                Call OfflineKanban()
            Catch ex As Exception
                MsgBox("PxP Kanban QR scanned failed.", MsgBoxStyle.Critical, "Kanban Part Scan")
                Cursor.Current = Cursors.Default
                txtPxPKanbanQR.Focus()
                txtPxPKanbanQR.SelectAll()
            End Try
        End If
    End Sub

    Private Sub OfflinePxPKanban()
        'o f f l i n e
        mode = False
        modScanOffline = True
        TimerCheckOnline.Enabled = True
        If offline1stTFlag Then
            If MessageBox.Show("Connection is down. Continue scan with offline mode?", _
                               "PxP Kanban Scan Offline Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                offline1stTFlag = False
                Call InsertKanbanScanOK(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), "N")
            Else
                Cursor.Current = Cursors.Default
                txtPxPKanbanQR.SelectAll()
                txtPxPKanbanQR.Focus()
                Exit Sub
            End If
        Else
            Call InsertKanbanScanOK(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), "N")
        End If
    End Sub

    Private Function ModuleRetrieveOnline(ByVal moduleNo As String, ByVal partNo As String, ByVal seqNo As String, ByVal branch As String) As DataTable
        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then '***
                Return ws_dcsClient.getData("*", TblJSPSupplyPLPartsIDView, _
                                            " AND MODULE_NO = " & SQLQuote(moduleNo) & " AND PART_NO = " & SQLQuote(partNo) & _
                                            " AND PART_SEQ_NO = " & seqNo & " AND BRANCH_NO = " & branch)
            End If
        End If
        Return Nothing
    End Function

    Private Sub KanbanPartScanChecker()
        Cursor.Current = Cursors.WaitCursor
        If ws_dcsClient.isOracleConnected Then '***
            InitWebServices()
            Dim forceStatusExt As String = "N"
            Dim forceStatusInt As String = "N"
            Dim dtPartList As DataTable = New DataTable

            dtPartList = ModuleRetrieveOnline(PART.MODULE_CATEGORY + PART.LOT_NO, String.Format("{0}-{1}", PART.PARTS_NO.Insert(5, "-"), PART.PART_NO_SFX), PART.PART_SEQ_NUMBER, PART.PART_BRANCH_NUMBER)

            If dtPartList.Rows.Count > 0 Then
                PART.MODULE_ID = dtPartList.Rows(0).Item("MODULE_ID").ToString()
                PART.MODULE_NO = dtPartList.Rows(0).Item("MODULE_NO").ToString()
                PART.PART_ID = dtPartList.Rows(0).Item("PART_ID").ToString()
            End If

            If reasonExt <> 0 Then
                forceStatusExt = "Y"
                PART.QTY_BOX = dtPartList.Rows(0).Item("QUANTITY_BOX").ToString()
            End If

            If reasonInt <> 0 Then
                forceStatusInt = "Y"
            End If

            Dim dt As DateTime = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            Dim currentDate As String = dt.ToString("dd-MM-yyyy hh:mm:ss tt")

            msgCode = ws_validationClient.processValidation(GetBatchID("PROGRESS_LANE", "4"), gScannerID, "SUPPLY", "303", _
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                               Nothing, IIf(PART.MODULE_ID.Trim() = Nothing, Nothing, PART.MODULE_ID), _
                               IIf(PART.MODULE_NO.Trim() = Nothing, Nothing, PART.MODULE_NO), _
                               PART.PARTS_NO, PART.PART_NO_SFX, PART.PART_SEQ_NUMBER, PART.QTY_BOX, _
                               IIf(PART.MANUFACTURE_CODE.Trim() = Nothing, Nothing, PART.MANUFACTURE_CODE), _
                               IIf(PART.SUPPLIER_CODE.Trim() = Nothing, Nothing, PART.SUPPLIER_CODE), _
                               IIf(PART.SUPPLIER_PLANT_CODE.Trim() = Nothing, Nothing, PART.SUPPLIER_PLANT_CODE), _
                               IIf(PART.SUPPLIER_SHIPPING_DOCK.Trim() = Nothing, Nothing, PART.SUPPLIER_SHIPPING_DOCK), _
                               IIf(PART.BEFORE_PACKING_ROUTING.Trim() = Nothing, Nothing, PART.BEFORE_PACKING_ROUTING), _
                               IIf(PART.RECEIVING_COMPANY_CODE.Trim() = Nothing, Nothing, PART.RECEIVING_COMPANY_CODE), _
                               IIf(PART.RECEIVING_PLANT_CODE.Trim() = Nothing, Nothing, PART.RECEIVING_PLANT_CODE), _
                               IIf(PART.RECEIVING_DOCK_CODE.Trim() = Nothing, Nothing, PART.RECEIVING_DOCK_CODE), _
                               IIf(PART.PACKING_ROUTING_CODE.Trim() = Nothing, Nothing, PART.PACKING_ROUTING_CODE), _
                               IIf(PART.GRANTER_CODE.Trim() = Nothing, Nothing, PART.GRANTER_CODE), _
                               IIf(PART.ORDER_TYPE.Trim() = Nothing, Nothing, PART.ORDER_TYPE), _
                               IIf(PART.KANBAN_CLASSIFICATION.Trim() = Nothing, Nothing, PART.KANBAN_CLASSIFICATION), _
                               IIf(PART.MROS.Trim() = Nothing, Nothing, PART.MROS), _
                               IIf(PART.ORDER_NUMBER.Trim() = Nothing, Nothing, PART.ORDER_NUMBER), _
                               IIf(PART.DELIVERY_CODE.Trim() = Nothing, Nothing, PART.DELIVERY_CODE), _
                               IIf(PART.DELIVERY_NUMBER.Trim() = Nothing, Nothing, PART.DELIVERY_NUMBER), _
                               IIf(PART.BACK_NUMBER.Trim() = Nothing, Nothing, PART.BACK_NUMBER), _
                               IIf(PART.RUNOUT_FLAG.Trim() = Nothing, Nothing, PART.RUNOUT_FLAG), _
                               IIf(PART.BOX_TYPE.Trim() = Nothing, Nothing, PART.BOX_TYPE), _
                               IIf(PART.BRANCH_NUMBER.Trim() = Nothing, Nothing, PART.BRANCH_NUMBER), _
                               IIf(PART.ADDRESS.Trim() = Nothing, Nothing, PART.ADDRESS), _
                               IIf(PART.PACKING_DATE.Trim() = Nothing, Nothing, PART.PACKING_DATE), _
                               IIf(PART.KATASHIKI_JERSEY_NUMBER.Trim() = Nothing, Nothing, PART.KATASHIKI_JERSEY_NUMBER), _
                               IIf(PART.LOT_NO.Trim() = Nothing, Nothing, PART.LOT_NO), _
                               IIf(PART.MODULE_CATEGORY.Trim() = Nothing, Nothing, PART.MODULE_CATEGORY), _
                               IIf(PART.PART_BRANCH_NUMBER.Trim() = Nothing, Nothing, PART.PART_BRANCH_NUMBER), _
                               IIf(PART.DUMMY.Trim() = Nothing, Nothing, PART.DUMMY), _
                               IIf(PART.VERSION_NO.Trim() = Nothing, Nothing, PART.VERSION_NO), _
                               KB.PDIO_ID, KB.PDIO_NO, KB.DOCK_CODE, _
                               KB.ORDER_TYPE, KB.TRANSPORTER_ID, KB.VENDOR_ID, KB.LANE_ID, KB.TIER, KB.PART_NO, KB.SEQ_NO, _
                               KB.BACK_NO, KB.QTY_ORDER, KB.DELIVERY_TYPE, KB.ORG_ID, KB.DELIVERY_DATE, gScannerID, currentDate, _
                               Nothing, Nothing, Nothing, _
                               Nothing, Nothing, Nothing, gScannerID, "Y", gScannerID, currentDate, _
                               Nothing, Nothing, forceStatusExt, reasonExt, forceStatusInt, reasonInt, "PL", _
                               SHOP.SHOP_ID, msgDesc)

            lblPartType.Text = String.Empty
            currentDate = dt.ToString("yyyy-MM-dd hh:mm:ss tt")

            If msgCode = "OK" Then
                InsertKanbanScanOK(currentDate, "Y")
                msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("PROGRESS_LANE", "4"), "SUPPLY", "304", org_ID, Nothing, _
                                                                         Nothing, Nothing, Nothing, Nothing, msgDesc)
                If msgCode = "OK" Then
                    lblPLIntExtStatusMsg.BackColor = Color.LimeGreen
                    lblPLIntExtStatusMsg.Text = msgCode
                    lblPLIntExtStatusMsgDesc.Text = msgDesc
                    lblPartNoInt.Text = String.Empty
                    lblSeqNoInt.Text = String.Empty
                    lblQtyInt.Text = String.Empty
                    lblTitleBranchExt.Visible = True
                    lblBranchExt.Text = String.Empty
                    lblBranchExt.Visible = False
                    lblTitleBranchExt.Visible = False
                    lblTotalScannedInt.Text = String.Empty
                    txtKanbanQR.Text = String.Empty
                    txtKanbanQR.Focus()
                    txtKanbanQR.BackColor = Color.LimeGreen
                    txtPxPKanbanQR.BackColor = Color.Transparent
                    txtPxPKanbanQR.Text = String.Empty
                    Call DeletePendingRecord()
                    Call UpdateBatch()
                    Dim dbReader As SqlCeDataReader
                    dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0}", TblJSPSupplyPLPendingView), objConn)
                    If dbReader.Read Then
                        If CInt(dbReader(0)) = 0 Then
                            Call DeleteTable()
                            txtProdDate.Text = String.Empty
                            txtShop.Text = String.Empty
                            txtLane.Text = String.Empty
                            txtShoppingNo.Text = String.Empty
                            txtShoppingNo.Focus()
                            lblPLStatusMsg.Text = String.Empty
                            lblPLStatusMsgDesc.Text = String.Empty
                            MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
                            bringPanelToFront(pnlPLScanShopping, pnlPLScanIntPart)
                        End If
                    End If
                ElseIf msgCode = "NG" Then
                    lblPLIntExtStatusMsg.BackColor = Color.Red
                    lblPLIntExtStatusMsg.Text = msgCode
                    lblPLIntExtStatusMsgDesc.Text = msgDesc
                    lblPartNoInt.Text = String.Empty
                    lblSeqNoInt.Text = String.Empty
                    lblQtyInt.Text = String.Empty
                    lblTitleBranchExt.Visible = True
                    lblBranchExt.Text = String.Empty
                End If
            ElseIf msgCode = "NG" Then
                lblPLIntExtStatusMsg.BackColor = Color.Red
                lblPLIntExtStatusMsg.Text = msgCode
                lblPLIntExtStatusMsgDesc.Text = msgDesc
                lblPartNoInt.Text = Nothing
                lblSeqNoInt.Text = Nothing
                lblQtyInt.Text = Nothing
                txtPxPKanbanQR.Focus()
                txtPxPKanbanQR.SelectAll()
            End If
            loadlstView(lstViewScannedSummary, lblTotalScannedView)
            loadlstPendingView(lstViewPendingSummary, lblTotalPending)
            lblTotalScannedInt.Text = lstViewScannedSummary.Items.Count
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub InsertKanbanScanOK(ByVal currentDate As String, ByVal isOnline As String)
        Call InsertKanbanTable(KB, PART, currentDate, "PL", isOnline, reasonShop, reasonInt, reasonExt)
        Call UpdateKanbanPendingDetails(PART.PARTS_NO, KB.SEQ_NO)
        Call UpdatePostStatus(msgCode, PART.PARTS_NO)
        lblPLIntExtStatusMsg.BackColor = Color.LimeGreen
        lblPLIntExtStatusMsg.Text = msgCode
        lblPLIntExtStatusMsgDesc.Text = msgDesc
        lblPartNoInt.Text = PART.PARTS_NO
        lblSeqNoInt.Text = PART.PART_SEQ_NUMBER
        lblQtyInt.Text = PART.QTY_BOX
        lblTitleBranchExt.Visible = True
        lblBranchExt.Visible = True
        lblBranchExt.Text = PART.PART_BRANCH_NUMBER

        'proceed second scan
        txtKanbanQR.Text = String.Empty
        txtKanbanQR.BackColor = Color.LimeGreen
        txtKanbanQR.Focus()
        txtPxPKanbanQR.Text = String.Empty
        txtPxPKanbanQR.BackColor = Color.Transparent
        txtPxPKanbanQR.Enabled = False
        btnFScanExt.Enabled = False
    End Sub

#End Region

#End Region

#Region ". Abnormal Mode Navigation and Private Function ."

    Private Sub btnPLAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnScan.Click
        isNormal = False
        reasonShop = Nothing
        reasonInt = Nothing
        reasonExt = Nothing
        lblTitleBranchExtAbn.Visible = False
        lblBranchExtAbn.Visible = False
        txtPxPKanbanQRAbn.Enabled = False
        txtKanbanQRAbn.Focus()
        txtKanbanQRAbn.Enabled = True
        btnFScanExtAbn.Enabled = False
        lblTotalScannedAbn.Text = lstViewScannedSummaryAbn.Items.Count()
        lblPLStatusMsgAbn.Text = String.Empty
        lblPLStatusMsgDescAbn.Text = String.Empty
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbnScanPart, pnlPLAbn)
    End Sub

    Private Sub btnPLAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnView.Click
        Dim total As System.Windows.Forms.Label = New System.Windows.Forms.Label() With {.Text = "0"}
        loadlstViewAbn(lstViewScannedSummaryAbn, total)
        lblHeaderAbnVwDet.Text = String.Format("Total Record: {0}", total.Text)
        Me.Text = String.Format("{0} - View", strOfflineTitle)
        bringPanelToFront(pnlPLAbnViewDet, pnlPLAbn)
    End Sub

    Private Sub btnPLAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnPost.Click
        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    bringPanelToFront(pnlLogin, pnlPLAbn)
                    txtUsername.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("No connection to post.")
        End Try
    End Sub

    Private Sub btnPLAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLAbnDelete.Click
        loadlstViewAbnDelete(lstViewDelete, lblDeleteTotalAbn)
        bringPanelToFront(pnlPLDelete, pnlPLAbn)
    End Sub

    Private Sub btnCloseAbnPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnPL.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlPLMain, pnlPLAbn)
    End Sub

    Private Sub btnFScanIntAbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanIntAbn.Click
        GetReason(lstViewFScanInt)
        txtFSKanbanPartNo.Focus()
        isNormal = False
        GetTxnCode()
        cmbBoxTxnCode.Visible = True
        lblTxnCode.Visible = True
        Me.Text = String.Format("{0} - View", strOfflineTitle)
        bringPanelToFront(pnlPLFScanIntPart, pnlPLAbnScanPart)
    End Sub

    Private Sub btnFScanExtAbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanExtAbn.Click
        GetReason(lstViewFScanExt)
        txtFSPxPModuleNo.Focus()
        isNormal = False
        Me.Text = String.Format("{0} - View", strOfflineTitle)
        bringPanelToFront(pnlPLFScanExtPart, pnlPLAbnScanPart)
    End Sub

    Private Sub btnBackPLAbnScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackPLAbnScanPart.Click
        lblPartTypeAbn.Text = String.Empty
        txtKanbanQRAbn.Text = String.Empty
        txtPxPKanbanQRAbn.Text = String.Empty
        lblPartNoIntAbn.Text = String.Empty
        lblSeqNoIntAbn.Text = String.Empty
        lblQtyIntAbn.Text = String.Empty
        lblBranchExtAbn.Text = String.Empty
        lblBranchExtAbn.Visible = False
        lblTitleBranchExtAbn.Visible = False
        txtKanbanQRAbn.BackColor = Color.LimeGreen
        txtPxPKanbanQRAbn.BackColor = Color.Transparent
        lblPLStatusMsgAbn.BackColor = Color.Transparent
        txtKanbanQRAbn.Focus()
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLAbnScanPart)
    End Sub

    Private Sub btnClosePLAbnViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePLViewDet.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLAbnViewDet)
    End Sub

    Private Sub btnPLSubmitPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPLSubmitPosting.Click
        Dim sSQL As String = Nothing
        Dim forceScanKanbanReasonID As String = Nothing
        Dim forceScanKanbanStatus As String = "N"
        Dim forceScanPxPReasonID As String = Nothing
        Dim forceScanPxPStatus As String = "N"
        Dim lstDt As DataTable = New DataTable()
        Dim currentDate As String = Nothing

        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    Cursor.Current = Cursors.WaitCursor
                    lstDt = getDTData(String.Format("SELECT * FROM {0} WHERE RCV_INTERFACE_BATCH_ID = '{1}' AND SCANNER_SCREEN_CODE = '{2}'", TblJSPSupplyInterface, GetBatchID("PROGRESS_LANE", "4"), "PL"))
                    currentDate = ws_dcsClient.getTime

                    If lstDt.Rows.Count > 0 Then
                        For i As Integer = 0 To lstDt.Rows.Count - 1
                            Call InitWebServices()
                            If IsDBNull(lstDt.Rows(i).Item("FORCE_P2_REASON_ID")) Then
                                forceScanKanbanReasonID = Nothing
                            Else
                                forceScanKanbanReasonID = lstDt.Rows(i).Item("FORCE_P2_REASON_ID")
                                forceScanKanbanStatus = "Y"
                            End If

                            If IsDBNull(lstDt.Rows(i).Item("FORCE_PXP_REASON_ID")) Then
                                forceScanPxPReasonID = Nothing
                            Else
                                forceScanPxPReasonID = lstDt.Rows(i).Item("FORCE_PXP_REASON_ID")
                                forceScanPxPStatus = "Y"
                            End If

                            If Not IsDBNull(lstDt.Rows(i).Item("P2_PART_NO")) Then
                                Dim dt As DataTable = ws_dcsClient.getData("*", TblJSPSupplyPLDetailsView, _
                                                                           " AND PART_NO = " & SQLQuote(lstDt.Rows(i).Item("P2_PART_NO").ToString) & _
                                                                           " AND SEQ_NO = " & lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString)
                                If dt.Rows.Count <> 0 Then
                                    lstDt.Rows(i).Item("PDIO_ID") = dt.Rows(0).Item("PDIO_ID").ToString
                                    lstDt.Rows(i).Item("PDIO_NO") = dt.Rows(0).Item("PDIO_NO").ToString
                                    lstDt.Rows(i).Item("BACK_NO") = dt.Rows(0).Item("BACK_NUMBER").ToString
                                    lstDt.Rows(i).Item("QTY_ORDER") = dt.Rows(0).Item("QTY_PER_BOX").ToString
                                    lstDt.Rows(i).Item("ORG_ID") = dt.Rows(0).Item("ORG_ID").ToString
                                    KB.TRANSACTION_CODE = dt.Rows(0).Item("TRANSACTION_CODE").ToString
                                End If
                            End If

                            If Not IsDBNull(lstDt.Rows(i).Item("PXP_PART_NO")) And IsDBNull(lstDt.Rows(i).Item("MODULE_ID")) Then
                                Dim dt As DataTable = ModuleRetrieveOnline(lstDt.Rows(i).Item("MODULE_CATEGORY").ToString + lstDt.Rows(i).Item("LOT_NO").ToString, _
                                                      String.Format("{0}-{1}", lstDt.Rows(i).Item("PXP_PART_NO").ToString.Insert(5, "-"), lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString), _
                                                      lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString, _
                                                      lstDt.Rows(i).Item("PART_BRANCH_NO").ToString)
                                If dt.Rows.Count > 0 Then
                                    PART.MODULE_ID = dt.Rows(0).Item("MODULE_ID").ToString()
                                    PART.MODULE_NO = dt.Rows(0).Item("MODULE_NO").ToString()
                                    PART.PART_ID = dt.Rows(0).Item("PART_ID").ToString()
                                End If
                            End If

                            Dim tempDeliveryDate = Nothing
                            If Not String.IsNullOrEmpty(lstDt.Rows(i).Item("DELIVERY_DATE").ToString) Then
                                tempDeliveryDate = Convert.ToDateTime(lstDt.Rows(i).Item("DELIVERY_DATE").ToString()).ToString("dd/MM/yyyy")
                            End If
                            msgCode = ws_validationClient.processValidation(GetBatchID("PROGRESS_LANE", "4"), gScannerID, "SUPPLY", "305", _
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                      Nothing, Nothing, Nothing, Nothing, _
                                      IIf(lstDt.Rows(i).Item("PDIO_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PDIO_ID").ToString()), _
                                      IIf(lstDt.Rows(i).Item("PDIO_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PDIO_NO").ToString()), _
                                      IIf(lstDt.Rows(i).Item("DOCK_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("DOCK_CODE").ToString()), _
                                      IIf(lstDt.Rows(i).Item("PDIO_ORDER_TYPE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PDIO_ORDER_TYPE").ToString()), _
                                      IIf(lstDt.Rows(i).Item("TRANSPORTER_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("TRANSPORTER_ID").ToString()), _
                                      IIf(lstDt.Rows(i).Item("VENDOR_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("VENDOR_ID").ToString()), _
                                      IIf(lstDt.Rows(i).Item("LANE_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("LANE_ID").ToString()), _
                                      IIf(lstDt.Rows(i).Item("TIER").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("TIER").ToString()), _
                                      IIf(lstDt.Rows(i).Item("P2_PART_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("P2_PART_NO").ToString()), _
                                      IIf(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString()), _
                                      IIf(lstDt.Rows(i).Item("BACK_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("BACK_NO").ToString()), _
                                      IIf(lstDt.Rows(i).Item("QTY_ORDER").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("QTY_ORDER").ToString()), _
                                      IIf(lstDt.Rows(i).Item("DELIVERY_TYPE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("DELIVERY_TYPE").ToString()), _
                                      IIf(lstDt.Rows(i).Item("ORG_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("ORG_ID").ToString()), _
                                      tempDeliveryDate, _
                                      gScannerID, IIf(lstDt.Rows(i).Item("SUPPLY_DATE").ToString() = String.Empty, Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SUPPLY_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt")), _
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, gScannerID, "N", gScannerID, _
                                      IIf(lstDt.Rows(i).Item("SCAN_DATE").ToString() = String.Empty, Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SCAN_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt")), _
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "PL", _
                                      IIf(lstDt.Rows(i).Item("SHOP_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("SHOP_ID").ToString()), _
                                      msgDesc)
                            If msgCode = "OK" Then
                                If Not KB.TRANSACTION_CODE = "02" Then
                                    msgCode = ws_validationClient.processValidation(GetBatchID("PROGRESS_LANE", "4"), gScannerID, "SUPPLY", "306", _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    IIf(lstDt.Rows(i).Item("MODULE_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("MODULE_ID").ToString()), _
                                    IIf(lstDt.Rows(i).Item("MODULE_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("MODULE_NO").ToString()), _
                                    IIf(lstDt.Rows(i).Item("PXP_PART_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PXP_PART_NO").ToString()), _
                                    IIf(lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PXP_PART_NO_SFX").ToString()), _
                                    IIf(lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PXP_PART_SEQ_NO").ToString()), _
                                    IIf(lstDt.Rows(i).Item("QTY_BOX").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("QTY_BOX").ToString()), _
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
                                    IIf(lstDt.Rows(i).Item("PDIO_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PDIO_ID").ToString()), _
                                    IIf(lstDt.Rows(i).Item("PDIO_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PDIO_NO").ToString()), _
                                    IIf(lstDt.Rows(i).Item("DOCK_CODE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("DOCK_CODE").ToString()), _
                                    IIf(lstDt.Rows(i).Item("PDIO_ORDER_TYPE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("PDIO_ORDER_TYPE").ToString()), _
                                    IIf(lstDt.Rows(i).Item("TRANSPORTER_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("TRANSPORTER_ID").ToString()), _
                                    IIf(lstDt.Rows(i).Item("VENDOR_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("VENDOR_ID").ToString()), _
                                    IIf(lstDt.Rows(i).Item("LANE_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("LANE_ID").ToString()), _
                                    IIf(lstDt.Rows(i).Item("TIER").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("TIER").ToString()), _
                                    IIf(lstDt.Rows(i).Item("P2_PART_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("P2_PART_NO").ToString()), _
                                    IIf(lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString()), _
                                    IIf(lstDt.Rows(i).Item("BACK_NO").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("BACK_NO").ToString()), _
                                    IIf(lstDt.Rows(i).Item("QTY_ORDER").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("QTY_ORDER").ToString()), _
                                    IIf(lstDt.Rows(i).Item("DELIVERY_TYPE").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("DELIVERY_TYPE").ToString()), _
                                    IIf(lstDt.Rows(i).Item("ORG_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("ORG_ID").ToString()), _
                                    tempDeliveryDate, _
                                    gScannerID, IIf(lstDt.Rows(i).Item("SUPPLY_DATE").ToString() = String.Empty, Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SUPPLY_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt")), _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, gScannerID, "N", gScannerID, _
                                    IIf(lstDt.Rows(i).Item("SCAN_DATE").ToString() = String.Empty, Nothing, Convert.ToDateTime(lstDt.Rows(i).Item("SCAN_DATE").ToString()).ToString("dd-MM-yyyy hh:mm:ss tt")), _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "PL", _
                                    IIf(lstDt.Rows(i).Item("SHOP_ID").ToString() = String.Empty, Nothing, lstDt.Rows(i).Item("SHOP_ID").ToString()), _
                                    msgDesc)
                                End If
                                If msgCode = "OK" Then
                                    lblPLStatusMsgAbn.Text = msgCode
                                    lblPLStatusMsgDescAbn.Text = msgDesc
                                    sSQL = String.Format("UPDATE {0} SET RETURN_VAL = '{1}' WHERE PDIO_NO = '{2}' AND P2_PART_NO = '{3}' AND P2_PART_SEQ_NO = {4}", TblJSPSupplyInterface, _
                                                         msgCode, lstDt.Rows(i).Item("PDIO_NO").ToString(), lstDt.Rows(i).Item("P2_PART_NO").ToString(), lstDt.Rows(i).Item("P2_PART_SEQ_NO").ToString())
                                    ExecuteSQL(sSQL)

                                    msgCode = ws_inventoryClient.processInventoryConsumption(GetBatchID("PROGRESS_LANE", "4"), "SUPPLY", "307", org_ID, Nothing, Nothing, _
                                                                        Nothing, Nothing, Nothing, msgDesc)
                                    If msgCode = "OK" Then
                                        lblPLStatusMsgAbn.Text = msgCode
                                        lblPLStatusMsgDescAbn.Text = msgDesc
                                        sSQL = String.Format("UPDATE {0} SET POSTED = '{1}' WHERE RCV_INTERFACE_BATCH_ID = '{2}'", TblJSPSupplyInterface, _
                                                                           "Y", GetBatchID("PROGRESS_LANE", "4"))
                                        If ExecuteSQL(sSQL) = True Then
                                            UpdateBatch()
                                        End If
                                    Else
                                        lblPLStatusMsgAbn.Text = msgCode
                                        lblPLStatusMsgDescAbn.Text = msgDesc
                                    End If
                                Else
                                    loadlstViewAbn(lstViewPosting, lblPostingTotalPdgAbn)
                                    MsgBox("Failed to post: " & msgDesc, MsgBoxStyle.Information, Me.Text)
                                End If
                            Else
                                loadlstViewAbn(lstViewPosting, lblPostingTotalPdgAbn)
                                MsgBox("Failed to post: " & msgDesc, MsgBoxStyle.Information, Me.Text)
                            End If
                        Next
                        loadlstViewAbn(lstViewPosting, lblPostingTotalPdgAbn)
                        MsgBox(successMsg, MsgBoxStyle.Information, Me.Text)
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
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClosePLPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePLPosting.Click
        txtUsername.Text = String.Empty
        txtPwd.Text = String.Empty
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLPosting)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Confirm to delete all posted record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            DeleteTableAbn()
            loadlstViewAbn(lstViewDelete, lblDeleteTotalAbn)
        End If
    End Sub

    Private Sub btnCloseDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePLDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlPLAbn, pnlPLDelete)
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
                    bringPanelToFront(pnlPLPosting, pnlLogin)
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
        txtUsername.Text = String.Empty
        txtPwd.Text = String.Empty
        bringPanelToFront(pnlPLAbn, pnlLogin)
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

    Private Sub txtKanbanQRAbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKanbanQRAbn.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not String.IsNullOrEmpty(txtKanbanQRAbn.Text) Then
                    If IsKanbanQRReadable(txtKanbanQRAbn.Text) Then
                        Call VerifyOrgId(KB.ORG_ID.Trim())
                        Call KanbanAbnScanChecker() 'Part already scanned?
                    Else
                        txtKanbanQRAbn.SelectAll()
                        txtKanbanQRAbn.Focus()
                    End If
                Else
                    MessageBox.Show("Kanban QR is required")
                    txtKanbanQRAbn.SelectAll()
                    txtKanbanQRAbn.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub KanbanAbnScanChecker()
        Try
            Dim dbReader As SqlCeDataReader
            'Part already scanned?
            If isForceScan Then
                dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE P2_PART_NO = '{1}' AND P2_PART_SEQ_NO = '{2}'", TblJSPSupplyInterface, KB.PART_NO, KB.SEQ_NO), objConn)
            Else
                dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PDIO_NO = '{1}' AND P2_PART_NO = '{2}' AND P2_PART_SEQ_NO = '{3}'", TblJSPSupplyInterface, KB.PDIO_NO, KB.PART_NO, KB.SEQ_NO), objConn)
            End If
            If dbReader.Read Then
                If CInt(dbReader(0)) > 0 Then
                    txtKanbanQRAbn.Focus()
                    txtKanbanQRAbn.SelectAll()
                    lblPLStatusMsgAbn.BackColor = Color.Red
                    lblPLStatusMsgAbn.Text = "Duplicated Part No"
                    lblPLStatusMsgDescAbn.Text = String.Empty
                    Cursor.Current = Cursors.Default
                Else
                    lblPartNoIntAbn.Text = KB.PART_NO
                    lblSeqNoIntAbn.Text = KB.SEQ_NO
                    lblQtyIntAbn.Text = KB.QTY_ORDER
                    lblBranchExtAbn.Text = String.Empty
                    lblBranchExtAbn.Visible = False
                    lblTitleBranchExtAbn.Visible = False

                    If KB.TRANSACTION_CODE = "01" Then 'JSP
                        lblPartTypeAbn.Text = Nothing
                        txtKanbanQRAbn.BackColor = Color.Transparent
                        txtPxPKanbanQRAbn.BackColor = Color.LimeGreen
                        txtPxPKanbanQRAbn.Enabled = True
                        btnFScanExtAbn.Enabled = True
                        txtPxPKanbanQRAbn.Focus()
                    ElseIf KB.TRANSACTION_CODE = "02" Then 'MSP
                        lblPartTypeAbn.Text = "MSP"
                        InsertKanbanTable(KB, Nothing, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), "PL", "N", reasonShop, reasonInt, reasonExt)
                        loadlstViewAbn(lstViewScannedSummaryAbn, lblTotalScannedAbn)
                        lblTotalScannedAbn.Text = lstViewScannedSummaryAbn.Items.Count
                        txtKanbanQRAbn.Text = String.Empty
                        txtKanbanQRAbn.Focus()
                    End If

                    lblPLStatusMsgAbn.Text = "OK"
                    lblPLStatusMsgAbn.BackColor = Color.LimeGreen
                    lblPLStatusMsgDescAbn.Text = String.Empty
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPxPKanbanQRAbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPxPKanbanQRAbn.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not String.IsNullOrEmpty(txtPxPKanbanQRAbn.Text) Then
                    If IsKanbanPartQRReadable(txtPxPKanbanQRAbn.Text) Then
                        Call KanbanPartAbnScanChecker()
                    End If
                Else
                    MessageBox.Show("PxP Kanban QR is required")
                End If
                txtPxPKanbanQRAbn.SelectAll()
                txtPxPKanbanQRAbn.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub KanbanPartAbnScanChecker()
        Try
            Dim p2PartNo = KB.PART_NO.Replace("-", "")
            If (p2PartNo.Substring(0, 10) = PART.PARTS_NO) Then 'Part Matching?
                Dim dbReader As SqlCeDataReader
                dbReader = OpenRecordset(String.Format("SELECT COUNT(*) FROM {0} WHERE PXP_PART_NO = {1} AND PXP_PART_SEQ_NO = {2} AND MODULE_NO = {3} AND PART_BRANCH_NO = {4}", _
                                                       TblJSPSupplyInterface, SQLQuote(PART.PARTS_NO), SQLQuote(PART.PART_SEQ_NUMBER), SQLQuote(PART.MODULE_NO), _
                                                       SQLQuote(PART.PART_BRANCH_NUMBER)), objConn)
                If dbReader.Read Then
                    If CInt(dbReader(0)) > 0 Then
                        lblPLStatusMsgAbn.BackColor = Color.Red
                        lblPLStatusMsgAbn.Text = "Already Exist"
                        lblPLStatusMsgDescAbn.Text = String.Empty
                        txtPxPKanbanQRAbn.Focus()
                        txtPxPKanbanQRAbn.SelectAll()
                    Else
                        PART.MODULE_ID = String.Empty
                        PART.PART_ID = String.Empty
                        InsertKanbanTable(KB, PART, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), "PL", "N", reasonShop, reasonInt, reasonExt)
                        lblPartNoIntAbn.Text = String.Format("{0}-{1}", PART.PARTS_NO.Insert(5, "-"), PART.PART_NO_SFX)
                        lblSeqNoIntAbn.Text = PART.PART_SEQ_NUMBER
                        lblQtyIntAbn.Text = PART.QTY_BOX
                        lblBranchExtAbn.Text = PART.PART_BRANCH_NUMBER
                        lblBranchExtAbn.Visible = True
                        lblTitleBranchExtAbn.Visible = True
                        loadlstViewAbn(lstViewScannedSummaryAbn, lblTotalScannedAbn)
                        lblTotalScannedAbn.Text = lstViewScannedSummaryAbn.Items.Count()
                        lblPLStatusMsgAbn.BackColor = Color.LimeGreen
                        lblPLStatusMsgAbn.Text = "OK"
                        lblPLStatusMsgDescAbn.Text = String.Empty

                        'Clear
                        lblPartNoIntAbn.Text = String.Empty
                        lblSeqNoIntAbn.Text = String.Empty
                        lblQtyIntAbn.Text = String.Empty
                        lblBranchExtAbn.Text = String.Empty
                        lblTitleBranchExtAbn.Visible = False
                        lblBranchExtAbn.Visible = False
                        txtKanbanQRAbn.Focus()
                        txtKanbanQRAbn.Text = String.Empty
                        txtKanbanQRAbn.Enabled = True
                        txtKanbanQRAbn.BackColor = Color.LimeGreen
                        btnFScanExtAbn.Enabled = False
                        txtPxPKanbanQRAbn.Text = String.Empty
                        txtPxPKanbanQRAbn.Enabled = False
                        txtPxPKanbanQRAbn.BackColor = Color.Transparent
                        reasonShop = Nothing
                        reasonInt = Nothing
                        reasonExt = Nothing
                    End If
                End If

            Else
                lblPartNoIntAbn.Text = String.Empty
                lblSeqNoIntAbn.Text = String.Empty
                lblQtyIntAbn.Text = String.Empty
                lblBranchExtAbn.Text = String.Empty
                lblTitleBranchExtAbn.Visible = True
                lblBranchExtAbn.Visible = True
                lblPLStatusMsgAbn.Text = "Invalid Part No"
                lblPLStatusMsgDescAbn.Text = "Part No Not Match."
                lblPLStatusMsgAbn.BackColor = Color.Red
                txtKanbanQRAbn.Focus()
                txtKanbanQRAbn.SelectAll()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

End Class
