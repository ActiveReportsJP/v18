using System;
using System.Collections.Specialized;
using System.Threading;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// http://cloudmade.com/ からマップタイル画像を提供するサービスを表します。 
	/// </summary>
	public sealed class CloudMadeTileProvider : BaseTileProvider, IMapTileProvider
	{
		private const string UrlTemplate = "http://a.tile.cloudmade.com/{0}/{1}/256/{2}/{3}/{4}.png";
		
		/// <summary>
		/// プロバイダーの設定：
		/// ApiKey - APIにアクセス用のキー
		/// ColorStyle - （http://maps.cloudmade.com/editor）からのスタイル番号
		/// Timeout- レスポンスタイムアウト
		/// </summary>
		public NameValueCollection Settings { get; private set; }
		
		public CloudMadeTileProvider()
		{
			Settings = new NameValueCollection();		   
			Settings.Set("UseSecureConnection.IsVisible", "False");
			Settings.Set("Style.IsVisible", "False");
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var parameters = GetParameters();
			
			var url = string.Format(UrlTemplate,
				parameters.Key,
				parameters.ColorStyle,
				key.LevelOfDetail,
				key.Col,
			key.Row);
			WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, (stream, contentType) => success(new MapTile(key, new ImageInfo(stream, contentType))), error);
		}

		#region Parameters

		private Parameters GetParameters()
		{
			var parameters = new Parameters
			{
				Key = Settings["ApiKey"] ?? "yourKey",
				ColorStyle = !string.IsNullOrEmpty(Settings["ColorStyle"]) ? int.Parse(Settings["ColorStyle"]) : 1,
				Timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1
			};

			return parameters;
		}

		class Parameters
		{
			public string Key;
			public int ColorStyle;
			public int Timeout;
		}

		#endregion
	}
}
