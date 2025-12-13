using System;
using System.Windows.Forms;

namespace ExpenseManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // ⭐ 優化 1：設定 Enter 鍵觸發登入
            this.AcceptButton = btnLogin;

            // ⭐ 優化 2：讓密碼欄位變星號 (也可以在設計介面設)
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // ⭐ 優化 3：UI 層先檢查是否為空
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("請輸入帳號與密碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AuthService.Login(txtUsername.Text, txtPassword.Text, out string msg))
            {
                MessageBox.Show(msg); // 也可以不顯示，直接進主畫面比較流暢
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg, "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 登入失敗通常會清空密碼，方便重打
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("請輸入帳號與密碼！", "提示");
                return;
            }

            if (AuthService.Register(txtUsername.Text, txtPassword.Text, out string msg))
            {
                MessageBox.Show(msg + "\n請直接點擊登入按鈕進入系統。", "註冊成功");
                // 註冊成功後，不用清空，使用者可以直接按登入
            }
            else
            {
                MessageBox.Show(msg, "註冊失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}