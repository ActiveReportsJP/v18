Imports System
Imports System.IO
Imports System.Windows.Forms

Partial Public Class CSVFileSelector
	Inherits UserControl

	Private _selectedFileName As String

	Public ReadOnly Property CSVFileName() As String
		Get
			Return _selectedFileName
		End Get
	End Property

	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub btnSelectFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSelectFile.Click
		Dim openFile As New OpenFileDialog()
		openFile.Filter = "CSVファイル(*.csv)|*.csv"
		openFile.Title = "CSVファイルの選択"

		Dim CombinedPath As String = Path.Combine(Directory.GetCurrentDirectory(), "..\..\")
		openFile.InitialDirectory = Path.GetFullPath(CombinedPath)

		If (openFile.ShowDialog()).Equals(DialogResult.OK) Then
			_selectedFileName = openFile.FileName
		End If
	End Sub

End Class
