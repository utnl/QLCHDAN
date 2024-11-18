using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNGK
{
    public partial class frmProductView : Form
    {
        public frmProductView()
        {
            InitializeComponent();
        }
        ketnoiSQL db = new ketnoiSQL();
        private void frmProductView_Load(object sender, EventArgs e)
        {
            ketnoiSQL ketnoiSQL = new ketnoiSQL();
            guna2DataGridView2.DataSource = ketnoiSQL.GetTable("SanPham");
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            frmProductAdd frmProductAdd = new frmProductAdd();
            frmProductAdd.ShowDialog();
            frmProductView_Load(sender, e);
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView2.CurrentCell.OwningColumn.Name == "delete")
            {
                // Lấy mã sản phẩm từ ô tại cột maSP
                string maSP = guna2DataGridView2.Rows[e.RowIndex].Cells["maSP"].Value.ToString();

                // Xác nhận việc xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa Sản phẩm có mã: " + maSP + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Gọi hàm xóa sản phẩm từ ketnoiSQL
                    bool isDeleted = db.DeleteProduct(maSP);
                    if (isDeleted)
                    {
                        MessageBox.Show("Sản phẩm đã được xóa!");
                        frmProductView_Load(this, EventArgs.Empty); // Reload lại danh sách sản phẩm
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa sản phẩm!");
                    }
                }
            }

            if (guna2DataGridView2.CurrentCell.OwningColumn.Name == "edit")
            {
                if (e.RowIndex >= 0) 
                {
                    DataGridViewRow row = guna2DataGridView2.Rows[e.RowIndex]; 
                    string id = row.Cells["maSP"].Value.ToString();
                    string name = row.Cells["Column3"].Value.ToString();
                    string mcc = row.Cells["Column2"].Value.ToString();
                    string price = row.Cells["Column5"].Value.ToString();
                    string nsx = row.Cells["Column7"].Value.ToString();
                    string nhh = row.Cells["Column8"].Value.ToString();
                    string sl = row.Cells["Column4"].Value.ToString();

                    byte[] imageData = null;
                    if (row.Cells["Column6"].Value != DBNull.Value)
                    {
                        imageData = (byte[])row.Cells["Column6"].Value;
                    }

                    // Tạo form chỉnh sửa sản phẩm và truyền dữ liệu
                    frmProductAdd frmProductAdd = new frmProductAdd();
                    frmProductAdd.nsx = nsx;
                    frmProductAdd.nhh = nhh;
                    frmProductAdd.id = id;
                    frmProductAdd.name = name;
                    frmProductAdd.price = price;
                    frmProductAdd.mcc = mcc;
                    frmProductAdd.sl = sl;
                    frmProductAdd.productImage = imageData;

                    // Cập nhật thông tin vào form chỉnh sửa
                    frmProductAdd.UpdateInfo();
                    frmProductAdd.ShowDialog(); // Hiển thị form sửa sản phẩm

                    // Reload lại danh sách sản phẩm sau khi sửa
                    frmProductView_Load(this, EventArgs.Empty);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            DataTable resultTable = db.SeachProduct(searchKeyword);

            guna2DataGridView2.DataSource = resultTable;
        }
    }
}
