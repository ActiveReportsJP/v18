Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering

	''' <summary>
	''' 単一のマップタイルを表します。
	''' </summary>
	Friend NotInheritable Class MapTile
		Implements IMapTile
		''' <summary>
		''' <see cref="MapTile"/>の新しいインスタンスを初期化します。
		''' </summary>
		Public Sub New(id As MapTileKey, imageStream As ImageInfo)
			_id = id
			_image = imageStream
		End Sub

		''' <summary>
		''' タイル識別子を取得します。
		''' </summary>
		''' 
		Public ReadOnly Property Id() As MapTileKey Implements IMapTile.Id
			Get
				Return _id
			End Get
		End Property
		Private _id As MapTileKey

		''' <summary>
		''' タイル画像のストリームを取得します。
		''' </summary>
		Public ReadOnly Property Image() As ImageInfo Implements IMapTile.Image
			Get
				Return _image
			End Get

		End Property
		Private _image As ImageInfo
	End Class
