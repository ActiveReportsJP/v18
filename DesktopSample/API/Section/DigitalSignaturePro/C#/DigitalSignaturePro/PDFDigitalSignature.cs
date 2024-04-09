using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using GrapeCity.ActiveReports.Export.Pdf.Section.Signing;
using GrapeCity.ActiveReports.Export.Pdf.Section;
using System.Resources;
using System.Diagnostics;

namespace ActiveReports.Samples.DigitalSignaturePro
{
	public partial class PDFDigitalSignature : Form
	{
		public PDFDigitalSignature()
		{
			InitializeComponent();
		}

		private void PDFDigitalSignature_Load(object sender, EventArgs e)
		{
			//「署名の形式」コンボのデフォルト状態を設定します。
			cmbVisibilityType.SelectedIndex = 3;

			arvMain.LoadDocument("..//..//..//Report//Invoice.rpx");
		}

		private void pdfExportButton_Click(object sender, EventArgs e)
		{
			var oPDFExport = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
			SaveFileDialog sfd = new SaveFileDialog();
			Cursor tmpCursor = Cursor;
			string tempPath = string.Empty;
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
				oPDFExport.Signature.VisibilityType = (VisibilityType)cmbVisibilityType.SelectedIndex;

				// 署名を表示する領域を設定します。
				oPDFExport.Signature.Stamp.Bounds = new RectangleF(0.05F, 0.05F, 4.0F, 0.93F);

				// 署名テキストの文字位置を設定します
				oPDFExport.Signature.Stamp.TextAlignment = Alignment.Left;

				oPDFExport.Signature.Stamp.Font = new Font("MS Pゴシック", 9, FontStyle.Regular, GraphicsUnit.Point, 128);


				// 署名を表示する領域内でテキストが配置される長方形を設定します。
				// ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
				oPDFExport.Signature.Stamp.TextRectangle = new RectangleF(1.2F, 0.0F, 2.8F, 0.93F);

				// 署名イメージを設定します。
				using (var fs = new FileStream(Path.GetFullPath("..//..//..//Image//northwind.bmp"), FileMode.Open, FileAccess.Read))
					oPDFExport.Signature.Stamp.Image = new Bitmap(Image.FromStream(fs));

				// 署名イメージの表示位置を設定します。
				oPDFExport.Signature.Stamp.ImageAlignment = Alignment.Center;

				// 署名を表示する領域内でイメージが配置される長方形を設定します。
				// ※このプロパティは左上点を起点とし、署名長方形に相対する座標で指定します。
				oPDFExport.Signature.Stamp.ImageRectangle = new RectangleF(0.0f, 0.0f, 1.0f, 0.93f);

				// デジタル署名の証明書とパスワードを設定します。
				// ※X509Certificate2クラスについては Microsoft社のサイト等をご覧ください。
				// 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
				// 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
				oPDFExport.Signature.Certificate = new X509Certificate2(Path.GetFullPath("..//..//..//certificate.pfx"), "test");

				// タイムスタンプを設定します。
				if (chkTimeStamp.Checked)
				{
					oPDFExport.Signature.TimeStamp = new TimeStamp("https://freetsa.org/tsr", "", "");
				}

				// 署名情報を設定します。
				oPDFExport.Signature.SignDate = new SignatureField<DateTime>(DateTime.Now, true); // 署名時間
				oPDFExport.Signature.DistinguishedName.Visible = false; // 識別名表示可否

				oPDFExport.Signature.Location = new SignatureField<string>("仙台", true);
                oPDFExport.Signature.Contact = "support@example.com";     

                // 場所
                tempPath = Path.GetTempFileName();

				// ファイルをエクスポートします。
				oPDFExport.Export(arvMain.Document, tempPath);
				File.Delete(sfd.FileName);
				File.Move(tempPath, sfd.FileName);

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
				File.Delete(tempPath);
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
				oPDFExport.Dispose();
			}
		}
	}
}
