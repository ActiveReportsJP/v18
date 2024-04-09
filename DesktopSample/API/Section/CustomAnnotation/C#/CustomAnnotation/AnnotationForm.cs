using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Document.Section.Annotations;

namespace ActiveReports.Samples.CustomAnnotation
{
	public partial class AnnotationForm : Form
	{
		/// <summary>
		/// AnnotationForm の概要の説明です。
		/// </summary>
		public AnnotationForm()
		{
			InitializeComponent();
		}

		private void AnnotationForm_Load(object sender, EventArgs e)
		{
			var resource = new ResourceManager(GetType());
			// 注釈用のカスタムボタンを追加します。
			ToolStrip ts = arvMain.Toolbar.ToolStrip;
			ToolStripButton tsbAnnotation = new ToolStripButton(resource.GetString("CustomAnnotation"));
			
			tsbAnnotation.Click += new EventHandler(tsbAnnotation_Click);
			ts.Items.Add(tsbAnnotation);

			//レイアウトをロードし、レポートを実行します。
			arvMain.LoadDocument(Properties.Resources.FileName);
		}

		void tsbAnnotation_Click(object sender, EventArgs e)
		{
			//注釈の有無に応じて、確認メッセージを表示します。 
			if (arvMain.Document.Pages[arvMain.ReportViewer.CurrentPage - 1].Annotations.Count > 0)
			{
				MessageBox.Show(Properties.Resources.StampMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			// 印鑑イメージをリソースから取得します。
			System.Reflection.Assembly thisExe;
			thisExe = System.Reflection.Assembly.GetExecutingAssembly();
			System.IO.Stream file = thisExe.GetManifestResourceStream("ActiveReports.Samples.CustomAnnotation.Resources.stamp.png");
			Bitmap imgStamp = new Bitmap(file);

			// 注釈を作成し、プロパティ値を割り当てます。
			AnnotationImage annoImg = new AnnotationImage();
			annoImg.BackgroundImage = imgStamp;			 // 画像
			annoImg.Color = Color.Transparent;			  // 背景色
			annoImg.BackgroundLayout = GrapeCity.ActiveReports.Document.Section.Annotations.ImageLayout.Zoom; // 表示形式
			annoImg.ShowBorder = false;					 //枠線表示（非表示） 

			// 注釈を追加します。
			// (追加位置の指定)
			annoImg.Attach(6.09F, 1.19F);
			arvMain.Document.Pages[arvMain.ReportViewer.CurrentPage - 1].Annotations.Add(annoImg);
			// (サイズの設定)
			annoImg.Height = 0.7F;
			annoImg.Width = 0.7F;

			//Viewerを更新します。 
			arvMain.Refresh();
		}
	}
}
