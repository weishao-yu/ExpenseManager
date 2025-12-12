using System;
using System.Windows.Forms;

namespace ExpenseManager
{
    public partial class FormSearchFilter : Form
    {
        // 回傳給 Form1 的搜尋條件
        public SearchFilter Result { get; private set; }

        public FormSearchFilter()
        {
            InitializeComponent();

            // ===== 綁定類型切換事件 =====
            rbAll.CheckedChanged += TypeChanged;
            rbIncome.CheckedChanged += TypeChanged;
            rbExpense.CheckedChanged += TypeChanged;

            // ===== 初始化日期 =====
            dtFrom.Value = DateTime.Today.AddMonths(-1);
            dtTo.Value = DateTime.Today;

            // ===== 預設為「全部」 =====
            rbAll.Checked = true;
            UpdateCategoryByType();
        }

        // ============================
        // 類型切換 → 更新分類
        // ============================
        private void TypeChanged(object sender, EventArgs e)
        {
            UpdateCategoryByType();
        }

        private void UpdateCategoryByType()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add(""); // 空白 = 不限制

            if (rbIncome.Checked)
            {
                cmbCategory.Items.AddRange(
                    CategoryManager.Categories["收入"].ToArray()
                );
            }
            else if (rbExpense.Checked)
            {
                cmbCategory.Items.AddRange(
                    CategoryManager.Categories["支出"].ToArray()
                );
            }
            else // 全部
            {
                cmbCategory.Items.AddRange(
                    CategoryManager.Categories["收入"].ToArray()
                );
                cmbCategory.Items.AddRange(
                    CategoryManager.Categories["支出"].ToArray()
                );
            }

            cmbCategory.SelectedIndex = 0;
        }

        // ============================
        // 套用（確認搜尋）
        // ============================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string type = "";
            if (rbIncome.Checked) type = "收入";
            else if (rbExpense.Checked) type = "支出";

            Result = new SearchFilter
            {
                DateFrom = dtFrom.Value.Date,
                DateTo = dtTo.Value.Date,
                Category = cmbCategory.Text,
                Keyword = txtKeyword.Text.Trim(),
                MinAmount = double.TryParse(txtAmountMin.Text, out var min)
                            ? min
                            : null,
                MaxAmount = double.TryParse(txtAmountMax.Text, out var max)
                            ? max
                            : null,
                Type = type,
                IsReset = false
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        // ============================
        // 清空條件（Reset）
        // ============================
        private void btnReset_Click(object sender, EventArgs e)
        {
            // UI 重置
            rbAll.Checked = true;
            txtKeyword.Clear();
            txtAmountMin.Clear();
            txtAmountMax.Clear();

            dtFrom.Value = DateTime.Today.AddMonths(-1);
            dtTo.Value = DateTime.Today;

            UpdateCategoryByType();

            // 回傳 Reset 訊號
            Result = new SearchFilter
            {
                IsReset = true
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        // ============================
        // 取消
        // ============================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
