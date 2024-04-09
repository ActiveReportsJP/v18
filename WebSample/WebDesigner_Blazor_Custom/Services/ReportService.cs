using WebDesigner_Blazor_Custom.Data;

namespace WebDesigner_Blazor_Custom.Services;

internal class ReportService
{
    private readonly ReportsDbContext _reportsDbContext;
    
    public ReportService(ReportsDbContext reportsDbContext)
    {
        _reportsDbContext = reportsDbContext;
    }

    public Report GetReport(string id)
    {
        var reportData = _reportsDbContext.Reports.FirstOrDefault(r => r.Id == id);
    
        return reportData ?? throw new ArgumentNullException(nameof(reportData));
    }

    public Stream GetReportContent(string id)
    {
		var reportData = _reportsDbContext.Reports.FirstOrDefault(r => r.Id == id);

        if (reportData == null) 
           return Stream.Null;

		return new MemoryStream(reportData.Content);
    }

    public List<Report> GetReports()
    {
        var reports = _reportsDbContext.Reports.ToList();
        return reports;
    }

    public void SaveReport(Report report)
    {
        if(_reportsDbContext.Reports == null)
			throw new Exception("The list of reports is empty");

        var reportInfo = _reportsDbContext.Reports.FirstOrDefault(r => r.Id == report.Id);
        if (reportInfo != null)
        {
            if (reportInfo.Readonly)
                throw new Exception("Original report cannot be changed, use 'Save As' with new report name");

            reportInfo.Content = report.Content;
        }
        else
        {
            _reportsDbContext.Reports.Add(report);
        }
        
        _reportsDbContext.SaveChanges();
    }

    public void DeleteReport(string id)
    {
		if (_reportsDbContext.Reports == null)
			throw new Exception("The list of reports is empty");

		var reportInfo = _reportsDbContext.Reports.FirstOrDefault(r => r.Id == id);

        if(reportInfo == null) 
            return; 

        _reportsDbContext.Reports.Remove(reportInfo);
        _reportsDbContext.SaveChanges();
    }
}
