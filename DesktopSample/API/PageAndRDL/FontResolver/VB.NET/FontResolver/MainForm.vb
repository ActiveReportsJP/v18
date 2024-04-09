Imports GrapeCity.ActiveReports.Rendering.IO
Imports System.IO
Imports System.Text
Imports GrapeCity.ActiveReports.Export.Pdf.Page
Imports GrapeCity.ActiveReports

Public Class MainForm

    Private _pageReport As New PageReport()

    'Constructor.
    Sub New()
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
        InitializeComponent()
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        _pageReport.Load(New FileInfo("../../../../../../Reports/Barcodes.rdlx"))
        _pageReport.FontResolver = BarcodeFontResolver.Instance
        reportViewer.LoadDocument(_pageReport.Document)
        btnPdfExport.Enabled = True
    End Sub

    'Export the report displayed in Viewer to PDF.
    Private Sub btnPdfExport_Click(sender As Object, e As EventArgs) Handles btnPdfExport.Click
        If _pageReport IsNot Nothing Then
            Dim settings As New Settings()
            settings.HideToolbar = True
            settings.HideMenubar = True
            settings.HideWindowUI = True
            saveFileDialog.Filter = My.Resources.Resources.PDFFilter

            Dim renderingExtension As New PdfRenderingExtension()
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                If File.Exists(saveFileDialog.FileName) Then
                    File.Delete(saveFileDialog.FileName)
                End If

                Dim exportFile As New FileStreamProvider(New DirectoryInfo(Path.GetDirectoryName(saveFileDialog.FileName)), Path.GetFileNameWithoutExtension(saveFileDialog.FileName))
                _pageReport.Document.Render(renderingExtension, exportFile, settings)
            End If
        End If
    End Sub

End Class
