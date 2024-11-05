using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace QuanLyNGK.View
{

    public partial class frmStaffview : Form
    {
        
        String query;
        public frmStaffview()
        {
            InitializeComponent();
        }


        ketnoiSQL ketnoiSQL = new ketnoiSQL();
        private void frmStaffview_Load_1(object sender, EventArgs e)
        {
            
            ketnoiSQL.getConnection();      
            guna2DataGridView2.DataSource = ketnoiSQL.GetTable("NhanVien");
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            frmStaffAdd frmStaffAdd = new frmStaffAdd();
            frmStaffAdd.ShowDialog();
            frmStaffview_Load_1(sender, e);
        }

        private void guna2DataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (guna2DataGridView2.CurrentCell.OwningColumn.Name == "delete")
            {
               
                string maNV = guna2DataGridView2.Rows[e.RowIndex].Cells["Column1"].Value.ToString();

               
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên có mã: " + maNV + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ketnoiSQL.DeleteNhanVien(maNV);
                    
                    frmStaffview_Load_1(this, EventArgs.Empty);
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
                    int age = Convert.ToInt32(row.Cells["Column3"].Value);
                    string gender = row.Cells["Column4"].Value.ToString();

                    // Tạo một đối tượng mới của frmStaffadd
                    frmStaffAdd frm = new frmStaffAdd();


                    // Gán giá trị cho các biến thành viên của frmStaffadd

                    frm.id = id;
                    frm.name = name;
                    frm.phone = phone;
                    frm.addr = addr;
                    frm.age = age;
                    frm.gender = gender;

                    // Gọi phương thức UpdateInfo từ đối tượng frmStaffadd
                    frm.UpdateInfo();
                    frm.ShowDialog();
                    frmStaffview_Load_1(this, EventArgs.Empty);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            DataTable resultTable = ketnoiSQL.SearchNhanVienByName(searchKeyword);

            guna2DataGridView2.DataSource = resultTable;
        }
    }
}
