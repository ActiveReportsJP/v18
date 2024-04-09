using System;
using System.IO;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export.Pdf.Section;

namespace ActiveReports.Samples.Web.CustomPreview
{
	/// <summary>
	/// CustomerExportPdf - Webアプリケーションで、ストリームを使用して、PDFへのエクスポートを紹介します。
	/// </summary>
	public partial class CustomExportPdf : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			SectionReport rpt = new SectionReport();

			var reportsPath =Path.Combine(Server.MapPath("~"), "Reports") + @"\";
			rpt.ResourceLocator = new DefaultResourceLocator(new Uri(reportsPath));
			System.Xml.XmlTextReader xtr = new System.Xml.XmlTextReader(Path.Combine(reportsPath, "Invoice.rpx"));
			rpt.LoadLayout(xtr);
			xtr.Close();
			try
			{
				rpt.Run(false);
			}
			catch (ReportException eRunReport)
			{
				// レポートの作成に失敗した場合、クライアントにエラーメッセージを表示します。
				Response.Clear();
				Response.Write( GetLocalResourceObject("Error"));
				Response.Write(eRunReport.ToString());
				return;
			}

			// ブラウザに対してPDFドキュメントの適切なビューワを使用するように指定します。
			// エクスポート形式別にコンテンツタイプを
			// 以下のように変更できます。
			//	ExportType  ContentType
			//	PDF	   "application/pdf"  （小文字）
			//	RTF	   "application/rtf"
			//	TIFF	  "image/tiff"	   （ブラウザとは別のビューワで表示される）
			//	HTML	  "message/rfc822"   （画像を含む圧縮されたHTMLページに適用する）
			//	Excel	 "application/vnd.ms-excel"
			//	Excel	 "application/excel" （いずれかが動作される）
			//	Text	  "text/plain"  
			Response.ContentType = "application/pdf";

			Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");

			// PDFエクスポートオブジェクトを作成します。
			PdfExport pdf = new PdfExport();
			// PDFの出力用のメモリストリームを作成します。
			System.IO.MemoryStream memStream = new System.IO.MemoryStream();
			// レポートをPDFにエクスポートします。
			pdf.Export(rpt.Document, memStream);
			// 出力ストリームにPDFストリームを出力します。
			Response.BinaryWrite(memStream.ToArray());
			// バッファーされているすべての内容をクライアントへ送信します。
			Response.End();
		}

		
		override protected void OnInit(EventArgs e)
		{
			//
			//CODEGEN：この呼び出しは、ASP.NET Webフォームデザイナに必要です。
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
		}
	}
}
