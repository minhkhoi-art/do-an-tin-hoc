IF DB_ID(N'KhoLuuTruCoPhieuSQL') IS NULL
BEGIN
CREATE Database KhoLuuTruCoPhieuSQL;
END
GO
use KhoLuuTruCoPhieuSQL
Go
-- 1. Bảng lưu danh sách mã chứng khoán
CREATE TABLE DanhSachMaCK (
    MaCK VARCHAR(10) PRIMARY KEY,
    TenCongTy NVARCHAR(200),
    SanGiaoDich VARCHAR(10) -- HOSE, HNX, UPCOM
);
-- 2. Bảng lưu dữ liệu giá bảng điện realtime mới nhất
CREATE TABLE KhoLuuTruCoPhieuSQL (
    MaCK VARCHAR(10) PRIMARY KEY,
    GiaThamChieu DECIMAL(18, 2),
    GiaTran DECIMAL(18, 2),
    GiaSan DECIMAL(18, 2),
    GiaKhop DECIMAL(18, 2),
    KhoiLuongKhop BIGINT,
    ThayDoi DECIMAL(18, 2),
    PhanTramThayDoi DECIMAL(18, 2),
    ThoiGianCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaCK) REFERENCES DanhSachMaCK(MaCK)
);

-- 3. Bảng lưu thông tin tài khoản người dùng (Hỗ trợ đăng nhập & số dư)
CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhauMaHoA VARCHAR(MAX), -- Lưu mật khẩu mã hóa RSA từ file text hoặc database
    SoDuTienMat DECIMAL(18, 2) DEFAULT 1000000.00 -- Mặc định vốn ban đầu 1 triệu
);

-- 4. Bảng lưu danh mục cổ phiếu mà người dùng đang sở hữu
CREATE TABLE DanhMucSoHuu (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50),
    MaCK VARCHAR(10),
    SoLuong INT,
    GiaVonTB DECIMAL(18, 2),
    FOREIGN KEY (TenDangNhap) REFERENCES TaiKhoan(TenDangNhap),
    FOREIGN KEY (MaCK) REFERENCES DanhSachMaCK(MaCK)
);
GO
-- Nạp sẵn dữ liệu mẫu mã cổ phiếu
INSERT INTO DanhSachMaCK (MaCK, TenCongTy, SanGiaoDich) VALUES
('FPT', N'Công ty Cổ phần FPT', 'HOSE'),
('VHM', N'Công ty Cổ phần Vinhomes', 'HOSE'),
('VIC', N'Tập đoàn Vingroup', 'HOSE'),
('MBB', N'Ngân hàng TMCP Quân Đội', 'HOSE');

INSERT INTO KhoLuuTruCoPhieuSQL(MaCK, GiaThamChieu, GiaTran, GiaSan, GiaKhop, KhoiLuongKhop, ThayDoi, PhanTramThayDoi) VALUES
('FPT', 90.0, 96.3, 83.7, 90.0, 0, 0, 0),
('VHM', 42.5, 45.4, 39.6, 42.5, 0, 0, 0),
('VIC', 40.0, 42.8, 37.2, 40.0, 0, 0, 0),
('MBB', 24.0, 25.6, 22.4, 24.0, 0, 0, 0);
GO

