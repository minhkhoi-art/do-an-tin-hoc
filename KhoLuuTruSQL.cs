using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace đồ_án_tin_học_1
{
    /// <summary>Đọc và ghi bảng giá cổ phiếu trong SQL Server.</summary>
    internal class KhoLuuTruSQL
    {
        private static readonly string ChuoiKetNoi = KetNoi.chuoiKetNoiKhoLuuTru;

        // 1. Lấy danh sách mã cổ phiếu cần theo dõi
        public List<string> LayDanhSachMaTheoDoi()
        {
            var danhSach = new List<string>();

            using (var conn = new SqlConnection(ChuoiKetNoi))
            using (var cmd = new SqlCommand("SELECT MaCK FROM DanhSachMaCK", conn))
            {
                conn.Open();
                using (SqlDataReader nguoiDoc = cmd.ExecuteReader())
                {
                    while (nguoiDoc.Read())
                    {
                        danhSach.Add(nguoiDoc["MaCK"].ToString().Trim());
                    }
                }
            }
            return danhSach;
        }

        // 2. Lấy bảng giá khởi tạo để đổ vào DataGridView
        public DataTable LayBangGia()
        {
            const string truyVan = @"
                SELECT MaCK AS [Ma CK],
                       GiaThamChieu AS [Giá tham chiếu],
                       GiaTran AS [Giá trần],
                       GiaSan AS [Giá sàn],
                       GiaKhop AS [Giá hiện tại],
                       PhanTramThayDoi AS [% biến động]
                FROM KhoLuuTruCoPhieuSQL";

            var bang = new DataTable();
            using (var conn = new SqlConnection(ChuoiKetNoi))
            using (var adapter = new SqlDataAdapter(truyVan, conn))
            {
                conn.Open();
                adapter.Fill(bang);
            }
            return bang;
        }

        // 3. Ghi giá realtime (từ API) xuống SQL Server
        public async Task CapNhatGiaRealTimeAsync(string maCk, decimal giaKhop, decimal thayDoi, decimal phanTram)
        {
            const string truyVan = @"
                UPDATE KhoLuuTruCoPhieuSQL
                SET GiaKhop = @GiaKhop,
                    ThayDoi = @ThayDoi,
                    PhanTramThayDoi = @PhanTram,
                    ThoiGianCapNhat = GETDATE()
                WHERE MaCK = @MaCK";

            using (var conn = new SqlConnection(ChuoiKetNoi))
            using (var cmd = new SqlCommand(truyVan, conn))
            {
                cmd.Parameters.AddWithValue("@MaCK", maCk);
                cmd.Parameters.AddWithValue("@GiaKhop", giaKhop);
                cmd.Parameters.AddWithValue("@ThayDoi", thayDoi);
                cmd.Parameters.AddWithValue("@PhanTram", phanTram);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
