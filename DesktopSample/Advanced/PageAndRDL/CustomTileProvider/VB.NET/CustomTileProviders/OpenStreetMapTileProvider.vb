Imports System.Collections.Specialized
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering

''' <summary>
''' OpenStreetMap(http://www.openstreetmap.org)からマップタイル画像を提供するサービスを表します。
''' </summary>
Public NotInheritable Class OpenStreetMapTileProvider
    Inherits BaseTileProvider
    Implements IMapTileProvider

    Private Const UrlTemplate As String = "http://a.tile.openstreetmap.org/{0}/{1}/{2}.png"
    Private _settings As NameValueCollection

    ''' <summary>
	''' プロバイダーの設定：
    ''' Timeout- レスポンスタイムアウト
    Private ReadOnly Property Settings As NameValueCollection Implements IMapTileProvider.Settings
        Get
            Return _settings
        End Get
    End Property

    Private ReadOnly Property IMapTileProvider_Copyright As String Implements IMapTileProvider.Copyright
        Get
            Return Copyright
        End Get
    End Property

    Public Sub New()
        _settings = New NameValueCollection()
        _settings.[Set]("UseSecureConnection.IsVisible", "False")
        _settings.[Set]("Style.IsVisible", "False")
    End Sub

    Public Sub GetTile(ByVal key As MapTileKey, ByVal success As Action(Of IMapTile), ByVal [error] As Action(Of Exception)) Implements IMapTileProvider.GetTile
        Dim url = String.Format(UrlTemplate, key.LevelOfDetail, key.Col, key.Row)
        Dim timeout = If(Not String.IsNullOrEmpty(Settings("Timeout")), Integer.Parse(Settings("Timeout")), -1)
        Dim userAgent As String = $"ActiveReports.Core/{[GetType]().Assembly.GetName().Version}"

        WebRequestHelper.DownloadDataAsync(url, timeout, Sub(stream, contentType) success(New MapTile(key, New ImageInfo(stream, contentType))), [error], userAgent)
    End Sub

End Class

