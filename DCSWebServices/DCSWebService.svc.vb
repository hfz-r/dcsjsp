' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
'Imports TDS.Core
'Imports TDS
'Imports System.Data.SqlClient
Imports System.Data
'Imports Oracle.DataAccess.Client
'Imports Oracle.DataAccess.Types
Imports System.Web.Services
Imports Oracle.ManagedDataAccess.Client

Public Class DCSWebService
    Implements IDCSWebService

    Private timeFormat As String = ConfigurationManager.AppSettings("TimeFormat").ToString()
    Private oradb As String = "Data Source=(DESCRIPTION=" _
           & "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" & ConfigurationManager.AppSettings("OracleHost").ToString() & ")(PORT=" & ConfigurationManager.AppSettings("OraclePort").ToString() & ")))" _
           & "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" & ConfigurationManager.AppSettings("OracleServiceName").ToString() & ")));" _
           & "User Id=" & ConfigurationManager.AppSettings("OracleUserID").ToString() & ";Password=" & ConfigurationManager.AppSettings("OraclePassword").ToString() & ";"
    Private logPath As String = ConfigurationManager.AppSettings("LogFolder").ToString() 'don't put backslash as last character
    Private isLogging As Boolean = IIf(ConfigurationManager.AppSettings("SystemLog").ToString() = "Y", True, False) 'later user can control to do logging or not
    Private conn As String = String.Empty

    Public Sub New()
    End Sub

#Region "General Public Functions"

    Public Function isConnected() As Boolean Implements IDCSWebService.isConnected
        Return True
    End Function

    <WebMethod()>
    Public Function InitiliazeConnection(ByVal host As String,
                                         ByVal port As String,
                                         ByVal servicename As String,
                                         ByVal userid As String,
                                         ByVal userpassword As String) As String Implements IDCSWebService.InitiliazeConnection
        Try
            conn = "Data Source=(DESCRIPTION=" _
                   & "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" & host & ")(PORT=" & port & ")))" _
                   & "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" & servicename & ")));" _
                   & "User Id=" & userid & ";Password=" & userpassword & ";"
        Catch ex As Exception
            writeLog("InitiliazeConnection," & """" & ex.Message & """")
        End Try
        Return conn
    End Function

    <WebMethod()> _
    Public Function getTime() As String Implements IDCSWebService.getTime
        Dim strTime As String = "01-01-1900 00:00:01"
        Try

            Dim dt As DataTable = executeSQLRead("SELECT TO_CHAR (SYSDATE, 'DD-MM-YYYY HH:MI:SS AM') ""NOW"" FROM DUAL")

            If dt.Rows.Count > 0 Then
                strTime = dt.Rows(0).Item("NOW").ToString
            End If

        Catch ex As Exception
            writeLog("getTime," & """" & ex.Message & """")
        End Try

        Return strTime
    End Function

    <WebMethod()> _
    Public Function getTimeSet() As String Implements IDCSWebService.getTimeSet
        Dim strTime As String = "01-01-1900 00:00:01"
        Try

            Dim dt As DataTable = executeSQLRead("SELECT TO_CHAR (SYSDATE, 'MM-DD-YYYY HH:MI:SS AM') ""NOW"" FROM DUAL")

            If dt.Rows.Count > 0 Then
                strTime = dt.Rows(0).Item("NOW").ToString
            End If

        Catch ex As Exception
            writeLog("getTimeSet," & """" & ex.Message & """")
        End Try

        Return strTime
    End Function



    Public Function isOracleConnected() As Boolean Implements IDCSWebService.isOracleConnected
        Dim dt As DataTable = New DataTable()
        isOracleConnected = False
        Try
            Using connection As New OracleConnection(IIf(String.IsNullOrEmpty(conn), oradb, conn))
                Dim command As New OracleCommand("SELECT TO_CHAR (SYSDATE, 'DD-MM-YYYY HH24:MI:SS') ""NOW"" FROM DUAL")
                command.Connection = connection
                Try
                    connection.Open()
                    Dim dr As OracleDataReader = command.ExecuteReader()
                    dt.Load(dr)
                    isOracleConnected = True
                    dt.Dispose()
                Catch ex As Exception
                    writeLog("isOracleConnected," & """" & ex.Message & """")
                End Try
            End Using
        Catch ex As Exception
            writeLog("isOracleConnected," & """" & ex.Message & """")
        End Try


    End Function

#End Region

#Region "Service Part"

    Public Function verifyLogin(ByVal uname As String, ByVal pwd As String) As Boolean Implements IDCSWebService.verifyLogin
        verifyLogin = False
        Try
            Dim str As String = "SELECT * FROM SEP_LOGIN_V WHERE LOGIN_ID = " & strQuote(uname) & " AND PASSWORD = " & strQuote(pwd)
            Dim dt As DataTable = executeSQLRead(str)

            If dt.Rows.Count > 0 Then
                verifyLogin = True
            End If

        Catch ex As Exception
            writeLog("verifyLogin," & """" & ex.Message & """")
        End Try
    End Function

    Public Function getData(ByVal columns As String, ByVal tableName As String, ByVal whereSQL As String) As DataTable Implements IDCSWebService.getData
        Dim selectSQL As String = "SELECT " & columns & " FROM " & tableName & " WHERE 1=1 " & whereSQL
        Dim dt As DataTable = New DataTable()
        Try
            If selectSQL <> "" Then
                dt = executeSQLRead(selectSQL)
            Else
                writeLog("getData," & """" & "blank query" & """")
            End If
        Catch ex As Exception
            writeLog("getData," & """" & ex.Message & """")
        End Try

        Return dt
    End Function

    Public Function insertData(ByVal insertSQL As String) As Boolean Implements IDCSWebService.insertData
        'insertSQL is insert statement
        insertData = False

        Try

            If insertSQL <> "" Then
                insertData = executeSQLNonRead(insertSQL)
            Else
                writeLog("insertData," & """" & "blank query" & """")
            End If
        Catch ex As Exception
            writeLog("insertData," & """" & ex.Message & """")
        End Try

    End Function

    Public Function updateData(ByVal updateSQL As String) As Boolean Implements IDCSWebService.updateData
        'updateSQL is update statement
        updateData = False

        Try

            If updateSQL <> "" Then
                updateData = executeSQLNonRead(updateSQL)
            Else
                writeLog("updateData," & """" & "blank query" & """")
            End If
        Catch ex As Exception
            writeLog("updateData," & """" & ex.Message & """")
        End Try

    End Function


#End Region

#Region "Private Functions"

    Private Function executeSQLRead(ByVal strSQL As String) As DataTable

        'strSQL is select statement

        Dim dt As DataTable = New DataTable()
        Try

            If strSQL <> "" Then
                Using connection As New OracleConnection(IIf(String.IsNullOrEmpty(conn), oradb, conn))
                    Dim command As New OracleCommand(strSQL)
                    command.Connection = connection
                    Try
                        connection.Open()
                        Dim dr As OracleDataReader = command.ExecuteReader()
                        dt.Load(dr)
                    Catch ex As Exception
                        writeLog("executeSQLRead," & """" & ex.Message & """")
                    End Try
                End Using
            Else
                writeLog("executeSQLRead," & """" & "blank query" & """")
            End If
        Catch ex As Exception
            writeLog("executeSQLRead," & """" & ex.Message & """")
        End Try

        Return dt
    End Function

    Private Function executeSQLNonRead(ByVal strSQL As String) As Boolean

        'strSQL is either insert or update statement
        executeSQLNonRead = False

        Try

            If strSQL <> "" Then
                Using connection As New OracleConnection(IIf(String.IsNullOrEmpty(conn), oradb, conn))
                    connection.Open()

                    Dim command As OracleCommand = connection.CreateCommand()
                    Dim transaction As OracleTransaction

                    ' Start a local transaction
                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
                    ' Assign transaction object for a pending local transaction
                    command.Transaction = transaction

                    Try
                        command.CommandText = strSQL
                        command.ExecuteNonQuery()
                        transaction.Commit()
                        executeSQLNonRead = True
                    Catch ex As Exception
                        transaction.Rollback()
                        writeLog("executeSQLNonRead," & """" & ex.Message & """")
                    End Try
                End Using
            Else
                writeLog("executeSQLNonRead," & """" & "blank query" & """")
            End If
        Catch ex As Exception
            writeLog("executeSQLNonRead," & """" & ex.Message & """")
        End Try

    End Function

    Private Sub writeLog(ByVal msgLog As String)
        'msgLog = one line message
        Try
            If isLogging Then
                Dim folder As String = logPath
                Dim filename As String = folder & "\" & Date.Now.ToString("yyyyMMdd") & ".csv"


                If (Not System.IO.Directory.Exists(folder)) Then
                    System.IO.Directory.CreateDirectory(folder)
                End If

                Dim objWriter As New System.IO.StreamWriter(filename, True)

                If System.IO.File.Exists(filename) = False Then
                    System.IO.File.Create(filename).Dispose()
                End If

                objWriter.WriteLine(DateTime.Now.ToString(timeFormat) & "," & msgLog)
                objWriter.Close()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function strQuote(ByVal str As String) As String
        Try
            'str = """" & str & """"
            str = "'" & str & "'"
        Catch ex As Exception
            writeLog("strQuote," & """" & ex.Message & """")
        End Try

        Return str
    End Function

#End Region

#Region "Compression Method - GZip"



#End Region

End Class
