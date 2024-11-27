using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
<<<<<<< HEAD
using System.Data.SqlClient;
=======
>>>>>>> 40d1565d4134c341f67d6fb65bf5038f23332118
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNGK
{
    public partial class frmPOSview : Form
    {
        public frmPOSview()
        {
            InitializeComponent();
        }
        ketnoiSQL db = new ketnoiSQL();
        private void frmPOSview_Load(object sender, EventArgs e)
        {
            // Dispose các controls không còn cần thiết
            while (ProductPanel.Controls.Count > 0)
            {
                ProductPanel.Controls[0].Dispose();
            }
            LoadData();
        }
        public void LoadData()
        {

            DataTable dt = db.GetTable("SanPham");

            foreach (DataRow row in dt.Rows)
            {
                UcProduct ucProduct = new UcProduct
                {
                    id = row["maSP"].ToString(),
                    PName = row["tenSP"].ToString(),
                    PPrice = row["giaSP"].ToString() + " Đ"

                };

                // Chuyển đổi hình ảnh từ kiểu byte[]
                byte[] imageBytes = row["hinhanh"] as byte[];
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        ucProduct.PImage = Image.FromStream(ms);
                    }
                }
                else
                {
                    ucProduct.PImage = Image.FromFile(@"C:\Users\lengu\Downloads\dang-cap-nhat-san-pham-446.png");
                }

                // Gắn sự kiện chọn sản phẩm
                ucProduct.onSelect += UcProduct_onSelect;

                // Thêm sản phẩm vào ProductPanel
                this.ProductPanel.Controls.Add(ucProduct);
            }
<<<<<<< HEAD
            db.closeConnect();
=======
>>>>>>> 40d1565d4134c341f67d6fb65bf5038f23332118

        }
        private void UcProduct_onSelect(object sender, EventArgs e)
        {
            if (sender is UcProduct selectedProduct)
            {
                string productName = selectedProduct.PName;
                string productPrice = selectedProduct.PPrice.Replace(" Đ", "");
                Image productImage = selectedProduct.PImage;

                bool productExists = false;
                foreach (DataGridViewRow row in guna2DataGridView2.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == productName)
                    {
                        int currentQty = Convert.ToInt32(row.Cells[1].Value);
                        row.Cells[1].Value = currentQty + 1;
                        decimal productAmount = (currentQty + 1) * Convert.ToDecimal(productPrice);
                        row.Cells[3].Value = productAmount;
                        productExists = true;
                        break;
                    }
                }

                if (!productExists)
                {
                    guna2DataGridView2.Rows.Add(productName, 1, productPrice, productPrice);
                }

                CalculateTotal();
            }
        }
        private void CalculateTotal()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                if (!row.IsNewRow)
                {
                    totalAmount += Convert.ToDecimal(row.Cells["amount"].Value);
                }
            }

            lblTotal.Text = totalAmount.ToString();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = guna2ComboBox1.SelectedItem.ToString(); // Lấy giá trị được chọn

            // Xóa các sản phẩm hiện tại trong ProductPanel
            ProductPanel.Controls.Clear();

            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            DataTable dt = db.GetTable("SanPham");

            // Lọc sản phẩm theo loại
            foreach (DataRow row in dt.Rows)
            {
                if (row["loaiSP"].ToString() == selectedCategory)
                {
                    UcProduct ucProduct = new UcProduct
                    {
                        id = row["maSP"].ToString(),
                        PName = row["tenSP"].ToString(),
                        PPrice = row["giaSP"].ToString() + " Đ"
                    };

                    // Chuyển đổi hình ảnh từ kiểu byte[]
                    byte[] imageBytes = row["hinhanh"] as byte[];
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            ucProduct.PImage = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        ucProduct.PImage = Image.FromFile(@"C:\Users\lengu\Downloads\dang-cap-nhat-san-pham-446.png");
                    }

                    // Gắn sự kiện chọn sản phẩm
                    ucProduct.onSelect += UcProduct_onSelect;

                    // Thêm sản phẩm vào ProductPanel
                    ProductPanel.Controls.Add(ucProduct);
                }
            }
        }
<<<<<<< HEAD
     
=======

>>>>>>> 40d1565d4134c341f67d6fb65bf5038f23332118
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (UcProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }
<<<<<<< HEAD
       
        private void guna2Button2_Click(object sender, EventArgs e)//btnPay
        {
            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                string maSP = row.Cells[0].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells[1].Value);

                // Kiểm tra số lượng sản phẩm
                if (!ktrSoluongSP(maSP, soLuong))
                {
                    MessageBox.Show("Sản phẩm không đủ số lượng đáp ứng, vui lòng giảm số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng lại và không thực hiện tạo hóa đơn
                }
            }

            string maNV = fManage.user;
            string maKH = txtKh.Text.ToString();

            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-5I8I5M5T\SQLEXPRESS;Initial Catalog=QLDAN;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Kiểm tra nếu mã khách hàng đã tồn tại trong bảng KhachHang
                    string checkCustomerQuery = "SELECT COUNT(*) FROM KhachHang WHERE maKH = @maKH";
                    SqlCommand checkCustomerCmd = new SqlCommand(checkCustomerQuery, connection);
                    checkCustomerCmd.Parameters.AddWithValue("@maKH", maKH);

                    int customerExists = (int)checkCustomerCmd.ExecuteScalar();

                    // Nếu khách hàng đã tồn tại, cập nhật số lần mua
                    if (customerExists > 0)
                    {
                        string updateCustomerQuery = "UPDATE KhachHang SET solanmuahang = solanmuahang + 1 WHERE maKH = @maKH";
                        SqlCommand updateCustomerCmd = new SqlCommand(updateCustomerQuery, connection);
                        updateCustomerCmd.Parameters.AddWithValue("@maKH", maKH);
                        updateCustomerCmd.ExecuteNonQuery();
                    }                  

                    // Thêm hóa đơn vào bảng HoaDonBanHang
                    string queryHoaDon = "INSERT INTO HoaDonBanHang (ngayLapHD, tenNV, TongTien, TrangThai, maKH) " +
                                         "VALUES (@ngayLapHD, @tenNV, @TongTien, @TrangThai, @maKH); " +
                                         "SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdHoaDon = new SqlCommand(queryHoaDon, connection);
                    cmdHoaDon.Parameters.AddWithValue("@ngayLapHD", DateTime.Now);
                    cmdHoaDon.Parameters.AddWithValue("@tenNV", maNV);
                    cmdHoaDon.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(lblTotal.Text));
                    cmdHoaDon.Parameters.AddWithValue("@TrangThai", txtTrangthai.Text.ToString());
                    cmdHoaDon.Parameters.AddWithValue("@maKH", maKH); // Có thể là null

                    // Lấy maHD vừa được tạo ra
                    int maHD = Convert.ToInt32(cmdHoaDon.ExecuteScalar());

                    // Thêm chi tiết hóa đơn vào bảng ChiTietHoaDon
                    foreach (DataGridViewRow row in guna2DataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            int soLuong = Convert.ToInt32(row.Cells[1].Value);
                            decimal donGia = Convert.ToDecimal(row.Cells[2].Value);
                            string tenSP = row.Cells[0].Value.ToString(); // Tên sản phẩm

                            // Lấy mã sản phẩm từ tên sản phẩm
                            string maSP = GetProductIDByName(tenSP);
                            if (string.IsNullOrEmpty(maSP))
                            {
                                MessageBox.Show("Không tìm thấy mã sản phẩm cho " + tenSP, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Dừng lại nếu không tìm thấy mã sản phẩm
                            }

                            string queryChiTiet = "INSERT INTO ChiTietHoaDon (maHD, maSP, soLuong, DonGia, tenSP) " +
                                                  "VALUES (@maHD, @maSP, @soLuong, @DonGia, @tenSP)";
                            SqlCommand cmdChiTiet = new SqlCommand(queryChiTiet, connection);
                            cmdChiTiet.Parameters.AddWithValue("@maHD", maHD);
                            cmdChiTiet.Parameters.AddWithValue("@maSP", maSP);
                            cmdChiTiet.Parameters.AddWithValue("@soLuong", soLuong);
                            cmdChiTiet.Parameters.AddWithValue("@DonGia", donGia);
                            cmdChiTiet.Parameters.AddWithValue("@tenSP", tenSP);

                            // Thực thi truy vấn thêm chi tiết hóa đơn
                            cmdChiTiet.ExecuteNonQuery();

                            // Cập nhật số lượng sản phẩm trong kho (trừ số lượng đã bán)
                            UpdateProductQuantity(maSP, soLuong); // Trừ số lượng đã bán
                        }
                    }

                    // Hiển thị thông báo thành công
                    MessageBox.Show("Hóa đơn đã được thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi trong quá trình thực thi, hiển thị thông báo lỗi
                    MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string GetProductIDByName(string productName)
        {
            string selectQuery = @"SELECT maSP FROM SanPham WHERE tenSP = @TenSP";
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-5I8I5M5T\SQLEXPRESS;Initial Catalog=QLDAN;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@TenSP", productName);
                    object result = command.ExecuteScalar();

                    // Nếu không tìm thấy, trả về null
                    return result?.ToString();
                }
            }
        }
        private bool ktrSoluongSP(string productName, int quantity)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-5I8I5M5T\SQLEXPRESS;Initial Catalog=QLDAN;Integrated Security=True"))
            {
                try
                {
                    string query = "SELECT soLuong FROM SanPham WHERE tenSP = @productName";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@productName", productName);
                    connection.Open();

                    object result = cmd.ExecuteScalar();
                    int soLuongKho = result != null ? Convert.ToInt32(result) : 0;

                    // Kiểm tra nếu số lượng trong kho đủ
                    return soLuongKho >= quantity;
                }
                catch (Exception)
                {                   
                    return false;
                }
            }
        }
        private void UpdateProductQuantity(string maSP, int soLuong)
        {
            string updateQuery = @"UPDATE SanPham SET soLuong = soLuong - @SoLuong WHERE maSP = @MaSP";
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-5I8I5M5T\SQLEXPRESS;Initial Catalog=QLDAN;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@SoLuong", soLuong);
                    command.Parameters.AddWithValue("@MaSP", maSP);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2DataGridView2.Rows.Clear();
            lblTotal.Text = "0";
            txtKh.Text = "";
            txtTrangthai.Text = "";
        }
=======
>>>>>>> 40d1565d4134c341f67d6fb65bf5038f23332118
    }
}
