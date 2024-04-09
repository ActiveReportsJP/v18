Imports System
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Data
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data.SQLite
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Configuration
Imports GrapeCity.ActiveReports.ReportsCore.Data.DataProviders

'本サンプルは、レポートにデータを連結する様々な方法を紹介します。
Public Class MainForm
	Inherits System.Windows.Forms.Form
	ReadOnly _settingForNoHeaderFixed As String = My.Resources.NoHeaderFixed
	ReadOnly _settingForHeaderExistsFixed As String = My.Resources.HeaderExistsFixed
	Public Sub New()
		MyBase.New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		'Windowsフォームデザイナサポートに必要です。
		InitializeComponent()

		'InitializeComponent()を呼び出した後、初期化します。
	End Sub

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'
		'ドロップダウンリストをクリアします。
		cbCompanyName.Items.Clear()
	End Sub 'MainForm_Load

	'SelectedIndexChangedイベントでタブの切り替え時にビューワをクリアします。
	Private Sub tabDataBinding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDataBinding.SelectedIndexChanged
		'既存のページをクリアします。
		arvMain.Document = New GrapeCity.ActiveReports.Document.SectionDocument()
	End Sub 'tabDataBinding_SelectedIndexChanged


	'cbCompanyNameのDropDownイベントでドロップダウンリストにNorthwindデータベースから会社名を追加します。
	Private Sub cbCompanyName_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles cbCompanyName.DropDown
		'コンボボックスに項目がない場合は、挿入します。
		If cbCompanyName.Items.Count = 0 Then

			Cursor = Cursors.WaitCursor

			'データベースへの接続を設定します。
			Dim nwindConn As New SQLiteConnection()
			nwindConn.ConnectionString = My.Resources.ConnectionString
			'会社名を取得するSQL文を指定します。
			Dim selectCMD As New SQLiteCommand("SELECT DISTINCT Customers_CompanyName from Invoices", nwindConn)

			nwindConn.Open()
			Dim companyNamesDR As SQLiteDataReader = selectCMD.ExecuteReader()

			'データがある場合は、リストに新しい会社名を追加します。
			While companyNamesDR.Read()
				cbCompanyName.Items.Add(companyNamesDR(0).ToString())
			End While
			nwindConn.Close()

			'リスト内の最初の項目を選択します。
			cbCompanyName.SelectedIndex = 0

			Cursor = Cursors.Arrow
		End If
	End Sub 'cbCompanyName_DropDown

	' 
	' DataSetをデータソースとして使用します。
	Private Sub btnDataSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataSet.Click
		Cursor = Cursors.WaitCursor

		'
		'データを格納するDataSet。
		Dim invoiceData As New DataSet()
		invoiceData.Locale = CultureInfo.InvariantCulture

		'
		'データベースの接続でNorthwindデータベースからデータを追加します。
		Dim nwindConn As New SQLiteConnection()
		nwindConn.ConnectionString = My.Resources.ConnectionString

		'
		'SQLコマンドを実行します。
		Dim selectCMD As New SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn)
		selectCMD.CommandTimeout = 30

		'
		'Selectコマンドを実行するDataAdapter。
		Dim invoicesDA As New SQLiteDataAdapter()
		invoicesDA.SelectCommand = selectCMD

		'
		'DataSetにデータを挿入します。
		invoicesDA.Fill(invoiceData, "Invoices")
		nwindConn.Close()

		'
		'レポートを作成し、データソースを割り当てます。
		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\", My.Resources.ReportName)))
		rpt.DataSource = invoiceData
		rpt.DataMember = invoiceData.Tables(0).TableName

		'
		'レポートを実行して、表示します。
		arvMain.LoadDocument(rpt)

		Cursor = Cursors.Arrow
	End Sub 'btnDataSet_Click

	' 
	' DataTableをデータソースとして使用します。

	Private Sub btnDataTable_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataTable.Click
		Cursor = Cursors.WaitCursor

		'データを格納するDataTable。
		Dim invoiceData As New DataTable("Invoices")
		invoiceData.Locale = CultureInfo.InvariantCulture

		'データベースの接続でNorthwindデータベースからデータを追加します。
		Dim nwindConn As New SQLiteConnection()
		nwindConn.ConnectionString = My.Resources.ConnectionString

		'SQLコマンドを実行します。
		Dim selectCMD As New SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn)
		selectCMD.CommandTimeout = 30

		'Selectコマンドを実行するDataAdapter。
		Dim invoicesDA As New SQLiteDataAdapter()
		invoicesDA.SelectCommand = selectCMD

		'DataSetにデータを挿入します。
		invoicesDA.Fill(invoiceData)
		nwindConn.Close()

		'レポートを作成し、データソースを割り当てます。
		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\", My.Resources.ReportName)))
		rpt.DataSource = invoiceData
		'レポートを実行して、表示します。
		arvMain.LoadDocument(rpt)

		Cursor = Cursors.Arrow
	End Sub 'btnDataTable_Click

	'
	'btnDataView_Clickは -  DataViewをデータソースとして使用します。
	Private Sub btnDataView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataView.Click
		Cursor = Cursors.WaitCursor

		'
		'会社名が選択されているかどうかを確認します。
		If cbCompanyName.SelectedItem Is Nothing Then
			MessageBox.Show(My.Resources.SelectCompanyName)

			Cursor = Cursors.Arrow
			Return
		End If

		'DataTableには、完全なデータを保持する
		Dim invoiceData As New DataTable("Invoices")
		invoiceData.Locale = CultureInfo.InvariantCulture

		'データベース接続は、サンプルのNorthwind Accessデータベースから移入
		Dim nwindConn As New SQLiteConnection()
		nwindConn.ConnectionString = My.Resources.ConnectionString

		'SQLコマンドを実行します。
		Dim selectCMD As New SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn)
		selectCMD.CommandTimeout = 30

		'データアダプタは、selectコマンドを実行するために使用される
		Dim invoicesDA As New SQLiteDataAdapter()
		invoicesDA.SelectCommand = selectCMD

		'DataTableにデータをll the DataTable
		invoicesDA.Fill(invoiceData)
		nwindConn.Close()

		'DataViewを作成し、選択した仕入先RowFilterを割り当てる
		Dim invoiceDataView As New DataView(invoiceData)
		invoiceDataView.RowFilter = "Customers_CompanyName='" + Convert.ToString(cbCompanyName.SelectedItem).Replace("'", "''") + "'"

		'レポートを作成し、データ·ソースを割り当てる
		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\", My.Resources.ReportName)))
		rpt.DataSource = invoiceDataView
		'実行してレポートを表示する
		arvMain.LoadDocument(rpt)

		Cursor = Cursors.Arrow
	End Sub 'btnDataView_Click

	'btnDataReader_Clickは -  DataReaderをデータソースとして使用します。
	Private Sub btnDataReader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataReader.Click
		Cursor = Cursors.WaitCursor

		'データベースの接続でNorthwindデータベースからデータを追加します。
		Dim nwindConn As New SQLiteConnection()
		nwindConn.ConnectionString = My.Resources.ConnectionString

		'SQLコマンドを実行します。
		Dim selectCMD As New SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn)
		selectCMD.CommandTimeout = 30

		'データを読み込むDataReader。
		Dim invoiceDataReader As SQLiteDataReader

		'データベース接続を開き、DataReaderを実行します。
		nwindConn.Open()
		invoiceDataReader = selectCMD.ExecuteReader()

		'レポートを作成し、データソースを割り当てます。
		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\", My.Resources.ReportName)))
		rpt.DataSource = invoiceDataReader
		'レポートを実行して、表示します。
		arvMain.Document = rpt.Document
		rpt.Run(False)

		'データベース接続を閉じます。
		nwindConn.Close()

		Cursor = Cursors.Arrow
	End Sub 'btnDataReader_Click

	'btnSqliteDb_Click - GrapeCity Sqliteオブジェクトをデータソースとして使用します。
	Private Sub btnSqliteDb_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSqliteDb.Click
		Cursor = Cursors.WaitCursor

		Dim config As New ReportingConfiguration()

		Dim dp As DataProviderInfo = config.DataProviders.First(Function(x) x.InvariantName = "SQLITE")

		'使用されるデータソースオブジェクト。

		Dim db As New Data.DbDataSource(dp)

		'データベースの接続でNorthwindデータベースからデータを追加します。
		db.ConnectionString = My.Resources.ConnectionString

		'SQLコマンドを実行します。
		db.SQL = "SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID"

		'レポートを作成し、データソースを割り当てます。
		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\", My.Resources.ReportName)))
		rpt.DataSource = db
		'レポートを実行して、表示します。
		arvMain.LoadDocument(rpt)

		Cursor = Cursors.Arrow
	End Sub 'btnSqliteDb_Click

	Private Sub btnGenerateXML_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerateXML.Click
		'
		'データを格納するDataSet。
		Dim invoiceData As New DataSet("Northwind")
		invoiceData.Locale = CultureInfo.InvariantCulture

		'
		'データベースの接続でNorthwindデータベースからデータを追加します。
		Dim nwindConn As New SQLiteConnection()
		nwindConn.ConnectionString = My.Resources.ConnectionString

		'
		'SQLコマンドを実行します。
		Dim selectCMD As New SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn)
		selectCMD.CommandTimeout = 30

		'
		'Selectコマンドを実行するDataAdapter。
		Dim invoicesDA As New SQLiteDataAdapter()
		invoicesDA.SelectCommand = selectCMD

		'
		'DataSetにデータを挿入します。
		invoicesDA.FillSchema(invoiceData, SchemaType.Source, "Invoices")
		invoiceData.Tables("Invoices").Columns("OrderDate").DataType = System.Type.GetType("System.String")
		invoiceData.Tables("Invoices").Columns("ShippedDate").DataType = System.Type.GetType("System.String")
		invoicesDA.Fill(invoiceData, "Invoices")

		'
		'[名前を付けて保存]ダイアログボックスを初期化します。

		dlgSave.Title = My.Resources.SaveDataAs

		dlgSave.FileName = "Invoices.xml"
		dlgSave.Filter = My.Resources.Filter
		If (dlgSave.ShowDialog() = DialogResult.OK) Then
			btnXML.Enabled = False
			Dim x As Integer
			'
			'有効な名前が返された場合は、指定されたファイル名にDataSetを保存します。
			If dlgSave.FileName.Length <> 0 Then
				'
				'レポートのXML内のすべての日付フィールドの書式を設定します。
				For x = 0 To invoiceData.Tables("Invoices").Rows.Count - 1
					invoiceData.Tables("Invoices").Rows(x)("OrderDate") = Convert.ToDateTime(invoiceData.Tables("Invoices").Rows(x)("OrderDate")).ToShortDateString()
					If Not invoiceData.Tables("Invoices").Rows(x)("ShippedDate").GetType() Is Type.GetType("System.DBNull") Then
						invoiceData.Tables("Invoices").Rows(x)("ShippedDate") = Convert.ToDateTime(invoiceData.Tables("Invoices").Rows(x)("ShippedDate")).ToShortDateString()
					End If
				Next

				invoiceData.WriteXml(dlgSave.FileName)
			End If

			btnXML.Enabled = True
		End If
	End Sub

	'
	'btnXML_Clickは - GrapeCityXMLオブジェクトをデータソースとして使用します。
	Private Sub btnXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnXML.Click
		'
		'[開く]ダイアログボックスを初期化します。

		dlgOpen.Title = My.Resources.OpenDataFile

		dlgOpen.FileName = dlgSave.FileName
		dlgOpen.Filter = My.Resources.Filter

		If dlgOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then

			'
			'有効な名前が返された場合は、データを開き、レポートを実行します。
			If dlgOpen.FileName.Length <> 0 Then
				Cursor = Cursors.WaitCursor

				'
				'使用されるXMLデータソースオブジェクトを示します。
				Dim xml As New Data.XMLDataSource() '  Dim _XML As New GrapeCity.ActiveReports.DataSources.XMLDataSource()

				'
				'選択されたXMLデータファイルのファイル名を割り当てます。
				xml.FileURL = dlgOpen.FileName
				'
				'レコードセットパターンを割り当てます。
				xml.RecordsetPattern = "//Northwind/Invoices"

				'
				'レポートを作成し、データソースを割り当てます。
				Dim rpt As New SectionReport()
				rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\", My.Resources.ReportName)))
				rpt.DataSource = xml
				'
				'レポートを実行して、表示します。
				arvMain.LoadDocument(rpt)

				Cursor = Cursors.Arrow
			End If
		End If

	End Sub

	'
	Private Sub btnCSV_Click(sender As Object, e As EventArgs) Handles btnCSV.Click

		Const settingForNoHeaderDelimited As String = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued"

		Cursor = Cursors.WaitCursor
		'
		Dim csv As New Data.CsvDataSource()

		'
		Dim encoding As String = My.Resources.CSVEncoding

		'
		Dim connectionString As String = String.Empty

		'
		If rbtnNoHeaderComma.Checked Then
			connectionString = (Convert.ToString((Convert.ToString("Path=") & My.Resources.CSVDataSetPathComma) + ";" + "Encoding=") & encoding) + ";" + "TextQualifier="";" + "ColumnsSeparator=,;" + "RowsSeparator=\r\n;" + "Columns=" + settingForNoHeaderDelimited
			'
		ElseIf rbtnHeaderTab.Checked Then

			connectionString = (Convert.ToString((Convert.ToString("Path=") & My.Resources.CSVDataSetPathHeaderTab) + ";" + "Encoding=") & encoding) + ";" + "TextQualifier="";" + "ColumnsSeparator=" & vbTab & ";" + "RowsSeparator=\r\n;" + "HasHeaders=True"
			'
		ElseIf rbtnHeader.Checked Then
			connectionString = (Convert.ToString((Convert.ToString("Path=") & My.Resources.CSVDataSetPathHeaderFixed) + ";" + "Encoding=") & encoding) + ";" +
			"Columns=" + _settingForHeaderExistsFixed + ";" +
			"HasHeaders=True"
			'
		ElseIf rbtnNoHeader.Checked Then
			connectionString = (Convert.ToString((Convert.ToString("Path=") & My.Resources.CSVDataSetPathFixed) + ";" + "Encoding=") & encoding) + ";" + "Columns=" + _settingForNoHeaderFixed
		End If
		'
		csv.ConnectionString = connectionString

		'
		Dim productList As New ProductList With {
			.ResourceLocator = New DefaultResourceLocator(New Uri(Path.GetDirectoryName(Application.ExecutablePath) + "\")),
			.DataSource = csv
		}

		'
		arvMain.LoadDocument(productList)

		Cursor = Cursors.Arrow

	End Sub
End Class
