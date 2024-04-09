using System;
using System.Collections.Specialized;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// OpenStreetMap(http://www.openstreetmap.org)からマップタイル画像を提供するサービスを表します。
	/// </summary>
	public sealed class OpenStreetMapTileProvider : BaseTileProvider, IMapTileProvider
	{
		private const string UrlTemplate = "http://a.tile.openstreetmap.org/{0}/{1}/{2}.png";

		/// <summary>
		/// プロバイダーの設定：
		/// Timeout- レスポンスタイムアウト
		/// UseSecureConnection.IsVisible - False
		/// Style.IsVisible - False
		/// </summary>
		public NameValueCollection Settings { get; private set; }

		public OpenStreetMapTileProvider()
		{
			Settings = new NameValueCollection();
			Settings.Set("UseSecureConnection.IsVisible", "False");
			Settings.Set("Style.IsVisible", "False");
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var url = string.Format(UrlTemplate, key.LevelOfDetail, key.Col, key.Row);
			var timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1;
			string userAgent = $"ActiveReports.Core/{GetType().Assembly.GetName().Version}";

			WebRequestHelper.DownloadDataAsync(url, timeout, (stream, contentType) => success(new MapTile(key, new ImageInfo(stream, contentType))), error, userAgent);
		}
	}
}
