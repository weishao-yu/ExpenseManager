using OfficeOpenXml;
using OfficeOpenXml.Style;    // ★ 在 EPPlus 7 可以用 Style
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ExpenseManager
{
    public static class ExcelReportGenerator
    {
        public static void Export(string path, List<Record> records)
        {
            // ★ 非商用授權設定（EPPlus 7 正確用法）
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 資料排序：防呆處理，避免壞掉的日期導致崩潰
            var sorted = records
                .OrderByDescending(r =>
                    DateTime.TryParse(r.Date, out var d) ? d : DateTime.MinValue)
                .ToList();

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("記帳報表");

            int row = 2;

            // ======== 1. 標題 ========
            ws.Cells[row, 1].Value = "📘 記帳統計報表";
            ws.Cells[row, 1].Style.Font.Size = 20;
            ws.Cells[row, 1].Style.Font.Bold = true;
            row += 2;

            // ======== 2. 統計資訊 ========
            double income = records.Where(r => r.Type == "收入").Sum(r => r.Amount);
            double expense = records.Where(r => r.Type == "支出").Sum(r => r.Amount);
            double net = income - expense;

            ws.Cells[row, 1].Value = $"總收入：NT${income:N0}";
            row++;

            ws.Cells[row, 1].Value = $"總支出：NT${expense:N0}";
            row++;

            ws.Cells[row, 1].Value = $"淨收益：NT${net:N0}";
            row += 2;

            // ======== 3. 表格欄位標題 ========
            ws.Cells[row, 1].Value = "日期";
            ws.Cells[row, 2].Value = "類型";
            ws.Cells[row, 3].Value = "分類";
            ws.Cells[row, 4].Value = "項目";
            ws.Cells[row, 5].Value = "金額";

            // 設定標題樣式
            using (var range = ws.Cells[row, 1, row, 5])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            row++;
            int dataStartRow = row; // 記住資料是從哪一行開始的

            // ======== 4. 填寫資料 (只填值，不畫框) ========
            foreach (var r in sorted)
            {
                ws.Cells[row, 1].Value = r.Date;
                ws.Cells[row, 2].Value = r.Type;
                ws.Cells[row, 3].Value = r.Category;
                ws.Cells[row, 4].Value = r.Item;
                ws.Cells[row, 5].Value = r.Amount; // 這裡存的是數字，方便 Excel 計算

                row++;
            }

            int dataEndRow = row - 1;

            // ======== 5. 批次樣式設定 (效能優化區) ========
            if (dataEndRow >= dataStartRow) // 確保有資料才畫
            {
                // A. 一次畫好所有資料的格線
                using (var dataRange = ws.Cells[dataStartRow, 1, dataEndRow, 5])
                {
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                // B. 設定金額欄位 (第 5 欄) 為千分位格式 (例如 1,000)
                using (var amountCol = ws.Cells[dataStartRow, 5, dataEndRow, 5])
                {
                    amountCol.Style.Numberformat.Format = "#,##0";
                }
            }

            // 自動調整欄寬
            ws.Cells[1, 1, row, 5].AutoFitColumns();

            // 存檔
            package.SaveAs(new FileInfo(path));
        }
    }
}