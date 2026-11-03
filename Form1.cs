
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
namespace đồ_án_tin_học_1
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        frmsignup signup;
        public Form1()
        {
            InitializeComponent();
        }
        //  AddWithValue():Là phương thức dùng để thêm một tham số và gán giá trị cho tham số đó.
        //cmd = new SqlCommand(cauLenhSQL, con);
        //        Có nghĩa là:
        //- cmd: Biến dùng để lưu đối tượng câu lệnh SQL.
        //- new SqlCommand: Tạo một đối tượng câu lệnh SQL mới.
        //- cauLenhSQL: Nội dung câu truy vấn cần thực hiện.
        //- con: Đối tượng kết nối đến SQL Server đã được mở.

        //Kiểm tra tài khoản → ExecuteScalar() : Chạy SQL, lấy một giá trị.
        //Lấy danh sách cổ phiếu → ExecuteReader() : Chạy SQL, lấy nhiều dòng dữ liệu.
        //Thêm tài khoản mới → ExecuteNonQuery() : Chạy SQL để thêm, sửa, xóa dữ liệu.
        private void btnLogIn_Click(object sender, EventArgs e)
        {

            con = new SqlConnection(KetNoi.chuoiKetNoi);
            try
            {
                con.Open();

                //AddWithValue điền giá trị, ví dụ nhập tentaikhoa = khoi :
                //thì : ... where TenDangNhap = 'khoi',con);
                cmd = new SqlCommand("select TenDangNhap from TaiKhoan " +
                "where TenDangNhap =@TenDangNhap ", con);
                cmd.Parameters.AddWithValue("@TenDangNhap", txtTenTk.Text);

                // lấy tên tài khoản -> lục trong sql xem có cái nào trùng với của người dùng nhập
                string kqtendangnhap = Convert.ToString(cmd.ExecuteScalar());

                // nếu không trùng -> nếu khác tên -> hiện thông báo
                if (kqtendangnhap != txtTenTk.Text)
                {
                    lblThongBao.BackColor = Color.Red;
                    lblThongBao.Text = " Tên Tài Khoản Sai !!!";
                    return;
                }
                // nếu có trùng -> đúng tên tài khoản -> kiểm tiếp tới mật khẩu
                else
                {
                    //AddWithValue điền giá trị, ví dụ nhập mật khẩu = 123:
                    //thì : ... where MatKhau='123',con);
                    cmd = new SqlCommand("select MatKhau from TaiKhoan " +
                    "where MatKhau =@MatKhau ", con);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);

                    // lấy mật khẩu -> lục trong sql xem có cái nào trùng với của người dùng nhập
                    string kqmatkhau = Convert.ToString(cmd.ExecuteScalar());

                    // nếu không trùng -> mật khẩu sai -> hiện thông báo : 
                    if (kqmatkhau.ToString() != txtMatKhau.Text)
                    {
                        lblThongBao.BackColor = Color.Red;
                        lblThongBao.Text = "Mật Khẩu Sai!!!!";
                        return;
                    }
                    // nếu có trùng -> đúng thì hiện form trang chủ
                    else
                    {
                        TrangChu tc = new TrangChu();
                        tc.FormClosed += (s, args) => this.Show();
                        this.Hide();
                        tc.Show();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            signup = new frmsignup();
            // khi mà trang login đóng thì trang đăng nhập (form1) hiện ra :
            // FormClosed : sự kiện diễn ra sau khi from login đóng 
            signup.FormClosed += (s, args) => this.Show();
            this.Hide();
            signup.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void ckBshowpass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBshowpass.Checked)
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }
    }
}
