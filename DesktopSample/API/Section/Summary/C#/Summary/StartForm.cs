using GrapeCity.ActiveReports;
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ActiveReports.Samples.CalculatedFields
{
	/// <summary>
	/// 本サンプルは、新しい計算フィールドの作成および集計機能を紹介します。
	/// </summary>
	public partial class StartForm : System.Windows.Forms.Form
	{
		public StartForm()
		{
			InitializeComponent();
			comboBox1.Items.AddRange(new[] { Properties.Resources.OrdersReport, Properties.Resources.DataFieldExpressionsReport });
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

		private void Button_Click(object sender, EventArgs e)
		{
			var rpt = new SectionReport();
			var reportPath = "..\\..\\..\\" + (string)comboBox1.SelectedItem;
			rpt.LoadLayout(XmlReader.Create(reportPath));
			rpt.PrintWidth = 6.5F;
			arvMain.LoadDocument(rpt);
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!button1.Enabled && comboBox1.SelectedItem != null)
			{
				button1.Enabled = true;
			}
		}

		private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}
	}
}
