using System;
using System.IO;
using System.Resources;

namespace ActiveReports.Samples.Inheritance
{
	
	/// csvファイルの読み込み機能を実装した基本クラスです。
	public partial class rptInheritBase : GrapeCity.ActiveReports.SectionReport
	{
		//csvファイルのパス。
		private String _csvPath;

		//csvファイルを読み込むストリーム。
		private StreamReader _invoiceFileStream;

		//データを格納する文字列の配列。
		private string [] _fieldNameArray;

		ResourceManager _resource;
		
		public rptInheritBase()
		{
			_resource = new ResourceManager(typeof(rptInheritBase));
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//

			//イベントハンドラを追加します。
			DataInitialize += new EventHandler(BaseReport_DataInitialize);
			FetchData += new FetchEventHandler(BaseReport_FetchData);
			ReportStart += new System.EventHandler(this.rptInheritBase_ReportStart);
		}

		//CsvPathプロパティ
		protected string CsvPath
		{
			set
			{
				_csvPath = value;
			}
		}

		protected void BaseReport_DataInitialize(object sender, System.EventArgs eArgs)
		{

			//csvファイルをストリームにロードします。
			StreamReader invoiceFileStream = new StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))));
			//ストリームから1行読み込み、文字列配列を作成します。
			string currentLine = invoiceFileStream.ReadLine();
			_fieldNameArray = currentLine.Split(new char[] { ',' });

			//配列の数だけFieldオブジェクトに格納しています。
			for (int i = 0; i < _fieldNameArray.Length; i++)
				Fields.Add(_fieldNameArray[i]);
		   
		}

		protected void BaseReport_FetchData(object sender, FetchEventArgs eArgs)
		{
			try
			{
				if (_invoiceFileStream.Peek() >= 0)
				{
					//ストリームから1行読み込み、文字列配列を作成します。
					string _currentLine = _invoiceFileStream.ReadLine();
					string[] _currentArray = _currentLine.Split(new char[] { ',' });

					//配列の数だけFieldオブジェクトのValueプロパティに格納しています。
					for (int i = 0; i < _currentArray.Length; i++)
						Fields[_fieldNameArray[i]].Value = _currentArray[i];

					//EOFをfalseに設定し、データの読み込みを継続します。
					eArgs.EOF = false;
				}
				else
				{
					_invoiceFileStream.Close();
					eArgs.EOF = true;
				}
			}
			catch
			{
				//ストリームの読み込みが最終行を超えたとき、ストリームを閉じます。
				_invoiceFileStream.Close();

				//EOFをtrueに設定し、データの読み込みを終了します。
				eArgs.EOF = true;
			}
		}

		protected void rptInheritBase_ReportStart(object sender, EventArgs e)
		{
			//csvファイルをストリームにロードします。
			_invoiceFileStream = new StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))));
			_invoiceFileStream.ReadLine();
		}
	}
}
