﻿Imports System.Resources

	Partial Class SelectSummaryOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectSummaryOptions))
        Me.label1 = New System.Windows.Forms.Label()
        Me.selectedGroups = New System.Windows.Forms.ListBox()
        Me.outputFields = New System.Windows.Forms.ListBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.masterReport = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.grandTotal = New System.Windows.Forms.CheckBox()
        Me.subtotals = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'label1
        '
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        '
        'selectedGroups
        '
        resources.ApplyResources(Me.selectedGroups, "selectedGroups")
        Me.selectedGroups.FormattingEnabled = True
        Me.selectedGroups.Name = "selectedGroups"
        Me.selectedGroups.SelectionMode = System.Windows.Forms.SelectionMode.None
        '
        'outputFields
        '
        resources.ApplyResources(Me.outputFields, "outputFields")
        Me.outputFields.FormattingEnabled = True
        Me.outputFields.Name = "outputFields"
        Me.outputFields.SelectionMode = System.Windows.Forms.SelectionMode.None
        '
        'label2
        '
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.Name = "label2"
        '
        'label3
        '
        resources.ApplyResources(Me.label3, "label3")
        Me.label3.Name = "label3"
        '
        'masterReport
        '
        resources.ApplyResources(Me.masterReport, "masterReport")
        Me.masterReport.Name = "masterReport"
        Me.masterReport.ReadOnly = True
        '
        'label4
        '
        resources.ApplyResources(Me.label4, "label4")
        Me.label4.Name = "label4"
        '
        'grandTotal
        '
        resources.ApplyResources(Me.grandTotal, "grandTotal")
        Me.grandTotal.Name = "grandTotal"
        Me.grandTotal.UseVisualStyleBackColor = True
        '
        'subtotals
        '
        resources.ApplyResources(Me.subtotals, "subtotals")
        Me.subtotals.Name = "subtotals"
        Me.subtotals.UseVisualStyleBackColor = True
        '
        'SelectSummaryOptions
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.subtotals)
        Me.Controls.Add(Me.grandTotal)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.masterReport)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.outputFields)
        Me.Controls.Add(Me.selectedGroups)
        Me.Controls.Add(Me.label1)
        Me.Description = "前のステップで選択したオプションを確認して下さい。また、レポートにどのような集計データを表示するかを選択して下さい。"
        Me.Name = "SelectSummaryOptions"
        Me.Tag = ""
        Me.Title = "集計設定と確認"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private label1 As System.Windows.Forms.Label
		Private selectedGroups As System.Windows.Forms.ListBox
		Private outputFields As System.Windows.Forms.ListBox
		Private label2 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private masterReport As System.Windows.Forms.TextBox
		Private label4 As System.Windows.Forms.Label
		Private grandTotal As System.Windows.Forms.CheckBox
		Private subtotals As System.Windows.Forms.CheckBox
	End Class
