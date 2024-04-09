using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;
using WebDesignerCustomStore.Implementation.Storage;

namespace WebDesignerCustomStore.Implementation
{
	public class ReportStore : IReportStore
	{
		private readonly ICustomStorage _store;
		private readonly Dictionary<string, byte[]> _tempStorage = new Dictionary<string, byte[]>();
		
		public ReportStore(ICustomStorage store)
		{
			_store = store;
		}
		
		public ReportDescriptor GetReportDescriptor(string reportId)
		{
			if (_tempStorage.ContainsKey(reportId))
				return new ReportDescriptor(GetReportTypeByExtension(Path.GetExtension(reportId)));
			
			return _store.GetReportDescriptor(reportId);
		}

		public Stream LoadReport(string reportId)
		{
			if (_tempStorage.TryGetValue(reportId, out var tempReport))
				return new MemoryStream(tempReport);
			
			return _store.GetReport(reportId);
		}

		public string SaveReport(ReportType reportType, string reportId, Stream reportData, SaveSettings settings = SaveSettings.None)
		{
			if ((settings & SaveSettings.IsTemporary) != 0)
			{
				var tempName = Guid.NewGuid() + GetReportExtension(reportType);
				_tempStorage.Add(tempName, reportData.ToArray());
				return tempName;
			}
			
			return _store.SaveReport(reportType, reportId, reportData);
		}

		public string UpdateReport(ReportType reportType, string reportId, Stream reportData)
		{
			return _store.SaveReport(reportType, reportId, reportData);
		}

		public ReportInfo[] ListReports()
		{
			return _store.GetReportsList().ToArray();
		}

		public void DeleteReport(string reportId)
		{
			if (_tempStorage.ContainsKey(reportId))
			{
				_tempStorage.Remove(reportId);
				return;
			}
			
			_store.DeleteReport(reportId);
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
	}
}