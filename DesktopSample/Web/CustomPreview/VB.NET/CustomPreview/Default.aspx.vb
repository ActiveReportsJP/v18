Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Controls
Imports GrapeCity.ActiveReports.SectionReportModel
Imports GrapeCity.ActiveReports.Document.Section


	''' <summary>
	''' ActiveReports StandardのWebサンプルのスタートページを示します。
	''' </summary>

Partial Public Class DefaultPage
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(sender As Object, e As System.EventArgs)
		' ここでページを初期化するユーザーコードを追加します。
	End Sub

	Protected Overrides Sub OnInit(e As EventArgs)
		'
		' CODEGEN：この呼び出しは、ASP.NET Webフォームデザイナに必要です。
		'
		InitializeComponent()
		MyBase.OnInit(e)
	End Sub

	''' <summary>
	''' デザイナ サポートに必要なメソッドです。このメソッドの内容を
	''' コード エディタで変更しないでください。
	''' </summary>
	Private Sub InitializeComponent()

	End Sub
End Class
