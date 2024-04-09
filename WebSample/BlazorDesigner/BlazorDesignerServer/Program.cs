using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using GrapeCity.ActiveReports.Web.Designer;

var builder = WebApplication.CreateBuilder(args);

var ResourcesRootDirectory =
	new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "resources"));

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Add services to the container.
builder.Services.AddReportViewer();
builder.Services.AddReportDesigner();
builder.Services.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddServerSideBlazor();
builder.Services.Configure<HubOptions>(options =>
{
	options.MaximumReceiveMessageSize = 524288000; //500MB
});

builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// For use as a server for BlazorWebAssembly
app.UseCors(cors => cors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
	.AllowAnyMethod()
	.AllowAnyHeader()
	.AllowCredentials()
	.WithExposedHeaders("Content-Disposition"));

app.UseReportDesigner(config => config.UseFileStore(ResourcesRootDirectory, null, FileStoreOptions.NestedFoldersLookup));

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
