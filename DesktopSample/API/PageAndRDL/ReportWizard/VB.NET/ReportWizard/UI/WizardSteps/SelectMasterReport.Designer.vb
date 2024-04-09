Partial Class SelectMasterReport
	' <summary>
	'必要なデザイナ変数です。
	' </summary>
	Private components As System.ComponentModel.IContainer = Nothing
	' <summary>
	'使用中のリソースをすべてクリーンアップします。
	' </summary>
	'<param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso Not ((components) Is Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub
	' <summary>
	'デザイナサポートに必要なメソッド - 変更しない
	'コードエディタでは、このメソッドの内容を表示します。
	'</summary>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectMasterReport))
		Me.tipControl1 = New ActiveReports.Samples.ReportWizard.TipControl()
		Me.reportList = New System.Windows.Forms.ListBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'tipControl1
		'
		resources.ApplyResources(Me.tipControl1, "tipControl1")
		Me.tipControl1.Name = "tipControl1"
		'
		'reportList
		'
		Me.reportList.DisplayMember = "Title"
		Me.reportList.FormattingEnabled = True
		resources.ApplyResources(Me.reportList, "reportList")
		Me.reportList.Name = "reportList"
		'
		'label1
		'
		resources.ApplyResources(Me.label1, "label1")
		Me.label1.Name = "label1"
		'
		'SelectMasterReport
		'
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.tipControl1)
		Me.Controls.Add(Me.reportList)
		Me.Controls.Add(Me.label1)
		Me.Description = "分析するレポートのタイプを選択してください。"
		Me.Name = "SelectMasterReport"
		Me.Tag = ""
		Me.Title = "レポートタイプの選択"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Private label1 As System.Windows.Forms.Label
	Private WithEvents reportList As System.Windows.Forms.ListBox
	Private tipControl1 As TipControl
End Class
