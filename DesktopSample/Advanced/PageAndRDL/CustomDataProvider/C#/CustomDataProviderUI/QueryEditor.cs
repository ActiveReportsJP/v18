using System;
using System.Drawing.Design;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms.Design;

namespace ActiveReports.Samples.CustomDataProviderUI
{
	public sealed class QueryEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}
		
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			CSVFileSelector fileSelector = new CSVFileSelector();
			edSvc.DropDownControl(fileSelector);
			string csvFileName =  fileSelector.CSVFileName;
			if(string.IsNullOrEmpty(csvFileName)) return string.Empty;
			if(!File.Exists(csvFileName)) return string.Empty;
			return GetCSVQuery(csvFileName);
		}
		
		/// <summary>
		/// 指定したファイルの内容を読み込み、CSVデータプロバイダのクエリ文字列を作成します。
		/// </summary>
		/// <param name="csvFileName">CSVファイルの完全なパス。</param>
		/// <returns>CSVデータプロバイダのクエリ文字列。</returns>
		/// <remarks>
		/// この方法は、ファイルの行を1つずつ読み込み、クエリ文字列に追加します。
		/// CSVのクエリ文字列の先頭行には、列名と列のデータ型があるため、
		///特有の処理を実行します。
		/// </remarks>
		private static string GetCSVQuery(string csvFileName)
		{
			StreamReader sr = null;
			try
			{
				sr = new StreamReader(csvFileName);
				string ret = string.Empty;
				string currentLine;
				int line = 0;
				while ((currentLine = sr.ReadLine()) != null)
				{
					if (line == 0)
						ret += ProcessColumnsDefinition(currentLine) + "\r\n";
					else
						ret += currentLine + "\r\n";
					line++;
				}
				return ret;
			}
			catch (IOException)
			{
				return string.Empty;
			}
			finally
			{
				if (sr != null)
					sr.Close();
			}
		}

		/// <summary>
		///指定した文字列からCSVデータの列の定義を読み込み、必要に応じて調整します。
		/// </summary>
		/// <param name="line">CSVデータの列の定義を含む文字列。</param>
		/// <returns>データ型の定義を含むCSVデータ列の定義。</returns>
		private static string ProcessColumnsDefinition(string line)
		{
			const string ColumnWithDataTypeRegex = @"[""]?\w+[\""]?\(.+\)";
			string[] columns = line.Split(new string[] {","}, StringSplitOptions.None);
			string ret = null;
			foreach(string column in columns)
			{
				if(!string.IsNullOrEmpty(ret))
					ret+= ",";
				if(!Regex.Match(column, ColumnWithDataTypeRegex).Success)
				{
					ret += column + "(string)";
				}
				else
				{
					ret += column;
				}
			}
			return ret;
		}
	}
}
