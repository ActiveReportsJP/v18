Imports System.IO
Imports System.Xml
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Export.Pdf.Section


'CustomerExportPdf - Webアプリケーションで、ストリームを使用して、PDFへのエクスポートを紹介します。
Public Class CustomExportPdf
	Inherits System.Web.UI.Page
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Init
		'CODEGEN：この呼び出しは、ASP.NET Webフォームデザイナに必要です。

		InitializeComponent()
	End Sub
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim rpt As New SectionReport()
		Dim reportsPath As String = Path.Combine(Server.MapPath("~"), "Reports") + "\"
		rpt.ResourceLocator = New DefaultResourceLocator(New Uri(reportsPath))
		Dim xtr As New XmlTextReader(Path.Combine(reportsPath, "Invoice.rpx"))
		rpt.LoadLayout(xtr)
		xtr.Close()

		Try
			rpt.Run(False)
		Catch eRunReport As ReportException
			' レポートの作成に失敗した場合、クライアントにエラーメッセージを表示します。
			Response.Clear()
			Response.Write(GetLocalResourceObject("Error"))
			Response.Write(eRunReport.ToString())
			Return
		End Try

		' ブラウザに対してPDFドキュメントの適切なビューワを使用するように指定します。
		'  エクスポート形式別にコンテンツタイプを
		' 以下のように変更できます。
		'ExportType		(ContentType)
		'	PDF		   "application/pdf"  （小文字）
		'	RTF		  ("application/rtf")
		'	TIFF		  "image/tiff"	   （ブラウザとは別のビューワで表示される）
		'	HTML		  "message/rfc822"   （画像を含む圧縮されたHTMLページに適用する）
		'	Excel		("application/vnd.ms-excel")
		'	Excel		 "application/excel" （いずれかが動作される）
		'	Text		  ("text/plain")
		Response.ContentType = "application/pdf"


		Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF")

		' PDFエクスポートオブジェクトを作成します。
		Dim pdf As New PdfExport()
		' PDFの出力用のメモリストリームを作成します。
		Dim memStream As New System.IO.MemoryStream()
		' レポートをPDFにエクスポートします。
		pdf.Export(rpt.Document, memStream)
		'  出力ストリームにPDFのストリームを出力します。
		Response.BinaryWrite(memStream.ToArray())
		' バッファーされているすべての内容をクライアントへ送信します。
		Response.End()
	End Sub 'Page_Load

End Class
