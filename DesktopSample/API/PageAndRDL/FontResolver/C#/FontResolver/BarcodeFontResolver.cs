using GrapeCity.ActiveReports;
using GrapeCity.Documents.Text;
using System;

namespace ActiveReports.Samples.FontResolver
{
	internal sealed class BarcodeFontResolver : IFontResolver
	{
		public static IFontResolver Instance = new BarcodeFontResolver();

		FontCollection _fonts;

		private BarcodeFontResolver()
		{
			_fonts = new FontCollection();
			// load Windows fonts
			_fonts.RegisterDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Fonts));
			// load barcode fonts
			_fonts.RegisterDirectory("../../../../../../Fonts");
			_fonts.DefaultFont = _fonts.FindFamilyName("Arial");
		}

		FontCollection IFontResolver.GetFonts(string familyName, bool isBold, bool isItalic)
		{
			var fonts = new FontCollection();
			fonts.Add(_fonts.FindFamilyName(familyName, isBold, isItalic) ?? _fonts.DefaultFont);
			return fonts;
		}
	}
}
