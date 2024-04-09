Imports System.Xml
Imports GrapeCity.ActiveReports

'<summary>
' BindIListToDataGridSampleの概要の説明です。
'</summary>
Public Class BindIListToDataGridSample
	Inherits Form
	Dim _resourceManager As Resources.ResourceManager
	Public Sub New()
		MyBase.New()
		'Windowsフォームデザイナサポートに必要です。
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



	'<summary>
	' btnGenerateReport_Click - DataLayer オブジェクトにバインドされたレポートを
	' 表示するために、新しいViewerFormを開きます。
	'</summary>

	Private Sub btnGenerateReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerateReport.Click
		' 新しいレポートオブジェクトを作成します。
		Dim rpt As SectionReport
		rpt = New SectionReport
		rpt.LoadLayout(XmlReader.Create("IlistReportSample.rpx"))
		rpt.DataSource = productCollection

		' ViewerForm上に表示するドキュメントを渡します。
		Dim frmViewer As New ViewerForm
		frmViewer.Show()
		frmViewer.LoadReport(rpt)
	End Sub

	Private Sub BindIListToDataGridSample_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		dataGridView.ColumnHeadersDefaultCellStyle.Font = New Font(My.Resources.Resource.DefaultFontName, 10)

	End Sub
End Class
