using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GrapeCity.ActiveReports.Rdl.Themes;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;
using Microsoft.Azure.Cosmos;
using WebDesigner_CustomStore.Implementation.Storage;

namespace WebDesignerCustomStore.Implementation.Storage
{
	public class CosmoDBInitializer
	{
		private static readonly string _root = Path.Combine(Directory.GetCurrentDirectory(), "resources", "CosmosDB");
		
		private readonly DirectoryInfo _imagesDir = new DirectoryInfo(Path.Combine(_root, "images"));
		private readonly DirectoryInfo _templatesDir = new DirectoryInfo(Path.Combine(_root, "templates"));
		private readonly DirectoryInfo _themesDir = new DirectoryInfo(Path.Combine(_root, "themes"));
		private readonly DirectoryInfo _reportsDir = new DirectoryInfo(Path.Combine(_root, "reports"));
		
		private static readonly string[] ReportExtensions =
		{
			".rdl",
			".rdlx",
			".rdlx-master",
			".rpx"
		};
		
		public async Task Initialize()
		{
			var clientOptions = new CosmosClientOptions()
			{
				SerializerOptions = new CosmosSerializationOptions(){
					IgnoreNullValues = true,
					PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
				},
				ConnectionMode = ConnectionMode.Gateway,
			};
			
			var connectionString = ConfigurationManager.ConnectionStrings["CosmosDB"].ConnectionString;
			var client = new CosmosClient(connectionString, clientOptions);

			if (DataBaseExists(client))
				return;
			
			var db = await client.CreateDatabaseIfNotExistsAsync(CosmoDB.DATABASE_NAME);
			var reportsContainer = await db.Database.CreateContainerIfNotExistsAsync(CosmoDB.REPORTS, "/reports");
			var themesContainer = await db.Database.CreateContainerIfNotExistsAsync(CosmoDB.THEMES, "/themes");
			var imagesContainer = await db.Database.CreateContainerIfNotExistsAsync(CosmoDB.IMAGES, "/images");
			var templatesContainer = await db.Database.CreateContainerIfNotExistsAsync(CosmoDB.TEMPLATES, "/templates");

			AddImages(imagesContainer.Container);
			AddThemes(themesContainer.Container);
			AddTemplates(templatesContainer.Container);
			AddReports(reportsContainer.Container);
		}

		private bool DataBaseExists(CosmosClient client)
		{
			try
			{
				var cosmosDatabase = client.GetDatabase(CosmoDB.DATABASE_NAME);
				cosmosDatabase.ReadAsync().GetAwaiter().GetResult();

				return true;
			}
			catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				return false;
			}
		}

		private void AddImages(Container imagesContainer)
		{
			foreach (var file in _imagesDir.EnumerateFiles())
			{
				var image = CreateImageResource(file);
				imagesContainer.CreateItemAsync(image, PartitionKey.None).GetAwaiter().GetResult();
			}
		}

		private ImageResource CreateImageResource(FileInfo file)
		{
			using var content = file.OpenRead();
			var image = new ImageResource
			{
				Id = file.Name,
				Name = file.Name,
				Content = content.ToArray(),
				ContentType = MimeTypeByExtension[file.Extension],
			};
			return image;
		}

		private void AddThemes(Container themesContainer)
		{
			foreach (var file in _themesDir.EnumerateFiles())
			{
				var theme = CreateThemeResource(file);
				themesContainer.CreateItemAsync(theme, PartitionKey.None).GetAwaiter().GetResult();
			}
		}
		
		private ThemeResource CreateThemeResource(FileInfo file)
		{
			using var stream = file.OpenRead();
			var theme = Theme.Load(stream);

			var resource = new ThemeResource()
			{
				Id = file.Name,
				Title = Path.GetFileNameWithoutExtension(file.Name),
				Dark1 = theme.Colors.Dark1,
				Dark2 = theme.Colors.Dark2,
				Light1 = theme.Colors.Light1,
				Light2 = theme.Colors.Light2,
				Accent1 = theme.Colors.Accent1,
				Accent2 = theme.Colors.Accent2,
				Accent3 = theme.Colors.Accent3,
				Accent4 = theme.Colors.Accent4,
				Accent5 = theme.Colors.Accent5,
				Accent6 = theme.Colors.Accent6,
				MajorFontFamily = theme.Fonts.MajorFont.Family,
				MinorFontFamily = theme.Fonts.MinorFont.Family,
				Content = file.OpenRead().ToArray()
			};

			return resource;
		}
		
		private void AddTemplates(Container templatesContainer)
		{
			foreach (var file in _templatesDir.EnumerateFiles().Where(fileInfo => ReportExtensions.Any(ext =>
				         fileInfo.Extension.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase))))
			{
				var template = CreateReportResource(file);
				templatesContainer.CreateItemAsync(template, PartitionKey.None).GetAwaiter().GetResult();
			}
		}
		
		private ReportResource CreateReportResource(FileInfo file)
		{
			using var content = file.OpenRead();
			var template = new ReportResource
				{
					Id = file.Name,
					Name = file.Name,
					Content = content.ToArray(),
					ReportType = GetReportTypeByExtension(file.Extension),
					Thumbnail = CreateThumbnail(Path.GetFileNameWithoutExtension(file.Name))
				};

			return template;
		}

		private Thumbnail CreateThumbnail(string templateName)
		{
			var thumbnail = _templatesDir.EnumerateFiles()
				.Where(f => MimeTypeByExtension.ContainsKey(f.Extension))
				.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f.Name) == templateName);
			
			if (thumbnail == null)
				return null;
		
			using var image = thumbnail.OpenRead();
			return new Thumbnail()
			{
				Data = image.ToArray(),
				ContentType = MimeTypeByExtension[thumbnail.Extension]
			};
		}
		
		private void AddReports(Container reportsContainer)
		{
			foreach (var file in _reportsDir.EnumerateFiles("*", SearchOption.AllDirectories).Where(fileInfo => ReportExtensions.Any(ext =>
				         fileInfo.Extension.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase))))
			{
				var report = CreateReport(file);
				reportsContainer.CreateItemAsync(report, PartitionKey.None).GetAwaiter().GetResult();
			}
		}
		
		private Report CreateReport(FileInfo file)
		{
			using var content = file.OpenRead();
			var report = new Report
			{
				Id = file.Name,
				Name = file.Name,
				ReportType = GetReportTypeByExtension(file.Extension),
				Content = content.ToArray(),
			};
			
			return report;
		}
		
		private static ReportType GetReportTypeByExtension(string extension)
		{
			switch (extension)
			{
				case ".rdl":
				case ".rdlx":
					return ReportType.RdlXml;
				case ".rdlx-master":
					return ReportType.RdlMasterXml;
				case ".rpx":
					return ReportType.RpxXml;
				default:
					throw new ArgumentOutOfRangeException(nameof(extension), extension, null);
			}
		}
		
		private static readonly IDictionary<string, string> MimeTypeByExtension = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
		{
			{".bmp", "image/bmp"},
			{".dib", "image/bmp"},
			{".emf", "image/x-emf"},
			{".gif", "image/gif"},
			{".ico", "image/x-icon"},
			{".jfif", "image/pjpeg"},
			{".jpe", "image/jpeg"},
			{".jpeg", "image/jpeg"},
			{".jpg", "image/jpeg"},
			{".pbm", "image/x-portable-bitmap"},
			{".pct", "image/pict"},
			{".pic", "image/pict"},
			{".pict", "image/pict"},
			{".png", "image/png"},
			{".pnz", "image/png"},
			{".svg", "image/svg+xml" },
			{".tif", "image/tiff"},
			{".tiff", "image/tiff"},
			{".wmf", "image/x-wmf"},
			{".xbm", "image/x-xbitmap"},
		};
	}
}