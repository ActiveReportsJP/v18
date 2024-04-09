using System;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace ActiveReports.Samples.IListBinding.DataLayer
{
	public class ProductCollection : CollectionBase, IComponent
	{
		private ISite _site;  //IComponentの実装に必要です。
		public ProductCollection()
		{
			FillWithAllProducts();
			_site = null;
		}
	
		public void FillWithAllProducts()
		{
			// 現在のリストをリセットします。
			List.Clear();
			
			using (var cn = DataProvider.NewConnection)
			{
				cn.Open();
				var cmd = new SQLiteCommand("SELECT * FROM Products", cn);
				IDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					List.Add(ProductFromDataReader(reader));
				}
			}
		}

		/// <summary>
		/// ProductFromDataReader: リストのデータベースから新しい製品を作成するために使用します。
		/// </summary>
		/// <param name="reader">データを含む有効なDataReaderオブジェクト。</param>
		/// <returns>データ行から新しく作成されたProductオブジェクト。</returns>
		internal static Product ProductFromDataReader(IDataReader reader)
		{
			Product p = new Product();
			p.CategoryID = Convert.ToInt32(reader["CategoryID"]);
			p.Discontinued = Convert.ToBoolean(reader["Discontinued"]);
			p.ProductID  = Convert.ToInt32(reader["ProductID"]);
			p.ProductName  = Convert.ToString(reader["ProductName"]);
			p.QuantityPerUnit = Convert.ToString(reader["QuantityPerUnit"]);
			p.ReorderLevel = Convert.ToInt32(reader["ReorderLevel"]);
			p.SupplierID = Convert.ToInt32(reader["SupplierID"]);
			p.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
			p.UnitsInStock = Convert.ToInt32(reader["UnitsInStock"]);
			p.UnitsOnOrder = Convert.ToInt32(reader["UnitsOnOrder"]);
			return p;
		}


		public event EventHandler Disposed;

		public ISite Site
		{
			get
			{
				return _site;
			}
			set
			{
				_site = value;
			}
		}

		/// <summary>
		/// IComponentの実装に必要です。
		/// </summary>
		public void Dispose()
		{
			OnDisposed(EventArgs.Empty);
		}

		/// <summary>
		/// IComponentの実装に必要です。
		/// </summary>
		protected virtual void OnDisposed(EventArgs e)
		{
			if (Disposed != null)
				Disposed(this, e);
		}
	}
}
