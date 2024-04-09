Imports System.Xml
Imports System.Xml.XPath

' サンプルで使用するデータを提供します。
Friend NotInheritable Class DataLayer

	Public Function CreateReader() As XmlReader
		Dim txtReader As New XmlTextReader("..\..\..\MyXmlDB.xml")
		Return txtReader
	End Function

	Public Function CreateDocument() As IXPathNavigable
		Dim doc As New XPathDocument("..\..\..\MyXmlDB.xml")
		Return doc
	End Function

End Class
