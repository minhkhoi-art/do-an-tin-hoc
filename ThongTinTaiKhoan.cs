using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{

    public partial class ThongTinTaiKhoan : Form
    {
        BangGia bg ;
        fDatLenh fdl;

        private Timer timeCapnhat;
        private void timerCapNhat_Tick(object sender,EventArgs e)
        {
            cap_nhat_so_lieu();
        }


        public void cap_nhat_so_lieu()
        {
            decimal tonggiavon = 0;
            decimal tongiatrihientai = 0;
            decimal tongtamlailo = 0;

            // dò trong bảng sở hữu cổ phiếu
            foreach (DataRow row in bg.DanhMucTable.Rows)
            {
                string mack = row["Ma CP"].ToString();
                int soluong = Convert.ToInt32(row["So Luong"]);
                decimal giavontb = Convert.ToDecimal(row["Gia Von TB"]);
                // tính Tổng giá vốn bỏ ra 
                tonggiavon += (giavontb * soluong * 100);
                // tính tổng tạm giá 
                decimal tonggiamua = Convert.ToDecimal(row["Tong Gia Mua"]);
                decimal tonggiaban = Convert.ToDecimal(row["Tổng Giá Khi Bán"]);
                tongtamlailo += tonggiaban - tonggiamua;
                foreach (DataRow marketrow in bg.MarketTable.Rows)
                {
                    // lấy mã cổ phiếu trong bảng sỡ hữu cổ phiếu : ra dò cái bảng giá cổ phiếu chung
                    if (marketrow["Ma CK"].ToString() == mack)
                    {
                        decimal giahientai = Convert.ToDecimal(marketrow["Giá hiện tại"]);

                        //tính giá ở cái lblDangDauTu : xong lấy nó so với giá vốn xem tăng hay giảm
                        tongiatrihientai += giahientai * soluong * 100;
                    }
                }

            }
            // hiển thị tiền 
            lblTienMat1.Text = bg.SoDu.ToString("N0");
            lblGiaVon1.Text = tonggiavon.ToString("N0");
            lblDangDauTu1.Text = tongiatrihientai.ToString("N0");
            lblTongTam1.Text = tongtamlailo.ToString("N0");
            lblTong1.Text = Convert.ToString(bg.LaiLo);
        }
        private void hienthi_tentaikhoan()
        {
            SqlCommand cmd;
            SqlConnection con;
            // hien thi ten tai khoan   
            con = new SqlConnection(KetNoi.chuoiKetNoi);
            con.Open();
            cmd = new SqlCommand("SELECT TenDangNhap from TaiKhoan", con);
            lblTenTaiKhoan.Text = Convert.ToString(cmd.ExecuteScalar());
        }

        public ThongTinTaiKhoan()
        {
            InitializeComponent();
            hienthi_tentaikhoan();
            // gọi để sử dụng cái proprety trong 2 bảng kia
            bg = dulieuchung.Banggia;
            fdl = dulieuchung.FDL;
            // cập nhật lại giá số liệu của cổ phiếu 
            cap_nhat_so_lieu();
            // hiển thị danh sách ra bảng
            hienthidanhsach();
            // mỗi 3 giây bên bảng giá cập nhật xong : thì bên này cũng sẽ gọi hàm TimerCapNhat_Tick
            // để cập nhật lại giá và tính toán lại các phần trăm 
            timeCapnhat = new Timer();
            timeCapnhat.Interval = 3000;
            timeCapnhat.Tick += timerCapNhat_Tick;
            timeCapnhat.Start();
        }
        public void hienthidanhsach()
        {
            dgvDanhsachDangGiu.DataSource = bg.DanhMucTable;
            ThietKeBangDanhMuc();
        }

        
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            // quay lại trang chủ
            this.Close();
        }

        private void ThongTinTaiKhoan_Load(object sender, EventArgs e)
        {

        }

        private void ThietKeBangDanhMuc()
        {

            // Chiều cao dòng
            dgvDanhsachDangGiu.RowTemplate.Height = 45;
            // Chế độ chọn dòng
            dgvDanhsachDangGiu.SelectionMode =DataGridViewSelectionMode.FullRowSelect;

            dgvDanhsachDangGiu.MultiSelect = false;
            // Tự chia độ rộng cột
            dgvDanhsachDangGiu.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt tên tiêu đề ngắn gọn
            dgvDanhsachDangGiu.Columns["Ma CP"].HeaderText = "MÃ CP";
            dgvDanhsachDangGiu.Columns["So Luong"].HeaderText = "SỐ LƯỢNG";
            dgvDanhsachDangGiu.Columns["Gia Von TB"].HeaderText = "GIÁ VỐN TB";
            dgvDanhsachDangGiu.Columns["Gia Luc Mua"].HeaderText = "GIÁ LÚC MUA";
            dgvDanhsachDangGiu.Columns["Gia Hien Tai"].HeaderText = "GIÁ HIỆN TẠI";
            dgvDanhsachDangGiu.Columns["Tong Gia Mua"].HeaderText = "TỔNG TIỀN MUA";
            dgvDanhsachDangGiu.Columns["Tổng Giá Khi Bán"].HeaderText = "GIÁ TRỊ NẾU BÁN";
            dgvDanhsachDangGiu.Columns["Tổng Lãi Lỗ Nếu Bán"].HeaderText = "LÃI / LỖ DỰ KIẾN";

            // Độ rộng tương đối của từng cột
            dgvDanhsachDangGiu.Columns["Ma CP"].FillWeight = 65;
            dgvDanhsachDangGiu.Columns["So Luong"].FillWeight = 80;
            dgvDanhsachDangGiu.Columns["Gia Von TB"].FillWeight = 85;
            dgvDanhsachDangGiu.Columns["Gia Luc Mua"].FillWeight = 85;
            dgvDanhsachDangGiu.Columns["Gia Hien Tai"].FillWeight = 90;
            dgvDanhsachDangGiu.Columns["Tong Gia Mua"].FillWeight = 105;
            dgvDanhsachDangGiu.Columns["Tổng Giá Khi Bán"].FillWeight = 110;
            dgvDanhsachDangGiu.Columns["Tổng Lãi Lỗ Nếu Bán"].FillWeight = 110;

            // Căn phải các cột số
            foreach (DataGridViewColumn cot in dgvDanhsachDangGiu.Columns)
            {
                if (cot.Name != "Ma CP")
                {
                    cot.DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Định dạng số
            dgvDanhsachDangGiu.Columns["So Luong"].DefaultCellStyle.Format = "N0";
            dgvDanhsachDangGiu.Columns["Gia Von TB"].DefaultCellStyle.Format = "N2";
            dgvDanhsachDangGiu.Columns["Gia Luc Mua"].DefaultCellStyle.Format = "N2";
            dgvDanhsachDangGiu.Columns["Gia Hien Tai"].DefaultCellStyle.Format = "N2";
            dgvDanhsachDangGiu.Columns["Tong Gia Mua"].DefaultCellStyle.Format = "N0";
            dgvDanhsachDangGiu.Columns["Tổng Giá Khi Bán"].DefaultCellStyle.Format = "N0";
            dgvDanhsachDangGiu.Columns["Tổng Lãi Lỗ Nếu Bán"].DefaultCellStyle.Format = "N0";

            // Làm nổi bật mã cổ phiếu
            dgvDanhsachDangGiu.Columns["Ma CP"].DefaultCellStyle.ForeColor =Color.FromArgb(96, 165, 250);
            dgvDanhsachDangGiu.Columns["Ma CP"].DefaultCellStyle.Font =new Font("Segoe UI", 10F, FontStyle.Bold);

            // Đổi màu lãi/lỗ theo giá trị
            dgvDanhsachDangGiu.CellFormatting -= dgvDanhsachDangGiu_CellFormatting;
            dgvDanhsachDangGiu.CellFormatting += dgvDanhsachDangGiu_CellFormatting;
        }

        private void dgvDanhsachDangGiu_CellFormatting(
            object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvDanhsachDangGiu.Columns[e.ColumnIndex].Name== "Tổng Lãi Lỗ Nếu Bán")
            {
                if (e.Value == null)
                    return;

                decimal laiLo = Convert.ToDecimal(e.Value);

                if (laiLo > 0)
                    e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);

                else if (laiLo < 0)
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68);

                else
                    e.CellStyle.ForeColor = Color.FromArgb(148, 163, 184);

                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }

    }
}
