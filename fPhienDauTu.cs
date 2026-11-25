using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>
    /// Trang "Phiên đầu tư": chọn vốn và thời gian để bắt đầu phiên mới, xem lịch sử các phiên.
    /// Giao diện được dựng bằng code nên không có file Designer.
    /// </summary>
    public class fPhienDauTu : Form
    {
        #region Các lựa chọn trong ComboBox

        private sealed class LuaChonVon
        {
            public string Ten;
            public decimal Von;
            public override string ToString() { return Ten; }
        }

        private sealed class LuaChonThoiGian
        {
            public string Ten;
            public TimeSpan ThoiLuong;
            public override string ToString() { return Ten; }
        }

        #endregion

        private readonly ComboBox cbVon = new ComboBox();
        private readonly ComboBox cbThoiGian = new ComboBox();
        private readonly Button btnBatDau = new Button();
        private readonly Label lblTrangThai;
        private readonly Label lblLichSu;
        private readonly DataGridView dgvLichSu = new DataGridView();

        // Được gọi khi người dùng bắt đầu phiên mới thành công
        public event EventHandler PhienDaBatDau;

        public fPhienDauTu()
        {
            SuspendLayout();
            BackColor = GiaoDien.Nen;
            ForeColor = GiaoDien.Chu;
            Font = new Font("Segoe UI", 10F);
            Text = "Phiên đầu tư";

            // Tiêu đề và mô tả
            Label lblTieuDe = GiaoDien.TaoNhan("Phiên đầu tư thử", 20F, true, GiaoDien.XanhSang);
            lblTieuDe.Location = new Point(40, 28);

            var lblMoTa = new Label
            {
                Text = "Chọn số vốn được cấp và thời gian đầu tư. Trong thời gian này bạn mua/bán cổ phiếu như bình thường. "
                     + "Khi hết giờ, toàn bộ cổ phiếu sẽ tự động được bán theo giá hiện tại và chương trình báo bạn lãi hay lỗ.",
                Location = new Point(40, 78),
                Size = new Size(940, 50),
                ForeColor = GiaoDien.ChuNhat
            };

            // Chọn vốn và thời gian
            Label lblVon = GiaoDien.TaoNhan("Số vốn được cấp (VND)", 10F, true, GiaoDien.Chu);
            lblVon.Location = new Point(40, 145);
            TaoComboBox(cbVon, new Point(40, 175));
            foreach (decimal von in new decimal[] { 1000000m, 10000000m, 50000000m, 100000000m, 1000000000m })
            {
                cbVon.Items.Add(new LuaChonVon { Ten = von.ToString("N0") + " VND", Von = von });
            }
            cbVon.SelectedIndex = 0;

            Label lblThoiGian = GiaoDien.TaoNhan("Thời gian đầu tư", 10F, true, GiaoDien.Chu);
            lblThoiGian.Location = new Point(390, 145);
            TaoComboBox(cbThoiGian, new Point(390, 175));
            cbThoiGian.Items.Add(new LuaChonThoiGian { Ten = "5 phút (thử nghiệm)", ThoiLuong = TimeSpan.FromMinutes(5) });
            cbThoiGian.Items.Add(new LuaChonThoiGian { Ten = "1 giờ", ThoiLuong = TimeSpan.FromHours(1) });
            cbThoiGian.Items.Add(new LuaChonThoiGian { Ten = "1 ngày", ThoiLuong = TimeSpan.FromDays(1) });
            cbThoiGian.Items.Add(new LuaChonThoiGian { Ten = "3 ngày", ThoiLuong = TimeSpan.FromDays(3) });
            cbThoiGian.Items.Add(new LuaChonThoiGian { Ten = "1 tuần", ThoiLuong = TimeSpan.FromDays(7) });
            cbThoiGian.Items.Add(new LuaChonThoiGian { Ten = "2 tuần", ThoiLuong = TimeSpan.FromDays(14) });
            cbThoiGian.Items.Add(new LuaChonThoiGian { Ten = "1 tháng (30 ngày)", ThoiLuong = TimeSpan.FromDays(30) });
            cbThoiGian.SelectedIndex = 2;

            btnBatDau.Text = "Bắt đầu phiên";
            btnBatDau.Location = new Point(740, 168);
            btnBatDau.Size = new Size(240, 46);
            btnBatDau.FlatStyle = FlatStyle.Flat;
            btnBatDau.FlatAppearance.BorderSize = 0;
            btnBatDau.BackColor = GiaoDien.Xanh;
            btnBatDau.ForeColor = Color.White;
            btnBatDau.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnBatDau.Cursor = Cursors.Hand;
            btnBatDau.Click += btnBatDau_Click;

            lblTrangThai = new Label
            {
                Location = new Point(40, 232),
                Size = new Size(940, 44),
                ForeColor = GiaoDien.Lai,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold)
            };

            // Lịch sử
            lblLichSu = GiaoDien.TaoNhan("Lịch sử các phiên", 12F, true, GiaoDien.Chu);
            lblLichSu.Location = new Point(40, 290);

            GiaoDien.DinhDangBang(dgvLichSu);
            dgvLichSu.Location = new Point(40, 325);
            dgvLichSu.Size = new Size(940, 340);
            dgvLichSu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLichSu.CellFormatting += dgvLichSu_CellFormatting;

            Controls.AddRange(new Control[]
            {
                lblTieuDe, lblMoTa, lblVon, cbVon, lblThoiGian, cbThoiGian,
                btnBatDau, lblTrangThai, lblLichSu, dgvLichSu
            });
            ResumeLayout(false);

            CapNhatTrangThai();
            NapLichSu();
        }

        private static void TaoComboBox(ComboBox cb, Point viTri)
        {
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FlatStyle = FlatStyle.Flat;
            cb.BackColor = GiaoDien.NenPhu;
            cb.ForeColor = Color.White;
            cb.Font = new Font("Segoe UI", 11F);
            cb.Location = viTri;
            cb.Size = new Size(320, 30);
        }

        // Đang có phiên chạy thì khóa phần chọn, chỉ hiện thông tin phiên
        private void CapNhatTrangThai()
        {
            PhienDauTu phien = PhienService.Hien;
            bool coPhien = phien != null;

            cbVon.Enabled = !coPhien;
            cbThoiGian.Enabled = !coPhien;
            btnBatDau.Enabled = !coPhien;
            btnBatDau.BackColor = coPhien ? GiaoDien.NenPhu : GiaoDien.Xanh;

            lblTrangThai.Text = coPhien
                ? "Bạn đang có phiên #" + phien.MaPhien + ": vốn " + phien.VonBanDau.ToString("N0")
                  + " VND, kết thúc lúc " + phien.ThoiGianKetThuc.ToString("dd/MM/yyyy HH:mm") + "."
                : "";
        }

        private void NapLichSu()
        {
            try
            {
                dgvLichSu.DataSource = new PhienDauTuSQL().LayLichSu(dulieuchung.TenDangNhap);

                dgvLichSu.Columns["Bắt đầu"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvLichSu.Columns["Kết thúc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvLichSu.Columns["Vốn"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["Tổng tài sản cuối"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["Lãi / Lỗ"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["% lãi / lỗ"].DefaultCellStyle.Format = "0.00";
                dgvLichSu.Columns["Phiên"].FillWeight = 40;
            }
            catch (Exception ex)
            {
                lblLichSu.Text = "Lịch sử các phiên (không đọc được: " + ex.Message + ")";
            }
        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {
            var von = (LuaChonVon)cbVon.SelectedItem;
            var thoiGian = (LuaChonThoiGian)cbThoiGian.SelectedItem;

            string cauHoi = "Bắt đầu phiên đầu tư với vốn " + von.Von.ToString("N0") + " VND trong "
                          + thoiGian.Ten + "?\n\nKhi hết thời gian, toàn bộ cổ phiếu sẽ tự động được bán theo giá hiện tại.";
            if (MessageBox.Show(cauHoi, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                PhienService.BatDau(dulieuchung.Banggia, von.Von, thoiGian.ThoiLuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể bắt đầu phiên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CapNhatTrangThai();
            if (PhienDaBatDau != null)
            {
                PhienDaBatDau(this, EventArgs.Empty);
            }
        }

        // Tô màu cột lãi / lỗ trong lịch sử
        private void dgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !(e.Value is decimal)) return;

            string tenCot = dgvLichSu.Columns[e.ColumnIndex].Name;
            if (tenCot == "Lãi / Lỗ" || tenCot == "% lãi / lỗ")
            {
                e.CellStyle.ForeColor = GiaoDien.MauLaiLo((decimal)e.Value);
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }
    }
}
