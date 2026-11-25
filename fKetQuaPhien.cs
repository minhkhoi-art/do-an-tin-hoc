using System;
using System.Drawing;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>Hộp thoại tổng kết lãi/lỗ khi phiên đầu tư kết thúc. Giao diện dựng bằng code.</summary>
    public class fKetQuaPhien : Form
    {
        private readonly DataGridView dgvChiTiet = new DataGridView();

        internal fKetQuaPhien(KetQuaPhien ketQua)
        {
            SuspendLayout();
            Text = "Kết quả phiên đầu tư";
            ClientSize = new Size(780, 570);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = GiaoDien.Nen;
            ForeColor = GiaoDien.Chu;
            Font = new Font("Segoe UI", 10.5F);

            // Dòng kết luận lớn: lãi / lỗ / hòa vốn
            string ketLuan = ketQua.LaiLo > 0 ? "BẠN ĐÃ LÃI"
                           : ketQua.LaiLo < 0 ? "BẠN ĐÃ LỖ"
                           : "HÒA VỐN";
            Color mau = GiaoDien.MauLaiLo(ketQua.LaiLo);

            Label lblKetLuan = GiaoDien.TaoNhan(ketLuan, 24F, true, mau);
            lblKetLuan.Location = new Point(30, 20);
            Controls.Add(lblKetLuan);

            string dau = ketQua.LaiLo >= 0 ? "+" : "";
            string laiLoText = dau + ketQua.LaiLo.ToString("N0") + " VND  (" + dau + ketQua.PhanTram.ToString("0.00") + "%)";

            ThemDong("Thời gian đầu tư",
                ketQua.ThoiGianBatDau.ToString("dd/MM/yyyy HH:mm") + "  →  " + ketQua.ThoiGianKetThuc.ToString("dd/MM/yyyy HH:mm"),
                GiaoDien.Chu, 90);
            ThemDong("Vốn ban đầu", ketQua.VonBanDau.ToString("N0") + " VND", GiaoDien.Chu, 122);
            ThemDong("Tổng tài sản cuối", ketQua.GiaTriCuoi.ToString("N0") + " VND", GiaoDien.Chu, 154);
            ThemDong("Lãi / Lỗ", laiLoText, mau, 186);

            Label lblChiTiet = GiaoDien.TaoNhan(
                ketQua.ChiTiet.Rows.Count > 0
                    ? "Cổ phiếu đã được bán khi kết thúc phiên"
                    : "Khi kết thúc bạn không còn giữ cổ phiếu nào",
                11F, true, GiaoDien.Chu);
            lblChiTiet.Location = new Point(30, 240);
            Controls.Add(lblChiTiet);

            GiaoDien.DinhDangBang(dgvChiTiet);
            dgvChiTiet.Location = new Point(30, 275);
            dgvChiTiet.Size = new Size(720, 220);
            // Phải thêm bảng vào form TRƯỚC khi gán DataSource, nếu không các cột chưa được tạo
            Controls.Add(dgvChiTiet);
            dgvChiTiet.DataSource = ketQua.ChiTiet;
            dgvChiTiet.Columns["Số lượng"].DefaultCellStyle.Format = "N0";
            dgvChiTiet.Columns["Giá vốn TB"].DefaultCellStyle.Format = "N2";
            dgvChiTiet.Columns["Giá bán"].DefaultCellStyle.Format = "N2";
            dgvChiTiet.Columns["Lãi / Lỗ"].DefaultCellStyle.Format = "N0";
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;

            var btnDong = new Button
            {
                Text = "Đóng",
                DialogResult = DialogResult.OK,
                Location = new Point(550, 512),
                Size = new Size(200, 42),
                FlatStyle = FlatStyle.Flat,
                BackColor = GiaoDien.Xanh,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDong.FlatAppearance.BorderSize = 0;
            Controls.Add(btnDong);
            AcceptButton = btnDong;

            ResumeLayout(false);
        }

        // Một dòng "tên: giá trị" trong phần tổng kết
        private void ThemDong(string ten, string giaTri, Color mauGiaTri, int y)
        {
            Label lblTen = GiaoDien.TaoNhan(ten, 11F, false, GiaoDien.ChuNhat);
            lblTen.Location = new Point(30, y);

            Label lblGiaTri = GiaoDien.TaoNhan(giaTri, 12F, true, mauGiaTri);
            lblGiaTri.Location = new Point(250, y - 2);

            Controls.Add(lblTen);
            Controls.Add(lblGiaTri);
        }

        private void dgvChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !(e.Value is decimal)) return;

            if (dgvChiTiet.Columns[e.ColumnIndex].Name == "Lãi / Lỗ")
            {
                e.CellStyle.ForeColor = GiaoDien.MauLaiLo((decimal)e.Value);
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }
    }
}
