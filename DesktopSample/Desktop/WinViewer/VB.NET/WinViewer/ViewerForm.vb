Imports GrapeCity.ActiveReports.Viewer.Win.Internal.Export
Imports System.Reflection
Imports System.IO
Imports System.ComponentModel
Imports System.Text
Imports GrapeCity.Viewer.Common.ViewModel
Imports ActiveReports.Samples.WinViewer.ActiveReports.Viewer.Helper
Imports ActiveReports.Samples.WinViewer.ActiveReports.Win.Export

Partial Friend Class ViewerForm
	Inherits Form
	Private _exportForm As ExportForm
	Private _reportType As System.Nullable(Of ExportForm.ReportType)
	Private _openFileName As String

	Public Sub New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		InitializeComponent()
		Icon = My.Resources.App
		SetReportName(Nothing)
	End Sub

	''' <summary>
	''' Loads the document.
	''' </summary>
	''' <param name="file">The file.</param>
	Public Sub LoadDocument(file As FileInfo)
		Try
			_reportType = ViewerHelper.DetermineReportType(file)
			Dim isRdf As Boolean = ViewerHelper.IsRdf((file))
			Dim reportServerUri = If(Not isRdf, ViewerHelper.GetReportServerUri(file), String.Empty)

			If Not String.IsNullOrEmpty(reportServerUri) Then

				Throw New NotSupportedException("Server report is not supported")
			Else
				viewer.LoadDocument(file.FullName)
			End If
			_openFileName = file.FullName

			ExportToolStripMenuItem.Enabled = True
			SetReportName(openFileDialog.FileName)
		Catch ex As Exception
			viewer.HandleError(ex)
		End Try
	End Sub

	Private Sub OpenMenuItemHandler(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
		openFileDialog.FileName = String.Empty
		openFileDialog.Filter = My.Resources.Filter

		If openFileDialog.ShowDialog(Me) = DialogResult.OK Then
			LoadDocument(New FileInfo(openFileDialog.FileName))
		End If
	End Sub

	Private Sub CloseMenuItemHandler(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
		Close()
	End Sub

	Private Sub SetReportName(reportName As String)
		Text = If(String.IsNullOrEmpty(reportName), String.Format(My.Resources.DefaultTitleFormat, My.Resources.ViewerFormTitle), String.Format(My.Resources.TitleFormat, My.Resources.ViewerFormTitle, Path.GetFileName(reportName)))
	End Sub

	Private Sub AboutMenuItemHandler(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
		Const showAboutBoxMethodName As String = "ShowAboutBox"
		Dim attributes = viewer.[GetType]().GetCustomAttributes(GetType(LicenseProviderAttribute), False)
		If attributes.Length <> 1 Then
			Return
		End If
		Dim attr As LicenseProviderAttribute = CType(attributes(0), LicenseProviderAttribute)
		Dim provider As Object = (CType(Activator.CreateInstance(attr.LicenseProvider), LicenseProvider))

		Dim methodInfo As Object = provider.[GetType]().GetMethod(showAboutBoxMethodName, BindingFlags.NonPublic Or BindingFlags.Instance)

		If methodInfo IsNot Nothing Then
			methodInfo.Invoke(provider, New Object() {viewer.[GetType]()})
		End If
	End Sub

	Private Sub ExportMenuItemHandler(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click
		If _exportForm Is Nothing Then
			_exportForm = New ExportForm()
		End If

		Dim reportType = If(_reportType, DetermineOpenedReportType())

		_exportForm.Show(Me, New ExportViewer(viewer), reportType)
	End Sub

	Private Function DetermineOpenedReportType() As ExportForm.ReportType
		If viewer.Document IsNot Nothing Then
			Return ExportForm.ReportType.Section
		End If
		Return If(viewer.OpenedReport = OpenedReport.Fpl,
			ExportForm.ReportType.PageFpl,
			ExportForm.ReportType.PageCpl)
	End Function
End Class
