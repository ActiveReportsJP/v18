Imports System
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.SectionReportModel

'XMLデータソースを使い、受注一覧レポートを作成します。
' サブレポートを使う方法と、XML階層構造を使用してレポートを作成する方法を示します。
Public Class StartForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		' 必要なデザイナ変数です。
		InitializeComponent()
	End Sub

	'Clean up any resources being used.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'btnCustomers_Clickイベントでラジオボタンを選択し、レポートのオブジェクトのデータを設定します。
	Private Sub btnCustomers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomers.Click
		Try
			'Dim rpt As New CustomersOrders()
			Dim rpt As New SectionReport()
			rpt.LoadLayout(XmlReader.Create(My.Resources.CustomersOrders))

			CType(rpt.DataSource, Data.XMLDataSource).FileURL = My.Resources.ConnectionString
			If radioAll.Checked Then 'データをすべて表示します。
				CType(rpt.DataSource, Data.XMLDataSource).RecordsetPattern = "//CUSTOMER"
			ElseIf radioID.Checked Then 'ID=2301に設定されたデータを表示します。
				CType(rpt.DataSource, Data.XMLDataSource).RecordsetPattern = "//CUSTOMER[@id = " + """" + "2301" + """" + "]"
			ElseIf radioEmail.Checked Then '有効なE-mailを含むデータを表示します。
				CType(rpt.DataSource, Data.XMLDataSource).RecordsetPattern = "//CUSTOMER[@email]"
			End If

			Dim frm As New ViewerForm()
			frm.Show()
			frm.LoadReport(rpt)
		Catch ex As ReportException
			MessageBox.Show(ex.ToString(), Text)
		End Try
	End Sub 'btnCustomers_Click

	'btnCustomersLeveled_Clickイベントで OrdersLeveledレポートを作成し、
	' データソースを設定します。
	Private Sub btnCustomersLeveled_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomersLeveled.Click
		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.OrdersLeveled))
		CType(rpt.DataSource, Data.XMLDataSource).FileURL = My.Resources.ConnectionString
		rpt.Run()
		Dim frm As New ViewerForm()
		frm.Show()
		frm.LoadReport(rpt)
	End Sub 'btnCustomersLeveled_Click

End Class 'StartForm
