using System.Data.SqlClient;

namespace đồ_án_tin_học_1
{
    /// <summary>
    /// Nơi duy nhất chứa thông tin kết nối SQL Server.
    /// Đổi sang máy khác chỉ cần sửa MayChu bên dưới.
    /// </summary>
    internal class KetNoi
    {
        // Tên máy chủ SQL Server (đổi ở đây khi chạy trên máy khác)
        private const string MayChu = @"DESKTOP-7DQF29G\SQLEXPRESS03";

        // Database tài khoản người dùng
        public static readonly string chuoiKetNoi = TaoChuoiKetNoi("QuanLyCoPhieu");

        // Database lưu bảng giá cổ phiếu
        public static readonly string chuoiKetNoiKhoLuuTru = TaoChuoiKetNoi("KhoLuuTruCoPhieuSQL");

        // Integrated Security=True       : đăng nhập SQL Server bằng tài khoản Windows hiện tại.
        // TrustServerCertificate=True    : tin cậy chứng chỉ máy chủ, tránh lỗi xác thực khi học tập.
        private static string TaoChuoiKetNoi(string tenDatabase)
        {
            return "Data Source=" + MayChu + ";"
                 + "Initial Catalog=" + tenDatabase + ";"
                 + "Integrated Security=True;"
                 + "TrustServerCertificate=True;";
        }

        // Tạo một đối tượng kết nối mới tới database QuanLyCoPhieu
        public static SqlConnection taoketnoi()
        {
            return new SqlConnection(chuoiKetNoi);
        }
    }
}
