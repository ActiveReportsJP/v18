﻿Imports System.Resources

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
	Inherits System.Windows.Forms.Form

	'
	'フォームはコンポーネント一覧をクリーンアップするためにdisposeをオーバーライドします。
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'必要なデザイナ変数です。
	Private components As System.ComponentModel.IContainer

	'注：以下のプロシージャはWindowsフォームデザイナで必要です
	'これは、Windowsフォームデザイナを使って変更することができます。
	'それはコードエディタを使用して変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.ReportPreview1 = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.SuspendLayout()
		'
		'ReportPreview1
		'
		Me.ReportPreview1.CurrentPage = 0
		Me.ReportPreview1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ReportPreview1.Location = New System.Drawing.Point(0, 0)
		Me.ReportPreview1.Name = "ReportPreview1"
		Me.ReportPreview1.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.ReportPreview1.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.ReportPreview1.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.ReportPreview1.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.ThumbnailsPanel.Width = 200
		Me.ReportPreview1.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.ReportPreview1.Sidebar.TocPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.TocPanel.Expanded = True
		Me.ReportPreview1.Sidebar.TocPanel.Width = 200
		Me.ReportPreview1.Sidebar.Width = 200
		Me.ReportPreview1.Size = New System.Drawing.Size(660, 467)
		Me.ReportPreview1.TabIndex = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(660, 467)
		Me.Controls.Add(Me.ReportPreview1)
		Me.Name = "MainForm"
		Me.Text = "Xmlデータソースサンプル"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ReportPreview1 As GrapeCity.ActiveReports.Viewer.Win.Viewer

End Class
