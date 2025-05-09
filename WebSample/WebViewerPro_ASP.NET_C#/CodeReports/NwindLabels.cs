using System;

namespace GrapeCity.ActiveReports.Samples.Web.CodeReports
{
	/// <summary>
	/// Summary description for NWindLabels.
	/// </summary>
	public partial class NwindLabels : GrapeCity.ActiveReports.SectionReport
	{
		System.Data.DataTable table = new System.Data.DataTable("Results");
		int counter = 0;

		public NwindLabels()
		{
			//
			// Required for Windows Form Designer support.
			//
			InitializeComponent();
		}

		private void ActiveReport_DataInitialize(object sender, System.EventArgs args)
		{
			table.Columns.Add("CustomerID", typeof(String));
			this.Fields.Add("CustomerID");
			table.Columns.Add("CompanyName", typeof(String));
			this.Fields.Add("CompanyName");
			table.Columns.Add("ContactName", typeof(String));
			this.Fields.Add("ContactName");
			table.Columns.Add("ContactTitle", typeof(String));
			this.Fields.Add("ContactTitle");
			table.Columns.Add("Address", typeof(String));
			this.Fields.Add("Address");
			table.Columns.Add("City", typeof(String));
			this.Fields.Add("City");
			table.Columns.Add("Region", typeof(String));
			this.Fields.Add("Region");
			table.Columns.Add("PostalCode", typeof(String));
			this.Fields.Add("PostalCode");
			table.Columns.Add("Country", typeof(String));
			this.Fields.Add("Country");
			table.Columns.Add("Phone", typeof(String));
			this.Fields.Add("Phone");
			table.Columns.Add("Fax", typeof(String));
			this.Fields.Add("Fax");
			table.Rows.Add(new object[] {"1","喫茶たいむましん","林　 千春","店長","佐賀市長瀬町 23-XX","佐賀市",null,"840-0853","佐賀県","(0952)26-64XX",null,});
			table.Rows.Add(new object[] {"10","東海道スーパー","河本 なみ","料理長","那覇市繁多川 1-21-XX","那覇市",null,"902-0071","沖縄県","(0988)55-87XX","(0988)55-87XX",});
			table.Rows.Add(new object[] {"11","小町ストアー","山久 良美","料理長","富山市朝菜町 2-702-XX","富山市",null,"939-8066","富山県","(0764)25-58XX","(0764)25-58XX",});
			table.Rows.Add(new object[] {"12","北冷マート","和辺 義隆","料理長","津市一身田中野 19-X","津市",null,"514-0112","三重県","(0592)32-65XX","(0592)32-65XX",});
			table.Rows.Add(new object[] {"13","札幌フード","渡川 秀人","料理長","京都市西京区山田平尾町 73-X","京都府",null,"615-8256","京都府","(075)392-76XX","(075)392-76XX",});
			table.Rows.Add(new object[] {"14","雪花ガーデン","小田 勝也","料理長","板野郡藍住町住吉 5-XXX","板野郡",null,"771-1264","徳島県","(0886)92-34XX",null,});
			table.Rows.Add(new object[] {"15","城元株式会社","池林 裕香","店長","大田区大森東 1-35-X","大田区",null,"143-0012","東京都","(03)3763-01XX","(03)3763-01XX",});
			table.Rows.Add(new object[] {"16","宮株式会社","池山 剛司","料理長","吾妻郡草津町 462-XX","吾妻郡","豆島","377-1700","群馬県","(0279)88-31XX","(0279)88-31XX",});
			table.Rows.Add(new object[] {"17","月野株式会社","木山 勇","店長","長野市大豆島 1798-X","長野市",null,"381-0022","長野県","(0262)21-48XX",null,});
			table.Rows.Add(new object[] {"18","葉薄ふぁん","鈴藤 哲也","店長","習志野市津田沼 2-6-XX","習志野市",null,"275-0016","千葉県","(0474)78-57XX","(0474)78-57XX",});
			table.Rows.Add(new object[] {"19","屋台すまいる","佐本 久明","店長","大館市釈迦内字館 30-XX","大館市",null,"017-0012","秋田県","(0186)48-40XX","(0186)48-40XX",});
			table.Rows.Add(new object[] {"2","小料理なんごく","坂田 由利","店長","札幌市中央区北5条西 12-2-19-XXX","札幌市",null,"060-0005","北海道","(011)272-01XX","(011)272-01XX",});
			table.Rows.Add(new object[] {"20","商店せんしょう","園村 真一","店長","札幌市手稲区前田 4-9-3-XX","札幌市","手稲区前田","006-0814","北海道","(011)681-67XX","(011)681-67XX",});
			table.Rows.Add(new object[] {"21","名店はかたっこ","田本 千賀","営業部","札幌市豊平区中の島一条2丁目 4-XX","札幌市",null,"062-0921","北海道","(011)831-99XX","(011)831-99XX",});
			table.Rows.Add(new object[] {"22","食所あんどう","山水 伸俊","営業部","多賀城市笠神 3-2-X","多賀城市",null,"985-0831","宮城県","(022)362-30XX","(022)362-30XX",});
			table.Rows.Add(new object[] {"23","自然食なちゅらる","清岡 裕美子","営業部","仙台市宮城野区古宮 4-X","仙台市",null,"983-0000","宮城県","(022)238-53XX","(022)238-53XX",});
			table.Rows.Add(new object[] {"24","惣菜びみ","武島 友子","営業部","仙台市太白区人来田 3-15-XX","仙台市","太白区","982-0222","宮城県","(022)243-27XX","(022)243-27XX",});
			table.Rows.Add(new object[] {"25","洋食ちくさ","大田 泰江","料理長","吹田市竹見台 2-X","吹田市",null,"565-0863","大阪府","(06)6872-40XX","(06)6872-40XX",});
			table.Rows.Add(new object[] {"26","小料理ひろ","島中 和明","料理長","池田市伏尾台 2-9-X","池田市","伏尾台","563-0017","大阪府","(0727)51-94XX","(0727)51-94XX",});
			table.Rows.Add(new object[] {"27","洋風居酒屋けい・えっくす","田山 雄一","店長","伊丹市池尻 5-XX","伊丹市",null,"664-0027","兵庫県","(0727)77-19XX","(0727)77-19XX",});
			table.Rows.Add(new object[] {"28","料亭きゅうきゅう","玉原 義弘","店長","朝倉郡夜須町篠隈 225-XX","朝倉郡","須町","838-0215","福岡県","(0946)42-30XX","(0946)42-30XX",});
			table.Rows.Add(new object[] {"29","食料品店ふじ","木原 晃一","料理長","粕屋郡志免町御手洗 51-X","粕屋郡",null,"811-2206","福岡県","(092)621-16XX","(092)621-16XX",});
			table.Rows.Add(new object[] {"3","割烹ふじい","鈴崎 礼子","店長","福岡市博多区東平尾 2-10-XX","福岡市","博多区","816-0053","福岡県","(092)611-36XX","(092)611-36XX",});
			table.Rows.Add(new object[] {"30","夷そば","村中 真人","店長","西春日井郡豊山町豊場新田 17-X","西春日井郡",null,"480-0202","愛知県","(0568)28-32XX","(0568)28-21XX",});
			table.Rows.Add(new object[] {"31","びしゃもんや","渡　 浩志","料理長","名古屋市瑞穂区中根町 5-4-X","名古屋市","中根町","467-0055","愛知県","(052)833-58XX","(052)833-58XX",});
			table.Rows.Add(new object[] {"32","コンビニエンス北風","辺上 寿生","料理長","春日井市味美白山町 1-9-X","春日井市",null,"486-0969","愛知県","(0568)34-07XX","(0568)34-07XX",});
			table.Rows.Add(new object[] {"33","笹の葉食料品店","森野 恭久","料理長","鹿児島市宇宿町 2655-XX","鹿児島市","白山町","890-0074","鹿児島県","(0992)56-46XX","(0992)56-46XX",});
			table.Rows.Add(new object[] {"34","ジャンボストアー","河垣 加奈子","料理長","広島市西区観音新町 1-16-X","広島市","西区","733-0036","広島県","(082)233-18XX","(082)233-18XX",});
			table.Rows.Add(new object[] {"35","よろず商店","柴瀬 満","店長","浜北市於呂 3482-XX","浜北市",null,"434-0015","静岡県","(05358)8-27XX","(05358)8-27XX",});
			table.Rows.Add(new object[] {"36","山門屋","岩村 浩之","料理長","鹿児島市宇宿 2-14-X","鹿児島市",null,"890-0073","鹿児島県","(0992)59-12XX","(0992)59-12XX",});
			table.Rows.Add(new object[] {"37","イルカランド","西詰 幸造","料理長","成田市台方 XXX","成田市",null,"286-0003","千葉県","(0476)26-97XX","(0476)26-96XX",});
			table.Rows.Add(new object[] {"38","大宮ユニオン","所　 政司","店長","横浜市南区大岡 1-44-X","横浜市",null,"232-0061","神奈川県","(045)741-12XX",null,});
			table.Rows.Add(new object[] {"39","アリス亭","塚田 冬美","店長","横浜市緑区千草台 7-X","横浜市","千草台","226-0000","神奈川県","(045)973-66XX","(045)973-66XX",});
			table.Rows.Add(new object[] {"4","海鮮料理くじら","上川 昌真","店長","成田市加良部 5-3-X","千葉県",null,"286-0036","千葉県","(0476)27-36XX","(0476)27-36XX",});
			table.Rows.Add(new object[] {"40","みちのく本陣","川井 伸好","店長","川崎市中原区宮内 XXX","成田市","中原区","211-0051","神奈川県","(044)752-15XX","(044)752-15XX",});
			table.Rows.Add(new object[] {"41","ポム・ド・テール","横浦 幸雄","店長","中野区大和町 2-41-XX","川崎市",null,"165-0034","東京都","(03)3339-32XX",null,});
			table.Rows.Add(new object[] {"42","コーヒーハウスフェンス","箕村 綾子","営業部","大田区萩中 2-4-X","中野区",null,"144-0047","東京都","(03)3742-44XX","(03)3742-44XX",});
			table.Rows.Add(new object[] {"43","甘味喫茶ダイ","野内 良昭","営業部","草加市吉町 3-4-X","草加市",null,"340-0017","埼玉県","(0489)28-98XX","(0489)28-98XX",});
			table.Rows.Add(new object[] {"44","蓬莱堂","内藤 一志","料理長","野田市日之出町 27-XX","野田市",null,"270-0234","千葉県","(0471)29-16XX","(0471)29-16XX",});
			table.Rows.Add(new object[] {"45","健食弁当株式会社","瀬　 知子","料理長","杉並区成田東 5-35-XX","杉並区",null,"166-0015","東京都","(03)5397-67XX","(03)5397-37XX",});
			table.Rows.Add(new object[] {"46","ヒロコーポレーション","若本 喜一","料理長","川越市熊野町 12-2-XXX","川越市",null,"350-1146","埼玉県","(0492)41-27XX","(0492)41-27XX",});
			table.Rows.Add(new object[] {"47","浜辺商店","宮部 圭江","店長","君津市袖ケ浦町野里 1539-X","君津市",null,"299-1100","千葉県","(0438)75-20XX",null,});
			table.Rows.Add(new object[] {"48","レストラン石坂","越安 辰夫","店長","逗子市山の根 3-15-XX","逗子市",null,"249-0002","神奈川県","(0468)73-24XX","(0468)73-24XX",});
			table.Rows.Add(new object[] {"49","パーラーえんとつ","中田 栄","営業部","世田谷区給田 3-31-X","世田谷区","世田谷区","157-0064","東京都","(03)3300-27XX","(03)3300-27XX",});
			table.Rows.Add(new object[] {"5","居酒屋ななべえ","林　 智由","営業部","世田谷区奥沢 1-37-XX","世田谷区",null,"158-0083","東京都","(03)3728-86XX","(03)3728-76XX",});
			table.Rows.Add(new object[] {"50","高原亭","小熊 一之","営業部","横浜市旭区左近山 3-18-XXX","横浜市","旭区","241-0831","神奈川県","(045)352-37XX","(045)352-37XX",});
			table.Rows.Add(new object[] {"51","からんころん","古井 広宣","店長","茅ヶ崎市西久保 1592-X","茅ヶ崎市",null,"253-0083","神奈川県","(0467)85-35XX","(0467)85-35XX",});
			table.Rows.Add(new object[] {"6","酒蔵でん","佐賀 明美","営業部","志木市幸町 1-8-40-XXXX","志木市",null,"353-0005","埼玉県","(0484)71-42XX","(0484)71-42XX",});
			table.Rows.Add(new object[] {"7","寿ストアー","川田 隆裕","店長","練馬区東大泉 1-26-31-XXX","練馬区","大泉","178-0063","東京都","(03)3923-48XX",null,});
			table.Rows.Add(new object[] {"8","温泉レストラン","山神 祐子","料理長","牛久市下根町 1504-XX","牛久市",null,"300-1203","茨城県","(0298)73-12XX","(0298)73-12XX",});
			table.Rows.Add(new object[] {"9","大和マーケット","江田 真理子","料理長","横浜市中区千代崎町1-3-5","横浜市","中区","231-0864","神奈川県","(045)622-36XX","(045)622-84XX",});

		}
		private void ActiveReport_FetchData(object sender, FetchEventArgs eArgs)
		{

			if (counter == table.Rows.Count)
			{

				eArgs.EOF = true;
				return;

			}
			else
			{
				this.Fields["CustomerID"].Value = table.Rows[counter]["CustomerID"];
				this.Fields["CompanyName"].Value = table.Rows[counter]["CompanyName"];
				this.Fields["ContactName"].Value = table.Rows[counter]["ContactName"];
				this.Fields["ContactTitle"].Value = table.Rows[counter]["ContactTitle"];
				this.Fields["Address"].Value = table.Rows[counter]["Address"];
				this.Fields["City"].Value = table.Rows[counter]["City"];
				this.Fields["Region"].Value = table.Rows[counter]["Region"];
				this.Fields["PostalCode"].Value = table.Rows[counter]["PostalCode"];
				this.Fields["Country"].Value = table.Rows[counter]["Country"];
				this.Fields["Phone"].Value = table.Rows[counter]["Phone"];
				this.Fields["Fax"].Value = table.Rows[counter]["Fax"];
				counter++;
				eArgs.EOF = false;
			}
		}
	}
}