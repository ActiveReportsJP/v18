﻿Imports System.Resources

	Partial Class SelectOutputFields
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectOutputFields))
        Me.label1 = New System.Windows.Forms.Label()
        Me.availableOutputFields = New ActiveReports.Samples.ReportWizard.DragDropListBox()
        Me.addOutputField = New System.Windows.Forms.Button()
        Me.removeOutputField = New System.Windows.Forms.Button()
        Me.selectedOutputFields = New System.Windows.Forms.ListBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'label1
        '
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        '
        'availableOutputFields
        '
        Me.availableOutputFields.FormattingEnabled = True
        resources.ApplyResources(Me.availableOutputFields, "availableOutputFields")
        Me.availableOutputFields.Name = "availableOutputFields"
        '
        'addOutputField
        '
        resources.ApplyResources(Me.addOutputField, "addOutputField")
        Me.addOutputField.Name = "addOutputField"
        Me.addOutputField.UseVisualStyleBackColor = True
        '
        'removeOutputField
        '
        resources.ApplyResources(Me.removeOutputField, "removeOutputField")
        Me.removeOutputField.Name = "removeOutputField"
        Me.removeOutputField.UseVisualStyleBackColor = True
        '
        'selectedOutputFields
        '
        Me.selectedOutputFields.AllowDrop = True
        Me.selectedOutputFields.FormattingEnabled = True
        resources.ApplyResources(Me.selectedOutputFields, "selectedOutputFields")
        Me.selectedOutputFields.Name = "selectedOutputFields"
        '
        'label2
        '
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.Name = "label2"
        '
        'SelectOutputFields
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.selectedOutputFields)
        Me.Controls.Add(Me.removeOutputField)
        Me.Controls.Add(Me.addOutputField)
        Me.Controls.Add(Me.availableOutputFields)
        Me.Controls.Add(Me.label1)
        Me.Description = "レポートで表示するフィールドを選択してください。"
        Me.Name = "SelectOutputFields"
        Me.Title = "出力フィールドの選択"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private label1 As System.Windows.Forms.Label
		Friend WithEvents availableOutputFields As DragDropListBox
		Friend WithEvents addOutputField As System.Windows.Forms.Button
		Friend WithEvents removeOutputField As System.Windows.Forms.Button
		Friend WithEvents selectedOutputFields As System.Windows.Forms.ListBox
		Friend WithEvents label2 As System.Windows.Forms.Label
	End Class
