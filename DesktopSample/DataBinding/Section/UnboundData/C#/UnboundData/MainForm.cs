using GrapeCity.ActiveReports;
using System;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ActiveReports.Samples.UnboundData
{
	/// <summary>
	/// ActiveReports でアンバウンドデータの使用方法を紹介します。
	/// </summary>
	public partial class MainForm : System.Windows.Forms.Form
	{
		public MainForm()
		{
			//
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
            Application.Run(new MainForm());
		}

		/// <summary>
		/// tabDataBinding_SelectedIndexChangedイベントでは、切り替え時にビューワをクリアします。
	   /// </summary>
		private void tabDataBinding_SelectedIndexChanged(object sender, EventArgs e)
		{
		  //既存のページをクリアします。
		  //
			arvMain.Document = new GrapeCity.ActiveReports.Document.SectionDocument();
		}

		//#region アンバウンドデータのコード
		//

		/// <summary>
		/// btnDataSet_Clickイベントでは、DataSetをデータソースとして使用します。 
		/// </summary>
		private void btnDataSet_Click(object sender, EventArgs e)
		{
			//レポートを作成します。
			var rpt = new SectionReport();
			rpt.AddAssembly(typeof(SQLiteConnection).Assembly);
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundDSInvoice));
			//実行してレポートを表示します。
			arvMain.LoadDocument(rpt);
		}

		/// <summary>
		/// btnDataReader_Clickイベントでは、DataReaderをデータソースとして使用します。
		/// </summary>
		private void btnDataReader_Click(object sender, System.EventArgs e)
		{
			//レポートを作成します。
			var rpt = new SectionReport();
			rpt.AddAssembly(typeof(SQLiteConnection).Assembly);
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundDRInvoice));
			//実行してレポートを表示します。
			arvMain.LoadDocument(rpt);
		}

		/// <summary>
		/// btnTextFile_Clickイベントでは、テキストファイルをデータソースとして使用します。
		/// </summary>
		private void btnTextFile_Click(object sender, System.EventArgs e)
		{
			//レポートを作成します。
			//UnboundTFInvoice rpt = new UnboundTFInvoice();
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundTFInvoice));
			//実行してレポートを表示します。
			arvMain.LoadDocument(rpt);		
		}

		/// <summary>
		/// btnArray_Clickイベントでは、配列をデータソースとして使用します。
		/// </summary>
		private void btnArray_Click(object sender, System.EventArgs e)
		{
			//レポートを作成します。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundDAInvoice));
			//実行してレポートを表示します。
			arvMain.LoadDocument(rpt);
		}

		//#endregion
	}
}
