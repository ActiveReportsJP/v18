Imports System.Data.SQLite
Imports System.Text
Imports System.Xml
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.SectionReportModel

Public Class ViewerForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		'必要なデザイナ変数です。
		InitializeComponent()

		' 
		'TODO：InitializeComponentの呼び出しの後に、それはコンストラクタです。コードを追加してください。
	End Sub

	' Windows フォーム デザイナ サポートに必要です。
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub ViewerForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		cboStyle.Items.Add(My.Resources.TwoDBarChart)
		cboStyle.Items.Add(My.Resources.ThreeDPieChart)
		cboStyle.Items.Add(My.Resources.ThreeDBarChart)
		cboStyle.Items.Add(My.Resources.FinanceChart)
		cboStyle.Items.Add(My.Resources.StackedAreaChart)

		' 「グラフの種類」コンボボックスの初期選択状態を設定します。
		cboStyle.SelectedIndex = 0
	End Sub

	Private Sub cboList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboStyle.SelectedIndexChanged
		' 「折れ線グラフ」選択時のみ、カスタムプロパティコンボボックスを有効にします。
		Call SetCustomProperties(Me.cboStyle.SelectedIndex)
	End Sub

	Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReport.Click

		Dim rpt As New SectionReport()
		Try
			' 「グラフの種類」コンボボックスにあわせて、プレビュー表示します。
			Select Case cboStyle.SelectedIndex
				Case 0  ' 2D棒グラフ
					rpt.LoadLayout(XmlReader.Create(My.Resources.rpt2DBar))
				Case 1  ' 3D円グラフ
					rpt.LoadLayout(XmlReader.Create(My.Resources.rpt3DPie))
					If cboCustom.SelectedIndex = 0 Then
						DirectCast(rpt.Sections("Detail").Controls("ChartSalesCategories"), ChartControl).Series(0).Properties("Clockwise") = True
					Else
						DirectCast(rpt.Sections("Detail").Controls("ChartSalesCategories"), ChartControl).Series(0).Properties("Clockwise") = False
					End If
				Case 2  '  3D棒グラフ
					rpt.AddAssembly(GetType(SQLiteConnection).Assembly)
					rpt.LoadLayout(XmlReader.Create(My.Resources.rpt3DBar))
				Case 3  '  ファイナンスチャート
					rpt.LoadLayout(XmlReader.Create(My.Resources.rptCandle))
				Case 4  '  積層エリアグラフ
					rpt.LoadLayout(XmlReader.Create(My.Resources.rptStackedArea))
			End Select
			arvMain.LoadDocument(DirectCast(rpt, SectionReport))
		Catch ex As Exception
			MessageBox.Show(ex.ToString)
		End Try

	End Sub

	Private Sub SetCustomProperties(ByVal iGraphStype As Integer)
		' 選択候補をクリアします。
		cboCustom.Items.Clear()

		' 選択候補を追加し、選択状態にします。
		Select Case iGraphStype
			Case 1    '  2D円グラフ
				' 
				'可視状態を変更します。
				cboCustom.Visible = True
				lblCustom.Visible = True

				cboCustom.Items.Add(My.Resources.Clockwise)
				cboCustom.Items.Add(My.Resources.Counterclockwise)

				cboCustom.SelectedIndex = 1

				'
				'ラベルを設定します。
				lblCustom.Text = My.Resources.DirectionOfRotation
			Case Else   '  その他
				'不可視状態にします。
				cboCustom.Visible = False
				lblCustom.Visible = False
		End Select

		Application.DoEvents()
	End Sub

End Class
