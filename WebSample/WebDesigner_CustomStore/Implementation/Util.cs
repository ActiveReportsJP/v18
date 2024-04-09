using System;
using System.IO;
using System.Drawing;
using GrapeCity.Documents.Imaging;
	
namespace WebDesignerCustomStore.Implementation
{
	public static class Util
	{
		/// <summary>
		/// Resizes the input image to 128x128 by default.
		/// Used to display a thumbnail in the image list. 
		/// </summary>
		/// <param name="image">Image represented as an array of bytes.</param>
		/// <param name="size">Size to resize. 128x128 by default.</param>
		/// <returns>The content of the thumbnail, represented as bytes.</returns>
		public static byte[] GetImageThumbnail(byte[] image, Size? thumbnailSize = null)
		{
			using var stream = new MemoryStream(image);
			using var original = new GcBitmap(stream);

			var size = thumbnailSize ?? new(128, 128);
			var thumbnail = original.Resize(size.Width, size.Height);
			using var thumbnailStream = new MemoryStream();

			thumbnail.SaveAsPng(thumbnailStream);
			return thumbnailStream.ToArray();
		}
	}
}
