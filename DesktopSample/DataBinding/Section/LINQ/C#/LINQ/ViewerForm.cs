using GrapeCity.ActiveReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace ActiveReports.Samples.LINQ
{
	public partial class ViewerForm : Form
	{
		private System.Resources.ResourceManager _manager;
		public ViewerForm()
		{
			_manager = new System.Resources.ResourceManager(typeof(ViewerForm));
			InitializeComponent();
		}
	  
		// LINQtoObject用の構造体を定義します。
		private void ViewerForm_Load(object sender, EventArgs e)
		{
			// レポートを生成します。
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create("..\\..\\..\\rptLINQtoObject.rpx"));
			// レポートを実行します。
			arvMain.LoadDocument(rpt);
		}
	}
}
