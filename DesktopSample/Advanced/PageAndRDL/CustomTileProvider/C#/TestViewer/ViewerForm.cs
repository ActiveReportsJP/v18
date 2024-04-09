using System;
using System.Windows.Forms;

namespace ActiveReports.Samples.TestViewer
{
	public partial class ViewerForm : Form
	{
		public ViewerForm()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			viewer1.LoadDocument("../../../../../Reports/CustomTileProvider.rdlx");
		}
	}
}
