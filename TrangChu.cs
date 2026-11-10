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
            frmBangGia.Show();  //  mo frm
        }

        private void btnMuaBan_Click(object sender, EventArgs e)
        {
            fDatLenh frm = new fDatLenh();
            frm.ShowDialog();
        }

        private void btnTTTK_Click(object sender, EventArgs e)
        {
            ThongTinTaiKhoan tttk = new ThongTinTaiKhoan();
            tttk.Show();
        }
    }
}
