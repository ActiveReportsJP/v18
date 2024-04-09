Imports System.ComponentModel.DataAnnotations

Public Class Customer
    Public Property CustomerID As String
    Public Property CompanyName As String
    Public Property ContactName As String
    Public Property ContactTitle As String
    Public Property Address As String
    Public Property City As String
    Public Property Region As String
    Public Property PostalCode As String
    Public Property Country As String
    Public Property Phone As String
    Public Property Fax As String
End Class

Public Class Root
    Public Property Customers As List(Of Customer)
End Class
