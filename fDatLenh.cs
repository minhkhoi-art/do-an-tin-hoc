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
    public partial class fDatLenh : Form
    {
        private decimal VonBanDau = 1000000m;
        private DataTable marketTable;
        private DataTable DanhMucSoHuuTable;

        private BangGia bangGia;

        #region hiểu thị số dư 
        public fDatLenh(BangGia bg)
        {
            InitializeComponent();
            bangGia = bg;
            lblSoDu.Text="So Du Tien Mat: "+bangGia.SoDu.ToString("N0")+" VND";
        }
        #endregion
        //public fDatLenh()
        //{
        //    InitializeComponent();
        //}

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
           
            //. Kiểm tra xem người dùng đã chọn mã và nhập số lượng chưa

            if (cbMaCP.SelectedItem == null || string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui Lòng Nhập Mã Cổ Phiếu và Số Lượng ", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string symbol = cbMaCP.SelectedItem.ToString();
            if (!int.TryParse(txtSoLuong.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("So luong phai la so nguyen lon hon khong", "loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 2. Lấy giá hiện tại của mã cổ phiếu đó từ bảng điện (marketTable)
            decimal giahientai = 0;
            foreach (DataRow row in bangGia.MarketTable.Rows)
            {
                if (row["Ma CK"].ToString() == symbol)
                {
                    giahientai = Convert.ToDecimal(row["Giá hiện tại"]);
                    break;
                }
                ;
            }
            decimal total = giahientai * quantity * 100;  //1lo=100CP
            // Xử lý logic Mua (BUY) hoặc Bán (SELL)
            if (rbtnMua.Checked)
            {
                if (bangGia.SoDu < total)
                {
                    MessageBox.Show("So du tai khoang khong du", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bangGia.SoDu -= total;
                bool found = false;
                foreach (DataRow pRow in bangGia.DanhMucTable.Rows)
                {
                    if (pRow["Ma CK"].ToString() == symbol)
                    {
                        int soluongcu = Convert.ToInt32(pRow["So Luong"]);
                        decimal GiavonTBCu = Convert.ToDecimal(pRow["Gia Von TB"]);

                        int soluongmoi = soluongcu + quantity;
                        decimal GiaBinhQuanMoi = ((soluongcu * GiavonTBCu) + (quantity * giahientai)) / soluongmoi;
                        pRow["So Luong"] = soluongmoi;
                        pRow["Gia Von TB"] = Math.Round(GiaBinhQuanMoi, 2);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    bangGia.DanhMucTable.Rows.Add(symbol, quantity, giahientai);
                }
                MessageBox.Show($"Mua thanh cong{quantity}, co phieu {symbol}, voi gia {giahientai}! ", "Thanh Cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (rbtnBan.Checked)
            {
                bool CoPhieuSoHuu = false;
                foreach (DataRow pRow in bangGia.DanhMucTable.Rows)
                {
                    if (pRow["Ma CK"].ToString() == symbol)
                    {
                        int soluongsohuu = Convert.ToInt32(pRow["So Luong"]);
                        if (soluongsohuu < quantity)
                        {
                            MessageBox.Show(" So luong CP khong du de ban ", "Thong Bao ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;

                        }
                        pRow["So Luong"] = soluongsohuu - quantity;
                        CoPhieuSoHuu = true;
                        //Nếu bán hết cổ phiếu thì xóa khỏi bản 
                        if (Convert.ToInt32(pRow["So Luong"]) == 0)
                        {
                            bangGia.DanhMucTable.Rows.Remove(pRow);
                        }
                        break;
                    }
                }
                if (!CoPhieuSoHuu)
                {
                    MessageBox.Show("Ban Khong So Huu Co Phieu Nay ", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //cong tien sau khi ban 
                bangGia.SoDu += total;
                MessageBox.Show($"Ban thanh cong{quantity}, co phieu {symbol}, voi gia {giahientai}! ", "Thanh Cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            lblSoDu.Text = "So Du Tien Mat: " + bangGia.SoDu.ToString("N0") + "VND";
        }

        private void fDatLenh_Load(object sender, EventArgs e)
        {

        }
    }
}
