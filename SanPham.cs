namespace QuanLyNGK
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        [StringLength(20)]
        public string maSP { get; set; }

        [Required]
        [StringLength(20)]
        public string loaiSP { get; set; }

        [Required]
        [StringLength(50)]
        public string tenSP { get; set; }

        [Required]
        [StringLength(20)]
        public string maNCC { get; set; }

        public int soLuong { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaySX { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayHH { get; set; }

        public int giaSP { get; set; }

        public byte[] hinhanh { get; set; }
    }
}
