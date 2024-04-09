Imports System.Text
Imports System.Xml
Imports GrapeCity.ActiveReports

'本サンプルは、新しい計算フィールドの作成および集計機能を紹介します。
Public Class StartForm
    Inherits System.Windows.Forms.Form

    Dim rpt As New SectionReport
    Public Sub New()
        MyBase.New()
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
        'Windowsフォームデザイナにこの呼び出しは必要です。

        'Windowsフォームデザイナにこの呼び出しは必要です。
        InitializeComponent()
        Me.comboBox1.Items.AddRange(New String() {My.Resources.OrdersReport, My.Resources.DataFieldExpressionsReport})

    End Sub

    'フォームがコンポーネントの一覧をクリーンアップするために Dispose をオーバーライドします。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles button1.Click
        Dim rpt As New SectionReport
        Dim reportPath As String = "..\\..\\..\\" + CType(comboBox1.SelectedItem, String)
        rpt.LoadLayout(XmlReader.Create(reportPath))
        rpt.PrintWidth = 6.5!
        arvMain.LoadDocument(rpt)
    End Sub

    Private Sub comboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBox1.SelectedIndexChanged
        If (Not button1.Enabled AndAlso Not IsNothing(comboBox1.SelectedItem)) Then
            button1.Enabled = True
        End If
    End Sub

    Private Sub comboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles comboBox1.KeyPress
        e.Handled = True
    End Sub
End Class
