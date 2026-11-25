using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>Form đặt lệnh mua / bán cổ phiếu.</summary>
    public partial class fDatLenh : Form
    {
        private const int CoPhieuMoiLo = 100; // 1 lô = 100 cổ phiếu

        private readonly BangGia bangGia;

        public fDatLenh()
        {
            InitializeComponent();
            bangGia = dulieuchung.Banggia;
            CapNhatSoDu();
        }

        // Hàm này được Designer gắn vào lblGia, để trống
        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void cbMaCP_DropDownClosed(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Chỉ đặt lệnh khi có phiên đầu tư đang chạy (chưa hết giờ)
            if (!PhienService.DangChay)
            {
                MessageBox.Show("Chưa có phiên đầu tư đang chạy hoặc phiên đã hết giờ.", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Kiểm tra người dùng đã chọn mã và nhập số lượng chưa
            if (cbMaCP.SelectedItem == null || string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui Lòng Nhập Mã Cổ Phiếu và Số Lượng ", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string symbol = cbMaCP.SelectedItem.ToString();

            // Số lượng phải là số nguyên lớn hơn 0
            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("So luong phai la so nguyen lon hon khong", "loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lấy giá hiện tại và tính tổng tiền của lệnh
            decimal giaHienTai = LayGiaHienTai(symbol);
            if (giaHienTai <= 0)
            {
                MessageBox.Show("Chưa có giá của mã này. Hãy mở \"Danh sách cổ phiếu\" để tải giá rồi thử lại.", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal tongTien = giaHienTai * soLuong * CoPhieuMoiLo;

            // 3. Mua hoặc bán
            if (rbtnMua.Checked)
            {
                if (!Mua(symbol, soLuong, giaHienTai, tongTien)) return;
            }
            else if (rbtnBan.Checked)
            {
                if (!Ban(symbol, soLuong, giaHienTai, tongTien)) return;
            }

            LuuPhien();
            CapNhatSoDu();
        }

        // Lưu tiền mặt và danh mục xuống SQL để mở lại chương trình vẫn còn
        private void LuuPhien()
        {
            try
            {
                PhienService.Luu(bangGia);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lưu được phiên đầu tư vào SQL:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Mua

        // Trả về false nếu lệnh bị từ chối
        private bool Mua(string symbol, int soLuong, decimal giaHienTai, decimal tongTien)
        {
            if (bangGia.SoDu < tongTien)
            {
                MessageBox.Show("So du tai khoang khong du", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bangGia.SoDu -= tongTien;

            DataRow dong = TimDongDanhMuc(symbol);
            if (dong == null)
            {
                // Mua mã này lần đầu: thêm dòng mới vào danh mục
                bangGia.DanhMucTable.Rows.Add(
                    symbol,
                    soLuong,
                    giaHienTai,   // Giá vốn TB
                    giaHienTai,   // Giá lúc mua
                    giaHienTai,   // Giá hiện tại
                    tongTien,     // Tổng giá mua
                    tongTien,     // Tổng giá khi bán
                    0m);          // Lãi lỗ nếu bán
            }
            else
            {
                MuaThem(dong, soLuong, giaHienTai, tongTien);
            }

            MessageBox.Show($"Mua thanh cong{soLuong}, co phieu {symbol}, voi gia 1 co phieu la : {giaHienTai}! ", "Thanh Cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        // Đã có mã này trong danh mục: cộng dồn số lượng và tính lại giá vốn trung bình
        private static void MuaThem(DataRow dong, int soLuongMua, decimal giaHienTai, decimal tienMuaLanNay)
        {
            int soLuongCu = Convert.ToInt32(dong["So Luong"]);
            decimal giaVonCu = Convert.ToDecimal(dong["Gia Von TB"]);
            decimal tongGiaMuaCu = Convert.ToDecimal(dong["Tong Gia Mua"]);
            int soLuongMoi = soLuongCu + soLuongMua;

            decimal giaVonMoi = ((soLuongCu * giaVonCu) + (soLuongMua * giaHienTai)) / soLuongMoi;
            decimal tongGiaMuaMoi = tongGiaMuaCu + tienMuaLanNay;
            decimal tongGiaKhiBan = giaHienTai * soLuongMoi * CoPhieuMoiLo;

            dong["So Luong"] = soLuongMoi;
            dong["Gia Von TB"] = Math.Round(giaVonMoi, 2);
            dong["Gia Luc Mua"] = giaHienTai;
            dong["Tong Gia Mua"] = tongGiaMuaMoi;
            dong["Gia Hien Tai"] = giaHienTai;
            dong["Tổng Giá Khi Bán"] = tongGiaKhiBan;
            dong["Tổng Lãi Lỗ Nếu Bán"] = tongGiaKhiBan - tongGiaMuaMoi;
        }

        #endregion

        #region Bán

        // Trả về false nếu lệnh bị từ chối
        private bool Ban(string symbol, int soLuong, decimal giaHienTai, decimal tongTien)
        {
            DataRow dong = TimDongDanhMuc(symbol);
            if (dong == null)
            {
                MessageBox.Show("Ban Khong So Huu Co Phieu Nay ", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int soLuongSoHuu = Convert.ToInt32(dong["So Luong"]);
            if (soLuongSoHuu < soLuong)
            {
                MessageBox.Show(" So luong CP khong du de ban ", "Thong Bao ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            dong["So Luong"] = soLuongSoHuu - soLuong;
            dong["Gia Hien Tai"] = giaHienTai;

            // Lãi/lỗ của lần bán này cộng dồn vào tổng lãi lỗ của tài khoản
            decimal giaVon = Convert.ToDecimal(dong["Gia Von TB"]);
            bangGia.LaiLo += (giaHienTai - giaVon) * soLuong * CoPhieuMoiLo;

            // Cộng tiền bán được vào số dư
            bangGia.SoDu += tongTien;

            MessageBox.Show($"Ban thanh cong{soLuong}, co phieu {symbol}, voi gia {giaHienTai}! ", "Thanh Cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Bán hết thì xóa mã khỏi danh mục
            if (Convert.ToInt32(dong["So Luong"]) == 0)
            {
                bangGia.DanhMucTable.Rows.Remove(dong);
            }
            return true;
        }

        #endregion

        #region Hàm hỗ trợ

        // Giá hiện tại của mã trong bảng giá (0 nếu không tìm thấy)
        private decimal LayGiaHienTai(string symbol)
        {
            foreach (DataRow row in bangGia.MarketTable.Rows)
            {
                if (row["Ma CK"].ToString() == symbol)
                {
                    return Convert.ToDecimal(row["Giá hiện tại"]);
                }
            }
            return 0m;
        }

        // Dòng của mã trong danh mục đang sở hữu (null nếu chưa có)
        private DataRow TimDongDanhMuc(string symbol)
        {
            return bangGia.DanhMucTable.AsEnumerable()
                .FirstOrDefault(row => row["Ma CP"].ToString() == symbol);
        }

        private void CapNhatSoDu()
        {
            // "N0": số có dấu phân cách hàng nghìn, không có phần thập phân
            lblSoDu.Text = "So Du Tien Mat: " + bangGia.SoDu.ToString("N0") + " VND";
        }

        #endregion
    }
}
