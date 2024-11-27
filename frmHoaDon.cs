using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNGK
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }
        ketnoiSQL db = new ketnoiSQL();
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource = db.GetTable("HoaDonBanHang");
        }
        
        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView2.Columns["info"].Index && e.RowIndex >= 0)
            {
                // Lấy giá trị của mã hóa đơn từ hàng tương ứng
                string maHD = guna2DataGridView2.Rows[e.RowIndex].Cells["maHD"].Value.ToString();

                // Truy vấn thông tin chi tiết hóa đơn từ cơ sở dữ liệu
                string query = "SELECT * FROM ChiTietHoaDon WHERE MaHD = @MaHD";

                // Tạo kết nối và thực thi truy vấn
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-5I8I5M5T\SQLEXPRESS;Initial Catalog=QLDAN;Integrated Security=True"))
                {
                    try
                    {
                        connection.Open();

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@MaHD", maHD);

                        SqlDataReader reader = cmd.ExecuteReader();

                        // Dùng StringBuilder để tạo chuỗi thông tin chi tiết hóa đơn
                        StringBuilder sb = new StringBuilder();

                        // Kiểm tra nếu có dữ liệu trả về
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sb.AppendLine("Mã sản phẩm: " + reader["MaSP"].ToString());
                                sb.AppendLine("Tên sản phẩm: " + reader["tenSP"].ToString());
                                sb.AppendLine("Số lượng: " + reader["SoLuong"].ToString());
                                sb.AppendLine("Đơn giá: " + reader["DonGia"].ToString());
                                sb.AppendLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            }
                        }
                        else
                        {
                            sb.AppendLine("Không có chi tiết hóa đơn cho mã hóa đơn này.");
                        }

                        // Hiển thị thông tin chi tiết hóa đơn
                        MessageBox.Show(sb.ToString(), "Thông tin chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi khi kết nối hoặc truy vấn cơ sở dữ liệu
                        MessageBox.Show("Lỗi khi truy vấn thông tin chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
    }
}

