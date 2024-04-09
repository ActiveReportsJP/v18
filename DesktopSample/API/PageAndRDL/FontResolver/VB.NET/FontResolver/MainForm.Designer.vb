<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
	Inherits System.Windows.Forms.Form

	'Dispose to clean up the components
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required designer variable.
	Private components As System.ComponentModel.IContainer

	'The following procedure Is required by Windows Form Designer
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
		Me.bottomPanel = New System.Windows.Forms.Panel()
		Me.reportViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.topPanel = New System.Windows.Forms.Panel()
		Me.panelButtonContainer = New System.Windows.Forms.Panel()
		Me.btnPdfExport = New System.Windows.Forms.Button()
		Me.btnPreview = New System.Windows.Forms.Button()
		Me.bottomPanel.SuspendLayout()
		Me.topPanel.SuspendLayout()
		Me.panelButtonContainer.SuspendLayout()
		Me.SuspendLayout()
		'
		'bottomPanel
		'
		Me.bottomPanel.Controls.Add(Me.reportViewer)
		Me.bottomPanel.Controls.Add(Me.topPanel)
		resources.ApplyResources(Me.bottomPanel, "bottomPanel")
		Me.bottomPanel.Name = "bottomPanel"
		'
		'reportViewer
		'
		Me.reportViewer.CurrentPage = 0
		resources.ApplyResources(Me.reportViewer, "reportViewer")
		Me.reportViewer.Name = "reportViewer"
		Me.reportViewer.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.reportViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.reportViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.reportViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ThumbnailsPanel.Width = 200
		Me.reportViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.reportViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.TocPanel.Expanded = True
		Me.reportViewer.Sidebar.TocPanel.Width = 200
		Me.reportViewer.Sidebar.Width = 200
		'
		'topPanel
		'
		Me.topPanel.Controls.Add(Me.panelButtonContainer)
		resources.ApplyResources(Me.topPanel, "topPanel")
		Me.topPanel.Name = "topPanel"
		'
		'panelButtonContainer
		'
		Me.panelButtonContainer.Controls.Add(Me.btnPdfExport)
		Me.panelButtonContainer.Controls.Add(Me.btnPreview)
		resources.ApplyResources(Me.panelButtonContainer, "panelButtonContainer")
		Me.panelButtonContainer.Name = "panelButtonContainer"
		'
		'btnPdfExport
		'
		resources.ApplyResources(Me.btnPdfExport, "btnPdfExport")
		Me.btnPdfExport.Name = "btnPdfExport"
		Me.btnPdfExport.UseVisualStyleBackColor = True
		'
		'btnPreview
		'
		resources.ApplyResources(Me.btnPreview, "btnPreview")
		Me.btnPreview.Name = "btnPreview"
		Me.btnPreview.UseVisualStyleBackColor = True
		'
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
		Me.Controls.Add(Me.bottomPanel)
		Me.Name = "MainForm"
		Me.bottomPanel.ResumeLayout(False)
		Me.topPanel.ResumeLayout(False)
		Me.panelButtonContainer.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents bottomPanel As System.Windows.Forms.Panel
	Private WithEvents panelButtonContainer As System.Windows.Forms.Panel
	Private WithEvents btnPdfExport As System.Windows.Forms.Button
	Private WithEvents btnPreview As System.Windows.Forms.Button

	Private WithEvents topPanel As System.Windows.Forms.Panel
	Friend WithEvents reportViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer

End Class
