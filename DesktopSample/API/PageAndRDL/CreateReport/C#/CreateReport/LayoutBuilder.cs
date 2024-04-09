using System;
using System.Text;
using System.IO;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.Expressions;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.CreateReport
{
	internal sealed class LayoutBuilder
	{
		//このセクションはページレポートのレイアウトを作成します。
		//レポート本文にテーブルを追加します。
		//TableRows、TableColumnsとTableCellsを追加します。
		//TableCellsにTextBoxのReportItemsを追加します。
		public static PageReport BuildReportLayout()
		{
			PageReport report = new PageReport();
			report.Report.Body.Height = "5cm";
			report.Report.Width = "20cm";
			//テーブルレポートアイテムを作成します。
			Table table = new Table();
			table.Name = "Table1";
			//テーブル用の行、列、およびTableCellsを作成します。また、TableCellsに配置するテキストボックスを作成します。
			TextBox[] tableTextBoxes = new TextBox[6];
			TableCell[] tableCells = new TableCell[6];
			TableRow[] tableRows = new TableRow[2];
			TableColumn[] tableColumns = new TableColumn[3];
			String[] textBoxValues = new String[] {Properties.Resources.TitleValue, Properties.Resources.YearReleasedValue, Properties.Resources.MPAAValue, Properties.Resources.TitleField, Properties.Resources.YearReleasedField, Properties.Resources.MPAAField };

			String[] columnsWidth = new String[] { "9cm", "4.6cm", "5.3cm" };
			String[] rowsHeight = new String[] { "1.5cm", "1.5cm" };


			//TableCells内に配置するテキストボックスのプロパティを設定します。
			for (int i = 0; i < tableTextBoxes.Length; i++)
			{
				tableTextBoxes.SetValue(new TextBox(), i);
				tableTextBoxes[i].Name = "textBox" + (i + 1);
				tableTextBoxes[i].Value = ExpressionInfo.FromString(textBoxValues[i]);
				tableTextBoxes[i].Style.PaddingBottom = tableTextBoxes[i].Style.PaddingLeft = tableTextBoxes[i].Style.PaddingRight = tableTextBoxes[i].Style.PaddingTop = ExpressionInfo.FromString("2pt");
				tableTextBoxes[i].Style.TextAlign = ExpressionInfo.FromString("Left");
				tableCells.SetValue(new TableCell(), i);
				tableCells[i].ReportItems.Add(tableTextBoxes[i]);//TableCellsにテキストボックスを追加します。
				if (i < rowsHeight.Length)
				{
					tableRows.SetValue(new TableRow(), i);
					tableRows[i].Height = "1.25cm";
					table.Height += "1.25cm";
				}
				if (i < columnsWidth.Length)
				{
					tableColumns.SetValue(new TableColumn(), i);
					tableColumns[i].Width = columnsWidth[i];
					table.Width += columnsWidth[i];
					table.TableColumns.Add(tableColumns[i]);
					tableCells[i].ReportItems[0].Style.BackgroundColor = ExpressionInfo.FromString("LightBlue");
					tableRows[0].TableCells.Add(tableCells[i]);
				}
				else
				{
					tableCells[i].ReportItems[0].Style.BackgroundColor = ExpressionInfo.FromString("=Choose((RowNumber(\"Table1\") +1) mod 2, \"PaleGreen\",)");
					tableRows[1].TableCells.Add(tableCells[i]);
				}
			}
			table.Header.TableRows.Add(tableRows[0]);
			table.Details.TableRows.Add(tableRows[1]);
			table.Top = "1cm";
			table.Left = "0.635cm";
			report.Report.Body.ReportItems.Add(table);
			return report;
		}


		//レポートに使用したデータソースを追加します。
		//レポートのためデータセットを追加し、フィールドおよびその式を追加します。 
		public static PageReport AddDataSetDataSource(PageReport report)
		{
			// レポート用のデータソースを作成します。
			DataSource dataSource = new DataSource();
			dataSource.Name = "Reels Database";
			dataSource.ConnectionProperties.DataProvider = "SQLITE";
			dataSource.ConnectionProperties.ConnectString = ExpressionInfo.FromString(Properties.Resources.ConnectionString);
			//指定されたクエリのデータセットを作成し、データセットにデータベースフィールドを読み込みます。
			Query query = new Query();
			query.DataSourceName = "Reels Database";
			query.CommandType = QueryCommandType.Text;
			query.CommandText = ExpressionInfo.FromString(Constants.cmdText);
			DataSet dataSet = new DataSet();
			dataSet.Name = "Sample DataSet";
			dataSet.Query = query;

			String[] fieldsList = new String[] { "MoviedID", "Title", "YearReleased", "MPAA" };

			foreach (string fieldName in fieldsList)
			{
				Field field = new Field(fieldName, fieldName, null);
				dataSet.Fields.Add(field);
			}
			//指定されたデータセットとデータソースのレポート定義を作成します。
			report.Report.DataSources.Add(dataSource);
			report.Report.DataSets.Add(dataSet);
			return report;
		}

		//このセクションはストリームで作成したページレポートのオブジェクトを読み込みます。
		public static MemoryStream LoadReportToStream(PageReport report)
		{
			string rpt = report.ToRdlString();
			byte[] data = Encoding.UTF8.GetBytes(rpt);
			MemoryStream stream = new MemoryStream(data);
			return stream;
		}
	}
}
