using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

namespace ExpenseManager
{
    public class EmbeddedFontResolver : IFontResolver
    {
        private const string RegularFontName = "NotoSansTC-Regular";
        private const string BoldFontName = "NotoSansTC-Bold";

        public byte[] GetFont(string faceName)
        {
            string resourceName = faceName switch
            {
                RegularFontName => "ExpenseManager.NotoSansTC-Regular.ttf",
                BoldFontName => "ExpenseManager.NotoSansTC-Bold.ttf",
                _ => null
            };

            if (resourceName == null)
                return null;

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null) throw new Exception($"無法讀取字型：{resourceName}");

            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("NotoSansTC", StringComparison.OrdinalIgnoreCase))
            {
                if (isBold) return new FontResolverInfo(BoldFontName);
                return new FontResolverInfo(RegularFontName);
            }

            return null;
        }
    }
}
