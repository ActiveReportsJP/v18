Imports System.Web

' CustomExportHtml - WebアプリケーションでHTMLへのエクスポートを紹介します。
Public Class CustomExportHtml
	Inherits System.Web.UI.Page
	'デザイナサポートに必要なメソッド - 
	'コードエディタでは、このメソッドの内容を変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Init
		'CODEGEN：この呼び出しは、ASP.NET Webフォームデザイナに必要です。

		InitializeComponent()
	End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		' リクエストによって変更される動的なレポートであるため、ページでのデータをキャッシュする必要があることをブラウザ、または、「network」に通知します。
		Response.Cache.SetCacheability(HttpCacheability.NoCache)
		' ブラウザに対してHTMLドキュメントの適切なビューワを使用するように指定します。
		Response.ContentType = "text/html"

		Response.Redirect("Reports/NwindLabels.rpx")
	End Sub	'Page_Load

End Class
