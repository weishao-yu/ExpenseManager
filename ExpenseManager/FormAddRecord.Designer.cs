using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseManager
{
    partial class FormAddRecord
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dtDate = new DateTimePicker();
            cmbType = new ComboBox();
            cmbCategory = new ComboBox();
            txtAmount = new TextBox();
            txtNote = new TextBox();
            btnConfirm = new Button();
            btnCancel = new Button();

            SuspendLayout();

            // === 日期、類型、分類 ===
            dtDate.Location = new Point(30, 30);
            dtDate.Size = new Size(200, 30);

            cmbType.Location = new Point(30, 80);
            cmbType.Size = new Size(200, 31);

            cmbCategory.Location = new Point(30, 130);
            cmbCategory.Size = new Size(200, 31);

            // === 金額與備註 ===
            txtAmount.Location = new Point(30, 180);
            txtAmount.Size = new Size(246, 30);
            txtAmount.ReadOnly = true;
            txtAmount.TextAlign = HorizontalAlignment.Right;
            txtAmount.Font = new Font("Consolas", 18, FontStyle.Bold);

            txtNote.Location = new Point(30, 230);
            txtNote.Multiline = true;
            txtNote.Size = new Size(246, 60);
            txtNote.Font = new Font("Microsoft JhengHei", 10);

            // === 確認、取消 ===
            btnConfirm.Text = "確定";
            btnConfirm.Location = new Point(30, 320);
            btnConfirm.Size = new Size(100, 35);
            btnConfirm.Click += btnConfirm_Click;

            btnCancel.Text = "取消";
            btnCancel.Location = new Point(160, 320);
            btnCancel.Size = new Size(100, 35);
            btnCancel.Click += btnCancel_Click;

            // === 數字鍵盤 ===
            int startX = 320, startY = 30, w = 60, h = 40, gap = 10;

            // 數字鍵
            btn7 = CreateNumButton("7", startX, startY);
            btn8 = CreateNumButton("8", startX + (w + gap), startY);
            btn9 = CreateNumButton("9", startX + 2 * (w + gap), startY);

            btn4 = CreateNumButton("4", startX, startY + (h + gap));
            btn5 = CreateNumButton("5", startX + (w + gap), startY + (h + gap));
            btn6 = CreateNumButton("6", startX + 2 * (w + gap), startY + (h + gap));

            btn1 = CreateNumButton("1", startX, startY + 2 * (h + gap));
            btn2 = CreateNumButton("2", startX + (w + gap), startY + 2 * (h + gap));
            btn3 = CreateNumButton("3", startX + 2 * (w + gap), startY + 2 * (h + gap));

            btn0 = CreateNumButton("0", startX + (w + gap), startY + 3 * (h + gap));

            // 運算鍵 + - = ←
            btnAdd = CreateOpButton("+", startX + 3 * (w + gap), startY);
            btnMinus = CreateOpButton("-", startX + 3 * (w + gap), startY + (h + gap));
            btnEqual = CreateOpButton("=", startX + 3 * (w + gap), startY + 2 * (h + gap));
            btnBack = CreateOpButton("←", startX + 3 * (w + gap), startY + 3 * (h + gap));

            // === 視窗設定 ===
            ClientSize = new Size(620, 400);
            Controls.AddRange(new Control[]
            {
                dtDate, cmbType, cmbCategory, txtAmount, txtNote, btnConfirm, btnCancel
            });
            Text = "新增記帳紀錄";
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.WhiteSmoke;

            ResumeLayout(false);
            PerformLayout();
        }

        // === 數字鍵建立 ===
        private Button CreateNumButton(string text, int x, int y)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(60, 40),
                Location = new Point(x, y),
                Font = new Font("Consolas", 12),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderColor = Color.Gray;
            btn.Click += new EventHandler(this.NumberButton_Click);
            Controls.Add(btn);
            return btn;
        }

        // === 運算鍵建立 ===
        private Button CreateOpButton(string text, int x, int y)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(60, 40),
                Location = new Point(x, y),
                Font = new Font("Consolas", 12, FontStyle.Bold),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderColor = Color.DimGray;

            if (text == "+") btn.Click += new EventHandler(this.btnAdd_Click);
            else if (text == "-") btn.Click += new EventHandler(this.btnMinus_Click);
            else if (text == "=") btn.Click += new EventHandler(this.btnEqual_Click);
            else if (text == "←") btn.Click += new EventHandler(this.btnBack_Click);

            Controls.Add(btn);
            return btn;
        }

        #endregion

        private DateTimePicker dtDate;
        private ComboBox cmbType;
        private ComboBox cmbCategory;
        private TextBox txtAmount;
        private TextBox txtNote;
        private Button btnConfirm;
        private Button btnCancel;
        private Button btnAdd;
        private Button btnMinus;
        private Button btnEqual;
        private Button btnBack;
        private Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    }
}
