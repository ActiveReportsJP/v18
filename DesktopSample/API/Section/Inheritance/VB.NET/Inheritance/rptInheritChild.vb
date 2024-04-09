Public Class rptInheritChild
	Inherits ActiveReports.Samples.Inheritance.rptInheritBase

	Public Sub New()

		'  ActiveReports デザイナにこの呼び出しは必要です。
		InitializeComponent()

		' InitializeComponent() 呼び出しの後初期化を追加します。
		AddHandler DataInitialize, AddressOf BaseReport_DataInitialize

		CsvPath = "..\..\..\Customers.csv"

	End Sub

#Region "ActiveReports Designer generated code"

	'ActiveReport がコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
		End If
		MyBase.Dispose(disposing)
	End Sub

	'メモ: 以下のプロシージャは ActiveReport デザイナで必要です。
	'ActiveReport デザイナを使用して変更できます。  
	'コード エディタを使って変更しないでください。

#End Region
  
End Class
