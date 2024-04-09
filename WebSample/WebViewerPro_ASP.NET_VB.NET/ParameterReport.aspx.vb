Imports System.Globalization

Public Class ParameterReport
    Inherits System.Web.UI.Page

    ' Parametrized Report Generation
    Dim _rpt As New SectionReport

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.MinValue)

        MyBase.OnInit(e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Load the report layout
        Dim xtr As New System.Xml.XmlTextReader(Server.MapPath("~") + "\RpxReports\Params.rpx")
        _rpt.LoadLayout(xtr)
        xtr.Close()
        ' Set parameter's value
        If Not String.IsNullOrEmpty(Request.QueryString("date")) Then
            Dim paramDate As DateTime = DateTime.ParseExact(Request.QueryString("date"), "MM-dd-yyyy", CultureInfo.InvariantCulture)
            Calendar1.SelectedDate = paramDate
            _rpt.Parameters(0).DefaultValue = paramDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        End If
        If Not IsPostBack Then

            ' The default is set to HTML viewer.
            Dim initDate As New DateTime(1994, 8, 2)
            Calendar1.VisibleDate = initDate

        End If
        _rpt.Restart()
        WebViewer.Report = _rpt
        Session("Report") = _rpt
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Redirect to the report page with parameter's value
        Response.Redirect("ParameterReport.aspx?date=" + Calendar1.SelectedDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture))
    End Sub
End Class