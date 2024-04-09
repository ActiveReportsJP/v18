
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.IListBinding
{
	/// <summary>
	/// ViewerForm の概要の説明です。
	/// </summary>
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		public ViewerForm()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();
		}

		public void LoadDocument(SectionReport rpt)
		{
			arvMain.LoadDocument(rpt);
		}
	}
}
