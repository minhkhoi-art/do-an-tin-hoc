using System.Drawing;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    /// <summary>Màu sắc và hàm dựng giao diện dùng chung cho các form tạo bằng code.</summary>
    internal static class GiaoDien
    {
        public static readonly Color Nen = Color.FromArgb(10, 19, 48);
        public static readonly Color NenPhu = Color.FromArgb(18, 33, 63);
        public static readonly Color Chu = Color.FromArgb(203, 213, 225);
        public static readonly Color ChuNhat = Color.FromArgb(148, 163, 184);
        public static readonly Color Xanh = Color.FromArgb(37, 99, 235);
        public static readonly Color XanhSang = Color.FromArgb(59, 130, 246);
        public static readonly Color Lai = Color.FromArgb(34, 197, 94);
        public static readonly Color Lo = Color.FromArgb(239, 68, 68);

        // Lãi: xanh lá, lỗ: đỏ, hòa vốn: xám
        public static Color MauLaiLo(decimal giaTri)
        {
            if (giaTri > 0) return Lai;
            if (giaTri < 0) return Lo;
            return ChuNhat;
        }

        public static Label TaoNhan(string noiDung, float coChu, bool inDam, Color mau)
        {
            return new Label
            {
                Text = noiDung,
                AutoSize = true,
                ForeColor = mau,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", coChu, inDam ? FontStyle.Bold : FontStyle.Regular)
            };
        }

        public static void DinhDangBang(DataGridView bang)
        {
            bang.BackgroundColor = Nen;
            bang.BorderStyle = BorderStyle.None;
            bang.GridColor = Color.FromArgb(30, 45, 80);

            bang.EnableHeadersVisualStyles = false;
            bang.ColumnHeadersDefaultCellStyle.BackColor = NenPhu;
            bang.ColumnHeadersDefaultCellStyle.ForeColor = Chu;
            bang.ColumnHeadersDefaultCellStyle.SelectionBackColor = NenPhu;
            bang.ColumnHeadersDefaultCellStyle.SelectionForeColor = Chu;

            bang.DefaultCellStyle.BackColor = Nen;
            bang.DefaultCellStyle.ForeColor = Chu;
            bang.DefaultCellStyle.SelectionBackColor = Xanh;
            bang.DefaultCellStyle.SelectionForeColor = Color.White;

            bang.RowHeadersVisible = false;
            bang.AllowUserToAddRows = false;
            bang.AllowUserToDeleteRows = false;
            bang.AllowUserToResizeRows = false;
            bang.ReadOnly = true;
            bang.MultiSelect = false;
            bang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bang.RowTemplate.Height = 34;
        }
    }
}
