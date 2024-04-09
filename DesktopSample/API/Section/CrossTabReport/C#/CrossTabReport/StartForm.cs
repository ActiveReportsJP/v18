using GrapeCity.ActiveReports;
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ActiveReports.Samples.CrossTabReport
{
	/// <summary>
	/// 本サンプルでは、アンバウンドデータ、条件付きテキストの強調表示、
	/// 列をまたがっているクロスタブビューおよびデータ集計を紹介します。
	/// </summary>
	public partial class StartForm : System.Windows.Forms.Form
	{
		public StartForm()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();
		}

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
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

		private void StartForm_Load(object sender, EventArgs e)
		{
			try
			{
				//レポートを作成し、ビューワに表示します。
				var rpt = new SectionReport();
				rpt.LoadLayout(XmlReader.Create( Properties.Resources.ProductWeeklySales));
				arvMain.LoadDocument(rpt);
			}
			catch (Exception ex)
			{
				//レポート作成時にエラーが発生した際にはメッセージを表示します。
				MessageBox.Show(ex.ToString(), Text);
			}
		}
	}
}
