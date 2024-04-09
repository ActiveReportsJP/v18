Imports GrapeCity.ActiveReports.Export.Pdf.Section
Imports GrapeCity.ActiveReports.Export.Pdf.Section.Signing
Imports System.IO
Imports System.Resources
Imports System.Text
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports

Public Class PDFDigitalSignature
	Private _pageDocument As PageDocument

	Public Sub New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		InitializeComponent()
	End Sub

	Private Sub PDFDigitalSignature_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		' 「署名の形式」コンボのデフォルト状態を設定
		cmbVisibilityType.SelectedIndex = 3
		Dim PageReport = New PageReport()
		_pageDocument = PageReport.Document
		PageReport.Load(New FileInfo("..//..//..//..//..//Report//Catalog.rdlx"))
		arvMain.LoadDocument(_pageDocument)
	End Sub

	Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExport.Click

		Dim pdfRE = New GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension()
		Dim settings = New GrapeCity.ActiveReports.Export.Pdf.Page.Settings()

		Dim sfd As New SaveFileDialog()
		Dim tmpCursor As Cursor = Nothing
		Dim tempPath As String = String.Empty

		sfd.Title = "電子署名付PDFファイルの保存"
		' タイトル
		' 初期表示するファイル名
		sfd.FileName = "DigitalSignature.pdf"
		sfd.Filter = "PDF|*.pdf"           ' フィルタ
		If sfd.ShowDialog() <> DialogResult.OK Then
			Exit Sub
		End If

		Try
			' カーソルを変更します
			Cursor = Cursors.WaitCursor
			Application.DoEvents()

			' 署名のタイプを設定します。
			settings.SignatureVisibilityType = CType(cmbVisibilityType.SelectedIndex, VisibilityType)

			' 署名を表示する領域を設定します。
			settings.SignatureStampBounds = New RectangleF(0.05F, 0.05F, 5.0F, 0.93F)

			' 署名テキストの文字位置を設定します
			settings.SignatureStampTextAlignment = Alignment.Left
			settings.SignatureStampFontName = "MS Pゴシック"
			settings.SignatureStampFontSize = 9
			settings.SignatureContact = "support@example.com"

			' 署名を表示する領域内でテキストが配置される長方形を設定します。
			' ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
			settings.SignatureStampTextRectangle = New RectangleF(1.2F, 0.0F, 3.8F, 0.93F)

			' 署名イメージの表示位置を設定します。
			settings.SignatureStampImageAlignment = Alignment.Center

			' 署名を表示する領域内でイメージが配置される長方形を設定します。
			' ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
			settings.SignatureStampImageRectangle = New RectangleF(0F, 0F, 1.0F, 0.93F)
			settings.SignatureStampImageFileName = Path.GetFullPath("..//..//..//Image//northwind.bmp")

			' デジタル署名の証明書とパスワードを設定します。
			' ※X509Certificate2クラスについては Microsoft社のサイト等をご覧ください。
			' 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
			' 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
			settings.SignatureCertificateFileName = Path.GetFullPath("..//..//..//certificate.pfx")
			settings.SignatureCertificatePassword = "test"
			' タイムスタンプを設定します。
			If chkTimeStamp.Checked Then
				settings.SignatureTimeStamp = New TimeStamp("https://tsa.wotrus.com", "", "")
			End If

			' 署名情報を設定します。
			settings.SignatureSignDate = DateTime.Now.ToString()
			' 署名時間
			settings.SignatureDistinguishedNameVisible = False
			settings.SignatureLocation = "仙台"
			' 場所
			Dim outputDirectory = New DirectoryInfo(Path.GetDirectoryName(sfd.FileName))
			Dim outputProvider = New Rendering.IO.FileStreamProvider(outputDirectory, sfd.FileName)
			outputProvider.OverwriteOutputFile = True

			' ファイルをエクスポートします。
			_pageDocument.Render(pdfRE, outputProvider, settings)

			'出力したファイルを起動します。
			Dim ProcessProperties As New ProcessStartInfo
			ProcessProperties.CreateNoWindow = True
			ProcessProperties.UseShellExecute = True
			ProcessProperties.Verb = "open"
			ProcessProperties.FileName = sfd.FileName
			Process.Start(ProcessProperties)

			' 通知メッセージを表示します。
			MessageBox.Show(My.Resources.Resource.FinishExportMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As PdfSigningException
			File.Delete(tempPath)
			MessageBox.Show(My.Resources.Resource.LimitMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		Finally
			' カーソルを元に戻します
			Cursor = tmpCursor
			Application.DoEvents()

			' 終了処理
			sfd.Dispose()
		End Try
	End Sub

End Class
