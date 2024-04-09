Imports System.IO
Imports System.Text
Imports GrapeCity.ActiveReports

Public Class ReportsForm

    Public Sub New()
        '必要なデザイナ変数です。
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
        InitializeComponent()
    End Sub

    Private Sub ReportsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ページレポートオブジェクトにレイアウトを読み込みます。
        Dim report As PageReport = LayoutBuilder.BuildReportLayout()
        ' ページレポートオブジェクトにデータソースを追加します。
        report = LayoutBuilder.AddDataSetDataSource(report)
        ' ストリームにページレポートオブジェクトを読み込みます。
        Dim reportStream As MemoryStream = LayoutBuilder.LoadReportToStream(report)

        arvMain.LoadDocument(reportStream, GrapeCity.Viewer.Common.DocumentFormat.Rdlx)
        report.Dispose()
        reportStream.Dispose()
    End Sub

End Class
