using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.ObjectDataSource
{
	partial class MainForm : Form
	{
		private readonly IList<Year> _dataList;

		public MainForm()
		{
			_dataList = DataLayer.LoadData();

			InitializeComponent();
		}

		// レポート用の適当なデータを返す<see cref="PageDocument.LocateDataSource"/>のハンドラ。
		private void OnLocateDataSource(object sender, LocateDataSourceEventArgs args)
		{
			if (args.DataSet.Name == "Years") // ObjectsReport :bind collection to report
			{
				args.Data = _dataList;
			}
			else if (args.DataSet.Name == "Movies") // SubObjectsReport :bind subcollection to subreport
			{
				var yearReleased = 1970;
				foreach (var param in args.Parameters)
					if (param.Name == "YearReleased")
					{
						yearReleased = int.Parse(param.Value.ToString());
						break;
					}
				for (int i = 0; i < _dataList.Count; i++)
					if (_dataList[i].YearReleased == yearReleased)
					{
						args.Data = _dataList[i].Movies;
						break;
					}
			}
		}

		// レポートをロードして表示します。
		private void MainForm_Load(object sender, EventArgs e)
		{
			FileInfo rptPath = new FileInfo(@"..\..\..\ObjectsReport.rdlx");
			PageReport pageReport = new PageReport(rptPath);
			pageReport.Document.LocateDataSource += new LocateDataSourceEventHandler(OnLocateDataSource);
			reportPreview.LoadDocument(pageReport.Document);
		}
	}
}
