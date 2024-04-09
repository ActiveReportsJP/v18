using GrapeCity.ActiveReports.Design.Advanced;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using GrapeCity.ActiveReports.Viewer.Win;

namespace ActiveReports.Samples.TestDesignerPro.Export
{
	internal class ExportViewerFactory : IExportViewerFactory
	{
		public IExportViewer CreateExportViewer(Viewer viewer)
		{
			return new ExportViewer(viewer);
		}
	}
}
