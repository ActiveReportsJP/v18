using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Xml;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.SubReport
{
	/// <summary>
	/// A description of the overview of the ViewerForm.
	/// ViewerForm の概要の説明です。
	/// </summary>
	public partial class ViewerForm : System.Windows.Forms.Form
	{
	   
		public ViewerForm()
		{
			//
			//Required for Windows Form Designer support.
			//
			InitializeComponent();

			//
			//TODO: after the call to InitializeComponent, please add the constructor code.
			//
		}

		/// <summary>
		/// The main entry point for the application.
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
			Application.Run(new ViewerForm());
		}

		private void ViewerForm_Load(object sender, EventArgs e)
		{
			cboReport.Items.Add(Properties.Resources.ItemText1);
			cboReport.Items.Add(Properties.Resources.ItemText2);
			cboReport.Items.Add(Properties.Resources.ItemText3);
			cboReport.Items.Add(Properties.Resources.ItemText5);
			cboReport.Items.Add(Properties.Resources.ItemText6);
			cboReport.Items.Add(Properties.Resources.ItemText7);
			cboReport.Items.Add(Properties.Resources.ItemText8);
			cboReport.Items.Add(Properties.Resources.ItemText9);

			cboReport.SelectedIndex = 0;
		   
		}

	   

		private void cboReport_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				// Change the cursor
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				switch (((ComboBox)sender).SelectedIndex)
				{
					case 1: // Simple sub-report 
						RunReport_Simple();
						break;
					case 2: // Nested sub-reports 
						RunReport_Nested();
						break;
					case 3: // Sub report using the data set that contains the relationship
						RunReport_DSRelations();
						break;
					case 4: // Master-detail report containing a subreport
						RunReport_MasterSubreport();
						break;
					case 5: // Bookmark in sub-report
						RunReport_Bookmark();
						break;
					case 6: // Use a parameter in the subreport
						RunReport_Parameter();
						break;
					case 7: // To view the DataSet with multiple tables using sub-reports
						RunReport_UnboundDataSet();
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				//Cursor back to the original
				Cursor = Cursors.Default;
				Application.DoEvents();
			}
		}

		private void RunReport_Simple()
		{
			// ***** Simple sub-report *****

			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptSimpleMain));
			arvMain.LoadDocument(rpt);
		}

		private void RunReport_Nested()
		{
			// ***** Nested sub-reports *****

			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptNestedParent));
			arvMain.LoadDocument(rpt);
		}

		private void RunReport_DSRelations()
		{
			// ***** Sub report using the data set that contains the relationship *****

			var myJoinedDS = new DataSet();
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptDSRelationParent));

			string cnnString = Properties.Resources.ConnectionString;

			var cnn = new SQLiteConnection(cnnString);
			cnn.Open();
			var catAd = new SQLiteDataAdapter("Select * from categories order by categoryname", cnn);
			var prodAd = new SQLiteDataAdapter("Select * from products order by productname", cnn);
			var ODAd = new SQLiteDataAdapter("Select * from order_details", cnn);
			//Add three DataTables in the DataSet (myJoinedDS)
			catAd.Fill(myJoinedDS, "Categories");
			prodAd.Fill(myJoinedDS, "Products");
			ODAd.Fill(myJoinedDS, "OrderDetails");
			cnn.Close();

			//Sets the parent-child relationship between DataTable.
			myJoinedDS.Relations.Add("CategoriesProducts", myJoinedDS.Tables["Categories"].Columns["CategoryID"], myJoinedDS.Tables["Products"].Columns["CategoryID"]);
			myJoinedDS.Relations.Add("ProductsOrderDetails", myJoinedDS.Tables["Products"].Columns["ProductID"], myJoinedDS.Tables["OrderDetails"].Columns["ProductID"]);
			rpt.DataSource = (myJoinedDS);
			rpt.DataMember = "Categories";
			arvMain.LoadDocument(rpt);
		}

		private void RunReport_MasterSubreport()
		{
			// ***** Master-detail report contains a subreport *****

			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptMasterMain));

			arvMain.LoadDocument(rpt);

		}

		private void RunReport_Bookmark()
		{
			// ***** Bookmark in sub-report *****
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptBookmarkMain));
			arvMain.LoadDocument(rpt);
		}

		private void RunReport_Parameter()
		{
			// ***** Use a parameter in the subreport *****

			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptParamMain));
			arvMain.LoadDocument(rpt);
		}

		private void RunReport_UnboundDataSet()
		{
			// ***** To view the DataSet with multiple tables using sub-reports *****

			// To generate dataset using "Customers" and "Orders" tables.
			var nwindConn = new SQLiteConnection(Properties.Resources.ConnectionString);

			var selectCMD = new SQLiteCommand("SELECT * FROM Customers", nwindConn);
			selectCMD.CommandTimeout = 30;
			var selectCMD2 = new SQLiteCommand("SELECT * FROM Orders", nwindConn);
			selectCMD2.CommandTimeout = 30;

			var custDA = new SQLiteDataAdapter();
			custDA.SelectCommand = selectCMD;
			var ordersDA = new SQLiteDataAdapter();
			ordersDA.SelectCommand = selectCMD2;

			DataSet DS = new DataSet();
			custDA.Fill(DS, "Customers");
			ordersDA.Fill(DS, "Orders");
			nwindConn.Close();

			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptUnboundDSMain));
			rpt.DataSource = DS;
			rpt.DataMember = "Customers";
			arvMain.LoadDocument(rpt);
		}	   
	}
}
