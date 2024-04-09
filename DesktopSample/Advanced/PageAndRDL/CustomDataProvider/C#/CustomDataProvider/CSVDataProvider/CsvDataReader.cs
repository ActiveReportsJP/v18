using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	/// .NETフレームワークのCSVデータプロバイダの<see cref="Extensibility.Data.IDataReader"/>を実装する方法を提供します。
	/// </summary>
	internal class CsvDataReader : DbDataReader
	{
		private Hashtable _typeLookup = new Hashtable(new myCultureComparer(new CultureInfo(Properties.Resources.CultureString)));

		private Hashtable _columnLookup = new Hashtable();
		private object[] _columns;
		private TextReader _textReader;
		private object[] _currentRow;

		//早急な処理を行うために、正規表現をプリコンパイルします。

		//マルチスレッドを避けるために、プロパティは読み取り専用に設定されています。

		private static readonly Regex _rxDataRow = new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))", RegexOptions.Compiled);
		//データ行を解析するために使用されます。

		private static readonly Regex _rxHeaderRow =
			new Regex(@"(?<fieldName>(\w*\s*)*)\((?<fieldType>\w*)\)", RegexOptions.Compiled); //ヘッダ行を解析するために使用されます。

		/// <summary>
		/// </summary>
		/// <param name="textReader">データ読み込みに使用される<see cref="TextReader"/>。</param>
		public CsvDataReader(TextReader textReader)
		{
			_textReader = textReader;
			ParseCommandText();
		}

		/// <summary>
		///渡されたコマンドのテキストを解析します。
		/// </summary>
		private void ParseCommandText()
		{
			if (_textReader.Peek() == -1)
				return; //コマンドのテキストは空であるか既に末尾にあります。

			FillTypeLookup();
			try
			{
				if (!ParseHeader(_textReader.ReadLine()))
					throw new InvalidOperationException(Properties.Resources.ParseTextException);

			}
			catch (Exception)
			{
			}
		}

		//ハッシュテーブルは、ヘッダのテキストで使用される文字列値の型を返すために使用されます。

		private void FillTypeLookup()
		{
			_typeLookup.Add("string", typeof(String));
			_typeLookup.Add("byte", typeof(Byte));
			_typeLookup.Add("boolean", typeof(Boolean));
			_typeLookup.Add("datetime", typeof(DateTime));
			_typeLookup.Add("decimal", typeof(Decimal));
			_typeLookup.Add("double", typeof(Double));
			_typeLookup.Add("int16", typeof(Int16));
			_typeLookup.Add("int32", typeof(Int32));
			_typeLookup.Add("int", typeof(Int32));
			_typeLookup.Add("integer", typeof(Int32));
			_typeLookup.Add("int64", typeof(Int64));
			_typeLookup.Add("sbyte", typeof(SByte));
			_typeLookup.Add("single", typeof(Single));
			_typeLookup.Add("time", typeof(DateTime));
			_typeLookup.Add("date", typeof(DateTime));
			_typeLookup.Add("uint16", typeof(UInt16));
			_typeLookup.Add("uint32", typeof(UInt32));
			_typeLookup.Add("uint64", typeof(UInt64));
		}

		/// <summary>
		/// ヘッダテキストの文字列から渡された文字列の値に基づいてデータ型を返します。一致しない場合は、文字列型が返されます。
		/// </summary>
		/// <param name="fieldType">ヘッダコマンドのテキスト文字列からの文字列値。</param>
		private Type GetFieldTypeFromString(string fieldType)
		{
			if (_typeLookup.Contains(fieldType))
				return _typeLookup[fieldType] as Type;
			return typeof(String);
		}

		/// <summary>
		/// 渡されたコマンドのテキストの先頭行を解析し、フィールド名およびデータ型を作成します。フィールドの情報は、
		/// <see cref="CsvColumn"/>構造体に保存され、列情報の項目はArrayListに格納されています。列名は、容易に検索できるハッシュテーブルに
		/// 追加します。
		/// </summary>
		/// <param name="header">すべてのフィールドを含むヘッダ文字列。</param>
		/// <returns>ヘッダ文字列を解析できる場合は、True。それ以外の場合は、False。</returns>
		private bool ParseHeader(string header)
		{
			string fieldName;
			int index = 0;
			if (header.IndexOf("(") == -1)
			{
				return false;
			}

			MatchCollection matches = _rxHeaderRow.Matches(header);
			_columns = new object[matches.Count];
			foreach (Match match in matches)
			{
				fieldName = match.Groups["fieldName"].Value;
				Type fieldType = GetFieldTypeFromString(match.Groups["fieldType"].Value);
				_columns.SetValue(new CsvColumn(fieldName, fieldType), index);
				_columnLookup.Add(fieldName, index);
				index++;
			}
			return true;
		}

		/// <summary>
		/// 正規表現を使用し、データの行を解析します。データの現在の行となるオブジェクト配列内に情報を保存します。
		/// 行でフィールド数が正しくない場合は、例外が発生します。
		/// </summary>
		/// <param name="dataRow">カンマ区切りのデータ行を表す文字列値。</param>
		/// <returns>データ文字列を解析できる場合は、True。それ以外の場合は、False。</returns>
		private bool ParseDataRow(string dataRow)
		{
			int index = 0;
			string[] tempData = _rxDataRow.Split(dataRow);

			_currentRow = new object[tempData.Length];
			if (tempData.Length != _columns.Length)
			{
				string error = string.Format(CultureInfo.InvariantCulture, Properties.Resources.ParseDataRowError, dataRow);
				
				throw new InvalidOperationException(error);
			}
			for (int i = 0; i < tempData.Length; i++)
			{
				string value = tempData[i];

				if (value.Length > 1)
				{
					if (value.IndexOf('"', 0) == 0 && value.IndexOf('"', 1) == value.Length - 1)
						value = value.Substring(1, value.Length - 2);
				}
				_currentRow.SetValue(ConvertValue(GetFieldType(index), value), index);
				index++;
			}
			return true;
		}

		/// <summary>
		///フィールド型に基づいてコマンドのテキストからの文字列値を適切なデータ型に変換します。
		/// また、String.EmptyまたはSystem.Data.DBNullを返す必要がないかを確認するためにいくつか文字列値のルールをチックします。
		/// </summary>
		/// <param name="type">現在の列の型は、データが属している.</param>
		/// <param name="originalValue">コマンドテキストからの文字列値。</param>
		/// <returns>データ型に基づいている変換された文字列を返します。</returns>
		private object ConvertValue(Type type, string originalValue)
		{
			Type fieldType = type;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			try
			{
				if (originalValue == "\"\"" || originalValue == " ")
					return string.Empty;
				if (originalValue == "")
					return DBNull.Value;
				if (originalValue == "DBNull")
					return DBNull.Value;
				if (fieldType == typeof(String))
					return originalValue.Trim();
				if (fieldType == typeof(Int32))
					return Convert.ToInt32(originalValue, invariantCulture);
				if (fieldType == typeof(Boolean))
					return Convert.ToBoolean(originalValue, invariantCulture);
				if (fieldType == typeof(DateTime))
					return Convert.ToDateTime(originalValue, invariantCulture);
				if (fieldType == typeof(Decimal))
					return Convert.ToDecimal(originalValue, invariantCulture);
				if (fieldType == typeof(Double))
					return Convert.ToDouble(originalValue, invariantCulture);
				if (fieldType == typeof(Int16))
					return Convert.ToInt16(originalValue, invariantCulture);
				if (fieldType == typeof(Int64))
					return Convert.ToInt64(originalValue, invariantCulture);
				if (fieldType == typeof(Single))
					return Convert.ToSingle(originalValue, invariantCulture);
				if (fieldType == typeof(Byte))
					return Convert.ToByte(originalValue, invariantCulture);
				if (fieldType == typeof(SByte))
					return Convert.ToSByte(originalValue, invariantCulture);
				if (fieldType == typeof(UInt16))
					return Convert.ToUInt16(originalValue, invariantCulture);
				if (fieldType == typeof(UInt32))
					return Convert.ToUInt32(originalValue, invariantCulture);
				if (fieldType == typeof(UInt64))
					return Convert.ToUInt64(originalValue, invariantCulture);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException(string.Format(Properties.Resources.ConvertValueExceprion, originalValue, type), e);
			}
			//一致しない場合は、DBNullを返します。

			return DBNull.Value;
		}


		/// <summary>
		/// <see cref="CsvDataReader"/>を次のレコードに移動します。
		/// </summary>
		/// <returns>他の行がある場合はTrue、それ以外の場合はFalse。</returns>
		public override bool Read()
		{
			if (_textReader.Peek() > -1)
				ParseDataRow(_textReader.ReadLine());
			else
				return false;

			return true;
		}

		/// <summary>
		///<see cref="CsvDataReader"/>により使用されたリソースを解放します。
		/// </summary>
		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_textReader != null)
					_textReader.Close();
			}

			_typeLookup = null;
			_columnLookup = null;
			_columns = null;
			_currentRow = null;
		}

		/// <summary>
		/// Objectがガベージ コレクションにより収集される前に、その Object がリソースを解放し、その他のクリーンアップ操作を実行できるようにします。
		/// </summary>
		~CsvDataReader()
		{
			Dispose(false);
		}

		/// <summary>
		/// 現在の行の列数を取得します。
		/// </summary>
		public override int FieldCount
		{
			get {
				if (_columns == null)
					_columns = new object[0];
				return _columns.Length;
			}
		}

		public override int Depth
		{
			get { return 0; }
		}

		public override bool IsClosed
		{
			get { return false; }
		}

		public override int RecordsAffected
		{
			get { return 0; }
		}

		public override bool HasRows
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override object this[string name]
		{
			get { return this[GetOrdinal(name).ToString()]; }
		}

		public override object this[int i]
		{
			get { return GetValue(i); }
		}

		/// <summary>
		/// 指定したフィールドの値を返します。
		/// </summary>
		/// <param name="i">検索するフィールドのインデックス。 </param>
		/// <returns>フィールドの値を返す<see cref="Object"/>。</returns>
		public override Type GetFieldType(int ordinal)
		{
			if (ordinal > _columns.Length - 1)
				return null;

			return ((CsvColumn) _columns.GetValue(ordinal)).DataType;
		}
		
		/// <summary>
		/// 指定したフィールドの値を返します。
		/// </summary>
		/// <param name="i">検索するフィールドのインデックス。 </param>	   
		///フィールドの名前を返します。値はない場合は、空の文字列（""）を返します。
		public override string GetName(int i)
		{
			if (i > _columns.Length - 1)
				return string.Empty;

			return ((CsvColumn) _columns.GetValue(i)).FieldName;
		}

		public override int GetOrdinal(string name)
		{
			object value = _columnLookup[name];
			if (value == null)
				throw new IndexOutOfRangeException("name");
			return (int) value;
		}

		/// <summary>
		/// 指定したフィールドの値を返します。
		/// </summary>
		/// <param name="i">検索するフィールドのインデックス。 </param>
		/// <returns>フィールドの値を返す<see cref="Object"/>。</returns>
		public override object GetValue(int i)
		{
			if (i > _columns.Length - 1)
				return null;

			return _currentRow.GetValue(i);
		}

		public override void Close()
		{}

		public override System.Data.DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		public override bool NextResult()
		{
			throw new NotImplementedException();
		}

		public override string GetDataTypeName(int i)
		{
			return GetFieldType(i).ToString();
		}

		public override int GetValues(object[] values)
		{
			throw new NotImplementedException();
		}

		public override  bool GetBoolean(int i)
		{
			throw new NotImplementedException();
		}

		public override byte GetByte(int i)
		{
			throw new NotImplementedException();
		}

		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public override char GetChar(int i)
		{
			throw new NotImplementedException();
		}

		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public override Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		public override short GetInt16(int i)
		{
			throw new NotImplementedException();
		}

		public override int GetInt32(int i)
		{
			throw new NotImplementedException();
		}

		public override long GetInt64(int i)
		{
			throw new NotImplementedException();
		}

		public override float GetFloat(int i)
		{
			throw new NotImplementedException();
		}

		public override double GetDouble(int i)
		{
			throw new NotImplementedException();
		}

		public override string GetString(int i)
		{
			throw new NotImplementedException();
		}

		public override decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		public override DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		public override bool IsDBNull(int i)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#region EqualityComparer

		class myCultureComparer : IEqualityComparer
		{
			public CaseInsensitiveComparer myComparer;

			public myCultureComparer()
			{
				myComparer = CaseInsensitiveComparer.DefaultInvariant;
			}

			public myCultureComparer(CultureInfo myCulture)
			{
				myComparer = new CaseInsensitiveComparer(myCulture);
			}

			public new bool Equals(object x, object y)
			{
				if (myComparer.Compare(x, y) == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			public int GetHashCode(object obj)
			{
				return obj.ToString().ToLower().GetHashCode();
			}
		}

		#endregion 
	}
}
