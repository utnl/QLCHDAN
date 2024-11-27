using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNGK
{
    internal class DoanhThuRP
    {
        public int MaHD { get; set; }

        // Ngày lập hóa đơn
        public string NgayLapHD { get; set; }

        // Tên nhân viên (username từ Account)
        public string TenNV { get; set; }

        // Tổng tiền hóa đơn
        public decimal TongTien { get; set; }

        // Mã khách hàng (mặc định: '....')
        public string MaKH { get; set; } = "....";
    }
}
