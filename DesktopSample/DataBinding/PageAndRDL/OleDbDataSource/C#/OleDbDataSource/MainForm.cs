using System;
using System.Windows.Forms;
using System.IO;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.OleDbDataSource
{
	partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		// レポートをロードして表示します。
		private void MainForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\..\OleDBReport.rdlx");
			var pageReport = new PageReport(rptPath);
			reportPreview.LoadDocument(pageReport.Document);
		}
	}
}
