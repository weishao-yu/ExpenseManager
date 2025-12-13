using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseManager
{
    public partial class FormAddRecord : Form
    {
        private string input = "";
        public Record NewRecord { get; private set; }
        private bool isEditing = false;

        public FormAddRecord()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            cmbType.Items.AddRange(new string[] { "支出", "收入" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;

            LoadCategories("支出");
            dtDate.Value = DateTime.Now;

            txtAmount.ReadOnly = true;
            txtAmount.Font = new Font("Consolas", 18, FontStyle.Bold);
            txtAmount.TextAlign = HorizontalAlignment.Right;
            txtAmount.BackColor = Color.White;

            txtNote.Font = new Font("Microsoft JhengHei", 10);
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategories(cmbType.SelectedItem.ToString());
        }

        private void LoadCategories(string type)
        {
            cmbCategory.Items.Clear();

            // 🧠 從 CategoryManager 讀取最新分類
            if (CategoryManager.Categories.ContainsKey(type))
            {
                cmbCategory.Items.AddRange(CategoryManager.Categories[type].ToArray());
            }

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            // ✅ 若是剛進入編輯模式，允許在舊值後繼續輸入
            if (isEditing)
            {
                input = txtAmount.Text;  // 保留舊數值
                isEditing = false;       // 僅第一次生效
            }

            input += btn.Text;
            txtAmount.Text = input;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (input.EndsWith("+") || input.EndsWith("-")) return;
            input += "+";
            txtAmount.Text = input;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (input.EndsWith("+") || input.EndsWith("-")) return;
            input += "-";
            txtAmount.Text = input;
        }
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                double result = EvaluateExpression(input);
                txtAmount.Text = result.ToString();
                input = result.ToString();   // ✅ 保留上次結果，允許繼續 +
            }
            catch
            {
                MessageBox.Show("輸入格式錯誤！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
                txtAmount.Text = input;
            }
        }
        public void LoadExistingRecord(Record rec)
        {
            if (rec == null) return;

            isEditing = true;  // ← 表示是從編輯模式開啟
            dtDate.Value = DateTime.Parse(rec.Date);
            cmbType.SelectedItem = rec.Type;
            cmbCategory.Text = rec.Category;
            txtAmount.Text = rec.Amount.ToString();
            input = rec.Amount.ToString();  // ✅ 同步目前輸入值
            txtNote.Text = rec.Item;
        }


        private double EvaluateExpression(string expr)
        {
            if (string.IsNullOrWhiteSpace(expr))
                throw new Exception("空白輸入");

            expr = expr.Replace(" ", "");
            double total = 0;
            double current = 0;
            char lastOp = '+';
            string num = "";

            for (int i = 0; i < expr.Length; i++)
            {
                char c = expr[i];

                if (char.IsDigit(c) || c == '.')
                {
                    num += c;
                }
                else if (c == '+' || c == '-')
                {
                    if (num != "")
                    {
                        current = double.Parse(num);
                        total = lastOp == '+' ? total + current : total - current;
                        num = "";
                    }
                    lastOp = c;
                }
                else
                {
                    throw new Exception("包含非法字元");
                }
            }

            if (num != "")
            {
                current = double.Parse(num);
                total = lastOp == '+' ? total + current : total - current;
            }

            return total;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 1. 檢查是否為空
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("請輸入金額！", "提示");
                return;
            }

            // ⭐ 修正邏輯：如果輸入框裡面包含 + 或 -，先幫他算出來！
            if (input.Contains("+") || input.Contains("-"))
            {
                try
                {
                    // 嘗試計算結果
                    double result = EvaluateExpression(input);
                    txtAmount.Text = result.ToString(); // 更新畫面
                    input = result.ToString();          // 更新變數
                }
                catch
                {
                    MessageBox.Show("算式無法計算，請檢查輸入！", "錯誤");
                    return;
                }
            }

            // 2. 現在 txtAmount.Text 肯定是純數字了，可以安心轉換
            if (!double.TryParse(txtAmount.Text, out double amount))
            {
                MessageBox.Show("金額格式錯誤！", "錯誤");
                return;
            }

            NewRecord = new Record(
                dtDate.Value.ToShortDateString(),
                txtNote.Text,
                amount,
                cmbCategory.Text,
                cmbType.Text
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
