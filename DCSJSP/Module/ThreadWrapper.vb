Imports System.Data
Imports System.Data.SqlServerCe
Imports System.Threading
Imports DCSJSP.frmUnpack

Module ThreadWrapper

    Private thisLock As Object = New Object()

    Function CeNonQuery(ByVal query As String) As Boolean
        Dim conn As New SqlCeConnection(ConnStr)
        Try
            Dim cmd As New SqlCeCommand(query, conn)
            conn.Open()

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
        End Try
        Return True
    End Function

    Function CeDataAdapter(ByVal query As String) As DataTable
        Dim conn As New SqlCeConnection(ConnStr)
        Dim adapter As SqlCeDataAdapter = Nothing
        Dim dt As DataTable = New DataTable
        Try
            adapter = New SqlCeDataAdapter(query, conn)
            conn.Open()

            adapter.Fill(dt)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
        End Try
        Return dt
    End Function

    Function UnpackTableReader(ByVal TABLE As String, _
                               ByVal MODULE_NO As String, _
                               ByVal PART_NO As String, _
                               ByVal PART_SEQ_NO As String, _
                               ByVal BRANCH_NO As String, _
                               ByVal ORG_ID As String, _
                               Optional ByVal ROBB_MODULE_NO As String = "") As Integer
        Dim count As Integer = 0
        Dim query As String = Nothing
        Dim conn As New SqlCeConnection(ConnStr)
        Dim cmd As SqlCeCommand = Nothing
        Dim reader As SqlCeDataReader = Nothing
        Try
            SyncLock thisLock
                Select Case TABLE
                    Case UNPACK_PENDING
                        query = String.Format("SELECT COUNT(*) FROM [{0}] WHERE MODULE_NO = {1} " & _
                                              "AND PART_NO = {2} AND PART_SEQ_NO = {3} AND BRANCH_NO = {4} " & _
                                              "AND ORG_ID = {5} AND ROBB_MODULE_NO = {6}", _
                                              UNPACK_PENDING, SQLQuote(MODULE_NO), SQLQuote(PART_NO), _
                                              CInt(PART_SEQ_NO), SQLQuote(BRANCH_NO), SQLQuote(ORG_ID), _
                                              SQLQuote(IIf(Not String.IsNullOrEmpty(ROBB_MODULE_NO), ROBB_MODULE_NO, "N")))
                    Case UNPACK_INTERFACE
                        query = String.Format("SELECT COUNT(*) FROM [{0}] WHERE MODULE_NO = {1} " & _
                                              "AND PART_NO = {2} AND PXP_PART_SEQ_NO = {3} AND PART_BRANCH_NO = {4} " & _
                                              "AND ORG_ID = {5} AND ROBB_MODULE_NO = {6}", _
                                              UNPACK_INTERFACE, SQLQuote(MODULE_NO), SQLQuote(PART_NO), CInt(PART_SEQ_NO), _
                                              SQLQuote(BRANCH_NO), SQLQuote(ORG_ID), _
                                              SQLQuote(IIf(Not String.IsNullOrEmpty(ROBB_MODULE_NO), ROBB_MODULE_NO, "N")))
                End Select

                cmd = New SqlCeCommand(query, conn)
                conn.Open()

                reader = cmd.ExecuteReader()
                While reader.Read
                    count = reader.GetInt32(0)
                End While
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
            conn.Close()
        End Try
        Return count
    End Function

    Function PLaneTableReader(ByVal TABLE As String, _
                              ByVal PART_NO As String, _
                              ByVal SEQ_NO As String, _
                              ByVal ORG_ID As String) As Integer
        Dim count As Integer = 0
        Dim query As String = Nothing
        Dim conn As New SqlCeConnection(ConnStr)
        Dim cmd As SqlCeCommand = Nothing
        Dim reader As SqlCeDataReader = Nothing
        Try
            SyncLock thisLock
                Select Case TABLE
                    Case TblJSPSupplyPLPendingView
                        query = String.Format("SELECT COUNT(*) FROM [{0}] WHERE PART_NO = {1} AND SEQ_NO = {2} AND ORG_ID = {3}", _
                                              TblJSPSupplyPLPendingView, SQLQuote(PART_NO), SQLQuote(SEQ_NO), SQLQuote(ORG_ID))
                    Case TblJSPSupplyInterface
                        query = String.Format("SELECT COUNT(*) FROM [{0}] WHERE PXP_PART_NO = {1} AND PXP_PART_SEQ_NO = {2} AND ORG_ID = {3}", _
                                              TblJSPSupplyInterface, SQLQuote(PART_NO), SQLQuote(SEQ_NO), SQLQuote(ORG_ID))
                End Select

                cmd = New SqlCeCommand(query, conn)
                conn.Open()

                reader = cmd.ExecuteReader()
                While reader.Read
                    count = reader.GetInt32(0)
                End While

            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
            conn.Close()
        End Try
        Return count
    End Function

End Module