<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PDFDigitalSignature
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
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

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PDFDigitalSignature))
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.splitContainer = New System.Windows.Forms.SplitContainer()
		Me.lblVisibilityType = New System.Windows.Forms.Label()
		Me.cmbVisibilityType = New System.Windows.Forms.ComboBox()
		Me.chkTimeStamp = New System.Windows.Forms.CheckBox()
		Me.pdfExportButton = New System.Windows.Forms.Button()
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer.Panel1.SuspendLayout()
		Me.splitContainer.Panel2.SuspendLayout()
		Me.splitContainer.SuspendLayout()
		Me.SuspendLayout()
		'
		'arvMain
		'
		Me.arvMain.CurrentPage = 0
		resources.ApplyResources(Me.arvMain, "arvMain")
		Me.arvMain.Name = "arvMain"
		Me.arvMain.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.arvMain.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ThumbnailsPanel.Width = 200
		Me.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.TocPanel.Expanded = True
		Me.arvMain.Sidebar.TocPanel.Width = 200
		Me.arvMain.Sidebar.Width = 200
		'
		'splitContainer
		'
		resources.ApplyResources(Me.splitContainer, "splitContainer")
		Me.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer.Name = "splitContainer"
		'
		'splitContainer.Panel1
		'
		Me.splitContainer.Panel1.Controls.Add(Me.lblVisibilityType)
		Me.splitContainer.Panel1.Controls.Add(Me.cmbVisibilityType)
		Me.splitContainer.Panel1.Controls.Add(Me.chkTimeStamp)
		Me.splitContainer.Panel1.Controls.Add(Me.pdfExportButton)
		'
		'splitContainer.Panel2
		'
		Me.splitContainer.Panel2.Controls.Add(Me.arvMain)
		'
		'lblVisibilityType
		'
		resources.ApplyResources(Me.lblVisibilityType, "lblVisibilityType")
		Me.lblVisibilityType.Name = "lblVisibilityType"
		'
		'cmbVisibilityType
		'
		Me.cmbVisibilityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		resources.ApplyResources(Me.cmbVisibilityType, "cmbVisibilityType")
		Me.cmbVisibilityType.FormattingEnabled = True
		Me.cmbVisibilityType.Items.AddRange(New Object() {resources.GetString("cmbVisibilityType.Items"), resources.GetString("cmbVisibilityType.Items1"), resources.GetString("cmbVisibilityType.Items2"), resources.GetString("cmbVisibilityType.Items3")})
		Me.cmbVisibilityType.Name = "cmbVisibilityType"
		'
		'chkTimeStamp
		'
		resources.ApplyResources(Me.chkTimeStamp, "chkTimeStamp")
		Me.chkTimeStamp.Checked = True
		Me.chkTimeStamp.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkTimeStamp.Name = "chkTimeStamp"
		Me.chkTimeStamp.UseVisualStyleBackColor = True
		'
		'pdfExportButton
		'
		resources.ApplyResources(Me.pdfExportButton, "pdfExportButton")
		Me.pdfExportButton.Name = "pdfExportButton"
		Me.pdfExportButton.UseVisualStyleBackColor = True
		'
		'PDFDigitalSignature
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.splitContainer)
		Me.Name = "PDFDigitalSignature"
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.Panel1.PerformLayout()
		Me.splitContainer.Panel2.ResumeLayout(False)
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Private WithEvents arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Private WithEvents splitContainer As SplitContainer
	Private WithEvents lblVisibilityType As Label
	Private WithEvents cmbVisibilityType As ComboBox
	Private WithEvents chkTimeStamp As CheckBox
	Private WithEvents pdfExportButton As Button
End Class
