using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseManager
{
    public partial class Form1 : Form
    {
        private readonly List<Record> records = new();
        public Form1()
        {
            InitializeComponent();

            // DataGridView 基本保護
            dgvList.AllowUserToOrderColumns = true;
            dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvList.ReadOnly = true;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            InitOverviewChart();
            UpdateGrid();         // 空清單也能把圖表初始化好
        }

        // === 初始化收支圓餅圖 ===
        private void InitOverviewChart()
        {
            chartOverview.Series.Clear();
            chartOverview.ChartAreas.Clear();
            chartOverview.Titles.Clear();
            chartOverview.Legends.Clear();

            var area = new ChartArea("Main");
            area.BackColor = Color.WhiteSmoke;
            chartOverview.ChartAreas.Add(area);

            var legend = new Legend("說明");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.Font = new Font("Microsoft JhengHei", 9);
            legend.LegendStyle = LegendStyle.Row;
            chartOverview.Legends.Add(legend);

            chartOverview.BackColor = Color.WhiteSmoke;
        }
        private void btnCategory_Click(object sender, EventArgs e)
        {
            FormCategoryManager form = new FormCategoryManager();
            form.ShowDialog(this); // 開新視窗管理分類
        }

        // === 更新收支總覽圖表 ===
        private void UpdateOverviewChart()
        {
            chartOverview.Series.Clear();

            double income = records.Where(r => r.Type == "收入").Sum(r => r.Amount);
            double expense = records.Where(r => r.Type == "支出").Sum(r => r.Amount);
            double net = income - expense;

            var s = new Series("收支統計")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = false,
                Font = new Font("Microsoft JhengHei", 10, FontStyle.Bold),
                ChartArea = "Main"
            };

            s.Points.AddXY("收入", income);
            s.Points.AddXY("支出", expense);

            if (s.Points.Count >= 2)
            {
                s.Points[0].Color = Color.MediumSeaGreen; // 收入
                s.Points[1].Color = Color.OrangeRed;      // 支出
            }

            foreach (var p in s.Points)
            {
                p.Label = "#VALX：#PERCENT{P0}";
                p.LabelForeColor = Color.Black;
            }

            s["PieLabelStyle"] = "Disabled";


            chartOverview.Series.Add(s);

            // 更新三個標籤
            lblIncome.Text = $"總收入：{income:C0}";
            lblExpense.Text = $"總支出：{expense:C0}";
            lblNet.Text = $"總收益（收入－支出）：{net:C0}";
        }

        // === 更新清單與統計 ===
        private void UpdateGrid()
        {
            dgvList.DataSource = null;

            var sorted = records
                .OrderByDescending(r => DateTime.Parse(r.Date))
                .ToList();

            dgvList.DataSource = sorted;

            UpdateOverviewChart();
        }

        // === 新增紀錄（改用子視窗） ===
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new FormAddRecord();    // 你已經做好的 POS 風格輸入窗
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.NewRecord != null)
            {
                records.Add(dlg.NewRecord);
                UpdateGrid();
                AutoSave();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MessageBox.Show("請先選擇要編輯的紀錄！", "提示");
                return;
            }

            var row = dgvList.SelectedRows[0];
            if (row.DataBoundItem is Record oldRec)
            {
                using var dlg = new FormAddRecord();

                // ✅ 用方法設定舊資料
                dlg.LoadExistingRecord(oldRec);

                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.NewRecord != null)
                {
                    var idx = records.FindIndex(r =>
                        r.Date == oldRec.Date &&
                        r.Item == oldRec.Item &&
                        Math.Abs(r.Amount - oldRec.Amount) < 1e-9 &&
                        r.Category == oldRec.Category &&
                        r.Type == oldRec.Type);

                    if (idx >= 0)
                    {
                        records[idx] = dlg.NewRecord;
                        UpdateGrid();
                        AutoSave();
                    }
                }
            }
        }



        // === 載入 ===
        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "選擇要載入的記帳檔案";
                ofd.Filter = "CSV 檔案 (*.csv)|*.csv|所有檔案 (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var data = FileManager.Load(ofd.FileName);
                    if (data.Count == 0)
                    {
                        MessageBox.Show("載入失敗或檔案內容為空！", "警告");
                        return;
                    }

                    // ✅ 更新目前儲存路徑
                    savePath = ofd.FileName;

                    records.Clear();
                    records.AddRange(data);
                    UpdateGrid();

                    Console.WriteLine($"[Load] 已載入檔案：{savePath}");
                }
            }
        }

        // === 刪除（以 DataGridView 選取列為準） ===
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MessageBox.Show("請先選擇要刪除的紀錄！");
                return;
            }

            // 注意：因為 DataSource 綁的是 sorted 副本，所以用資料物件來刪
            var row = dgvList.SelectedRows[0];
            if (row.DataBoundItem is Record rec)
            {
                var idx = records.FindIndex(r =>
                    r.Date == rec.Date &&
                    r.Item == rec.Item &&
                    Math.Abs(r.Amount - rec.Amount) < 1e-9 &&
                    r.Category == rec.Category &&
                    r.Type == rec.Type);

                if (idx >= 0)
                {
                    records.RemoveAt(idx);
                    UpdateGrid();
                }
            }
            AutoSave();
        }
        private string savePath = "expense.csv"; // 預設儲存檔案

        // === 自動儲存 ===
        private void AutoSave()
        {
            try
            {
                if (string.IsNullOrEmpty(savePath))
                    savePath = "expense.csv"; // 若還沒設定，則使用預設檔

                FileManager.Save(records, savePath);
                Console.WriteLine($"[AutoSave] 已自動儲存至 {savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("自動儲存失敗：" + ex.Message, "錯誤");
            }
        }



        private void btnStats_Click(object sender, EventArgs e)
        {
            if (records.Count == 0)
            {
                MessageBox.Show("目前沒有任何紀錄！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnStats.Enabled = false;
            btnStats.Text = "📊 載入中...";

            // ✅ 指定完整命名空間
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 300;
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                btnStats.Text = "📊 統計";
                btnStats.Enabled = true;

                using var stats = new FormStats(records);
                stats.ShowDialog(this);
            };
            timer.Start();
        }


    }
}
