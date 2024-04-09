using ActiveReports.Samples.CustomDataProvider.CsvDataProvider;
using System.Data.Common;

namespace ActiveReports.Samples.CustomDataProvider.CSVDataProvider
{
	/// <summary>
	/// .NETフレームワークのCSVデータプロバイダの<see cref="DataProviderFactory"/>を実装する方法を提供します。
	/// </summary>
	public class CsvDataProviderFactory : DbProviderFactory
	{
		/// <summary>
		/// <see cref="CsvCommand"/>の新しいインスタンスを返します。
		/// </summary>
		public override DbCommand CreateCommand()
		{
			return new CsvCommand();
		}

		/// <summary>
		/// <see cref="CsvConnection"/>の新しいインスタンスを返します。
		/// </summary>
		public override DbConnection CreateConnection()
		{
			return new CsvConnection();
		}
	}
}
