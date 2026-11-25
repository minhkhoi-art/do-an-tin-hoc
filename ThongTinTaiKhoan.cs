using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>Trang thông tin tài khoản: tiền mặt, danh mục đang giữ, lãi lỗ.</summary>
    public partial class ThongTinTaiKhoan : Form
    {
        private const int CoPhieuMoiLo = 100; // 1 lô = 100 cổ phiếu
        private const int ChuKyCapNhatMs = 3000; // 3 giây

        private readonly BangGia bg;
        private readonly Timer timerCapNhat;

        // Cấu hình từng cột của bảng danh mục: tên cột, tiêu đề, độ rộng tương đối, định dạng số
        private static readonly (string Ten, string TieuDe, float DoRong, string DinhDang)[] CacCot =
        {
            ("Ma CP",                "MÃ CP",             65,  null),
            ("So Luong",             "SỐ LƯỢNG",          80,  "N0"),
            ("Gia Von TB",           "GIÁ VỐN TB",        85,  "N2"),
            ("Gia Luc Mua",          "GIÁ LÚC MUA",       85,  "N2"),
            ("Gia Hien Tai",         "GIÁ HIỆN TẠI",      90,  "N2"),
            ("Tong Gia Mua",         "TỔNG TIỀN MUA",     105, "N0"),
            ("Tổng Giá Khi Bán",     "GIÁ TRỊ NẾU BÁN",   110, "N0"),
            ("Tổng Lãi Lỗ Nếu Bán",  "LÃI / LỖ DỰ KIẾN",  110, "N0"),
        };

        public ThongTinTaiKhoan()
        {
            InitializeComponent();
            HienThiTenTaiKhoan();

            // Lấy dữ liệu dùng chung từ form bảng giá
            bg = dulieuchung.Banggia;

            HienThiDanhSach();
            CapNhatSoLieu();

            // Cứ mỗi 3 giây tính lại giá và lãi lỗ theo bảng giá mới nhất
            timerCapNhat = new Timer { Interval = ChuKyCapNhatMs };
            timerCapNhat.Tick += (sender, e) => CapNhatSoLieu();
            timerCapNhat.Start();
        }

        // Hàm này được Designer gắn vào sự kiện Load, để trống
        private void ThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HienThiTenTaiKhoan()
        {
            lblTenTaiKhoan.Text = dulieuchung.TenDangNhap;
        }

        #region Tính và hiển thị số liệu

        public void CapNhatSoLieu()
        {
            decimal tongGiaVon = 0;
            decimal tongGiaTriHienTai = 0;
            decimal tongLaiLoTam = 0;

            foreach (DataRow row in bg.DanhMucTable.Rows)
            {
                int soLuong = Convert.ToInt32(row["So Luong"]);
                decimal giaVonTB = Convert.ToDecimal(row["Gia Von TB"]);
                decimal tongGiaMua = Convert.ToDecimal(row["Tong Gia Mua"]);

                decimal giaHienTai = LayGiaHienTai(row["Ma CP"].ToString());
                decimal tongGiaKhiBan = giaHienTai * soLuong * CoPhieuMoiLo;
                decimal laiLoDuKien = tongGiaKhiBan - tongGiaMua;

                // Ghi kết quả vào các cột của bảng danh mục
                row["Gia Hien Tai"] = giaHienTai;
                row["Tổng Giá Khi Bán"] = tongGiaKhiBan;
                row["Tổng Lãi Lỗ Nếu Bán"] = laiLoDuKien;

                tongGiaVon += giaVonTB * soLuong * CoPhieuMoiLo;
                tongGiaTriHienTai += tongGiaKhiBan;
                tongLaiLoTam += laiLoDuKien;
            }

            lblTienMat1.Text = bg.SoDu.ToString("N0");
            lblGiaVon1.Text = tongGiaVon.ToString("N0");
            lblDangDauTu1.Text = tongGiaTriHienTai.ToString("N0");
            lblTongTam1.Text = tongLaiLoTam.ToString("N0");
            lblTong1.Text = bg.LaiLo.ToString("N0");

            HienThiDanhSach();
            dgvDanhsachDangGiu.Refresh();
        }

        // Tìm giá hiện tại của mã trong bảng giá chung (0 nếu không thấy)
        private decimal LayGiaHienTai(string maCK)
        {
            foreach (DataRow dong in bg.MarketTable.Rows)
            {
                if (dong["Ma CK"].ToString() == maCK)
                {
                    return Convert.ToDecimal(dong["Giá hiện tại"]);
                }
            }
            return 0m;
        }

        #endregion

        #region Bảng danh mục đang giữ

        private void HienThiDanhSach()
        {
            dgvDanhsachDangGiu.DataSource = null;
            dgvDanhsachDangGiu.AutoGenerateColumns = true;
            dgvDanhsachDangGiu.DataSource = bg.DanhMucTable;

            ThietKeBangDanhMuc();
        }

        private void ThietKeBangDanhMuc()
        {
            var bang = dgvDanhsachDangGiu;

            bang.RowTemplate.Height = 45;
            bang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bang.MultiSelect = false;
            bang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (var cauHinh in CacCot)
            {
                DataGridViewColumn cot = bang.Columns[cauHinh.Ten];
                cot.HeaderText = cauHinh.TieuDe;
                cot.FillWeight = cauHinh.DoRong;

                if (cauHinh.DinhDang != null)
                {
                    cot.DefaultCellStyle.Format = cauHinh.DinhDang;
                }
                // Cột số căn phải, riêng cột mã cổ phiếu thì giữ mặc định
                if (cauHinh.Ten != "Ma CP")
                {
                    cot.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Làm nổi bật mã cổ phiếu
            DataGridViewCellStyle kieuMa = bang.Columns["Ma CP"].DefaultCellStyle;
            kieuMa.ForeColor = Color.FromArgb(96, 165, 250);
            kieuMa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Đổi màu lãi/lỗ theo giá trị (gỡ trước rồi gắn lại để không bị gắn trùng)
            bang.CellFormatting -= dgvDanhsachDangGiu_CellFormatting;
            bang.CellFormatting += dgvDanhsachDangGiu_CellFormatting;
        }

        // Lãi: xanh lá, lỗ: đỏ, hòa vốn: xám
        private void dgvDanhsachDangGiu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;
            if (dgvDanhsachDangGiu.Columns[e.ColumnIndex].Name != "Tổng Lãi Lỗ Nếu Bán") return;

            decimal laiLo = Convert.ToDecimal(e.Value);

            if (laiLo > 0) e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);
            else if (laiLo < 0) e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68);
            else e.CellStyle.ForeColor = Color.FromArgb(148, 163, 184);

            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        #endregion
    }
}
