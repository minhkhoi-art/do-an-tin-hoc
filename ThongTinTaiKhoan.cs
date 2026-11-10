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
        }

        
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            // quay lại trang chủ
            this.Close();
        }
    }
}
