Imports System.Web.Services
Imports System.Web.Script.Services

<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<CompilerServices.DesignerGenerated()>
<ComponentModel.ToolboxItem(False)>
<ScriptService()>
Public Class Service
    Inherits Services.WebService

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJson() As String
        Return My.Resources.Resource.customers
    End Function

End Class
