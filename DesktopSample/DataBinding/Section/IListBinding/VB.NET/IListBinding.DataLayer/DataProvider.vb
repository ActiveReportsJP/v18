Imports System.Data.SQLite

Friend Class DataProvider

	'<summary>
	' ProductCollection内のデータの読み込み用の新しい接続オブジェクトを返します。
	'</summary>
	Friend Shared ReadOnly Property NewConnection() As SQLiteConnection
		Get
			Dim connectString As String = My.Resources.ConnectionString
			Return New SQLiteConnection(connectString)
		End Get
	End Property

End Class 'DataProvider
