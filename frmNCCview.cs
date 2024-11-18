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
    public partial class frmNCCview : Form
    {
        public frmNCCview()
        {
            InitializeComponent();
        }
        ketnoiSQL db=new ketnoiSQL();   
        private void frmNCCview_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource = db.GetTable("NhaCungUng");
        }

        private void btnAddNCC_Click(object sender, EventArgs e)
        {
           frmNCCadd frmNCCadd = new frmNCCadd();
            frmNCCadd.ShowDialog();
            frmNCCview_Load(sender, e);
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView2.CurrentCell.OwningColumn.Name == "delete")
            {
                string maNCC = guna2DataGridView2.Rows[e.RowIndex].Cells["Column1"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa Nhà cung ứng có mã: " + maNCC + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    db.DeleteNCC(maNCC);
                    frmNCCview_Load(sender, e);
                }
            }

            if (guna2DataGridView2.CurrentCell.OwningColumn.Name == "edit")
            {

                if (e.RowIndex >= 0) 
                {
                    DataGridViewRow row = guna2DataGridView2.Rows[e.RowIndex];

                    // Trích xuất thông tin từ hàng được chọn
                    string id = row.Cells["Column1"].Value.ToString();
                    string name = row.Cells["Column2"].Value.ToString();
                    string phone = row.Cells["Column5"].Value.ToString();
                    string addr = row.Cells["Column6"].Value.ToString();

                    // Tạo một đối tượng mới của frmStaffadd
                    frmNCCadd frm = new frmNCCadd();


                    // Gán giá trị cho các biến thành viên của frmStaffadd

                    frm.id = id;
                    frm.name = name;
                    frm.phone = phone;
                    frm.addr = addr;

                    // Gọi phương thức UpdateInfo từ đối tượng frmStaffadd
                    frm.UpdateInfo();
                    frm.ShowDialog();
                    frmNCCview_Load(sender, e);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            DataTable resultTable = db.SearchNCC(searchKeyword);

            guna2DataGridView2.DataSource = resultTable;
        }
    }
}
