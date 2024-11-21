using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (UcProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }
    }
}
