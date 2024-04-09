Imports GrapeCity.ActiveReports

Public Class ViewerForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()

		'Windows フォーム デザイナ サポートに必要です。
		InitializeComponent()
	End Sub

	'使用されているリソースをクリーンアップします。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Public Sub LoadReport(ByVal report As SectionReport)
		arvMain.LoadDocument(report)
	End Sub
End Class
