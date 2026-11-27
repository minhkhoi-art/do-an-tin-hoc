using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace đồ_án_tin_học_1
{
    internal class KhoLuuTruSQL
    {
        public readonly string ChuoiKetNoi =
              @"Data Source=.\SQLEXPRESS;
              Initial Catalog=KhoLuuTruCoPhieuSQL;
              Integrated Security=True;
              TrustServerCertificate=True;";
        public List<string> LayDanhSachMaTheoDoi()
        {
            var list = new List<string>();
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            {
                conn.Open();
                string truyvan = "SELECT MaCK FROM DanhSachMaCK";
                using (SqlCommand cmd = new SqlCommand(truyvan, conn))
                {
                    using (SqlDataReader NguoiDoc = cmd.ExecuteReader())
                    {
                        while (NguoiDoc.Read())
                        {
                            list.Add(NguoiDoc["MaCK"].ToString().Trim());
                        }

                    }

                }
            }
            return list;
        }
        // 2. Lấy dữ liệu bảng giá khởi tạo để đổ vào DataTable cho DataGridView

        public DataTable LayBangGia()
        {
            DataTable dt = new DataTable();
            using (SqlConnection coon = new SqlConnection(ChuoiKetNoi))
            {
                coon.Open();
                string truyvan = @"SELECT MaCK AS [Ma CK], 
                                        GiaThamChieu AS [Giá tham chiếu], 
                                        GiaTran AS [Giá trần], 
                                        GiaSan AS [Giá sàn], 
                                        GiaKhop AS [Giá hiện tại], 
                                        PhanTramThayDoi AS [% biến động] 
                                 FROM KhoLuuTruCoPhieuSQL";
                using (SqlDataAdapter adapter = new SqlDataAdapter(truyvan, coon))
                {
                    adapter.Fill(dt);
                }

            }
            return dt;
        }
        // 3. Cập nhật giá realtime từ FireAnt API hoặc luồng giả lập xuống SQL Server
        public async Task CapNhatGiaRealTimeAsync(string maCk, decimal giakhop, decimal thaydoi, decimal phantram)
        {
            using (SqlConnection coon = new SqlConnection(ChuoiKetNoi))
            {
                await coon.OpenAsync();
                string truyvan = @"UPDATE KhoLuuTruCoPhieuSQL 
                                 SET GiaKhop = @GiaKhop,
                                     ThayDoi = @ThayDoi,
                                     PhanTramThayDoi = @PhanTram,
                                     ThoiGianCapNhat = GETDATE()
                                 WHERE MaCK = @MaCK";
                using (SqlCommand cmd = new SqlCommand(truyvan, coon))
                {
                    cmd.Parameters.AddWithValue("@MaCK", maCk);
                    cmd.Parameters.AddWithValue("@GiaKhop", giakhop);
                    cmd.Parameters.AddWithValue("@ThayDoi", thaydoi);
                    cmd.Parameters.AddWithValue("@PhanTram", phantram);
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }
    }
}
