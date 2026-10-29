using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    public partial class frmsignup : Form
    {
        private string tentk;
        private string matkhau;

        public string Tentk
        {
            get { return tentk; }
            set { tentk = value; }
        }
        public string MatKhau
        {
            get { return matkhau; }
            set { matkhau = value; }
        }
        public frmsignup()
        {
            InitializeComponent();
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            tentk = txtTenTk.Text;
            matkhau = txtMk.Text;
            this.Close();
        }

        private void ckBHienMk_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBHienMk.Checked)
            {
                txtMk.PasswordChar = '\0';
            }
            else
            {
                txtMk.PasswordChar = '*';
            }
        }

        private void frmsignup_Load(object sender, EventArgs e)
        {
            txtMk.PasswordChar = '*';
        }
    }
}
