﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.8789
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.8789.
'
Namespace InventoryConsumptionWebService
    
    '''<remarks/>
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="perodua_eai_process_inventory_ws_processInventoryConsumptionService_Binder", [Namespace]:="http://services.perodua.com.my/EAI/Inventory/InventoryService/v1/")>  _
    Partial Public Class processInventoryConsumptionService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private eaiHeaderValueField As eaiHeader
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = "http://VAWBMTD0.perodua.com.my:5559/ws/perodua.eai.process.inventory.ws:processIn"& _ 
                "ventoryConsumptionService/perodua_eai_process_inventory_ws_processInventoryConsu"& _ 
                "mptionService_Port"
        End Sub
        
        Public Property eaiHeaderValue() As eaiHeader
            Get
                Return Me.eaiHeaderValueField
            End Get
            Set
                Me.eaiHeaderValueField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("eaiHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.InOut),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("perodua_eai_process_inventory_ws_processInventoryConsumptionService_Binder_proces"& _ 
            "sInventoryConsumption", RequestElementName:="processInventoryConsumptionReqDoc", RequestNamespace:="http://services.perodua.com.my/EAI/Inventory/InventoryService/v1/", ResponseElementName:="processInventoryConsumptionRespDoc", ResponseNamespace:="http://services.perodua.com.my/EAI/Inventory/InventoryService/v1/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function processInventoryConsumption(<System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal scannerBatchId As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal processType As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal processCode As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal orgID As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal inpF01 As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal inpF02 As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal inpF03 As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal inpF04 As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByVal inpF05 As String, <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> ByRef msgDesc As String) As <System.Xml.Serialization.XmlElementAttribute("msgCode", [Namespace]:="http://schemas.perodua.com.my/EAI/Inventory/Inventory/v1/", IsNullable:=true)> String
            Dim results() As Object = Me.Invoke("processInventoryConsumption", New Object() {scannerBatchId, processType, processCode, orgID, inpF01, inpF02, inpF03, inpF04, inpF05})
            msgDesc = CType(results(1),String)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Function BeginprocessInventoryConsumption(ByVal scannerBatchId As String, ByVal processType As String, ByVal processCode As String, ByVal orgID As String, ByVal inpF01 As String, ByVal inpF02 As String, ByVal inpF03 As String, ByVal inpF04 As String, ByVal inpF05 As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("processInventoryConsumption", New Object() {scannerBatchId, processType, processCode, orgID, inpF01, inpF02, inpF03, inpF04, inpF05}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndprocessInventoryConsumption(ByVal asyncResult As System.IAsyncResult, ByRef msgDesc As String) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            msgDesc = CType(results(1),String)
            Return CType(results(0),String)
        End Function
    End Class
    
    '''<remarks/>
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/common/soap/v1/"),  _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://schemas.perodua.com.my/EAI/common/soap/v1/", IsNullable:=false)>  _
    Partial Public Class eaiHeader
        Inherits System.Web.Services.Protocols.SoapHeader
        
        Private fromField As String
        
        Private toField As String
        
        Private appIdField As String
        
        Private msgTypeField As String
        
        Private msgIdField As String
        
        Private correlationIdField As String
        
        Private timestampField As String
        
        '''<remarks/>
        Public Property from() As String
            Get
                Return Me.fromField
            End Get
            Set
                Me.fromField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property [to]() As String
            Get
                Return Me.toField
            End Get
            Set
                Me.toField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property appId() As String
            Get
                Return Me.appIdField
            End Get
            Set
                Me.appIdField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property msgType() As String
            Get
                Return Me.msgTypeField
            End Get
            Set
                Me.msgTypeField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property msgId() As String
            Get
                Return Me.msgIdField
            End Get
            Set
                Me.msgIdField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property correlationId() As String
            Get
                Return Me.correlationIdField
            End Get
            Set
                Me.correlationIdField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property timestamp() As String
            Get
                Return Me.timestampField
            End Get
            Set
                Me.timestampField = value
            End Set
        End Property
    End Class
End Namespace
