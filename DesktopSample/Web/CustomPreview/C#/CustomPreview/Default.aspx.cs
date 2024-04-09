using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Document.Section;

namespace ActiveReports.Samples.Web.CustomPreview
{
	/// <summary>
	/// ActiveReports StandardのWebサンプルのスタートページを示します。
	/// </summary>
   
	public partial class DefaultPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// ここでページを初期化するユーザーコードを追加します。
		}


		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN：この呼び出しは、ASP.NET Webフォームデザイナに必要です。
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
