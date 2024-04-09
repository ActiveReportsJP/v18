Imports System.IO
Imports System.Resources

Public Class rptInheritBase
	Inherits GrapeCity.ActiveReports.SectionReport

	'csvファイルのパス。
	Private _csvPath As String
	'csvファイルを読み込むストリーム。
	Private _invoiceFileStream As StreamReader
	'データを格納する文字列の配列。
	Private _fieldNameArray As String()

	Private _resource As New ResourceManager(GetType(rptInheritBase))

	Public Sub New()
		' ActiveReportsデザイナにこの呼び出しは必要です。
	End Sub

	'ActiveReportがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
		End If
		MyBase.Dispose(disposing)
	End Sub

	Protected WriteOnly Property CsvPath() As String
		Set(ByVal Value As String)
			'CsvPathプロパティ
			_csvPath = Value
		End Set
	End Property

	Protected Sub BaseReport_DataInitialize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DataInitialize

		'csvファイルをストリームにロードします。
		Dim invoiceFileStream As New StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))))


		'ストリームから1行読み込み、文字列配列を作成します。
		Dim _currentLine As String = invoiceFileStream.ReadLine()
		_fieldNameArray = _currentLine.Split(New Char() {","c})

		'配列の数だけFieldオブジェクトに格納しています。
		Dim i As Integer
		For i = 0 To _fieldNameArray.Length - 1
			Fields.Add(_fieldNameArray(i).ToString())
		Next i
	End Sub

	Protected Sub BaseReport_FetchData(ByVal sender As Object, ByVal eArgs As GrapeCity.ActiveReports.SectionReport.FetchEventArgs) Handles MyBase.FetchData
		Try
			If _invoiceFileStream.Peek() >= 0 Then
				'ストリームから1行読み込み、文字列配列を作成します。
				Dim _currentLine As String = _invoiceFileStream.ReadLine()
				Dim _currentArray As String() = _currentLine.Split(New Char() {","c})

				'配列の数だけFieldオブジェクトのValueプロパティに格納しています。
				Dim i As Integer
				For i = 0 To _currentArray.Length - 1
					Fields(_fieldNameArray(i).ToString()).Value = _currentArray(i).ToString()
				Next i

				'EOFをfalseに設定し、データの読み込みを継続します。
				eArgs.EOF = False
			Else
				_invoiceFileStream.Close()
				eArgs.EOF = True
			End If
		Catch
			'ストリームの読み込みが最終行を超えたとき、ストリームを閉じます。
			_invoiceFileStream.Close()

			'EOFをtrueに設定し、データの読み込みを終了します。
			eArgs.EOF = True
		End Try
	End Sub

	Protected Sub rptInheritBase_ReportStart(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ReportStart
		'csvファイルをストリームにロードします。
		_invoiceFileStream = New StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))))
		_invoiceFileStream.ReadLine()
	End Sub
End Class
