Imports System
Imports System.Data.SQLite
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports GrapeCity.ActiveReports

'ActiveReports でアンバウンドデータの使用方法を紹介します。
Public Class MainForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		'Windowsフォームデザイナにこの呼び出しは必要です
		InitializeComponent()
	End Sub

	'フォームはコンポーネント一覧をクリーンアップするためにdisposeをオーバーライドします。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'tabDataBinding_SelectedIndexChangedイベントでは、切り替え時にビューワをクリアします。
	'タブ
	Private Sub tabDataBinding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDataBinding.SelectedIndexChanged
		'既存のページをクリアします。
		arvMain.Document = New Document.SectionDocument()
	End Sub


	'btnDataSet_Clickイベントでは、DataSetをデータソースとして使用します。
	Private Sub btnDataSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataSet.Click
		'レポートを作成します。
		Dim rpt As New SectionReport
		rpt.AddAssembly(GetType(SQLiteConnection).Assembly)
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundDSInvoice))
		rpt.PrintWidth = 6.5!
		'実行してレポートを表示します。
		arvMain.LoadDocument(rpt)
	End Sub 'btnDataSet_Click

	'btnDataReader_Clickイベントでは、DataReaderをデータソースとして使用します。
	Private Sub btnDataReader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataReader.Click
		'レポートを作成します。
		Dim rpt As New SectionReport
		rpt.AddAssembly(GetType(SQLiteConnection).Assembly)
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundDRInvoice))
		rpt.PrintWidth = 6.5!
		'実行してレポートを表示します。
		arvMain.LoadDocument(rpt)
	End Sub 'btnDataReader_Click

	'btnTextFile_Clickは - のデータソースとして[テキストファイルを使用して示しています。
	Private Sub btnTextFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTextFile.Click
		'レポートを作成します。
		Dim rpt As New SectionReport
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundTFInvoice))
		rpt.PrintWidth = 6.5!
		'実行してレポートを表示します。
		arvMain.LoadDocument(rpt)
	End Sub 'btnTextFile_Click

	'btnArray_Clickイベントでは、配列をデータソースとして使用します。
	Private Sub btnArray_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnArray.Click
		'レポートを作成します。
		Dim rpt As New SectionReport
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundDAInvoice))
		rpt.PrintWidth = 6.5!
		'実行してレポートを表示します。
		arvMain.LoadDocument(rpt)
	End Sub 'btnArray_Click

End Class
