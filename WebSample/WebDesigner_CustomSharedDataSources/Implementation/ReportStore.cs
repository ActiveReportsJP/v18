using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;
using ReportNotFoundException = GrapeCity.ActiveReports.Web.Designer.ReportNotFoundException;

namespace WebDesigner_CustomSharedDataSources.Implementation;

public class ReportStore : IReportStore
{
	private static readonly string[] ReportExtensions =
	{
		".rdl",
		".rdlx",
		".rdlx-master",
		".rpx"
	};
	
	private readonly Dictionary<string, byte[]> _tempStorage = new Dictionary<string, byte[]>();
	
	private readonly DirectoryInfo _rootDirectory;

	public ReportStore (DirectoryInfo rootDirectory)
	{
		_rootDirectory = rootDirectory;
	}
	
	public ReportDescriptor GetReportDescriptor(string reportId)
	{
		if (_tempStorage.ContainsKey(reportId))
			return new ReportDescriptor(GetReportTypeByExtension(Path.GetExtension(reportId)));
		
		var fileInfo = new FileInfo(Path.Combine(_rootDirectory.FullName, reportId));
		return new ReportDescriptor(GetReportTypeByExtension(fileInfo.Extension));
	}

	public Stream LoadReport(string reportId)
	{
		if (_tempStorage.TryGetValue(reportId, out var tempReport))
			return new MemoryStream(tempReport);
		
		var file = new FileInfo(Path.Combine(_rootDirectory.FullName, reportId));
		
		if (!file.Exists)
			throw new ReportNotFoundException();

		return file.OpenRead();
	}

	public string SaveReport(ReportType reportType, string reportId, Stream reportData, SaveSettings settings = SaveSettings.None)
	{
		if ((settings & SaveSettings.IsTemporary) != 0)
		{
			var tempName = Guid.NewGuid() + GetReportExtension(reportType);
			_tempStorage.Add(tempName, reportData.ToArray());
			return tempName;
		}
		
		var reportFullPath = Path.Combine(_rootDirectory.FullName, reportId);
		using var fileStream = new FileStream(reportFullPath, FileMode.Create, FileAccess.Write);
		reportData.CopyTo(fileStream);

		return reportId;
	}

	public string UpdateReport(ReportType reportType, string reportId, Stream reportData)
	{
		return SaveReport(reportType, reportId, reportData);
	}

	public ReportInfo[] ListReports()
	{
		var reports = _rootDirectory
			.EnumerateFiles("*.*")
			.Where(fileInfo => ReportExtensions.Any(ext =>
				fileInfo.Extension.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)))
			.Select(fileInfo => new ReportInfo()
			{
				Id = fileInfo.Name,
				Name = fileInfo.Name,
				ReportType = GetReportTypeByExtension(fileInfo.Extension),
			}).ToArray();

		return reports;
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
	
	private static string GetReportExtension(ReportType type)
	{
		return type switch
		{
			ReportType.RdlXml => ".rdlx",
			ReportType.RdlMasterXml => ".rdlx-master",
			ReportType.RpxXml => ".rpx",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}

	public void DeleteReport(string reportId)
	{
		if (_tempStorage.ContainsKey(reportId))
		{
			_tempStorage.Remove(reportId);
			return;
		}
        
		var file = new FileInfo(Path.Combine(_rootDirectory.FullName, reportId));
        
		if(file.Exists)
			file.Delete();;
	}
	
}