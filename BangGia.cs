using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>Bảng giá cổ phiếu; đồng thời giữ số dư và danh mục đầu tư dùng chung cho các form khác.</summary>
    public partial class BangGia : Form
    {
        private const int ChuKyCapNhatMs = 3000; // 3 giây

        private readonly Random random = new Random();
        private readonly KhoLuuTruSQL khoLuuTruSQL = new KhoLuuTruSQL();
        private readonly XuLyAPI xuLyAPI = new XuLyAPI();
        private readonly Timer timerGiaLap = new Timer();   // giả lập giá biến động ngẫu nhiên
        private readonly Timer timerCapNhat = new Timer();  // lấy giá thật từ API

        private readonly DataTable marketTable = new DataTable();
        private readonly DataTable danhMucTable = new DataTable();
        private decimal soDu = 1000000m;
        private decimal tongLaiLo = 0;
        private bool dangCapNhat;

        // Giá giả lập ngẫu nhiên +-1% mỗi 3 giây. Đặt true nếu chỉ muốn demo giao diện;
        // khi đầu tư thử nên để false để lãi/lỗ tính theo giá thật.
        private static readonly bool GiaLapNgauNhien = false;

        #region Dữ liệu dùng chung cho các form khác

        public DataTable MarketTable => marketTable;

        public DataTable DanhMucTable => danhMucTable;

        public decimal SoDu
        {
            get { return soDu; }
            set { soDu = value; }
        }

        public decimal LaiLo
        {
            get { return tongLaiLo; }
            set { tongLaiLo = value; }
        }

        #endregion

        public BangGia()
        {
            InitializeComponent();

            TaoBangThiTruong();
            TaoBangDanhMuc();

            lblMarketTime.Text = DinhDangGio(DateTime.Now);

            // Timer: cứ mỗi ChuKyCapNhatMs thì gọi hàm Tick một lần
            timerGiaLap.Interval = ChuKyCapNhatMs;
            timerGiaLap.Tick += TimerGiaLap_Tick;
            if (GiaLapNgauNhien)
            {
                timerGiaLap.Start();
            }

            timerCapNhat.Interval = ChuKyCapNhatMs;
            timerCapNhat.Tick += async (sender, e) => await CapNhatGiaTuApiAsync();

            Load += BangGia_Load;
        }

        // Dừng các bộ đếm khi đăng xuất
        public void DungCapNhat()
        {
            timerGiaLap.Stop();
            timerCapNhat.Stop();
        }

        private static string DinhDangGio(DateTime gio)
        {
            return gio.ToString("dd/MM/yyyy HH:mm:ss");
        }

        #region Khởi tạo bảng

        // Tạo bảng giá và gán cho DataGridView để bảng giá hiện lên khi mở form
        private void TaoBangThiTruong()
        {
            marketTable.Columns.Add("Ma CK", typeof(string));
            marketTable.Columns.Add("Giá tham chiếu", typeof(decimal));
            marketTable.Columns.Add("Giá trần", typeof(decimal));
            marketTable.Columns.Add("Giá sàn", typeof(decimal));
            marketTable.Columns.Add("Giá hiện tại", typeof(decimal));
            marketTable.Columns.Add("% biến động", typeof(string));

            dgvMarket.AutoGenerateColumns = true;
            dgvMarket.DataSource = marketTable;
            dgvMarket.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Bảng cổ phiếu đang sở hữu (fDatLenh ghi vào, ThongTinTaiKhoan đọc ra)
        private void TaoBangDanhMuc()
        {
            danhMucTable.Columns.Add("Ma CP", typeof(string));
            danhMucTable.Columns.Add("So Luong", typeof(int));
            danhMucTable.Columns.Add("Gia Von TB", typeof(decimal));
            danhMucTable.Columns.Add("Gia Luc Mua", typeof(decimal));
            danhMucTable.Columns.Add("Gia Hien Tai", typeof(decimal));
            danhMucTable.Columns.Add("Tong Gia Mua", typeof(decimal));
            danhMucTable.Columns.Add("Tổng Giá Khi Bán", typeof(decimal));
            danhMucTable.Columns.Add("Tổng Lãi Lỗ Nếu Bán", typeof(decimal));
        }

        #endregion

        #region Nạp dữ liệu và cập nhật giá

        private async void BangGia_Load(object sender, EventArgs e)
        {
            try
            {
                NapDuLieuSQL();
                await CapNhatGiaTuApiAsync();
                timerCapNhat.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu bảng giá:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NapDuLieuSQL()
        {
            marketTable.Rows.Clear();
            DataTable dulieuSQL = khoLuuTruSQL.LayBangGia();

            if (dulieuSQL != null && dulieuSQL.Rows.Count > 0)
            {
                foreach (DataRow row in dulieuSQL.Rows)
                {
                    string maCK = row["Ma CK"].ToString();
                    bool daTonTai = marketTable.AsEnumerable().Any(r => r["Ma CK"].ToString() == maCK);
                    if (daTonTai) continue;

                    marketTable.Rows.Add(
                        maCK,
                        ChuyenDecimal(row["Giá tham chiếu"]),
                        ChuyenDecimal(row["Giá trần"]),
                        ChuyenDecimal(row["Giá sàn"]),
                        ChuyenDecimal(row["Giá hiện tại"]),
                        row["% biến động"].ToString());
                }
                return;
            }

            // Bảng giá trong SQL còn trống: lấy danh sách mã từ bảng DanhSachMaCK, giá để 0
            List<string> danhSachMa = khoLuuTruSQL.LayDanhSachMaTheoDoi();
            foreach (string maCK in danhSachMa.Where(ma => !string.IsNullOrWhiteSpace(ma)))
            {
                marketTable.Rows.Add(maCK.ToUpper(), 0m, 0m, 0m, 0m, "0.00%");
            }
        }

        // Lấy giá thật từ API cho từng mã, rồi ghi lại vào bảng và SQL
        private async Task CapNhatGiaTuApiAsync()
        {
            // Đang cập nhật dở thì bỏ qua, tránh chạy chồng lên nhau
            if (dangCapNhat || marketTable.Rows.Count == 0)
            {
                return;
            }

            dangCapNhat = true;
            try
            {
                foreach (DataRow row in marketTable.Rows)
                {
                    string maCK = row["Ma CK"].ToString();
                    if (string.IsNullOrWhiteSpace(maCK)) continue;

                    BaoGiaCoPhieu baoGia = await xuLyAPI.LayGiaCoPhietAsync(maCK);
                    if (baoGia == null) continue;

                    row["Giá tham chiếu"] = baoGia.GiaThamChieu;
                    row["Giá trần"] = baoGia.GiaTran;
                    row["Giá sàn"] = baoGia.GiaSan;
                    row["Giá hiện tại"] = baoGia.GiaKhop;
                    row["% biến động"] = DinhDangPhanTram(baoGia.PhanTramThayDoi);

                    await khoLuuTruSQL.CapNhatGiaRealTimeAsync(
                        baoGia.MaCK,
                        baoGia.GiaKhop,
                        baoGia.ThayDoi,
                        baoGia.PhanTramThayDoi);
                }

                lblMarketTime.Text = DinhDangGio(DateTime.Now);
                dgvMarket.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật giá cổ phiếu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                dangCapNhat = false;
            }
        }

        // Giả lập giá biến động ngẫu nhiên -1% đến +1% mỗi 3 giây
        private void TimerGiaLap_Tick(object sender, EventArgs e)
        {
            lblMarketTime.Text = DinhDangGio(DateTime.Now);

            foreach (DataRow row in marketTable.Rows)
            {
                decimal giaThamChieu = Convert.ToDecimal(row["Giá tham chiếu"]);
                decimal giaTran = Convert.ToDecimal(row["Giá trần"]);
                decimal giaSan = Convert.ToDecimal(row["Giá sàn"]);
                decimal giaHienTai = Convert.ToDecimal(row["Giá hiện tại"]);

                // random.NextDouble() cho số từ 0 đến dưới 1 -> (x * 2 - 1) * 0.01 cho khoảng -1% đến +1%
                double tyLeBienDong = (random.NextDouble() * 2 - 1) * 0.01;
                decimal giaMoi = Math.Round(giaHienTai * (decimal)(1 + tyLeBienDong), 2);

                // Giữ giá trong khoảng sàn - trần
                if (giaMoi > giaTran) giaMoi = giaTran;
                if (giaMoi < giaSan) giaMoi = giaSan;

                decimal phanTram = giaThamChieu > 0
                    ? Math.Round((giaMoi - giaThamChieu) / giaThamChieu * 100, 2)
                    : 0;

                row["Giá hiện tại"] = giaMoi;
                row["% biến động"] = DinhDangPhanTram(phanTram);
            }

            // Vẽ lại bảng để màu theo giá mới
            dgvMarket.Refresh();
        }

        private static string DinhDangPhanTram(decimal phanTram)
        {
            return (phanTram >= 0 ? "+" : "") + phanTram.ToString("0.00") + "%";
        }

        private static decimal ChuyenDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0m;
            }
            decimal.TryParse(value.ToString(), out decimal ketQua);
            return ketQua;
        }

        #endregion

        #region Đổi màu chữ trong bảng giá

        // Hàm này được Designer gắn vào sự kiện CellFormatting của dgvMarket
        private void dgvMarket_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string tenCot = dgvMarket.Columns[e.ColumnIndex].Name;

            // Màu cố định cho 3 cột giá chuẩn
            switch (tenCot)
            {
                case "Giá tham chiếu": e.CellStyle.ForeColor = Color.FromArgb(250, 204, 21); break;  // vàng
                case "Giá trần": e.CellStyle.ForeColor = Color.FromArgb(232, 121, 249); break;      // tím
                case "Giá sàn": e.CellStyle.ForeColor = Color.FromArgb(34, 211, 238); break;        // xanh lơ
            }
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;

            // Hai cột còn lại đổi màu theo giá hiện tại so với giá tham chiếu
            if (tenCot != "Giá hiện tại" && tenCot != "% biến động") return;

            DataGridViewRow row = dgvMarket.Rows[e.RowIndex];
            object giaThamChieuValue = row.Cells["Giá tham chiếu"].Value;
            object giaHienTaiValue = row.Cells["Giá hiện tại"].Value;
            if (giaThamChieuValue == null || giaHienTaiValue == null) return;

            decimal giaThamChieu = Convert.ToDecimal(giaThamChieuValue);
            decimal giaHienTai = Convert.ToDecimal(giaHienTaiValue);

            Color mauChu;
            if (giaHienTai > giaThamChieu) mauChu = Color.Green;       // tăng
            else if (giaHienTai < giaThamChieu) mauChu = Color.Red;    // giảm
            else mauChu = Color.Blue;                                  // đứng giá

            e.CellStyle.ForeColor = mauChu;
            e.CellStyle.SelectionForeColor = mauChu;
        }

        #endregion

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
