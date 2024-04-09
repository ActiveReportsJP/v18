using System;
using System.IO;
using System.Linq;
using System.Web;
using GrapeCity.ActiveReports.Aspnet.Designer;

using Owin;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using System.Web.Mvc;
using System.Web.Routing;
using GrapeCity.ActiveReports.Web.Designer;

[assembly: OwinStartup(typeof(WebDesigner_MVC.Startup))]

namespace WebDesigner_MVC
{
	public class Startup
	{
		private static readonly DirectoryInfo ResourcesRootDirectory =
			new DirectoryInfo(String.Format(@"{0}.\resources\", HttpRuntime.AppDomainAppPath));
		private static readonly DirectoryInfo TemplatesRootDirectory =
			new DirectoryInfo(String.Format(@"{0}.\templates\", HttpRuntime.AppDomainAppPath));

		public void Configuration(IAppBuilder app)
		{
			app.UseErrorPage();
			
			app.UseReportDesigner(config => 
				config.UseFileStore(ResourcesRootDirectory, null, FileStoreOptions.NestedFoldersLookup));

			app.UseStaticFiles(new StaticFileOptions { FileSystem = new PhysicalFileSystem(String.Format(@"{0}.\wwwroot\", HttpRuntime.AppDomainAppPath)) });
		}
	}
}
