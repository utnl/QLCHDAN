using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyNGK
{
    public partial class frmRPdoanhthu : Form
    {
        public frmRPdoanhthu()
        {
            InitializeComponent();
        }

        private void frmRPdoanhthu_Load(object sender, EventArgs e)
        {
            // Thêm các tháng vào ComboBox
            for (int i = 1; i <= 12; i++)
            {
                cbThang.Items.Add($"Tháng {i}");
            }

            // Chọn tháng hiện tại mặc định
            cbThang.SelectedIndex = DateTime.Now.Month - 1;

            // Hiển thị doanh thu tháng hiện tại
            ThongKeDoanhThuTheoThang(DateTime.Now.Month);
        }


        private void ThongKeDoanhThuTheoThang(int selectedMonth)
        {
            using (QLDAN_DT context = new QLDAN_DT())
            {
                int currentYear = DateTime.Now.Year;

                // Lọc hóa đơn theo tháng và năm hiện tại
                List<HoaDonBanHang> listHD = context.HoaDonBanHangs
                    .Where(hd => hd.ngayLapHD.Month == selectedMonth && hd.ngayLapHD.Year == currentYear)
                    .ToList();

                // Tạo danh sách cho báo cáo
                List<DoanhThuRP> listDT = listHD.Select(hd => new DoanhThuRP
                {
                    MaHD = hd.maHD,
                    NgayLapHD = hd.ngayLapHD.ToString("dd/MM/yyyy"),
                    TongTien = hd.TongTien,
                    TenNV = hd.tenNV,
                    MaKH = hd.maKH ?? "...."
                }).ToList();

                // Cập nhật dữ liệu cho ReportViewer
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\RPdoanhthu.rdlc";
                var source = new ReportDataSource("doanhthuDataSet", listDT);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(source);

                // Hiển thị lại báo cáo
                reportViewer1.RefreshReport();
            }
        }

        private void cbThang_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Lấy tháng từ ComboBox
            int selectedMonth = cbThang.SelectedIndex + 1;

            // Thống kê doanh thu theo tháng
            ThongKeDoanhThuTheoThang(selectedMonth);
        }
    }
}
