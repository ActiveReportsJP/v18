using System;
using System.Web;

namespace ActiveReports.Samples.Web.CustomPreview
{
	/// <summary>
	/// CustomExportHtml - WebアプリケーションでHTMLへのエクスポートを紹介します。
	/// </summary>
	public partial class CustomExportHtml : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// リクエストによって変更される動的なレポートであるため、ページでのデータをキャッシュする必要があることをブラウザ、または、「network」に通知します。
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			// ブラウザに対してHTMLドキュメントの適切なビューワを使用するように指定します。
			Response.ContentType = "text/html";

			Response.Redirect("Reports/NwindLabels.rpx");
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
