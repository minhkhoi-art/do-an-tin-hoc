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
using static System.Net.Mime.MediaTypeNames;

namespace đồ_án_tin_học_1
{
    public partial class BangGia : Form
    {
        // lưu dữ liệu bảng 
        private DataTable marketTable;
        private decimal VonBanDau = 1000000m;
        private DataTable DanhMucSoHuuTable;
        private double percentChange;
        private decimal Tonglailo = 0;

        private readonly KhoLuuTruSQL khoLuuTruSQL;
        private readonly XuLyAPI fireantDataService;
        private readonly Timer timerCapNhat;
        private bool dangCapNhat;



        public decimal LaiLo
        {
            get { return Tonglailo; }
            set { Tonglailo = value; }
        }
        public double Phantramthaydoi
        {
            get { return percentChange; }
        }
        public DataTable MarketTable
        {
            get { return marketTable; }
        }

        public DataTable DanhMucTable
        {
            get { return DanhMucSoHuuTable; }
        }

        public decimal SoDu
        {
            get { return VonBanDau; }
            set { VonBanDau = value; }
        }

        public BangGia()
        {
            InitializeComponent();
            //Gọi hàm InitializeMarketData() để tạo bảng dữ liệu cổ phiếu và đưa dữ liệu lên DataGridView.
            //Tức là khi mở Form, bảng giá cổ phiếu sẽ xuất hiện.
            InitializeMarketData();

            khoLuuTruSQL = new KhoLuuTruSQL();
            fireantDataService = new XuLyAPI();

            khoitaobanggia();
            dulieuchung.Banggia = this;
            NapDuLieuSQL();


            //Khi DataGridView cần hiển thị hoặc
            //định dạng một ô dữ liệu thì hàm dgvMarket_CellFormatting sẽ được gọi.
            //Trong bài này, hàm đó được dùng để đổi màu chữ giá cổ phiếu.
            dgvMarket.CellFormatting += dgvMarket_CellFormatting;


            #region  Cài đặt Thời gian 
            // cho nó hiển thị thời gian
            lblMarketTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
            // khơi tạo bảng danhmucsohuu
            DanhMucSoHuuTable = new DataTable();
            #endregion

            //Tạo từng cột cho bảng danh mục cổ phiếu
            DanhMucSoHuuTable.Columns.Add("Ma CP", typeof(string));
            DanhMucSoHuuTable.Columns.Add("So Luong", typeof(int));
            DanhMucSoHuuTable.Columns.Add("Gia Von TB", typeof(decimal));
            DanhMucSoHuuTable.Columns.Add("Gia Luc Mua", typeof(decimal));
            DanhMucSoHuuTable.Columns.Add("Gia Hien Tai", typeof(decimal));
            DanhMucSoHuuTable.Columns.Add("Tong Gia Mua", typeof(decimal));
            DanhMucSoHuuTable.Columns.Add("Tổng Giá Khi Bán", typeof(decimal));
            DanhMucSoHuuTable.Columns.Add("Tổng Lãi Lỗ Nếu Bán", typeof(decimal));


            #region  Cài đặt Thời gian 
            lblMarketTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm::ss");
            #endregion
            timerCapNhat = new Timer();
            timerCapNhat.Interval = 3000;
            timerCapNhat.Tick += async (sender, e) =>
            {
                await CapNhatGiaTuFireAntAsync();
            };
            Load += BangGia_Load;
            // nếu tắt bảng giá thì dừng cập nhật
            this.Disposed += (s, e) => { 
                timerCapNhat.Stop();
                timerCapNhat.Dispose(); 
            };
        }
        private async void BangGia_Load(object sender, EventArgs e)
        {
            try
            {
                await CapNhatGiaTuFireAntAsync();
                timerCapNhat.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu bảng giá:\n" + ex.Message,"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    bool daTonTai = marketTable.AsEnumerable()
                        .Any(r => r["Ma CK"].ToString() == maCK);

                    if (!daTonTai)
                    {
                        marketTable.Rows.Add(
                             row["Ma CK"].ToString(),
                            ChuyenDecimal(row["Giá tham chiếu"]),
                             ChuyenDecimal(row["Giá trần"]),
                             ChuyenDecimal(row["Giá sàn"]),
                             ChuyenDecimal(row["Giá hiện tại"]),
                             row["% biến động"].ToString());
                    }              
                }
            }
            else
            {
                // Trường hợp bảng KhoLuuTruCoPhieuSQL chưa có dữ liệu,
                // lấy danh sách mã từ bảng DanhSachMaCK.

                List<string> danhSachMa = khoLuuTruSQL.LayDanhSachMaTheoDoi();
                foreach (string maCK in danhSachMa)
                {
                    if (!string.IsNullOrWhiteSpace(maCK))
                    {
                        marketTable.Rows.Add(maCK.ToUpper(),
                                                0m,
                                                0m,
                                                0m,
                                                0m,
                                                "0.00%");
                    }
                }
            }
        }
        private async Task CapNhatGiaTuFireAntAsync()
        {
            if (dangCapNhat || marketTable == null || marketTable.Rows.Count == 0)
            {
                return;
            }
            dangCapNhat = true;
            try
            {
                foreach (DataRow row in marketTable.Rows)
                {
                    string MaCK = row["Ma CK"].ToString();
                    if (string.IsNullOrWhiteSpace(MaCK))
                    {
                        continue;
                    }
                    BaoGiaCoPhieu baogia = await fireantDataService.LayGiaCoPhietAsync(MaCK);
                    if (baogia == null)
                    {
                        System.Diagnostics.Debug.WriteLine("FireAnt không trả giá: " + MaCK);
                        continue;
                    }
         

                    row["Giá tham chiếu"] = baogia.GiaThamChieu;
                    row["Giá trần"] = baogia.GiaTran;
                    row["Giá sàn"] = baogia.GiaSan;
                    row["Giá hiện tại"] = baogia.GiaKhop;
            


                    row["% biến động"] =
                        (baogia.PhanTramThayDoi >= 0 ? "+" : "")
                        + baogia.PhanTramThayDoi.ToString("0.00")
                        + "%";

                    await khoLuuTruSQL.CapNhatGiaRealTimeAsync(
                    baogia.MaCK,
                    baogia.GiaKhop,
                    baogia.ThayDoi,
                    baogia.PhanTramThayDoi);
                }
                lblMarketTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
        private decimal ChuyenDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0m;
            }
            decimal.TryParse(value.ToString(), out decimal ketQua);
            return ketQua;
        }
        private void khoitaobanggia()
        {
          
            dgvMarket.AutoGenerateColumns = true;
            dgvMarket.DataSource = marketTable;
            dgvMarket.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }
        private void InitializeMarketData()
        {
            #region Khai báo và thêm Cổ Phiếu 
            // khởi tạo bảng marketTable
            marketTable = new DataTable();
            marketTable.Columns.Add("Ma CK", typeof(string));
            marketTable.Columns.Add("Giá tham chiếu", typeof(decimal));
            marketTable.Columns.Add("Giá trần", typeof(decimal));
            marketTable.Columns.Add("Giá sàn", typeof(decimal));
            marketTable.Columns.Add("Giá hiện tại", typeof(decimal));
            marketTable.Columns.Add("% biến động", typeof(string));

            ////Thêm các mã cổ phiếu mẫu
            //marketTable.Rows.Add("FPT", 90.0m, 96.3m, 83.7m, 90.0m, "0.00%");
            //marketTable.Rows.Add("VHM", 42.5m, 45.4m, 39.6m, 42.5m, "0.00%");
            //marketTable.Rows.Add("VIC", 40.0m, 42.8m, 37.2m, 40.0m, "0.00%");
            //marketTable.Rows.Add("MBB", 24.0m, 25.6m, 22.4m, 24.0m, "0.00%");
            #endregion


            //Cho phép DataGridView tự động tạo các cột dựa theo các cột có trong DataTable.
            dgvMarket.AutoGenerateColumns = true;
            // Gán vào đúng DataGridView dgvMarket : liên kết dữ liệu dgvMarket = marketTable
            dgvMarket.DataSource = marketTable;
            //Tự động điều chỉnh độ rộng các cột để lấp đầy chiều ngang của DataGridView.
            dgvMarket.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        #region Đổi màu cổ phiếu 

        private void dgvMarket_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.RowIndex<0)
            {
                return;
            }
            // Thêm màu chữ cho từng cột
            if (e.ColumnIndex == 1 )
                e.CellStyle.ForeColor = Color.FromArgb(250, 204, 21); // Vàng
            else if (e.ColumnIndex == 2)
                e.CellStyle.ForeColor = Color.FromArgb(232, 121, 249); // Tím
            else if (e.ColumnIndex == 3)
                e.CellStyle.ForeColor = Color.FromArgb(34, 211, 238); // Xanh lơ
 

            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            //Lấy dòng hiện tại trong DataGridView và lưu vào biến row.
            DataGridViewRow row = dgvMarket.Rows[e.RowIndex];
            //object là kiểu dữ liệu có thể chứa nhiều loại giá trị khác nhau.
            object giaThamChieuValue = row.Cells["Giá tham chiếu"].Value;
            object giaHienTaiValue = row.Cells["Giá hiện tại"].Value;

            if(giaHienTaiValue==null || giaThamChieuValue == null)
            {
                return;
            }

            // sau khi lấy dữ liệu rồi : mới chuyển sang kiểu decimal
            decimal giaThamChieu = Convert.ToDecimal(giaThamChieuValue);
            decimal giaHienTai =Convert.ToDecimal(giaHienTaiValue);

            Color mauChu = Color.Black;

            if (giaHienTai > giaThamChieu)
            {
                mauChu = Color.Green;
            }
            else if(giaHienTai < giaThamChieu)
            {
                mauChu = Color.Red;
            }
            else
            {
                mauChu = Color.Blue;
            }


            //// Khi người dùng nhấn vào cột nào sẽ lấy tên cột đó :
            ////e.ColumnIndex: Lấy vị trí cột đang được xử lý.
            ////dgvMarket.Columns[...]: Truy cập cột tương ứng.
            ////.Name: Lấy tên của cột đó.
            string tenCot = dgvMarket.Columns[e.ColumnIndex].Name;
            if (tenCot == "Giá hiện tại" || tenCot == "% biến động")
            {
                //Dùng để thiết lập màu chữ bình thường của ô khi ô đó chưa được chọn.
                e.CellStyle.ForeColor = mauChu;
                //Dùng để thiết lập màu chữ khi người dùng nhấn chọn ô đó.
                e.CellStyle.SelectionForeColor = mauChu;
            }
        }
        #endregion

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
       

    }
    public class BaoGiaCoPhieu
    {
        public string MaCK { get; set; }
        public decimal GiaThamChieu { get; set; }
        public decimal GiaTran { get; set; }
        public decimal GiaSan { get; set; }
        public decimal GiaKhop { get; set; }
        public long KhoiLuongKhop { get; set; }
        public decimal ThayDoi { get; set; }
        public decimal PhanTramThayDoi { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
