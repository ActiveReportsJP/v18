Imports GrapeCity.ActiveReports.Document.Section.Annotations
Imports System.IO
Imports System.Resources
Imports System.Text

Public Class AnnotationForm
    Private resource As New ResourceManager(Me.GetType)
    Private WithEvents _tsbAnnotation As New ToolStripButton(resource.GetString("CustomAnnotation"))

    Sub New()
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
        InitializeComponent()
    End Sub
    
    Private Sub AnnotationForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        ' 注釈用のカスタムボタンを追加します。
        Dim ts As ToolStrip = arvMain.Toolbar.ToolStrip
        ts.Items.Add(_tsbAnnotation)
        'レイアウトをロードし、レポートを実行します。
        arvMain.LoadDocument("..//..//..//Report//Annotation.rpx")
    End Sub

    Private Sub tsbExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _tsbAnnotation.Click
        '注釈の有無に応じて、確認メッセージを表示します。 
        If arvMain.Document.Pages(arvMain.ReportViewer.CurrentPage - 1).Annotations.Count > 0 Then
            MessageBox.Show(My.Resources.StampMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        ' 印鑑イメージをリソースから取得します。
        Dim thisExe As Reflection.Assembly
        thisExe = Reflection.Assembly.GetExecutingAssembly()
        Dim file As FileStream = New FileStream("..//..//Resources//stamp.png", FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim imgStamp As New Bitmap(file)

        ' 注釈を作成し、プロパティ値を割り当てます。
        Dim annoImg As New AnnotationImage()
        annoImg.BackgroundImage = imgStamp ' 画像
        annoImg.Color = Color.Transparent  ' 背景色
        annoImg.BackgroundLayout = GrapeCity.ActiveReports.Document.Section.Annotations.ImageLayout.Zoom  ' 表示形式
        annoImg.ShowBorder = False  '枠線表示（非表示）  
        ' 注釈を追加します。
        ' (追加位置の指定)
        annoImg.Attach(6.09F, 1.19F)
        arvMain.Document.Pages(arvMain.ReportViewer.CurrentPage - 1).Annotations.Add(annoImg)
        ' (サイズの設定)
        annoImg.Height = 0.7F
        annoImg.Width = 0.7F

        'Viewerを更新します。 
        arvMain.Refresh()
    End Sub

End Class
