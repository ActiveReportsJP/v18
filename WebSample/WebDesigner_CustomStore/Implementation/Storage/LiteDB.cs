using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LiteDB;
using WebDesignerCustomStore.Implementation.Storage;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;
using WebDesigner_CustomStore.Implementation.Storage;

namespace WebDesignerCustomStore.Implementation.Database
{
	public class LiteDB : ICustomStorage
	{
		private const string IMAGES = "images";
		private const string THEMES = "themes";
		private const string REPORTS = "reports";
		private const string TEMPLATES = "templates";

		private readonly LiteDatabase _lite;

		public LiteDB(string databasePath)
		{
			_lite = new LiteDatabase(databasePath);
		}

		public void Dispose()
		{
			_lite.Dispose();
		}

		public Stream GetImage(string imageId)
		{
			var image = _lite.GetCollection<ImageResource>(IMAGES)
				.FindById(imageId);

			return new MemoryStream(image.Content);
		}

		public IEnumerable<ImageResourceDescriptor> GetImagesList()
		{
			var imagesList = _lite
				.GetCollection<ImageResource>(IMAGES)
				.FindAll()
				.Select(img => new ImageResourceDescriptor()
				{
					Id = img.Id,
					Name = img.Name,
					ContentType = img.ContentType,
					Thumbnail = new Thumbnail()
					{
						Data = Util.GetImageThumbnail(img.Content),
						ContentType = img.ContentType
					}
				});

			return imagesList;
		}

		public ReportDescriptor GetReportDescriptor(string reportId)
		{
			var report = _lite.GetCollection<Report>(REPORTS)
				.FindById(reportId);
			
			return new ReportDescriptor(report.ReportType);
		}

		public Stream GetReport(string reportId)
		{
			var report = _lite.GetCollection<Report>(REPORTS)
								  .FindById(reportId);

			if (report is null)
				return Stream.Null;
			
			return new MemoryStream(report.Content);
		}

		public string SaveReport(ReportType reportType, string reportId, Stream report)
		{
			_lite
				.GetCollection<Report>(REPORTS)
				.Upsert(new Report
				{
					Id = reportId,
					Name = reportId,
					ReportType = reportType,
					Content = report.ToArray(),
				});

			return reportId;
		}

		public void DeleteReport(string reportId)
		{
			_lite.GetCollection<ReportInfo>(REPORTS)
				 .Delete(reportId);
		}

		public IEnumerable<ReportInfo> GetReportsList()
		{
			return _lite.GetCollection<ReportInfo>(REPORTS)
						.FindAll();
		}

		public Stream GetTemplate(string templateId)
		{
			var template = _lite.GetCollection<ReportResource>(TEMPLATES)
								.FindById(templateId);
		
			if (template is null)
				return Stream.Null;
		
			return new MemoryStream(template.Content);
		}

		public ReportResourceDescriptor GetTemplatesDescriptor(string templateId)
		{
			var template = _lite.GetCollection<ReportResource>(TEMPLATES)
				.FindById(templateId);
		
			return template;
		}

		public IEnumerable<ReportResourceDescriptor> GetTemplatesList()
		{
			return _lite.GetCollection<ReportResource>(TEMPLATES)
				.FindAll();
		}

		public Stream GetTheme(string themeId)
		{
			var theme = _lite.GetCollection<ThemeResource>(THEMES)
							.FindById(themeId);

			if (theme is null)
				return Stream.Null;
			
			return new MemoryStream(theme.Content);
		}

		public IEnumerable<ThemeResourceDescriptor> GetThemesList()
		{
			var themesList = _lite
				.GetCollection<ThemeResource>(THEMES)
				.FindAll()
				.Cast<ThemeResourceDescriptor>();

			return themesList;
		}
	}
}
