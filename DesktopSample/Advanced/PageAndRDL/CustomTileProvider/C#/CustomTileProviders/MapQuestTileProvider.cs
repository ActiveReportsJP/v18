using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Xml;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;
using ActiveReports.Samples.CustomTileProviders.Properties;

namespace ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// MapQuest(http://www.mapquest.com/)からマップタイル画像を提供するサービスを表します。
	/// </summary>
	public sealed class MapQuestTileProvider : BaseTileProvider, IMapTileProvider
	{
		private const string UrlTemplate = "https://www.mapquestapi.com/staticmap/v5/map?key={0}&center={1},{2}&zoom={3}&size=256,256&type={4}&format=png&scalebar=false";
		/// <summary>
		/// プロバイダーの設定：
		/// ApiKey - APIにアクセス用のキー
		/// Timeout- レスポンスタイムアウト
		/// Language - API language
		/// UseSecureConnection.IsVisible - False
		/// </summary>
		public NameValueCollection Settings { get; private set; }

		public MapQuestTileProvider()
		{
			Settings = new NameValueCollection();
			Settings.Set("UseSecureConnection.IsVisible", "False");
			Settings.Set("Styles", string.Join(";", Enum.GetNames(typeof(MapTypes))));
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var parameters = GetParameters();

			ValidateApiKey(parameters, () =>
			{
				var url = GetTileUrl(key, parameters);

				WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, (stream, contentType) => success(new MapTile(key, new ImageInfo(stream, contentType))), error);
			}, error);
		}

		private static string GetTileUrl(MapTileKey key, Parameters parameters)
		{
			var p = key.ToWorldPos();
			var url = string.Format(CultureInfo.InvariantCulture.NumberFormat, UrlTemplate,
				parameters.Key,
				p.Y,
				p.X,
				key.LevelOfDetail,
				parameters.MapType.ToString().ToLower());

			if (!string.IsNullOrEmpty(parameters.Language))
				url += "&language=" + parameters.Language;
			return url;
		}

		private void ValidateApiKey(Parameters parameters, Action success, Action<Exception> error)
		{
			using (var client = new HttpClient())
			{
				if (parameters.Timeout > 0)
				{
					client.Timeout = new TimeSpan(0, 0, 0, 0, parameters.Timeout);
				}

				try
				{
					var task = client.GetAsync("http://www.mapquestapi.com/geocoding/v1/reverse?location=0,0&outFormat=xml&key=" + parameters.Key).ContinueWith((responseTask) =>
					{
						var response = responseTask.Result;

						if(response.StatusCode == HttpStatusCode.Unauthorized ||
						response.StatusCode == HttpStatusCode.Forbidden)
						{
							error(new MapQuestServiceMapsKeyError());
							return;
						}

						try
						{
							var readTask = response.Content.ReadAsStreamAsync();
							readTask.Wait();

							//Copy data from buffer (It must be done, otherwise the buffer overflow and we stop to get repsonses).
							using (var reader = new StreamReader(readTask.Result))
							{
								var result = reader.ReadToEnd();
								var doc = new XmlDocument();
								doc.LoadXml(result);
								var infoNode = doc.SelectSingleNode("response/info");
								if (infoNode != null && infoNode["statusCode"] != null)
								{
									var statusCode = infoNode["statusCode"].InnerText;
									if (statusCode == "403")
									{
										error(new MapQuestServiceMapsKeyError());
										return;
									}
								}
							}

							success();
						}
						catch (Exception exception)
						{
							var webEx = exception as WebException;
							if (webEx != null)
							{
								if (webEx.Status == WebExceptionStatus.ProtocolError)
								{
									var webExResponse = webEx.Response as HttpWebResponse;
									if (webExResponse != null && webExResponse.StatusCode == HttpStatusCode.Forbidden)
									{
										error(new MapQuestServiceMapsKeyError());
										return;
									}
								}
							}

							error(exception);
						}
					});

					task.Wait();
				}
				catch(Exception exception)
				{
					error(exception);
				}
			}
		}

		#region Parameters
		private Parameters GetParameters()
		{
			var parameters = new Parameters
			{
				Key = Settings["ApiKey"] ?? "Fmjtd%7Cluur21ua2l%2C2x%3Do5-90t5h6",
				Language = Settings["Language"],
				Timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1
			};

			switch (Settings["Style"])
			{
				case "Road":
				case "Map":
					parameters.MapType = MapTypes.Map;
					break;
				case "Aerial":
				case "Sat":
					parameters.MapType = MapTypes.Sat;
					break;
				case "Hybrid":
				case "Hyb":
					parameters.MapType = MapTypes.Hyb;
					break;
			}

			return parameters;
		}

		class Parameters
		{
			public string Key;
			public MapTypes MapType;
			public string Language;
			public int Timeout;
		}

		//[DoNotObfuscateType]
		enum MapTypes
		{
			Map,
			Sat,
			Hyb
		}
		#endregion
	}

	internal class MapQuestServiceMapsKeyError : Exception
	{
		public MapQuestServiceMapsKeyError() : base(string.Format(Resources.MapQuestMapsKeyIsInvalid)) { }
	}
}
