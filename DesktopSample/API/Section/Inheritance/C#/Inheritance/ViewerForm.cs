using System;
using System.Text;
using System.Windows.Forms;

namespace ActiveReports.Samples.Inheritance
{
	/// <summary>
	/// ViewerForm の概要の説明です。
	/// </summary>
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		public ViewerForm()
		{
			//
			//Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();
		 }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
#if NET6_0_OR_GREATER
			Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
#endif
			Application.Run(new ViewerForm());
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			rptInheritChild rpt = new rptInheritChild();
			arvMain.LoadDocument(rpt);
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			rptDesignChild rpt = new rptDesignChild();
			arvMain.LoadDocument(rpt);
		}
	}
}
