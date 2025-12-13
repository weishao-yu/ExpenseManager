using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseManager
{
    public class FormStats : Form
    {
        private List<Record> records;
        private Chart chartTrend;
        private Label lblSummary;
        private Label lblExpenseTitle;
        private Label lblIncomeTitle;
        private Panel pnlExpenseList;
        private Panel pnlIncomeList;
        private ComboBox cmbYear;   // 年份選擇

        public FormStats(List<Record> records)
        {
            this.records = records;
            BuildUI();
            InitYearSelector();
        }

        private void BuildUI()
        {
            Text = "📊 收支統計分析";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1000, 780);   // ↑ 視窗加高一點
            BackColor = Color.WhiteSmoke;
            Font = new Font("Microsoft JhengHei", 10);

            // === 年份選擇 ===
            Label lblYear = new Label()
            {
                Text = "📅 年份：",
                Font = new Font("Microsoft JhengHei", 10, FontStyle.Bold),
                Location = new Point(1, 20),
                AutoSize = true
            };

            cmbYear = new ComboBox()
            {
                Location = new Point(100, 16),
                Size = new Size(120, 28),
                Font = new Font("Microsoft JhengHei", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbYear.SelectedIndexChanged += (s, e) => LoadData();

            // === Summary 總覽列（移到年份下方） ===
            lblSummary = new Label()
            {
                Text = "統計資料載入中...",
                Font = new Font("Microsoft JhengHei", 11, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 55),
                Size = new Size(1000, 45),
                BackColor = Color.FromArgb(245, 245, 245)
            };

            // === 折線圖（下移避免擋住） ===
            chartTrend = new Chart()
            {
                Location = new Point(30, 110),
                Size = new Size(940, 250),
                BackColor = Color.White
            };
            var areaTrend = new ChartArea("Trend");
            areaTrend.AxisX.Title = "月份";
            areaTrend.AxisY.Title = "金額 (元)";
            areaTrend.AxisX.Interval = 1;
            areaTrend.AxisX.Minimum = 1;
            areaTrend.AxisX.Maximum = 12;
            areaTrend.AxisX.LabelStyle.Font = new Font("Microsoft JhengHei", 9);
            areaTrend.AxisY.LabelStyle.Font = new Font("Microsoft JhengHei", 9);
            chartTrend.ChartAreas.Add(areaTrend);
            chartTrend.Titles.Add("📈 每月收入 / 支出變化");
            chartTrend.Titles[0].Font = new Font("Microsoft JhengHei", 12, FontStyle.Bold);

            // === 支出 / 收入區塊 ===
            lblExpenseTitle = new Label()
            {
                Text = "💸 支出分類比例",
                Font = new Font("Microsoft JhengHei", 11, FontStyle.Bold),
                Location = new Point(60, 380),
                AutoSize = true
            };
            pnlExpenseList = new Panel()
            {
                Location = new Point(60, 410),
                Size = new Size(400, 320),
                BackColor = Color.White,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblIncomeTitle = new Label()
            {
                Text = "💰 收入分類比例",
                Font = new Font("Microsoft JhengHei", 11, FontStyle.Bold),
                Location = new Point(540, 380),
                AutoSize = true
            };
            pnlIncomeList = new Panel()
            {
                Location = new Point(540, 410),
                Size = new Size(400, 320),
                BackColor = Color.White,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.AddRange(new Control[]
            {
        lblYear, cmbYear,
        lblSummary, chartTrend,
        lblExpenseTitle, pnlExpenseList,
        lblIncomeTitle, pnlIncomeList
            });
        }


        private void InitYearSelector()
        {
            // 取得所有年份 (先過濾掉日期格式錯誤的，再轉年份)
            var years = records
                .Where(r => DateTime.TryParse(r.Date, out _)) // ✅ 只留日期合法的
                .Select(r => DateTime.Parse(r.Date).Year)     // ✅ 這裡就安全了
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            if (years.Count == 0)
            {
                MessageBox.Show("沒有任何可用的統計資料。");
                // 建議這裡不要 return，不然下面的 cmbYear 沒東西，LoadData 可能會錯
                // 可以加個 currentYear 做預設
            }

            cmbYear.Items.Clear();
            foreach (var y in years)
                cmbYear.Items.Add($"{y} 年");

            cmbYear.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            if (cmbYear.SelectedItem == null) return;

            int selectedYear = int.Parse(cmbYear.SelectedItem.ToString().Replace(" 年", ""));
            var yearRecords = records.Where(r => DateTime.Parse(r.Date).Year == selectedYear).ToList();

            if (yearRecords.Count == 0)
            {
                lblSummary.Text = $"{selectedYear} 年無任何收支資料。";
                pnlExpenseList.Controls.Clear();
                pnlIncomeList.Controls.Clear();
                chartTrend.Series.Clear();
                return;
            }

            // === 基本統計 ===
            double income = yearRecords.Where(r => r.Type == "收入").Sum(r => r.Amount);
            double expense = yearRecords.Where(r => r.Type == "支出").Sum(r => r.Amount);
            double net = income - expense;

            lblSummary.Text = $"📅 {selectedYear} 年　💵 總收入：{income:C0}　💸 總支出：{expense:C0}　📈 淨收益：{net:C0}";
            lblSummary.ForeColor = net >= 0 ? Color.SeaGreen : Color.IndianRed;

            // === 折線圖（固定顯示 12 個月） ===
            chartTrend.Series.Clear();
            var incomeSeries = new Series("收入")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.SeaGreen,
                MarkerStyle = MarkerStyle.Circle
            };
            var expenseSeries = new Series("支出")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.OrangeRed,
                MarkerStyle = MarkerStyle.Diamond
            };

            // 初始化每月 1~12
            double[] monthlyIncome = new double[12];
            double[] monthlyExpense = new double[12];

            foreach (var r in yearRecords)
            {
                if (!DateTime.TryParse(r.Date, out DateTime d)) continue; // 跳過錯誤日期

                int month = d.Month - 1;

                // 雙重保險：確保 month 在 0~11 之間 (雖然 Month 屬性通常安全，但多一層保險無害)
                if (month < 0 || month > 11) continue;

                if (r.Type == "收入") monthlyIncome[month] += r.Amount;
                else if (r.Type == "支出") monthlyExpense[month] += r.Amount;
            }

            for (int m = 1; m <= 12; m++)
            {
                incomeSeries.Points.AddXY(m, monthlyIncome[m - 1]);
                expenseSeries.Points.AddXY(m, monthlyExpense[m - 1]);
            }

            chartTrend.Series.Add(incomeSeries);
            chartTrend.Series.Add(expenseSeries);

            // === 支出分類比例 ===
            pnlExpenseList.Controls.Clear();
            var expenseGroups = yearRecords
                .Where(r => r.Type == "支出")
                .GroupBy(r => r.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(x => x.Amount),
                    Percent = (expense == 0 ? 0 : g.Sum(x => x.Amount) / expense * 100)
                })
                .OrderByDescending(g => g.Total);

            int y = 10;
            foreach (var e in expenseGroups)
            {
                var lbl = new Label()
                {
                    Text = $"{e.Category,-6}：{e.Percent,5:F1}%　({e.Total,8:C0})",
                    Font = new Font("Microsoft JhengHei", 10),
                    ForeColor = Color.FromArgb(200, 60, 60),
                    AutoSize = true,
                    Location = new Point(10, y)
                };
                pnlExpenseList.Controls.Add(lbl);
                y += 28;
            }

            // === 收入分類比例 ===
            pnlIncomeList.Controls.Clear();
            var incomeGroups = yearRecords
                .Where(r => r.Type == "收入")
                .GroupBy(r => r.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(x => x.Amount),
                    Percent = (income == 0 ? 0 : g.Sum(x => x.Amount) / income * 100)
                })
                .OrderByDescending(g => g.Total);

            y = 10;
            foreach (var i in incomeGroups)
            {
                var lbl = new Label()
                {
                    Text = $"{i.Category,-6}：{i.Percent,5:F1}%　({i.Total,8:C0})",
                    Font = new Font("Microsoft JhengHei", 10),
                    ForeColor = Color.FromArgb(40, 120, 40),
                    AutoSize = true,
                    Location = new Point(10, y)
                };
                pnlIncomeList.Controls.Add(lbl);
                y += 28;
            }
        }
    }
}
