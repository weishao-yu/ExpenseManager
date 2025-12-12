using System;
using System.Windows.Forms;

namespace ExpenseManager
{
    public partial class FormExportOption : Form
    {
        public FormExportOption()
        {
            InitializeComponent();
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;   // PDF
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;  // Excel
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
