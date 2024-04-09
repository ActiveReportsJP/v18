using GrapeCity.DataVisualization.TypeScript;
using System;
using System.Globalization;
using System.Web;

namespace GrapeCity.ActiveReports.Samples.Web
{
    public partial class ParameterReport : System.Web.UI.Page
    {
        // Parametrized Report Generation
        SectionReport _rpt = new SectionReport();

		protected override void OnInit(EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoStore();
			Response.Cache.SetExpires(DateTime.MinValue);

			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// Load the report layout
			System.Xml.XmlTextReader
				xtr = new System.Xml.XmlTextReader(Server.MapPath("~") + @"\RpxReports\Params.rpx");
			_rpt.LoadLayout(xtr);
			xtr.Close();
			// Set parameter's value
			if (!string.IsNullOrEmpty(Request.QueryString["date"]))
			{
				var paramDate = DateTime.ParseExact(Request.QueryString["date"], "MM-dd-yyyy", CultureInfo.InvariantCulture);
				Calendar1.SelectedDate = paramDate;
				_rpt.Parameters[0].DefaultValue = paramDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
			}
			if (!IsPostBack)
			{
				// The default is set to HTML viewer.
				DateTime InitDate = new DateTime(1994, 8, 2);
				Calendar1.VisibleDate = InitDate;
			}
			_rpt.Restart();
			WebViewer.Report = _rpt;
		}

		protected void Calendar1_SelectionChanged(object sender, EventArgs e)
		{
			// Redirect to the report page with parameter's value
			Response.Redirect("ParameterReport.aspx?date=" + Calendar1.SelectedDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture));
		}
	}
}