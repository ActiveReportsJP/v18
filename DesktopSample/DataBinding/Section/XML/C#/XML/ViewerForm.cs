
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.XML
{
	/// <summary>
	/// XMLベースのActiveReportのビューワウィンドウを表します。
	/// </summary>
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		/// <summary>
		///必要なデザイナ変数です。
		/// </summary>
		public ViewerForm()
		{
			//Windowsフォームデザイナサポートに必要です。 
			InitializeComponent();
		}

		/// <summary>
		/// レポートドキュメントをフォーム上のビューワに設定します。
		/// </summary>
		public void LoadReport(SectionReport rpt)
		{
			arvMain.LoadDocument(rpt);
		}
	}
}
