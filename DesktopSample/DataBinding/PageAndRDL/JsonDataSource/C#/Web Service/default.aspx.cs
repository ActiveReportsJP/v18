using System;

namespace ActiveReports.Samples.WebService
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			messageLabel.Text = Properties.Resource.bodyOfMessage;
		}
	}
}
