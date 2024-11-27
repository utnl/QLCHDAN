using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyNGK
{
    public partial class QLDAN_DT : DbContext
    {
        public QLDAN_DT()
            : base("name=DoanhThu")
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<HoaDonBanHang> HoaDonBanHangs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.maSP)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.DonGia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<HoaDonBanHang>()
                .Property(e => e.tenNV)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBanHang>()
                .Property(e => e.TongTien)
                .HasPrecision(10, 2);

            modelBuilder.Entity<HoaDonBanHang>()
                .Property(e => e.maKH)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBanHang>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDonBanHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.maSP)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.maNCC)
                .IsUnicode(false);
        }
    }
}
