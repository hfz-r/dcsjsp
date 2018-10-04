Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Data.SqlServerCe
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Threading
Imports DCSJSP.GeneralFunction

Public Class frmUnpack

#Region ". Property ."
    Public Class Module_Input
        Private _module_no As String
        Property MODULE_NO() As String
            Get
                Return _module_no
            End Get
            Set(ByVal value As String)
                _module_no = value
            End Set
        End Property
        '---
        Private _pilling_no As String
        Property PILLING_NO() As String
            Get
                Return _pilling_no
            End Get
            Set(ByVal value As String)
                _pilling_no = value
            End Set
        End Property

        Private _gross_weight As Decimal
        Property GROSS_WEIGHT() As Decimal
            Get
                Return _gross_weight
            End Get
            Set(ByVal value As Decimal)
                _gross_weight = value
            End Set
        End Property

        Private _order_no As String
        Property ORDER_NO() As String
            Get
                Return _order_no
            End Get
            Set(ByVal value As String)
                _order_no = value
            End Set
        End Property

        Private _ReasonID As String = Nothing
        Property REASONID() As String
            Get
                Return _ReasonID
            End Get
            Set(ByVal value As String)
                _ReasonID = value
            End Set
        End Property

    End Class

    Public Class Part_Input
        Private _manufacture_code As String
        Property MANUFACTURE_CODE() As String
            Get
                Return _manufacture_code
            End Get
            Set(ByVal value As String)
                _manufacture_code = value
            End Set
        End Property

        Private _supplier_code As String
        Property SUPPLIER_CODE() As String
            Get
                Return _supplier_code
            End Get
            Set(ByVal value As String)
                _supplier_code = value
            End Set
        End Property

        Private _supplier_plant_code As String
        Property SUPPLIER_PLANT_CODE() As String
            Get
                Return _supplier_plant_code
            End Get
            Set(ByVal value As String)
                _supplier_plant_code = value
            End Set
        End Property

        Private _supplier_shipping_dock As String
        Property SUPPLIER_SHIPPING_DOCK() As String
            Get
                Return _supplier_shipping_dock
            End Get
            Set(ByVal value As String)
                _supplier_shipping_dock = value
            End Set
        End Property

        Private _before_packing_routing As String
        Property BEFORE_PACKING_ROUTING() As String
            Get
                Return _before_packing_routing
            End Get
            Set(ByVal value As String)
                _before_packing_routing = value
            End Set
        End Property

        Private _receiving_company_code As String
        Property RECEIVING_COMPANY_CODE() As String
            Get
                Return _receiving_company_code
            End Get
            Set(ByVal value As String)
                _receiving_company_code = value
            End Set
        End Property

        Private _receiving_plant_code As String
        Property RECEIVING_PLANT_CODE() As String
            Get
                Return _receiving_plant_code
            End Get
            Set(ByVal value As String)
                _receiving_plant_code = value
            End Set
        End Property

        Private _receiving_dock_code As String
        Property RECEIVING_DOCK_CODE() As String
            Get
                Return _receiving_dock_code
            End Get
            Set(ByVal value As String)
                _receiving_dock_code = value
            End Set
        End Property

        Private _packing_routing_code As String
        Property PACKING_ROUTING_CODE() As String
            Get
                Return _packing_routing_code
            End Get
            Set(ByVal value As String)
                _packing_routing_code = value
            End Set
        End Property

        Private _granter_code As String
        Property GRANTER_CODE() As String
            Get
                Return _granter_code
            End Get
            Set(ByVal value As String)
                _granter_code = value
            End Set
        End Property

        Private _order_type As String
        Property ORDER_TYPE() As String
            Get
                Return _order_type
            End Get
            Set(ByVal value As String)
                _order_type = value
            End Set
        End Property

        Private _kanban_classification As String
        Property KANBAN_CLASSIFICATION() As String
            Get
                Return _kanban_classification
            End Get
            Set(ByVal value As String)
                _kanban_classification = value
            End Set
        End Property

        Private _delivery_date As String
        Property DELIVERY_DATE() As String
            Get
                Return _delivery_date
            End Get
            Set(ByVal value As String)
                _delivery_date = value
            End Set
        End Property

        Private _delivery_code As String
        Property DELIVERY_CODE() As String
            Get
                Return _delivery_code
            End Get
            Set(ByVal value As String)
                _delivery_code = value
            End Set
        End Property

        Private _mros As String
        Property MROS() As String
            Get
                Return _mros
            End Get
            Set(ByVal value As String)
                _mros = value
            End Set
        End Property

        Private _order_number As String
        Property ORDER_NUMBER() As String
            Get
                Return _order_number
            End Get
            Set(ByVal value As String)
                _order_number = value
            End Set
        End Property

        Private _delivery_number As String
        Property DELIVERY_NUMBER() As String
            Get
                Return _delivery_number
            End Get
            Set(ByVal value As String)
                _delivery_number = value
            End Set
        End Property

        Private _back_number As String
        Property BACK_NUMBER() As String
            Get
                Return _back_number
            End Get
            Set(ByVal value As String)
                _back_number = value
            End Set
        End Property

        Private _parts_no As String
        Property PARTS_NO() As String
            Get
                Return _parts_no
            End Get
            Set(ByVal value As String)
                _parts_no = value
            End Set
        End Property

        Private _part_no_sfx As String
        Property PART_NO_SFX() As String
            Get
                Return _part_no_sfx
            End Get
            Set(ByVal value As String)
                _part_no_sfx = value
            End Set
        End Property

        Private _qty_box As String
        Property QTY_BOX() As String
            Get
                Return _qty_box
            End Get
            Set(ByVal value As String)
                _qty_box = value
            End Set
        End Property

        Private _runout_flag As String
        Property RUNOUT_FLAG() As String
            Get
                Return _runout_flag
            End Get
            Set(ByVal value As String)
                _runout_flag = value
            End Set
        End Property

        Private _delivery_code_2 As String
        Property DELIVERY_CODE_2() As String
            Get
                Return _delivery_code_2
            End Get
            Set(ByVal value As String)
                _delivery_code_2 = value
            End Set
        End Property

        Private _box_type As String
        Property BOX_TYPE() As String
            Get
                Return _box_type
            End Get
            Set(ByVal value As String)
                _box_type = value
            End Set
        End Property

        Private _branch_number As String
        Property BRANCH_NUMBER() As String
            Get
                Return _branch_number
            End Get
            Set(ByVal value As String)
                _branch_number = value
            End Set
        End Property

        Private _address As String
        Property ADDRESS() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        Private _delivery_time As DateTime
        Property DELIVERY_TIME() As DateTime
            Get
                Return _delivery_time
            End Get
            Set(ByVal value As DateTime)
                _delivery_time = value
            End Set
        End Property

        Private _packing_date As String
        Property PACKING_DATE() As String
            Get
                Return _packing_date
            End Get
            Set(ByVal value As String)
                _packing_date = value
            End Set
        End Property

        Private _katashiki_jersey_number As String
        Property KATASHIKI_JERSEY_NUMBER() As String
            Get
                Return _katashiki_jersey_number
            End Get
            Set(ByVal value As String)
                _katashiki_jersey_number = value
            End Set
        End Property

        Private _lot_no As String
        Property LOT_NO() As String
            Get
                Return _lot_no
            End Get
            Set(ByVal value As String)
                _lot_no = value
            End Set
        End Property

        Private _module_category As String
        Property MODULE_CATEGORY() As String
            Get
                Return _module_category
            End Get
            Set(ByVal value As String)
                _module_category = value
            End Set
        End Property

        Private _part_seq_number As String
        Property PART_SEQ_NUMBER() As String
            Get
                Return _part_seq_number
            End Get
            Set(ByVal value As String)
                _part_seq_number = value
            End Set
        End Property

        Private _part_branch_number As String
        Property PART_BRANCH_NUMBER() As String
            Get
                Return _part_branch_number
            End Get
            Set(ByVal value As String)
                _part_branch_number = value
            End Set
        End Property

        Private _dummy As String
        Property DUMMY() As String
            Get
                Return _dummy
            End Get
            Set(ByVal value As String)
                _dummy = value
            End Set
        End Property

        Private _version_no As String
        Property VERSION_NO() As String
            Get
                Return _version_no
            End Get
            Set(ByVal value As String)
                _version_no = value
            End Set
        End Property

        Private _PartReasonID As String = Nothing
        Property PARTREASONID() As String
            Get
                Return _PartReasonID
            End Get
            Set(ByVal value As String)
                _PartReasonID = value
            End Set
        End Property

        Private _robb_module_no As String = "N"
        Property ROBB_MODD_NO() As String
            Get
                Return _robb_module_no
            End Get
            Set(ByVal value As String)
                _robb_module_no = value
            End Set
        End Property
    End Class

#End Region

#Region ". Variable Declaration ."

    Dim modScanOffline As Boolean = False
    Dim msgCode As String = Nothing
    Dim msgDesc As String = Nothing
    Dim curr_BATCH As String = Nothing
    Dim prev_BATCH As String = Nothing
    Dim skipFlag As Boolean = False
    Dim cntPending As Integer = 0
    Dim cntScanned As Integer = 0
    Dim cntAbnPartScanned As Integer = 0
    Dim cntAbnTotal As Integer = 0
    Dim cntAbnDelete As Integer = 0
    Dim showError As Boolean = True
    Dim offline1stTFlag As Boolean = True
    Dim showPnlModule As Boolean = False
    Private Const strOnlineTitle As String = "Unpacking"
    Private Const strOfflineTitle As String = "Abnormal Unpacking"
    Private MODD As New Module_Input()
    Private PART As New Part_Input()
    Shared lockObject As Object = New Object()

    Delegate Sub StringArgReturningVoidDelegate(ByVal [text] As String)
    Delegate Sub ListViewReturningVoidDelegate(ByRef [dt] As DataTable)

#End Region

#Region ". Main Menu Navigation ."

    Private Sub frmUnpack_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Init()
    End Sub

    Private Sub frmUnpack_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not skipFlag Then
            If MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                'ws_dcsClient.Dispose()
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Public Sub Init()

        Try
            Me.Text = strOnlineTitle
            footerStatusBar.Visible = False
            bringPanelToFront(pnlUnpackMain, pnlUnpackScanModule)
            Call InitUnpackBatch()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Unpack")
            Exit Sub
        End Try
    End Sub

#End Region

#Region ". Form Events/Private Function ."

    Private Sub btnScanUnpack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanUnpack.Click
        Call LoadModuleScan(True)
        mode = True
        bringPanelToFront(pnlUnpackScanModule, pnlUnpackMain)
    End Sub

    Private Sub btnAbnormalUnpack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnormalUnpacking.Click
        mode = False
        timer.Enabled = True
        modScanOffline = True
        bringPanelToFront(pnlUnpackAbn, pnlUnpackMain)
    End Sub

    Private Sub btnUnpackAbnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpackAbnScan.Click
        Call LoadAbnModuleScan(True)
        bringPanelToFront(pnlUnpackAbnScanModule, pnlUnpackAbn)
    End Sub

    Private Sub btnUnpackAbnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpackAbnView.Click
        Call LoadAbnUnpackDetails(True)
        showPnlModule = False
        bringPanelToFront(pnlUnpackAbnViewDet, pnlUnpackAbn)
    End Sub

    Private Sub btnUnpackAbnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpackAbnPost.Click
        Try
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then
                    If Counter("Pending") = 0 Then
                        txtUsername.Text = String.Empty
                        txtPwd.Text = String.Empty
                        txtUsername.Focus()
                        bringPanelToFront(pnlLogin, pnlUnpackAbn)
                    Else
                        MessageBox.Show("Module Not Complete Yet. Hence, can`t post.")
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("No connection to post.")
        End Try
    End Sub

    Private Sub btnUnpackAbnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpackAbnDelete.Click
        Call LoadAbnUnpackDelete()
        bringPanelToFront(pnlUnpackDelete, pnlUnpackAbn)
    End Sub

    Private Sub btnBackScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackScanModule.Click
        Me.Text = strOnlineTitle
        timer.Enabled = False
        timer.Dispose()
        bringPanelToFront(pnlUnpackMain, pnlUnpackScanModule)
    End Sub

    Private Sub btnBackFrViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFrViewDet.Click
        If Counter("Pending") = 0 Then
            Call LoadModuleScan()
            bringPanelToFront(pnlUnpackScanModule, pnlUnpackViewDet)
        Else
            Call LoadPartScan()
            bringPanelToFront(pnlUnpackScanPart, pnlUnpackViewDet)
        End If
    End Sub

    Private Sub btnForce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
        If lstUnpackModule.FocusedItem Is Nothing Then
            MsgBox("Please select pending items to force scan.", MsgBoxStyle.Critical, "Unpack Force")
            Exit Sub
        End If
        Call LoadPartForceScan(True)
        bringPanelToFront(pnlUnpackFScanPart, pnlUnpackViewDet)
    End Sub

    Private Sub btnBackFrModuleError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Text = strOnlineTitle
        ''''bringPanelToFront(pnlUnpackMain, pnlUnpackScanModuleError)
    End Sub

    Private Sub btnBackFrAbnormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlUnpackAbn, pnlUnpackAbnScanPart)
    End Sub

    Private Sub btnCloseViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnViewDet.Click
        If showPnlModule Then
            Call LoadAbnPartScan()
            bringPanelToFront(pnlUnpackAbnScanPart, pnlUnpackAbnViewDet)
        Else
            Me.Text = strOfflineTitle
            bringPanelToFront(pnlUnpackAbn, pnlUnpackAbnViewDet)
        End If
    End Sub

    Private Sub btnClosePosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePosting.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlUnpackAbn, pnlUnpackPosting)
    End Sub

    Private Sub btnCloseDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseDelete.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlUnpackAbn, pnlUnpackDelete)
    End Sub

    Private Sub btnCloseAbnUnpack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAbnUnpack.Click
        Me.Text = strOnlineTitle
        bringPanelToFront(pnlUnpackMain, pnlUnpackAbn)
    End Sub

    Private Sub btnCloseUnpack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseUnpack.Click
        Me.Close()
    End Sub

    Private Sub btnBackUnpackScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackUnpackScanPart.Click
        Me.Text = strOnlineTitle
        txtModuleQR.Focus()
        txtModuleQR.SelectAll()
        lblTotalScanned.Text = Counter("Scanned")
        bringPanelToFront(pnlUnpackScanModule, pnlUnpackScanPart)
    End Sub

    Private Sub btnBackUnpackAbnScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackUnpackAbnScanPart.Click
        Me.Text = strOfflineTitle
        txtAbnModNo.Focus()
        txtAbnModNo.SelectAll()
        lblAbnTotalScan.Text = Counter("AbnPartScanned")
        bringPanelToFront(pnlUnpackAbnScanModule, pnlUnpackAbnScanPart)
    End Sub

    Private Sub btnBackFScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScanPart.Click
        Call LoadPartScan()
        bringPanelToFront(pnlUnpackScanPart, pnlUnpackFScanPart)
    End Sub

    Private Sub btnBackAbnFScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackAbnFScanPart.Click
        Call LoadAbnPartScan()
        bringPanelToFront(pnlUnpackAbnScanPart, pnlUnpackAbnFscanPart)
    End Sub

    Private Sub btnBackFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFScan.Click
        Call LoadModuleScan()
        bringPanelToFront(pnlUnpackScanModule, pnlUnpackFScan)
    End Sub

    Private Sub btnBackUnpackAbnScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackUnpackAbnScanModule.Click
        Me.Text = strOfflineTitle
        bringPanelToFront(pnlUnpackAbn, pnlUnpackAbnScanModule)
    End Sub

    Private Sub btnBackUnpackAbnFscanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackUnpackAbnFscanModule.Click
        Call LoadAbnModuleScan()
        bringPanelToFront(pnlUnpackAbnScanModule, pnlUnpackAbnFscanModule)
    End Sub

    Private Sub isMakeUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles isMakeUp.Click
        If (isMakeUp.Checked) Then
            lblRobModuleNo.Visible = True
            txtUnpAbnFSRobModNo.Visible = True
            btnUnpAbnFSPartSave.Enabled = False
        Else
            lblRobModuleNo.Visible = False
            txtUnpAbnFSRobModNo.Visible = False
            txtUnpAbnFSRobModNo.Text = String.Empty

            If Not String.IsNullOrEmpty(txtUnpAbnFSPartNo.Text) And _
            Not String.IsNullOrEmpty(txtUnpAbnFSPartBranchNo.Text) And _
            Not String.IsNullOrEmpty(txtUnpAbnFSPartSeqNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = True
            End If
        End If
    End Sub

#End Region

#Region ". Module Scan ."

    Private Sub LoadModuleScan(Optional ByVal OnFirstLoad As Boolean = Nothing)
        Me.Text = strOnlineTitle
        txtModuleQR.Focus()
        txtModuleQR.Text = String.Empty
        txtModuleNo.Text = String.Empty
        txtOrderNo.Text = String.Empty
        btnScanDetails.Enabled = False
        lblMsgOkScanMod.BackColor = Color.LimeGreen
        lblMsgOkScanMod.Text = "Scan.."
        lblSuccessScanMod.Text = String.Empty
        If OnFirstLoad Then
            lblTotalScanned.Text = String.Empty
        Else
            lblTotalScanned.Text = Counter("Scanned")
        End If
    End Sub

    Private Sub txtModuleQR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtModuleQR.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o n l i n e
                mode = True
                If Not String.IsNullOrEmpty(txtModuleQR.Text) Then
                    Call IsModuleReadable(txtModuleQR.Text)
                    Call VerifyOrgId()
                    Call LoadModuleChecker()
                Else
                    Throw New Exception("Failed to read the Module QR.")
                End If
            Catch ex As WebException
                'o f f l i n e
                mode = False
                timer.Enabled = True
                Call ChangeProcess()
            Catch ex As CustomException
                MsgBox("Access Restricted!" + Environment.NewLine + ex.Message.ToString(), MsgBoxStyle.Critical, "Organization ID Mismatch")
                skipFlag = True
                Me.Close()
            Catch ex As Exception
                MsgBox("Module scanned failed.", MsgBoxStyle.Critical, "Unpack - Module Scan")
                Cursor.Current = Cursors.Default
                txtModuleQR.Focus()
                txtModuleQR.SelectAll()
            End Try
        End If
    End Sub

    Private Sub LoadModuleChecker(Optional ByVal IsForceScan As Boolean = Nothing)
        Dim dt As DateTime = Nothing
        Dim currentDate As String = Nothing

        Call InitWebServices()
        Cursor.Current = Cursors.WaitCursor
        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then '***
                dt = DateTime.ParseExact(GetServerTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
                currentDate = dt.ToString("dd-MM-yyyy hh:mm:ss tt")

                msgCode = ws_validationClient.processValidation(curr_BATCH, gScannerID, "UNPACK", "201", Nothing, Nothing, _
                                       MODD.PILLING_NO, MODD.GROSS_WEIGHT.ToString, Nothing, Nothing, _
                                       IIf(IsForceScan, "Y", "N"), IIf(IsForceScan, MODD.REASONID, Nothing), _
                                       Nothing, MODD.MODULE_NO, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                       Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                       Nothing, Nothing, Nothing, Nothing, MODD.ORDER_NO, Nothing, Nothing, _
                                       Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                       Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                       Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                       org_ID, Nothing, gScannerID, currentDate, gScannerID, currentDate, _
                                       Nothing, Nothing, Nothing, Nothing, gScannerID, "N", gScannerID, currentDate, _
                                       Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, msgDesc)
                currentDate = dt.ToString("yyyy-MM-dd hh:mm:ss tt")

                If msgCode = "OK" Then
                    Dim t As Thread = New Thread(AddressOf GetPartList)
                    t.IsBackground = True
                    t.Start()
                    SetPanelForResult("PanelModOK", msgCode, msgDesc, IsForceScan)
                ElseIf msgCode = "NG" Then
                    SetPanelForResult("PanelModError", msgCode, msgDesc, IsForceScan)
                End If
            End If
        End If
    End Sub

    Private Sub GetPartList()
        Dim sqlStr As String = Nothing
        Dim dtPartList As DataTable = New DataTable
        If ws_dcsClient.isConnected Then '***
            dtPartList = ws_dcsClient.getData("*", "JSP_UNPACKING_DETAILS_VIEW", String.Format(" AND MODULE_NO = {0} AND ORG_ID = {1}", SQLQuote(MODD.MODULE_NO), SQLQuote(org_ID)))
            If dtPartList.Rows.Count > 0 Then
                For i As Integer = 0 To dtPartList.Rows.Count - 1
                    If UnpackTableReader(UNPACK_PENDING, _
                                 dtPartList.Rows(i).Item("MODULE_NO").ToString, _
                                 dtPartList.Rows(i).Item("PART_NO").ToString.Replace("-", "").Substring(0, 10), _
                                 dtPartList.Rows(i).Item("PART_SEQ_NO").ToString, _
                                 dtPartList.Rows(i).Item("BRANCH_NO").ToString, _
                                 dtPartList.Rows(i).Item("ORG_ID").ToString, _
                                 dtPartList.Rows(i).Item("ROBB_MODULE_NO").ToString) = 0 Then
                        If UnpackTableReader(UNPACK_INTERFACE, _
                                 dtPartList.Rows(i).Item("MODULE_NO").ToString, _
                                 dtPartList.Rows(i).Item("PART_NO").ToString.Replace("-", "").Substring(0, 10), _
                                 dtPartList.Rows(i).Item("PART_SEQ_NO").ToString, _
                                 dtPartList.Rows(i).Item("BRANCH_NO").ToString, _
                                 dtPartList.Rows(i).Item("ORG_ID").ToString, _
                                 dtPartList.Rows(i).Item("ROBB_MODULE_NO").ToString) = 0 Then
                            sqlStr = "INSERT INTO [" & UNPACK_PENDING & "] (MODULE_ID, MODULE_NO, PART_NO, PART_NO_SFX, PART_NAME, QUANTITY_BOX, PART_SEQ_NO, " & _
                                     "BRANCH_NO, ORG_ID, ROBB_MODULE_ID, ROBB_MODULE_NO)"
                            sqlStr = sqlStr & "VALUES ("
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("MODULE_ID").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("MODULE_NO").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("PART_NO").ToString.Replace("-", "").Substring(0, 10)) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("PART_NO").ToString.Replace("-", "").Substring(10, 2)) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("PART_NAME").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("QUANTITY_BOX").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("PART_SEQ_NO").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("BRANCH_NO").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("ORG_ID").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("ROBB_MODULE_ID").ToString) & " , "
                            sqlStr = sqlStr & SQLQuote(dtPartList.Rows(i).Item("ROBB_MODULE_NO").ToString)
                            sqlStr = sqlStr & ")"
                            If CeNonQuery(sqlStr) = False Then
                                Throw New Exception("Failed to retrieved Part List from server.")
                            End If
                        End If
                    End If
                Next
                Me.SetLabelText(Counter("Scanned"))
            End If
        End If
    End Sub

    Private Sub SetLabelText(ByVal [text] As String)
        If lblTotalScanned.InvokeRequired Then
            Dim d As New StringArgReturningVoidDelegate(AddressOf SetLabelText)
            Me.Invoke(d, New Object() {[text]})
        Else
            lblTotalScanned.Text = [text]
        End If
    End Sub

#End Region

#Region ". Module Force Scan ."

    Private Sub LoadModuleForceScan()
        Me.Text = strOnlineTitle
        'Call PopulateForceScan("Module")
        Call GetAbnReasonCode(lstViewFSMod)
        Call InitWebServices()
        txtFSModNo.Text = String.Empty
        txtFSModOrder.Text = String.Empty
    End Sub

    Private Sub btnFScanModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanModule.Click
        txtFSModNo.Focus()
        Call LoadModuleForceScan()
        bringPanelToFront(pnlUnpackFScan, pnlUnpackScanModule)
    End Sub

    Private Sub txtFSModNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSModNo.TextChanged
        If Not String.IsNullOrEmpty(txtFSModNo.Text) Then
            If String.IsNullOrEmpty(txtFSModOrder.Text) Then
                btnSaveForceScan.Enabled = False
            Else
                btnSaveForceScan.Enabled = True
            End If
        Else
            btnSaveForceScan.Enabled = False
        End If
    End Sub

    Private Sub txtFSModOrder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSModOrder.TextChanged
        If Not String.IsNullOrEmpty(txtFSModOrder.Text) Then
            If String.IsNullOrEmpty(txtFSModNo.Text) Then
                btnSaveForceScan.Enabled = False
            Else
                btnSaveForceScan.Enabled = True
            End If
        Else
            btnSaveForceScan.Enabled = False
        End If
    End Sub

    Private Sub btnSaveForceScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveForceScan.Click
        Dim cnt As Integer = 0
        Dim listViews As ListViewItem = New ListViewItem()

        For i As Integer = 0 To lstViewFSMod.Items.Count - 1
            listViews = lstViewFSMod.Items(i)
            If Not listViews.Selected Then
                cnt = cnt + 1
            End If
            If cnt = lstViewFSMod.Items.Count Then
                MsgBox("Failed to save. Reason is required.", MsgBoxStyle.Critical, "Module Force Scan")
                Exit Sub
            End If
        Next

        If String.IsNullOrEmpty(txtFSModNo.Text) Then
            MsgBox("Module No is required", MsgBoxStyle.Critical, "Module Force Scan")
            txtFSModNo.Focus()
            txtFSModNo.SelectAll()
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtFSModOrder.Text) Then
            MsgBox("Order No is required", MsgBoxStyle.Critical, "Module Force Scan")
            txtFSModOrder.Focus()
            txtFSModOrder.SelectAll()
            Exit Sub
        End If

        MODD.MODULE_NO = txtFSModNo.Text.Trim
        MODD.ORDER_NO = txtFSModOrder.Text.Trim
        MODD.REASONID = lstViewFSMod.FocusedItem.SubItems(1).Text
        MODD.PILLING_NO = "0"
        MODD.GROSS_WEIGHT = 0D
        Call LoadModuleChecker(True)
    End Sub

#End Region

#Region ". Part Scan ."

    Private Sub LoadPartScan()
        Me.Text = strOnlineTitle
        lblPartModNo.Text = MODD.MODULE_NO
        txtKanbanQRPart.Focus()
        txtKanbanQRPart.Text = String.Empty
        lblPartNo.Text = String.Empty
        lblPartSeqNo.Text = String.Empty
        lblPartQty.Text = String.Empty
        lblPartBranchNo.Text = String.Empty
        lblPartSuccess.BackColor = Color.LimeGreen
        lblPartSuccess.Text = "Scan.."
        Label2.Text = String.Empty
        lblPartTotalScan.Text = Counter("Scanned")
    End Sub

    Private Sub txtKanbanQRPart_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKanbanQRPart.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o n l i n e
                mode = True
                If Not String.IsNullOrEmpty(txtKanbanQRPart.Text) Then
                    If Not IsPartQRReadable(txtKanbanQRPart.Text, Nothing) Then
                        Exit Sub
                    End If
                    Call LoadPartScanChecker()
                Else
                    Throw New Exception("Failed to read the Module QR.")
                End If
            Catch ex As WebException
                'o f f l i n e
                mode = False
                timer.Enabled = True
                modScanOffline = True
                If offline1stTFlag Then
                    If MessageBox.Show("Connection is down. Continue scan with offline mode?", _
                                       "Unpack - Part Scan Offline Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        offline1stTFlag = False
                        Call PartScanOffline()
                    Else
                        Cursor.Current = Cursors.Default
                        txtKanbanQRPart.Focus()
                        txtKanbanQRPart.SelectAll()
                        Exit Sub
                    End If
                Else
                    Call PartScanOffline()
                End If
            Catch ex As Exception
                MsgBox("Part scanned failed.", MsgBoxStyle.Critical, "Unpack - Part Scan")
                Cursor.Current = Cursors.Default
                txtKanbanQRPart.Focus()
                txtKanbanQRPart.SelectAll()
            End Try
        End If
    End Sub

    Private Sub LoadPartScanChecker(Optional ByVal IsForceScan As Boolean = Nothing)
        Dim dt As DateTime = Nothing
        Dim currentDate As String = Nothing

        Call InitWebServices()
        Cursor.Current = Cursors.WaitCursor

        If ws_dcsClient.isConnected Then
            If ws_dcsClient.isOracleConnected Then '***
                dt = DateTime.ParseExact(GetServerTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
                currentDate = dt.ToString("dd-MM-yyyy hh:mm:ss tt")
                Call GetRobbModuleNo(IsForceScan, txtUnpFSRobModNo.Text)
                msgCode = ws_validationClient.processValidation(curr_BATCH, gScannerID, "UNPACK", "202", Nothing, Nothing, MODD.PILLING_NO, _
                                       MODD.GROSS_WEIGHT.ToString, Nothing, Nothing, IIf(Not String.IsNullOrEmpty(MODD.REASONID), "Y", "N"), _
                                       MODD.REASONID, Nothing, MODD.MODULE_NO, PART.PARTS_NO, PART.PART_NO_SFX, PART.PART_SEQ_NUMBER, PART.QTY_BOX, _
                                       PART.MANUFACTURE_CODE, PART.SUPPLIER_CODE, PART.SUPPLIER_PLANT_CODE, PART.SUPPLIER_SHIPPING_DOCK, _
                                       PART.BEFORE_PACKING_ROUTING, PART.RECEIVING_COMPANY_CODE, PART.RECEIVING_PLANT_CODE, PART.RECEIVING_DOCK_CODE, _
                                       PART.PACKING_ROUTING_CODE, PART.GRANTER_CODE, PART.ORDER_TYPE, PART.KANBAN_CLASSIFICATION, PART.MROS, PART.ORDER_NUMBER, _
                                       PART.DELIVERY_CODE, PART.DELIVERY_NUMBER, PART.BACK_NUMBER, PART.RUNOUT_FLAG, PART.BOX_TYPE, PART.BRANCH_NUMBER, PART.ADDRESS, _
                                       PART.PACKING_DATE, PART.KATASHIKI_JERSEY_NUMBER, PART.LOT_NO, PART.MODULE_CATEGORY, PART.PART_BRANCH_NUMBER, PART.DUMMY, PART.VERSION_NO, _
                                       Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, org_ID, PART.DELIVERY_DATE, _
                                       gScannerID, currentDate, gScannerID, currentDate, Nothing, Nothing, Nothing, Nothing, gScannerID, "N", gScannerID, currentDate, Nothing, Nothing, _
                                       IIf(IsForceScan, "Y", "N"), IIf(IsForceScan, PART.PARTREASONID, Nothing), Nothing, Nothing, Nothing, Nothing, msgDesc)
                currentDate = dt.ToString("yyyy-MM-dd hh:mm:ss tt")
                Select Case msgCode
                    Case "OK"
                        Call AddToUnpackInterface("N", currentDate, IsForceScan, MODD, PART)
                        Call UpdateUnpackPendingDetails(MODD.MODULE_NO, PART.PARTS_NO, PART.PART_SEQ_NUMBER, PART.PART_BRANCH_NUMBER)
                        Call UpdatePostStatus(MODD.MODULE_NO, PART.PARTS_NO, PART.PART_SEQ_NUMBER, PART.PART_BRANCH_NUMBER, PART.ROBB_MODD_NO)
                        SetPanelForResult("PanelPartOK", msgCode, msgDesc, IsForceScan)
                    Case "NG"
                        SetPanelForResult("PanelPartError", msgCode, msgDesc, IsForceScan)
                    Case "CP"
                        Call AddToUnpackInterface("N", currentDate, IsForceScan, MODD, PART)
                        Call UpdateUnpackPendingDetails(MODD.MODULE_NO, PART.PARTS_NO, PART.PART_SEQ_NUMBER, PART.PART_BRANCH_NUMBER)
                        Call UpdatePostStatus(MODD.MODULE_NO, PART.PARTS_NO, PART.PART_SEQ_NUMBER, PART.PART_BRANCH_NUMBER, PART.ROBB_MODD_NO)
                        If ws_dcsClient.isOracleConnected Then '***
                            msgCode = ws_inventoryClient.processInventoryConsumption(curr_BATCH, "UNPACK", "203", org_ID, Nothing, _
                                                                            Nothing, Nothing, Nothing, Nothing, msgDesc)
                            If msgCode = "OK" Then
                                ExecuteSQL("DELETE FROM [" & UNPACK_INTERFACE & "] WHERE RCV_INTERFACE_BATCH_ID = " & SQLQuote(curr_BATCH))
                                SetPanelForResult("PanelPartPost", msgCode, msgDesc, IsForceScan)
                            Else
                                Throw New Exception("Failed to Post Part. - " & msgDesc)
                            End If
                        End If
                End Select
            End If
        End If
    End Sub

    Private Sub PartScanOffline(Optional ByVal IsForceScan As Boolean = Nothing)
        'o f f l i n e
        Dim dtPartList As DataTable = New DataTable()
        Try
            Cursor.Current = Cursors.WaitCursor
            Call GetRobbModuleNo(IsForceScan, txtUnpFSRobModNo.Text)
            dtPartList = getData("SELECT * FROM [" & UNPACK_PENDING & "] " & _
                                 "WHERE MODULE_NO = " & SQLQuote(MODD.MODULE_NO) & " " & _
                                 "AND PART_NO = " & SQLQuote(PART.PARTS_NO) & " " & _
                                 "AND PART_SEQ_NO = " & CInt(PART.PART_SEQ_NUMBER) & " " & _
                                 "AND BRANCH_NO = " & SQLQuote(PART.PART_BRANCH_NUMBER) & " " & _
                                 "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                                 "AND ROBB_MODULE_NO = " & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.ROBB_MODD_NO), PART.ROBB_MODD_NO, "N")))
            If dtPartList.Rows.Count > 0 Then
                Call AddToUnpackInterface("Y", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), IsForceScan, MODD, PART)
                Call UpdateUnpackPendingDetails(MODD.MODULE_NO, PART.PARTS_NO, PART.PART_SEQ_NUMBER, PART.PART_BRANCH_NUMBER)
                msgCode = "OK"
                msgDesc = "Succesfully Updated"
                SetPanelForResult("PanelPartOK", msgCode, msgDesc, IsForceScan)
            Else
                msgCode = "NG"
                msgDesc = "Part Already Scanned."
                SetPanelForResult("PanelPartError", msgCode, msgDesc, IsForceScan)
            End If
        Catch ex As Exception
            MsgBox("Part scanned failed. ", MsgBoxStyle.Critical, "Unpack - Part Scan Offline Mode")
            Cursor.Current = Cursors.Default
            txtKanbanQRPart.Focus()
            txtKanbanQRPart.SelectAll()
        End Try
    End Sub

#End Region

#Region ". Part Force Scan ."

    Private Sub LoadPartForceScan(Optional ByVal IsPopulate As Boolean = Nothing)
        Me.Text = strOnlineTitle
        lblUnpFSPModNo.Text = MODD.MODULE_NO
        'Call PopulateForceScan("Part")
        Call GetAbnReasonCode(lstviewUnpFSPReason)
        If IsPopulate Then
            txtUnpFSPartNo.Text = lstUnpackModule.FocusedItem.SubItems(1).Text
            txtUnpFSPBranchNo.Text = lstUnpackModule.FocusedItem.SubItems(4).Text
            txtUnpFSPSeqNo.Text = lstUnpackModule.FocusedItem.SubItems(2).Text
            txtUnpFSRobModNo.Text = lstUnpackModule.FocusedItem.SubItems(5).Text
        Else
            txtUnpFSPartNo.Text = String.Empty
            txtUnpFSPBranchNo.Text = String.Empty
            txtUnpFSPSeqNo.Text = String.Empty
            txtUnpFSRobModNo.Text = String.Empty
        End If
    End Sub

    Private Sub btnFScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFScanPart.Click
        Call LoadPartForceScan()
        bringPanelToFront(pnlUnpackFScanPart, pnlUnpackScanPart)
    End Sub

    Private Sub txtUnpFSPartNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnpFSPartNo.TextChanged
        If Not String.IsNullOrEmpty(txtUnpFSPartNo.Text) Then
            If String.IsNullOrEmpty(txtUnpFSPBranchNo.Text) Then
                btnSaveFScanPart.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpFSPSeqNo.Text) Then
                btnSaveFScanPart.Enabled = False
            Else
                btnSaveFScanPart.Enabled = True
            End If
        Else
            btnSaveFScanPart.Enabled = False
        End If
    End Sub

    Private Sub txtUnpFSPBranchNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnpFSPBranchNo.TextChanged
        If Not String.IsNullOrEmpty(txtUnpFSPBranchNo.Text) Then
            If String.IsNullOrEmpty(txtUnpFSPartNo.Text) Then
                btnSaveFScanPart.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpFSPSeqNo.Text) Then
                btnSaveFScanPart.Enabled = False
            Else
                btnSaveFScanPart.Enabled = True
            End If
        Else
            btnSaveFScanPart.Enabled = False
        End If
    End Sub

    Private Sub txtUnpFSPSeqNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnpFSPSeqNo.TextChanged
        If Not String.IsNullOrEmpty(txtUnpFSPSeqNo.Text) Then
            If String.IsNullOrEmpty(txtUnpFSPartNo.Text) Then
                btnSaveFScanPart.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpFSPBranchNo.Text) Then
                btnSaveFScanPart.Enabled = False
            Else
                btnSaveFScanPart.Enabled = True
            End If
        Else
            btnSaveFScanPart.Enabled = False
        End If
    End Sub

    Private Sub btnSaveFScanPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFScanPart.Click
        Dim cnt As Integer = 0
        Dim listViews As ListViewItem = New ListViewItem()

        For i As Integer = 0 To lstviewUnpFSPReason.Items.Count - 1
            listViews = lstviewUnpFSPReason.Items(i)
            If Not listViews.Selected Then
                cnt = cnt + 1
            End If
            If cnt = lstviewUnpFSPReason.Items.Count Then
                MsgBox("Failed to save. Reason is required.", MsgBoxStyle.Critical, "Part Force Scan")
                Exit Sub
            End If
        Next

        If String.IsNullOrEmpty(txtUnpFSPartNo.Text) Then
            MsgBox("Part No is required", MsgBoxStyle.Critical, "Part Force Scan")
            txtUnpFSPartNo.Focus()
            txtUnpFSPartNo.SelectAll()
            Exit Sub
        ElseIf txtUnpFSPartNo.Text.Length <> 14 Then
            MsgBox("Failed to save. Invalid Part No Format", MsgBoxStyle.Critical, "Part Force Scan")
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtUnpFSPSeqNo.Text) Then
            MsgBox("Seq No is required", MsgBoxStyle.Critical, "Part Force Scan")
            txtUnpFSPSeqNo.Focus()
            txtUnpFSPSeqNo.SelectAll()
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtUnpFSPBranchNo.Text) Then
            MsgBox("Branch No is required", MsgBoxStyle.Critical, "Part Force Scan")
            txtUnpFSPBranchNo.Focus()
            txtUnpFSPBranchNo.SelectAll()
            Exit Sub
        End If

        Try
            mode = True
            PART.PARTS_NO = txtUnpFSPartNo.Text.Replace("-", "").Substring(0, 10)
            PART.PART_NO_SFX = txtUnpFSPartNo.Text.Replace("-", "").Substring(10, 2)
            PART.PART_SEQ_NUMBER = txtUnpFSPSeqNo.Text
            PART.PART_BRANCH_NUMBER = txtUnpFSPBranchNo.Text
            PART.PARTREASONID = lstviewUnpFSPReason.FocusedItem.SubItems(1).Text
            Call LoadPartScanChecker(True)
        Catch ex As WebException
            mode = False
            timer.Enabled = True
            modScanOffline = True
            If offline1stTFlag Then
                If MessageBox.Show("Connection is down. Continue scan with offline mode?", _
                                   "Unpack - Part Scan Offline Mode", MessageBoxButtons.YesNo, _
                                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    offline1stTFlag = False
                    Call PartScanOffline(True)
                Else
                    Cursor.Current = Cursors.Default
                    txtUnpFSPartNo.Focus()
                    txtUnpFSPartNo.SelectAll()
                    Exit Sub
                End If
            Else
                Call PartScanOffline(True)
            End If
        Catch ex As Exception
            MsgBox("Part scanned failed.", MsgBoxStyle.Critical, "Part Force Scan")
            Cursor.Current = Cursors.Default
            txtUnpFSPartNo.Focus()
            txtUnpFSPartNo.SelectAll()
        End Try
    End Sub

#End Region

#Region ". Unpack Details ."

    Private Sub LoadUnpackDetails()
        Me.Text = strOnlineTitle + " - View"
        lblHeaderUnpackVwDet.Text = IIf(Not String.IsNullOrEmpty(MODD.MODULE_NO), "Module No : " _
                                        & MODD.MODULE_NO, String.Empty)
        Call PopulateUnpackPendingDetails(MODD.MODULE_NO)
        Call PopulateUnpackScannedDetails(MODD.MODULE_NO)
        lblRCITotalScan.Text = cntPending.ToString
        lblRCIS2TotalScan.Text = cntScanned.ToString
    End Sub

    Private Sub btnScanDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDetails.Click
        Call LoadUnpackDetails()
        bringPanelToFront(pnlUnpackViewDet, pnlUnpackScanModule)
    End Sub

    Private Sub btnUnpackViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpackViewDet.Click
        Call LoadUnpackDetails()
        bringPanelToFront(pnlUnpackViewDet, pnlUnpackScanPart)
    End Sub

    Private Sub PopulateUnpackPendingDetails(ByVal MODULE_NO As String)
        Dim lstViewItem As ListViewItem
        Dim dt As DataTable = New DataTable

        lstUnpackModule.Items.Clear()
        If Not String.IsNullOrEmpty(MODULE_NO) Then
            dt = CeDataAdapter("SELECT COALESCE(SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)+'-'+" & _
                               "PART_NO_SFX, SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)) AS PART_NO," & _
                               "PART_SEQ_NO, QUANTITY_BOX, BRANCH_NO, ROBB_MODULE_NO " & _
                               "FROM [" & UNPACK_PENDING & "] " & _
                               "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                               "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                               "ORDER BY PART_NO")
            For i As Integer = 0 To dt.Rows.Count - 1
                lstViewItem = New ListViewItem
                lstViewItem.Text = i + 1
                lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_NO").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_SEQ_NO").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("QUANTITY_BOX").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("BRANCH_NO").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("ROBB_MODULE_NO").ToString())
                lstUnpackModule.Items.Add(lstViewItem)
            Next
            cntPending = dt.Rows.Count
        End If
    End Sub

    Private Sub PopulateUnpackScannedDetails(ByVal MODULE_NO As String)
        Dim dt As DataTable = New DataTable

        If Not String.IsNullOrEmpty(MODULE_NO) Then
            dt = CeDataAdapter("SELECT COALESCE(SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)+'-'+" & _
                               "PXP_PART_NO_SFX, SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)) AS PART_NO," & _
                               "PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO " & _
                               "FROM [" & UNPACK_INTERFACE & "] " & _
                               "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                               "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                               "GROUP BY PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO " & _
                               "ORDER BY PART_NO")
            Me.SetListViewItem(dt)
            cntScanned = dt.Rows.Count
        End If
    End Sub

    Private Sub SetListViewItem(ByRef [dt] As DataTable)
        Dim lstViewItem As ListViewItem
        If lstViewRCISummary.InvokeRequired Then
            Dim lv As New ListViewReturningVoidDelegate(AddressOf SetListViewItem)
            Me.Invoke(lv, New Object() {[dt]})
        Else
            lstViewRCISummary.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                lstViewItem = New ListViewItem
                lstViewItem.Text = i + 1
                lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_NO").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("PXP_PART_SEQ_NO").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("QTY_BOX").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_BRANCH_NO").ToString())
                lstViewItem.SubItems.Add(dt.Rows(i).Item("ROBB_MODULE_NO").ToString())
                lstViewRCISummary.Items.Add(lstViewItem)
            Next
        End If
    End Sub

#End Region

#Region ". Batch Helper ."

    Private Sub InitUnpackBatch()
        curr_BATCH = GetBatchID("UNPACK", "2")
    End Sub

    Private Sub UpdateBatch()
        Dim currentNo As String = DateTime.ParseExact(ws_dcsClient.getTime, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss")
        If Not ExecuteSQL(String.Format("UPDATE [{0}] SET CURRENT_NO = {1} WHERE CATEGORY = 'UNPACK'", TblBatch, SQLQuote(currentNo))) Then
            Throw New Exception("Failed to Update Batch.")
        End If
        prev_BATCH = curr_BATCH
        Call InitUnpackBatch()
    End Sub

#End Region

#Region ". Offline Module Scan ."

    Private Sub LoadAbnModuleScan(Optional ByVal OnFirstLoad As Boolean = Nothing)
        Me.Text = strOfflineTitle
        txtAbnModNo.Focus()
        txtAbnModNo.Text = String.Empty
        lblAbnModNo.Text = String.Empty
        lblAbnOrderNo.Text = String.Empty
        lblAbnMsgCode.BackColor = Color.LimeGreen
        lblAbnMsgCode.Text = "Scan.."
        lblAbnMsgDesc.Text = String.Empty
        If OnFirstLoad Then
            lblAbnTotalScan.Text = String.Empty
        Else
            lblAbnTotalScan.Text = Counter("AbnPartScanned")
        End If
    End Sub

    Private Sub txtAbnModNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAbnModNo.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o f f l i n e
                If Not String.IsNullOrEmpty(txtAbnModNo.Text) Then
                    Call IsModuleReadable(txtAbnModNo.Text)
                    Call LoadAbnModuleChecker()
                Else
                    Throw New Exception("Failed to read the Module QR.")
                End If
            Catch ex As Exception
                MsgBox("Module scanned failed.", MsgBoxStyle.Critical, "Unpack - Abnormal Module Scan")
                Cursor.Current = Cursors.Default
                txtAbnModNo.Focus()
                txtAbnModNo.SelectAll()
            End Try
        End If
    End Sub

    Private Sub LoadAbnModuleChecker(Optional ByVal IsForceScan As Boolean = Nothing)
        Cursor.Current = Cursors.WaitCursor
        msgCode = "Success"
        msgDesc = "Succesfully Updated"
        SetPanelForResult("PanelAbnModOK", msgCode, msgDesc, IsForceScan)
    End Sub

#End Region

#Region ". Offline Module Force Scan ."

    Private Sub LoadAbnModuleForceScan()
        Me.Text = strOfflineTitle
        'Call PopulateForceScan("Abn_Module")
        Call GetAbnReasonCode(lstViewAbnFSMod)
        txtFSAbnModNo.Text = String.Empty
        txtFSAbnOrdNo.Text = String.Empty
    End Sub

    Private Sub btnAbnFScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnFScan.Click
        txtFSAbnModNo.Focus()
        Call LoadAbnModuleForceScan()
        bringPanelToFront(pnlUnpackAbnFscanModule, pnlUnpackAbnScanModule)
    End Sub

    Private Sub txtFSAbnModNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSAbnModNo.TextChanged
        If Not String.IsNullOrEmpty(txtFSAbnModNo.Text) Then
            If String.IsNullOrEmpty(txtFSAbnOrdNo.Text) Then
                btnAbnFScanSave.Enabled = False
            Else
                btnAbnFScanSave.Enabled = True
            End If
        Else
            btnAbnFScanSave.Enabled = False
        End If
    End Sub

    Private Sub txtFSAbnOrdNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFSAbnOrdNo.TextChanged
        If Not String.IsNullOrEmpty(txtFSAbnOrdNo.Text) Then
            If String.IsNullOrEmpty(txtFSAbnModNo.Text) Then
                btnAbnFScanSave.Enabled = False
            Else
                btnAbnFScanSave.Enabled = True
            End If
        Else
            btnAbnFScanSave.Enabled = False
        End If
    End Sub

    Private Sub btnAbnFScanSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnFScanSave.Click
        Dim cnt As Integer = 0
        Dim listViews As ListViewItem = New ListViewItem()

        For i As Integer = 0 To lstViewAbnFSMod.Items.Count - 1
            listViews = lstViewAbnFSMod.Items(i)
            If Not listViews.Selected Then
                cnt = cnt + 1
            End If
            If cnt = lstViewAbnFSMod.Items.Count Then
                MsgBox("Failed to save. Reason is required.", MsgBoxStyle.Critical, "Abnormal Module Force Scan")
                Exit Sub
            End If
        Next

        If String.IsNullOrEmpty(txtFSAbnModNo.Text) Then
            MsgBox("Module No is required", MsgBoxStyle.Critical, "Abnormal Module Force Scan")
            txtFSAbnModNo.Focus()
            txtFSAbnModNo.SelectAll()
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtFSAbnOrdNo.Text) Then
            MsgBox("Order No is required", MsgBoxStyle.Critical, "Abnormal Module Force Scan")
            txtFSAbnOrdNo.Focus()
            txtFSAbnOrdNo.SelectAll()
            Exit Sub
        End If

        MODD.MODULE_NO = txtFSAbnModNo.Text.Trim
        MODD.ORDER_NO = txtFSAbnOrdNo.Text.Trim
        MODD.REASONID = lstViewAbnFSMod.FocusedItem.SubItems(1).Text
        MODD.PILLING_NO = "0"
        MODD.GROSS_WEIGHT = 0D
        Call LoadAbnModuleChecker(True)
    End Sub

#End Region

#Region ". Offline Part Scan ."

    Private Sub LoadAbnPartScan()
        Me.Text = strOfflineTitle
        lblAbnSPartModNo.Text = MODD.MODULE_NO
        txtAbnSPartKanban.Focus()
        txtAbnSPartKanban.Text = String.Empty
        lblAbnScanPartNo.Text = String.Empty
        lblAbnSPartSeqNo.Text = String.Empty
        Label3.Text = String.Empty
        Label4.Text = String.Empty
        lblAbnSPartMsgC.BackColor = Color.LimeGreen
        lblAbnSPartMsgC.Text = "Scan.."
        lblAbnDesc.Text = String.Empty
        lblAbnScanPartTotal.Text = Counter("AbnPartScanned")
    End Sub

    Private Sub txtAbnSPartKanban_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAbnSPartKanban.KeyDown
        If Not e Is Nothing And e.KeyCode = Keys.Enter Then
            Try
                'o f f l i n e
                If Not String.IsNullOrEmpty(txtAbnSPartKanban.Text) Then
                    If Not IsPartQRReadable(txtAbnSPartKanban.Text, "Abn") Then
                        Exit Sub
                    End If
                    Call LoadAbnPartChecker()
                Else
                    Throw New Exception("Failed to read the Part QR.")
                End If
            Catch ex As Exception
                MsgBox("Part scanned failed.", MsgBoxStyle.Critical, "Unpack - Abnormal Part Scan")
                Cursor.Current = Cursors.Default
                txtAbnSPartKanban.Focus()
                txtAbnSPartKanban.SelectAll()
            End Try
        End If
    End Sub

    Private Sub LoadAbnPartChecker(Optional ByVal IsForceScan As Boolean = Nothing)
        Cursor.Current = Cursors.WaitCursor
        Call GetRobbModuleNo(IsForceScan, IIf(isMakeUp.Checked, txtUnpAbnFSRobModNo.Text, String.Empty))
        If UnpackTableReader(UNPACK_INTERFACE, _
                             MODD.MODULE_NO, _
                             PART.PARTS_NO, _
                             PART.PART_SEQ_NUMBER, _
                             PART.PART_BRANCH_NUMBER, _
                             org_ID, _
                             IIf(Not String.IsNullOrEmpty(PART.ROBB_MODD_NO), PART.ROBB_MODD_NO, "N")) = 0 Then
            Call AddToUnpackInterface("Y", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), IsForceScan, MODD, PART)
            Call UpdateUnpackPendingDetails(MODD.MODULE_NO, PART.PARTS_NO, PART.PART_SEQ_NUMBER, PART.PART_BRANCH_NUMBER)
            msgCode = "Success"
            msgDesc = "Succesfully Updated"
            SetPanelForResult("PanelAbnPartOK", msgCode, msgDesc, IsForceScan)
        Else
            msgCode = "Failed"
            msgDesc = "Duplicate Part No."
            SetPanelForResult("PanelAbnPartError", msgCode, msgDesc, IsForceScan)
        End If
    End Sub

#End Region

#Region ". Offline Part Force Scan ."

    Private Sub LoadAbnPartForceScan()
        Me.Text = strOfflineTitle
        lblUnpAbnFSPartModNo.Text = MODD.MODULE_NO
        Call GetAbnReasonCode(lstViewUnpAbnFSPartReason)
        'Call PopulateForceScan("Abn_Part")
        txtUnpAbnFSPartNo.Text = String.Empty
        txtUnpAbnFSPartBranchNo.Text = String.Empty
        txtUnpAbnFSPartSeqNo.Text = String.Empty
        isMakeUp.Checked = False
        lblRobModuleNo.Visible = False
        txtUnpAbnFSRobModNo.Visible = False
    End Sub

    Private Sub btnAbnFSPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnFSPart.Click
        Call LoadAbnPartForceScan()
        bringPanelToFront(pnlUnpackAbnFscanPart, pnlUnpackAbnScanPart)
    End Sub

    Private Sub txtUnpAbnFSPartNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnpAbnFSPartNo.TextChanged
        If Not String.IsNullOrEmpty(txtUnpAbnFSPartNo.Text) Then
            If String.IsNullOrEmpty(txtUnpAbnFSPartBranchNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpAbnFSPartSeqNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf isMakeUp.Checked And String.IsNullOrEmpty(txtUnpAbnFSRobModNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            Else
                btnUnpAbnFSPartSave.Enabled = True
            End If
        Else
            btnUnpAbnFSPartSave.Enabled = False
        End If
    End Sub

    Private Sub txtUnpAbnFSPartBranchNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnpAbnFSPartBranchNo.TextChanged
        If Not String.IsNullOrEmpty(txtUnpAbnFSPartBranchNo.Text) Then
            If String.IsNullOrEmpty(txtUnpAbnFSPartNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpAbnFSPartSeqNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf isMakeUp.Checked And String.IsNullOrEmpty(txtUnpAbnFSRobModNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            Else
                btnUnpAbnFSPartSave.Enabled = True
            End If
        Else
            btnUnpAbnFSPartSave.Enabled = False
        End If
    End Sub

    Private Sub txtUnpAbnFSRobModNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnpAbnFSRobModNo.TextChanged
        If Not String.IsNullOrEmpty(txtUnpAbnFSRobModNo.Text) Then
            If String.IsNullOrEmpty(txtUnpAbnFSPartNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpAbnFSPartSeqNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpAbnFSPartBranchNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            Else
                btnUnpAbnFSPartSave.Enabled = True
            End If
        Else
            btnUnpAbnFSPartSave.Enabled = False
        End If
    End Sub

    Private Sub txtUnpAbnFSPartSeqNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnpAbnFSPartSeqNo.TextChanged
        If Not String.IsNullOrEmpty(txtUnpAbnFSPartSeqNo.Text) Then
            If String.IsNullOrEmpty(txtUnpAbnFSPartNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf String.IsNullOrEmpty(txtUnpAbnFSPartBranchNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            ElseIf isMakeUp.Checked And String.IsNullOrEmpty(txtUnpAbnFSRobModNo.Text) Then
                btnUnpAbnFSPartSave.Enabled = False
            Else
                btnUnpAbnFSPartSave.Enabled = True
            End If
        Else
            btnUnpAbnFSPartSave.Enabled = False
        End If
    End Sub

    Private Sub btnUnpAbnFSPartSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpAbnFSPartSave.Click
        Dim cnt As Integer = 0
        Dim listViews As ListViewItem = New ListViewItem()

        For i As Integer = 0 To lstViewUnpAbnFSPartReason.Items.Count - 1
            listViews = lstViewUnpAbnFSPartReason.Items(i)
            If Not listViews.Selected Then
                cnt = cnt + 1
            End If
            If cnt = lstViewUnpAbnFSPartReason.Items.Count Then
                MsgBox("Failed to save. Reason is required.", MsgBoxStyle.Critical, "Abnormal Part Force Scan")
                Exit Sub
            End If
        Next

        If String.IsNullOrEmpty(txtUnpAbnFSPartNo.Text) Then
            MsgBox("Part No is required", MsgBoxStyle.Critical, "Abnormal Part Force Scan")
            txtUnpAbnFSPartNo.Focus()
            txtUnpAbnFSPartNo.SelectAll()
            Exit Sub
        ElseIf txtUnpAbnFSPartNo.Text.Length <> 14 Then
            MsgBox("Failed to save. Invalid Part No Format", MsgBoxStyle.Critical, "Abnormal Part Force Scan")
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtUnpAbnFSPartSeqNo.Text) Then
            MsgBox("Seq No is required", MsgBoxStyle.Critical, "Abnormal Part Force Scan")
            txtUnpAbnFSPartSeqNo.Focus()
            txtUnpAbnFSPartSeqNo.SelectAll()
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtUnpAbnFSPartBranchNo.Text) Then
            MsgBox("Branch No is required", MsgBoxStyle.Critical, "Abnormal Part Force Scan")
            txtUnpAbnFSPartBranchNo.Focus()
            txtUnpAbnFSPartBranchNo.SelectAll()
            Exit Sub
        ElseIf isMakeUp.Checked And String.IsNullOrEmpty(txtUnpAbnFSRobModNo.Text) Then
            MsgBox("Robbing Module No is required", MsgBoxStyle.Critical, "Abnormal Part Force Scan")
            txtUnpAbnFSRobModNo.Focus()
            txtUnpAbnFSRobModNo.SelectAll()
            Exit Sub
        End If

        PART.PARTS_NO = txtUnpAbnFSPartNo.Text.Replace("-", "").Substring(0, 10)
        PART.PART_NO_SFX = txtUnpAbnFSPartNo.Text.Replace("-", "").Substring(10, 2)
        PART.PART_SEQ_NUMBER = txtUnpAbnFSPartSeqNo.Text
        PART.PART_BRANCH_NUMBER = txtUnpAbnFSPartBranchNo.Text
        PART.PARTREASONID = lstViewUnpAbnFSPartReason.FocusedItem.SubItems(1).Text
        Call LoadAbnPartChecker(True)
    End Sub

#End Region

#Region ". Offline Populate Post/Delete/Details ."

    Private Function GetAbnPostedDetails(ByVal IsMain As Boolean)
        Dim dt As DataTable = New DataTable
        If (IsMain) Then
            dt = getData("SELECT COALESCE(SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)+'-'+" & _
                     "PXP_PART_NO_SFX, SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)) AS PART_NO," & _
                     "PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO, MODULE_NO " & _
                     "FROM [" & UNPACK_INTERFACE & "] " & _
                     "WHERE ON_OFF_LINE_FLAG = 'Y' " & _
                     "AND PROCESS_FLAG IS NULL OR PROCESS_FLAG = '' OR PROCESS_FLAG != 'POSTED' " & _
                     "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                     "GROUP BY PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO, MODULE_NO " & _
                     "ORDER BY MODULE_NO, PART_NO, PXP_PART_SEQ_NO")
        Else
            dt = getData("SELECT COALESCE(SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)+'-'+" & _
                     "PXP_PART_NO_SFX, SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)) AS PART_NO," & _
                     "PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO, MODULE_NO " & _
                     "FROM [" & UNPACK_INTERFACE & "] " & _
                     "WHERE MODULE_NO = " & SQLQuote(MODD.MODULE_NO) & " " & _
                     "AND ON_OFF_LINE_FLAG = 'Y' " & _
                     "AND PROCESS_FLAG IS NULL OR PROCESS_FLAG = '' OR PROCESS_FLAG != 'POSTED' " & _
                     "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                     "GROUP BY PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO, MODULE_NO " & _
                     "ORDER BY MODULE_NO, PART_NO, PXP_PART_SEQ_NO")
        End If

        Return dt
    End Function

    Private Function GetAbnDeleteDetails()
        Dim dt As DataTable = New DataTable

        dt = getData("SELECT COALESCE(SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)+'-'+" & _
                     "PXP_PART_NO_SFX, SUBSTRING(PART_NO, 1, 5)+'-'+SUBSTRING(PART_NO, 6, 5)) AS PART_NO," & _
                     "PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO, MODULE_NO " & _
                     "FROM [" & UNPACK_INTERFACE & "] " & _
                     "WHERE ON_OFF_LINE_FLAG = 'Y' " & _
                     "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                     "GROUP BY PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, PART_BRANCH_NO, ROBB_MODULE_NO, MODULE_NO " & _
                     "ORDER BY MODULE_NO, PART_NO, PXP_PART_SEQ_NO")
        Return dt
    End Function

#End Region

#Region ". Offline Unpack Details ."

    Private Sub LoadAbnUnpackDetails(ByVal IsMain As Boolean)
        Me.Text = strOfflineTitle + " - View"

        If (IsMain) Then
            lblUnpAbnModNoDet.Text = String.Empty
            lblUnpAbnModNoDet.Visible = False
        Else
            lblUnpAbnModNoDet.Visible = True
            lblUnpAbnModNoDet.Text = IIf(Not String.IsNullOrEmpty(MODD.MODULE_NO), "Module No : " & MODD.MODULE_NO, String.Empty)
        End If
        Call PopulateAbnUnpackDetails(IsMain)
        If IsMain Then
            lblTotalUnpAbnModScan.Text = "Total Records: " & cntAbnTotal.ToString
        Else
            lblTotalUnpAbnModScan.Text = "Total Records: " & cntAbnPartScanned.ToString
        End If
    End Sub

    Private Sub btnAbnScanViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnScanViewDet.Click
        Call LoadAbnUnpackDetails(False)
        bringPanelToFront(pnlUnpackAbnViewDet, pnlUnpackAbnScanModule)
    End Sub

    Private Sub btnAbnSPartViewDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbnSPartViewDet.Click
        Call LoadAbnUnpackDetails(False)
        showPnlModule = True
        bringPanelToFront(pnlUnpackAbnViewDet, pnlUnpackAbnScanPart)
    End Sub

    Private Sub PopulateAbnUnpackDetails(ByVal IsMain As Boolean)
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim dt As DataTable = New DataTable()

        lstViewUnpAbnModDet.Items.Clear()
        dt = GetAbnPostedDetails(IsMain)
        For i As Integer = 0 To dt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(dt.Rows(i).Item("MODULE_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PXP_PART_SEQ_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("QTY_BOX").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_BRANCH_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("ROBB_MODULE_NO").ToString())
            lstViewUnpAbnModDet.Items.Add(lstViewItem)
        Next
        cntAbnPartScanned = dt.Rows.Count
    End Sub

#End Region

#Region ". Offline Delete & Posting ."

    Private Sub LoadAbnUnpackDelete()
        Me.Text = strOfflineTitle + " - Delete"
        Call PopulateAbnDelete()
        Label6.Text = "Total Records: " & cntAbnDelete.ToString
    End Sub

    Private Sub PopulateAbnDelete()
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim dt As DataTable = New DataTable()

        ListView2.Items.Clear()
        dt = GetAbnDeleteDetails()
        For i As Integer = 0 To dt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(dt.Rows(i).Item("MODULE_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PXP_PART_SEQ_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("QTY_BOX").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_BRANCH_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("ROBB_MODULE_NO").ToString())
            ListView2.Items.Add(lstViewItem)
        Next
        cntAbnDelete = dt.Rows.Count
    End Sub

    Private Sub btnDeleteUnpack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteUnpack.Click
        Dim contDelete As System.Windows.Forms.DialogResult = Nothing

        Try
            contDelete = MessageBox.Show("Confirm to delete all posted record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If contDelete = Windows.Forms.DialogResult.Yes Then
                Cursor.Current = Cursors.WaitCursor
                Call DeleteAbnRecords()
                Call LoadAbnUnpackDelete()
                Cursor.Current = Cursors.Default
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Delete operation failed.", MsgBoxStyle.Critical, "Unpack - Abnormal Delete")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub DeleteAbnRecords()
        If Not ExecuteSQL("DELETE FROM [" & UNPACK_INTERFACE & "] " & _
                          "WHERE RCV_INTERFACE_BATCH_ID = " & SQLQuote(IIf(Not String.IsNullOrEmpty(prev_BATCH), prev_BATCH, curr_BATCH)) & " " & _
                          "AND ON_OFF_LINE_FLAG = 'Y'") Then
            Throw New Exception("Failed to delete records.")
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub LoadAbnUnpackPost()
        Me.Text = strOfflineTitle + " - Posting"
        Call PopulateAbnPost()
        lblUnpAbnScanPartTotalDet.Text = "Total Pending Posting Records: " & cntAbnTotal.ToString
    End Sub

    Private Sub PopulateAbnPost()
        Dim lstViewItem As ListViewItem = New ListViewItem()
        Dim dt As DataTable = New DataTable()

        lstViewUnpAbnPartDet.Items.Clear()
        dt = GetAbnPostedDetails(True)
        For i As Integer = 0 To dt.Rows.Count - 1
            lstViewItem = New ListViewItem
            lstViewItem.Text = i + 1
            lstViewItem.SubItems.Add(dt.Rows(i).Item("MODULE_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PXP_PART_SEQ_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("QTY_BOX").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("PART_BRANCH_NO").ToString())
            lstViewItem.SubItems.Add(dt.Rows(i).Item("ROBB_MODULE_NO").ToString())
            lstViewUnpAbnPartDet.Items.Add(lstViewItem)
        Next
        cntAbnTotal = dt.Rows.Count
    End Sub

    Private Sub btnSubmitPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitPosting.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            'p o s t
            If ws_dcsClient.isConnected Then
                If ws_dcsClient.isOracleConnected Then '***
                    If (ValidateAbnRecords()) Then
                        msgCode = ws_inventoryClient.processInventoryConsumption(curr_BATCH, "UNPACK", "205", org_ID, _
                                                     Nothing, Nothing, Nothing, Nothing, Nothing, msgDesc)
                        If msgCode = "OK" Then
                            SetPanelForResult("PanelAbnPartPost", msgCode, msgDesc)
                        Else
                            Throw New Exception("Failed to Post Part. - " & msgDesc)
                        End If
                    Else
                        MsgBox("No records to post.", MsgBoxStyle.Critical, "Unpack - Abnormal Post")
                    End If
                    Cursor.Current = Cursors.Default
                End If
            End If
        Catch ex As WebException
            MsgBox("No connection to post.", MsgBoxStyle.Critical, "Unpack - Abnormal Post")
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("Posting failed.", MsgBoxStyle.Critical, "Unpack - Abnormal Post")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ValidateAbnRecords() As Boolean
        Dim dt As DataTable = New DataTable()
        Dim forceScanModuleReasonID As String = String.Empty
        Dim forceScanReasonID As String = String.Empty
        Call InitWebServices()

        dt = getData("SELECT * FROM [" & UNPACK_INTERFACE & "] " & _
                     "WHERE RCV_INTERFACE_BATCH_ID = " & SQLQuote(curr_BATCH) & " " & _
                     "AND ON_OFF_LINE_FLAG = 'Y'")
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If ws_dcsClient.isOracleConnected Then '***
                    If IsDBNull(dt.Rows(i).Item("FORCE_PXP_REASON_ID")) Then
                        forceScanReasonID = Nothing
                    Else
                        forceScanReasonID = dt.Rows(i).Item("FORCE_PXP_REASON_ID").ToString()
                    End If

                    If IsDBNull(dt.Rows(i).Item("FORCE_MODULE_REASON_ID")) Then
                        forceScanModuleReasonID = Nothing
                    Else
                        forceScanModuleReasonID = dt.Rows(i).Item("FORCE_MODULE_REASON_ID").ToString()
                    End If

                    msgCode = ws_validationClient.processValidation(curr_BATCH, gScannerID, "UNPACK", "204", Nothing, Nothing, MODD.PILLING_NO, MODD.GROSS_WEIGHT.ToString, Nothing, Nothing, _
                                   dt.Rows(i).Item("FORCE_MODULE_STATUS").ToString, forceScanModuleReasonID, _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("MODULE_ID").ToString), dt.Rows(i).Item("MODULE_ID").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("MODULE_NO").ToString), dt.Rows(i).Item("MODULE_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("PART_NO").ToString), dt.Rows(i).Item("PART_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("PXP_PART_NO_SFX").ToString), dt.Rows(i).Item("PXP_PART_NO_SFX").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("PXP_PART_SEQ_NO").ToString), dt.Rows(i).Item("PXP_PART_SEQ_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("QTY_BOX").ToString), dt.Rows(i).Item("QTY_BOX").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("MANUFACTURE_CODE").ToString), dt.Rows(i).Item("MANUFACTURE_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("SUPPLIER_CODE").ToString), dt.Rows(i).Item("SUPPLIER_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("SUPPLIER_PLANT_CODE").ToString), dt.Rows(i).Item("SUPPLIER_PLANT_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("SUPPLIER_SHIPPING_DOCK").ToString), dt.Rows(i).Item("SUPPLIER_SHIPPING_DOCK").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("BEFORE_PACKING_ROUTING").ToString), dt.Rows(i).Item("BEFORE_PACKING_ROUTING").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("RECEIVING_COMPANY_CODE").ToString), dt.Rows(i).Item("RECEIVING_COMPANY_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("RECEIVING_PLANT_CODE").ToString), dt.Rows(i).Item("RECEIVING_PLANT_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("RECEIVING_DOCK_CODE").ToString), dt.Rows(i).Item("RECEIVING_DOCK_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("PACKING_ROUTING_CODE").ToString), dt.Rows(i).Item("PACKING_ROUTING_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("GRANTER_CODE").ToString), dt.Rows(i).Item("GRANTER_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("ORDER_TYPE").ToString), dt.Rows(i).Item("ORDER_TYPE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("KANBAN_CLASSIFICATION").ToString), dt.Rows(i).Item("KANBAN_CLASSIFICATION").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("MROS").ToString), dt.Rows(i).Item("MROS").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("ORDER_NO").ToString), dt.Rows(i).Item("ORDER_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("DELIVERY_CODE").ToString), dt.Rows(i).Item("DELIVERY_CODE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("DELIVERY_NO").ToString), dt.Rows(i).Item("DELIVERY_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("BACK_NUMBER").ToString), dt.Rows(i).Item("BACK_NUMBER").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("RUNOUT_FLAG").ToString), dt.Rows(i).Item("RUNOUT_FLAG").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("BOX_TYPE").ToString), dt.Rows(i).Item("BOX_TYPE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("BRANCH_NO").ToString), dt.Rows(i).Item("BRANCH_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("ADDRESS").ToString), dt.Rows(i).Item("ADDRESS").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("PACKING_DATE").ToString), dt.Rows(i).Item("PACKING_DATE").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("KATASHIKI_JERSEY_NO").ToString), dt.Rows(i).Item("KATASHIKI_JERSEY_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("LOT_NO").ToString), dt.Rows(i).Item("LOT_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("MODULE_CATEGORY").ToString), dt.Rows(i).Item("MODULE_CATEGORY").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("PART_BRANCH_NO").ToString), dt.Rows(i).Item("PART_BRANCH_NO").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("DUMMY").ToString), dt.Rows(i).Item("DUMMY").ToString, Nothing), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("VERSION_NO").ToString), dt.Rows(i).Item("VERSION_NO").ToString, Nothing), _
                                   Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, SelectOrgIdForPosting(dt.Rows(i).Item("ORG_ID").ToString), _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("DELIVERY_DATE").ToString), dt.Rows(i).Item("DELIVERY_DATE").ToString, Nothing), _
                                   gScannerID, GetServerTime, gScannerID, GetServerTime, Nothing, Nothing, Nothing, Nothing, gScannerID, "Y", gScannerID, GetServerTime, Nothing, Nothing, _
                                   IIf(Not String.IsNullOrEmpty(dt.Rows(i).Item("FORCE_PXP_STATUS").ToString), dt.Rows(i).Item("FORCE_PXP_STATUS").ToString, Nothing), _
                                   forceScanReasonID, Nothing, Nothing, Nothing, Nothing, msgDesc)
                    If msgCode = "OK" Then
                        Cursor.Current = Cursors.Default
                        Call UpdatePostStatus(dt.Rows(i).Item("MODULE_NO").ToString, _
                                              dt.Rows(i).Item("PART_NO").ToString, _
                                              dt.Rows(i).Item("PXP_PART_SEQ_NO").ToString, _
                                              dt.Rows(i).Item("PART_BRANCH_NO").ToString, _
                                              dt.Rows(i).Item("ROBB_MODULE_NO").ToString)
                    End If
                End If
            Next
            Return True
        Else
            Return False
        End If
    End Function

    Function SelectOrgIdForPosting(ByVal TempOrgID As String) As String
        Return IIf(Not String.IsNullOrEmpty(TempOrgID), IIf(Not TempOrgID = "0", TempOrgID, org_ID), org_ID)
    End Function

#End Region

#Region ". Abnormal Login ."

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
                    Call LoadAbnUnpackPost()
                    lblUsername.Text = String.Format("USER NAME: {0}", txtUsername.Text)
                    bringPanelToFront(pnlUnpackPosting, pnlLogin)
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
        bringPanelToFront(pnlUnpackAbn, pnlLogin)
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

#Region ". Global Helper ."

    Function GetServerTime() As String
        If mode = True Then
            Return ws_dcsClient.getTime
        ElseIf mode = False Then
            Return DateTime.Now.ToString
        End If
        Return Nothing
    End Function

    Private Sub ChangeProcess()
        Cursor.Current = Cursors.Default
        If showError = True Then
            showError = False
            If modScanOffline Then
                showError = True
                Exit Sub
            Else
                If MessageBox.Show("Connection is down. Change to Abnormal Process?", "Unpack - Offline", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    timer.Enabled = False
                    timer.Dispose()
                    showError = True
                    bringPanelToFront(pnlUnpackMain, pnlUnpackScanModule)
                    Exit Sub
                Else
                    showError = True
                    'txtModuleQR.Focus()
                    'txtModuleQR.SelectAll()
                End If
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Try
            If mode = False Then
                If ws_dcsClient.isConnected Then
                    If ws_dcsClient.isOracleConnected Then
                        mode = True
                        offline1stTFlag = True
                        modScanOffline = False
                        timer.Enabled = False
                        timer.Dispose()
                        MsgBox("Connection is back. Please Try Again to Scan.", MsgBoxStyle.Information, Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            Call ChangeProcess()
        End Try
    End Sub

    Private Sub SetPanelForResult(ByVal Panel As String, ByVal msg As String, ByVal desc As String, _
                                  Optional ByVal IsForceScan As Boolean = Nothing)
        Cursor.Current = Cursors.Default
        If Panel = "PanelModOK" Then
            lblMsgOkScanMod.BackColor = Color.LimeGreen
            lblMsgOkScanMod.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            lblSuccessScanMod.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            btnScanDetails.Enabled = True
            'lblTotalScanned.Text = Counter("Scanned")
            txtModuleNo.Text = MODD.MODULE_NO
            txtOrderNo.Text = MODD.ORDER_NO
            Call LoadPartScan()
            If IsForceScan Then
                bringPanelToFront(pnlUnpackScanPart, pnlUnpackFScan)
            Else
                bringPanelToFront(pnlUnpackScanPart, pnlUnpackScanModule)
            End If
        ElseIf Panel = "PanelModError" Then
            lblMsgOkScanMod.BackColor = Color.Red
            lblMsgOkScanMod.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            lblSuccessScanMod.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            btnScanDetails.Enabled = False
            lblTotalScanned.Text = 0
            txtModuleNo.Text = Nothing
            txtOrderNo.Text = Nothing
            txtModuleQR.Focus()
            txtModuleQR.SelectAll()
            If IsForceScan Then
                bringPanelToFront(pnlUnpackScanModule, pnlUnpackFScan)
            End If
        ElseIf Panel = "PanelPartOK" Then
            lblPartSuccess.BackColor = Color.LimeGreen
            lblPartSuccess.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label2.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            lblPartNo.Text = PART.PARTS_NO
            lblPartSeqNo.Text = PART.PART_SEQ_NUMBER
            lblPartQty.Text = PART.QTY_BOX
            lblPartBranchNo.Text = PART.PART_BRANCH_NUMBER
            lblPartTotalScan.Text = Counter("Scanned")
            txtKanbanQRPart.Focus()
            txtKanbanQRPart.SelectAll()
            If Counter("Pending") = 0 Then
                MsgBox(successMsgPart, MsgBoxStyle.Information, Me.Text)
                Call LoadModuleScan()
                If IsForceScan Then
                    bringPanelToFront(pnlUnpackScanModule, pnlUnpackFScanPart)
                Else
                    bringPanelToFront(pnlUnpackScanModule, pnlUnpackScanPart)
                End If
            End If
            If IsForceScan Then
                bringPanelToFront(pnlUnpackScanPart, pnlUnpackFScanPart)
            End If
        ElseIf Panel = "PanelPartError" Then
            lblPartSuccess.BackColor = Color.Red
            lblPartSuccess.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label2.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            lblPartNo.Text = Nothing
            lblPartSeqNo.Text = Nothing
            lblPartQty.Text = Nothing
            lblPartBranchNo.Text = Nothing
            txtKanbanQRPart.Focus()
            txtKanbanQRPart.SelectAll()
            If IsForceScan Then
                bringPanelToFront(pnlUnpackScanPart, pnlUnpackFScanPart)
            End If
        ElseIf Panel = "PanelPartPost" Then
            lblPartSuccess.BackColor = Color.LimeGreen
            lblPartSuccess.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            Label2.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            lblPartNo.Text = PART.PARTS_NO
            lblPartSeqNo.Text = PART.PART_SEQ_NUMBER
            lblPartQty.Text = PART.QTY_BOX
            lblPartBranchNo.Text = PART.PART_BRANCH_NUMBER
            lblPartTotalScan.Text = Counter("Scanned")
            MsgBox(successMsgPart, MsgBoxStyle.Information, Me.Text)
            Call LoadModuleScan()
            If IsForceScan Then
                bringPanelToFront(pnlUnpackScanModule, pnlUnpackFScanPart)
            Else
                bringPanelToFront(pnlUnpackScanModule, pnlUnpackScanPart)
            End If
            Call UpdateBatch()
        ElseIf Panel = "PanelAbnModOK" Then
            lblAbnMsgCode.BackColor = Color.LimeGreen
            lblAbnMsgCode.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            lblAbnMsgDesc.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            lblAbnTotalScan.Text = Counter("AbnPartScanned")
            lblAbnModNo.Text = MODD.MODULE_NO
            lblAbnOrderNo.Text = MODD.ORDER_NO
            Call LoadAbnPartScan()
            If IsForceScan Then
                bringPanelToFront(pnlUnpackAbnScanPart, pnlUnpackAbnFscanModule)
            Else
                bringPanelToFront(pnlUnpackAbnScanPart, pnlUnpackAbnScanModule)
            End If
        ElseIf Panel = "PanelAbnPartOK" Then
            lblAbnSPartMsgC.BackColor = Color.LimeGreen
            lblAbnSPartMsgC.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            lblAbnDesc.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            lblAbnScanPartNo.Text = PART.PARTS_NO
            lblAbnSPartSeqNo.Text = PART.PART_SEQ_NUMBER
            Label3.Text = PART.QTY_BOX
            Label4.Text = PART.PART_BRANCH_NUMBER
            lblAbnScanPartTotal.Text = Counter("AbnPartScanned")
            txtAbnSPartKanban.Focus()
            txtAbnSPartKanban.SelectAll()
            If IsForceScan Then
                bringPanelToFront(pnlUnpackAbnScanPart, pnlUnpackAbnFscanPart)
            End If
        ElseIf Panel = "PanelAbnPartError" Then
            lblAbnSPartMsgC.BackColor = Color.Red
            lblAbnSPartMsgC.Text = (IIf(String.IsNullOrEmpty(msg), Nothing, msg))
            lblAbnDesc.Text = (IIf(String.IsNullOrEmpty(desc), Nothing, desc))
            lblAbnScanPartTotal.Text = Counter("AbnPartScanned")
            lblAbnScanPartNo.Text = Nothing
            lblAbnSPartSeqNo.Text = Nothing
            Label3.Text = Nothing
            Label4.Text = Nothing
            txtAbnSPartKanban.Focus()
            txtAbnSPartKanban.SelectAll()
            If IsForceScan Then
                bringPanelToFront(pnlUnpackAbnScanPart, pnlUnpackAbnFscanPart)
            End If
        ElseIf Panel = "PanelAbnPartPost" Then
            MsgBox(successMsgPart, MsgBoxStyle.Information, Me.Text)
            Call LoadAbnUnpackPost()
            Call UpdateBatch()
        End If
    End Sub

    Private Sub AddToUnpackInterface(ByVal ONOFFLINE_FLAG As String, _
                                     ByVal currentDate As String, _
                                     ByVal isForceScan As Boolean, _
                                     Optional ByVal object1 As Object = Nothing, _
                                     Optional ByVal object2 As Object = Nothing)
        Dim sql As String = Nothing
        Dim tempMODD As Module_Input = Nothing
        Dim tempPART As Part_Input = Nothing
        Dim dr As DataRow = Nothing
        tempMODD = TryCast(object1, Module_Input)
        If tempMODD IsNot Nothing Then
            MODD = New Module_Input
            MODD = tempMODD
        End If
        tempPART = TryCast(object2, Part_Input)
        If tempPART IsNot Nothing Then
            PART = New Part_Input
            PART = tempPART
        End If
        If UnpackTableReader(UNPACK_INTERFACE, _
                       MODD.MODULE_NO, _
                       PART.PARTS_NO, _
                       PART.PART_SEQ_NUMBER, _
                       PART.PART_BRANCH_NUMBER, _
                       org_ID, _
                       IIf(Not String.IsNullOrEmpty(PART.ROBB_MODD_NO), PART.ROBB_MODD_NO, "N")) = 0 Then
            sql = "INSERT INTO [" & UNPACK_INTERFACE & "] (" + QueryColStr() + ") "
            sql = sql & "VALUES ("
            sql = sql & SQLQuote(curr_BATCH) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(MODD.MODULE_NO), MODD.MODULE_NO, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.PARTS_NO), PART.PARTS_NO, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.PART_NO_SFX), PART.PART_NO_SFX, Nothing)) & ", "
            sql = sql & PART.PART_SEQ_NUMBER & ", "
            sql = sql & IIf(Not PART.QTY_BOX = Nothing, PART.QTY_BOX, "null") & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.MANUFACTURE_CODE), PART.MANUFACTURE_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.SUPPLIER_CODE), PART.SUPPLIER_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.SUPPLIER_PLANT_CODE), PART.SUPPLIER_PLANT_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.SUPPLIER_SHIPPING_DOCK), PART.SUPPLIER_SHIPPING_DOCK, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.BEFORE_PACKING_ROUTING), PART.BEFORE_PACKING_ROUTING, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.RECEIVING_COMPANY_CODE), PART.RECEIVING_COMPANY_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.RECEIVING_PLANT_CODE), PART.RECEIVING_PLANT_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.RECEIVING_DOCK_CODE), PART.RECEIVING_DOCK_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.PACKING_ROUTING_CODE), PART.PACKING_ROUTING_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.GRANTER_CODE), PART.GRANTER_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.ORDER_TYPE), PART.ORDER_TYPE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.KANBAN_CLASSIFICATION), PART.KANBAN_CLASSIFICATION, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.DELIVERY_DATE), PART.DELIVERY_DATE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.DELIVERY_CODE), PART.DELIVERY_CODE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.MROS), PART.MROS, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.ORDER_NUMBER), PART.ORDER_NUMBER, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.DELIVERY_NUMBER), PART.DELIVERY_NUMBER, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.BACK_NUMBER), PART.BACK_NUMBER, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.RUNOUT_FLAG), PART.RUNOUT_FLAG, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.BOX_TYPE), PART.BOX_TYPE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.BRANCH_NUMBER), PART.BRANCH_NUMBER, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.ADDRESS), PART.ADDRESS, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.PACKING_DATE), PART.PACKING_DATE, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.KATASHIKI_JERSEY_NUMBER), PART.KATASHIKI_JERSEY_NUMBER, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.LOT_NO), PART.LOT_NO, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.MODULE_CATEGORY), PART.MODULE_CATEGORY, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.PART_BRANCH_NUMBER), PART.PART_BRANCH_NUMBER, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.DUMMY), PART.DUMMY, Nothing)) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.VERSION_NO), PART.VERSION_NO, Nothing)) & ", "
            sql = sql & org_ID & ", "
            sql = sql & SQLQuote(gScannerID) & ", "
            sql = sql & SQLQuote(currentDate) & ", "
            sql = sql & SQLQuote(ONOFFLINE_FLAG) & ", "
            sql = sql & SQLQuote(currentDate) & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(MODD.REASONID), "Y", "N")) & ", "
            sql = sql & IIf(Not String.IsNullOrEmpty(MODD.REASONID), MODD.REASONID, "null") & ", "
            sql = sql & SQLQuote(IIf(isForceScan, "Y", "N")) & ", "
            sql = sql & IIf(isForceScan, PART.PARTREASONID, "null") & ", "
            sql = sql & SQLQuote(IIf(Not String.IsNullOrEmpty(PART.ROBB_MODD_NO), PART.ROBB_MODD_NO, "N"))
            sql = sql & ")"
            If CeNonQuery(sql) = False Then
                Throw New Exception("Failed to Create Local Part Table.")
            End If
        Else
            Throw New Exception("Part Already Existed On Local Database.")
        End If
    End Sub

    Private Sub UpdateUnpackPendingDetails(ByVal MODULE_NO As String, _
                                           ByVal PART_NO As String, _
                                           ByVal PART_SEQ_NO As String, _
                                           ByVal PART_BRANCH_NO As String)
        Dim dt As DataTable = New DataTable()
        Dim ROBB_MODD_NO As String = String.Empty
        dt = CeDataAdapter("SELECT * FROM [" & UNPACK_PENDING & "] " & _
                          "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                          "AND PART_NO = " & SQLQuote(PART_NO) & " " & _
                          "AND PART_SEQ_NO = " & CInt(PART_SEQ_NO) & " " & _
                          "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                          "AND BRANCH_NO = " & SQLQuote(PART_BRANCH_NO))
        If dt.Rows.Count > 0 Then
            If dt.Rows.Count > 1 Then
                If PART.MODULE_CATEGORY & PART.LOT_NO = MODD.MODULE_NO Then
                    ROBB_MODD_NO = "N"
                Else
                    ROBB_MODD_NO = PART.MODULE_CATEGORY & PART.LOT_NO
                End If
                If Not CeNonQuery("DELETE FROM [" & UNPACK_PENDING & "] " & _
                             "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                             "AND PART_NO = " & SQLQuote(PART_NO) & " " & _
                             "AND PART_SEQ_NO = " & CInt(PART_SEQ_NO) & " " & _
                             "AND BRANCH_NO = " & SQLQuote(PART_BRANCH_NO) & " " & _
                             "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                             "AND ROBB_MODULE_NO = " & SQLQuote(ROBB_MODD_NO)) Then
                    Throw New Exception("Failed to Create Local Part Table.")
                End If
            Else
                If Not CeNonQuery("DELETE FROM [" & UNPACK_PENDING & "] " & _
                             "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                             "AND PART_NO = " & SQLQuote(PART_NO) & " " & _
                             "AND PART_SEQ_NO = " & CInt(PART_SEQ_NO) & " " & _
                             "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                             "AND BRANCH_NO = " & SQLQuote(PART_BRANCH_NO)) Then
                    Throw New Exception("Failed to Create Local Part Table.")
                End If
            End If
        End If
    End Sub

    Private Sub UpdatePostStatus(ByVal MODULE_NO As String, _
                                 ByVal PART_NO As String, _
                                 ByVal PART_SEQ_NO As String, _
                                 ByVal PART_BRANCH_NO As String, _
                                 Optional ByVal ROBB_MODD_NO As String = "N")
        If Not CeNonQuery("UPDATE [" & UNPACK_INTERFACE & "] " & _
                         "SET PROCESS_FLAG = 'POSTED' " & _
                         "WHERE MODULE_NO = " & SQLQuote(MODULE_NO) & " " & _
                         "AND PART_NO = " & SQLQuote(PART_NO) & " " & _
                         "AND PXP_PART_SEQ_NO = " & CInt(PART_SEQ_NO) & " " & _
                         "AND PART_BRANCH_NO = " & SQLQuote(PART_BRANCH_NO) & " " & _
                         "AND ORG_ID = " & SQLQuote(org_ID) & " " & _
                         "AND ROBB_MODULE_NO = " & SQLQuote(ROBB_MODD_NO)) Then
            Throw New Exception("Failed to Create Local Part Table.")
        End If
    End Sub

    Public Function QueryColStr() As String
        Dim buildQuery As String = Nothing
        buildQuery = "RCV_INTERFACE_BATCH_ID, MODULE_NO, PART_NO, PXP_PART_NO_SFX, PXP_PART_SEQ_NO, QTY_BOX, MANUFACTURE_CODE, SUPPLIER_CODE, SUPPLIER_PLANT_CODE, "
        buildQuery = buildQuery + "SUPPLIER_SHIPPING_DOCK, BEFORE_PACKING_ROUTING, RECEIVING_COMPANY_CODE, RECEIVING_PLANT_CODE, RECEIVING_DOCK_CODE, "
        buildQuery = buildQuery + "PACKING_ROUTING_CODE, GRANTER_CODE, ORDER_TYPE, KANBAN_CLASSIFICATION, DELIVERY_DATE, DELIVERY_CODE, MROS, ORDER_NO, "
        buildQuery = buildQuery + "DELIVERY_NO, BACK_NUMBER, RUNOUT_FLAG, BOX_TYPE, BRANCH_NO, ADDRESS, PACKING_DATE, KATASHIKI_JERSEY_NO, LOT_NO, "
        buildQuery = buildQuery + "MODULE_CATEGORY, PART_BRANCH_NO, DUMMY, VERSION_NO, ORG_ID, UNPACK_BY, UNPACK_DATE, ON_OFF_LINE_FLAG, SCAN_DATE, "
        buildQuery = buildQuery + "FORCE_MODULE_STATUS, FORCE_MODULE_REASON_ID, FORCE_PXP_STATUS, FORCE_PXP_REASON_ID, ROBB_MODULE_NO"
        Return buildQuery
    End Function

    Function Counter(ByVal state As String) As String
        Dim result As String = Nothing

        If state = "Pending" Then
            Call PopulateUnpackPendingDetails(MODD.MODULE_NO)
            result = cntPending.ToString()
        ElseIf state = "Scanned" Then
            Call PopulateUnpackScannedDetails(MODD.MODULE_NO)
            result = cntScanned.ToString()
        ElseIf state = "AbnPartScanned" Then
            Call PopulateAbnUnpackDetails(False)
            result = cntAbnPartScanned.ToString
        ElseIf state = "AbnDelete" Then
            Call PopulateAbnDelete()
            result = cntAbnDelete.ToString
        ElseIf state = "AbnTotal" Then
            Call PopulateAbnPost()
            result = cntAbnTotal.ToString
        End If
        Return result
    End Function

    Private Function IsNumber(ByVal input As String) As Boolean
        Return Regex.IsMatch(input, "^[0-9 ]+$")
    End Function

    Private Function IsEmptyString(ByVal input As String) As Boolean
        Return Regex.IsMatch(input, "\s")
    End Function

    Private Sub VerifyOrgId()
        Dim dtPartList As DataTable = ws_dcsClient.getData("*", "JSP_UNPACKING_DETAILS_VIEW", String.Format(" AND MODULE_NO = {0} AND ORG_ID = {1}", SQLQuote(MODD.MODULE_NO), SQLQuote(org_ID)))
        If dtPartList.Rows.Count > 0 Then
            If Not org_ID = dtPartList.Rows(0).Item("ORG_ID").ToString.TrimStart("0"c) Then
                Throw New CustomException
            End If
        End If
    End Sub

    'Module Force Scan All Mode - start
    Private Sub PopulateForceScan(Optional ByVal type As String = Nothing)
        Dim lv As ListView = New ListView
        Dim newItem As ListViewItem
        Dim dtReason As DataTable = New DataTable

        If type = "Module" Then
            lv = lstViewFSMod
        ElseIf type = "Part" Then
            lv = lstviewUnpFSPReason
        ElseIf type = "Abn_Module" Then
            lv = lstViewAbnFSMod
        ElseIf type = "Abn_Part" Then
            lv = lstViewUnpAbnFSPartReason
        End If

        lv.Items.Clear()
        dtReason = getData("SELECT REASON_CODE, REASON FROM [" & TblJSPAbnormalReasonCodeDb & "]")
        If dtReason.Rows.Count > 0 Then
            For i As Integer = 0 To dtReason.Rows.Count - 1
                newItem = New ListViewItem
                newItem.Text = dtReason.Rows(i).Item("REASON").ToString
                newItem.SubItems.Add(dtReason.Rows(i).Item("REASON_CODE").ToString)
                lv.Items.Add(newItem)
            Next
        End If
    End Sub
    'Module Force Scan All Mode - end

    Private Sub GetRobbModuleNo(Optional ByVal TForceScan As Boolean = False, _
                                Optional ByVal TTextField As String = Nothing)
        If TForceScan Then
            If Not String.IsNullOrEmpty(TTextField.Trim) Then
                If TTextField.Trim.Length <> 6 Then
                    PART.MODULE_CATEGORY = MODD.MODULE_NO.Substring(0, 2)
                    PART.LOT_NO = MODD.MODULE_NO.Substring(2, 4)
                    PART.ROBB_MODD_NO = "N"
                Else
                    PART.MODULE_CATEGORY = TTextField.Substring(0, 2)
                    PART.LOT_NO = TTextField.Substring(2, 4)
                    PART.ROBB_MODD_NO = TTextField
                End If
            Else
                PART.MODULE_CATEGORY = MODD.MODULE_NO.Substring(0, 2)
                PART.LOT_NO = MODD.MODULE_NO.Substring(2, 4)
                PART.ROBB_MODD_NO = "N"
            End If
        Else
            If (PART.MODULE_CATEGORY & PART.LOT_NO) = MODD.MODULE_NO Then
                PART.ROBB_MODD_NO = "N"
            Else
                PART.ROBB_MODD_NO = (PART.MODULE_CATEGORY & PART.LOT_NO)
            End If
        End If
    End Sub

#End Region

#Region ". QR Readable Helper ."

    Function IsPartQRReadable(ByVal input As String, Optional ByVal type As String = Nothing) As Boolean
        Dim errorMsg As String = "Invalid Part."
        Dim _temp_manufacture_code As String = Nothing
        Dim _temp_supplier_code As String = Nothing
        Dim _temp_supplier_plant_code As String = Nothing
        Dim _temp_supplier_shipping_dock As String = Nothing
        Dim _temp_before_packing_routing As String = Nothing
        Dim _temp_receiving_company_code As String = Nothing
        Dim _temp_receiving_plant_code As String = Nothing
        Dim _temp_receiving_dock_code As String = Nothing
        Dim _temp_packing_routing_code As String = Nothing
        Dim _temp_granter_code As String = Nothing
        Dim _temp_order_type As String = Nothing
        Dim _temp_kanban_classification As String = Nothing
        Dim _temp_delivery_date As String = Nothing
        Dim _temp_delivery_code As String = Nothing
        Dim _temp_mros As String = Nothing
        Dim _temp_order_number As String = Nothing
        Dim _temp_delivery_number As String = Nothing
        Dim _temp_back_number As String = Nothing
        Dim _temp_parts_no As String = Nothing
        Dim _temp_part_no_sfx As String = Nothing
        Dim _temp_qty_box As String = Nothing
        Dim _temp_runout_flag As String = Nothing
        Dim _temp_delivery_code_2 As String = Nothing
        Dim _temp_box_type As String = Nothing
        Dim _temp_branch_number As String = Nothing
        Dim _temp_address As String = Nothing
        Dim TimeFormat As String = "HH:mm"
        Dim _temp_delivery_time As String = Nothing
        Dim _temp_packing_date As String = Nothing
        Dim formatted As DateTime = Nothing
        Dim _temp_katashiki_jersey_num As String = Nothing
        Dim _temp_lot_no As String = Nothing
        Dim _temp_mod_category As String = Nothing
        Dim _temp_part_seq_num As String = Nothing
        Dim _temp_part_branch_num As String = Nothing
        Dim _temp_dummy As String = Nothing
        Dim _temp_version_no As String = Nothing
        Dim cont As MsgBoxResult = Nothing = Nothing

        Try
            _temp_manufacture_code = input.Substring(0, MANUFACTURE_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_manufacture_code) Then
                If IsEmptyString(_temp_manufacture_code) Then
                    PART.MANUFACTURE_CODE = Nothing
                Else
                    PART.MANUFACTURE_CODE = _temp_manufacture_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_supplier_code = input.Substring(2, SUPPLIER_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_supplier_code) Then
                If IsEmptyString(_temp_supplier_code) Then
                    PART.SUPPLIER_CODE = Nothing
                Else
                    PART.SUPPLIER_CODE = _temp_supplier_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_supplier_plant_code = input.Substring(6, SUPPLIER_PLANT_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_supplier_plant_code) Then
                If IsEmptyString(_temp_supplier_plant_code) Then
                    PART.SUPPLIER_PLANT_CODE = Nothing
                Else
                    PART.SUPPLIER_PLANT_CODE = _temp_supplier_plant_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            'Dim str As String = input
            'str = str.Replace(" ", String.Empty)
            'MsgBox(str)
            'Dim cleaned As String = Regex.Replace(input, "\s{2,}", " ")

            _temp_supplier_shipping_dock = input.Substring(7, SUPPLIER_SHIPPING_DOCK_LENGTH)
            If Not String.IsNullOrEmpty(_temp_supplier_shipping_dock) Or _temp_supplier_shipping_dock = " " Then
                If IsEmptyString(_temp_supplier_shipping_dock) Then
                    PART.SUPPLIER_SHIPPING_DOCK = Nothing
                Else
                    PART.SUPPLIER_SHIPPING_DOCK = _temp_supplier_shipping_dock
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_before_packing_routing = input.Substring(10, BEFORE_PACKING_ROUTING_LENGTH)
            If Not String.IsNullOrEmpty(_temp_before_packing_routing) Then
                If IsEmptyString(_temp_before_packing_routing) Then
                    PART.BEFORE_PACKING_ROUTING = Nothing
                Else
                    PART.BEFORE_PACKING_ROUTING = _temp_before_packing_routing
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_receiving_company_code = input.Substring(16, RECEIVING_COMPANY_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_receiving_company_code) Then
                If IsEmptyString(_temp_receiving_company_code) Then
                    PART.RECEIVING_COMPANY_CODE = Nothing
                Else
                    PART.RECEIVING_COMPANY_CODE = _temp_receiving_company_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_receiving_plant_code = input.Substring(20, RECEIVING_PLANT_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_receiving_plant_code) Then
                If IsEmptyString(_temp_receiving_plant_code) Then
                    PART.RECEIVING_PLANT_CODE = Nothing
                Else
                    PART.RECEIVING_PLANT_CODE = _temp_receiving_plant_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_receiving_dock_code = input.Substring(21, RECEIVING_DOCK_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_receiving_dock_code) Then
                If IsEmptyString(_temp_receiving_dock_code) Then
                    PART.RECEIVING_DOCK_CODE = Nothing
                Else
                    PART.RECEIVING_DOCK_CODE = _temp_receiving_dock_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_packing_routing_code = input.Substring(23, PACKING_ROUTING_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_packing_routing_code) Then
                If IsEmptyString(_temp_packing_routing_code) Then
                    PART.PACKING_ROUTING_CODE = Nothing
                Else
                    PART.PACKING_ROUTING_CODE = _temp_packing_routing_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_granter_code = input.Substring(29, GRANTER_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_granter_code) Then
                If IsEmptyString(_temp_granter_code) Then
                    PART.GRANTER_CODE = Nothing
                Else
                    PART.GRANTER_CODE = _temp_granter_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If
            '====

            _temp_order_type = input.Substring(33, ORDER_TYPE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_order_type) Then
                If IsEmptyString(_temp_order_type) Then
                    PART.ORDER_TYPE = Nothing
                Else
                    PART.ORDER_TYPE = _temp_order_type
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_kanban_classification = input.Substring(34, KANBAN_CLASSIFICATION_LENGTH)
            If Not String.IsNullOrEmpty(_temp_kanban_classification) Then
                If IsEmptyString(_temp_kanban_classification) Then
                    PART.KANBAN_CLASSIFICATION = Nothing
                Else
                    PART.KANBAN_CLASSIFICATION = _temp_kanban_classification
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_delivery_date = input.Substring(35, DELIVERY_DATE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_date) Then
                If IsEmptyString(_temp_delivery_date) Then
                    PART.DELIVERY_DATE = Nothing
                Else
                    PART.DELIVERY_DATE = _temp_delivery_date
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_delivery_code = input.Substring(39, DELIVERY_CODE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_code) Then
                If IsEmptyString(_temp_delivery_code) Then
                    PART.DELIVERY_CODE = Nothing
                Else
                    PART.DELIVERY_CODE = _temp_delivery_code
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_mros = input.Substring(41, MROS_LENGTH)
            If Not String.IsNullOrEmpty(_temp_mros) Then
                If IsEmptyString(_temp_mros) Then
                    PART.MROS = Nothing
                Else
                    PART.MROS = _temp_mros
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_order_number = input.Substring(43, ORDER_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_order_number) Then
                If IsEmptyString(_temp_order_number) Then
                    PART.ORDER_NUMBER = Nothing
                Else
                    PART.ORDER_NUMBER = _temp_order_number
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_delivery_number = input.Substring(55, DELIVERY_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_number) Then
                If IsEmptyString(_temp_delivery_number) Then
                    PART.DELIVERY_NUMBER = Nothing
                Else
                    PART.DELIVERY_NUMBER = _temp_delivery_number
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_back_number = input.Substring(60, BACK_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_back_number) Then
                If IsEmptyString(_temp_back_number) Then
                    PART.BACK_NUMBER = Nothing
                Else
                    PART.BACK_NUMBER = _temp_back_number
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_parts_no = input.Substring(64, PARTS_NO_LENGTH)
            If Not String.IsNullOrEmpty(_temp_parts_no) Then
                If IsEmptyString(_temp_parts_no) Then
                    PART.PARTS_NO = Nothing
                Else
                    PART.PARTS_NO = _temp_parts_no
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_part_no_sfx = input.Substring(74, PART_NO_SFX_LENGTH)
            If Not String.IsNullOrEmpty(_temp_part_no_sfx) Then
                If IsEmptyString(_temp_part_no_sfx) Then
                    PART.PART_NO_SFX = Nothing
                Else
                    PART.PART_NO_SFX = _temp_part_no_sfx
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_qty_box = input.Substring(76, QTY_BOX_LENGTH)
            If Not String.IsNullOrEmpty(_temp_qty_box) Then
                If IsEmptyString(_temp_qty_box) Then
                    PART.QTY_BOX = Nothing
                Else
                    PART.QTY_BOX = _temp_qty_box
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_runout_flag = input.Substring(81, RUNOUT_FLAG_LENGTH)
            If Not String.IsNullOrEmpty(_temp_runout_flag) Then
                If IsEmptyString(_temp_runout_flag) Then
                    PART.RUNOUT_FLAG = Nothing
                Else
                    PART.RUNOUT_FLAG = _temp_runout_flag
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_delivery_code_2 = input.Substring(82, DELIVERY_CODE_2_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_code_2) Then
                If IsEmptyString(_temp_delivery_code_2) Then
                    PART.DELIVERY_CODE_2 = Nothing
                Else
                    PART.DELIVERY_CODE_2 = _temp_delivery_code_2
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_box_type = input.Substring(83, BOX_TYPE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_box_type) Then
                If IsEmptyString(_temp_box_type) Then
                    PART.BOX_TYPE = Nothing
                Else
                    PART.BOX_TYPE = _temp_box_type
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_branch_number = input.Substring(91, BRANCH_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_branch_number) Then
                If IsEmptyString(_temp_branch_number) Then
                    PART.BRANCH_NUMBER = Nothing
                Else
                    PART.BRANCH_NUMBER = _temp_branch_number
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_address = input.Substring(95, ADDRESS_LENGTH)
            If Not String.IsNullOrEmpty(_temp_address) Then
                If IsEmptyString(_temp_address) Then
                    PART.ADDRESS = Nothing
                Else
                    PART.ADDRESS = _temp_address
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_delivery_time = input.Substring(105, DELIVERY_TIME_LENGTH)
            If Not String.IsNullOrEmpty(_temp_delivery_time) And Not IsEmptyString(_temp_delivery_time) Then
                Try
                    formatted = DateTime.ParseExact(_temp_delivery_time, TimeFormat, CultureInfo.InvariantCulture)
                    PART.DELIVERY_TIME = formatted
                Catch ex As Exception
                    Throw New FormatException(errorMsg)
                End Try
            Else
                PART.DELIVERY_TIME = Nothing
            End If

            _temp_packing_date = input.Substring(110, PACKING_DATE_LENGTH)
            If Not String.IsNullOrEmpty(_temp_packing_date) Then
                If IsEmptyString(_temp_packing_date) Then
                    PART.PACKING_DATE = Nothing
                Else
                    PART.PACKING_DATE = _temp_packing_date
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_katashiki_jersey_num = input.Substring(118, KATASHIKI_JERSEY_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_katashiki_jersey_num) Then
                If IsEmptyString(_temp_katashiki_jersey_num) Then
                    PART.KATASHIKI_JERSEY_NUMBER = Nothing
                Else
                    PART.KATASHIKI_JERSEY_NUMBER = _temp_katashiki_jersey_num
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_lot_no = input.Substring(121, LOT_NO_LENGTH)
            If Not String.IsNullOrEmpty(_temp_lot_no) Then
                If IsEmptyString(_temp_lot_no) Then
                    PART.LOT_NO = Nothing
                Else
                    PART.LOT_NO = _temp_lot_no
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_mod_category = input.Substring(125, MODULE_CATEGORY_LENGTH)
            If Not String.IsNullOrEmpty(_temp_mod_category) Then
                If IsEmptyString(_temp_mod_category) Then
                    PART.MODULE_CATEGORY = Nothing
                Else
                    PART.MODULE_CATEGORY = _temp_mod_category
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_part_seq_num = input.Substring(127, PART_SEQ_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_part_seq_num) Then
                If IsEmptyString(_temp_part_seq_num) Then
                    PART.PART_SEQ_NUMBER = Nothing
                Else
                    PART.PART_SEQ_NUMBER = _temp_part_seq_num
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_part_branch_num = input.Substring(129, PART_BRANCH_NUMBER_LENGTH)
            If Not String.IsNullOrEmpty(_temp_part_branch_num) Then
                If IsEmptyString(_temp_part_branch_num) Then
                    PART.PART_BRANCH_NUMBER = Nothing
                Else
                    PART.PART_BRANCH_NUMBER = _temp_part_branch_num
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_dummy = input.Substring(131, DUMMY_LENGTH)
            If Not String.IsNullOrEmpty(_temp_dummy) Then
                If IsEmptyString(_temp_dummy) Then
                    PART.DUMMY = Nothing
                Else
                    PART.DUMMY = _temp_dummy
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            _temp_version_no = input.Substring(151, VERSION_NO_LENGTH)
            If Not String.IsNullOrEmpty(_temp_version_no) Then
                If IsEmptyString(_temp_version_no) Then
                    PART.VERSION_NO = Nothing
                Else
                    PART.VERSION_NO = _temp_version_no
                End If
            Else
                Throw New FormatException(errorMsg)
            End If

            Return True

        Catch ex As Exception
            If Not type = "Abn" Then
                lblPartModNo.Text = MODD.MODULE_NO
                txtKanbanQRPart.Focus()
                txtKanbanQRPart.SelectAll()
            Else
                lblAbnSPartModNo.Text = MODD.MODULE_NO
                txtAbnSPartKanban.Focus()
                txtAbnSPartKanban.SelectAll()
            End If
            'cont = MsgBox("Unpack - Part scanned failed. Exception:" + ex.Message.ToString() + Environment.NewLine + _
            '              "Proceed to Force Scan?", MsgBoxStyle.Question, _
            '              IIf((Not type = "Abn"), "Unpack - Part Scan", "Unpack - Abnormal Part Scan"))
            cont = MsgBox("Unpack - Part scanned failed." + Environment.NewLine + _
                                      "Proceed to Force Scan?", MsgBoxStyle.Question, _
                                      IIf((Not type = "Abn"), "Unpack - Part Scan", "Unpack - Abnormal Part Scan"))
            If cont = MsgBoxResult.Yes Then
                If Not type = "Abn" Then
                    bringPanelToFront(pnlUnpackFScanPart, pnlUnpackScanPart)
                Else
                    bringPanelToFront(pnlUnpackAbnFscanPart, pnlUnpackAbnScanPart)
                End If
            Else
                Exit Function
            End If
            Return False
        End Try
    End Function

    Private Sub IsModuleReadable(ByVal input As String)
        Dim errorMsg As String = "Invalid Module."
        Dim temp_module_no As String = Nothing
        Dim temp_pilling_no As String = Nothing
        Dim temp_gross_weight As String = Nothing
        Dim temp_order_no As String = Nothing

        'MODULE_NO - START
        temp_module_no = input.Substring(0, UNPACK_MODULE_NO_LENGTH)
        If Not String.IsNullOrEmpty(temp_module_no) Then
            If IsEmptyString(temp_module_no) Then
                MODD.MODULE_NO = Nothing
            Else
                MODD.MODULE_NO = temp_module_no
            End If
        Else
            Throw New FormatException(errorMsg)
        End If
        'MODULE_NO - END

        'PILLING_NO - START
        temp_pilling_no = input.Substring(6, UNPACK_PILLING_NO_LENGTH)
        If Not String.IsNullOrEmpty(temp_pilling_no) Then
            If IsEmptyString(temp_pilling_no) Then
                MODD.PILLING_NO = Nothing
            Else
                MODD.PILLING_NO = temp_pilling_no
            End If
        Else
            Throw New FormatException(errorMsg)
        End If
        'PILLING_NO - END

        'GROSS_WEIGHT - START
        temp_gross_weight = input.Substring(7, UNPACK_GROSS_WEIGHT_LENGTH)
        If Not String.IsNullOrEmpty(temp_gross_weight) And IsNumber(temp_gross_weight) Then
            If IsEmptyString(temp_gross_weight) Then
                MODD.GROSS_WEIGHT = Nothing
            Else
                MODD.GROSS_WEIGHT = temp_gross_weight
            End If
        Else
            Throw New FormatException(errorMsg)
        End If
        'GROSS_WEIGHT - END

        temp_order_no = input.Substring(12, UNPACK_ORDER_NO_LENGTH)
        If Not String.IsNullOrEmpty(temp_order_no) Then
            If IsEmptyString(temp_order_no) Then
                MODD.ORDER_NO = Nothing
            Else
                MODD.ORDER_NO = temp_order_no
            End If
        Else
            Throw New FormatException(errorMsg)
        End If
    End Sub

#End Region

End Class