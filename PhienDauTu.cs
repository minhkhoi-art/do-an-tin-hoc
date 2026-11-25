using System;
using System.Data;

namespace đồ_án_tin_học_1
{
    /// <summary>Một phiên đầu tư thử: có vốn ban đầu, thời gian bắt đầu và thời gian kết thúc.</summary>
    internal class PhienDauTu
    {
        public int MaPhien { get; set; }
        public string TenDangNhap { get; set; }
        public decimal VonBanDau { get; set; }

        // Tiền mặt và lãi lỗ đã chốt tại lần lưu gần nhất (dùng khi mở lại chương trình)
        public decimal TienMat { get; set; }
        public decimal LaiLoDaChot { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        public bool DaHetGio
        {
            get { return DateTime.Now >= ThoiGianKetThuc; }
        }

        public TimeSpan ConLai
        {
            get
            {
                TimeSpan conLai = ThoiGianKetThuc - DateTime.Now;
                return conLai < TimeSpan.Zero ? TimeSpan.Zero : conLai;
            }
        }
    }

    /// <summary>Kết quả tổng kết khi một phiên đầu tư kết thúc.</summary>
    internal class KetQuaPhien
    {
        public decimal VonBanDau { get; set; }
        public decimal GiaTriCuoi { get; set; }   // tiền mặt sau khi bán hết cổ phiếu
        public decimal LaiLo { get; set; }        // GiaTriCuoi - VonBanDau
        public decimal PhanTram { get; set; }     // LaiLo / VonBanDau * 100
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        // Các cổ phiếu đã bán khi kết thúc: Mã CP, Số lượng, Giá vốn TB, Giá bán, Lãi / Lỗ
        public DataTable ChiTiet { get; set; }
    }
}
