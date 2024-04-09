using System.Data.SQLite;

namespace ActiveReports.Samples.IListBinding.DataLayer
{
	internal class DataProvider
	{
		/// <summary>
		/// ProductCollection内のデータの読み込み用の新しい接続オブジェクトを返します。
		/// </summary>
		internal static SQLiteConnection NewConnection
			=> new SQLiteConnection(ActiveReports.Samples.IListBinding.DataLayer.Properties.Resources.ConnectionString);
	}
}
