namespace ExpenseManager
{
    partial class FormBookManager
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
            btnDeleteBook = new Button();
            btnClose = new Button();
            btnImportCsv = new Button();
            btnRenameBook = new Button();
            btnAddBook = new Button();
            cboBooks = new ComboBox();
            btnExportThisBook = new Button();
            btnExportAllBooks = new Button();
            SuspendLayout();
            // 
            // btnDeleteBook
            // 
            btnDeleteBook.Location = new Point(124, 75);
            btnDeleteBook.Name = "btnDeleteBook";
            btnDeleteBook.Size = new Size(80, 30);
            btnDeleteBook.TabIndex = 0;
            btnDeleteBook.Text = "刪除";
            btnDeleteBook.UseVisualStyleBackColor = true;
            btnDeleteBook.Click += btnDeleteBook_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(300, 202);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 30);
            btnClose.TabIndex = 1;
            btnClose.Text = "關閉";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnImportCsv
            // 
            btnImportCsv.Location = new Point(24, 138);
            btnImportCsv.Name = "btnImportCsv";
            btnImportCsv.Size = new Size(80, 30);
            btnImportCsv.TabIndex = 2;
            btnImportCsv.Text = "匯入";
            btnImportCsv.UseVisualStyleBackColor = true;
            btnImportCsv.Click += btnImportCsv_Click;
            // 
            // btnRenameBook
            // 
            btnRenameBook.Location = new Point(24, 75);
            btnRenameBook.Name = "btnRenameBook";
            btnRenameBook.Size = new Size(80, 30);
            btnRenameBook.TabIndex = 3;
            btnRenameBook.Text = "更名";
            btnRenameBook.UseVisualStyleBackColor = true;
            btnRenameBook.Click += btnRenameBook_Click;
            // 
            // btnAddBook
            // 
            btnAddBook.Location = new Point(124, 138);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.Size = new Size(80, 30);
            btnAddBook.TabIndex = 4;
            btnAddBook.Text = "新增";
            btnAddBook.UseVisualStyleBackColor = true;
            btnAddBook.Click += btnAddBook_Click;
            // 
            // cboBooks
            // 
            cboBooks.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBooks.FormattingEnabled = true;
            cboBooks.Location = new Point(20, 20);
            cboBooks.Name = "cboBooks";
            cboBooks.Size = new Size(260, 31);
            cboBooks.TabIndex = 5;
            // 
            // btnExportThisBook
            // 
            btnExportThisBook.Location = new Point(229, 73);
            btnExportThisBook.Name = "btnExportThisBook";
            btnExportThisBook.Size = new Size(136, 34);
            btnExportThisBook.TabIndex = 6;
            btnExportThisBook.Text = "匯出單一CSV";
            btnExportThisBook.UseVisualStyleBackColor = true;
            btnExportThisBook.Click += btnExportThisBook_Click;
            // 
            // btnExportAllBooks
            // 
            btnExportAllBooks.Location = new Point(229, 134);
            btnExportAllBooks.Name = "btnExportAllBooks";
            btnExportAllBooks.Size = new Size(136, 34);
            btnExportAllBooks.TabIndex = 7;
            btnExportAllBooks.Text = "匯出全部帳本 (CSV)";
            btnExportAllBooks.UseVisualStyleBackColor = true;
            btnExportAllBooks.Click += btnExportAllBooks_Click;
            // 
            // FormBookManager
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 244);
            Controls.Add(btnExportAllBooks);
            Controls.Add(btnExportThisBook);
            Controls.Add(cboBooks);
            Controls.Add(btnAddBook);
            Controls.Add(btnRenameBook);
            Controls.Add(btnImportCsv);
            Controls.Add(btnClose);
            Controls.Add(btnDeleteBook);
            Name = "FormBookManager";
            StartPosition = FormStartPosition.CenterParent;
            Text = "帳本管理";
            ResumeLayout(false);
        }

        #endregion

        private Button btnDeleteBook;
        private Button btnClose;
        private Button btnImportCsv;
        private Button btnRenameBook;
        private Button btnAddBook;
        private ComboBox cboBooks;
        private Button btnExportThisBook;
        private Button btnExportAllBooks;
    }
}