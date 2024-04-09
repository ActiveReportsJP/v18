Imports System.Collections.Specialized
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering
''' <summary>
''' http://cloudmade.com/ からマップタイル画像を提供するサービスを表します。 
''' </summary>

Public NotInheritable Class CloudMadeTileProvider
	Inherits BaseTileProvider
	Implements IMapTileProvider
	Private Const UrlTemplate As String = "http://a.tile.cloudmade.com/{0}/{1}/256/{2}/{3}/{4}.png"

	''' <summary>
	''' プロバイダーの設定：
	''' ApiKey - APIにアクセス用のキー
	''' ColorStyle - （http://maps.cloudmade.com/editor）からのスタイル番号
	''' Timeout- レスポンスタイムアウト
	''' </summary>
	Public ReadOnly Property Settings() As NameValueCollection Implements IMapTileProvider.Settings
		Get
			Return _settings
		End Get

	End Property
	Private _settings As NameValueCollection

	Public Sub New()
		_settings = New NameValueCollection()
		_settings.Set("UseSecureConnection.IsVisible", "False")
		_settings.Set("Style.IsVisible", "False")
	End Sub

	Public Sub GetTile(key As MapTileKey, success As Action(Of IMapTile), [error] As Action(Of Exception)) Implements IMapTileProvider.GetTile
		Dim parameters = GetParameters()
		Dim url = String.Format(UrlTemplate, parameters.Key, parameters.ColorStyle, key.LevelOfDetail, key.Col, key.Row)
		WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, Sub(stream, contentType) success(New MapTile(key, New ImageInfo(stream, contentType))), [error])
	End Sub

#Region "Parameters"

	Dim params As Parameters
	Private Function GetParameters() As Parameters
		params = New Parameters() With {
			.Key = If(Settings("ApiKey"), "8ee2a50541944fb9bcedded5165f09d9"),
			.ColorStyle = If(Not String.IsNullOrEmpty(Settings("ColorStyle")), Integer.Parse(Settings("ColorStyle")), 1),
			.Timeout = If(Not String.IsNullOrEmpty(Settings("Timeout")), Integer.Parse(Settings("Timeout")), -1)
		}

		Return params
	End Function

	Private Class Parameters
		Public Key As String
		Public ColorStyle As Integer
		Public Timeout As Integer
	End Class

#End Region

	Private ReadOnly Property IMapTileProvider_Copyright As String Implements IMapTileProvider.Copyright
		Get
			Return Copyright
		End Get
	End Property
End Class
