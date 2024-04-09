using GrapeCity.ActiveReports;
using System;
using System.IO;
using System.Windows.Forms;

namespace ActiveReports.Samples.XmlDataSource
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
			var dataSourceName = args.DataSet.Query.DataSourceName;
			var source = new DataLayer();
			if (dataSourceName == "BandedListDS")
			{
				data = source.CreateReader();
			}
			else if (dataSourceName == "CountrySalesDS")
			{
				data = source.CreateDocument();
			}

			args.Data = data;
		}

		// レポートをロードして表示します。
		private void MainForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\..\BandedListXML.rdlx");
			var definition = new PageReport(rptPath);
			definition.Document.LocateDataSource += OnLocateDataSource;
			reportPreview.ReportViewer.LoadDocument(definition.Document);
		}
	}
}
