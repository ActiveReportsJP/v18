using GrapeCity.ActiveReports.Design;
namespace ActiveReports.Samples.CreateReport
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

		#region  Windowsフォームデザイナで生成されたコード


		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
			this.viewer1 = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.SuspendLayout();
			// 
			// viewer1
			// 
			this.viewer1.CurrentPage = 0;
			resources.ApplyResources(this.viewer1, "viewer1");
			this.viewer1.Name = "viewer1";
			this.viewer1.PreviewPages = 0;
			// 
			// viewer1.Sidebar.ParametersPanel
			//
			resources.ApplyResources(this.viewer1.Sidebar.ParametersPanel, "viewer1.Sidebar.ParametersPanel");
			this.viewer1.Sidebar.ParametersPanel.ContextMenu = null;
			this.viewer1.Sidebar.ParametersPanel.Width = 200;
			// 
			// viewer1.Sidebar.SearchPanel
			// 
			resources.ApplyResources(this.viewer1.Sidebar.SearchPanel, "viewer1.Sidebar.SearchPanel");
			this.viewer1.Sidebar.SearchPanel.ContextMenu = null;
			this.viewer1.Sidebar.SearchPanel.Width = 200;
			// 
			// viewer1.Sidebar.ThumbnailsPanel
			// 
			resources.ApplyResources(this.viewer1.Sidebar.ThumbnailsPanel, "viewer1.Sidebar.ThumbnailsPanel");
			this.viewer1.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.viewer1.Sidebar.ThumbnailsPanel.Width = 200;
			// 
			// viewer1.Sidebar.TocPanel
			// 
			resources.ApplyResources(this.viewer1.Sidebar.TocPanel, "viewer1.Sidebar.TocPanel");
			this.viewer1.Sidebar.TocPanel.ContextMenu = null;
			this.viewer1.Sidebar.TocPanel.Expanded = true;
			this.viewer1.Sidebar.TocPanel.Width = 200;
			this.viewer1.Sidebar.Width = 200;
			// 
			// ReportsForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.viewer1);
			this.Name = "ReportsForm";
			this.Load += new System.EventHandler(this.ReportsForm_Load);
			this.ResumeLayout(false);

		}
		#endregion
		private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer1;
		
	}
}
