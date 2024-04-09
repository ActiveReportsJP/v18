using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.SectionReportModel;
using System;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ActiveReports.Samples.Charting
{
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		public ViewerForm()
		{
			//
			//Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			// TODO：InitializeComponentの呼び出しの後に、それはコンストラクタです。コードを追加してください
			//
		}
		
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

		/// <summary>
		/// ViewerForm上のViewerコントロールにレポートをロードします。
		/// ViewerコントロールがViewerFormインスタンス上にロードされたとき、Loadイベントは発生します。
		/// </summary>
		private void ViewerForm_Load(object sender, EventArgs e)
		{
			cboStyle.Items.Add(Properties.Resources.TwoDBarChart);
			cboStyle.Items.Add(Properties.Resources.ThreeDPieChart);
			cboStyle.Items.Add(Properties.Resources.ThreeDBarChart);
			cboStyle.Items.Add(Properties.Resources.FinanceChart);
			cboStyle.Items.Add(Properties.Resources.StackedAreaChart);

			// 「グラフの種類」コンボボックスの初期選択状態を設定します。
			cboStyle.SelectedIndex = 0;
		}

		private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			//「折れ線グラフ」選択時のみ、カスタムプロパティコンボボックスを有効にします。
			SetCustomProperties(cboStyle.SelectedIndex);
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			SectionReport rpt = new SectionReport();

			try
			{
				//「グラフの種類」コンボボックスにあわせて、プレビュー表示します。
				switch (cboStyle.SelectedIndex)
				{
					case 0: //  2D棒グラフ
						rpt.LoadLayout(XmlReader.Create(Properties.Resources.rpt2DBar));
						break;
					case 1: //  3D円グラフ
						rpt.LoadLayout(XmlReader.Create(Properties.Resources.rpt3DPie));
						//回転方向を設定します。
						if (cboCustom.SelectedIndex == 0)
						{
							((ChartControl)(rpt.Sections["Detail"].Controls["ChartSalesCategories"])).Series[0].Properties["Clockwise"] = true;
						}
						else
						{
							((ChartControl)(rpt.Sections["Detail"].Controls["ChartSalesCategories"])).Series[0].Properties["Clockwise"] = false;
						}
						break;
					case 2: // 3D棒グラフ
						rpt.AddAssembly(typeof(SQLiteConnection).Assembly);
						rpt.LoadLayout(XmlReader.Create (Properties.Resources.rpt3DBar));
						break;
					case 3: //  ファイナンスチャート
						rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptCandle));
						break;
					case 4: //  積層エリアグラフ
						rpt.LoadLayout(XmlReader.Create (Properties.Resources.rptStackedArea));
						break;
				}

				arvMain.LoadDocument(rpt ?? new SectionReport());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void SetCustomProperties(Int32 iGraphStype)
		{
			//選択候補をクリアします。
			cboCustom.Items.Clear();

			//選択候補を追加し、選択状態にします。
			switch (iGraphStype) 
			{
				case 1:	 // 2D円グラフ
					//可視状態にします。
					cboCustom.Visible = true;
					lblCustom.Visible = true;

					cboCustom.Items.Add(Properties.Resources.Clockwise);
					cboCustom.Items.Add(Properties.Resources.Counterclockwise);

					cboCustom.SelectedIndex = 1;

					//ラベルを設定します。
					lblCustom.Text = Properties.Resources.DirectionOfRotation;
					break;
					
				default:  //  その他
					//不可視状態にします。
					cboCustom.Visible = false;
					lblCustom.Visible = false;
					break;
			}

			Application.DoEvents();
		}
	}
}
