Imports GrapeCity.ActiveReports.Document

Public Class PreviewForm

	Dim _reportRuntime As PageDocument

	Public Sub New(ByVal runtime As PageDocument)
		' この呼び出しは、Windowsフォームデザイナで必要になります。
		InitializeComponent()
		_reportRuntime = runtime
		' 任意の初期化()の呼び出しで、InitializeComponent後に追加します。

	End Sub
	Private Sub PreviewForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If (Not _reportRuntime Is Nothing) Then
			arvMain.LoadDocument(_reportRuntime)
		End If
	End Sub
End Class
