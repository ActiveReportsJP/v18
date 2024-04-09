Imports System.Data.Common
Imports System.Data
Imports System.Collections.Specialized
Imports System


''' <summary>
''' .NETフレームワークのCSVデータプロバイダの<see cref="IDbConnection"/>を実装する方法を提供します。
''' </summary>
Public NotInheritable Class CsvConnection
	Inherits DbConnection
	Private ReadOnly _localizedName As String

	''' <summary>
	''' <see cref="CsvConnection"/>クラスの新しいインスタンスを作成します。
	''' </summary>
	Public Sub New()
		_localizedName = "Csv"
	End Sub

	''' <summary>
	''' <see cref="CsvConnection"/>クラスの新しいインスタンスを作成します。
	''' </summary>
	'''<param name="localizeName"><see cref="CsvConnection"/>のインスタンスのローカライズされた名前。</param>
	Public Sub New(localizeName As String)
		_localizedName = localizeName
	End Sub

	''' <summary>
	''' データソースへの接続を開くために使用される文字列を取得または設定します。
	''' </summary>
	''' <remarks>Csvデータプロバイダに対する使用されません。</remarks>
	Public Overrides Property ConnectionString() As String
		Get
			Return String.Empty
		End Get
		Set
		End Set
	End Property

	''' <summary>
	''' 接続の実行を終了して、エラーを生成するまでの待機時間を取得します。
	''' </summary>
	''' <remarks>Csvデータプロバイダに対する使用されません。</remarks>
	Public Overrides ReadOnly Property ConnectionTimeout() As Integer
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	''' <summary>
	'''データソースのトランザクションを開始します。
	''' </summary>
	''' 新しいトランザクションを表すオブジェクト。
	''' <remarks>Csvデータプロバイダに対する使用されません。</remarks>
	Protected Overrides Function BeginDbTransaction(isolationLevel As IsolationLevel) As DbTransaction
		Return Nothing
	End Function

	''' <summary>
	''' データソース接続を開きます。
	''' </summary>
	''' <remarks>Csvデータプロバイダに対する使用されません。</remarks>
	Public Overrides Sub Open()
	End Sub

	''' <summary>
	''' データソースへの接続を閉じます。
	''' </summary>
	Public Overrides Sub Close()
		Dispose()
	End Sub

	Protected Overrides Function CreateDbCommand() As DbCommand
		Return New CsvCommand(String.Empty)
	End Function

	''' <summary>
	'''<see cref="CsvCommand"/>により使用されたリソースを解放します。
	''' </summary>
	Public Shadows Sub Dispose()
		MyBase.Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	''' <summary>
	''' <see cref="CsvConnection"/>のローカライズされた名前を取得します。
	''' </summary>
	Public ReadOnly Property LocalizedName() As String
		Get
			Return _localizedName
		End Get
	End Property

	Public Overrides ReadOnly Property State() As ConnectionState
		Get
			Return ConnectionState.Open
		End Get
	End Property

	Public Overrides ReadOnly Property Database() As String
		Get
			Return String.Empty
		End Get
	End Property

	Public Overrides ReadOnly Property DataSource() As String
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	Public Overrides ReadOnly Property ServerVersion() As String
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	''' <summary>
	''' この拡張機能の構成情報を指定します。
	''' </summary>
	''' <param name="configurationSettings">この設定の<see cref="NameValueCollection"/>。</param>
	Public Sub SetConfiguration(configurationSettings As NameValueCollection)
	End Sub

	Public Shadows Function BeginTransaction(il As IsolationLevel) As IDbTransaction
		'do nothing
		Return Nothing
	End Function

	Public Overrides Sub ChangeDatabase(databaseName As String)
		'do nothing
	End Sub
End Class
