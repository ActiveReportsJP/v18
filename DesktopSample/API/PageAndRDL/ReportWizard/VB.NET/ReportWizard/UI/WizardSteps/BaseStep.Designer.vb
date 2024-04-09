	Partial Class BaseStep
		' <summary>
		'必要なデザイナ変数です。
		' </summary>
		Private components As System.ComponentModel.IContainer = Nothing
		' <summary>
		'使用中のリソースをすべてクリーンアップします。
		' </summary>
		' <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso Not ((components) Is Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
		' <summary>
		'デザイナサポートに必要なメソッド - 変更しない
		'コードエディタでは、このメソッドの内容を表示します。
		Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BaseStep))
		Me.SuspendLayout()
		'
		'BaseStep
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Name = "BaseStep"
		Me.Tag = ""
		Me.ResumeLayout(False)

	End Sub
End Class
