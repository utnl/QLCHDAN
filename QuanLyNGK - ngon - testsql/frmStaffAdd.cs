using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuanLyNGK
{
    public partial class frmStaffAdd : Form
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }
        public void clear()
        {
            textbox1.Text = txthoten.Text = txtAge.Text = txtPhone.Text = txtAddr.Text = string.Empty;
            cbGender.SelectedIndex = -1;
        }
        public string id, name, gender, addr, phone;
        public int age;
        public void UpdateInfo()
        {
            label6.Text = "Edit Infomation Staff";
            guna2Button1.Text = "Update";
            textbox1.Text = id;
            textbox1.ReadOnly = true;
            txthoten.Text = name;
            txtPhone.Text = phone;
            txtAddr.Text = addr;
            txtAge.Text = age.ToString();
            cbGender.Text = gender;
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ketnoiSQL db = new ketnoiSQL();
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
                if (guna2Button1.Text == "Add")
                {
                    if (textbox1.Text != "" && txthoten.Text != "" && txtAge.Text != "" && cbGender.SelectedIndex != -1 && txtPhone.Text != "" && txtAddr.Text != "")
                    {
                     
                        if (db.CheckMaNVExists(textbox1.Text))
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác.");
                            return;
                        }

                      
                        if (!int.TryParse(txtAge.Text, out int age))
                        {
                            MessageBox.Show("Tuổi phải là một số nguyên hợp lệ!");
                            return;
                        }

                        bool success = db.AddNhanVien(textbox1.Text, txthoten.Text, age, cbGender.SelectedItem.ToString(), txtPhone.Text, txtAddr.Text);
                        if (success)
                        {
                            MessageBox.Show("Đã thêm dữ liệu thành công!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hãy nhập đủ thông tin!!!");
                    }
                }
                else if (guna2Button1.Text == "Update")
                {
                    if (textbox1.Text != "" && txthoten.Text != "" && txtAge.Text != "" && cbGender.SelectedIndex != -1 && txtPhone.Text != "" && txtAddr.Text != "")
                    {
                        if (!int.TryParse(txtAge.Text, out int age))
                        {
                            MessageBox.Show("Tuổi phải là một số nguyên hợp lệ!");
                            return;
                        }

                      
                        bool success = db.UpdateNhanVien(textbox1.Text, txthoten.Text, age, cbGender.SelectedItem.ToString(), txtPhone.Text, txtAddr.Text);
                        if (success)
                        {
                            MessageBox.Show("Đã cập nhật dữ liệu thành công!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hãy nhập đủ thông tin!!!");
                    }
                }
            

        }
    }
}
