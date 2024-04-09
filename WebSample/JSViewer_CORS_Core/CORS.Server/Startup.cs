using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using System.Text;

namespace JSViewer_CORS_Core
{
	public class Startup
	{
		private static readonly string CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? String.Empty;
		public static readonly DirectoryInfo ReportsDirectory = new DirectoryInfo(Path.Combine(CurrentDir, "Reports"));

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			services
				.AddLogging(config =>
				{
					// Disable the default logging configuration
					config.ClearProviders();

					// Enable logging for debug mode only
					if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
					{
						config.AddConsole();
					}
				})
				.AddReportViewer()
				.AddMvc(options => options.EnableEndpointRouting = false);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseFileServer();
			
			// Configure CORS
			app.UseCors(cors => cors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
				.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("Content-Disposition"));
			app.UseReportViewer(settings =>
			{
				settings.UseFileStore(ReportsDirectory);
			});

			app.UseMvc();
		}
	}
}
