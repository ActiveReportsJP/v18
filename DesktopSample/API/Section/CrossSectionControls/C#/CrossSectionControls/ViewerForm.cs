using System;
using System.Windows.Forms;
using System.Xml;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Data;
using GrapeCity.ActiveReports.SectionReportModel;

namespace ActiveReports.Samples.CrossSectionControls
{
	public partial class ViewerForm : Form
	{
		enum ReportType
		{
			CrossSectionControls,
			RepeatToFill,
			PrintAtBottom
		}

		private delegate void LoadReportInvoker(ReportType reportType);

		public ViewerForm()
		{
			InitializeComponent();
		}

		private void ViewerForm_Load(object sender, EventArgs e)
		{
			// レポートが描画中に、フォームを開きます。
			BeginInvoke(new LoadReportInvoker(LoadReport), ReportType.CrossSectionControls);
			BeginInvoke(new LoadReportInvoker(LoadReport), ReportType.PrintAtBottom);
			BeginInvoke(new LoadReportInvoker(LoadReport), ReportType.RepeatToFill);
		}

		void LoadReport(ReportType reportType)
		{
			// 新しいInvoiceレポートをインスタンス化します。
			var report = new SectionReport();
			report.LoadLayout(XmlReader.Create(Properties.Resources.Invoice));

			report.MaxPages = 10;

			// 接続文字列にサンプルデータベースを参照します。
			((DbDataSource)report.DataSource).ConnectionString = Properties.Resources.ConnectionString;			
			switch (reportType)
			{
				case ReportType.CrossSectionControls:
					cscViewer.LoadDocument(report);
					break;
				case ReportType.RepeatToFill:
					((Detail) report.Sections["Detail"]).RepeatToFill = true;
					repeatToFillViewer.LoadDocument(report);
					break;
				case ReportType.PrintAtBottom:
					((GroupFooter) report.Sections["customerGroupFooter"]).PrintAtBottom = true;
					printAtBottomViewer.LoadDocument(report);
					break;
			}
		}
	}
}
