﻿Imports GrapeCity.ActiveReports.Design
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportsForm
	Inherits System.Windows.Forms.Form
    'フォームはコンポーネント一覧をクリーンアップするためにdisposeをオーバーライドします。
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
    '必要なデザイナ変数です。
    Private components As System.ComponentModel.IContainer
    '注：以下のプロシージャはWindowsフォームデザイナで必要です
    'これは、Windowsフォームデザイナを使って変更することができます。
    'それはコードエディタを使用して変更しないでください。

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportsForm))
        Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
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
        Me.arvMain.Sidebar.ParametersPanel.Text = "Parameters"
        Me.arvMain.Sidebar.ParametersPanel.Width = 200
        '
        '
        '
        Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.SearchPanel.Text = "Search results"
        Me.arvMain.Sidebar.SearchPanel.Width = 200
        '
        '
        '
        Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
        Me.arvMain.Sidebar.ThumbnailsPanel.Width = 200
        Me.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1R
        '
        '
        '
        Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.TocPanel.Expanded = True
        Me.arvMain.Sidebar.TocPanel.Text = "Document map"
        Me.arvMain.Sidebar.TocPanel.Width = 200
        Me.arvMain.Sidebar.Width = 200
        '
        'ReportsForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.arvMain)
        Me.Name = "ReportsForm"
        Me.ResumeLayout(False)

    End Sub


    Friend WithEvents arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer
End Class
