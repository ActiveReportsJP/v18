Imports System.IO
Imports System.Data.Common

''' <summary>
'''.NETフレームワークのCSVデータプロバイダの<see cref="IDbCommand"/>を実装する方法を提供します。
''' </summary>
Public NotInheritable Class CsvCommand
	Inherits DbCommand
	Private _commandText As String
	Private _connection As DbConnection
	Private _commandTimeout As Integer
	Private _commandType As CommandType

	''' <summary>
	''' <see cref="CsvCommand"/>クラスの新しいインスタンスを作成します。
	''' </summary>
	Public Sub New()
		Me.New(String.Empty)
	End Sub

	''' <summary>
	''' コマンドのテキストとともに<see cref="CsvCommand"/>クラスの新しいインスタンスを作成します。
	''' </summary>
	''' <param name="commandText">コマンドのテキスト。</param>
	Public Sub New(commandText As String)
		Me.New(commandText, Nothing)
	End Sub

	''' <summary>
	''' コマンドのテキストとともに<see cref="CsvCommand"/>クラスの新しいインスタンスを作成します。<see cref="CsvConnection"/>.
	''' </summary>
	''' <param name="commandText">コマンドのテキスト。</ PARAM>
	''' <param name="connection">データソースへの接続を表す<see cref="CsvConnection"/>。</ PARAM>
	Public Sub New(commandText As String, connection As CsvConnection)
		_commandText = commandText
		_connection = connection
	End Sub

	''' <summary>
	''' データソースで実行するコマンドを取得または設定します。
	''' </summary>
	Public Overrides Property CommandText() As String
		Get
			Return _commandText
		End Get
		Set
			_commandText = Value
		End Set
	End Property

	''' <summary>
	''' コマンドの実行を終了して、エラーを生成するまでの待機時間を取得または設定します。
	''' </summary>
	Public Overrides Property CommandTimeout() As Integer
		Get
			Return _commandTimeout
		End Get
		Set
			_commandTimeout = Value
		End Set
	End Property

	''' <summary>
	''' <see cref="CommandText"/>プロパティの解釈方法を示す値を取得または設定します。
	''' </summary>
	''' <remarks>We don't use it for Csv Data Provider.</remarks> 
	Public Overrides Property CommandType() As CommandType
		Get
			Return _commandType
		End Get
		Set
			_commandType = Value
		End Set
	End Property

	''' <summary>
	''' <see cref="CsvConnection"/>に<see cref="CommandText"/>を送信し、<see cref="CommandBehavior"/>の1つの値を使用し、<see cref="CsvDataReader"/>を作成します。
	''' </summary>
	''' <param name="behavior"><see cref="CommandBehavior"/>値の1つ。</param>
	''' <returns><see cref="CsvDataReader"/>のオブジェクト。</returns>
	Protected Overrides Function ExecuteDbDataReader(behavior As CommandBehavior) As DbDataReader
		Return New CsvDataReader(New StringReader(_commandText))
	End Function

	''' <summary>
	'''定数に拡張されたパラメータを持つコマンドのテキストを返します。
	''' </summary>
	''' <returns>定数に拡張されたパラメータを持つコマンドのテキストを表す文字列。</returns>
	Public Function GenerateRewrittenCommandText() As String
		Return _commandText
	End Function

	''' <summary>
	''' <see cref="CsvConnection"/>に<see cref="CommandText"/>を送信し、<see cref="CsvDataReader"/>を作成します。
	''' </summary>
	''' <returns><see cref="CsvDataReader"/>のオブジェクト。</returns>
	Public Shadows Function ExecuteReader() As IDataReader
		Return mybase.ExecuteReader(CommandBehavior.SchemaOnly)
	End Function

	Public Overrides Property UpdatedRowSource() As UpdateRowSource
		Get
			Return UpdateRowSource.None
		End Get
		Set
		End Set
	End Property

	Protected Overrides Property DbConnection As DbConnection
		Get
			Return _connection
		End Get
		Set(value As DbConnection)
			_connection = value
		End Set
	End Property

	Protected Overrides ReadOnly Property DbParameterCollection As DbParameterCollection
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	Protected Overrides Property DbTransaction As DbTransaction
		Get
			Throw New NotImplementedException()
		End Get
		Set(value As DbTransaction)
			Throw New NotImplementedException()
		End Set
	End Property

	Public Overrides Property DesignTimeVisible As Boolean
		Get
			Throw New NotImplementedException()
		End Get
		Set(value As Boolean)
			Throw New NotImplementedException()
		End Set
	End Property

	Public Overrides Sub Cancel()
		'do nothing
	End Sub

	Public Shadows Function CreateParameter() As IDbDataParameter
		'do nothing
		Return Nothing
	End Function


	''' <summary>
	''' <see cref="CsvCommand"/>により使用されたリソースを解放します。
	''' </summary>
	Public Shadows Sub Dispose()
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Shadows Sub Dispose(disposing As Boolean)
		If disposing Then
			If _connection IsNot Nothing Then
				_connection.Dispose()
				_connection = Nothing
			End If
		End If
	End Sub

	Public Overrides Sub Prepare()
		'do nothing
	End Sub

	Public Overrides Function ExecuteNonQuery() As Integer
		Return 0
	End Function

	Public Overrides Function ExecuteScalar() As Object
		'do nothing
		Return Nothing
	End Function

	Protected Overrides Function CreateDbParameter() As DbParameter
		Throw New NotImplementedException()
	End Function
End Class
