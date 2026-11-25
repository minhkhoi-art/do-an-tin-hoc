using System;

namespace đồ_án_tin_học_1
{
    /// <summary>Thông tin giá của một mã cổ phiếu tại một thời điểm.</summary>
    public class BaoGiaCoPhieu
    {
        public string MaCK { get; set; }
        public decimal GiaThamChieu { get; set; }
        public decimal GiaTran { get; set; }
        public decimal GiaSan { get; set; }
        public decimal GiaKhop { get; set; }
        public long KhoiLuongKhop { get; set; }
        public decimal ThayDoi { get; set; }
        public decimal PhanTramThayDoi { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
