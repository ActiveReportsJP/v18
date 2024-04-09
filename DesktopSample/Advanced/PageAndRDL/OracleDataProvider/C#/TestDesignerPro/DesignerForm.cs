using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
﻿using ActiveReports.Samples.TestDesignerPro.Properties;
using GrapeCity.ActiveReports.Design;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using ActiveReports.Samples.TestDesignerPro.Export;
using GrapeCity.Viewer.Common.ViewModel;
using GrapeCity.ActiveReports.PageReportModel;
using System.Linq;

namespace ActiveReports.Samples.TestDesignerPro
{
	public partial class DesignerForm : Form
	{
		private string _reportName = "DemoReport.rdlx";
		private ExportForm _exportForm;
		private ExportViewerFactory _exportViewerFactory;
		private ToolStripMenuItem _exportToolStrip;
		private ToolStripButton _exportToolStripButton;

		public DesignerForm()
		{		
			InitializeComponent();

			_exportForm = new ExportForm();
			_exportViewerFactory = new ExportViewerFactory();
			reportDesigner.DesignerTabChanged += ReportDesigner_DesignerTabChanged;
			reportDesigner.NewReport(DesignerReportType.Page);//デザイナで空のページレポートを設定します。

			//ツールボックス、レポートエクスプローラおよびプロパティグリッドに項目を追加します。
			reportDesigner.Toolbox = reportToolbox;//ツールボックスをレポートデザイナに接続します。
			reportDesigner.LayoutChanged += OnDesignerLayoutChanged;
			reportExplorer.ReportDesigner = reportDesigner;//レポートエクスプローラをレポートデザイナに接続します
			reportDesigner.PropertyGrid = propertyGrid;//プロパティグリッドをレポートデザイナにに接続します。
			groupEditor.ReportDesigner = reportDesigner;
			layerList.ReportDesigner = reportDesigner;
			reportsLibrary.ReportDesigner = reportDesigner;

			//メニューに項目を追加します。
			ToolStrip toolStrip = reportDesigner.CreateToolStrips(new DesignerToolStrips[]
				{
					DesignerToolStrips.Menu
				})[0];
			toolStrip.Items.RemoveAt(2);
			ToolStripDropDownItem fileMenu = (ToolStripDropDownItem)toolStrip.Items[0];
			CreateFileMenu(fileMenu);
			AppendToolStrips(0, new ToolStrip[]
				{
					toolStrip
				});
			AppendToolStrips(1, reportDesigner.CreateToolStrips(new DesignerToolStrips[]
				{
					DesignerToolStrips.Edit, 
					DesignerToolStrips.Undo, 
					DesignerToolStrips.Zoom
				}));
			AppendToolStrips(1, new ToolStrip[]
				{
					CreateReportToolbar()
				});
			AppendToolStrips(2, reportDesigner.CreateToolStrips(new DesignerToolStrips[]
				{
					DesignerToolStrips.Format, 
					DesignerToolStrips.Layout
				}));
			reportDesigner.ReportChanged += (_, __) => UpdateReportName();
			InitGroupEditorToggle();
			SetExportState(false);
		}

		private void ReportDesigner_DesignerTabChanged(object sender, EventArgs e)
		{
			if (reportDesigner == null || reportDesigner.ActiveTab == DesignerTab.Designer)
			{
				SetExportState(false);
				return;
			}

			bool enabledExport = reportDesigner.ActiveTab == DesignerTab.Preview && reportDesigner.ReportViewer != null;
			SetExportState(enabledExport);
		}

		private void SetExportState(bool state)
		{
			if (_exportToolStrip != null) _exportToolStrip.Enabled = state;
			if (_exportToolStripButton != null) _exportToolStripButton.Enabled = state;
		}

		private void Export()
		{
			if (_exportForm == null || _exportViewerFactory == null || reportDesigner.ActiveTab != DesignerTab.Preview)
				return;

			_exportForm.Show(this, _exportViewerFactory.CreateExportViewer(reportDesigner.ReportViewer), DetermineReportType());
		}

		private ExportForm.ReportType DetermineReportType()
		{
			if (reportDesigner.ActiveTab == DesignerTab.Preview)
			{
				return DetermineReportTypeByOpenedReport();
			}

			var sectionReport = reportDesigner.Report as SectionReport;
			if (sectionReport != null) return ExportForm.ReportType.Section;

			var pageReport = reportDesigner.Report as PageReport;
			if (pageReport == null) return DefaultReportType;

			var report = pageReport.Report;

			if (report?.ViewerType == ViewerType.Dashboard) return ExportForm.ReportType.Dashboard;

			if (report?.ReportSections?.FirstOrDefault()?.Body == null) return DefaultReportType;

			ReportItemCollection items = report.ReportSections.FirstOrDefault()?.Body.ReportItems;
			return items != null && items.Count == 1 && items[0] is FixedPage ? ExportForm.ReportType.PageFpl : ExportForm.ReportType.PageCpl;
		}

		private const ExportForm.ReportType DefaultReportType = ExportForm.ReportType.PageCpl;

		private ExportForm.ReportType DetermineReportTypeByOpenedReport()
		{
			OpenedReport openedReport = reportDesigner.ReportViewer.OpenedReport;

			switch (openedReport)
			{
				case OpenedReport.Fpl:
					return ExportForm.ReportType.PageFpl;
				case OpenedReport.Dashboard:
					return ExportForm.ReportType.Dashboard;
				case OpenedReport.Section:
					return ExportForm.ReportType.Section;
				case OpenedReport.Cpl:
				case OpenedReport.None:
				default:
					return DefaultReportType;
			}
		}

		private void OnExport(object sender, EventArgs e) => Export();

		private void SetReportName(string reportName)
		{
			if (string.IsNullOrEmpty(reportName))
			{
				reportDesigner.IsDirty = false;
				_reportName = reportDesigner.Report is PageReport ? Resources.DefaultReportNameRdlx : Resources.DefaultReportNameRpx;
			}
			else
			{
				_reportName = reportName;
			}

			Text = Resources.SampleNameTitle + Path.GetFileName(_reportName) + (reportDesigner.IsDirty ? Resources.DirtySign : string.Empty);
		}

		private void UpdateReportName()
		{
			SetReportName(_reportName);
		}

		private int _groupEditorSize;
		private void InitGroupEditorToggle()
		{
			GroupEditorToggleButton.Image = Resources.GroupEditorHide;
			GroupEditorToggleButton.MouseEnter += (sender, args) => { GroupEditorToggleButton.BackColor = Color.LightGray; };
			GroupEditorToggleButton.MouseLeave += (sender, args) => { GroupEditorToggleButton.BackColor = Color.Gainsboro; };
			GroupEditorToggleButton.Click += (sender, args) =>
			{
				if (groupEditor.Visible)
				{
					GroupEditorToggleButton.Image = Resources.GroupEditorShow;
					_groupEditorSize = splitContainer1.ClientSize.Height - splitContainer1.SplitterDistance;
					splitContainer1.SplitterDistance = splitContainer1.ClientSize.Height - GroupEditorSeparator.Height - GroupEditorContainer.Padding.Vertical - splitContainer1.Panel2.Padding.Vertical - splitContainer1.SplitterWidth;
					splitContainer1.IsSplitterFixed = true;
					groupEditor.Visible = false;
				}
				else
				{
					GroupEditorToggleButton.Image = Resources.GroupEditorHide;
					splitContainer1.SplitterDistance = _groupEditorSize < splitContainer1.ClientSize.Height ? splitContainer1.ClientSize.Height - _groupEditorSize : splitContainer1.ClientSize.Height * 2 / 3;
					splitContainer1.IsSplitterFixed = false;
					groupEditor.Visible = true;
				}
			};

			groupEditor.VisibleChanged += (sender, args) => GroupPanelVisibility.SetToolTip(GroupEditorToggleButton, groupEditor.Visible ? Resources.HideGroupPanelToolTip : Resources.ShowGroupPanelToolTip);
		}

		//ToolStripDropDownItemにDropDownItemsを追加します。
		private void CreateFileMenu(ToolStripDropDownItem fileMenu)
		{
			fileMenu.DropDownItems.Clear();
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuNew, Resources.CmdNewReport, new EventHandler(OnNew), (Keys.Control | Keys.N)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuOpen, Resources.CmdOpen, new EventHandler(OnOpen), (Keys.Control | Keys.O)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuSave, Resources.CmdSave, new EventHandler(OnSave), (Keys.Control | Keys.S)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuSaveAs, Resources.CmdSaveAs, new EventHandler(OnSaveAs)));
			fileMenu.DropDownItems.Add(new ToolStripSeparator());
			_exportToolStrip = new ToolStripMenuItem(Resources.Export, Resources.CmdExport, new EventHandler(OnExport), (Keys.Control | Keys.E));
			fileMenu.DropDownItems.Add(_exportToolStrip);
			fileMenu.DropDownItems.Add(new ToolStripSeparator());
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuExit, null, new EventHandler(OnExit)));
		}

		private ToolStrip CreateReportToolbar()
		{
			_exportToolStripButton = CreateToolStripButton(Resources.Export, Resources.CmdExport, new EventHandler(OnExport), Resources.Export);

			return new ToolStrip(new ToolStripButton[]
			{
				CreateToolStripButton(Resources.MenuNew, Resources.CmdNewReport,new EventHandler(OnNew), Resources.MenuNew),
				CreateToolStripButton(Resources.MenuOpen, Resources.CmdOpen,new EventHandler(OnOpen), Resources.MenuOpen),
				CreateToolStripButton(Resources.MenuSave, Resources.CmdSave,new EventHandler(OnSave), Resources.MenuSave),
				_exportToolStripButton
			});
		}

		//デザイナで「新規作成」をクリックして、新しいレポートを開きます。
		private void OnNew(object sender, EventArgs e)
		{
			if (ConfirmSaveChanges())
			{
				reportDesigner.ReportChanged -= (_, __) => UpdateReportName();
				reportDesigner.ExecuteAction(DesignerAction.NewReport);
				reportDesigner.IsDirty = false;
				SetReportName(null);
				reportDesigner.ReportChanged += (_, __) => UpdateReportName();
			}
			ShowHideGroupEditor();
		}

		//デザイナで「開く」をクリックして、新しいレポートを開きます。
		private void OnOpen(object sender, EventArgs e)
		{
			if (!ConfirmSaveChanges())
			{
				return;
			}

			using (var openDialog = new OpenFileDialog())
			{
				openDialog.FileName = string.Empty;
				openDialog.Filter = Properties.Resources.RdlxFilter;
				if (openDialog.ShowDialog(this) == DialogResult.OK)
				{
					_reportName = openDialog.FileName;
					reportDesigner.LoadReport(new FileInfo(_reportName));
				}
			}
			ShowHideGroupEditor();
		}

		private void OnDesignerLayoutChanged(object sender, LayoutChangedArgs e)
		{
			// load or new report events
			if (e.Type == LayoutChangeType.ReportLoad || e.Type == LayoutChangeType.ReportClear)
			{
				reportToolbox.Reorder(reportDesigner);
				reportToolbox.EnsureCategories(); //check Data tools availability
				reportToolbox.Refresh();
			}
		}

		private void ShowHideGroupEditor()
		{
			if (reportDesigner.ReportType == DesignerReportType.Section)
			{
				splitContainer1.Panel2Collapsed = true;
			}
			else
			{
				splitContainer1.Panel2Collapsed = false;
			}
		}

		//デザイナで「上書きを保存」をクリックして、新しいレポートを開きます。
		private void OnSave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_reportName)
				|| string.IsNullOrEmpty(Path.GetDirectoryName(_reportName))
				|| !File.Exists(_reportName))
			{
				if (PerformSaveAs())
					reportDesigner.SaveReport(new FileInfo(_reportName));
			}
			else
			{
				reportDesigner.SaveReport(new FileInfo(_reportName));
			}
			SetReportName(_reportName);
		}

		//デザイナで「名前を付けて保存」をクリックして、新しいレポートを開きます。
		private void OnSaveAs(object sender, EventArgs e)
		{
			PerformSaveAs();
		}

		private bool PerformSaveAs()
		{
			using (var saveDialog = new SaveFileDialog())
			{
				saveDialog.Filter = GetSaveFilter();
				saveDialog.FileName = Path.GetFileName(_reportName);
				saveDialog.DefaultExt = ".rdlx";
				saveDialog.InitialDirectory = new DirectoryInfo(Application.ExecutablePath).Parent.Parent.Parent.FullName;
				if (saveDialog.ShowDialog() == DialogResult.OK)
				{
					_reportName = saveDialog.FileName;
					reportDesigner.SaveReport(new FileInfo(_reportName));
					reportDesigner.IsDirty = false;
					return true;
				}
			}

			return false;
		}

		private string GetSaveFilter()
		{
			switch (reportDesigner.ReportType)
			{
				case DesignerReportType.Section:
					return Resources.RpxFilter;
				case DesignerReportType.Page:
				case DesignerReportType.Rdl:
					return Resources.RdlxFilter;
				default:
					return Resources.RpxFilter;
			}
		}

		private void OnExit(object sender, EventArgs e)
		{
			Close();
		}

		//修正がデザイナーに読み込まれたレポートに加えられたかどうかをチェックする
		private bool ConfirmSaveChanges()
		{
			if (reportDesigner.IsDirty)
			{
				DialogResult result = MessageBox.Show(this, Resources.SaveConformation, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Cancel)
				{
					return false;
				}
				if (result == DialogResult.Yes)
				{
					return PerformSaveAs();
				}
			}
			return true;
		}
	   
		private void AppendToolStrips(int row, IList<ToolStrip> toolStrips)
		{
			ToolStripPanel topToolStripPanel = toolStripContainer.TopToolStripPanel;
			int num = toolStrips.Count;
			while (--num >= 0)
			{
				topToolStripPanel.Join(toolStrips[num], row);
			}
		}

		private static ToolStripButton CreateToolStripButton(string text, System.Drawing.Image image, EventHandler handler, string toolTip)
		{
			return new ToolStripButton(text, image, handler)
			{
				DisplayStyle = ToolStripItemDisplayStyle.Image,
				ToolTipText = toolTip,
				DoubleClickEnabled = true
			};
		}

		private void UnifiedDesignerForm_Load(object sender, EventArgs e)
		{
			reportDesigner.LoadReport(new FileInfo(_reportName));
		}

		private void CreateReportExplorer()
		{
			if (reportDesigner.ReportType == DesignerReportType.Section)
			{
				if (explorerpropertygridContainer.Panel1.Controls.Contains(reportExplorerTabControl))
				{
					reportExplorerTabControl.TabPages[0].SuspendLayout();
					explorerpropertygridContainer.Panel1.SuspendLayout();
					explorerpropertygridContainer.Panel1.Controls.Remove(reportExplorerTabControl);
					explorerpropertygridContainer.Panel1.Controls.Add(reportExplorer);
					reportExplorerTabControl.TabPages[0].ResumeLayout();
					explorerpropertygridContainer.Panel1.ResumeLayout();
				}
			}
			else if (!explorerpropertygridContainer.Panel1.Controls.Contains(reportExplorerTabControl))
			{
				reportExplorerTabControl.TabPages[0].SuspendLayout();
				explorerpropertygridContainer.Panel1.SuspendLayout();
				explorerpropertygridContainer.Panel1.Controls.Remove(reportExplorer);
				reportExplorerTabControl.TabPages[0].Controls.Add(reportExplorer);
				explorerpropertygridContainer.Panel1.Controls.Add(reportExplorerTabControl);
				reportExplorerTabControl.TabPages[0].ResumeLayout();
				explorerpropertygridContainer.Panel1.ResumeLayout();
			}
		}

		private void reportDesigner_LayoutChanged(object sender, LayoutChangedArgs e)
		{
			CreateReportExplorer();
		}
	}
}
