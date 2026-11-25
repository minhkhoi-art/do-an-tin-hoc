namespace đồ_án_tin_học_1
{
    /// <summary>Dữ liệu dùng chung giữa các form trong một lần đăng nhập.</summary>
    internal class dulieuchung
    {
        public static BangGia Banggia;
        public static fDatLenh FDL;

        // Tài khoản đang đăng nhập
        public static string TenDangNhap;

        // Phiên đầu tư đang chạy của tài khoản (null nếu chưa có)
        public static PhienDauTu PhienHienTai;
    }
}
