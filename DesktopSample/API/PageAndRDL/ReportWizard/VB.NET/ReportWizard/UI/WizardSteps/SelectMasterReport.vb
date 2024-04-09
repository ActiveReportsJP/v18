Imports ActiveReports.Samples.ReportWizard.MetaData

Partial Public Class SelectMasterReport
	Inherits BaseStep
	Public Sub New()
		InitializeComponent()
	End Sub
	Protected Overloads Overrides Sub OnDisplay(ByVal firstDisplay As Boolean)
		If firstDisplay Then
			reportList.DataSource = ReportsForm.AvailableReportTemplates
			reportList.DisplayMember = "Title"

			reportList.SelectedIndex = 0
		End If
	End Sub
	Public Overloads Overrides Sub UpdateState()
		ReportWizardState.SelectedMasterReport = CType(reportList.SelectedItem, ReportMetaData)
	End Sub
	Private Sub reportList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles reportList.SelectedIndexChanged
		ReportWizardState.DetailGroupingField = Nothing
	End Sub
End Class
