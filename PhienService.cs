using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace đồ_án_tin_học_1
{
    /// <summary>
    /// Điều phối vòng đời một phiên đầu tư: bắt đầu, khôi phục khi mở lại, lưu sau mỗi lệnh,
    /// và kết thúc (bán hết theo giá hiện tại, tính lãi/lỗ).
    /// Tiền mặt và danh mục vẫn nằm trong BangGia (SoDu, DanhMucTable) như trước.
    /// </summary>
    internal static class PhienService
    {
        private const int CoPhieuMoiLo = 100; // 1 lô = 100 cổ phiếu

        private static readonly PhienDauTuSQL sql = new PhienDauTuSQL();

        public static PhienDauTu Hien
        {
            get { return dulieuchung.PhienHienTai; }
        }

        // Có phiên và chưa hết giờ -> được phép đặt lệnh
        public static bool DangChay
        {
            get { return Hien != null && !Hien.DaHetGio; }
        }

        #region Bắt đầu / khôi phục / lưu

        private static void KiemTraDaDangNhap()
        {
            if (string.IsNullOrEmpty(dulieuchung.TenDangNhap))
            {
                throw new InvalidOperationException("Chưa đăng nhập. Hãy đăng nhập bằng form đăng nhập trước.");
            }
        }

        // Mở lại chương trình: nạp phiên đang chạy (nếu có) vào BangGia. Trả về null nếu chưa có phiên.
        public static PhienDauTu KhoiPhuc(BangGia bg)
        {
            KiemTraDaDangNhap();
            PhienDauTu phien = sql.LayPhienDangChay(dulieuchung.TenDangNhap);
            dulieuchung.PhienHienTai = phien;
            if (phien == null) return null;

            bg.SoDu = phien.TienMat;
            bg.LaiLo = phien.LaiLoDaChot;
            bg.DanhMucTable.Rows.Clear();

            foreach (DataRow dong in sql.LayDanhMuc(phien.MaPhien).Rows)
            {
                int soLuong = Convert.ToInt32(dong["SoLuong"]);
                decimal giaVon = Convert.ToDecimal(dong["GiaVonTB"]);
                decimal giaLucMua = Convert.ToDecimal(dong["GiaLucMua"]);
                decimal tongGiaMua = Convert.ToDecimal(dong["TongGiaMua"]);
                decimal tongGiaKhiBan = giaLucMua * soLuong * CoPhieuMoiLo;

                // Giá hiện tại tạm lấy giá lúc mua, sẽ được tính lại khi có giá mới
                bg.DanhMucTable.Rows.Add(
                    dong["MaCP"].ToString(),
                    soLuong,
                    giaVon,
                    giaLucMua,
                    giaLucMua,
                    tongGiaMua,
                    tongGiaKhiBan,
                    tongGiaKhiBan - tongGiaMua);
            }
            return phien;
        }

        // Bắt đầu phiên mới với số vốn và thời lượng đã chọn
        public static PhienDauTu BatDau(BangGia bg, decimal von, TimeSpan thoiLuong)
        {
            if (Hien != null)
            {
                throw new InvalidOperationException("Bạn đang có một phiên đầu tư chưa kết thúc.");
            }

            KiemTraDaDangNhap();
            DateTime batDau = DateTime.Now;
            DateTime ketThuc = batDau + thoiLuong;
            int maPhien = sql.TaoPhien(dulieuchung.TenDangNhap, von, batDau, ketThuc);

            var phien = new PhienDauTu
            {
                MaPhien = maPhien,
                TenDangNhap = dulieuchung.TenDangNhap,
                VonBanDau = von,
                TienMat = von,
                LaiLoDaChot = 0,
                ThoiGianBatDau = batDau,
                ThoiGianKetThuc = ketThuc
            };

            bg.SoDu = von;
            bg.LaiLo = 0;
            bg.DanhMucTable.Rows.Clear();
            dulieuchung.PhienHienTai = phien;
            dulieuchung.FDL = null;   // form đặt lệnh sẽ được tạo lại với số dư mới
            return phien;
        }

        // Lưu tiền mặt và danh mục hiện tại xuống SQL (gọi sau mỗi lệnh mua/bán)
        public static void Luu(BangGia bg)
        {
            if (Hien == null) return;
            sql.LuuTrangThai(Hien.MaPhien, bg.SoDu, bg.LaiLo, bg.DanhMucTable);
        }

        #endregion

        #region Kết thúc phiên

        // Hết giờ: bán hết cổ phiếu theo giá hiện tại, tính lãi/lỗ và lưu kết quả
        public static async Task<KetQuaPhien> KetThucAsync(BangGia bg)
        {
            PhienDauTu phien = Hien;
            if (phien == null)
            {
                throw new InvalidOperationException("Không có phiên đầu tư nào để kết thúc.");
            }

            Dictionary<string, decimal> giaTuSql = LayGiaTuSQL();
            var xuLyApi = new XuLyAPI();

            var chiTiet = new DataTable();
            chiTiet.Columns.Add("Mã CP", typeof(string));
            chiTiet.Columns.Add("Số lượng", typeof(int));
            chiTiet.Columns.Add("Giá vốn TB", typeof(decimal));
            chiTiet.Columns.Add("Giá bán", typeof(decimal));
            chiTiet.Columns.Add("Lãi / Lỗ", typeof(decimal));

            decimal tongTienBan = 0;

            // Chụp lại danh sách dòng trước vì bên trong vòng lặp có await
            List<DataRow> cacDong = bg.DanhMucTable.Rows.Cast<DataRow>().ToList();
            foreach (DataRow dong in cacDong)
            {
                string maCP = dong["Ma CP"].ToString();
                int soLuong = Convert.ToInt32(dong["So Luong"]);
                decimal giaVon = Convert.ToDecimal(dong["Gia Von TB"]);

                decimal giaBan = await LayGiaChotAsync(maCP, giaVon, bg, giaTuSql, xuLyApi);

                tongTienBan += giaBan * soLuong * CoPhieuMoiLo;
                decimal laiLoCoPhieu = (giaBan - giaVon) * soLuong * CoPhieuMoiLo;
                chiTiet.Rows.Add(maCP, soLuong, giaVon, giaBan, laiLoCoPhieu);
            }

            // Tổng tài sản cuối = tiền mặt còn lại + tiền thu được khi bán hết
            decimal giaTriCuoi = bg.SoDu + tongTienBan;
            decimal laiLo = giaTriCuoi - phien.VonBanDau;
            decimal phanTram = phien.VonBanDau > 0
                ? Math.Round(laiLo / phien.VonBanDau * 100, 2)
                : 0;

            sql.KetThucPhien(phien.MaPhien, giaTriCuoi, laiLo, phanTram);

            bg.SoDu = giaTriCuoi;
            bg.LaiLo = laiLo;
            bg.DanhMucTable.Rows.Clear();
            dulieuchung.PhienHienTai = null;
            dulieuchung.FDL = null;

            return new KetQuaPhien
            {
                VonBanDau = phien.VonBanDau,
                GiaTriCuoi = giaTriCuoi,
                LaiLo = laiLo,
                PhanTram = phanTram,
                ThoiGianBatDau = phien.ThoiGianBatDau,
                ThoiGianKetThuc = phien.ThoiGianKetThuc,
                ChiTiet = chiTiet
            };
        }

        // Giá chốt của một mã, ưu tiên: API -> bảng giá đang mở -> giá lưu trong SQL -> giá vốn
        private static async Task<decimal> LayGiaChotAsync(
            string maCP, decimal giaVon, BangGia bg, Dictionary<string, decimal> giaTuSql, XuLyAPI xuLyApi)
        {
            BaoGiaCoPhieu baoGia = await xuLyApi.LayGiaCoPhietAsync(maCP);
            if (baoGia != null && baoGia.GiaKhop > 0) return baoGia.GiaKhop;

            foreach (DataRow dong in bg.MarketTable.Rows)
            {
                if (dong["Ma CK"].ToString() == maCP)
                {
                    decimal gia = Convert.ToDecimal(dong["Giá hiện tại"]);
                    if (gia > 0) return gia;
                    break;
                }
            }

            decimal giaSql;
            if (giaTuSql.TryGetValue(maCP, out giaSql) && giaSql > 0) return giaSql;

            return giaVon;
        }

        private static Dictionary<string, decimal> LayGiaTuSQL()
        {
            var ketQua = new Dictionary<string, decimal>();
            try
            {
                DataTable bang = new KhoLuuTruSQL().LayBangGia();
                foreach (DataRow dong in bang.Rows)
                {
                    decimal gia;
                    if (decimal.TryParse(dong["Giá hiện tại"].ToString(), out gia))
                    {
                        ketQua[dong["Ma CK"].ToString().Trim()] = gia;
                    }
                }
            }
            catch (Exception)
            {
                // Không đọc được SQL thì bỏ qua, dùng các nguồn giá khác
            }
            return ketQua;
        }

        #endregion
    }
}
