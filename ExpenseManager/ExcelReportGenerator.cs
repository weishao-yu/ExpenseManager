using OfficeOpenXml;
using OfficeOpenXml.Style;   // ★ 在 EPPlus 7 可以用 Style
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ExpenseManager
{
    public static class ExcelReportGenerator
    {
        public static void Export(string path, List<Record> records)
        {
            // ★ 非商用授權設定（EPPlus 7 正確用法）
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("記帳報表");

            int row = 2;

            // ======== 標題 ========
            ws.Cells[row, 1].Value = "📘 記帳統計報表";
            ws.Cells[row, 1].Style.Font.Size = 20;
            ws.Cells[row, 1].Style.Font.Bold = true;
            row += 2;

            // ======== 統計 ========
            double income = records.Where(r => r.Type == "收入").Sum(r => r.Amount);
            double expense = records.Where(r => r.Type == "支出").Sum(r => r.Amount);
            double net = income - expense;

            ws.Cells[row, 1].Value = $"總收入：NT${income:N0}";
            row++;

            ws.Cells[row, 1].Value = $"總支出：NT${expense:N0}";
            row++;

            ws.Cells[row, 1].Value = $"淨收益：NT${net:N0}";
            row += 2;

            // ======== 表格標題 ========
            ws.Cells[row, 1].Value = "日期";
            ws.Cells[row, 2].Value = "類型";
            ws.Cells[row, 3].Value = "分類";
            ws.Cells[row, 4].Value = "項目";
            ws.Cells[row, 5].Value = "金額";

            using (var range = ws.Cells[row, 1, row, 5])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            row++;

            // ======== 資料列 ========
            foreach (var r in records)
            {
                ws.Cells[row, 1].Value = r.Date;
                ws.Cells[row, 2].Value = r.Type;
                ws.Cells[row, 3].Value = r.Category;
                ws.Cells[row, 4].Value = r.Item;
                ws.Cells[row, 5].Value = r.Amount;

                // 加邊框
                for (int col = 1; col <= 5; col++)
                    ws.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                row++;
            }

            // 自動調欄寬
            ws.Cells[1, 1, row, 5].AutoFitColumns();

            package.SaveAs(new FileInfo(path));
        }
    }
}
