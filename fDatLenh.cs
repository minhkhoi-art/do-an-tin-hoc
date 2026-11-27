using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    public partial class fDatLenh : Form
    {
        
        private DataTable marketTable;
        private DataTable DanhMucSoHuuTable;
        private BangGia bangGia;
        private decimal total;
        private decimal GiaBinhQuanMoi;
        private int quantity;
        public int SOLUONGCP
        {
            get { return quantity;  }
        }
        public decimal GIABINHQUAN
        {
            get { return GiaBinhQuanMoi; }
        }
        public decimal Tonggiatridautu
        {
            get { return total; }
        }

        #region hiểu thị số dư 
      
        public fDatLenh()
        {
            InitializeComponent();
            bangGia = dulieuchung.Banggia;
            LayVonDauTu();
        }
        #endregion
        private void label2_Click(object sender, EventArgs e)
        {

        }
        // hiện giá tiền số dư : chưa mua bán gì hết chỉ hiện giá tiền người dùng nhập khi thiết lập
        private void LayVonDauTu()
        {
            using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
            {
                string sql = @"
            SELECT SoDuTienMat
            FROM TaiKhoan
            WHERE TenDangNhap = @TenDangNhap";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(
                    "@TenDangNhap",
                    dulieuchung.tenDangNhap
                );

                conn.Open();

                object ketQua = cmd.ExecuteScalar();

                if (ketQua == DBNull.Value || ketQua == null)
                {
                    MessageBox.Show(" Không có số dư tiền mặt ");
                    return;
                }
                decimal sodu = Convert.ToDecimal(ketQua);
                bangGia.SoDu = sodu;
                lblSoDu.Text = "Số dư tiền mặt " + bangGia.SoDu.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + "VNĐ";
            }
        }
        // cập nhật lại : giá hiện tại của 1 cổ phiếu : Tổng giá khi bán : tổng lãi lỗ khi bán
        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (DataRow pRow in bangGia.DanhMucTable.Rows)
            {
                string mack = pRow["Ma CP"].ToString();
                decimal giahientai = 0;
                // lấy số lượng hiện có 
                int soluong = Convert.ToInt32(pRow["So Luong"]);
                foreach (DataRow row in bangGia.MarketTable.Rows)
                {
                    if (mack == row["Ma CK"].ToString())
                    {
                        
                        // lấy giá hiện tại của cổ phiếu
                        giahientai = Convert.ToDecimal(row["Giá hiện tại"]);
                        pRow["Gia Hien Tai"] = giahientai;
                        break;


                    }
                }
               
                // tính tổng khi bán
                decimal tong_gia_khi_ban = soluong * giahientai * 100; ;
                pRow["Tổng Giá Khi Bán"] = tong_gia_khi_ban;
                decimal tong_gia_khi_mua = Convert.ToDecimal(pRow["Tong Gia Mua"]);
                // tính tổng lãi lỗ nếu bán =  tổng khi bán - tổng khi mua
                decimal tong_lai_lo_neu_ban = tong_gia_khi_ban - tong_gia_khi_mua;
                pRow["Tổng Lãi Lỗ Nếu Bán"] = tong_lai_lo_neu_ban;
            }
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu đầu vào
            if (cbMaCP.SelectedItem == null ||string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng chọn mã cổ phiếu và nhập số lượng!");
                return;
            }
            if (string.IsNullOrWhiteSpace(dulieuchung.tenDangNhap))
            {
                MessageBox.Show("Không tìm thấy tài khoản đăng nhập!");
                return;
            }
            if (!rbtnMua.Checked && !rbtnBan.Checked)
            {
                MessageBox.Show("Vui lòng chọn mua hoặc bán!");
                return;
            }
            if (!int.TryParse(txtSoLuong.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
                return;
            }

            // truyền mã cổ phiếu người dùng đã chọn vào symbol
            string symbol = cbMaCP.SelectedItem.ToString();

            // 2. Lấy giá hiện tại
            decimal giaHienTai = 0;
            bool timThay = false;
            // xem coi có tìm thấy giá cổ phiếu mà người dùng chọn không
            foreach (DataRow row in bangGia.MarketTable.Rows)
            {
                if (row["Ma CK"].ToString() == symbol)
                {
                    giaHienTai = Convert.ToDecimal(row["Giá hiện tại"]);
                    timThay = true;
                    break;
                }
            }

            if (!timThay || giaHienTai <= 0)
            {
                MessageBox.Show("Không tìm thấy giá cổ phiếu!");
                return;
            }

            // 3. Tính tổng tiền
            // quantity là số lô, 1 lô = 100 cổ phiếu
            total = giaHienTai * quantity * 100;

            bool laMua = rbtnMua.Checked;
            // Các giá trị sau giao dịch
            decimal soDuMoi = 0;
            int soLuongMoi = 0;
            decimal giaVonTBMoi = 0;
            decimal tongGiaMuaMoi = 0;
            decimal loiLoBanNay = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
                {
                    conn.Open();

                    // Bắt đầu giao dịch SQL
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        // kiểm tra xem người dùng đã sở hữu cổ phiếu chưa
                        try
                        {
                        // 4. Lấy số dư tiền mặt từ SQL
                        //UPDLOCK: yêu cầu khóa cập nhật khi đọc dữ liệu trong giao dịch.
                        //ROWLOCK: yêu cầu ưu tiên khóa ở cấp dòng.
                            string sqlSoDu = @"SELECT SoDuTienMat FROM TaiKhoan WITH (UPDLOCK, ROWLOCK) WHERE TenDangNhap = @TenDangNhap";

                            // lấy soducu : để mua hoặc bán sẽ lấy nó ra tính toán
                            decimal soDuCu;
                            using (SqlCommand cmd = new SqlCommand(sqlSoDu, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);

                                object ketQua = cmd.ExecuteScalar();

                                if (ketQua == null || ketQua == DBNull.Value)
                                {
                                    MessageBox.Show("Không tìm thấy số dư tài khoản!");
                                    tran.Rollback();
                                    return;
                                }

                                soDuCu = Convert.ToDecimal(ketQua);
                            }

                            // 5. Kiểm tra cổ phiếu đã có trong danh mục SQL chưa
                            bool daSoHuu = false;
                            // lấy các giá trị : để lát mua thêm thì sẽ lấy nó tính toán
                            int soLuongCu = 0;
                            decimal giaVonTBCu = 0;
                            decimal tongGiaMuaCu = 0;

                            string sqlDanhMuc = @"SELECT SoLuong, GiaVonTB, TongGiaMua FROM DanhMucSoHuu WITH (UPDLOCK, ROWLOCK)
                                                 WHERE TenDangNhap = @TenDangNhap AND MaCK = @MaCK";

                            using (SqlCommand cmd = new SqlCommand(sqlDanhMuc, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);
                                cmd.Parameters.AddWithValue("@MaCK", symbol);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    //kiểm tra xem SQL có tìm thấy dòng nào không.
                                    if (reader.Read())
                                    {
                                        daSoHuu = true;
                                        soLuongCu = Convert.ToInt32(reader["SoLuong"]);
                                        giaVonTBCu = Convert.ToDecimal(reader["GiaVonTB"]);
                                        tongGiaMuaCu = Convert.ToDecimal(reader["TongGiaMua"]);
                                    }
                                }
                            }
                            // 6. XỬ LÝ MUA
                            if (laMua)
                            {
                                // Kiểm tra đủ tiền không
                                if (soDuCu < total)
                                {
                                    MessageBox.Show("Số dư tài khoản không đủ!");
                                    tran.Rollback();
                                    return;
                                }

                                soDuMoi = soDuCu - total;
                                // nếu đã sở hữu
                                if (daSoHuu)
                                {
                                    // Đã sở hữu -> cộng số lượng
                                    soLuongMoi = soLuongCu + quantity;
                                    // Tính giá vốn trung bình mới
                                    giaVonTBMoi =((soLuongCu * giaVonTBCu)+ (quantity * giaHienTai))/ soLuongMoi;

                                    giaVonTBMoi = Math.Round(giaVonTBMoi, 2);

                                    // Cộng tiền mua vào tổng giá mua
                                    tongGiaMuaMoi = tongGiaMuaCu + total;

                                    string sqlUpdate = @"UPDATE DanhMucSoHuu SET SoLuong = @SoLuong, GiaVonTB = @GiaVonTB, GiaLucMua = @GiaLucMua, TongGiaMua = @TongGiaMua 
                                                       WHERE TenDangNhap = @TenDangNhap AND MaCK = @MaCK";

                                    using (SqlCommand cmd =new SqlCommand(sqlUpdate, conn, tran))
                                    {
                                        cmd.Parameters.AddWithValue("@SoLuong", soLuongMoi);
                                        cmd.Parameters.AddWithValue("@GiaVonTB", giaVonTBMoi);
                                        cmd.Parameters.AddWithValue("@GiaLucMua", giaHienTai);
                                        cmd.Parameters.AddWithValue("@TongGiaMua", tongGiaMuaMoi);
                                        cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);
                                        cmd.Parameters.AddWithValue("@MaCK", symbol);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // Mua lần đầu -> thêm dòng mới
                                    soLuongMoi = quantity;
                                    giaVonTBMoi = giaHienTai;
                                    tongGiaMuaMoi = total;

                                    string sqlInsert = @" INSERT INTO DanhMucSoHuu(TenDangNhap, MaCK, SoLuong, GiaVonTB,GiaLucMua, TongGiaMua)
                                                         VALUES(@TenDangNhap, @MaCK, @SoLuong, @GiaVonTB,@GiaLucMua, @TongGiaMua)";

                                    using (SqlCommand cmd =new SqlCommand(sqlInsert, conn, tran))
                                    {
                                        cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);
                                        cmd.Parameters.AddWithValue("@MaCK", symbol);
                                        cmd.Parameters.AddWithValue("@SoLuong", soLuongMoi);
                                        cmd.Parameters.AddWithValue("@GiaVonTB", giaVonTBMoi);
                                        cmd.Parameters.AddWithValue("@GiaLucMua", giaHienTai);
                                        cmd.Parameters.AddWithValue("@TongGiaMua", tongGiaMuaMoi);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                // Trừ tiền trong SQL
                                string sqlTruTien = @"UPDATE TaiKhoan SET SoDuTienMat = @SoDu WHERE TenDangNhap = @TenDangNhap";

                                using (SqlCommand cmd = new SqlCommand(sqlTruTien, conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@SoDu", soDuMoi);
                                    cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);
                                    cmd.ExecuteNonQuery();
                                }

                                // Thêm lịch sử mua
                                ThemLichSuGiaoDich(conn,tran,"BUY",symbol,quantity, giaHienTai,total ,0m);
                            }
                            // 7. XỬ LÝ BÁN
                            else
                            {
                                // Không sở hữu mã cổ phiếu
                                if (!daSoHuu)
                                {
                                    MessageBox.Show("Bạn không sở hữu cổ phiếu này!");
                                    tran.Rollback();
                                    return;
                                }

                                // Không đủ số lượng để bán
                                if (soLuongCu < quantity)
                                {
                                    MessageBox.Show("Số lượng cổ phiếu không đủ để bán!");
                                    tran.Rollback();
                                    return;
                                }

                                soLuongMoi = soLuongCu - quantity;

                                // Tính lãi/lỗ của lần bán
                                loiLoBanNay =(giaHienTai - giaVonTBCu) * quantity * 100;

                                // Cộng tiền bán vào số dư
                                soDuMoi = soDuCu + total;

                                if (soLuongMoi == 0)
                                {
                                    // Bán hết -> xóa khỏi danh mục
                                    string sqlXoa = @"DELETE FROM DanhMucSoHuu WHERE TenDangNhap = @TenDangNhap AND MaCK = @MaCK";

                                    using (SqlCommand cmd = new SqlCommand(sqlXoa, conn, tran))
                                    {
                                        cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);
                                        cmd.Parameters.AddWithValue("@MaCK", symbol);
                                        cmd.ExecuteNonQuery();
                                    }

                                    giaVonTBMoi = 0;
                                    tongGiaMuaMoi = 0;
                                }
                                else
                                {
                                    // Bán một phần -> chỉ giảm số lượng
                                    giaVonTBMoi = giaVonTBCu;

                                    // Trừ phần giá vốn tương ứng với số lô đã bán
                                    decimal giaVonDaBan =tongGiaMuaCu * quantity / soLuongCu;

                                    tongGiaMuaMoi =tongGiaMuaCu - giaVonDaBan;
                                    tongGiaMuaMoi =Math.Round(tongGiaMuaMoi, 2);

                                    string sqlCapNhat = @"UPDATE DanhMucSoHuu SET SoLuong = @SoLuong,TongGiaMua = @TongGiaMua
                                                            WHERE TenDangNhap = @TenDangNhap AND MaCK = @MaCK";

                                    using (SqlCommand cmd =new SqlCommand(sqlCapNhat, conn, tran))
                                    {
                                        cmd.Parameters.AddWithValue("@SoLuong", soLuongMoi);
                                        cmd.Parameters.AddWithValue("@TongGiaMua", tongGiaMuaMoi);
                                        cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);
                                        cmd.Parameters.AddWithValue("@MaCK", symbol);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                // Cập nhật số dư trong SQL
                                string sqlCongTien = @" UPDATE TaiKhoan SET SoDuTienMat = @SoDu WHERE TenDangNhap = @TenDangNhap";

                                using (SqlCommand cmd =new SqlCommand(sqlCongTien, conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@SoDu", soDuMoi);
                                    cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);
                                    cmd.ExecuteNonQuery();
                                }

                                // Thêm lịch sử bán
                                ThemLichSuGiaoDich(conn, tran, "SELL", symbol, quantity, giaHienTai, total, loiLoBanNay);
                            }

                            // Tất cả thao tác thành công mới lưu
                            tran.Commit();
                        }
                        catch
                        {
                            // nếu bị lỗi nào đó : như mua thành công giao dịch thành công nhma lịch sử giao dịch
                            //                    lôĩ thì nó sẽ huỷ cuộc giao dịch đó luôn
                            tran.Rollback();
                           // ném lỗi vừa bắt được ra bên ngoài để chỗ xử lý lỗi bên ngoài tiếp tục nhận nó.
                           // là đưa xuống mục catch ở dưới để hiện ra cái messegeBox á
                            throw;
                        }
                    }
                }

                // 8. Sau khi SQL lưu thành công mới cập nhật bộ nhớ
                bangGia.SoDu = soDuMoi;
                bangGia.LaiLo += loiLoBanNay;

                CapNhatDanhMucTrongBoNho(symbol,soLuongMoi,giaVonTBMoi,giaHienTai,tongGiaMuaMoi,laMua);

                // 9. Cập nhật số dư trên giao diện
                lblSoDu.Text = "Số dư tiền mặt: " + bangGia.SoDu.ToString("N0",CultureInfo.GetCultureInfo("vi-VN")) + " VND";

                MessageBox.Show((laMua ? "Mua" : "Bán") + " cổ phiếu thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Lỗi khi thực hiện giao dịch: " + ex.Message,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        // khi mua hoặc bán : thì thêm nó vào lịch sử giao dịch
        private void ThemLichSuGiaoDich( SqlConnection conn,SqlTransaction tran,string loaiLenh,string symbol,int soLuong,decimal gia, decimal tongTien,decimal laiLo)
        {
            string sql = @"INSERT INTO LichSuGiaoDich (TenDangNhap, MaCK, LoaiLenh, SoLuong, Gia, TongTien,LaiLo)
                         VALUES (@TenDangNhap, @MaCK, @LoaiLenh, @SoLuong, @Gia, @TongTien,@LaiLo)";

            using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@TenDangNhap",
                    dulieuchung.tenDangNhap
                );

                cmd.Parameters.AddWithValue("@MaCK", symbol);
                cmd.Parameters.AddWithValue("@LoaiLenh", loaiLenh);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@Gia", gia);
                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                cmd.Parameters.AddWithValue("@LaiLo", laiLo);


                cmd.ExecuteNonQuery();
            }
        }
        // cập nhâtj datatable sau khi sql đã giao dịch thành công
        private void CapNhatDanhMucTrongBoNho(string symbol, int soLuongMoi,decimal giaVonTB,decimal giaHienTai,decimal tongGiaMuaMoi,bool laMua)
        {
            DataRow dongTimThay = null;
            // tìm coi thấy dòng cần tìm không
            foreach (DataRow row in bangGia.DanhMucTable.Rows)
            {
                if (row["Ma CP"].ToString() == symbol)
                {
                    dongTimThay = row;
                    break;
                }
            }

            // Nếu đã bán hết thì xóa khỏi DataTable
            if (soLuongMoi == 0)
            {
                if (dongTimThay != null)
                {
                    bangGia.DanhMucTable.Rows.Remove(dongTimThay);
                }

                return;
            }
            // nếu mua hoặc bán 1 nửa cổ phần
            decimal tongGiaKhiBan = giaHienTai * soLuongMoi * 100;
            decimal laiLo = tongGiaKhiBan - tongGiaMuaMoi;
            // mua lần đầu thì add nó vào sql
            if (dongTimThay == null)
            {
                // Chưa có mã cổ phiếu thì thêm dòng mới
                bangGia.DanhMucTable.Rows.Add(symbol, soLuongMoi, giaVonTB, giaHienTai, giaHienTai, tongGiaMuaMoi, tongGiaKhiBan, laiLo);
            }
            // nếu đã có rồi thì thay đổi khi các giá trị nếu mua thêm hoặc bán bớt
            else
            {
                // Đã có thì cập nhật dòng cũ
                dongTimThay["So Luong"] = soLuongMoi;
                dongTimThay["Gia Von TB"] = giaVonTB;
                dongTimThay["Gia Hien Tai"] = giaHienTai;
                dongTimThay["Tong Gia Mua"] = tongGiaMuaMoi;
                dongTimThay["Tổng Giá Khi Bán"] = tongGiaKhiBan;
                dongTimThay["Tổng Lãi Lỗ Nếu Bán"] = laiLo;

                if (laMua)
                {
                    dongTimThay["Gia Luc Mua"] = giaHienTai;
                }

            }
        }
        // khi mở ra thì lấy số dư tiền mặt
        private void LuuSoDuTienMat()
        {
            using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
            {
                string sql = @"UPDATE TaiKhoan SET SoDuTienMat = @SoDu Where TenDangNhap = @TenDangNhap";
                SqlCommand cmd = new SqlCommand(sql, conn);
                // truyền tiền từ số dư trong bảng giá qua @SoDu
                cmd.Parameters.AddWithValue("@SoDu", bangGia.SoDu);
                cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private void fDatLenh_Load(object sender, EventArgs e)
        {

        }

        private void cbMaCP_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void btnCong_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text == "")
            {
                txtSoLuong.Text= "1";
                return;
            }
            int soluong = int.Parse(txtSoLuong.Text);
            soluong += 1;
            txtSoLuong.Text = soluong.ToString();
        }

        private void btnTru_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show(" Hãy nhập số lượng ", " Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtSoLuong.Text == "0")
            {
                MessageBox.Show(" Số lượng đã đạt mức tối thiểu ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int soluong = int.Parse(txtSoLuong.Text);
                soluong -= 1;
                txtSoLuong.Text = soluong.ToString();
            }
        }

        private void cbMaCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bangGia.MarketTable == null)
                return;

            string symbol = cbMaCP.Text;
            if (string.IsNullOrEmpty(symbol))
            {
                return;
            }
            else
            {
                foreach(DataRow row in bangGia.MarketTable.Rows)
                {
                    if (row["Ma CK"].ToString() == symbol)
                    {
                        decimal giahientai = decimal.Parse(row["Giá hiện tại"].ToString());
                        txtGia.Text = giahientai.ToString();
                        return;
                    }
                }
            }
        }
    }
}
