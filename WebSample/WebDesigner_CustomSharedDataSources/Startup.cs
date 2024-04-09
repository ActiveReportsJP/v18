using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using System.Text;
using GrapeCity.ActiveReports.Web.Designer;
using WebDesigner_CustomSharedDataSources.Implementation;

namespace WebDesigner_CustomSharedDataSources
{
	public class Startup
	{
		private static readonly DirectoryInfo ResourcesRootDirectory = 
			new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "resources"));

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
				.AddSingleton<IReportStore>(new ReportStore(ResourcesRootDirectory))
				.AddSingleton<IResourceRepositoryProvider>(new ResourceProvider(ResourcesRootDirectory))
				.AddMvc(options => options.EnableEndpointRouting = false)
				.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, 
			IWebHostEnvironment env, 
			IReportStore reportStore, 
			IResourceRepositoryProvider resourceProvider
			)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var pathToConfig = Path.Combine(Environment.CurrentDirectory, "ActiveReports.config");

			app.UseReportDesigner(config =>
			{
				config.UseReportsProvider(reportStore);
				config.UseResourcesProvider(resourceProvider);
				config.UseConfig(pathToConfig);
			});

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseMvc();
		}
	}
}
