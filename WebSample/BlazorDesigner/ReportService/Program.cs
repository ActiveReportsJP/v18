using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using System.Text;
using GrapeCity.ActiveReports.Web.Designer;

var builder = WebApplication.CreateBuilder(args);

var ResourcesRootDirectory =
	new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "resources"));

var TemplatesRootDirectory =
	new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "templates"));

var DataSetsRootDirectory =
	new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "datasets"));

// Add services to the container.
builder.Services.AddLogging(config =>
{
	// Disable the default logging configuration
	config.ClearProviders();

	// Enable logging for debug mode only
	if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
	{
		config.AddConsole();
	}
});

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddReportViewer();
builder.Services.AddReportDesigner();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// Configure CORS
app.UseCors(cors => cors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
	.AllowAnyMethod()
	.AllowAnyHeader()
	.AllowCredentials()
	.WithExposedHeaders("Content-Disposition"));

app.UseReportDesigner(config => config.UseFileStore(ResourcesRootDirectory, null, FileStoreOptions.NestedFoldersLookup));

app.UseMvc();

app.Run();
