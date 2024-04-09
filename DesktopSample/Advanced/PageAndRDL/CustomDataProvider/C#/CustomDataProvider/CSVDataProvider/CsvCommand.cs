using System;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	///.NETフレームワークのCSVデータプロバイダの<see cref="IDbCommand"/>を実装する方法を提供します。
	/// </summary>
	public sealed class CsvCommand : DbCommand
	{
		private string _commandText;
		private DbConnection _connection;
		private int _commandTimeout;
		private CommandType _commandType;

		/// <summary>
		/// <see cref="CsvCommand"/>クラスの新しいインスタンスを作成します。
		/// </summary>
		public CsvCommand()
			: this(string.Empty)
		{
		}

		/// <summary>
		/// コマンドのテキストとともに<see cref="CsvCommand"/>クラスの新しいインスタンスを作成します。
		/// </summary>
		/// <param name="commandText">コマンドのテキスト。</param>
		public CsvCommand(string commandText)
			: this(commandText, null)
		{
		}

		/// <summary>
		/// コマンドのテキストとともに<see cref="CsvCommand"/>クラスの新しいインスタンスを作成します。<see cref="CsvConnection"/>.
		/// </summary>
		/// <param name="commandText">コマンドのテキスト。</ PARAM>
		/// <param name="connection">データソースへの接続を表す<see cref="CsvConnection"/>。</ PARAM>
		public CsvCommand(string commandText, CsvConnection connection)
		{
			_commandText = commandText;
			_connection = connection;
		}

		/// <summary>
		/// データソースで実行するコマンドを取得または設定します。
		/// </summary>
		public override string CommandText
		{
			get { return _commandText; }
			set { _commandText = value; }
		}

		/// <summary>
		/// コマンドの実行を終了して、エラーを生成するまでの待機時間を取得または設定します。
		/// </summary>
		public override int CommandTimeout
		{
			get { return _commandTimeout; }
			set { _commandTimeout = value; }
		}

		/// <summary>
		/// <see cref="CommandText"/>プロパティの解釈方法を示す値を取得または設定します。
		/// </summary>
		/// <remarks>We don't use it for Csv Data Provider.</remarks> 
		public override CommandType CommandType
		{
			get { return _commandType; }
			set { _commandType = value; }
		}

		/// <summary>
		/// <see cref="CsvConnection"/>に<see cref="CommandText"/>を送信し、<see cref="CommandBehavior"/>の1つの値を使用し、<see cref="CsvDataReader"/>を作成します。
		/// </summary>
		/// <param name="behavior"><see cref="CommandBehavior"/>値の1つ。</param>
		/// <returns><see cref="CsvDataReader"/>のオブジェクト。</returns>
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return new CsvDataReader(new StringReader(_commandText));
		}

		/// <summary>
		///定数に拡張されたパラメータを持つコマンドのテキストを返します。
		/// </summary>
		/// <returns>定数に拡張されたパラメータを持つコマンドのテキストを表す文字列。</returns>
		public string GenerateRewrittenCommandText()
		{
			return _commandText;
		}

		/// <summary>
		/// <see cref="CsvConnection"/>に<see cref="CommandText"/>を送信し、<see cref="CsvDataReader"/>を作成します。
		/// </summary>
		/// <returns><see cref="CsvDataReader"/>のオブジェクト。</returns>
		public new IDataReader ExecuteReader()
		{
			return ExecuteReader(CommandBehavior.SchemaOnly);
		}

		public override UpdateRowSource UpdatedRowSource
		{
			get { return UpdateRowSource.None; }
			set { }
		}

		protected override DbConnection DbConnection
		{
			get { return _connection; }
			set { _connection = value; }
		}

		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected override DbTransaction DbTransaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool DesignTimeVisible
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void Cancel()
		{
			//do nothing
		}

		public new IDbDataParameter CreateParameter()
		{
			//do nothing
			return null;
		}


		/// <summary>
		/// <see cref="CsvCommand"/>により使用されたリソースを解放します。
		/// </summary>
		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_connection != null)
				{
					_connection.Dispose();
					_connection = null;
				}
			}
		}

		public override void Prepare()
		{
			//do nothing
		}

		public override int ExecuteNonQuery()
		{
			return 0;
		}

		public override object ExecuteScalar()
		{
			//do nothing
			return null;
		}

		protected override DbParameter CreateDbParameter()
		{
			throw new NotImplementedException();
		}
	}
}
