using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace đồ_án_tin_học_1
{
    public partial class fDatLenh : Form
    {
        private decimal VonBanDau = 1000000m;
        private DataTable marketTable;
        private DataTable DanhMucSoHuuTable;
        private BangGia bangGia;
        private decimal total;
        private decimal GiaBinhQuanMoi;
        private int quantity;
        public int SOLUONGCP
        {
            get { return quantity;  }
        }
        public decimal GIABINHQUAN
        {
            get { return GiaBinhQuanMoi; }
        }
        public decimal Tonggiatridautu
        {
            get { return total; }
        }

        #region hiểu thị số dư 
      
        public fDatLenh()
        {
            InitializeComponent();
            bangGia = dulieuchung.Banggia;
            //ToString("N0"): Chuyển số thành chuỗi có dấu phân cách hàng nghìn
            //không hiển thị phần thập phân .
            lblSoDu.Text="So Du Tien Mat: "+bangGia.SoDu.ToString("N0")+" VND";
        }
        #endregion
        private void label2_Click(object sender, EventArgs e)
        {

        }
        // cập nhật lại : giá hiện tại của 1 cổ phiếu : Tổng giá khi bán : tổng lãi lỗ khi bán
        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (DataRow pRow in bangGia.DanhMucTable.Rows)
            {
                string mack = pRow["Ma CP"].ToString();
                decimal giahientai = 0;
                // lấy số lượng hiện có 
                int soluong = Convert.ToInt32(pRow["So Luong"]);
                foreach (DataRow row in bangGia.MarketTable.Rows)
                {
                    if (mack == row["Ma CK"].ToString())
                    {
                        // lấy giá hiện tại của cổ phiếu
                        giahientai = Convert.ToDecimal(row["Giá hiện tại"]);
                        pRow["Gia Hien Tai"] = giahientai;
                        break;
                    }
                }
                // tính tổng khi bán
                decimal tong_gia_khi_ban = soluong * giahientai * 100; ;
                pRow["Tổng Giá Khi Bán"] = tong_gia_khi_ban;
                decimal tong_gia_khi_mua = Convert.ToDecimal(pRow["Tong Gia Mua"]);
                // tính tổng lãi lỗ nếu bán =  tổng khi bán - tổng khi mua
                decimal tong_lai_lo_neu_ban = tong_gia_khi_ban - tong_gia_khi_mua;
                pRow["Tổng Lãi Lỗ Nếu Bán"] = tong_lai_lo_neu_ban;
            }
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
           
            //1. Kiểm tra xem người dùng đã chọn mã và nhập số lượng chưa
        // nếu mã cổ phiếu chưa được chọn || dòng số lượng cổ phiếu chưa được nhập số lượng
            if (cbMaCP.SelectedItem == null || string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui Lòng Nhập Mã Cổ Phiếu và Số Lượng ", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //lấy mã : ví dụ : symbol = "fpt" ;
            string symbol = cbMaCP.SelectedItem.ToString();

            // int.TryParse : thử chuyển từ chuỗi sanng số : vd : "10" , "50" từ chuỗi sang số được
            // out int quantity : Nếu chuyển đổi thành công :
            // giá trị số nguyên sẽ được đưa vào biến quantity

            // ĐIỀU KIỆN TRONG IF CÓ NGHĨA LÀ : 
            // NẾU NHẬP SỐ LƯỢNG MÀ ( "KHÔNG" ) BIẾN TỪ CHUỖI SANG SỐ ĐƯỢC || SỐ LƯỢNG BẠN NHẬP <= 0
            // VÀ TRONG QUÁ TRÌNH XÉT ĐIỀU KIỆN CỦA IF : "QUANTITY CŨNG SẼ ĐƯỢC GÁN TRÁ TRỊ LUÔN" 
            if (!int.TryParse(txtSoLuong.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("So luong phai la so nguyen lon hon khong", "loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            // 2. Lấy giá hiện tại của mã cổ phiếu đó từ bảng điện (marketTable)
            decimal giahientai = 0;
            // duyệt từng dòng trong bảng MarketTable
            foreach (DataRow row in bangGia.MarketTable.Rows)
            {
                // lấy đúng dòng : có chuỗi giống với dòng của người dùng chọn 
                if (row["Ma CK"].ToString() == symbol)
                {
                    // giá hiện tại = giá ở trong dòng của người dùng đã chọn
                    giahientai = Convert.ToDecimal(row["Giá hiện tại"]);
                    break;
                }
                ;
            }
        
            // 3. Bắt đầu mua 
            // tính tổng giá = giá hiện tại của cổ phiếu ng dùng chọn * số lượng người dùng nhập
            total = giahientai * quantity * 100;  //1 lô =100CP

            // Xử lý logic Mua (BUY) hoặc Bán (SELL)
            if (rbtnMua.Checked)
            {
                // nếu số dư bé < tổng giá trị muốn mua
                if (bangGia.SoDu < total)
                {
                    MessageBox.Show("So du tai khoang khong du", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // mua xong : số dư cập nhật
                bangGia.SoDu -= total;

                // cập nhật giá hiện tại của 1 cổ phiếu và tính giá khi bán 
                Timer timer = new Timer();
                timer.Interval = 3000;
                timer.Tick += timer_Tick;
                timer.Start();

                // kiểm tra cổ phiếu đã có trong danh mục chưa
                bool found = false;
                // duyệt từng dòng trong bảng Sở hữu cổ phiếu 
                foreach (DataRow pRow in bangGia.DanhMucTable.Rows)
                {
                    // nếu có dòng mã trùng với dòng người dùng chọn
                    if (pRow["Ma CP"].ToString() == symbol)
                    {
                        // lấy số lượng cũ cổ phiếu của dòng có mã cổ phiếu ng dùng chon
                        int soluongcu = Convert.ToInt32(pRow["So Luong"]);
                        // lấy giá vốn trung bình cũ 
                        decimal GiavonTBCu = Convert.ToDecimal(pRow["Gia Von TB"]);
                        // NHẤN MUA THÊM = sẽ cộng dồn vô
                        int soluongmoi = soluongcu + quantity;
                        // tính giá bình quân mới
                        GiaBinhQuanMoi = ((soluongcu * GiavonTBCu) + (quantity * giahientai)) / soluongmoi;
                        // cập nhât tổng giá mua mới 
                        total = soluongmoi * giahientai * 100;
                        // cập nhật số lượng mới
                        pRow["So Luong"] = soluongmoi;
                        // cập nhật giá vốn tb mới = math.round(giabinhquanmoi,2) : làm tròn 2 chữ số
                        pRow["Gia Von TB"] = GiaBinhQuanMoi;
                        pRow["Gia Luc Mua"] = giahientai;
                        pRow["Tong Gia Mua"] += total.ToString("N0");

                        // đánh dấu đã tìm thấy cổ phiếu
                        found = true;
                        break;
                    }
                }
                // nếu mà MUA CÁI MÃ CỔ PHIẾU ĐÓ LẦN ĐẦU thì add dòng mới vào BẢNG
                if (!found)
                {
                    GiaBinhQuanMoi = giahientai;
                    // lãi lỗ khi phần trăm giảm : chưa tính bán chỉ là tạm tính của 1 giá cổ phiếu 
                    decimal lailocua1cophieu = Math.Round(total - giahientai, 2);
                    bangGia.DanhMucTable.Rows.Add(symbol, quantity, GiaBinhQuanMoi , giahientai , giahientai ,total,total,0m);
                }
                MessageBox.Show($"Mua thanh cong{quantity}, co phieu {symbol}, voi gia 1 co phieu la : {giahientai}! ", "Thanh Cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (rbtnBan.Checked)
            {
                
                bool CoPhieuSoHuu = false;
                // dò trong danh mục sở hữu cổ phiếu
                foreach (DataRow pRow in bangGia.DanhMucTable.Rows)
                {
                    // tìm mã ng dùng nhập
                    if (pRow["Ma CP"].ToString() == symbol)
                    {
                        // xem số lượng cổ phiếu sỡ hữu
                        int soluongsohuu = Convert.ToInt32(pRow["So Luong"]);

                        // kiểm tra xem số lượng có sẵn có đủ số lượng người dùng muốn bán không
                        if (soluongsohuu < quantity)
                        { 
                            // không đủ -> hiện thông báo
                            MessageBox.Show(" So luong CP khong du de ban ", "Thong Bao ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        
                        // nếu đủ : thì cập nhật lại số lượng
                        pRow["So Luong"] = soluongsohuu - quantity;
                        pRow["Gia Hien Tai"] = giahientai;

                        // khi bán thành công sẽ tính tổng lãi lỗ : tttk
                        decimal giavonTB = Convert.ToDecimal(pRow["Gia Von TB"].ToString());
                        decimal loi_lo_lan_mua_ban_nay = (giahientai - giavonTB) * quantity * 100;

                        // tính tổng lãi lỗ của tất cả các cổ phiếu có trong bảng : gộp lại
                        bangGia.LaiLo += loi_lo_lan_mua_ban_nay;
                        
                        //cong tien sau khi ban 
                        bangGia.SoDu += total;
                        MessageBox.Show($"Ban thanh cong{quantity}, co phieu {symbol}, voi gia {giahientai}! ", "Thanh Cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // true ở đây : nghĩa là đã tìm thấy cổ phiếu 
                        CoPhieuSoHuu = true;

                        //Nếu bán hết cổ phiếu thì xóa khỏi bảng 
                        if (Convert.ToInt32(pRow["So Luong"]) == 0)
                        {
                            // xoá khỏi bảng -> nếu mà muốn bán nữa : sẽ không thể tìm được MÃ 
                            bangGia.DanhMucTable.Rows.Remove(pRow);
                        }
                        break;
                    }

                }
                // nếu nó không có trong bảng 
                if (!CoPhieuSoHuu)
                {
                    MessageBox.Show("Ban Khong So Huu Co Phieu Nay ", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                
            }
            lblSoDu.Text = "So Du Tien Mat: " + bangGia.SoDu.ToString("N0") + "VND";

        }

        private void fDatLenh_Load(object sender, EventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
