using GrapeCity.ActiveReports.Export.Pdf.Page;
using GrapeCity.ActiveReports.Rendering.IO;
using ActiveReports.Samples.FontResolver.Properties;
using System;
using System.IO;
using System.Windows.Forms;
using Settings = GrapeCity.ActiveReports.Export.Pdf.Page.Settings;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.FontResolver
{
	public partial class MainForm : Form
	{
		private PageReport _pageReport = new PageReport();

		/// <summary>
		/// Form Constructor.
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Preview the Layer report and set the target device for the layer to the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPreview_Click(object sender, EventArgs e)
		{
			_pageReport.Load(new FileInfo("../../../../../../Reports/Barcodes.rdlx"));
			_pageReport.FontResolver = BarcodeFontResolver.Instance;
			reportViewer.LoadDocument(_pageReport.Document);
			btnPdfExport.Enabled = true;
		}

		/// <summary>
		/// Export the report displayed in Viewer to PDF.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPdfExport_Click(object sender, EventArgs e)
		{
			if (_pageReport != null)
			{
				Settings settings = new Settings();
				settings.HideToolbar = true;
				settings.HideMenubar = true;
				settings.HideWindowUI = true;
				saveFileDialog.Filter = Resources.PDFFilter;

				PdfRenderingExtension _renderingExtension = new PdfRenderingExtension();
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					if (File.Exists(saveFileDialog.FileName))
						File.Delete(saveFileDialog.FileName);

					FileStreamProvider _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(saveFileDialog.FileName)), Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
					_pageReport.Document.Render(_renderingExtension, _exportfile, settings);
				}
			}
		}
	}
}
