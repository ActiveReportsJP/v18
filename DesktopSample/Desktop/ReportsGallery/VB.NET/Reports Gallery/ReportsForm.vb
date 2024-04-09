Imports GrapeCity.ActiveReports.Document
Imports System.IO
Imports System.Xml
Imports System.Text
Imports GrapeCity.ActiveReports

Public Class ReportsForm

	Private _document As PageDocument
	Private loaded As Boolean = True
	Private Shared _folderPath As String
	Public Shared ReadOnly Property FolderPath() As String
		Get
			Return _folderPath
		End Get
	End Property

	Private Shared _excludeFoldersList As New List(Of String)()
	Public Shared ReadOnly Property ExcludeFoldersList() As List(Of String)
		Get
			Return _excludeFoldersList
		End Get
	End Property

	Private Shared _excludeFilesList As New List(Of String)()
	Public Shared ReadOnly Property ExcludeFilesList() As List(Of String)
		Get
			Return _excludeFilesList
		End Get
	End Property

	'ツリービューにノードを追加します。
	Private Sub ListDirectory(ByVal treeView As TreeView, ByVal path As String)
		treeView.Nodes.Clear()
		Dim rootDirectoryInfo = New DirectoryInfo(path)
		For Each directory As DirectoryInfo In rootDirectoryInfo.GetDirectories()
			treeView.Nodes.Add(CreateDirectoryNode(directory))
		Next
	End Sub

	' サンプルフォルダを走査し、ツリーを作成します。
	Private Shared Function CreateDirectoryNode(ByVal directoryInfo As DirectoryInfo) As TreeNode
		Dim directoryNode = New TreeNode(directoryInfo.Name)
		For Each directory As DirectoryInfo In directoryInfo.GetDirectories()
			Dim dirname As String = directory.FullName
			If Not ExcludeFoldersList.Exists(Function(t) t.ToString() = dirname) Then
				directoryNode.Nodes.Add(CreateDirectoryNode(directory))
			End If
		Next
		For Each file As FileInfo In directoryInfo.GetFiles("*.rpx").Concat(directoryInfo.GetFiles("*.rdlx"))
			Dim filename As String = file.FullName
			If Not ExcludeFilesList.Exists(Function(t) t.ToString() = filename) Then
				Dim reportFileNode As New TreeNode(file.Name)
				reportFileNode.ImageIndex = 2
				reportFileNode.SelectedImageIndex = 2
				reportFileNode.ForeColor = Color.Brown
				reportFileNode.Tag = file.FullName
				directoryNode.Nodes.Add(reportFileNode)
			End If
		Next
		Return directoryNode
	End Function
	' フォルダのローカライズ
	Private Sub FolderLocalization()

		Dim strReplace As New Hashtable()
		Dim reader As New StreamReader(New FileStream("ReportsGallery.config", FileMode.Open, FileAccess.Read, FileShare.Read))
		Dim doc As New XmlDocument()
		Dim xmlIn As String = reader.ReadToEnd()
		reader.Close()
		doc.LoadXml(xmlIn)
		For Each child As XmlNode In doc.ChildNodes(1).ChildNodes
			If child.Name.Equals("Localization") Then
				For Each node As XmlNode In child.ChildNodes
					If node.Name.Equals("ReplaceName") Then
						strReplace.Add(node.Attributes("OriginalName").Value, node.Attributes("ReplaceWith").Value)
					End If
				Next
			End If
		Next

		For i As Integer = 0 To treeView.Nodes.Count - 1
			For Each entry As DictionaryEntry In strReplace
				If treeView.Nodes(i).Text.Equals(entry.Key.ToString()) Then
					treeView.Nodes(i).Text = entry.Value.ToString()
				End If
			Next
		Next
	End Sub
	Private Sub ReportsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not String.IsNullOrEmpty(FolderPath) Then
			ListDirectory(treeView, FolderPath)
		End If
		FolderLocalization()
		treeView.Nodes(0).Expand()
		treeView.Nodes(0).Nodes(0).Expand()
		Dim reportFile = New FileInfo(treeView.Nodes(0).Nodes(0).Nodes(0).Tag.ToString())
		Dim report = New PageReport(reportFile)
		_document = New PageDocument(report)
		reportViewer.LoadDocument(_document)
	End Sub

	Shared Sub New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		Dim loaded As XDocument = XDocument.Load("ReportsGallery.config")
		_folderPath = loaded.Descendants("FolderPath").[Select](Function(t) t.Value.ToString()).ToList()(0)
		Dim reportbasefolder As New DirectoryInfo(FolderPath)
		_excludeFilesList = loaded.Descendants("ExcludeFiles").ToList()(0).Descendants("File").[Select](Function(t) reportbasefolder.FullName + "\" & t.Value.ToString()).ToList()
		_excludeFoldersList = loaded.Descendants("ExcludeFolders").ToList()(0).Descendants("Folder").[Select](Function(t) reportbasefolder.FullName + "\" & t.Value.ToString()).ToList()
	End Sub


	Public Sub New()
		InitializeComponent()
		Icon = My.Resources.App
	End Sub

	Private Sub TreeView_AfterCollapse(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView.AfterCollapse
		e.Node.ImageIndex = 0
		e.Node.SelectedImageIndex = 0
	End Sub

	Private Sub TreeView_AfterExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView.AfterExpand
		e.Node.ImageIndex = 1
		e.Node.ImageIndex = 1
		e.Node.SelectedImageIndex = 1
	End Sub

	'ツリーノードでNodeMouseClickイベントを処理します。
	Private Sub TreeView_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles treeView.NodeMouseClick

		If (e.Node.Text.ToLower().EndsWith(".rdlx")) Then
			e.Node.ImageIndex = 2
			treeView.SelectedNode = e.Node
			Dim reportFile As New FileInfo(e.Node.Tag.ToString())
			Dim pageReport As New PageReport(reportFile)
			_document = New PageDocument(pageReport)
			If treeView.SelectedNode.Text = "SalesDashboard.rdlx" Then
				loaded = False
			Else
				loaded = True
			End If
			reportViewer.LoadDocument(_document)
		Else
			If e.Node.Text.ToLower().EndsWith(".rpx") Then
				Using xmlReader As New XmlTextReader(e.Node.Tag.ToString())
					e.Node.ImageIndex = 2
					treeView.SelectedNode = e.Node
					Dim report As New SectionReport()
					report.LoadLayout(xmlReader)
					reportViewer.LoadDocument(report)
				End Using

			Else
				If e.Node.Parent IsNot Nothing Then
					If e.Node.Parent.Parent IsNot Nothing Then
						MessageBox.Show(My.Resources.Resources.InvalidFileText)
					End If
				End If
			End If
		End If
	End Sub
	Private Sub reportViewer_LoadCompleted(sender As Object, e As EventArgs) Handles reportViewer.LoadCompleted
		If loaded Then
			Return
		End If
		reportViewer.GalleyMode = True
		loaded = True
	End Sub
End Class
