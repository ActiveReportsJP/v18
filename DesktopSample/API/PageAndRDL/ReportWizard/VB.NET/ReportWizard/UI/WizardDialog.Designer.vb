Partial Class WizardDialog
    ' <summary>
    '必要なデザイナ変数です。
    ' </summary>
    Private components As System.ComponentModel.IContainer = Nothing
    ' <summary>
    '使用中のリソースをすべてクリーンアップします。
    ' </summary>
    '<param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        For Each [step] As BaseStep In Steps
            [step].Dispose()
        Next
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardDialog))
        Me.stepPanel = New System.Windows.Forms.Panel()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.back = New System.Windows.Forms.Button()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.stepDescription = New System.Windows.Forms.Label()
        Me.stepTitle = New System.Windows.Forms.Label()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'stepPanel
        '
        resources.ApplyResources(Me.stepPanel, "stepPanel")
        Me.stepPanel.MaximumSize = New System.Drawing.Size(638, 270)
        Me.stepPanel.MinimumSize = New System.Drawing.Size(638, 270)
        Me.stepPanel.Name = "stepPanel"
        '
        'btnNext
        '
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Name = "btnNext"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'back
        '
        resources.ApplyResources(Me.back, "back")
        Me.back.Name = "back"
        Me.back.UseVisualStyleBackColor = True
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.Transparent
        Me.panel1.BackgroundImage = Global.ActiveReports.Samples.ReportWizard.Resources.Bg_Blue
        Me.panel1.Controls.Add(Me.stepDescription)
        Me.panel1.Controls.Add(Me.stepTitle)
        resources.ApplyResources(Me.panel1, "panel1")
        Me.panel1.Name = "panel1"
        '
        'stepDescription
        '
        resources.ApplyResources(Me.stepDescription, "stepDescription")
        Me.stepDescription.Name = "stepDescription"
        Me.stepDescription.UseMnemonic = False
        '
        'stepTitle
        '
        resources.ApplyResources(Me.stepTitle, "stepTitle")
        Me.stepTitle.Name = "stepTitle"
        Me.stepTitle.UseMnemonic = False
        '
        'WizardDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.back)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.stepPanel)
        Me.Controls.Add(Me.panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WizardDialog"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private panel1 As System.Windows.Forms.Panel
    Private stepPanel As System.Windows.Forms.Panel
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents back As System.Windows.Forms.Button
    Private stepDescription As System.Windows.Forms.Label
    Private stepTitle As System.Windows.Forms.Label
End Class
