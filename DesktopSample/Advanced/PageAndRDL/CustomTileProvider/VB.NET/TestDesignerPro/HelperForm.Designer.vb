<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HelperForm
	Inherits System.Windows.Forms.Form
	'
	'フォームはコンポーネント一覧をクリーンアップするためにdisposeをオーバーライドします。
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
	'必要なデザイナ変数です。
	Private components As System.ComponentModel.IContainer
	'注：以下のプロシージャはWindowsフォームデザイナで必要です
	'これは、Windowsフォームデザイナを使って変更することができます。
	'それはコードエディタを使用して変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HelperForm))
		Me.rtfHelp = New System.Windows.Forms.RichTextBox()
		Me.SuspendLayout()
		'
		'rtfHelp
		'
		resources.ApplyResources(Me.rtfHelp, "rtfHelp")
		Me.rtfHelp.Name = "rtfHelp"
		Me.rtfHelp.ReadOnly = True
		'
		'HelperForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.rtfHelp)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "HelperForm"
		Me.TopMost = True
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents rtfHelp As System.Windows.Forms.RichTextBox
End Class
