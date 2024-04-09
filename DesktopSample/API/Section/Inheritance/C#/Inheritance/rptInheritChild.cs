
namespace ActiveReports.Samples.Inheritance
{
	/// <summary>
	/// ChildReport の概要の説明です。
	/// </summary>
	public partial class rptInheritChild : Inheritance.rptInheritBase
	{
		public rptInheritChild()
		{
			//
			// ActiveReport デザイナ サポートに必要です。
			//
			InitializeComponent();
			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			// csvファイルのパスを設定します。		   
			CsvPath = "../../../Customers.csv";
		}
	}
}
