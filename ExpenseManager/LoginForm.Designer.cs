namespace ExpenseManager
{
    partial class LoginForm
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
            lblTitle = new Label();
            label2 = new Label();
            label3 = new Label();
            btnRegister = new Button();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft JhengHei UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(70, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(178, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "使用者登入";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 71);
            label2.Name = "label2";
            label2.Size = new Size(46, 23);
            label2.TabIndex = 1;
            label2.Text = "帳號";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 106);
            label3.Name = "label3";
            label3.Size = new Size(46, 23);
            label3.TabIndex = 2;
            label3.Text = "密碼";
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(59, 152);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(80, 30);
            btnRegister.TabIndex = 3;
            btnRegister.Text = "註冊";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(87, 103);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(180, 30);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(87, 68);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(180, 30);
            txtUsername.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(159, 152);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(80, 30);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "登入";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 204);
            Controls.Add(btnLogin);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(btnRegister);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label label2;
        private Label label3;
        private Button btnRegister;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Button btnLogin;
    }
}