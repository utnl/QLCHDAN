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
	diaChi NVARCHAR(50)
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
  maHD varchar(20) PRIMARY KEY NOT NULL,
  ngayLapHD datetime NOT NULL,
  tenNV VARCHAR(20) NOT NULL,
  TongTien DECIMAL(10,2) NOT NULL,
  TrangThai NVARCHAR(255) NOT NULL,
  maKH VARCHAR(100) default null,
  FOREIGN KEY (tenNV) REFERENCES Account(username)
);
CREATE TABLE HoaDonNhapHang (
    maHDNH INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ngayLapHDNH datetime NOT NULL,
    maNCC VARCHAR(20) NOT NULL,
    maSP VARCHAR(20)NOT NULL ,
	tenSP Nvarchar(20)NOT NULL,
    soLuong INT NOT NULL,
    donGia int NOT NULL,
    tongTien AS (soLuong * donGia) PERSISTED,
    TrangThai NVARCHAR(255),
    FOREIGN KEY (maNCC) REFERENCES NhaCungUng(maNCC),  
);
GO



CREATE TABLE ChiTietHoaDon (
  maHDCT INT PRIMARY KEY IDENTITY(1,1),
  maHD varchar(20) NOT NULL,
  maSP varchar(20) NOT NULL,
  soLuong INT NOT NULL,
  DonGia DECIMAL(10,2) NOT NULL,
  tenSP NVARCHAR(50) NOT NULL,
  FOREIGN KEY (maHD) REFERENCES HoaDonBanHang(maHD),
);
GO
INSERT INTO Account (username, password)
VALUES ('admin', 'admin'),
       ('lqviet', '123123'),
       ('nhquang', '123123');

-- Bảng NhanVien
INSERT INTO NhanVien (maNV, tenNV, tuoi, gioiTinh, sdt, diaChi)
VALUES ('NV001', N'Nguyễn Văn Thành', 30, N'Nam', '0123456789', 'Hà Nội'),
       ('NV002', N'Lê Q Việt', 25, N'Nữ', '0987654321', 'Hồ Chí Minh');


