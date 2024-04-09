Imports System
Imports System.ComponentModel
Imports System.Collections
Imports System.Data
Imports System.Data.SQLite

Public Class ProductCollection
	Inherits CollectionBase
	Implements IComponent

	Private _site As ISite

	Public Sub New()
		FillWithAllProducts()
		_site = Nothing
	End Sub 'New

	'<summary>
	' FillWithAllProducts - DBからのすべての製品のリストを作成するために使用
	'</summary>
	Public Sub FillWithAllProducts()
		'現在のリストをリセットする
		List.Clear()

		'開いている接続とデータを読み取る data
		Dim cn As SQLiteConnection = DataProvider.NewConnection
		Try
			cn.Open()
			Dim cmd As New SQLiteCommand("SELECT * FROM Products", cn)
			Dim reader As IDataReader = cmd.ExecuteReader()

			While reader.Read()
				List.Add(ProductFromDataReader(reader))
			End While
		Finally
			cn.Dispose()
		End Try
	End Sub

	'<summary>
	' ProductFromDataReader - リストのデータベースから新しい製品を作成するために使用
	'</summary>
	'<param name="reader">データを含む有効なDataReaderオブジェクト</param>
	'<returns>データ行から、新しく作成された製品固有のオブジェクト</returns>
	Friend Shared Function ProductFromDataReader(ByVal reader As IDataReader) As Product
		Dim p As New Product
		p.CategoryID = Convert.ToInt32(reader("CategoryID"))
		p.Discontinued = Convert.ToBoolean(reader("Discontinued"))
		p.ProductID = Convert.ToInt32(reader("ProductID"))
		p.ProductName = Convert.ToString(reader("ProductName"))
		p.QuantityPerUnit = Convert.ToString(reader("QuantityPerUnit"))
		p.ReorderLevel = Convert.ToInt32(reader("ReorderLevel"))
		p.SupplierID = Convert.ToInt32(reader("SupplierID"))
		p.UnitPrice = Convert.ToDecimal(reader("UnitPrice"))
		p.UnitsInStock = Convert.ToInt32(reader("UnitsInStock"))
		p.UnitsOnOrder = Convert.ToInt32(reader("UnitsOnOrder"))
		Return p
	End Function 'ProductFromDataReader

	Public Event Disposed As EventHandler Implements IComponent.Disposed

	'IComponentの実装に必要な
	Public Sub Dispose() Implements IComponent.Dispose
		'きれいにするものは何もありません。
		RaiseEvent Disposed(Me, EventArgs.Empty)
	End Sub

	'IComponentの実装に必要な
	Public Property Site() As ISite Implements IComponent.Site
		Get
			Return _site
		End Get
		Set(ByVal Value As ISite)
			_site = Value
		End Set
	End Property

End Class 'ProductCollection
