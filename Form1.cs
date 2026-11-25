using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>Form đăng nhập.</summary>
    public partial class frmLogin : Form
    {
        private const char KyTuMatKhau = '●';

        public frmLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = KyTuMatKhau;
        }

        #region Đăng nhập

        // Các cách chạy SQL hay dùng:
        //  - ExecuteScalar()    : lấy một giá trị (dùng để kiểm tra tài khoản)
        //  - ExecuteReader()    : lấy nhiều dòng dữ liệu
        //  - ExecuteNonQuery()  : thêm, sửa, xóa dữ liệu
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = KetNoi.taoketnoi())
                {
                    con.Open();

                    // Bước 1: kiểm tra tên tài khoản
                    if (LayGiaTri(con, "TenDangNhap", txtTenTk.Text) != txtTenTk.Text)
                    {
                        HienThongBao(" Tên Tài Khoản Sai !!!");
                        return;
                    }

                    // Bước 2: kiểm tra mật khẩu của đúng tài khoản này
                    if (!MatKhauDung(con, txtTenTk.Text, txtMatKhau.Text))
                    {
                        HienThongBao("Mật Khẩu Sai!!!!");
                        return;
                    }
                }

                // Ghi nhớ tài khoản đang đăng nhập để các form khác dùng (phiên đầu tư, thông tin tài khoản)
                dulieuchung.TenDangNhap = txtTenTk.Text;
                MoTrangChu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }
        }

        // Tìm trong bảng TaiKhoan giá trị của cột khớp với giá trị người dùng nhập.
        // Trả về chuỗi rỗng nếu không có dòng nào.
        // (tenCot chỉ nhận hằng số trong code, còn giá trị nhập vào đi qua tham số nên an toàn SQL injection)
        private static string LayGiaTri(SqlConnection con, string tenCot, string giaTriNhap)
        {
            string truyVan = "select " + tenCot + " from TaiKhoan where " + tenCot + " = @GiaTri";
            using (var cmd = new SqlCommand(truyVan, con))
            {
                cmd.Parameters.AddWithValue("@GiaTri", giaTriNhap);
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }

        // Mật khẩu nhập vào có khớp với mật khẩu của tài khoản tenDangNhap không
        private static bool MatKhauDung(SqlConnection con, string tenDangNhap, string matKhauNhap)
        {
            using (var cmd = new SqlCommand("select MatKhau from TaiKhoan where TenDangNhap = @TenDangNhap", con))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                return Convert.ToString(cmd.ExecuteScalar()) == matKhauNhap;
            }
        }

        private void HienThongBao(string noiDung)
        {
            lblThongBao.BackColor = Color.FromArgb(11, 21, 48);
            lblThongBao.Text = noiDung;
        }

        private void MoTrangChu()
        {
            var trangChu = new TrangChu();
            // Khi trang chủ đóng (đăng xuất) thì form đăng nhập hiện lại
            trangChu.FormClosed += (s, args) => Show();
            Hide();
            trangChu.Show();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            var signup = new frmsignup();
            // Khi form đăng ký đóng thì form đăng nhập hiện lại
            signup.FormClosed += (s, args) => Show();
            Hide();
            signup.Show();
        }

        #endregion

        #region Hiệu ứng nút bấm

        private void btnLogIn_MouseEnter(object sender, EventArgs e)
        {
            btnLogIn.BackColor = Color.FromArgb(59, 130, 246);
        }

        private void btnLogIn_MouseLeave(object sender, EventArgs e)
        {
            btnLogIn.BackColor = Color.FromArgb(37, 99, 235);
        }

        private void btnSignUp_MouseEnter(object sender, EventArgs e)
        {
            btnSignUp.BackColor = Color.FromArgb(51, 65, 85);
        }

        private void btnSignUp_MouseLeave(object sender, EventArgs e)
        {
            btnSignUp.BackColor = Color.FromArgb(27, 45, 77);
        }

        #endregion

        private void btnHienMatKhau_Click(object sender, EventArgs e)
        {
            bool dangAn = txtMatKhau.PasswordChar == KyTuMatKhau;
            txtMatKhau.PasswordChar = dangAn ? '\0' : KyTuMatKhau;
            btnHienMatKhau.Text = dangAn ? "ẨN" : "HIỆN";
        }
    }
}
