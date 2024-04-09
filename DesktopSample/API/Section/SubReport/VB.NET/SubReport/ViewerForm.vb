Imports System.Data.OleDb
Imports System.Data.SQLite
Imports System.Text
Imports System.Xml
Imports GrapeCity.ActiveReports

Public Class ViewerForm
	Inherits System.Windows.Forms.Form


	Public Sub New()
		MyBase.New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		'Windows フォーム デザイナ サポートに必要です。
		InitializeComponent()
	End Sub

	' 
	'使用されているリソースに後処理を実行します。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub ViewerForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		cboReport.Items.Add(My.Resources.ItemText1)
		cboReport.Items.Add(My.Resources.ItemText2)
		cboReport.Items.Add(My.Resources.ItemText3)
		cboReport.Items.Add(My.Resources.ItemText5)
		cboReport.Items.Add(My.Resources.ItemText6)
		cboReport.Items.Add(My.Resources.ItemText7)
		cboReport.Items.Add(My.Resources.ItemText8)
		cboReport.Items.Add(My.Resources.ItemText9)

		cboReport.SelectedIndex = 0
	End Sub

	Private Sub cboReport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboReport.SelectedIndexChanged
		Try
			'カーソルを変更します。
			Cursor = Cursors.WaitCursor
			Application.DoEvents()
			Select Case CType(sender, ComboBox).SelectedIndex
				Case 1  '  単純なサブレポート
					Call RunReport_Simple()
				Case 2  '  サブレポートのネスト
					Call RunReport_Nested()
				Case 3  '  リレーションシップを含むデータセットを使用したサブレポート
					Call RunReport_DSRelations()
				Case 4  '  サブレポートを含むマスター詳細レポート
					Call RunReport_MasterSubreport()
				Case 5  '  サブレポートにおけるブックマーク
					Call RunReport_Bookmark()
				Case 6  '  サブレポートでパラメータを使用する
					Call RunReport_Parameter()
				Case 7  '  サブレポートを使用して複数のテーブルを持ったデータセットを表示する
					Call RunReport_UnboundDataSet()
			End Select
		Catch ex As Exception
			MessageBox.Show(ex.ToString)
		Finally
			' 
			'カーソルを元に戻す。
			Cursor = Cursors.Default
			Application.DoEvents()
		End Try
	End Sub

	Private Sub RunReport_Simple()
		'
		'*****単純なサブレポート*****

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptSimpleMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_Nested()
		' 
		'*****サブレポートのネスト**********

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptNestedParent))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_DSRelations()
		' *****リレーションシップを含むデータセットを使用したサブレポート*****

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptDSRelationParent))
		Dim myJoinedDS As New DataSet()
		Dim cnnString As String
		cnnString = My.Resources.ConnectionString
		Dim cnn = New SQLiteConnection(cnnString)
		cnn.Open()
		Dim catAd As New SQLiteDataAdapter("Select * from categories order by categoryname", cnn)
		Dim prodAd As New SQLiteDataAdapter("Select * from products order by productname", cnn)
		Dim ODAd As New SQLiteDataAdapter("Select * from order_details", cnn)
		catAd.Fill(myJoinedDS, "Categories")
		prodAd.Fill(myJoinedDS, "Products")
		ODAd.Fill(myJoinedDS, "OrderDetails")
		cnn.Close()
		'
		'DataTable間に親子のリレーションを設定します。
		myJoinedDS.Relations.Add("CategoriesProducts", myJoinedDS.Tables("Categories").Columns("CategoryID"), myJoinedDS.Tables("Products").Columns("CategoryID"))
		myJoinedDS.Relations.Add("ProductsOrderDetails", myJoinedDS.Tables("Products").Columns("ProductID"), myJoinedDS.Tables("OrderDetails").Columns("ProductID"))
		rpt.DataSource = (myJoinedDS)
		rpt.DataMember = "Categories"
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_MasterSubreport()
		' 
		' *****サブレポートを含むマスター詳細レポート**********

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptMasterMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_Bookmark()
		' ***** Bookmark in sub-report *****
		' *****サブレポートにおけるブックマーク*****

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptBookmarkMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_Parameter()
		' 
		' *****サブレポートでパラメータを使用する*****

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptParamMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_UnboundDataSet()
		' *****サブレポートを使用して複数のテーブルを持ったデータセットを表示する*****

		'「Customers」テーブルと「Orders」テーブルを持つデータセットを生成します。
		Dim nwindConn = New SQLiteConnection(My.Resources.ConnectionString)
		Dim selectCMD = New SQLiteCommand("SELECT * FROM Customers", nwindConn)
		selectCMD.CommandTimeout = 30
		Dim selectCMD2 = New SQLiteCommand("SELECT * FROM Orders", nwindConn)
		selectCMD2.CommandTimeout = 30

		Dim custDA = New SQLiteDataAdapter()
		custDA.SelectCommand = selectCMD
		Dim ordersDA = New SQLiteDataAdapter()
		ordersDA.SelectCommand = selectCMD2

		Dim DS As DataSet = New DataSet()
		custDA.Fill(DS, "Customers")
		ordersDA.Fill(DS, "Orders")
		nwindConn.Close()

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptUnboundDSMain))
		rpt.DataSource = DS
		rpt.DataMember = "Customers"
		arvMain.LoadDocument(rpt)
	End Sub
End Class
