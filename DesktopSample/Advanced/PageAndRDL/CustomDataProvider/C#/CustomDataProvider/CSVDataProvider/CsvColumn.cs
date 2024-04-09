using System;

namespace ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	/// データソースのフィールドの情報を表します。
	/// </summary>
	internal struct CsvColumn
	{
		private readonly string _fieldName;
		private readonly Type _dataType;

		/// <summary>
		/// <see cref="CsvColumn"/>クラスの新しいインスタンスを作成します。
		/// </summary>
		/// <param name="fieldName">このインスタンスに対するフィールドの<see cref="CsvColumn"/>名前。</param>
		///<param name="dataType">このインスタンスに対するフィールドの<see cref="Type"/>。</param>
		public CsvColumn(string fieldName, Type dataType)
		{
			if (fieldName == null)
				throw new ArgumentNullException("fieldName");
			if (dataType == null)
				throw new ArgumentNullException("dataType");
			_fieldName = fieldName;
			_dataType = dataType;
		}

		/// <summary>
		/// このインスタンスに対するフィールドの名前。
		/// </summary>
		public string FieldName
		{
			get { return _fieldName; }
		}

		/// <summary>
		/// このインスタンスに対するフィールドの<see cref="Type"/>。
		/// </summary>
		public Type DataType
		{
			get { return _dataType; }
		}

		/// <summary>
		///文字列に変換された<see cref="CsvColumn"/>のインスタンスを返します。
		/// </summary>
		///  <returns><see cref="CsvColumn"/>のインスタンスを表す文字列。</returns>
		public override string ToString()
		{
			return String.Concat(new string[] {FieldName, "(", DataType.ToString(), ")"});
		}

		/// <summary>
		/// <see cref="Csv Column"/>の2つのインスタンスが等しいかどうかを確認します。
		/// </summary>
		///  <param name="obj">現在の<see cref="CsvColumn"/>と比較する<see cref="CsvColumn"/>。</ PARAM>
		/// <returns>指定した<see cref="CsvColumn"/>は現在の<see cref="CsvColumn"/>と等しい場合は、True。そうでない場合はFalseです。</returns>
		public override bool Equals(object obj)
		{
			bool flag;

			if (obj is CsvColumn)
			{
				flag = Equals((CsvColumn) obj);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private bool Equals(CsvColumn column)
		{
			return column.FieldName == FieldName;
		}

		/// <summary>
		/// ハッシュテーブルのようなハッシュアルゴリズムやデータ構造で使用される<see cref="CsvColumn"/>のハッシュ関数です。
		/// </summary>
		/// <returns>現在<seeのcref="CsvColumn"/>のインスタンスのハッシュコード。</returns>
		public override int GetHashCode()
		{
			return (FieldName.GetHashCode() + DataType.GetHashCode());
		}
	}
}
