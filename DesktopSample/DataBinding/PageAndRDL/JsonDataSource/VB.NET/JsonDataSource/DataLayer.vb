Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Text.Json

' Provides the data used in the sample.
Friend NotInheritable Class DataLayer
    Public Shared Function CreateData() As String
        Const sourceUrl As String = "http://localhost:10395/Service.asmx/GetJson"
        Dim responseText As String = Nothing

        Using httpClient = New HttpClient()
            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " &
                Convert.ToBase64String(Encoding.[Default].GetBytes("admin:1")))
            httpClient.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            Dim content = New StringContent(String.Empty, Encoding.UTF8, "application/json")
            Dim response = httpClient.PostAsync(sourceUrl, content).Result
            Dim json = response.Content.ReadAsStringAsync().Result
            Dim values = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(json)

            If values.ContainsKey("d") Then
                responseText = values("d")
            End If
        End Using

        Return responseText
    End Function
End Class
