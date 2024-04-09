<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportsForm
	Inherits System.Windows.Forms.Form


	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub


	Private components As System.ComponentModel.IContainer


	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportsForm))
		Me.mainContainer = New System.Windows.Forms.SplitContainer()
		Me.reportViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.treeView = New System.Windows.Forms.TreeView()
		Me.mainContainer.Panel1.SuspendLayout()
		Me.mainContainer.Panel2.SuspendLayout()
		Me.mainContainer.SuspendLayout()
		Me.SuspendLayout()
		resources.ApplyResources(Me.mainContainer, "mainContainer")
		Me.mainContainer.Name = "mainContainer"
		Me.mainContainer.Panel1.Controls.Add(Me.treeView)
		Me.mainContainer.Panel2.Controls.Add(Me.reportViewer)
		Me.mainContainer.Dock = DockStyle.Fill
		Me.reportViewer.Dock = DockStyle.Fill
		Me.reportViewer.CurrentPage = 0
		resources.ApplyResources(Me.reportViewer, "reportViewer")
		Me.reportViewer.Name = "reportViewer"
		Me.reportViewer.PreviewPages = 0
		Me.reportViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ParametersPanel.Width = 180
		Me.reportViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.SearchPanel.Width = 180
		Me.reportViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ThumbnailsPanel.Width = 180
		Me.reportViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.TocPanel.Width = 180
		Me.reportViewer.Sidebar.Width = 180
		resources.ApplyResources(Me.treeView, "treeView")
		Me.treeView.Name = "treeView"
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.mainContainer)
		Me.Name = "ReportsForm"
		Me.mainContainer.Panel1.ResumeLayout(False)
		Me.mainContainer.Panel2.ResumeLayout(False)
		Me.mainContainer.ResumeLayout(False)
		Me.mainContainer.Orientation = Orientation.Vertical
		Me.ResumeLayout(False)
	End Sub

	Friend WithEvents mainContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents reportViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Friend WithEvents treeView As System.Windows.Forms.TreeView
End Class
