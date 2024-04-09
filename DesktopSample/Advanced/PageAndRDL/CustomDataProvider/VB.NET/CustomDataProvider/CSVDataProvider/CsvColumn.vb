' <summary>
' データソースのフィールドの情報を表します。
' </summary>
Structure CsvColumn
	Private ReadOnly _fieldName As String
	Private ReadOnly _dataType As Type

   ' <summary>
	' <see cref="CsvColumn"/>クラスの新しいインスタンスを作成します。
	' </summary>
	' <param name="fieldName">このインスタンスに対するフィールドの<see cref="CsvColumn"/>名前。</param>
	'<param name="dataType">このインスタンスに対するフィールドの<see cref="Type"/>。</param>
	Public Sub New(ByVal fieldName As String, ByVal dataType As Type)
		If (fieldName Is Nothing) Then
			Throw New ArgumentNullException("fieldName")
		End If
		If (dataType Is Nothing) Then
			Throw New ArgumentNullException("dataType")
		End If
		_fieldName = fieldName
		_dataType = dataType
	End Sub

   ' <summary>
	' フィールドの名前は、このインスタンスで表される <see cref="CsvColumn"/>.
	' </summary>
	Public ReadOnly Property FieldName() As String
		Get
			Return _fieldName
		End Get
	End Property

	' <summary>
	' このインスタンスに対するフィールドの<see cref="Type"/>。
	' </summary>
	Public ReadOnly Property DataType() As Type
		Get
			Return _dataType
		End Get
	End Property


	' <summary>
	'文字列に変換された<see cref="CsvColumn"/>のインスタンスを返します。
	' </summary>
	'  <returns><see cref="CsvColumn"/>のインスタンスを表す文字列。</returns>
	Public Overrides Function ToString() As String
		Return String.Concat(New String() {FieldName, "(", DataType.ToString(), ")"})
	End Function

	' <summary>
	' <see cref="Csv Column"/>の2つのインスタンスが等しいかどうかを確認します。
	' </summary>
	'  <param name="obj">現在の<see cref="CsvColumn"/>と比較する<see cref="CsvColumn"/>。</ PARAM>
	' <returns>指定した<see cref="CsvColumn"/>は現在の<see cref="CsvColumn"/>と等しい場合は、True。そうでない場合はFalseです。</returns>
	Public Overrides Function Equals(ByVal obj As Object) As Boolean
		Dim flag As Boolean

		If (TypeOf (obj) Is CsvColumn) Then
			flag = Equals(CType(obj, CsvColumn))
		Else
			flag = False

		End If
		Return flag
	End Function

	Private Overloads Function Equals(ByVal column As CsvColumn) As Boolean
		Return (column.FieldName = FieldName)
	End Function

	' <summary>
	' ハッシュテーブルのようなハッシュアルゴリズムやデータ構造で使用される<see cref="CsvColumn"/>のハッシュ関数です。
	' </summary>
	' <returns>現在<seeのcref="CsvColumn"/>のインスタンスのハッシュコード。</returns>
	Public Overrides Function GetHashCode() As Integer
		Return (FieldName.GetHashCode() + DataType.GetHashCode())
	End Function

End Structure
