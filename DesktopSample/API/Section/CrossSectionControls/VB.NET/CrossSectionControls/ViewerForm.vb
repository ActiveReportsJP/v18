Imports System.Text
Imports System.Xml
Imports GrapeCity.ActiveReports

Public Class ViewerForm

	Enum ReportType
		CrossSectionControls
		RepeatToFill
		PrintAtBottom
	End Enum

	Sub New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		InitializeComponent()
	End Sub

	Private Delegate Sub LoadReportInvoker(ByVal reportType As ReportType)

	Private Sub ViewerForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		' レポートが描画中に、フォームを開きます。
		BeginInvoke(New LoadReportInvoker(AddressOf LoadReport), ReportType.CrossSectionControls)
		BeginInvoke(New LoadReportInvoker(AddressOf LoadReport), ReportType.RepeatToFill)
		BeginInvoke(New LoadReportInvoker(AddressOf LoadReport), ReportType.PrintAtBottom)
	End Sub

	Private Sub LoadReport(ByVal rptType As ReportType)
		' 新しいInvoiceレポートをインスタンス化します。
		Dim report As New SectionReport
		report.LoadLayout(XmlReader.Create(My.Resources.Invoice))
		report.MaxPages = 10

		' 接続文字列にサンプルデータベースを参照します。
		CType(report.DataSource, Data.DbDataSource).ConnectionString = My.Resources.ConnectionString
		Select Case rptType
			Case ReportType.CrossSectionControls
				cscViewer.LoadDocument(report)
			Case ReportType.RepeatToFill
				CType(report.Sections("Detail"), SectionReportModel.Detail).RepeatToFill = True
				repeatToFillViewer.LoadDocument(report)
			Case ReportType.PrintAtBottom
				CType(report.Sections("customerGroupFooter"), SectionReportModel.GroupFooter).PrintAtBottom = True
				printAtBottomViewer.LoadDocument(report)
		End Select
	End Sub

End Class
