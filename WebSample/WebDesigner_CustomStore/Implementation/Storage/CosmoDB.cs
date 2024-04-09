using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;
using WebDesigner_CustomStore.Implementation.Storage;

namespace WebDesignerCustomStore.Implementation.Storage
{
	public class CosmoDB : ICustomStorage
	{
		internal const string DATABASE_NAME = "cosmos";

		internal const string IMAGES = "images";
		internal const string THEMES = "themes";
		internal const string REPORTS = "reports";
		internal const string TEMPLATES = "templates";

		private readonly List<(string db, string container)> _containers = new()
		{
			(DATABASE_NAME, "images"),
			(DATABASE_NAME, "themes"),
			(DATABASE_NAME, "templates"),
			(DATABASE_NAME, "reports")
		};

		private readonly CosmosClient _client;
		private readonly Microsoft.Azure.Cosmos.Database _db;

		public CosmoDB()
		{
			var connectionString = ConfigurationManager.ConnectionStrings["CosmosDB"].ConnectionString;
			
			var clientOptions = new CosmosClientOptions()
			{
				SerializerOptions = new CosmosSerializationOptions(){
					IgnoreNullValues = true,
					PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
				},
				ConnectionMode = ConnectionMode.Gateway,
			};
			
			_client = CosmosClient.CreateAndInitializeAsync(connectionString, _containers, clientOptions).Result;
			_db = _client.GetDatabase(DATABASE_NAME);
		}

		public void Dispose()
		{
			_client.Dispose();
		}

		public Stream GetImage(string imageId)
		{
			var response = _db.GetContainer(IMAGES)
				.ReadItemAsync<ImageResource>(imageId, PartitionKey.None)
				.Result;

			if (response.StatusCode != HttpStatusCode.OK)
				return null;
			
			return new MemoryStream(response.Resource.Content);
		}

		public IEnumerable<ImageResourceDescriptor> GetImagesList()
		{
			var imagesList = _db.GetContainer(IMAGES)
				.GetItemLinqQueryable<ImageResource>(true)
				.AsEnumerable()
				.Select(image => new ImageResourceDescriptor
				{
					Id = image.Id,
					Name = image.Name,
					ContentType = image.ContentType,
					Thumbnail = new Thumbnail()
					{
						ContentType = image.ContentType,
						Data = Util.GetImageThumbnail(image.Content)
					},
				});

			return imagesList;
		}
		
		public ReportDescriptor GetReportDescriptor(string reportId)
		{
			var response = _db.GetContainer(REPORTS).ReadItemAsync<Report>(reportId, PartitionKey.None).Result;
			
			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			return new ReportDescriptor(response.Resource.ReportType);
		}

		public Stream GetReport(string reportId)
		{
			var response = _db.GetContainer(REPORTS).ReadItemAsync<Report>(reportId, PartitionKey.None).Result;

			if (response.StatusCode != HttpStatusCode.OK)
			 	return null;

			return new MemoryStream(response.Resource.Content);
		}

		public string SaveReport(ReportType reportType, string reportId, Stream report)
		{
			var reportInfo = new Report
			{
				Id = reportId,
				Name = reportId,
				ReportType = reportType,
				Content = report.ToArray(),
			};

			_db.GetContainer(REPORTS).CreateItemAsync<Report>(reportInfo, PartitionKey.None).GetAwaiter().GetResult();
			
			return reportId;
		}

		public void DeleteReport(string reportId)
		{
			_db.GetContainer(REPORTS)
				.DeleteItemAsync<Report>(reportId, PartitionKey.None)
				.GetAwaiter()
				.GetResult();
		}

		public IEnumerable<ReportInfo> GetReportsList()
		{
			var reportsList = _db.GetContainer(REPORTS)
				.GetItemLinqQueryable<Report>(true)
				.AsEnumerable();

			return reportsList;
		}

		public Stream GetTemplate(string templateId)
		{
			var response = _db.GetContainer(TEMPLATES)
				.ReadItemAsync<ReportResource>(templateId, PartitionKey.None)
				.Result;
			
			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			return new MemoryStream(response.Resource.Content);
		}
		
		public ReportResourceDescriptor GetTemplatesDescriptor(string templateId)
		{
			try
			{
				var response = _db.GetContainer(TEMPLATES)
					.ReadItemAsync<ReportResource>(templateId, PartitionKey.None)
					.Result;
			
				if (response.StatusCode != HttpStatusCode.OK)
					return null;

				return response.Resource;
			}
			catch (AggregateException ex) when (ex.InnerException is CosmosException cex && cex.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				//ignore not found exception
			}

			return null;
		}

		public IEnumerable<ReportResourceDescriptor> GetTemplatesList()
		{
			var templatesList = _db.GetContainer(TEMPLATES)
				.GetItemLinqQueryable<ReportResource>(true)
				.AsEnumerable();
			
			return templatesList;
		}

		public Stream GetTheme(string themeId)
		{
			var response = _db.GetContainer(THEMES).ReadItemAsync<ThemeResource>(themeId, PartitionKey.None).Result;
			
			if (response.StatusCode != HttpStatusCode.OK)
			 	return Stream.Null;
			
			return new MemoryStream(response.Resource.Content);
		}

		public IEnumerable<ThemeResourceDescriptor> GetThemesList()
		{
			var themesList = _db.GetContainer(THEMES)
								.GetItemLinqQueryable<ThemeResource>(true)
								.AsEnumerable();
			
			return themesList;
		}
	}
}
