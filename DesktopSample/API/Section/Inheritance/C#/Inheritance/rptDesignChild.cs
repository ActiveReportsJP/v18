using System.Resources;

namespace ActiveReports.Samples.Inheritance
{
	/// <summary>
	/// rptDesignChild の概要の説明です。
	/// </summary>
	public partial class rptDesignChild : Inheritance.rptDesignBase
	{
		private ResourceManager _resource;
		public rptDesignChild()
		{
			_resource = new ResourceManager(typeof(rptDesignChild));
			//
			// ActiveReport デザイナ サポートに必要です。
			//

			InitializeComponent();
		}
	}
}
