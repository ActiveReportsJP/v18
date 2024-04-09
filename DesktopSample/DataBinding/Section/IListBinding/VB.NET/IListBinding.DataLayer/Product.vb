'<summary>
' Productの概要の説明です。
'</summary>
Public Class Product
	Friend Sub New()
	End Sub 'New

	'******
	'* 以下のプロパティは、データ層にある製品を定義します。 
	'******

	Public Property ProductID As Integer

	Public Property ProductName As String

	Public Property SupplierID As Integer

	Public Property CategoryID As Integer

	Public Property QuantityPerUnit As String

	Public Property UnitPrice As Decimal

	Public Property UnitsInStock As Integer

	Public Property UnitsOnOrder As Integer

	Public Property ReorderLevel As Integer

	Public Property Discontinued As Boolean
End Class 'Product
