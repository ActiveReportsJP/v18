using System;
using System.Collections.Specialized;
using System.Globalization;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// Googleマップ(http://maps.google.com)からマップタイル画像を提供するサービスを表します。
	/// </summary>
	public sealed class GoogleMapsTileProvider : BaseTileProvider, IMapTileProvider
	{
		private const string UrlTemplate =
			"{3}://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size=256x256&sensor=false";

		/// <summary>
		/// プロバイダーの設定：
		/// ApiKey - APIにアクセス用のキー
		/// Timeout- レスポンスタイムアウト
		/// Style - Road/Aerial/Hybrid
		/// Language - API language
		/// </summary>
		public NameValueCollection Settings { get; private set; }

		public GoogleMapsTileProvider()
		{
			Settings = new NameValueCollection();
			Settings.Set("Styles", string.Join(";", Enum.GetNames(typeof(MapTypes))));
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var tilePosition = key.ToWorldPos();
			var parameters = GetParameters();

			var url = string.Format(CultureInfo.InvariantCulture.NumberFormat, UrlTemplate,
				tilePosition.Y,
				tilePosition.X,
				key.LevelOfDetail,
				parameters.UseSecureConnection ? "https" : "http");

			if (parameters.MapType.HasValue)
			{
				var maptype = Enum.GetName(typeof(MapTypes), parameters.MapType);
				if (maptype != null)
					url += "&maptype=" + maptype.ToLower();
			}

			if (!string.IsNullOrEmpty(parameters.Key))
				url += "&key=" + parameters.Key;

			if (!string.IsNullOrEmpty(parameters.Language))
				url += "&language=" + parameters.Language;

			WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, (stream, contentType) => success(new MapTile(key, new ImageInfo(stream, contentType))), error);
		}


		#region Parameters
		private Parameters GetParameters()
		{
			var parameters = new Parameters
			{
				Key = Settings["ApiKey"],
				Timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1,
				Language = Settings["Language"],
				UseSecureConnection = !string.IsNullOrEmpty(Settings["UseSecureConnection"])
						&& Convert.ToBoolean(Settings["UseSecureConnection"])
			};

			switch (Settings["Style"])
			{
				case "Road":
				case "Roadmap":
					parameters.MapType = MapTypes.Roadmap;
					break;
				case "Aerial":
				case "Satellite":
					parameters.MapType = MapTypes.Satellite;
					break;
				case "Hybrid":
					parameters.MapType = MapTypes.Hybrid;
					break;
				case "Terrain":
					parameters.MapType = MapTypes.Terrain;
					break;
			}

			return parameters;
		}

		class Parameters
		{
			public string Key;
			public MapTypes? MapType;
			public string Language;
			public bool UseSecureConnection;
			public int Timeout;
		}

		//[DoNotObfuscateType]
		enum MapTypes
		{
			Roadmap,
			Satellite,
			Hybrid,
			Terrain
		}
		#endregion
	}
}
