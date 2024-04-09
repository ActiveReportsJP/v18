using GrapeCity.ActiveReports;
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ActiveReports.Samples.XML
{
	/// <summary>
	/// XMLデータソースを使い、受注一覧レポートを作成します。
	/// サブレポートを使う方法と、XML階層構造を使用してレポートを作成する方法を示します。
	/// </summary>
	public partial class StartForm : System.Windows.Forms.Form
	{
		
		public StartForm()
		{
			// Windows フォーム デザイナ サポートに必要です。
			//
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
            Application.Run(new StartForm());
		}

		/// <summary>
		/// btnCustomers_Clickイベントでラジオボタンを選択し、レポートのオブジェクトのデータを設定します。
		/// </summary>
		private void btnCustomers_Click(object sender, EventArgs e)
		{
			try
			{
				var rpt = new SectionReport();
				rpt.LoadLayout(XmlReader.Create(Properties.Resources.CustomersOrders));

				var ds = rpt.DataSource as GrapeCity.ActiveReports.Data.XMLDataSource;
				if (ds == null)
				{
					// エラーが発生した場合、メッセージを表示します。
					MessageBox.Show(Properties.Resources.DataSourceError, this.Text);
					return;
				}

				ds.FileURL = Properties.Resources.ConnectionString;

				// 作成するレポートに合わせて、ノード（レコード）を取得するためのXSLパターンを設定します。
				if (radioAll.Checked)
				{
					// データをすべて表示します。
					ds.RecordsetPattern = "//CUSTOMER";
				}
				else if (radioID.Checked)
				{
					// ID=2301に設定されたデータを表示します。
					ds.RecordsetPattern = "//CUSTOMER[@id = " + @"""" + "2301" + @"""" + "]";
				}
				else if (radioEmail.Checked)
				{
					// 有効なE-mailを含むデータを表示します。
					ds.RecordsetPattern = "//CUSTOMER[@email]";
				}
				
				ViewerForm frm = new ViewerForm();
				frm.Show();
				frm.LoadReport(rpt);
			}
			catch (ReportException ex)
			{
				MessageBox.Show(ex.ToString(), Text);
			}
		}

		/// <summary>
		/// btnCustomersLeveled_Clickイベントで OrdersLeveledレポートを作成し、
		/// データソースを設定します。
		/// </summary>
		private void btnCustomersLeveled_Click(object sender, EventArgs e)
		{
			try
			{
				//OrdersLeveled rpt = new OrdersLeveled();
				var rpt = new SectionReport();
				rpt.LoadLayout(XmlReader.Create(Properties.Resources.OrdersLeveled));

				var ds = rpt.DataSource as GrapeCity.ActiveReports.Data.XMLDataSource;
				if (ds == null)
				{
					// エラーが発生した場合、メッセージを表示します。
					MessageBox.Show(Properties.Resources.DataSourceError, this.Text);
					return;
				}

				// XMLファイル名を設定します。
				ds.FileURL = Properties.Resources.ConnectionString;
				
				// レポートを表示します。
				ViewerForm frm = new ViewerForm();
				frm.Show();
				frm.LoadReport(rpt);
			}
			catch (ReportException ex)
			{
				MessageBox.Show(ex.ToString(), Text);
			}
		}
	}
}
