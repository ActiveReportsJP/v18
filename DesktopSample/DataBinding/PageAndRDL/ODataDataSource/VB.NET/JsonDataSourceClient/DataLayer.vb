Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Friend Class DataLayer

	Private Shared ReadOnly Sources As String() = {
		"http://localhost:55856/Customers?$format=application/json;odata.metadata=none",
		"https://services.odata.org/V4/Northwind/Northwind.svc/Customers?$select=CustomerID,%20CompanyName,%20ContactName,%20Address%20&%20$format=application/json;odata.metadata=none"
	}

	Public Shared Function CreateData(ByVal service As Service) As String
		Dim sourceUrl = Sources(CInt(service))

		Using httpClient = New HttpClient()
			Dim request = New HttpRequestMessage(HttpMethod.[Get], sourceUrl)
			Dim response = httpClient.SendAsync(request).Result
			Dim json = response.Content.ReadAsStringAsync().Result
			Dim jObject = CType(JsonConvert.DeserializeObject(json), JObject)

			For Each obj In jObject

				If obj.Key = "value" Then
					Return "{""" & obj.Key & """:" & obj.Value.ToString() & "}"
				End If
			Next

			Return ""
		End Using
	End Function
End Class
