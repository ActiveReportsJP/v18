using System;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Data.SQLite;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Configuration;
using GrapeCity.ActiveReports.Data;
using GrapeCity.ActiveReports.ReportsCore.Data.DataProviders;

namespace ActiveReports.Samples.BoundData
{
	/// <summary>
	/// 本サンプルは、レポートにデータを連結する様々な方法を紹介します。
	/// </summary>
	public partial class MainForm : System.Windows.Forms.Form
	{
		readonly string _settingForNoHeaderFixed;
		readonly string _settingForHeaderExistsFixed;
		public MainForm()
		{
			_settingForNoHeaderFixed = Properties.Resources.NoHeaderFixed;
			_settingForHeaderExistsFixed = Properties.Resources.HeaderExistsFixed;
			// Windowsフォームデザイナサポートに必要です。
			InitializeComponent();
		}

		/// <summary>
		/// アプリケーションのメインエントリポイントです。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if NET6_0_OR_GREATER
			Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
#endif
            Application.Run(new MainForm());
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			//ドロップダウンリストをクリアします。
			cbCompanyName.Items.Clear();
		}

		/// <summary>
		/// SelectedIndexChangedイベントでタブの切り替え時にビューワをクリアします。
		/// </summary>
		/// 
		private void tabDataBinding_SelectedIndexChanged(object sender, EventArgs e)
		{
			//既存のページをクリアする
			arvMain.Document = new GrapeCity.ActiveReports.Document.SectionDocument();
		}

		/// <summary>
		/// cbCompanyNameのDropDownイベントでドロップダウンリストにNorthwindデータベースから会社名を追加します。
		/// </summary>
		private void cbCompanyName_DropDown(object sender, EventArgs e)
		{
			//コンボボックスに項目がない場合は、挿入します。
			if (cbCompanyName.Items.Count == 0)
			{
				Cursor = Cursors.WaitCursor;

				//データベースへの接続を設定します。
				var nwindConn = new SQLiteConnection();
				nwindConn.ConnectionString = Properties.Resources.ConnectionString;
				//会社名を取得するSQL文を指定します。
				var selectCMD = new SQLiteCommand("SELECT DISTINCT Customers_CompanyName from Invoices", nwindConn);
				nwindConn.Open();	
				var companyNamesDR = selectCMD.ExecuteReader();
				 //While the reader has data add a new Company Name to the list.
				while (companyNamesDR.Read())
				{
					cbCompanyName.Items.Add(companyNamesDR[0].ToString());
				}
				nwindConn.Close();
				//リスト内の最初の項目を選択します。
				cbCompanyName.SelectedIndex = 0;
				Cursor = Cursors.Arrow;
			}		
		}

		/// <summary>
		/// DataSetをデータソースとして使用します。
		/// </summary>
		private void btnDataSet_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//データを格納するDataSet。
			DataSet invoiceData = new DataSet();
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//データベースの接続でNorthwindデータベースからデータを追加します。
			var nwindConn = new SQLiteConnection();			
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			
			
			//SQLコマンドを実行します。
			var selectCMD = new SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//Selectコマンドを実行するDataAdapter。
			var invoicesDA = new SQLiteDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//DataSetにデータを挿入します。
			invoicesDA.Fill(invoiceData, "Invoices");
			nwindConn.Close();

			//レポートを作成し、データソースを割り当てます。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = invoiceData;
			rpt.DataMember = invoiceData.Tables[0].TableName;
			//レポートを実行して、表示します。
			arvMain.LoadDocument(rpt);

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 
		/// DataTableをデータソースとして使用します。
		/// </summary>
		private void btnDataTable_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//データを格納するDataTable。
			DataTable invoiceData = new DataTable("Invoices");
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//データベースの接続でNorthwindデータベースからデータを追加します。
			var nwindConn = new SQLiteConnection();			
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			

			//SQLコマンドを実行します。
			var selectCMD = new SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//Selectコマンドを実行するDataAdapter。
			var invoicesDA = new SQLiteDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//DataSetにデータを挿入します。
			invoicesDA.Fill(invoiceData);
			nwindConn.Close();

			//レポートを作成し、データソースを割り当てます。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\", Properties.Resources.ReportName)));
			rpt.DataSource = invoiceData;
			//レポートを実行して、表示します。
			arvMain.LoadDocument(rpt);		

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 
		/// DataViewをデータソースとして使用します。
		/// </summary>
		private void btnDataView_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//
			//会社名が選択されているかどうかを確認します。
			if (cbCompanyName.SelectedItem == null)
			{
				MessageBox.Show(Properties.Resources.SelectCompanyName);
				Cursor = Cursors.Arrow;
				return;
			}

			//データを格納するDataTable。
			DataTable invoiceData = new DataTable("Invoices");
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//データベースの接続でNorthwindデータベースからデータを追加します。
			var nwindConn = new SQLiteConnection();			
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			

			//SQLコマンドを実行します。
			var selectCMD = new SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//Selectコマンドを実行するDataAdapter。
			var invoicesDA = new SQLiteDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//DataSetにデータを挿入します。
			invoicesDA.Fill(invoiceData);
			nwindConn.Close();

			//DataViewを作成し、選択した会社名によってフィルタを割り当てます。
			DataView invoiceDataView = new DataView(invoiceData);
			invoiceDataView.RowFilter = "Customers_CompanyName='" + Convert.ToString(cbCompanyName.SelectedItem).Replace("'", "''") + "'";

			//レポートを作成し、データソースを割り当てます。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = invoiceDataView;
			//レポートを実行して、表示します。
			arvMain.LoadDocument(rpt);		

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 
		/// DataReaderをデータソースとして使用します。
		/// </summary>
		private void btnDataReader_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//データベースの接続でNorthwindデータベースからデータを追加します。
			var nwindConn = new SQLiteConnection();
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;

			//SQLコマンドを実行します。
			var selectCMD = new SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//データベース接続を開き、DataReaderを実行します。
			nwindConn.Open();
			var invoiceDataReader = selectCMD.ExecuteReader();

			//レポートを作成し、データソースを割り当てます。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = invoiceDataReader;
			//レポートを実行して、表示します
			arvMain.Document = rpt.Document;
			rpt.Run(false);
			
			//データベース接続を閉じます。
			nwindConn.Close();

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 
		/// GrapeCity Sqliteオブジェクトをデータソースとして使用します。
		/// </summary>
		private void btnSqliteDb_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			var config = new ReportingConfiguration();

			DataProviderInfo dp = config.DataProviders.First(x => x.InvariantName == "SQLITE");

			//使用されるデータソースオブジェクト。
			DbDataSource db = new DbDataSource(dp);

			//データベースの接続でNorthwindデータベースからデータを追加します。
			db.ConnectionString = Properties.Resources.ConnectionString;
			
			//SQLコマンドを実行します。
			db.SQL = "SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID";

			//レポートを作成し、データソースを割り当てます。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = db;
			//レポートを実行して、表示します。
			arvMain.LoadDocument(rpt);

			Cursor = Cursors.Arrow;
		}
		
		/// <summary>
		/// DataSetを生成し、XMLデータファイルとして保存します。
		/// </summary>
		private void btnGenerateXML_Click(object sender, EventArgs e)
		{
			//データを格納するDataSet。
			DataSet invoiceData = new DataSet("Northwind");
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//データベース接続は、サンプルのNorthwind Accessデータベースから移入
			var nwindConn = new SQLiteConnection();
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			

			//SQLのSELECTコマンドをデータベースに対して実行する
			var selectCMD = new SQLiteCommand("SELECT * FROM Invoices ORDER BY Customers_CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//データアダプタは、selectコマンドを実行するために使用される
			var invoicesDA = new SQLiteDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//DataSetにデータを挿入します。
			invoicesDA.FillSchema(invoiceData, SchemaType.Source, "Invoices");
			invoiceData.Tables["Invoices"].Columns["OrderDate"].DataType = Type.GetType("System.String");
			invoiceData.Tables["Invoices"].Columns["ShippedDate"].DataType = Type.GetType("System.String");
			invoicesDA.Fill(invoiceData, "Invoices");

			//
			//[名前を付けて保存]ダイアログボックスを初期化します。
			dlgSave.Title =  Properties.Resources.SaveDataAs;
			dlgSave.FileName = "Invoices.xml";
			dlgSave.Filter = Properties.Resources.Filter;
			if (dlgSave.ShowDialog() == DialogResult.OK)
			{
				btnXML.Enabled = false;
				//
				//有効な名前が返された場合は、指定されたファイル名にDataSetを保存します。
				if (dlgSave.FileName.Length != 0)
				{
					//
					//レポートのXML内のすべての日付フィールドの書式を設定します。
					for (int x = 0; x < invoiceData.Tables["Invoices"].Rows.Count; x++)
					{
						invoiceData.Tables["Invoices"].Rows[x]["OrderDate"] = Convert.ToDateTime(invoiceData.Tables["Invoices"].Rows[x]["OrderDate"]).ToShortDateString();
						if (invoiceData.Tables["Invoices"].Rows[x]["ShippedDate"].GetType() != Type.GetType("System.DBNull"))
						{
							invoiceData.Tables["Invoices"].Rows[x]["ShippedDate"] = Convert.ToDateTime(invoiceData.Tables["Invoices"].Rows[x]["ShippedDate"]).ToShortDateString();
						}
					}
					invoiceData.WriteXml(dlgSave.FileName);
				}
				btnXML.Enabled = true;
			}
		}

		/// <summary>
		/// 
		/// GrapeCityXMLオブジェクトをデータソースとして使用します。
		/// </summary>
		private void btnXML_Click(object sender, EventArgs e)
		{
			//
			//[開く]ダイアログボックスを初期化します。
			
			dlgOpen.Title = Properties.Resources.OpenDataFile;
			dlgOpen.FileName = dlgSave.FileName;
			dlgOpen.Filter = Properties.Resources.Filter;

			if (dlgOpen.ShowDialog() == DialogResult.OK)
			{
				//
				//有効な名前が返された場合は、データを開いて、レポートを実行します。
				if (dlgOpen.FileName.Length != 0)
				{
					Cursor = Cursors.WaitCursor;

					//使用されるXMLデータソースオブジェクトを示します。
					var xml = new GrapeCity.ActiveReports.Data.XMLDataSource();

					//選択されたXMLデータファイルのファイル名を割り当てます。
					xml.FileURL = dlgOpen.FileName;
					//レコードセットパターンを割り当てます。
					xml.RecordsetPattern = @"//Northwind/Invoices";

					//レポートを作成し、データソースを割り当てます。
					var rpt = new SectionReport();
					rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\..\\",Properties.Resources.ReportName)));
					rpt.DataSource = xml;
					//レポートを実行して、表示します。
					arvMain.LoadDocument(rpt);

					Cursor = Cursors.Arrow;
				}
			}
		}

		private void btnCSV_Click(object sender, EventArgs e)
		{
			const string settingForNoHeaderDelimited = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued";

			Cursor = Cursors.WaitCursor;
			//CSV Data Source object to use.
			var csv = new GrapeCity.ActiveReports.Data.CsvDataSource();


			//Dataset encoding.
			string encoding = Properties.Resources.CSVEncoding;

			//Configure connection string by selected dataset type
			string connectionString = string.Empty;

			//Delimited Data: No header, column separator is comma
			if (rbtnNoHeaderComma.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathComma + ";" +
								   "Encoding=" + encoding + ";" +
								   "TextQualifier=\";" +
								   "ColumnsSeparator=,;" +
								   "RowsSeparator=\\r\\n;" +
								   "Columns=" + settingForNoHeaderDelimited;
			}
			//Delimited Data: Header exists, column separator is Tab
			else if (rbtnHeaderTab.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathHeaderTab + ";" +
								   "Encoding=" + encoding + ";" +
								   "TextQualifier=\";" +
								   "ColumnsSeparator=\t;" +
								   "RowsSeparator=\\r\\n;" +
								   "HasHeaders=True";

			}
			//Fixed width Data: Header exists
			else if (rbtnHeader.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathHeaderFixed + ";" +
								   "Encoding=" + encoding + ";" +
								   "Columns=" + _settingForHeaderExistsFixed + ";" +
								   "HasHeaders=True";
			}
			//Fixed width Data: No header
			else if (rbtnNoHeader.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathFixed + ";" +
								   "Encoding=" + encoding + ";" +
								   "Columns=" + _settingForNoHeaderFixed;
			}
			//Applying specified connection string to data source
			csv.ConnectionString = connectionString;

			//Create the report and assign the data source.
			ProductList productList = new ProductList
			{
				ResourceLocator = new DefaultResourceLocator(new Uri(Path.GetDirectoryName(Application.ExecutablePath) + "\\")),
				DataSource = csv
			};

			//Run and view the report
			arvMain.LoadDocument(productList);

			Cursor = Cursors.Arrow;
		}
	}
}
