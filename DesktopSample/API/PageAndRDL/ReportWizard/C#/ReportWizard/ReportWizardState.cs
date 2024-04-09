using System.Collections.Generic;
using ActiveReports.Samples.ReportWizard.MetaData;

namespace ActiveReports.Samples.ReportWizard 
{
	public class ReportWizardState
	{
		public ReportWizardState()
		{
			AvailableMasterReports = new List<ReportMetaData>();
			GroupingFields = new List<FieldMetaData>();
			DisplayFields = new List<FieldMetaData>();
		}

		public readonly List<ReportMetaData> AvailableMasterReports;
		
		public ReportMetaData SelectedMasterReport;
		public readonly List<FieldMetaData> GroupingFields;
		public FieldMetaData DetailGroupingField;
		public readonly List<FieldMetaData> DisplayFields;
		public bool DisplayGroupSubtotals;
		public bool DisplayGrandTotals;
	}
}
