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
                decimal giaHienTai = 0m;

                // tính tổng tạm giá 
                decimal tonggiamua = Convert.ToDecimal(row["Tong Gia Mua"]);
                foreach (DataRow marketrow in bg.MarketTable.Rows)
                {
                    // lấy mã cổ phiếu trong bảng sỡ hữu cổ phiếu : ra dò cái bảng giá cổ phiếu chung
                    if (marketrow["Ma CK"].ToString() == mack)
                    {
                        giaHienTai = Convert.ToDecimal(marketrow["Giá hiện tại"]);
                        break;
                    }
                }
                decimal tongGiaKhiBan = giaHienTai * soluong * 100;
                decimal lailodukien = tongGiaKhiBan - tonggiamua;


                // Ghi lại dữ liệu vào các cột của DataTable
                row["Gia Hien Tai"] = giaHienTai;
                row["Tổng Giá Khi Bán"] = tongGiaKhiBan;
                row["Tổng Lãi Lỗ Nếu Bán"] = lailodukien;


                // tính Tổng giá vốn bỏ ra 
                tonggiavon += tonggiamua;
                tongiatrihientai += tongGiaKhiBan;
                tongtamlailo += lailodukien;


            }
          
            // hiển thị tiền 
            lblTienMat1.Text = bg.SoDu.ToString("N0");
            lblGiaVon1.Text = tonggiavon.ToString("N0");
            lblDangDauTu1.Text = tongiatrihientai.ToString("N0");
            lblTongTam1.Text = tongtamlailo.ToString("N0");
            using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
            {
               // ISNULL(giá_trị, 0) nghĩa là: nếu giá trị là NULL thì thay bằng 0
                string sql = @"select ISNULL(SUM(LaiLo), 0) from LichSuGiaoDich where TenDangNhap = @TenDangNhap";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
                conn.Open();
                decimal lailo = Convert.ToDecimal(cmd.ExecuteScalar());
                lblTong1.Text = lailo.ToString("N0");
            }
            dgvDanhsachDangGiu.Refresh();

        }

        private void NapSoDuTuSQL()
        {
            using (SqlConnection conn =new SqlConnection(KetNoi.chuoiKetNoi))
            {
                string sql = @"SELECT SoDuTienMat FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);

                conn.Open();

                object ketQua = cmd.ExecuteScalar();

                if (ketQua == null || ketQua == DBNull.Value)
                {
                    MessageBox.Show("Không tìm thấy số dư tiền mặt!");
                    return;
                }

                // Đưa số dư SQL vào BangGia
                bg.SoDu = Convert.ToDecimal(ketQua);
            }
        }
        private void Napdulieusql() // khi mua thì cái này nạp dòng mới vào mục bg.DanhMucTable
        {

            if (bg == null || bg.DanhMucTable == null)
            {
                MessageBox.Show("Danh mục cổ phiếu chưa được khởi tạo!");
                return;
            }

            if (string.IsNullOrWhiteSpace(dulieuchung.tenDangNhap))
            {
                MessageBox.Show("Không tìm thấy tài khoản đăng nhập!");
                return;
            }

            DataTable dt = bg.DanhMucTable;
            // xoá toàn bộ dòng cũ : trước khi tải dòng mới 
            dt.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
            {
                string sql = @"select MaCK , SoLuong , GiaVonTB,GiaLucMua,TongGiaMua from DanhMucSoHuu Where TenDangNhap =@TenDangNhap";
            
                SqlCommand cmd = new SqlCommand(sql, conn);
      
                cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
           
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string mack = reader["MaCK"].ToString();
                        int soluong = Convert.ToInt32(reader["SoLuong"]);
                        decimal giavonTB = Convert.ToDecimal(reader["GiaVonTB"]);
                        decimal gialucMua = Convert.ToDecimal(reader["GiaLucMua"]);
                        decimal tonggiamua = Convert.ToDecimal(reader["TongGiaMua"]);
                        decimal giahientai = LayGiaHienTai(mack);
                        decimal giatriNeuBan = giahientai * soluong * 100;
                        decimal lailo = giatriNeuBan - tonggiamua;
                        DataRow dongMoi = dt.NewRow();
                        dongMoi["Ma CP"] = mack;
                        dongMoi["So Luong"] = soluong;
                        dongMoi["Gia Von TB"] = giavonTB;
                        dongMoi["Gia Luc Mua"] = gialucMua;
                        dongMoi["Gia Hien Tai"] = giahientai;
                        dongMoi["Tong Gia Mua"] = tonggiamua;
                        dongMoi["Tổng Giá Khi Bán"] = giatriNeuBan;
                        dongMoi["Tổng Lãi Lỗ Nếu Bán"] = lailo;
                        dt.Rows.Add(dongMoi);
                    }
                }

            }
        }
        private decimal LayGiaHienTai(string maCK)
        {
            if (bg == null || bg.MarketTable == null)
            {
                return 0;
            }

            foreach (DataRow row in bg.MarketTable.Rows)
            {
                if (row["Ma CK"].ToString() == maCK)
                {
                    return Convert.ToDecimal(row["Giá hiện tại"]);
                }
            }

            return 0;
        }
        private void hienthi_tentaikhoan()
        {
            lblTenTaiKhoan.Text = dulieuchung.tenDangNhap;
        }

        public ThongTinTaiKhoan()
        {
            InitializeComponent();
            // gọi để sử dụng cái proprety trong 2 bảng kia
            bg = dulieuchung.Banggia;
            fdl = dulieuchung.FDL;


            if(bg == null)
            {
                MessageBox.Show("Bảng giá chưa được khởi tạo ");
                return;
            }
            //hiển thị tên tài khoản
            hienthi_tentaikhoan();
            // lấy số dư từ sql
            NapSoDuTuSQL();
            //lấy danh mục cổ phiếu từ sql
            Napdulieusql();
            // hiển thị danh sách ra bảng
            hienthidanhsach();
            // cập nhật lại giá số liệu của cổ phiếu 
            cap_nhat_so_lieu();

            // mỗi 3 giây bên bảng giá cập nhật xong : thì bên này cũng sẽ gọi hàm TimerCapNhat_Tick
            // để cập nhật lại giá và tính toán lại các phần trăm 
            timeCapnhat = new Timer();
            timeCapnhat.Interval = 3000;
            timeCapnhat.Tick += timerCapNhat_Tick;
            timeCapnhat.Start();
            // dừng time khi đóng form
            this.FormClosed += (s, e) =>
            {
                timeCapnhat.Stop();
                timeCapnhat.Dispose();
            };
        }
        public void hienthidanhsach()
        {

            dgvDanhsachDangGiu.DataSource = null;
            dgvDanhsachDangGiu.AutoGenerateColumns = true;
            dgvDanhsachDangGiu.DataSource=bg.DanhMucTable;
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

        private void dgvDanhsachDangGiu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
