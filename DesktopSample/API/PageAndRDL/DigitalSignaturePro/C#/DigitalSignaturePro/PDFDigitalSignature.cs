using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GrapeCity.ActiveReports.Export.Pdf.Section.Signing;
using GrapeCity.ActiveReports.Export.Pdf.Section;
using System.Resources;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.DigitalSignaturePro
{
	public partial class PDFDigitalSignature : Form
	{
		private PageDocument _pageDocument;
		public PDFDigitalSignature()
		{
			InitializeComponent();
		}

		private void PDFDigitalSignature_Load(object sender, EventArgs e)
		{
			//「署名の形式」コンボのデフォルト状態を設定します。
			cmbVisibilityType.SelectedIndex = 3;
			var pageReport = new PageReport();
			_pageDocument = pageReport.Document;
			pageReport.Load(new FileInfo(@"..\..\..\..\..\Report\Catalog.rdlx"));
			arvMain.LoadDocument(_pageDocument);
		}

		private void pdfExportButton_Click(object sender, EventArgs e)
		{
			var pdfRE = new GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension();
			var settings = new GrapeCity.ActiveReports.Export.Pdf.Page.Settings();

			SaveFileDialog sfd = new SaveFileDialog();
			Cursor tmpCursor = Cursor;
			// ファイル保存先ダイアログを表示します。
			
			sfd.Title = "電子署名付PDFファイルの保存";//タイトル
			sfd.FileName = "DigitalSignature.pdf";	  // 初期表示するファイル名
			sfd.Filter = "PDF|*.pdf";		  // フィルタ
			if (sfd.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			try
			{
				// カーソルを変更します
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();

				// 署名のタイプを設定します。
				settings.SignatureVisibilityType = (VisibilityType)cmbVisibilityType.SelectedIndex;

				// 署名を表示する領域を設定します。
				settings.SignatureStampBounds = new RectangleF(0.05F, 0.05F, 5.0F, 0.93F);

				// 署名テキストの文字位置を設定します
				settings.SignatureStampTextAlignment = Alignment.Left;
				settings.SignatureStampFontName = "MS Pゴシック";
				settings.SignatureStampFontSize = 9;
                settings.SignatureContact = "support@example.com";

                // 署名を表示する領域内でテキストが配置される長方形を設定します。
                // ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
                settings.SignatureStampTextRectangle = new RectangleF(1.2F, 0.0F, 3.8F, 0.93F);

				// 署名イメージの表示位置を設定します。
				settings.SignatureStampImageAlignment = Alignment.Center;

				// 署名を表示する領域内でイメージが配置される長方形を設定します。
				// ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
				settings.SignatureStampImageRectangle = new RectangleF(0.0f, 0.0f, 1.0f, 0.93F);
                settings.SignatureStampImageFileName = Path.GetFullPath(@"..\..\..\Image\northwind.bmp");

                // デジタル署名の証明書とパスワードを設定します。
                // ※X509Certificate2クラスについては Microsoft社のサイト等をご覧ください。
                // 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
                // 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
                settings.SignatureCertificateFileName = Path.GetFullPath(@"..\..\..\certificate.pfx");
				settings.SignatureCertificatePassword = "test";
				// タイムスタンプを設定します。
				if (chkTimeStamp.Checked)
				{
					settings.SignatureTimeStamp = new TimeStamp("https://tsa.wotrus.com", "", "");
				}

				// 署名情報を設定します。
				settings.SignatureSignDate = DateTime.Now.ToString(); // 署名時間
				settings.SignatureDistinguishedNameVisible  = false; // 識別名表示可否

				settings.SignatureLocation = "仙台";

                // 場所
                var outputDirectory = new DirectoryInfo(Path.GetDirectoryName(sfd.FileName));
				var outputProvider = new GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider(outputDirectory, sfd.FileName);
				outputProvider.OverwriteOutputFile = true;

				// ファイルをエクスポートします。
				_pageDocument.Render(pdfRE, outputProvider, settings);

				//出力したファイルを起動します。
				Process.Start(new ProcessStartInfo()
				{
					CreateNoWindow = true,
					UseShellExecute = true,
					Verb = "open",
					FileName = sfd.FileName
				});

				// 通知メッセージを表示します。
				MessageBox.Show(Resource.FinishExportMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (PdfSigningException)
			{
				File.Delete(sfd.FileName);
				MessageBox.Show(Resource.LimitMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				// カーソルを元に戻します
				Cursor = tmpCursor;
				Application.DoEvents();

				// 終了処理
				sfd.Dispose();
			}
		}
	}
}
