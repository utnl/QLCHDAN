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
    public partial class frmCustomerAdd : Form
    {
        public frmCustomerAdd()
        {
            InitializeComponent();
        }

        ketnoiSQL db = new ketnoiSQL();

        public void clear()
        {
            txtID.Text = txtName.Text = txtPhone.Text = txtAddr.Text = string.Empty;
            cbGender.SelectedIndex = -1;
            cbType.SelectedIndex = -1;
            guna2ComboBox1.SelectedIndex = -1; // Reset số lần mua hàng
        }

        public string id, name, gender, addr, phone, type;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int sl;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2Button1.Text == "Add") 
            {
                
                if (txtID.Text != "" && cbType.SelectedIndex != -1 && cbGender.SelectedIndex != -1 && guna2ComboBox1.SelectedIndex != -1)
                {
                    
                    id = txtID.Text;
                    name = txtName.Text;
                    gender = cbGender.SelectedItem.ToString();
                    phone = txtPhone.Text;
                    addr = txtAddr.Text;
                    type = cbType.SelectedItem.ToString();
                    sl = int.Parse(guna2ComboBox1.SelectedItem.ToString());

                    if (db.AddKhachHang(id, name, gender, phone, addr, sl, type))
                    {
                        MessageBox.Show("Khách hàng đã được thêm thành công!");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Thêm khách hàng không thành công.");
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập đủ thông tin!!!");
                }
            }
            else if (guna2Button1.Text == "Update")
            {
               
                if (txtID.Text != "" && txtName.Text != "" && cbGender.SelectedIndex != -1 && txtPhone.Text != "" && txtAddr.Text != "")
                {
                   
                    id = txtID.Text;
                    name = txtName.Text;
                    gender = cbGender.SelectedItem.ToString();
                    phone = txtPhone.Text;
                    addr = txtAddr.Text;
                    type = cbType.SelectedItem.ToString();
                    sl = int.Parse(guna2ComboBox1.SelectedItem.ToString()); 

                
                    if (db.UpdateKhachHang(id, name, gender, phone, addr, sl, type))
                    {
                        MessageBox.Show("Cập nhật khách hàng thành công!");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật khách hàng không thành công.");
                    }
                }
                else
                {
                    MessageBox.Show("Hãy nhập đủ thông tin!!!");
                }
            }
        }

        public void UpdateInfo()
        {
            // Cập nhật giao diện khi ở chế độ chỉnh sửa
            label6.Text = "Edit Information Customer";
            guna2Button1.Text = "Update";
            txtID.Text = id;
            txtID.ReadOnly = true;
            txtName.Text = name;
            txtPhone.Text = phone;
            txtAddr.Text = addr;
            cbType.Text = type;
            cbGender.Text = gender;

            // Cập nhật số lần mua hàng
            if (sl == 1)
            {
                guna2ComboBox1.SelectedIndex = 0; // Ví dụ: 1 là "Một lần"
            }
            else
            {
                guna2ComboBox1.SelectedIndex = 1; // Ví dụ: 0 là "Nhiều lần"
            }
        }
    }
}
