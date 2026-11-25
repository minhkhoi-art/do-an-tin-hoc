/*
  Tạo 2 bảng cho tính năng "Phiên đầu tư thử".
  Chạy file này MỘT LẦN trong SQL Server Management Studio (có thể chạy lại, không mất dữ liệu).
*/
USE [QuanLyCoPhieu]
GO

-- Mỗi dòng là một phiên đầu tư của một người dùng
IF OBJECT_ID(N'dbo.PhienDauTu', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhienDauTu (
        MaPhien         INT IDENTITY(1,1) PRIMARY KEY,
        TenDangNhap     NVARCHAR(100)  NOT NULL,
        VonBanDau       DECIMAL(18,2)  NOT NULL,
        TienMat         DECIMAL(18,2)  NOT NULL,                -- tiền mặt hiện có (cập nhật sau mỗi lệnh)
        LaiLoDaChot     DECIMAL(18,2)  NOT NULL DEFAULT 0,      -- lãi/lỗ của các lệnh bán trong phiên
        ThoiGianBatDau  DATETIME       NOT NULL,
        ThoiGianKetThuc DATETIME       NOT NULL,
        TrangThai       NVARCHAR(20)   NOT NULL DEFAULT N'DangChay',  -- DangChay hoặc KetThuc
        GiaTriCuoi      DECIMAL(18,2)  NULL,                    -- tổng tài sản khi kết thúc
        LaiLo           DECIMAL(18,2)  NULL,                    -- GiaTriCuoi - VonBanDau
        PhanTram        DECIMAL(18,2)  NULL,                    -- % lãi/lỗ so với vốn
        ThoiGianChot    DATETIME       NULL                     -- lúc chương trình chốt phiên
    )
END
GO

-- Cổ phiếu đang giữ trong từng phiên (để mở lại chương trình vẫn còn)
IF OBJECT_ID(N'dbo.DanhMucPhien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DanhMucPhien (
        MaPhien     INT           NOT NULL REFERENCES dbo.PhienDauTu(MaPhien),
        MaCP        NVARCHAR(20)  NOT NULL,
        SoLuong     INT           NOT NULL,
        GiaVonTB    DECIMAL(18,4) NOT NULL,
        GiaLucMua   DECIMAL(18,4) NOT NULL,
        TongGiaMua  DECIMAL(18,2) NOT NULL,
        PRIMARY KEY (MaPhien, MaCP)
    )
END
GO
