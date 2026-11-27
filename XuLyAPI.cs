using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace đồ_án_tin_học_1
{
    internal class XuLyAPI
    {
        private static readonly HttpClient httpClient = new HttpClient();
        //Lấy thông tin của Fireant 
        public async Task<BaoGiaCoPhieu> LayGiaCoPhietAsync(string symbol)
        {

            try
            {
                // BẮT BUỘC: Bật chuẩn bảo mật TLS 1.2 cho .NET Framework cũ để không bị chặn kết nối HTTPS
                //ServicePointManager: quản lý các kết nối mạng trong.NET.
                //SecurityProtocol: thuộc tính quy định giao thức bảo mật.
                //Tls12: yêu cầu sử dụng TLS 1.2.
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                if (!httpClient.DefaultRequestHeaders.UserAgent.Any())
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                }

                // URL API mẫu lấy thông tin giá và giao dịch hiện tại của mã cổ phiếu trên 
                string url = "https://" + "bgapidatafeed.vps.com.vn/getliststockdata/" + symbol.ToUpper();
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode) return null;

                string jsonResponse = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(jsonResponse)) return null;

                // Thêm dòng này để in toàn bộ JSON trả về từ FireAnt ra cửa sổ Output
                System.Diagnostics.Debug.WriteLine($"JSON trả về cho {symbol}: " + jsonResponse);

                JToken token = JToken.Parse(jsonResponse);

                // Map dữ liệu từ JSON của FireAnt vào mô hình StockQuote
                JObject data = null;

                if (token is JArray arr && arr.Count > 0)
                {
                    data = arr[0] as JObject;
                }
                else if (token is JObject obj)
                {
                    data = obj;
                }

                if (data == null) return null;


                BaoGiaCoPhieu quote = new BaoGiaCoPhieu
                {
                    //    MaCK = symbol.ToUpper(),
                    //GiaThamChieu = LayDecimal(data["BasicPrice"] ?? data["ReferencePrice"] ?? data["LastPrice"]),
                    //GiaTran = LayDecimal(data["Ceiling"]),
                    //GiaSan = LayDecimal(data["Floor"]),
                    //GiaKhop = LayDecimal(data["Price"] ?? data["MatchPrice"] ?? data["LastPrice"]),
                    //KhoiLuongKhop = LayLong(data["TotalVolume"] ?? data["Volume"]),
                    //ThoiGianCapNhat = DateTime.Now
                    MaCK = symbol.ToUpper(),
                    GiaThamChieu = LayDecimal(data["r"]),
                    GiaTran = LayDecimal(data["c"]),
                    GiaSan = LayDecimal(data["f"]),
                    GiaKhop = LayDecimal(data["lastPrice"]),
                    KhoiLuongKhop = LayLong(data["lot"]) * 10,
                    ThoiGianCapNhat = DateTime.Now
                };


                // Nếu chưa có giá khớp (ví dụ ngoài giờ giao dịch hoặc chưa khớp lệnh), lấy tạm giá tham chiếu
                if (quote.GiaKhop <= 0)
                {
                    quote.GiaKhop = quote.GiaThamChieu;
                }

                quote.ThayDoi = quote.GiaKhop - quote.GiaThamChieu;

                if (quote.GiaThamChieu > 0)
                {
                    quote.PhanTramThayDoi = Math.Round((quote.ThayDoi / quote.GiaThamChieu) * 100, 2);
                }
                else
                {
                    quote.PhanTramThayDoi = 0;
                }
                return quote;
            }
            catch (Exception ex)
            {
                string chiTietLoi = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                System.Diagnostics.Debug.WriteLine($"Lỗi API cho mã {symbol}: {ex.Message}");
                return null;
            }

        }
        private decimal LayDecimal(JToken value)
        {
            if (value == null) { return 0m; }
            decimal.TryParse(value.ToString(), out decimal ketQua);
            return ketQua;
        }
        private long LayLong(JToken value)
        {
            if (value == null) { return 0l; }
            long.TryParse(value.ToString(), out long ketQua);
            return ketQua;

        }

    }
}
