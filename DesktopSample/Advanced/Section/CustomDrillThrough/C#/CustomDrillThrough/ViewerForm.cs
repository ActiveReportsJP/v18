using GrapeCity.ActiveReports;
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ActiveReports.Samples.CustomDrillThrough
{
	/// <summary>
	///frmviewmainの概要の説明です。 
	/// 
	///本サンプルはハイパーリンクおよびビューワのハイパーリンクイベントを使用し、 
	///ドリルダウンレポートをシミュレートする方法を表します。
	/// </summary>
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private readonly bool _loadMainReport;

		public ViewerForm()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();
			_loadMainReport = true;
		}

		public ViewerForm(bool loadMainReport)
		{
			InitializeComponent();
			_loadMainReport = loadMainReport;
		}

		/// <summary>
		/// 
		/// アプリケーションのメインエントリポイントです。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#if NET6_0_OR_GREATER
			Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewerForm());
		}

		/// <summary>
		/// arvMain_HyperLink: ビューワオブジェクトから送信されたハイパーリンクを処理します。
		/// </summary>
		private void arvMain_HyperLink(object sender, GrapeCity.ActiveReports.Viewer.Win.HyperLinkEventArgs e)
		{
			e.Handled = true;
			string hyperlink = e.HyperLink.Split(':')[1];
			string report = e.HyperLink.Split(':')[0];

			if (report == "DrillThrough1")
			{
				// 顧客番号をクリックして、ドリルダウンレポートに移動し、顧客情報を表示します。
				var rpt2 = new SectionReport();
				rpt2.LoadLayout(XmlReader.Create(Properties.Resources.DrillThrough1));
				ViewerForm frm2 = new ViewerForm(false);
				rpt2.Parameters["customerID"].Value = hyperlink;
				frm2.arvMain.LoadDocument(rpt2);
				frm2.ShowDialog(this);
			}
			else if (report == "DrillThrough2")
			{
				// 受注番号をクリックし、受注情報を開きます。
				var rpt3 = new SectionReport();
				rpt3.LoadLayout(XmlReader.Create(Properties.Resources.DrillThrough2));
				ViewerForm frm3 = new ViewerForm(false);
				rpt3.Parameters["orderID"].Value = hyperlink;
				frm3.arvMain.LoadDocument(rpt3);
				frm3.ShowDialog(this);
			}
		}

		/// <summary>
		/// ViewerForm_Load - フォームで初期設定を行います。
		/// </summary>
		private void ViewerForm_Load(object sender, EventArgs e)
		{
			if(_loadMainReport)
			{
				var rpt = new SectionReport();
				rpt.LoadLayout(XmlReader.Create(Properties.Resources.DrillThroughMain));
				arvMain.LoadDocument(rpt);
			}
		}
	}
}
