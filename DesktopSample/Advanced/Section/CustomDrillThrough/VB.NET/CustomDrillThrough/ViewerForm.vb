Imports System
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports GrapeCity.ActiveReports

Public Class ViewerForm
	Inherits System.Windows.Forms.Form

	Private _loadMainReport As Boolean

	Public Sub New()
		MyBase.New()
		InitializeComponent()
		_loadMainReport = True
	End Sub

	Public Sub New(ByVal loadMainReport As Boolean)
		MyBase.New()
		InitializeComponent()
		_loadMainReport = loadMainReport
	End Sub

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	<STAThread()>
	Shared Sub Main()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		Application.Run(New ViewerForm())
	End Sub 'Main

	' arvMain_HyperLink: ビューワオブジェクトから送信されたハイパーリンクを処理します。
	Private Sub arvMain_HyperLink(ByVal sender As Object, ByVal e As GrapeCity.ActiveReports.Viewer.Win.HyperLinkEventArgs) Handles arvMain.HyperLink
		e.Handled = True
		Dim hyperlink As String = e.HyperLink.Split(New String() {":"}, StringSplitOptions.None)(1)
		Dim report As String = e.HyperLink.Split(New String() {":"}, StringSplitOptions.None)(0)
		If report = "DrillThrough1" Then
			' 顧客番号をクリックして、ドリルダウンレポートに移動し、顧客情報を表示します。
			Dim rpt2 As New SectionReport
			rpt2.LoadLayout(XmlReader.Create(My.Resources.DrillThrough1))
			Dim frm2 As New ViewerForm(False)
			rpt2.Parameters("customerID").Value = hyperlink
			frm2.arvMain.LoadDocument(rpt2)
			frm2.ShowDialog(Me)
		ElseIf report = "DrillThrough2" Then
			' 受注番号をクリックして受注情報を開きます。
			Dim rpt3 As New SectionReport
			rpt3.LoadLayout(XmlReader.Create(My.Resources.DrillThrough2))
			Dim frm3 As New ViewerForm(False)
			rpt3.Parameters("orderID").Value = hyperlink
			frm3.arvMain.LoadDocument(rpt3)
			frm3.ShowDialog(Me)
		End If
	End Sub 'arvMain_HyperLink

	' ViewerForm_Load - フォームで初期設定を行います。
	Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If _loadMainReport Then
			Dim rpt As New SectionReport
			rpt.LoadLayout(XmlReader.Create(My.Resources.DrillThroughMain))
			arvMain.LoadDocument(rpt)
		End If
	End Sub 'Form1_Load

End Class 'ViewerForm
