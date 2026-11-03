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
    public partial class BangGia : Form
    {
        private DataTable marketTable;
        private Random random = new Random();
        private decimal VonBanDau = 1000000m;
        private DataTable DanhMucSoHuuTable;
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
            // Cài đặt Timer để tự động cập nhật giá sau mỗi 3 giây (3000ms)
            InitializeMarketData();

            // Đổi màu
            dgvMarket.CellFormatting += dgvMarket_CellFormatting;

            #region  Cài đặt Thời gian 
            lblMarketTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm::ss");
            DanhMucSoHuuTable = new DataTable();
            #endregion

            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += Timer_Tick;
            timer.Start();
            DanhMucSoHuuTable.Columns.Add("Ma CK", typeof(string));
            DanhMucSoHuuTable.Columns.Add("So Luong", typeof(int));
            DanhMucSoHuuTable.Columns.Add("Gia Von TB", typeof(decimal));
        }
        private void InitializeMarketData()
        {
            #region Khai báo và thêm Cổ Phiếu 
            marketTable = new DataTable();
            marketTable.Columns.Add("Ma CK", typeof(string));
            marketTable.Columns.Add("Giá tham chiếu", typeof(decimal));
            marketTable.Columns.Add("Giá trần", typeof(decimal));
            marketTable.Columns.Add("Giá sàn", typeof(decimal));
            marketTable.Columns.Add("Giá hiện tại", typeof(decimal));
            marketTable.Columns.Add("% biến động", typeof(string));

            // Thêm các mã cổ phiếu mẫu
            marketTable.Rows.Add("FPT", 90.0m, 96.3m, 83.7m, 90.0m, "0.00%");
            marketTable.Rows.Add("VHM", 42.5m, 45.4m, 39.6m, 42.5m, "0.00%");
            marketTable.Rows.Add("VIC", 40.0m, 42.8m, 37.2m, 40.0m, "0.00%");
            marketTable.Rows.Add("MBB", 24.0m, 25.6m, 22.4m, 24.0m, "0.00%");

            #endregion

            dgvMarket.AutoGenerateColumns = true;

            // Gán vào đúng DataGridView dgvMarket 
            dgvMarket.DataSource = marketTable;
            dgvMarket.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            #region Kiểm tra nếu marketTime đã được khởi tạo

            lblMarketTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm::ss");
            if (lblMarketTime == null)
            {
                return;
            }
            #endregion
            // Kiểm tra nếu marketTable đã được khởi tạo
            if (marketTable == null) return;
            foreach (DataRow row in marketTable.Rows)
            {
                decimal refPrice = Convert.ToDecimal(row["Giá tham chiếu"]);
                decimal ceiling = Convert.ToDecimal(row["Giá trần"]);
                decimal floor = Convert.ToDecimal(row["Giá sàn"]);
                decimal currentPrice = Convert.ToDecimal(row["Giá hiện tại"]);

                // Biến động ngẫu nhiên trong khoảng -1% đến +1%
                double percentChange = (random.NextDouble() * 2 - 1) * 0.01;
                decimal newPrice = Math.Round(currentPrice * (decimal)(1 + percentChange), 2);

                if (newPrice > ceiling) newPrice = ceiling;
                if (newPrice < floor) newPrice = floor;

                decimal percentTotal = Math.Round(((newPrice - refPrice) / refPrice) * 100, 2);

                row["Giá hiện tại"] = newPrice;
                row["% biến động"] = (percentTotal >= 0 ? "+" : "") + percentTotal.ToString("0.00") + "%";
            }
            // mỗi lần thay đổi giá màu sẽ thay đổi
            dgvMarket.Refresh();
        }
        private void lblMarketTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblMarketTime_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region Đổi màu cổ phiếu 
        private void dgvMarket_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.RowIndex<0)
            {
                return;
            }
            DataGridViewRow row = dgvMarket.Rows[e.RowIndex];
            object giaThamChieuValue = row.Cells["Giá tham chiếu"].Value;
            object giaHienTaiValue = row.Cells["Giá hiện tại"].Value;

            if(giaHienTaiValue==null || giaThamChieuValue == null)
            {
                return;
            }
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
            string tenCot = dgvMarket.Columns[e.ColumnIndex].Name;
            if(tenCot == "Giá hiện tại" || tenCot== "% biến động")
            {
                e.CellStyle.ForeColor = mauChu;
                e.CellStyle.SelectionForeColor = mauChu;
            }
        }
        #endregion
    }
}
