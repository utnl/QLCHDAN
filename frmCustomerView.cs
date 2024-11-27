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
    public partial class frmCustomerView : Form
    {
        public frmCustomerView()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            frmCustomerAdd frmCustomerAdd = new frmCustomerAdd();   
            frmCustomerAdd.ShowDialog();
            frmCustomerView_Load(sender, e);
        }
        ketnoiSQL db=new ketnoiSQL();
        private void frmCustomerView_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource = db.GetTable("KhachHang");
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView2.CurrentCell.OwningColumn.Name == "delete")
            {
                
                string maKH = guna2DataGridView2.Rows[e.RowIndex].Cells["Column1"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa Khách hàng có mã: " + maKH + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                   
                    db.DeleteKhachHang(maKH);
                    frmCustomerView_Load(sender, e);
                }
            }

            if (guna2DataGridView2.CurrentCell.OwningColumn.Name == "edit")
            {

                if (e.RowIndex >= 0) 
                {
                    DataGridViewRow row = guna2DataGridView2.Rows[e.RowIndex]; 

                    string id = row.Cells["Column1"].Value.ToString();
                    string name = row.Cells["Column2"].Value.ToString();
                    string phone = row.Cells["Column5"].Value.ToString();
                    string addr = row.Cells["Column3"].Value.ToString();
                    string type = row.Cells["Type"].Value.ToString();
                    string gender = row.Cells["Column4"].Value.ToString();

                   
                    frmCustomerAdd fr = new frmCustomerAdd();


                   

                    fr.id = id;
                    fr.type = type;
                    fr.name = name;
                    fr.phone = phone;
                    fr.addr = addr;

                    fr.gender = gender;

                    fr.UpdateInfo();
                    fr.ShowDialog();
                    frmCustomerView_Load(sender, e);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            DataTable resultTable = db.SearchKhachHangByName(searchKeyword);

            guna2DataGridView2.DataSource = resultTable;
        }
    }
}
