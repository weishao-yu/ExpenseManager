using System;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Fonts;

namespace ExpenseManager
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            // 🟢 修正 PDFsharp 編碼錯誤（No data is available for encoding 1252）
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 使用嵌入字型
            GlobalFontSettings.FontResolver = new EmbeddedFontResolver();

            Db.Init();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
