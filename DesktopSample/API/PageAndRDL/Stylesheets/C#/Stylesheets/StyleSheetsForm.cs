using System;
using System.IO;
using System.Windows.Forms;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.PageReportModel;
using StyleSheet = GrapeCity.ActiveReports.SectionReportModel.StyleSheet;

namespace ActiveReports.Samples.StyleSheets
{
	public partial class StyleSheetsForm : Form
	{
		private static string _externalStyleSheet = string.Empty;
		private const string RdlReport = "ReorderList.rdlx";
		private const string PageReport = "Delivery Slip.rdlx";
		private static readonly string OutputFolder = Application.StartupPath;
		private static readonly string ReportsPath = Path.GetFullPath(Path.Combine(OutputFolder,@"..\..\..\..\..\Reports"));
		private FileInfo _file;

		public StyleSheetsForm()
		{
			InitializeComponent();
		}

		private void buttonRunReport_Click(object sender, EventArgs e)
		{
			PageReport report = null;
			
			// 
			// ビューワに表示するレポートを選択します。
			if (radioButtonRDLReport.Checked)
				_file = new FileInfo(Path.Combine(ReportsPath, RdlReport));
			else if (radioButtonPageReport.Checked)
				_file = new FileInfo(Path.Combine(ReportsPath , PageReport));
			report = new PageReport(_file);

			if (radioButtonEmbeddedStyle.Checked)
			{
				report.Report.StyleSheetSource = StyleSheetSource.Embedded;
				report.Report.StyleSheetValue = "BaseStyle";
			}
			else
			{
				report.Report.StyleSheetSource = StyleSheetSource.External;
			} 
			if (radioButtonOrangeStyle.Checked)
			{
				report.Report.StyleSheetValue = Path.Combine(ReportsPath, "ModernStyle.rdlx-styles");
			}
			if (radioButtonGreenStyle.Checked)
			{
				report.Report.StyleSheetValue = Path.Combine(ReportsPath, "FaxSheetStyle.rdlx-styles");
			}
			if (radioButtonBlueStyle.Checked)
			{
				report.Report.StyleSheetValue = Path.Combine(ReportsPath, "HighContrastStyle.rdlx-styles");
			}
			if (radioButtonCustomStyle.Checked)
			{
				report.Report.StyleSheetValue = Path.Combine(ReportsPath, _externalStyleSheet);
			}

			var pageDocument = new PageDocument(report);
			reportViewer.LoadDocument(pageDocument);
		}

		private void buttonChooseExtStyle_Click(object sender, EventArgs e)
		{
			// 
			// レポートに適用する外部のスタイルシートを選択します。
			FileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Properties.Resources.FilterText;
			openFileDialog.InitialDirectory = ReportsPath;
			openFileDialog.CheckFileExists = true;

			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileInfo styleSheetFile = new FileInfo(openFileDialog.FileName);
				_externalStyleSheet = styleSheetFile.FullName;
				radioButtonCustomStyle.Text = Properties.Resources.StyleSheetText + styleSheetFile.Name;
				radioButtonCustomStyle.AutoEllipsis = true;
				radioButtonCustomStyle.Checked = true;
			}
		}
	}
}
