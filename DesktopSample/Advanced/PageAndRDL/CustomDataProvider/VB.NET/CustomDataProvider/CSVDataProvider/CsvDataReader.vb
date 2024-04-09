Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Globalization
Imports System.Data.Common


''' <summary>
''' .NETフレームワークのCSVデータプロバイダの<see cref="Extensibility.Data.IDataReader"/>を実装する方法を提供します。
''' </summary>
Friend Class CsvDataReader
	Inherits DbDataReader
	Private _typeLookup As New Hashtable(New myCultureComparer(New CultureInfo(My.Resources.CultureString)))

	Private _columnLookup As New Hashtable()
	Private _columns As Object()
	Private _textReader As TextReader
	Private _currentRow As Object()

	'早急な処理を行うために、正規表現をプリコンパイルします。

	'マルチスレッドを避けるために、プロパティは読み取り専用に設定されています。

	Private Shared ReadOnly _rxDataRow As New Regex(",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))", RegexOptions.Compiled)
	'データ行を解析するために使用されます。

	Private Shared ReadOnly _rxHeaderRow As New Regex("(?<fieldName>(\w*\s*)*)\((?<fieldType>\w*)\)", RegexOptions.Compiled)
	'ヘッダ行を解析するために使用されます。
	''' <summary>
	''' </summary>
	''' <param name="textReader">データ読み込みに使用される<see cref="TextReader"/>。</param>
	Public Sub New(textReader As TextReader)
		_textReader = textReader
		ParseCommandText()
	End Sub

	''' <summary>
	'''渡されたコマンドのテキストを解析します。
	''' </summary>
	Private Sub ParseCommandText()
		If _textReader.Peek() = -1 Then
			Return
		End If
		'コマンドのテキストは空であるか既に末尾にあります。
		FillTypeLookup()
		Try
			If Not ParseHeader(_textReader.ReadLine()) Then
				Throw New InvalidOperationException(My.Resources.ParseTextException)

			End If
		Catch generatedExceptionName As Exception
		End Try
	End Sub

	'ハッシュテーブルは、ヘッダのテキストで使用される文字列値の型を返すために使用されます。

	Private Sub FillTypeLookup()
		_typeLookup.Add("string", GetType([String]))
		_typeLookup.Add("byte", GetType([Byte]))
		_typeLookup.Add("boolean", GetType([Boolean]))
		_typeLookup.Add("datetime", GetType(DateTime))
		_typeLookup.Add("decimal", GetType([Decimal]))
		_typeLookup.Add("double", GetType([Double]))
		_typeLookup.Add("int16", GetType(Int16))
		_typeLookup.Add("int32", GetType(Int32))
		_typeLookup.Add("int", GetType(Int32))
		_typeLookup.Add("integer", GetType(Int32))
		_typeLookup.Add("int64", GetType(Int64))
		_typeLookup.Add("sbyte", GetType([SByte]))
		_typeLookup.Add("single", GetType([Single]))
		_typeLookup.Add("time", GetType(DateTime))
		_typeLookup.Add("date", GetType(DateTime))
		_typeLookup.Add("uint16", GetType(UInt16))
		_typeLookup.Add("uint32", GetType(UInt32))
		_typeLookup.Add("uint64", GetType(UInt64))
	End Sub

	''' <summary>
	''' ヘッダテキストの文字列から渡された文字列の値に基づいてデータ型を返します。一致しない場合は、文字列型が返されます。
	''' </summary>
	''' <param name="fieldType">ヘッダコマンドのテキスト文字列からの文字列値。</param>
	Private Function GetFieldTypeFromString(fieldType As String) As Type
		If _typeLookup.Contains(fieldType) Then
			Return TryCast(_typeLookup(fieldType), Type)
		End If
		Return GetType([String])
	End Function

	''' <summary>
	''' 渡されたコマンドのテキストの先頭行を解析し、フィールド名およびデータ型を作成します。フィールドの情報は、
	''' <see cref="CsvColumn"/>構造体に保存され、列情報の項目はArrayListに格納されています。列名は、容易に検索できるハッシュテーブルに
	''' 追加します。
	''' </summary>
	''' <param name="header">すべてのフィールドを含むヘッダ文字列。</param>
	''' <returns>ヘッダ文字列を解析できる場合は、True。それ以外の場合は、False。</returns>
	Private Function ParseHeader(header As String) As Boolean
		Dim fieldName As String
		Dim index As Integer = 0
		If header.IndexOf("(") = -1 Then
			Return False
		End If

		Dim matches As MatchCollection = _rxHeaderRow.Matches(header)
		_columns = New Object(matches.Count - 1) {}
		For Each match As Match In matches
			fieldName = match.Groups("fieldName").Value
			Dim fieldType As Type = GetFieldTypeFromString(match.Groups("fieldType").Value)
			_columns.SetValue(New CsvColumn(fieldName, fieldType), index)
			_columnLookup.Add(fieldName, index)
			index += 1
		Next
		Return True
	End Function

	''' <summary>
	''' 正規表現を使用し、データの行を解析します。データの現在の行となるオブジェクト配列内に情報を保存します。
	''' 行でフィールド数が正しくない場合は、例外が発生します。
	''' </summary>
	''' <param name="dataRow">カンマ区切りのデータ行を表す文字列値。</param>
	''' <returns>データ文字列を解析できる場合は、True。それ以外の場合は、False。</returns>
	Private Function ParseDataRow(dataRow As String) As Boolean
		Dim index As Integer = 0
		Dim tempData As String() = _rxDataRow.Split(dataRow)

		_currentRow = New Object(tempData.Length - 1) {}
		If tempData.Length <> _columns.Length Then
			Dim [error] As String = String.Format(CultureInfo.InvariantCulture, My.Resources.ParseDataRowError, dataRow)

			Throw New InvalidOperationException([error])
		End If
		Dim i As Integer = 0
		While i < tempData.Length
			Dim value As String = tempData(i)

			If value.Length > 1 Then
				If value.IndexOf(""""c, 0) = 0 AndAlso value.IndexOf(""""c, 1) = value.Length - 1 Then
					value = value.Substring(1, value.Length - 2)
				End If
			End If
			_currentRow.SetValue(ConvertValue(GetFieldType(index), value), index)
			index += 1
			i += 1
		End While
		Return True
	End Function

	''' <summary>
	'''フィールド型に基づいてコマンドのテキストからの文字列値を適切なデータ型に変換します。
	''' また、String.EmptyまたはSystem.Data.DBNullを返す必要がないかを確認するためにいくつか文字列値のルールをチックします。
	''' </summary>
	''' <param name="type">現在の列の型は、データが属している.</param>
	''' <param name="originalValue">コマンドテキストからの文字列値。</param>
	''' <returns>データ型に基づいている変換された文字列を返します。</returns>
	Private Function ConvertValue(type As Type, originalValue As String) As Object
		Dim fieldType As Type = type
		Dim invariantCulture As CultureInfo = CultureInfo.InvariantCulture
		Try
			If originalValue = """""" OrElse originalValue = " " Then
				Return String.Empty
			End If
			If originalValue = "" Then
				Return DBNull.Value
			End If
			If originalValue = "DBNull" Then
				Return DBNull.Value
			End If
			If fieldType = GetType([String]) Then
				Return originalValue.Trim()
			End If
			If fieldType = GetType(Int32) Then
				Return Convert.ToInt32(originalValue, invariantCulture)
			End If
			If fieldType = GetType([Boolean]) Then
				Return Convert.ToBoolean(originalValue, invariantCulture)
			End If
			If fieldType = GetType(DateTime) Then
				Return Convert.ToDateTime(originalValue, invariantCulture)
			End If
			If fieldType = GetType([Decimal]) Then
				Return Convert.ToDecimal(originalValue, invariantCulture)
			End If
			If fieldType = GetType([Double]) Then
				Return Convert.ToDouble(originalValue, invariantCulture)
			End If
			If fieldType = GetType(Int16) Then
				Return Convert.ToInt16(originalValue, invariantCulture)
			End If
			If fieldType = GetType(Int64) Then
				Return Convert.ToInt64(originalValue, invariantCulture)
			End If
			If fieldType = GetType([Single]) Then
				Return Convert.ToSingle(originalValue, invariantCulture)
			End If
			If fieldType = GetType([Byte]) Then
				Return Convert.ToByte(originalValue, invariantCulture)
			End If
			If fieldType = GetType([SByte]) Then
				Return Convert.ToSByte(originalValue, invariantCulture)
			End If
			If fieldType = GetType(UInt16) Then
				Return Convert.ToUInt16(originalValue, invariantCulture)
			End If
			If fieldType = GetType(UInt32) Then
				Return Convert.ToUInt32(originalValue, invariantCulture)
			End If
			If fieldType = GetType(UInt64) Then
				Return Convert.ToUInt64(originalValue, invariantCulture)
			End If
		Catch e As Exception
			Throw New InvalidOperationException(String.Format(My.Resources.ConvertValueExceprion, originalValue, type), e)
		End Try
		'一致しない場合は、DBNullを返します。

		Return DBNull.Value
	End Function


	''' <summary>
	''' <see cref="CsvDataReader"/>を次のレコードに移動します。
	''' </summary>
	''' <returns>他の行がある場合はTrue、それ以外の場合はFalse。</returns>
	Public Overrides Function Read() As Boolean
		If _textReader.Peek() > -1 Then
			ParseDataRow(_textReader.ReadLine())
		Else
			Return False
		End If

		Return True
	End Function

	''' <summary>
	'''<see cref="CsvDataReader"/>により使用されたリソースを解放します。
	''' </summary>
	Public Shadows Sub Dispose()
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Shadows Sub Dispose(disposing As Boolean)
		If disposing Then
			If _textReader IsNot Nothing Then
				_textReader.Close()
			End If
		End If

		_typeLookup = Nothing
		_columnLookup = Nothing
		_columns = Nothing
		_currentRow = Nothing
	End Sub

	''' <summary>
	''' Objectがガベージ コレクションにより収集される前に、その Object がリソースを解放し、その他のクリーンアップ操作を実行できるようにします。
	''' </summary>
	Protected Overrides Sub Finalize()
		Try
			Dispose(False)
		Finally
			MyBase.Finalize()
		End Try
	End Sub

	''' <summary>
	''' 現在の行の列数を取得します。
	''' </summary>
	Public Overrides ReadOnly Property FieldCount() As Integer
		Get
			If _columns Is Nothing Then
				_columns = New Object(-1) {}
			End If
			Return _columns.Length
		End Get
	End Property

	Public Overrides ReadOnly Property Depth() As Integer
		Get
			Return 0
		End Get
	End Property

	Public Overrides ReadOnly Property IsClosed() As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property RecordsAffected() As Integer
		Get
			Return 0
		End Get
	End Property

	Default Public Overrides ReadOnly Property Item(name As String) As Object
		Get
			Return Me(GetOrdinal(name).ToString())
		End Get
	End Property

	Default Public Overrides ReadOnly Property Item(i As Integer) As Object
		Get
			Return GetValue(i)
		End Get
	End Property

	Public Overrides ReadOnly Property HasRows As Boolean
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	''' <summary>
	''' 指定したフィールドの値を返します。
	''' </summary>
	''' <param name="i">検索するフィールドのインデックス。 </param>
	''' <returns>フィールドの値を返す<see cref="Object"/>。</returns>
	Public Overrides Function GetFieldType(ordinal As Integer) As Type
		If ordinal > _columns.Length - 1 Then
			Return Nothing
		End If

		Return CType(_columns.GetValue(ordinal), CsvColumn).DataType
	End Function

	''' <summary>
	''' 指定したフィールドの値を返します。
	''' </summary>
	''' <param name="i">検索するフィールドのインデックス。 </param>	   
	'''フィールドの名前を返します。値はない場合は、空の文字列（""）を返します。
	Public Overrides Function GetName(i As Integer) As String
		If i > _columns.Length - 1 Then
			Return String.Empty
		End If

		Return CType(_columns.GetValue(i), CsvColumn).FieldName
	End Function

	Public Overrides Function GetOrdinal(name As String) As Integer
		Dim value As Object = _columnLookup(name)
		If value Is Nothing Then
			Throw New IndexOutOfRangeException("name")
		End If
		Return CType(value, Integer)
	End Function

	''' <summary>
	''' 指定したフィールドの値を返します。
	''' </summary>
	''' <param name="i">検索するフィールドのインデックス。 </param>
	''' <returns>フィールドの値を返す<see cref="Object"/>。</returns>
	Public Overrides Function GetValue(i As Integer) As Object
		If i > _columns.Length - 1 Then
			Return Nothing
		End If

		Return _currentRow.GetValue(i)
	End Function

	Public Overrides Sub Close()
	End Sub

	Public Overrides Function GetSchemaTable() As System.Data.DataTable
		Throw New NotImplementedException()
	End Function

	Public Overrides Function NextResult() As Boolean
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetDataTypeName(i As Integer) As String
		Return GetFieldType(i).ToString()
	End Function

	Public Overrides Function GetValues(values As Object()) As Integer
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetBoolean(i As Integer) As Boolean
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetByte(i As Integer) As Byte
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetBytes(i As Integer, fieldOffset As Long, buffer As Byte(), bufferoffset As Integer, length As Integer) As Long
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetChar(i As Integer) As Char
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetChars(i As Integer, fieldoffset As Long, buffer As Char(), bufferoffset As Integer, length As Integer) As Long
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetGuid(i As Integer) As Guid
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetInt16(i As Integer) As Short
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetInt32(i As Integer) As Integer
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetInt64(i As Integer) As Long
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetFloat(i As Integer) As Single
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetDouble(i As Integer) As Double
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetString(i As Integer) As String
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetDecimal(i As Integer) As Decimal
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetDateTime(i As Integer) As DateTime
		Throw New NotImplementedException()
	End Function

	Public Overrides Function IsDBNull(i As Integer) As Boolean
		Throw New NotImplementedException()
	End Function

	Public Overrides Function GetEnumerator() As IEnumerator
		Throw New NotImplementedException()
	End Function

#Region "EqualityComparer"

	Private Class myCultureComparer
		Implements IEqualityComparer
		Public myComparer As CaseInsensitiveComparer

		Public Sub New()
			myComparer = CaseInsensitiveComparer.DefaultInvariant
		End Sub

		Public Sub New(myCulture As CultureInfo)
			myComparer = New CaseInsensitiveComparer(myCulture)
		End Sub

		Public Shadows Function Equals(x As Object, y As Object) As Boolean Implements IEqualityComparer.Equals
			If myComparer.Compare(x, y) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		Public Shadows Function GetHashCode(obj As Object) As Integer Implements IEqualityComparer.GetHashCode
			Return obj.ToString().ToLower().GetHashCode()
		End Function
	End Class

#End Region
End Class
