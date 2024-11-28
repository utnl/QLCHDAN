CREATE DATABASE QLDAN;
GO

USE QLDAN;
GO

CREATE TABLE Account
(
    username varchar(20) PRIMARY KEY not null,
	password varchar(20) not null
);
GO

CREATE TABLE NhanVien (
    maNV VARCHAR(20) PRIMARY KEY NOT NULL,
    tenNV NVARCHAR(50) NOT NULL,
    tuoi INT NOT NULL,
    gioiTinh NVARCHAR(10) NOT NULL,
    sdt VARCHAR(20) NOT NULL,
    diaChi NVARCHAR(50) NOT NULL
);
GO
CREATE TABLE KhachHang (
    maKH VARCHAR(20) PRIMARY KEY,
    kieuKH NVARCHAR(20),
    tenKH NVARCHAR(50),
    gioiTinh NVARCHAR(10),
    sdt NVARCHAR(20),
	diaChi NVARCHAR(50),
	solanmuahang int
);


CREATE TABLE NhaCungUng(
    maNCC VARCHAR(20) PRIMARY KEY NOT NULL,
    tenNCC NVARCHAR(50) NOT NULL,
	sdtNCC NVARCHAR(15) not null
);
GO
ALTER TABLE NhaCungUng
ADD diaChi NVARCHAR(100) NOT NULL;

CREATE TABLE SanPham (
    maSP VARCHAR(20) PRIMARY KEY NOT NULL,
	loaiSP Nvarchar(20) NOT NULL,
    tenSP NVARCHAR(50) NOT NULL,
	maNCC VARCHAR(20) NOT NULL,
	soLuong INT NOT NULL DEFAULT 0,
    ngaySX DATE NOT NULL,
    ngayHH DATE NOT NULL,
    giaSP INT NOT NULL,    
	hinhanh VARBINARY(MAX) ,
    FOREIGN KEY (maNCC) REFERENCES NhaCungUng(maNCC)
);
GO

CREATE TABLE HoaDonBanHang (
  maHD INT PRIMARY KEY IDENTITY(1,1), -- Mã hóa đơn tự tăng
  ngayLapHD DATETIME NOT NULL,        -- Ngày lập hóa đơn
  tenNV VARCHAR(20) NOT NULL,         -- Tên nhân viên (username từ Account)
  TongTien DECIMAL(10,2) NOT NULL,    -- Tổng tiền hóa đơn
  TrangThai NVARCHAR(255) Default 'Hoàn tất',   -- Trạng thái hóa đơn
  maKH VARCHAR(100) DEFAULT '....',     -- Mã khách hàng (nếu có)
  FOREIGN KEY (tenNV) REFERENCES Account(username) -- Khóa ngoại liên kết với bảng Account
);



CREATE TABLE ChiTietHoaDon (
  maHDCT INT PRIMARY KEY IDENTITY(1,1), -- Mã chi tiết hóa đơn tự tăng
  maHD INT NOT NULL,                    -- Mã hóa đơn (liên kết với HoaDonBanHang)
  maSP VARCHAR(20) NOT NULL,            -- Mã sản phẩm
  soLuong INT NOT NULL,                 -- Số lượng sản phẩm
  DonGia DECIMAL(10,2) NOT NULL,        -- Đơn giá sản phẩm
  tenSP NVARCHAR(50) NOT NULL,          -- Tên sản phẩm
  FOREIGN KEY (maHD) REFERENCES HoaDonBanHang(maHD) -- Khóa ngoại liên kết với bảng HoaDonBanHang
);

INSERT INTO Account (username, password)
VALUES ('admin', 'admin');

-- Bảng NhanVien
INSERT INTO NhanVien (maNV, tenNV, tuoi, gioiTinh, sdt, diaChi)
VALUES ('NV001', N'Nguyễn Văn Thành', 30, N'Nam', '0123456789', 'Hà Nội'),
       ('NV002', N'Lê Q Việt', 25, N'Nữ', '0987654321', 'Hồ Chí Minh');


