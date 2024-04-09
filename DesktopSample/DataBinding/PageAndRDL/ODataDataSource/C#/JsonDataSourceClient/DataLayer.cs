using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ActiveReports.Samples.ODataDataSource
{
	internal class DataLayer
	{
		private static readonly string[] Sources = 
			{
			@"http://localhost:55749/Customers?$format=application/json;odata.metadata=none",
			@"https://services.odata.org/V4/Northwind/Northwind.svc/Customers?$select=CustomerID,%20CompanyName,%20ContactName,%20Address%20&%20$format=application/json;odata.metadata=none"
			};

		public static string CreateData(Service service)
		{
			string sourceUrl = Sources[(int)service];

			using (var httpClient = new HttpClient())
			{
				var request = new HttpRequestMessage(HttpMethod.Get, sourceUrl);
				var response = httpClient.SendAsync(request).Result;
				var json = response.Content.ReadAsStringAsync().Result;
				var jObject = (JObject)JsonConvert.DeserializeObject(json);
				foreach (var obj in jObject)
				{
					if (obj.Key == "value")
					{
						return "{\"" + obj.Key + "\":" + obj.Value + "}";
					}
				}
				return "";
			}
		}
	}
}
