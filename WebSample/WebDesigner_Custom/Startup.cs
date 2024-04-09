using System.IO;
using System.Text;
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using GrapeCity.ActiveReports.Web.Designer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebDesigner_Custom.Data;
using WebDesigner_Custom.Implementation;
using WebDesigner_Custom.Services;

namespace WebDesigner_Custom;

public class Startup
{
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
			.AddDbContext<ReportsDbContext>(ServiceLifetime.Singleton)
			.AddSingleton<ReportService>()
			.AddSingleton<IReportStore>(s => new ReportStore(s.GetRequiredService<ReportService>()))
			.AddSingleton<IResourceRepositoryProvider, ResourceProvider>()
			.AddMvc(options => options.EnableEndpointRouting = false)
			.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IReportStore reportStore, IResourceRepositoryProvider resourceProvider)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		
			
		app.UseReportDesigner(config =>
		{
			config.UseReportsProvider(reportStore);
			config.UseResourcesProvider(resourceProvider);
		});
		app.UseFileServer();
		app.UseMvc();
	}
}
