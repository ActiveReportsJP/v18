using GrapeCity.ActiveReports;
using System;
using System.IO;
using System.Windows.Forms;

namespace ActiveReports.Samples.JsonDataSource
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		// レポート用の適当なデータを返す<see cref="PageDocument.LocateDataSource"/>のハンドラ。
		private void OnLocateDataSource(object sender, LocateDataSourceEventArgs args)
		{
			object data = null;
			var dataSourceName = args.DataSet.Name;
			if (dataSourceName == "DataSet1")
			{
				data = DataLayer.CreateData();
			}

			args.Data = data;
		}

		// レポートをロードして表示します。
		private void MainForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\..\testReport.rdlx");
			var definition = new PageReport(rptPath);
			definition.Document.LocateDataSource += OnLocateDataSource;
			reportPreview.ReportViewer.LoadDocument(definition.Document);
		}
	}
}
