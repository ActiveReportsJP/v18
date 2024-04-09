Imports System.ComponentModel
Imports System.Windows.Forms
Partial Public Class BaseStep
    Inherits UserControl
    Private firstDisplay As Boolean
    Private stepTitle As String
    Private stepDescription As String
    Public Sub New()
        firstDisplay = True
        stepTitle = ""
        stepDescription = ""

        InitializeComponent()
    End Sub
    Private wizState As ReportWizardState
    <Browsable(False)>
    Public Property ReportWizardState() As ReportWizardState
        Get
            Return wizState
        End Get
        Set(ByVal value As ReportWizardState)
            wizState = value
        End Set
    End Property

    <Browsable(True)>
    <Description("表示するウィザードのタイトル")>
    <Category("外観")>
    <DefaultValue("")>
    Public Property Title() As String

        Get
            Return stepTitle
        End Get
        Set(ByVal value As String)
            stepTitle = value
        End Set
    End Property

    <Browsable(True)>
    <Description("ステップの実装方法を説明するテキスト")>
    <Category("外観")>
    <DefaultValue("")>
    Public Property Description() As String
        Get
            Return stepDescription
        End Get
        Set(ByVal value As String)
            stepDescription = value
        End Set
    End Property
    Public Sub OnDisplay()
        OnDisplay(Me.firstDisplay)
        firstDisplay = False
    End Sub
    Protected Overridable Sub OnDisplay(ByVal firstDisplay As Boolean)
    End Sub
    'ウィザードは前または次のステップへ移動する前に呼び出します。
    Public Overridable Sub UpdateState()
    End Sub
End Class
