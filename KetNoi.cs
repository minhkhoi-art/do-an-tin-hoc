
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
namespace đồ_án_tin_học_1
{
    internal class KetNoi
    {
        // @ : Giúp viết chuỗi nhiều dòng dễ dàng,
        // không cần xử lý ký tự đặc biệt như \.

        //Trusted_Connection = True Sử dụng tài khoản Windows
        //hiện tại để đăng nhập SQL Server.

        //TrustServerCertificate = True Tin cậy chứng chỉ máy chủ khi kết nối mã hóa,
        //tránh lỗi xác thực chứng chỉ trong môi trường học tập.
        public static string chuoiKetNoi =
            @"Data Source=.\SQLEXPRESS ; 
            Initial Catalog=QuanLyCoPhieu;
            Integrated Security=True;
            TrustServerCertificate=True";


        public static SqlConnection taoketnoi()
        {
            // tạo 1 đối tượng kết nối mới dựa vào thông tin ở trên
            // : để biết kết nối vào đâu  
            return new SqlConnection(chuoiKetNoi);
        }
    }
}

