using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;


namespace đồ_án_tin_học_1{
    public partial class frmThietLapDauTu : Form
    {
        public frmThietLapDauTu()
        {
            InitializeComponent();
            // khi chọn sẽ cập nhât lại ngày
            rdo3Thang.CheckedChanged += rdoThoiHan_CheckedChanged;
            rdo6Thang.CheckedChanged += rdoThoiHan_CheckedChanged;
            rdo12Thang.CheckedChanged += rdoThoiHan_CheckedChanged;
            lblHienthisongaybd.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblHienthisongaykt.Text = "Chưa xác định";
        }
        private void rdoThoiHan_CheckedChanged(object sender,EventArgs e)
        {
            CapNhatNgay();
        }
        
        // 1. Tính ngày kết thúc dự kiến
        private void CapNhatNgay()
        {
       
            int soThang = rdo3Thang.Checked ? 3 : rdo6Thang.Checked ? 6 : rdo12Thang.Checked ? 12 : 0;
            DateTime ngayBatDau = DateTime.Today;
            lblHienthisongaybd.Text = ngayBatDau.ToString("dd/MM/yyyy");

            if (soThang == 0)
            {
                MessageBox.Show(" Bạn chưa chọn thời hạn thiết lập đầu tư ", " Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            DateTime ngayKetThuc = ngayBatDau.AddMonths(soThang);
            lblHienthisongaykt.Text = ngayKetThuc.ToString("dd/MM/yyyy");
        }
        //  Nút xác nhận
        private void btnXacNhan_Click(object sender, EventArgs e)
        {

        }

        // Nút đăng xuất
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            dulieuchung.tenDangNhap = null;

            frmLogin dangnhap = new frmLogin();
            dangnhap.Show();

            this.Close();
        }

        private void txtVonNhap_TextChanged(object sender, EventArgs e)
        {
            // lấy nội dung trong txtVonNhap : xoá tất cả các dấu : "." , "," , " " 
            string tien = txtVonNhap.Text.Replace(".", "").Replace(",", "");
            if (tien == "")
            {
                return;
            }
            // chuyển đổi thành kiểu decimal
            if(decimal.TryParse(tien,out decimal sotien))
            {
                //ToString("N0"): chuyển số thành chuỗi có dấu phân cách hàng nghìn, không có phần thập phân.
                //vi - VN: sử dụng cách định dạng số của Việt Nam, tức dấu chấm phân cách hàng nghìn.
                txtVonNhap.Text = sotien.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                //Ví dụ: Sau khi chuyển 1000 thành 1.000, con trỏ sẽ nằm sau số 0 cuối cùng để m tiếp tục nhập.
                txtVonNhap.SelectionStart = txtVonNhap.Text.Length;
            }
        }

        private void txtVonNhap_Enter(object sender, EventArgs e)
        {
            if(txtVonNhap.Text== "Nhập Số Tiền Bạn Muốn Bắt Đầu Đầu Tư")
            {
                txtVonNhap.Font = new Font("Segoe UI", 11, style: FontStyle.Bold);
                txtVonNhap.Text = "";
                txtVonNhap.ForeColor = Color.FromArgb(255,255,255);
            }
        }

        private void txtVonNhap_Leave(object sender, EventArgs e)
        {
            if (txtVonNhap.Text == "")
            {
                txtVonNhap.Text = "Nhập Số Tiền Bạn Muốn Bắt Đầu Đầu Tư";
                txtVonNhap.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        private void btnThietLap_Click(object sender, EventArgs e)
        {
            // Kiểm tra tài khoản đăng nhập
            if (string.IsNullOrWhiteSpace(dulieuchung.tenDangNhap))
            {
                MessageBox.Show("Không tìm thấy tài khoản đăng nhập!");
                return;
            }

            // Kiểm tra số vốn
            decimal vonDauTu;

            bool hopLe = decimal.TryParse(txtVonNhap.Text.Trim(), NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN")
                , out vonDauTu
            );

            if (!hopLe || vonDauTu <= 0)
            {
                MessageBox.Show(
                    "Vui lòng nhập số vốn hợp lệ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                //nếu mà bị lỗi thì con trỏ sẽ xuất hiện ngay dòng txtVonNhap
                txtVonNhap.Focus();
                return;
            }

            // Kiểm tra thời hạn
            if (rdo3Thang.Checked == false && rdo6Thang.Checked == false && rdo12Thang.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn thời hạn đầu tư!");
                return;
            }

            int thoiHan = 0;

            if (rdo12Thang.Checked == true)
            {
                thoiHan = 12;
            }
            else if (rdo6Thang.Checked == true)
            {
                thoiHan = 6;
            }
            else
            {
                thoiHan = 3;
            }

            DateTime ngayBatDau = DateTime.Today;

            // Lưu thông tin vào SQL Server
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(KetNoi.chuoiKetNoi))
                {
                    // update tài khoản : thay đổi vốn, thời hạn, ngày bắt đầu :
                    // tìm tên đăng nhập đúng tên thì đổi trong cái acc đó
                    string sql = @"
                    UPDATE TaiKhoan
                    SET VonDauTuBanDau = @Von,
                        SoDuTienMat=@Von,
                        ThoiHanDauTu = @ThoiHan,
                        NgayBatDauDauTu = @NgayBatDau
                    WHERE TenDangNhap = @TenDangNhap";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Von", vonDauTu);
                        cmd.Parameters.AddWithValue("@ThoiHan", thoiHan);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                        cmd.Parameters.AddWithValue(
                            "@TenDangNhap",
                            dulieuchung.tenDangNhap
                        );

                        conn.Open();

                        int ketQua = cmd.ExecuteNonQuery();

                        if (ketQua > 0)
                        {
                            MessageBox.Show(
                                "Thiết lập đầu tư thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            // Mở giao diện chính
                            TrangChu trangChu = new TrangChu();
                            trangChu.Show();

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản cần cập nhật!");
                        }
                    }
                }
                TrangChu tc = new TrangChu();
                tc.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi lưu thiết lập đầu tư: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
      
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmLogin dangnhap = new frmLogin();
            dangnhap.Show();
            this.Close();
        }
    } 

}

