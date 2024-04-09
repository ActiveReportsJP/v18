Imports System.Data.Common

''' <summary>
''' .NETフレームワークのCSVデータプロバイダの<see cref="DataProviderFactory"/>を実装する方法を提供します。
''' </summary>
Public Class CsvDataProviderFactory
	Inherits DbProviderFactory

	''' <summary>
	''' <see cref="CsvCommand"/>の新しいインスタンスを返します。
	''' </summary>
	Public Overrides Function CreateCommand() As DbCommand
		Return New CsvCommand()
	End Function

	''' <summary>
	''' <see cref="CsvConnection"/>の新しいインスタンスを返します。
	''' </summary>
	Public Overrides Function CreateConnection() As DbConnection
		Return New CsvConnection()
	End Function
End Class
