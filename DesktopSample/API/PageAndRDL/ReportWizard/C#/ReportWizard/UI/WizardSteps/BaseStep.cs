using System.ComponentModel;
using System.Windows.Forms;

namespace ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	public partial class BaseStep : UserControl
	{
		private bool _firstDisplay;
		private string _stepTitle;
		private string _stepDescription;
		private ReportWizardState _wizState;

		public BaseStep()
		{
			_firstDisplay = true;
			_stepTitle = "";
			_stepDescription = "";
			InitializeComponent();
		}

		[Browsable(false)]
		public ReportWizardState ReportWizardState
		{
			get { return _wizState; }
			set { _wizState = value; }
		}

		[Browsable(true)]
		[Description("表示するウィザードのタイトル")]
		[Category("外観")]
		[DefaultValue("")]
		public string Title
		{
			get { return _stepTitle; }
			set { _stepTitle = value; }
		}

		[Browsable(true)]
		[Description("ステップの実装方法を説明するテキスト")]
		[Category("外観")]
		[DefaultValue("")]
		public string Description
		{
			get { return _stepDescription; }
			set { _stepDescription = value; }
		}

		public void OnDisplay()
		{
			OnDisplay(_firstDisplay);
			_firstDisplay = false;
		}

		protected virtual void OnDisplay(bool firstDisplay)
		{
		}

		/// <summary>
		///	ウィザードは前または次のステップへ移動する前に呼び出します。
		/// </summary>
		public virtual void UpdateState()
		{
		}
	}
}
