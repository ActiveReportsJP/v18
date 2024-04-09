using System.IO;
using Microsoft.Owin;
using Owin;
using System.Web.Routing;
using GrapeCity.ActiveReports.Aspnet.Viewer;
using System.Reflection;

[assembly: OwinStartup(typeof(JSViewer_MVC.Startup))]

namespace JSViewer_MVC
{
    public class Startup
    {
        private static readonly string CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)?.Replace("file:\\", "");
        public static readonly DirectoryInfo ReportsDirectory = new DirectoryInfo(Path.Combine(CurrentDir, "Reports"));

        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();

            app.UseReportViewer(settings =>
            {
                settings.UseFileStore(ReportsDirectory);
            });

            RouteTable.Routes.RouteExistingFiles = true;
        }
    }
}
