using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.ReportsGallery
{
	public partial class ReportsForm : Form
	{

		PageDocument _document;
		Boolean loaded = true;
		
		static readonly string FolderPath = "";
		static readonly List<string> ExcludeFilesList = new List<string>();
		static readonly List<string> ExcludeFoldersList = new List<string>();

		public ReportsForm()
		{
			InitializeComponent();
			Icon = Properties.Resources.App;
		}

		//CONFIGファイルから設定を読み込みします。
		static ReportsForm()
		{
			XDocument loaded = XDocument.Load("ReportsGallery.config");
			FolderPath = loaded.Descendants("FolderPath").Select(t => t.Value.ToString()).ToList()[0];
			DirectoryInfo reportbasefolder = new DirectoryInfo(FolderPath);
			ExcludeFilesList = loaded.Descendants("ExcludeFiles").ToList()[0].Descendants("File").Select(t => reportbasefolder.FullName + "\\" + t.Value.ToString()).ToList<string>();
			ExcludeFoldersList = loaded.Descendants("ExcludeFolders").ToList()[0].Descendants("Folder").Select(t => reportbasefolder.FullName + "\\" + t.Value.ToString()).ToList<string>();
		}

		// Form_Loadイベント
		private void ReportsForm_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(FolderPath))
			{
				ListDirectory(treeView, FolderPath);
			}
			FolderLocalization();
			treeView.Nodes[0].Expand();
			treeView.Nodes[0].Nodes[0].Expand();
			var reportFile = new FileInfo(treeView.Nodes[0].Nodes[0].Nodes[0].Tag.ToString());
			PageReport report = new PageReport(reportFile);
			_document = new PageDocument(report);
			reportViewer.LoadDocument(_document);
		}

		// ツリービューにノードを追加します。
		private void ListDirectory(TreeView treeView, string path)
		{
			treeView.Nodes.Clear();
			var rootDirectoryInfo = new DirectoryInfo(path);
			foreach (var directory in rootDirectoryInfo.GetDirectories())
			{
				treeView.Nodes.Add(CreateDirectoryNode(directory));
			}
		}

		// サンプルフォルダを走査し、ツリーを作成します。
		private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			var directoryNode = new TreeNode(directoryInfo.Name);
			foreach (var directory in directoryInfo.GetDirectories())
			{
				if (!ExcludeFoldersList.Exists(t => t.ToString() == directory.FullName))
				{
					directoryNode.Nodes.Add(CreateDirectoryNode(directory));
				}
			}
			foreach (var file in directoryInfo.GetFiles("*.rpx").Concat(directoryInfo.GetFiles("*.rdlx")))
			{
				if (!ExcludeFilesList.Exists(t => t.ToString() == file.FullName))
				{
					TreeNode reportFileNode = new TreeNode(file.Name);
					reportFileNode.ImageIndex = 2;
					reportFileNode.SelectedImageIndex = 2;
					reportFileNode.ForeColor = Color.Brown;
					reportFileNode.Tag = file.FullName;
					directoryNode.Nodes.Add(reportFileNode);
				}
			}
			return directoryNode;
		}
		//Folder Localization
		private void FolderLocalization()
		{

			Hashtable strReplace = new Hashtable();
			StreamReader reader = new StreamReader(new FileStream("ReportsGallery.config", FileMode.Open, FileAccess.Read, FileShare.Read));
			XmlDocument doc = new XmlDocument();
			string xmlIn = reader.ReadToEnd();
			reader.Close();
			doc.LoadXml(xmlIn);
			foreach (XmlNode child in doc.ChildNodes[1].ChildNodes)
				if (child.Name.Equals("Localization"))
					foreach (XmlNode node in child.ChildNodes)
						if (node.Name.Equals("ReplaceName"))
							strReplace.Add
							(
								node.Attributes["OriginalName"].Value,
								node.Attributes["ReplaceWith"].Value
							);

			for (int i = 0; i < treeView.Nodes.Count; i++)
			{
				foreach (DictionaryEntry entry in strReplace)
				{
					if (treeView.Nodes[i].Text.Equals(entry.Key.ToString()))
					{
						treeView.Nodes[i].Text = entry.Value.ToString();
					}
				}
			}
		}
		//ツリービューのAfterCollapseイベントを処理します。
		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			e.Node.ImageIndex = 0;
			e.Node.SelectedImageIndex = 0;
		}

		//ツリービューのAfterExpandイベントを処理します。
		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			e.Node.ImageIndex = 1;
			e.Node.SelectedImageIndex = 1;
		}

		//ツリーノードでNodeMouseClickイベントを処理します。
		private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if ((e.Node.Text.ToLower().EndsWith(".rdlx")))
			{
				e.Node.ImageIndex = 2;	
				treeView.SelectedNode = e.Node;
				FileInfo reportFile = new FileInfo(e.Node.Tag.ToString());
				PageReport report = new PageReport(reportFile);
				_document = new PageDocument(report);
			 if (treeView.SelectedNode.Text == "SalesDashboard.rdlx")
				{
					loaded = false;
				}
				else
					loaded = true;
				reportViewer.LoadDocument(_document);
			}
			else
			{
				if (e.Node.Text.ToLower().EndsWith(".rpx"))
				{
					using (XmlTextReader xmlReader = new XmlTextReader(e.Node.Tag.ToString()))
					{
						e.Node.ImageIndex = 2; 
						treeView.SelectedNode = e.Node;
						SectionReport report = new SectionReport();
						report.LoadLayout(xmlReader);
						reportViewer.LoadDocument(report);
					}
				}
				else
				{
					if (e.Node.Parent != null)
					{
						if(e.Node.Parent.Parent!=null)
						{
							MessageBox.Show(Properties.Resources.InvalidFileText);
						}
					}
				}
			}
		}
		
		//SalesDashboardレポートのTRUするGalleryModeを設定
		void reportViewer_LoadCompleted(object sender, EventArgs e)
		{
			if (loaded)
				return;
			else
				reportViewer.GalleyMode = true;
				loaded=true;
		}

	}
}
