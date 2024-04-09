using System.Diagnostics;
using System.Windows.Forms;

namespace ActiveReports.Samples.TestDesignerPro
{
	public partial class HelperForm : Form
	{
		public HelperForm()
		{
			InitializeComponent();
		}

		private void rtfHelp_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo {FileName = e.LinkText, UseShellExecute = true});
		}

		private void HelperForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		private void rtfHelp_TextChanged(object sender, System.EventArgs e)
		{

		}
	}
}
