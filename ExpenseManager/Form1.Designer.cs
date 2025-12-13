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
            ChartArea chartArea1 = new ChartArea();
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
            btnAuth = new Button();
            btnBookManager = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartOverview).BeginInit();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(24, 23);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(98, 34);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "新增";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(134, 23);
            btnEdit.Margin = new Padding(4, 3, 4, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(98, 34);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "編輯";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(794, 236);
            btnLoad.Margin = new Padding(4, 3, 4, 3);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(98, 34);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "載入";
            btnLoad.Visible = false;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(240, 23);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 34);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "刪除";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnStats
            // 
            btnStats.Location = new Point(346, 23);
            btnStats.Margin = new Padding(4, 3, 4, 3);
            btnStats.Name = "btnStats";
            btnStats.Size = new Size(98, 34);
            btnStats.TabIndex = 4;
            btnStats.Text = "統計";
            btnStats.Click += btnStats_Click;
            // 
            // btnCategory
            // 
            btnCategory.Location = new Point(452, 23);
            btnCategory.Margin = new Padding(4, 3, 4, 3);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(122, 34);
            btnCategory.TabIndex = 5;
            btnCategory.Text = "分類管理";
            btnCategory.Click += btnCategory_Click;
            // 
            // btnExportReport
            // 
            btnExportReport.Location = new Point(582, 23);
            btnExportReport.Margin = new Padding(4, 3, 4, 3);
            btnExportReport.Name = "btnExportReport";
            btnExportReport.Size = new Size(122, 34);
            btnExportReport.TabIndex = 11;
            btnExportReport.Text = "匯出報表";
            btnExportReport.Click += btnExportReport_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(712, 23);
            btnSearch.Margin = new Padding(4, 3, 4, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(98, 34);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "搜尋";
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvList
            // 
            dgvList.AllowUserToAddRows = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.BackgroundColor = Color.White;
            dgvList.ColumnHeadersHeight = 34;
            dgvList.Location = new Point(24, 460);
            dgvList.Margin = new Padding(4, 3, 4, 3);
            dgvList.Name = "dgvList";
            dgvList.RowHeadersWidth = 62;
            dgvList.Size = new Size(1161, 345);
            dgvList.TabIndex = 10;
            // 
            // lblIncome
            // 
            lblIncome.AutoSize = true;
            lblIncome.Font = new Font("微軟正黑體", 11F, FontStyle.Bold);
            lblIncome.Location = new Point(794, 104);
            lblIncome.Margin = new Padding(4, 0, 4, 0);
            lblIncome.Name = "lblIncome";
            lblIncome.Size = new Size(113, 28);
            lblIncome.TabIndex = 7;
            lblIncome.Text = "總收入：0";
            // 
            // lblExpense
            // 
            lblExpense.AutoSize = true;
            lblExpense.Font = new Font("微軟正黑體", 11F, FontStyle.Bold);
            lblExpense.Location = new Point(794, 150);
            lblExpense.Margin = new Padding(4, 0, 4, 0);
            lblExpense.Name = "lblExpense";
            lblExpense.Size = new Size(113, 28);
            lblExpense.TabIndex = 8;
            lblExpense.Text = "總支出：0";
            // 
            // lblNet
            // 
            lblNet.AutoSize = true;
            lblNet.Font = new Font("微軟正黑體", 11F, FontStyle.Bold);
            lblNet.Location = new Point(794, 196);
            lblNet.Margin = new Padding(4, 0, 4, 0);
            lblNet.Name = "lblNet";
            lblNet.Size = new Size(113, 28);
            lblNet.TabIndex = 9;
            lblNet.Text = "淨收益：0";
            // 
            // chartOverview
            // 
            chartOverview.BackColor = Color.WhiteSmoke;
            chartArea1.Name = "MainArea";
            chartOverview.ChartAreas.Add(chartArea1);
            chartOverview.Location = new Point(24, 80);
            chartOverview.Margin = new Padding(4, 3, 4, 3);
            chartOverview.Name = "chartOverview";
            chartOverview.Size = new Size(733, 345);
            chartOverview.TabIndex = 6;
            // 
            // btnAuth
            // 
            btnAuth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAuth.Location = new Point(1087, 23);
            btnAuth.Name = "btnAuth";
            btnAuth.Size = new Size(98, 34);
            btnAuth.TabIndex = 13;
            btnAuth.Text = "登入";
            btnAuth.UseVisualStyleBackColor = false;
            btnAuth.Click += btnAuth_Click;
            // 
            // btnBookManager
            // 
            btnBookManager.Location = new Point(817, 23);
            btnBookManager.Name = "btnBookManager";
            btnBookManager.Size = new Size(98, 34);
            btnBookManager.TabIndex = 14;
            btnBookManager.Text = "帳本";
            btnBookManager.UseVisualStyleBackColor = false;
            btnBookManager.Click += btnBookManager_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1222, 862);
            Controls.Add(btnBookManager);
            Controls.Add(btnAuth);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnLoad);
            Controls.Add(btnDelete);
            Controls.Add(btnStats);
            Controls.Add(btnCategory);
            Controls.Add(chartOverview);
            Controls.Add(lblIncome);
            Controls.Add(lblExpense);
            Controls.Add(lblNet);
            Controls.Add(dgvList);
            Controls.Add(btnExportReport);
            Controls.Add(btnSearch);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "💰 記帳系統";
            ((System.ComponentModel.ISupportInitialize)dgvList).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartOverview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnAuth;
        private Button btnBookManager;
    }
}
