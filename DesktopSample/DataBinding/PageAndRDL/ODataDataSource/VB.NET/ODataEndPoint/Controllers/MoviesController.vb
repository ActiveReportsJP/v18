Imports System.IO
Imports System.Text.Json
Imports System.Web.Http
Imports System.Web.OData

''' <summary>
''' Controller is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
''' </summary>
Public Class MoviesController
    Inherits ODataController
    Public Function [Get]() As IList(Of Movie)
        Dim connStr = Utility.UpdateConnectionString("$appPath$..\..\..\..\..\Data\movie.json")
        Dim jsonString = File.ReadAllText(connStr)
        Dim movies = JsonSerializer.Deserialize(Of List(Of Movie))(jsonString)

        Return movies
    End Function

End Class
