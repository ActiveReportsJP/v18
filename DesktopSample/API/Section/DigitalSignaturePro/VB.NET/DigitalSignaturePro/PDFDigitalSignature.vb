Imports System.Resources
Imports GrapeCity.ActiveReports.Export.Pdf.Section.Signing
Imports System.Security.Cryptography.X509Certificates
Imports GrapeCity.ActiveReports.Export.Pdf.Section
Imports System.IO
Imports System.Text
Imports GrapeCity.ActiveReports

Public Class PDFDigitalSignature
	Public Sub New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		InitializeComponent()
	End Sub

	Private Sub PDFDigitalSignature_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'「署名の形式」コンボのデフォルト状態を設定します。
		cmbVisibilityType.SelectedIndex = 3

		arvMain.LoadDocument("..//..//..//Report//Invoice.rpx")
	End Sub

	Private Sub pdfExportButton_Click(sender As Object, e As EventArgs) Handles pdfExportButton.Click
		Dim oPDFExport As New Export.Pdf.Section.PdfExport()
		Dim sfd As New SaveFileDialog()
		Dim tmpCursor As Cursor = Cursor
		Dim tempPath As String = String.Empty
		' ファイル保存先ダイアログを表示します。

		sfd.Title = "電子署名付PDFファイルの保存"       'タイトル
		sfd.FileName = "DigitalSignature.pdf"       ' 初期表示するファイル名
		sfd.Filter = "PDF|*.pdf"        ' フィルタ
		If sfd.ShowDialog() <> DialogResult.OK Then
			Return
		End If

		Try
			' カーソルを変更します
			Cursor = Cursors.WaitCursor
			Application.DoEvents()

			' 署名のタイプを設定します。
			oPDFExport.Signature.VisibilityType = CType(cmbVisibilityType.SelectedIndex, VisibilityType)

			' 署名を表示する領域を設定します。
			oPDFExport.Signature.Stamp.Bounds = New RectangleF(0.05F, 0.05F, 4.0F, 0.93F)

			' 署名テキストの文字位置を設定します
			oPDFExport.Signature.Stamp.TextAlignment = Alignment.Left

			oPDFExport.Signature.Stamp.Font = New Font("MS Pゴシック", 9, FontStyle.Regular, GraphicsUnit.Point, 128)


			' 署名を表示する領域内でテキストが配置される長方形を設定します。
			' ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
			oPDFExport.Signature.Stamp.TextRectangle = New RectangleF(1.2F, 0F, 2.8F, 0.93F)

			' 署名イメージを設定します。
			Using fs As New FileStream(Path.GetFullPath("..//..//..//Image//northwind.bmp"), FileMode.Open, FileAccess.Read)
				oPDFExport.Signature.Stamp.Image = New Bitmap(Image.FromStream(fs))
			End Using

			' 署名イメージの表示位置を設定します。
			oPDFExport.Signature.Stamp.ImageAlignment = Alignment.Center

			' 署名を表示する領域内でイメージが配置される長方形を設定します。
			' ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
			oPDFExport.Signature.Stamp.ImageRectangle = New RectangleF(0F, 0F, 1.0F, 0.93F)

			' デジタル署名の証明書とパスワードを設定します。
			' ※X509Certificate2クラスについては Microsoft社のサイト等をご覧ください。
			' 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
			' 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
			oPDFExport.Signature.Certificate = New X509Certificate2(Path.GetFullPath("..//..//..//certificate.pfx"), "test")

			' タイムスタンプを設定します。
			If chkTimeStamp.Checked Then
				oPDFExport.Signature.TimeStamp = New TimeStamp("https://freetsa.org/tsr", "", "")
			End If

			' 署名情報を設定します。
			oPDFExport.Signature.SignDate = New SignatureField(Of DateTime)(DateTime.Now, True)
			' 署名時間
			oPDFExport.Signature.DistinguishedName.Visible = False

			oPDFExport.Signature.Location = New SignatureField(Of String)("仙台", True)
			oPDFExport.Signature.Contact = "support@example.com"

			' 場所
			tempPath = Path.GetTempFileName()
			' ファイルをエクスポートします。
			oPDFExport.Export(arvMain.Document, tempPath)
			File.Delete(sfd.FileName)
			File.Move(tempPath, sfd.FileName)

			'出力したファイルを起動します。
			Dim ProcessProperties As New ProcessStartInfo
			ProcessProperties.CreateNoWindow = True
			ProcessProperties.UseShellExecute = True
			ProcessProperties.Verb = "open"
			ProcessProperties.FileName = sfd.FileName
			Process.Start(ProcessProperties)

			' 通知メッセージを表示します。
			MessageBox.Show(Resource.FinishExportMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch generatedExceptionName As PdfSigningException
			File.Delete(tempPath)
			MessageBox.Show(Resource.LimitMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
		Finally
			' カーソルを元に戻します
			Cursor = tmpCursor
			Application.DoEvents()

			' 終了処理
			sfd.Dispose()
			oPDFExport.Dispose()
		End Try
	End Sub
End Class
