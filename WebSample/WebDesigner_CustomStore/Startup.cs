using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using WebDesignerCustomStore.Implementation.Storage;
using System.Text;
using GrapeCity.ActiveReports.Web.Designer;
using WebDesignerCustomStore.Implementation;

namespace WebDesignerCustomStore
{
	public class Startup
	{
		private static readonly string ResourcesRoot = Path.Combine(Directory.GetCurrentDirectory(), "resources");

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			// Add implemented IResourcesService to service collection for correct disposing and lifetime
			services
				.AddReportViewer()
				.AddReportDesigner()
				.AddSingleton<ICustomStorage>(s => new Implementation.Database.LiteDB(Path.Combine(ResourcesRoot, "lite.db")))
				//.AddSingleton<ICustomStorage, CosmoDB>()
				.AddSingleton<IReportStore>(s => new ReportStore(s.GetRequiredService<ICustomStorage>()))
				.AddSingleton<IResourceRepositoryProvider>(s => new ResourceProvider(s.GetRequiredService<ICustomStorage>()))
				.AddMvc(options => options.EnableEndpointRouting = false)
				.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//cosmosDB initializer
			//var cosmoDbInitializer = new CosmoDBInitializer();
			//cosmoDbInitializer.Initialize().Wait();
				
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			

			
			var reportStore = app.ApplicationServices.GetRequiredService<IReportStore>();
			var resourceProvider = app.ApplicationServices.GetRequiredService<IResourceRepositoryProvider>();
			
			app.UseReportDesigner(config =>
			{
				config.UseReportsProvider(reportStore);
				config.UseResourcesProvider(resourceProvider);
			});
			app.UseFileServer();
			app.UseMvc();
		}
	}
}
