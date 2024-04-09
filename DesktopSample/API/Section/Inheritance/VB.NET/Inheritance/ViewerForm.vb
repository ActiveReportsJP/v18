Imports System.Text

Public Class ViewerForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		' Windows フォーム デザイナ サポートに必要です。
		InitializeComponent()

		' InitializeComponent() 呼び出しの後初期化を追加します。

	End Sub

	' ActiveReport がコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B 
	

	' メモ: 以下のプロシージャは ActiveReport デザイナで必要です。
	'ActiveReport デザイナを使用して変更できます。  
	'コード エディタを使って変更しないでください。
	
	Private Sub runTimeRptBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles runTimeRptBtn.Click
        Dim rpt As New rptInheritChild
        arvMain.LoadDocument(rpt)
	End Sub

	Private Sub designTimeRptBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles designTimeRptBtn.Click
        Dim rpt As New rptDesignChild
        arvMain.LoadDocument(rpt)
	End Sub

End Class
