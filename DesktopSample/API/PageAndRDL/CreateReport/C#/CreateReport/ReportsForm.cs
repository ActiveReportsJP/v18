using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.CreateReport
{
	public partial class ReportsForm : Form
	{
		public ReportsForm()
		{
			InitializeComponent();
		}

		private void ReportsForm_Load(object sender, EventArgs e)
		{
			// ページレポートオブジェクトにレイアウトを読み込みます。
			PageReport report = LayoutBuilder.BuildReportLayout();
			// ページレポートオブジェクトにデータソースを追加します。
			report = LayoutBuilder.AddDataSetDataSource(report);
			// ストリームにページレポートオブジェクトを読み込みます。
			MemoryStream reportStream = LayoutBuilder.LoadReportToStream(report);
			reportStream.Position = 0;
			// ストリームをViewerにロードします。
			viewer1.LoadDocument(reportStream, GrapeCity.Viewer.Common.DocumentFormat.Rdlx);

			report.Dispose();
			reportStream.Dispose();
		}
	}
}
