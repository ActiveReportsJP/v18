using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using System.Data.SQLite.EF6;
using System.Text;
using C1.AdoNet.OData;

namespace WebDesignerCustomDataProviders
{
	public class Startup
	{
		private static readonly DirectoryInfo ResourcesRootDirectory = 
			new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "resources" + Path.DirectorySeparatorChar));
		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			services
				.AddReportViewer()
				.AddReportDesigner()
				.AddMvc(options => options.EnableEndpointRouting = false)
				.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}


			
			app.UseReportDesigner(config =>
			{
				config.UseFileStore(ResourcesRootDirectory);
				config.UseDataProviders(
				[
					new GrapeCity.ActiveReports.Web.Designer.DataProviderInfo("SQLITE",
						typeof(SQLiteProviderFactory).AssemblyQualifiedName,
						typeof(SQLiteConnectionAdapter).AssemblyQualifiedName),
					new GrapeCity.ActiveReports.Web.Designer.DataProviderInfo("ODATA",
						typeof(C1ODataProviderFactory).AssemblyQualifiedName,
						typeof(C1ODataConnectionAdapter).AssemblyQualifiedName)
				]);
				config.OmitViewerConfiguration = true;
			});
			
			app.UseReportViewer(config =>
			{
				config.UseFileStore(ResourcesRootDirectory);
				config.UseDataProviders(
				[
					new GrapeCity.ActiveReports.Web.Viewer.DataProviderInfo("SQLITE",
						typeof(SQLiteProviderFactory).AssemblyQualifiedName,
						typeof(SQLiteConnectionAdapter).AssemblyQualifiedName),
					new GrapeCity.ActiveReports.Web.Viewer.DataProviderInfo("ODATA",
						typeof(C1ODataProviderFactory).AssemblyQualifiedName,
						typeof(C1ODataConnectionAdapter).AssemblyQualifiedName)
				]);
			});
			app.UseFileServer();
			app.UseMvc();
		}
	}
}
