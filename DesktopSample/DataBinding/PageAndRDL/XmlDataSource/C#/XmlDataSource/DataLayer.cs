using System.Xml;
using System.Xml.XPath;

namespace ActiveReports.Samples.XmlDataSource
{
	// サンプルで使用するデータを提供します。
	internal sealed class DataLayer
	{
		public XmlReader CreateReader()
		{
			var txtReader = new XmlTextReader(@"..\..\..\MyXmlDB.xml");
			return txtReader;
		}

		public IXPathNavigable CreateDocument()
		{
			var doc = new XPathDocument(@"..\..\..\MyXmlDB.xml");
			return doc;
		}
	}
}
