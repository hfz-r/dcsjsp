' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
<ServiceContract()>
<XmlSerializerFormat()>
Public Interface IDCSWebService

    '----------User Management-----------------------------------------------------------------

    <OperationContract()>
    Function isConnected() As Boolean

    <OperationContract()>
    Function isOracleConnected() As Boolean

    <OperationContract()>
    Function getTime() As String

    <OperationContract()>
    Function getTimeSet() As String

    <OperationContract()>
    Function verifyLogin(ByVal uname As String, ByVal pwd As String) As Boolean


    <OperationContract()>
    Function getData(ByVal columns As String, ByVal tableName As String, ByVal whereSQL As String) As DataTable

    <OperationContract()>
    Function insertData(ByVal insertSQL As String) As Boolean



    <OperationContract()>
    Function updateData(ByVal updateSQL As String) As Boolean


End Interface

' Use a data contract as illustrated in the sample below to add composite types to service operations.

<DataContract()>
Public Class CompositeType

    <DataMember()>
    Public Property BoolValue() As Boolean

    <DataMember()>
    Public Property StringValue() As String

End Class
