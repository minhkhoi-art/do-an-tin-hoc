using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>Form đăng ký: thêm tài khoản mới vào bảng TaiKhoan.</summary>
    public partial class frmsignup : Form
    {
        private const char KyTuMatKhau = '●';

        public frmsignup()
        {
            InitializeComponent();
        }

        private void frmsignup_Load(object sender, EventArgs e)
        {
            txtMk.PasswordChar = KyTuMatKhau;
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = KetNoi.taoketnoi())
                using (var cmd = new SqlCommand(
                    "insert into TaiKhoan (TenDangNhap,MatKhau) values (@TenDangNhap , @MatKhau)", con))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", txtTenTk.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMk.Text);

                    con.Open();
                    // ExecuteNonQuery: lúc này lệnh mới thật sự gửi sang SQL Server để ghi dòng mới
                    cmd.ExecuteNonQuery();
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Khong the ket noi voi database" + ex.Message);
            }
        }

        private void ckBHienMk_CheckedChanged(object sender, EventArgs e)
        {
            txtMk.PasswordChar = ckBHienMk.Checked ? '\0' : KyTuMatKhau;
        }

        #region Chữ gợi ý ("Tên tài khoản", "Mật khẩu") nằm đè lên ô nhập

        private void txtTenTk_Enter(object sender, EventArgs e)
        {
            lblGoiYTen.Visible = false;
        }

        private void txtTenTk_Leave(object sender, EventArgs e)
        {
            if (txtTenTk.Text == "")
            {
                lblGoiYTen.Visible = true;
            }
        }

        // Bấm vào chữ gợi ý thì ẩn nó và đưa con trỏ vào ô nhập
        private void lblGoiYTen_Click(object sender, EventArgs e)
        {
            lblGoiYTen.Visible = false;
            txtTenTk.Focus();
        }

        private void txtMk_Enter(object sender, EventArgs e)
        {
            lblGoiYMatKhau.Visible = false;
        }

        private void txtMk_Leave(object sender, EventArgs e)
        {
            if (txtMk.Text == "")
            {
                lblGoiYMatKhau.Visible = true;
            }
        }

        private void lblGoiYMatKhau_Click(object sender, EventArgs e)
        {
            lblGoiYMatKhau.Visible = false;
            txtMk.Focus();
        }

        #endregion
    }
}
