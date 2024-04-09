using System.Web.Services;
using System.Web.Script.Services;

namespace ActiveReports.Samples.WebService
{
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{
		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public string GetJson()
		{
			return Properties.Resource.customers;
		}
	}
}
