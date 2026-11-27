using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace đồ_án_tin_học_1
{
    public partial class frmLichSuGiaoDich : Form
    {
        public frmLichSuGiaoDich()
        {
            InitializeComponent();
            CaiDatBieuDo();
            NapDuLieuBieuDo();
            hienthidulieu();
            hienthingay();
            hienthitong();

        }
        #region chỉnh màu 3 nút : tất cả , mua , bán : khi ấn sẽ đổi màu
        private void ChonNut(Button nutDuocChon)
        {
            btnTatCa.BackColor = Color.FromArgb(18, 43, 78);
            btnMua.BackColor = Color.FromArgb(18, 43, 78);
            btnBan.BackColor = Color.FromArgb(18, 43, 78);

            nutDuocChon.BackColor = Color.FromArgb(35, 115, 245);
        }

        private void hienthingay()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
                {
                    string sql = @"select NgayBatDauDauTu , ThoiHanDauTu from TaiKhoan where TenDangNhap = @TenDangNhap";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime tungay = Convert.ToDateTime(reader["NgayBatDauDauTu"]);
                            int thoihan = Convert.ToInt32(reader["ThoiHanDauTu"]);
                            lblTuNgay.Text = "Ngày Bắt Đầu : "+tungay.ToString();
                            DateTime denngay = tungay.AddMonths(thoihan);
                            lblDenNgay.Text = "Ngày Kết Thúc : "+ denngay.ToString();
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(" Lỗi không tìm thấy ngày thiết lập : " + ex.Message);
            }
        }
        private void hienthidulieu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
                {
                    string sql = @"select NgayGiaoDich , MaCK ,LoaiLenh,SoLuong,Gia,TongTien,LaiLo from LichSuGiaoDich where TenDangNhap = @TenDangNhap";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
                    conn.Open();
                    // Tạo bảng nhận dữ liệu
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu SQL vào DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    // Đưa dữ liệu lên DataGridView
                    dgvLichSuGD.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử giao dịch : " + ex.Message);
            }
        }
        private void hienthitong()
        {
            if (string.IsNullOrWhiteSpace(dulieuchung.tenDangNhap))
            {
                MessageBox.Show("Không tìm thấy tài khoản đăng nhập!!!hienthitong");
                return;
            }

            else {
                try
                {
                    using(SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
                    {
                        string sql = @"select Count(*) from LichSuGiaoDich where TenDangNhap = @TenDangNhap";
                        string sqltongtienmua = @"select SUM(TongTien) from LichSuGiaoDich where TenDangNhap=@TenDangNhap and LoaiLenh ='BUY' ";
                        string sqltongtienban = @"select SUM(TongTien) from LichSuGiaoDich where TenDangNhap=@TenDangNhap and LoaiLenh ='SELL' ";
                        string sqltonglailo = @"select SUM(LaiLo) from LichSuGiaoDich where TenDangNhap=@TenDangNhap  ";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlCommand cmdmua = new SqlCommand(sqltongtienmua, conn);
                        SqlCommand cmdban = new SqlCommand(sqltongtienban, conn);
                        SqlCommand cmdlailo = new SqlCommand(sqltonglailo, conn);

                        cmd.Parameters.AddWithValue( "@TenDangNhap", dulieuchung.tenDangNhap);
                        cmdmua.Parameters.AddWithValue( "@TenDangNhap", dulieuchung.tenDangNhap);
                        cmdban.Parameters.AddWithValue( "@TenDangNhap", dulieuchung.tenDangNhap);
                        cmdlailo.Parameters.AddWithValue( "@TenDangNhap", dulieuchung.tenDangNhap);
                        
                        conn.Open();

                        int tongsolenh = Convert.ToInt32(cmd.ExecuteScalar());

                        object kqmua = cmdmua.ExecuteScalar();
                        decimal tongtienmua = (kqmua == null || kqmua == DBNull.Value) ? 0 : Convert.ToDecimal(kqmua);

                        object kqban = cmdban.ExecuteScalar();
                        decimal tongtienban = (kqban==null||kqban==DBNull.Value)? 0 : Convert.ToDecimal(kqban);

                        object lailo = cmdlailo.ExecuteScalar();
                        decimal LaiLo = (lailo == null || lailo == DBNull.Value) ? 0 : Convert.ToDecimal(lailo);

                        lblTongLenh.Text = tongsolenh.ToString();
                        lblTongMua.Text = tongtienmua.ToString("N0");
                        lblTongBan.Text = tongtienban.ToString("N0");
                        lblLaiLo.Text = LaiLo.ToString("N0");
                        decimal bggiatri = Convert.ToDecimal(lblLaiLo.Text);
                        if(bggiatri > 0)
                        {
                            lblBDGiaTri.Text = "+" + bggiatri.ToString("N0");
                            lblBDGiaTri.ForeColor= Color.FromArgb(34, 197, 94);
                        }
                        else if(bggiatri == 0)
                        {
                            lblBDGiaTri.Text =  bggiatri.ToString("N0");
                            lblBDGiaTri.ForeColor = Color.FromArgb(148, 163, 184);
                        }
                        else
                        {
                            lblBDGiaTri.Text = bggiatri.ToString("N0");
                            lblBDGiaTri.ForeColor = Color.FromArgb(239, 68, 68);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(" Lỗi  : " + ex.Message);
                }
            }

        }
        private void btnTatCa_Click(object sender, EventArgs e)
        {
            ChonNut(btnTatCa);
            if (string.IsNullOrWhiteSpace(dulieuchung.tenDangNhap))
            {
                MessageBox.Show("Không tìm thấy tài khoản đăng nhập!");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
                {
                    string sql = @"select NgayGiaoDich , MaCK ,LoaiLenh,SoLuong,Gia,TongTien,LaiLo from LichSuGiaoDich where TenDangNhap = @TenDangNhap";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
                    conn.Open();
                    // Tạo bảng nhận dữ liệu
                    DataTable dt = new DataTable();

                // Đổ dữ liệu SQL vào DataTable 
                // SqlDataAdapter:  đổ kết quả câu SELECT thẳng vào DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    // Đưa dữ liệu lên DataGridView
                    dgvLichSuGD.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử giao dịch : " + ex.Message);
            }
        }
        private void btnMua_Click(object sender, EventArgs e)
        {
            ChonNut(btnMua);
            if (string.IsNullOrWhiteSpace(dulieuchung.tenDangNhap))
            {
                MessageBox.Show("Không tìm thấy tài khoản đăng nhập!");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
                {

                    string sql = @"select NgayGiaoDich , MaCK ,LoaiLenh,SoLuong,Gia,TongTien,LaiLo from LichSuGiaoDich where TenDangNhap = @TenDangNhap and LoaiLenh = 'BUY'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter apt = new SqlDataAdapter(cmd))
                    {
                        apt.Fill(dt);
                    }
                    dgvLichSuGD.DataSource = dt;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử giao dịch lúc mua : " + ex.Message);
            }
        }
        private void btnBan_Click(object sender, EventArgs e)
        {
            ChonNut(btnBan);
            if (string.IsNullOrWhiteSpace(dulieuchung.tenDangNhap))
            {
                MessageBox.Show("Không tìm thấy tài khoản đăng nhập!");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKetNoi))
                {

                    string sql = @"select NgayGiaoDich , MaCK ,LoaiLenh,SoLuong,Gia,TongTien,LaiLo from LichSuGiaoDich where TenDangNhap = @TenDangNhap and LoaiLenh = 'SELL'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", dulieuchung.tenDangNhap);
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter apt = new SqlDataAdapter(cmd))
                    {
                        apt.Fill(dt);
                    }
                    dgvLichSuGD.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử giao dịch lúc bán : "+ex.Message);

            }
        }
        #endregion
        private void CaiDatBieuDo()
        {
            // 1. Màu sắc
            Color nen = Color.FromArgb(15, 29, 61);
            Color luoi = Color.FromArgb(35, 50, 82);
            Color chu = Color.FromArgb(148, 163, 184);

            Color mauDuong = Color.FromArgb(160, 210, 190);
            Color mauVung = Color.FromArgb(55, 70, 190, 130);

            // 2. Cấu hình Chart
            chartLaiLo.BackColor = nen;
            chartLaiLo.BorderlineWidth = 0;
            chartLaiLo.AntiAliasing = AntiAliasingStyles.All;
            chartLaiLo.TextAntiAliasingQuality =
                TextAntiAliasingQuality.High;

            // Xóa biểu đồ cũ
            chartLaiLo.Series.Clear();
            chartLaiLo.Legends.Clear();
            chartLaiLo.ChartAreas.Clear();

            // 3. Tạo vùng biểu đồ
            ChartArea ca = chartLaiLo.ChartAreas.Add("ca");
            ca.BackColor = nen;

            // 4. Trục X
            ca.AxisX.LabelStyle.ForeColor = chu;
            ca.AxisX.LabelStyle.Font = new Font("Segoe UI", 9F);
            ca.AxisX.LineColor = luoi;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisX.MajorTickMark.Enabled = false;
            ca.AxisX.Interval = 1;
            ca.AxisX.LabelStyle.Angle = -25;

            // 5. Ẩn trục Y bên trái
            ca.AxisY.Enabled = AxisEnabled.False;

            // 6. Trục Y bên phải
            ca.AxisY2.Enabled = AxisEnabled.True;
            ca.AxisY2.LabelStyle.ForeColor = chu;
            ca.AxisY2.LabelStyle.Font = new Font("Segoe UI", 9F);
            ca.AxisY2.LabelStyle.Format = "#,##0; -#,##0";
            ca.AxisY2.LineColor = nen;
            ca.AxisY2.MajorGrid.LineColor = luoi;
            ca.AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            ca.AxisY2.MajorTickMark.Enabled = false;
            ca.AxisY2.IsStartedFromZero = false;

            // 7. Đường ngang tại mức 0
            ca.AxisY2.StripLines.Add(new StripLine
            {
                IntervalOffset = 0,
                StripWidth = 0,
                BorderColor = Color.FromArgb(80, 110, 140),
                BorderDashStyle = ChartDashStyle.Dash
            });

            Series s = chartLaiLo.Series.Add("LaiLo");

            s.ChartArea = "ca";
            s.ChartType = SeriesChartType.Point;

            s.XValueType = ChartValueType.Int32;
            s.YAxisType = AxisType.Secondary;

            // Series này chỉ lưu dữ liệu, không tự vẽ đường hoặc vùng
            s.MarkerStyle = MarkerStyle.None;
            s.IsVisibleInLegend = false;
        }
        private void VeDoanTheoHuong(double x1, double y1, double x2, double y2, string tip, ref int soDoan)
        {
            Color mau;

            if (y2 > y1)
                mau = Color.FromArgb(34, 197, 94);    // lên: xanh
            else if (y2 < y1)
                mau = Color.FromArgb(239, 68, 68);    // xuống: đỏ
            else
                mau = Color.FromArgb(148, 163, 184);  // ngang: xám

            ThemDoanDuong(x1, y1, x2, y2, mau, tip, soDoan++);
        }
        private void ThemDoanDuong(double x1, double y1, double x2, double y2, Color mau, string tip, int soDoan)
        {
            // 1. Vùng màu bên dưới đoạn
            Series vung = chartLaiLo.Series.Add("VungLaiLo" + soDoan);
            vung.ChartArea = "ca";
            vung.ChartType = SeriesChartType.Area;
            vung.XValueType = ChartValueType.Int32;
            vung.YAxisType = AxisType.Secondary;
            vung.Color = Color.FromArgb(55, mau);
            vung.BorderWidth = 0;
            vung.IsVisibleInLegend = false;
            vung.ToolTip = tip;                 // trỏ vào vùng màu cũng hiện thông tin

            vung.Points.AddXY(x1, y1);
            vung.Points.AddXY(x2, y2);

            // 2. Đường nối hai điểm
            Series doan = chartLaiLo.Series.Add("DoanLaiLo" + soDoan);
            doan.ChartArea = "ca";
            doan.ChartType = SeriesChartType.Line;
            doan.XValueType = ChartValueType.Int32;
            doan.YAxisType = AxisType.Secondary;
            doan.Color = mau;
            doan.BorderWidth = 2;
            doan.MarkerStyle = MarkerStyle.None;
            doan.IsVisibleInLegend = false;
            doan.ToolTip = tip;                 // trỏ vào đường cũng hiện thông tin

            // Thiếu 2 dòng này nên đường không hiện
            doan.Points.AddXY(x1, y1);
            doan.Points.AddXY(x2, y2);
        }
        private void NapDuLieuBieuDo()
        {
            Series s = chartLaiLo.Series["LaiLo"];

            s.Points.Clear();

            Color mauLai = Color.FromArgb(34, 197, 94);  // Xanh
            Color mauLo = Color.FromArgb(239, 68, 68);   // Đỏ
            Color mauHoaVon = Color.FromArgb(160, 210, 190);

            using (SqlConnection conn =
                new SqlConnection(KetNoi.chuoiKetNoi))
            {
                string sql = @"
SELECT MaGiaoDich, MaCK, NgayGiaoDich, SoLuong, Gia, ISNULL(LaiLo, 0) AS LaiLo
FROM LichSuGiaoDich
WHERE TenDangNhap = @TenDangNhap AND LoaiLenh = 'SELL'
ORDER BY NgayGiaoDich, MaGiaoDich";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap",dulieuchung.tenDangNhap);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Tạo điểm xuất phát tại mức 0
                        int diemDau = s.Points.AddXY(0, 0);
                        s.Points[diemDau].MarkerStyle = MarkerStyle.None;

                        decimal laiLoLuyKe = 0;
                        int soLenh = 0;
                        int soDoan = 0;
                        double xTruoc = 0;
                        double yTruoc = 0;
                        while (reader.Read())
                        {
                            // Chỉ đếm các lần bán
                            soLenh++;

                            string maCK = reader["MaCK"].ToString();
                            DateTime ngay = Convert.ToDateTime(reader["NgayGiaoDich"]);
                            decimal soLuong = Convert.ToDecimal(reader["SoLuong"]);
                            decimal gia = Convert.ToDecimal(reader["Gia"]);
                            decimal laiLoLanNay = Convert.ToDecimal(reader["LaiLo"]);

                            laiLoLuyKe += laiLoLanNay;

                            double x = soLenh;
                            double y = (double)laiLoLuyKe;

                            // Tooltip dùng chung cho dấu chấm và đoạn đường
                            string tip =
                                "Lệnh bán #" + soLenh
                                + "\nMã cổ phiếu: " + maCK
                                + "\nSố lượng: " + soLuong.ToString("N0")
                                + "\nGiá bán: " + gia.ToString("N0") + " VNĐ"
                                + "\nNgày bán: " + ngay.ToString("dd/MM/yyyy HH:mm:ss")
                                + "\nLãi/lỗ lần bán: " + laiLoLanNay.ToString("N0") + " VNĐ"
                                + "\nLãi/lỗ lũy kế: " + laiLoLuyKe.ToString("N0") + " VNĐ";

                            int viTri = s.Points.AddXY(x, y);
                            s.Points[viTri].AxisLabel = "Lệnh " + soLenh;
                            s.Points[viTri].ToolTip = tip;

                            // Đoạn nối từ lệnh trước đến lệnh này = đoạn của lệnh bán này
                            VeDoanTheoHuong(xTruoc, yTruoc, x, y, tip, ref soDoan);

                            xTruoc = x;
                            yTruoc = y;
                        }
                        // Tạo Series hiển thị dấu chấm giao dịch
                        Series diem = chartLaiLo.Series.Add("DiemGiaoDich");

                        diem.ChartArea = "ca";
                        diem.ChartType = SeriesChartType.Point;
                        diem.XValueType = ChartValueType.Int32;
                        diem.YAxisType = AxisType.Secondary;
                        diem.IsVisibleInLegend = false;

                        for (int i = 1; i < s.Points.Count; i++)
                        {
                            int viTri = diem.Points.AddXY(
                                s.Points[i].XValue,
                                s.Points[i].YValues[0]
                            );

                            diem.Points[viTri].MarkerStyle = MarkerStyle.Circle;
                            diem.Points[viTri].MarkerSize = 8;
                            diem.Points[viTri].MarkerColor = Color.FromArgb(160, 210, 190);
                            diem.Points[viTri].MarkerBorderColor = Color.White;
                            diem.Points[viTri].MarkerBorderWidth = 1;

                            diem.Points[viTri].ToolTip = s.Points[i].ToolTip;
                        }
                        // Đổi màu theo tổng lãi/lỗ lũy kế hiện tại
                        Color mauHienTai;

                        if (laiLoLuyKe > 0)
                        {
                            mauHienTai = mauLai;
                        }
                        else if (laiLoLuyKe < 0)
                        {
                            mauHienTai = mauLo;
                        }
                        else
                        {
                            mauHienTai = mauHoaVon;
                        }

                        // Đổi màu đường biểu đồ
                        s.BorderColor = mauHienTai;

                        // Đổi màu vùng bên dưới đường
                        s.Color = Color.FromArgb(55, mauHienTai);

                        // Đổi màu các dấu chấm
                        s.MarkerColor = mauHienTai;
                    }
                }
            }

            // 10. Điều chỉnh trục Y
            if (s.Points.Count <= 1)
                return;

            double giaNhoNhat = 0;
            double giaLonNhat = 0;

            foreach (DataPoint p in s.Points)
            {
                double giaTri = p.YValues[0];

                if (giaTri < giaNhoNhat)
                    giaNhoNhat = giaTri;

                if (giaTri > giaLonNhat)
                    giaLonNhat = giaTri;
            }

            double khoangCach = giaLonNhat - giaNhoNhat;

            double lech = khoangCach == 0? Math.Max(Math.Abs(giaLonNhat) * 0.15, 1000): khoangCach * 0.15;
            ChartArea ca = chartLaiLo.ChartAreas["ca"];
            ca.AxisY2.Minimum = giaNhoNhat - lech;
            ca.AxisY2.Maximum = giaLonNhat + lech;
        }
    }
    
}
