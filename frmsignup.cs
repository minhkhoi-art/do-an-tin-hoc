
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
namespace đồ_án_tin_học_1
{
    //Thêm tài khoản mới → ExecuteNonQuery() : Chạy SQL để thêm, sửa, xóa dữ liệu.
    public partial class frmsignup : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public frmsignup()
        {
            InitializeComponent();
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("insert into TaiKhoan (TenDangNhap,MatKhau) values (@TenDangNhap , @MatKhau)", con);
                cmd.Parameters.AddWithValue("@TenDangNhap", txtTenTk.Text);
                cmd.Parameters.AddWithValue("@MatKhau", txtMk.Text);
                // Tạo cmd và AddWithValue chỉ là chuẩn bị.
                // Đến dòng ExecuteNonQuery() thì C# mới gửi lệnh sang SQL Server:
                // SQL Server thực hiện và ghi dòng mới vào bảng.
                // Từ lúc này dữ liệu nằm trong database, tắt app vẫn còn.
                cmd.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Khong the ket noi voi database" + ex.Message);
            }

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
            // mở lên tạo 1 cái kết nối 
            con = new SqlConnection(KetNoi.chuoiKetNoi);
        }
    }
}
