using System.Collections.Concurrent;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;
using WebDesigner_Blazor_Custom.Data;
using WebDesigner_Blazor_Custom.Services;
using ReportInfo = GrapeCity.ActiveReports.Web.Designer.ReportInfo;

namespace WebDesigner_Blazor_Custom.Implementation;

internal class ReportStore : IReportStore
{
	private readonly ReportService _reportService;
	private readonly Dictionary<string, Report> _tempReportStorage = new();
	
	public ReportStore(ReportService reportService)
	{
		_reportService = reportService;
	}
	
	public ReportDescriptor GetReportDescriptor(string reportId)
	{
		if (_tempReportStorage.TryGetValue(reportId, out var reportData))
			return new ReportDescriptor(reportData.ReportType);
		
		var reportInfo = _reportService.GetReport(reportId);
		return new ReportDescriptor(reportInfo.ReportType);
	}

	public Stream LoadReport(string reportId)
	{
		if (_tempReportStorage.TryGetValue(reportId, out var reportData))
			return new MemoryStream(reportData.Content);
		
		return _reportService.GetReportContent(reportId);
	}

	public string SaveReport(ReportType reportType, string reportId, Stream reportData, SaveSettings settings = SaveSettings.None)
	{
		var newReport = new Report
		{
			Id = (settings & SaveSettings.IsTemporary) != 0 ? $"{Guid.NewGuid()}.rdlx" : reportId,
			Name = reportId,
			Content = reportData.ToArray(),
			ReportType = reportType
		};

		if ((settings & SaveSettings.IsTemporary) != 0)
			_tempReportStorage.Add(newReport.Id, newReport);
		else
			_reportService.SaveReport(newReport);
		
		return newReport.Id;
	}

	public string UpdateReport(ReportType reportType, string reportId, Stream reportData)
	{
		return SaveReport(reportType, reportId, reportData);
	}

	public ReportInfo[] ListReports()
	{
		return _reportService.GetReports().Cast<ReportInfo>().ToArray();
	}

	public void DeleteReport(string reportId)
	{
		if (_tempReportStorage.ContainsKey(reportId))
			_tempReportStorage.Remove(reportId, out _);
		else
			_reportService.DeleteReport(reportId);
	}
}