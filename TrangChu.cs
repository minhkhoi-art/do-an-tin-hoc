using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    public partial class TrangChu : Form
    {
        private BangGia frmBangGia;

        public TrangChu()
        {
            InitializeComponent();
            frmBangGia = new BangGia();
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // quay lại form1
            this.Close();
        }
        private void btnDS_Click(object sender, EventArgs e)
        {
            hienthi(frmBangGia); //  mo frm
        }

        private void btnMuaBan_Click(object sender, EventArgs e)
        {
            hienthi(new fDatLenh());
        }

        private void btnTTTK_Click(object sender, EventArgs e)
        {
            hienthi(new ThongTinTaiKhoan());
        }
        private void hienthi(Form F)
        {
            //Xóa hết những gì đang nằm trong panel bên phải(form cũ), để chuẩn bị chỗ cho form mới.
            pnlNoiDung.Controls.Clear();
            //Mặc định form là “cửa sổ cấp cao nhất” (tự đứng một mình trên màn hình).
            //Đặt false để form trở thành control con, cho phép nằm trong panel.
            //Thiếu dòng này sẽ báo lỗi.
            F.TopLevel = false;
            //Bỏ viền và thanh tiêu đề
            //(nút thu nhỏ, phóng to, đóng) của form :
            //để nhìn như một phần của giao diện chứ không phải cửa sổ.
            F.FormBorderStyle = FormBorderStyle.None;
            //Cho form giãn kín toàn bộ panel, panel to nhỏ thế nào thì form theo thế đó.
            F.Dock = DockStyle.Fill;
            //Thêm form vào panel. Lúc này nó thuộc về panel nhưng chưa hiện.
            pnlNoiDung.Controls.Add(F);
            F.Show();
        }
    }
}
