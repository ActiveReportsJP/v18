using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Resources;
using System.Xml;

namespace ActiveReports.Samples.IListBinding
{
	/// <summary>
	/// BindIListToDataGridSampleの概要の説明です。
	/// </summary>>
	public partial class BindIListToDataGridSample : System.Windows.Forms.Form
	{
		private DataLayer.ProductCollection _productCollection;
		private System.ComponentModel.IContainer components = null;

		public BindIListToDataGridSample()
		{
			//
			// Windowsフォーム デザイナ サポートに必要です。
			//
			InitializeComponent();																										
		}

		/// <summary>
		/// btnGenerateReport_Click - DataLayer オブジェクトにバインドされたレポートを
		/// 表示するために、新しいViewerFormを開きます。
		/// </summary>
		private void btnGenerateReport_Click(object sender, System.EventArgs e)
		{
			// 新しいレポートオブジェクトを生成します。。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create("IlistReportSample.rpx"));
			rpt.DataSource = _productCollection;
			
			// ViewerForm上に表示するドキュメントを渡します。
			ViewerForm frmViewer = new ViewerForm();
			frmViewer.Show();
			frmViewer.LoadDocument(rpt);
		}

		private void BindIListToDataGridSample_Load(object sender, EventArgs e)
		{
			dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(Resource.DefaultFontName, 10);
		}
	}
}
