namespace ExpenseManager
{
    partial class FormSearchFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblFrom = new Label();
            lblTo = new Label();
            lblType = new Label();
            lblCategory = new Label();
            lblKeyword = new Label();
            lblAmount = new Label();
            lblWave = new Label();
            dtTo = new DateTimePicker();
            dtFrom = new DateTimePicker();
            rbAll = new RadioButton();
            rbIncome = new RadioButton();
            rbExpense = new RadioButton();
            cmbCategory = new ComboBox();
            txtAmountMin = new TextBox();
            txtAmountMax = new TextBox();
            btnSearch = new Button();
            btnCancel = new Button();
            btnReset = new Button();
            txtKeyword = new TextBox();
            SuspendLayout();
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(30, 30);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(68, 23);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "日期從:";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(30, 70);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(32, 23);
            lblTo.TabIndex = 1;
            lblTo.Text = "到:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(30, 110);
            lblType.Name = "lblType";
            lblType.Size = new Size(50, 23);
            lblType.TabIndex = 2;
            lblType.Text = "類型:";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(30, 150);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(50, 23);
            lblCategory.TabIndex = 3;
            lblCategory.Text = "分類:";
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(30, 190);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(68, 23);
            lblKeyword.TabIndex = 4;
            lblKeyword.Text = "關鍵字:";
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(30, 230);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(68, 23);
            lblAmount.TabIndex = 5;
            lblAmount.Text = "金額從:";
            // 
            // lblWave
            // 
            lblWave.AutoSize = true;
            lblWave.Location = new Point(220, 230);
            lblWave.Name = "lblWave";
            lblWave.Size = new Size(23, 23);
            lblWave.TabIndex = 6;
            lblWave.Text = "~";
            // 
            // dtTo
            // 
            dtTo.Location = new Point(130, 66);
            dtTo.Name = "dtTo";
            dtTo.Size = new Size(300, 30);
            dtTo.TabIndex = 7;
            // 
            // dtFrom
            // 
            dtFrom.Location = new Point(130, 26);
            dtFrom.Name = "dtFrom";
            dtFrom.Size = new Size(300, 30);
            dtFrom.TabIndex = 8;
            // 
            // rbAll
            // 
            rbAll.AutoSize = true;
            rbAll.Checked = true;
            rbAll.Location = new Point(130, 110);
            rbAll.Name = "rbAll";
            rbAll.Size = new Size(71, 27);
            rbAll.TabIndex = 9;
            rbAll.TabStop = true;
            rbAll.Text = "全部";
            rbAll.UseVisualStyleBackColor = true;
            // 
            // rbIncome
            // 
            rbIncome.AutoSize = true;
            rbIncome.Location = new Point(200, 110);
            rbIncome.Name = "rbIncome";
            rbIncome.Size = new Size(71, 27);
            rbIncome.TabIndex = 10;
            rbIncome.Text = "收入";
            rbIncome.UseVisualStyleBackColor = true;
            // 
            // rbExpense
            // 
            rbExpense.AutoSize = true;
            rbExpense.Location = new Point(270, 110);
            rbExpense.Name = "rbExpense";
            rbExpense.Size = new Size(71, 27);
            rbExpense.TabIndex = 11;
            rbExpense.Text = "支出";
            rbExpense.UseVisualStyleBackColor = true;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(130, 146);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(240, 31);
            cmbCategory.TabIndex = 12;
            // 
            // txtAmountMin
            // 
            txtAmountMin.Location = new Point(130, 226);
            txtAmountMin.Name = "txtAmountMin";
            txtAmountMin.Size = new Size(80, 30);
            txtAmountMin.TabIndex = 14;
            // 
            // txtAmountMax
            // 
            txtAmountMax.Location = new Point(250, 226);
            txtAmountMax.Name = "txtAmountMax";
            txtAmountMax.Size = new Size(80, 30);
            txtAmountMax.TabIndex = 15;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(260, 290);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 32);
            btnSearch.TabIndex = 16;
            btnSearch.Text = "確定";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(350, 290);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 32);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(440, 290);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(80, 32);
            btnReset.TabIndex = 18;
            btnReset.Text = "重置";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(130, 186);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(240, 30);
            txtKeyword.TabIndex = 19;
            // 
            // FormSearchFilter
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(558, 324);
            Controls.Add(txtKeyword);
            Controls.Add(btnReset);
            Controls.Add(btnCancel);
            Controls.Add(btnSearch);
            Controls.Add(txtAmountMax);
            Controls.Add(txtAmountMin);
            Controls.Add(cmbCategory);
            Controls.Add(rbExpense);
            Controls.Add(rbIncome);
            Controls.Add(rbAll);
            Controls.Add(dtFrom);
            Controls.Add(dtTo);
            Controls.Add(lblWave);
            Controls.Add(lblAmount);
            Controls.Add(lblKeyword);
            Controls.Add(lblCategory);
            Controls.Add(lblType);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            Name = "FormSearchFilter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormSearchFilter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFrom;
        private Label lblTo;
        private Label lblType;
        private Label lblCategory;
        private Label lblKeyword;
        private Label lblAmount;
        private Label lblWave;
        private DateTimePicker dtTo;
        private DateTimePicker dtFrom;
        private RadioButton rbAll;
        private RadioButton rbIncome;
        private RadioButton rbExpense;
        private ComboBox cmbCategory;
        private TextBox txtAmountMin;
        private TextBox txtAmountMax;
        private Button btnSearch;
        private Button btnCancel;
        private Button btnReset;
        private TextBox txtKeyword;
    }
}