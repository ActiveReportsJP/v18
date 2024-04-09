using GrapeCity.ActiveReports.Web.Designer;

namespace WebDesigner_CustomStore.Implementation.Storage
{
	public class Report : ReportInfo
	{
		public byte[] Content { get; set; }
	}
	
	public class ImageResource : ImageResourceDescriptor
	{
		public byte[] Content { get; set; }
	}
	
	public class ThemeResource : ThemeResourceDescriptor
	{
		public byte[] Content { get; set; }
	}
	
	public class ReportResource : ReportResourceDescriptor
	{
		public byte[] Content { get; set; }
	}
}