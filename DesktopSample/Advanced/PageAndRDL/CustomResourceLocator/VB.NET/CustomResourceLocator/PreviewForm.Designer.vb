<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PreviewForm
	Inherits System.Windows.Forms.Form

	'フォームよりも優先され、コンポーネントリストの整理に廃棄してください。
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Windowsフォームデザイナで必要な
	Private components As System.ComponentModel.IContainer

	'メモ:次の手順では、Windowsフォームデザイナで必要です
	'は、Windowsフォームデザイナを使用して変更することができます。
	'は、コードエディタを使用してを変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PreviewForm))
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.SuspendLayout()
		'
		'arvMain
		'
		resources.ApplyResources(Me.arvMain, "arvMain")
		Me.arvMain.BackColor = System.Drawing.Color.WhiteSmoke
		Me.arvMain.CurrentPage = 0
		Me.arvMain.Name = "arvMain"
		Me.arvMain.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.arvMain.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ParametersPanel.Enabled = False
		Me.arvMain.Sidebar.ParametersPanel.Visible = False
		Me.arvMain.Sidebar.ParametersPanel.Width = 180
		'
		'
		'
		Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.SearchPanel.Width = 180
		'
		'
		'
		Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ThumbnailsPanel.Width = 180
		'
		'
		'
		Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.TocPanel.Enabled = False
		Me.arvMain.Sidebar.TocPanel.Visible = False
		Me.arvMain.Sidebar.TocPanel.Width = 180
		Me.arvMain.Sidebar.Width = 180
		'
		'PreviewForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.arvMain)
		Me.Name = "PreviewForm"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer
End Class
