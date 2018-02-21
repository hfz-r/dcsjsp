Imports System.Data.SqlClient
Imports System.Data.SqlServerCe

Public Class clsConnection

    Private Shared gblAppConnectionString As String
    Private Shared gblAppSQLServer As String
    Private Shared gblAppSQLDatabase As String
    Private Shared gblAppSQLUserID As String
    Private Shared gblAppSQLPassword As String

    Private ConnectionString As String
    Private Cnn As New SqlConnection
    Private DatabaseName As String

    Public ReadOnly Property GetDatabaseName()
        Get
            GetDatabaseName = SQLDatabaseName
        End Get
    End Property

    Public Shared Sub CloseSQLConn(ByRef sConn As SqlConnection)
        If sConn.State = Data.ConnectionState.Open Then
            sConn.Close()
        End If
    End Sub

    Public Shared Sub CloseSQLAdapter(ByRef sAdpt As SqlDataAdapter)
        If Not IsNothing(sAdpt) Then
            sAdpt.Dispose()
            sAdpt = Nothing
        End If
    End Sub

    Public Shared Sub CloseSQLCmd(ByRef sCmd As SqlCommand)
        If Not IsNothing(sCmd) Then
            sCmd.Dispose()
            sCmd = Nothing
        End If
    End Sub

    Public Shared Sub CloseSQLRdr(ByRef sRdr As SqlDataReader)
        If Not IsNothing(sRdr) Then
            sRdr.Close()
            sRdr = Nothing
        End If
    End Sub

    Public Shared Sub CloseSQLCERdr(ByRef sRdr As SqlCeDataReader)
        If Not IsNothing(sRdr) Then
            sRdr.Close()
            sRdr = Nothing
        End If
    End Sub

    Public Shared WriteOnly Property SetAppConnString() As String
        Set(ByVal Value As String)
            gblAppConnectionString = Value
            SplitAppInfor(Value)
        End Set
    End Property

    Private Shared WriteOnly Property SetAppSQLServer() As String
        Set(ByVal Value As String)
            gblAppSQLServer = Value
        End Set
    End Property

    Private Shared WriteOnly Property SetAppSQLDatabase() As String
        Set(ByVal Value As String)
            gblAppSQLDatabase = Value
        End Set
    End Property

    Private Shared WriteOnly Property SetAppSQLUserID() As String
        Set(ByVal Value As String)
            gblAppSQLUserID = Value
        End Set
    End Property

    Private Shared WriteOnly Property SetAppSQLPassword() As String
        Set(ByVal Value As String)
            gblAppSQLPassword = Value
        End Set
    End Property

    Private Shared Sub SplitAppInfor(ByVal ConnectionString As String)

        Dim xTemp As String = ConnectionString
        Dim xPos As Int16

        'Data Source=TDSSenior;Initial Catalog=NetITS;Persist Security Info=True;User ID=sa;Password=solution
        If InStr(xTemp, "Data Source=") > 0 Then
            xTemp = Mid(ConnectionString, InStr(ConnectionString, "Data Source="))
            xPos = InStr(xTemp, ";")
            SetAppSQLServer = xTemp.Substring(InStr(xTemp, "Data Source=") + Len("Data Source=") - 1, xPos - Len("Data Source=") - 1)

            xTemp = Mid(ConnectionString, InStr(ConnectionString, "Initial Catalog="))
            xPos = InStr(xTemp, ";")
            SetAppSQLDatabase = xTemp.Substring(InStr(xTemp, "Initial Catalog=") + Len("Initial Catalog=") - 1, xPos - Len("Initial Catalog=") - 1)

            xTemp = Mid(ConnectionString, InStr(ConnectionString, "User ID="))

            xPos = InStr(xTemp, ";")
            SetAppSQLUserID = xTemp.Substring(InStr(xTemp, "User ID=") + Len("User ID=") - 1, xPos - Len("User ID=") - 1)

            xTemp = Mid(ConnectionString, InStr(ConnectionString, "Password="))
            xPos = InStr(xTemp, ";")

            If xPos > 0 Then
                SetAppSQLPassword = xTemp.Substring(InStr(xTemp, "Password=") + Len("Password=") - 1, xPos - Len("Password=") - 1)

            Else
                SetAppSQLPassword = xTemp.Substring(InStr(xTemp, "Password=") + Len("Password=") - 1)
            End If

        End If

    End Sub
End Class
