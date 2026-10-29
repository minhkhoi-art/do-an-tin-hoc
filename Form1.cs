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
    public partial class Form1 : Form
    {
        private string tentkdadangky;
        private string matkhaudangky;
        frmsignup signup;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {


            // nếu tên tài khoản hoặc mật khẩu :
            // chưa có giá trị trong bộ nhớ :: hoặc rỗng  
            if (signup==null|| string.IsNullOrEmpty(signup.Tentk)||string.IsNullOrEmpty(signup.MatKhau))
            {
                lblThongBao.BackColor = Color.Red;
                lblThongBao.Text = " Tài Khoản chưa đăng ký ! Xin hãy đăng ký ";
            // nếu tài khoản chưa đki => dừng việc kiểm tra mật khẩu :: để đăng nhập
                return;

            }
            tentkdadangky = signup.Tentk;
            matkhaudangky = signup.MatKhau;
            if (tentkdadangky == txtTenTk.Text)
            {
                if (matkhaudangky == txtMatKhau.Text)
                {
                    TrangChu tc = new TrangChu();
                    // khi mà trang trangchu đóng thì trang đăng nhập (form1) hiện ra :
                    // FormClosed : sự kiện diễn ra sau khi from login đóng 
                    tc.FormClosed += (s, args) => this.Show();
                    this.Hide();
                    tc.Show();
                }
                else
                {
                    lblThongBao.BackColor = Color.Red;
                    lblThongBao.Text = " Mật Khẩu không đúng ! Xin vui lòng thử lại ";
                }
            }
            else
            {
                lblThongBao.BackColor = Color.Red;
                lblThongBao.Text = "Tên Tài Khoản không đúng ! Xin vui lòng thử lại ";
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
