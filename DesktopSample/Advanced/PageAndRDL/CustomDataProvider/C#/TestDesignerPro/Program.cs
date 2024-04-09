using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ActiveReports.Samples.TestDesignerPro
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメインエントリポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
#if NET6_0_OR_GREATER
			Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
#endif
			Application.Run(new DesignerForm());
		}
	}
}
