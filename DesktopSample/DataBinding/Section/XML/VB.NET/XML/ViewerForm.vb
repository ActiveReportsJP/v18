'XMLベースのActiveReportのビューワウィンドウを表します。
Imports GrapeCity.ActiveReports

Public Class ViewerForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()

		'必要なデザイナ変数です。
		InitializeComponent()
	End Sub

	'使用されているリソースに後処理を実行します。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'レポートドキュメントをフォーム上のビューワに設定します。
	Public Sub LoadReport(ByVal report As SectionReport)
		arvMain.LoadDocument(report)
	End Sub

End Class 'ViewerForm
