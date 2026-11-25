# đồ án tin học-1

Ứng dụng mô phỏng đầu tư cổ phiếu (WinForms, .NET Framework 4.7.2, SQL Server).

## Cài đặt
1. Chạy các file SQL trong SQL Server: `QuanLyCoPhieu.sql`, `KhoLuuTruCoPhieuSQL.sql`, rồi `PhienDauTu.sql`.
2. Sửa tên máy chủ SQL trong `KetNoi.cs` (hằng `MayChu`).
3. Mở file `.sln` bằng Visual Studio và chạy.

## Phiên đầu tư thử
- Sau khi đăng nhập, chọn số vốn và thời gian đầu tư rồi bấm **Bắt đầu phiên**.
- Mua/bán cổ phiếu trong thời gian của phiên. Tiền mặt và danh mục được lưu vào SQL nên tắt app mở lại vẫn còn.
- Hết giờ, chương trình tự bán hết cổ phiếu theo giá hiện tại và hiện bảng tổng kết lãi/lỗ. Lịch sử các phiên xem ở mục **Phiên đầu tư**.
- Nếu phiên hết giờ khi chương trình đang tắt, phiên được chốt theo giá tại thời điểm bạn mở lại.
