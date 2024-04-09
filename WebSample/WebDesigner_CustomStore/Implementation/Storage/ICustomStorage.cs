using System;
using System.Collections.Generic;
using System.IO;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;

namespace WebDesignerCustomStore.Implementation.Storage
{
	public interface ICustomStorage : IDisposable
	{
		Stream GetTheme(string themeId);
		IEnumerable<ThemeResourceDescriptor> GetThemesList();
		
		Stream GetImage(string imageId);
		IEnumerable<ImageResourceDescriptor> GetImagesList();

		ReportDescriptor GetReportDescriptor(string reportId);
		Stream GetReport(string reportId);
		string SaveReport(ReportType reportType, string reportId, Stream report);
		void DeleteReport(string reportId);
		IEnumerable<ReportInfo> GetReportsList();

		Stream GetTemplate(string templateId);
		IEnumerable<ReportResourceDescriptor> GetTemplatesList();
		ReportResourceDescriptor GetTemplatesDescriptor(string templateId);
	}
}
