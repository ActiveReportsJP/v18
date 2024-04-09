using System.Collections.Generic;

namespace ActiveReports.Samples.ReportWizard.MetaData
{
	public class ReportMetaData
	{
		private string _title;

		public ReportMetaData()
		{
			Title = "";
			MasterReportFile = "";
			Fields = new Dictionary<string, FieldMetaData>();
		}

		// データ連結用のプロパティ。
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public string MasterReportFile;
		public readonly Dictionary<string, FieldMetaData> Fields;
	}
}
