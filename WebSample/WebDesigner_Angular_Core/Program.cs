using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using GrapeCity.ActiveReports.Web.Designer;

internal class Program
{
    private static readonly DirectoryInfo ResourcesRootDirectory =
            new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "resources" + Path.DirectorySeparatorChar));

    private static readonly DirectoryInfo TemplatesRootDirectory =
        new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "templates" + Path.DirectorySeparatorChar));

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();

        builder.Services
                .AddReportViewer()
                .AddReportDesigner()
                .AddMvc(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
        }


        app.UseRouting();

        app.UseReportDesigner(config => config.UseFileStore(ResourcesRootDirectory, null, FileStoreOptions.NestedFoldersLookup));
        app.UseStaticFiles();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
