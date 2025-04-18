﻿using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export.Pdf.Page;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Rendering.IO;
using System;
using System.IO;
using System.Windows.Forms;

namespace ActiveReports.Samples.Layers
{
	public partial class LayersForm : Form
	{
		PageReport _pageReport = new PageReport();
		PageDocument _reportRuntime;
		TargetDevices _targetDevices;

		/// <summary>
		/// Layersフォームのコンストラクタ。
		/// </summary>
		public LayersForm()
		{
			InitializeComponent();
			_targetDevices = TargetDevices.All;
		}

		/// <summary>
		/// Layerレポートをプレビューして、レポートにレイヤーのターゲットデバイスの設定を追加します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPreview_Click(object sender, EventArgs e)
		{
			_pageReport.Load(new FileInfo("Layer.rdlx"));
			_reportRuntime = new PageDocument(_pageReport);
			_reportRuntime.PageReport.Report.Layers[1].TargetDevice = _targetDevices;
			reportViewer.LoadDocument(_reportRuntime);
			btnPdfExport.Enabled = true;
		}

		/// <summary>
		/// Viewerで表示されたレポートをPDFにエクスポートします。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPdfExport_Click(object sender, EventArgs e)
		{
			if(_reportRuntime!=null)
			{
				Settings settings = new Settings();
				settings.HideToolbar = true;
				settings.HideMenubar = true;
				settings.HideWindowUI = true;
				saveFileDialog.Filter =Properties.Resources.PDFFilter;

				PdfRenderingExtension _renderingExtension = new PdfRenderingExtension();
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					if(File.Exists(saveFileDialog.FileName))
					{
						File.Delete(saveFileDialog.FileName);
					}

					FileStreamProvider _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(saveFileDialog.FileName)), Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
					_reportRuntime.Render(_renderingExtension, _exportfile, settings);
				}
			}
		}

		/// <summary>
		/// チェックされたチェックボックスによってターゲットデバイスを追加または削除します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			_targetDevices =TargetDevices.None;
			btnPdfExport.Enabled = false;
			if(checkBoxPaper.Checked)
			{
				_targetDevices = _targetDevices ^ TargetDevices.Paper;
			}
			if(checkBoxPDF.Checked)
			{
				_targetDevices = _targetDevices ^ TargetDevices.Export;
			}
			if(checkBoxScreen.Checked)
			{
				_targetDevices = _targetDevices ^ TargetDevices.Screen;
			}
		}
	}
}
