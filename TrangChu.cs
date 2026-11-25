using System;
using System.Drawing;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    public partial class TrangChu : Form
    {
        private readonly BangGia frmBangGia;
        private readonly Timer timerDemNguoc = new Timer { Interval = 1000 };

        private Button btnPhien;   // nút "Phiên đầu tư" trên menu (tạo bằng code)
        private Label lblPhien;    // ô đếm ngược ở cuối menu (tạo bằng code)

        private bool dangKetThuc;  // đang chốt phiên, tránh chốt hai lần
        private bool loiKetThuc;   // chốt phiên bị lỗi, dừng tự thử lại

        public TrangChu()
        {
            InitializeComponent();
            frmBangGia = new BangGia();
            dulieuchung.Banggia = frmBangGia;

            TaoThanhPhien();

            timerDemNguoc.Tick += TimerDemNguoc_Tick;
            Load += TrangChu_Load;
            FormClosed += TrangChu_FormClosed;
        }

        #region Khởi động và đóng

        private async void TrangChu_Load(object sender, EventArgs e)
        {
            try
            {
                PhienDauTu phien = PhienService.KhoiPhuc(frmBangGia);

                if (phien == null)
                {
                    HienThiPhien();                  // chưa có phiên -> mời bắt đầu phiên mới
                }
                else if (phien.DaHetGio)
                {
                    await KetThucPhienAsync();       // phiên đã hết giờ khi chương trình đang tắt
                }
                else
                {
                    HienThi(frmBangGia);             // phiên đang chạy -> vào bảng giá để tải giá
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không đọc được dữ liệu phiên đầu tư:\n" + ex.Message +
                    "\n\nHãy chạy file PhienDauTu.sql trong SQL Server rồi mở lại chương trình.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CapNhatNhan();
            timerDemNguoc.Start();
        }

        private void TrangChu_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Dừng các bộ đếm và xóa dữ liệu của lần đăng nhập này
            timerDemNguoc.Stop();
            frmBangGia.DungCapNhat();
            dulieuchung.FDL = null;
            dulieuchung.PhienHienTai = null;
        }

        #endregion

        #region Menu

        // Đăng xuất: đóng trang chủ, form đăng nhập sẽ tự hiện lại
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDS_Click(object sender, EventArgs e)
        {
            HienThi(frmBangGia);
        }

        private void btnMuaBan_Click(object sender, EventArgs e)
        {
            if (!PhienService.DangChay)
            {
                MessageBox.Show("Bạn chưa có phiên đầu tư đang chạy. Hãy bắt đầu một phiên mới.",
                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiPhien();
                return;
            }

            if (dulieuchung.FDL == null)
            {
                dulieuchung.FDL = new fDatLenh();
            }
            HienThi(dulieuchung.FDL);
        }

        private void btnTTTK_Click(object sender, EventArgs e)
        {
            HienThi(new ThongTinTaiKhoan());
        }

        // Thêm nút "Phiên đầu tư" vào menu và ô đếm ngược ở cuối menu
        private void TaoThanhPhien()
        {
            btnPhien = new Button
            {
                Cursor = Cursors.Hand,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Font = btnTTTK.Font,
                ForeColor = btnTTTK.ForeColor,
                Padding = btnTTTK.Padding,
                Size = btnTTTK.Size,
                Text = "🏆 Phiên đầu tư",
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = true
            };
            btnPhien.FlatAppearance.BorderSize = 0;
            btnPhien.FlatAppearance.MouseOverBackColor = btnTTTK.FlatAppearance.MouseOverBackColor;
            btnPhien.Click += (sender, e) => HienThiPhien();

            lblPhien = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 110,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(203, 213, 225),
                Padding = new Padding(25, 8, 10, 8),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlMeunu.Controls.Add(btnPhien);
            pnlMeunu.Controls.Add(lblPhien);
            // BringToFront để nút nằm dưới "Thông Tin Tài Khoản" và ô đếm ngược nằm trên nút Đăng Xuất
            btnPhien.BringToFront();
            lblPhien.BringToFront();
        }

        #endregion

        #region Phiên đầu tư

        private void HienThiPhien()
        {
            var trang = new fPhienDauTu();
            trang.PhienDaBatDau += (sender, e) =>
            {
                CapNhatNhan();
                HienThi(frmBangGia);   // vào bảng giá để tải giá, rồi sang Mua/Bán để đầu tư
            };
            HienThi(trang);
        }

        // Mỗi giây: cập nhật đếm ngược, hết giờ thì tự chốt phiên
        private async void TimerDemNguoc_Tick(object sender, EventArgs e)
        {
            CapNhatNhan();

            PhienDauTu phien = PhienService.Hien;
            if (phien != null && phien.DaHetGio && !dangKetThuc && !loiKetThuc)
            {
                await KetThucPhienAsync();
            }
        }

        // Bán hết cổ phiếu theo giá hiện tại, lưu kết quả và hiện bảng tổng kết lãi/lỗ
        private async System.Threading.Tasks.Task KetThucPhienAsync()
        {
            if (dangKetThuc) return;
            dangKetThuc = true;
            try
            {
                KetQuaPhien ketQua = await PhienService.KetThucAsync(frmBangGia);

                CapNhatNhan();
                HienThiPhien();   // hiện trang phiên phía sau để bắt đầu phiên mới và xem lịch sử
                using (var hopThoai = new fKetQuaPhien(ketQua))
                {
                    hopThoai.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                loiKetThuc = true;
                MessageBox.Show(
                    "Không thể kết thúc phiên đầu tư:\n" + ex.Message +
                    "\n\nHãy kiểm tra kết nối SQL Server rồi đăng nhập lại để chốt phiên.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dangKetThuc = false;
            }
        }

        private void CapNhatNhan()
        {
            PhienDauTu phien = PhienService.Hien;
            if (phien == null)
            {
                lblPhien.Text = "Chưa có phiên đầu tư\nBấm \"Phiên đầu tư\" để bắt đầu";
                return;
            }

            lblPhien.Text = "Phiên đầu tư #" + phien.MaPhien
                          + "\nCòn lại: " + DinhDangConLai(phien.ConLai)
                          + "\nKết thúc: " + phien.ThoiGianKetThuc.ToString("dd/MM/yyyy HH:mm");
        }

        private static string DinhDangConLai(TimeSpan conLai)
        {
            string gio = conLai.ToString(@"hh\:mm\:ss");
            return conLai.Days > 0 ? conLai.Days + " ngày " + gio : gio;
        }

        #endregion

        /// <summary>Nhúng một form con vào panel bên phải của trang chủ.</summary>
        private void HienThi(Form form)
        {
            // Xóa form cũ đang nằm trong panel
            pnlNoiDung.Controls.Clear();

            // Form mặc định là cửa sổ cấp cao nhất; đặt false để nó trở thành control con của panel
            form.TopLevel = false;
            // Bỏ viền và thanh tiêu đề để trông như một phần của giao diện
            form.FormBorderStyle = FormBorderStyle.None;
            // Giãn kín panel
            form.Dock = DockStyle.Fill;

            pnlNoiDung.Controls.Add(form);
            form.Show();
        }
    }
}
