Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports System.IO
Imports System.Text.Json
Imports System.Web.Http
Imports System.Web.OData

''' <summary>
''' Controller is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
''' </summary>
<EnableQuery>
Public Class CustomersController
    Inherits ODataController
    Public Function [Get]() As IHttpActionResult
        Dim connStr = Utility.UpdateConnectionString("$appPath$..\..\..\..\..\Data\customers.json")
        Dim jsonString = File.ReadAllText(connStr)
        Dim root = JsonSerializer.Deserialize(Of Root)(jsonString)

        Return Ok(root.Customers)
    End Function
End Class
