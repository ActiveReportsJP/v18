using System.Windows.Forms;
namespace ActiveReports.Samples.ReportsGallery
{
	partial class ReportsForm
	{
	  
		private System.ComponentModel.IContainer components = null;

	   
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
	
		#region Windowsフォームデザイナは、コードを生成
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.treeView = new System.Windows.Forms.TreeView();
			this.reportViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.mainContainer.Panel1.SuspendLayout();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainContainer
			// 
			resources.ApplyResources(this.mainContainer, "mainContainer");
			this.mainContainer.Name = "mainContainer";
			this.mainContainer.Dock = DockStyle.Fill;
			this.mainContainer.Orientation = Orientation.Vertical;
			// 
			// mainContainer.Panel1
			// 
			this.mainContainer.Panel1.Controls.Add(this.treeView);
			// 
			// mainContainer.Panel2
			// 
			this.mainContainer.Panel2.Controls.Add(this.reportViewer);
			// 
			// treeView
			// 
			resources.ApplyResources(this.treeView, "treeView");
			this.treeView.Name = "treeView";
			this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
			// 
			// reportViewer
			// 
			this.reportViewer.CurrentPage = 0;
			resources.ApplyResources(this.reportViewer, "reportViewer");
			this.reportViewer.Name = "reportViewer";
			this.reportViewer.PreviewPages = 0;
			this.reportViewer.Dock = DockStyle.Fill;
			// 
			// 
			// 
			// 
			// 
			// 
			this.reportViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.reportViewer.Sidebar.ParametersPanel.Width = 180;
			// 
			// 
			// 
			this.reportViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.reportViewer.Sidebar.SearchPanel.Width = 180;
			// 
			// 
			// 
			this.reportViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.reportViewer.Sidebar.ThumbnailsPanel.Width = 180;
			// 
			// 
			// 
			this.reportViewer.Sidebar.TocPanel.ContextMenu = null;
			this.reportViewer.Sidebar.TocPanel.Width = 180;
			this.reportViewer.Sidebar.Width = 180;
			this.reportViewer.LoadCompleted += reportViewer_LoadCompleted;
			// 
			// ReportsForm
			// 
			resources.ApplyResources(this, "$this");			
			this.Controls.Add(this.mainContainer);
			this.Name = "ReportsForm";
			this.Load += new System.EventHandler(this.ReportsForm_Load);
			this.mainContainer.Panel1.ResumeLayout(false);
			this.mainContainer.Panel2.ResumeLayout(false);		  
			this.mainContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.TreeView treeView;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportViewer;
	}
}
