using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using GrapeCity.ActiveReports.Aspnetcore.Viewer;

namespace JSViewerReactCore
{
    public class Startup
    {
        private static readonly string CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        public static readonly DirectoryInfo ReportsDirectory = new DirectoryInfo(Path.Combine(CurrentDir, "Reports"));

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
                .AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseReportViewer(settings =>
            {
                settings.UseFileStore(ReportsDirectory);
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
