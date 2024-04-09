using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using GrapeCity.ActiveReports.Extensibility;
using ActiveReports.Samples.CustomResourceLocator.Properties;
using GrapeCity.ActiveReports;

namespace ActiveReports.Samples.CustomResourceLocator
{
	/// <summary>
	///  [ピクチャ]フォルダ内にリソースを検索すします。 <see cref="ResourceLocator"/> 
	/// </summary>
	internal sealed class MyPicturesLocator : ResourceLocator
	{
		private const string UriSchemeMyImages = "MyPictures:";
		/// <summary>
		/// リソースを取得し、返します。 
		/// </summary>
		/// <param name="resourceInfo">取得するリソースの情報を示します。 </param>
		/// <returns>リソースを検索できない場合は、Nullを返します。 </returns>
		public override Resource GetResource(ResourceInfo resourceInfo)
		{
			Resource resource;
			string name = resourceInfo.Name;
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(Resources.ResourceNameIsNull, "resourceInfo");
			}
			Uri uri = new Uri(name);
			if (uri.GetLeftPart(UriPartial.Scheme).StartsWith(UriSchemeMyImages, true, CultureInfo.InvariantCulture))
			{
				Stream stream = GetPictureFromSpecialFolder(uri);
				if (stream == null)
				{
					stream = new MemoryStream();
					Resources.NoImage.Save(stream, Resources.NoImage.RawFormat);
				}
				resource = new Resource(stream, uri);
			}
			else
			{
				throw new InvalidOperationException(Resources.ResourceSchemeIsNotSupported);
			}
			return resource;
		}

		/// <summary>
		/// [ピクチャ]フォルダから指定された画像を返します。 
		/// </summary>
		/// <param name="path">[ピクチャ]コードにある画像（myimages:logo.gif）のURI。</param>
		/// <returns>画像のデータを格納しているストリーム。画像を検索できないかハンドルできない場合は、Nullを返します。</returns>
		private static Stream GetPictureFromSpecialFolder(Uri path)
		{
			int startPathPos = UriSchemeMyImages.Length;
			if (startPathPos >= path.ToString().Length)
			{
				return null;
			}
			string pictureName = path.ToString().Substring(startPathPos);
			string myPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			if (!myPicturesPath.EndsWith("\\")) myPicturesPath += "\\";
			 string picturePath = Path.Combine(myPicturesPath, pictureName);
			if (!File.Exists(picturePath)) return null;
			MemoryStream stream = new MemoryStream();
			try
			{
				Image picture = Image.FromFile(picturePath);
				picture.Save(stream, picture.RawFormat);
				stream.Position = 0;
			}
			catch (OutOfMemoryException) // 有効な画像ファイルではないか、GDI+によりサポートされていない画像です。
			{
				return null;				
			}
			catch (ExternalException)
			{
				return null;
			}
			return stream;
		}
	}
}
