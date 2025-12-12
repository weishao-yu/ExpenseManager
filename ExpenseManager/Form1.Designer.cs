using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseManager
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnLoad;
        private Button btnDelete;
        private Button btnStats;
        private Button btnCategory;
        private Button btnExportReport;
        private Button btnSearch;

        private DataGridView dgvList;
        private Label lblIncome;
        private Label lblExpense;
        private Label lblNet;
        private Chart chartOverview;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            btnAdd = new Button();
            btnEdit = new Button();
            btnLoad = new Button();
            btnDelete = new Button();
            btnStats = new Button();
            btnCategory = new Button();
            btnExportReport = new Button();
            btnSearch = new Button();

            dgvList = new DataGridView();
            lblIncome = new Label();
            lblExpense = new Label();
            lblNet = new Label();
            chartOverview = new Chart();

            ((System.ComponentModel.ISupportInitialize)dgvList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartOverview).BeginInit();
            SuspendLayout();
            // ===========================
            // 📄 匯出報表按鈕
            // ===========================
            btnExportReport.Text = "匯出報表";
            btnExportReport.Location = new Point(580, 20);
            btnExportReport.Size = new Size(100, 30);
            btnExportReport.Click += btnExportReport_Click;

            // ===========================
            // 🔘 功能按鈕區
            // ===========================
            btnAdd.Text = "新增";
            btnAdd.Location = new Point(20, 20);
            btnAdd.Size = new Size(80, 30);
            btnAdd.Click += btnAdd_Click;

            btnEdit.Text = "編輯";
            btnEdit.Location = new Point(110, 20);
            btnEdit.Size = new Size(80, 30);
            btnEdit.Click += btnEdit_Click;

            btnLoad.Text = "載入";
            btnLoad.Location = new Point(200, 20);
            btnLoad.Size = new Size(80, 30);
            btnLoad.Click += btnLoad_Click;

            btnDelete.Text = "刪除";
            btnDelete.Location = new Point(290, 20);
            btnDelete.Size = new Size(80, 30);
            btnDelete.Click += btnDelete_Click;

            btnStats.Text = "統計";
            btnStats.Location = new Point(380, 20);
            btnStats.Size = new Size(80, 30);
            btnStats.Click += btnStats_Click;

            btnCategory.Text = "分類管理";
            btnCategory.Location = new Point(470, 20);
            btnCategory.Size = new Size(100, 30);
            btnCategory.Click += btnCategory_Click;

            // === 搜尋按鈕 ===
            btnSearch.Text = "搜尋";
            btnSearch.Location = new Point(690, 20);
            btnSearch.Size = new Size(80, 30);
            btnSearch.Click += btnSearch_Click;
            // ===========================
            // 📊 圖表 Chart
            // ===========================
            ChartArea chartArea = new ChartArea("MainArea");
            chartOverview.ChartAreas.Add(chartArea);
            chartOverview.Location = new Point(20, 70);
            chartOverview.Size = new Size(600, 300);
            chartOverview.BackColor = Color.WhiteSmoke;

            // ===========================
            // 💬 標籤區（顯示收入/支出/淨收益）
            // ===========================
            lblIncome.Text = "總收入：0";
            lblIncome.Font = new Font("微軟正黑體", 11, FontStyle.Bold);
            lblIncome.Location = new Point(650, 90);
            lblIncome.AutoSize = true;

            lblExpense.Text = "總支出：0";
            lblExpense.Font = new Font("微軟正黑體", 11, FontStyle.Bold);
            lblExpense.Location = new Point(650, 130);
            lblExpense.AutoSize = true;

            lblNet.Text = "淨收益：0";
            lblNet.Font = new Font("微軟正黑體", 11, FontStyle.Bold);
            lblNet.Location = new Point(650, 170);
            lblNet.AutoSize = true;

            // ===========================
            // 📋 資料表 DataGridView
            // ===========================
            dgvList.Location = new Point(20, 400);
            dgvList.Size = new Size(950, 300);
            dgvList.AllowUserToAddRows = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.BackgroundColor = Color.White;

            // ===========================
            // 🪟 Form 視窗設定
            // ===========================
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1000, 750);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "💰 記帳系統";

            // === 加入控制項 ===
            Controls.AddRange(new Control[]
            {
                btnAdd, btnEdit, btnLoad, btnDelete, btnStats, btnCategory,
                chartOverview, lblIncome, lblExpense, lblNet, dgvList, btnExportReport, btnSearch
            });

            ((System.ComponentModel.ISupportInitialize)dgvList).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartOverview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
