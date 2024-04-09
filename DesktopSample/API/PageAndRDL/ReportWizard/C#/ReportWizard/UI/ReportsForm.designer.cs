using GrapeCity.ActiveReports.Design;
namespace ActiveReports.Samples.ReportWizard.UI 
{
	partial class ReportsForm
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		
	#region Windows Form Designer generated code
	 private void InitializeComponent()
   {
	   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
	   this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
	   this.SuspendLayout();
	   // 
	   // arvMain
	   // 
	   this.arvMain.CurrentPage = 0;
	   resources.ApplyResources(this.arvMain, "arvMain");
	   this.arvMain.Name = "arvMain";
	   this.arvMain.PreviewPages = 0;
	   this.arvMain.Dock = System.Windows.Forms.DockStyle.Fill;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.ParametersPanel, "arvMain.Sidebar.ParametersPanel");
	   this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
	   this.arvMain.Sidebar.ParametersPanel.Width = 200;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.SearchPanel, "arvMain.Sidebar.SearchPanel");
	   this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
	   this.arvMain.Sidebar.SearchPanel.Width = 200;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.ThumbnailsPanel, "arvMain.Sidebar.ThumbnailsPanel");
	   this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
	   this.arvMain.Sidebar.ThumbnailsPanel.Width = 200;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.TocPanel, "arvMain.Sidebar.TocPanel");
	   this.arvMain.Sidebar.TocPanel.ContextMenu = null;
	   this.arvMain.Sidebar.TocPanel.Width = 200;
	   this.arvMain.Sidebar.Width = 200;
	   this.arvMain.Size = new System.Drawing.Size(1000, 650);
	   // 
	   // ReportsForm
	   // 
	   resources.ApplyResources(this, "$this");
	   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	   this.ClientSize = new System.Drawing.Size(1000, 650);
	   this.Controls.Add(this.arvMain);
	   this.Name = "UnifiedDesignerForm";
	   this.Load += new System.EventHandler(this.ReportsForm_Load);
	   
	   this.ResumeLayout(false);

   }
   #endregion
	private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
	}
}
