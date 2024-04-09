Imports System.Collections.Specialized
Imports System.Globalization
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering
''' <summary>
''' Googleマップ(http://maps.google.com)からマップタイル画像を提供するサービスを表します。
''' </summary>
Public NotInheritable Class GoogleMapsTileProvider
    Inherits BaseTileProvider
    Implements IMapTileProvider

    ''' <summary>
    ''' プロバイダーの設定：
	''' ApiKey - APIにアクセス用のキー
    ''' Timeout- レスポンスタイムアウト
    ''' Style - Road/Aerial/Hybrid
    ''' Language - API language
    ''' </summary>
    Private _Settings As NameValueCollection
    Private Const UrlTemplate As String = "{3}://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size=256x256&sensor=false"

    Public Property Settings As NameValueCollection Implements IMapTileProvider.Settings
        Get
            Return _Settings
        End Get
        Private Set(ByVal value As NameValueCollection)
            _Settings = value
        End Set
    End Property

    Private ReadOnly Property IMapTileProvider_Copyright As String Implements IMapTileProvider.Copyright
        Get
            Return Copyright
        End Get
    End Property

    Public Sub New()
        Settings = New NameValueCollection()
        Settings.[Set]("Styles", String.Join(";", [Enum].GetNames(GetType(MapTypes))))
    End Sub

    Public Sub GetTile(ByVal key As MapTileKey, ByVal success As Action(Of IMapTile), ByVal [error] As Action(Of Exception)) Implements IMapTileProvider.GetTile
        Dim tilePosition = key.ToWorldPos()
        Dim parameters = GetParameters()

        Dim url = String.Format(CultureInfo.InvariantCulture.NumberFormat, UrlTemplate, tilePosition.Y, tilePosition.X, key.LevelOfDetail, If(parameters.UseSecureConnection, "https", "http"))

        If parameters.MapType.HasValue Then
            Dim maptype = [Enum].GetName(GetType(MapTypes), parameters.MapType)
            If maptype IsNot Nothing Then url += "&maptype=" & maptype.ToLower()
        End If

        If Not String.IsNullOrEmpty(parameters.Key) Then url += "&key=" & parameters.Key

        If Not String.IsNullOrEmpty(parameters.Language) Then url += "&language=" & parameters.Language

        WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, Sub(stream, contentType) success(New MapTile(key, New ImageInfo(stream, contentType))), [error])
    End Sub

    Private Function GetParameters() As Parameters
        Dim parameters = New Parameters With {
            .Key = Me.Settings("ApiKey"),
            .Timeout = If(Not String.IsNullOrEmpty(Me.Settings("Timeout")), Integer.Parse(Me.Settings("Timeout")), -1),
            .Language = Me.Settings("Language"),
            .UseSecureConnection = Not String.IsNullOrEmpty(Me.Settings("UseSecureConnection")) AndAlso Convert.ToBoolean(Me.Settings("UseSecureConnection"))
}

        Select Case Me.Settings("Style")
            Case "Road", "Roadmap"
                parameters.MapType = MapTypes.Roadmap
            Case "Aerial", "Satellite"
                parameters.MapType = MapTypes.Satellite
            Case "Hybrid"
                parameters.MapType = MapTypes.Hybrid
            Case "Terrain"
                parameters.MapType = MapTypes.Terrain
        End Select

        Return parameters
    End Function

    Friend Class Parameters
        Public Key As String
        Public MapType As MapTypes?
        Public Language As String
        Public UseSecureConnection As Boolean
        Public Timeout As Integer
    End Class

    '[DoNotObfuscateType]
    Friend Enum MapTypes
        Roadmap
        Satellite
        Hybrid
        Terrain
    End Enum
End Class
