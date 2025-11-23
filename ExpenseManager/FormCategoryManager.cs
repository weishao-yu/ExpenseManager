using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseManager
{
    public class FormCategoryManager : Form
    {
        // ===== 控制項欄位 =====
        private ComboBox cmbType;
        private ListBox lstCategories;
        private TextBox txtNewCategory;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClose;
        private Label lblType;
        private Label lblList;
        private Label lblNew;
        private Panel pnlDivider;

        public FormCategoryManager()
        {
            CategoryManager.Load(); // 確保有資料
            BuildUI();               // 動態建立畫面
            InitData();              // 載入分類
            LoadCategories();        // ✅ 立即顯示「現有分類」

        }

        // ===== 建立畫面 (取代 Designer) =====
        private void BuildUI()
        {
            // === 視窗設定 ===
            this.Text = "📂 分類管理";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(360, 480);
            this.Font = new Font("Microsoft JhengHei", 10);
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // === 標題 ===
            lblType = new Label()
            {
                Text = "分類類型：",
                Location = new Point(25, 25),
                AutoSize = true,
                Font = new Font("Microsoft JhengHei", 10, FontStyle.Bold)
            };

            cmbType = new ComboBox()
            {
                Location = new Point(140, 20), // ← 向右多移 15px
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Microsoft JhengHei", 10)
            };


            // === 分隔線 ===
            pnlDivider = new Panel()
            {
                Location = new Point(20, 60),
                Size = new Size(320, 2),
                BackColor = Color.LightGray
            };

            // === 現有分類 ===
            lblList = new Label()
            {
                Text = "現有分類：",
                Location = new Point(25, 75),
                AutoSize = true
            };

            lstCategories = new ListBox()
            {
                Location = new Point(25, 100),
                Size = new Size(300, 180),
                Font = new Font("Microsoft JhengHei", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // === 新增區 ===
            lblNew = new Label()
            {
                Text = "新增分類：",
                Location = new Point(25, 300),
                AutoSize = true
            };

            txtNewCategory = new TextBox()
            {
                Location = new Point(140, 296),
                Size = new Size(205, 28)
            };

            // === 按鈕列 ===
            btnAdd = new Button()
            {
                Text = "＋ 新增",
                Location = new Point(25, 340),
                Size = new Size(90, 36),
                BackColor = Color.LightSteelBlue,
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += btnAdd_Click;

            btnDelete = new Button()
            {
                Text = "🗑 刪除選擇",
                Location = new Point(130, 340),
                Size = new Size(120, 36),
                BackColor = Color.LightCoral,
                FlatStyle = FlatStyle.Flat
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += btnDelete_Click;

            btnClose = new Button()
            {
                Text = "✖ 關閉",
                Location = new Point(265, 340),
                Size = new Size(60, 36),
                BackColor = Color.Gainsboro,
                FlatStyle = FlatStyle.Flat
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += btnClose_Click;

            // === 加入控制項 ===
            this.Controls.AddRange(new Control[]
            {
                lblType, cmbType, pnlDivider,
                lblList, lstCategories,
                lblNew, txtNewCategory,
                btnAdd, btnDelete, btnClose
            });
        }

        // ===== 初始化資料 =====
        private void InitData()
        {
            cmbType.Items.Clear();
            cmbType.Items.AddRange(new string[] { "支出", "收入" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged; // ✅ 綁事件

        }

        private void LoadCategories()
        {
            lstCategories.Items.Clear();
            var type = cmbType.SelectedItem?.ToString() ?? "支出";

            if (CategoryManager.Categories.ContainsKey(type))
            {
                foreach (var c in CategoryManager.Categories[type])
                    lstCategories.Items.Add(c);
            }
        }

        // ===== 事件處理 =====
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var type = cmbType.SelectedItem?.ToString() ?? "支出";
            var newCat = (txtNewCategory.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(newCat))
            {
                MessageBox.Show("請輸入分類名稱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CategoryManager.Categories[type].Contains(newCat))
            {
                MessageBox.Show("此分類已存在！", "重複", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CategoryManager.Add(type, newCat);
            LoadCategories();
            txtNewCategory.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem == null)
            {
                MessageBox.Show("請先選擇要刪除的分類！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var type = cmbType.SelectedItem?.ToString() ?? "支出";
            var cat = lstCategories.SelectedItem.ToString();

            var confirm = MessageBox.Show($"確定要刪除「{cat}」嗎？",
                "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                CategoryManager.Remove(type, cat);
                LoadCategories();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
