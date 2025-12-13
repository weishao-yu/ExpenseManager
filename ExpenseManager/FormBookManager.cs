using System;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseManager
{
    public partial class FormBookManager : Form
    {
        public int SelectedBookId { get; private set; }

        public FormBookManager(int currentBookId)
        {
            InitializeComponent();
            LoadBooks();

            // 若還有帳本，嘗試選回原本那一本
            if (cboBooks.Items.Count > 0)
            {
                cboBooks.SelectedValue = currentBookId;
                SelectedBookId = (int)cboBooks.SelectedValue;
            }
        }

        private void LoadBooks()
        {
            var books = BookService.GetMyBooks();

            cboBooks.DataSource = null;
            cboBooks.DataSource = books;
            cboBooks.DisplayMember = "Name";
            cboBooks.ValueMember = "Id";

            // 若有帳本，預設選第一本
            if (books.Count > 0)
            {
                cboBooks.SelectedIndex = 0;
                SelectedBookId = books[0].Id;
            }
            else
            {
                SelectedBookId = 0;
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "請輸入帳本名稱", "新增帳本", "");

            if (string.IsNullOrWhiteSpace(name)) return;

            BookService.AddBook(name);
            LoadBooks();
        }

        private void btnRenameBook_Click(object sender, EventArgs e)
        {
            if (cboBooks.SelectedItem is not Book book) return;

            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "新的帳本名稱", "更名帳本", book.Name);

            if (string.IsNullOrWhiteSpace(name)) return;

            BookService.RenameBook(book.Id, name);
            LoadBooks();
            cboBooks.SelectedValue = book.Id;
            SelectedBookId = book.Id;
        }
        private void btnExportAllBooks_Click(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "AllBooks.csv";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var all = new List<Record>();
            var books = BookService.GetMyBooks();

            foreach (var b in books)
            {
                var records = RecordService.GetRecordsByBook(b.Id);

                // 可選：在 Item 前加帳本名，避免混淆
                foreach (var r in records)
                    r.Item = $"[{b.Name}] {r.Item}";

                all.AddRange(records);
            }

            CsvExportService.Export(sfd.FileName, all);

            MessageBox.Show("所有帳本 CSV 匯出完成！");
        }

        private void btnExportThisBook_Click(object sender, EventArgs e)
        {
            if (cboBooks.SelectedItem is not Book book)
                return;

            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = $"{book.Name}.csv";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var records = RecordService.GetRecordsByBook(book.Id);

            CsvExportService.Export(sfd.FileName, records);

            MessageBox.Show("帳本 CSV 匯出完成！");
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (cboBooks.SelectedItem is not Book book) return;

            var books = BookService.GetMyBooks();
            if (books.Count <= 1)
            {
                MessageBox.Show("至少需要保留一個帳本");
                return;
            }

            var confirm = MessageBox.Show(
                $"確定刪除「{book.Name}」？\n底下所有紀錄都會被刪除。",
                "確認刪除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            BookService.DeleteBook(book.Id);
            LoadBooks();
        }

        private void btnImportCsv_Click(object sender, EventArgs e)
        {
            if (cboBooks.SelectedValue is not int bookId) return;

            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                // 1. 測試讀取
                var data = FileManager.Load(ofd.FileName);
                MessageBox.Show($"成功讀取 CSV，共 {data.Count} 筆資料"); // 👈 如果這裡顯示 0，就是 FileManager 寫錯了

                int successCount = 0;
                foreach (var r in data)
                {
                    // 2. 確保資料正確
                    r.UserId = Session.UserId;
                    r.BookId = bookId;
                    r.Id = 0;

                    // 3. 執行寫入
                    RecordService.AddMyRecord(r, bookId);
                    successCount++;
                }

                MessageBox.Show($"成功寫入 {successCount} 筆資料到資料庫！");
            }
            catch (Exception ex)
            {
                // 👈 這裡會告訴你為什麼失敗！
                MessageBox.Show($"發生錯誤：{ex.Message}\n{ex.StackTrace}");
            }

            DialogResult = DialogResult.OK;
            Close();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            // 最後保險：一定回傳一個有效帳本
            if (cboBooks.SelectedValue is int bid)
                SelectedBookId = bid;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
