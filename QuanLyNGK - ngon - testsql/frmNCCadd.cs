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
    public partial class frmNCCadd : Form
    {
        public frmNCCadd()
        {
            InitializeComponent();
        }
        public string id, name, addr, phone;
        ketnoiSQL db = new ketnoiSQL();
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string maNCC = txtID.Text.Trim();
            string tenNCC = txtName.Text.Trim();
            string SDT = txtPhone.Text.Trim();
            string diaChi = txtAddr.Text.Trim();

            if (guna2Button1.Text == "Add") 
            {
                if (!string.IsNullOrEmpty(maNCC) && !string.IsNullOrEmpty(tenNCC) && !string.IsNullOrEmpty(SDT) && !string.IsNullOrEmpty(diaChi))
                {
                   
                    if (db.AddNCC(maNCC, tenNCC, SDT, diaChi))
                    {
                        MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields(); 
                    
                    }
                    else
                    {
                        MessageBox.Show("Thêm nhà cung cấp thất bại. Hãy kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập đủ thông tin!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (guna2Button1.Text == "Update")
            {
                if (!string.IsNullOrEmpty(maNCC) && !string.IsNullOrEmpty(tenNCC) && !string.IsNullOrEmpty(SDT) && !string.IsNullOrEmpty(diaChi))
                {
                  
                    if (db.UpdateNCC(maNCC, tenNCC, SDT, diaChi))
                    {
                        MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                      
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật nhà cung cấp thất bại. Hãy kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập đủ thông tin!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        public void UpdateInfo()
        {
            label6.Text = "Edit Infomation Supplier";
            guna2Button1.Text = "Update";
            txtID.Text = id;
            txtID.ReadOnly = true;
            txtName.Text = name;
            txtPhone.Text = phone;
            txtAddr.Text = addr;
        }
        private void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtAddr.Clear();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
