using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseManager
{
    public static class PdfReportGenerator
    {
        public static void Export(string path, List<Record> records)
        {
            var sorted = records
                .OrderByDescending(r =>
                    DateTime.TryParse(r.Date, out var d) ? d : DateTime.MinValue)
                .ToList();
            PdfDocument doc = new PdfDocument();
            doc.Info.Title = "記帳報表";

            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont titleFont = new XFont("NotoSansTC", 20, XFontStyle.Bold);
            XFont headerFont = new XFont("NotoSansTC", 12, XFontStyle.Bold);
            XFont cellFont = new XFont("NotoSansTC", 11, XFontStyle.Regular);

            int y = 40;

            // ======== 標題 ========
            gfx.DrawString("記帳統計報表", titleFont, XBrushes.Black,
                new XRect(0, y, page.Width, 30), XStringFormats.TopCenter);
            y += 50;

            // ======== 統計 ========
            double income = records.Where(r => r.Type == "收入").Sum(r => r.Amount);
            double expense = records.Where(r => r.Type == "支出").Sum(r => r.Amount);
            double net = income - expense;

            gfx.DrawString($"總收入：NT${income:N0}", cellFont, XBrushes.Black, 40, y); y += 20;
            gfx.DrawString($"總支出：NT${expense:N0}", cellFont, XBrushes.Black, 40, y); y += 20;
            gfx.DrawString($"淨收益：NT${net:N0}", cellFont, XBrushes.Black, 40, y); y += 30;

            // 分隔線
            gfx.DrawLine(XPens.Gray, 40, y, page.Width - 40, y);
            y += 20;

            DrawTableHeader(gfx, page, ref y, headerFont);

            // ======== 表格內容 ========
            foreach (var r in sorted)
            {
                DrawRow(gfx, page, ref y, cellFont,
                    r.Date,
                    r.Type,
                    r.Category,
                    r.Item,
                    $"NT${r.Amount:N0}"
                );

                // 換頁
                if (y > page.Height - 50)
                {
                    page = doc.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 40;

                    DrawTableHeader(gfx, page, ref y, headerFont);
                }
            }

            doc.Save(path);
        }

        // ================================
        //       畫表頭（灰底＋粗體）
        // ================================
        private static void DrawTableHeader(XGraphics gfx, PdfPage page, ref int y, XFont font)
        {
            int x = 40;
            int height = 25;

            // 灰色背景
            gfx.DrawRectangle(XBrushes.LightGray, x, y, page.Width - 115, height);

            DrawCell(gfx, x, y, 90, height, "日期", font);
            DrawCell(gfx, x + 90, y, 50, height, "類型", font);
            DrawCell(gfx, x + 140, y, 80, height, "分類", font);
            DrawCell(gfx, x + 220, y, 140, height, "項目", font);
            DrawCell(gfx, x + 360, y, 120, height, "金額", font);

            y += height;
        }

        // ================================
        //          畫內容列
        // ================================
        private static void DrawRow(XGraphics gfx, PdfPage page, ref int y, XFont font,
            string date, string type, string category, string item, string amount)
        {
            int x = 40;
            int height = 22;

            DrawCell(gfx, x, y, 90, height, date, font);
            DrawCell(gfx, x + 90, y, 50, height, type, font);
            DrawCell(gfx, x + 140, y, 80, height, category, font);
            DrawCell(gfx, x + 220, y, 140, height, item, font);
            DrawCell(gfx, x + 360, y, 120, height, amount, font);

            y += height;
        }

        // ================================
        //          畫一格（含框線）
        // ================================
        private static void DrawCell(XGraphics gfx, int x, int y, int width, int height,
            string text, XFont font, bool rightAlign = false)
        {
            gfx.DrawRectangle(XPens.Black, x, y, width, height);

            XStringFormat format = rightAlign ?
                XStringFormats.CenterRight :
                XStringFormats.CenterLeft;

            gfx.DrawString(text, font, XBrushes.Black,
                new XRect(x + 5, y + 5, width - 10, height), format);
        }
    }
}
