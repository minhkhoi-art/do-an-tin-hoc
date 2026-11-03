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
    public partial class dsCoPhieu : Form
    {
        public dsCoPhieu()
        {
            InitializeComponent();
        }

        private void dsCoPhieu_Load(object sender, EventArgs e)
        {
            // không tự thiết kế cột
            dgvDanhsach.AutoGenerateColumns = false;

            //
            dgvDanhsach.Rows.Add(
       "AAPL", "$343.91", "-1.10%",
       "+26.50%", "$5.01T", "42.3M",
       "$243.42 - $351.73"
   );

            dgvDanhsach.Rows.Add(
                "MSFT", "$532.60", "+1.54%",
                "+10.13%", "$3.93T", "18.2M",
                "$349.20 - $553.72"
            );

            dgvDanhsach.Rows.Add(
                "NVDA", "$216.00", "+2.39%",
                "+15.82%", "$5.21T", "90.0M",
                "$164.27 - $243.37"
            );
        }

        private void txtTenCoPhieu_TextChanged(object sender, EventArgs e)
        {
            // dùng để tìm kiếm : mã :
            // hoặc bấm vào cổ phiếu ở datagridview thì sẽ hiện mã ở trên
        }

        private void btnMua_Click(object sender, EventArgs e)
        {
            // khi ấn vào sẽ chuyển qua trang mua/bán
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // quay lại form trang chủ
        }
    }
}
