Public MustInherit Class BaseTileProvider
    Private Function GetCopyright() As String
        Dim titleNameKey = [GetType]().Name & "_TileProviderCopyright"
        Dim title = ActiveReports.Samples.CustomTileProviders.My.Resources.Resources.ResourceManager.GetString(titleNameKey)

        If Not String.IsNullOrEmpty(title) AndAlso title <> titleNameKey Then
            Return String.Format(title, DateTime.Now.Year)
        End If

        Return String.Empty
    End Function

    Public Overridable ReadOnly Property Copyright As String
        Get
            Return GetCopyright()
        End Get
    End Property
End Class
