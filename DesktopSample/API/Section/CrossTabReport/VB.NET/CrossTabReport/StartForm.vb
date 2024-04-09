Imports System.Text
Imports System.Xml
Imports GrapeCity.ActiveReports

Public Class StartForm
	Inherits System.Windows.Forms.Form

	' 本サンプルでは、アンバウンドデータ、条件付きテキストの強調表示、
	' 列をまたがっているクロスタブビューおよびデータ集計を紹介します。
	Public Sub New()
		MyBase.New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		InitializeComponent()
	End Sub

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub StartForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		Try
			'レポートを作成し、ビューワに表示します。
			Dim rpt As New SectionReport()
			rpt.LoadLayout(XmlReader.Create(My.Resources.ProductWeeklySales))
			arvMain.LoadDocument(rpt)
		Catch ex As ReportException
			MessageBox.Show(ex.ToString(), Text)
		End Try
	End Sub

End Class
