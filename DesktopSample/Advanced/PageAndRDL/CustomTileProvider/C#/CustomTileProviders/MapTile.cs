using System.IO;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// 単一のマップタイルを表します。
	/// </summary>
	internal sealed class MapTile : IMapTile
	{
		/// <summary>
		/// <see cref="MapTile"/>の新しいインスタンスを初期化します。
		/// </summary>
		public MapTile(MapTileKey id, ImageInfo imageStream)
		{
			Id = id;
			Image = imageStream;
		}

		/// <summary>
		/// タイル識別子を取得します。
		/// </summary>
		public MapTileKey Id { get; private set; }

		/// <summary>
		/// タイル画像のストリームを取得します。
		/// </summary>
		public ImageInfo Image { get; private set; }
	}
}
