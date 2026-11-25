using System;
using System.Data;
using System.Data.SqlClient;

namespace đồ_án_tin_học_1
{
    /// <summary>Đọc và ghi bảng PhienDauTu, DanhMucPhien (tạo bằng file PhienDauTu.sql).</summary>
    internal class PhienDauTuSQL
    {
        private const string TrangThaiDangChay = "DangChay";
        private const string TrangThaiKetThuc = "KetThuc";

        // Phiên đang chạy của người dùng (null nếu chưa có)
        public PhienDauTu LayPhienDangChay(string tenDangNhap)
        {
            const string truyVan = @"
                SELECT TOP 1 MaPhien, TenDangNhap, VonBanDau, TienMat, LaiLoDaChot, ThoiGianBatDau, ThoiGianKetThuc
                FROM PhienDauTu
                WHERE TenDangNhap = @TenDangNhap AND TrangThai = @TrangThai
                ORDER BY MaPhien DESC";

            using (SqlConnection con = KetNoi.taoketnoi())
            using (var cmd = new SqlCommand(truyVan, con))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@TrangThai", TrangThaiDangChay);
                con.Open();

                using (SqlDataReader doc = cmd.ExecuteReader())
                {
                    if (!doc.Read()) return null;

                    return new PhienDauTu
                    {
                        MaPhien = doc.GetInt32(0),
                        TenDangNhap = doc.GetString(1),
                        VonBanDau = doc.GetDecimal(2),
                        TienMat = doc.GetDecimal(3),
                        LaiLoDaChot = doc.GetDecimal(4),
                        ThoiGianBatDau = doc.GetDateTime(5),
                        ThoiGianKetThuc = doc.GetDateTime(6)
                    };
                }
            }
        }

        // Tạo phiên mới, trả về mã phiên
        public int TaoPhien(string tenDangNhap, decimal von, DateTime batDau, DateTime ketThuc)
        {
            const string truyVan = @"
                INSERT INTO PhienDauTu (TenDangNhap, VonBanDau, TienMat, LaiLoDaChot, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
                VALUES (@TenDangNhap, @Von, @Von, 0, @BatDau, @KetThuc, @TrangThai);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection con = KetNoi.taoketnoi())
            using (var cmd = new SqlCommand(truyVan, con))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@Von", von);
                cmd.Parameters.AddWithValue("@BatDau", batDau);
                cmd.Parameters.AddWithValue("@KetThuc", ketThuc);
                cmd.Parameters.AddWithValue("@TrangThai", TrangThaiDangChay);
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        // Cổ phiếu đang giữ trong phiên: MaCP, SoLuong, GiaVonTB, GiaLucMua, TongGiaMua
        public DataTable LayDanhMuc(int maPhien)
        {
            const string truyVan = @"
                SELECT MaCP, SoLuong, GiaVonTB, GiaLucMua, TongGiaMua
                FROM DanhMucPhien
                WHERE MaPhien = @MaPhien";

            var bang = new DataTable();
            using (SqlConnection con = KetNoi.taoketnoi())
            using (var adapter = new SqlDataAdapter(truyVan, con))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@MaPhien", maPhien);
                adapter.Fill(bang);
            }
            return bang;
        }

        // Lưu tiền mặt, lãi lỗ đã chốt và toàn bộ danh mục hiện tại (trong một giao dịch)
        public void LuuTrangThai(int maPhien, decimal tienMat, decimal laiLoDaChot, DataTable danhMuc)
        {
            using (SqlConnection con = KetNoi.taoketnoi())
            {
                con.Open();
                using (SqlTransaction giaoDich = con.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE PhienDauTu SET TienMat = @TienMat, LaiLoDaChot = @LaiLo WHERE MaPhien = @MaPhien",
                            con, giaoDich))
                        {
                            cmd.Parameters.AddWithValue("@TienMat", tienMat);
                            cmd.Parameters.AddWithValue("@LaiLo", laiLoDaChot);
                            cmd.Parameters.AddWithValue("@MaPhien", maPhien);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand(
                            "DELETE FROM DanhMucPhien WHERE MaPhien = @MaPhien", con, giaoDich))
                        {
                            cmd.Parameters.AddWithValue("@MaPhien", maPhien);
                            cmd.ExecuteNonQuery();
                        }

                        foreach (DataRow dong in danhMuc.Rows)
                        {
                            using (var cmd = new SqlCommand(
                                @"INSERT INTO DanhMucPhien (MaPhien, MaCP, SoLuong, GiaVonTB, GiaLucMua, TongGiaMua)
                                  VALUES (@MaPhien, @MaCP, @SoLuong, @GiaVonTB, @GiaLucMua, @TongGiaMua)",
                                con, giaoDich))
                            {
                                cmd.Parameters.AddWithValue("@MaPhien", maPhien);
                                cmd.Parameters.AddWithValue("@MaCP", dong["Ma CP"].ToString());
                                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(dong["So Luong"]));
                                cmd.Parameters.AddWithValue("@GiaVonTB", Convert.ToDecimal(dong["Gia Von TB"]));
                                cmd.Parameters.AddWithValue("@GiaLucMua", Convert.ToDecimal(dong["Gia Luc Mua"]));
                                cmd.Parameters.AddWithValue("@TongGiaMua", Convert.ToDecimal(dong["Tong Gia Mua"]));
                                cmd.ExecuteNonQuery();
                            }
                        }

                        giaoDich.Commit();
                    }
                    catch
                    {
                        giaoDich.Rollback();
                        throw;
                    }
                }
            }
        }

        // Đánh dấu phiên đã kết thúc, ghi kết quả và xóa danh mục của phiên
        public void KetThucPhien(int maPhien, decimal giaTriCuoi, decimal laiLo, decimal phanTram)
        {
            const string truyVan = @"
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;
                UPDATE PhienDauTu
                SET TrangThai = @TrangThai, TienMat = @GiaTriCuoi, GiaTriCuoi = @GiaTriCuoi,
                    LaiLo = @LaiLo, PhanTram = @PhanTram, ThoiGianChot = GETDATE()
                WHERE MaPhien = @MaPhien;
                DELETE FROM DanhMucPhien WHERE MaPhien = @MaPhien;
                COMMIT TRANSACTION;";

            using (SqlConnection con = KetNoi.taoketnoi())
            using (var cmd = new SqlCommand(truyVan, con))
            {
                cmd.Parameters.AddWithValue("@TrangThai", TrangThaiKetThuc);
                cmd.Parameters.AddWithValue("@GiaTriCuoi", giaTriCuoi);
                cmd.Parameters.AddWithValue("@LaiLo", laiLo);
                cmd.Parameters.AddWithValue("@PhanTram", phanTram);
                cmd.Parameters.AddWithValue("@MaPhien", maPhien);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Lịch sử các phiên của người dùng (mới nhất ở trên)
        public DataTable LayLichSu(string tenDangNhap)
        {
            const string truyVan = @"
                SELECT MaPhien AS [Phiên],
                       ThoiGianBatDau AS [Bắt đầu],
                       ThoiGianKetThuc AS [Kết thúc],
                       VonBanDau AS [Vốn],
                       GiaTriCuoi AS [Tổng tài sản cuối],
                       LaiLo AS [Lãi / Lỗ],
                       PhanTram AS [% lãi / lỗ],
                       CASE TrangThai WHEN N'DangChay' THEN N'Đang chạy' ELSE N'Đã kết thúc' END AS [Trạng thái]
                FROM PhienDauTu
                WHERE TenDangNhap = @TenDangNhap
                ORDER BY MaPhien DESC";

            var bang = new DataTable();
            using (SqlConnection con = KetNoi.taoketnoi())
            using (var adapter = new SqlDataAdapter(truyVan, con))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                adapter.Fill(bang);
            }
            return bang;
        }
    }
}
