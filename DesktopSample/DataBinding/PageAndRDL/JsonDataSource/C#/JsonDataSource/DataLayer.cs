using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ActiveReports.Samples.JsonDataSource
{
	// サンプルで使用するデータを提供します。
	internal sealed class DataLayer
	{
		public static string CreateData()
		{
			const string sourceUrl = @"http://localhost:30187/Service.asmx/GetJson";
			string responseText = null;

			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " +
					Convert.ToBase64String(Encoding.Default.GetBytes("admin:1")));

				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
				var response = httpClient.PostAsync(sourceUrl, content).Result;
				var json = response.Content.ReadAsStringAsync().Result;
				var values = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
				if (values.ContainsKey("d"))
				{
					responseText = values["d"];
				}
			}
			
			return responseText;
		}
	}
}
