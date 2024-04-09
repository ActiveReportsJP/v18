using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;

namespace ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	/// .NETフレームワークのCSVデータプロバイダの<see cref="IDbConnection"/>を実装する方法を提供します。
	/// </summary>
	public sealed class CsvConnection : DbConnection
	{
		private readonly string _localizedName;

		/// <summary>
		/// <see cref="CsvConnection"/>クラスの新しいインスタンスを作成します。
		/// </summary>
		public CsvConnection()
		{
			_localizedName = "Csv";
		}

		/// <summary>
		/// <see cref="CsvConnection"/>クラスの新しいインスタンスを作成します。
		/// </summary>
		///<param name="localizeName"><see cref="CsvConnection"/>のインスタンスのローカライズされた名前。</param>
		public CsvConnection(string localizeName)
		{
			_localizedName = localizeName;
		}

		/// <summary>
		/// データソースへの接続を開くために使用される文字列を取得または設定します。
		/// </summary>
		/// <remarks>Csvデータプロバイダに対する使用されません。</remarks>
		public override string ConnectionString
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// 接続の実行を終了して、エラーを生成するまでの待機時間を取得します。
		/// </summary>
		/// <remarks>Csvデータプロバイダに対する使用されません。</remarks>
		public override int ConnectionTimeout
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		///データソースのトランザクションを開始します。
		/// </summary>
		/// 新しいトランザクションを表すオブジェクト。
		/// <remarks>Csvデータプロバイダに対する使用されません。</remarks>
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return null;
		}

		/// <summary>
		/// データソース接続を開きます。
		/// </summary>
		/// <remarks>Csvデータプロバイダに対する使用されません。</remarks>
		public override void Open()
		{
		}

		/// <summary>
		/// データソースへの接続を閉じます。
		/// </summary>
		public override void Close()
		{
			Dispose();
		}

		protected override DbCommand CreateDbCommand()
		{
			return new CsvCommand(string.Empty);
		}

		/// <summary>
		///<see cref="CsvCommand"/>により使用されたリソースを解放します。
		/// </summary>
		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// <see cref="CsvConnection"/>のローカライズされた名前を取得します。
		/// </summary>
		public string LocalizedName
		{
			get { return _localizedName; }
		}

		public override ConnectionState State
		{
			get { return ConnectionState.Open; }
		}

		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		public override string DataSource
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override string ServerVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// この拡張機能の構成情報を指定します。
		/// </summary>
		/// <param name="configurationSettings">この設定の<see cref="NameValueCollection"/>。</param>
		public void SetConfiguration(NameValueCollection configurationSettings)
		{
		}

		public new IDbTransaction BeginTransaction(IsolationLevel il)
		{
			//do nothing
			return null;
		}

		public override void ChangeDatabase(string databaseName)
		{
			//do nothing
		}
	}
}
