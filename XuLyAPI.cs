using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace đồ_án_tin_học_1
{
    /// <summary>Gọi API của VPS để lấy giá cổ phiếu.</summary>
    internal class XuLyAPI
    {
        private const string UrlApi = "https://bgapidatafeed.vps.com.vn/getliststockdata/";

        private static readonly HttpClient httpClient = new HttpClient();

        // Cấu hình chạy một lần khi lớp được dùng lần đầu
        static XuLyAPI()
        {
            // .NET Framework cũ cần bật TLS thì mới không bị chặn kết nối HTTPS
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            // Lưu ý: dòng dưới bỏ qua việc kiểm tra chứng chỉ SSL, chỉ nên dùng khi học tập
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        /// <summary>Lấy giá hiện tại của một mã. Trả về null nếu không lấy được.</summary>
        public async Task<BaoGiaCoPhieu> LayGiaCoPhietAsync(string symbol)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(UrlApi + symbol.ToUpper());
                if (!response.IsSuccessStatusCode) return null;

                string jsonResponse = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(jsonResponse)) return null;

                // In JSON trả về ra cửa sổ Output để dễ kiểm tra
                Debug.WriteLine($"JSON trả về cho {symbol}: " + jsonResponse);

                JObject data = LayDoiTuongDau(JToken.Parse(jsonResponse));
                if (data == null) return null;

                return TaoBaoGia(symbol, data);
            }
            catch (Exception ex)
            {
                string chiTietLoi = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                Debug.WriteLine($"Lỗi API cho mã {symbol}: {chiTietLoi}");
                return null;
            }
        }

        // API có thể trả về mảng hoặc một đối tượng, lấy phần tử đầu tiên trong cả hai trường hợp
        private static JObject LayDoiTuongDau(JToken token)
        {
            if (token is JArray mang)
            {
                return mang.Count > 0 ? mang[0] as JObject : null;
            }
            return token as JObject;
        }

        // Đổi JSON của VPS (r, c, f, lastPrice, lot) sang đối tượng BaoGiaCoPhieu
        private static BaoGiaCoPhieu TaoBaoGia(string symbol, JObject data)
        {
            var baoGia = new BaoGiaCoPhieu
            {
                MaCK = symbol.ToUpper(),
                GiaThamChieu = LayDecimal(data["r"]),
                GiaTran = LayDecimal(data["c"]),
                GiaSan = LayDecimal(data["f"]),
                GiaKhop = LayDecimal(data["lastPrice"]),
                KhoiLuongKhop = LayLong(data["lot"]) * 10,
                ThoiGianCapNhat = DateTime.Now
            };

            // Chưa có giá khớp (ngoài giờ giao dịch...) thì tạm lấy giá tham chiếu
            if (baoGia.GiaKhop == 0)
            {
                baoGia.GiaKhop = baoGia.GiaThamChieu;
            }

            baoGia.ThayDoi = baoGia.GiaKhop - baoGia.GiaThamChieu;
            baoGia.PhanTramThayDoi = baoGia.GiaThamChieu > 0
                ? Math.Round(baoGia.ThayDoi / baoGia.GiaThamChieu * 100, 2)
                : 0;

            return baoGia;
        }

        private static decimal LayDecimal(JToken value)
        {
            if (value == null) return 0m;
            decimal.TryParse(value.ToString(), out decimal ketQua);
            return ketQua;
        }

        private static long LayLong(JToken value)
        {
            if (value == null) return 0L;
            long.TryParse(value.ToString(), out long ketQua);
            return ketQua;
        }
    }
}
